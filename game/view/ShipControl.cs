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
	public partial class ShipControl : UserControl
	{
		public Ship Ship;
		public List<RoomControl> Rooms;
		public ShipControl(Ship ship)
		{
			InitializeComponent();
			Ship = ship;
			Rooms = Ship.Rooms.Select(r => new RoomControl(r)).ToList();
			if (Ship is Titan || Ship is TestTitan)
			{
				BackgroundImage = new Bitmap("images/Titan.png");
				BackgroundImageLayout = ImageLayout.Stretch;
			}
			BackColor = Color.Transparent;
		}
	}
}
