using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;
using System;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if (!racerTwo.IsAvailable())
            {
                if (racerOne.IsAvailable())
                {
                    return string.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);
                }
                return OutputMessages.RaceCannotBeCompleted;
            }
            else if (!racerOne.IsAvailable())
            {
                if (racerTwo.IsAvailable())
                {
                    return string.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
                }
                return OutputMessages.RaceCannotBeCompleted;
            }

            racerOne.Race();
            racerTwo.Race();
            return GetWinner(racerOne, racerTwo);
        }

        private string GetWinner(IRacer racerOne, IRacer racerTwo)
        {
            double racerOneChance = racerOne.Car.HorsePower * racerOne.DrivingExperience * GetBehaviorMultiplier(racerOne.RacingBehavior);
            double racerTwoChance = racerTwo.Car.HorsePower * racerTwo.DrivingExperience * GetBehaviorMultiplier(racerTwo.RacingBehavior);

            if (racerOneChance > racerTwoChance)
            {
                return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, racerOne.Username);
            }
            else if (racerOneChance < racerTwoChance)
            {
                return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, racerTwo.Username);
            }
            else
            {
                throw new Exception();
            }
        }

        private double GetBehaviorMultiplier(string racingBehavior)
        {
            if (racingBehavior == "strict")
            {
                return 1.2;
            }
            else if (racingBehavior == "aggressive")
            {
                return 1.1;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
