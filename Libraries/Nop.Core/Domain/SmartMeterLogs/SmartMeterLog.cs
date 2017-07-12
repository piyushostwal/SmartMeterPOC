using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.SmartMeterLogs
{
    public partial class SmartMeterLog : BaseEntity
    {
        public Guid DeviceID { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public double Consumption { get; set; }
        public int TimeInterval { get; set; }
        public bool IsActive { get; set; }
        public DateTime LoggingTime { get; set; }
        public int Reading { get; set; }
        public int SolarGeneratedUnits { get; set; }
    }
}
