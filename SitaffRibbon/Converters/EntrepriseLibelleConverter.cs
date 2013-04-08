using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace SitaffRibbon.Converters
{
    class EntrepriseLibelleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = "";
            try
            {
                
            if (value != DependencyProperty.UnsetValue)
            {
                if (value != null)
                {
                    result = ((Entreprise)value).Libelle.ToString();
                }
            }
            }
            catch (Exception)
            {
                
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
