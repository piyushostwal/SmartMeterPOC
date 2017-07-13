using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.SmartMeterLogs
{
    public class CustomerDetailConsumptionFilterModel : BaseNopModel
    {
        public Guid DeviceID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Filter { get; set; }
        public int? TimeInterval { get; set; }
    }
}