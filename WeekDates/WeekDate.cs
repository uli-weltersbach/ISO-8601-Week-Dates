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
            StartOfWeek = GetStartOfWeek(date);
            EndOfWeek = GetEndOfWeek(date);
            DayOfWeek = date.DayOfWeek;
            WeekNumber = GetWeekNumber(date);
            Year = GetPivotDayOfWeek(StartOfWeek).Year;
        }

        public DateTime StartOfWeek
        {
            get;
            private set;
        }

        public DateTime EndOfWeek
        {
            get;
            private set;
        }

        public DayOfWeek DayOfWeek
        {
            get;
            private set;
        }

        public int WeekNumber
        {
            get;
            private set;
        }

        public int Year
        {
            get;
            private set;
        }

        public int CompareTo(WeekDate other)
        {
            return other == null ? -1 : string.Compare(ToString(), other.ToString(), StringComparison.Ordinal);
        }

        public bool Equals(WeekDate other)
        {
            return other != null && string.Equals(ToString(), other.ToString(), StringComparison.Ordinal);
        }

        private DateTime GetStartOfWeek(DateTime date)
        {
            while (date.DayOfWeek != FirstDayOfWeek)
            {
                date = date.AddDays(-1);
            }
            return date;
        }

        private DateTime GetEndOfWeek(DateTime date)
        {
            while (date.DayOfWeek != LastDayOfWeek)
            {
                date = date.AddDays(1);
            }
            return date;
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

        private int GetWeekNumber(DateTime date)
        {
            int weekNumber = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, FirstDayOfWeek);
            return IsFirstWeekOfNextYear(date, weekNumber) ? MinWeek : weekNumber;
        }

        private bool IsFirstWeekOfNextYear(DateTime date, int weekNumber)
        {
            if (weekNumber != MaxWeek)
                return false;
            DateTime startDate = GetStartOfWeek(date);
            DateTime pivotDate = GetPivotDayOfWeek(date);
            return startDate.Year != pivotDate.Year;
        }

        private DateTime GetPivotDayOfWeek(DateTime startDate)
        {
            return startDate.AddDays(GetDayNumber(PivotDayOfWeek) - GetDayNumber(startDate.DayOfWeek));
        }

        /// <summary>
        /// Returns a sortable ISO week date string.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}-W{1:00}-{2}", Year, WeekNumber, GetDayNumber(DayOfWeek));
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