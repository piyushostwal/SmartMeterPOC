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
        public int Consumption { get; set; }
        public int Reading { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public int SolarGeneratedUnits { get; set; }
    }
}
