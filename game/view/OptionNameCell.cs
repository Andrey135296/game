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
    public partial class OptionNameCell : UserControl
    {
        public OptionNameCell(string name)
        {
            InitializeComponent();
            this.Size = new Size(365, 65);
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;

            var t = new TableLayoutPanel();
            t.Size = new Size(355, 55);
            var panel = new Panel();
            panel.Size = new Size(355, 55);
            panel.BackColor = Color.White;
            panel.Dock = DockStyle.Fill;


            var label = new Label();
            label.Text = name;
            label.Font = new Font("Segoe UI", 18F, FontStyle.Regular,
                GraphicsUnit.Point, ((byte)(204)));
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
