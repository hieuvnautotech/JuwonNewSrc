using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Library.Extension
{
    public static class DateTimeExtension
    {
        public static string DateFormatStr(DateTime? dt)
        {
            var d = DateTime.Now;
            if (dt != null)
            {
                d = dt ?? default;
            }
            return d.ToString("yyyyMMdd");
            //if (DateTime.TryParseExact(str, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            //{
            //	Calendar cal = dfi.Calendar;

            //	return cal.GetWeekOfYear(dt, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            //}
            //else
            //{
            //	return 0;
            //}
        }

        public static int GetCurrentYear(string str)
        {
            string yearStr = str.Substring(0, 4);
            return int.Parse(yearStr);
        }

        public static int GetCurrentMonth(string str)
        {
            string monthStr = str.Substring(4, 2);
            return int.Parse(monthStr);
        }

        public static int GetCurrentDate(string str)
        {
            string dateStr = str.Substring(6, 2);
            return int.Parse(dateStr);
        }

        public static int GetWeekNumber(DateTime? dt)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            string d = DateFormatStr(dt);
            DateTime date = new DateTime(GetCurrentYear(d), GetCurrentMonth(d), GetCurrentDate(d));
            Calendar cal = dfi.Calendar;

            return cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }

        public static int GetWeekNumberByDateTimeString(string str)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            if (DateTime.TryParseExact(str, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
            {
                Calendar cal = dfi.Calendar;

                return cal.GetWeekOfYear(dt, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            }
            else
            {
                return 0;
            }
        }

        public static bool IsDate(string input, string expectedFormat)
        {
            return DateTime.TryParseExact(
                input,
                expectedFormat,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out _);
        }

        public static DateTime dateTimeStringToDateTime(string dateString)
        {

            string[] formats = {"M/d/yyyy h:mm:ss", "M/d/yyyy h:mm:ss","MM/dd/yyyy hh:mm:ss",
                "M/d/yyyy h:mm:ss","M/d/yyyy hh:mm", "M/d/yyyy hh:mm:ss", "M/d/yyyy hh:mm:ss",
                "M/d/yyyy hh:mm:ss" , "MM/dd/yyyy hh:mm:ss", "M/dd/yyyy hh:mm:ss",
                "MM/d/yyyy hh:mm:ss" ,"yyyy-MM-dd HH:mm:ss","M-d-yyyy hh:mm:ss", "M-d-yyyy hh:mm:ss",
                   "MM-dd-yyyy hh:mm:ss", "M-d-yyyy hh:mm:ss",
                   "M-d-yyyy hh:mm:ss", "M-d-yyyy hh:mm:ss",
                   "M-d-yyyy hh:mm:ss", "M-d-yyyy hh:mm:ss",
                   "MM-dd-yyyy HH:mm:ss", "M-dd-yyyy hh:mm:ss",
                   "MM-d-yyyy hh:mm:ss","yyyyMMddHHmmss"};

            if (String.IsNullOrEmpty(dateString))
            {
                return DateTime.Now;
            }

            DateTime.TryParseExact(dateString, formats,
                                                  CultureInfo.InvariantCulture,
                                                 DateTimeStyles.None, out DateTime dateValue);

            return dateValue;
        }

        public static DateTime DateStringToDateTime(string dateString)
        {

            string[] formats = {"M/d/yyyy", "M/d/yyyy","MM/dd/yyyy",
                "M/d/yyyy","M/d/yyyy", "M/d/yyyy", "M/d/yyyy",
                "M/d/yyyy", "MM/dd/yyyy", "M/dd/yyyy",
                "MM/d/yyyy" ,"yyyy-MM-dd","M-d-yyyy", "M-d-yyyy",
                   "MM-dd-yyyy", "M-d-yyyy",
                   "M-d-yyyy", "M-d-yyyy",
                   "M-d-yyyy", "M-d-yyyy",
                   "MM-dd-yyyy", "M-dd-yyyy",
                   "MM-d-yyyy"};
            if (String.IsNullOrEmpty(dateString))
            {
                return DateTime.Now;
            }

            var dateValue = DateTime.ParseExact(dateString, formats,
                                                new CultureInfo("en-US"),
                                                DateTimeStyles.None);

            return dateValue;
        }
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Monday)
        {
            var start = new DateTime(dt.Year, dt.Month, dt.Day);

            if (start.DayOfWeek != startDayOfWeek)
            {
                int d = startDayOfWeek - start.DayOfWeek;
                if (startDayOfWeek <= start.DayOfWeek)
                {
                    return start.AddDays(d);
                }
                return start.AddDays(-7 + d);
            }

            return start;
        }

        public static DateTime EndOfWeek(DateTime dateTime)
        {
            DateTime start = dateTime;

            return start.AddDays(6);
        }

    }
}
