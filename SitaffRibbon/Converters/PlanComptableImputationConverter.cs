using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace SitaffRibbon.Converters
{
    class PlanComptableImputationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = "";

            if (value != DependencyProperty.UnsetValue)
            {
                if (value != null)
                {
                    result = ((Plan_Comptable_Imputation)value).Numero.ToString() + "-" + ((Plan_Comptable_Imputation)value).Libelle.ToString();
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
