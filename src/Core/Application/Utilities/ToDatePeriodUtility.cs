using System;

namespace Application.Utilities
{
    public static class ToDatePeriodUtility
    {
        public static DateTime StartOfYear(DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }
        public static DateTime StartOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
        public static DateTime StartOfQuarter(DateTime date)
        {
            var quarterStartMonth = 10;
            while (quarterStartMonth > date.Month) quarterStartMonth -= 3;
            return new DateTime(date.Year, quarterStartMonth, 1);
        }
    }
}