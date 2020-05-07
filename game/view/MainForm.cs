using System;
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

		public MainForm()
		{
            DoubleBuffered = true;
			InitializeComponent();

			MainMenuGrid = GenerateMainMenu();
			Controls.Add(MainMenuGrid);
			AllGrids.Add(MainMenuGrid);

			OptionsGrid = GenerateOptionsMenu();
			Controls.Add(OptionsGrid);
			AllGrids.Add(OptionsGrid);
			Size = new Size(1280, 720);

			StartGrid = GenerateStartScreen();
			Controls.Add(StartGrid);
			AllGrids.Add(StartGrid);

        }

		protected override void OnPaint(PaintEventArgs e)
		{
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
			continueButton.Dock = DockStyle.Fill;
			continueButton.Enabled = false;
			playGrid.Controls.Add(continueButton, 0, 0);

			var newGameButton = new Button();
			newGameButton.Text = "Новая Игра";
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
			optionsButton.Dock = DockStyle.Fill;
			optionsButton.Click += (e, a) => this.TransitionTo(Screen.Options);
			otherGrid.Controls.Add(optionsButton, 0, 0);

			var exitButton = new Button();
			exitButton.Text = "Выход";
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
            //TODO - generate options
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
            backButton.Dock = DockStyle.Fill;
            backButton.Click += (e, a) => this.TransitionTo(Screen.Menu);
            optionsScreen.Controls.Add(backButton, 4, 1);

            var logo = new Label();
            logo.Text = "DTSb";
            logo.Dock = DockStyle.Fill;
            logo.BackColor = Color.Transparent;
            logo.Font = new Font(FontFamily.GenericSerif, 20, FontStyle.Bold);
            optionsScreen.Controls.Add(logo, 0, 1);

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

            var gameOpt1 = new OptionCell();
            optionsScreen.Controls.Add(gameOpt1, 1, 4);

            var gameOpt2 = new OptionNameCell("Игровые настройки");
            optionsScreen.Controls.Add(gameOpt2, 3, 4);

            return optionsScreen;
		}

		public TableLayoutPanel GenerateStartScreen()
		{
			var startScreen = new Panel();
			startScreen.Dock = DockStyle.Fill;
			startScreen.BackgroundImage = new Bitmap("images/StartBackground.jpg");
			startScreen.BackgroundImageLayout = ImageLayout.Stretch;

			var backButton = new Button();
			backButton.Text = "Назад";
			backButton.Dock = DockStyle.Right;
			backButton.Click += (e, a) => this.TransitionTo(Screen.Menu);
			startScreen.Controls.Add(backButton);

			var l = new List<CrewMember>();
			for (int i = 0; i < 7; i++)
			{
				l.Add(new CrewMember(null, Alignment.Player));
			}
			var crewPanel = new CrewPanel(l);
			crewPanel.Top = 5;
			crewPanel.Left = 5;
			startScreen.Controls.Add(crewPanel);

			var ship = new TestTitan(Alignment.Player);
			var systemPanel = new SystemsPanel(ship);
			systemPanel.Left = 312;
			systemPanel.Top = 5;
			startScreen.Controls.Add(systemPanel);

			var ans = new TableLayoutPanel();
			ans.RowCount = 1;
			ans.ColumnCount = 1;
			ans.Dock = DockStyle.Fill;
			ans.Controls.Add(startScreen);
			return ans;
		}

		public void TransitionTo(Screen screen)
		{
			foreach (var p in AllGrids)
				p.Visible = false;
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
	}
}
