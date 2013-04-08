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
    /// Logique d'interaction pour TvaWindow.xaml
    /// </summary>
    public partial class TvaWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public TvaWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxLibelleTva.Focus();
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
                double tmpTest;
                try
                {
                    tmpTest = double.Parse(this._TextBoxTauxTva.Text);
                }
                catch (Exception)
                {
                    tmpTest = 0;
                }
                if (((App)App.Current).mySitaffEntities.Tva.Where(act => act.Identifiant != ((Tva)this.DataContext).Identifiant).
                    Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxLibelleTva.Text.Trim().ToLower()).
                        Where(tau => tau.Taux == tmpTest).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Une Tva est déjà présente avec ce libellé et ce taux", "Doublon de Tva", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

            if (!verif_tab_tva())
            {
                verif = false;
            }


            return verif;
        }
        #region Tab Tva
        private bool verif_tab_tva()
        {
            bool test = true;

            if (!Verif_TextBoxLibelleTva())
            {
                test = false;
            }
            if (!Verif_TextBoxTauxTva())
            {
                test = false;
            }

            return test;
        }
        #endregion

        #region Libelle
        private bool Verif_TextBoxLibelleTva()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(_TextBoxLibelleTva, _TextBlockLibelleTva);
        }


        private void _TextBoxLibelleTva_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelleTva();
        }
        #endregion

        #region Code
        private bool Verif_TextBoxTauxTva()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(_TextBoxTauxTva, _TextBlockTauxTva);
        }


        private void _TextBoxTauxTva_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxTauxTva();
        }
        #endregion



        #endregion

        #region lecture seule

        public void lectureSeule()
        {
            _TextBoxLibelleTva.IsReadOnly = false;
            _TextBoxTauxTva.IsReadOnly = false;
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
