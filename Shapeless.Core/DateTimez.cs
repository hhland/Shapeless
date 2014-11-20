using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shapeless.Core
{
    public class DateTimez
    {

        public static string PATTERN_CN_MD = "MM月dd日";
        public static string PATTERN_CN_YMD = "yyyy年MM月dd日";
        public static string PATTERN_DB_DATE = "yyyy-MM-dd";
        public static string PATTERN_DB_DATE2 = "yyyy-M-dd";
        public static string PATTERN_DB_DATE3 = "yyyy-M-d";
        public static string PATTERN_DB_DATE4 = "yyyy-MM-d";
        public static string PATTERN_DB_DATE5 = "yyyy/MM/d";
        public static string PATTERN_DB_DATE6 = "yyyy/M/dd";
        public static string PATTERN_DB_DATE7 = "yyyy/M/d";
        public static string PATTERN_DB_DATETIME = "yyyy-MM-dd HH:mm:ss";
        public static string PATTERN_DB_YMD_HM = "yyyy-MM-dd HH:mm";
        public static string PATTERN_DB_TIME = "HH:mm:ss";

        public static DateTime SYSTEM_DATE_MIN = new DateTime(1970, 1, 1);

        public static DateTime MSSQL_DATE_MIN = new DateTime(1753, 1, 2);

        /// <summary>
        /// 获取当前时间毫秒数
        /// </summary>
        /// <returns></returns>
        public static long getTotalMilliseconds()
        {
            return getTotalMilliseconds(DateTime.Now);
        }

        /// <summary>
        /// 获取当前时间毫秒数
        /// </summary>
        /// <returns></returns>
        public static long getTotalMilliseconds(DateTime dt)
        {
            return Convert.ToInt64(new TimeSpan(dt.Ticks).TotalMilliseconds);
        }

        /// <summary>
        /// 将具有特定格式的日期字符串转换为等效的日期对象
        /// </summary>
        /// <param name="val">特定格式的日期字符串</param>
        /// <returns>等效的日期对象</returns>
        public static DateTime ParseDateTime(string val)
        {
            DateTime returnDateTime = MSSQL_DATE_MIN;
            string[] patterns = new string[] { "yyyy/MM/dd", PATTERN_DB_DATE, PATTERN_DB_DATETIME,
                PATTERN_DB_TIME, PATTERN_DB_DATE2, PATTERN_DB_DATE3, PATTERN_DB_DATE4,
                PATTERN_DB_DATE5, PATTERN_DB_DATE6, PATTERN_DB_DATE7 , PATTERN_DB_YMD_HM};
            foreach (string pattern in patterns)
            {
                returnDateTime = ParseDateTime(val, pattern);
                if (!returnDateTime.Equals(MSSQL_DATE_MIN))
                {
                    break;
                }
            }
            return returnDateTime;
        }

        /// <summary>
        /// 将具有特定格式的日期字符串，用指定的日期格式，转换为等效的日期对象
        /// </summary>
        /// <param name="val">特定格式的日期字符串</param>
        /// <param name="pattern">日期格式</param>
        /// <returns>等效的日期对象</returns>
        public static DateTime ParseDateTime(string val, string pattern)
        {
            DateTime returnDateTime = MSSQL_DATE_MIN;
            try
            {
                returnDateTime = DateTime.ParseExact(val, pattern, System.Threading.Thread.CurrentThread.CurrentCulture);
            }
            catch (Exception ex) { }
            return returnDateTime;
        }

        /// <summary>
        /// 将指定日期对象转换为等效的日期字符串
        /// </summary>
        /// <param name="datetime">日期对象</param>
        /// <returns>等效的日期字符串</returns>
        public static string format(DateTime datetime)
        {
            return format(datetime, PATTERN_DB_DATETIME);
        }

        /// <summary>
        /// 将指定日期对象，用指定的日期格式，转换为等效的日期字符串
        /// </summary>
        /// <param name="datetime">日期对象</param>
        /// <param name="pattern">日期格式</param>
        /// <returns>等效的日期字符串</returns>
        public static string format(DateTime datetime, string pattern)
        {
            try
            {
                return datetime.ToString(pattern);
            }
            catch (Exception ex)
            {
                return "";

            }
        }

        /// <summary>
        /// 将指定日期对象转换为等效的日期字符串
        /// </summary>
        /// <param name="datetime">可以为null的日期对象</param>
        /// <returns>等效的日期字符串</returns>
        public static string format(Nullable<DateTime> datetime)
        {
            try
            {
                return format(datetime.Value, PATTERN_DB_DATETIME);
            }
            catch (Exception ex)
            {
                return "";

            }
        }

        /// <summary>
        /// 将指定日期对象，用指定的日期格式，转换为等效的日期字符串
        /// </summary>
        /// <param name="datetime">可以为null的日期对象</param>
        /// <param name="pattern">日期格式</param>
        /// <returns>等效的日期字符串</returns>
        public static string format(Nullable<DateTime> datetime, string pattern)
        {
            try
            {
                return format(datetime.Value, pattern);
            }
            catch (Exception ex)
            {
                return "";

            }
        }

        /// <summary>
        /// 日期比较，比较年月日
        /// </summary>
        /// <param name="dt1">被比较的日期</param>
        /// <param name="dt2">用于比较的日期</param>
        /// <returns>dt1日期早于dt2，返回-1；相等返回0；dt1日期迟于dt2，返回1</returns>
        public static int CompareDay(DateTime dt1, DateTime dt2)
        {
            int re = 0;
            if (dt1.Year < dt2.Year)
            {
                return -1;
            }
            else if (dt1.Year > dt2.Year)
            {
                return 1;
            }

            int doy1 = Convert.ToInt32(dt1.DayOfYear);
            int doy2 = Convert.ToInt32(dt2.DayOfYear);
            if (doy1 < doy2)
            {
                return -1;
            }
            else if (doy1 > doy2)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 一周日期
        /// </summary>
        public class Week
        {

            public static string[] CN_NUMS = { "日", "一", "二", "三", "四", "五", "六" },
             EN_SHORT_NAMES = { "sun", "mon", "tue", "wed", "thu", "fri", "sat" };

            public Week(DateTime d)
            {
                init(d);
            }

            private void init(DateTime d)
            {
                int diff = 0;
                switch (d.DayOfWeek)
                {
                    case DayOfWeek.Sunday: diff = 0; break;
                    case DayOfWeek.Monday: diff = 1; break;
                    case DayOfWeek.Tuesday: diff = 2; break;
                    case DayOfWeek.Wednesday: diff = 3; break;
                    case DayOfWeek.Thursday: diff = 4; break;
                    case DayOfWeek.Friday: diff = 5; break;
                    case DayOfWeek.Saturday: diff = 6; break;
                }
                DateTime d0 = new DateTime(d.Year, d.Month, d.Day);
                d0 = d0.AddDays(-diff);
                int i = 0;
                sun = d0.AddDays(i++);
                mon = d0.AddDays(i++);
                tue = d0.AddDays(i++);
                wed = d0.AddDays(i++);
                thu = d0.AddDays(i++);
                fri = d0.AddDays(i++);
                sat = d0.AddDays(i++);
            }

            public DateTime[] days
            {
                get
                {
                    return new DateTime[]
                    {
                        sun,mon,tue,wed,thu,fri,sat
                    };
                }
            }
            public DateTime sun { get; private set; }
            public DateTime mon { get; private set; }
            public DateTime tue { get; private set; }
            public DateTime wed { get; private set; }
            public DateTime thu { get; private set; }
            public DateTime fri { get; private set; }
            public DateTime sat { get; private set; }

            public Week next(int num)
            {
                return new Week(mon.AddDays(7 * num));
            }
        }

    }
}
