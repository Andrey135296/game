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
	public partial class WeaponPanel : UserControl
	{
		public Ship Ship;
		public WeaponPanel(Ship ship)
		{
			InitializeComponent();
			Ship = ship;
			Size = new Size(150, 171);

			var t = new TableLayoutPanel();
			t.RowCount = 3;
			t.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
			t.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
			t.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
			t.ColumnCount = 2;
			t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
			t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
			t.Dock = DockStyle.Fill;
			t.Margin = new Padding(0, 0, 0, 0);
			t.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			Controls.Add(t);

			var label = new Label();
			label.Text = "Орудия";
			label.TextAlign = ContentAlignment.MiddleCenter;
			label.Dock = DockStyle.Fill;
			label.Font = new Font(FontFamily.GenericSerif, 15, FontStyle.Bold);
			t.Controls.Add(label, 0, 0);
			t.SetColumnSpan(label, 2);

			for (int i = 0; i<Ship.Weapons.Count && i<4; i++)
			{
				var weaponCell = new WeaponCell(Ship.Weapons[i]);
				weaponCell.Dock = DockStyle.Fill;
				weaponCell.Margin = new Padding(0, 0, 0, 0);
				t.Controls.Add(weaponCell, i % 2, 1 + i / 2);
			}
		}
	}
}
