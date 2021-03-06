﻿using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.SmartMeterLogs;
using Nop.Data;
using Nop.Services.Events;
using Nop.Services.Security;
using Nop.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.SmartMeterLogs
{
    public partial class SmartMeterLogService : ISmartMeterLogService
    {
        #region Fields

        private readonly IRepository<SmartMeterLog> _meterRepository;
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
        public SmartMeterLogService(IRepository<SmartMeterLog> meterRepository,
            IRepository<CustomerBillUnitRate> customerBillUnitRateRepository,
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
            this._meterRepository = meterRepository;
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
            this._customerBillUnitRateRepository = customerBillUnitRateRepository;
        }

        #endregion

        #region methods

        public virtual IPagedList<SmartMeterLog> GetMeterLog(Guid devieId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _meterRepository.Table;
            query = query.Where(m => m.DeviceID == devieId);
            query = query.OrderByDescending(c => c.DeviceID);
            var customers = new PagedList<SmartMeterLog>(query, pageIndex, pageSize);
            return customers;
        }

        public SmartMeterLog SaveMeterLog(SmartMeterLog meterLog)
        {
            if (meterLog != null)
            {
                _meterRepository.Insert(meterLog);
                //event notification
                _eventPublisher.EntityInserted(meterLog);
            }
            return meterLog;
        }

        #endregion
    }
}
