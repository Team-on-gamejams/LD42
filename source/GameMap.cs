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
		Coord camPos;
		GameCell[,] map;

		public GameMap() {
			map = new GameCell[Settings.fieldSize.X, Settings.fieldSize.Y];
			for (byte x = 0; x < map.GetLength(0); ++x)
				for (byte y = 0; y < map.GetLength(1); ++y)
					map[x, y] = new GameCell();
			camPos = new Coord(Settings.camStartPos);
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
		public Coord CamPos => camPos;

		public void TryMoveCamLeft() {
			--CamPos.X;
		}

		public void TryMoveCamUp() {
			--CamPos.Y;
		}

		public void TryMoveCamRight() {
			++CamPos.X;
		}

		public void TryMoveCamDown() {
			++CamPos.Y;
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
