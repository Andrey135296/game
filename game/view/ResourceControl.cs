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
	public partial class ResourceControl : UserControl
	{
		private int amount;
		private Label text;
		public int Amount { get { return amount; } set { amount = value;  text.Text = value.ToString(); text.Invalidate(); } }
		public ResourceControl(Bitmap bmp, int amount)
		{
			InitializeComponent();
			
			var t = new TableLayoutPanel
			{
				RowCount = 1,
				ColumnCount = 2,
				Dock = DockStyle.Fill
			};
			t.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
			t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
			t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
			t.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			Controls.Add(t);

			var image = new PictureBox();
			image.BackgroundImage = bmp;
			image.Dock = DockStyle.Fill;
			image.BackgroundImageLayout = ImageLayout.Zoom;
			t.Controls.Add(image, 0, 0);

			text = new Label();
			text.Text = amount.ToString();
			text.Dock = DockStyle.Fill;
			text.TextAlign = ContentAlignment.MiddleCenter;
			t.Controls.Add(text, 1, 0);

			Amount = amount;
			Margin = new Padding(0, 0, 0, 0);
		}
	}
}
