using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeFormatSample
{
    public static class DateTimeExtensions
    {
        public static string FormatLongDateForDisplay(this DateTime dateTime)
        {
            return dateTime.ToString("D", CultureInfo.DefaultThreadCurrentUICulture);
        }

        public static string FormatShortDateForDisplay1(this DateTime dateTime)
        {
            return dateTime.ToShortDateString();
        }

        public static string FormatShortDateForDisplay2(this DateTime dateTime)
        {
            var dateTimeFormat = CultureInfo.DefaultThreadCurrentUICulture.DateTimeFormat;
            return dateTime.ToString(dateTimeFormat.ShortDatePattern);
        }

        public static string FormatShortDateForDisplay3(this DateTime dateTime)
        {
            var currentCulture = CultureInfo.DefaultThreadCurrentUICulture;
            bool isCalendarBuddhist = currentCulture?.DateTimeFormat.Calendar is ThaiBuddhistCalendar;

            if (isCalendarBuddhist)
            {
                var cultureWithCalendar = (CultureInfo)currentCulture.Clone();
                cultureWithCalendar.DateTimeFormat.Calendar = new ThaiBuddhistCalendar();
                return dateTime.ToString(cultureWithCalendar.DateTimeFormat.ShortDatePattern, cultureWithCalendar);
            }

            return dateTime.ToString(currentCulture?.DateTimeFormat.ShortDatePattern, currentCulture);
        }
    }
}
