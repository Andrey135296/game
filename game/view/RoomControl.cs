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
		public int MaxX, MaxY, MinX, MinY;
		public RoomControl(Room room)
		{
			InitializeComponent();
			Room = room;
			Cells = Room.Cells.Select(c => new CellControl(c)).ToList();
			MinX = Room.Cells.Min(c => c.Coordinates.X);
			MaxX = Room.Cells.Max(c => c.Coordinates.X);
			MinY = Room.Cells.Min(c => c.Coordinates.Y);
			MaxY = Room.Cells.Max(c => c.Coordinates.Y);

			var t = new TableLayoutPanel();
			t.Dock = DockStyle.Fill;
			t.RowCount = MaxY - MinY + 1;
			t.ColumnCount = MaxX - MinX + 1;
			t.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			for (int i = 0; i < t.RowCount; i++)
				t.RowStyles.Add(new RowStyle(SizeType.Percent, 1.0f / t.RowCount));
			for (int i = 0; i < t.ColumnCount; i++)
				t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.0f / t.ColumnCount));

			foreach (var cellControl in Cells)
			{
				cellControl.Dock = DockStyle.Fill;
				cellControl.Margin = new Padding(0, 0, 0, 0);
				t.Controls.Add(cellControl, cellControl.cell.Coordinates.X - MinX, cellControl.cell.Coordinates.Y - MinY);
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
