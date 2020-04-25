using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	class GameModel
	{
		public Ship ship1;
		public Ship ship2;
		public int money;
		public int fuel;
		public Map map;

		public GameModel(Ship s1)
		{
			ship1 = s1;
			money = 0;
			fuel = 20;
			//map = new Map();
		}
	}
}
