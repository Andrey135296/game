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
		private int currentHP;
		public int CurrentHP
		{
			get { return currentHP; }
			set
			{
				if (currentHP != value)
				{
					currentHP = value;
					if (OnChange != null)
						OnChange.Invoke();
				}
			}
		}
		public Cell Destination;
		private CrewAction action;
        public CrewAction Action
		{
			get { return action; }
			set 
			{ 
				if (action != value)
				{
					action = value;
					if (OnChange != null)
						OnChange.Invoke();
				}
			}
		}
		public event Action OnChange;
        public bool IsAlive;
		public int RepairSpeed;
		public Alignment Alignment;
        private static readonly List<string> names = new List<string> { "Коля", "Петя", "Вася" };
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
			if (cell != null)
				cell.Stationed = this;
			this.Alignment = alignment;
        }
	}
}