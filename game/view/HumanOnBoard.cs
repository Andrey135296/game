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
	public partial class HumanOnBoard : UserControl
	{
		public Human Human;
		public HumanOnBoard(Human human)
		{
			InitializeComponent();
			BackColor = Color.Transparent;
			Size = new Size(20, 20);
			this.Human = human;
			Human.OnGraphicalChanges += () => this.Invalidate();
			Human.crewMember.OnChange += () => this.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var crewMember = this.Human.crewMember;
			var act = crewMember.Action;
			var cl = Color.White;
			switch (act)
			{
				case CrewAction.Idle:
					cl = Color.Blue;
					break;
				case CrewAction.Moving:
					cl = Color.Green;
					break;
				case CrewAction.Working:
					cl = Color.Orange;
					break;
			}
			var health = 1.0f * crewMember.CurrentHP / crewMember.MaxHP;
			e.Graphics.FillPie(new SolidBrush(cl), 0, 0, Width-1, Height-1, 90 - 360 * health/2, 360 * health);
			if (this.Human.IsSelected)
				e.Graphics.DrawRectangle(new Pen(Color.Red), 0, 0, Width - 1, Height - 1);
		}
	}
}
