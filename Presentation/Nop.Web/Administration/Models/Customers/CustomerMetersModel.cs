using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Admin.Models.Customers
{
    public class CustomerMetersModel
    {
        public string MeterId { get; set; }
        public bool Status { get; set; }
        public string BillingUnit { get; set; }
        public bool? IsBillPaid { get; set; }
        public string CustomerFullName { get; set; }
    }
}