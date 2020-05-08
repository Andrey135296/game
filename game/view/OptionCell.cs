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
        private int Value = 0;
        public OptionCell(string name, int value)
        {
            InitializeComponent();
            Value = value;
            this.Size = new Size(365, 65);
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;

            var t = new TableLayoutPanel();
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            t.Size = new Size(355, 55);
            var panel = new Panel();
            panel.Size = new Size(183, 55);
            panel.BackColor = Color.White;
            panel.Dock = DockStyle.Fill;
            var panel2 = new Panel();
            panel2.Size = new Size(183, 55);
            panel2.BackColor = Color.White;
            panel2.Dock = DockStyle.Fill;


            var label = new Label();
            label.Text = name;
            label.Font = new Font("Segoe UI", 12F, FontStyle.Regular,
                        GraphicsUnit.Point, ((byte)(204)));
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(label);

            var decreaseButton = new Button();
            decreaseButton.Size = new Size(20, 23);
            //decreaseButton.Text = "1";
            decreaseButton.Image = new Bitmap("images/arrow-left.png");
            decreaseButton.Top = 20;
            decreaseButton.Left = 35;
            panel2.Controls.Add(decreaseButton);

            var increaseButton = new Button();
            increaseButton.Size = new Size(20, 23);
            //increaseButton.Text = "2";
            increaseButton.Image = new Bitmap("images/arrow-right.png");
            increaseButton.Top = 20;
            increaseButton.Left = 117;
            panel2.Controls.Add(increaseButton);

            var valueLabel = new Label();
            valueLabel.Text = Value.ToString();
            valueLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(204)));
            valueLabel.Dock = DockStyle.Fill;
            valueLabel.TextAlign = ContentAlignment.MiddleCenter;
            valueLabel.Paint += (s,e) =>
            {
                valueLabel.Text = Value.ToString();
            };
            panel2.Controls.Add(valueLabel);

            decreaseButton.Click += (s, e) =>
            {
                Value -= 5;
                Value = Math.Max(0, Value);
                valueLabel.Invalidate();
            };

            increaseButton.Click += (s, e) =>
            {
                Value += 5;
                Value = Math.Min(100, Value);
                valueLabel.Invalidate();
            };

            t.Controls.Add(panel,0,0);
            t.Controls.Add(panel2,1,0);
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
