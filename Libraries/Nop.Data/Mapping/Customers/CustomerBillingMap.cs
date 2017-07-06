﻿using Nop.Core.Domain.Customers;
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
            this.Property(c => c.BillingUnit).HasMaxLength(50);
            this.Property(c => c.Status);

            this.HasRequired(c => c.Order)
                .WithMany()
                .HasForeignKey(c => c.OrderId)
                .WillCascadeOnDelete(false);
        }
    }
}