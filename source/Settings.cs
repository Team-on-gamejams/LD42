using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld42{
    static class Settings{
		static public byte safezone = 8;
		static public byte startingSpeedIndex = 2;
		static public bool repeatSound = true;

		static Settings() {
			string[] lines = System.IO.File.ReadAllText(@".\settings").Split('|');
			if (!byte.TryParse(lines[2], out safezone))
				safezone = 8;
			if (!byte.TryParse(lines[3], out startingSpeedIndex))
				startingSpeedIndex = 2;
			if (!bool.TryParse(lines[4], out repeatSound))
				repeatSound = true;
		}
	}
}
