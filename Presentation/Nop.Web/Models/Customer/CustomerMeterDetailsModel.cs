using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Customer
{
    public class CustomerMeterDetailsModel
    {
        public string meterId { get; set; }
        public string customerName { get; set; }
        public string location { get; set; }
        public string BillingUnit { get; set; }
        public int ConsumptionUnitReading { get; set; }
        public string BillPeriodFrom { get; set; }
        public string BillPeriodTo { get; set; }
        public decimal BillAmount { get; set; }
        public string BillPaymentDate { get; set; }
        public string BillDueDate { get; set; }
        public int PreviousConsumptionUnitReading { get; set; }
        public decimal? LastBillAmount { get; set; }
        public string LastBillPaymentDate { get; set; }
        
        
        
        
    }
}