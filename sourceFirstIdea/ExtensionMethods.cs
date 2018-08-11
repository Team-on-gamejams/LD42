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
	static class ExtensionMethods {
		static public void LoadImg(this Image img, string path) {
			img.Source = new BitmapImage(new Uri(Settings.imagePath + path + Settings.imageExt, UriKind.Relative));
		}
	}
}
