﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	class Node
	{
		public List<Node> neighbors;
		public Alignment alignment;
		public NodeType nodeType;
	}
}
