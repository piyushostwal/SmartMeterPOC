using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.SmartMeterLogs
{
    public class SmartMeterHeatMapResponseModel : BaseNopModel
    {
        public Guid DeviceId { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public int Consumption { get; set; }
        public int Reading { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerAddress { get; set; }
        public string MeterType { get; set; }
        public int SolarGeneratedUnits { get; set; }
        public int TimeInterval { get; set; }
        public DateTime LoggingTime { get; set; }
    }
}