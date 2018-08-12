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
using System.Timers;

using WpfAnimatedGif;

namespace ld42 {
	enum PlayerState : byte { Move, Jump, Slash, Roll }

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class GameWindow : Window {
		Random random = new Random(((int)(DateTime.Now.Ticks % int.MaxValue)));

		System.Timers.Timer timer;
		byte speedArrIndex = 0;
		byte[] speedArr = new byte[] { /*1, 2,*/ 5, 10, 25, 50 };
		ulong tick = 0;
		Image backgroundImage1;
		Image backgroundImage2;
		Image playerImage;

		ulong score;

		int stateRemaing;
		PlayerState playerState = PlayerState.Move;

		List<BasicObstacle> obstacles = new List<BasicObstacle>(20);

		public GameWindow() {
			//try {
			InitializeComponent();
			WindowManager.AddWindow(this);

			backgroundImage1 = new Image() { Source = new BitmapImage(new Uri(@"Resources\img\road.png", UriKind.Relative)) };
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

			obstacles.Add(null);
			obstacles.Add(null);
			obstacles.Add(null);
			obstacles.Add(null);
			obstacles.Add(null);
			obstacles.Add(null);
			obstacles.Add(null);
			obstacles.Add(null);
			for(int i = 0; i < 12; ++i)
				AddRandomObstacle();

			//}
			//catch (Exception ex) {
			//	MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
			//}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			System.Timers.Timer t = new System.Timers.Timer() {
				AutoReset = true,
				Interval = 10,
				Enabled = false,
			};

			t.Elapsed += (a, b) => {
				System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
					//try {
					++tick;
					if (tick % 10 == 0) {
						++score;
						if (tick % 5000 == 0 && speedArrIndex < speedArr.Length - 1)
							++speedArrIndex;
					}

					ProcessObstacles();
					ProcessPlayerState();

					ScrollBackground();
					DrawObstacles();
					scoreText.Text = score.ToString();
					//}
					//catch(Exception ex) {
					//	MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
					//}
				});
			};
			timer = t;
			t.Start();
		}

		void ScrollBackground() {
			Canvas.SetLeft(backgroundImage1, Canvas.GetLeft(backgroundImage1) - speedArr[speedArrIndex]);
			if (Canvas.GetLeft(backgroundImage1) <= -1000)
				Canvas.SetLeft(backgroundImage1, Canvas.GetLeft(backgroundImage1) + 2000);
			Canvas.SetLeft(backgroundImage2, Canvas.GetLeft(backgroundImage2) - speedArr[speedArrIndex]);
			if (Canvas.GetLeft(backgroundImage2) <= -1000)
				Canvas.SetLeft(backgroundImage2, Canvas.GetLeft(backgroundImage2) + 2000);
		}

		void DrawObstacles() {
			for (byte i = 0; i < 20; ++i) {
				if (obstacles[i] != null) {
					Canvas.SetLeft(obstacles[i].image, i * 50 
						- (int)( (tick % (ulong)(50 / speedArr[speedArrIndex])) * speedArr[speedArrIndex])
					);
				}
			}
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

		void ProcessObstacles() {
			if (tick % ((ulong)(50 / speedArr[speedArrIndex])) == 0) {
				if (obstacles[0] != null) {
					score += (ulong)(speedArr[speedArrIndex]);
					obstacles[0].Destroy();
				}
				obstacles.RemoveAt(0);
				AddRandomObstacle();
			}

			if(obstacles[2] != null) {
				if (playerState != obstacles[2].stateToAvoid) {
					ImageBehavior.SetAnimatedSource(playerImage, new BitmapImage(new Uri(@"Resources\img\playerFall.gif", UriKind.Relative)));
					timer.Stop();
					Timer gameoverTimer = new Timer() {
						AutoReset = false,
						Interval = 500,
						Enabled = false,
					};
					gameoverTimer.Elapsed += (a, b) => {
						System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
							WindowManager.ReopenWindow(this, MenuWindow.gameOverWindow);
						});
					};
					gameoverTimer.Start();
				}
				else if(obstacles[2].stateToAvoid == PlayerState.Slash) {
					obstacles[2].Destroy();
					obstacles[2] = null;
				}
			}
			if (obstacles[3] != null) {
				if (obstacles[3].stateToAvoid == PlayerState.Slash && playerState == PlayerState.Slash) {
					obstacles[3].Destroy();
					obstacles[3] = null;
				}
			}

		}

		void AddRandomObstacle() {
			if (obstacles[obstacles.Count - 1] == null && obstacles[obstacles.Count - 2] == null) {
				byte rand = (byte)random.Next(0, 100);
				if (rand < 50)
					obstacles.Add(null);
				else if (rand < 66)
					obstacles.Add(new ObstacleJump(gameCanvas));
				else if (rand < 83)
					obstacles.Add(new ObstacleRoll(gameCanvas));
				else if (rand < 100)
					obstacles.Add(new ObstacleSlash(gameCanvas));
			}
			else
				obstacles.Add(null);
		}

		private void Window_KeyDown(object sender, KeyEventArgs e) {
			if (timer.Enabled) {
				if ((e.Key == Key.Space || e.Key == Key.D || e.Key == Key.Right) /*&& playerState != PlayerState.Slash*/) {
					ImageBehavior.SetAnimatedSource(playerImage, new BitmapImage(new Uri(@"Resources\img\playerSlash.gif", UriKind.Relative)));
					playerState = PlayerState.Slash;
					stateRemaing = 50 / speedArr[speedArrIndex];
				}
				else if ((e.Key == Key.W || e.Key == Key.Up) /*&& playerState != PlayerState.Jump*/) {
					ImageBehavior.SetAnimatedSource(playerImage, new BitmapImage(new Uri(@"Resources\img\playerJump.gif", UriKind.Relative)));
					playerState = PlayerState.Jump;
					stateRemaing = 150 / speedArr[speedArrIndex];
				}
				else if ((e.Key == Key.S || e.Key == Key.Down) /*&& playerState != PlayerState.Roll*/) {
					ImageBehavior.SetAnimatedSource(playerImage, new BitmapImage(new Uri(@"Resources\img\playerRoll.gif", UriKind.Relative)));
					playerState = PlayerState.Roll;
					stateRemaing = 150 / speedArr[speedArrIndex];
				}
			}
		}

		private void WindowClosed(object sender, EventArgs e) {
			WindowManager.CloseAll();
		}
	}
}
