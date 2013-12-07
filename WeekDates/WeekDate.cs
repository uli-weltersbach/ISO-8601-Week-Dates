using System;

namespace ReasonCodeExample.WeekDates
{
    /// <summary>
    /// Represents an ISO 8601 compliant week.
    /// See http://www.iso.org/iso/home/standards/iso8601.htm 
    /// and http://en.wikipedia.org/wiki/ISO_week_date for details. 
    /// </summary>
    public class WeekDate : IComparable<WeekDate>, IEquatable<WeekDate>
    {
        /// <summary>
        /// Start of this week.
        /// </summary>
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// End of this week.
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// The year to which this week belongs.
        /// </summary>
        public int Year
        {
            get;
            set;
        }

        /// <summary>
        /// Week of the year from #1 to #53.
        /// </summary>
        public int Week
        {
            get;
            set;
        }

        /// <summary>
        /// Day of the week from Monday (#1) to Sunday (#7).
        /// </summary>
        public int Day
        {
            get;
            set;
        }

        public int CompareTo(WeekDate other)
        {
            return other == null ? -1 : string.Compare(ToString(), other.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        public bool Equals(WeekDate other)
        {
            return other != null && string.Equals(ToString(), other.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Returns a sortable ISO week string.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}-W{1:00}-{2}", Year, Week, Day);
        }
    }
}