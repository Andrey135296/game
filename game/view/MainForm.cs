using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			var mainMenuGrid = GenerateMainMenu();
			Controls.Add(mainMenuGrid);
		}

		public static TableLayoutPanel GenerateMainMenu()
		{
			var mainMenuGrid = new TableLayoutPanel();
			mainMenuGrid.ColumnCount = 3;
			mainMenuGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
			mainMenuGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
			mainMenuGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
			mainMenuGrid.RowCount = 5;
			mainMenuGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
			mainMenuGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
			mainMenuGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
			mainMenuGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
			mainMenuGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
			mainMenuGrid.Dock = DockStyle.Fill;

			var playGrid = new TableLayoutPanel();
			playGrid.Dock = DockStyle.Fill;
			playGrid.ColumnCount = 1;
			playGrid.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			playGrid.RowCount = 2;
			playGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
			playGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
			mainMenuGrid.Controls.Add(playGrid, 1, 1);

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
			mainMenuGrid.Controls.Add(otherGrid, 1, 3);

			var optionsButton = new Button();
			optionsButton.Text = "Настройки";
			optionsButton.Dock = DockStyle.Fill;
			otherGrid.Controls.Add(optionsButton, 0, 0);

			var exitButton = new Button();
			exitButton.Text = "Выход";
			exitButton.Dock = DockStyle.Fill;
			otherGrid.Controls.Add(exitButton, 0, 1);

			return mainMenuGrid;
		}
	}
}
