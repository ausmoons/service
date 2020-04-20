using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public interface IScooterService
    {
        void AddScooter(string id, decimal pricePerMinute);
        void RemoveScooter(string id);
        IList<Scooter> GetScooters();
        Scooter GetScooterById(string scooterId);
    }
}
