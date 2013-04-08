using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace SitaffRibbon.Converters
{
    class CommandeEntierementFactureeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = "";

            if (value != DependencyProperty.UnsetValue)
            {
                if (value != null)
                {
                    if ((bool)value == true)
                    {
                        result = "Oui";
                    }
                    else
                    {
                        result = "Non";
                    }
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
