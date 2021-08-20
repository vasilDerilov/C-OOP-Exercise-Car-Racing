using CarRacing.Models.Cars;
using CarRacing.Repositories.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRacing.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        private readonly List<Car> _models;
        public IReadOnlyCollection<Car> Models
        {
            get => _models.AsReadOnly();
        }

        public void Add(Car model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddCarRepository);
            }
            else
            {
                _models.Add(model);
            }
        }

        public CarRepository()
        {
            _models = new List<Car>();
        }

        public Car FindBy(string property)
        {
            return _models.Where(x => x.VIN == property).FirstOrDefault();
        }

        public bool Remove(Car model)
        {
            return _models.Remove(model);
        }
    }
}
