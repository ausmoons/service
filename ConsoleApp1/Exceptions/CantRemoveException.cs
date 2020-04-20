using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Exceptions
{
    public class CantRemoveException : Exception
    {
        public CantRemoveException() : base("Cant't remove scooter which don't exist!")
        {
        }
    }
}
