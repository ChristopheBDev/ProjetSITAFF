using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SitaffRibbon.Classes;
using SitaffRibbon.UserControls;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour FactureFournisseurWindow.xaml
    /// </summary>
    public partial class FactureFournisseurWindow : Window
    {

        #region propd

        public ObservableCollection<Commande_Fournisseur> listCommandeContenu
        {
            get { return (ObservableCollection<Commande_Fournisseur>)GetValue(listCommandeContenuProperty); }
            set { SetValue(listCommandeContenuProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listCommandeContenu.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listCommandeContenuProperty =
            DependencyProperty.Register("listCommandeContenu", typeof(ObservableCollection<Commande_Fournisseur>), typeof(FactureFournisseurWindow), new PropertyMetadata(null));



        public ObservableCollection<Plan_Comptable_Imputation> listPlanComptableImputation
        {
            get { return (ObservableCollection<Plan_Comptable_Imputation>)GetValue(listPlanComptableImputationProperty); }
            set { SetValue(listPlanComptableImputationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listPlanComptableImputation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listPlanComptableImputationProperty =
            DependencyProperty.Register("listPlanComptableImputation", typeof(ObservableCollection<Plan_Comptable_Imputation>), typeof(FactureFournisseurWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Plan_Comptable_Tva> listPlanComptableTva
        {
            get { return (ObservableCollection<Plan_Comptable_Tva>)GetValue(listPlanComptableTvaProperty); }
            set { SetValue(listPlanComptableTvaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listPlanComptableTva.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listPlanComptableTvaProperty =
            DependencyProperty.Register("listPlanComptableTva", typeof(ObservableCollection<Plan_Comptable_Tva>), typeof(FactureFournisseurWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(FactureFournisseurWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Condition_Reglement> list_Condition_Reglement
        {
            get { return (ObservableCollection<Condition_Reglement>)GetValue(list_Condition_ReglementProperty); }
            set { SetValue(list_Condition_ReglementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for list_Condition_Reglement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty list_Condition_ReglementProperty =
            DependencyProperty.Register("list_Condition_Reglement", typeof(ObservableCollection<Condition_Reglement>), typeof(FactureFournisseurWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Fournisseur> listFournisseur
        {
            get { return (ObservableCollection<Fournisseur>)GetValue(listFournisseurProperty); }
            set { SetValue(listFournisseurProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listFournisseur.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFournisseurProperty =
            DependencyProperty.Register("listFournisseur", typeof(ObservableCollection<Fournisseur>), typeof(FactureFournisseurWindow), new UIPropertyMetadata(null));



        #endregion

        #region Constructeur

        public FactureFournisseurWindow()
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
            this._textBoxNumeroFacture.Focus();
        }

        #region initialisation

        private void initialisationPropDependance()
        {
            this.listPlanComptableImputation = new ObservableCollection<Plan_Comptable_Imputation>(((App)App.Current).mySitaffEntities.Plan_Comptable_Imputation.OrderBy(pci => pci.Numero));
            this.listPlanComptableTva = new ObservableCollection<Plan_Comptable_Tva>(((App)App.Current).mySitaffEntities.Plan_Comptable_Tva.OrderBy(pct => pct.Numero));
            this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
            this.list_Condition_Reglement = new ObservableCollection<Condition_Reglement>(((App)App.Current).mySitaffEntities.Condition_Reglement.OrderBy(cr => cr.Libelle));
            this.listFournisseur = new ObservableCollection<Fournisseur>(((App)App.Current).mySitaffEntities.Fournisseur.OrderBy(fou => fou.Entreprise.Libelle));
            this._comboBoxFilterCommandeBL.ItemsSource = new ObservableCollection<Commande_Fournisseur>(((App)App.Current).mySitaffEntities.Commande_Fournisseur.OrderBy(com => com.Numero));
            this._comboBoxFilterEntrepriseMere.Items.Clear();
            this._comboBoxFilterEntrepriseMere.ItemsSource = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.Where(em => em.Plan_Comptable_Imputation.Count() > 0 || em.Plan_Comptable_Tva.Count() > 0).OrderBy(em => em.Nom));
            this.remplissageComboBoxCommandesContenu();
        }

        private void initialisationSecurite()
        {
            //Mise en place des droits sur les boutons et tabs

            if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeCommandeFournisseurControl", "Look"))
            {
                this.LookCommande.Visibility = Visibility.Collapsed;
                this.LookCommande1.Visibility = Visibility.Collapsed;
            }
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeAffaireControl", "Look"))
            {
                this.LookAffaire.Visibility = Visibility.Collapsed;
            }
        }

        private void creationMenuClicDroit()
        {
            ContextMenu contextMenu = new ContextMenu();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorToPut = "#A3D0D8E8";
            Brush colorMenu = (Brush)converter.ConvertFrom(colorToPut);
            contextMenu.Background = colorMenu;
            this._DataGridContenu.ContextMenu = contextMenu;

            MenuItem itemAfficher = new MenuItem();
            itemAfficher.Header = "Supprimer";
            itemAfficher.Click += new RoutedEventHandler(delegate { this.menuDelete(); });


            contextMenu.Items.Add(itemAfficher);
        }

        #endregion

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;

            this._verrouillerFacture();
            //this.SelectionAutoBL();
            //this.SelectionAutoLigneCommande();

            try
            {
                if (this._comboBoxFilterEntrepriseMere.ItemsSource.OfType<Entreprise_Mere>().Where(em => em.Identifiant == ((App)App.Current)._connectedUser.Salarie_Interne1.Entreprise_Mere1.Identifiant).Count() > 0)
                {
                    Entreprise_Mere tmp = ((App)App.Current)._connectedUser.Salarie_Interne1.Entreprise_Mere1;
                    this._comboBoxFilterEntrepriseMere.SelectedItem = tmp;
                }
            }
            catch (Exception) { }

            this.Calculer();
        }

        #endregion

        #region Boutons

        #region boutons vidoir / dévidoir BL

        private void _buttonGaucheDroiteBL_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridBLGauche.SelectedItem != null)
            {
                if (this._dataGridBLGauche.SelectedItems.Count == 1)
                {
                    foreach (Bon_Livraison_Contenu_Commande item in ((MatriceBL)this._dataGridBLGauche.SelectedItem).bon_livraison.Bon_Livraison_Contenu_Commande)
                    {
                        Facture_Fournisseur_Contenu temp = new Facture_Fournisseur_Contenu();
                        temp.Bon_Livraison_Contenu_Commande1 = item;
                        temp.Designation = item.Contenu_Commande_Fournisseur.Designation;
                        temp.Reference_Fournisseur = item.Contenu_Commande_Fournisseur.Reference;
                        temp.Qte_Commandee = item.Contenu_Commande_Fournisseur.Quantite;
                        temp.Qte_Livree = item.Quantite;
                        temp.Qte_Facturee = item.Quantite;
                        temp.Prix_Unitaire_Commande_HT = item.Contenu_Commande_Fournisseur.Prix_Unitaire;
                        temp.Prix_Unitaire_Remise_HT = item.Contenu_Commande_Fournisseur.Prix_Remise;
                        temp.Prix_Unitaire_Facture_HT = item.Contenu_Commande_Fournisseur.Prix_Remise;
                        temp.Prix_Total_Facture_HT = 0;
                        temp.Diff = 0;
                        temp.Affaire1 = item.Bon_Livraison1.Affaire1;
                        ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Add(temp);
                    }
                    foreach (Bon_Livraison_Contenu_Commande_Supplementaire item in ((MatriceBL)this._dataGridBLGauche.SelectedItem).bon_livraison.Bon_Livraison_Contenu_Commande_Supplementaire)
                    {
                        Facture_Fournisseur_Contenu temp = new Facture_Fournisseur_Contenu();
                        temp.Bon_Livraison_Contenu_Commande_Supplementaire1 = item;
                        temp.Designation = item.Designation;
                        temp.Reference_Fournisseur = item.Reference;
                        temp.Qte_Commandee = 0;
                        temp.Qte_Livree = item.Quantite;
                        temp.Qte_Facturee = item.Quantite;
                        temp.Prix_Unitaire_Commande_HT = item.Prix_Unitaire;
                        temp.Prix_Unitaire_Remise_HT = item.Prix_Remise;
                        temp.Prix_Unitaire_Facture_HT = item.Prix_Remise;
                        temp.Prix_Total_Facture_HT = 0;
                        temp.Diff = 0;
                        temp.Affaire1 = item.Bon_Livraison1.Affaire1;
                        ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Add(temp);
                    }
                    ((MatriceBL)this._dataGridBLGauche.SelectedItem).bon_livraison.Facture_Fournisseur1 = (Facture_Fournisseur)this.DataContext;
                }
                else
                {
                    foreach (MatriceBL ligne in this._dataGridBLGauche.SelectedItems.OfType<MatriceBL>())
                    {
                        foreach (Bon_Livraison_Contenu_Commande item in ligne.bon_livraison.Bon_Livraison_Contenu_Commande)
                        {
                            Facture_Fournisseur_Contenu temp = new Facture_Fournisseur_Contenu();
                            temp.Bon_Livraison_Contenu_Commande1 = item;
                            temp.Designation = item.Contenu_Commande_Fournisseur.Designation;
                            temp.Reference_Fournisseur = item.Contenu_Commande_Fournisseur.Reference;
                            temp.Qte_Commandee = item.Contenu_Commande_Fournisseur.Quantite;
                            temp.Qte_Livree = item.Quantite;
                            temp.Qte_Facturee = item.Quantite;
                            temp.Prix_Unitaire_Commande_HT = item.Contenu_Commande_Fournisseur.Prix_Unitaire;
                            temp.Prix_Unitaire_Remise_HT = item.Contenu_Commande_Fournisseur.Prix_Remise;
                            temp.Prix_Unitaire_Facture_HT = item.Contenu_Commande_Fournisseur.Prix_Remise;
                            temp.Prix_Total_Facture_HT = 0;
                            temp.Diff = 0;
                            temp.Affaire1 = item.Bon_Livraison1.Affaire1;
                            ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Add(temp);
                        }
                        foreach (Bon_Livraison_Contenu_Commande_Supplementaire item in ligne.bon_livraison.Bon_Livraison_Contenu_Commande_Supplementaire)
                        {
                            Facture_Fournisseur_Contenu temp = new Facture_Fournisseur_Contenu();
                            temp.Bon_Livraison_Contenu_Commande_Supplementaire1 = item;
                            temp.Designation = item.Designation;
                            temp.Reference_Fournisseur = item.Reference;
                            temp.Qte_Commandee = 0;
                            temp.Qte_Livree = item.Quantite;
                            temp.Qte_Facturee = item.Quantite;
                            temp.Prix_Unitaire_Commande_HT = item.Prix_Unitaire;
                            temp.Prix_Unitaire_Remise_HT = item.Prix_Remise;
                            temp.Prix_Unitaire_Facture_HT = item.Prix_Remise;
                            temp.Prix_Total_Facture_HT = 0;
                            temp.Diff = 0;
                            temp.Affaire1 = item.Bon_Livraison1.Affaire1;
                            ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Add(temp);
                        }
                        ligne.bon_livraison.Facture_Fournisseur1 = (Facture_Fournisseur)this.DataContext;
                    }
                }
            }
            this.SelectionAutoBL();
            _verrouillerFacture();
            this.calculChaqueLigne();
            this.calculResume();
        }

        private void _buttonDroiteGaucheBL_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridDroitBL.SelectedItem != null)
            {
                if (this._dataGridDroitBL.SelectedItems.Count == 1)
                {
                    ObservableCollection<Facture_Fournisseur_Contenu> toRemove = new ObservableCollection<Facture_Fournisseur_Contenu>();
                    foreach (Facture_Fournisseur_Contenu item in ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu)
                    {
                        if (item.Bon_Livraison_Contenu_Commande_Supplementaire1 != null)
                        {
                            if (item.Bon_Livraison_Contenu_Commande_Supplementaire1.Bon_Livraison1 == ((Bon_Livraison)this._dataGridDroitBL.SelectedItem))
                            {
                                toRemove.Add(item);
                            }
                        }
                        if (item.Bon_Livraison_Contenu_Commande1 != null)
                        {
                            if (item.Bon_Livraison_Contenu_Commande1.Bon_Livraison1 == ((Bon_Livraison)this._dataGridDroitBL.SelectedItem))
                            {
                                toRemove.Add(item);
                            }
                        }
                    }
                    foreach (Facture_Fournisseur_Contenu item in toRemove)
                    {
                        try
                        {
                            ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Remove(item);
                        }
                        catch (Exception) { }
                        try
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }
                        catch (Exception) { }
                    }
                    ((Bon_Livraison)this._dataGridDroitBL.SelectedItem).Facture_Fournisseur1 = null;
                }
                else
                {
                    foreach (Bon_Livraison ligne in this._dataGridDroitBL.SelectedItems.OfType<Bon_Livraison>())
                    {
                        ObservableCollection<Facture_Fournisseur_Contenu> toRemove = new ObservableCollection<Facture_Fournisseur_Contenu>();
                        foreach (Facture_Fournisseur_Contenu item in ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu)
                        {
                            if (item.Bon_Livraison_Contenu_Commande_Supplementaire1 != null)
                            {
                                if (item.Bon_Livraison_Contenu_Commande_Supplementaire1.Bon_Livraison1 == ligne)
                                {
                                    toRemove.Add(item);
                                }
                            }
                            if (item.Bon_Livraison_Contenu_Commande1 != null)
                            {
                                if (item.Bon_Livraison_Contenu_Commande1.Bon_Livraison1 == ligne)
                                {
                                    toRemove.Add(item);
                                }
                            }
                        }
                        foreach (Facture_Fournisseur_Contenu item in toRemove)
                        {
                            try
                            {
                                ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Remove(item);
                            }
                            catch (Exception) { }
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }
                            catch (Exception) { }
                        }
                        ligne.Facture_Fournisseur1 = null;
                    }
                }
            }
            this.SelectionAutoBL();
            _verrouillerFacture();
            this.calculChaqueLigne();
            this.calculResume();
        }

        #endregion

        #region boutons vidoir / dévidoir CCF

        private void _buttonGaucheDroiteCommande_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridCommandeGauche.SelectedItem != null)
            {
                if (this._dataGridCommandeGauche.SelectedItems.Count == 1)
                {
                    Facture_Fournisseur_Contenu temp = new Facture_Fournisseur_Contenu();
                    temp.Contenu_Commande_Fournisseur1 = ((Contenu_Commande_Fournisseur)this._dataGridCommandeGauche.SelectedItem);
                    temp.Designation = ((Contenu_Commande_Fournisseur)this._dataGridCommandeGauche.SelectedItem).Designation;
                    temp.Reference_Fournisseur = ((Contenu_Commande_Fournisseur)this._dataGridCommandeGauche.SelectedItem).Reference;
                    temp.Qte_Commandee = ((Contenu_Commande_Fournisseur)this._dataGridCommandeGauche.SelectedItem).Quantite;
                    temp.Qte_Livree = 0;
                    temp.Qte_Facturee = ((Contenu_Commande_Fournisseur)this._dataGridCommandeGauche.SelectedItem).QuantiteRestante;
                    temp.Prix_Unitaire_Commande_HT = ((Contenu_Commande_Fournisseur)this._dataGridCommandeGauche.SelectedItem).Prix_Unitaire;
                    temp.Prix_Unitaire_Remise_HT = ((Contenu_Commande_Fournisseur)this._dataGridCommandeGauche.SelectedItem).Prix_Remise;
                    temp.Prix_Unitaire_Facture_HT = ((Contenu_Commande_Fournisseur)this._dataGridCommandeGauche.SelectedItem).Prix_Remise;
                    temp.Prix_Total_Facture_HT = 0;
                    temp.Diff = 0;
                    temp.Affaire1 = ((Contenu_Commande_Fournisseur)this._dataGridCommandeGauche.SelectedItem).Commande_Fournisseur1.Affaire1;
                    ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Add(temp);

                    Facture_Fournisseur_Contenu_Commande_Fournisseur tmp = new Facture_Fournisseur_Contenu_Commande_Fournisseur();
                    tmp.Contenu_Commande_Fournisseur1 = (Contenu_Commande_Fournisseur)this._dataGridCommandeGauche.SelectedItem;
                    tmp.Facture_Fournisseur1 = (Facture_Fournisseur)this.DataContext;
                    //((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu_Commande_Fournisseur.Add();
                }
                else
                {
                    foreach (Contenu_Commande_Fournisseur ligne in this._dataGridCommandeGauche.SelectedItems.OfType<Contenu_Commande_Fournisseur>())
                    {
                        Facture_Fournisseur_Contenu temp = new Facture_Fournisseur_Contenu();
                        temp.Contenu_Commande_Fournisseur1 = ligne;
                        temp.Designation = ligne.Designation;
                        temp.Reference_Fournisseur = ligne.Reference;
                        temp.Qte_Commandee = ligne.Quantite;
                        temp.Qte_Livree = 0;
                        temp.Qte_Facturee = ligne.QuantiteRestante;
                        temp.Prix_Unitaire_Commande_HT = ligne.Prix_Unitaire;
                        temp.Prix_Unitaire_Remise_HT = ligne.Prix_Remise;
                        temp.Prix_Unitaire_Facture_HT = ligne.Prix_Remise;
                        temp.Prix_Total_Facture_HT = 0;
                        temp.Diff = 0;
                        temp.Affaire1 = ligne.Commande_Fournisseur1.Affaire1;
                        ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Add(temp);

                        Facture_Fournisseur_Contenu_Commande_Fournisseur tmp = new Facture_Fournisseur_Contenu_Commande_Fournisseur();
                        tmp.Contenu_Commande_Fournisseur1 = ligne;
                        tmp.Facture_Fournisseur1 = (Facture_Fournisseur)this.DataContext;
                    }
                }
            }
            this.SelectionAutoLigneCommande();
            _verrouillerFacture();
            this.calculChaqueLigne();
            this.calculResume();
        }

        private void _buttonDroiteGaucheCommande_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridDroitCommande.SelectedItem != null)
            {
                if (this._dataGridDroitCommande.SelectedItems.Count == 1)
                {
                    ObservableCollection<Facture_Fournisseur_Contenu> toRemove = new ObservableCollection<Facture_Fournisseur_Contenu>();
                    foreach (Facture_Fournisseur_Contenu item in ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu)
                    {
                        if (item.Contenu_Commande_Fournisseur1 != null)
                        {
                            if (item.Contenu_Commande_Fournisseur1 == ((Facture_Fournisseur_Contenu_Commande_Fournisseur)this._dataGridDroitCommande.SelectedItem).Contenu_Commande_Fournisseur1)
                            {
                                toRemove.Add(item);
                            }
                        }
                    }
                    foreach (Facture_Fournisseur_Contenu item in toRemove)
                    {
                        try
                        {
                            ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Remove(item);
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }
                        catch (Exception) { }
                        try
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                            ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Remove(item);
                        }
                        catch (Exception) { }
                    }
                    Facture_Fournisseur_Contenu_Commande_Fournisseur tmp = (Facture_Fournisseur_Contenu_Commande_Fournisseur)this._dataGridDroitCommande.SelectedItem;
                    tmp.Contenu_Commande_Fournisseur1 = null;
                    tmp.Facture_Fournisseur1 = null;
                    try
                    {
                        ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu_Commande_Fournisseur.Remove(tmp);
                        ((App)App.Current).mySitaffEntities.Detach(tmp);
                    }
                    catch (Exception) { }
                    try
                    {

                        ((App)App.Current).mySitaffEntities.Detach(tmp);
                        ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu_Commande_Fournisseur.Remove(tmp);
                    }
                    catch (Exception) { }
                }
                else
                {
                    foreach (Facture_Fournisseur_Contenu_Commande_Fournisseur ligne in this._dataGridDroitCommande.SelectedItems.OfType<Facture_Fournisseur_Contenu_Commande_Fournisseur>())
                    {
                        ObservableCollection<Facture_Fournisseur_Contenu> toRemove = new ObservableCollection<Facture_Fournisseur_Contenu>();
                        foreach (Facture_Fournisseur_Contenu item in ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu)
                        {
                            if (item.Contenu_Commande_Fournisseur1 != null)
                            {
                                if (item.Contenu_Commande_Fournisseur1 == ligne.Contenu_Commande_Fournisseur1)
                                {
                                    toRemove.Add(item);
                                }
                            }
                        }
                        foreach (Facture_Fournisseur_Contenu item in toRemove)
                        {
                            try
                            {
                                ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Remove(item);
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }
                            catch (Exception) { }
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                                ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Remove(item);
                            }
                            catch (Exception) { }
                        }
                        Facture_Fournisseur_Contenu_Commande_Fournisseur tmp = ligne;
                        tmp.Contenu_Commande_Fournisseur1 = null;
                        tmp.Facture_Fournisseur1 = null;
                        try
                        {
                            ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu_Commande_Fournisseur.Remove(tmp);
                            ((App)App.Current).mySitaffEntities.Detach(tmp);
                        }
                        catch (Exception) { }
                        try
                        {

                            ((App)App.Current).mySitaffEntities.Detach(tmp);
                            ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu_Commande_Fournisseur.Remove(tmp);
                        }
                        catch (Exception) { }
                    }
                }
            }
            this.SelectionAutoLigneCommande();
            _verrouillerFacture();
            this.calculChaqueLigne();
            this.calculResume();
        }

        #endregion

        #region boutons vidoir / dévidoir proforma

        private void _buttonGaucheDroiteProforma_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._dataGridProformaGauche.SelectedItem != null)
            {
                if (this._dataGridProformaGauche.SelectedItems.Count == 1)
                {

                    ((Facture_Proforma)this._dataGridProformaGauche.SelectedItem).Facture_Fournisseur1 = (Facture_Fournisseur)this.DataContext;
                }
            }
            this.SelectionAutoProforma();
            _verrouillerFacture();
            this.calculChaqueLigne();
            this.calculResume();
        }

        private void _buttonDroiteGaucheProforma_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._dataGridDroitProforma.SelectedItem != null)
            {
                if (this._dataGridDroitProforma.SelectedItems.Count == 1)
                {

                    ((Facture_Proforma)this._dataGridDroitProforma.SelectedItem).Facture_Fournisseur1 = null;
                }
            }
            this.SelectionAutoProforma();
            _verrouillerFacture();
            this.calculChaqueLigne();
            this.calculResume();
        }

        #endregion

        #region Boutons Masquer / Afficher

        private void _buttonMasquerHaut_Click(object sender, RoutedEventArgs e)
        {
            if (this._firstLineToHideTop.Height != 0 && this._secondLineToHideTop.Height != 0)
            {
                this._firstLineToHideTop.Height = 0;
                this._secondLineToHideTop.Height = 0;
                this._buttonMasquerHaut.Content = "Afficher le haut";
            }
            else
            {
                this._firstLineToHideTop.Height = double.NaN;
                this._secondLineToHideTop.Height = double.NaN;
                this._buttonMasquerHaut.Content = "Masquer le haut";
            }
        }

        private void _buttonMasquerBas_Click(object sender, RoutedEventArgs e)
        {
            if (this._partieBasse.Height != 0)
            {
                this._partieBasse.Height = 0;
                this._buttonMasquerBas.Content = "Afficher le bas";
            }
            else
            {
                this._partieBasse.Height = double.NaN;
                this._buttonMasquerBas.Content = "Masquer le bas";
            }
        }

        #endregion

        #region Boutons Ok / Annuler

        /// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Calculer();
            if (this.VerificationChamps())
            {
                if (((Facture_Fournisseur)this.DataContext).Diff != 0)
                {
                    if (((Facture_Fournisseur)this.DataContext).Avoir_Facture_Fournisseur.Count() != 0)
                    {
                        try
                        {
                            if (((Facture_Fournisseur)this.DataContext).Avoir_Facture_Fournisseur.First().Montant != ((Facture_Fournisseur)this.DataContext).Diff)
                            {
                                if (MessageBox.Show("Souhaitez-vous modifier l'avoir qui avait déjà été mis cette facture ? L'ancien avoir était d'un montant de : " + ((Facture_Fournisseur)this.DataContext).Avoir_Facture_Fournisseur.First().Montant.ToString(System.Globalization.CultureInfo.CurrentCulture) + "€ et vous avez maintenant un avoir d'un montant de " + ((Facture_Fournisseur)this.DataContext).Diff.ToString(System.Globalization.CultureInfo.CurrentCulture) + "€.", "Demande de modification d'avoir", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                {
                                    ((Facture_Fournisseur)this.DataContext).Avoir_Facture_Fournisseur.First().Montant = ((Facture_Fournisseur)this.DataContext).Diff;

                                    this.DialogResult = true;
                                    this.Close();
                                }
                                else
                                {
                                    this.DialogResult = true;
                                    this.Close();
                                }
                            }
                            else
                            {
                                this.DialogResult = true;
                                this.Close();
                            }
                        }
                        catch (Exception)
                        {
                            this.DialogResult = true;
                            this.Close();
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Souhaitez-vous réaliser un avoir pour cette facture d'un montant de " + ((Facture_Fournisseur)this.DataContext).Diff.ToString(System.Globalization.CultureInfo.CurrentCulture) + "€, correspondant à la différence ?", "Demande d'avoir", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            Avoir_Facture_Fournisseur tmp = new Avoir_Facture_Fournisseur();
                            tmp.Facture_Fournisseur1 = (Facture_Fournisseur)this.DataContext;
                            tmp.Fournisseur1 = ((Facture_Fournisseur)this.DataContext).Fournisseur1;
                            tmp.Montant = ((Facture_Fournisseur)this.DataContext).Diff;

                            this.DialogResult = true;
                            this.Close();
                        }
                        else
                        {
                            this.DialogResult = true;
                            this.Close();
                        }
                    }
                }
                else
                {
                    this.DialogResult = true;
                    this.Close();
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

        #region Boutons des comboBox

        private void LookAffaire_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._comboBoxFilterAffaire.SelectedItem != null)
            {
                ListeAffaireControl listeAffaireControl = new ListeAffaireControl();
                //listeAffaireControl.Look(((Affaire)this._comboBoxFilterAffaire.SelectedItem));
            }
        }

        private void LookCommande_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._comboBoxFilterCommandeBL.SelectedItem != null)
            {
                ListeCommandeFournisseurControl listeCommandeFournisseurControl = new ListeCommandeFournisseurControl();
                listeCommandeFournisseurControl.Look((Commande_Fournisseur)this._comboBoxFilterCommandeBL.SelectedItem);
            }
        }

        private void LookCommande1_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._comboBoxFilterCommande.SelectedItem != null)
            {
                ListeCommandeFournisseurControl listeCommandeFournisseurControl = new ListeCommandeFournisseurControl();
                listeCommandeFournisseurControl.Look((Commande_Fournisseur)this._comboBoxFilterCommande.SelectedItem);
            }
        }

        #endregion

        #region Boutons contenu

        private void _ButtonColler_Click_1(object sender, RoutedEventArgs e)
        {
            CopierColler ClassPaste = new CopierColler();
            ObservableCollection<Facture_Fournisseur_Contenu> listToAdd = ClassPaste.PasteDataFactureFournisseurWindow();
            if (listToAdd != null)
            {
                foreach (Facture_Fournisseur_Contenu ffc in listToAdd)
                {
                    ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Add(ffc);
                }
                this._DataGridContenu.Items.Refresh();
                this.Calculer();
            }
        }

        private void _ButtonSupprimer_Click_1(object sender, RoutedEventArgs e)
        {
            this.deleteLigne();
        }

        private void _ButtonCalculer_Click(object sender, RoutedEventArgs e)
        {
            this.Calculer();
        }

        #region Copier Vers le Bas

        #region PCT

        public void copierPCTverslebas()
        {
            Plan_Comptable_Tva temp = null;
            int i = 0;
            foreach (Facture_Fournisseur_Contenu cont in this._DataGridContenu.Items.OfType<Facture_Fournisseur_Contenu>())
            {
                if (i == 0)
                {
                    temp = cont.Plan_Comptable_Tva1;
                }
                cont.Plan_Comptable_Tva1 = temp;
                i++;
            }
            this._DataGridContenu.Items.Refresh();
            this.calculChaqueLigne();
            this.calculResume();
        }

        private void _ButtonCopierPCTBas_click(object sender, RoutedEventArgs e)
        {
            this.copierPCTverslebas();
        }

        #endregion

        #region PCI

        public void copierPCIverslebas()
        {
            Plan_Comptable_Imputation temp = null;
            int i = 0;
            foreach (Facture_Fournisseur_Contenu cont in this._DataGridContenu.Items.OfType<Facture_Fournisseur_Contenu>())
            {
                if (i == 0)
                {
                    temp = cont.Plan_Comptable_Imputation1;
                }
                cont.Plan_Comptable_Imputation1 = temp;
                i++;
            }
            this._DataGridContenu.Items.Refresh();
            this.calculChaqueLigne();
            this.calculResume();
        }

        private void _ButtonCopierPCIBas_click(object sender, RoutedEventArgs e)
        {
            this.copierPCIverslebas();
        }

        #endregion

        #region Affaire

        public void copierAffaireverslebas()
        {
            Affaire temp = null;
            int i = 0;
            foreach (Facture_Fournisseur_Contenu cont in this._DataGridContenu.Items.OfType<Facture_Fournisseur_Contenu>())
            {
                if (i == 0)
                {
                    temp = cont.Affaire1;
                }
                cont.Affaire1 = temp;
                i++;
            }
            this._DataGridContenu.Items.Refresh();
            this.calculChaqueLigne();
            this.calculResume();
        }

        private void _ButtonCopierAffaireBas_Click(object sender, RoutedEventArgs e)
        {
            this.copierAffaireverslebas();
        }

        #endregion

        #endregion

        #endregion

        #region Echeances

        private void _buttonGaucheDroite_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._listBoxCondReglementGauche.SelectedItem != null && this._listBoxCondReglementGauche.SelectedItems.Count == 1)
            {
                Facture_Fournisseur_Condition_Reglement temp = new Facture_Fournisseur_Condition_Reglement();
                temp.Condition_Reglement1 = (Condition_Reglement)this._listBoxCondReglementGauche.SelectedItem;
                ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Condition_Reglement.Add(temp);
                this._dataGridFactureFournisseurConditionReglement.Items.Refresh();
            }
        }

        private void _buttonDroiteGauche_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridFactureFournisseurConditionReglement.SelectedItem != null && this._dataGridFactureFournisseurConditionReglement.SelectedItems.Count == 1)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Facture_Fournisseur_Condition_Reglement.DeleteObject((Facture_Fournisseur_Condition_Reglement)this._dataGridFactureFournisseurConditionReglement.SelectedItem);
                    
                }
                catch (Exception)
                {
                    try
                    {
                        ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Condition_Reglement.Remove((Facture_Fournisseur_Condition_Reglement)this._dataGridFactureFournisseurConditionReglement.SelectedItem);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Condition_Reglement.Remove((Facture_Fournisseur_Condition_Reglement)this._dataGridFactureFournisseurConditionReglement.SelectedItem);
                        }
                        catch (Exception)
                        {
                            ((App)App.Current).mySitaffEntities.Detach((Facture_Fournisseur_Condition_Reglement)this._dataGridFactureFournisseurConditionReglement.SelectedItem);
                        }
                    }
                }
            }
            //this._listBoxCondReglementGauche.Items.Refresh();
            this._dataGridFactureFournisseurConditionReglement.Items.Refresh();
        }

        private void _calculerEcheances_Click_1(object sender, RoutedEventArgs e)
        {
            this.Calculer();
        }

        #endregion

        #region Litiges

        private void _buttonAjouterLitige_Click(object sender, RoutedEventArgs e)
        {
            LitigeFactureWindow litigeFactureWindow = new LitigeFactureWindow();
            litigeFactureWindow.DataContext = new Litige_Facture_Fournisseur();
            ((Litige_Facture_Fournisseur)litigeFactureWindow.DataContext).Facture_Fournisseur1 = (Facture_Fournisseur)this.DataContext;

            bool? dialogResult = litigeFactureWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {

            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Litige_Facture_Fournisseur)litigeFactureWindow.DataContext);
                }
                catch (Exception)
                {

                }
                this._dataGridLitigeFacture_SansBL.Items.Refresh();
            }
        }

        private void _buttonModifierLitige_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridLitigeFacture_SansBL.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner un litige à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridLitigeFacture_SansBL.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'un litige à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridLitigeFacture_SansBL.SelectedItem != null)
            {
                LitigeFactureWindow litigeFactureWindow = new LitigeFactureWindow();
                litigeFactureWindow.DataContext = (Litige_Facture_Fournisseur)this._dataGridLitigeFacture_SansBL.SelectedItem;

                bool? dialogResult = litigeFactureWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    this._dataGridLitigeFacture_SansBL.Items.Refresh();
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Litige_Facture_Fournisseur)litigeFactureWindow.DataContext);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void _buttonSupprimerLitige_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridLitigeFacture_SansBL.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un litige à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridLitigeFacture_SansBL.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les litiges à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ((Facture_Fournisseur)this.DataContext).Litige_Facture_Fournisseur.Remove((Litige_Facture_Fournisseur)this._dataGridLitigeFacture_SansBL.SelectedItem);
                    this._dataGridLitigeFacture_SansBL.Items.Refresh();
                }
            }
        }

        #endregion

        #region Ajouter un bl

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BonLivraisonWindow bonlivraisonWindow = new BonLivraisonWindow();

            //Création de l'objet temporaire
            Bon_Livraison tmp = new Bon_Livraison();

            //Mise de l'objet temporaire dans le datacontext
            bonlivraisonWindow.DataContext = tmp;

            SelectionTypeBL selectionTypeBL = new SelectionTypeBL();
            bool? dialogDemandeTypeBL = selectionTypeBL.ShowDialog();

            if (dialogDemandeTypeBL.HasValue && dialogDemandeTypeBL.Value == true)
            {
                if (selectionTypeBL.affaire == null && selectionTypeBL.commande_fournisseur == null)
                {
                    //Stock ou Divers ?
                    SelectionStockDiversBLWindow selectionStockDiversBLWindow = new SelectionStockDiversBLWindow();
                    bool? dialogStockDivers = selectionStockDiversBLWindow.ShowDialog();

                    if (dialogStockDivers.HasValue && dialogStockDivers.Value == true)
                    {
                        tmp.StockAtelier = selectionStockDiversBLWindow.stock;
                        tmp.Divers = selectionStockDiversBLWindow.divers;
                    }
                    else
                    {
                    }
                }
                if (selectionTypeBL.affaire == null && selectionTypeBL.commande_fournisseur != null)
                {
                    tmp.StockAtelier = selectionTypeBL.commande_fournisseur.Stock;
                    tmp.Divers = selectionTypeBL.commande_fournisseur.Divers;
                }
                if (selectionTypeBL.commande_fournisseur != null)
                {
                    tmp.Fournisseur1 = selectionTypeBL.commande_fournisseur.Fournisseur1;
                    if (selectionTypeBL.commande_fournisseur.Fournisseur1 != null)
                    {
                        bonlivraisonWindow._comboBoxFournisseur.IsEnabled = false;
                    }
                }
                tmp.Affaire1 = selectionTypeBL.affaire;
                tmp.Commande_Fournisseur1 = selectionTypeBL.commande_fournisseur;
                tmp.Salarie1 = selectionTypeBL.salarie;

                //booléen nullable vrai ou faux ou null
                bool? dialogResult = bonlivraisonWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.AddToBon_Livraison((Bon_Livraison)bonlivraisonWindow.DataContext);
                    }
                    catch (Exception) { }
                }
                else
                {
                    try
                    {
                        //On détache tous les élements liés au bl Bon_Livraison_Contenu_Commande_Supplementaire
                        ObservableCollection<Bon_Livraison_Contenu_Commande_Supplementaire> toRemove = new ObservableCollection<Bon_Livraison_Contenu_Commande_Supplementaire>();
                        foreach (Bon_Livraison_Contenu_Commande_Supplementaire item in ((Bon_Livraison)bonlivraisonWindow.DataContext).Bon_Livraison_Contenu_Commande_Supplementaire)
                        {
                            toRemove.Add(item);
                        }
                        foreach (Bon_Livraison_Contenu_Commande_Supplementaire item in toRemove)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }

                        //On détache tous les élements liés au bl Bon_Livraison_Contenu_Commande
                        ObservableCollection<Bon_Livraison_Contenu_Commande> toRemove1 = new ObservableCollection<Bon_Livraison_Contenu_Commande>();
                        foreach (Bon_Livraison_Contenu_Commande item in ((Bon_Livraison)bonlivraisonWindow.DataContext).Bon_Livraison_Contenu_Commande)
                        {
                            toRemove1.Add(item);
                        }
                        foreach (Bon_Livraison_Contenu_Commande item in toRemove1)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }

                        //On détache le bl
                        ((App)App.Current).mySitaffEntities.Detach((Bon_Livraison)bonlivraisonWindow.DataContext);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Bon_Livraison)bonlivraisonWindow.DataContext);
                }
                catch (Exception) { }
            }            
            this.SelectionAutoBL();
        }

        #endregion

        #endregion

        #region Vérifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TabTotal())
            {
                verif = false;
            }
            if (!Verif_EnTete())
            {
                verif = false;
            }
            if (!Verif_Echeances())
            {
                verif = false;
            }

            return verif;
        }

        #region En-Tête

        private bool Verif_EnTete()
        {
            bool verif = true;

            if (!this.Verif_textBoxNumeroFacture())
            {
                verif = false;
            }
            if (!this.Verif_textBoxNumeroPieceComptable())
            {
                verif = false;
            }
            if (!this.Verif_DatePickerDateFacture())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxFournisseur())
            {
                verif = false;
            }

            return verif;
        }

        #region NumeroFacture

        private bool Verif_textBoxNumeroFacture()
        {
            bool verif = true;

            if (this._textBoxNumeroFacture.Text.Trim().Length <= 0)
            {
                verif = false;
            }
            else
            {
                if (this._comboBoxFournisseur.SelectedItem != null)
                {
                    foreach (Facture_Fournisseur item in ((Fournisseur)this._comboBoxFournisseur.SelectedItem).Facture_Fournisseur)
                    {
                        if (item != (Facture_Fournisseur)this.DataContext)
                        {
                            if (item.Numero.ToLower() == this._textBoxNumeroFacture.Text.Trim().ToLower())
                            {
                                verif = false;
                            }
                        }
                    }
                }
                else
                {
                    verif = true;
                }
            }

			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._textBoxNumeroFacture, this._textBlockNumeroFacture,verif);

            return verif;
        }

        private void _textBoxNumeroFacture_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_textBoxNumeroFacture();
        }

        #endregion

        #region DateFacture

        private bool Verif_DatePickerDateFacture()
        {
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._DatePickerDateFacture, this._textBlockDateFacture);
        }

        private void _DatePickerDateFacture_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerDateFacture();
        }

        #endregion

        #region NuméroPieceComptable

        private bool Verif_textBoxNumeroPieceComptable()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxNumeroPieceComptable, this._textBlockNumeroPieceComptable);
        }

        private void _textBoxNumeroPieceComptable_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_textBoxNumeroPieceComptable();
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
            this.Verif_textBoxNumeroFacture();
            this.SelectionAutoBL();
            this.SelectionAutoProforma();
            this._verrouillerFacture();
            this.triAutoListeCommandes();
            this.triAutoListeAffaires();
        }

        #endregion

        #endregion

        #region Echéances

        private bool Verif_Echeances()
        {
            bool verif = true;
            double aTester = 0;

            foreach (Facture_Fournisseur_Condition_Reglement item in ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Condition_Reglement)
            {
                if (item.Pourcentage == null || item.Date_Echeance == null)
                {
                    verif = false;
                }
                if (item.Pourcentage != null)
                {
                    aTester = aTester + double.Parse(item.Pourcentage.ToString());
                }
            }
            if (aTester < 99.999999 || aTester > 100.000001)
            {
                verif = false;
            }
			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridFactureFournisseurConditionReglement, verif);
			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemEcheances, verif);

            return verif;
        }

        #endregion

        #region TabTotal

        private bool Verif_TabTotal()
        {
            bool verif = true;

            if (!this.Verif_textBoxTotalTheorique())
            {
                verif = false;
            }
            if (!this.Verif_textBoxEcartTva())
            {
                verif = false;
            }

            return verif;
        }

        #region Montant Theorique

        private bool Verif_textBoxTotalTheorique()
        {
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxMontantHTTheorique, this._textBlockMontantHTTheorique, this._textBoxMontantHT);
        }

        private void _textBoxMontant_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Calculer();
            this.Verif_textBoxTotalTheorique();
        }

        #endregion

        #region Ecart Tva

        private bool Verif_textBoxEcartTva()
        {
			if (this._textBoxEcartTva.Text.Trim().Length == 0)
			{
                this._textBoxEcartTva.Text = "0";
			}
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxEcartTva, this._textBlockEcartTva);
        }

        private void _textBoxEcartTva_LostFocus_1(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxEcartTva();
            this.Calculer();
        }

        #endregion

        #endregion

        #endregion

        #region Evénements

        #region Filtre Entreprise Mere

        private void _comboBoxFilterEntrepriseMere_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (this._comboBoxFilterEntrepriseMere.SelectedItem != null)
            {
                this.listPlanComptableImputation = new ObservableCollection<Plan_Comptable_Imputation>(((App)App.Current).mySitaffEntities.Plan_Comptable_Imputation.Where(pci => pci.Entreprise_Mere.Where(em => em.Identifiant == ((Entreprise_Mere)this._comboBoxFilterEntrepriseMere.SelectedItem).Identifiant).Count() > 0).OrderBy(pci => pci.Numero));
                this.listPlanComptableTva = new ObservableCollection<Plan_Comptable_Tva>(((App)App.Current).mySitaffEntities.Plan_Comptable_Tva.Where(pct => pct.Entreprise_Mere.Where(em => em.Identifiant == ((Entreprise_Mere)this._comboBoxFilterEntrepriseMere.SelectedItem).Identifiant).Count() > 0).OrderBy(pct => pct.Numero));
                foreach (Facture_Fournisseur_Contenu cont in this._DataGridContenu.Items.OfType<Facture_Fournisseur_Contenu>())
                {
                    if (cont.Plan_Comptable_Imputation1 != null)
                    {
                        if (this.listPlanComptableImputation.Where(pci => pci.Identifiant == cont.Plan_Comptable_Imputation1.Identifiant).Count() == 0)
                        {
                            this.listPlanComptableImputation.Add(cont.Plan_Comptable_Imputation1);
                        }
                    }
                    if (cont.Plan_Comptable_Tva1 != null)
                    {
                        if (this.listPlanComptableTva.Where(pct => pct.Identifiant == cont.Plan_Comptable_Tva1.Identifiant).Count() == 0)
                        {
                            this.listPlanComptableTva.Add(cont.Plan_Comptable_Tva1);
                        }
                    }
                }
            }
            else
            {
                this.listPlanComptableImputation = new ObservableCollection<Plan_Comptable_Imputation>(((App)App.Current).mySitaffEntities.Plan_Comptable_Imputation.OrderBy(pci => pci.Numero));
                this.listPlanComptableTva = new ObservableCollection<Plan_Comptable_Tva>(((App)App.Current).mySitaffEntities.Plan_Comptable_Tva.OrderBy(pct => pct.Numero));
            }
        }

        #endregion

        #region KeyUp

        private void _textBoxEcartTva_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_TextChanged((TextBox)e.OriginalSource, e);
        }

        private void _dataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Qté facturée":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "P.U facturé HT":
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

        private void _dataGridFactureFournisseurConditionReglement_KeyUp_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Pourcentage (%)":
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

        private void _textBox_KeyUp(object sender, KeyEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
        }

        #endregion

        #region modifs comboBox datagrid contenu

        private void lesPlanComptablesTva_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((ComboBox)sender).SelectedItem != null)
                {
                    ((Facture_Fournisseur_Contenu)((ComboBox)sender).DataContext).Plan_Comptable_Tva1 = (Plan_Comptable_Tva)((ComboBox)sender).SelectedItem;
                }
                else
                {
                    ((Facture_Fournisseur_Contenu)((ComboBox)sender).DataContext).Plan_Comptable_Tva1 = null;
                }
            }
            catch (Exception)
            {
            }

            this.calculChaqueLigne();
            this.calculResume();
        }

        private void lesPlanComptablesImputation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((ComboBox)sender).SelectedItem != null)
                {
                    ((Facture_Fournisseur_Contenu)((ComboBox)sender).DataContext).Plan_Comptable_Imputation1 = (Plan_Comptable_Imputation)((ComboBox)sender).SelectedItem;
                }
                else
                {
                    ((Facture_Fournisseur_Contenu)((ComboBox)sender).DataContext).Plan_Comptable_Imputation1 = null;
                }
            }
            catch (Exception)
            {
            }

            this.calculChaqueLigne();
            this.calculResume();
        }

        private void lesAffaires_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((ComboBox)sender).SelectedItem != null)
                {
                    ((Facture_Fournisseur_Contenu)((ComboBox)sender).DataContext).Affaire1 = (Affaire)((ComboBox)sender).SelectedItem;
                    foreach (Commande_Fournisseur item in ((Affaire)((ComboBox)sender).SelectedItem).Commande_Fournisseur.Where(com => com.Fournisseur1.Identifiant == ((Fournisseur)this._comboBoxFournisseur.SelectedItem).Identifiant))
                    {
                        if (this.listCommandeContenu.Where(pci => pci.Identifiant == item.Identifiant).Count() == 0)
                        {
                            this.listCommandeContenu.Add(item);
                        }
                    }
                    this.listCommandeContenu = new ObservableCollection<Commande_Fournisseur>(this.listCommandeContenu.OrderBy(com => com.Numero));
                }
                else
                {
                    ((Facture_Fournisseur_Contenu)((ComboBox)sender).DataContext).Affaire1 = null;
                }
            }
            catch (Exception)
            {
            }

            this.calculChaqueLigne();
            this.calculResume();
        }

        private void lesCommandes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.calculChaqueLigne();
            this.calculResume();
        }

        #endregion

        #endregion

        #region fonctions

        private void remplissageComboBoxCommandesContenu()
        {
            ObservableCollection<Commande_Fournisseur> listToPut = new ObservableCollection<Commande_Fournisseur>();
            foreach (Facture_Fournisseur_Contenu cont in this._DataGridContenu.Items.OfType<Facture_Fournisseur_Contenu>())
            {
                if (cont.Contenu_Commande_Fournisseur1 != null)
                {
                    if (cont.Contenu_Commande_Fournisseur1.Commande_Fournisseur1 != null)
                    {
                        if (!listToPut.Contains(cont.Contenu_Commande_Fournisseur1.Commande_Fournisseur1))
                        {
                            listToPut.Add(cont.Contenu_Commande_Fournisseur1.Commande_Fournisseur1);
                        }
                    }
                }
                if (cont.Bon_Livraison_Contenu_Commande1 != null)
                {
                    if (cont.Bon_Livraison_Contenu_Commande1.Bon_Livraison1 != null)
                    {
                        if (cont.Bon_Livraison_Contenu_Commande1.Bon_Livraison1.Commande_Fournisseur1 != null)
                        {
                            if (!listToPut.Contains(cont.Bon_Livraison_Contenu_Commande1.Bon_Livraison1.Commande_Fournisseur1))
                            {
                                listToPut.Add(cont.Bon_Livraison_Contenu_Commande1.Bon_Livraison1.Commande_Fournisseur1);
                            }
                        }
                    }
                }
                if (cont.Bon_Livraison_Contenu_Commande_Supplementaire1 != null)
                {
                    if (cont.Bon_Livraison_Contenu_Commande_Supplementaire1.Bon_Livraison1 != null)
                    {
                        if (cont.Bon_Livraison_Contenu_Commande_Supplementaire1.Bon_Livraison1.Commande_Fournisseur1 != null)
                        {
                            if (!listToPut.Contains(cont.Bon_Livraison_Contenu_Commande_Supplementaire1.Bon_Livraison1.Commande_Fournisseur1))
                            {
                                listToPut.Add(cont.Bon_Livraison_Contenu_Commande_Supplementaire1.Bon_Livraison1.Commande_Fournisseur1);
                            }
                        }
                    }
                }
                if (cont.Affaire1 != null)
                {
                    foreach (Commande_Fournisseur item in cont.Affaire1.Commande_Fournisseur.Where(com => com.Fournisseur1.Identifiant == ((Fournisseur)this._comboBoxFournisseur.SelectedItem).Identifiant))
                    {
                        if (listToPut.Where(pci => pci.Identifiant == item.Identifiant).Count() == 0)
                        {
                            listToPut.Add(item);
                        }
                    }
                }
            }
            this.listCommandeContenu = new ObservableCollection<Commande_Fournisseur>(listToPut.OrderBy(com => com.Numero));
        }

        #region calculs

        private void calculChaqueLigne()
        {
            double totalAvoir = 0;
            double totalHT = 0;
            double totalHTTheorique = 0;
            double totalTTC = 0;
            this.remplissageComboBoxCommandesContenu();
            foreach (Facture_Fournisseur_Contenu cont in this._DataGridContenu.Items.OfType<Facture_Fournisseur_Contenu>())
            {
                if (cont.Contenu_Commande_Fournisseur1 != null)
                {
                    if (cont.Contenu_Commande_Fournisseur1.Commande_Fournisseur1 != null)
                    {
                        cont.Commande_Fournisseur1 = cont.Contenu_Commande_Fournisseur1.Commande_Fournisseur1;
                    }
                }
                if (cont.Bon_Livraison_Contenu_Commande1 != null)
                {
                    if (cont.Bon_Livraison_Contenu_Commande1.Bon_Livraison1 != null)
                    {
                        if (cont.Bon_Livraison_Contenu_Commande1.Bon_Livraison1.Commande_Fournisseur1 != null)
                        {
                            cont.Commande_Fournisseur1 = cont.Bon_Livraison_Contenu_Commande1.Bon_Livraison1.Commande_Fournisseur1;
                        }
                    }
                }
                if (cont.Bon_Livraison_Contenu_Commande_Supplementaire1 != null)
                {
                    if (cont.Bon_Livraison_Contenu_Commande_Supplementaire1.Bon_Livraison1 != null)
                    {
                        if (cont.Bon_Livraison_Contenu_Commande_Supplementaire1.Bon_Livraison1.Commande_Fournisseur1 != null)
                        {
                            cont.Commande_Fournisseur1 = cont.Bon_Livraison_Contenu_Commande_Supplementaire1.Bon_Livraison1.Commande_Fournisseur1;
                        }
                    }
                }

                if (cont.Plan_Comptable_Tva1 != null)
                {
                    try
                    {
                        double totalReel = cont.Prix_Unitaire_Facture_HT * cont.Qte_Facturee;
                        double totalTheorique = cont.Prix_Unitaire_Remise_HT;
                        if (cont.Bon_Livraison_Contenu_Commande1 != null || cont.Bon_Livraison_Contenu_Commande_Supplementaire1 != null)
                        {
                            totalTheorique = totalTheorique * cont.Qte_Livree;
                        }
                        else if (cont.Contenu_Commande_Fournisseur1 != null)
                        {
                            totalTheorique = totalTheorique * cont.Qte_Commandee;
                        }
                        else
                        {
                            totalTheorique = totalTheorique * 0;
                        }
                        //cont.Diff = (cont.Prix_Unitaire_Remise_HT - cont.Prix_Unitaire_Facture_HT) * cont.Qte_Facturee;
                        cont.Diff = totalReel - totalTheorique;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        if (cont.Plan_Comptable_Tva1.AutoLiquidation == true)
                        {
                            cont.Montant_TTC = cont.Prix_Unitaire_Facture_HT * cont.Qte_Facturee;
                        }
                        else
                        {
                            cont.Montant_TTC = ((1 + (cont.Plan_Comptable_Tva1.Tva1.Taux / 100)) * cont.Prix_Unitaire_Facture_HT * cont.Qte_Facturee);
                        }
                    }
                    catch (Exception) { }
                    try
                    {
                        if (cont.Avoir == true)
                        {
                            totalAvoir = totalAvoir + cont.Diff;
                        }
                    }
                    catch (Exception) { }
                    try
                    {
                        //totalHTTheorique = totalHTTheorique + (cont.Bon_Livraison_Contenu_Commande1.Contenu_Commande_Fournisseur.Prix_Unitaire * cont.Bon_Livraison_Contenu_Commande1.Quantite);
                        totalHTTheorique = totalHTTheorique + (cont.Prix_Unitaire_Remise_HT * cont.Qte_Facturee);
                    }
                    catch (Exception) { }
                    try
                    {
                        cont.Prix_Total_Facture_HT = cont.Prix_Unitaire_Facture_HT * cont.Qte_Facturee;
                        totalHT = totalHT + (cont.Prix_Unitaire_Facture_HT * cont.Qte_Facturee);
                    }
                    catch (Exception) { }
                    try
                    {
                        if (cont.Plan_Comptable_Tva1.AutoLiquidation == true)
                        {
                            totalTTC = totalTTC + (cont.Prix_Unitaire_Facture_HT * cont.Qte_Facturee);
                        }
                        else
                        {
                            totalTTC = totalTTC + ((1 + (cont.Plan_Comptable_Tva1.Tva1.Taux / 100)) * cont.Prix_Unitaire_Facture_HT * cont.Qte_Facturee);
                        }
                    }
                    catch (Exception) { }
                }
            }

            if (this._textBoxEcartTva.Text != "")
            {
                double val;
                if (double.TryParse(this._textBoxEcartTva.Text, out val))
                {
                    totalTTC = totalTTC - double.Parse(this._textBoxEcartTva.Text);
                }
            }

            ((Facture_Fournisseur)this.DataContext).Montant_HT = totalHT;
            ((Facture_Fournisseur)this.DataContext).Montant_TTC = totalTTC;
            ((Facture_Fournisseur)this.DataContext).Diff = totalAvoir;

            //Calcul de l'accompte
            double totalProforma = 0;
            foreach (Facture_Proforma item in ((Facture_Fournisseur)this.DataContext).Facture_Proforma)
            {
                if (item.Montant != null)
                {
                    totalProforma += double.Parse(item.Montant.ToString());
                }
            }
            ((Facture_Fournisseur)this.DataContext).Accompte = totalProforma;

            if (this._textBoxEcartTva.Text != "")
            {
                double val;
                if (double.TryParse(this._textBoxEcartTva.Text, out val))
                {
                    this._textBoxMontantNetAPayer.Text = (((Facture_Fournisseur)this.DataContext).Montant_TTC - ((Facture_Fournisseur)this.DataContext).Accompte).ToString(System.Globalization.CultureInfo.CurrentCulture);
                }
                else
                {
                    this._textBoxMontantNetAPayer.Text = (((Facture_Fournisseur)this.DataContext).Montant_TTC - ((Facture_Fournisseur)this.DataContext).Accompte).ToString(System.Globalization.CultureInfo.CurrentCulture);
                }
            }
            else
            {
                this._textBoxMontantNetAPayer.Text = (((Facture_Fournisseur)this.DataContext).Montant_TTC - ((Facture_Fournisseur)this.DataContext).Accompte).ToString(System.Globalization.CultureInfo.CurrentCulture);
            }
        }

        private void calculResume()
        {
            ObservableCollection<RecapFacture> listRecapFacture = new ObservableCollection<RecapFacture>();
            ObservableCollection<RecapFactureTva> listRecapFactureTva = new ObservableCollection<RecapFactureTva>();
            listRecapFacture = new ObservableCollection<RecapFacture>();
            listRecapFactureTva = new ObservableCollection<RecapFactureTva>();
            double totTVA = 0;
            //Contenu
            foreach (Facture_Fournisseur_Contenu cont in this._DataGridContenu.Items.OfType<Facture_Fournisseur_Contenu>())
            {
                if (cont.Montant_TTC != null)
                {
                    if (cont.Plan_Comptable_Imputation1 != null && cont.Plan_Comptable_Tva1 != null)
                    {
                        //Imputation
                        double totalAImputer = cont.Qte_Facturee * cont.Prix_Unitaire_Facture_HT;
                        bool test1 = false;
                        RecapFacture toAdd = null;
                        foreach (RecapFacture rec in listRecapFacture)
                        {
                            if (rec.planComptableImputation.Identifiant == cont.Plan_Comptable_Imputation1.Identifiant)
                            {
                                test1 = true;
                                toAdd = rec;
                            }
                        }
                        if (test1)
                        {
                            toAdd.total = toAdd.total + totalAImputer;
                        }
                        else
                        {
                            RecapFacture recap1 = new RecapFacture(cont.Plan_Comptable_Imputation1, totalAImputer);
                            listRecapFacture.Add(recap1);
                        }

                        //Tva
                        double toTest;
                        //Tva sans échéancier
                        if (cont.Plan_Comptable_Tva1.Echeancier == false && cont.Plan_Comptable_Tva1.AutoLiquidation == false)
                        {
                            if (double.TryParse(cont.Montant_TTC.ToString(), out toTest))
                            {
                                double totalTva = double.Parse(cont.Montant_TTC.ToString()) - (cont.Prix_Unitaire_Facture_HT * cont.Qte_Facturee);
                                totTVA = totTVA + totalTva;
                                bool test2 = false;
                                RecapFactureTva toAdd2 = null;
                                foreach (RecapFactureTva rec in listRecapFactureTva)
                                {
                                    if (rec.chaine == cont.Plan_Comptable_Tva1.Libelle && rec.numero == cont.Plan_Comptable_Tva1.Numero)
                                    {
                                        test2 = true;
                                        toAdd2 = rec;
                                    }
                                }
                                if (test2)
                                {
                                    toAdd2.total = toAdd2.total + totalTva;
                                }
                                else
                                {
                                    RecapFactureTva recap2 = new RecapFactureTva(totalTva, cont.Plan_Comptable_Tva1.Libelle, cont.Plan_Comptable_Tva1.Numero);
                                    listRecapFactureTva.Add(recap2);
                                }
                            }
                        }
                        //tva avec échéancier
                        else
                        {
                            if (cont.Plan_Comptable_Tva1.Echeancier == true)
                            {
                                foreach (Facture_Fournisseur_Condition_Reglement item in ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Condition_Reglement)
                                {
                                    try
                                    {
                                        if (item.Date_Echeance != null && item.Pourcentage != null)
                                        {
                                            string month = item.Date_Echeance.Value.Month.ToString();
                                            string nomLigne = cont.Plan_Comptable_Tva1.Libelle + " - " + month;
                                            if (double.TryParse(cont.Montant_TTC.ToString(), out toTest))
                                            {
                                                double totalTva = (double.Parse(cont.Montant_TTC.ToString()) - (cont.Prix_Unitaire_Facture_HT * cont.Qte_Facturee)) * (double.Parse(item.Pourcentage.ToString()) / 100);
                                                totTVA = totTVA + totalTva;
                                                bool test2 = false;
                                                RecapFactureTva toAdd2 = null;
                                                foreach (RecapFactureTva rec in listRecapFactureTva)
                                                {
                                                    if (rec.chaine == nomLigne)
                                                    {
                                                        test2 = true;
                                                        toAdd2 = rec;
                                                    }
                                                }
                                                if (test2)
                                                {
                                                    toAdd2.total = toAdd2.total + totalTva;
                                                }
                                                else
                                                {
                                                    RecapFactureTva recap2 = new RecapFactureTva(totalTva, nomLigne, cont.Plan_Comptable_Tva1.Numero);
                                                    listRecapFactureTva.Add(recap2);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        MessageBox.Show(e.Message);
                                    }
                                }
                            }
                            else if (cont.Plan_Comptable_Tva1.AutoLiquidation == true)
                            {
                                if (double.TryParse(cont.Montant_TTC.ToString(), out toTest))
                                {
                                    double totalTva = (cont.Prix_Unitaire_Facture_HT * cont.Qte_Facturee) * (cont.Plan_Comptable_Tva1.Tva1.Taux / 100);
                                    totTVA = totTVA + totalTva;
                                    bool test2 = false;
                                    RecapFactureTva toAdd2 = null;
                                    foreach (RecapFactureTva rec in listRecapFactureTva)
                                    {
                                        if (rec.chaine == (cont.Plan_Comptable_Tva1.Libelle + " || +") && rec.numero == cont.Plan_Comptable_Tva1.Numero)
                                        {
                                            test2 = true;
                                            toAdd2 = rec;
                                        }
                                    }
                                    if (test2)
                                    {
                                        toAdd2.total = toAdd2.total + totalTva;
                                    }
                                    else
                                    {
                                        RecapFactureTva recap2 = new RecapFactureTva(totalTva, cont.Plan_Comptable_Tva1.Libelle + " || +", cont.Plan_Comptable_Tva1.Numero);
                                        listRecapFactureTva.Add(recap2);
                                    }

                                    totTVA = totTVA - totalTva;
                                    bool test3 = false;
                                    RecapFactureTva toAdd3 = null;
                                    foreach (RecapFactureTva rec in listRecapFactureTva)
                                    {
                                        if (rec.chaine == (cont.Plan_Comptable_Tva1.Libelle + " || -") && rec.numero == cont.Plan_Comptable_Tva1.Numero)
                                        {
                                            test3 = true;
                                            toAdd3 = rec;
                                        }
                                    }
                                    if (test3)
                                    {
                                        toAdd3.total = toAdd3.total - totalTva;
                                    }
                                    else
                                    {
                                        RecapFactureTva recap2 = new RecapFactureTva(-totalTva, cont.Plan_Comptable_Tva1.Libelle + " || -", cont.Plan_Comptable_Tva1.Numero);
                                        listRecapFactureTva.Add(recap2);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            int toPutonTVA = (int)(totTVA * 100);
            this._textBoxMontantTVA.Text = (((double)toPutonTVA) / 100).ToString(System.Globalization.CultureInfo.CurrentCulture);
            this._dataGridResume1.ItemsSource = listRecapFacture;
            this._dataGridResume2.ItemsSource = listRecapFactureTva;
            ((Facture_Fournisseur)this.DataContext).Montant_TVA = totTVA;
        }

        #region Evenements de calculs

        private void _DataGridContenu_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            this.calculChaqueLigne();
            this.calculResume();
        }

        private void _DataGridContenu_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            this.calculChaqueLigne();
            this.calculResume();
        }

        private void _DataGridContenu_CurrentCellChanged(object sender, EventArgs e)
        {
            this.calculChaqueLigne();
            this.calculResume();
        }

        private void _DataGridContenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.calculChaqueLigne();
            this.calculResume();
        }

        private void Calculer()
        {
            this.calculChaqueLigne();
            this.calculResume();
            Verif_textBoxTotalTheorique();
            this._dataGridFactureFournisseurConditionReglement.Items.Refresh();
        }

        #endregion

        #endregion

        #region Mettre à null

        private void _mettreANullAffaire_Click(object sender, RoutedEventArgs e)
        {
            this._comboBoxFilterAffaire.SelectedItem = null;
            this.SelectionAutoBL();
            if (this._comboBoxFournisseur.SelectedItem != null)
            {
                this.triAutoListeCommandes();
            }
        }

        private void _mettreANullCommande_Click(object sender, RoutedEventArgs e)
        {
            this._comboBoxFilterCommandeBL.SelectedItem = null;
            this.SelectionAutoBL();
        }

        #endregion

        #region TriAuto ComboBox et ListBox

        private void SelectionAutoBL()
        {
            ObservableCollection<MatriceBL> listBLToWatch = new ObservableCollection<MatriceBL>();
            ObservableCollection<Bon_Livraison> listToTest = new ObservableCollection<Bon_Livraison>();
            if (this._comboBoxFournisseur.SelectedItem != null)
            {
                listToTest = new ObservableCollection<Bon_Livraison>(((App)App.Current).mySitaffEntities.Bon_Livraison.Where(bl => bl.Fournisseur1.Identifiant == ((Fournisseur)this._comboBoxFournisseur.SelectedItem).Identifiant));
                if (this._comboBoxFilterAffaire.SelectedItem != null)
                {
                    listToTest = new ObservableCollection<Bon_Livraison>(listToTest.Where(bl => bl.Affaire1 != null));
                    listToTest = new ObservableCollection<Bon_Livraison>(listToTest.Where(bl => bl.Affaire1.Identifiant == ((Affaire)this._comboBoxFilterAffaire.SelectedItem).Identifiant));
                }
                if (this._comboBoxFilterCommandeBL.SelectedItem != null)
                {
                    listToTest = new ObservableCollection<Bon_Livraison>(listToTest.Where(bl => bl.Commande_Fournisseur1 != null));
                    listToTest = new ObservableCollection<Bon_Livraison>(listToTest.Where(bl => bl.Commande_Fournisseur1.Identifiant == ((Commande_Fournisseur)this._comboBoxFilterCommandeBL.SelectedItem).Identifiant));
                }
                foreach (Bon_Livraison item in listToTest)
                {
                    if (item.Facture_Fournisseur1 == null)
                    {
                        MatriceBL temp = new MatriceBL();
                        temp.bon_livraison = item;
                        temp.affaire = item.Affaire1;
                        temp.commande_fournisseur = item.Commande_Fournisseur1;
                        listBLToWatch.Add(temp);
                    }
                }
            }
            this._dataGridBLGauche.ItemsSource = listBLToWatch;

            this._dataGridDroitBL.Items.Refresh();
            this._dataGridBLGauche.Items.Refresh();
        }

        private void SelectionAutoProforma()
        {
            ObservableCollection<Facture_Proforma> listToWatch = new ObservableCollection<Facture_Proforma>();
            if (this._comboBoxFournisseur.SelectedItem != null)
            {
                listToWatch = new ObservableCollection<Facture_Proforma>(((Fournisseur)this._comboBoxFournisseur.SelectedItem).Facture_Proforma.Where(fpr => fpr.Facture_Fournisseur1 == null));
                this._dataGridProformaGauche.ItemsSource = listToWatch;
            }
        }

        private void SelectionAutoLigneCommande()
        {
            ObservableCollection<Contenu_Commande_Fournisseur> listCCFToWatch = new ObservableCollection<Contenu_Commande_Fournisseur>();
            if (this._comboBoxFournisseur.SelectedItem != null)
            {
                if (this._comboBoxFilterCommande.SelectedItem != null)
                {
                    this._dataGridCommandeGauche.ItemsSource = ((Commande_Fournisseur)this._comboBoxFilterCommande.SelectedItem).Contenu_Commande_Fournisseur.Where(ccf => ccf.QuantiteRestante > 0);
                }
            }
            this._dataGridCommandeGauche.Items.Refresh();
        }

        private void _comboBoxFilterAffaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectionAutoBL();
            if (this._comboBoxFournisseur.SelectedItem != null)
            {
                if (this._comboBoxFilterAffaire.SelectedItem != null)
                {
                    this._comboBoxFilterCommandeBL.ItemsSource = ((Affaire)this._comboBoxFilterAffaire.SelectedItem).Commande_Fournisseur.Where(com => com.Fournisseur1.Identifiant == ((Fournisseur)this._comboBoxFournisseur.SelectedItem).Identifiant);
                }
                else
                {
                    this._comboBoxFilterCommande.ItemsSource = ((Fournisseur)this._comboBoxFournisseur.SelectedItem).Commande_Fournisseur;
                }
            }
        }

        private void _comboBoxFilterCommandeBL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectionAutoBL();
            if (this._comboBoxFilterCommandeBL.SelectedItem != null)
            {
                this._comboBoxFilterAffaire.SelectedItem = ((Commande_Fournisseur)this._comboBoxFilterCommandeBL.SelectedItem).Affaire1;
            }
        }

        private void _comboBoxFilterCommande_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectionAutoLigneCommande();
        }

        private void triAutoListeCommandes()
        {
            this._comboBoxFilterCommande.ItemsSource = ((Fournisseur)this._comboBoxFournisseur.SelectedItem).Commande_Fournisseur.Where(com => com.ToutPasseEnFacture == false && com.ToutPasseEnBL == false);
            ObservableCollection<Commande_Fournisseur> listToPut = new ObservableCollection<Commande_Fournisseur>();
            foreach (Commande_Fournisseur item in ((Fournisseur)this._comboBoxFournisseur.SelectedItem).Commande_Fournisseur.Where(com => com.ToutPasseEnFacture == false))
            {
                bool test = false;
                foreach (Bon_Livraison itemBL in item.Bon_Livraison)
                {
                    if (itemBL.Facture_Fournisseur1 == null)
                    {
                        test = true;
                    }
                }
                if (test)
                {
                    listToPut.Add(item);
                }
            }
            this._comboBoxFilterCommandeBL.ItemsSource = listToPut;
        }

        private void triAutoListeAffaires()
        {
            ObservableCollection<Affaire> listAffaireToPutOnCB = new ObservableCollection<Affaire>();
            foreach (Commande_Fournisseur item in this._comboBoxFilterCommandeBL.ItemsSource.OfType<Commande_Fournisseur>())
            {
                if (item.Affaire1 != null)
                {
                    if (!listAffaireToPutOnCB.Contains(item.Affaire1))
                    {
                        listAffaireToPutOnCB.Add(item.Affaire1);
                    }
                }
            }
            this._comboBoxFilterAffaire.ItemsSource = listAffaireToPutOnCB;
        }

        #endregion

        private void _verrouillerFacture()
        {
            if (this._DataGridContenu.Items.Count > 0)
            {
                this._comboBoxFournisseur.IsEnabled = false;
            }
            if (this._dataGridDroitProforma.Items.Count > 0)
            {
                this._comboBoxFournisseur.IsEnabled = false;
            }
            if (this._comboBoxFournisseur.SelectedItem != null)
            {
                this._comboBoxFournisseur.IsEnabled = true;
                this._buttonGaucheDroite.IsEnabled = true;
                this._buttonGaucheDroiteBL.IsEnabled = true;
                this._buttonGaucheDroiteCommande.IsEnabled = true;
                this._buttonDroiteGauche.IsEnabled = true;
                this._buttonDroiteGaucheBL.IsEnabled = true;
                this._buttonDroiteGaucheCommande.IsEnabled = true;
                this._DataGridContenu.IsReadOnly = false;
                this._ButtonColler.IsEnabled = true;
            }
            else
            {
                this._comboBoxFournisseur.IsEnabled = true;
                this._buttonGaucheDroite.IsEnabled = false;
                this._buttonGaucheDroiteBL.IsEnabled = false;
                this._buttonGaucheDroiteCommande.IsEnabled = false;
                this._buttonDroiteGauche.IsEnabled = false;
                this._buttonDroiteGaucheBL.IsEnabled = false;
                this._buttonDroiteGaucheCommande.IsEnabled = false;
                this._DataGridContenu.IsReadOnly = true;
                this._ButtonColler.IsEnabled = false;
            }
        }

        private void deleteLigne()
        {
            if (this._DataGridContenu.SelectedItem != null && this._DataGridContenu.SelectedItems.Count == 1)
            {
                try
                {
                    ((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem).Qte_Commandee = 0;
                    ((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem).Qte_Facturee = 0;
                    ((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem).Qte_Livree = 0;
                    ((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem).Prix_Total_Facture_HT = 0;
                    ((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem).Prix_Unitaire_Commande_HT = 0;
                    ((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem).Prix_Unitaire_Facture_HT = 0;
                    ((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem).Prix_Unitaire_Remise_HT = 0;
                    ((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem).Plan_Comptable_Imputation1 = null;
                    ((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem).Plan_Comptable_Tva1 = null;
                    ((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem).Affaire1 = null;
                    ((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem).Designation = "";
                    ((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem).Reference_Fournisseur = "";
                }
                catch (Exception) { }
                try
                {
                    ((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem).Facture_Fournisseur1 = null;
                    ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Remove((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem);
                    ((App)App.Current).mySitaffEntities.Facture_Fournisseur_Contenu.DeleteObject(((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem));
                }
                catch (Exception)
                {
                    try
                    {
                        ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Remove((Facture_Fournisseur_Contenu)this._DataGridContenu.SelectedItem);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            this._DataGridContenu.Items.Remove(this._DataGridContenu.SelectedItem);
                        }
                        catch (Exception) { }
                    }
                }
            }
            else
            {
                if (this._DataGridContenu.SelectedItems.Count != 0)
                {
                    ObservableCollection<Facture_Fournisseur_Contenu> toRemove = new ObservableCollection<Facture_Fournisseur_Contenu>();
                    foreach (Facture_Fournisseur_Contenu item in this._DataGridContenu.SelectedItems.OfType<Facture_Fournisseur_Contenu>())
                    {
                        toRemove.Add(item);
                    }
                    foreach (Facture_Fournisseur_Contenu item in toRemove)
                    {
                        try
                        {
                            item.Qte_Commandee = 0;
                            item.Qte_Facturee = 0;
                            item.Qte_Livree = 0;
                            item.Prix_Total_Facture_HT = 0;
                            item.Prix_Unitaire_Commande_HT = 0;
                            item.Prix_Unitaire_Facture_HT = 0;
                            item.Prix_Unitaire_Remise_HT = 0;
                            item.Plan_Comptable_Imputation1 = null;
                            item.Plan_Comptable_Tva1 = null;
                            item.Affaire1 = null;
                            item.Designation = "";
                            item.Reference_Fournisseur = "";
                        }
                        catch (Exception) { }
                        try
                        {
                            item.Facture_Fournisseur1 = null;
                            ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Remove(item);
                            ((App)App.Current).mySitaffEntities.Facture_Fournisseur_Contenu.DeleteObject(item);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                ((Facture_Fournisseur)this.DataContext).Facture_Fournisseur_Contenu.Remove(item);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    this._DataGridContenu.Items.Remove(item);
                                }
                                catch (Exception) { }
                            }
                        }
                    }
                }
            }
            this._DataGridContenu.Items.Refresh();
            this.Calculer();
        }

        private void menuDelete()
        {
            this.deleteLigne();
        }

        #endregion

        #region Lecture seule

        public void lectureSeule()
        {
            //TextBox
            this._textBoxMontantHTTheorique.IsReadOnly = true;
            this._textBoxNumeroFacture.IsReadOnly = true;
            this._textBoxNumeroPieceComptable.IsReadOnly = true;
            this._textBoxEcartTva.IsReadOnly = true;

            //ComboBox
            this._comboBoxFournisseur.IsEnabled = false;

            //Boutons
            this._buttonAjouterLitige.IsEnabled = false;
            this._buttonModifierLitige.IsEnabled = false;
            this._buttonSupprimerLitige.IsEnabled = false;

            this._buttonDroiteGauche.IsEnabled = false;
            this._buttonGaucheDroite.IsEnabled = false;

            this._buttonDroiteGaucheBL.IsEnabled = false;
            this._buttonGaucheDroiteBL.IsEnabled = false;

            this._buttonGaucheDroiteProforma.IsEnabled = false;
            this._buttonDroiteGaucheProforma.IsEnabled = false;

            this._ButtonCopierAffaireBas.IsEnabled = false;
            this._ButtonCopierPCIBas.IsEnabled = false;
            this._ButtonCopierPCTBas.IsEnabled = false;
            this._ButtonCalculer.IsEnabled = false;
            this._ButtonColler.IsEnabled = false;
            this._ButtonSupprimer.IsEnabled = false;

            //Datagrid
            this._DataGridContenu.IsReadOnly = true;
            this._dataGridFactureFournisseurConditionReglement.IsReadOnly = true;

            //Date
            this._DatePickerDateFacture.IsEnabled = false;

            //TabControl
            this._tabControlSelections.Visibility = Visibility.Collapsed;
        }

        #endregion        

        private void _mettreANullPlanComptable_Click_1(object sender, RoutedEventArgs e)
        {
            this._comboBoxFilterEntrepriseMere.SelectedItem = null;
        }

    }
}
