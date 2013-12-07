using System;
using NUnit.Framework;

namespace ReasonCodeExample.WeekDates.Tests
{
    [TestFixture]
    public class ExtensionsTests
    {
        [TestCase("2013-04-01", "2013-04-01", true)]
        [TestCase("2013-04-02", "2013-04-01", true)]
        [TestCase("2013-04-08", "2013-04-01", false)]
        [TestCase("2013-12-15", "2013-12-16", false)]
        [TestCase(null, null, false)]
        [TestCase(null, "2013-12-16", false)]
        [TestCase("2013-12-16", null, false)]
        public void WeeksAreEquatable(string week, string otherWeek, bool expectedEqualityResult)
        {
            // Arrange
            WeekDate date = ParseWeekDate(week);
            WeekDate otherDate = ParseWeekDate(otherWeek);

            // Act
            bool equalityResult = Extensions.IsSameWeekAs(date, otherDate);

            // Assert
            Assert.That(equalityResult, Is.EqualTo(expectedEqualityResult));
        }

        private WeekDate ParseWeekDate(string week)
        {
            return string.IsNullOrEmpty(week) ? null : new WeekDateFactory().Create(DateTime.Parse(week));
        }
    }
}