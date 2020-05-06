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
		public CrewPanel(CrewMember[] crew)
		{
			InitializeComponent();
			this.Size = new Size(300, 200);
			this.BackColor = Color.White;
			
			var t = new TableLayoutPanel();
			t.RowCount = 2;
			for (int i = 0; i < 2; i++)
				t.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
			t.ColumnCount = 4;
			for (int i = 0; i < 4; i++)
				t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
			t.Size = new Size(300, 170);
			for (int i = 0; i < 8 && i<crew.Length; i++)
			{
				var panel = new Panel();
				panel.BackColor = Color.White;
				panel.Dock = DockStyle.Fill;

				var human = new Human(crew[i]);
				human.Size = new Size(20, 60);
				human.Left = 25;
				human.Click += (s, e) =>
				{
					human.crewMember.CurrentHP -= 10;
					human.Invalidate();
				};
				panel.Controls.Add(human);

				var label = new Label();
				label.Text = crew[i].Name;
				label.Size = new Size(60, 20);
				label.Top = 60;
				label.TextAlign = ContentAlignment.MiddleCenter;
				panel.Controls.Add(label);

				t.Controls.Add(panel, i % 4, i / 4);
			}
			t.Top = 30;
			t.BackColor = Color.Black;
			this.Controls.Add(t);
			var label2 = new Label();
			label2.Text = "Команда";
			label2.Size = new Size(300, 30);
			label2.Font = new Font(FontFamily.GenericSerif, 15, FontStyle.Bold);
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
	}
}
