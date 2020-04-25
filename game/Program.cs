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
            var cell1 = new Cell(new Point(1, 1));
            var cell2 = new Cell(new Point(2, 2));
            var cells = new List<Cell> { cell1, cell2 };
            var emptyroom = new EmptyRoom(cells);
            var s = new RadarRoom(emptyroom, 2, 1);
            Console.WriteLine(s.CurrentEnergy);
            Console.WriteLine(s.CurrentEnergyLimit);
            Console.WriteLine(s.EnergyLimit);
            Console.WriteLine(s.Type.ToString());
            if (s.Cells == cells)
                Console.WriteLine(123);
        }
    }
}