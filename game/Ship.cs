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
		public int FullEnergy;
		public int CurrentEnergy;
		public List<Cell> Cells;
		public int evasion;
		public int HP;
	}
}
