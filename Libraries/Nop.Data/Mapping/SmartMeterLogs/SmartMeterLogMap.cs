using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.SmartMeterLogs;

namespace Nop.Data.Mapping.SmartMeterLogs
{
    public class SmartMeterLogMap: NopEntityTypeConfiguration<SmartMeterLog>
    {
        public SmartMeterLogMap()
        {
            this.ToTable("SmartMeterLogs");
            this.HasKey(c => c.Id);
            this.Property(u => u.DeviceID).IsRequired();
            this.Property(u => u.Lattitude).IsRequired();
            this.Property(u => u.Longitude).IsRequired();
            this.Property(u => u.Consumption).IsRequired();
            this.Property(u => u.TimeInterval).IsRequired();
            this.Property(u => u.TimeIntervalSetTime).IsRequired();
            this.Property(u => u.IsActive).IsRequired();
            this.Property(u => u.LoggingTime).IsRequired();
        }
}
