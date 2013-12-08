using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace ReasonCodeExample.WeekDates
{
    /// <summary>
    /// Represents a ISO 8601 compliant week.
    /// </summary>
    public class Week : IComparable<Week>, IEquatable<Week>
    {
        private const int MinWeek = 1;
        private const int MaxWeek = 53;
        private const DayOfWeek FirstDayOfWeek = DayOfWeek.Monday;
        private const DayOfWeek LastDayOfWeek = DayOfWeek.Sunday;
        private const DayOfWeek PivotDayOfWeek = DayOfWeek.Thursday;

        public Week(DateTime date)
        {
            StartOfWeek = GetStartOfWeek(date);
            EndOfWeek = GetEndOfWeek(date);
            WeekNumber = GetWeekNumber(StartOfWeek);
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

        private int GetWeekNumber(DateTime startOfWeek)
        {
            int weekNumber = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(startOfWeek, CalendarWeekRule.FirstFourDayWeek, FirstDayOfWeek);
            return IsFirstWeekOfNextYear(startOfWeek, weekNumber) ? MinWeek : weekNumber;
        }

        private bool IsFirstWeekOfNextYear(DateTime startOfWeek, int weekNumber)
        {
            if (weekNumber < MaxWeek)
                return false;
            DateTime pivotOfWeek = GetPivotDayOfWeek(startOfWeek);
            return startOfWeek.Year < pivotOfWeek.Year;
        }

        private DateTime GetPivotDayOfWeek(DateTime startOfWeek)
        {
            while (startOfWeek.DayOfWeek != PivotDayOfWeek)
            {
                startOfWeek = startOfWeek.AddDays(1);
            }
            return startOfWeek;
        }

        /// <summary>
        /// Returns a sortable ISO week string (e.g. "2009-W53").
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}-W{1:00}", Year, WeekNumber);
        }

        public int CompareTo(Week other)
        {
            return other == null ? -1 : string.Compare(ToString(), other.ToString(), StringComparison.Ordinal);
        }

        public bool Equals(Week other)
        {
            return other != null && string.Equals(ToString(), other.ToString(), StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Week);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static IList<Week> GetWeeks(DateTime from, DateTime to)
        {
            List<Week> weeks = new List<Week>();
            weeks.Add(new Week(from));
            while (weeks.Last().EndOfWeek.Date < to.Date)
            {
                DateTime startOfNextWeek = weeks.Last().EndOfWeek.AddDays(1);
                weeks.Add(new Week(startOfNextWeek));
            }
            return weeks;
        }
    }
}