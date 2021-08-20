using CarRacing.Models.Cars.Contracts;
using CarRacing.Utilities.Messages;
using System;

namespace CarRacing.Models.Cars
{
    public abstract class Car : ICar
    {
        private string _make;
        public string Make 
        { 
            get => _make;
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarMake);
                }
                else
                {
                    _make = value;
                }
            } 
        }

        private string _model;
        public string Model
        {
            get => _model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarModel);
                }
                else
                {
                    _model = value;
                }
            }
        }

        private string _VIN;
        public string VIN
        {
            get => _VIN;
            private set
            {
                if (value.Length != 17)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarVIN);
                }
                else
                {
                    _VIN = value;
                }
            }
        }

        private int _horsePower;
        public int HorsePower
        {
            get => _horsePower;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarHorsePower);
                }
                else
                {
                    _horsePower = value;
                }
            }
        }

        private double _fuelAvailable;
        public double FuelAvailable
        {
            get => _fuelAvailable;
            private set
            {
                if (value < 0)
                {
                    _fuelAvailable = 0;
                }
                else
                {
                    _fuelAvailable = value;
                }
            }
        }

        private double _fuelConsumption;
        public double FuelConsumptionPerRace
        {
            get => _fuelConsumption;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarFuelConsumption);
                }
                else
                {
                    _fuelConsumption = value;
                }
            }
        }

        public Car(string make, string model, string VIN, int horsePower, double fuelAvailable, double fuelConsumptionPerRace)
        {
            Make = make;
            Model = model;
            this.VIN = VIN;
            HorsePower = horsePower;
            FuelAvailable = fuelAvailable;
            FuelConsumptionPerRace = fuelConsumptionPerRace;
        }

        public virtual void Drive()
        {
            FuelAvailable -= FuelConsumptionPerRace;
        }
    }
}
