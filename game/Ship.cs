using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	class Ship
	{
		public List<CrewMember> Crew;
		public List<Room> Rooms;
		public int HP { get; private set; }
		public Ship(int hp)
		{
			Crew = new List<CrewMember>();
			Rooms = new List<Room>();
			HP = hp;
		}
	}
}
