using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	class Cell
	{
		public Neighbors neighbors;
	}

	class Neighbors
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
	}
}
