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
    /// Logique d'interaction pour ModeFacturationWindow.xaml
    /// </summary>
    public partial class ModeFacturationWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public ModeFacturationWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxModeFacturation.Focus();
        }

        #endregion

        #region Boutons

        /// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                if (((App)App.Current).mySitaffEntities.Mode_Facturation.Where(act => act.Identifiant != ((Mode_Facturation)this.DataContext).Identifiant).Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxModeFacturation.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Un mode de facturation est déjà présent avec ce libellé", "Doublon de mode de facturation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        /// <summary>
        /// Fonction lancée après clic sur Annuler
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        #region Verifications
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxModeFacturation())
            {
                verif = false;
            }


            return verif;
        }

        #region _TextBoxModeFacturation
        private bool Verif_TextBoxModeFacturation()
        {
            bool verif = true;
            char[] masque = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

            if ((this._TextBoxModeFacturation.Text.Trim().Length > 0) && (this._TextBoxModeFacturation.Text.Trim().Length <= 255))
            {
                verif = true;
            }
            else
            {
                verif = false;
            }

			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxModeFacturation, this._TextBlockModeFacturation, verif);
            return verif;
        }

        private void _TextBoxModeFacturation_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxModeFacturation();
        }

        #endregion

        #endregion

        #region lecture seule

        //Passe tous les composant en lecture seule
        public void lectureSeule()
        {
            this._TextBoxModeFacturation.IsReadOnly = false;
        }

        #endregion

        #region fenetre chargé

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }
        #endregion
    }
}
