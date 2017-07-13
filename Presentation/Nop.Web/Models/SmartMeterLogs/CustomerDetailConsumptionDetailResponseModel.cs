using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.SmartMeterLogs
{
    public class CustomerDetailConsumptionDetailResponseModel : BaseNopModel
    {
        public Guid DeviceID { get; set; }
        public int CustomerId { get; set; }
        //public string MeterType { get; set; }
        public int Consumption { get; set; }
        public int Reading { get; set; }
        public int SolarGeneratedConsumption { get; set; }
        public string CustomerName { get; set; }
        public DateTime LoggingTime { get; set; }
        public string WeekendName { get; set; }
        public string HolidayName { get; set; }
    }
}