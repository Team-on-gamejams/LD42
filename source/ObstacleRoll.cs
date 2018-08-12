using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld42 {
	class ObstacleRoll : BasicObstacle {
		public ObstacleRoll(System.Windows.Controls.Canvas canvas) : base(@"Resources\img\blockTop.png", PlayerState.Roll, canvas) {

		}
	}
}
