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
        SmartMeterLog SaveMeterLog(SmartMeterLog meterLog);
        IPagedList<SmartMeterLogByTimeInterval> GetMeterlogsByTimeInterval(Guid deviceId, int timeInterval, DateTime startTime,
            DateTime endTime, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
