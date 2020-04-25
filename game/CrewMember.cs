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
        public Point Coordinates;
        public string Name;
        public int HP;
        public Point Destination;
        public CrewAction Action;
        public bool IsAlive;
        private static readonly List<string> names = new List<string> { "Kolya", "Petya" }


		public CrewMember(Point coordinates)
		{
            Speed = 1;
            Coordinates = coordinates;
            Name = GetName();
            Destination = coordinates;
            HP = 100;
            Action = 0;
            IsAlive = true;
		}

        private string GetName()
        {
            var random = new Random();
            return names[random.Next(0, names.Count);];
        }
	}
}
