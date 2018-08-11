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

namespace ld42 {
	class Game {
		public GameMap gameMap;
		public Snake snake;
		public List<Bullet> bullets;

		public Game() {
			bullets = new List<Bullet>();
			gameMap = new GameMap();
			snake = new Snake();
			Settings.game = this;
		}

		public void StartGame() {
			Init();

			Timer t = new Timer() {
				AutoReset = true,
				Enabled = false,
				Interval = Settings.tickInterval,
			};

			t.Elapsed += (a, b) => {
				++Settings.tick;
				Loop();
			};

			t.Start();
		}

		public void Init() {
			System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
				bullets.Clear();
				snake = new Snake();

				gameMap.GenerateNewMap();
				snake.AddHero(new Ghost());
				snake.AddHero(new Ghost());
				snake.AddHero(new Ghost());
				snake.AddHero(new Ghost());
				snake.AddHero(new Ghost());
			});
		}

		public void Loop() {
			System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
			//	try {
					Update();
					Render();
			//	}
			//	catch (Exception ex) {
					//MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
			//	}
			});
		}

		public void Update() {
			snake.ProcessShake();
			this.ProcessBullets();
		}

		public void Render() {
			gameMap.ProcessCamMove();
		}

		void ProcessBullets() {
			foreach (var b in bullets) {
				b.pos.X += b.speed * ((-90 < b.angle && b.angle < 90) ? 1 : -1) /
					(Math.Abs(b.angle) > 90 ? 180 - Math.Abs(b.angle) : Math.Abs(b.angle));
				b.pos.Y += b.speed * ((b.angle > 0) ? 1 : -1) / 
					Math.Abs(90 / (b.angle >90 ? (180 - b.angle) : b.angle));

				//if (-45 < b.angle && b.angle < 45)
				//	b.pos.X += b.speed;
				//else if (-135 < b.angle && b.angle > 135)
				//	b.pos.X -= b.speed;
				//else if (-135 < b.angle && b.angle < 135)
				//	b.pos.Y += b.speed;
				//else if (-135 < b.angle && b.angle < 135)
				//	b.pos.Y -= b.speed;

				Canvas.SetLeft(b.image, b.pos.X);
				Canvas.SetTop (b.image, b.pos.Y);
			}
			
		}

	}
}
