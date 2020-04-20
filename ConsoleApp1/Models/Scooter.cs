using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Scooter
    {
        public Scooter(string id, decimal pricePerMinute)
        {
            Id = id;
            PricePerMinute = pricePerMinute;
        }
        public string Id { get; }
        public decimal PricePerMinute { get; }
        public bool IsRented { get; set; }

    }
}
