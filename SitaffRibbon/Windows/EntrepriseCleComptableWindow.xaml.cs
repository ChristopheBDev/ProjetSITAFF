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
using System.Collections.ObjectModel;
//Using pour utiliser le type TypeConverter pour la conversion de couleur
using System.ComponentModel;
using SitaffRibbon.Classes;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour EntrepriseCleComptableWindow.xaml
    /// </summary>
    public partial class EntrepriseCleComptableWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region proprieté de dependance

        public ObservableCollection<Entreprise_Mere> listEntrepriseMere
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntrepriseMereProperty); }
            set { SetValue(listEntrepriseMereProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntrepriseMere.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseMereProperty =
            DependencyProperty.Register("listEntrepriseMere", typeof(ObservableCollection<Entreprise_Mere>), typeof(EntrepriseCleComptableWindow), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public EntrepriseCleComptableWindow(ObservableCollection<Entreprise_Mere> listToRemove)
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Initialisation de la sécurité
            this.initialisationSecurite();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            this.removeListEntrepriseMere(listToRemove);
        }

        #region initialisation

        private void initialisationPropDependance()
        {
            this.listEntrepriseMere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(nom => nom.Nom));
        }

        private void initialisationSecurite()
        {
            //Mise en place des droits sur les boutons et tabs
            
        }

        private void removeListEntrepriseMere(ObservableCollection<Entreprise_Mere> listToRemove)
        {
            if (listToRemove != null)
            {
                foreach (Entreprise_Mere item in listToRemove)
                {
                    try
                    {
                        this.listEntrepriseMere.Remove(item);
                    }
                    catch (Exception) { }
                }
            }
        }

        #endregion

        #endregion

        #region verifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_ComboBoxEntrepriseMere())
            {
                verif = false;
            }
            if (!Verif_TextBoxCle())
            {
                verif = false;
            }
            return verif;
        }

        #region entreprise mere

        private bool Verif_ComboBoxEntrepriseMere()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxEntrepriseMere, this._TextBlockEntrepriseMere);
        }

        private void _ComboBoxEntrepriseMere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxEntrepriseMere();
        }

        #endregion

        #region Champs cle
        private bool Verif_TextBoxCle()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxCle, this._TextBlockCle, 255);
        }

        private void _TextBoxCle_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCle();
        }
        #endregion

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
                this.DialogResult = true;
                this.Close();
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

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #endregion

        #region Lecture seule

        public void lectureSeule()
        {
            _ComboBoxEntrepriseMere.IsEnabled = false;
            _TextBoxCle.IsReadOnly = true;
        }

        #endregion

    }
}
