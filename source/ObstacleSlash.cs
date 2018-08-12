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
	class ObstacleSlash : BasicObstacle {
		static byte r = (byte) new Random().Next(0, 100);

		public ObstacleSlash(System.Windows.Controls.Canvas canvas) : base(
			r < 33 ? @"Resources\img\enemyShield.gif" :
			r < 66 ? @"Resources\img\enemyShield2.gif" :
			@"Resources\img\enemyShield3.gif",
			PlayerState.Slash, canvas
		) {
			r = (byte)new Random().Next(0, 100);
		}
	}
}
