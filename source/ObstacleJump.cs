using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld42 {
	class ObstacleJump : BasicObstacle {
		public ObstacleJump(System.Windows.Controls.Canvas canvas) : base(@"Resources\img\blockDown.png", PlayerState.Jump, canvas) {

		}
	}
}
