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
            CrewMembers = new List<CrewMember>();
            Durability = 100;
            IsIntact = true;
		}
	}
}
