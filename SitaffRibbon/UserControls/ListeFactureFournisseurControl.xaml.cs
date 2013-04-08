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
using System.ComponentModel;
using SitaffRibbon.Windows;
using SitaffRibbon.Classes;

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeFactureFournisseurControl.xaml
    /// </summary>
    public partial class ListeFactureFournisseurControl : UserControl
    {

        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_ColonneNumeroFacture;
        MenuItem MenuItem_ColonneFournisseur;
        MenuItem MenuItem_ColonneDate;
        MenuItem MenuItem_ColonneNumeroComptable;
        MenuItem MenuItem_ColonneMontantHT;
        MenuItem MenuItem_ColonneMontantTVA;
        MenuItem MenuItem_ColonneMontantTTC;
        MenuItem MenuItem_ColonneDifference;
        MenuItem MenuItem_ColonneAvoir;
        MenuItem MenuItem_ColonneAccompte;

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Propd

        public ObservableCollection<Facture_Fournisseur> listFactures
        {
            get { return (ObservableCollection<Facture_Fournisseur>)GetValue(listFacturesProperty); }
            set { SetValue(listFacturesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listFactures.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFacturesProperty =
            DependencyProperty.Register("listFactures", typeof(ObservableCollection<Facture_Fournisseur>), typeof(ListeFactureFournisseurControl), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public ListeFactureFournisseurControl()
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

        #region initialisation Zone de filtrage

        private void initialisationFilterZone()
        {
            this.initialisationAutoCompleteBox();
            this.initialisationComboBoxOuiNon();
            this._filterZone.Height = 21;
        }

        private void initialisationAutoCompleteBox()
        {
            List<string> listFournisseur = new List<string>();
            foreach (Facture_Fournisseur item in ((App)App.Current).mySitaffEntities.Facture_Fournisseur)
            {
                //Pour remplir les fournisseurs
                if (item.Fournisseur1 != null)
                {
                    if (item.Fournisseur1.Entreprise != null)
                    {
                        if (!listFournisseur.Contains(item.Fournisseur1.Entreprise.Libelle))
                        {
                            listFournisseur.Add(item.Fournisseur1.Entreprise.Libelle);
                        }
                    }
                }
            }

            _filterContainFournisseur.ItemsSource = listFournisseur;
        }

        private void initialisationComboBoxOuiNon()
        {
            ObservableCollection<ItemOuiNon> listItemOuiNon = new ObservableCollection<ItemOuiNon>();
            listItemOuiNon.Add(new ItemOuiNon("Oui"));
            listItemOuiNon.Add(new ItemOuiNon("Non"));
            this._filterContainAvoir.ItemsSource = listItemOuiNon;
        }


        #endregion

        #region initialisation Donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<Facture_Fournisseur> listToPut)
        {
            if (listToPut == null)
            {
                this.listFactures = new ObservableCollection<Facture_Fournisseur>(((App)App.Current).mySitaffEntities.Facture_Fournisseur.OrderBy(fac => fac.Numero_Piece_Comptable));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listFactures = new ObservableCollection<Facture_Fournisseur>(listToPut);
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

            MenuItem itemAfficher5 = new MenuItem();
            itemAfficher5.Header = "Relevé de facture(s)";
            itemAfficher5.Click += new RoutedEventHandler(delegate { this.menuReleve(); });

            MenuItem itemAfficher8 = RemplirMenuAfficherMasquerColonnes(new MenuItem());
            itemAfficher8.Header = "Afficher / Masquer";

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

            contextMenu.Items.Add(new Separator());

            contextMenu.Items.Add(itemAfficher8);
        }

        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {
            this.MenuItem_ColonneNumeroFacture = new MenuItem();
            this.MenuItem_ColonneNumeroFacture.IsChecked = false;
            this.MenuItem_ColonneNumeroFacture.Header = "Numéro de facture";
            this.MenuItem_ColonneNumeroFacture.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumeroFacture(); });
            this.AffMas_ColonneNumeroFacture();
            menuItem.Items.Add(this.MenuItem_ColonneNumeroFacture);

            this.MenuItem_ColonneNumeroComptable = new MenuItem();
            this.MenuItem_ColonneNumeroComptable.IsChecked = false;
            this.MenuItem_ColonneNumeroComptable.Header = "Numéro de pièce comptable";
            this.MenuItem_ColonneNumeroComptable.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumeroComptable(); });
            this.AffMas_ColonneNumeroComptable();
            menuItem.Items.Add(this.MenuItem_ColonneNumeroComptable);

            this.MenuItem_ColonneDate = new MenuItem();
            this.MenuItem_ColonneDate.IsChecked = false;
            this.MenuItem_ColonneDate.Header = "Date";
            this.MenuItem_ColonneDate.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDate(); });
            this.AffMas_ColonneDate();
            menuItem.Items.Add(this.MenuItem_ColonneDate);

            this.MenuItem_ColonneFournisseur = new MenuItem();
            this.MenuItem_ColonneFournisseur.IsChecked = false;
            this.MenuItem_ColonneFournisseur.Header = "Fournisseur";
            this.MenuItem_ColonneFournisseur.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFournisseur(); });
            this.AffMas_ColonneFournisseur();
            menuItem.Items.Add(this.MenuItem_ColonneFournisseur);

            this.MenuItem_ColonneMontantHT = new MenuItem();
            this.MenuItem_ColonneMontantHT.IsChecked = false;
            this.MenuItem_ColonneMontantHT.Header = "Montant HT";
            this.MenuItem_ColonneMontantHT.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneMontantHT(); });
            this.AffMas_ColonneMontantHT();
            menuItem.Items.Add(this.MenuItem_ColonneMontantHT);

            this.MenuItem_ColonneMontantTVA = new MenuItem();
            this.MenuItem_ColonneMontantTVA.IsChecked = false;
            this.MenuItem_ColonneMontantTVA.Header = "Montant TVA";
            this.MenuItem_ColonneMontantTVA.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneMontantTVA(); });
            this.AffMas_ColonneMontantTVA();
            menuItem.Items.Add(this.MenuItem_ColonneMontantTVA);

            this.MenuItem_ColonneMontantTTC = new MenuItem();
            this.MenuItem_ColonneMontantTTC.IsChecked = false;
            this.MenuItem_ColonneMontantTTC.Header = "Montant TTC";
            this.MenuItem_ColonneMontantTTC.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneMontantTTC(); });
            this.AffMas_ColonneMontantTTC();
            menuItem.Items.Add(this.MenuItem_ColonneMontantTTC);

            this.MenuItem_ColonneDifference = new MenuItem();
            this.MenuItem_ColonneDifference.IsChecked = true;
            this.MenuItem_ColonneDifference.Header = "Différence";
            this.MenuItem_ColonneDifference.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDifference(); });
            this.AffMas_ColonneDifference();
            menuItem.Items.Add(this.MenuItem_ColonneDifference);

            this.MenuItem_ColonneAvoir = new MenuItem();
            this.MenuItem_ColonneAvoir.IsChecked = true;
            this.MenuItem_ColonneAvoir.Header = "Avoir ?";
            this.MenuItem_ColonneAvoir.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneAvoir(); });
            this.AffMas_ColonneAvoir();
            menuItem.Items.Add(this.MenuItem_ColonneAvoir);

            this.MenuItem_ColonneAccompte = new MenuItem();
            this.MenuItem_ColonneAccompte.IsChecked = true;
            this.MenuItem_ColonneAccompte.Header = "Accompte";
            this.MenuItem_ColonneAccompte.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneAccompte(); });
            this.AffMas_ColonneAccompte();
            menuItem.Items.Add(this.MenuItem_ColonneAccompte);

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

        private void menuReleve()
        {
            ((App)App.Current)._theMainWindow._CommandRapportReleveFactureFournisseur.Command.Execute(((App)App.Current)._theMainWindow);
        }

        #region Afficher / Masquer

        #region Tout

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneNumeroFacture.IsChecked = false;
            this.MenuItem_ColonneFournisseur.IsChecked = false;
            this.MenuItem_ColonneDate.IsChecked = false;
            this.MenuItem_ColonneNumeroComptable.IsChecked = false;
            this.MenuItem_ColonneMontantHT.IsChecked = false;
            this.MenuItem_ColonneMontantTVA.IsChecked = false;
            this.MenuItem_ColonneMontantTTC.IsChecked = false;
            this.MenuItem_ColonneDifference.IsChecked = false;
            this.MenuItem_ColonneAvoir.IsChecked = false;
            this.MenuItem_ColonneAccompte.IsChecked = false;

            this.AffMas_ColonneNumeroFacture();
            this.AffMas_ColonneFournisseur();
            this.AffMas_ColonneDate();
            this.AffMas_ColonneNumeroComptable();
            this.AffMas_ColonneMontantHT();
            this.AffMas_ColonneMontantTVA();
            this.AffMas_ColonneMontantTTC();
            this.AffMas_ColonneDifference();
            this.AffMas_ColonneAvoir();
            this.AffMas_ColonneAccompte();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneNumeroFacture.IsChecked = true;
            this.MenuItem_ColonneFournisseur.IsChecked = true;
            this.MenuItem_ColonneDate.IsChecked = true;
            this.MenuItem_ColonneNumeroComptable.IsChecked = true;
            this.MenuItem_ColonneMontantHT.IsChecked = true;
            this.MenuItem_ColonneMontantTVA.IsChecked = true;
            this.MenuItem_ColonneMontantTTC.IsChecked = true;
            this.MenuItem_ColonneDifference.IsChecked = true;
            this.MenuItem_ColonneAvoir.IsChecked = true;
            this.MenuItem_ColonneAccompte.IsChecked = true;

            this.AffMas_ColonneNumeroFacture();
            this.AffMas_ColonneFournisseur();
            this.AffMas_ColonneDate();
            this.AffMas_ColonneNumeroComptable();
            this.AffMas_ColonneMontantHT();
            this.AffMas_ColonneMontantTVA();
            this.AffMas_ColonneMontantTTC();
            this.AffMas_ColonneDifference();
            this.AffMas_ColonneAvoir();
            this.AffMas_ColonneAccompte();
        }

        #endregion

        private void AffMas_ColonneNumeroFacture()
        {
            if (this.MenuItem_ColonneNumeroFacture.IsChecked == true)
            {
                this._ColonneNumeroFacture.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNumeroFacture.IsChecked = false;
            }
            else
            {
                this._ColonneNumeroFacture.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNumeroFacture.IsChecked = true;
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

        private void AffMas_ColonneDate()
        {
            if (this.MenuItem_ColonneDate.IsChecked == true)
            {
                this._ColonneDate.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDate.IsChecked = false;
            }
            else
            {
                this._ColonneDate.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDate.IsChecked = true;
            }
        }

        private void AffMas_ColonneNumeroComptable()
        {
            if (this.MenuItem_ColonneNumeroComptable.IsChecked == true)
            {
                this._ColonneNumeroComptable.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNumeroComptable.IsChecked = false;
            }
            else
            {
                this._ColonneNumeroComptable.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNumeroComptable.IsChecked = true;
            }
        }

        private void AffMas_ColonneMontantHT()
        {
            if (this.MenuItem_ColonneMontantHT.IsChecked == true)
            {
                this._ColonneMontantHT.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneMontantHT.IsChecked = false;
            }
            else
            {
                this._ColonneMontantHT.Visibility = Visibility.Visible;
                this.MenuItem_ColonneMontantHT.IsChecked = true;
            }
        }

        private void AffMas_ColonneMontantTVA()
        {
            if (this.MenuItem_ColonneMontantTVA.IsChecked == true)
            {
                this._ColonneMontantTVA.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneMontantTVA.IsChecked = false;
            }
            else
            {
                this._ColonneMontantTVA.Visibility = Visibility.Visible;
                this.MenuItem_ColonneMontantTVA.IsChecked = true;
            }
        }

        private void AffMas_ColonneMontantTTC()
        {
            if (this.MenuItem_ColonneMontantTTC.IsChecked == true)
            {
                this._ColonneMontantTTC.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneMontantTTC.IsChecked = false;
            }
            else
            {
                this._ColonneMontantTTC.Visibility = Visibility.Visible;
                this.MenuItem_ColonneMontantTTC.IsChecked = true;
            }
        }

        private void AffMas_ColonneDifference()
        {
            if (this.MenuItem_ColonneDifference.IsChecked == true)
            {
                this._ColonneDifference.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDifference.IsChecked = false;
            }
            else
            {
                this._ColonneDifference.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDifference.IsChecked = true;
            }
        }

        private void AffMas_ColonneAvoir()
        {
            if (this.MenuItem_ColonneAvoir.IsChecked == true)
            {
                this._ColonneAvoir.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneAvoir.IsChecked = false;
            }
            else
            {
                this._ColonneAvoir.Visibility = Visibility.Visible;
                this.MenuItem_ColonneAvoir.IsChecked = true;
            }
        }

        private void AffMas_ColonneAccompte()
        {
            if (this.MenuItem_ColonneAccompte.IsChecked == true)
            {
                this._ColonneAccompte.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneAccompte.IsChecked = false;
            }
            else
            {
                this._ColonneAccompte.Visibility = Visibility.Visible;
                this.MenuItem_ColonneAccompte.IsChecked = true;
            }
        }

        #endregion

        #endregion

        #endregion

        #region Fenêtre chargée

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
            ((App)App.Current)._theMainWindow.stopThread();
        }

        #endregion

        #region CRUD (Create Read Update Delete)
        /// <summary>
        /// Ajoute une nouvelle facture fournisseur à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Facture_Fournisseur Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture fournisseur en cours ...");

            //Initialisation de la fenêtre
            FactureFournisseurWindow factureFournisseurWindow = new FactureFournisseurWindow();

            //Création de l'objet temporaire
            Facture_Fournisseur tmp = new Facture_Fournisseur();

            //Mise de l'objet temporaire dans le datacontext
            factureFournisseurWindow.DataContext = tmp;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = factureFournisseurWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet facture fournisseur se trouvant dans le datacontext de la fenêtre
                return (Facture_Fournisseur)factureFournisseurWindow.DataContext;
            }
            else
            {
                try
                {
                    //On détache tous les éléments liés à la facture
                    ObservableCollection<Facture_Fournisseur_Contenu> toRemove = new ObservableCollection<Facture_Fournisseur_Contenu>();
                    foreach (Facture_Fournisseur_Contenu item in ((Facture_Fournisseur)factureFournisseurWindow.DataContext).Facture_Fournisseur_Contenu)
                    {
                        toRemove.Add(item);
                    }
                    foreach (Facture_Fournisseur_Contenu item in toRemove)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    ObservableCollection<Avoir_Facture_Fournisseur> toRemove2 = new ObservableCollection<Avoir_Facture_Fournisseur>();
                    foreach (Avoir_Facture_Fournisseur item in ((Facture_Fournisseur)factureFournisseurWindow.DataContext).Avoir_Facture_Fournisseur)
                    {
                        toRemove2.Add(item);
                    }
                    foreach (Avoir_Facture_Fournisseur item in toRemove2)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    ObservableCollection<Facture_Fournisseur_Condition_Reglement> toRemove3 = new ObservableCollection<Facture_Fournisseur_Condition_Reglement>();
                    foreach (Facture_Fournisseur_Condition_Reglement item in ((Facture_Fournisseur)factureFournisseurWindow.DataContext).Facture_Fournisseur_Condition_Reglement)
                    {
                        toRemove3.Add(item);
                    }
                    foreach (Facture_Fournisseur_Condition_Reglement item in toRemove3)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    ObservableCollection<Litige_Facture_Fournisseur> toRemove4 = new ObservableCollection<Litige_Facture_Fournisseur>();
                    foreach (Litige_Facture_Fournisseur item in ((Facture_Fournisseur)factureFournisseurWindow.DataContext).Litige_Facture_Fournisseur)
                    {
                        toRemove4.Add(item);
                    }
                    foreach (Litige_Facture_Fournisseur item in toRemove4)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    //On détache la facture
                    ((App)App.Current).mySitaffEntities.Detach((Facture_Fournisseur)factureFournisseurWindow.DataContext);
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture fournisseur annulé : " + this.listFactures.Count() + " / " + this.max);

                return null;
            }
        }

        /// <summary>
        /// Ouvre la facture fournisseur séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Facture_Fournisseur Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une facture fournisseur en cours ...");

                    //Création de la fenêtre
                    FactureFournisseurWindow factureFournisseurWindow = new FactureFournisseurWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    factureFournisseurWindow.DataContext = new Facture_Fournisseur();
                    factureFournisseurWindow.DataContext = this._DataGridMain.SelectedItem;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = factureFournisseurWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                        return (Facture_Fournisseur)factureFournisseurWindow.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Facture_Fournisseur)(this._DataGridMain.SelectedItem));
                        //La facture étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une facture fournisseur annulée : " + this.listFactures.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule facture fournisseur.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une facture fournisseur.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime la Facture_Fournisseur séléctionnée avec une confirmation
        /// </summary>
        public Facture_Fournisseur Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une commande fournisseur en cours ...");

                    if (MessageBox.Show("Voulez-vous rééllement supprimer la facture fournisseur séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //On détache tous les éléments liés à la facture
                        ObservableCollection<Facture_Fournisseur_Contenu> toRemove = new ObservableCollection<Facture_Fournisseur_Contenu>();
                        foreach (Facture_Fournisseur_Contenu item in ((Facture_Fournisseur)this._DataGridMain.SelectedItem).Facture_Fournisseur_Contenu)
                        {
                            toRemove.Add(item);
                        }
                        foreach (Facture_Fournisseur_Contenu item in toRemove)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }

                        ObservableCollection<Avoir_Facture_Fournisseur> toRemove2 = new ObservableCollection<Avoir_Facture_Fournisseur>();
                        foreach (Avoir_Facture_Fournisseur item in ((Facture_Fournisseur)this._DataGridMain.SelectedItem).Avoir_Facture_Fournisseur)
                        {
                            toRemove2.Add(item);
                        }
                        foreach (Avoir_Facture_Fournisseur item in toRemove2)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }

                        ObservableCollection<Facture_Fournisseur_Condition_Reglement> toRemove3 = new ObservableCollection<Facture_Fournisseur_Condition_Reglement>();
                        foreach (Facture_Fournisseur_Condition_Reglement item in ((Facture_Fournisseur)this._DataGridMain.SelectedItem).Facture_Fournisseur_Condition_Reglement)
                        {
                            toRemove3.Add(item);
                        }
                        foreach (Facture_Fournisseur_Condition_Reglement item in toRemove3)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }

                        ObservableCollection<Litige_Facture_Fournisseur> toRemove4 = new ObservableCollection<Litige_Facture_Fournisseur>();
                        foreach (Litige_Facture_Fournisseur item in ((Facture_Fournisseur)this._DataGridMain.SelectedItem).Litige_Facture_Fournisseur)
                        {
                            toRemove4.Add(item);
                        }
                        foreach (Litige_Facture_Fournisseur item in toRemove4)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }

                        ObservableCollection<Bon_Livraison> toRemove5 = new ObservableCollection<Bon_Livraison>();
                        foreach (Bon_Livraison item in ((Facture_Fournisseur)this._DataGridMain.SelectedItem).Bon_Livraison)
                        {
                            toRemove5.Add(item);
                        }
                        foreach (Bon_Livraison item in toRemove5)
                        {
                            ((Facture_Fournisseur)this._DataGridMain.SelectedItem).Bon_Livraison.Remove(item);
                        }

                        //Supprimer l'élément 
                        return (Facture_Fournisseur)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une facture fournisseur annulée : " + this.listFactures.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule facture fournisseur.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une facture fournisseur.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre la dao séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Facture_Fournisseur Look()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une commande fournisseur en cours ...");

                    //Création de la fenêtre
                    FactureFournisseurWindow factureFournisseurWindow = new FactureFournisseurWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association
                    factureFournisseurWindow.DataContext = new Facture_Fournisseur();
                    factureFournisseurWindow.DataContext = this._DataGridMain.SelectedItem;

                    //Je positionne la lecture seule sur la fenêtre
                    factureFournisseurWindow.lectureSeule();

                    //J'affiche la fenêtre
                    bool? dialogResult = factureFournisseurWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une facture fournisseur terminé : " + this.listFactures.Count() + " / " + this.max);

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule facture fournisseur.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une facture fournisseur.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre le rapport séléctionné
        /// </summary>
        public void RapportReleveFacture()
        {
            ReportingWindow reportingWindow = new ReportingWindow();
            reportingWindow._webBrowser.Source = new Uri("http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fFactures%2fReleve_fact_fournisseur&rs:Command=Render");
            reportingWindow.Title = "Rapport pour relevé de facture(s)";

            reportingWindow.Show();
        }

        #endregion

        #region Filtrages

        #region Remise à Zéro

        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            //Remise à zéro de tous les champs de filtrage
            //Text
            this._filterContainAvoir.Text = "";
            this._filterContainMontantHT.Text = "";
            this._filterContainMontantTTC.Text = "";
            this._filterContainNumeroFacture.Text = "";
            this._filterContainNumeroPieceComptable.Text = "";

            //Dates
            this._filterContainDateFacture.SelectedDate = null;

            //ComboBox
            this._filterContainFournisseur.SelectedItem = null;

            //Rechargement des élements
            this.initialisationDataDatagridMain(null);
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
                if (this._filterContainMontantHT.Text != "" || this._filterContainMontantTTC.Text != "" || this._filterContainFournisseur.Text != "" || this._filterContainAvoir.SelectedItem != null || this._filterContainDateFacture.SelectedDate != null || this._filterContainNumeroPieceComptable.Text != "" || this._filterContainNumeroFacture.Text != "" || this.max != this.listFactures.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainNumeroFacture.Focus();
            }
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

            ObservableCollection<Facture_Fournisseur> listToPut = new ObservableCollection<Facture_Fournisseur>(((App)App.Current).mySitaffEntities.Facture_Fournisseur.OrderBy(ccf => ccf.Numero));

            if (this._filterContainNumeroFacture.Text != "")
            {
                listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(fac => fac.Numero != null));
                listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(fac => fac.Numero.Trim().ToLower().Contains(this._filterContainNumeroFacture.Text.Trim().ToLower())));
            }
            if (this._filterContainNumeroPieceComptable.Text != "")
            {
                listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(fac => fac.Numero_Piece_Comptable != null));
                listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(fac => fac.Numero_Piece_Comptable.Trim().ToLower().Contains(this._filterContainNumeroPieceComptable.Text.Trim().ToLower())));
            }
            if (this._filterContainDateFacture.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(fac => fac.Date_Facture != null));
                listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(fac => fac.Date_Facture.Value.Year == this._filterContainDateFacture.SelectedDate.Value.Year && fac.Date_Facture.Value.Month == this._filterContainDateFacture.SelectedDate.Value.Month && fac.Date_Facture.Value.Day == this._filterContainDateFacture.SelectedDate.Value.Day));
            }
            if (this._filterContainMontantHT.Text != "")
            {
                double val;
                if (double.TryParse(this._filterContainMontantHT.Text.Trim(), out val))
                {
                    listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(fac => fac.Montant_HT.ToString().Contains(double.Parse(this._filterContainMontantHT.Text.Trim()).ToString())));
                }
            }
            if (this._filterContainMontantTTC.Text != "")
            {
                double val;
                if (double.TryParse(this._filterContainMontantTTC.Text.Trim(), out val))
                {
                    listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(fac => fac.Montant_TTC.ToString().Contains(double.Parse(this._filterContainMontantTTC.Text.Trim()).ToString())));
                }
            }
            if (this._filterContainFournisseur.Text != "")
            {
                listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(fac => fac.Fournisseur1 != null));
                listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(fac => fac.Fournisseur1.Entreprise != null));
                listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(fac => fac.Fournisseur1.Entreprise.Libelle.Trim().ToLower().Contains(this._filterContainFournisseur.Text.Trim().ToLower())));
            }
            if (this._filterContainAvoir.SelectedItem != null)
            {
                if (((ItemOuiNon)this._filterContainAvoir.SelectedItem).chaine == "Oui")
                {
                    listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(fac => fac.YATIlUnAvoir == "oui"));
                }
                else
                {
                    listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(fac => fac.YATIlUnAvoir == "non"));
                }
            }
            if (this._filterContainDateDebutFacture.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(com => com.Date_Facture != null));
                listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(com => com.Date_Facture.Value.Date >= this._filterContainDateDebutFacture.SelectedDate.Value.Date));
            }
            if (this._filterContainDateFinFacture.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(com => com.Date_Facture != null));
                listToPut = new ObservableCollection<Facture_Fournisseur>(listToPut.Where(com => com.Date_Facture.Value.Date <= this._filterContainDateFinFacture.SelectedDate.Value.Date));
            }

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            //Si aucun résultat, j'affiche un message
            if (this.listFactures.Count() == 0)
            {
                MessageBox.Show("Aucun résultat ne correspont à votre recherche.", "Aucun résultat", MessageBoxButton.OK);
            }
        }

        #endregion

        #region ComboBox null

        private void NullToutLivre_Click_1(object sender, RoutedEventArgs e)
        {
            this._filterContainAvoir.SelectedItem = null;
        }

        #endregion

        #endregion

        #region évenements

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

        #region Fonctions

        /// <summary>
        /// Recalcul le nombre d'élements maximum
        /// </summary>
        private void recalculMax()
        {
            this.max = ((App)App.Current).mySitaffEntities.Facture_Fournisseur.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Facture_Fournisseur ff)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des factures fournisseur terminée : " + this.listFactures.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listFactures.Add(ff);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture fournisseur numéro '" + ff.Numero + "' effectué avec succès. Nombre d'élements : " + this.listFactures.Count() + " / " + this.max);
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification de la facture fournisseur numéro : '" + ff.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listFactures.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listFactures.Remove(ff);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression de la facture fournisseur numéro : '" + ff.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listFactures.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else if (typeEtat == "Duplicate")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listFactures.Add(ff);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer une facture fournisseur numéro '" + ff.Numero + "' effectué avec succès. Nombre d'élements : " + this.listFactures.Count() + " / " + this.max);
            }
            else
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des factures fournisseur terminé : " + this.listFactures.Count() + " / " + this.max);
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
            this.listFactures = new ObservableCollection<Facture_Fournisseur>(this.listFactures.OrderBy(ccf => ccf.Numero_Piece_Comptable));
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
