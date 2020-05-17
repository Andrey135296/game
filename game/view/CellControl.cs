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
	public partial class CellControl : UserControl
	{
		public Cell cell;
		public CellControl(Cell cell)
		{
			InitializeComponent();
			BackColor = Color.Transparent;
			this.cell = cell;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//e.Graphics.DrawRectangle(new Pen(Color.Gray, 3), 4, 4, Width-9, Height-9);
		}
	}
}
