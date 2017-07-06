using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.SmartMeterLogs;
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
    public class SmartMeterLogController : ApiController
    {
        #region properties
        private readonly ISmartMeterLogService _smartMeterLogService;
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

        public SmartMeterLogController(ISmartMeterLogService smartMeterLogService,
                IWorkContext workContext,
                IStoreContext storeContext,
                ILocalizationService localizationService,
                IWorkflowMessageService workflowMessageService,
                IWebHelper webHelper,
                IPermissionService permissionService,
                ISmartMeterLogModelFactory smartMeterLogModelFactory,
                IEventPublisher eventPublisher,
                LocalizationSettings localizationSettings,
                CaptchaSettings captchaSettings)
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
        }

        #endregion

        // GET api/smartmeterlog
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/smartmeterlog/5
        public IHttpActionResult Get(Guid id)
        {
            var logs = _smartMeterLogModelFactory.PrepareSmartMeterLogPagingFilteringModel(id);
            return Ok(logs);
        }

        // POST api/smartmeterlog
        public IHttpActionResult Post([FromBody]SmartMeterLogModel smartMeterLogModel)
        {
            SmartMeterLog smartMeterLog = new SmartMeterLog();
            smartMeterLog.DeviceID = smartMeterLogModel.DeviceID;
            smartMeterLog.Lattitude = smartMeterLogModel.Lattitude;
            smartMeterLog.Longitude = smartMeterLogModel.Longitude;
            smartMeterLog.Consumption = smartMeterLogModel.Consumption;
            smartMeterLog.TimeInterval = smartMeterLogModel.TimeInterval;
            smartMeterLog.TimeIntervalSetTime = smartMeterLogModel.TimeIntervalSetTime;
            smartMeterLog.IsActive = smartMeterLogModel.IsActive;
            smartMeterLog.LoggingTime = smartMeterLogModel.LoggingTime;
            var data = _smartMeterLogService.SaveMeterLog(smartMeterLog);
            return Ok(data);
        }

        // PUT api/smartmeterlog/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/smartmeterlog/5
        public void Delete(int id)
        {
        }
    }
}
