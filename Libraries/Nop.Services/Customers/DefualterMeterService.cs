using Nop.Services.Logging;
using Nop.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Customers
{
    public partial class DefualterMeterService : ITask
    {
        private readonly ICustomerBillingService _customerBillingService;
        private readonly ICustomerProductDetailsService _customerProductDetailsService;
        private readonly ILogger _logger;


        public DefualterMeterService(ICustomerBillingService customerBillingService,ILogger logger,
                ICustomerProductDetailsService customerProductDetailsService)
        {
            this._customerBillingService = customerBillingService;
            this._logger = logger;
            this._customerProductDetailsService = customerProductDetailsService;
        }
        public void Execute()
        {
            try
            {
                var test = _customerBillingService.GetDefaulterCustomers();

                foreach (var item in test)
                {
                    var result = _customerProductDetailsService.UpdateCustomerProductStatus(item.DeviceId, false);
                    if (result != null)
                    {
                        _logger.Error(string.Format("Customer product status updated successfully"));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Error in fetching the Customer details"));
                
            }

            
            //throw new NotImplementedException();
        }
    }
}
