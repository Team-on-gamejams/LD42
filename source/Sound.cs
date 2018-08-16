using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Media;

namespace ld42 {
	static class Sound {
		static MediaPlayer clickPlayer, rollPlayer, jumpPlayer, slashPlayer, fallPlayer;
		static Sound() {
				clickPlayer = new MediaPlayer();
				rollPlayer = new MediaPlayer();
				jumpPlayer = new MediaPlayer();
				slashPlayer = new MediaPlayer();
				fallPlayer = new MediaPlayer();

				clickPlayer.Open(new Uri(@"Resources\music\click.wav", UriKind.Relative));
				rollPlayer.Open(new Uri(@"Resources\music\roll.wav", UriKind.Relative));
				jumpPlayer.Open(new Uri(@"Resources\music\jump.wav", UriKind.Relative));
				slashPlayer.Open(new Uri(@"Resources\music\slash.wav", UriKind.Relative));
				fallPlayer.Open(new Uri(@"Resources\music\fall.wav", UriKind.Relative));

				string[] lines = System.IO.File.ReadAllText(@".\settings").Split('|');
				if (int.TryParse(lines[1], out int result))
					Volume = result / 1000.0;
				else
					Volume = 0.5;
		}

		static public double Volume{
			get => clickPlayer.Volume;
			set => clickPlayer.Volume = rollPlayer.Volume = jumpPlayer.Volume = slashPlayer.Volume = fallPlayer.Volume = value;
		}
		
		static public void Click() { clickPlayer.Stop(); clickPlayer.Play();}
		static public void Jump()  { jumpPlayer.Stop(); jumpPlayer.Play();}
		static public void Roll()  { rollPlayer.Stop(); rollPlayer.Play();}
		static public void Slash() { slashPlayer.Stop(); slashPlayer.Play();}
		static public void Fall()  { fallPlayer.Stop(); fallPlayer.Play();}
	}
}
