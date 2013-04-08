using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace SitaffRibbon.Converters
{
    class Tache_AtelierLibelleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = "";

            if (value != DependencyProperty.UnsetValue)
            {
                if (value != null)
                {
                    result = ((Tache_Atelier)value).Libelle.ToString();
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
