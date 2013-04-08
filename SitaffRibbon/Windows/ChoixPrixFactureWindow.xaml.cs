using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour ChoixPrixFactureWindow.xaml
    /// </summary>
    public partial class ChoixPrixFactureWindow : Window
    {
        public bool leBooleen = true;

        public ChoixPrixFactureWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.leBooleen = true;
            try
            {
                this.CheckBox2.IsChecked = false;
            }
            catch (Exception) { }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.CheckBox2.IsChecked == false)
            {
                this.CheckBox.IsChecked = true;
            }
        }

        private void CheckBox2_Checked(object sender, RoutedEventArgs e)
        {
            this.leBooleen = false;
            try
            {
                this.CheckBox.IsChecked = false;
            }
            catch (Exception) { }
        }

        private void CheckBox2_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.CheckBox.IsChecked == false)
            {
                this.CheckBox2.IsChecked = true;
            }
        }
    }
}
