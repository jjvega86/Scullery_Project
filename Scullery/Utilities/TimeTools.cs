using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Scullery.Utilities
{
    public static class TimeTools
    {
        public static int GetTimeStamp(DateTime date)
        {
            Int32 unixTimestamp = (Int32)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            return unixTimestamp;
        }

        public static string ConvertDateTimeToMealPlanFormat(DateTime date)
        {

            return date.ToString("yyyy-MM-dd");
        }
        
    }
}
