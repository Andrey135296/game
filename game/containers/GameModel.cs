using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public class GameModel
	{
		public Ship Ship1;
		public Ship Ship2;
		public int Money;
		public int Fuel;
		public Map Map;
		/// <summary>
		/// tick time in milliseconds
		/// </summary>
		public int TickLength = 200;

		public GameModel(Ship s1, string mapString)
		{
			Ship1 = s1;
			Money = 0;
			Fuel = 20;
			Map = new Map(mapString);
		}

		public GameModel(Ship s1, Map map)
		{
			Ship1 = s1;
			Money = 0;
			Fuel = 20;
			this.Map = map;
		}
	}
}