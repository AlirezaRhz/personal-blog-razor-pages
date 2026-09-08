using System.Globalization;

namespace PersonalBlog.Helpers
{
    public static class PersianDateHelper
    {
        public static string ToPersianDate(DateTime utcDate)
        {
            var tehranTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(utcDate, "Iran Standard Time");
            var pc = new PersianCalendar();

            return $"{pc.GetYear(tehranTime)}/{pc.GetMonth(tehranTime):00}/{pc.GetDayOfMonth(tehranTime):00}";
        }
    }
}
