using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public class GameModel
	{
		public Ship ship1;
		public Ship ship2;
		public int money;
		public int fuel;
		public Map map;
		/// <summary>
		/// tick time in milliseconds
		/// </summary>
		public int tickLength = 200;

		public GameModel(Ship s1, string mapString)
		{
			ship1 = s1;
			money = 0;
			fuel = 20;
			map = new Map(mapString);
		}

		public GameModel(Ship s1, Map map)
		{
			ship1 = s1;
			money = 0;
			fuel = 20;
			this.map = map;
		}
	}
}