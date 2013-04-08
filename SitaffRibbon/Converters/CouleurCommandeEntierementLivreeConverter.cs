using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace SitaffRibbon.Converters
{
    class CouleurCommandeEntierementLivreeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.Windows.Media.Brush result = System.Windows.Media.Brushes.White;

            if (value != DependencyProperty.UnsetValue)
            {
                if (value == null)
                {
                    result = System.Windows.Media.Brushes.White;
                }
                else
                {
                    if ((bool)value == true)
                    {
                        result = System.Windows.Media.Brushes.Blue;
                    }
                    else
                    {
                        if ((bool)value == false)
                        {
                            result = System.Windows.Media.Brushes.Red;
                        }
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
