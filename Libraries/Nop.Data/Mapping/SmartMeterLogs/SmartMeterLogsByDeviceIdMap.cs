using Nop.Core.Domain.SmartMeterLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.SmartMeterLogs
{
    public class SmartMeterLogsByDeviceIdMap : NopEntityTypeConfiguration<SmartMeterLogsByDeviceId>
    {
        public SmartMeterLogsByDeviceIdMap()
        {
            this.ToTable("SmartMeterLogsByDeviceId");
            this.HasKey(c => c.Id);
            this.Property(u => u.DeviceID);
            this.Property(u => u.Consumption);
            this.Property(u => u.Reading);
            this.Property(u => u.CustomerId);
            //this.Property(u => u.Lattitude);
            //this.Property(u => u.Longitude);
            //this.Property(u => u.MeterType);
            this.Property(u => u.SolarGeneratedUnits);
            this.Property(u => u.LoggingTime);

        }
    }
}
