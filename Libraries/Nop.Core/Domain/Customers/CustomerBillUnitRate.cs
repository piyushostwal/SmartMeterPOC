using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Customers
{
    public partial class CustomerBillUnitRate : BaseEntity
    {
        public decimal RatePerUnit { get; set; }
        public int TimeInterval { get; set; }
        public DateTime TimeIntervalSetTime { get; set; }
    }
}
