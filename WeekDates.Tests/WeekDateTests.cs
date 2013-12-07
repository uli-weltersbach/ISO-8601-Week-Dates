using System;
using NUnit.Framework;

namespace ReasonCodeExample.WeekDates.Tests
{
    [TestFixture]
    public class WeekDateTests
    {
        [TestCase("2013-04-01", null, -1)]
        [TestCase("2013-04-01", "2013-04-02", -1)]
        [TestCase("2013-04-01", "2013-04-01", 0)]
        [TestCase("2013-04-02", "2013-04-01", 1)]
        public void WeekDatesAreComparable(string week, string otherWeek, int expectedComparisonResult)
        {
            // Arrange
            WeekDate date = ParseWeekDate(week);
            WeekDate otherDate = ParseWeekDate(otherWeek);

            // Act
            int comparisonResult = date.CompareTo(otherDate);

            // Assert
            Assert.That(comparisonResult, Is.EqualTo(expectedComparisonResult));
        }

        private WeekDate ParseWeekDate(string week)
        {
            return string.IsNullOrEmpty(week) ? null : new WeekDateFactory().Create(DateTime.Parse(week));
        }

        [TestCase("2013-04-01", null, false)]
        [TestCase("2013-04-01", "2013-04-02", false)]
        [TestCase("2013-04-01", "2013-04-01", true)]
        [TestCase("2013-04-02", "2013-04-01", false)]
        public void WeekDatesAreEquatable(string week, string otherWeek, bool expectedEqualityResult)
        {
            // Arrange
            WeekDate date = ParseWeekDate(week);
            WeekDate otherDate = ParseWeekDate(otherWeek);

            // Act
            bool equalityResult = date.Equals(otherDate);

            // Assert
            Assert.That(equalityResult, Is.EqualTo(expectedEqualityResult));
        }
    }
}