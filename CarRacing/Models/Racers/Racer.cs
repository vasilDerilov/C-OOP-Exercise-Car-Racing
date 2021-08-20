using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;
using System;

namespace CarRacing.Models.Racers
{
    public abstract class Racer : IRacer
    {
        private string _username;
        public string Username
        {
            get => _username;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerName);
                }
                else
                {
                    _username = value;
                }
            }
        }

        private string _racingBehavior;
        public string RacingBehavior
        {
            get => _racingBehavior;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerBehavior);
                }
                else
                {
                    _racingBehavior = value;
                }
            }
        }

        private int _drivingExperience;
        public int DrivingExperience
        {
            get => _drivingExperience;
            protected set
            {
                if (value < 0 || 100 < value)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerDrivingExperience);
                }
                else
                {
                    _drivingExperience = value;
                }
            }
        }

        private ICar _car;
        public ICar Car
        {
            get => _car;
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerCar);
                }
                else
                {
                    _car = value;
                }
            }
        }

        public Racer(string username, string racingBehavior, int drivingExperience, ICar car)
        {
            Username = username;
            RacingBehavior = racingBehavior;
            DrivingExperience = drivingExperience;
            Car = car;
        }
        
        public bool IsAvailable()
        {
            if (Car.FuelAvailable >= Car.FuelConsumptionPerRace)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void Race()
        {
            Car.Drive();
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {Username}\n--Driving behavior: {RacingBehavior}\n--Driving experience: {DrivingExperience}\n--Car: {Car.Make} {Car.Model} ({Car.VIN})";
        }
    }
}
