using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Customer
{
    public class CustomerMetersModel
    {
        public string meterId { get; set; }
        public bool status { get; set; }
        public string billingUnit { get; set; }
        public bool IsBillPaid { get; set; }
    }
}