using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public class GameModel
	{
		public Ship PlayerShip;
		public Ship OtherShip;
		public int Money;
		public int Fuel;
		public Map Map;
		/// <summary>
		/// tick time in milliseconds
		/// </summary>
		public int TickLength = 200;

		public GameModel(Ship ship1, string mapString)
		{
			PlayerShip = ship1;
			Money = 0;
			Fuel = 20;
			Map = new Map(mapString);
		}

		public GameModel(Ship ship, Map map)
		{
			PlayerShip = ship;
			Money = 0;
			Fuel = 20;
			this.Map = map;
		}
	}
}