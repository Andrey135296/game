using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public abstract class Ship
	{
		public List<CrewMember> Crew;
		public List<Room> Rooms;
		public List<SpecialRoom> SpecialRooms;
		public List<Cell> Cells;
		public ShipStat Stats;
		public List<Weapon> Weapons;
		public Alignment Alignment;
	}
}