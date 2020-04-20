using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class RentedScooter
    {
        public string ScooterId { get; set; }
        public DateTime RentalStart { get; set; }
        public DateTime? RentalEnd { get; set; }
        public decimal PricePerMinute { get; set; }
    }
}
