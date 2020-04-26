using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	class ShipStat
	{
		public int Evasion;
		public int HP;
		public int FullEnergy;
		public int CurrentEnergy;
        public Dictionary<RoomType, EnergyStat> EnergyConsumption;  

		public ShipStat(int evasion, int hp)
		{
			this.Evasion = evasion;
			this.HP = hp;
			this.FullEnergy = 0;
			this.CurrentEnergy = FullEnergy;
            foreach (var roomType in Enum.GetValues(typeof(RoomType)))
            {
                EnergyConsumption[(RoomType)roomType] = new EnergyStat(0, 0, 0);
            }
		}
	}
}
