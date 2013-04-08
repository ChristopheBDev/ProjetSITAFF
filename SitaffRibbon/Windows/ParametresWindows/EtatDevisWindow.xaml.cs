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
    /// Logique d'interaction pour EtatDevisWindow.xaml
    /// </summary>
    public partial class EtatDevisWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region contructeur

        public EtatDevisWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxLibelleEtatDevis.Focus();
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
                if (((App)App.Current).mySitaffEntities.Devis_Etat.Where(act => act.Identifiant != ((Devis_Etat)this.DataContext).Identifiant).Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxLibelleEtatDevis.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Un état de devis est déjà présent avec ce libellé", "Doublon d'état de devis", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

            if (!verif_tab_etat_devis())
            {
                verif = false;
            }


            return verif;
        }
        #region Tab Etat Devis
        private bool verif_tab_etat_devis()
        {
            bool test = true;

            if (!Verif_TextBoxLibelleEtatDevis())
            {
                test = false;
            }


            return test;
        }
        #endregion

        #region Libelle
        private bool Verif_TextBoxLibelleEtatDevis()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxLibelleEtatDevis, this._TextBlockLibelleEtatDevis);
        }


        private void _TextBoxLibelleEtatDevis_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelleEtatDevis();
        }
        #endregion


        #endregion

        #region lecture seule

        public void lectureSeule()
        {
            _TextBoxLibelleEtatDevis.IsReadOnly = false;
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

