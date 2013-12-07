using System;
using System.Globalization;

namespace ReasonCodeExample.WeekDates
{
    public class WeekDateFactory
    {
        private const int MinWeek = 1;
        private const int MaxWeek = 53;
        private const DayOfWeek FirstDayOfWeek = DayOfWeek.Monday;
        private const DayOfWeek LastDayOfWeek = DayOfWeek.Sunday;

        public WeekDate Create(DateTime date)
        {
            return new WeekDate
                {
                    StartDate = GetStartDate(date),
                    EndDate = GetEndDate(date),
                    Day = GetIsoDay(date.DayOfWeek),
                    Week = GetIsoWeek(date),
                    Year = GetIsoWeekYear(date)
                };
        }

        private DateTime GetStartDate(DateTime date)
        {
            while (date.DayOfWeek != FirstDayOfWeek)
            {
                date = date.AddDays(-1);
            }
            return date;
        }

        private DateTime GetEndDate(DateTime date)
        {
            while (date.DayOfWeek != LastDayOfWeek)
            {
                date = date.AddDays(1);
            }
            return date;
        }

        private int GetIsoDay(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                case DayOfWeek.Sunday:
                    return 7;
                default:
                    throw new ArgumentOutOfRangeException("dayOfWeek", dayOfWeek, "Unknown day of week.");
            }
        }

        private int GetIsoWeek(DateTime date)
        {
            int week = GetWeek(date);
            return IsFirstWeekOfYear(date, week) ? MinWeek : week;
        }

        private int GetWeek(DateTime date)
        {
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, FirstDayOfWeek);
        }

        private bool IsFirstWeekOfYear(DateTime date, int week)
        {
            if (week != MaxWeek)
                return false;
            DateTime startDate = GetStartDate(date);
            DateTime endDate = GetEndDate(date);
            DateTime thursday = GetThursday(endDate);
            return startDate.Year != thursday.Year;
        }

        private DateTime GetThursday(DateTime date)
        {
            DateTime startDate = GetStartDate(date);
            return startDate.AddDays(GetIsoDay(DayOfWeek.Thursday) - GetIsoDay(startDate.DayOfWeek));
        }

        private int GetIsoWeekYear(DateTime date)
        {
            return GetThursday(date).Year;
        }
    }
}