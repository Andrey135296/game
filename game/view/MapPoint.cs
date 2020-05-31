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
    public partial class MapPoint : UserControl
    {
        public Node PointNode;
		public Map Map;
        public int PointSize = 30;

        public MapPoint(Node node, Map map)
        {
            InitializeComponent();
            //button.Size = new Size(new Point(10,10));
            PointNode = node;
			this.Map = map;
            //this.Controls.Add(button);
            //this.Width = 30;
            //this.Height = 30;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            Color fillColor;
            var aligmenit = PointNode.Alignment;
            var greenColor = Color.FromArgb(255, 0, 202, 44);
            fillColor = aligmenit == Alignment.Player ? greenColor : Color.Red;
            int Size = (aligmenit == Alignment.Down || aligmenit == Alignment.Up) ? PointSize + 20 : PointSize;
            e.Graphics.FillEllipse(new SolidBrush(fillColor), 0, 0, Size, Size);
            e.Graphics.DrawEllipse(new Pen(Color.Black, 2), 0, 0, Size, Size);
			if (this.Map.CurrentNode == PointNode)
			{
				e.Graphics.DrawLine(new Pen(Color.Orange, 4), 0, 0, 30, 30);
				e.Graphics.DrawLine(new Pen(Color.Orange, 4), 30, 0, 0, 30);
			}
        }

        
    }
}
