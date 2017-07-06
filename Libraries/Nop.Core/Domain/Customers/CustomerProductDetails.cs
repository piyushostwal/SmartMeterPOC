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
        public int OrderId { get; set; }
        public Guid DeviceId { get; set; }
        public string BillingUnit { get; set; }
        public bool Status { get; set; }
        public virtual Order Order { get; set; }
        #endregion

    }
}
