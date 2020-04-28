using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public class TestLaserM0 : Weapon
	{
		public TestLaserM0()
		{
			this.Damage = 10;
			this.CoolDownTime = 2000;
			this.TimeLeftToCoolDown = this.CoolDownTime;
			this.Name = "LaserM0";
			this.IsOnline = false;
			this.EnergyPrice = 1;
			this.Price = 25;
		}
	}

	public class TestLaserM1 : Weapon
	{
		public TestLaserM1()
		{
			this.Damage = 10;
			this.CoolDownTime = 1000;
			this.TimeLeftToCoolDown = this.CoolDownTime;
			this.Name = "LaserM1";
			this.IsOnline = false;
			this.EnergyPrice = 2;
			this.Price = 75;
		}
	}

	public class TestLaserM2 : Weapon
	{
		public TestLaserM2()
		{
			this.Damage = 20;
			this.CoolDownTime = 1000;
			this.TimeLeftToCoolDown = this.CoolDownTime;
			this.Name = "LaserM2";
			this.IsOnline = false;
			this.EnergyPrice = 3;
			this.Price = 200;
		}
	}
}