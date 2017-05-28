using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace tstsoundgen
{
    public class SoundGen
    {
        CHeaderWav header;
        FormatChunk lechunk;
        WavDataChunk ledata;

        // genere le buffer
        public int gendata(SoundPArameters lesdata)
        {
            int freq = int.Parse(lesdata.stimFreq);
            int ppiatt = int.Parse(lesdata.ppiAtten);
            int ppistart = lesdata.debPPi;
            uint ppinbcycles = uint.Parse(lesdata.ppiDuration);
            int stimstart = int.Parse(lesdata.stimDelay);
            uint stimnbcycles = uint.Parse(lesdata.stimDuration);
            int stimduree = (int)float.Parse(lesdata.stimAbsDur);

            int totaldur = stimstart + stimduree + 100; // rajouote 100ms de silence a la fin
            uint nbsample = (uint)totaldur * 96; // a 96 khz
            int levelstim = int.Parse(lesdata.stimAtten);
            double stimamplit = Math.Pow(-levelstim/10.0, 10);
            int levelppi = int.Parse(lesdata.ppiAtten);
            int lightstrt = int.Parse(lesdata.lightDelay);
            uint lightnbcycles = uint.Parse(lesdata.lightDuration);
            lightnbcycles = (uint)(lightnbcycles * 10000.0 / (double)freq);  // calcul du nb ech de lumiere sachant qu'on definit sa longueur en nb cycles audio

            uint buffersize = (uint)totaldur * 2 * 96;

            //taille complete du uffer
ledata.floatArray = new float[buffersize];
            uint i;
            for (i = 0; i < buffersize; i++)
                ledata.floatArray[i] = (float)0.0;

            //int amplitude = 32000;
            double stimlevel = Math.Pow(10,-levelstim / 10);
            double ppilevel = stimlevel*Math.Pow(10, -levelppi / 10);
            double t = (double)(lechunk.dwSamplesPerSec)/(double)freq;// ombre echantilllons d'un cycle

            int canal = 0;  
            //double freqpi = 2.0 * Math.PI * freq;
            double freqpi = 2.0 * Math.PI / lechunk.dwSamplesPerSec * freq;

            uint nbsamplespulse = (uint)t * ppinbcycles;
            int offsetppi = ppistart * 96;
            for (i=0; i< nbsamplespulse; i++)
                    ledata.floatArray[2* offsetppi + 2 * i+canal] = (float)ppilevel * (float)(Math.Sin(freqpi * (double)i));

            nbsamplespulse = (uint)t * stimnbcycles ;
            int offsetstim = 2*(stimstart * 96); // x2 car en stereo il faut 2 echantillons pour chaque ecahntillon
            for (i = 0; i < nbsamplespulse ; i++)
                ledata.floatArray[offsetstim + 2 * i + canal] = (float)stimlevel * (float)(Math.Sin(freqpi * (double)i));

            // envoi lumiere sur lautre canal
            freqpi = 2.0 * Math.PI / lechunk.dwSamplesPerSec * 10000.0;
            canal = 1 - canal; // on passe sur l'autre canal
            t = (double)(lechunk.dwSamplesPerSec) / (double)10000;// ombre echantilllons d'un cycle
            nbsamplespulse = (uint)t * lightnbcycles;
            int offsetlight = 2 * (lightstrt * 96); // x2 car en stereo il faut 2 echantillons pour chaque ecahntillon
            double lightamplit = 1.0;

            for (i = 0; i < nbsamplespulse; i++)
            {
                float Value = (float)lightamplit * (float)(Math.Sin(freqpi * (double)i));
                //String outputstr = Value.ToString()+" ";
                //Debug.Write(outputstr);
                ledata.floatArray[offsetlight + 2 * i + canal] = Value; 
            }

            ledata.dwChunkSize = buffersize*4;

            return 0;


        }

        public SoundGen()
        {
            header = new CHeaderWav();
            lechunk = new FormatChunk();
            lechunk.Set32bits();
            ledata = new WavDataChunk();
        }


       

        public void SaveSound(String FileName)
        {


            uint samplespersec = lechunk.dwSamplesPerSec;
            int nbchannels = lechunk.wChannels;
            int numsamples = (int)samplespersec * nbchannels;


            // Create a file (it always overwrites)
            FileStream fileStream = new FileStream(FileName, FileMode.Create);
            
            // Use BinaryWriter to write the bytes to the file
            BinaryWriter writer = new BinaryWriter(fileStream);

            // Write the header
            writer.Write(header.sGroupID.ToCharArray());
            writer.Write(header.dwFileLength);
            writer.Write(header.sRiffType.ToCharArray());

            // Write the format chunk
            writer.Write(lechunk.sChunkID.ToCharArray());
            writer.Write(lechunk.dwChunkSize);
            writer.Write(lechunk.wFormatTag);
            writer.Write(lechunk.wChannels);
            writer.Write(lechunk.dwSamplesPerSec);
            writer.Write(lechunk.dwAvgBytesPerSec);
            writer.Write(lechunk.wBlockAlign);
            writer.Write(lechunk.wBitsPerSample);

            // Write the data chunk
            writer.Write(ledata.sChunkID.ToCharArray());
            writer.Write(ledata.dwChunkSize);

            // pour commencer on convertit le tableau de int en tableau de flaote et on le sauvegarde en bytes
            
            var byteArray = new byte[ledata.floatArray.Length * 4];
            for (int i=0;i< ledata.floatArray.Length; i++)
                Array.Copy(BitConverter.GetBytes(ledata.floatArray[i]), 0, byteArray, i * 4,4);
            //Buffer.BlockCopy(ledata.floatArray, 0, byteArray, 0, byteArray.Length);

            writer.Write(byteArray);


            // on ecrit en debut de fichier sa lonueur

            writer.Seek(4, SeekOrigin.Begin);
            uint filesize = (uint)writer.BaseStream.Length;
            writer.Write(filesize - 8);
            
            writer.Close();
            fileStream.Close();

        }
    }
}
