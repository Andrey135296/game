using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public class Cell
	{
		public Neighbors Neighbors = new Neighbors();
		public Point Coordinates;
		public CrewMember Stationed;

		public Cell(Point coord)
		{
			Coordinates = coord;
		}

		public Cell(int x, int y)
		{
			Coordinates = new Point(x, y);
		}

		public Cell(Point coord, Neighbors neighbors)
		{
			Coordinates = coord;
			this.Neighbors = neighbors;
		}
	}

	public class Neighbors : IEnumerable<Cell>
	{
		public Cell Left;
		public Cell Right;
		public Cell Up;
		public Cell Down;

		public Neighbors()
		{
		}

		public Neighbors(Cell left, Cell up, Cell right, Cell down)
		{
			this.Left = left;
			this.Up = up;
			this.Right = right;
			this.Down = down;
		}

		public IEnumerator<Cell> GetEnumerator()
		{
			if (Left!=null)
				yield return Left;
			if (Up != null)
				yield return Up;
			if (Right != null)
				yield return Right;
			if (Down != null)
				yield return Down;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}