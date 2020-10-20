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

        public static string ConvertTimeStampToStringDate(int date)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(date).ToLocalTime();
            string newStringDate = TimeTools.ConvertDateTimeToMealPlanFormat(dateTime);
            return newStringDate;
        }
        
    }
}
