using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Data;
using Nop.Services.Events;
using Nop.Services.Security;
using Nop.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Customers
{
    public partial class CustomerProductDetailsService : ICustomerProductDetailsService
    {
        #region Fields

        private readonly IRepository<CustomerProductDetails> _customerProductDetailsRepository;
        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IAclService _aclService;
        private readonly CommonSettings _commonSettings;
        private readonly CatalogSettings _catalogSettings;

        #endregion

        #region Ctor
        public CustomerProductDetailsService(IRepository<CustomerProductDetails> customerProductDetailsRepository,
            IDbContext dbContext,
            IDataProvider dataProvider,
            IWorkContext workContext,
            IStoreContext storeContext,
            IEventPublisher eventPublisher,
            ICacheManager cacheManager,
            IStoreMappingService storeMappingService,
            IAclService aclService,
            CommonSettings commonSettings,
            CatalogSettings catalogSettings)
        {
            this._customerProductDetailsRepository = customerProductDetailsRepository;
            this._cacheManager = cacheManager;
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._eventPublisher = eventPublisher;
            this._storeMappingService = storeMappingService;
            this._aclService = aclService;
            this._commonSettings = commonSettings;
            this._catalogSettings = catalogSettings;
        }
        #endregion

        #region methods

        public virtual IPagedList<CustomerProductDetails> GetAllCustomerProductDetails( int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _customerProductDetailsRepository.Table;
            query = query.OrderByDescending(c => c.Id);
            var customers = new PagedList<CustomerProductDetails>(query, pageIndex, pageSize);
            return customers;
        }

        public virtual IPagedList<CustomerProductDetails> GetCustomerProductDetails(int customerId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _customerProductDetailsRepository.Table;
            query = query.Where(m => m.CustomerId == customerId);
            query = query.OrderByDescending(c => c.Id);
            var customers = new PagedList<CustomerProductDetails>(query, pageIndex, pageSize);
            return customers;
        }

        public virtual CustomerProductDetails GetCustomerProductDetailsByDeviceId(Guid deviceId)
        {
            return _customerProductDetailsRepository.Table.FirstOrDefault(m => m.DeviceId == deviceId);
        }

        public virtual void UpdateCustomerProductDetails(CustomerProductDetails customerProductDetails)
        {
            _customerProductDetailsRepository.Update(customerProductDetails);

            //event notification
            _eventPublisher.EntityUpdated(customerProductDetails);
        }

        public virtual async Task UpdateCustomerProductStatus(Guid deviceId, bool isActive)
        {
            CustomerProductDetails customerProductDetails = GetCustomerProductDetailsByDeviceId(deviceId);
            customerProductDetails.Status = isActive;
            _customerProductDetailsRepository.Update(customerProductDetails);
            await UpdateDeviceStatus(deviceId, isActive);
            //event notification
            _eventPublisher.EntityUpdated(customerProductDetails);
        }

        public virtual IPagedList<CustomerProductDetails> SearchDevices(string deviceId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _customerProductDetailsRepository.Table.Where(m => m.DeviceId.ToString().Contains(deviceId));
            query = query.OrderByDescending(c => c.DeviceId);
            var devices = new PagedList<CustomerProductDetails>(query, pageIndex, pageSize);
            return devices;
        }

        /// <summary>
        /// Update IsActive stauts for device
        /// </summary>
        /// <param name="deviceId">device ID of the particualr device</param>
        /// <param name="status">IsActive status: device will work untill the status is true.</param>
        /// <returns>bool: Is update succesfule</returns>
        public virtual async Task<bool> UpdateDeviceStatus(Guid deviceId, bool status)
        {
            string requestUrl = "https://smarthome-46be4.firebaseio.com/smartmeters/" + deviceId + ".json";

            HttpClient hc = new HttpClient();
            //var token = await GetAppTokenAsync();
            //hc.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            hc.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer");
            var method = new HttpMethod("PATCH");
            var messageBody = "{ \"isActive\": \"" + status + "\" }";
            var request = new HttpRequestMessage(method, requestUrl)
            {
                Content = new StringContent(messageBody, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage hrm = await hc.SendAsync(request);
            if (hrm.IsSuccessStatusCode)
            {
                string jsonresult = await hrm.Content.ReadAsStringAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
