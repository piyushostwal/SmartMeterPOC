using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.SmartMeterLogs
{
    public static class SmartMeterLogCustomerGraphModelExtensions
    {
        public static int ConvertToMinutes(string timeInterval)
        {
            switch (timeInterval)
            {
                case "15":
                    return 15;
                case "30":
                    return 30;
                case "45":
                    return 45;
                case "60":
                    return 60;
                case "day":
                    return 24 * 60;
                case "week":
                    return 24 * 7 * 60;
                case "month":
                    return 0;
                case "year":
                    return 365 * 24 * 60;
                default:
                    return 0;



            }
        }
    }
}
