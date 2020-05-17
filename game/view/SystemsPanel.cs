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
	public partial class SystemsPanel : UserControl
	{
		private Ship ship;
		public SystemsPanel(Ship ship)
		{
			InitializeComponent();
			Size = new Size(309, 171);
			this.ship = ship;
			var t = new TableLayoutPanel();
			t.ColumnCount = 4;
			t.RowCount = 5;
			for (int i = 0; i < 5; i++)
				t.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
			t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));
			t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));
			t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));
			t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));
			t.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

			var label = new Label();
			label.Text = "Системы";
			label.Font = new Font(FontFamily.GenericSerif, 15, FontStyle.Bold);
			label.Dock = DockStyle.Fill;
			label.TextAlign = ContentAlignment.MiddleCenter;
			t.Controls.Add(label, 0, 0);
			t.SetColumnSpan(label, 4);
			t.Dock = DockStyle.Fill;

			var generatorBar = new EnergyBar(ship.SpecialRooms.Where(r => r.Type == RoomType.Generator).First(), ship);
			SetUpControl(generatorBar);
			t.Controls.Add(generatorBar, 1, 1);
			t.SetColumnSpan(generatorBar, 3);

			var radarBar = new EnergyBar(ship.SpecialRooms.Where(r => r.Type == RoomType.Radar).First(), ship);
			SetUpControl(radarBar);
			t.Controls.Add(radarBar, 1, 2);
			

			var weaponBar = new EnergyBar(ship.SpecialRooms.Where(r => r.Type == RoomType.Weapon).First(), ship);
			SetUpControl(weaponBar);
			t.Controls.Add(weaponBar, 3, 2);

			var engineBar = new EnergyBar(ship.SpecialRooms.Where(r => r.Type == RoomType.Engine).First(), ship);
			SetUpControl(engineBar);
			t.Controls.Add(engineBar, 1, 3);

			var livingRoomBar = new EnergyBar(ship.SpecialRooms.Where(r => r.Type == RoomType.Living).First(), ship);
			SetUpControl(livingRoomBar);
			t.Controls.Add(livingRoomBar, 1, 4);

			var controlBar = new EnergyBar(ship.SpecialRooms.Where(r => r.Type == RoomType.Control).First(), ship);
			SetUpControl(controlBar);
			t.Controls.Add(controlBar, 3, 3);

			var generatorImage = new PictureBox();
			//SetUpControl(generatorImage);
			generatorImage.Dock = DockStyle.Fill;
			generatorImage.Image = new Bitmap("images/Generator.png");
			generatorImage.SizeMode = PictureBoxSizeMode.Zoom;
			t.Controls.Add(generatorImage, 0, 1);

			var radarImage = new PictureBox();
			//SetUpControl(radarImage);
			radarImage.Dock = DockStyle.Fill;
			radarImage.Image = new Bitmap("images/Radar.png");
			radarImage.SizeMode = PictureBoxSizeMode.Zoom;
			t.Controls.Add(radarImage, 0, 2);

			var engineImage = new PictureBox();
			//SetUpControl(engineImage);
			engineImage.Image = new Bitmap("images/Engine.png");
			engineImage.SizeMode = PictureBoxSizeMode.Zoom;
			engineImage.Dock = DockStyle.Fill;
			t.Controls.Add(engineImage, 0, 3);

			var livingImage = new PictureBox();
			//SetUpControl(livingImage);
			livingImage.Dock = DockStyle.Fill;
			livingImage.Image = new Bitmap("images/Living.png");
			livingImage.SizeMode = PictureBoxSizeMode.Zoom;
			t.Controls.Add(livingImage, 0, 4);

			var weaponImage = new PictureBox();
			//SetUpControl(weaponImage);
			weaponImage.Dock = DockStyle.Fill;
			weaponImage.Image = new Bitmap("images/Weapon.png");
			weaponImage.SizeMode = PictureBoxSizeMode.Zoom;
			t.Controls.Add(weaponImage, 2, 2);

			var controlImage = new PictureBox();
			//SetUpControl(controlImage);
			controlImage.Dock = DockStyle.Fill;
			controlImage.Image = new Bitmap("images/Control.png");
			controlImage.SizeMode = PictureBoxSizeMode.Zoom;
			t.Controls.Add(controlImage, 2, 3);

			//label.Click += (s, a) => this.Width++;


			this.Controls.Add(t);
		}

		private static void SetUpControl(Control c)
		{
			c.Dock = DockStyle.Fill;
			c.Padding = new Padding(0, 0, 0, 0);
			c.Margin = new Padding(0, 0, 0, 0);
		}
	}
}
