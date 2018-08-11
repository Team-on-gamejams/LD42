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
	static class Settings {
		static public GameWindow gameWindow;

		static public string imagePath = "Resources\\";
		static public string imageExt = ".png";

		static public byte tickInterval = 10;
		static public ulong tick = 0;
		static public byte ticksForMove = 30;

		static public Coord fieldSize = new Coord(50, 20);

		static public Coord camStartPos = new Coord(0, 0);
		static public Coord camSize = new Coord(31, 15);
		static public CoordReal cellSize = new CoordReal();
	}
}
