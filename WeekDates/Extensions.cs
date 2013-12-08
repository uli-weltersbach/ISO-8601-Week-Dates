using System;
using System.Collections.Generic;
using System.Linq;

namespace ReasonCodeExample.WeekDates
{
    public static class Extensions
    {
        public static bool IsSameWeekAs(this DateTime date, DateTime otherDate)
        {
            return IsSameWeekAs(new WeekDate(date), new WeekDate(otherDate));
        }

        public static bool IsSameWeekAs(this WeekDate date, WeekDate otherDate)
        {
            return date != null && otherDate != null && (date.Year == otherDate.Year && date.WeekNumber == otherDate.WeekNumber);
        }

        public static IList<WeekDate> Create(this WeekDate date, DateTime from, DateTime to)
        {
            List<WeekDate> dates = new List<WeekDate>();
            dates.Add(new WeekDate(from));
            while (dates.Last().EndOfWeek.Date <= to.Date)
            {
                DateTime startDateOfNextWeek = dates.Last().EndOfWeek.AddDays(1);
                dates.Add(new WeekDate(startDateOfNextWeek));
            }
            return dates;
        }
    }
}