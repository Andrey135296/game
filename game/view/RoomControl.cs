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
	public partial class RoomControl : UserControl
	{
		public List<CellControl> Cells = new List<CellControl>();
		public Room Room;
		public RoomControl(Room room)
		{
			InitializeComponent();
			Room = room;
			Cells = Room.Cells.Select(c => new CellControl(c)).ToList();
			var minx = Room.Cells.Min(c => c.Coordinates.X);
			var maxx = Room.Cells.Max(c => c.Coordinates.X);
			var miny = Room.Cells.Min(c => c.Coordinates.Y);
			var maxy = Room.Cells.Min(c => c.Coordinates.Y);

			var t = new TableLayoutPanel();
			t.Dock = DockStyle.Fill;
			t.RowCount = maxy - miny + 1;
			t.ColumnCount = maxx - minx + 1;
			t.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			for (int i = 0; i < t.RowCount; i++)
				t.RowStyles.Add(new RowStyle(SizeType.Percent, 1.0f / t.RowCount));
			for (int i = 0; i < t.ColumnCount; i++)
				t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.0f / t.ColumnCount));

			foreach (var cellControl in Cells)
			{
				cellControl.Dock = DockStyle.Fill;
				t.Controls.Add(cellControl, cellControl.cell.Coordinates.X - minx, cellControl.cell.Coordinates.Y - miny);
			}

			Controls.Add(t);
			Padding = new Padding(1, 1, 1, 1);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.DrawRectangle(new Pen(Color.Black, 1), 0, 0, Width - 1, Height - 1);
		}
	}
}
