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
	/// Interaction logic for GameOverWindow.xaml
	/// </summary>
	public partial class GameOverWindow : Window {
		public GameOverWindow() {
			InitializeComponent();
			WindowManager.AddWindow(this);
		}

		private void WindowClosed(object sender, EventArgs e) {
			WindowManager.CloseAll();
		}

		private void Button_Play(object sender, EventArgs e) {
			Sound.Click();
			WindowManager.ReopenWindow(this, MenuWindow.menuWindow);
			MenuWindow.menuWindow.Button_Play(null, null);
			SaveScore();
		}

		private void Button_Menu(object sender, EventArgs e) {
			Sound.Click();
			WindowManager.ReopenWindow(this, MenuWindow.menuWindow);
			SaveScore();
		}

		void SaveScore() {
			System.IO.File.AppendAllText(@".\score", nick.Text + '|' + MenuWindow.gameWindow.score.ToString() + '\n');
		}

		private void Window_Activated(object sender, EventArgs e) {
			this.ScoreText.Text = $"Score: {MenuWindow.gameWindow.score}";
		}
	}
}
