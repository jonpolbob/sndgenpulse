using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tstsoundgen
{
	class WavDataChunk
	{
		public string sChunkID;     // "data"
		public uint dwChunkSize;    // Length of header in bytes
		public short[] shortArray;  // 8-bit audio
        public float[] floatArray;  // 8-bit audio

        /// <summary>
        /// Initializes a new data chunk with default values.
        /// </summary>
        public WavDataChunk()
		{
			shortArray = new short[0];
            floatArray = new float[0];

            dwChunkSize = 0;
			sChunkID = "data";
		}
	}
}
