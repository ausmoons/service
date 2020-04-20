using System;
using System.Runtime.Serialization;

namespace ConsoleApp1
{
    public class ScooterNotFoundException : Exception
    {
        public ScooterNotFoundException() : base("Scooter not found.")
        {

        }
    }
}