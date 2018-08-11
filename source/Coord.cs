using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld42 {
	class Coord {
		private ushort y;
		private ushort x;

		public ushort X { get => x; set { SizeChanged?.Invoke((sbyte)(value - x), 0); x = value;  } }
		public ushort Y { get => y; set { SizeChanged?.Invoke(0, (sbyte)(value - y)); y = value;  } }

		public Coord() {
			X = Y = 0;
		}

		public Coord(ushort X, ushort Y) {
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

		public delegate void SizeDelegate(SByte xChanged, SByte yChanged);
		public SizeDelegate SizeChanged;
	}
}
