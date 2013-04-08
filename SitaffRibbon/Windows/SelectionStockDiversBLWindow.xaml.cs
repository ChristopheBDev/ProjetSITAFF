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
    /// Logique d'interaction pour SelectionStockDiversBLWindow.xaml
    /// </summary>
    public partial class SelectionStockDiversBLWindow : Window
    {
        #region Attributs

        public bool stock = false;
        public bool divers = false;

        #endregion

        #region Constructeur

        public SelectionStockDiversBLWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);
        }

        #endregion

        #region Evenements

        private void _checkBoxStock_Checked(object sender, RoutedEventArgs e)
        {
            this.stock = true;
            this.divers = false;
            this._checkBoxDivers.IsChecked = false;
        }

        private void _checkBoxStock_Unchecked(object sender, RoutedEventArgs e)
        {
            this.stock = false;            
        }

        private void _checkBoxDivers_Checked(object sender, RoutedEventArgs e)
        {
            this.divers = true;
            this.stock = false;
            this._checkBoxStock.IsChecked = false;
        }

        private void _checkBoxDivers_Unchecked(object sender, RoutedEventArgs e)
        {
            this.divers = false;
        }

        #endregion

        #region Boutons

        private void _buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this._checkBoxStock.IsChecked == true || this._checkBoxDivers.IsChecked == true)
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void _buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion
    }
}
