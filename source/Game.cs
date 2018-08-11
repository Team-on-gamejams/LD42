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
		Snake player;

		public Game() {
			gameMap = new GameMap();
			player = new Snake();
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
			gameMap.GenerateNewMap();
		}

		public void Loop() {
			Update();
			Render();
		}

		public void Update() {

		}

		public void Render() {

		}



	}
}
