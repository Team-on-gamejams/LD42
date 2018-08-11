using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ld42 {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class GameWindow : Window {
		Game game = new Game();

		public GameWindow() {
			InitializeComponent();
			Settings.gameWindow = this;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			Settings.cellSize.X = GameCanvas.ActualWidth / Settings.camSize.X;
			Settings.cellSize.Y = GameCanvas.ActualHeight / Settings.camSize.Y;

			InitGameOutput();
			InitGameInput();

			game.StartGame();
		}

		void InitGameOutput() {
			for (byte x = 0; x < game.gameMap.SizeX; ++x) {
				for (byte y = 0; y < game.gameMap.SizeY; ++y) {
					game.gameMap[x, y].image.Width = Settings.cellSize.X;
					game.gameMap[x, y].image.Height = Settings.cellSize.Y;
					GameCanvas.Children.Add(game.gameMap[x, y].image);
					Canvas.SetLeft(game.gameMap[x, y].image, Settings.cellSize.X * (x - game.gameMap.CamPos.X));
					Canvas.SetTop(game.gameMap[x, y].image, Settings.cellSize.Y * (y - game.gameMap.CamPos.Y));
				}
			}

			game.gameMap.CamPos.SizeChanged += (xChange, yChange) => {
				if (xChange != 0) {
					for (byte x = 0; x < game.gameMap.SizeX; ++x)
						for (byte y = 0; y < game.gameMap.SizeY; ++y)
							Canvas.SetLeft(game.gameMap[x, y].image,
								Canvas.GetLeft(game.gameMap[x, y].image) - Settings.cellSize.X * xChange);
				}
				else if (yChange != 0) {
					for (byte x = 0; x < game.gameMap.SizeX; ++x)
						for (byte y = 0; y < game.gameMap.SizeY; ++y)
							Canvas.SetTop(game.gameMap[x, y].image,
								Canvas.GetTop(game.gameMap[x, y].image) - Settings.cellSize.Y * yChange);
				}
			};

		}

		void InitGameInput() {
			KeyDown += (a, b) => {
				if (b.Key == Key.A)
					game.gameMap.TryMoveCamLeft();
				else if (b.Key == Key.W)
					game.gameMap.TryMoveCamUp();
				else if (b.Key == Key.D)
					game.gameMap.TryMoveCamRight();
				else if (b.Key == Key.S)
					game.gameMap.TryMoveCamDown();
			};
		}
	}
}
