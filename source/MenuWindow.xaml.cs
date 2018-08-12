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
using System.Windows.Shapes;

namespace ld42 {
	/// <summary>
	/// Interaction logic for MenuWindow.xaml
	/// </summary>
	public partial class MenuWindow : Window {
		static public MenuWindow menuWindow;
		static public GameOverWindow gameOverWindow = new GameOverWindow();
		static public GameWindow gameWindow;
		static public HightscoresWindow hightscoresWindow = new HightscoresWindow();

		public MenuWindow() {
			InitializeComponent();
			WindowManager.AddWindow(this);
			menuWindow = this;
		}

		public void Button_Play(object sender, RoutedEventArgs e) {
			gameWindow = new GameWindow();
			this.ReopenWindow(gameWindow);
		}

		private void Button_Hightscores(object sender, RoutedEventArgs e) {
			this.ReopenWindow(hightscoresWindow);
		}

		private void Button_Exit(object sender, RoutedEventArgs e) {
			this.Close();
		}

		private void WindowClosed(object sender, EventArgs e) {
			WindowManager.CloseAll();
		}
	}
}
