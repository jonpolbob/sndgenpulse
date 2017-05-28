using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tstsoundgen
{


    
    public class SoundPArameters : INotifyPropertyChanged
    {
        private int m_stimdelay = 24;
        private int m_stimduration = 10;
        private int m_stimatten = 0;
        private int m_ppidelay = 24;
        private int m_ppiduration = 10;
        private int m_ppiatten = 0;
        private enumchoix m_ppisynctopulse=0;
        private int m_lightduration=4;
        private int m_lightdelay = 0;
        private enumchoix m_lightsynctopulse = 0;
        private int m_freq = 660;

        public event PropertyChangedEventHandler PropertyChanged;

        MainWindow m_drawcontainer = null;

        public SoundPArameters(MainWindow DrawContainer)
        { m_drawcontainer = DrawContainer;
        }

        public void OnPropertyChanged(string propertyName)
        {
            m_drawcontainer.updatedrawing();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public String stimDelay
        {
            get { return m_stimdelay.ToString(); }
            //set { m_stimdelay = ConverttoInt(value, m_stimdelay); }
            set
            {
                m_stimdelay = int.Parse(value);
            //    this.m_drawcontainer.updatedrawing();
                OnPropertyChanged("stimDelay");
            }
        }


        public enumchoix ppiSyncMode
        {get { return m_ppisynctopulse; }
         set { m_ppisynctopulse = value;
              OnPropertyChanged("ppiSyncMode");
            }
        }

        public enumchoix lightSyncMode
        {
            get { return m_lightsynctopulse; }
            set
            {
                m_lightsynctopulse = value;
                OnPropertyChanged("lightSyncMode");
            }
        }



        public int debLight
        { get {
                int lightdeb = m_lightdelay;

                if (m_lightsynctopulse == enumchoix.Choix2) lightdeb = debPPi - m_lightdelay;
                if (m_lightsynctopulse == enumchoix.Choix3) lightdeb = m_stimdelay - m_lightdelay;

                return lightdeb;

            }
        }

        public int debPPi
        { get
            {
                int ppideb = this.m_ppidelay;
                if (this.m_ppisynctopulse == enumchoix.Choix2)
                    ppideb = m_stimdelay - m_ppidelay;

                return ppideb;

            }
        }
        

        public string stimFreq
        {
            get { return m_freq.ToString(); }
            //set { m_stimdelay = ConverttoInt(value, m_stimdelay); }
            set
            {
                m_freq = int.Parse(value);
                //  this.m_drawcontainer.updatedrawing();
                OnPropertyChanged("stimFreq");
                OnPropertyChanged("ppiAbsDur");
                OnPropertyChanged("stimAbsDur");
            }
        }

        public String lightDelay
        {
            get { return m_lightdelay.ToString(); }
            //set { m_stimdelay = ConverttoInt(value, m_stimdelay); }
            set
            {
                m_lightdelay = int.Parse(value);
                //  this.m_drawcontainer.updatedrawing();
                OnPropertyChanged("lightDelay");
            }
        }


        public String ppiDelay
        {
            get { return m_ppidelay.ToString(); }
            //set { m_stimdelay = ConverttoInt(value, m_stimdelay); }
            set
            {
                m_ppidelay = int.Parse(value);
              //  this.m_drawcontainer.updatedrawing();
                OnPropertyChanged("ppiDelay");
            }
        }


        public String stimAbsDur
        {get
            {
                double duree = m_stimduration / (double)m_freq * 1000;
                return duree.ToString("#.#");
            }

        }

        public String ppiAbsDur
        {
            get
            {
                double duree = m_ppiduration / (double)m_freq * 1000;
                return duree.ToString("#.#");
            }

        }


        public String ppiDuration
        {
            get { return m_ppiduration.ToString(); }
            //set { m_stimdelay = ConverttoInt(value, m_stimdelay); }
            set
            {
                m_ppiduration = int.Parse(value);
                //this.m_drawcontainer.updatedrawing();
                OnPropertyChanged("ppiDuration");
                OnPropertyChanged("ppiAbsDur");
            }
        }


        public String stimDuration
        {
            get { return m_stimduration.ToString(); }
            //set { m_stimdelay = ConverttoInt(value, m_stimdelay); }
            set
            {
                m_stimduration = int.Parse(value);
                //this.m_drawcontainer.updatedrawing();
                OnPropertyChanged("stimDuration");
                OnPropertyChanged("stimAbsDur");
            }
        }


        public String lightDuration
        {
            get { return m_lightduration.ToString(); }
            //set { m_stimdelay = ConverttoInt(value, m_stimdelay); }
            set
            {
                m_lightduration = int.Parse(value);
                //this.m_drawcontainer.updatedrawing();
                OnPropertyChanged("lightDuration");
                }
        }


        public String stimAtten
        {
            get { return m_stimatten.ToString(); }
            //set { m_stimdelay = ConverttoInt(value, m_stimdelay); }
            set
            {
                m_stimatten = int.Parse(value);
                //this.m_drawcontainer.updatedrawing();
                OnPropertyChanged("stimAtten");
            }
        }

        public String ppiAtten
        {
            get { return m_ppiatten.ToString(); }
            //set { m_stimdelay = ConverttoInt(value, m_stimdelay); }
            set
            {
                m_ppiatten = int.Parse(value);
                //this.m_drawcontainer.updatedrawing();
                OnPropertyChanged("ppiAtten");
            }
        }

    }
}
