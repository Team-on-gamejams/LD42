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
	class Ghost : Hero {
		public Ghost() {
			var bi = new BitmapImage(new Uri(Settings.imagePath + @"hero\ghost.gif", UriKind.Relative));
			ImageBehavior.SetAnimatedSource(image, bi);
		}
	}
}
