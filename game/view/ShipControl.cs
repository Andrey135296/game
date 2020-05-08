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

			var bt = new TableLayoutPanel();
			bt.Dock = DockStyle.Fill;
			bt.BackColor = Color.Transparent;
			bt.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			bt.RowCount = 3;
			bt.ColumnCount = 3;
			bt.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
			bt.RowStyles.Add(new RowStyle(SizeType.Percent, 80));
			bt.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
			bt.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
			bt.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
			bt.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));


			Controls.Add(bt);
		}
	}
}
