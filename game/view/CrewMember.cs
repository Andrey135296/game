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
		public Human()
		{
			InitializeComponent();
			BackgroundImage = new Bitmap("images/Human.png");
			BackgroundImageLayout = ImageLayout.Stretch;
			Size = new Size(20, 60);
			this.Click += (s, e) => this.Top=100;
		}
	}
}
