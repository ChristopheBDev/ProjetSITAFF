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
/* 
 * Using pour utilisation des IObservableCollection (afin d'éviter de mettre
 * System.Collections.ObjectModel.IObservableCollection en entier)
 */
using System.Collections.ObjectModel;
//Using pour utiliser le type TypeConverter pour la conversion de couleur
using System.ComponentModel;
using System.Windows.Shapes;


namespace SitaffRibbon.Windows.ParametresWindows
{
    /// <summary>
    /// Logique d'interaction pour TypeReceptionWindow.xaml
    /// </summary>
    public partial class TypeReceptionWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public TypeReceptionWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxTypeReception.Focus();
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
                if (((App)App.Current).mySitaffEntities.Type_Reception.Where(act => act.Identifiant != ((Type_Reception)this.DataContext).Identifiant).Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxTypeReception.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Un type de récéption est déjà présent avec ce libellé", "Doublon de type de récéption", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

            if (!Verif_TextBoxTypeReception())
            {
                verif = false;
            }


            return verif;
        }
        #region _TextBoxTypeReception
        private bool Verif_TextBoxTypeReception()
        {
            bool verif = true;
            char[] masque = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int j = 0;
            char i;

            if ((this._TextBoxTypeReception.Text.Trim().Length > 0) && (this._TextBoxTypeReception.Text.Trim().Length <= 255))
            {
                while ((j < masque.Length) && (verif))
                {
                    i = masque[j];
                    if (this._TextBoxTypeReception.Text.Contains(i))
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

			((App)App.Current).verifications.MettreTextBoxEnCouleur(_TextBoxTypeReception, _TextBlocke, verif);
            return verif;
        }

        private void _TextBoxTypeReception_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxTypeReception();
        }
        #endregion

        #endregion

        #region lecture seule

        //Passe tous les composant en lecture seule
        public void lectureSeule()
        {
            this._TextBoxTypeReception.IsReadOnly = false;
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
