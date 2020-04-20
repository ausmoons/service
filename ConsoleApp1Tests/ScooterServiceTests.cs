using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp1.Exceptions;

namespace ConsoleApp1.Tests
{
    [TestClass()]
    public class ScooterServiceTests
    {
        [TestMethod]
        public void AddScooterTest()
        {
            IScooterService service = new ScooterService();
            service.AddScooter("scooter1", 0.2m);
            var scooter = service.GetScooterById("scooter1");

            Assert.AreEqual("scooter1", scooter.Id);
            Assert.AreEqual(0.2m, scooter.PricePerMinute);
        }
        [TestMethod]
        public void AddScooterTwiceTest()
        {
            IScooterService service = new ScooterService();
            service.AddScooter("scooter1", 0.2m);
            Assert.ThrowsException<ScooterExistsException>(() => service.AddScooter("scooter1", 0.2m));

        }

        [TestMethod]
        public void GetScooterByIdTest()
        {
            IScooterService service = new ScooterService();
            service.AddScooter("scooter1", 0.2m);
            var scooter = service.GetScooterById("scooter1");
            Assert.AreEqual("scooter1", scooter.Id);
            Assert.AreEqual(0.2m, scooter.PricePerMinute);
        }

        [TestMethod]
        public void GetNotExistingScooterByIdTest()
        {
            IScooterService service = new ScooterService();
            Assert.ThrowsException<ScooterNotFoundException>(() => service.GetScooterById("scooter1"));
        }

        [TestMethod]
        public void RemoveScooterTest()
        {
            IScooterService service = new ScooterService();
            Assert.ThrowsException<CantRemoveException>(() => service.RemoveScooter("scooter1"));
        }

        [TestMethod]
        public void RemoveNotExistingScooterTest()
        {
            IScooterService service = new ScooterService();
            Assert.ThrowsException<CantRemoveException>(() => service.RemoveScooter("scooter1"));
        }

        [TestMethod]
        public void RemoveRentedScooterTest()
        {
            IScooterService service = new ScooterService();
            service.AddScooter("scooter1", 0.2m);
            service.GetScooterById("scooter1").IsRented = true;
            Assert.ThrowsException<CantRemoveRentedScooterException>(() => service.RemoveScooter("scooter1"));
        }

        [TestMethod]
        public void GetScootersTest()
        {
            IScooterService service = new ScooterService();
            service.AddScooter("scooter1", 0.2m);
            service.AddScooter("scooter2", 0.2m);
            int scooterAmount = service.GetScooters().Count;
            Assert.AreEqual(2, scooterAmount);
        }

        [TestMethod]
        public void GetNoScootersTest()
        {
            IScooterService service = new ScooterService();
            Assert.ThrowsException<ScooterNotFoundException>(() => service.GetScooters());
        }
    }
}