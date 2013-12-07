namespace ReasonCodeExample.WeekDates
{
    public static class Extensions
    {
        public static bool IsSameWeekAs(this WeekDate date, WeekDate otherDate)
        {
            return date != null && otherDate != null && (date.Year == otherDate.Year && date.Week == otherDate.Week);
        }
    }
}