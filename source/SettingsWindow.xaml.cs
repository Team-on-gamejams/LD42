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

namespace ld42
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window{
        public SettingsWindow(){
            InitializeComponent();
			WindowManager.AddWindow(this);

			musicSlider.Value = Music.Volume * 10;
			musicText.Text = $"Music volume {(int)(musicSlider.Value * 10)}%";
			soundSlider.Value = Sound.Volume * 10;
			soundText.Text = $"Sound volume {(int)(soundSlider.Value * 10)}%";

			string[] lines = System.IO.File.ReadAllText(@".\settings").Split('|');
			speedText.Text = $"Starting speed {Settings.startingSpeedIndex}";

			this.safezoneSlider.Value = Settings.safezone;
			this.safezoneText.Text = $"Starting safezone {(byte)safezoneSlider.Value}";

			checkBoxRepeatSound.IsChecked = Settings.repeatSound;

			if (Settings.startingSpeedIndex == 0)
				speedSlider.Value = 0;
			else if (Settings.startingSpeedIndex == 1)
				speedSlider.Value = 1.5;
			else if (Settings.startingSpeedIndex == 2)
				speedSlider.Value = 3.5;
			else if (Settings.startingSpeedIndex == 3)
				speedSlider.Value = 5.5;
			else if (Settings.startingSpeedIndex == 4)
				speedSlider.Value = 7.5;
			else if (Settings.startingSpeedIndex == 5)
				speedSlider.Value = 10;
			speedText.Text = $"Starting speed {Settings.startingSpeedIndex}";
		}

		private void WindowClosed(object sender, EventArgs e) {
			WindowManager.CloseAll();
		}

		private void Button_Back(object sender, RoutedEventArgs e) {
			Sound.Click();
			WindowManager.ReopenWindow(this, MenuWindow.menuWindow);
		}

		private void musicSlider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e) {
			musicText.Text = $"Music volume {(int)(musicSlider.Value * 10)}%";
		}

		private void musicSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e) {
			Music.Volume = musicSlider.Value / 10;

			string text = "";
			string[] lines = System.IO.File.ReadAllText(@".\settings").Split('|');
			lines[0] = ((int)(musicSlider.Value * 100)).ToString();
			for(byte i = 0; i < lines.Length; ++i) 
				text += lines[i] + '|';
			text = text.Substring(0, text.Length - 1);
			System.IO.File.WriteAllText(@".\settings", text);
		}

		private void soundSlider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e) {
			soundText.Text = $"Sound volume {(int)(soundSlider.Value * 10)}%";
		}

		private void soundSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e) {
			Sound.Volume = soundSlider.Value / 10;

			string text = "";
			string[] lines = System.IO.File.ReadAllText(@".\settings").Split('|');
			lines[1] = ((int)(soundSlider.Value * 100)).ToString();
			for (byte i = 0; i < lines.Length; ++i)
				text += lines[i] + '|';
			text = text.Substring(0, text.Length - 1);
			System.IO.File.WriteAllText(@".\settings", text);
		}

		private void safezoneSlider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e) {
			this.safezoneText.Text = $"Starting safezone {(byte)safezoneSlider.Value}";
		}

		private void safezoneSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e) {
			Settings.safezone = (byte)safezoneSlider.Value;

			string text = "";
			string[] lines = System.IO.File.ReadAllText(@".\settings").Split('|');
			lines[2] = Settings.safezone.ToString();
			for (byte i = 0; i < lines.Length; ++i)
				text += lines[i] + '|';
			text = text.Substring(0, text.Length - 1);
			System.IO.File.WriteAllText(@".\settings", text);
		}

		private void speedSlider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e) {
			byte index = 5;
			if (speedSlider.Value <= 1)
				index = 0;
			else if (speedSlider.Value <= 3)
				index = 1;
			else if (speedSlider.Value <= 5)
				index = 2;
			else if (speedSlider.Value <= 7)
				index = 3;
			else if (speedSlider.Value <= 9)
				index = 4;
			speedText.Text = $"Starting speed {index}";
		}

		private void speedSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e) {
			byte index = 5;
			if (speedSlider.Value <= 1)
				index = 0;
			else if (speedSlider.Value <= 3)
				index = 1;
			else if (speedSlider.Value <= 5)
				index = 2;
			else if (speedSlider.Value <= 7)
				index = 3;
			else if (speedSlider.Value <= 9)
				index = 4;
			Settings.startingSpeedIndex = index;

			string text = "";
			string[] lines = System.IO.File.ReadAllText(@".\settings").Split('|');
			lines[3] = Settings.startingSpeedIndex.ToString();
			for (byte i = 0; i < lines.Length; ++i)
				text += lines[i] + '|';
			text = text.Substring(0, text.Length - 1);
			System.IO.File.WriteAllText(@".\settings", text);
		}

		private void checkBoxRepeatSound_Checked(object sender, RoutedEventArgs e) {
			Settings.repeatSound = checkBoxRepeatSound.IsChecked.Value;

			string text = "";
			string[] lines = System.IO.File.ReadAllText(@".\settings").Split('|');
			lines[4] = Settings.repeatSound.ToString();
			for (byte i = 0; i < lines.Length; ++i)
				text += lines[i] + '|';
			text = text.Substring(0, text.Length - 1);
			System.IO.File.WriteAllText(@".\settings", text);
		}
	}
}
