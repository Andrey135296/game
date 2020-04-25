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
			var nodes = new List<Node>();
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
					var coordinates = splitted[3].Split(',');
					var x = int.Parse(coordinates[0]);
					var y = int.Parse(coordinates[1]);
					nodes.Add(new Node((Alignment)alignment, (NodeType)nodeType, new Point(x, y)));
					if ((Alignment)alignment)
				}
				else
				{
					var n1 = int.Parse(splitted[0]);
					var n2 = int.Parse(splitted[1]);
					nodes[n1].neighbors.Add(nodes[n2]);
				}
			}
		}
	}
}
