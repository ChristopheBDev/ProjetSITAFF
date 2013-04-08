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
    /// Logique d'interaction pour ChapitreClauseWindow.xaml
    /// </summary>
    public partial class ChapitreClauseWindow : Window
    {

        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public ChapitreClauseWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxLibelle.Focus();
        }

        #endregion

        #region Boutons

        #region bouton ok
        /// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                if (((App)App.Current).mySitaffEntities.Chapitre_Clause.Where(act => act.Identifiant != ((Chapitre_Clause)this.DataContext).Identifiant).
                    Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxLibelle.Text.Trim().ToLower()).
                    Where(lib => lib.Libelle_Russe.Trim().ToLower() == this._TextBoxLibelleRusse.Text.Trim().ToLower()).
                        Where(cod => cod.Libelle_Anglais.Trim().ToLower() == this._TextBoxLibelleAnglais.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Une clause est déjà présente avec ces libellés", "Doublon de clause", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }
        #endregion

        #region bouton annuler
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

        #endregion

        #region Verifications
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxLibelle())
            {
                verif = false;
            }
            if (!Verif_TextBoxLibelleAnglais())
            {
                verif = false;
            }
            if (!Verif_TextBoxLibelleRusse())
            {
                verif = false;
            }


            return verif;
        }
        #region _TextBoxLibelle

        private bool Verif_TextBoxLibelle()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxLibelle, this.textBlock1);
        }

        private void _TextBoxLibelle_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelle();
        }
        #endregion

        #region _TextBoxLibelleAnglais

        private bool Verif_TextBoxLibelleAnglais()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxLibelleAnglais, this.textBlock2);
        }

        private void _TextBoxLibelleAnglais_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelleAnglais();
        }
        #endregion

        #region _TextBoxLibelleRusse

        private bool Verif_TextBoxLibelleRusse()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxLibelleRusse, this.textBlock3);
        }

        private void _TextBoxLibelleRusse_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelleRusse();
        }
        #endregion

        #endregion

        #region lecture seule

        //Passe tous les composant en lecture seule
        public void lectureSeule()
        {
            this._TextBoxLibelleRusse.IsReadOnly = false;
            this._TextBoxLibelleAnglais.IsReadOnly = false;
            this._TextBoxLibelle.IsReadOnly = false;
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
