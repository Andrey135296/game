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
    public partial class MapControl : UserControl
    {

        public List<MapPoint> MapNodes = new List<MapPoint>();

        public MapControl(GameModel gameModel)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            var map = gameModel.Map;
            foreach (var i in map.Nodes)
            {
                MapPoint point = new MapPoint(i, map);
                this.Controls.Add(point);
                point.Left = (i.Coordinates.X) * this.Width / 100;
                point.Top = (i.Coordinates.Y) * this.Height / 100;
                MapNodes.Add(point);
                //point.Click += (s, e) =>
                //{
                //    if (point.PointNode == map.CurrentNode)
                //        return;
                //    if (map.CurrentNode.Neighbors.Contains(point.PointNode)) 
                //        return;



                //};
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            foreach (var node in MapNodes)
            {
                node.Left = (node.PointNode.Coordinates.X) * this.Width / 100;
                node.Top = (node.PointNode.Coordinates.Y) * this.Height / 100;
            }
            foreach (var node in MapNodes)
            {
                foreach (var neib in MapNodes)
                {
                    if (node.PointNode.Neighbors.Contains(neib.PointNode))
                        e.Graphics.DrawLine(new Pen(Color.FromArgb(255, 108, 246, 255)),
                            node.Left + (node.PointSize / 2), node.Top + (node.PointSize / 2),
                            neib.Left + (neib.PointSize / 2), neib.Top + (neib.PointSize / 2));
                }
            }
        }
    }
}
