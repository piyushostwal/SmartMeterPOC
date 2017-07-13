using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.SmartMeterLogs;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.SmartMeterLogs;
using Nop.Web.Factories;
using Nop.Web.Framework.Security.Captcha;
using Nop.Web.Models.SmartMeterLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nop.Services.Common;
using Nop.Core.Domain.Customers;

namespace Nop.Web.Controllers
{
    public class SmartMeterGraphController : ApiController
    {
        #region properties
        private readonly ISmartMeterLogService _smartMeterLogService;
        private readonly ICustomerBillUnitRateService _customerBillUnitRateService;
        private readonly ICustomerProductDetailsService _customerProductDetailsService;
        private readonly ICustomerService _customerService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IWebHelper _webHelper;
        private readonly IPermissionService _permissionService;
        private readonly ISmartMeterLogModelFactory _smartMeterLogModelFactory;
        private readonly IEventPublisher _eventPublisher;

        private readonly LocalizationSettings _localizationSettings;
        private readonly CaptchaSettings _captchaSettings;

        #endregion

        #region constructor

        public SmartMeterGraphController(ISmartMeterLogService smartMeterLogService,
                IWorkContext workContext,
                IStoreContext storeContext,
                ILocalizationService localizationService,
                IWorkflowMessageService workflowMessageService,
                IWebHelper webHelper,
                IPermissionService permissionService,
                ISmartMeterLogModelFactory smartMeterLogModelFactory,
                IEventPublisher eventPublisher,
                LocalizationSettings localizationSettings,
                CaptchaSettings captchaSettings,
                ICustomerBillUnitRateService customerBillUnitRateService,
                ICustomerProductDetailsService customerProductDetailsService,
                ICustomerService customerService)
        {
            _smartMeterLogService = smartMeterLogService;
            _workContext = workContext;
            _storeContext = storeContext;
            _localizationService = localizationService;
            _workflowMessageService = workflowMessageService;
            _webHelper = webHelper;
            _permissionService = permissionService;
            _smartMeterLogModelFactory = smartMeterLogModelFactory;
            _eventPublisher = eventPublisher;
            _localizationSettings = localizationSettings;
            _captchaSettings = captchaSettings;
            _customerBillUnitRateService = customerBillUnitRateService;
            _customerProductDetailsService = customerProductDetailsService;
            this._customerService = customerService;
        }

        #endregion

        [HttpPost]
        [Route("api/customer/graph")]
        public IHttpActionResult GetCustomerGraphData(SmartMeterLogCustomerGraphModel filterModel)
        {
            IPagedList<SmartMeterLogByTimeInterval> meterlogs = GetCustomerLogsFromService(filterModel.TimeInterval, filterModel.CustomerID, filterModel.TimeIntervalDate);
            var formattedList = meterlogs.GroupBy(l => l.DeviceID, (key, g) =>
                new
                {
                    deviceId = key,
                    consumptionList = g.Select(l => new ConsumptionList() { Consumption = l.Consumption, LoggingTime = l.LoggingTime }),
                    solarGeneratedUnitsList = g.Select(l => new SolarGeneratedUnitsList() { LoggingTime = l.LoggingTime, SolarGeneratedUnits = l.SolarGeneratedUnits })
                }).ToList();

            return Ok(formattedList);
        }

        [HttpPost]
        [Route("api/heatmap")]
        public IHttpActionResult GetHeatMapData(SmartMeterLogHeatMapModel heatMapModel)
        {
            var data = GetCustomerLogsByLocation(heatMapModel);
            var allCustomers = data.Select(c => c.CustomerId).ToArray();
            var customersObjs = _customerService.GetCustomersByIds(allCustomers);
            var customerFiltered = customersObjs.Select(c => new { customerId = c.Id, name = c.GetAttribute<string>(SystemCustomerAttributeNames.FirstName) + " " + c.GetAttribute<string>(SystemCustomerAttributeNames.LastName), address = c.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress) }).ToList();

            var formattedData = data.Select(d => new SmartMeterHeatMapResponseModel()
            {
                Consumption = d.Consumption,
                CustomerId = d.CustomerId,
                DeviceId = d.DeviceID,
                Lattitude = d.Lattitude,
                Longitude = d.Lattitude,
                MeterType = d.MeterType,
                Reading = d.Reading,
                CustomerName = customerFiltered.Where(c => c.customerId == d.CustomerId).Select(c => c.name).FirstOrDefault(),
                CustomerAddress = customerFiltered.Where(c => c.customerId == d.CustomerId).Select(c => c.address).FirstOrDefault(),
            });
            return Ok(formattedData);
        }

        // GET api/smartmetergraph
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/smartmetergraph/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/smartmetergraph
        public void Post([FromBody]string value)
        {
        }

        // PUT api/smartmetergraph/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/smartmetergraph/5
        public void Delete(int id)
        {
        }

        #region private methods

        private IPagedList<SmartMeterLogsByLocation> GetCustomerLogsByLocation(SmartMeterLogHeatMapModel heatMapModel)
        {
            if (string.IsNullOrEmpty(heatMapModel.Filter))
            {
                //var temp = _smartMeterLogService.GetMeterlogsByLocation("18.620245", "73.718760", "18.442511", "73.976252", Convert.ToDateTime("2016-01-01 00:00:00"), Convert.ToDateTime("2017-07-08 00:00:00"));
                return _smartMeterLogService.GetMeterlogsByLocation(heatMapModel.MinLattitude, heatMapModel.MinLongitude, heatMapModel.MaxLattiude, heatMapModel.MaxLongitude, heatMapModel.StartDate, heatMapModel.EndDate);
            }
            else if (heatMapModel.Filter.Equals("weekends"))
            {
                return _smartMeterLogService.GetMeterlogsByLocation(heatMapModel.MinLattitude, heatMapModel.MinLongitude, heatMapModel.MaxLattiude, heatMapModel.MaxLongitude, heatMapModel.StartDate, heatMapModel.EndDate, heatMapModel.Filter);
            }
            else if (heatMapModel.Filter.Equals("holidays"))
            {
                return _smartMeterLogService.GetMeterlogsByLocation(heatMapModel.MinLattitude, heatMapModel.MinLongitude, heatMapModel.MaxLattiude, heatMapModel.MaxLongitude, heatMapModel.StartDate, heatMapModel.EndDate, heatMapModel.Filter);
            }
            return null;

        }

        private IPagedList<SmartMeterLogByTimeInterval> GetCustomerLogsFromService(string timeInterval, int customerId, DateTime date)
        {
            switch (timeInterval)
            {
                case TimeIntervals.SFifteen:
                    return _smartMeterLogService.GetMeterlogsByCustomerId(TimeIntervals.Fifteen / 15, customerId, 0, 0, date);
                case TimeIntervals.SThirty:
                    return _smartMeterLogService.GetMeterlogsByCustomerId(TimeIntervals.Thirty / 15, customerId, 0, 0, date);
                case TimeIntervals.SFortyFive:
                    return _smartMeterLogService.GetMeterlogsByCustomerId(TimeIntervals.FortyFive / 15, customerId, 0, 0, date);
                case TimeIntervals.SSixty:
                    return _smartMeterLogService.GetMeterlogsByCustomerId(TimeIntervals.Sixty / 15, customerId, 0, 0, date);
                case TimeIntervals.SDay:
                    return _smartMeterLogService.GetMeterlogsByCustomerId(TimeIntervals.Day / 15, customerId, 0, 0, date); //24 * 60;
                case TimeIntervals.SWeek:
                    return _smartMeterLogService.GetMeterlogsByCustomerId(TimeIntervals.Week / 15, customerId, date.Month, date.Year); // 24 * 7 * 60;
                case TimeIntervals.SMonth:
                    return _smartMeterLogService.GetMeterlogsByCustomerId(TimeIntervals.Month / 15, customerId, 0, date.Year);
                case TimeIntervals.SYear:
                    return _smartMeterLogService.GetMeterlogsByCustomerId(TimeIntervals.Year / 15, customerId, 0, date.Year); //365 * 24 * 60;

                default:
                    return null;
            }
        }

        private List<SmartMeterHeatMapResponseModel> GetHeatMapSampleData()
        {
            List<SmartMeterHeatMapResponseModel> data = new List<SmartMeterHeatMapResponseModel>() {
                new SmartMeterHeatMapResponseModel(){
                    DeviceId=new Guid("03f00e0a-4661-4ba9-b6d5-0524c963da6c"),
                    Lattitude="18.531007",
                    Longitude="73.830409",
                    Consumption=9623,
                    Reading=962300,
                    CustomerName="Aslam Shrimali",
                    CustomerId=12,
                    CustomerAddress="test address",
                    MeterType="1",
                },
                new SmartMeterHeatMapResponseModel(){
                    DeviceId=new Guid("322da26e-cddd-43fb-bc38-3296ffcb6845"),
                    Lattitude="18.583008",
                    Longitude="73.782570",
                    Consumption=2718,
                    Reading=271800,
                    CustomerName="James Pan",
                    CustomerId=4,
                    CustomerAddress="test address 1",
                    MeterType="2",
                },
                new SmartMeterHeatMapResponseModel(){
                    DeviceId=new Guid("C56A2690-F588-4758-A874-BADDDA23C166"),
                    Lattitude="18.553661",
                    Longitude="73.806960",
                    Consumption=3687,
                    Reading=368700,
                    CustomerName="Piyush Ostwal",
                    CustomerId=15,
                    CustomerAddress="test address 2",
                    MeterType="1",
                }
            };

            return data;
        }
        #endregion
    }
}
