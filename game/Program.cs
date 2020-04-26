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
            var l = new List<int>();
            for (int i = 0; i < 10000000; i++)
                l.Add(1);
            for (int i  = 0; i < 10000000; i++)
            {
                l.Last();
            }
        }
    }
}