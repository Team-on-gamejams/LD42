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
	class Bullet {
		public CoordReal pos;
		public Image image;
		public byte speed;
		public double angle;

		public Bullet(string path, double angle, CoordReal shootPos, byte speed) {
			pos = new CoordReal(shootPos);
			this.speed = speed;
			this.angle = angle;

			image = new Image();
			image.LoadImg(path);
			image.RenderTransformOrigin = new Point(0.5, 0.5);
			image.RenderTransform = new RotateTransform(angle);
			Settings.gameWindow.GameCanvas.Children.Add(image);
			Canvas.SetZIndex(image, 10);
			Canvas.SetLeft(image, pos.X);
			Canvas.SetTop(image, pos.Y);

			Settings.game.bullets.Add(this);
		}

		public void Collision() {
			Settings.gameWindow.GameCanvas.Children.Remove(image);
		}
	}
}
