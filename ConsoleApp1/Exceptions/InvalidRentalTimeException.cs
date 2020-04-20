using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Exceptions
{
    public class InvalidRentalTimeException : Exception
    {
        public InvalidRentalTimeException() : base("Rental time is not valid")
        {
        }
    }
}
