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
                new { deviceId = key, 
                    consumptionList = g.Select(l => new ConsumptionList() { Consumption = l.Consumption, LoggingTime = l.LoggingTime }), 
                    solarGeneratedUnitsList = g.Select(l => new SolarGeneratedUnitsList() { LoggingTime = l.LoggingTime, SolarGeneratedUnits = l.SolarGeneratedUnits }) }).ToList();

            return Ok(formattedList);
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
        public IPagedList<SmartMeterLogByTimeInterval> GetCustomerLogsFromService(string timeInterval, int customerId, DateTime date)
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
        #endregion
    }
}
