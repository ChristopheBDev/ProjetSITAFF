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
    /// Logique d'interaction pour DAOWindow.xaml
    /// </summary>
    public partial class DAOWindow : Window
    {

        #region Variables

        public bool creation = false;

        #endregion

        #region Propriétés de dépendances



        public ObservableCollection<Entreprise_Mere> listEntrepriseMeres
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntrepriseMeresProperty); }
            set { SetValue(listEntrepriseMeresProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntrepriseMeres.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseMeresProperty =
            DependencyProperty.Register("listEntrepriseMeres", typeof(ObservableCollection<Entreprise_Mere>), typeof(DAOWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Salarie> listSalaries
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalariesProperty); }
            set { SetValue(listSalariesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalaries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalariesProperty =
            DependencyProperty.Register("listSalaries", typeof(ObservableCollection<Salarie>), typeof(DAOWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Devis> listDevis
        {
            get { return (ObservableCollection<Devis>)GetValue(listDevisProperty); }
            set { SetValue(listDevisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listDevis.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listDevisProperty =
            DependencyProperty.Register("listDevis", typeof(ObservableCollection<Devis>), typeof(DAOWindow), new UIPropertyMetadata(null));




        public ObservableCollection<Affaire> listAffaires
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffairesProperty); }
            set { SetValue(listAffairesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaires.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffairesProperty =
            DependencyProperty.Register("listAffaires", typeof(ObservableCollection<Affaire>), typeof(DAOWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Client> listClients
        {
            get { return (ObservableCollection<Client>)GetValue(listClientsProperty); }
            set { SetValue(listClientsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listClients.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listClientsProperty =
            DependencyProperty.Register("listClients", typeof(ObservableCollection<Client>), typeof(DAOWindow), new UIPropertyMetadata(null));



        #endregion

        #region Constructeur

        public DAOWindow()
        {
            InitializeComponent();
            
            //Remplissage des ComboBox
            this.initialisationComboBox();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);
        }

        #region initialisations

        public void initialisationComboBox()
        {
            this.listAffaires = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
            this.listClients = new ObservableCollection<Client>(((App)App.Current).mySitaffEntities.Client.OrderBy(cli => cli.Entreprise.Libelle));
            this.listEntrepriseMeres = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(em => em.Nom));
            this.listSalaries = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.OrderBy(sal => sal.Personne.Nom));
            this.listDevis = new ObservableCollection<Devis>(((App)App.Current).mySitaffEntities.Devis.OrderBy(dev => dev.Numero));
        }

        #endregion

        #endregion

        #region Boutons

        #region Boutons Ok / Annuler

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                this.creationNumeroAuto();
                this.DialogResult = true;
                this.Close();
            }
        }

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

            if (!this.Verif_comboBoxDemandeur())
            {
                verif = false;
            }
            if (!Verif_comboBoxClient())
            {
                verif = false;
            }
            if (!Verif_textBoxLibelle())
            {
                verif = false;
            }
            if (!Verif_comboBoxAffaire())
            {
                verif = false;
            }
            if (!Verif_comboBoxDevis())
            {
                verif = false;
            }
            if (!Verif_textBoxHeures())
            {
                verif = false;
            }
            if (!Verif_textBoxCommentaire())
            {
                verif = false;
            }
            if (!Verif_comboBoxEntrepriseMere())
            {
                verif = false;
            }

            return verif;
        }

        #region Demandeur

        private bool Verif_comboBoxDemandeur()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxSalarie, this._textBlockSalarie);
        }

        private void _comboBoxSalarie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Verif_comboBoxDemandeur())
            {
                if (this._comboBoxSalarie.SelectedItem != null)
                {
                    if (((Salarie)this._comboBoxSalarie.SelectedItem).Salarie_Interne != null)
                    {
                        this._comboBoxEntrepriseMere.SelectedItem = ((Salarie)this._comboBoxSalarie.SelectedItem).Salarie_Interne.Entreprise_Mere1;
                    }
                }
            }
        }

        #endregion

        #region Client

        private bool Verif_comboBoxClient()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxClient, this._textBlockClient);
        }

        private void _comboBoxClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxClient();
        }

        #endregion

        #region Designation du produit

        private bool Verif_textBoxLibelle()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxLibelle, this._textBlockLibelle);
        }

        private void _textBoxLibelle_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxLibelle();
        }

        #endregion

        #region Affaire

        private bool Verif_comboBoxAffaire()
        {
            bool verif = true;

            if (this._checkBoxSurAffaire.IsChecked == true)
            {
				verif = ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxAffaire, this._textBlockAffaire);
            }
            else
            {
				verif = ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxAffaire, this._textBlockAffaire);
            }

            return verif;
        }

        private void _comboBoxAffaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Verif_comboBoxAffaire())
            {
                if (creation)
                {
                    if (this._comboBoxAffaire.SelectedItem != null)
                    {
                        if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.Count != 0)
                        {
                            Versions temp;
                            temp = ((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First();
                            this._comboBoxClient.SelectedItem = temp.Devis1.Client;
                            this._comboBoxSalarie.SelectedItem = temp.Devis1.Salarie;
                        }
                    }
                }
            }
        }

        #endregion

        #region Devis

        private bool Verif_comboBoxDevis()
		{
            bool verif = true;

            if (this._checkBoxSurDevis.IsChecked == true)
            {
				verif = ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxDevis, this._textBlockDevis);
            }
            else
			{
				verif = ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxDevis, this._textBlockDevis);
            }

            return verif;
        }

        private void _comboBoxDevis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Verif_comboBoxDevis())
            {
                if (creation)
                {
                    if (this._comboBoxDevis.SelectedItem != null)
                    {
                        this._comboBoxClient.SelectedItem = ((Devis)this._comboBoxDevis.SelectedItem).Client;
                        this._comboBoxSalarie.SelectedItem = ((Devis)this._comboBoxDevis.SelectedItem).Salarie;
                    }
                }
            }
        }

        #endregion

        #region Heures Passées

        private bool Verif_textBoxHeures()
        {
			return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._textBoxHeures, this._textBlockHeures);
        }

        private void _textBoxHeures_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxHeures();
        }

        #endregion

        #region Commentaire

        private bool Verif_textBoxCommentaire()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxCommentaire, this._textBlockCommentaire);
        }

        private void _textBoxCommentaire_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxCommentaire();
        }

        #endregion

        #region EntrepriseMere

        private bool Verif_comboBoxEntrepriseMere()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxEntrepriseMere, this._textBlockEntrepriseMere);
        }

        private void _comboBoxEntrepriseMere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxEntrepriseMere();
        }

        #endregion

        #endregion

        #region Evénements

        #region chechbox

        #region Affaire

        private void _checkBoxSurAffaire_Unchecked(object sender, RoutedEventArgs e)
        {
            this._comboBoxAffaire.SelectedItem = null;
            this._comboBoxAffaire.Visibility = Visibility.Hidden;
            this._textBlockAffaire.Visibility = Visibility.Hidden;
        }

        private void _checkBoxSurAffaire_Checked(object sender, RoutedEventArgs e)
        {
            this._checkBoxSurDevis.IsChecked = false;
            this._comboBoxAffaire.Visibility = Visibility.Visible;
            this._textBlockAffaire.Visibility = Visibility.Visible;
        }

        #endregion

        #region Devis

        private void _checkBoxSurDevis_Unchecked(object sender, RoutedEventArgs e)
        {
            this._comboBoxDevis.SelectedItem = null;
            this._comboBoxDevis.Visibility = Visibility.Hidden;
            this._textBlockDevis.Visibility = Visibility.Hidden;
        }

        private void _checkBoxSurDevis_Checked(object sender, RoutedEventArgs e)
        {
            this._checkBoxSurAffaire.IsChecked = false;
            this._comboBoxDevis.Visibility = Visibility.Visible;
            this._textBlockDevis.Visibility = Visibility.Visible;
        }

        #endregion

        #endregion        

        #endregion

        #region Fonctions

        private void creationNumeroAuto()
        {
            if (creation)
            {
                ((DAO)this.DataContext).Annee = DateTime.Today.Year;
                ((DAO)this.DataContext).Mois = DateTime.Today.Month;
                ObservableCollection<DAO> tempRecherche = new ObservableCollection<DAO>(((App)App.Current).mySitaffEntities.DAO.Where(dao => dao.Annee == ((DAO)this.DataContext).Annee && dao.Mois == ((DAO)this.DataContext).Mois));
                int incrementation = 1;
                if (tempRecherche.Count != 0)
                {
                    foreach (DAO dao in tempRecherche.OrderBy(dao => dao.Increment))
                    {
                        incrementation = dao.Increment;
                    }
                    incrementation = incrementation + 1;
                }
                ((DAO)this.DataContext).Increment = incrementation;
                MessageBox.Show("Votre dessin sera enregistré sous le numéro suivant : " + ((DAO)this.DataContext).getNumero, "Dessin numéro " + ((DAO)this.DataContext).getNumero, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;

            if (!creation)
            {
                this.Title = "DAO : " + ((DAO)this.DataContext).getNumero;
                if (((DAO)this.DataContext).Affaire1 != null)
                {
                    this._checkBoxSurAffaire.IsChecked = true;
                }
                if (((DAO)this.DataContext).Devis1 != null)
                {
                    this._checkBoxSurDevis.IsChecked = true;
                }
            }
        }

        #endregion

        #region lectureSeule

        public void lectureSeule()
        {
            //ComboBox
            this._comboBoxSalarie.IsEnabled = false;
            this._comboBoxClient.IsEnabled = false;
            this._comboBoxAffaire.IsEnabled = false;
            this._comboBoxDevis.IsEnabled = false;
            this._comboBoxEntrepriseMere.IsEnabled = false;

            //CheckBox
            this._checkBoxSurAffaire.IsEnabled = false;
            this._checkBoxSurDevis.IsEnabled = false;

            //TextBox
            this._textBoxLibelle.IsReadOnly = true;
            this._textBoxHeures.IsReadOnly = true;
            this._textBoxCommentaire.IsReadOnly = true;
        }

        #endregion

    }
}
