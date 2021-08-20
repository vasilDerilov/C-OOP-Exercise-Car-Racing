using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Maps;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Repositories;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Core
{
    class Controller : IController
    {
        private readonly CarRepository _cars;
        private readonly RacerRepository _racers;
        private IMap _map;

        public Controller()
        {
            _cars = new CarRepository();
            _racers = new RacerRepository();
            _map = new Map();
        }

        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            if (type == "SuperCar")
            {
                _cars.Add(new SuperCar(make, model, VIN, horsePower));
            }
            else if (type == "TunedCar")
            {
                _cars.Add(new TunedCar(make, model, VIN, horsePower));
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidCarType);
            }
            return string.Format(OutputMessages.SuccessfullyAddedCar, make, model, VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            if (_cars.FindBy(carVIN) == null)
            {
                throw new ArgumentException(ExceptionMessages.CarCannotBeFound);
            }
            
            if (type == "ProfessionalRacer")
            {
                _racers.Add(new ProfessionalRacer(username, _cars.FindBy(carVIN)));
            }
            else if (type == "StreetRacer")
            {
                _racers.Add(new StreetRacer(username, _cars.FindBy(carVIN)));
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidRacerType);
            }
            return string.Format(OutputMessages.SuccessfullyAddedRacer, username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            Racer racerOne = _racers.FindBy(racerOneUsername);
            Racer racerTwo = _racers.FindBy(racerTwoUsername);

            if (racerOne == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerOneUsername));
            }
            else if (racerTwo == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerTwoUsername));
            }

            return _map.StartRace(racerOne, racerTwo);
        }

        public string Report()
        {
            List<Racer> sortedRacers = _racers.Models
                .OrderByDescending(x => x.DrivingExperience)
                .ThenBy(x => x.Username).ToList();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (Racer racer in sortedRacers)
            {
                stringBuilder.AppendLine(racer.ToString());
            }

            return stringBuilder.ToString().Trim();
        }
    }
}
