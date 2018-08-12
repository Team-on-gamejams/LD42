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
	abstract class BasicObstacle {
		public Image image;
		public PlayerState stateToAvoid;
		Canvas canvas;

		protected BasicObstacle(string imgPath, PlayerState stateToAvoid, Canvas canvas) {
			this.image = new Image();
			ImageBehavior.SetAnimatedSource(image, new BitmapImage(new Uri(imgPath, UriKind.Relative)));
			this.stateToAvoid = stateToAvoid;
			this.canvas = canvas;
			Canvas.SetTop(image, 50);
			canvas.Children.Add(image);
		}

		public void Destroy() {
			canvas.Children.Remove(image);
		}
	}
}
