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
	public partial class WeaponCell : UserControl
	{
		public Weapon Weapon;
		public WeaponCell(Weapon weapon)
		{
			InitializeComponent();
			Size = new Size(70, 68);
			Weapon = weapon;
			var t = new TableLayoutPanel();
			t.RowCount = 3;
			t.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
			t.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
			t.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
			t.ColumnCount = 1;
			t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
			t.Dock = DockStyle.Fill;

			var weaponControl = new WeaponControl(Weapon);
			weaponControl.Dock = DockStyle.Fill;
			weaponControl.Margin = new Padding(0, 0, 0, 0);
			t.Controls.Add(weaponControl, 0, 0);

			var label = new Label();
			label.Text = Weapon.Name;
			label.Dock = DockStyle.Fill;
			label.TextAlign = ContentAlignment.MiddleCenter;
			t.Controls.Add(label, 0, 1);

			var reload = new WeaponReload(Weapon);
			reload.Margin = new Padding(0, 0, 0, 0);
			reload.Dock = DockStyle.Fill;
			t.Controls.Add(reload, 0, 2);

			Controls.Add(t);
		}
	}
}
