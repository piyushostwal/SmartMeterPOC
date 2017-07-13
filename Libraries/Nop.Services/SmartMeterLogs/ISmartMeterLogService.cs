using Nop.Core;
using Nop.Core.Domain.SmartMeterLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.SmartMeterLogs
{
    public partial interface ISmartMeterLogService
    {
        IPagedList<SmartMeterLog> GetMeterLog(Guid devieId, int pageIndex = 0, int pageSize = int.MaxValue);
        Task<SmartMeterLog> SaveMeterLog(SmartMeterLog meterLog);
        IPagedList<SmartMeterLogByTimeInterval> GetMeterlogsByTimeInterval(int timeInterval, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<SmartMeterLog> GetMeterLogForMultipleDeviceIds(Guid[] deviceIds, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<SmartMeterLogByTimeInterval> GetMeterlogsByCustomerId(int timeInterval, int customerId, int month, int year, DateTime? date = null,
            int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<SmartMeterLogsByLocation> GetMeterlogsByLocation(string minLatitude = "", string minLogitude = "", string maxLatitude = "", string maxLogitude = "", DateTime? startDate = null, DateTime? endDate = null, int? metertypeId = null, string customerIds = "", string weekEnd = "", string holiday = "", int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
