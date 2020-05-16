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
		public int Max, Current;
		public WeaponReload(Weapon weapon)
		{
			InitializeComponent();
			this.Weapon = weapon;
			Max = Weapon.CoolDownTime;
			BackColor = Color.LightGray;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Current = Max - Weapon.TimeLeftToCoolDown;
			var part = Current * 1.0 / Max;
			e.Graphics.FillRectangle(new SolidBrush(Color.Green), 0, 0, (float)(Width * part), Height);
		}
	}
}
