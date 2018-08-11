using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld42 {
	class ObstacleJump : BasicObstacle {
		public ObstacleJump() : base(@"Resources\img\blockDown.png", PlayerState.Jump) {

		}
	}
}
