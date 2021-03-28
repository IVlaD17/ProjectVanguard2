using NUnit.Framework;

using ProjectVanguard.Models.Entities;

namespace ProjectVanguard2Tests
{
    public class TimeTest
    {
        string singleDigitsValue = "01";
        string doubleDigitsValue = "10";

        [Test]
        public void SingleDigitsSecondsValue()
        {
            Time singleDigitsTime = new Time(1);
            Assert.AreEqual(singleDigitsValue, singleDigitsTime.ToString());
        }
        [Test]
        public void DoubleDigitsSecondsValue()
        {

        }

        [Test]
        public void SingleDigitsMinutesValue()
        {

        }
        [Test]
        public void DoubleDigitsMinutesValue()
        {

        }

        [Test]
        public void SingleDigitsHoursValue()
        {

        }
        [Test]
        public void DoubleDigitsHoursValue()
        {

        }

        [Test]
        public void SingleDigitsDaysValue()
        {

        }
        [Test]
        public void DoubleDigitsDaysValue()
        {

        }

        [Test]
        public void SingleDigitsMonthsValue()
        {

        }
        [Test]
        public void DoubleDigitsMonthsValue()
        {

        }

        [Test]
        public void SingleDigitsYearsValue()
        {

        }
        [Test]
        public void DoubleDigitsYearsValue()
        {

        }
    }
}
