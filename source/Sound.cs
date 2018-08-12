using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Media;

namespace ld42 {
	static class Sound {
		static SoundPlayer clickPlayer, rollPlayer, jumpPlayer, slashPlayer, fallPlayer;
		static Sound() {
			clickPlayer =			new SoundPlayer(@".\Resources\music\click.wav");
			rollPlayer =			new SoundPlayer(@".\Resources\music\roll.wav");
			jumpPlayer =			new SoundPlayer(@".\Resources\music\jump.wav");
			slashPlayer =			new SoundPlayer(@".\Resources\music\slash.wav");
			fallPlayer =			new SoundPlayer(@".\Resources\music\fall.wav");

			clickPlayer.Load();
			rollPlayer.Load();
			jumpPlayer.Load();
			slashPlayer.Load();
			fallPlayer.Load();
		}
		
		static public void Click() => clickPlayer.Play();

		static public void Jump() =>  jumpPlayer.Play();
		static public void Roll() =>  rollPlayer.Play();
		static public void Slash() => slashPlayer.Play();
		static public void Fall() =>  fallPlayer.Play();
	}
}
