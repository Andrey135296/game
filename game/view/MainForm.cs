﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using game.view;
using System.Media;

namespace game
{
	public partial class MainForm : Form 
	{
		public TableLayoutPanel MainMenuGrid;
		public TableLayoutPanel OptionsGrid;
		public TableLayoutPanel StartGrid;
		public TableLayoutPanel MapGrid;
		public TableLayoutPanel FightGrid;
		public List<TableLayoutPanel> AllGrids = new List<TableLayoutPanel>();
		public GameModel gameModel = null;
		public ISelectable Selected = null;

		public MainForm()
		{

			InitializeComponent();
			gameModel = new GameModel(new Titan(Alignment.Player), Map.LoadFromFile("maps/map1.txt"));
			//
			gameModel.OtherShip = new Titan(Alignment.Enemy);
			//

			DoubleBuffered = true;
			MainMenuGrid = GenerateMainMenu();
			Controls.Add(MainMenuGrid);
			AllGrids.Add(MainMenuGrid);

			OptionsGrid = GenerateOptionsMenu();
			Controls.Add(OptionsGrid);
			AllGrids.Add(OptionsGrid);
			Size = new Size(1280, 720);
			//client size: 1264, 681

			StartGrid = GenerateStartScreen();
			Controls.Add(StartGrid);
			AllGrids.Add(StartGrid);

            MapGrid = GenerateMapScreen();
            Controls.Add(MapGrid);
            AllGrids.Add(MapGrid);

			FightGrid = GenerateFightScreen();
			Controls.Add(FightGrid);
			AllGrids.Add(FightGrid);

            SoundPlayer sp = new SoundPlayer("music/testsound.wav");
			//sp.Play();

			foreach (var control in GetAll(this, typeof(Human)))
				control.Click += (s, e) =>
				{
					var selectable = (ISelectable)control;
					DropSelection();
					selectable.IsSelected = true;
					Selected = selectable;
					selectable.Invalidate();
				};
			foreach (var control in GetAll(this, typeof(WeaponControl)))
				control.Click += (s, e) =>
				{
					var selectable = (ISelectable)control;
					DropSelection();
					selectable.IsSelected = true;
					Selected = selectable;
					selectable.Invalidate();
				};
		}

		public TableLayoutPanel GenerateMainMenu()
		{
			var mainGrid = new TableLayoutPanel();
			mainGrid.ColumnCount = 3;
			mainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
			mainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
			mainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
			mainGrid.RowCount = 5;
			mainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
			mainGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
			mainGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
			mainGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
			mainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
			mainGrid.Dock = DockStyle.Fill;
			mainGrid.BackgroundImage = new Bitmap("images/MenuBackground.png");
			mainGrid.BackgroundImageLayout = ImageLayout.Stretch;

			var logo = new Label();
			logo.Text = "DTSb";
			logo.Dock = DockStyle.Fill;
			logo.BackColor = Color.Transparent;
			logo.Font = new Font(FontFamily.GenericSerif, 20, FontStyle.Bold);
			mainGrid.Controls.Add(logo, 0, 0);

			var playGrid = new TableLayoutPanel();
			playGrid.Dock = DockStyle.Fill;
			playGrid.ColumnCount = 1;
			playGrid.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			playGrid.RowCount = 2;
			playGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
			playGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
			mainGrid.Controls.Add(playGrid, 1, 1);

			var continueButton = new Button();
			continueButton.Text = "Продолжить";
            continueButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular,
                                    GraphicsUnit.Point, ((byte)(204)));
            continueButton.Dock = DockStyle.Fill;
			//continueButton.Enabled = false;
			continueButton.Click += (e, a) => TransitionTo(Screen.Fight);
			//
			playGrid.Controls.Add(continueButton, 0, 0);

			var newGameButton = new Button();
			newGameButton.Text = "Новая Игра";
            newGameButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular,
                        GraphicsUnit.Point, ((byte)(204)));
            newGameButton.Dock = DockStyle.Fill;
			newGameButton.Click += (e, a) => TransitionTo(Screen.Start);
			playGrid.Controls.Add(newGameButton, 0, 1);


			var otherGrid = new TableLayoutPanel();
			otherGrid.Dock = DockStyle.Fill;
			otherGrid.ColumnCount = 1;
			otherGrid.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			otherGrid.RowCount = 2;
			otherGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
			otherGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
			mainGrid.Controls.Add(otherGrid, 1, 3);

			var optionsButton = new Button();
			optionsButton.Text = "Настройки";
            optionsButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular,
                        GraphicsUnit.Point, ((byte)(204)));
            optionsButton.Dock = DockStyle.Fill;
			optionsButton.Click += (e, a) => this.TransitionTo(Screen.Options);
			otherGrid.Controls.Add(optionsButton, 0, 0);

			var exitButton = new Button();
			exitButton.Text = "Выход";
            exitButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular,
                        GraphicsUnit.Point, ((byte)(204)));
            exitButton.Dock = DockStyle.Fill;
            exitButton.Click += (s, e) =>
            {
                var result = MessageBox.Show("Вы действительно хотите выйти?", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) Application.Exit();
            };
            otherGrid.Controls.Add(exitButton, 0, 1);

			return mainGrid;
		}

		public TableLayoutPanel GenerateOptionsMenu()
		{
			var optionsScreen = new TableLayoutPanel();
            optionsScreen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 89));
            optionsScreen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 367));
            optionsScreen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 367));
            optionsScreen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 367));
            optionsScreen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90));
            optionsScreen.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
            optionsScreen.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            optionsScreen.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            optionsScreen.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
            optionsScreen.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
            optionsScreen.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
            optionsScreen.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
            optionsScreen.RowStyles.Add(new RowStyle(SizeType.Absolute, 244));

            //optionsScreen.RowStyles.Add(new RowStyle(SizeType.Percent, 3));
            //optionsScreen.RowStyles.Add(new RowStyle(SizeType.Percent, 9));
            //optionsScreen.RowStyles.Add(new RowStyle(SizeType.Percent, 18));
            //optionsScreen.RowStyles.Add(new RowStyle(SizeType.Percent, 9));
            //optionsScreen.RowStyles.Add(new RowStyle(SizeType.Percent, 9));
            //optionsScreen.RowStyles.Add(new RowStyle(SizeType.Percent, 9));
            //optionsScreen.RowStyles.Add(new RowStyle(SizeType.Percent, 9));
            //optionsScreen.RowStyles.Add(new RowStyle(SizeType.Percent, 34));

            //optionsScreen.AutoSize = true;
            optionsScreen.Dock = DockStyle.Fill;
            optionsScreen.BackgroundImage = new Bitmap("images/MenuBackground.png");
            optionsScreen.BackgroundImageLayout = ImageLayout.Stretch;

            var backButton = new Button();
            backButton.Text = "Назад";
            backButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular,
                            GraphicsUnit.Point, ((byte)(204)));
            backButton.Dock = DockStyle.Fill;
            backButton.Click += (e, a) => this.TransitionTo(Screen.Menu);
            optionsScreen.Controls.Add(backButton, 4, 1);

            var logo = new Label();
            logo.Text = "DTSb";
            logo.Dock = DockStyle.Fill;
            logo.BackColor = Color.Transparent;
            logo.Font = new Font(FontFamily.GenericSerif, 20, FontStyle.Bold);
            optionsScreen.Controls.Add(logo, 0, 0);

            var optionsLabel = new Label();
            optionsLabel.Text = "Настройки";
            optionsLabel.Dock = DockStyle.Fill;
            optionsLabel.TextAlign = ContentAlignment.MiddleCenter;
            optionsLabel.BackColor = Color.Transparent;
            optionsLabel.Font = new Font(FontFamily.GenericSerif, 40, FontStyle.Bold);
            optionsScreen.Controls.Add(optionsLabel, 2, 1);

            var soundOpt = new OptionNameCell("Звук");
            optionsScreen.Controls.Add(soundOpt, 1, 3);

            var videoOpt = new OptionNameCell("Видео");
            optionsScreen.Controls.Add(videoOpt, 2, 3);

            var gameOpt = new OptionNameCell("Игровые настройки");
            optionsScreen.Controls.Add(gameOpt, 3, 3);

            var musicOpt = new OptionCell("Музыка", 50);
            optionsScreen.Controls.Add(musicOpt, 1, 4);

            var effectOpt = new OptionCell("Эффекты", 50);
            optionsScreen.Controls.Add(effectOpt, 1, 5);

            var allSoundsOpt = new OptionCell("Общая громкость", 50);
            optionsScreen.Controls.Add(allSoundsOpt, 1, 6);

            var fullSceen = new OptionCell("Яркость", 50);
            optionsScreen.Controls.Add(fullSceen, 2, 4);

            var autoPause = new OptionCell("Автопауза", 50);
            optionsScreen.Controls.Add(autoPause, 3, 4);

            var buffer = new Label();
            buffer.BackColor = Color.Transparent;
            optionsScreen.Controls.Add(buffer, 1, 7);

            return optionsScreen;
		}

		public TableLayoutPanel GenerateStartScreen()
		{
			var startScreen = new Panel();
			startScreen.Dock = DockStyle.Fill;
			startScreen.BackgroundImage = new Bitmap("images/StartBackground.jpg");
			startScreen.BackgroundImageLayout = ImageLayout.Stretch;
			startScreen.Margin = new Padding(0, 0, 0, 0);
			startScreen.Click += (s, e) => DropSelection();

			var backButton = new Button();
			backButton.Text = "Назад";
            backButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular,
                            GraphicsUnit.Point, ((byte)(204)));
			backButton.Left = 1059;
			backButton.Height = 50;
			backButton.Width = 200;
			backButton.Top = 5;
			backButton.Click += (e, a) => this.TransitionTo(Screen.Menu);
			startScreen.Controls.Add(backButton);

			var mapButton = new Button();
			mapButton.Text = "Старт";
			mapButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular,
							GraphicsUnit.Point, ((byte)(204)));
			mapButton.Left = 1059;
			mapButton.Top = 60;
			mapButton.Height = 50;
			mapButton.Width = 200;
			mapButton.Click += (e, a) => this.TransitionTo(Screen.Map);
			startScreen.Controls.Add(mapButton);

			var crewPanel = new CrewPanel(gameModel.PlayerShip.Crew);
			crewPanel.Top = 5;
			crewPanel.Left = 5;
			startScreen.Controls.Add(crewPanel);

			var systemPanel = new SystemsPanel(gameModel.PlayerShip);
			systemPanel.Left = 230;
			systemPanel.Top = 5;
			startScreen.Controls.Add(systemPanel);

			var shipControl = new ShipControl(gameModel.PlayerShip, false);
			shipControl.Left = 150;
			shipControl.Top = 350;
			shipControl.Size = new Size(750, 300);
			startScreen.Controls.Add(shipControl);

			var weaponPanel = new WeaponPanel(gameModel.PlayerShip);
			weaponPanel.Top = 5;
			weaponPanel.Left = 544;
			startScreen.Controls.Add(weaponPanel);

			var resourcePanel = new ResourcePanel(gameModel);
			resourcePanel.Left = 781;
			resourcePanel.Top = 5;
			resourcePanel.Size = new Size(150, 100);
			startScreen.Controls.Add(resourcePanel);

			var ans = new TableLayoutPanel();
			ans.RowCount = 1;
			ans.ColumnCount = 1;
			ans.Dock = DockStyle.Fill;
			ans.Controls.Add(startScreen);
			ans.Margin = new Padding(0, 0, 0, 0);
			return ans;
		}

        public TableLayoutPanel GenerateMapScreen()
        {
            var mainMapGrid = new TableLayoutPanel();
            mainMapGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 87));
            mainMapGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13));
            mainMapGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14));
            mainMapGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 86));
            mainMapGrid.Dock = DockStyle.Fill;
            mainMapGrid.BackgroundImage = new Bitmap("images/MapBackground.png");
            mainMapGrid.BackgroundImageLayout = ImageLayout.Stretch;

            //var logo = new Label();
            //logo.Text = "DTSb";
            //logo.Dock = DockStyle.Fill;
            //logo.BackColor = Color.Transparent;
            //logo.Font = new Font(FontFamily.GenericSerif, 20, FontStyle.Bold);
            //mainMapGrid.Controls.Add(logo, 0, 0);

            var backButton = new Button();
            backButton.Text = "Назад";
            backButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular,
                            GraphicsUnit.Point, ((byte)(204)));
            backButton.Dock = DockStyle.Fill;
            backButton.Click += (e, a) => this.TransitionTo(Screen.Menu);
            mainMapGrid.Controls.Add(backButton, 1, 0);

            var playGrid = new TableLayoutPanel();
            playGrid.Dock = DockStyle.Fill;
            playGrid.ColumnCount = 1;
            playGrid.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            playGrid.RowCount = 2;
            playGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            playGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            playGrid.BackColor = Color.Transparent;
            mainMapGrid.Controls.Add(playGrid, 1, 1);

            var resorsePanel = new ResourcePanel(gameModel);
            playGrid.Controls.Add(resorsePanel, 0, 0);
            resorsePanel.BackColor = Color.White;

            //var continueButton = new Button();
            //continueButton.Text = "Продолжить";
            //continueButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular,
            //                        GraphicsUnit.Point, ((byte)(204)));
            //continueButton.Dock = DockStyle.Fill;
            //continueButton.Enabled = false;
            //playGrid.Controls.Add(continueButton, 0, 0);

            //var newGameButton = new Button();
            //newGameButton.Text = "Новая Игра";
            //newGameButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular,
            //            GraphicsUnit.Point, ((byte)(204)));
            //newGameButton.Dock = DockStyle.Fill;
            //newGameButton.Click += (e, a) => TransitionTo(Screen.Start);
            //playGrid.Controls.Add(newGameButton, 0, 1);


            var otherGrid = new TableLayoutPanel();
            otherGrid.Dock = DockStyle.Fill;
            otherGrid.ColumnCount = 1;
            playGrid.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            otherGrid.RowCount = 2;
            otherGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40));
            otherGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            otherGrid.BackColor = Color.Transparent;
            mainMapGrid.Controls.Add(otherGrid, 0, 1);

            var label = new Label();
            label.Text = "Карта уровня";
            label.Dock = DockStyle.Fill;
            label.Height = 35;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.BackColor = Color.Transparent;
            label.ForeColor = Color.White;
            //label.Font = new Font(FontFamily.GenericSerif, 20, FontStyle.Bold);
            label.Font = new Font("Segoe UI", 20, FontStyle.Bold,
                                    GraphicsUnit.Point, ((byte)(204)));
            otherGrid.Controls.Add(label, 0, 0);

            var optionsButton = new Button();
            optionsButton.Text = "Настройки";
            optionsButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular,
                        GraphicsUnit.Point, ((byte)(204)));
            optionsButton.Dock = DockStyle.Fill;
            optionsButton.Click += (e, a) => this.TransitionTo(Screen.Options);
			//otherGrid.Controls.Add(optionsButton, 0, 0);

			var fight = new Button();
			fight.Click += (e, a) => this.TransitionTo(Screen.Fight);
			mainMapGrid.Controls.Add(fight);

            return mainMapGrid;
        }

		public TableLayoutPanel GenerateFightScreen()
		{
			var t = new TableLayoutPanel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 0) };
			var screen = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 0) };
			screen.BackgroundImage = new Bitmap("images/BattleBackground.jpg");
			screen.Click += (s, e) => DropSelection();
			t.Controls.Add(screen);

			var weaponPanel = new WeaponPanel(gameModel.PlayerShip) { Left = 0, Top = 510 };
			screen.Controls.Add(weaponPanel);

			var systemsPanel = new SystemsPanel(gameModel.PlayerShip) { Left = 149, Top = 510};
			screen.Controls.Add(systemsPanel);

			var crewPanel = new CrewPanel(gameModel.PlayerShip.Crew) { Left = 457, Top = 510};
			screen.Controls.Add(crewPanel);

			var playerShip = new ShipControl(gameModel.PlayerShip){ Width = 540, Height = 216, Top = 200, Left = 30};
			screen.Controls.Add(playerShip);

			gameModel.PlayerShip.Stats.CurrentHP = 1500;

			var playerHpBar = new HPBar(gameModel.PlayerShip) { Width = 630, Height = 30};
			screen.Controls.Add(playerHpBar);
			

			if (gameModel.OtherShip != null)
			{
				var otherShip = new ShipControl(gameModel.OtherShip, true) { Width = 540, Height = 216,
					Top = 200, Left = 694 };
				screen.Controls.Add(otherShip);
			}


			return t;
		}

        public void TransitionTo(Screen screen)
		{
			foreach (var p in AllGrids)
				p.Visible = false;
			DropSelection();
			switch (screen)
			{
				case Screen.Menu:
					MainMenuGrid.Visible = true;
					break;
				case Screen.Options:
					OptionsGrid.Visible = true;
					break;
				case Screen.Start:
					StartGrid.Visible = true;
					break;
				case Screen.Map:
					MapGrid.Visible = true;
					break;
				case Screen.Fight:
					FightGrid.Visible = true;
					break;
				default:
					throw new Exception("Unknown screen type");
			}
		}

		public static IEnumerable<Control> GetAll(Control control, Type type)
		{
			var controls = control.Controls.Cast<Control>();

			return controls.SelectMany(ctrl => GetAll(ctrl, type))
									  .Concat(controls)
									  .Where(c => c.GetType() == type);
		}

		private void DropSelection()
		{
			if (Selected != null)
			{
				Selected.IsSelected = false;
				Selected.Invalidate();
			}
			Selected = null;
		}
	}
}
