using System;
using System.Collections.Generic;
using System.Linq;

namespace ReasonCodeExample.WeekDates
{
    /// <summary>
    /// Represents a date in a ISO 8601 compliant week.
    /// See http://www.iso.org/iso/home/standards/iso8601.htm 
    /// and http://en.wikipedia.org/wiki/ISO_week_date for details. 
    /// </summary>
    public class WeekDate : Week
    {
        private readonly DateTime _date;

        public WeekDate(DateTime date)
            : base(date)
        {
            _date = date;
            DayNumber = GetDayNumber(date.DayOfWeek);
        }

        public int DayNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns a sortable ISO week date string (e.g. "2009-W53-2").
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}-W{1:00}-{2}", WeekYear, WeekNumber, DayNumber);
        }

        private int GetDayNumber(DayOfWeek dayOfWeek)
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

        public static IList<WeekDate> GetWeekDates(DateTime from, DateTime to)
        {
            List<WeekDate> weekDates = new List<WeekDate>();
            weekDates.Add(new WeekDate(from));
            while (weekDates.Last()._date < to.Date)
            {
                DateTime nextDay = weekDates.Last()._date.AddDays(1);
                weekDates.Add(new WeekDate(nextDay));
            }
            return weekDates;
        }
    }
}