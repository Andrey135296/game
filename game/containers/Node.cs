using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace game
{
	public class Node
	{
		public List<Node> Neighbors;
		public Alignment Alignment;
		public NodeType Type;
		public Point Coordinates;

		public Node(Alignment alignment, NodeType nodeType, List<Node> neighbors, Point pos)
		{
			this.Neighbors = neighbors.ToList();
			this.Alignment = alignment;
			this.Type = nodeType;
			this.Coordinates = pos;
		}

		public Node(Alignment alignment, NodeType nodeType, Point pos)
		{
			this.Neighbors = new List<Node>();
			this.Alignment = alignment;
			this.Type = nodeType;
			this.Coordinates = pos;
		}
	}
}