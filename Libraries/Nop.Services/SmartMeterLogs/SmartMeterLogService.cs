using Nop.Core;
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
using System.Data;
using System.Linq;
using System.Net.Http;
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

        public async Task<SmartMeterLog> SaveMeterLog(SmartMeterLog meterLog)
        {
            if (meterLog != null)
            {
                _meterRepository.Insert(meterLog);
                //event notification
                _eventPublisher.EntityInserted(meterLog);
                var deviceId = meterLog.DeviceID;
                Uri serviceRoot = new Uri("https://smarthome-46be4.firebaseio.com/smartmeterslogs/" + deviceId + ".json");
                string requestUrl = "https://smarthome-46be4.firebaseio.com/smartmeterslogs/" + deviceId + ".json";

                HttpClient hc = new HttpClient();
                //hc.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                hc.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer");

                var method = new HttpMethod("PATCH");

                var request = new HttpRequestMessage(method, requestUrl)
                {
                    Content = new StringContent("{ \"DateTime\": " + new DateTime().Ticks + " }", Encoding.UTF8, "application/json")
                };

                HttpResponseMessage hrm = await hc.SendAsync(request);
                if (hrm.IsSuccessStatusCode)
                {
                    return meterLog;
                }
                else
                {
                    return meterLog;
                }

            }
            return meterLog;
        }

        //public virtual IPagedList<SmartMeterLogByTimeInterval> GetMeterlogsByTimeInterval(Guid deviceId, int timeInterval, DateTime startTime,
        //    DateTime endTime, int pageIndex = 0, int pageSize = int.MaxValue)
        //{


        //    //prepare parameters
        //    var TimeInterval = _dataProvider.GetParameter();
        //    TimeInterval.ParameterName = "TimeInterval";
        //    TimeInterval.Value = timeInterval;
        //    TimeInterval.DbType = DbType.Int32;

        //    var DeviceId = _dataProvider.GetParameter();
        //    DeviceId.ParameterName = "DeviceId";
        //    DeviceId.Value = deviceId;
        //    DeviceId.DbType = DbType.Guid;

        //    var StartTime = _dataProvider.GetParameter();
        //    StartTime.ParameterName = "StartTime";
        //    StartTime.Value = startTime;
        //    StartTime.DbType = DbType.DateTime;

        //    var EndTime = _dataProvider.GetParameter();
        //    EndTime.ParameterName = "EndTime";
        //    EndTime.Value = endTime;
        //    EndTime.DbType = DbType.DateTime;

        //    var totalRecordsParameter = _dataProvider.GetParameter();
        //    totalRecordsParameter.ParameterName = "TotalRecords";
        //    totalRecordsParameter.Direction = ParameterDirection.Output;
        //    totalRecordsParameter.DbType = DbType.Int32;

        //    //invoke stored procedure
        //    var smartMeterLogs = _dbContext.ExecuteStoredProcedureList<SmartMeterLogByTimeInterval>("UspSelectSmartmeterLogs",
        //        DeviceId, TimeInterval, StartTime, EndTime, totalRecordsParameter);
        //    var totalRecords = (totalRecordsParameter.Value != DBNull.Value) ? Convert.ToInt32(totalRecordsParameter.Value) : 0;

        //    //paging
        //    return new PagedList<SmartMeterLogByTimeInterval>(smartMeterLogs, pageIndex, pageSize);
        //}

        public virtual IPagedList<SmartMeterLogByTimeInterval> GetMeterlogsByTimeInterval(int timeInterval, int pageIndex = 0, int pageSize = int.MaxValue)
        {


            //prepare parameters
            var TimeInterval = _dataProvider.GetParameter();
            TimeInterval.ParameterName = "TimeInterval";
            TimeInterval.Value = timeInterval;
            TimeInterval.DbType = DbType.Int32;

            var totalRecordsParameter = _dataProvider.GetParameter();
            totalRecordsParameter.ParameterName = "TotalRecords";
            totalRecordsParameter.Direction = ParameterDirection.Output;
            totalRecordsParameter.DbType = DbType.Int32;

            //invoke stored procedure
            var smartMeterLogs = _dbContext.ExecuteStoredProcedureList<SmartMeterLogByTimeInterval>("UspSelectSmartmeterLogs",
                TimeInterval, totalRecordsParameter);
            var totalRecords = (totalRecordsParameter.Value != DBNull.Value) ? Convert.ToInt32(totalRecordsParameter.Value) : 0;

            //paging
            return new PagedList<SmartMeterLogByTimeInterval>(smartMeterLogs, pageIndex, pageSize);
        }
        public virtual IPagedList<SmartMeterLogByTimeInterval> GetMeterlogsByCustomerId(int timeInterval, int customerId, int month, int year, DateTime? date = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {


            //prepare parameters
            var CustomerId = _dataProvider.GetParameter();
            CustomerId.ParameterName = "CustomerId";
            CustomerId.Value = customerId;
            CustomerId.DbType = DbType.Int32;

            var TimeInterval = _dataProvider.GetParameter();
            TimeInterval.ParameterName = "TimeInterval";
            TimeInterval.Value = timeInterval;
            TimeInterval.DbType = DbType.Int32;

            var Month = _dataProvider.GetParameter();
            Month.ParameterName = "Month";
            Month.Value = month != 0 ? month : System.Data.SqlTypes.SqlInt32.Null;
            Month.DbType = DbType.Int32;

            var Year = _dataProvider.GetParameter();
            Year.ParameterName = "Year";
            Year.Value = year != 0 ? year : System.Data.SqlTypes.SqlInt32.Null;
            Year.DbType = DbType.Int32;

            var Date = _dataProvider.GetParameter();
            Date.ParameterName = "date";
            Date.Value = date != null ? Convert.ToDateTime(date.Value) : System.Data.SqlTypes.SqlDateTime.Null;
            Date.DbType = DbType.DateTime;

            var totalRecordsParameter = _dataProvider.GetParameter();
            totalRecordsParameter.ParameterName = "TotalRecords";
            totalRecordsParameter.Direction = ParameterDirection.Output;
            totalRecordsParameter.DbType = DbType.Int32;

            //invoke stored procedure
            var smartMeterLogs = _dbContext.ExecuteStoredProcedureList<SmartMeterLogByTimeInterval>(
                "UspSelectSmartmeterLogsByCustomerId", CustomerId, TimeInterval, Month, Year, Date, totalRecordsParameter);

            var totalRecords = (totalRecordsParameter.Value != DBNull.Value) ? Convert.ToInt32(totalRecordsParameter.Value) : 0;

            //paging
            return new PagedList<SmartMeterLogByTimeInterval>(smartMeterLogs, pageIndex, pageSize);
        }

        public virtual IPagedList<SmartMeterLog> GetMeterLogForMultipleDeviceIds(Guid[] deviceIds, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _meterRepository.Table.Where(m => deviceIds.Contains(m.DeviceID)).OrderByDescending(c => c.LoggingTime);
            var customers = new PagedList<SmartMeterLog>(query, pageIndex, pageSize);
            return customers;
        }

        public virtual IPagedList<SmartMeterLogsByLocation> GetMeterlogsByLocation(string minLatitude = "", string minLogitude = "", string maxLatitude = "", string maxLogitude = "", DateTime? startDate = null, DateTime? endDate = null, int? metertypeId = null, string customerIds = "", string weekEnd = "", string holiday = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {


            //prepare parameters
            var MinLatitude = _dataProvider.GetParameter();
            MinLatitude.ParameterName = "MinLatitude";
            MinLatitude.Value = string.IsNullOrEmpty(minLatitude) ? System.Data.SqlTypes.SqlString.Null : minLatitude;
            MinLatitude.DbType = DbType.String;

            var MinLogitude = _dataProvider.GetParameter();
            MinLogitude.ParameterName = "MinLongitude";
            MinLogitude.Value = string.IsNullOrEmpty(minLogitude) ? System.Data.SqlTypes.SqlString.Null : minLogitude;
            MinLogitude.DbType = DbType.String;

            var MaxLatitude = _dataProvider.GetParameter();
            MaxLatitude.ParameterName = "MaxLatitude";
            MaxLatitude.Value = string.IsNullOrEmpty(maxLatitude) ? System.Data.SqlTypes.SqlString.Null : maxLatitude;
            MaxLatitude.DbType = DbType.String;

            var MaxLogitude = _dataProvider.GetParameter();
            MaxLogitude.ParameterName = "MaxLongitude";
            MaxLogitude.Value = string.IsNullOrEmpty(maxLogitude) ? System.Data.SqlTypes.SqlString.Null : maxLogitude;
            MaxLogitude.DbType = DbType.String;

            var StartDate = _dataProvider.GetParameter();
            StartDate.ParameterName = "StartDate";
            StartDate.Value = startDate != null && startDate != DateTime.MinValue ? Convert.ToDateTime(startDate.Value) : System.Data.SqlTypes.SqlDateTime.Null;
            StartDate.DbType = DbType.DateTime;

            var EndDate = _dataProvider.GetParameter();
            EndDate.ParameterName = "EndDate";
            EndDate.Value = endDate != null && endDate != DateTime.MinValue ? Convert.ToDateTime(endDate.Value) : System.Data.SqlTypes.SqlDateTime.Null;
            EndDate.DbType = DbType.DateTime;

            var WeekEnd = _dataProvider.GetParameter();
            WeekEnd.ParameterName = "WeekEnd";
            WeekEnd.Value = !string.IsNullOrEmpty(weekEnd) ? weekEnd : System.Data.SqlTypes.SqlString.Null;
            WeekEnd.DbType = DbType.String;

            var Holiday = _dataProvider.GetParameter();
            Holiday.ParameterName = "Holiday";
            Holiday.Value = !string.IsNullOrEmpty(holiday) ? holiday : System.Data.SqlTypes.SqlString.Null;
            Holiday.DbType = DbType.String;

            var MeterTypeId = _dataProvider.GetParameter();
            MeterTypeId.ParameterName = "MeterTypeId";
            MeterTypeId.Value = metertypeId != null ? metertypeId.Value : System.Data.SqlTypes.SqlInt32.Null;
            MeterTypeId.DbType = DbType.Int32;

            var CustomerIds = _dataProvider.GetParameter();
            CustomerIds.ParameterName = "CustomerIds";
            CustomerIds.Value = !string.IsNullOrEmpty(customerIds) ? customerIds : System.Data.SqlTypes.SqlString.Null;
            CustomerIds.DbType = DbType.String;

            var totalRecordsParameter = _dataProvider.GetParameter();
            totalRecordsParameter.ParameterName = "TotalRecords";
            totalRecordsParameter.Direction = ParameterDirection.Output;
            totalRecordsParameter.DbType = DbType.Int32;

            //invoke stored procedure
            var smartMeterLogs = _dbContext.ExecuteStoredProcedureList<SmartMeterLogsByLocation>(
                "UspSelectSmartmeterLogByLocationandParameters", MinLatitude, MinLogitude, MaxLatitude, MaxLogitude, StartDate, EndDate,
                WeekEnd, Holiday, MeterTypeId, CustomerIds, totalRecordsParameter);

            var totalRecords = (totalRecordsParameter.Value != DBNull.Value) ? Convert.ToInt32(totalRecordsParameter.Value) : 0;

            //paging
            return new PagedList<SmartMeterLogsByLocation>(smartMeterLogs, pageIndex, pageSize);
        }

        public virtual IPagedList<SmartMeterLogsByDeviceId> GetMeterlogsByDeviceIdAndParameters(Guid deviceId, int? timeInterval, DateTime? startDate = null,
          DateTime? endDate = null, string weekEnd = "", string holiday = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            //prepare parameters
            var DeviceId = _dataProvider.GetParameter();
            DeviceId.ParameterName = "DeviceId";
            DeviceId.Value = deviceId.ToString();
            DeviceId.DbType = DbType.String;

            var TimeInterval = _dataProvider.GetParameter();
            TimeInterval.ParameterName = "TimeInterval";
            TimeInterval.Value = timeInterval.HasValue ? timeInterval.Value : System.Data.SqlTypes.SqlInt32.Null;
            TimeInterval.DbType = DbType.Int32;

            var StartDate = _dataProvider.GetParameter();
            StartDate.ParameterName = "StartDate";
            StartDate.Value = startDate != null ? Convert.ToDateTime(startDate.Value) : System.Data.SqlTypes.SqlDateTime.Null;
            StartDate.DbType = DbType.DateTime;

            var EndDate = _dataProvider.GetParameter();
            EndDate.ParameterName = "EndDate";
            EndDate.Value = endDate != null ? Convert.ToDateTime(endDate.Value) : System.Data.SqlTypes.SqlDateTime.Null; ;
            EndDate.DbType = DbType.DateTime;

            var WeekEnd = _dataProvider.GetParameter();
            WeekEnd.ParameterName = "WeekEnd";
            WeekEnd.Value = !string.IsNullOrEmpty(weekEnd) ? weekEnd : string.Empty;
            WeekEnd.DbType = DbType.String;

            var Holiday = _dataProvider.GetParameter();
            Holiday.ParameterName = "Holiday";
            Holiday.Value = !string.IsNullOrEmpty(holiday) ? holiday : string.Empty;
            Holiday.DbType = DbType.String;

            var totalRecordsParameter = _dataProvider.GetParameter();
            totalRecordsParameter.ParameterName = "TotalRecords";
            totalRecordsParameter.Direction = ParameterDirection.Output;
            totalRecordsParameter.DbType = DbType.Int32;

            //invoke stored procedure
            var smartMeterLogs = _dbContext.ExecuteStoredProcedureList<SmartMeterLogsByDeviceId>(
                "UspSelectSmartMeterLogByDeviceIdAndParameter", DeviceId, TimeInterval, StartDate, EndDate, WeekEnd, Holiday, totalRecordsParameter);

            var totalRecords = (totalRecordsParameter.Value != DBNull.Value) ? Convert.ToInt32(totalRecordsParameter.Value) : 0;

            //paging
            return new PagedList<SmartMeterLogsByDeviceId>(smartMeterLogs, pageIndex, pageSize);
        }

        #endregion
    }
}
