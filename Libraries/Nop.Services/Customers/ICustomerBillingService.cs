using Nop.Core;
using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Customers
{
    public partial interface ICustomerBillingService
    {
        IPagedList<CustomerBilling> GetCustomerBills(Guid deviceId, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
