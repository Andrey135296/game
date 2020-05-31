using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public class Room
    {
        public List<Cell> Cells;
        public List<CrewMember> CrewMembers;
		private int currentDurability;
        public int CurrentDurability 
		{ 
			get { return currentDurability; }
			set 
			{ 
				if (currentDurability != value)
				{
					currentDurability = value;
					if (OnDurabilityChange != null)
						OnDurabilityChange.Invoke();
				}
			}
		}
		public event Action OnDurabilityChange;
        public int MaxDurability;
        public bool IsIntact;
        public RoomType Type;

        public Room(List<Cell> cells)
        {
            Cells = cells;
            CrewMembers = cells.Select(cell => cell.Stationed).Where(crew => crew != null).ToList();
            MaxDurability = 100;
            CurrentDurability = MaxDurability;
            IsIntact = true;
        }
    }

    public class SpecialRoom : Room
    {
        //public bool IsOnline;
        public RoomStat Stat;
        //public List<CrewMember> WorkingCrewMembers;

        public SpecialRoom(List<Cell> cells, RoomType roomType, RoomStat roomStat) : base(cells)
        {
            //IsOnline = false;
            Type = roomType;
            Stat = roomStat;
        }
        public SpecialRoom(Room room, RoomType roomType, RoomStat roomStat) : base(room.Cells)
        {
            Type = roomType;
            Stat = roomStat;
		}
    }
}