using System;
using System.Collections;
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
	class GameMap : IEnumerable {
		double camdChange = 1.0 / Settings.tickInterval;
		double camdx, camdy;
		CoordReal camPos;
		GameCell[,] map;

		public GameMap() {
			map = new GameCell[Settings.fieldSize.X, Settings.fieldSize.Y];
			for (byte x = 0; x < map.GetLength(0); ++x)
				for (byte y = 0; y < map.GetLength(1); ++y)
					map[x, y] = new GameCell();
			camPos = new CoordReal(Settings.camStartPos);
		}

		public void GenerateNewMap() {
			for(byte x = 0; x < map.GetLength(0); ++x) {
				for (byte y = 0; y < map.GetLength(1); ++y) {
					if (x == 0 || y == 0 || x == map.GetLength(0) - 1 || y == map.GetLength(1) - 1) {
						map[x, y].isWall = true;
						map[x, y].image.LoadImg(@"wall\1");
					}
					else {
						map[x, y].image.LoadImg(@"floor\1");
					}
				}
			}


		}

		//------------------------------------------- CAMERA ------------------------------------------
		public CoordReal CamPos => camPos;

		public void TryMoveCamLeft() {
			--camdx;
		}

		public void TryMoveCamUp() {
			--camdy;
		}

		public void TryMoveCamRight() {
			++camdx;
		}

		public void TryMoveCamDown() {
			++camdy;
		}

		public void ProcessCamMove() {
			Settings.gameWindow.Title = $"{camdChange.ToString()} \n {camdx} \n {camdy} \n {camdx != 0} \n {camdy != 0}";
			if (camdx < 0) {
				CamPos.X -= camdChange;
				camdx += camdChange;
			}
			else if (camdx > 0) {
				CamPos.X += camdChange;
				camdx -= camdChange;
			}
			else if (camdy < 0) {
				CamPos.Y -= camdChange;
				camdy += camdChange;
			}
			else if (camdy > 0) {
				CamPos.Y += camdChange;
				camdy -= camdChange;
			}
		}

		//------------------------------------------- ULT ------------------------------------------

		public ushort SizeX => (ushort)map.GetLength(0);
		public ushort SizeY => (ushort)map.GetLength(1);

		public GameCell this[short x, short y] {
			get {
				if (x < 0 || y < 0 || x >= map.GetLength(0) || y >= map.GetLength(1))
					return null;
				return map[x, y];
			}
		}

		public IEnumerator GetEnumerator() {
			return map.GetEnumerator();
		}
	}
}
