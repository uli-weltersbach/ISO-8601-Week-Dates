using System;
using System.Globalization;

namespace ReasonCodeExample.WeekDates
{
    /// <summary>
    /// Represents an ISO8601 compliant week.
    /// </summary>
    public class WeekDate
    {
        public DateTime StartDate
        {
            get;
            set;
        }

        public DateTime EndDate
        {
            get;
            set;
        }

        public int Year
        {
            get;
            set;
        }

        public int Week
        {
            get;
            set;
        }

        public int Day
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("{0}-W{1:00}-{2}", Year, Week, Day);
        }
    }
}