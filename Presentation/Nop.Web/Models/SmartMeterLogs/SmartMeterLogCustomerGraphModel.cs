using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.SmartMeterLogs
{
    public class SmartMeterLogCustomerGraphModel : BaseNopModel
    {
        public int CustomerID { get; set; }
        public string TimeInterval { get; set; }
        public DateTime TimeIntervalDate { get; set; }
    }
}