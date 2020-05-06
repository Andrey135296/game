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
	public partial class EnergyCell : UserControl
	{
		public int state = 0;
		public EnergyCell()
		{
			InitializeComponent();
			Size = new Size(20, 30);

			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			//this.BackColor = Color.Transparent;
			e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0, 0), 2), 2, 2, this.Width-4, this.Height-4);
			Color cl;
			switch (state)
			{
				case 0:
					cl = Color.Gray;
					break;
				case 1:
					cl = Color.White;
					break;
				case 2:
					cl = Color.Green;
					break;
				default:
					throw new Exception("ошибка, произошла ошибка");
			}
			e.Graphics.FillRectangle(new SolidBrush(cl), 2, 2, this.Width - 4, this.Height - 4);
		}
	}
}
