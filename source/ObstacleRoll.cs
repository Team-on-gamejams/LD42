using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld42 {
	class ObstacleRoll : BasicObstacle {
		public ObstacleRoll() : base(@"Resources\img\blockTop.png", PlayerState.Roll) {

		}
	}
}
