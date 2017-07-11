using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Customers;

namespace Nop.Services.Customers
{
    public partial interface ICustomerProductDetailsService
    {
        IPagedList<CustomerProductDetails> GetAllCustomerProductDetails(int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<CustomerProductDetails> GetCustomerProductDetails(int customerId, int pageIndex = 0, int pageSize = int.MaxValue);
        CustomerProductDetails GetCustomerProductDetailsByDeviceId(Guid deviceId);
        Task<bool> UpdateDeviceStatus(Guid deviceId, bool status);
        Task UpdateCustomerProductStatus(Guid deviceId, bool isActive);
        IPagedList<CustomerProductDetails> SearchDevices(string deviceId, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
