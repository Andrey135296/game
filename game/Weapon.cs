using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	abstract class Weapon
	{
		public Room Target;
		public int damage;
		public int CoolDownTime;
		public int TimeLeftToCoolDown;
		public string name;
		public bool IsOnline;
		public int EnergyPrice;
		public int Price;
	}
}
