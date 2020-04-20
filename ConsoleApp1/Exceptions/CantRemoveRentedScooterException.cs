using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Exceptions
{
   public class CantRemoveRentedScooterException: Exception
   {
        public CantRemoveRentedScooterException() : base("Cant't remove scooter. It is in rental proces!")
        {
        }
    }
}
