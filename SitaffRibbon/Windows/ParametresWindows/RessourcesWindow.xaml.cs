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
/* 
 * Using pour utilisation des IObservableCollection (afin d'éviter de mettre
 * System.Collections.ObjectModel.IObservableCollection en entier)
 */
using System.Collections.ObjectModel;
//Using pour utiliser le type TypeConverter pour la conversion de couleur
using System.ComponentModel;

namespace SitaffRibbon.Windows.ParametresWindows
{
    /// <summary>
    /// Logique d'interaction pour RessourcesWindow.xaml
    /// </summary>
    public partial class RessourcesWindow : Window
    {
        public RessourcesWindow()
        {
            InitializeComponent();
        }

        #region Verfication champs

        #region _TextBoxBesoin

        private bool Verif_TextBoxBesoin()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxBesoin.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBoxBesoin.Foreground = Brushes.Red;
                this._TextBoxBesoin.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBoxBesoin.Foreground = Brushes.Green;
                this._TextBoxBesoin.Background = vert;
            }

            return verif;
        }

        private void _TextBoxBesoin_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxBesoin();
        }

        #endregion

        #region _TextBoxNature

        private bool Verif_TextBoxNature()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxNature.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBoxNature.Foreground = Brushes.Red;
                this._TextBoxNature.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBoxNature.Foreground = Brushes.Green;
                this._TextBoxNature.Background = vert;
            }

            return verif;
        }

        private void _TextBoxNature_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxNature();
        }

        #endregion



        #endregion

        #region bouton ok et annuler

        private void _BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.Verif_TextBoxBesoin() && this.Verif_TextBoxNature())
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void _BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        public void lectureSeule()
        {
            this._TextBoxNature.IsEnabled = false;
            this._TextBoxBesoin.IsEnabled = false;
        }
    }
}
