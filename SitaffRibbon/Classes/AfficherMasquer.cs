using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SitaffRibbon.Classes
{
    public class AfficherMasquer
    {
        public AfficherMasquer()
        {

        }

        public void AffMas_Colonne(MenuItem menuItem, DataGridColumn dataGridColumn)
        {
            if (menuItem.IsChecked == true)
            {
                dataGridColumn.Visibility = Visibility.Collapsed;
                menuItem.IsChecked = false;
            }
            else
            {
                dataGridColumn.Visibility = Visibility.Visible;
                menuItem.IsChecked = true;
            }
        }
    }
}
