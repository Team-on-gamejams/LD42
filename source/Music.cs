using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld42{
    static class Music{
		static Music() {
			string[] lines = System.IO.File.ReadAllText(@".\settings").Split('|');
			if (int.TryParse(lines[0], out int result))
				Volume = result / 1000.0;
			else
				Volume = 0.5;
		}

		static public double Volume {
			get;
			set;
		}
	}
}
