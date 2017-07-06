using System;
using System.Collections.Generic;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;

namespace Nop.Core.Domain.Customers
{
    public class CustomerProductDetails : BaseEntity
    {
        #region
        public CustomerProductDetails()
        { }
        #endregion

        #region Proeprties
        public int CustomerId { get; set; }
        public Guid DeviceId { get; set; }
        public string BillingUnit { get; set; }
        public bool Status { get; set; }
        public DateTime Createdon { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public int? UpdatedBy { get; set; }
        public virtual Customer Customer { get; set; }
        #endregion

    }
}
