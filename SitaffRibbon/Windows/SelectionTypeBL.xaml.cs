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
    /// Logique d'interaction pour SelectionTypeBL.xaml
    /// </summary>
    public partial class SelectionTypeBL : Window
    {

        #region Attributs

        public Affaire affaire = null;
        public Salarie salarie = null;
        public Commande_Fournisseur commande_fournisseur = null;
        public bool testSupp = false;

        #endregion

        #region Propd



        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(SelectionTypeBL), new UIPropertyMetadata(null));



        public ObservableCollection<Commande_Fournisseur> listCommande_Fournisseur
        {
            get { return (ObservableCollection<Commande_Fournisseur>)GetValue(listCommande_FournisseurProperty); }
            set { SetValue(listCommande_FournisseurProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listCommande_Fournisseur.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listCommande_FournisseurProperty =
            DependencyProperty.Register("listCommande_Fournisseur", typeof(ObservableCollection<Commande_Fournisseur>), typeof(SelectionTypeBL), new UIPropertyMetadata(null));



        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(SelectionTypeBL), new UIPropertyMetadata(null));



        #endregion

        #region Constructeur

        public SelectionTypeBL()
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
            this.listSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.OrderBy(sal => sal.Personne.Nom).ThenBy(sal => sal.Personne.Prenom));
            this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
            this.listCommande_Fournisseur = new ObservableCollection<Commande_Fournisseur>(((App)App.Current).mySitaffEntities.Commande_Fournisseur.OrderBy(com => com.Numero));
        }

        #endregion

        #endregion

        #region Verifs

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!this.Verif_comboBoxDonneurOrdre())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxCommande())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxAffaire())
            {
                verif = false;
            }

            return verif;
        }

        #region Donneur Ordre
        private bool Verif_comboBoxDonneurOrdre()
        {
            bool verif = true;

            if (this._comboBoxDonneurOrdre.SelectedItem == null)
            {
                if (testSupp == false)
                {
                    verif = false;
                    this.salarie = null;
                }
                else
                {
                    verif = true;
                    this.salarie = null;
                }
            }
            else
            {
                verif = true;
                this.salarie = (Salarie)this._comboBoxDonneurOrdre.SelectedItem;
            }
			((App)App.Current).verifications.MettreComboxEnCouleur(this._comboBoxDonneurOrdre, this._textBlockDonneurOrdre, verif);
            return verif;
        }

        private void _comboBoxDonneurOrdre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxDonneurOrdre();
        }
        #endregion

        #region Commande
        private bool Verif_comboBoxCommande()
        {
            bool verif = true;

            if (this._comboBoxCommande.SelectedItem == null)
            {
                if (testSupp == false)
                {
                    verif = true;
                    this.commande_fournisseur = null;
                }
                else
                {
                    verif = false;
                    this.commande_fournisseur = null;
                }
            }
            else
            {
                verif = true;
                this.commande_fournisseur = (Commande_Fournisseur)this._comboBoxCommande.SelectedItem;
                this._comboBoxAffaire.SelectedItem = this.commande_fournisseur.Affaire1;
                this._comboBoxDonneurOrdre.SelectedItem = this.commande_fournisseur.Salarie;
            }
			((App)App.Current).verifications.MettreComboxEnCouleur(this._comboBoxCommande, this._textBlockCommande, verif);
            return verif;
        }

        private void _comboBoxCommande_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxCommande();
        }
        #endregion

        #region Affaire
        private bool Verif_comboBoxAffaire()
        {
            bool verif = true;

            if (this._comboBoxAffaire.SelectedItem == null)
            {
                verif = true;
                this.affaire = null;
                this.listCommande_Fournisseur = new ObservableCollection<Commande_Fournisseur>(((App)App.Current).mySitaffEntities.Commande_Fournisseur.OrderBy(com => com.Numero));
            }
            else
            {
                verif = true;
                this.affaire = (Affaire)this._comboBoxAffaire.SelectedItem;
                this._comboBoxCommande.ItemsSource = ((Affaire)this._comboBoxAffaire.SelectedItem).Commande_Fournisseur;                
            }
			((App)App.Current).verifications.MettreComboxEnCouleur(this._comboBoxAffaire, this._textBlockAffaire, verif);
            return verif;
        }

        private void _comboBoxAffaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxAffaire();
        }
        #endregion

        #endregion

        #region fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Boutons

        private void _buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void _buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

    }
}
