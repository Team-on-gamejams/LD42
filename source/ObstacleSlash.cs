using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld42 {
	class ObstacleSlash : BasicObstacle {
		public ObstacleSlash(System.Windows.Controls.Canvas canvas) : base(@"Resources\img\enemyShield.gif", PlayerState.Slash, canvas) {

		}
	}
}
