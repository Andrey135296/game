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

		public MainForm()
		{
			InitializeComponent();

			MainMenuGrid = GenerateMainMenu();
			Controls.Add(MainMenuGrid);
			AllGrids.Add(MainMenuGrid);

			OptionsGrid = GenerateOptionsMenu();
			Controls.Add(OptionsGrid);
			AllGrids.Add(OptionsGrid);
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

			return optionsScreen;
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
