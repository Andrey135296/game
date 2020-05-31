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
        public MapPoint(Node node)
        {
            InitializeComponent();
            var button = new Button();
            //button.Size = new Size(new Point(10,10));
            PointNode = node;
        }
    }
}
