using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
	public partial class EnergyBar : UserControl
	{
		public int Max;
		public int Unlocked;
		public int Active;
		private List<EnergyCell> cells = new List<EnergyCell>();
		private SpecialRoom room;
		private Ship ship;
		public static Dictionary<Room, List<EnergyBar>> allBars = new Dictionary<Room, List<EnergyBar>>();
		public EnergyBar(SpecialRoom room, Ship ship)
		{
			InitializeComponent();
			this.ship = ship;
			if (!allBars.ContainsKey(room))
				allBars[room] = new List<EnergyBar>();
			allBars[room].Add(this);
			Max = room.Stat.MaxEnergyLimit;
			Unlocked = room.Stat.CurrentEnergyLimit;
			Active = room.Stat.CurrentEnergy;
			if (room.Type == RoomType.Generator)
				Active = ship.Stats.FullEnergy - ship.Stats.CurrentEnergy;
			this.room = room;
			var t = new TableLayoutPanel();
			//this.BackColor = Color.Transparent;
			t.ColumnCount = Max;
			for (int i = 0; i < Max; i++)
				t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)(1.0 / Max)));
			t.RowCount = 1;
			t.Dock = DockStyle.Fill;
			//t.BackColor = Color.Transparent;
			for (int i = 0; i < Max; i++)
			{
				var cell = new EnergyCell();
				var j = i+1;
				cell.Click += (s, e) =>
				{
					if (cells[j-1].state==2 && ((j<Max && cells[j].state!=2) || (j==Max)))
						PlayerCommands.TrySetRoomEnergyConsumption(room, j-1, ship);
					else
						PlayerCommands.TrySetRoomEnergyConsumption(room, j, ship);
					foreach (var bar in ship.SpecialRooms.SelectMany(r => allBars[r]))
						bar.Invalidate();
				};
				cell.Dock = DockStyle.Fill;
				cell.Margin = new Padding(0, 0, 0, 0);
				cells.Add(cell);
				t.Controls.Add(cell, i, 0);
			}
			Controls.Add(t);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Unlocked = room.Stat.CurrentEnergyLimit;
			Active = room.Stat.CurrentEnergy;
			if (room.Type == RoomType.Generator)
				Active = ship.Stats.FullEnergy - ship.Stats.CurrentEnergy;
			for (int i = 0; i < Active; i++)
				cells[i].state = 2;
			for (int i = Active; i < Unlocked; i++)
				cells[i].state = 1;
			for (int i = Unlocked; i < Max; i++)
				cells[i].state = 0;
			base.OnPaint(e);
			foreach (var cell in cells)
				cell.Invalidate();
		}
	}
}
