using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public class RoomStat
    {
        public int MaxEnergyLimit;
        public int CurrentEnergyLimit;
        public int CurrentEnergy;
        public int MaxWorkingSpaces;
        public int EmptyWorkingSpaces;

        public RoomStat(int energyLimit, int currentEnergyLimit, int currentEnergy, int maxWorkingSpaces)
        {
            MaxEnergyLimit = energyLimit;
            CurrentEnergy = currentEnergy;
            CurrentEnergyLimit = currentEnergyLimit;
            MaxWorkingSpaces = maxWorkingSpaces;
            EmptyWorkingSpaces = maxWorkingSpaces;
        }
    }
}
