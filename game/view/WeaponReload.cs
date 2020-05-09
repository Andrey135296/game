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
	public partial class WeaponReload : UserControl
	{
		public Weapon Weapon;
		public int Max;
		public WeaponReload(Weapon weapon)
		{
			InitializeComponent();
			
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
		}
	}
}
