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

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour ShopCommandeWindow.xaml
    /// </summary>
    public partial class ShopCommandeWindow : Window
    {
        #region Attributs

        public CommandeWindow commandeWindow;

        public BonLivraisonWindow bonLivraisonWindow;

        public SortieAtelierWindow sortieAtelierWindow;

        #endregion

        #region Propriétés de dépendances


        public ObservableCollection<Fournisseur> listFournisseur
        {
            get { return (ObservableCollection<Fournisseur>)GetValue(listFournisseurProperty); }
            set { SetValue(listFournisseurProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listFournisseur.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFournisseurProperty =
            DependencyProperty.Register("listFournisseur", typeof(ObservableCollection<Fournisseur>), typeof(ShopCommandeWindow), new UIPropertyMetadata(null));



        public ObservableCollection<ItemShop> listShop
        {
            get { return (ObservableCollection<ItemShop>)GetValue(listShopProperty); }
            set { SetValue(listShopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listShop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listShopProperty =
            DependencyProperty.Register("listShop", typeof(ObservableCollection<ItemShop>), typeof(ShopCommandeWindow), new PropertyMetadata(null));



        #endregion

        #region Constructeur

        public ShopCommandeWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            this.creationMenuClicDroit();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);
        }

        #region clic droit

        private void creationMenuClicDroit()
        {
            ContextMenu contextMenu = new ContextMenu();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorToPut = "#A3D0D8E8";
            Brush colorMenu = (Brush)converter.ConvertFrom(colorToPut);
            contextMenu.Background = colorMenu;
            this._dataGridFournisseur.ContextMenu = contextMenu;

            MenuItem itemAfficher = new MenuItem();
            itemAfficher.Header = "Afficher la tendance";
            itemAfficher.Click += new RoutedEventHandler(delegate { this.RapportTendance(); });

            contextMenu.Items.Add(itemAfficher);
        }

        #endregion

        #region Initialisation PropD

        private void initialisationPropDependance()
        {
            this.listFournisseur = new ObservableCollection<Fournisseur>(((App)App.Current).mySitaffEntities.Fournisseur.Where(fou => fou.Commande_Fournisseur.Count() > 0).OrderBy(fou => fou.Entreprise.Libelle));
            //this.listShop = new ObservableCollection<Vue_Shop_Commande>(((App)App.Current).mySitaffEntities.Vue_Shop_Commande.OrderBy(vsc => vsc.Designation));
        }

        #endregion

        #endregion

        #region Fonctions

        private void import()
        {
            if (this.commandeWindow != null)
            {
                foreach (ItemShop item in this._dataGridFournisseur.SelectedItems.OfType<ItemShop>())
                {
                    try
                    {
                        Contenu_Commande_Fournisseur newItem = new Contenu_Commande_Fournisseur();
                        newItem.Reference = item.Reference;
                        newItem.Designation = item.Designation;
                        newItem.Quantite = 1;
                        newItem.Prix_Unitaire = double.Parse(item.Min_Prix_Unitaire.ToString());
                        ((Commande_Fournisseur)this.commandeWindow.DataContext).Contenu_Commande_Fournisseur.Add(newItem);
                    }
                    catch (Exception) { }
                }
                try
                {
                    this.commandeWindow._dataGridContenuCommande.Items.Refresh();
                }
                catch (Exception) { }
            }
            if (this.bonLivraisonWindow != null)
            {
                foreach (ItemShop item in this._dataGridFournisseur.SelectedItems.OfType<ItemShop>())
                {
                    try
                    {
                        Bon_Livraison_Contenu_Commande_Supplementaire newItem = new Bon_Livraison_Contenu_Commande_Supplementaire();
                        newItem.Reference = item.Reference;
                        newItem.Designation = item.Designation;
                        newItem.Quantite_Livree = 1;
                        newItem.Prix_Remise = double.Parse(item.Min_Prix_Unitaire.ToString());
                        ((Bon_Livraison)this.bonLivraisonWindow.DataContext).Bon_Livraison_Contenu_Commande_Supplementaire.Add(newItem);
                    }
                    catch (Exception) { }
                }
                try
                {
                    this.bonLivraisonWindow._dataGridContenuSupplementaire.Items.Refresh();
                }
                catch (Exception) { }
            }
            if (this.sortieAtelierWindow != null)
            {
                foreach (ItemShop item in this._dataGridFournisseur.SelectedItems.OfType<ItemShop>())
                {
                    try
                    {
                        Contenu_Sortie_Atelier newItem = new Contenu_Sortie_Atelier();
                        newItem.Reference = item.Reference;
                        newItem.Designation = item.Designation;
                        newItem.Quantite = 1;
                        newItem.Prix = double.Parse(item.Moyenne_Prix_Unitaire.ToString());
                        newItem.Prix_Remise = double.Parse(item.Min_Prix_Unitaire.ToString());
                        ((Sortie_Atelier)this.sortieAtelierWindow.DataContext).Contenu_Sortie_Atelier.Add(newItem);
                    }
                    catch (Exception) { }
                }
                try
                {
                    this.sortieAtelierWindow._dataGridContenu.Items.Refresh();
                }
                catch (Exception) { }
            }
        }

        #endregion

        #region Boutons

        #region importer

        private void Importer_Click(object sender, RoutedEventArgs e)
        {
            this.import();
        }

        #endregion

        #region null

        private void NullFournisseur_Click(object sender, RoutedEventArgs e)
        {
            this._ComboBoxFournisseur.SelectedItem = null;
        }

        #endregion

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #endregion

        #region Evenement fournisseur modifié

        private void _mettreANull_Click(object sender, RoutedEventArgs e)
        {
            this._ComboBoxFournisseur.SelectedItem = null;
            this.filtrage();
        }

        private void _TextBoxDesignation_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.filtrage();
            }
        }

        private void _ComboBoxFournisseur_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.filtrage();
        }

        #endregion

        #region filtrage

        private void filtrage()
        {
            if (this._ComboBoxFournisseur.SelectedItem != null)
            {
                ObservableCollection<ItemShop> toPutOnLine = new ObservableCollection<ItemShop>();

                foreach (GetShopCommandeWithEntrepriseName_Result item in ((App)App.Current).mySitaffEntities.GetShopCommandeWithEntrepriseName(((Fournisseur)this._ComboBoxFournisseur.SelectedItem).Identifiant))
                {
                    bool test = true;

                    if (this._TextBoxDesignation.Text.Trim() != "" && test)
                    {
                        if (item.Designation != null)
                        {
                            if (item.Designation.ToLower().Trim().Contains(this._TextBoxDesignation.Text.ToLower().Trim()))
                            {
                                test = true;
                            }
                            else
                            {
                                test = false;
                            }
                        }
                        else
                        {
                            test = false;
                        }
                    }

                    if (this._TextBoxReference.Text.Trim() != "" && test)
                    {
                        if (this._TextBoxReference.Text.Contains(";"))
                        {
                            ObservableCollection<String> listRef = new ObservableCollection<string>(this._TextBoxReference.Text.Split(';'));
                            bool test2 = false;
                            foreach (String mot in listRef)
                            {
                                if (item.Reference != null)
                                {
                                    if (item.Reference.ToLower().Trim().Contains(mot.ToLower().Trim()))
                                    {
                                        test = true;
                                        test2 = true;
                                    }
                                }
                                else
                                {
                                    test = false;
                                }
                            }
                            if (test && !test2)
                            {
                                test = false;
                            }
                        }
                        else
                        {
                            if (item.Reference != null)
                            {
                                if (item.Reference.ToLower().Trim().Contains(this._TextBoxReference.Text.ToLower().Trim()))
                                {
                                    test = true;
                                }
                                else
                                {
                                    test = false;
                                }
                            }
                            else
                            {
                                test = false;
                            }
                        }
                    }

                    if (test)
                    {
                        ItemShop tmp = new ItemShop(item.Reference, item.Designation, item.Libelle, 0, item.Nb_Fois_Commande, item.Min_Prix_Remise, item.Moyenne_Prix_Remise, item.Max_Prix_Remise, item.Min_Prix_Unitaire, item.Moyenne_Prix_Unitaire, item.Max_Prix_Unitaire);
                        toPutOnLine.Add(tmp);
                    }

                }

                this._dataGridFournisseur.ItemsSource = toPutOnLine;
            }
            else
            {
                ObservableCollection<ItemShop> toPutOnLine = new ObservableCollection<ItemShop>();

                foreach (GetShopCommandeWithoutFournisseurWithEntrepriseName_Result item in ((App)App.Current).mySitaffEntities.GetShopCommandeWithoutFournisseurWithEntrepriseName())
                {
                    bool test = true;

                    if (this._TextBoxDesignation.Text.Trim() != "" && test)
                    {
                        if (item.Designation != null)
                        {
                            if (item.Designation.ToLower().Trim().Contains(this._TextBoxDesignation.Text.ToLower().Trim()))
                            {
                                test = true;
                            }
                            else
                            {
                                test = false;
                            }
                        }
                        else
                        {
                            test = false;
                        }
                    }

                    if (this._TextBoxReference.Text.Trim() != "" && test)
                    {
                        if (this._TextBoxReference.Text.Contains(";"))
                        {
                            ObservableCollection<String> listRef = new ObservableCollection<string>(this._TextBoxReference.Text.Split(';'));
                            bool test2 = false;
                            foreach (String mot in listRef)
                            {
                                if (item.Reference != null)
                                {
                                    if (item.Reference.ToLower().Trim().Contains(mot.ToLower().Trim()))
                                    {
                                        test = true;
                                        test2 = true;
                                    }
                                }
                                else
                                {
                                        test = false;
                                }
                            }
                            if (test && !test2)
                            {
                                test = false;
                            }
                        }
                        else
                        {
                            if (item.Reference != null)
                            {
                                if (item.Reference.ToLower().Trim().Contains(this._TextBoxReference.Text.ToLower().Trim()))
                                {
                                    test = true;
                                }
                                else
                                {
                                    test = false;
                                }
                            }
                            else
                            {
                                test = false;
                            }
                        }
                    }

                    if (test)
                    {
                        ItemShop tmp = new ItemShop(item.Reference, item.Designation, item.Libelle, 0, item.Nb_Fois_Commande, item.Min_Prix_Remise, item.Moyenne_Prix_Remise, item.Max_Prix_Remise, item.Min_Prix_Unitaire, item.Moyenne_Prix_Unitaire, item.Max_Prix_Unitaire);
                        toPutOnLine.Add(tmp);
                    }

                }

                this._dataGridFournisseur.ItemsSource = toPutOnLine;
            }
        }

        #endregion        

        public void RapportTendance()
        {
            if (this._dataGridFournisseur.SelectedItem != null)
            {
                if (this._dataGridFournisseur.SelectedItems.Count == 1)
                {
                    ReportingWindow reportingWindow = new ReportingWindow();
                    //long fourToShow = ((ItemShop)this._dataGridFournisseur.SelectedItem).idFournisseur;
                    long fourToShow = this.getIdFournisseur(((ItemShop)this._dataGridFournisseur.SelectedItem).Fournisseur);
                    string referenceToShow = ((ItemShop)this._dataGridFournisseur.SelectedItem).Reference;
                    reportingWindow._webBrowser.Source = new Uri("http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fProduitEvolution&rs:Command=Render&Fournisseur=" + fourToShow + "&Reference=" + referenceToShow);
                    reportingWindow.Title = "Tendance de prix";

                    reportingWindow.Show();
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul produit.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un produit.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        public long getIdFournisseur(string entrepriseName)
        {
            long toReturn = 0;
            foreach (Entreprise item in ((App)App.Current).mySitaffEntities.Entreprise.Where(ent => ent.Libelle == entrepriseName))
            {
                toReturn = item.Identifiant;
            }
            return toReturn;
        }

    }
}
