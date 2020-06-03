using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public abstract class Weapon
	{
		public Room Target;
		public int Damage;
		/// <summary>
		/// Cooldown time in milliseconds
		/// </summary>
		public int CoolDownTime;
		/// <summary>
		/// Time left to Cool down in milliseconds
		/// </summary>
		public int TimeLeftToCoolDown;
		public string Name;
		public bool IsOnline;
		//public int EnergyPrice;
		public int Price;
	}

	public class LaserM0 : Weapon
	{
		public LaserM0()
		{
			this.Damage = 10;
			this.CoolDownTime = 6000;
			this.TimeLeftToCoolDown = this.CoolDownTime;
			this.Name = "LaserM0";
			this.IsOnline = false;
			//this.EnergyPrice = 1;
			this.Price = 25;
		}
	}

	public class LaserM1 : Weapon
	{
		public LaserM1()
		{
			this.Damage = 20;
			this.CoolDownTime = 10000;
			this.TimeLeftToCoolDown = this.CoolDownTime;
			this.Name = "LaserM1";
			this.IsOnline = false;
			//this.EnergyPrice = 2;
			this.Price = 75;
		}
	}

	public class LaserM2 : Weapon
	{
		public LaserM2()
		{
			this.Damage = 30;
			this.CoolDownTime = 15000;
			this.TimeLeftToCoolDown = this.CoolDownTime;
			this.Name = "LaserM2";
			this.IsOnline = false;
			//this.EnergyPrice = 3;
			this.Price = 200;
		}
	}
}