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
			var d = new Dictionary<int, Node>();
			var num = 0;
			foreach (var line in data.Split('\n'))
			{
				var coord = line.Split('-')[0];
				var neighbors = line.Split('-')[1];


				num++;
			}
		}
	}
}
