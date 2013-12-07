using System;
using System.Collections.Generic;
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
            return string.IsNullOrEmpty(week) ? null : new WeekDate(DateTime.Parse(week));
        }

        [Test]
        public void WeeksAreEquatableFromDateTime()
        {
            // Arrange
            DateTime date = DateTime.Now;
            DateTime otherDate = DateTime.Now;

            // Act
            bool equalityResult = Extensions.IsSameWeekAs(date, otherDate);

            // Assert
            Assert.That(equalityResult, Is.True);
        }

        [TestCase("2012-01-04", "2012-01-04", 1)]
        [TestCase("2012-01-04", "2012-12-28", 52)]
        [TestCase("2008-12-29", "2009-12-27", 53)]
        public void WeekDateRangeIsCreatedCorrectly(string startDate, string endDate, int expectedWeekCount)
        {
            // Arrange
            DateTime from = DateTime.Parse(startDate);
            DateTime to = DateTime.Parse(endDate);

            // Act
            IList<WeekDate> weekDates = Extensions.Create(ParseWeekDate(startDate), from, to);

            // Assert
            Assert.That(weekDates.Count, Is.EqualTo(expectedWeekCount));
        }
    }
}