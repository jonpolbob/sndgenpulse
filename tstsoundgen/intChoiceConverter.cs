using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace tstsoundgen
{
    public enum enumchoix { Choix1, Choix2, Choix3 };

    public class intChoiceConverter : IValueConverter
    {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
            //Debug.Write("ok");
                return value.Equals(parameter);  // renvoie true si on a la valeur
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {// renvoie la valeur en int si on a recu un true
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
       
    }
}
