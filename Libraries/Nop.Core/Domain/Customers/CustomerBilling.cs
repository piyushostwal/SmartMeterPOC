using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Customers
{
    public class CustomerBilling : BaseEntity
    {
        public CustomerBilling()
        {

        }
        public Guid DeviceId { get; set; }
        public DateTime BillPeriodFrom { get; set; }
        public DateTime BillPeriodTo { get; set; }
        public int ConsumptionUnitReading { get; set; }
        public int PreviousConsumptionUnitReadind { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDueDate { get; set; }
        public DateTime BillPaymentDate { get; set; }
        public bool IsBillPaid { get; set; }
        public decimal? LastBillAmount { get; set; }
        public DateTime? LastBillPaymentDate { get; set; }
        public int CustomerId { get; set; }


        public virtual Customer Customer { get; set; }
        

    }
}
