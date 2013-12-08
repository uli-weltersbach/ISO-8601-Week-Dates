using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ReasonCodeExample.WeekDates.Tests
{
    [TestFixture]
    public class WeekTests
    {
        [TestCase("2013-04-01", "2013-04-01", true)]
        [TestCase("2013-04-02", "2013-04-01", true)]
        [TestCase("2013-04-08", "2013-04-01", false)]
        [TestCase("2013-12-15", "2013-12-16", false)]
        [TestCase("2013-12-16", null, false)]
        public void WeeksAreEquatable(string date, string otherDate, bool expectedEqualityResult)
        {
            // Arrange
            Week week = ParseWeek(date);
            Week otherWeek = ParseWeek(otherDate);

            // Act
            bool equalityResult = week.Equals(otherWeek);

            // Assert
            Assert.That(equalityResult, Is.EqualTo(expectedEqualityResult));
        }

        private Week ParseWeek(string week)
        {
            return string.IsNullOrEmpty(week) ? null : new Week(DateTime.Parse(week));
        }

        [TestCase("2008-12-29", "2009-12-28", 53)] // Start of first week to start of last week
        [TestCase("2009-01-04", "2010-01-03", 53)] // End of first week to end of last week
        [TestCase("2008-12-29", "2010-01-03", 53)] // Start of first week to end of last week
        [TestCase("2012-01-04", "2012-01-04", 1)]
        [TestCase("2012-01-04", "2012-12-28", 52)]
        [TestCase("2013-12-23", "2013-12-29", 1)]
        [TestCase("2013-12-30", "2014-01-05", 1)]
        [TestCase("2013-12-23", "2014-01-05", 2)]
        public void WeekRangeIsCorrect(string startDate, string endDate, int expectedWeekCount)
        {
            // Arrange
            DateTime from = DateTime.Parse(startDate);
            DateTime to = DateTime.Parse(endDate);

            // Act
            IList<Week> weekDates = Week.GetWeeks(from, to);

            // Assert
            Assert.That(weekDates.Count, Is.EqualTo(expectedWeekCount));
        }
    }
}