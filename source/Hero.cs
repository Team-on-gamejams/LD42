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
	abstract class Hero {
		public Coord pos;
		public Direction dir;
		public byte tickOnCell;

		public Image image;

		public Hero() {
			image = new Image();
			image.Width = Settings.cellSize.X;
			image.Height = Settings.cellSize.Y;
			pos = new Coord();
		}

		abstract public Bullet Shoot(double angle, CoordReal shootPos);
	}
}
