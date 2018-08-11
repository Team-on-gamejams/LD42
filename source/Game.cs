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

		public Game() {
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
		}

		public void Render() {
			gameMap.ProcessCamMove();
		}



	}
}
