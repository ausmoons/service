using ConsoleApp1.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ConsoleApp1
{
    public class ScooterService : IScooterService
    {
        private List<Scooter> _scooters;

        public ScooterService()
        {
            _scooters = new List<Scooter>();
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            if (_scooters.Any(x => x.Id == id))
            {
                throw new ScooterExistsException();
            }
            _scooters.Add(new Scooter(id, pricePerMinute));
        }

        public Scooter GetScooterById(string scooterId)
        {
            var scooter = _scooters.FirstOrDefault(x => x.Id == scooterId);
            if (scooter == null)
            {
                throw new ScooterNotFoundException();
            }
            return scooter;
        }

        public IList<Scooter> GetScooters()
        {
            var scooters = _scooters.ToList();
            if(scooters.Count == 0)
            {
                throw new ScooterNotFoundException();
            }
            return scooters;
        }

        public void RemoveScooter(string id)
        {
            var scooter = _scooters.FirstOrDefault(x => x.Id == id);
            if (scooter == null)
            {
                throw new CantRemoveException();
            }
            else if (scooter.IsRented == true)
            {
                throw new CantRemoveRentedScooterException();
            }
            else  
            {
                _scooters.Remove(scooter);
            }
        }
    }
}
