using System;
using NUnit.Framework;

namespace ReasonCodeExample.WeekDates.Tests
{
    [TestFixture]
    public class WeekDateTests
    {
        [TestCase("2013-04-01", "2013-04-02", -1)]
        [TestCase("2013-04-01", "2013-04-01", 0)]
        [TestCase("2013-04-02", "2013-04-01", 1)]
        public void WeekDatesAreComparable(string week, string otherWeek, int expectedComparisonResult)
        {
            // Arrange
            WeekDate date = new WeekDateFactory().Create(DateTime.Parse(week));
            WeekDate otherDate = new WeekDateFactory().Create(DateTime.Parse(otherWeek));

            // Act
            int comparisonResult = date.CompareTo(otherDate);

            // Assert
            Assert.That(comparisonResult, Is.EqualTo(expectedComparisonResult));
        }

        [TestCase("2013-04-01", "2013-04-02", false)]
        [TestCase("2013-04-01", "2013-04-01", true)]
        [TestCase("2013-04-02", "2013-04-01", false)]
        public void WeekDatesAreEquatable(string week, string otherWeek, bool expectedEqualityResult)
        {
            // Arrange
            WeekDate date = new WeekDateFactory().Create(DateTime.Parse(week));
            WeekDate otherDate = new WeekDateFactory().Create(DateTime.Parse(otherWeek));

            // Act
            bool equalityResult = date.Equals(otherDate);

            // Assert
            Assert.That(equalityResult, Is.EqualTo(expectedEqualityResult));
        }
    }
}