using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Security;
using Nop.Services.Authentication.External;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Web.Framework.Security.Captcha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Factories
{
    public class CustomerBillingModelFactory : ICustomerBillingModelFactory
    {
         #region Fields

        private readonly ICustomerBillingService _customerBillingService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IOpenAuthenticationService _openAuthenticationService;
        private readonly IReturnRequestService _returnRequestService;
        private readonly MediaSettings _mediaSettings;
        private readonly CaptchaSettings _captchaSettings;
        private readonly SecuritySettings _securitySettings;
        private readonly ExternalAuthenticationSettings _externalAuthenticationSettings;

        #endregion

        #region Ctor

        public CustomerBillingModelFactory(
            ICustomerBillingService customerBillingService,
            IDateTimeHelper dateTimeHelper,
            DateTimeSettings dateTimeSettings,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IGenericAttributeService genericAttributeService,
            IOpenAuthenticationService openAuthenticationService,
            IReturnRequestService returnRequestService,
            MediaSettings mediaSettings,
            CaptchaSettings captchaSettings,
            SecuritySettings securitySettings,
            ExternalAuthenticationSettings externalAuthenticationSettings)
        {
            this._customerBillingService = customerBillingService;
            this._dateTimeHelper = dateTimeHelper;
            this._dateTimeSettings = dateTimeSettings;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._openAuthenticationService = openAuthenticationService;
            this._returnRequestService = returnRequestService;
            this._mediaSettings = mediaSettings;
            this._captchaSettings = captchaSettings;
            this._securitySettings = securitySettings;
            this._externalAuthenticationSettings = externalAuthenticationSettings;
        }

        #endregion

        #region Methods

        public IPagedList<CustomerBilling> PrepareCustomerBillingPagingFilteringModel(Guid deviceId)
        {
            return _customerBillingService.GetCustomerBills(deviceId);
        }

        
        #endregion
    }
}