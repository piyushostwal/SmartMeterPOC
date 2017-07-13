using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.SmartMeterLogs
{
    public class SmartMeterLogHeatMapModel : BaseNopModel
    {
        public string MinLattitude { get; set; }
        public string MaxLattiude { get; set; }
        public string MinLongitude { get; set; }
        public string MaxLongitude { get; set; }
        public string Filter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CustomerType { get; set; }
        public int? MeterTypeId { get; set; }
    }
}