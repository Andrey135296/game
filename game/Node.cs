using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace game
{
	class Node
	{
		public List<Node> neighbors;
		public Alignment alignment;
		public NodeType Type;
		public Point Coordinates;

		public Node(Alignment alignment, NodeType nodeType, List<Node> neighbors, Point pos)
		{
			this.neighbors = neighbors.ToList();
			this.alignment = alignment;
			this.Type = nodeType;
			this.Coordinates = pos;
		}
	}
}
