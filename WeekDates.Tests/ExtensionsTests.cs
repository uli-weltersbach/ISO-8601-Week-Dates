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
        public void WeeksAreEquatable(string week, string otherWeek, bool expectedEqualityResult)
        {
            // Arrange
            WeekDate date = new WeekDateFactory().Create(DateTime.Parse(week));
            WeekDate otherDate = new WeekDateFactory().Create(DateTime.Parse(otherWeek));

            // Act
            bool equalityResult = date.IsSameWeekAs(otherDate);

            // Assert
            Assert.That(equalityResult, Is.EqualTo(expectedEqualityResult));
        }
    }
}