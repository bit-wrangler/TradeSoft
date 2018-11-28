using NUnit.Framework;
using Application.Utilities;
using System;

namespace Tests
{
    [TestFixture]
    public class ToDatePeriodUtilityTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckYearToDateStart()
        {
            var testDate = new DateTime(2018, 5, 10);
            Assert.AreEqual(new DateTime(2018,1,1), ToDatePeriodUtility.StartOfYear(testDate));
        }

        [Test]
        public void CheckMonthToDateStart()
        {
            var testDate = new DateTime(2018, 5, 10);
            Assert.AreEqual(new DateTime(2018,5,1), ToDatePeriodUtility.StartOfMonth(testDate));
        }

        [Test]
        public void CheckQuarterToDateStart()
        {
            Assert.AreEqual(new DateTime(2018,1,1), ToDatePeriodUtility.StartOfQuarter(new DateTime(2018, 1, 1)));
            Assert.AreEqual(new DateTime(2018,1,1), ToDatePeriodUtility.StartOfQuarter(new DateTime(2018, 1, 2)));
            Assert.AreEqual(new DateTime(2018,1,1), ToDatePeriodUtility.StartOfQuarter(new DateTime(2018, 2, 1)));
            Assert.AreEqual(new DateTime(2018,1,1), ToDatePeriodUtility.StartOfQuarter(new DateTime(2018, 3, 10)));

            Assert.AreEqual(new DateTime(2018,4,1), ToDatePeriodUtility.StartOfQuarter(new DateTime(2018, 4, 1)));
            Assert.AreEqual(new DateTime(2018,4,1), ToDatePeriodUtility.StartOfQuarter(new DateTime(2018, 5, 2)));
            Assert.AreEqual(new DateTime(2018,4,1), ToDatePeriodUtility.StartOfQuarter(new DateTime(2018, 6, 3)));

            Assert.AreEqual(new DateTime(2018,7,1), ToDatePeriodUtility.StartOfQuarter(new DateTime(2018, 7, 1)));
            Assert.AreEqual(new DateTime(2018,7,1), ToDatePeriodUtility.StartOfQuarter(new DateTime(2018, 8, 1)));
            Assert.AreEqual(new DateTime(2018,7,1), ToDatePeriodUtility.StartOfQuarter(new DateTime(2018, 9, 1)));

            Assert.AreEqual(new DateTime(2018,10,1), ToDatePeriodUtility.StartOfQuarter(new DateTime(2018, 10, 1)));
            Assert.AreEqual(new DateTime(2018,10,1), ToDatePeriodUtility.StartOfQuarter(new DateTime(2018, 11, 2)));
            Assert.AreEqual(new DateTime(2018,10,1), ToDatePeriodUtility.StartOfQuarter(new DateTime(2018, 12, 1)));
            Assert.AreEqual(new DateTime(2018,10,1), ToDatePeriodUtility.StartOfQuarter(new DateTime(2018, 12, 31)));
        }
    }
}