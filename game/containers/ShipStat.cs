using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public class ShipStat
	{
		public int Evasion = 0;
		public int Radar = 0;
		public double DamageMultiplier = 1.0;
		public int Heal = 0;
		public int CurrentHP;
		public int MaxHP;
		public int FullEnergy;
		public int CurrentEnergy;

		public ShipStat(int hp)
		{
			this.CurrentHP = hp;
			this.MaxHP = hp;
			this.FullEnergy = 0;
			this.CurrentEnergy = FullEnergy;
		}
	}
}
