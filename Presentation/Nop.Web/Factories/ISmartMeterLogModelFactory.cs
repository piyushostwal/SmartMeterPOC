using Nop.Core;
using Nop.Core.Domain.SmartMeterLogs;
using Nop.Web.Models.SmartMeterLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Web.Factories
{
    public partial interface ISmartMeterLogModelFactory
    {
        IPagedList<SmartMeterLog> PrepareSmartMeterLogPagingFilteringModel(Guid deviceId);
        SmartMeterLog SaveMeterLog(SmartMeterLogModel model);
    }
}
