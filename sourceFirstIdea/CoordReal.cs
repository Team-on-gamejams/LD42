using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld42 {
	class CoordReal {
		private double y;
		private double x;

		public double X { get => x; set { SizeChanged?.Invoke(value - x, 0); x = value; } }
		public double Y { get => y; set { SizeChanged?.Invoke(0, value - y); y = value; } }

		public CoordReal() {
			X = Y = 0;
		}

		public CoordReal(double x, double y) {
			this.X = x;
			this.Y = y;
		}

		public CoordReal(Coord c) {
			X = c.X;
			Y = c.Y;
		}

		public CoordReal(CoordReal c) {
			X = c.X;
			Y = c.Y;
		}

		public override bool Equals(object obj) {
			CoordReal cr = obj as CoordReal;
			if (cr != null)
				return X == cr.X && Y == cr.Y;

			Coord c = obj as Coord;
			if (c != null)
				return X == c.X && Y == c.Y;
			return false;
		}

		public delegate void RealSizeDelegate(double xChanged, double yChanged);
		public RealSizeDelegate SizeChanged;
	}
}
