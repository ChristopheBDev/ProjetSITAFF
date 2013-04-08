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
    /// Logique d'interaction pour BanqueWindow.xaml
    /// </summary>
    public partial class BanqueWindow : Window
    {

        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public BanqueWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxLibelleBanque.Focus();
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
                if (((App)App.Current).mySitaffEntities.Banque.Where(act => act.Identifiant != ((Banque)this.DataContext).Identifiant).Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxLibelleBanque.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Une banque est déjà présente avec ce libellé", "Doublon de banque", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

        /// <summary>
        /// Verifie si tous les champs sont bien renseignés.
        /// </summary>
        /// <returns>booléen vrai si tous les champs sont bien renseignés, sinon retourne faux</returns>
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!verif_tab_banque())
            {
                verif = false;
            }


            return verif;
        }
        #region Tab Banque
        private bool verif_tab_banque()
        {
            bool test = true;

            if (!Verif_TextBoxLibelleBanque())
            {
                test = false;
            }
            if (!Verif_TextBoxCodeBanque())
            {
                test = false;
            }

            return test;
        }
        #endregion

        #region Libelle
        private bool Verif_TextBoxLibelleBanque()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxLibelleBanque, this._TextBlockLibelleBanque);
        }


        private void _TextBoxLibelleBanque_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelleBanque();
        }
        #endregion

        #region Code
        private bool Verif_TextBoxCodeBanque()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxCodeBanque, this._TextBlockCodeBanque);
        }


        private void _TextBoxCodeBanque_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCodeBanque();
        }
        #endregion

        #endregion

        #region lecture seule

        public void lectureSeule()
        {
            _TextBoxLibelleBanque.IsReadOnly = false;
            _TextBoxCodeBanque.IsReadOnly = false;
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
