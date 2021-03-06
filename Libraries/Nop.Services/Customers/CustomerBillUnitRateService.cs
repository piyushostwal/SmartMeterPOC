﻿using Nop.Core;
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
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Customers
{
    public partial class CustomerBillUnitRateService : ICustomerBillUnitRateService
    {
        #region Fields

        private readonly IRepository<CustomerBillUnitRate> _customerBillUnitRateRepository;
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
        public CustomerBillUnitRateService(IRepository<CustomerBillUnitRate> customerBillUnitRateRepository,
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
            this._customerBillUnitRateRepository = customerBillUnitRateRepository;
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

        public CustomerBillUnitRate GetBillingRateInformation()
        {
            return _customerBillUnitRateRepository.Table.FirstOrDefault();
            //var query = _customerBillUnitRateRepository.Table;
            //query = query.OrderByDescending(c => c.Id);
            //if (query != null)
            //    return query.FirstOrDefault();
            //return null;
        }

        public void SaveBillingRateInformation(CustomerBillUnitRate billUnitRateInfo)
        {
            if (billUnitRateInfo != null)
            {
                _customerBillUnitRateRepository.Insert(billUnitRateInfo);
                //event notification
                _eventPublisher.EntityInserted(billUnitRateInfo);
            }
        }

        #endregion

    }
}
