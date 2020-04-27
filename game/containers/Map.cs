using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace game
{
	class Map
	{
		public Node CurrentNode;
		public List<Node> Nodes;


		public Map(string data)
		{
			Nodes = new List<Node>();
			bool nodesDefined = false;
			foreach (var line in data.Split('\n'))
			{
				if (line.StartsWith("//"))
					continue;
				if (line == "#")
				{
					nodesDefined = true;
					continue;
				}
				var splitted = line.Split('-');
				if (!nodesDefined)
				{
					var alignment = Enum.Parse(typeof(Alignment), splitted[0]);
					var nodeType = Enum.Parse(typeof(NodeType), splitted[1]);
					var coordinates = splitted[2].Split(',');
					var x = int.Parse(coordinates[0]);
					var y = int.Parse(coordinates[1]);
					Nodes.Add(new Node((Alignment)alignment, (NodeType)nodeType, new Point(x, y)));
				}
				else
				{
					var n1 = int.Parse(splitted[0]);
					var n2 = int.Parse(splitted[1]);
					Nodes[n1].neighbors.Add(Nodes[n2]);
					Nodes[n2].neighbors.Add(Nodes[n1]);
				}
			}
			CurrentNode = Nodes[0];
		}
	}
}