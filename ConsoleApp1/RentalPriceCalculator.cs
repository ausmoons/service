using ConsoleApp1.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class RentalPriceCalculator : IRentalPriceCalculator
    {
        private decimal _maxPriceForDay;

        public RentalPriceCalculator(decimal maxPriceForDay)
        {
            _maxPriceForDay = maxPriceForDay;
        }

        public decimal CalculateRentalPrice(DateTime startDate, DateTime? endDate, decimal pricePerMinute)
        {
            var rentalTime = endDate - startDate;
            if (rentalTime?.TotalMinutes < 0)
            {
                throw new InvalidRentalTimeException();
            }

            var inSameDay = endDate?.Date == startDate.Date;

            if (inSameDay == true)
            {
                return CalculatePrice(rentalTime, pricePerMinute);
            }
            else
            {
                return CalculateForMultipleDays(startDate, endDate, pricePerMinute);
            }
        }

        private decimal CalculateForMultipleDays(DateTime startDate, DateTime? endDate, decimal pricePerMinute)
        {

            var minutesInFirstDate = 1440 - (Convert.ToDecimal(startDate.Minute) + Convert.ToDecimal(startDate.Hour) * 60);
            var minutesInLastDay = Convert.ToInt32(endDate?.Minute) + Convert.ToInt32(endDate?.Hour) * 60;
            var priceForFirstDay = minutesInFirstDate * pricePerMinute > _maxPriceForDay ? _maxPriceForDay : minutesInFirstDate * pricePerMinute;
            var priceForLastDay = minutesInLastDay * pricePerMinute > _maxPriceForDay ? _maxPriceForDay : minutesInLastDay * pricePerMinute;
            var priceForFirstLast = priceForFirstDay + priceForLastDay;

            var atleasOneFullDay = endDate?.Date - startDate.Date;
            var numberDays = Convert.ToInt32(atleasOneFullDay?.Days);

               if (numberDays > 1)
               {
                   int fullDays = numberDays - 1;
                   var fullPrice = (fullDays * _maxPriceForDay) + priceForFirstLast;

                   return fullPrice;
               }
               else
               {
                   return priceForFirstLast;
               }

           // return 2;

        }

        private decimal CalculatePrice(TimeSpan? rentalTime, decimal pricePerMinute)
        {
            var price = Math.Round((Convert.ToDecimal(rentalTime?.TotalMinutes) * pricePerMinute), 2);
            price = price > _maxPriceForDay ? _maxPriceForDay : price;

            return price;
        }
    }

}

