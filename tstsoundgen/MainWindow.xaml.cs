using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tstsoundgen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SoundGen legenerator = new SoundGen();

        SoundPArameters lesdatas = null;

        public SeriesCollection lesseries { get; set; }




        
        int ConverttoInt(string txtin, int prvvalue)
        {
            int lavaleur = 0;
            try
            {
                lavaleur = int.Parse(txtin);
            }
            catch (FormatException lexception)
            { return prvvalue; }

            return lavaleur;
        }

        public MainWindow()
        {
            lesdatas = new SoundPArameters(this);

            InitializeComponent();

            lesseries = new SeriesCollection
            {

                new LineSeries
                { Values = new ChartValues<ObservablePoint>
                    { new ObservablePoint(0,0),
                    new ObservablePoint(100, 0),
                    new ObservablePoint(100, 50),
                    new ObservablePoint(110, 50),
                    new ObservablePoint(110, 0),
                    new ObservablePoint(600, 0)
                    },

                LineSmoothness=0,
                PointGeometrySize=0,
                StrokeThickness=1,
                Stroke=new SolidColorBrush(Colors.DarkRed),
                Fill=new SolidColorBrush(Colors.Pink)

                },


            new LineSeries
                { Values = new ChartValues<ObservablePoint>
                    { new ObservablePoint(0,0),
                    new ObservablePoint(500, 0),
                    new ObservablePoint(500, 100),
                    new ObservablePoint(510, 100),
                    new ObservablePoint(510, 0),
                    new ObservablePoint(600, 0)
                    },

                LineSmoothness=0,
                PointGeometrySize=0,
                StrokeThickness=1,
                Stroke=new SolidColorBrush(Colors.DarkRed),
                Fill=new SolidColorBrush(Colors.Red)
                },

                new LineSeries
                { Values = new ChartValues<ObservablePoint>
                    {
                    new ObservablePoint(490, 0),
                    new ObservablePoint(490, 80),
                    new ObservablePoint(505, 80),
                    new ObservablePoint(505, 0)

                    },
                 LineSmoothness=0,
                PointGeometrySize=0,
                StrokeThickness=1,
                Opacity=.2,
                Fill=new SolidColorBrush(new Color {R = Colors.AliceBlue.R, G=Colors.AliceBlue.G, B=Colors.AliceBlue.B,A=150 }),
                Stroke=new SolidColorBrush(new Color{ R=0, A = 150,  G=0 , B=255 })
                }



            };

            DataContext = this;
            ReglPulse.DataContext = this.lesdatas;
            ReglPpi.DataContext = this.lesdatas;
            ReglLight.DataContext = this.lesdatas;
            //txtPulsDelay.DataContext = this;


        }

        // appele lors du changement d'une valeur
        public void updatedrawing()
        {
            int stimdelay = int.Parse(lesdatas.stimDelay);
            int stimdur = (int)Math.Truncate(double.Parse(lesdatas.stimAbsDur));
            int stimatt = 100-int.Parse(lesdatas.stimAtten);

            int ppidelay = int.Parse(lesdatas.ppiDelay);
            int ppidur = (int)Math.Truncate(double.Parse(lesdatas.ppiAbsDur));
            int ppiatt = stimatt - int.Parse(lesdatas.ppiAtten);
            if (ppiatt <0)
                ppiatt = 0;
            enumchoix ppimode = lesdatas.ppiSyncMode;

            int ppideb = lesdatas.debPPi;

            int lightdur = int.Parse(lesdatas.lightDuration);
            int lightdeb = lesdatas.debLight;

            ObservablePoint lept = lesseries[1].Values[1] as ObservablePoint;
            lept.X = stimdelay;
            //lept.Y = int.Parse(0);

            lept = lesseries[1].Values[2] as ObservablePoint;
            lept.X = stimdelay;
            lept.Y = stimatt;

            lept = lesseries[1].Values[3] as ObservablePoint;
            lept.X = stimdelay+stimdur;
            lept.Y = stimatt;

            lept = lesseries[1].Values[4] as ObservablePoint;
            lept.X = stimdelay+stimdur;

            lept = lesseries[1].Values[5] as ObservablePoint;
            lept.X = stimdelay + stimdur+100;


            lept = lesseries[0].Values[1] as ObservablePoint;
            lept.X = ppideb;
            lept = lesseries[0].Values[2] as ObservablePoint;
            lept.X = ppideb;
            lept.Y = ppiatt;

            lept = lesseries[0].Values[3] as ObservablePoint;
            lept.X = ppideb + ppidur;
            lept.Y = ppiatt;

            lept = lesseries[0].Values[4] as ObservablePoint;
            lept.X = ppideb + ppidur;
            lept.Y = 0;

            lept = lesseries[2].Values[0] as ObservablePoint;
            lept.X = lightdeb;
            lept = lesseries[2].Values[1] as ObservablePoint;
            lept.X = lightdeb;
            lept = lesseries[2].Values[2] as ObservablePoint;
            lept.X = lightdeb + lightdur;
            lept = lesseries[2].Values[3] as ObservablePoint;
            lept.X = lightdeb + lightdur;

            //lept.Y = int.Parse(0)

        }



        private  bool IsTextNumber(string text)
        {
            Regex regex = new Regex("[^0-9-]+"); //regex that matches disallowed text
            return regex.IsMatch(text);
        }

        int valeur = 10;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            legenerator.gendata(lesdatas);
            legenerator.SaveSound("a.wav");
        }

        private void txtPulsAtt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumber(e.Text);
        }
    }
}
