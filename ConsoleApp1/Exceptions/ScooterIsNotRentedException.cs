using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Exceptions
{
   public class ScooterIsNotRentedException: Exception
    {
        public ScooterIsNotRentedException() : base("Scooter is not rented.")
        {

        }
    }
}
