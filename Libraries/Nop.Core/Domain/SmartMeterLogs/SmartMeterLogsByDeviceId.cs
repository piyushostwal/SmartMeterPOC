using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.SmartMeterLogs
{
    public class SmartMeterLogsByDeviceId : BaseEntity
    {
        public Guid DeviceID { get; set; }
        public int CustomerId { get; set; }
        //public string Lattitude { get; set; }
        //public string Longitude { get; set; }
        //public string MeterType { get; set; }
        public int Consumption { get; set; }
        public int Reading { get; set; }
        public int SolarGeneratedUnits { get; set; }
        public DateTime LoggingTime { get; set; }
        public string WeekendName { get; set; }
        public string HolidayName { get; set; }
    }
}
