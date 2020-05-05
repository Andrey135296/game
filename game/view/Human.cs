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
	public partial class Human : UserControl
	{
		public CrewMember crewMember;
		public Human()
		{
			InitializeComponent();
			BackgroundImage = new Bitmap("images/Human.png");
			BackgroundImageLayout = ImageLayout.Stretch;
			Size = new Size(20, 60);
			BackColor = Color.Transparent;
			Click += (s, a) =>
			{
				crewMember.CurrentHP-=10;
				Invalidate();
			};
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var cl = Color.FromArgb(0, 255, 0, 0);
			if (!(crewMember is null))
				cl = Color.FromArgb(255-255*crewMember.CurrentHP / crewMember.MaxHP, 255, 0, 0);
			e.Graphics.FillEllipse(new SolidBrush(cl), 1, 0, 15, 14);
		}
	}
}
