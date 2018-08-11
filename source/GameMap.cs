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

		public void MoveCamLeft() {
			if (-0.1 < camdx && camdx < 0.1)
				--camdx;
		}

		public void MoveCamUp() {
			if (-0.1 < camdy && camdy < 0.1)
				--camdy;
		}

		public void MoveCamRight() {
			if (-0.1 < camdx && camdx < 0.1)
				++camdx;
		}

		public void MoveCamDown() {
			if (-0.1 < camdy && camdy < 0.1)
				++camdy;
		}

		public void ProcessCamMove() {
			//Settings.gameWindow.Title = $"{Settings.camSpeed} \n {camdx:0.00} \n {camdy:0.00} \n {CamPos.X:0.00} \n {CamPos.Y:0.00}";

			if (camdx <= -0.1) {
				CamPos.X -= Settings.camSpeed;
				camdx += Settings.camSpeed;
			}
			else if (camdx >= 0.1) {
				CamPos.X += Settings.camSpeed;
				camdx -= Settings.camSpeed;
			}
			else if (camdy <= -0.1) {
				CamPos.Y -= Settings.camSpeed;
				camdy += Settings.camSpeed;
			}
			else if (camdy >= 0.1) {
				CamPos.Y += Settings.camSpeed;
				camdy -= Settings.camSpeed;
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
