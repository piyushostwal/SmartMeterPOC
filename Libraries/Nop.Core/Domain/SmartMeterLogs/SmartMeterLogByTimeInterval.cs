using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.SmartMeterLogs
{
    public partial class SmartMeterLogByTimeInterval:BaseEntity
    {
        public Guid DeviceID { get; set; }
        public DateTime LoggingTime { get; set; }
        public int TimeInterval { get; set; }
        public double Consumption { get; set; }
        public int Reading { get; set; }
    }
}
