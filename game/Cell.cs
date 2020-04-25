using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	class Cell
	{
		public Neighbors neighbors;
		public Point coordinates;
		public CrewMember stationed;
		public List<CrewMember> passing;

		public Cell(Point coord)
		{
			coordinates = coord;
		}

		public Cell(Point coord, Neighbors neighbors)
		{
			coordinates = coord;
			this.neighbors = neighbors;
		}
	}

	class Neighbors : IEnumerable<Cell>
	{
		public Cell left;
		public Cell right;
		public Cell up;
		public Cell down;

		public Neighbors(Cell left, Cell up, Cell right, Cell down)
		{
			this.left = left;
			this.up = up;
			this.right = right;
			this.down = down;
		}

		public IEnumerator<Cell> GetEnumerator()
		{
			if (left!=null)
				yield return left;
			if (up != null)
				yield return up;
			if (right != null)
				yield return right;
			if (down != null)
				yield return down;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
