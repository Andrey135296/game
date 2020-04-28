//using NUnitLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;

namespace game
{
    class Program
    {
        static void Main(string[] args)
        {
			var m = Map.LoadFromFile(@"maps\mapExample.txt");
			var g = new GameModel(new Titan(Alignment.Player), m);
        }
    }
}