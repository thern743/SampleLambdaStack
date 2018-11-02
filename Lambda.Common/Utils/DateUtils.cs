using System;

namespace Lambda.Common.Utils
{
    public static class DateUtils
    {
        /// <summary>
        /// Convert unix time string to DateTime
        /// </summary>
        /// <param name="unixTimeString"></param>
        /// <returns>DateTime</returns>
        public static DateTime FromEpoch(string unixTimeString) => FromEpoch((long)Convert.ToDouble(unixTimeString));

        /// <summary>
        /// Convert unix time long to DateTime
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static DateTime FromEpoch(long unixTime) => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(unixTime);
    }
}