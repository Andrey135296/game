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
		public TableLayoutPanel mainMenuGrid;
		public TableLayoutPanel optionsGrid;
		public List<TableLayoutPanel> allGrids = new List<TableLayoutPanel>();

		public MainForm()
		{
			InitializeComponent();

			mainMenuGrid = GenerateMainMenu();
			Controls.Add(mainMenuGrid);
			allGrids.Add(mainMenuGrid);

			optionsGrid = GenerateOptionsMenu();
			Controls.Add(optionsGrid);
			allGrids.Add(optionsGrid);
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
            optionsScreen.ColumnCount = 3;
            optionsScreen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            optionsScreen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
            optionsScreen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            optionsScreen.RowCount = 2;
            optionsScreen.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            optionsScreen.RowStyles.Add(new RowStyle(SizeType.Percent, 80));
            //optionsScreen.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
            //optionsScreen.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            //optionsScreen.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            optionsScreen.Dock = DockStyle.Fill;
            optionsScreen.BackgroundImage = new Bitmap("images/MenuBackground.png");
            optionsScreen.BackgroundImageLayout = ImageLayout.Stretch;

            var backButton = new Button();
            backButton.Text = "Назад";
            backButton.Dock = DockStyle.Fill;
            optionsScreen.Controls.Add(backButton, 2, 0);

            return optionsScreen;
		}

		public void TransitionTo(Screen screen)
		{
			foreach (var p in allGrids)
				p.Visible = false;
			switch (screen)
			{
				case Screen.Menu:
					mainMenuGrid.Visible = true;
					break;
				case Screen.Options:
					optionsGrid.Visible = true;
					break;
				default:
					throw new Exception("Unknown screen type");
			}
		}
	}
}
