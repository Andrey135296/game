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
	public partial class Human : UserControl, ISelectable
	{
		public CrewMember crewMember;
		public bool IsSelected { get; set; }
		public Human()
		{
			InitializeComponent();
			BackgroundImage = new Bitmap("images/Human.png");
			BackgroundImageLayout = ImageLayout.Stretch;
			Size = new Size(20, 60);
			BackColor = Color.Transparent;
		}

		public Human(CrewMember crewMember) : this()
		{
			this.crewMember = crewMember;
		}


		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var cl = Color.FromArgb(0, 255, 0, 0);
			if (!(crewMember is null))
				cl = Color.FromArgb(255-255*crewMember.CurrentHP / crewMember.MaxHP, 255, 0, 0);
			e.Graphics.FillEllipse(new SolidBrush(cl), (int)(Size.Width*1.5/20), 0, Size.Width*15/20, Size.Height*14/60);
			if (IsSelected)
				e.Graphics.DrawRectangle(new Pen(Color.Red), 0, 0, Width-1, Height-1);
		}
	}
}
