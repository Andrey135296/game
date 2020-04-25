using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	class Room
	{
        public List<Cell> Cells;
        public List<CrewMember> CrewMembers;
        public int Durability;
        public bool IsIntact;


        public Room(List<Cell> cells)
		{
            Cells = cells;
            CrewMembers = cells.Select(c=>c.stationed).Where(c=>c!=null).ToList();
            Durability = 100;
            IsIntact = true;
		}
	}

    class EngineRoom : Room
    {
        public EngineRoom(List<Cell> cells) : base(cells)
        {

        }
    }
}
