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
        //public GameModel gameModel;
        public MapControl(GameModel gameModel)
        {
            InitializeComponent();
            var mapPanel = new TableLayoutPanel();
            var rowColomnNumber = 100;
            for (var i = 0; i < rowColomnNumber; i++)
            {
                mapPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100/rowColomnNumber));
                mapPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100/rowColomnNumber));
            }

            foreach (var i in gameModel.Map.Nodes)
            {
                MapPoint point = new MapPoint(i);
                mapPanel.Controls.Add(point,i.Coordinates.X, i.Coordinates.Y);
            }

        }
    }
}
