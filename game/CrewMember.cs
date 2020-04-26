using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	class CrewMember
	{
        public int Speed;
        public Cell Cell;
        public string Name;
        public int HP;
        public Cell Destination;
        public CrewAction Action;
        public bool IsAlive;
        private static readonly List<string> names = new List<string> { "Kolya", "Petya" };


		public CrewMember(Cell cell)
		{
            Speed = 1;
            Cell = cell;
			Destination = null;
			cell.stationed = this;
			Name = GetName();
            HP = 100;
            Action = CrewAction.Idle;
            IsAlive = true;
		}

        private string GetName()
        {
            var random = new Random();
            return names[random.Next(0, names.Count)];
        }
	}
}