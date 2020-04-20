using ConsoleApp1.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace ConsoleApp1
{
    public class RentalCompany : IRentalCompany
    {
        private readonly IScooterService _scooterService;
        private readonly IList<RentedScooter> _rentedScooters;
        private readonly IRentalPriceCalculator _rentalPriceCalculator;
        public string Name { get; }

        public RentalCompany(string name, IScooterService scooterService, IRentalPriceCalculator rentalPriceCalculator,
            IList<RentedScooter> rentals)
        {
            Name = name;
            _scooterService = scooterService;
            _rentedScooters = rentals;
            _rentalPriceCalculator = rentalPriceCalculator;
        }

        private Scooter GetScooter(string id)
        {
            if (_scooterService.GetScooterById(id) == null)
            {
                throw new ScooterNotFoundException();
            }
            return _scooterService.GetScooterById(id);
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            decimal totalIncome = 0.0m;

            if (year != null)
            {
                if (includeNotCompletedRentals == true && year == DateTime.UtcNow.Year)
                {
                    var alsoNotComplitedRentsInThisYear = _rentedScooters.Where(x => x.RentalStart.Year == DateTime.UtcNow.Year && (x.RentalEnd?.Year == null || x.RentalEnd?.Year == year)).ToList();

                    foreach (var rental in alsoNotComplitedRentsInThisYear)
                    {
                        totalIncome += _rentalPriceCalculator.CalculateRentalPrice(rental.RentalStart, rental.RentalEnd == null ? rental.RentalEnd = DateTime.UtcNow : rental.RentalEnd, rental.PricePerMinute);
                    }
                }
                else if (includeNotCompletedRentals == true && year != DateTime.UtcNow.Year)
                {
                    var alsoNotComplitedRentsInAnotherYear = _rentedScooters.Where(x => x.RentalStart.Year == year && (x.RentalEnd?.Year != year || x.RentalEnd?.Year == year)).ToList();

                    foreach (var rental in alsoNotComplitedRentsInAnotherYear)
                    {
                        totalIncome += _rentalPriceCalculator.CalculateRentalPrice(rental.RentalStart, rental.RentalEnd?.Year != year ? rental.RentalEnd = new DateTime(year.GetValueOrDefault() + 1, 01, 01) : rental.RentalEnd, rental.PricePerMinute);
                    }
                }
                else
                {
                    var rentalsInYear = _rentedScooters.Where(x => x.RentalEnd?.Year == year).ToList();
                    foreach (var rental in rentalsInYear)
                    {
                        if (includeNotCompletedRentals == false && !rental.RentalEnd.HasValue)
                        {
                            continue;
                        }
                        totalIncome += _rentalPriceCalculator.CalculateRentalPrice(rental.RentalStart,
                           rental.RentalEnd, rental.PricePerMinute); ;
                    }

                }
                return totalIncome;
            }
            else
            {
                foreach (var rental in _rentedScooters)
                {
                    if (includeNotCompletedRentals == false && !rental.RentalEnd.HasValue)
                    {
                        continue;
                    }

                    totalIncome += _rentalPriceCalculator.CalculateRentalPrice(rental.RentalStart,
                       rental.RentalEnd.HasValue ? rental.RentalEnd : DateTime.UtcNow, rental.PricePerMinute);
                }

                return totalIncome;
            }
        }

        public decimal EndRent(string id)
        {
            Scooter scooter = GetScooter(id);
            RentedScooter rented = _rentedScooters.FirstOrDefault(x =>
                x.ScooterId == id && x.RentalEnd == null && x.RentalStart <= DateTime.UtcNow);

            if (rented == null)
            {
                throw new ScooterIsNotRentedException();
            }

            rented.RentalEnd = DateTime.UtcNow;
            scooter.IsRented = false;

            return _rentalPriceCalculator.CalculateRentalPrice(rented.RentalStart, rented.RentalEnd,
               scooter.PricePerMinute);
        }

        public void StartRent(string id)
        {
            Scooter scooter = GetScooter(id);
            if (scooter == null)
            {
                throw new ScooterNotFoundException();
            }
            else if (scooter.IsRented == true)
            {
                throw new ScooterAlreadyRentedException();
            }
            scooter.IsRented = true;
            _rentedScooters.Add(new RentedScooter
            { ScooterId = id, RentalStart = DateTime.UtcNow, PricePerMinute = scooter.PricePerMinute });
        }
    }
}
