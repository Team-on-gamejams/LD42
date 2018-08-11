using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld42 {
	class Coord {
		private short y;
		private short x;

		public short X { get => x; set { Changed?.Invoke((sbyte)(value - x), 0); x = value;  } }
		public short Y { get => y; set { Changed?.Invoke(0, (sbyte)(value - y)); y = value;  } }

		public Coord() {
			X = Y = 0;
		}

		public Coord(short X, short Y) {
			this.X = X;
			this.Y = Y;
		}

		public Coord(Coord c) {
			X = c.X;
			Y = c.Y;
		}

		public override bool Equals(object obj) {
			return X == ((Coord)(obj)).X && Y == ((Coord)(obj)).Y;
		}

		public override string ToString() {
			return $"(x={X} y={Y})";
		}

		public delegate void SizeDelegate(SByte xChanged, SByte yChanged);
		public SizeDelegate Changed;
	}
}
