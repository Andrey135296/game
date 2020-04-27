//using NUnitLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace game
{
    class Program
    {
        static void Main(string[] args)
        {
			//var a = new Titan();
			//         var c = 0;
			var m = new Map("Player-empty-100,100");
			var a = new List<int> { 1, 2, 3 };
			foreach (var i in a)
				if (i == 2)
					a.Remove(i);
        }
    }
}