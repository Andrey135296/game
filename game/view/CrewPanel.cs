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
			this.Size = new Size(302, 171);
			
			var t = new TableLayoutPanel();
			t.RowCount = 3;
			t.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
			for (int i = 0; i < 2; i++)
				t.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
			t.ColumnCount = 4;
			for (int i = 0; i < 4; i++)
				t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
			t.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			t.Dock = DockStyle.Fill;

			for (int i = 0; i < 8 && i<crew.Length; i++)
			{
				var panel = new Panel();
				panel.Dock = DockStyle.Fill;

				var human = new Human(crew[i]);
				human.Size = new Size(20, 50);
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
				label.Top = 45;
				label.TextAlign = ContentAlignment.MiddleCenter;
				panel.Controls.Add(label);

				t.Controls.Add(panel, i % 4, 1+(i / 4));
			}

			var label2 = new Label();
			label2.Text = "Команда";
			label2.Dock = DockStyle.Fill;
			label2.Font = new Font(FontFamily.GenericSerif, 15, FontStyle.Bold);
			label2.TextAlign = ContentAlignment.MiddleCenter;
			t.Controls.Add(label2, 0, 0);
			t.SetColumnSpan(label2, 4);

			this.Controls.Add(t);
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
