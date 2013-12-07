using System;
using System.Globalization;

namespace ReasonCodeExample.WeekDates
{
    /// <summary>
    /// Represents a date in a ISO 8601 compliant week.
    /// See http://www.iso.org/iso/home/standards/iso8601.htm 
    /// and http://en.wikipedia.org/wiki/ISO_week_date for details. 
    /// </summary>
    public class WeekDate : IComparable<WeekDate>, IEquatable<WeekDate>
    {
        private const int MinWeek = 1;
        private const int MaxWeek = 53;
        private const DayOfWeek FirstDayOfWeek = DayOfWeek.Monday;
        private const DayOfWeek LastDayOfWeek = DayOfWeek.Sunday;
        private const DayOfWeek PivotDayOfWeek = DayOfWeek.Thursday;

        public WeekDate(DateTime date)
        {
            Start = GetStartDate(date);
            End = GetEndDate(date);
            Day = GetIsoDay(date.DayOfWeek);
            Week = GetIsoWeek(date);
            Year = GetPivotDate(Start).Year;
        }

        /// <summary>
        /// Start of the week in which this date lies.
        /// </summary>
        public DateTime Start
        {
            get;
            private set;
        }

        /// <summary>
        /// End of the week in which this date lies.
        /// </summary>
        public DateTime End
        {
            get;
            private set;
        }

        /// <summary>
        /// The year to which this week belongs.
        /// </summary>
        public int Year
        {
            get;
            private set;
        }

        /// <summary>
        /// Week of the year, ranging from 1 to 53.
        /// </summary>
        public int Week
        {
            get;
            private set;
        }

        /// <summary>
        /// Day of the week this instance represents, 
        /// ranging from Monday (1) to Sunday (7).
        /// </summary>
        public int Day
        {
            get;
            private set;
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
            int gregorianCalendarWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, FirstDayOfWeek);
            return IsFirstWeekOfNextYear(date, gregorianCalendarWeek) ? MinWeek : gregorianCalendarWeek;
        }

        private bool IsFirstWeekOfNextYear(DateTime date, int gregorianCalendarWeek)
        {
            if (gregorianCalendarWeek != MaxWeek)
                return false;
            DateTime startDate = GetStartDate(date);
            DateTime pivotDate = GetPivotDate(date);
            return startDate.Year != pivotDate.Year;
        }

        private DateTime GetPivotDate(DateTime startDate)
        {
            return startDate.AddDays(GetIsoDay(PivotDayOfWeek) - GetIsoDay(startDate.DayOfWeek));
        }

        /// <summary>
        /// Returns a sortable ISO week date string.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}-W{1:00}-{2}", Year, Week, Day);
        }

        public int CompareTo(WeekDate other)
        {
            return other == null ? -1 : string.Compare(ToString(), other.ToString(), StringComparison.Ordinal);
        }

        public bool Equals(WeekDate other)
        {
            return other != null && string.Equals(ToString(), other.ToString(), StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as WeekDate);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}