using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public interface IRentalPriceCalculator
    {
        decimal CalculateRentalPrice(DateTime startDate, DateTime? endDate, decimal pricePerMinute);
    }
}
