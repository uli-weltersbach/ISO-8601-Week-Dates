﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ReasonCodeExample.WeekDates.Tests
{
    [TestFixture]
    public class WeekDateFactoryTests
    {
        [TestCase("2005-01-01", "2004-W53-6")]
        [TestCase("2005-01-02", "2004-W53-7")]
        [TestCase("2005-12-31", "2005-W52-6")]
        [TestCase("2007-01-01", "2007-W01-1")]
        [TestCase("2007-12-30", "2007-W52-7")]
        [TestCase("2007-12-31", "2008-W01-1")]
        [TestCase("2008-01-01", "2008-W01-2")]
        [TestCase("2008-12-28", "2008-W52-7")]
        [TestCase("2008-12-29", "2009-W01-1")]
        [TestCase("2008-12-30", "2009-W01-2")]
        [TestCase("2008-12-31", "2009-W01-3")]
        [TestCase("2009-01-01", "2009-W01-4")]
        [TestCase("2009-12-31", "2009-W53-4")]
        [TestCase("2010-01-01", "2009-W53-5")]
        [TestCase("2010-01-02", "2009-W53-6")]
        [TestCase("2010-01-03", "2009-W53-7")]
        [TestCase("2013-12-28", "2013-W52-6")]
        [TestCase("2013-12-29", "2013-W52-7")]
        [TestCase("2013-12-30", "2014-W01-1")]
        [TestCase("2013-12-31", "2014-W01-2")]
        public void WeekDateConformsToISO8601(string date, string expectedWeekDate)
        {
            // Arrange
            WeekDate weekDate = new WeekDateFactory().Create(DateTime.Parse(date));

            // Act
            string weekDateStringRepresentation = weekDate.ToString();

            // Assert
            Assert.That(weekDateStringRepresentation, Is.EqualTo(expectedWeekDate));
        }

        [TestCase("2012-01-04", "2012-12-28", 52)]
        [TestCase("2008-12-29", "2009-12-27", 53)]
        public void WeekDateRangeIsCreatedCorrectly(string startDate, string endDate, int expectedWeekCount)
        {
            // Arrange
            DateTime from = DateTime.Parse(startDate);
            DateTime to = DateTime.Parse(endDate);

            // Act
            IList<WeekDate> weekDates = new WeekDateFactory().Create(from, to);

            // Assert
            Assert.That(weekDates.Count, Is.EqualTo(expectedWeekCount));
        }
    }
}