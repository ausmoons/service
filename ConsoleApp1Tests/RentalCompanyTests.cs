using ConsoleApp1;
using ConsoleApp1.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1Tests
{
    [TestClass()]
    public class ScooterServiceTests
    {

        private decimal renatlCalculatorMaxPrice = 20.0m;


        [TestMethod]
        public void StartRentalTest()
        {
            var name = "my company";
            IScooterService service = new ScooterService();
            service.AddScooter("scooter1", 0.2m);
            IRentalCompany company = new RentalCompany(name, service, new RentalPriceCalculator(renatlCalculatorMaxPrice), new List<RentedScooter>());
            company.StartRent("scooter1");
            var scooter = service.GetScooterById("scooter1");
            Assert.IsTrue(scooter.IsRented);
        }

        [TestMethod]
        public void StartNotExistingScooterRentalTest()
        {
            var name = "my company";
            IScooterService service = new ScooterService();
            IRentalCompany company = new RentalCompany(name, service, new RentalPriceCalculator(renatlCalculatorMaxPrice), new List<RentedScooter>());
            Assert.ThrowsException<ScooterNotFoundException>(() => company.StartRent("scooter1"));
        }

        [TestMethod]
        public void StartRentedScooterRentalTest()
        {
            var name = "my company";
            IScooterService service = new ScooterService();
            service.AddScooter("scooter1", 0.2m);
            IRentalCompany company = new RentalCompany(name, service, new RentalPriceCalculator(renatlCalculatorMaxPrice), new List<RentedScooter>());
            company.StartRent("scooter1");
            Assert.ThrowsException<ScooterAlreadyRentedException>(() => company.StartRent("scooter1"));
        }

        [TestMethod]
        public void EndRentTest()
        {
            var name = "my company";
            IScooterService service = new ScooterService();
            service.AddScooter("scooter1", 0.2m);
            IRentalCompany company = new RentalCompany(name, service, new RentalPriceCalculator(renatlCalculatorMaxPrice), new List<RentedScooter>());
            company.StartRent("scooter1");
            var scooter = service.GetScooterById("scooter1");
            Assert.IsTrue(scooter.IsRented);
            company.EndRent("scooter1");
            Assert.IsFalse(scooter.IsRented);
        }

        [TestMethod]
        public void EndNotStartedRentalTest()
        {
            var name = "my company";
            IScooterService service = new ScooterService();
            service.AddScooter("scooter1", 0.2m);
            IRentalCompany company = new RentalCompany(name, service, new RentalPriceCalculator(renatlCalculatorMaxPrice), new List<RentedScooter>());
            Assert.ThrowsException<ScooterIsNotRentedException>(() => company.EndRent("scooter1"));
        }

        [TestMethod]
        public void CalculateThisYearAllIncomeTest()
        {
            var name = "my company";
            IScooterService service = new ScooterService();
            var date = DateTime.UtcNow;
            var rentals = new List<RentedScooter>
            {
                new RentedScooter
                {
                    RentalStart = date, RentalEnd = date.AddMinutes(5), ScooterId = "scooter1", PricePerMinute = 0.2m
                },
                new RentedScooter
                {
                    RentalStart = date, RentalEnd = date.AddDays(5), ScooterId = "scooter2", PricePerMinute = 0.5m
                },
                new RentedScooter
                {
                    RentalStart = date.AddDays(-2), RentalEnd = null, ScooterId = "scooter3", PricePerMinute = 1.0m
                }
            };


            IRentalCompany company = new RentalCompany(name, service, new RentalPriceCalculator(renatlCalculatorMaxPrice), rentals);
            Assert.AreEqual(181.0m, company.CalculateIncome(2020, true));
        }

        [TestMethod]
        public void CalculateThisYearFinishedIncomeTest()
        {
            var name = "my company";
            IScooterService service = new ScooterService();
            var date = DateTime.UtcNow;
            var rentals = new List<RentedScooter>
            {
                new RentedScooter
                {
                    RentalStart = date, RentalEnd = date.AddMinutes(5), ScooterId = "scooter1", PricePerMinute = 0.2m
                },
                new RentedScooter
                {
                    RentalStart = date, RentalEnd = date.AddDays(5), ScooterId = "scooter2", PricePerMinute = 0.5m
                },
                new RentedScooter
                {
                    RentalStart = date.AddDays(-2), RentalEnd = null, ScooterId = "scooter3", PricePerMinute = 1.0m
                }
            };


            IRentalCompany company = new RentalCompany(name, service, new RentalPriceCalculator(renatlCalculatorMaxPrice), rentals);
            Assert.AreEqual(121.0m, company.CalculateIncome(2020, false));
        }


        [TestMethod]
        public void CalculateAnotherYearAllIncomeTest()
        {
            var name = "my company";
            IScooterService service = new ScooterService();
            var date = new DateTime(2019, 12, 30);
            var rentals = new List<RentedScooter>
            {
                new RentedScooter
                {
                    RentalStart = date, RentalEnd = date.AddMinutes(5), ScooterId = "scooter1", PricePerMinute = 0.2m
                },
                new RentedScooter
                {
                    RentalStart = date, RentalEnd = date.AddDays(5), ScooterId = "scooter2", PricePerMinute = 0.5m
                }
            };


            IRentalCompany company = new RentalCompany(name, service, new RentalPriceCalculator(renatlCalculatorMaxPrice), rentals);
            Assert.AreEqual(41.0m, company.CalculateIncome(2019, true));
        }

        [TestMethod]
        public void CalculateLastYearFinishedIncomeTest()
        {
            var name = "my company";
            IScooterService service = new ScooterService();
            var date = new DateTime(2019, 12, 30);
            var rentals = new List<RentedScooter>
            {
                new RentedScooter
                {
                    RentalStart = date, RentalEnd = date.AddMinutes(5), ScooterId = "scooter1", PricePerMinute = 0.2m
                },
                new RentedScooter
                {
                    RentalStart = date, RentalEnd = date.AddDays(5), ScooterId = "scooter2", PricePerMinute = 0.5m
                }
            };


            IRentalCompany company = new RentalCompany(name, service, new RentalPriceCalculator(renatlCalculatorMaxPrice), rentals);
            Assert.AreEqual(1.0m, company.CalculateIncome(2019, false));
        }

        [TestMethod]
        public void CalculateTotalFinishedRentIncomeTest()
        {
            var name = "my company";
            IScooterService service = new ScooterService();
            var date = new DateTime(2019, 12, 30);
            var rentals = new List<RentedScooter>
            {
                new RentedScooter
                {
                    RentalStart = date, RentalEnd = date.AddMinutes(5), ScooterId = "scooter1", PricePerMinute = 0.2m
                },
                new RentedScooter
                {
                    RentalStart = date, RentalEnd = date.AddDays(5), ScooterId = "scooter2", PricePerMinute = 0.5m
                },
                new RentedScooter
                {
                    RentalStart = DateTime.UtcNow.AddDays(-1), RentalEnd = null, ScooterId = "scooter3", PricePerMinute = 1.0m
                }
            };


            IRentalCompany company = new RentalCompany(name, service, new RentalPriceCalculator(renatlCalculatorMaxPrice), rentals);
            Assert.AreEqual(101.0m, company.CalculateIncome(null, false));
        }

        [TestMethod]
        public void CalculateTotalIncomeTest()
        {
            var name = "my company";
            IScooterService service = new ScooterService();
            var date = new DateTime(2019, 12, 30);
            var rentals = new List<RentedScooter>
            {
                new RentedScooter
                {
                    RentalStart = date, RentalEnd = date.AddMinutes(5), ScooterId = "scooter1", PricePerMinute = 0.2m
                },
                new RentedScooter
                {
                    RentalStart = date, RentalEnd = date.AddDays(5), ScooterId = "scooter2", PricePerMinute = 0.5m
                },
                 new RentedScooter
                {
                    RentalStart = DateTime.UtcNow.AddDays(-1), RentalEnd = null, ScooterId = "scooter3", PricePerMinute = 1.0m
                }
            };


            IRentalCompany company = new RentalCompany(name, service, new RentalPriceCalculator(renatlCalculatorMaxPrice), rentals);
            Assert.AreEqual(141.0m, company.CalculateIncome(null, true));
        }

    }
}
