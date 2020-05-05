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
			e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0, 0), 2), 0, 0, this.Width, this.Height);
			switch (state)
			{
				case 0:
					BackColor = Color.Gray;
					break;
				case 1:
					BackColor = Color.White;
					break;
				case 2:
					BackColor = Color.Green;
					break;
				default:
					throw new Exception("ошибка, произошла ошибка");
			}
		}
	}
}
