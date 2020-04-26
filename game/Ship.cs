using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	abstract class Ship
	{
		public List<CrewMember> Crew;
		public List<Room> Rooms;
		public List<Cell> Cells;
		public ShipStat Stats;
	}

	class Titan : Ship
	{
		public Titan()
		{
			Stats = new ShipStat(10, 20);
			GenerateCells();

		}

		private void GenerateCells()
		{
			for (int i = 6; i < 8; i++)
				Cells.Add(new Cell(i, 0));
			for (int i = 4; i < 12; i++)
				Cells.Add(new Cell(i, 1));
			for (int i = 0; i < 14; i++)
				Cells.Add(new Cell(i, 2));
			for (int i = 2; i < 12; i++)
				Cells.Add(new Cell(i, 3));
			foreach (var cell in Cells)
			{
				foreach (var otherCell in Cells)
				{
					var dx = otherCell.coordinates.X - cell.coordinates.X;
					var dy = otherCell.coordinates.Y - cell.coordinates.Y;
					if (dx == 1 && dy == 0)
						cell.neighbors.right = otherCell;
					if (dx == -1 && dy == 0)
						cell.neighbors.left = otherCell;
					if (dx == 0 && dy == 1)
						cell.neighbors.down = otherCell;
					if (dx == 0 && dy == -1)
						cell.neighbors.up = otherCell;
				}
			}
		}
	}
}