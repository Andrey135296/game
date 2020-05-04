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
			BackgroundImage = new Bitmap("images/opt.png");
			BackgroundImageLayout = ImageLayout.Stretch;
			Size = new Size(20, 60);
			BackColor = Color.Transparent;
			//Click += (s, e) => this.Top=100;

		}
	}
}
