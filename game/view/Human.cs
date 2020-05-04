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
			//Click += (s, e) => this.Top=100;

		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.DrawLine(new Pen(Color.Red), 0, 0, 100, 100);
		}
	}
}
