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
	public partial class ResourcePanel : UserControl
	{
		private GameModel gameModel;
		private ResourceControl fuel, money;
		public ResourcePanel(GameModel gameModel)
		{
			InitializeComponent();
			this.gameModel = gameModel;
			Size = new Size(150, 100);
			fuel = new ResourceControl(new Bitmap("images/Fuel.png"), gameModel.Fuel);
			fuel.Dock = DockStyle.Fill;
			money = new ResourceControl(new Bitmap("images/Money.png"), gameModel.Fuel);
			money.Dock = DockStyle.Fill;

			var t = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1 };
			t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
			for (int i = 0; i < t.RowCount; i++)
				t.RowStyles.Add(new RowStyle(SizeType.Percent, 1.0f / t.RowCount));
			t.Controls.Add(fuel, 0, 0);
			t.Controls.Add(money, 0, 1);
			Controls.Add(t);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			fuel.Amount = gameModel.Fuel;
			money.Amount = gameModel.Money;
		}
	}
}
