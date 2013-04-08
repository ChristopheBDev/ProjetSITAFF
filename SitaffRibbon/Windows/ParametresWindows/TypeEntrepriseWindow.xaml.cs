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
    /// Logique d'interaction pour TypeEntrepriseWindow.xaml
    /// </summary>
    public partial class TypeEntrepriseWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public TypeEntrepriseWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxTypeEntreprise.Focus();
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
                if (((App)App.Current).mySitaffEntities.Type_Entreprise.Where(act => act.Identifiant != ((Type_Entreprise)this.DataContext).Identifiant).Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxTypeEntreprise.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Un type d'entreprise est déjà présent avec ce libellé", "Doublon de type d'entreprise", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

            if (!Verif_TextBoxTypeEntreprise())
            {
                verif = false;
            }


            return verif;
        }

        #region _TextBoxTypeEntreprise
        private bool Verif_TextBoxTypeEntreprise()
        {
            bool verif = true;
            char[] masque = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int j = 0;
            char i;

            if ((this._TextBoxTypeEntreprise.Text.Trim().Length > 0) && (this._TextBoxTypeEntreprise.Text.Trim().Length <= 255))
            {
                while ((j < masque.Length) && (verif))
                {
                    i = masque[j];
                    if (this._TextBoxTypeEntreprise.Text.Contains(i))
                    {
                        verif = false;
                    }
                    else
                    {
                        verif = true;
                    }
                    j++;
                }
            }
            else
            {
                verif = false;
            }
			((App)App.Current).verifications.MettreTextBoxEnCouleur(_TextBoxTypeEntreprise, _TextBlockTypeEntreprise, verif);
            return verif;
        }

        private void _TextBoxTypeEntreprise_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxTypeEntreprise();
        }
        #endregion

        #endregion

        #region lecture seule

        public void lectureSeule()
        {
            this._TextBoxTypeEntreprise.IsReadOnly = false;
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
