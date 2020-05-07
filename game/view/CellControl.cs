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
		Cell cell;
		public CellControl()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(new Pen(Color.Black), 2, 2, Width-5, Height-5);
		}
	}
}
