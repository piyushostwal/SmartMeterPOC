using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.SmartMeterLogs
{
    public class SmartMeterLogGraphModel : BaseNopModel
    {
        //public Guid deviceId { get; set; }
        public int TimeInterval { get; set; }
        public DateTime LoggingTime { get; set; }
        public double consumption { get; set; }
        public int Reading { get; set; }
        public int SolarGeneratedUnits { get; set; }
    }
}