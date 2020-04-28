using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public class CrewMember
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
		public Alignment Alignment;
        private static readonly List<string> names = new List<string> { "Kolya", "Petya", "Vasya" };
        private static readonly Random random = new Random();


		public CrewMember(Cell cell, Alignment alignment)
		{
            Speed = 1;
            Cell = cell;
			Destination = null;
			Name = names[random.Next(0, names.Count)];
            MaxHP = 100;
			CurrentHP = 100;
            Action = CrewAction.Idle;
            IsAlive = true;
			RepairSpeed = 1;
            cell.Stationed = this;
			this.Alignment = alignment;
        }
	}
}