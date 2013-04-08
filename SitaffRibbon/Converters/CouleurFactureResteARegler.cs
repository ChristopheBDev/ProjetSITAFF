using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace SitaffRibbon.Converters
{
    class CouleurFactureResteARegler : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.Windows.Media.Brush result = System.Windows.Media.Brushes.White;

            if (value != DependencyProperty.UnsetValue)
            {
                if (value == null)
                {
                    result = System.Windows.Media.Brushes.Black;
                }
                else
                {
                    if ((double)value < 0)
                    {
                        result = System.Windows.Media.Brushes.Red;
                    }
                    else
                    {
                        result = System.Windows.Media.Brushes.Black;
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
