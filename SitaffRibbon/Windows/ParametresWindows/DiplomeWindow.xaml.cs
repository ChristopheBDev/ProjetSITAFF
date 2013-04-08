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
    /// Logique d'interaction pour DiplomeWindow.xaml
    /// </summary>
    public partial class DiplomeWindow : Window
    {

        #region Attributs

        public bool soloLecture = false;

        #endregion

        public DiplomeWindow()
        {
            InitializeComponent();
        }

        #region bouton ok et annuler

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void _ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        #region verifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxLibelle())
            {
                verif = false;
            }


            return verif;
        }

        private bool Verif_TextBoxLibelle()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxLibelle, this._TextBlockLibelle);
        }

        private void _TextBoxSalarieNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelle();
        }

        #endregion

        #region Lecture seule
        //Passe tous les composant en lecture seule
        public void lectureSeule()
        {
            this._TextBoxLibelle.IsEnabled = false;
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }
    }
}
