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
	}
}
