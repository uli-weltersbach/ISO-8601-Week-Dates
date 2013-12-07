using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ReasonCodeExample.WeekDates
{
    public class WeekDateFactory
    {
        private const int MinWeek = 1;
        private const int MaxWeek = 53;
        private const DayOfWeek FirstDayOfWeek = DayOfWeek.Monday;
        private const DayOfWeek LastDayOfWeek = DayOfWeek.Sunday;
        private const DayOfWeek PivotDayOfWeek = DayOfWeek.Thursday;

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
            return IsFirstWeekOfNextYear(date, week) ? MinWeek : week;
        }

        private int GetWeek(DateTime date)
        {
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, FirstDayOfWeek);
        }

        private bool IsFirstWeekOfNextYear(DateTime date, int week)
        {
            if (week != MaxWeek)
                return false;
            DateTime startDate = GetStartDate(date);
            DateTime pivotDate = GetPivotDate(date);
            return startDate.Year != pivotDate.Year;
        }

        private DateTime GetPivotDate(DateTime date)
        {
            DateTime startDate = GetStartDate(date);
            return startDate.AddDays(GetIsoDay(PivotDayOfWeek) - GetIsoDay(startDate.DayOfWeek));
        }

        private int GetIsoWeekYear(DateTime date)
        {
            return GetPivotDate(date).Year;
        }

        public IList<WeekDate> Create(DateTime from, DateTime to)
        {
            List<WeekDate> dates = new List<WeekDate>();
            dates.Add(Create(from));
            while (dates.Last().EndDate.Date <= to.Date)
            {
                dates.Add(Create(dates.Last().EndDate.AddDays(1)));
            }
            return dates;
        }
    }
}