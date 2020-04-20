using System;
using System.Runtime.Serialization;

namespace ConsoleApp1
{
    public class ScooterExistsException : Exception
    {
        public ScooterExistsException() : base("Scooter Exists!")
        {
        }
    }
}