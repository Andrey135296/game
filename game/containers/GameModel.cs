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
		public int Money = 0;
		public int Fuel = 20;
		public Map Map;
		public bool IsRunning = false;
		/// <summary>
		/// tick time in milliseconds
		/// </summary>
		public int TickLength = 200;

		public GameModel(Ship ship1, string mapString)
		{
			PlayerShip = ship1;
			Map = new Map(mapString);
		}

		public GameModel(Ship ship, Map map)
		{
			PlayerShip = ship;
			this.Map = map;
		}
	}
}