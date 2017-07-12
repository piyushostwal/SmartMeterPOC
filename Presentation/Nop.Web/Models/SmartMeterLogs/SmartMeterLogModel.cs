using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.SmartMeterLogs
{
    public class SmartMeterLogModel : BaseNopEntityModel
    {
        public int Id { get; set; }
        public Guid DeviceID { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public int Consumption { get; set; }
        public int TimeInterval { get; set; }
        public DateTime TimeIntervalSetTime { get; set; }
        public bool IsActive { get; set; }
        public DateTime LoggingTime { get; set; }
        public int Reading { get; set; }
    }
}