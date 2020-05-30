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
	public partial class HPBar : UserControl
	{
		public Ship Ship;
		private Label label;
		public HPBar(Ship ship)
		{
			InitializeComponent();
			this.Ship = ship;
			label = new Label() { BackColor = Color.Transparent, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter};
			Controls.Add(label);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var max = this.Ship.Stats.MaxHP;
			var cur = this.Ship.Stats.CurrentHP;
			label.Text = cur.ToString() + " / " + max.ToString();
			e.Graphics.FillRectangle(new SolidBrush(Color.Green), 0, 0, 1.0f*Width * cur / max, Height);
			e.Graphics.FillRectangle(new SolidBrush(Color.Red), Width * cur / max, 0,
				Width * (1 - 1.0f * cur / max), Height);
		}
	}
}
