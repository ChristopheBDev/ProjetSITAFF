using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace SitaffRibbon.Converters
{
    class CommandeFournisseurNumeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = "";

            if (value != DependencyProperty.UnsetValue)
            {
                if (value != null)
                {
                    result = ((Commande_Fournisseur)value).Numero.ToString();
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
