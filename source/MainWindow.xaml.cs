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
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		ulong tick = 1;

		Image backgroundImage1;
		Image backgroundImage2;
		short backgroundImagePos = 0;

		public MainWindow() {
			InitializeComponent();

			backgroundImage1 = new Image() { Source=new BitmapImage(new Uri(@"Resources\img\road.png", UriKind.Relative))};
			gameCanvas.Children.Add(backgroundImage1);
			Canvas.SetLeft(backgroundImage1, 0);
			backgroundImage2 = new Image() { Source = new BitmapImage(new Uri(@"Resources\img\road.png", UriKind.Relative)) };
			gameCanvas.Children.Add(backgroundImage2);
			Canvas.SetLeft(backgroundImage2, 1000);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			System.Timers.Timer t = new System.Timers.Timer() {
				AutoReset = true,
				Interval = 10,
				Enabled = false,
			};

			t.Elapsed += (a, b) => {
				System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
					try {
						Canvas.SetLeft(backgroundImage1, Canvas.GetLeft(backgroundImage1) - 1);
						if (Canvas.GetLeft(backgroundImage1) <= -1000)
							Canvas.SetLeft(backgroundImage1, 1000);
						Canvas.SetLeft(backgroundImage2, Canvas.GetLeft(backgroundImage2) - 1);
						if (Canvas.GetLeft(backgroundImage2) <= -1000)
							Canvas.SetLeft(backgroundImage2, 1000);
					}
					catch(Exception ex) {
						MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
					}
				});
			};

			t.Start();
		}
	}
}
