using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.SmartMeterLogs
{
    public class SmartMeterLogGraphResponseModel : BaseNopModel
    {
        public List<ConsumptionList> Cosumptions { get; set; }
        public List<SolarGeneratedUnitsList> SolarUnitConsumption { get; set; }
    }

    public class ConsumptionList
    {
        public int Consumption { get; set; }
        public DateTime LoggingTime { get; set; }
    }

    public class SolarGeneratedUnitsList
    {
        public int SolarGeneratedUnits { get; set; }
        public DateTime LoggingTime { get; set; }
    }
}