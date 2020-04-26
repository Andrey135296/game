using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	class ShipStat
	{
		public int Evasion = 0;
		public int Radar = 0;
		public double DamageMultiplier = 1.0;
		public int Heal = 0;
		public int HP;
		public int FullEnergy;
		public int CurrentEnergy;
        //public Dictionary<RoomType, EnergyStat> EnergyConsumption;  

		public ShipStat(int hp)
		{
			this.HP = hp;
			this.FullEnergy = 0;
			this.CurrentEnergy = FullEnergy;
			//this.EnergyConsumption = new Dictionary<RoomType, EnergyStat>();
   //         foreach (var roomType in Enum.GetValues(typeof(RoomType)))
   //         {
   //             EnergyConsumption[(RoomType)roomType] = new EnergyStat(0, 0, 0);
   //         }
		}
	}
}
