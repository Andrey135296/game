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
	public partial class CrewPanel : UserControl
	{
		//TableLayoutPanel t;
		public CrewPanel(CrewMember[] crew)
		{
			this.Size = new Size(300, 200);
			this.BackColor = Color.Transparent;
			InitializeComponent();
			var t = new TableLayoutPanel();
			t.RowCount = 2;
			for (int i = 0; i < 2; i++)
				t.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
			t.ColumnCount = 4;
			for (int i = 0; i < 4; i++)
				t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
			t.Size = new Size(300, 170);
			for (int i = 0; i < 8; i++)
			{
				var panel = new Panel();
				var human = new Human(crew[i]);
				var label = new Label();
				label.Text = crew[i].Name;
				panel.Controls.Add(human);
				human.Size = new Size(20, 60);
				panel.Controls.Add(label);
				label.Size = new Size(60, 20);
				label.Top = 60;
				panel.Dock = DockStyle.Fill;
				panel.Size = new Size(t.Size.Width / 4, t.Size.Height / 2);
				t.Controls.Add(panel, i % 4, i / 4);
			}
			t.Top = 30;
			this.Controls.Add(t);
			var label2 = new Label();
			label2.Text = "Комманда";
			label2.Size = new Size(300, 30);
			label2.TextAlign = ContentAlignment.MiddleCenter;
			this.Controls.Add(label2);
		}

		public CrewPanel(List<CrewMember> crew) : this(crew.ToArray())
		{
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
		}
	}
}
