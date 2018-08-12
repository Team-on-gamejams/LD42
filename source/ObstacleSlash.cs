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
		public ObstacleSlash(System.Windows.Controls.Canvas canvas) : base(@"Resources\img\enemyShield.gif", PlayerState.Slash, canvas) {

		}
	}
}
