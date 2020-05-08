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
	public partial class WeaponControl : UserControl, ISelectable
	{
		public Weapon Weapon;
		public bool IsSelected { get; set; }

		public WeaponControl(Weapon weapon)
		{
			InitializeComponent();
			this.Weapon = weapon;
			if (Weapon is LaserM0 || Weapon is LaserM1 || Weapon is LaserM2 ||
				Weapon is TestLaserM0 || Weapon is TestLaserM1 || Weapon is TestLaserM2)
				this.BackgroundImage = new Bitmap("images/Laser.png");
			BackColor = Color.Transparent;
			BackgroundImageLayout = ImageLayout.Stretch;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (IsSelected)
				e.Graphics.DrawRectangle(new Pen(Color.Red), 0, 0, Width - 1, Height - 1);
		}
		
	}
}
