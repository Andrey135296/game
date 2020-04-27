﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class EnergyStat
    {
        public int MaxEnergyLimit;
        public int CurrentEnergyLimit;
        public int CurrentEnergy;

        public EnergyStat(int energyLimit, int currentEnergyLimit, int currentEnergy)
        {
            MaxEnergyLimit = energyLimit;
            CurrentEnergy = currentEnergy;
            CurrentEnergyLimit = currentEnergyLimit;
        }
    }
}
