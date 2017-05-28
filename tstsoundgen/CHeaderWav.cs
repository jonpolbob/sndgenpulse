using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tstsoundgen
{
	class CHeaderWav
	{
		public string sGroupID; // RIFF
		public uint dwFileLength; // total file length minus 8, which is taken up by RIFF
		public string sRiffType; // always WAVE

		/// <summary>
		/// Initializes a WaveHeader object with the default values.
		/// </summary>
		public CHeaderWav()
		{
			dwFileLength = 0;
			sGroupID = "RIFF";
			sRiffType = "WAVE";
		}
	}
}
