using Nop.Core.Domain.SmartMeterLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.SmartMeterLogs
{
    public class SmartMeterLogsByLocationMap : NopEntityTypeConfiguration<SmartMeterLogsByLocation>
    {
        public SmartMeterLogsByLocationMap()
        {
            this.ToTable("SmartMeterLogsByLocation");
            this.HasKey(c => c.Id);
            this.Property(u => u.DeviceID);

            this.Property(u => u.Consumption);
            this.Property(u => u.Reading);            
            this.Property(u => u.CustomerId);
            this.Property(u => u.Lattitude);
            this.Property(u => u.Longitude);
            this.Property(u => u.MeterType);
            
        }
    }
}
