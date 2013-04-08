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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Threading;
using SitaffRibbon.Windows;
using SitaffRibbon.Classes;
using System.ComponentModel;

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeBonLivraisonControl.xaml
    /// </summary>
    public partial class ListeBonLivraisonControl : UserControl
    {
        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_ColonneNumeroBL;
        MenuItem MenuItem_ColonneNumeroAffaire;
        MenuItem MenuItem_ColonneNumeroCommande;
        MenuItem MenuItem_ColonneMontant;
        MenuItem MenuItem_ColonneDonneurOrdre;
        MenuItem MenuItem_ColonneAssociation;
        MenuItem MenuItem_ColonneRecu;
        MenuItem MenuItem_ColonneDateEnvoi;
        MenuItem MenuItem_ColonneDateReception;
        MenuItem MenuItem_ColonneFournisseur;
        MenuItem MenuItem_ColonneFactureFournisseur;
        MenuItem MenuItem_ColonneFactureFournisseurPieceComptable;

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region proprieté de dependance

        public ObservableCollection<Bon_Livraison> listBL
        {
            get { return (ObservableCollection<Bon_Livraison>)GetValue(listBLProperty); }
            set { SetValue(listBLProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listBL.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listBLProperty =
            DependencyProperty.Register("listBL", typeof(ObservableCollection<Bon_Livraison>), typeof(ListeBonLivraisonControl), new UIPropertyMetadata(null));



        public ObservableCollection<Commande_Fournisseur> listCommande
        {
            get { return (ObservableCollection<Commande_Fournisseur>)GetValue(listCommandeProperty); }
            set { SetValue(listCommandeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listCommande.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listCommandeProperty =
            DependencyProperty.Register("listCommande", typeof(ObservableCollection<Commande_Fournisseur>), typeof(ListeBonLivraisonControl), new UIPropertyMetadata(null));



        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(ListeBonLivraisonControl), new UIPropertyMetadata(null));



        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(ListeBonLivraisonControl), new UIPropertyMetadata(null));



        public ObservableCollection<Fournisseur> listFournisseur
        {
            get { return (ObservableCollection<Fournisseur>)GetValue(listFournisseurProperty); }
            set { SetValue(listFournisseurProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listFournisseur.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFournisseurProperty =
            DependencyProperty.Register("listFournisseur", typeof(ObservableCollection<Fournisseur>), typeof(ListeBonLivraisonControl), new UIPropertyMetadata(null));



        #endregion

        #region constructeur

        public ListeBonLivraisonControl()
        {
            InitializeComponent();

            //Initialisation de la zone de filtrage
            this.initialisationFilterZone();

            //Création du menu du clic droit
            this.creationMenuClicDroit();

            //Je calcul le nombre d'élements du datagrid
            this.recalculMax();

            //J'initialise les data
            this.initialisationDataDatagridMain(null);

            //Je passe le usercontrol à la personnalisation de l'utilisateur
            ((App)App.Current).personnalisation.initUserControl(this);
            //Je récupère les couleurs car on ne peut les faire en automatique pour TOUS les usercontrols
            this._filterZone.Background = ((App)App.Current).personnalisation.BackGroundUserControlFilterColor;
            this._DataGridMain.RowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridColor;
            this._DataGridMain.AlternatingRowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridAlternateColor;
            this._textBlockTaillePolice.Text = "Taille police (" + this._DataGridMain.FontSize.ToString() + ") :     ";
        }

        #region initialisation de la zone de filtrage

        private void initialisationFilterZone()
        {
            this.initialisationAutoCompleteBox();
            this.initialisationComboBoxAssociation();
            this._filterZone.Height = 21;
        }

        private void initialisationAutoCompleteBox()
        {
            List<string> listFournisseur = new List<string>();
            List<string> listAffaire = new List<string>();
            List<string> listCommande = new List<string>();
            List<string> listSalarie = new List<string>();

            foreach (Bon_Livraison item in ((App)App.Current).mySitaffEntities.Bon_Livraison)
            {
                //Pour remplir les affaires
                if (item.Affaire1 != null)
                {
                    if (!listAffaire.Contains(item.Affaire1.Numero))
                    {
                        listAffaire.Add(item.Affaire1.Numero);
                    }
                }

                //Pour remplir les commandes
                if (item.Commande_Fournisseur1 != null)
                {
                    if (!listCommande.Contains(item.Commande_Fournisseur1.Numero))
                    {
                        listCommande.Add(item.Commande_Fournisseur1.Numero);
                    }
                }

                //Pour remplir les fournisseur
                if (item.Fournisseur1 != null)
                {
                    if (!listFournisseur.Contains(item.Fournisseur1.Entreprise.Libelle))
                    {
                        listFournisseur.Add(item.Fournisseur1.Entreprise.Libelle);
                    }
                }

                //Pour remplir les donneurs d'ordre
                if (item.Salarie1 != null)
                {
                    if (item.Salarie1.Personne != null)
                    {
                        if (!listSalarie.Contains(item.Salarie1.Personne.fullname))
                        {
                            listSalarie.Add(item.Salarie1.Personne.fullname);
                        }
                    }
                }
            }

            _filterContainNumeroCommande.ItemsSource = listCommande;
            _filterContainNumeroAffaire.ItemsSource = listAffaire;
            _filterContainDonneurOrdre.ItemsSource = listSalarie;
            _filterContainFournisseur.ItemsSource = listFournisseur;
        }

        private void initialisationComboBoxAssociation()
        {
            ObservableCollection<AssociationBL> listAssociation = new ObservableCollection<AssociationBL>();
            listAssociation.Add(new AssociationBL("Stock"));
            listAssociation.Add(new AssociationBL("Divers"));
            listAssociation.Add(new AssociationBL("Sur affaire"));
            listAssociation.Add(new AssociationBL("Rien, erreur !"));
            this._filterContainAssociation.ItemsSource = listAssociation;
        }


        #endregion

        #region initialisation Donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<Bon_Livraison> listToPut)
        {
            if (listToPut == null)
            {
                this.listBL = new ObservableCollection<Bon_Livraison>(((App)App.Current).mySitaffEntities.Bon_Livraison.OrderBy(bl => bl.Numero));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listBL = new ObservableCollection<Bon_Livraison>(listToPut);
                this.MiseAJourEtat("Filtrage", null);
            }
        }

        #endregion

        #region clic droit

        private void creationMenuClicDroit()
        {
            ContextMenu contextMenu = new ContextMenu();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorToPut = "#A3D0D8E8";
            Brush colorMenu = (Brush)converter.ConvertFrom(colorToPut);
            contextMenu.Background = colorMenu;
            this._DataGridMain.ContextMenu = contextMenu;

            MenuItem itemAfficher = new MenuItem();
            itemAfficher.Header = "Afficher";
            itemAfficher.Click += new RoutedEventHandler(delegate { this.menuLook(); });

            MenuItem itemAfficher2 = new MenuItem();
            itemAfficher2.Header = "Ajouter";
            itemAfficher2.Click += new RoutedEventHandler(delegate { this.menuAdd(); });

            MenuItem itemAfficher3 = new MenuItem();
            itemAfficher3.Header = "Modifier";
            itemAfficher3.Click += new RoutedEventHandler(delegate { this.menuUpdate(); });

            MenuItem itemAfficher4 = new MenuItem();
            itemAfficher4.Header = "Supprimer";
            itemAfficher4.Click += new RoutedEventHandler(delegate { this.menuDelete(); });

            MenuItem itemAfficher5 = RemplirMenuAfficherMasquerColonnes(new MenuItem());
            itemAfficher5.Header = "Afficher / Masquer";

            Securite securite = new Securite();
            if (securite.VerificationDroitActionsCRUD(this.ToString(), "Look"))
            {
                contextMenu.Items.Add(itemAfficher);
            }
            if (securite.VerificationDroitActionsCRUD(this.ToString(), "Add"))
            {
                contextMenu.Items.Add(itemAfficher2);
            }
            if (securite.VerificationDroitActionsCRUD(this.ToString(), "Update"))
            {
                contextMenu.Items.Add(itemAfficher3);
            }
            if (securite.VerificationDroitActionsCRUD(this.ToString(), "Remove"))
            {
                contextMenu.Items.Add(itemAfficher4);
            }

            contextMenu.Items.Add(new Separator());

            contextMenu.Items.Add(itemAfficher5);
        }

        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {
            this.MenuItem_ColonneNumeroBL = new MenuItem();
            this.MenuItem_ColonneNumeroBL.IsChecked = false;
            this.MenuItem_ColonneNumeroBL.Header = "Numéro Bon Livraison";
            this.MenuItem_ColonneNumeroBL.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumeroBL(); });
            this.AffMas_ColonneNumeroBL();
            menuItem.Items.Add(this.MenuItem_ColonneNumeroBL);

            this.MenuItem_ColonneMontant = new MenuItem();
            this.MenuItem_ColonneMontant.IsChecked = false;
            this.MenuItem_ColonneMontant.Header = "Montant";
            this.MenuItem_ColonneMontant.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneMontant(); });
            this.AffMas_ColonneMontant();
            menuItem.Items.Add(this.MenuItem_ColonneMontant);

            this.MenuItem_ColonneFournisseur = new MenuItem();
            this.MenuItem_ColonneFournisseur.IsChecked = false;
            this.MenuItem_ColonneFournisseur.Header = "Fournisseur";
            this.MenuItem_ColonneFournisseur.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFournisseur(); });
            this.AffMas_ColonneFournisseur();
            menuItem.Items.Add(this.MenuItem_ColonneFournisseur);

            this.MenuItem_ColonneNumeroAffaire = new MenuItem();
            this.MenuItem_ColonneNumeroAffaire.IsChecked = false;
            this.MenuItem_ColonneNumeroAffaire.Header = "Numéro de l'affaire";
            this.MenuItem_ColonneNumeroAffaire.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumeroAffaire(); });
            this.AffMas_ColonneNumeroAffaire();
            menuItem.Items.Add(this.MenuItem_ColonneNumeroAffaire);

            this.MenuItem_ColonneNumeroCommande = new MenuItem();
            this.MenuItem_ColonneNumeroCommande.IsChecked = false;
            this.MenuItem_ColonneNumeroCommande.Header = "Numéro de commande";
            this.MenuItem_ColonneNumeroCommande.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumeroCommande(); });
            this.AffMas_ColonneNumeroCommande();
            menuItem.Items.Add(this.MenuItem_ColonneNumeroCommande);

            this.MenuItem_ColonneDonneurOrdre = new MenuItem();
            this.MenuItem_ColonneDonneurOrdre.IsChecked = false;
            this.MenuItem_ColonneDonneurOrdre.Header = "Donneur d'ordre";
            this.MenuItem_ColonneDonneurOrdre.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDonneurOrdre(); });
            this.AffMas_ColonneDonneurOrdre();
            menuItem.Items.Add(this.MenuItem_ColonneDonneurOrdre);

            this.MenuItem_ColonneDateReception = new MenuItem();
            this.MenuItem_ColonneDateReception.IsChecked = false;
            this.MenuItem_ColonneDateReception.Header = "Date réception";
            this.MenuItem_ColonneDateReception.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateReception(); });
            this.AffMas_ColonneDateReception();
            menuItem.Items.Add(this.MenuItem_ColonneDateReception);

            this.MenuItem_ColonneDateEnvoi = new MenuItem();
            this.MenuItem_ColonneDateEnvoi.IsChecked = false;
            this.MenuItem_ColonneDateEnvoi.Header = "Date envoi";
            this.MenuItem_ColonneDateEnvoi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateEnvoi(); });
            this.AffMas_ColonneDateEnvoi();
            menuItem.Items.Add(this.MenuItem_ColonneDateEnvoi);

            this.MenuItem_ColonneAssociation = new MenuItem();
            this.MenuItem_ColonneAssociation.IsChecked = false;
            this.MenuItem_ColonneAssociation.Header = "Condition de règlement";
            this.MenuItem_ColonneAssociation.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneAssociation(); });
            this.AffMas_ColonneAssociation();
            menuItem.Items.Add(this.MenuItem_ColonneAssociation);

            this.MenuItem_ColonneRecu = new MenuItem();
            this.MenuItem_ColonneRecu.IsChecked = true;
            this.MenuItem_ColonneRecu.Header = "Reçu?";
            this.MenuItem_ColonneRecu.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneRecu(); });
            this.AffMas_ColonneRecu();
            menuItem.Items.Add(this.MenuItem_ColonneRecu);

            this.MenuItem_ColonneFactureFournisseur = new MenuItem();
            this.MenuItem_ColonneFactureFournisseur.IsChecked = true;
            this.MenuItem_ColonneFactureFournisseur.Header = "Numéro facture fournisseur";
            this.MenuItem_ColonneFactureFournisseur.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFactureFournisseur(); });
            this.AffMas_ColonneFactureFournisseur();
            menuItem.Items.Add(this.MenuItem_ColonneFactureFournisseur);

            this.MenuItem_ColonneFactureFournisseurPieceComptable = new MenuItem();
            this.MenuItem_ColonneFactureFournisseurPieceComptable.IsChecked = false;
            this.MenuItem_ColonneFactureFournisseurPieceComptable.Header = "Facture fournisseur - Pièce comptable";
            this.MenuItem_ColonneFactureFournisseurPieceComptable.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFactureFournisseurPieceComptable(); });
            this.AffMas_ColonneFactureFournisseurPieceComptable();
            menuItem.Items.Add(this.MenuItem_ColonneFactureFournisseurPieceComptable);

            menuItem.Items.Add(new Separator());

            this.MenuItem_AfficherTout = new MenuItem();
            this.MenuItem_AfficherTout.Header = "Afficher tout";
            this.MenuItem_AfficherTout.Click += new RoutedEventHandler(delegate { this.AffMas_AfficherTout(); });
            menuItem.Items.Add(this.MenuItem_AfficherTout);

            this.MenuItem_MasquerTout = new MenuItem();
            this.MenuItem_MasquerTout.Header = "Masquer tout";
            this.MenuItem_MasquerTout.Click += new RoutedEventHandler(delegate { this.AffMas_MasquerTout(); });
            menuItem.Items.Add(this.MenuItem_MasquerTout);

            return menuItem;
        }


        private void menuLook()
        {
            ((App)App.Current)._theMainWindow._CommandLook.Command.Execute(((App)App.Current)._theMainWindow);
        }

        private void menuAdd()
        {
            ((App)App.Current)._theMainWindow._CommandAdd.Command.Execute(((App)App.Current)._theMainWindow);
        }

        private void menuUpdate()
        {
            ((App)App.Current)._theMainWindow._CommandUpdate.Command.Execute(((App)App.Current)._theMainWindow);
        }

        private void menuDelete()
        {
            ((App)App.Current)._theMainWindow._CommandDelete.Command.Execute(((App)App.Current)._theMainWindow);
        }

        #endregion

        #region afficher masquer

        #region Tout

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneNumeroBL.IsChecked = false;
            this.MenuItem_ColonneNumeroCommande.IsChecked = false;
            this.MenuItem_ColonneNumeroAffaire.IsChecked = false;
            this.MenuItem_ColonneMontant.IsChecked = false;
            this.MenuItem_ColonneDateReception.IsChecked = false;
            this.MenuItem_ColonneFactureFournisseur.IsChecked = false;
            this.MenuItem_ColonneRecu.IsChecked = false;
            this.MenuItem_ColonneDonneurOrdre.IsChecked = false;
            this.MenuItem_ColonneDateEnvoi.IsChecked = false;
            this.MenuItem_ColonneFournisseur.IsChecked = false;
            this.MenuItem_ColonneAssociation.IsChecked = false;
            this.MenuItem_ColonneFactureFournisseurPieceComptable.IsChecked = false;

            this.AffMas_ColonneNumeroBL();
            this.AffMas_ColonneNumeroCommande();
            this.AffMas_ColonneNumeroAffaire();
            this.AffMas_ColonneMontant();
            this.AffMas_ColonneFournisseur();
            this.AffMas_ColonneDateReception();
            this.AffMas_ColonneDateEnvoi();
            this.AffMas_ColonneAssociation();
            this.AffMas_ColonneFactureFournisseur();
            this.AffMas_ColonneDonneurOrdre();
            this.AffMas_ColonneRecu();
            this.AffMas_ColonneFactureFournisseurPieceComptable();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneNumeroBL.IsChecked = true;
            this.MenuItem_ColonneNumeroCommande.IsChecked = true;
            this.MenuItem_ColonneNumeroAffaire.IsChecked = true;
            this.MenuItem_ColonneMontant.IsChecked = true;
            this.MenuItem_ColonneDateReception.IsChecked = true;
            this.MenuItem_ColonneFactureFournisseur.IsChecked = true;
            this.MenuItem_ColonneRecu.IsChecked = true;
            this.MenuItem_ColonneDonneurOrdre.IsChecked = true;
            this.MenuItem_ColonneDateEnvoi.IsChecked = true;
            this.MenuItem_ColonneFournisseur.IsChecked = true;
            this.MenuItem_ColonneAssociation.IsChecked = true;
            this.MenuItem_ColonneFactureFournisseurPieceComptable.IsChecked = true;

            this.AffMas_ColonneNumeroBL();
            this.AffMas_ColonneNumeroCommande();
            this.AffMas_ColonneNumeroAffaire();
            this.AffMas_ColonneMontant();
            this.AffMas_ColonneFournisseur();
            this.AffMas_ColonneDateReception();
            this.AffMas_ColonneDateEnvoi();
            this.AffMas_ColonneAssociation();
            this.AffMas_ColonneFactureFournisseur();
            this.AffMas_ColonneDonneurOrdre();
            this.AffMas_ColonneRecu();
            this.AffMas_ColonneFactureFournisseurPieceComptable();
        }

        #endregion

        private void AffMas_ColonneNumeroBL()
        {
            if (this.MenuItem_ColonneNumeroBL.IsChecked == true)
            {
                this._ColonneNumeroBL.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNumeroBL.IsChecked = false;
            }
            else
            {
                this._ColonneNumeroBL.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNumeroBL.IsChecked = true;
            }
        }

        private void AffMas_ColonneNumeroAffaire()
        {
            if (this.MenuItem_ColonneNumeroAffaire.IsChecked == true)
            {
                this._ColonneNumeroAffaire.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNumeroAffaire.IsChecked = false;
            }
            else
            {
                this._ColonneNumeroAffaire.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNumeroAffaire.IsChecked = true;
            }
        }

        private void AffMas_ColonneMontant()
        {
            if (this.MenuItem_ColonneMontant.IsChecked == true)
            {
                this._ColonneMontant.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneMontant.IsChecked = false;
            }
            else
            {
                this._ColonneMontant.Visibility = Visibility.Visible;
                this.MenuItem_ColonneMontant.IsChecked = true;
            }
        }

        private void AffMas_ColonneFournisseur()
        {
            if (this.MenuItem_ColonneFournisseur.IsChecked == true)
            {
                this._ColonneFournisseur.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFournisseur.IsChecked = false;
            }
            else
            {
                this._ColonneFournisseur.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFournisseur.IsChecked = true;
            }
        }

        private void AffMas_ColonneDateReception()
        {
            if (this.MenuItem_ColonneDateReception.IsChecked == true)
            {
                this._ColonneDateReception.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDateReception.IsChecked = false;
            }
            else
            {
                this._ColonneDateReception.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDateReception.IsChecked = true;
            }
        }

        private void AffMas_ColonneDateEnvoi()
        {
            if (this.MenuItem_ColonneDateEnvoi.IsChecked == true)
            {
                this._ColonneDateEnvoi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDateEnvoi.IsChecked = false;
            }
            else
            {
                this._ColonneDateEnvoi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDateEnvoi.IsChecked = true;
            }
        }

        private void AffMas_ColonneAssociation()
        {
            if (this.MenuItem_ColonneAssociation.IsChecked == true)
            {
                this._ColonneAssociation.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneAssociation.IsChecked = false;
            }
            else
            {
                this._ColonneAssociation.Visibility = Visibility.Visible;
                this.MenuItem_ColonneAssociation.IsChecked = true;
            }
        }

        private void AffMas_ColonneFactureFournisseur()
        {
            if (this.MenuItem_ColonneFactureFournisseur.IsChecked == true)
            {
                this._ColonneFactureFournisseur.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFactureFournisseur.IsChecked = false;
            }
            else
            {
                this._ColonneFactureFournisseur.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFactureFournisseur.IsChecked = true;
            }
        }

        private void AffMas_ColonneDonneurOrdre()
        {
            if (this.MenuItem_ColonneDonneurOrdre.IsChecked == true)
            {
                this._ColonneDonneurOrdre.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDonneurOrdre.IsChecked = false;
            }
            else
            {
                this._ColonneDonneurOrdre.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDonneurOrdre.IsChecked = true;
            }
        }

        private void AffMas_ColonneRecu()
        {
            if (this.MenuItem_ColonneRecu.IsChecked == true)
            {
                this._ColonneRecu.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneRecu.IsChecked = false;
            }
            else
            {
                this._ColonneRecu.Visibility = Visibility.Visible;
                this.MenuItem_ColonneRecu.IsChecked = true;
            }
        }

        private void AffMas_ColonneNumeroCommande()
        {
            if (this.MenuItem_ColonneNumeroCommande.IsChecked == true)
            {
                this._ColonneNumeroCommande.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNumeroCommande.IsChecked = false;
            }
            else
            {
                this._ColonneNumeroCommande.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNumeroCommande.IsChecked = true;
            }
        }

        private void AffMas_ColonneFactureFournisseurPieceComptable()
        {
            if (this.MenuItem_ColonneFactureFournisseurPieceComptable.IsChecked == true)
            {
                this._ColonneFactureFournisseurPieceComptable.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFactureFournisseurPieceComptable.IsChecked = false;
            }
            else
            {
                this._ColonneFactureFournisseurPieceComptable.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFactureFournisseurPieceComptable.IsChecked = true;
            }
        }

        #endregion

        #endregion

        #region fenetre chargé

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
            ((App)App.Current)._theMainWindow.stopThread();
        }
        #endregion

        #region CRUD (Create Read Update Delete)

        /// <summary>
        /// Ajoute une nouvelle Bon_Livraison à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Bon_Livraison Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un bon de livraison en cours ...");

            //Initialisation de la fenêtre
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
                        return null;
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
                    //Si j'appuie sur le bouton Ok, je renvoi l'objet bon_livraison se trouvant dans le datacontext de la fenêtre
                    return (Bon_Livraison)bonlivraisonWindow.DataContext;
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

                    //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    this.recalculMax();
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un bon de livraison annulé : " + this.listBL.Count() + " / " + this.max);

                    return null;
                }
            }
            else
            {
                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un bon de livraison annulé : " + this.listBL.Count() + " / " + this.max);

                return null;
            }
        }

        /// <summary>
        /// Ouvre la commande fournisseur séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Bon_Livraison Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    if (((Bon_Livraison)this._DataGridMain.SelectedItem).Facture_Fournisseur1 == null)
                    {
                        //Affichage du message "modification en cours"
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un bon de livraison en cours ...");

                        //Création de la fenêtre
                        BonLivraisonWindow bonlivraisonWindow = new BonLivraisonWindow();

                        //Initialisation du Datacontext en Bon_Livraison et association à la Bon_Livraison sélectionnée
                        bonlivraisonWindow.DataContext = new Bon_Livraison();
                        bonlivraisonWindow.DataContext = (Bon_Livraison)this._DataGridMain.SelectedItem;

                        //booléen nullable vrai ou faux ou null
                        bool? dialogResult = bonlivraisonWindow.ShowDialog();

                        if (dialogResult.HasValue && dialogResult.Value == true)
                        {
                            //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                            return (Bon_Livraison)bonlivraisonWindow.DataContext;
                        }
                        else
                        {
                            //Je récupère les anciennes données de la base sur les modifications effectuées
                            ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Bon_Livraison)(this._DataGridMain.SelectedItem));
                            //Le bl étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                            ((App)App.Current).refreshEDMXSansVidage();
                            this.filtrage();

                            //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
                            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un bon de livraison annulée : " + this.listBL.Count() + " / " + this.max);

                            return null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vous ne pouvez modifier ce bon de livraison car il est associé à une facture.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul bon de livraison.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un bon de livraison.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime le BL séléctionnée avec une confirmation
        /// </summary>
        public Bon_Livraison Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    if (((Bon_Livraison)this._DataGridMain.SelectedItem).Facture_Fournisseur1 == null)
                    {
                        //Affichage du message "suppression en cours"
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un bon de livraison en cours ...");

                        if (MessageBox.Show("Voulez-vous rééllement supprimer la commande fournisseur séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            //On détache tous les élements liés à la commande Commande_Fournisseur_Condition_Reglement
                            ObservableCollection<Bon_Livraison_Contenu_Commande> toRemove = new ObservableCollection<Bon_Livraison_Contenu_Commande>();
                            foreach (Bon_Livraison_Contenu_Commande item in ((Bon_Livraison)this._DataGridMain.SelectedItem).Bon_Livraison_Contenu_Commande)
                            {
                                toRemove.Add(item);
                            }
                            foreach (Bon_Livraison_Contenu_Commande item in toRemove)
                            {
                                ((Bon_Livraison)this._DataGridMain.SelectedItem).Bon_Livraison_Contenu_Commande.Remove(item);
                                try
                                {
                                    ((App)App.Current).mySitaffEntities.Bon_Livraison_Contenu_Commande.DeleteObject(item);
                                }
                                catch (Exception) { }
                            }

                            //On détache tous les élements liés à la commande Contenu_Commande_Fournisseur
                            ObservableCollection<Bon_Livraison_Contenu_Commande_Supplementaire> toRemove1 = new ObservableCollection<Bon_Livraison_Contenu_Commande_Supplementaire>();
                            foreach (Bon_Livraison_Contenu_Commande_Supplementaire item in ((Bon_Livraison)this._DataGridMain.SelectedItem).Bon_Livraison_Contenu_Commande_Supplementaire)
                            {
                                toRemove1.Add(item);
                            }
                            foreach (Bon_Livraison_Contenu_Commande_Supplementaire item in toRemove1)
                            {
                                ((Bon_Livraison)this._DataGridMain.SelectedItem).Bon_Livraison_Contenu_Commande_Supplementaire.Remove(item);
                                try
                                {
                                    ((App)App.Current).mySitaffEntities.Bon_Livraison_Contenu_Commande_Supplementaire.DeleteObject(item);
                                }
                                catch (Exception) { }
                            }

                            //Supprimer l'élément 
                            return (Bon_Livraison)this._DataGridMain.SelectedItem;
                        }
                        else
                        {
                            //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
                            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un bon de livraison annulé : " + this.listBL.Count() + " / " + this.max);

                            return null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vous ne pouvez supprimer ce bon de livraison car il est associé à une facture.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul bon de livraison.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un bon de livraison.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre la commande fournisseur séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Bon_Livraison Look(Bon_Livraison bon_livraison)
        {
            if (this._DataGridMain.SelectedItem != null || bon_livraison != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1 || bon_livraison != null)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un bon de livraison en cours ...");

                    //Création de la fenêtre
                    BonLivraisonWindow bonlivraisonWindow = new BonLivraisonWindow();

                    //Initialisation du Datacontext en Bon_Livraison et association à la Bon_Livraison sélectionnée
                    bonlivraisonWindow.DataContext = new Bon_Livraison();
                    if (bon_livraison != null)
                    {
                        bonlivraisonWindow.DataContext = bon_livraison;
                    }
                    else
                    {
                        bonlivraisonWindow.DataContext = (Bon_Livraison)this._DataGridMain.SelectedItem;
                    }

                    //Je positionne la lecture seule sur la fenêtre
                    bonlivraisonWindow.lectureSeule();
                    bonlivraisonWindow.soloLecture = true;

                    //J'affiche la fenêtre
                    bool? dialogResult = bonlivraisonWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un bon de livraison terminé : " + this.listBL.Count() + " / " + this.max);

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul bon de livraison.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une bon de livraison.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        #endregion

        #region actions supplementaires

        #endregion

        #region Filtrage

        #region remise a zero

        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            _filterContainNumeroBL.Text = "";
            _filterContainMontant.Text = "";
            _filterContainNumeroAffaire.Text = "";
            _filterContainNumeroCommande.Text = "";
            _filterContainDonneurOrdre.Text = "";
            _filterContainFournisseur.Text = "";

            _filterContainAssociation.SelectedItem = null;

            _filterContainDateEnvoi.SelectedDate = null;
            _filterContainDateReception.SelectedDate = null;

            //Rechargement des élements
            this.initialisationDataDatagridMain(null);
        }

        #endregion

        #region bouton filtrer
        private void _buttonFiltrer_Click(object sender, RoutedEventArgs e)
        {
            this.filtrage();
        }

        private void filtrage()
        {
            ((App)App.Current)._theMainWindow._mutex.WaitOne();
            ((App)App.Current)._theMainWindow.startThread();
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Filtrage en cours ...");

            ObservableCollection<Bon_Livraison> listToPut = new ObservableCollection<Bon_Livraison>(((App)App.Current).mySitaffEntities.Bon_Livraison.OrderBy(bl => bl.Numero));

            if (this._filterContainNumeroBL.Text != "")
            {
                listToPut = new ObservableCollection<Bon_Livraison>(listToPut.Where(bl => bl.Numero.Contains(this._filterContainNumeroBL.Text.Trim())));
            }
            if (this._filterContainMontant.Text != "")
            {
                double val;
                if (double.TryParse(this._filterContainMontant.Text, out val))
                {
                    listToPut = new ObservableCollection<Bon_Livraison>(listToPut.Where(com => com.Montant.ToString().Contains(double.Parse(this._filterContainMontant.Text.Trim()).ToString())));
                }
            }
            if (this._filterContainFournisseur.Text != "")
            {
                listToPut = new ObservableCollection<Bon_Livraison>(listToPut.Where(bl => bl.Fournisseur1 != null));
                listToPut = new ObservableCollection<Bon_Livraison>(listToPut.Where(bl => bl.Fournisseur1.Entreprise.Libelle.Trim().ToLower().Contains(this._filterContainFournisseur.Text.Trim().ToLower())));
            }
            if (this._filterContainNumeroAffaire.Text != "")
            {
                listToPut = new ObservableCollection<Bon_Livraison>(listToPut.Where(bl => bl.Affaire1 != null));
                listToPut = new ObservableCollection<Bon_Livraison>(listToPut.Where(bl => bl.Affaire1.Numero.Trim().ToLower().Contains(this._filterContainNumeroAffaire.Text.Trim().ToLower())));
            }
            if (this._filterContainNumeroCommande.Text != "")
            {
                listToPut = new ObservableCollection<Bon_Livraison>(listToPut.Where(bl => bl.Commande_Fournisseur1 != null));
                listToPut = new ObservableCollection<Bon_Livraison>(listToPut.Where(bl => bl.Commande_Fournisseur1.Numero.Trim().ToLower().Contains(this._filterContainNumeroCommande.Text.Trim().ToLower())));
            }
            if (this._filterContainDonneurOrdre.Text != "")
            {
                listToPut = new ObservableCollection<Bon_Livraison>(listToPut.Where(bl => bl.Salarie1 != null));
                listToPut = new ObservableCollection<Bon_Livraison>(listToPut.Where(bl => bl.Salarie1.Personne.fullname.Trim().ToLower().Contains(this._filterContainDonneurOrdre.Text.Trim().ToLower()) || bl.Salarie1.Personne.Initiales.Trim().ToLower().Contains(this._filterContainDonneurOrdre.Text.Trim().ToLower())));
            }
            if (this._filterContainDateEnvoi.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Bon_Livraison>(listToPut.Where(bl => bl.Date_Envoi == this._filterContainDateEnvoi.SelectedDate));
            }
            if (this._filterContainDateReception.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Bon_Livraison>(listToPut.Where(bl => bl.Date_Reception == this._filterContainDateReception.SelectedDate));
            }
            if (this._filterContainAssociation.SelectedItem != null)
            {
                listToPut = new ObservableCollection<Bon_Livraison>(listToPut.Where(dao => dao.onWhat == ((AssociationBL)this._filterContainAssociation.SelectedItem).chaine));
            }

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            if (this.listBL.Count() == 0)
            {
                MessageBox.Show("Aucun résultat ne correspont à votre recherche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region bouton masquer / afficher

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.AfficherMasquer();
        }

        public void AfficherMasquer()
        {
            if (_filterZone.Height != 21)
            {
                this._filterZone.Height = 21;
                this._ButtonMasqueFiltre.Content = "Afficher les filtres";
                //Lorsque je masque, je remet à zéro si certains champs sont rempli OU si le nombre d'élements max n'est pas égal au nombre d'éléments actuel
                if (_filterContainFournisseur.Text != "" || _filterContainNumeroBL.Text != "" || _filterContainMontant.Text != "" || _filterContainNumeroAffaire.Text != "" || _filterContainNumeroCommande.Text != "" || _filterContainAssociation.SelectedItem != null || _filterContainDonneurOrdre.Text != "" || _filterContainMontant.Text != "" || _filterContainMontant.Text != "" || _filterContainDateEnvoi.SelectedDate != null || _filterContainDateReception.SelectedDate != null || this.max != this.listBL.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainNumeroBL.Focus();
            }
        }

        #endregion

        #endregion

        #region evenements

        #region bouton click

        /// <summary>
        /// Augmente la taille de la police
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ButtonPlusPolice_Click(object sender, RoutedEventArgs e)
        {
            this._DataGridMain.FontSize = this._DataGridMain.FontSize + 0.5;
            this._textBlockTaillePolice.Text = "Taille police (" + this._DataGridMain.FontSize.ToString() + ") :     ";
        }

        /// <summary>
        /// Rappetice la taille de la police
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ButtonMoinsPolice_Click(object sender, RoutedEventArgs e)
        {
            if (this._DataGridMain.FontSize > 0.5)
            {
                this._DataGridMain.FontSize = this._DataGridMain.FontSize - 0.5;
                this._textBlockTaillePolice.Text = "Taille police (" + this._DataGridMain.FontSize.ToString() + ") :     ";
            }
        }

        #endregion

        #region double click

        /// <summary>
        /// Double click sur une ligne du datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _DataGridMain_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((App)App.Current)._theMainWindow._CommandLook.Command.Execute(((App)App.Current)._theMainWindow);
        }

        #endregion

        #region KeyUp

        /// <summary>
        /// Quand l'utilisateur fais entrée dans une AutoCompleteBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _filter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.filtrage();
            }
        }

        private void _filterContainMontant_KeyUp_1(object sender, KeyEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
        }

        #endregion

        #endregion

        #region fonctions

        /// <summary>
        /// Recalcul le nombre d'élements maximum
        /// </summary>
        private void recalculMax()
        {
            this.max = ((App)App.Current).mySitaffEntities.Bon_Livraison.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Bon_Livraison soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Bon_Livraison bl)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Filtrage des bons de livraison terminée : " + this.listBL.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la Bon_Livraison dans le linsting
                this.listBL.Add(bl);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un bon de livraison numéro '" + bl.Numero + "' effectué avec succès. Nombre d'élements : " + this.listBL.Count() + " / " + this.max);
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification du bon de livraison numéro : '" + bl.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listBL.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listBL.Remove(bl);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression du bon de livraison numéro : '" + bl.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listBL.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des bons de livraison terminé : " + this.listBL.Count() + " / " + this.max);
            }
            //Je retri les données dans le sens par défaut
            this.triDatas();
            //J'arrete la progressbar
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
        }


        /// <summary>
        /// Tri les données dans le sens par défaut
        /// </summary>
        private void triDatas()
        {
            this.listBL = new ObservableCollection<Bon_Livraison>(this.listBL.OrderBy(bl => bl.Numero));
        }

        #endregion

        #region Commandes

        #region Plus

        private void _CommandPlus_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._DataGridMain.FontSize = this._DataGridMain.FontSize + 0.5;
            this._textBlockTaillePolice.Text = "Taille police (" + this._DataGridMain.FontSize.ToString() + ") :     ";
        }

        private void _CommandPlus_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region Moins

        private void _CommandMoins_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this._DataGridMain.FontSize > 0.5)
            {
                this._DataGridMain.FontSize = this._DataGridMain.FontSize - 0.5;
            }
            this._textBlockTaillePolice.Text = "Taille police (" + this._DataGridMain.FontSize.ToString() + ") :     ";
        }

        private void _CommandMoins_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region Filtrage

        private void _CommandFiltrage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.AfficherMasquer();
        }

        private void _CommandFiltrage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #endregion
    }
}
