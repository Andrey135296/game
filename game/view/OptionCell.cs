using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game.view
{
    public partial class OptionCell : UserControl
    {
        public OptionCell(string str)
        {
            InitializeComponent();
            this.Size = new Size(365, 65);
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;

            var t = new TableLayoutPanel();
            t.Size = new Size(345, 55);
            var panel = new Panel();
            panel.Size = new Size(345, 55);
            panel.BackColor = Color.White;
            panel.Dock = DockStyle.Fill;


            var label = new Label();
            label.Text = str;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(label);

            t.Controls.Add(panel);
            t.BackColor = Color.Black;
            t.Dock = DockStyle.Fill;
            this.Controls.Add(t);
            this.Margin = new Padding(0, 0, 0, 0);
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
    }
}
