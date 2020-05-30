using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;
using System.Timers;
using System.Windows.Forms;

namespace game
{
    class Program
    {
        static void Main(string[] args)
        {
			var map = Map.LoadFromFile(@"maps\map1.txt");
			var g = new GameModel(new Titan(Alignment.Player), map);
			var t = new System.Timers.Timer();
			t.Interval = g.TickLength;
			t.Elapsed += new ElapsedEventHandler((s, e) => GameTick.Tick(g));
			t.AutoReset = true;
			t.Start();
			var mf = new MainForm(g);
			Application.Run(mf);
		}
    }
}