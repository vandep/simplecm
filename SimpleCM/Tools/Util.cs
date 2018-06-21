using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCM.Tools
{
    class Util
    {
        /// <summary>
        /// Get the DataTime
        /// </summary>
        /// <param name="millis">the millis senconds from 1970/1/1. this is utc time param>
        /// <returns></returns>
        public static DateTime GetTimeFromMillis(long millis)
        {
            DateTime time = new DateTime(1970, 1, 1);
            TimeSpan diff = new TimeSpan(millis * 10000);
            time += diff;
            return time;
        }

        public static long GetTimeMillis(int year, int month, int day)
        {
            DateTime dateTime = new DateTime(year, month, day);
            return (long)(dateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}
