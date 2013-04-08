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
using System.ComponentModel;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour ChoixTypeCommandeWindow.xaml
    /// </summary>
    public partial class ChoixTypeCommandeWindow : Window
    {

        #region Attributs

        public Affaire affaire = null;
        public Entreprise_Mere entreprise_mere = null;
        public bool divers = false;
        public bool stock = false;

        #endregion

        #region Propd



        public ObservableCollection<Entreprise_Mere> listEntreprise_Mere
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntreprise_MereProperty); }
            set { SetValue(listEntreprise_MereProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntreprise_Mere.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntreprise_MereProperty =
            DependencyProperty.Register("listEntreprise_Mere", typeof(ObservableCollection<Entreprise_Mere>), typeof(ChoixTypeCommandeWindow), new UIPropertyMetadata(null));

        

        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(ChoixTypeCommandeWindow), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public ChoixTypeCommandeWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);
        }

        #region initialisations

        private void initialisationPropDependance()
        {
            this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
            this.listEntreprise_Mere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(em => em.Nom));
        }

        #endregion

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this._checkBoxAffaire.IsChecked = true;
        }

        #endregion

        #region Evenements

        private void _checkBoxStock_Checked(object sender, RoutedEventArgs e)
        {
            this._checkBoxAffaire.IsChecked = false;
            this._checkBoxDivers.IsChecked = false;
            this._comboBoxAffaire.IsEnabled = false;
            this._comboBoxAffaire.SelectedItem = null;
            this._comboBoxEntreprise_MereDivers.IsEnabled = false;
            this._comboBoxEntreprise_MereDivers.SelectedItem = null;
            this._comboBoxEntreprise_MereStock.IsEnabled = true;
            this.stock = true;
        }

        private void _checkBoxStock_Unchecked(object sender, RoutedEventArgs e)
        {
            this.stock = false;
            this._comboBoxEntreprise_MereStock.IsEnabled = false;
        }

        private void _checkBoxAffaire_Checked(object sender, RoutedEventArgs e)
        {
            this._checkBoxStock.IsChecked = false;
            this._comboBoxEntreprise_MereStock.IsEnabled = false;
            this._comboBoxEntreprise_MereStock.SelectedItem = null;
            this._checkBoxDivers.IsChecked = false;
            this._comboBoxEntreprise_MereDivers.IsEnabled = false;
            this._comboBoxEntreprise_MereDivers.SelectedItem = null;
            this._comboBoxAffaire.IsEnabled = true;
        }

        private void _checkBoxAffaire_Unchecked(object sender, RoutedEventArgs e)
        {
            this.affaire = null;
            this._comboBoxAffaire.IsEnabled = false;
        }

        private void _checkBoxDivers_Checked(object sender, RoutedEventArgs e)
        {
            this._checkBoxAffaire.IsChecked = false;
            this._comboBoxAffaire.IsEnabled = false;
            this._comboBoxAffaire.SelectedItem = null;
            this._checkBoxStock.IsChecked = false;
            this._comboBoxEntreprise_MereStock.IsEnabled = false;
            this._comboBoxEntreprise_MereStock.SelectedItem = null;
            this._comboBoxEntreprise_MereDivers.IsEnabled = true;
            this.divers = true;
        }

        private void _checkBoxDivers_Unchecked(object sender, RoutedEventArgs e)
        {
            this.divers = false;
        }

        #endregion

        #region Verifs

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!this.Verif_checkBoxs())
            {
                verif = false;
            }            
            if (!this.Verif_comboBoxAffaire())
            {
                verif = false;
            }
            if (this._checkBoxStock.IsChecked == true)
            {
                if (!this.Verif_comboBoxEntreprise_MereStock())
                {
                    verif = false;
                }
            }
            if (this._checkBoxDivers.IsChecked == true)
            {
                if (!this.Verif_comboBoxEntreprise_MereDivers())
                {
                    verif = false;
                }
            }

            return verif;
        }

        private bool Verif_checkBoxs()
        {
            bool verif = false;

            if (this._checkBoxAffaire.IsChecked == true || this._checkBoxStock.IsChecked == true || this._checkBoxDivers.IsChecked == true)
            {
                verif = true;
            }

            return verif;
        }

        #region Entreprise_MereDivers
        private bool Verif_comboBoxEntreprise_MereDivers()
        {
            bool verif = true;

            if (this._checkBoxDivers.IsChecked == true)
            {
                if (this._comboBoxEntreprise_MereDivers.SelectedItem == null)
                {
                    verif = false;
                }
                else
                {
                    verif = true;
                    this.entreprise_mere = (Entreprise_Mere)this._comboBoxEntreprise_MereDivers.SelectedItem;
                }
            }
            else
            {
                verif = true;
                this.entreprise_mere = null;
            }
			((App)App.Current).verifications.MettreComboxEnCouleur(this._comboBoxEntreprise_MereDivers, this._textBlockDivers, verif);
            return verif;
        }

        private void _comboBoxEntreprise_MereDivers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxEntreprise_MereDivers();
        }
        #endregion  
      
        #region Entreprise_MereStock
        private bool Verif_comboBoxEntreprise_MereStock()
        {
            bool verif = true;

            if (this._checkBoxStock.IsChecked == true)
            {
                if (this._comboBoxEntreprise_MereStock.SelectedItem == null)
                {
                    verif = false;
                }
                else
                {
                    verif = true;
                    this.entreprise_mere = (Entreprise_Mere)this._comboBoxEntreprise_MereStock.SelectedItem;
                }
            }
            else
            {
                verif = true;
            }

			((App)App.Current).verifications.MettreComboxEnCouleur(this._comboBoxEntreprise_MereStock, this._textBlockStock, verif);

            return verif;
        }

        private void _comboBoxEntreprise_MereStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxEntreprise_MereStock();
        }
        #endregion        

        #region Affaire
        private bool Verif_comboBoxAffaire()
        {
            bool verif = true;

            if (this._checkBoxAffaire.IsChecked == true)
            {
                if (this._comboBoxAffaire.SelectedItem == null)
                {
                    verif = false;
                }
                else
                {
                    verif = true;
                    this.affaire = (Affaire)this._comboBoxAffaire.SelectedItem;
                }
            }
            else
            {
                verif = true;
                this.affaire = null;
            }

			((App)App.Current).verifications.MettreComboxEnCouleur(this._comboBoxAffaire, this._textBlockAffaire, verif);

            return verif;
        }

        private void _comboBoxAffaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxAffaire();
            if (this._checkBoxAffaire.IsChecked == true && this._comboBoxAffaire.SelectedItem != null)
            {
                if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.Count() == 0)
                {
                    MessageBox.Show("Votre affaire n° " + ((Affaire)this._comboBoxAffaire.SelectedItem).Numero + " n'a aucun devis d'associé, nous ne pouvons donc pas facturer sur cette affaire. Veuillez régler ce problème au prélable. Pardonnez-nous du dérangement.", "Pas de devis associé", MessageBoxButton.OK, MessageBoxImage.Information);
                    this._comboBoxAffaire.SelectedItem = null;
                }
            }
        }
        #endregion        

        #endregion

        #region Boutons ok / annuler

        private void _buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void _buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        #endregion

    }
}
