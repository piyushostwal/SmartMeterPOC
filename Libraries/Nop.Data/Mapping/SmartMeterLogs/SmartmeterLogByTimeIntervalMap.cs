using Nop.Core.Domain.SmartMeterLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.SmartMeterLogs
{
    public class SmartMeterLogByTimeIntervalMap : NopEntityTypeConfiguration<SmartMeterLogByTimeInterval>
    {
        public SmartMeterLogByTimeIntervalMap()
        {
            this.ToTable("SmartMeterLogByTimeInterval");
            this.HasKey(c => c.Id);
            this.Property(u => u.DeviceID);
           
            this.Property(u => u.Consumption);
            this.Property(u => u.Reading);
            this.Property(u => u.TimeInterval);
            this.Property(u => u.LoggingTime);
            this.Property(u => u.CustomerId);
            this.Property(u => u.CustomerName);
            this.Property(u => u.CustomerAddress);
           
        }
    }
}
