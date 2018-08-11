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

using WpfAnimatedGif;

namespace ld42 {
	enum PlayerState : byte { Move, Jump, Slash, Roll }

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		ulong tick = 0;
		Image backgroundImage1;
		Image backgroundImage2;
		Image playerImage;

		ulong stateRemaing;
		PlayerState playerState = PlayerState.Move;

		List<BasicObstacle> obstacles = new List<BasicObstacle>(20);

		public MainWindow() {
			try {
				InitializeComponent();

				backgroundImage1 = new Image() { Source=new BitmapImage(new Uri(@"Resources\img\road.png", UriKind.Relative))};
				gameCanvas.Children.Add(backgroundImage1);
				Canvas.SetLeft(backgroundImage1, 0);
				Canvas.SetZIndex(backgroundImage1, 0);
				backgroundImage2 = new Image() { Source = new BitmapImage(new Uri(@"Resources\img\road.png", UriKind.Relative)) };
				gameCanvas.Children.Add(backgroundImage2);
				Canvas.SetLeft(backgroundImage2, 1000);
				Canvas.SetZIndex(backgroundImage2, 0);


				playerImage = new Image();
				playerImage.Width = 150;
				playerImage.Height = 150;
				ImageBehavior.SetAnimatedSource(playerImage, new BitmapImage(new Uri(@"Resources\img\playerMove.gif", UriKind.Relative)));
				gameCanvas.Children.Add(playerImage);
				Canvas.SetLeft(playerImage, 0);
				Canvas.SetTop(playerImage, 50);
				Canvas.SetZIndex(playerImage, 5);
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			System.Timers.Timer t = new System.Timers.Timer() {
				AutoReset = true,
				Interval = 10,
				Enabled = false,
			};

			t.Elapsed += (a, b) => {
				System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
					try {
						++tick;
						ScrollBackground();
						DrawObstacles();
						ProcessPlayerState();
					}
					catch(Exception ex) {
						MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
					}
				});
			};

			t.Start();
		}

		void ScrollBackground() {
			Canvas.SetLeft(backgroundImage1, Canvas.GetLeft(backgroundImage1) - 1);
			if (Canvas.GetLeft(backgroundImage1) <= -1000)
				Canvas.SetLeft(backgroundImage1, 1000);
			Canvas.SetLeft(backgroundImage2, Canvas.GetLeft(backgroundImage2) - 1);
			if (Canvas.GetLeft(backgroundImage2) <= -1000)
				Canvas.SetLeft(backgroundImage2, 1000);
		}

		void DrawObstacles() {

		}

		void ProcessPlayerState() {
			if (playerState != PlayerState.Move) {
				if (stateRemaing == 0) {
					ImageBehavior.SetAnimatedSource(playerImage, new BitmapImage(new Uri(@"Resources\img\playerMove.gif", UriKind.Relative)));
					playerState = PlayerState.Move;
				}
				else
					--stateRemaing;
			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e) {
			if((e.Key == Key.Space || e.Key == Key.D || e.Key == Key.Right) && playerState != PlayerState.Slash) {
				ImageBehavior.SetAnimatedSource(playerImage, new BitmapImage(new Uri(@"Resources\img\playerSlash.gif", UriKind.Relative)));
				playerState = PlayerState.Slash;
				stateRemaing = 50;
			}
			else if ((e.Key == Key.W || e.Key == Key.Up) && playerState != PlayerState.Jump) {
				ImageBehavior.SetAnimatedSource(playerImage, new BitmapImage(new Uri(@"Resources\img\playerJump.gif", UriKind.Relative)));
				playerState = PlayerState.Jump;
				stateRemaing = 100;
			}
			else if ((e.Key == Key.S || e.Key == Key.Down) && playerState != PlayerState.Roll) {
				ImageBehavior.SetAnimatedSource(playerImage, new BitmapImage(new Uri(@"Resources\img\playerRoll.gif", UriKind.Relative)));
				playerState = PlayerState.Roll;
				stateRemaing = 100;
			}
		}
	}
}
