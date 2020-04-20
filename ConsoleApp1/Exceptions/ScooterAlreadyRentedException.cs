using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Exceptions
{
    public class ScooterAlreadyRentedException: Exception
    {
        public ScooterAlreadyRentedException() : base("Scooter is already rented.")
        {
        }
    }
}
