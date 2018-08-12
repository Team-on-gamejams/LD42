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
	/// Interaction logic for HightscoresWindow.xaml
	/// </summary>
	public partial class HightscoresWindow : Window {
		public HightscoresWindow() {
			InitializeComponent();
			WindowManager.AddWindow(this);
		}

		private void WindowClosed(object sender, EventArgs e) {
			WindowManager.CloseAll();
		}

		private void Button_Back(object sender, RoutedEventArgs e) {
			Sound.Click();
			WindowManager.ReopenWindow(this, MenuWindow.menuWindow);
		}

		private void Window_Activated(object sender, EventArgs e) {
			nickPanel.Children.Clear();
			scorePanel.Children.Clear();

			string[] lines = System.IO.File.ReadAllLines(@".\score");
			List<Tuple<string, ulong>> scores = new List<Tuple<string, ulong>>(lines.Length);
			foreach (var l in lines) {
				var tmp = l.Split('|');
				scores.Add(new Tuple<string, ulong>(tmp[0], ulong.Parse(tmp[1])));
			}

			scores.Sort((a, b) => (int)(b.Item2 - a.Item2));

			foreach (var s in scores) {
				nickPanel.Children.Add(new TextBlock() { Text = s.Item1, Style = (Style)FindResource("hightscoresTextNick") });
				scorePanel.Children.Add(new TextBlock() { Text = s.Item2.ToString(), Style = (Style)FindResource("hightscoresTextNick") });
			}
		}
	}
}
