using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ecoplastol.globals
{
    //class converters
    //{
    //}

    public class BackgroundColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ilosc = (int)value;

            if(ilosc > 50)
            {
                return Brushes.Red;
            }
            else
            {
                return Brushes.LightGreen;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RezystancjaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // wciśnięcie klawisza w kontrolce
            // - sprawdzić czy jest tylko jeden przecinek
            // - zamienić na 0,00
            string rez = "0,0";

            var str = (string)value;
            if (string.IsNullOrEmpty(str))
            {
                rez = "0,00";
            }
            else
            {
                var poz = str.IndexOf(",");

                // ,
                if (poz == 0 && poz + 1 == str.Length)
                {
                    rez = "0,00";
                }

                // 0000,
                if (poz > 0 && poz + 1 == str.Length)
                {
                    rez = string.Concat(str, "0");
                }
                
                // ,0000
                if (poz == 0 && str.Length > 1)
                {
                    rez = string.Concat("0", str);
                }

                // 000,00
                // 00
                if ((poz > 0 && poz + 1 < str.Length) || (poz < 0))
                {
                    rez = str;
                }


                //var calkowita = str.Substring(0, poz);
                //var dziesietna = str.Substring(poz, str.Length - poz);
                //double value = Double.TryParse("", out value) ? value : 0.0;
            }
            
            return rez;
        }
    }
}
