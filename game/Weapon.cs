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
		/// <summary>
		/// Cooldown time in milliseconds
		/// </summary>
		public int CoolDownTime;
		/// <summary>
		/// Time left to Cool down in milliseconds
		/// </summary>
		public int TimeLeftToCoolDown;
		public string name;
		public bool IsOnline;
		public int EnergyPrice;
		public int Price;
	}

	class LaserM0 : Weapon
	{
		public LaserM0()
		{
			this.damage = 1;
			this.CoolDownTime = 2000;
			this.TimeLeftToCoolDown = this.CoolDownTime;
			this.name = "LaserM0";
			this.IsOnline = false;
			this.EnergyPrice = 1;
			this.Price = 25;
		}
	}

	class LaserM1 : Weapon
	{
		public LaserM1()
		{
			this.damage = 1;
			this.CoolDownTime = 1000;
			this.TimeLeftToCoolDown = this.CoolDownTime;
			this.name = "LaserM1";
			this.IsOnline = false;
			this.EnergyPrice = 2;
			this.Price = 75;
		}
	}

	class LaserM2 : Weapon
	{
		public LaserM2()
		{
			this.damage = 2;
			this.CoolDownTime = 1000;
			this.TimeLeftToCoolDown = this.CoolDownTime;
			this.name = "LaserM2";
			this.IsOnline = false;
			this.EnergyPrice = 3;
			this.Price = 200;
		}
	}
}
