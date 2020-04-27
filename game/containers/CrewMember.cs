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
        public int MaxHP;
		public int CurrentHP;
        public Cell Destination;
        public CrewAction Action;
        public bool IsAlive;
		public int RepairSpeed;
		public Alignment alignment;
        private static readonly List<string> names = new List<string> { "Kolya", "Petya" };


		public CrewMember(Cell cell, Alignment alignment)
		{
            Speed = 1;
            Cell = cell;
			Destination = null;
			Name = GetName();
            MaxHP = 100;
			CurrentHP = 100;
            Action = CrewAction.Idle;
            IsAlive = true;
			RepairSpeed = 1;
            cell.stationed = this;
			this.alignment = alignment;
        }

        private string GetName()
        {
            var random = new Random();
            return names[random.Next(0, names.Count)];
        }
	}
}