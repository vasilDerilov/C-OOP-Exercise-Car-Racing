using CarRacing.Models.Racers;
using CarRacing.Repositories.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRacing.Repositories
{
    public class RacerRepository : IRepository<Racer>
    {
        private readonly List<Racer> _models;
        public IReadOnlyCollection<Racer> Models
        {
            get => _models.AsReadOnly();
        }

        public void Add(Racer model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddRacerRepository);
            }
            else
            {
                _models.Add(model);
            }
        }

        public RacerRepository()
        {
            _models = new List<Racer>();
        }

        public Racer FindBy(string property)
        {
            return _models.Where(x => x.Username == property).FirstOrDefault();
        }

        public bool Remove(Racer model)
        {
            return _models.Remove(model);
        }
    }
}
