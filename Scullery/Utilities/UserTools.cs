using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Scullery.Utilities
{
    public static class UserTools
    {
        public static string GetTimeStamp(DateTime date)
        {
            return date.ToString("yyyyMMddHHmmssffff");
        }

        public static string ConvertDateTimeToMealPlanFormat(DateTime date)
        {
            return date.ToString("yyyy-mm-dd");
        }
        
    }
}
