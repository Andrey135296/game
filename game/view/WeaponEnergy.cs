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
	public partial class WeaponEnergy : UserControl
	{
		public Weapon Weapon;
		public int Max, Current;
		public List<EnergyCell> Cells;
		public WeaponEnergy(Weapon weapon)
		{
			InitializeComponent();
			this.Weapon = weapon;
			Max = Weapon.EnergyPrice;
			Cells = new List<EnergyCell>();
			var t = new TableLayoutPanel();
			t.RowCount = 1;
			t.ColumnCount = Max;
			for (int i = 0; i < Max; i++)
				t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.0f / Max));
			t.BackColor = Color.Transparent;
			t.Dock = DockStyle.Fill;
			for (int i = 0; i < Max; i++)
			{
				var c = new EnergyCell();
				c.state = 1;
				Cells.Add(c);
				c.Dock = DockStyle.Fill;
				t.Controls.Add(c, i, 0);
			}
			Controls.Add(t);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Current = Weapon.IsOnline ? Max : 0;
		}
	}
}
