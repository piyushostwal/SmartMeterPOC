using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Customers
{
    public class CustomerMeterTypeMap : NopEntityTypeConfiguration<CustomerMeterType>
    {
        public CustomerMeterTypeMap()
        {
            this.ToTable("CustomerMeterType");
            this.HasKey(c => c.Id);
            this.Property(c => c.MeterType).IsRequired().HasMaxLength(50);
        }
    }
}
