using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Customers
{
    public class CustomerBillUnitRateMap: NopEntityTypeConfiguration<CustomerBillUnitRate>
    {
        public CustomerBillUnitRateMap()
        {
            this.ToTable("CustomerBillUnitRate");
            this.HasKey(c => c.Id);
            this.Property(u => u.RatePerUnit).IsRequired();
            this.Property(u => u.TimeInterval).IsRequired();
            this.Property(u => u.TimeIntervalSetTime).IsRequired();
        }
    }
}
