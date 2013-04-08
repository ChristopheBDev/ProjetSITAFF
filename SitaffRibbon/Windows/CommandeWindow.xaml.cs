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
using SitaffRibbon.Classes;
using SitaffRibbon.UserControls;
using SitaffRibbon.Windows.ParametresUserControls;
using SitaffRibbon.Windows.ParametresWindows;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour CommandeWindow.xaml
    /// </summary>
    public partial class CommandeWindow : Window
    {
        #region Attributs

        public bool commande = true;
        public bool soloLecture = false;
        public double totalReelTemp = 0;
        public bool ventil = false;
        public bool chargement = true;

        public ShopCommandeWindow shopCommandeWindow = null;

        #endregion

        #region Propriétés de dépendances

        public ObservableCollection<Personne> listContactReception
        {
            get { return (ObservableCollection<Personne>)GetValue(listContactReceptionProperty); }
            set { SetValue(listContactReceptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listContactReception.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listContactReceptionProperty =
            DependencyProperty.Register("listContactReception", typeof(ObservableCollection<Personne>), typeof(CommandeWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Type_Commande> listTypeCommande
        {
            get { return (ObservableCollection<Type_Commande>)GetValue(listTypeCommandeProperty); }
            set { SetValue(listTypeCommandeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listTypeCommande.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listTypeCommandeProperty =
            DependencyProperty.Register("listTypeCommande", typeof(ObservableCollection<Type_Commande>), typeof(CommandeWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Entreprise> listEntreprise
        {
            get { return (ObservableCollection<Entreprise>)GetValue(listEntrepriseProperty); }
            set { SetValue(listEntrepriseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntreprise.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseProperty =
            DependencyProperty.Register("listEntreprise", typeof(ObservableCollection<Entreprise>), typeof(CommandeWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Condition_Reglement> list_Condition_Reglement
        {
            get { return (ObservableCollection<Condition_Reglement>)GetValue(list_Condition_ReglementProperty); }
            set { SetValue(list_Condition_ReglementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for list_Condition_Reglement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty list_Condition_ReglementProperty =
            DependencyProperty.Register("list_Condition_Reglement", typeof(ObservableCollection<Condition_Reglement>), typeof(CommandeWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Contact> listContacts
        {
            get { return (ObservableCollection<Contact>)GetValue(listContactsProperty); }
            set { SetValue(listContactsProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listContacts.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listContactsProperty =
            DependencyProperty.Register("listContacts", typeof(ObservableCollection<Contact>), typeof(CommandeWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Personne> listMonteur
        {
            get { return (ObservableCollection<Personne>)GetValue(listMonteurProperty); }
            set { SetValue(listMonteurProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listMonteur.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listMonteurProperty =
            DependencyProperty.Register("listMonteur", typeof(ObservableCollection<Personne>), typeof(CommandeWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Salarie> Donneur_Ordre
        {
            get { return (ObservableCollection<Salarie>)GetValue(Donneur_OrdreProperty); }
            set { SetValue(Donneur_OrdreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Donneur_Ordre.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Donneur_OrdreProperty =
            DependencyProperty.Register("Donneur_Ordre", typeof(ObservableCollection<Salarie>), typeof(CommandeWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Fournisseur> listFournisseurs
        {
            get { return (ObservableCollection<Fournisseur>)GetValue(listFournisseursProperty); }
            set { SetValue(listFournisseursProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listFournisseurs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFournisseursProperty =
            DependencyProperty.Register("listFournisseurs", typeof(ObservableCollection<Fournisseur>), typeof(CommandeWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Affaire> listAffaires
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffairesProperty); }
            set { SetValue(listAffairesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaires.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffairesProperty =
            DependencyProperty.Register("listAffaires", typeof(ObservableCollection<Affaire>), typeof(CommandeWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Entreprise> listEntrepriseLivraison
        {
            get { return (ObservableCollection<Entreprise>)GetValue(listEntrepriseLivraisonProperty); }
            set { SetValue(listEntrepriseLivraisonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntrepriseLivraison.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseLivraisonProperty =
            DependencyProperty.Register("listEntrepriseLivraison", typeof(ObservableCollection<Entreprise>), typeof(CommandeWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Entreprise> listEntrepriseFacturation
        {
            get { return (ObservableCollection<Entreprise>)GetValue(listEntrepriseFacturationProperty); }
            set { SetValue(listEntrepriseFacturationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntrepriseFacturation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseFacturationProperty =
            DependencyProperty.Register("listEntrepriseFacturation", typeof(ObservableCollection<Entreprise>), typeof(CommandeWindow), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public CommandeWindow()
        {
            InitializeComponent();

            //Création du menu de clic droit sur le datagrid
            this.creationMenuClicDroit();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Initialisation de la sécurité
            this.initialisationSecurite();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._datePickerDateLivraisonPrevu.Focus();
        }

        #region initialisation

        private void initialisationPropDependance()
        {
            this.list_Condition_Reglement = new ObservableCollection<Condition_Reglement>(((App)App.Current).mySitaffEntities.Condition_Reglement.OrderBy(cr => cr.Libelle));
            this.listContacts = new ObservableCollection<Contact>(((App)App.Current).mySitaffEntities.Contact.OrderBy(con => con.Personne.Nom));
            this.listMonteur = new ObservableCollection<Personne>(((App)App.Current).mySitaffEntities.Personne.Where(sal => sal.Salarie != null && sal.Salarie.Chantier == true).OrderBy(p => p.Nom).ThenBy(sal => sal.Prenom));
            this.Donneur_Ordre = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.OrderBy(sal => sal.Personne.Nom));
            this.listAffaires = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(bl => bl.Numero));
            this.listEntrepriseLivraison = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.OrderBy(ent => ent.Libelle));
            this.listEntreprise = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.OrderBy(ent => ent.Libelle));
            this.listFournisseurs = new ObservableCollection<Fournisseur>(((App)App.Current).mySitaffEntities.Fournisseur.OrderBy(fo => fo.Entreprise.Libelle));
            this.listTypeCommande = new ObservableCollection<Type_Commande>(((App)App.Current).mySitaffEntities.Type_Commande.OrderBy(tc => tc.Libelle));

            this.listEntrepriseFacturation = new ObservableCollection<Entreprise>();
            foreach (Entreprise_Mere em in ((App)App.Current).mySitaffEntities.Entreprise_Mere)
            {
                bool test = false;
                foreach (Entreprise ent in this.listEntrepriseFacturation)
                {
                    if (ent.Identifiant == em.Entreprise1.Identifiant)
                    {
                        test = true;
                    }
                }
                if (test == false)
                {
                    this.listEntrepriseFacturation.Add(em.Entreprise1);
                }
            }
            this.listEntrepriseFacturation = new ObservableCollection<Entreprise>(this.listEntrepriseFacturation.OrderBy(ent => ent.Libelle));
        }

        private void initialisationSecurite()
        {
            //Mise en place des droits sur les boutons et tabs
            if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeEntreprisesControl", "Add"))
            {
                this.NewFournisseur.Visibility = Visibility.Collapsed;
                this.NewEntreprise.Visibility = Visibility.Collapsed;
            }
            if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeEntreprisesControl", "Look"))
            {
                this.LookFournisseur.Visibility = Visibility.Collapsed;
                this.LookEntreprise.Visibility = Visibility.Collapsed;
            }

            if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeContactsControl", "Add"))
            {
                this.NewContact.Visibility = Visibility.Collapsed;
            }
            if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeContactsControl", "Look"))
            {
                this.LookContact.Visibility = Visibility.Collapsed;
            }

            if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl", "Add"))
            {
                this.NewDocTechnique.Visibility = Visibility.Collapsed;
            }
            if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbo.Windows.ParametresUserControls.ParametreTypeCommandeControl", "Look"))
            {
                this.LookDocTechnique.Visibility = Visibility.Collapsed;
            }

            if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.UserControls.ListeBonLivraisonControl", "Add"))
            {
                this._buttonAjouterBonLivraison.IsEnabled = false;
            }
            if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.UserControls.ListeBonLivraisonControl", "Update"))
            {
                this._buttonModifierBonLivraison.IsEnabled = false;
            }
            if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.UserControls.ListeBonLivraisonControl", "Delete"))
            {
                this._buttonSupprimerBonLivraison.IsEnabled = false;
            }
        }

        #endregion

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;

            if (((Commande_Fournisseur)this.DataContext).Entreprise1 != null)
            {
                listContactReception = new ObservableCollection<Personne>((((Commande_Fournisseur)this.DataContext).Entreprise1).Personne.Where(per => per.Contact != null));
            }

            if (this._datePickerDateCommande.SelectedDate == null)
            {
                this._datePickerDateCommande.SelectedDate = DateTime.Today;
            }

            if (((Commande_Fournisseur)this.DataContext).Remise_Somme != null)
            {
                if (((Commande_Fournisseur)this.DataContext).Remise_Somme != 0)
                {
                    this._checkBoxRemiseSomme.IsChecked = true;
                }
                else
                {
                    this._checkBoxRemiseSomme.IsChecked = false;
                }
            }
            else
            {
                this._checkBoxRemiseSomme.IsChecked = false;
            }

            if (((Commande_Fournisseur)this.DataContext).Remise != null)
            {
                if (((Commande_Fournisseur)this.DataContext).Remise != 0)
                {
                    this._checkBoxRemise.IsChecked = true;
                }
                else
                {
                    this._checkBoxRemise.IsChecked = false;
                }
            }
            else
            {
                this._checkBoxRemise.IsChecked = false;
            }

            this.calculLoaded();

            this.VerrouillerContenu();

            if (((Commande_Fournisseur)this.DataContext).Contact2 != null)
            {
                this._checkBoxContact.IsChecked = true;
                this._comboBoxContactLivraison.IsEnabled = true;
                this._comboBoxMonteur.IsEnabled = false;
            }
            else
            {
                this._checkBoxContact.IsChecked = false;
                this._comboBoxContactLivraison.IsEnabled = false;
                this._comboBoxMonteur.IsEnabled = true;
            }

            if (((Commande_Fournisseur)this.DataContext).Entreprise1 != null)
            {
                listContactReception = new ObservableCollection<Personne>((((Commande_Fournisseur)this.DataContext).Entreprise1).Personne);
            }

            this.generationNumeroCommande();

            try
            {
                this._comboBoxAffaire.SelectedItem = ((Commande_Fournisseur)this.DataContext).Affaire1;
            }
            catch (Exception) { }

            if (((App)App.Current)._connectedUser.Nom_Utilisateur == "mmassicot" || ((App)App.Current)._connectedUser.Nom_Utilisateur == "jlesnault" || ((App)App.Current)._connectedUser.Nom_Utilisateur == "dbrielles" || ((App)App.Current)._connectedUser.Nom_Utilisateur == "cbrochard" || ((App)App.Current)._connectedUser.Nom_Utilisateur == "scochet" || ((App)App.Current)._connectedUser.Nom_Utilisateur == "avallee" || ((App)App.Current)._connectedUser.Nom_Utilisateur == "aquinton")
            {
                this._textBoxNumeroCommande.IsReadOnly = false;
            }

            if (((Commande_Fournisseur)this.DataContext).ISEntrepriseEnlevement == true)
            {
                this._tabItemEntrepriseEnlevement.Visibility = Visibility.Visible;
            }
            else
            {
                this._tabItemEntrepriseEnlevement.Visibility = Visibility.Collapsed;
            }

            this.VerrouillerContenu();
            this.chargement = false;
        }

        #endregion

        #region Boutons

        #region Boutons OK et Annuler

        private void _buttonOk_Click(object sender, RoutedEventArgs e)
        {
            //Validation si cellule en édition
            try
            {
                this._dataGridContenuCommande.CommitEdit();
            }
            catch (Exception)
            {

            }
            //Recalcul du contenu
            this._DatagridContenuCommandeCalcul();

            //Assurance des chiffres bien insérés
            double val;
            if (double.TryParse(this._textBoxTotalCommande.Text, out val))
            {
                ((Commande_Fournisseur)this.DataContext).Total_Commande = double.Parse(this._textBoxTotalCommande.Text);
            }
            if (double.TryParse(this._textBoxTotalRameneA.Text, out val) && double.TryParse(this._textBoxTauxRemise.Text, out val))
            {
                ((Commande_Fournisseur)this.DataContext).Total_Ramene_A = double.Parse(this._textBoxTotalRameneA.Text);
                ((Commande_Fournisseur)this.DataContext).Remise = double.Parse(this._textBoxTauxRemise.Text);
            }

            if (this.VerificationChamps())
            {
                if (this._comboBoxDonneurOrdre.SelectedItem != null)
                {
                    Salarie toTest = (Salarie)this._comboBoxDonneurOrdre.SelectedItem;
                    if (toTest.SeuilCommande == 0)
                    {
                        this.DialogResult = true;
                        try
                        {
                            this.shopCommandeWindow.Close();
                        }
                        catch (Exception) { }
                        this.Close();
                    }
                    else
                    {
                        if (((Commande_Fournisseur)this.DataContext).MontantCommande != null)
                        {
                            if (((Commande_Fournisseur)this.DataContext).MontantCommande <= toTest.SeuilCommande)
                            {
                                this.DialogResult = true;
                                try
                                {
                                    this.shopCommandeWindow.Close();
                                }
                                catch (Exception) { }
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Vous ne pouvez valider votre commande car vos commandes ne peuvent dépasser les " + toTest.SeuilCommande + ". Veuillez modifier votre commande ou demander à votre supérieur des droits supplémentaires.", "Impossible de valider", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            }
                        }
                        else
                        {
                            this.DialogResult = true;
                            try
                            {
                                this.shopCommandeWindow.Close();
                            }
                            catch (Exception) { }
                            this.Close();
                        }
                    }
                }
            }

        }

        private void _buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            try
            {
                this.shopCommandeWindow.Close();
            }
            catch (Exception) { }
            this.Close();
        }

        #endregion

        #region Boutons BL

        private void _buttonAjouterBonLivraison_Click(object sender, RoutedEventArgs e)
        {
            BonLivraisonWindow bonLivraisonWindow = new BonLivraisonWindow();
            bonLivraisonWindow.DataContext = new Bon_Livraison();
            ((Bon_Livraison)bonLivraisonWindow.DataContext).Commande_Fournisseur1 = (Commande_Fournisseur)this.DataContext;
            ((Bon_Livraison)bonLivraisonWindow.DataContext).StockAtelier = ((Commande_Fournisseur)this.DataContext).Stock;
            ((Bon_Livraison)bonLivraisonWindow.DataContext).Divers = ((Commande_Fournisseur)this.DataContext).Divers;
            ((Bon_Livraison)bonLivraisonWindow.DataContext).Affaire1 = ((Commande_Fournisseur)this.DataContext).Affaire1;
            ((Bon_Livraison)bonLivraisonWindow.DataContext).Salarie1 = ((Commande_Fournisseur)this.DataContext).Salarie;
            ((Bon_Livraison)bonLivraisonWindow.DataContext).Fournisseur1 = ((Commande_Fournisseur)this.DataContext).Fournisseur1;
            bonLivraisonWindow._comboBoxFournisseur.IsEnabled = false;

            bool? dialogResult = bonLivraisonWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {

            }
            else
            {
                // On enlève tous les contenu de commandes associés
                foreach (Bon_Livraison_Contenu_Commande blcc in ((Bon_Livraison)bonLivraisonWindow.DataContext).Bon_Livraison_Contenu_Commande)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(blcc);
                        ((Bon_Livraison)bonLivraisonWindow.DataContext).Bon_Livraison_Contenu_Commande.Remove(blcc);

                    }
                    catch (Exception)
                    {
                        ((Bon_Livraison)bonLivraisonWindow.DataContext).Bon_Livraison_Contenu_Commande.Remove(blcc);
                        ((App)App.Current).mySitaffEntities.Detach(blcc);
                    }

                }
                //On enlève tous les contenu supp
                foreach (Bon_Livraison_Contenu_Commande_Supplementaire blcc in ((Bon_Livraison)bonLivraisonWindow.DataContext).Bon_Livraison_Contenu_Commande_Supplementaire)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(blcc);
                        ((Bon_Livraison)bonLivraisonWindow.DataContext).Bon_Livraison_Contenu_Commande_Supplementaire.Remove(blcc);

                    }
                    catch (Exception)
                    {
                        ((Bon_Livraison)bonLivraisonWindow.DataContext).Bon_Livraison_Contenu_Commande_Supplementaire.Remove(blcc);
                        ((App)App.Current).mySitaffEntities.Detach(blcc);
                    }

                }
                //On détache le BL
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Bon_Livraison)bonLivraisonWindow.DataContext);
                    ((Commande_Fournisseur)this.DataContext).Bon_Livraison.Remove((Bon_Livraison)bonLivraisonWindow.DataContext);
                }
                catch (Exception)
                {
                    ((Commande_Fournisseur)this.DataContext).Bon_Livraison.Remove((Bon_Livraison)bonLivraisonWindow.DataContext);
                    ((App)App.Current).mySitaffEntities.Detach((Bon_Livraison)bonLivraisonWindow.DataContext);
                }

            }
            this.VerrouillerContenu();
        }


        private void _buttonModifierBonLivraison_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridBonLivraison.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner un bon de livraison à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridBonLivraison.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'un bon de livraison à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridBonLivraison.SelectedItem != null)
            {
                if (((Bon_Livraison)this._dataGridBonLivraison.SelectedItem).Facture_Fournisseur1 == null)
                {
                    BonLivraisonWindow bonLivraisonWindow = new BonLivraisonWindow();
                    bonLivraisonWindow.DataContext = (Bon_Livraison)this._dataGridBonLivraison.SelectedItem;
                    ((Bon_Livraison)bonLivraisonWindow.DataContext).Fournisseur1 = ((Commande_Fournisseur)this.DataContext).Fournisseur1;

                    bool? dialogResult = bonLivraisonWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        this._dataGridBonLivraison.Items.Refresh();
                    }
                    else
                    {
                        try
                        {
                            ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Bon_Livraison)bonLivraisonWindow.DataContext);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne pouvez modifier ce bon de livraison car il est associé à une facture.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            this.VerrouillerContenu();
        }


        private void _buttonSupprimerBonLivraison_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridBonLivraison.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un bon de livraison à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridBonLivraison.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les bons de livraisons à supprimer un par un.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (((Bon_Livraison)this._dataGridBonLivraison.SelectedItem).Facture_Fournisseur1 == null)
                    {
                        // On enlève tous les contenu de commandes associés
                        foreach (Bon_Livraison_Contenu_Commande blcc in ((Bon_Livraison)this._dataGridBonLivraison.SelectedItem).Bon_Livraison_Contenu_Commande)
                        {
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Detach(blcc);
                                ((Bon_Livraison)this._dataGridBonLivraison.SelectedItem).Bon_Livraison_Contenu_Commande.Remove(blcc);

                            }
                            catch (Exception)
                            {
                                ((Bon_Livraison)this._dataGridBonLivraison.SelectedItem).Bon_Livraison_Contenu_Commande.Remove(blcc);
                                ((App)App.Current).mySitaffEntities.Detach(blcc);
                            }

                        }
                        //On enlève tous les contenu supp
                        foreach (Bon_Livraison_Contenu_Commande_Supplementaire blcc in ((Bon_Livraison)this._dataGridBonLivraison.SelectedItem).Bon_Livraison_Contenu_Commande_Supplementaire)
                        {
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Detach(blcc);
                                ((Bon_Livraison)this._dataGridBonLivraison.SelectedItem).Bon_Livraison_Contenu_Commande_Supplementaire.Remove(blcc);

                            }
                            catch (Exception)
                            {
                                ((Bon_Livraison)this._dataGridBonLivraison.SelectedItem).Bon_Livraison_Contenu_Commande_Supplementaire.Remove(blcc);
                                ((App)App.Current).mySitaffEntities.Detach(blcc);
                            }

                        }
                        //On détache le BL
                        try
                        {
                            ((App)App.Current).mySitaffEntities.Detach((Bon_Livraison)this._dataGridBonLivraison.SelectedItem);
                            ((Commande_Fournisseur)this.DataContext).Bon_Livraison.Remove((Bon_Livraison)this._dataGridBonLivraison.SelectedItem);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                ((Commande_Fournisseur)this.DataContext).Bon_Livraison.Remove((Bon_Livraison)this._dataGridBonLivraison.SelectedItem);
                                ((App)App.Current).mySitaffEntities.Detach((Bon_Livraison)this._dataGridBonLivraison.SelectedItem);
                            }
                            catch (Exception) { }
                        }
                        this._dataGridBonLivraison.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Vous ne pouvez supprimer ce bon de livraison car il est associé à une facture.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }
            }
            this.VerrouillerContenu();
        }

        #endregion

        #region Boutons Conditions réglements

        private void _buttonGaucheDroite_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._listBoxCondReglementGauche.SelectedItem != null && this._listBoxCondReglementGauche.SelectedItems.Count == 1)
            {
                Commande_Fournisseur_Condition_Reglement temp = new Commande_Fournisseur_Condition_Reglement();
                temp.Condition_Reglement1 = (Condition_Reglement)this._listBoxCondReglementGauche.SelectedItem;

                ((Commande_Fournisseur)this.DataContext).Commande_Fournisseur_Condition_Reglement.Add(temp);
            }
            this.Verif_dataGridCommandeFournisseurConditionReglement();
        }

        private void _buttonDroiteGauche_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridCommandeFournisseurConditionReglement.SelectedItem != null && this._dataGridCommandeFournisseurConditionReglement.SelectedItems.Count == 1)
            {
                Commande_Fournisseur_Condition_Reglement itemToRemove = (Commande_Fournisseur_Condition_Reglement)this._dataGridCommandeFournisseurConditionReglement.SelectedItem;
                try
                {
                    itemToRemove.Commande_Fournisseur1 = null;
                    ((Commande_Fournisseur)this.DataContext).Commande_Fournisseur_Condition_Reglement.Remove(itemToRemove);
                    ((App)App.Current).mySitaffEntities.Commande_Fournisseur_Condition_Reglement.DeleteObject(itemToRemove);
                    //((App)App.Current).mySitaffEntities.Detach(itemToRemove);                    
                }
                catch (Exception)
                {
                    ((App)App.Current).mySitaffEntities.Detach(itemToRemove);
                    ((Commande_Fournisseur)this.DataContext).Commande_Fournisseur_Condition_Reglement.Remove(itemToRemove);
                }
            }
            this._dataGridCommandeFournisseurConditionReglement.Items.Refresh();
            this.Verif_dataGridCommandeFournisseurConditionReglement();
        }

        private void _importerConditions_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._comboBoxFournisseur.SelectedItem != null)
            {
                if (((Fournisseur)this._comboBoxFournisseur.SelectedItem).Fournisseur_Condition_Reglement.Count() != 0)
                {
                    foreach (Fournisseur_Condition_Reglement item in ((Fournisseur)this._comboBoxFournisseur.SelectedItem).Fournisseur_Condition_Reglement)
                    {
                        Commande_Fournisseur_Condition_Reglement temp = new Commande_Fournisseur_Condition_Reglement();
                        temp.Condition_Reglement1 = item.Condition_Reglement1;
                        temp.Commentaire = item.Commentaire;
                        temp.Pourcentage = item.Pourcentage;

                        ((Commande_Fournisseur)this.DataContext).Commande_Fournisseur_Condition_Reglement.Add(temp);
                    }
                }
                else
                {
                    MessageBox.Show("Aucune condition de réglement n'est enregistrée pour ce fournisseur.");
                }
            }
            else
            {
                MessageBox.Show("Aucune fournisseur sélectionné.");
            }
            this.Verif_dataGridCommandeFournisseurConditionReglement();
        }

        #endregion

        #region Boutons Contenu

        private void _buttonColler_Click(object sender, RoutedEventArgs e)
        {
            CopierColler ClassPaste = new CopierColler();
            ObservableCollection<Contenu_Commande_Fournisseur> listToAdd = ClassPaste.PasteDataCommandeWindow();
            if (listToAdd != null)
            {
                foreach (Contenu_Commande_Fournisseur ccf in listToAdd)
                {
                    //((Commande_Fournisseur)this.DataContext).Contenu_Commande_Fournisseur.Add(ccf);
                    ((App)App.Current).mySitaffEntities.AddToContenu_Commande_Fournisseur(ccf);
                    ccf.Commande_Fournisseur1 = (Commande_Fournisseur)this.DataContext;
                }
                this._dataGridContenuCommande.Items.Refresh();
                this._DatagridContenuCommandeCalcul();
            }
        }

        private void _buttonCalculer_Click(object sender, RoutedEventArgs e)
        {
            this._DatagridContenuCommandeCalcul();
        }

        private void _buttonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            this.deleteLigne();
        }

        private void _buttonShop_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                this.shopCommandeWindow.Close();
            }
            catch (Exception) { }
            this.shopCommandeWindow = new ShopCommandeWindow();
            this.shopCommandeWindow._ComboBoxFournisseur.SelectedItem = this._comboBoxFournisseur.SelectedItem;
            this.shopCommandeWindow.commandeWindow = this;
            this.shopCommandeWindow.Show();
        }

        #endregion

        #region boutons des combobox

        #region contact qui réceptionne

        private void NullContactLivraison_Click_1(object sender, RoutedEventArgs e)
        {
            this._comboBoxContactLivraison.SelectedItem = null;
        }

        #endregion

        #region Monteur

        private void NullMonteur_Click_1(object sender, RoutedEventArgs e)
        {
            this._comboBoxMonteur.SelectedItem = null;
        }

        #endregion

        #region fournisseurs

        private void NewFournisseur_Click(object sender, RoutedEventArgs e)
        {
            EntrepriseWindow entrepriseWindow = new EntrepriseWindow();
            Entreprise tmp = new Entreprise();
            tmp.Adresse1 = new Adresse();
            tmp.Client = new Client();
            tmp.Is_Client = false;
            tmp.Fournisseur = new Fournisseur();
            //tmp.Is_Fournisseur = true;
            entrepriseWindow.DataContext = tmp;
            entrepriseWindow.creation = true;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = entrepriseWindow.ShowDialog();
            Entreprise entreprise = (Entreprise)entrepriseWindow.DataContext;

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                if (entreprise.Fournisseur != null)
                {
                    this.listFournisseurs = new ObservableCollection<Fournisseur>(((App)App.Current).mySitaffEntities.Fournisseur.OrderBy(ent => ent.Entreprise.Libelle));
                    this.listFournisseurs.Add(entreprise.Fournisseur);
                    this._comboBoxFournisseur.SelectedItem = entreprise.Fournisseur;
                }
                else
                {
                    MessageBox.Show("L'entreprise que vous avez ajouté n'a pas été définie en tant que 'fournisseur', vous ne pourrez donc pas la sélectionner", "Entreprise non fournisseur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                //Entreprise non validée (on détache tout !)
                // On enlève tous les Commande_Fournisseur associés
                foreach (Commande_Fournisseur item in entreprise.Commande_Fournisseur1)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Commande_Fournisseur1.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Commande_Fournisseur1.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Commande_Fournisseur associés
                foreach (Commande_Fournisseur item in entreprise.Commande_Fournisseur2)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Commande_Fournisseur1.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Commande_Fournisseur1.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Entreprise_Activite associés
                foreach (Entreprise_Activite item in entreprise.Entreprise_Activite)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Entreprise_Activite.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Entreprise_Activite.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Entreprise_Litige associés
                foreach (Entreprise_Litige item in entreprise.Entreprise_Litige)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Entreprise_Litige.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Entreprise_Litige.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Entreprise_Mere associés
                foreach (Entreprise_Mere item in entreprise.Entreprise_Mere)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Entreprise_Mere.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Entreprise_Mere.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Numero_Tva_Intraco associés
                foreach (Numero_Tva_Intraco item in entreprise.Numero_Tva_Intraco)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Numero_Tva_Intraco.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Numero_Tva_Intraco.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les NumeroTvaIntracommunautaire associés
                foreach (NumeroTvaIntracommunautaire item in entreprise.NumeroTvaIntracommunautaire)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.NumeroTvaIntracommunautaire.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.NumeroTvaIntracommunautaire.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Personne associés
                foreach (Personne item in entreprise.Personne)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Personne.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Personne.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(entreprise.Fournisseur);
                    entreprise.Fournisseur = null;

                }
                catch (Exception)
                {
                    try
                    {
                        entreprise.Fournisseur = null;
                        ((App)App.Current).mySitaffEntities.Detach(entreprise.Fournisseur);
                    }
                    catch (Exception)
                    {

                    }
                }
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(entreprise.Client);
                    entreprise.Client = null;

                }
                catch (Exception)
                {
                    try
                    {
                        entreprise.Client = null;
                        ((App)App.Current).mySitaffEntities.Detach(entreprise.Client);
                    }
                    catch (Exception)
                    {

                    }
                }
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(entreprise);
                }
                catch (Exception)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Entreprise.DeleteObject(entreprise);
                    }
                    catch (Exception)
                    {

                    }
                }

            }
        }

        private void LookFournisseur_Click(object sender, RoutedEventArgs e)
        {
            if (this._comboBoxFournisseur.SelectedItem != null)
            {
                ListeEntreprisesControl listeEntrepriseControl = new ListeEntreprisesControl();
                listeEntrepriseControl.Look(((Fournisseur)this._comboBoxFournisseur.SelectedItem).Entreprise);
            }
        }

        #endregion

        #region contacts

        private void NullContact_Click_1(object sender, RoutedEventArgs e)
        {
            this._comboBoxContact.SelectedItem = null;
        }

        private void NewContact_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow contactWindow = new ContactWindow();
            Personne tmp = new Personne();
            if (this._comboBoxFournisseur.SelectedItem != null)
            {
                tmp.Entreprise1 = ((Fournisseur)this._comboBoxFournisseur.SelectedItem).Entreprise;
            }
            tmp.Contact = new Contact();
            contactWindow.DataContext = tmp;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = contactWindow.ShowDialog();
            Personne personne = (Personne)contactWindow.DataContext;

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                this.listContacts.Clear();
                if (this._comboBoxFournisseur.SelectedItem != null)
                {
                    foreach (Personne pers in ((Fournisseur)this._comboBoxFournisseur.SelectedItem).Entreprise.Personne.Where(per => per.Contact != null))
                    {
                        this.listContacts.Add(pers.Contact);
                    }
                }
                this._comboBoxContact.SelectedItem = personne.Contact;
                if (this._comboBoxEntLivraison.SelectedItem != null)
                {
                    Entreprise E = (Entreprise)this._comboBoxEntLivraison.SelectedItem;
                    this._comboBoxContact.SelectedItem = personne.Contact;
                    ObservableCollection<Contact> temp = new ObservableCollection<Contact>();
                    foreach (Personne p in E.Personne)
                    {
                        if (p.Contact != null)
                        {
                            temp.Add(p.Contact);
                        }
                    }
                    this._comboBoxContactLivraison.ItemsSource = temp;
                }
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(personne.Contact);
                    ((App)App.Current).mySitaffEntities.Detach(personne);
                }
                catch (Exception)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Contact.DeleteObject(personne.Contact);
                        ((App)App.Current).mySitaffEntities.Personne.DeleteObject(personne);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void LookContact_Click(object sender, RoutedEventArgs e)
        {
            if (this._comboBoxContact.SelectedItem != null)
            {
                ListeContactsControl listeContactsControl = new ListeContactsControl();
                listeContactsControl.Look(((Contact)this._comboBoxContact.SelectedItem).Personne);
            }
        }

        #endregion

        #region Doc Technique

        private void NewDocTechnique_Click(object sender, RoutedEventArgs e)
        {
            TypeCommandeWindow typecommandewindow = new TypeCommandeWindow();
            typecommandewindow.DataContext = new Type_Commande();
            //booléen nullable vrai ou faux ou null

            bool? dialogResult = typecommandewindow.ShowDialog();
            Type_Commande type_commande = (Type_Commande)typecommandewindow.DataContext;

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                this.listTypeCommande = new ObservableCollection<Type_Commande>(((App)App.Current).mySitaffEntities.Type_Commande.OrderBy(civ => civ.Libelle));
                this.listTypeCommande.Add(type_commande);
                this.listTypeCommande = new ObservableCollection<Type_Commande>(this.listTypeCommande.OrderBy(tc => tc.Libelle));

                this._comboBoxTypeCommande.SelectedItem = type_commande;
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(type_commande);
                }
                catch (Exception)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Type_Commande.DeleteObject(type_commande);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void LookDocTechnique_Click(object sender, RoutedEventArgs e)
        {
            if (this._comboBoxTypeCommande.SelectedItem != null)
            {
                ParametreTypeCommandeControl parametreTypeCommandeControl = new ParametreTypeCommandeControl();
                if (this._comboBoxAffaire.SelectedItem != null)
                {
                    if (((Affaire)this._comboBoxAffaire.SelectedItem).Affaire_Type_Commande.Where(aff => aff.Type_Commande1.Identifiant == ((Type_Commande)this._comboBoxTypeCommande.SelectedItem).Identifiant).Count() != 0)
                    {
                        Affaire_Type_Commande toWatch = ((Affaire)this._comboBoxAffaire.SelectedItem).Affaire_Type_Commande.Where(aff => aff.Type_Commande1.Identifiant == ((Type_Commande)this._comboBoxTypeCommande.SelectedItem).Identifiant).First();
                        TypeDocTechniqueWindow typedocTechniqueWindow = new TypeDocTechniqueWindow();
                        if (toWatch != null)
                        {
                            typedocTechniqueWindow.DataContext = toWatch;
                        }
                        else
                        {
                            typedocTechniqueWindow.DataContext = (Type_Commande)this._comboBoxTypeCommande.SelectedItem;
                        }
                        typedocTechniqueWindow.LectureSeule();

                        bool? dialogResult = typedocTechniqueWindow.ShowDialog();
                    }
                    else
                    {
                        parametreTypeCommandeControl.Look((Type_Commande)this._comboBoxTypeCommande.SelectedItem);
                    }
                }
                else
                {
                    parametreTypeCommandeControl.Look((Type_Commande)this._comboBoxTypeCommande.SelectedItem);
                }
            }
        }

        #endregion

        #region Entreprise

        private void NewEntreprise_Click(object sender, RoutedEventArgs e)
        {
            EntrepriseWindow entrepriseWindow = new EntrepriseWindow();
            Entreprise tmp = new Entreprise();
            tmp.Adresse1 = new Adresse();
            tmp.Adresse2 = new Adresse();
            tmp.Client = new Client();
            tmp.Is_Client = false;
            tmp.Fournisseur = new Fournisseur();
            entrepriseWindow.DataContext = tmp;
            entrepriseWindow.creation = true;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = entrepriseWindow.ShowDialog();
            Entreprise entreprise = (Entreprise)entrepriseWindow.DataContext;

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                ((App)App.Current).mySitaffEntities.Attach(entreprise);
                if (_checkBoxEntAtelier.IsChecked == true)
                {
                    this.miseAutoEntrepriseLivraison();
                }
                if (_checkBoxEntAutre.IsChecked == true)
                {
                    this.TouteEntreprise();
                }
                if (_checkBoxEntChantier.IsChecked == true)
                {
                    this.TriEntreprise();
                }
                this._comboBoxEntLivraison.SelectedItem = entreprise;
            }
            else
            {
                //Entreprise non validée (on détache tout !)
                // On enlève tous les Commande_Fournisseur associés
                foreach (Commande_Fournisseur item in entreprise.Commande_Fournisseur1)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Commande_Fournisseur1.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Commande_Fournisseur1.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Commande_Fournisseur associés
                foreach (Commande_Fournisseur item in entreprise.Commande_Fournisseur2)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Commande_Fournisseur1.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Commande_Fournisseur1.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Entreprise_Activite associés
                foreach (Entreprise_Activite item in entreprise.Entreprise_Activite)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Entreprise_Activite.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Entreprise_Activite.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Entreprise_Litige associés
                foreach (Entreprise_Litige item in entreprise.Entreprise_Litige)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Entreprise_Litige.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Entreprise_Litige.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Entreprise_Mere associés
                foreach (Entreprise_Mere item in entreprise.Entreprise_Mere)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Entreprise_Mere.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Entreprise_Mere.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Numero_Tva_Intraco associés
                foreach (Numero_Tva_Intraco item in entreprise.Numero_Tva_Intraco)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Numero_Tva_Intraco.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Numero_Tva_Intraco.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les NumeroTvaIntracommunautaire associés
                foreach (NumeroTvaIntracommunautaire item in entreprise.NumeroTvaIntracommunautaire)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.NumeroTvaIntracommunautaire.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.NumeroTvaIntracommunautaire.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Personne associés
                foreach (Personne item in entreprise.Personne)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Personne.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Personne.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(entreprise.Fournisseur);
                    entreprise.Fournisseur = null;

                }
                catch (Exception)
                {
                    try
                    {
                        entreprise.Fournisseur = null;
                        ((App)App.Current).mySitaffEntities.Detach(entreprise.Fournisseur);
                    }
                    catch (Exception)
                    {

                    }
                }
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(entreprise.Client);
                    entreprise.Client = null;

                }
                catch (Exception)
                {
                    try
                    {
                        entreprise.Client = null;
                        ((App)App.Current).mySitaffEntities.Detach(entreprise.Client);
                    }
                    catch (Exception)
                    {

                    }
                }
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(entreprise);
                }
                catch (Exception)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Entreprise.DeleteObject(entreprise);
                    }
                    catch (Exception)
                    {

                    }
                }
            }


            ListeEntreprisesControl listeEntrepriseControl = new ListeEntreprisesControl();
            if (entreprise != null)
            {
                //this.listFournisseurs = new ObservableCollection<Fournisseur>(((App)App.Current).mySitaffEntities.Fournisseur.OrderBy(ent => ent.Entreprise.Libelle));
            }
            else
            {
                this._comboBoxFournisseur.SelectedItem = null;
            }
        }

        private void LookEntreprise_Click(object sender, RoutedEventArgs e)
        {
            if (this._comboBoxFournisseur.SelectedItem != null)
            {
                ListeEntreprisesControl listeEntrepriseControl = new ListeEntreprisesControl();
                listeEntrepriseControl.Look((Entreprise)this._comboBoxEntLivraison.SelectedItem);
            }
        }

        #endregion

        #endregion

        #region autre

        private void _buttonMasquerHaut_Click(object sender, RoutedEventArgs e)
        {
            if (this._grid1ToHide.Height != 0 && this._grid1ToHide.Height != 0)
            {
                this._grid1ToHide.Height = 0;
                this._grid2ToHide.Height = 0;
                this._buttonMasquerHaut.Content = "Afficher le haut";
            }
            else
            {
                this._grid1ToHide.Height = double.NaN;
                this._grid2ToHide.Height = double.NaN;
                this._buttonMasquerHaut.Content = "Masquer le haut";
            }
        }

        #endregion

        #region null

        #endregion

        #endregion

        #region Verifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!this.Verif_Commande())
            {
                verif = false;
            }

            //Si verif = faux, la verif du haut de la commande contient une erreur
            ((App)App.Current).verifications.MettreBoutonEnCouleur(this._buttonMasquerHaut, verif);

            if (!this.Verif_Tab_Entreprise_Enlevement())
            {
                verif = false;
            }
            if (!this.Verif_Tab_Condition_Reglement())
            {
                verif = false;
            }

            return verif;
        }

        #region Commande

        private bool Verif_Commande()
        {
            bool verif = true;

            if (!this.Verif_textBoxNumeroCommande())
            {
                verif = false;
            }
            if (!this.Verif_datePickerDateCommande())
            {
                verif = false;
            }
            if (!this.Verif_datePickerDateLivraisonPrevu())
            {
                verif = false;
            }
            if (!this.Verif_textBoxTotalCommande())
            {
                verif = false;
            }
            if (!this.Verif_textBoxTotalRameneA())
            {
                verif = false;
            }
            if (!this.Verif_textBoxTauxRemise())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxFournisseur())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxAffaire())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxMonteur())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxLivraisonContact())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxDonneurOrdre())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxContact())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxEntLivraison())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxEntFacturation())
            {
                verif = false;
            }
            if (!this.Verif_textBoxNature())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxTypeCommande())
            {
                verif = false;
            }
            if (!this.Verif_textBoxCommentaireLivraison())
            {
                verif = false;
            }
            if (!this.Verif_textBoxCommentaireGeneral())
            {
                verif = false;
            }

            return verif;
        }

        #region Numero de la commande

        private bool Verif_textBoxNumeroCommande()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxNumeroCommande, this._textBlockNumeroCommande);
        }

        private void _textBoxNumeroCommande_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_textBoxNumeroCommande();
        }

        #endregion

        #region Commentaire sur date

        private bool Verif_textBoxCommentaireLivraison()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxCommentaireLivraison, this._textBlockCommentaireDateLiv1);
        }

        private void _textBoxCommentaireSurDate_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Verif_textBoxCommentaireGeneral();
        }

        #endregion

        #region Commentaire général

        private bool Verif_textBoxCommentaireGeneral()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxCommentaireGeneral, this._textBlockCommentaireGeneral);
        }

        private void _textBoxCommentaireGeneral_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Verif_textBoxCommentaireGeneral();
        }

        #endregion

        #region Fournisseur

        private bool Verif_comboBoxFournisseur()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxFournisseur, this._textBlockFournisseur);
        }

        private void _comboBoxFournisseur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxFournisseur();
            this.listContacts.Clear();
            if (this._comboBoxFournisseur.SelectedItem != null && !this._comboBoxFournisseur.SelectedItem.Equals(null))
            {
                foreach (Personne pers in ((Fournisseur)this._comboBoxFournisseur.SelectedItem).Entreprise.Personne.Where(per => per.Contact != null))
                {
                    this.listContacts.Add(pers.Contact);
                }
            }
        }

        #endregion

        #region Date de la commande

        private bool Verif_datePickerDateCommande()
        {
            return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateCommande, this._textBlockDateCommande);
        }

        private void _datePickerDateCommande_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_datePickerDateCommande();
            this.Verif_datePickerDateLivraisonPrevu();
        }

        #endregion

        #region Date de la livraion prévu

        private bool Verif_datePickerDateLivraisonPrevu()
        {
            return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateLivraisonPrevu, this._textBlockDateLivraisonPrevu, this._datePickerDateCommande);
        }

        private void _datePickerDateLivraisonPrevu_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_datePickerDateCommande();
            this.Verif_datePickerDateLivraisonPrevu();
        }

        #endregion

        #region Affaire

        private bool Verif_comboBoxAffaire()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxAffaire, this._textBlockAffaire);
        }

        private void _comboBoxAffaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxAffaire();
            if (this._comboBoxAffaire.SelectedItem != null)
            {
                if (((Commande_Fournisseur)this.DataContext).Numero == null)
                {
                    if (this._checkBoxEntChantier.IsChecked == true)
                    {
                        this.TriEntreprise();
                    }
                    if (this._checkBoxEntAutre.IsChecked == true)
                    {
                        this.TouteEntreprise();
                    }
                    if (this._checkBoxEntAtelier.IsChecked == true)
                    {
                        this.miseAutoEntrepriseLivraison();
                    }
                    //Recréation du numéro
                    String numTemp = "";
                    if (((Commande_Fournisseur)this.DataContext).Affaire1 != null && ((Commande_Fournisseur)this.DataContext).Affaire1.Numero != "")
                    {
                        numTemp = ((Commande_Fournisseur)this.DataContext).Affaire1.Numero + "-";
                    }
                    else if (((Commande_Fournisseur)this.DataContext).Stock == true)
                    {
                        numTemp = "Stock" + "-";
                    }
                    else if (((Commande_Fournisseur)this.DataContext).Divers == true)
                    {
                        numTemp = "Divers" + "-";
                    }
                    else
                    {
                        numTemp = "Autre-Erreur" + "-";
                    }
                    String year = DateTime.Today.Year.ToString();
                    String mois = DateTime.Today.Month.ToString();
                    int i = 1;
                    foreach (char c in year)
                    {
                        if (i == 3 || i == 4)
                        {
                            numTemp = numTemp + c;
                        }
                        i = i + 1;
                    }
                    int nbCaracteres = 0;
                    foreach (Char c in mois)
                    {
                        nbCaracteres++;
                    }
                    if (nbCaracteres == 1)
                    {
                        numTemp += "0";
                    }
                    numTemp = numTemp + mois + "-";

                    String incrementToPut = "001";

                    ObservableCollection<Commande_Fournisseur> toTest = new ObservableCollection<Commande_Fournisseur>(((App)App.Current).mySitaffEntities.Commande_Fournisseur.Where(com => com.Numero.Contains(numTemp)));
                    if (toTest.Count() == 0)
                    {

                    }
                    else
                    {
                        ObservableCollection<int> lesEntiersPourIncr = new ObservableCollection<int>();
                        int PlusGrand = 0;
                        foreach (Commande_Fournisseur item in toTest)
                        {
                            int test;
                            if (int.TryParse(item.Numero.Replace(numTemp, ""), out test))
                            {
                                lesEntiersPourIncr.Add(int.Parse(item.Numero.Replace(numTemp, "")));
                            }
                        }
                        foreach (int entier in lesEntiersPourIncr)
                        {
                            if (entier > PlusGrand)
                            {
                                PlusGrand = entier;
                            }
                        }
                        PlusGrand = PlusGrand + 1;
                        incrementToPut = PlusGrand.ToString();
                        String tempIncrement = "";
                        nbCaracteres = 0;
                        foreach (Char c in incrementToPut)
                        {
                            nbCaracteres++;
                        }
                        if (nbCaracteres == 1)
                        {
                            tempIncrement += "00";
                        }
                        if (nbCaracteres == 2)
                        {
                            tempIncrement += "0";
                        }
                        tempIncrement = tempIncrement + incrementToPut;
                        incrementToPut = tempIncrement;
                    }
                    numTemp = numTemp + incrementToPut;

                    ((Commande_Fournisseur)this.DataContext).Numero = numTemp;
                    this._textBoxNumeroCommande.Text = numTemp;
                }
                else if (!((Commande_Fournisseur)this.DataContext).Numero.Contains(((Affaire)this._comboBoxAffaire.SelectedItem).Numero))
                {
                    if (this._checkBoxEntChantier.IsChecked == true)
                    {
                        this.TriEntreprise();
                    }
                    if (this._checkBoxEntAutre.IsChecked == true)
                    {
                        this.TouteEntreprise();
                    }
                    if (this._checkBoxEntAtelier.IsChecked == true)
                    {
                        this.miseAutoEntrepriseLivraison();
                    }
                    //Recréation du numéro
                    String numTemp = "";
                    if (((Commande_Fournisseur)this.DataContext).Affaire1 != null && ((Commande_Fournisseur)this.DataContext).Affaire1.Numero != "")
                    {
                        numTemp = ((Commande_Fournisseur)this.DataContext).Affaire1.Numero + "-";
                    }
                    else if (((Commande_Fournisseur)this.DataContext).Stock == true)
                    {
                        numTemp = "Stock" + "-";
                    }
                    else if (((Commande_Fournisseur)this.DataContext).Divers == true)
                    {
                        numTemp = "Divers" + "-";
                    }
                    else
                    {
                        numTemp = "Autre-Erreur" + "-";
                    }
                    String year = DateTime.Today.Year.ToString();
                    String mois = DateTime.Today.Month.ToString();
                    int i = 1;
                    foreach (char c in year)
                    {
                        if (i == 3 || i == 4)
                        {
                            numTemp = numTemp + c;
                        }
                        i = i + 1;
                    }
                    int nbCaracteres = 0;
                    foreach (Char c in mois)
                    {
                        nbCaracteres++;
                    }
                    if (nbCaracteres == 1)
                    {
                        numTemp += "0";
                    }
                    numTemp = numTemp + mois + "-";

                    String incrementToPut = "001";

                    ObservableCollection<Commande_Fournisseur> toTest = new ObservableCollection<Commande_Fournisseur>(((App)App.Current).mySitaffEntities.Commande_Fournisseur.Where(com => com.Numero.Contains(numTemp)));
                    if (toTest.Count() == 0)
                    {

                    }
                    else
                    {
                        ObservableCollection<int> lesEntiersPourIncr = new ObservableCollection<int>();
                        int PlusGrand = 0;
                        foreach (Commande_Fournisseur item in toTest)
                        {
                            int test;
                            if (int.TryParse(item.Numero.Replace(numTemp, ""), out test))
                            {
                                lesEntiersPourIncr.Add(int.Parse(item.Numero.Replace(numTemp, "")));
                            }
                        }
                        foreach (int entier in lesEntiersPourIncr)
                        {
                            if (entier > PlusGrand)
                            {
                                PlusGrand = entier;
                            }
                        }
                        PlusGrand = PlusGrand + 1;
                        incrementToPut = PlusGrand.ToString();
                        String tempIncrement = "";
                        nbCaracteres = 0;
                        foreach (Char c in incrementToPut)
                        {
                            nbCaracteres++;
                        }
                        if (nbCaracteres == 1)
                        {
                            tempIncrement += "00";
                        }
                        if (nbCaracteres == 2)
                        {
                            tempIncrement += "0";
                        }
                        tempIncrement = tempIncrement + incrementToPut;
                        incrementToPut = tempIncrement;
                    }
                    numTemp = numTemp + incrementToPut;

                    ((Commande_Fournisseur)this.DataContext).Numero = numTemp;
                    this._textBoxNumeroCommande.Text = numTemp;
                }
            }
            this.miseAutoEntrepriseFacturation();
            this.miseAutoEntrepriseLivraison();
        }

        #endregion

        #region Monteur

        private bool Verif_comboBoxMonteur()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxMonteur, this._textBlockMonteur);
        }

        private void _comboBoxMonteur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxMonteur();
        }

        #endregion

        #region LivraisonContact

        private bool Verif_comboBoxLivraisonContact()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxContactLivraison, this._textBlockContactLivraison);
        }

        private void _comboBoxContactLivraison_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxLivraisonContact();
        }

        #endregion

        #region Donneur d'ordres

        private bool Verif_comboBoxDonneurOrdre()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxDonneurOrdre, this._textBlockDonneurOrdre);
        }

        private void _comboBoxDonneurOrdre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxDonneurOrdre();
        }

        #endregion

        #region Contact

        private bool Verif_comboBoxContact()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxContact, this._textBlockContact);
        }

        private void _comboBoxContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxContact();
        }

        #endregion

        #region Entreprise de livraison

        private bool Verif_comboBoxEntLivraison()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxEntLivraison, this._textBlockEntLivraison);
        }

        private void _comboBoxEntLivraison_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxEntLivraison();
            if (this._comboBoxEntLivraison.SelectedItem != null)
            {
                Entreprise E = (Entreprise)this._comboBoxEntLivraison.SelectedItem;
                this._textBoxAddEntLivraison.Text = E.Adresse1.Rue + " " + E.Adresse1.Complement_Adresse;
                this._textBoxCpEntLivraison.Text = E.Adresse1.Ville1.Code_Postal;
                this._comboBoxVilleEntLivraison.Text = E.Adresse1.Ville1.Libelle;
                this._comboBoxPaysEntLivraison.Text = E.Adresse1.Ville1.Pays1.Libelle;
                ObservableCollection<Contact> temp = new ObservableCollection<Contact>();
                foreach (Personne p in E.Personne)
                {
                    if (p.Contact != null)
                    {
                        temp.Add(p.Contact);
                    }
                }
                this._comboBoxContactLivraison.ItemsSource = temp;
            }
            else
            {
                this._textBoxAddEntLivraison.Text = "";
                this._textBoxCpEntLivraison.Text = "";
                this._comboBoxVilleEntLivraison.Text = "";
                this._comboBoxPaysEntLivraison.Text = "";
            }
        }

        #endregion

        #region Entreprise de facturation

        private bool Verif_comboBoxEntFacturation()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxEntFacturation, this._textBlockEntFacturation);
        }

        private void _comboBoxEntFacturation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxEntFacturation();
            Entreprise E = new Entreprise();
            E = (Entreprise)this._comboBoxEntFacturation.SelectedItem;
            if (E != null)
            {
                this._comboBoxEntFacturation.SelectedItem = E;
                this._textBoxAddEntFacturation.Text = E.Adresse1.Rue + " " + E.Adresse1.Complement_Adresse;
                this._textBoxCpEntFacturation.Text = E.Adresse1.Ville1.Code_Postal;
                this._textBoxVilleEntFacturation.Text = E.Adresse1.Ville1.Libelle;
                this._textBoxPaysEntFacturation.Text = E.Adresse1.Ville1.Pays1.Libelle;
            }
            else
            {
                this._comboBoxEntFacturation.SelectedItem = null;
                this._textBoxAddEntFacturation.Text = null;
                this._textBoxCpEntFacturation.Text = null;
                this._textBoxVilleEntFacturation.Text = null;
                this._textBoxPaysEntFacturation.Text = null;
            }
        }

        #endregion

        #region Type commande

        private bool Verif_comboBoxTypeCommande()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxTypeCommande, this._textBockTypeCommande);
        }

        private void _comboBoxTypeCommande_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxTypeCommande();
        }

        #endregion

        #region Nature de la commande

        private bool Verif_textBoxNature()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxNature, this._textBlockNature);
        }

        private void _textBoxNature_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_textBoxNature();
        }

        #endregion

        #region Total commande

        private bool Verif_textBoxTotalCommande()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxTotalCommande, this._textBlockTotalCommande);
        }

        private void _textBoxTotalCommande_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxTotalCommande();
        }

        #endregion

        #region Total ramené à

        private bool Verif_textBoxTotalRameneA()
        {
            return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._textBoxTotalRameneA, this._textBlockTotalRameneA);
        }

        private void _textBoxTotalRameneA_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxTotalRameneA();
        }

        #endregion

        #region Taux remisé

        private bool Verif_textBoxTauxRemise()
        {
            bool verif = true;

            if (this._checkBoxRemise.IsChecked == true)
            {
                verif = ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxTauxRemise, this._textBlockTauxRemise);
            }
            else
            {
                verif = ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxTauxRemise, this._textBlockTauxRemise);
            }

            return verif;
        }

        private void _textBoxTauxRemise_LostFocus(object sender, RoutedEventArgs e)
        {
            //this.Verif_textBoxTauxRemise();            
            double val;
            if (this._dataGridContenuCommande.IsReadOnly == false)
            {
                if (double.TryParse(this._textBoxTotalCommande.Text, out val) && double.TryParse(this._textBoxTauxRemise.Text, out val))
                {
                    ((Commande_Fournisseur)this.DataContext).Total_Ramene_A = double.Parse(this._textBoxTotalCommande.Text) * (1 - (double.Parse(this._textBoxTauxRemise.Text) / 100));
                    this.VentilationRemise(0, false);
                }
            }

            this._DatagridContenuCommandeCalcul();
        }

        #endregion

        #endregion

        #region Tab Conditions de réglement

        private bool Verif_Tab_Condition_Reglement()
        {
            bool test = true;

            if (!Verif_dataGridCommandeFournisseurConditionReglement())
            {
                test = false;
            }

            ((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemCondReglement, test);

            return test;
        }

        #region Conditions_Reglement

        private bool Verif_dataGridCommandeFournisseurConditionReglement()
        {
            bool verif = true;

            if (this._dataGridCommandeFournisseurConditionReglement.Items.Count == 0)
            {
                verif = false;
            }
            else
            {
                double somme = 0;
                foreach (Commande_Fournisseur_Condition_Reglement item in this._dataGridCommandeFournisseurConditionReglement.Items.OfType<Commande_Fournisseur_Condition_Reglement>())
                {
                    try
                    {
                        somme = somme + double.Parse(item.Pourcentage.ToString());
                    }
                    catch (Exception) { }
                }
                if (somme <= 100)
                {
                    verif = true;
                }
                else
                {
                    verif = false;
                }
            }
            ((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridCommandeFournisseurConditionReglement, verif);
            return verif;
        }

        #endregion

        #endregion

        #region Tab Entreprise Enlevement

        private bool Verif_Tab_Entreprise_Enlevement()
        {
            bool test = true;

            if (!Verif_comboBoxEntEnlevement())
            {
                test = false;
            }

            ((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemEntrepriseEnlevement, test);

            return test;
        }

        #region Entreprise enlèvement

        private bool Verif_comboBoxEntEnlevement()
        {
            bool verif = true;

            if (this._checkBoxIsEntrepriseEnlevement.IsChecked == true)
            {
                verif = ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxEntEnlevement, this._textBlockEntEnlevement);
            }
            else
            {
                this._comboBoxEntEnlevement.SelectedItem = null;
                verif = ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxEntEnlevement, this._textBlockEntEnlevement);
            }

            return verif;
        }

        private void _comboBoxEntEnlevement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxEntEnlevement();
            if (this._comboBoxEntEnlevement.SelectedItem != null)
            {
                Entreprise E = (Entreprise)this._comboBoxEntEnlevement.SelectedItem;
                this._textBoxAddEntEnlevement.Text = E.Adresse1.Rue + " " + E.Adresse1.Complement_Adresse;
                this._textBoxCpEntEnlevement.Text = E.Adresse1.Ville1.Code_Postal;
                this._comboBoxVilleEntEnlevement.Text = E.Adresse1.Ville1.Libelle;
                this._comboBoxPaysEntEnlevement.Text = E.Adresse1.Ville1.Pays1.Libelle;
            }
            else
            {
                this._textBoxAddEntEnlevement.Text = "";
                this._textBoxCpEntEnlevement.Text = "";
                this._comboBoxVilleEntEnlevement.Text = "";
                this._comboBoxPaysEntEnlevement.Text = "";
            }
        }

        #endregion

        #endregion

        #endregion

        #region Lecture seule

        public void lectureSeule()
        {
            //TextBox
            this._textBoxNumeroCommande.IsReadOnly = true;
            this._textBoxCommentaireLivraison.IsReadOnly = true;
            this._textBoxCommentaireEntEnlevement.IsReadOnly = true;
            this._TextBoxDescription.IsReadOnly = true;
            this._textBoxTotalRameneA.IsReadOnly = true;
            this._textBoxTauxRemise.IsReadOnly = true;
            this._textBoxNature.IsReadOnly = true;
            this._textBoxCommentaireGeneral.IsReadOnly = true;
            this._textBoxRemise.IsReadOnly = true;
            this._TextBoxDescription.IsReadOnly = true;

            //ComboBox
            this._comboBoxFournisseur.IsEnabled = false;
            this._comboBoxTypeCommande.IsEnabled = false;
            this._comboBoxAffaire.IsEnabled = false;
            this._comboBoxMonteur.IsEnabled = false;
            this._comboBoxDonneurOrdre.IsEnabled = false;
            this._comboBoxContact.IsEnabled = false;
            this._comboBoxContactLivraison.IsEnabled = false;
            this._comboBoxEntLivraison.IsEnabled = false;
            this._comboBoxEntFacturation.IsEnabled = false;
            this._comboBoxEntEnlevement.IsEnabled = false;

            //Boutons
            this._buttonGaucheDroite.IsEnabled = false;
            this._buttonDroiteGauche.IsEnabled = false;
            this._buttonAjouterBonLivraison.IsEnabled = false;
            this._buttonModifierBonLivraison.IsEnabled = false;
            this._buttonSupprimerBonLivraison.IsEnabled = false;
            this._buttonCalculer.IsEnabled = false;
            this._buttonColler.IsEnabled = false;
            this._importerConditions.IsEnabled = false;
            this._buttonSupprimer.IsEnabled = false;
            this.NullContact.IsEnabled = false;
            this.NullContactLivraison.IsEnabled = false;
            this.NullMonteur.IsEnabled = false;
            this._buttonShop.IsEnabled = false;
            this.NewFournisseur.IsEnabled = false;
            this.NewContact.IsEnabled = false;
            this.NewDocTechnique.IsEnabled = false;
            this.NewFournisseur.IsEnabled = false;

            //DataGrid
            this._dataGridContenuCommande.IsReadOnly = true;
            this._col1.IsReadOnly = true;
            this._col2.IsReadOnly = true;
            this._col3.IsReadOnly = true;
            this._col4.IsReadOnly = true;
            this._dataGridCommandeFournisseurConditionReglement.IsReadOnly = true;
            this._dataGridBonLivraison.IsReadOnly = true;

            //DatePicker
            this._datePickerDateCommande.IsEnabled = false;
            this._datePickerDateLivraisonPrevu.IsEnabled = false;

            //Suppression des menus clic droit
            this._dataGridContenuCommande.ContextMenu = null;

            //checkBox
            this._checkBoxEntAtelier.IsEnabled = false;
            this._checkBoxEntAutre.IsEnabled = false;
            this._checkBoxEntChantier.IsEnabled = false;
            this._checkBoxContact.IsEnabled = false;
            this._checkBoxFranco.IsEnabled = false;
            this._checkBoxIsEntrepriseEnlevement.IsEnabled = false;
            this._checkBoxRemise.IsEnabled = false;
            this._checkBoxRemiseSomme.IsEnabled = false;
        }

        public void DeleteLectureSeule()
        {
            if (!soloLecture)
            {
                //TextBox
                //this._textBoxNumeroCommande.IsReadOnly = false;
                this._textBoxCommentaireLivraison.IsReadOnly = false;
                this._textBoxCommentaireEntEnlevement.IsReadOnly = false;
                this._TextBoxDescription.IsReadOnly = false;
                this._textBoxTotalRameneA.IsReadOnly = false;
                this._textBoxTauxRemise.IsReadOnly = false;
                this._textBoxRemise.IsReadOnly = false;
                this._textBoxNature.IsReadOnly = false;
                this._textBoxCommentaireGeneral.IsReadOnly = false;
                this._TextBoxDescription.IsReadOnly = false;

                //ComboBox
                this._comboBoxFournisseur.IsEnabled = true;
                this._comboBoxTypeCommande.IsEnabled = true;
                //this._comboBoxAffaire.IsEnabled = true;
                this._comboBoxMonteur.IsEnabled = true;
                this._comboBoxDonneurOrdre.IsEnabled = true;
                if (this._checkBoxContact.IsChecked == false)
                {
                    this._comboBoxContact.IsEnabled = true;
                    this._comboBoxContactLivraison.IsEnabled = false;
                }
                else
                {
                    this._comboBoxContactLivraison.IsEnabled = true;
                    this._comboBoxContact.IsEnabled = false;
                }
                this._comboBoxEntLivraison.IsEnabled = true;
                this._comboBoxEntFacturation.IsEnabled = true;
                this._comboBoxEntEnlevement.IsEnabled = true;

                //Boutons
                this._buttonGaucheDroite.IsEnabled = true;
                this._buttonDroiteGauche.IsEnabled = true;
                this._buttonAjouterBonLivraison.IsEnabled = true;
                this._buttonModifierBonLivraison.IsEnabled = true;
                this._buttonSupprimerBonLivraison.IsEnabled = true;
                this._buttonCalculer.IsEnabled = true;
                this._buttonColler.IsEnabled = true;
                this._importerConditions.IsEnabled = true;
                this._buttonSupprimer.IsEnabled = true;
                this.NullContact.IsEnabled = true;
                this.NullContactLivraison.IsEnabled = true;
                this.NullMonteur.IsEnabled = true;
                this._buttonShop.IsEnabled = true;
                this.NewFournisseur.IsEnabled = true;
                this.NewContact.IsEnabled = true;
                this.NewDocTechnique.IsEnabled = true;
                this.NewFournisseur.IsEnabled = true;

                //DataGrid
                //this._dataGridContenuCommande.IsReadOnly = true;
                this._col1.IsReadOnly = false;
                this._col2.IsReadOnly = false;
                this._col3.IsReadOnly = false;
                this._col4.IsReadOnly = false;
                this._dataGridCommandeFournisseurConditionReglement.IsReadOnly = false;
                this._dataGridBonLivraison.IsReadOnly = false;

                //DatePicker
                this._datePickerDateCommande.IsEnabled = true;
                this._datePickerDateLivraisonPrevu.IsEnabled = true;

                //remise en route des menus clic droit
                this.creationMenuClicDroit();

                //checkBox
                this._checkBoxEntAtelier.IsEnabled = true;
                this._checkBoxEntAutre.IsEnabled = true;
                this._checkBoxEntChantier.IsEnabled = true;
                this._checkBoxContact.IsEnabled = true;
                this._checkBoxFranco.IsEnabled = true;
                this._checkBoxIsEntrepriseEnlevement.IsEnabled = true;
                this._checkBoxRemise.IsEnabled = true;
                this._checkBoxRemiseSomme.IsEnabled = true;
            }
        }

        #endregion

        #region Fonctions

        public void miseAutoInfos(TextBox refe)
        {
            try
            {
                bool test = false;
                //MessageBox.Show(((App)App.Current).mySitaffEntities.GetShopCommandeWithEntrepriseNameAndReference(((Fournisseur)this._comboBoxFournisseur.SelectedItem).Identifiant, refe.Text).Count().ToString());
                foreach (GetShopCommandeWithEntrepriseNameAndReference_Result item in ((App)App.Current).mySitaffEntities.GetShopCommandeWithEntrepriseNameAndReference(((Fournisseur)this._comboBoxFournisseur.SelectedItem).Identifiant, refe.Text))
                {
                    if (item.Reference.ToLower().Trim() == refe.Text.ToLower().Trim())
                    {
                        ((Contenu_Commande_Fournisseur)this._dataGridContenuCommande.SelectedItem).Reference = item.Reference;
                        ((Contenu_Commande_Fournisseur)this._dataGridContenuCommande.SelectedItem).Designation = item.Designation;
                        ((Contenu_Commande_Fournisseur)this._dataGridContenuCommande.SelectedItem).Prix_Unitaire = double.Parse(item.Min_Prix_Unitaire.ToString());
                        refe.SelectionStart = refe.Text.Length;
                        test = true;
                    }
                }
                if (!test)
                {
                    ((Contenu_Commande_Fournisseur)this._dataGridContenuCommande.SelectedItem).Designation = "";
                    ((Contenu_Commande_Fournisseur)this._dataGridContenuCommande.SelectedItem).Prix_Unitaire = 0;
                }
            }
            catch (Exception) {}
        }

        private void generationNumeroCommande()
        {
            if (((Commande_Fournisseur)this.DataContext).Numero == null || ((Commande_Fournisseur)this.DataContext).Numero == "")
            {
                String numTemp = "";
                if (((Commande_Fournisseur)this.DataContext).Affaire1 != null && ((Commande_Fournisseur)this.DataContext).Affaire1.Numero != "")
                {
                    numTemp = ((Commande_Fournisseur)this.DataContext).Affaire1.Numero + "-";
                }
                else if (((Commande_Fournisseur)this.DataContext).Stock == true)
                {
                    numTemp = "Stock" + "-";
                }
                else if (((Commande_Fournisseur)this.DataContext).Divers == true)
                {
                    numTemp = "Divers" + "-";
                }
                else
                {
                    numTemp = "Autre-Erreur" + "-";
                }
                String year = DateTime.Today.Year.ToString();
                String mois = DateTime.Today.Month.ToString();
                int i = 1;
                foreach (char c in year)
                {
                    if (i == 3 || i == 4)
                    {
                        numTemp = numTemp + c;
                    }
                    i = i + 1;
                }
                int nbCaracteres = 0;
                foreach (Char c in mois)
                {
                    nbCaracteres++;
                }
                if (nbCaracteres == 1)
                {
                    numTemp += "0";
                }
                numTemp = numTemp + mois + "-";

                String incrementToPut = "001";

                ObservableCollection<Commande_Fournisseur> toTest = new ObservableCollection<Commande_Fournisseur>(((App)App.Current).mySitaffEntities.Commande_Fournisseur.Where(com => com.Numero.Contains(numTemp)));
                if (toTest.Count() == 0)
                {

                }
                else
                {
                    ObservableCollection<int> lesEntiersPourIncr = new ObservableCollection<int>();
                    int PlusGrand = 0;
                    foreach (Commande_Fournisseur item in toTest)
                    {
                        int test;
                        if (int.TryParse(item.Numero.Replace(numTemp, ""), out test))
                        {
                            lesEntiersPourIncr.Add(int.Parse(item.Numero.Replace(numTemp, "")));
                        }
                    }
                    foreach (int entier in lesEntiersPourIncr)
                    {
                        if (entier > PlusGrand)
                        {
                            PlusGrand = entier;
                        }
                    }
                    PlusGrand = PlusGrand + 1;
                    incrementToPut = PlusGrand.ToString();
                    String tempIncrement = "";
                    nbCaracteres = 0;
                    foreach (Char c in incrementToPut)
                    {
                        nbCaracteres++;
                    }
                    if (nbCaracteres == 1)
                    {
                        tempIncrement += "00";
                    }
                    if (nbCaracteres == 2)
                    {
                        tempIncrement += "0";
                    }
                    tempIncrement = tempIncrement + incrementToPut;
                    incrementToPut = tempIncrement;
                }
                numTemp = numTemp + incrementToPut;

                ((Commande_Fournisseur)this.DataContext).Numero = numTemp;
                this._textBoxNumeroCommande.Text = numTemp;
            }
        }

        #region Fonctions des checkbox livraison

        private void miseAutoEntrepriseFacturation()
        {
            if (this._comboBoxAffaire.SelectedItem != null)
            {
                this._comboBoxEntFacturation.SelectedItem = ((Affaire)this._comboBoxAffaire.SelectedItem).Entreprise_Mere1.Entreprise1;
            }
            else
            {
                this._comboBoxEntFacturation.SelectedItem = null;
            }
        }

        private void miseAutoEntrepriseLivraison()
        {
            if (this._checkBoxEntAtelier.IsChecked == true)
            {
                this.listEntrepriseLivraison = new ObservableCollection<Entreprise>();
                foreach (Entreprise_Mere em in ((App)App.Current).mySitaffEntities.Entreprise_Mere)
                {
                    bool test = false;
                    foreach (Entreprise ent in this.listEntrepriseLivraison)
                    {
                        if (ent.Identifiant == em.Entreprise1.Identifiant)
                        {
                            test = true;
                        }
                    }
                    if (test == false)
                    {
                        this.listEntrepriseLivraison.Add(em.Entreprise1);
                    }
                }
                this.listEntrepriseLivraison = new ObservableCollection<Entreprise>(this.listEntrepriseLivraison.OrderBy(ent => ent.Libelle));

                if (this._comboBoxAffaire.SelectedItem != null)
                {
                    try
                    {
                        this._comboBoxEntLivraison.SelectedItem = ((Affaire)this._comboBoxAffaire.SelectedItem).Entreprise_Mere1.Entreprise1;
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    this._comboBoxEntLivraison.SelectedItem = null;
                }
            }
        }

        public void TriEntreprise()
        {
            this.listEntrepriseLivraison = new ObservableCollection<Entreprise>();

            if (this._comboBoxAffaire.SelectedItem != null)
            {
                foreach (Versions ver in ((Affaire)this._comboBoxAffaire.SelectedItem).Versions)
                {
                    if (!this.listEntrepriseLivraison.Contains(ver.Devis1.Client1.Entreprise))
                    {
                        this.listEntrepriseLivraison.Add(ver.Devis1.Client1.Entreprise);
                    }
                }
            }
            if (this.listEntrepriseLivraison.Count == 1)
            {
                foreach (Entreprise ent in this.listEntrepriseLivraison)
                {
                    this._comboBoxEntLivraison.SelectedItem = ent;
                }
            }
        }

        public void TouteEntreprise()
        {
            this.listEntrepriseLivraison = this.listEntreprise;
        }

        #endregion

        public void VerrouillerContenu()
        {
            bool toTest = true;

            if (((Commande_Fournisseur)this.DataContext).Bon_Livraison.Count() != 0)
            {
                toTest = false;
            }
            if (((Commande_Fournisseur)this.DataContext).Facture_Proforma.Count() != 0)
            {
                toTest = false;
            }
            foreach (Contenu_Commande_Fournisseur item in ((Commande_Fournisseur)this.DataContext).Contenu_Commande_Fournisseur)
            {
                if (item.Facture_Fournisseur_Contenu.Count() > 0)
                {
                    toTest = false;
                }
            }

            Securite securite = new Securite();
            if (!toTest)
            {
                this.lectureSeule();
                if (!soloLecture)
                {
                    if (!securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.UserControls.ListeBonLivraisonControl", "Add"))
                    {
                        this._buttonAjouterBonLivraison.IsEnabled = false;
                    }
                    else
                    {
                        this._buttonAjouterBonLivraison.IsEnabled = true;
                    }
                    if (!securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.UserControls.ListeBonLivraisonControl", "Update"))
                    {
                        this._buttonModifierBonLivraison.IsEnabled = false;
                    }
                    else
                    {
                        this._buttonModifierBonLivraison.IsEnabled = true;
                    }
                    if (!securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.UserControls.ListeBonLivraisonControl", "Delete"))
                    {
                        this._buttonSupprimerBonLivraison.IsEnabled = false;
                    }
                    else
                    {
                        this._buttonSupprimerBonLivraison.IsEnabled = true;
                    }
                }
            }
            else
            {
                this.DeleteLectureSeule();
                if (!soloLecture)
                {
                    if (!securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.UserControls.ListeBonLivraisonControl", "Add"))
                    {
                        this._buttonAjouterBonLivraison.IsEnabled = false;
                    }
                    if (!securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.UserControls.ListeBonLivraisonControl", "Update"))
                    {
                        this._buttonModifierBonLivraison.IsEnabled = false;
                    }
                    if (!securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.UserControls.ListeBonLivraisonControl", "Delete"))
                    {
                        this._buttonSupprimerBonLivraison.IsEnabled = false;
                    }
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this._textBoxTauxRemise.Visibility = Visibility.Collapsed;
            this._textBoxTauxRemise.Text = "0";
            ((Commande_Fournisseur)this.DataContext).Remise = 0;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this._checkBoxRemiseSomme.IsChecked = false;
            this._textBoxTauxRemise.Visibility = Visibility.Visible;
        }

        private void calculLoaded()
        {
            double total = 0;
            double totalSansRemise = 0;
            double totalAvecRemise = 0;
            double val;

            if (double.TryParse(this._textBoxTotalCommande.Text, out val) && double.TryParse(this._textBoxRemise.Text, out val))
            {
                ((Commande_Fournisseur)this.DataContext).Total_Ramene_A = double.Parse(this._textBoxTotalCommande.Text) - double.Parse(this._textBoxRemise.Text);
                this.VentilationRemise((1 - ((double.Parse(this._textBoxTotalCommande.Text) - double.Parse(this._textBoxRemise.Text)) / (double.Parse(this._textBoxTotalCommande.Text)))) * 100, true);
            }

            foreach (Contenu_Commande_Fournisseur cont in this._dataGridContenuCommande.Items.OfType<Contenu_Commande_Fournisseur>())
            {
                if (cont.Commande_Fournisseur1 == null)
                {
                    cont.Commande_Fournisseur1 = (Commande_Fournisseur)this.DataContext;
                }
            }

            ObservableCollection<Contenu_Commande_Fournisseur> contenu = new ObservableCollection<Contenu_Commande_Fournisseur>();

            foreach (Contenu_Commande_Fournisseur cont in ((Commande_Fournisseur)this.DataContext).Contenu_Commande_Fournisseur)
            {
                cont.Prix_Total = cont.Quantite * cont.Prix_Unitaire;
                //if (this._checkBoxRemise.IsChecked == true && double.TryParse(this._textBoxTauxRemise.Text, out val))
                //{
                //    if (cont.Taux_Remise == 0)
                //    {
                //        cont.Taux_Remise = double.Parse(this._textBoxTauxRemise.Text);
                //    }
                //}
                cont.Prix_Remise = cont.Prix_Unitaire * (1 - (cont.Taux_Remise / 100));

                totalSansRemise = totalSansRemise + cont.Prix_Total;
                total = total + cont.Prix_Remise;
                totalAvecRemise = totalAvecRemise + (cont.Prix_Remise * cont.Quantite);
            }
            this._textBoxTotalCommande.Text = totalSansRemise.ToString();
            ((Commande_Fournisseur)this.DataContext).Total_Commande = totalSansRemise;
            ((Commande_Fournisseur)this.DataContext).Total_Ramene_A = totalAvecRemise;
        }

        private double _DatagridContenuCommandeCalcul()
        {
            //Validation si cellule en édition
            try
            {
                this._dataGridContenuCommande.CommitEdit();
            }
            catch (Exception)
            {

            }
            try
            {
                this._buttonCalculer.Focus();
            }
            catch (Exception) { }

            try
            {
                double total = 0;
                double totalSansRemise = 0;
                double totalAvecRemise = 0;
                double val;

                if (double.TryParse(this._textBoxTotalCommande.Text, out val) && double.TryParse(this._textBoxRemise.Text, out val))
                {
                    ((Commande_Fournisseur)this.DataContext).Total_Ramene_A = double.Parse(this._textBoxTotalCommande.Text) - double.Parse(this._textBoxRemise.Text);
                    this.VentilationRemise((1 - ((double.Parse(this._textBoxTotalCommande.Text) - double.Parse(this._textBoxRemise.Text)) / (double.Parse(this._textBoxTotalCommande.Text)))) * 100, true);
                }

                foreach (Contenu_Commande_Fournisseur cont in this._dataGridContenuCommande.Items.OfType<Contenu_Commande_Fournisseur>())
                {
                    if (cont.Commande_Fournisseur1 == null)
                    {
                        cont.Commande_Fournisseur1 = (Commande_Fournisseur)this.DataContext;
                    }
                }

                ObservableCollection<Contenu_Commande_Fournisseur> contenu = new ObservableCollection<Contenu_Commande_Fournisseur>();

                foreach (Contenu_Commande_Fournisseur cont in this._dataGridContenuCommande.Items.OfType<Contenu_Commande_Fournisseur>())
                {
                    cont.Prix_Total = cont.Quantite * cont.Prix_Unitaire;
                    //if (this._checkBoxRemise.IsChecked == true && double.TryParse(this._textBoxTauxRemise.Text, out val))
                    //{
                    //    if (cont.Taux_Remise == 0)
                    //    {
                    //        cont.Taux_Remise = double.Parse(this._textBoxTauxRemise.Text);
                    //    }
                    //}
                    cont.Prix_Remise = cont.Prix_Unitaire * (1 - (cont.Taux_Remise / 100));

                    totalSansRemise = totalSansRemise + cont.Prix_Total;
                    total = total + cont.Prix_Remise;
                    totalAvecRemise = totalAvecRemise + (cont.Prix_Remise * cont.Quantite);
                }
                this._textBoxTotalCommande.Text = totalSansRemise.ToString();
                ((Commande_Fournisseur)this.DataContext).Total_Commande = totalSansRemise;
                ((Commande_Fournisseur)this.DataContext).Total_Ramene_A = totalAvecRemise;
                if (this._checkBoxRemiseSomme.IsChecked == true && double.TryParse(this._textBoxRemise.Text, out val))
                {
                    total = total - double.Parse(this._textBoxRemise.Text);
                }
                try
                {
                    this._dataGridContenuCommande.Items.Refresh();
                }
                catch (Exception)
                {
                }
                //this._textBoxTotalRameneA.Text = total.ToString();
                //((Commande_Fournisseur)this.DataContext).Total_Ramene_A = total;
                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void VentilationRemise(double remi, bool rem)
        {
            double val;

            if (rem == true)
            {
                val = remi;
            }
            else
            {
                if (double.TryParse(this._textBoxTauxRemise.Text, out val))
                {
                    val = double.Parse(this._textBoxTauxRemise.Text);
                }
                else
                {
                    val = 0;
                }
            }
            foreach (Contenu_Commande_Fournisseur cont in this._dataGridContenuCommande.Items.OfType<Contenu_Commande_Fournisseur>())
            {
                cont.Prix_Total = cont.Quantite * cont.Prix_Unitaire;
                cont.Taux_Remise = val;
                cont.Prix_Remise = cont.Prix_Total * (1 - (cont.Taux_Remise / 100));
            }
        }

        private void deleteLigne()
        {
            if (this._dataGridContenuCommande.SelectedItem != null && this._dataGridContenuCommande.SelectedItems.Count == 1)
            {
                try
                {
                    ((Contenu_Commande_Fournisseur)this._dataGridContenuCommande.SelectedItem).Quantite = 0;
                    ((Contenu_Commande_Fournisseur)this._dataGridContenuCommande.SelectedItem).Prix_Unitaire = 0;
                    ((Contenu_Commande_Fournisseur)this._dataGridContenuCommande.SelectedItem).Prix_Total = 0;
                    ((Contenu_Commande_Fournisseur)this._dataGridContenuCommande.SelectedItem).Prix_Remise = 0;
                    ((Contenu_Commande_Fournisseur)this._dataGridContenuCommande.SelectedItem).Taux_Remise = 0;
                }
                catch (Exception) { }
                Contenu_Commande_Fournisseur item = (Contenu_Commande_Fournisseur)this._dataGridContenuCommande.SelectedItem;
                try
                {
                    item.Commande_Fournisseur1 = null;
                    ((Commande_Fournisseur)this.DataContext).Contenu_Commande_Fournisseur.Remove(item);
                    ((App)App.Current).mySitaffEntities.Contenu_Commande_Fournisseur.DeleteObject(item);
                }
                catch (Exception)
                {
                    try
                    {
                        ((Commande_Fournisseur)this.DataContext).Contenu_Commande_Fournisseur.Remove(item);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            this._dataGridContenuCommande.Items.Remove(item);
                        }
                        catch (Exception) { }
                    }
                }
            }
            else
            {
                if (this._dataGridContenuCommande.SelectedItems.Count != 0)
                {
                    ObservableCollection<Contenu_Commande_Fournisseur> toRemove = new ObservableCollection<Contenu_Commande_Fournisseur>();
                    foreach (Contenu_Commande_Fournisseur item in this._dataGridContenuCommande.SelectedItems.OfType<Contenu_Commande_Fournisseur>())
                    {
                        toRemove.Add(item);
                    }
                    foreach (Contenu_Commande_Fournisseur item in toRemove)
                    {
                        try
                        {
                            item.Quantite = 0;
                            item.Prix_Unitaire = 0;
                            item.Prix_Total = 0;
                            item.Prix_Remise = 0;
                            item.Taux_Remise = 0;
                        }
                        catch (Exception) { }
                        try
                        {
                            item.Commande_Fournisseur1 = null;
                            ((Commande_Fournisseur)this.DataContext).Contenu_Commande_Fournisseur.Remove(item);
                            ((App)App.Current).mySitaffEntities.Contenu_Commande_Fournisseur.DeleteObject(item);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                ((Commande_Fournisseur)this.DataContext).Contenu_Commande_Fournisseur.Remove(item);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    this._dataGridContenuCommande.Items.Remove(item);
                                }
                                catch (Exception) { }
                            }
                        }
                    }
                }
            }
            this._dataGridContenuCommande.Items.Refresh();
        }

        #endregion

        #region Evenements

        #region coller CTRL+V

        private void _CommandColler_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            CopierColler ClassPaste = new CopierColler();
            ObservableCollection<Contenu_Commande_Fournisseur> listToAdd = ClassPaste.PasteDataCommandeWindow();
            if (listToAdd != null)
            {
                foreach (Contenu_Commande_Fournisseur ccf in listToAdd)
                {
                    ((Commande_Fournisseur)this.DataContext).Contenu_Commande_Fournisseur.Add(ccf);
                }
                this._dataGridContenuCommande.Items.Refresh();
                this._DatagridContenuCommandeCalcul();
            }
        }

        private void _CommandColler_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region CheckBoxs Livraison

        private void _checkBoxEntAtelier_Click(object sender, RoutedEventArgs e)
        {
            this._checkBoxEntAutre.IsChecked = false;
            this._checkBoxEntChantier.IsChecked = false;
            this.miseAutoEntrepriseLivraison();
        }

        private void _checkBoxEntAutre_Click(object sender, RoutedEventArgs e)
        {
            this._checkBoxEntAtelier.IsChecked = false;
            this._checkBoxEntChantier.IsChecked = false;
            this.TouteEntreprise();
        }

        private void _checkBoxEntChantier_Click(object sender, RoutedEventArgs e)
        {
            this._checkBoxEntAtelier.IsChecked = false;
            this._checkBoxEntAutre.IsChecked = false;
            this.TriEntreprise();
        }

        #endregion

        #region CheckBoxs Contact

        private void _checkBoxContact_Checked(object sender, RoutedEventArgs e)
        {
            if (this._checkBoxContact.IsChecked == true)
            {
                this._comboBoxMonteur.SelectedItem = null;
                this._comboBoxContactLivraison.IsEnabled = true;
                this._comboBoxMonteur.IsEnabled = false;
            }
            else
            {
                this._comboBoxContactLivraison.SelectedItem = null;
                this._comboBoxContactLivraison.IsEnabled = false;
                this._comboBoxMonteur.IsEnabled = true;
            }
        }

        private void _checkBoxContact_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this._checkBoxContact.IsChecked == true)
            {
                this._comboBoxMonteur.SelectedItem = null;
                this._comboBoxContactLivraison.IsEnabled = true;
                this._comboBoxMonteur.IsEnabled = false;
            }
            else
            {
                this._comboBoxContactLivraison.SelectedItem = null;
                this._comboBoxContactLivraison.IsEnabled = false;
                this._comboBoxMonteur.IsEnabled = true;
            }
        }

        #endregion

        #region CheckBox Remise

        private void CheckBoxRemise_Checked(object sender, RoutedEventArgs e)
        {
            this._checkBoxRemise.IsChecked = false;
            this._textBoxRemise.Visibility = Visibility.Visible;
        }

        private void CheckBoxRemise_Unchecked(object sender, RoutedEventArgs e)
        {
            this._textBoxRemise.Visibility = Visibility.Collapsed;
            this._textBoxRemise.Text = "0";
            ((Commande_Fournisseur)this.DataContext).Remise_Somme = 0;
            this._DatagridContenuCommandeCalcul();
        }

        #endregion

        #region textbox Remise

        private void _textBoxRemise_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (!chargement)
            //{
            //    double val;

            //    if (double.TryParse(this._textBoxTotalCommande.Text, out val) && double.TryParse(this._textBoxRemise.Text, out val))
            //    {
            //        ((Commande_Fournisseur)this.DataContext).Total_Ramene_A = double.Parse(this._textBoxTotalCommande.Text) - double.Parse(this._textBoxRemise.Text);
            //        this.VentilationRemise((1 - ((double.Parse(this._textBoxTotalCommande.Text) - double.Parse(this._textBoxRemise.Text)) / (double.Parse(this._textBoxTotalCommande.Text)))) * 100, true);
            //    }

            //    this._DatagridContenuCommandeCalcul();
            //}
        }

        #endregion

        #region KeyUp

        private void _dataGridContenuCommande_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Référence":
                            this.miseAutoInfos(((TextBox)e.OriginalSource));
                            break;
                        case "Quantité":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "P.U.":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "P.U. Remisé":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Prix Total Non remisé":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Prix Total Remisé":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Taux de remise %":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void _textBoxTotalRameneA_KeyUp(object sender, KeyEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
        }

        private void _dataGridCommandeFournisseurConditionReglement_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Pourcentage":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region TextBox TotalRamene A

        private void _textBoxTotalRameneA_TextChanged(object sender, RoutedEventArgs e)
        {
            double val;
            double totalrameneabak = 0;

            if (this._checkBoxRemise.IsChecked == false)
            {
                if (double.TryParse(this._textBoxTotalRameneA.Text, out val))
                {
                    totalrameneabak = double.Parse(this._textBoxTotalRameneA.Text);
                    this._checkBoxRemiseSomme.IsChecked = true;
                    this._checkBoxRemise.IsChecked = false;
                    //this._textBoxRemise.Text = (double.Parse(this._textBoxTotalCommande.Text) - totalrameneabak).ToString();
                    ((Commande_Fournisseur)this.DataContext).Remise_Somme = double.Parse(this._textBoxTotalCommande.Text) - totalrameneabak;
                }
            }
            this._DatagridContenuCommandeCalcul();
        }

        #endregion

        #region TextBox Taux Remise

        private void _textBoxTauxRemise_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!chargement)
            {
                if (this._dataGridContenuCommande.IsReadOnly == false)
                {
                    this.VentilationRemise(0, false);
                    this._DatagridContenuCommandeCalcul();
                }
            }
        }

        #endregion

        #region checkBox Entreprise Enlevement

        private void _checkBoxIsEntrepriseEnlevement_Checked(object sender, RoutedEventArgs e)
        {
            this._tabItemEntrepriseEnlevement.Visibility = Visibility.Visible;
        }

        private void _checkBoxIsEntrepriseEnlevement_Unchecked(object sender, RoutedEventArgs e)
        {
            this._tabItemEntrepriseEnlevement.Visibility = Visibility.Collapsed;
            this._comboBoxEntEnlevement.SelectedItem = null;
            this._textBoxCommentaireEntEnlevement.Text = "";
            if (this._tabControl.SelectedContent == this._tabItemEntrepriseEnlevement)
            {
                this._dataGridContenuCommande.Focus();
            }
        }

        #endregion

        #endregion

        #region clic droit

        #region Contenu Commande

        private void creationMenuClicDroit()
        {
            ContextMenu contextMenu = new ContextMenu();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorToPut = "#A3D0D8E8";
            Brush colorMenu = (Brush)converter.ConvertFrom(colorToPut);
            contextMenu.Background = colorMenu;
            this._dataGridContenuCommande.ContextMenu = contextMenu;

            MenuItem itemAfficher = new MenuItem();
            itemAfficher.Header = "Supprimer";
            itemAfficher.Click += new RoutedEventHandler(delegate { this.menuDelete(); });


            contextMenu.Items.Add(itemAfficher);
        }

        private void menuDelete()
        {
            this.deleteLigne();
        }

        #endregion

        private void _TextBoxDescription_LostFocus_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ((Contenu_Commande_Fournisseur)((TextBox)sender).DataContext).Description = ((TextBox)sender).Text;
            }
            catch (Exception) { }
        }

        #endregion

        private void _textBoxRemise_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (!chargement)
            {
                double val;

                if (double.TryParse(this._textBoxTotalCommande.Text, out val) && double.TryParse(this._textBoxRemise.Text, out val))
                {
                    ((Commande_Fournisseur)this.DataContext).Total_Ramene_A = double.Parse(this._textBoxTotalCommande.Text) - double.Parse(this._textBoxRemise.Text);
                    this.VentilationRemise((1 - ((double.Parse(this._textBoxTotalCommande.Text) - double.Parse(this._textBoxRemise.Text)) / (double.Parse(this._textBoxTotalCommande.Text)))) * 100, true);
                }

                this._DatagridContenuCommandeCalcul();
            }
        }

    }
}
