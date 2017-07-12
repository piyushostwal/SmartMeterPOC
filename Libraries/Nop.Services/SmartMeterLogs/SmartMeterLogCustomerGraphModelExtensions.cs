using Nop.Core;
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
            int returnValue = 0;
            switch (timeInterval)
            {
                case TimeIntervals.SFifteen:
                    returnValue = TimeIntervals.Fifteen;
                    break;
                case TimeIntervals.SThirty:
                    returnValue = TimeIntervals.Thirty;
                    break;
                case TimeIntervals.SFortyFive:
                    returnValue = TimeIntervals.FortyFive;
                    break;
                case TimeIntervals.SSixty:
                    returnValue = TimeIntervals.Sixty;
                    break;
                case TimeIntervals.SDay:
                    returnValue = TimeIntervals.Day; //24 * 60;
                    break;
                case TimeIntervals.SWeek:
                    returnValue = TimeIntervals.Week; // 24 * 7 * 60;
                    break;
                case TimeIntervals.SMonth:
                    return TimeIntervals.Month;
                case TimeIntervals.SYear:
                    returnValue = TimeIntervals.Year; //365 * 24 * 60;
                    break;
                default:
                    returnValue = 0;
                    break;
            }
            return returnValue / 15;
        }
    }
}
