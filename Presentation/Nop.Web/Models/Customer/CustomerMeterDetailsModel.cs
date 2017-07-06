using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Customer
{
    public class CustomerMeterDetailsModel
    {
        public long meterId { get; set; }
        public string location { get; set; }
        public string customerName { get; set; }
        public string lastMarkedDate { get; set; }
        public int lastMarkedUnit { get; set; }
        public string lastPaymentDate { get; set; }
        public int lastPaidAmount { get; set; }
        public string dueDateBill { get; set; }
        public int dueAmount { get; set; }
        public string billingUnit { get; set; }
    }
}