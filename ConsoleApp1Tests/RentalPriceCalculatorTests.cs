using ConsoleApp1;
using ConsoleApp1.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1Tests
{
    [TestClass()]
    public class RentalPriceCalculatorTests
    {
        [TestMethod]
        public void RentalLessThanOneDayTest()
        {
            var calculator = new RentalPriceCalculator(20);
            var price = calculator.CalculateRentalPrice(DateTime.Now, DateTime.Now.AddMinutes(5), 0.2m);
            Assert.AreEqual(1.0m, price);
        }
        public void TwoDatesTest()
        {
            var calculator = new RentalPriceCalculator(20);
            var startDate = new DateTime(2019, 12, 30, 23, 0, 0);
            var endDate = new DateTime(2019, 12, 31, 01, 0, 0);
            var price = calculator.CalculateRentalPrice(startDate, endDate, 0.2m);
            Assert.AreEqual(24.0m, price);
        }

        [TestMethod]
        public void MultipleDaysTest()
        {
            var calculator = new RentalPriceCalculator(20);
            var price = calculator.CalculateRentalPrice(DateTime.UtcNow, DateTime.UtcNow.AddDays(5), 0.2m);
            Assert.AreEqual(120.0m, price);
        }

        [TestMethod]
        public void NegativeRentalTimeTest()
        {
            var calculator = new RentalPriceCalculator(20);
            Assert.ThrowsException<InvalidRentalTimeException>(() => calculator.CalculateRentalPrice(DateTime.Now.AddDays(5), DateTime.Now, 0.2m));
        }
    }
}
