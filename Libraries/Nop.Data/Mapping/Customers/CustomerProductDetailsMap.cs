using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Customers
{
    class CustomerProductDetailsMap : NopEntityTypeConfiguration<CustomerProductDetails>
    {
        public CustomerProductDetailsMap()
        {
            this.ToTable("CustomerProductDetails");
            this.HasKey(c => c.Id);
            this.Property(c => c.DeviceId);
            this.Property(c => c.BillingUnit).HasMaxLength(50);
            this.Property(c => c.Status);
            this.Property(c => c.MeterId);
            this.Property(c => c.MaxConsumptionPerMonth);
            this.Property(c => c.IsHighConsumption);
            this.Property(c => c.CreatedBy);
            this.Property(c => c.Createdon);
            this.Property(c => c.UpdatedBy);
            this.Property(c => c.UpdateOn);

            this.HasRequired(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.CustomerId)
                .WillCascadeOnDelete(false);
            this.HasRequired(c => c.CustomerMeterType)
                .WithMany()
                .HasForeignKey(c => c.MeterTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
