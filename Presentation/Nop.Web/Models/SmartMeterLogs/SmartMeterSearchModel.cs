using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.SmartMeterLogs
{
    public class SmartMeterSearchModel : BaseNopEntityModel
    {
        public Guid DeviceId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
    }
}