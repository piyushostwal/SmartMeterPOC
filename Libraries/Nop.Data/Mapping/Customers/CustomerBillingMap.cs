using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Customers
{
    class CustomerBillingMap : NopEntityTypeConfiguration<CustomerBilling>
    {
        public CustomerBillingMap()
        {
            this.ToTable("CustomerBilling");
            this.HasKey(c => c.Id);
            this.Property(c => c.DeviceId);
            this.Property(c => c.BillPeriodFrom);
            this.Property(c => c.BillPeriodTo);
            this.Property(c => c.ConsumptionUnitReading);
            this.Property(c => c.PreviousConsumbptionUnitReading);
            this.Property(c => c.BillAmount);
            this.Property(c => c.BillDueDate);
            this.Property(c => c.BillPaymentDate);
            this.Property(c => c.IsBillPaid);
            this.Property(c => c.LastBillAmount);
            this.Property(c => c.LastBillPaymentDate);
            this.Property(c => c.CreatedBy);
            this.Property(c => c.Createdon);
            this.Property(c => c.UpdatedBy);
            this.Property(c => c.UpdateOn);

            
        }
    }
}
