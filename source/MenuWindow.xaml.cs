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
using System.Media;

namespace ld42 {
	/// <summary>
	/// Interaction logic for MenuWindow.xaml
	/// </summary>
	public partial class MenuWindow : Window {
		public static MenuWindow menuWindow;
		private static GameWindow gameWindow;
		private static GameOverWindow gameOverWindow = new GameOverWindow();
		private static Lazy<HightscoresWindow> hightscoresWindow = new Lazy<HightscoresWindow>();
		private static Lazy<SettingsWindow> settingsWindow = new Lazy<SettingsWindow>();
		private static Lazy<CreditsWindow> creditsWindow = new Lazy<CreditsWindow>();

		public static GameWindow GameWindow => gameWindow;
		public static GameOverWindow GameOverWindow => gameOverWindow;
		public static HightscoresWindow HightscoresWindow => hightscoresWindow.Value;
		public static SettingsWindow SettingsWindow => settingsWindow.Value;
		public static CreditsWindow CreditsWindow => creditsWindow.Value;


		public MenuWindow() {
			InitializeComponent();
			WindowManager.AddWindow(this);
			menuWindow = this;
		}

		public void Button_Play(object sender, RoutedEventArgs e) {
			Sound.Click();
			gameWindow = new GameWindow();
			this.ReopenWindow(GameWindow);
		}

		private void Button_Hightscores(object sender, RoutedEventArgs e) {
			Sound.Click();
			this.ReopenWindow(HightscoresWindow);
		}

		private void Button_Settings(object sender, RoutedEventArgs e) {
			Sound.Click();
			this.ReopenWindow(SettingsWindow);
		}

		private void Button_Credits(object sender, RoutedEventArgs e) {
			Sound.Click();
			this.ReopenWindow(CreditsWindow);
		}

		private void Button_Exit(object sender, RoutedEventArgs e) {
			//Sound.Click();
			this.Close();
		}

		private void WindowClosed(object sender, EventArgs e) {
			WindowManager.CloseAll();
		}
	}
}
