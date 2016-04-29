using System;
using System.Drawing;

namespace LuckyColorBot
{
    public class RandomColor
    {
        private readonly DateTime unixEpochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private readonly Mt19937 mt;

        public RandomColor()
        {
            mt = new Mt19937((uint) GetTodayUnixTime()%uint.MaxValue);
        }

        /// <summary>
        ///     現在のUNIX時刻を返す
        /// </summary>
        private long GetNowUnixTime()
        {
            return GetUnixTime(DateTime.Now);
        }

        /// <summary>
        ///     今日のUNIX時刻を返す
        /// </summary>
        private long GetTodayUnixTime()
        {
            return GetUnixTime(DateTime.Today);
        }

        /// <summary>
        ///     UNIX時刻を返す
        /// </summary>
        private long GetUnixTime(DateTime dateTime)
        {
            return (long) dateTime.ToUniversalTime().Subtract(unixEpochTime).TotalSeconds;
        }

        public void InitializeSeed(int seed)
        {
            mt.InitGet((uint) (seed%uint.MaxValue));
        }

        public void InitializeSeed(uint seed)
        {
            mt.InitGet(seed);
        }

        public Color GetRandomColor()
        {
            var r = (byte) mt.GetInt32()%256;
            var g = (byte) mt.GetInt32()%256;
            var b = (byte) mt.GetInt32()%256;
            return Color.FromArgb(r, g, b);
        }
    }
}