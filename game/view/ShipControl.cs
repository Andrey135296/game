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
	public partial class ShipControl : UserControl
	{
		public Ship Ship;
		public List<RoomControl> Rooms;
		public int MaxX, MaxY, MinX, MinY;
		public ShipControl(Ship ship)
		{
			InitializeComponent();
			Ship = ship;
			Rooms = Ship.Rooms.Select(r => new RoomControl(r)).ToList();
			if (Ship is Titan || Ship is TestTitan)
			{
				BackgroundImage = new Bitmap("images/Titan.png");
				BackgroundImageLayout = ImageLayout.Stretch;
			}
			BackColor = Color.Transparent;
			MaxX = Rooms.Max(r => r.MaxX);
			MinX = Rooms.Min(r => r.MinX);
			MaxY = Rooms.Max(r => r.MaxY);
			MinY = Rooms.Min(r => r.MinY);

			var bt = new TableLayoutPanel();
			bt.Dock = DockStyle.Fill;
			bt.BackColor = Color.Transparent;
			bt.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			bt.RowCount = 3;
			bt.ColumnCount = 3;
			bt.RowStyles.Add(new RowStyle(SizeType.Percent, 22));
			bt.RowStyles.Add(new RowStyle(SizeType.Percent, 48));
			bt.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
			bt.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16));
			bt.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78));
			bt.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6));

			var t = new TableLayoutPanel();
			t.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			t.Dock = DockStyle.Fill;
			t.Margin = new Padding(0, 0, 0, 0);
			t.ColumnCount = MaxX - MinX + 1;
			t.RowCount = MaxY - MinY + 1;
			for (int i = 0; i < t.RowCount; i++)
				t.RowStyles.Add(new RowStyle(SizeType.Percent, 1.0f / t.RowCount));
			for (int i = 0; i < t.ColumnCount; i++)
				t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.0f / t.ColumnCount));

			bt.Controls.Add(t, 1, 1);


			Controls.Add(bt);
		}
	}
}
