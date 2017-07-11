using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.SmartMeterLogs
{
    public class SmartMeterLogGraphFilterModel : BaseNopModel
    {
        public SmartMeterLogGraphFilterModel()
        {
            PageIndex = 0;
            PageSize = 10;
        }

        public Guid DeviceID { get; set; }
        public int TimeInterval { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}