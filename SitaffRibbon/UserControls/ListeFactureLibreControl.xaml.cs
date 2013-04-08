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
using SitaffRibbon.Windows;
using System.Threading;
using SitaffRibbon.Classes;

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeFactureLibreControl.xaml
    /// </summary>
    public partial class ListeFactureLibreControl : UserControl
    {

        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_ColonneNumeroFacture;
        MenuItem MenuItem_ColonneDateFacture;
        MenuItem MenuItem_ColonneDateEcheance;
        MenuItem MenuItem_ColonneMontant;
        MenuItem MenuItem_ColonneMontantTVA;
        MenuItem MenuItem_ColonneMontantTTC;
        MenuItem MenuItem_ColonneConditionReglement;
        MenuItem MenuItem_ColonneClient;
        MenuItem MenuItem_ColonneTVA;
        MenuItem MenuItem_ColonneMontantRegle;
        MenuItem MenuItem_ColonneResteARegler;
        MenuItem MenuItem_ColonnePourcentageRegle;

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Propriétés de dépendances

        public ObservableCollection<Condition_Reglement> listConditionReglement
        {
            get { return (ObservableCollection<Condition_Reglement>)GetValue(listConditionReglementProperty); }
            set { SetValue(listConditionReglementProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listCondition_Reglement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listConditionReglementProperty =
            DependencyProperty.Register("listConditionReglement", typeof(ObservableCollection<Condition_Reglement>), typeof(ListeFactureLibreControl), new UIPropertyMetadata(null));

        public ObservableCollection<Facture_Libre> listFacture
        {
            get { return (ObservableCollection<Facture_Libre>)GetValue(listFactureProperty); }
            set { SetValue(listFactureProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFactureProperty =
            DependencyProperty.Register("listFacture", typeof(ObservableCollection<Facture_Libre>), typeof(ListeFactureLibreControl), new UIPropertyMetadata(null));

        public Client listClient
        {
            get { return (Client)GetValue(listClientProperty); }
            set { SetValue(listClientProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listClient.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listClientProperty =
            DependencyProperty.Register("listClient", typeof(Client), typeof(ListeFactureLibreControl), new UIPropertyMetadata(null));


        #endregion

        #region constructeur

        public ListeFactureLibreControl()
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
            this._filterZone.Height = 21;
        }

        private void initialisationAutoCompleteBox()
        {
            List<string> listConditionReglement = new List<string>();
            foreach (Facture_Libre item in ((App)App.Current).mySitaffEntities.Facture_Libre)
            {
                //Pour remplir les conditions de reglement
                if (item.Condition_Reglement1 != null)
                {
                    if (!listConditionReglement.Contains(item.Condition_Reglement1.Libelle))
                    {
                        listConditionReglement.Add(item.Condition_Reglement1.Libelle);
                    }
                }
            }

            _filterContainConditionReglement.ItemsSource = listConditionReglement;
        }

        #endregion

        #region initialisation Donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<Facture_Libre> listToPut)
        {
            if (listToPut == null)
            {
                this.listFacture = new ObservableCollection<Facture_Libre>(((App)App.Current).mySitaffEntities.Facture_Libre.OrderBy(ccf => ccf.Numero));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listFacture = new ObservableCollection<Facture_Libre>(listToPut);
                this.MiseAJourEtat("Filtrage", null);
            }
        }

        #endregion

        #region clic droit

        private void creationMenuClicDroit()
        {
            ContextMenu contextMenu = new ContextMenu();
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

            MenuItem itemAfficher6 = new MenuItem();
            itemAfficher6.Header = "Dupliquer";
            itemAfficher6.Click += new RoutedEventHandler(delegate { this.clicDroitDupliquer(); });

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

            if (securite.VerificationDroitActionsCRUD(this.ToString(), "Add"))
            {
                contextMenu.Items.Add(itemAfficher6);
                contextMenu.Items.Add(new Separator());
            }

            contextMenu.Items.Add(itemAfficher5);
        }

        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {
            this.MenuItem_ColonneNumeroFacture = new MenuItem();
            this.MenuItem_ColonneNumeroFacture.IsChecked = false;
            this.MenuItem_ColonneNumeroFacture.Header = "Numéro facture";
            this.MenuItem_ColonneNumeroFacture.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumeroFacture(); });
            this.AffMas_ColonneNumeroFacture();
            menuItem.Items.Add(this.MenuItem_ColonneNumeroFacture);

            this.MenuItem_ColonneClient = new MenuItem();
            this.MenuItem_ColonneClient.IsChecked = false;
            this.MenuItem_ColonneClient.Header = "Client";
            this.MenuItem_ColonneClient.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneClient(); });
            this.AffMas_ColonneClient();
            menuItem.Items.Add(this.MenuItem_ColonneClient);

            this.MenuItem_ColonneDateFacture = new MenuItem();
            this.MenuItem_ColonneDateFacture.IsChecked = false;
            this.MenuItem_ColonneDateFacture.Header = "Date facture";
            this.MenuItem_ColonneDateFacture.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateFacture(); });
            this.AffMas_ColonneDateFacture();
            menuItem.Items.Add(this.MenuItem_ColonneDateFacture);

            this.MenuItem_ColonneDateEcheance = new MenuItem();
            this.MenuItem_ColonneDateEcheance.IsChecked = false;
            this.MenuItem_ColonneDateEcheance.Header = "Date facture";
            this.MenuItem_ColonneDateEcheance.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateEcheance(); });
            this.AffMas_ColonneDateEcheance();
            menuItem.Items.Add(this.MenuItem_ColonneDateEcheance);

            this.MenuItem_ColonneMontant = new MenuItem();
            this.MenuItem_ColonneMontant.IsChecked = false;
            this.MenuItem_ColonneMontant.Header = "Montant HT";
            this.MenuItem_ColonneMontant.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneMontant(); });
            this.AffMas_ColonneMontant();
            menuItem.Items.Add(this.MenuItem_ColonneMontant);

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

            this.MenuItem_ColonneConditionReglement = new MenuItem();
            this.MenuItem_ColonneConditionReglement.IsChecked = false;
            this.MenuItem_ColonneConditionReglement.Header = "Condition de règlement";
            this.MenuItem_ColonneConditionReglement.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneConditionReglement(); });
            this.AffMas_ColonneConditionReglement();
            menuItem.Items.Add(this.MenuItem_ColonneConditionReglement);

            this.MenuItem_ColonneTVA = new MenuItem();
            this.MenuItem_ColonneTVA.IsChecked = true;
            this.MenuItem_ColonneTVA.Header = "Tva";
            this.MenuItem_ColonneTVA.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTVA(); });
            this.AffMas_ColonneTVA();
            menuItem.Items.Add(this.MenuItem_ColonneTVA);

            this.MenuItem_ColonneMontantRegle = new MenuItem();
            this.MenuItem_ColonneMontantRegle.IsChecked = false;
            this.MenuItem_ColonneMontantRegle.Header = "Montant réglé";
            this.MenuItem_ColonneMontantRegle.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneMontantRegle(); });
            this.AffMas_ColonneMontantRegle();
            menuItem.Items.Add(this.MenuItem_ColonneMontantRegle);

            this.MenuItem_ColonneResteARegler = new MenuItem();
            this.MenuItem_ColonneResteARegler.IsChecked = false;
            this.MenuItem_ColonneResteARegler.Header = "Reste à régler";
            this.MenuItem_ColonneResteARegler.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneResteARegler(); });
            this.AffMas_ColonneResteARegler();
            menuItem.Items.Add(this.MenuItem_ColonneResteARegler);

            this.MenuItem_ColonnePourcentageRegle = new MenuItem();
            this.MenuItem_ColonnePourcentageRegle.IsChecked = false;
            this.MenuItem_ColonnePourcentageRegle.Header = "Pourcentage réglé";
            this.MenuItem_ColonnePourcentageRegle.Click += new RoutedEventHandler(delegate { this.AffMas_ColonnePourcentageRegle(); });
            this.AffMas_ColonnePourcentageRegle();
            menuItem.Items.Add(this.MenuItem_ColonnePourcentageRegle);

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

        private void clicDroitDupliquer()
        {
            ((App)App.Current)._theMainWindow._CommandDuplicateFacture.Command.Execute(((App)App.Current)._theMainWindow);
        }

        #endregion

        #region Afficher / Masquer

        #region Tout

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneMontant.IsChecked = false;
            this.MenuItem_ColonneMontantTVA.IsChecked = false;
            this.MenuItem_ColonneMontantTTC.IsChecked = false;
            this.MenuItem_ColonneDateFacture.IsChecked = false;
            this.MenuItem_ColonneDateEcheance.IsChecked = false;
            this.MenuItem_ColonneClient.IsChecked = false;
            this.MenuItem_ColonneConditionReglement.IsChecked = false;
            this.MenuItem_ColonneNumeroFacture.IsChecked = false;
            this.MenuItem_ColonneTVA.IsChecked = false;
            this.MenuItem_ColonneMontantRegle.IsChecked = false;
            this.MenuItem_ColonneResteARegler.IsChecked = false;
            this.MenuItem_ColonnePourcentageRegle.IsChecked = false;

            this.AffMas_ColonneMontant();
            this.AffMas_ColonneMontantTVA();
            this.AffMas_ColonneMontantTTC();
            this.AffMas_ColonneNumeroFacture();
            this.AffMas_ColonneDateEcheance();
            this.AffMas_ColonneDateFacture();
            this.AffMas_ColonneTVA();
            this.AffMas_ColonneClient();
            this.AffMas_ColonneConditionReglement();
            this.AffMas_ColonneMontantRegle();
            this.AffMas_ColonneResteARegler();
            this.AffMas_ColonnePourcentageRegle();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneMontant.IsChecked = true;
            this.MenuItem_ColonneMontantTVA.IsChecked = true;
            this.MenuItem_ColonneMontantTTC.IsChecked = true;
            this.MenuItem_ColonneDateFacture.IsChecked = true;
            this.MenuItem_ColonneDateEcheance.IsChecked = true;
            this.MenuItem_ColonneClient.IsChecked = true;
            this.MenuItem_ColonneConditionReglement.IsChecked = true;
            this.MenuItem_ColonneNumeroFacture.IsChecked = true;
            this.MenuItem_ColonneTVA.IsChecked = true;
            this.MenuItem_ColonneMontantRegle.IsChecked = true;
            this.MenuItem_ColonneResteARegler.IsChecked = true;
            this.MenuItem_ColonnePourcentageRegle.IsChecked = true;


            this.AffMas_ColonneMontant();
            this.AffMas_ColonneMontantTVA();
            this.AffMas_ColonneMontantTTC();
            this.AffMas_ColonneNumeroFacture();
            this.AffMas_ColonneDateEcheance();
            this.AffMas_ColonneDateFacture();
            this.AffMas_ColonneTVA();
            this.AffMas_ColonneClient();
            this.AffMas_ColonneConditionReglement();
            this.AffMas_ColonneMontantRegle();
            this.AffMas_ColonneResteARegler();
            this.AffMas_ColonnePourcentageRegle();
        }

        #endregion

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

        private void AffMas_ColonneDateEcheance()
        {
            if (this.MenuItem_ColonneDateEcheance.IsChecked == true)
            {
                this._ColonneDateEcheance.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDateEcheance.IsChecked = false;
            }
            else
            {
                this._ColonneDateEcheance.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDateEcheance.IsChecked = true;
            }
        }

        private void AffMas_ColonneDateFacture()
        {
            if (this.MenuItem_ColonneDateFacture.IsChecked == true)
            {
                this._ColonneDateFacture.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDateFacture.IsChecked = false;
            }
            else
            {
                this._ColonneDateFacture.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDateFacture.IsChecked = true;
            }
        }

        private void AffMas_ColonneTVA()
        {
            if (this.MenuItem_ColonneTVA.IsChecked == true)
            {
                this._ColonneTVA.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTVA.IsChecked = false;
            }
            else
            {
                this._ColonneTVA.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTVA.IsChecked = true;
            }
        }

        private void AffMas_ColonneClient()
        {
            if (this.MenuItem_ColonneClient.IsChecked == true)
            {
                this._ColonneClient.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneClient.IsChecked = false;
            }
            else
            {
                this._ColonneClient.Visibility = Visibility.Visible;
                this.MenuItem_ColonneClient.IsChecked = true;
            }
        }

        private void AffMas_ColonneConditionReglement()
        {
            if (this.MenuItem_ColonneConditionReglement.IsChecked == true)
            {
                this._ColonneConditionReglement.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneConditionReglement.IsChecked = false;
            }
            else
            {
                this._ColonneConditionReglement.Visibility = Visibility.Visible;
                this.MenuItem_ColonneConditionReglement.IsChecked = true;
            }
        }

        private void AffMas_ColonnePourcentageRegle()
        {
            if (this.MenuItem_ColonnePourcentageRegle.IsChecked == true)
            {
                this._ColonnePourcentageRegle.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonnePourcentageRegle.IsChecked = false;
            }
            else
            {
                this._ColonnePourcentageRegle.Visibility = Visibility.Visible;
                this.MenuItem_ColonnePourcentageRegle.IsChecked = true;
            }
        }

        private void AffMas_ColonneResteARegler()
        {
            if (this.MenuItem_ColonneResteARegler.IsChecked == true)
            {
                this._ColonneResteARegler.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneResteARegler.IsChecked = false;
            }
            else
            {
                this._ColonneResteARegler.Visibility = Visibility.Visible;
                this.MenuItem_ColonneResteARegler.IsChecked = true;
            }
        }

        private void AffMas_ColonneMontantRegle()
        {
            if (this.MenuItem_ColonneMontantRegle.IsChecked == true)
            {
                this._ColonneMontantRegle.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneMontantRegle.IsChecked = false;
            }
            else
            {
                this._ColonneMontantRegle.Visibility = Visibility.Visible;
                this.MenuItem_ColonneMontantRegle.IsChecked = true;
            }
        }

        #endregion

        #endregion

        #region Fenêtre chargée
        /// <summary>
        /// Fin du chargement de la fenêtre (pour fermer la fenêtre de chargement)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
            ((App)App.Current)._theMainWindow.stopThread();
        }

        #endregion

        #region CRUD (Create Read Update Delete)

        /// <summary>
        /// Ajoute une nouvelle Facture à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Facture_Libre Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture en cours ...");

            //Initialisation de la fenêtre
            FactureLibreWindow factureWindow = new FactureLibreWindow();

            //Création de l'objet temporaire
            Facture_Libre tmp = new Facture_Libre();

            //Mise de l'objet temporaire dans le datacontext
            factureWindow.DataContext = tmp;


            //booléen nullable vrai ou faux ou null
            bool? dialogResult = factureWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet Facture_Libre se trouvant dans le datacontext de la fenêtre
                return (Facture_Libre)factureWindow.DataContext;
            }
            else
            {
                try
                {
                    //On détache la Facture_Libre
                    ((App)App.Current).mySitaffEntities.Detach(((Facture_Libre)factureWindow.DataContext).Litige_Facture_Client_Libre);
                    ((App)App.Current).mySitaffEntities.Detach((Facture_Libre)factureWindow.DataContext);
                }
                catch (Exception)
                {
                    try
                    {
                        //On détache la Facture_Libre
                        ((App)App.Current).mySitaffEntities.Detach((Facture_Libre)factureWindow.DataContext);
                        ((App)App.Current).mySitaffEntities.Detach(((Facture_Libre)factureWindow.DataContext).Litige_Facture_Client_Libre);
                    }
                    catch (Exception)
                    {
                    }
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture annulé : " + this.listFacture.Count() + " / " + this.max);

                return null;
            }
        }

        /// <summary>
        /// Ouvre la facture séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Facture_Libre Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une facture en cours ...");

                    //Création de la fenêtre
                    FactureLibreWindow factureWindow = new FactureLibreWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    factureWindow.DataContext = new Facture_Libre();
                    factureWindow.DataContext = (Facture_Libre)this._DataGridMain.SelectedItem;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = factureWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                        return (Facture_Libre)factureWindow.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Facture_Libre)(this._DataGridMain.SelectedItem));
                        //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une facture annulée : " + this.listFacture.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime la Facture_Libre séléctionnée avec une confirmation
        /// </summary>
        public Facture_Libre Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une facture en cours ...");

                    if (MessageBox.Show("Voulez-vous rééllement supprimer la facture séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //Supprimer l'élément 
                        return (Facture_Libre)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une facture annulée : " + this.listFacture.Count() + " / " + this.max);

                        return null;
                    }

                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre la facture séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Facture_Libre Look()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une facture en cours ...");

                    //Création de la fenêtre
                    FactureLibreWindow factureWindow = new FactureLibreWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    factureWindow.DataContext = new Facture_Libre();
                    factureWindow.DataContext = (Facture_Libre)this._DataGridMain.SelectedItem;

                    //Je positionne la lecture seule sur la fenêtre
                    factureWindow.lectureSeule();
                    factureWindow.soloLecture = true;

                    //J'affiche la fenêtre
                    bool? dialogResult = factureWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une facture terminé : " + this.listFacture.Count() + " / " + this.max);

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        #endregion

        #region Actions supplémentaires

        /// <summary>
        /// duplique une Commande_Fournisseur à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Facture_Libre Duplicate()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "ajout en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer une facture en cours ...");

                    //Création de la fenêtre
                    FactureLibreWindow factureWindow = new FactureLibreWindow();

                    //Duplication de la commande sélectionnée
                    Facture_Libre tmp = new Facture_Libre();
                    tmp = duplicateCommande((Facture_Libre)this._DataGridMain.SelectedItem);

                    //Association de l'élement dupliqué au datacontext de la fenêtre
                    factureWindow.DataContext = tmp;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = factureWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Facture_Libre)factureWindow.DataContext;
                    }
                    else
                    {
                        try
                        {
                            //On détache la commande
                            ((App)App.Current).mySitaffEntities.Detach((Facture_Libre)factureWindow.DataContext);
                        }
                        catch (Exception)
                        {
                        }

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer une facture annulé : " + this.listFacture.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        #endregion

        #region Filtrage

        #region remise a zero
        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            //Text
            _filterContainMontant.Text = "";
            _filterContainNumeroFacture.Text = "";

            //Dates
            this._filterContainDateEcheance.SelectedDate = null;
            this._filterContainDateFacture.SelectedDate = null;
            this._filterContainDateDebutFacture.SelectedDate = null;
            this._filterContainDateFinFacture.SelectedDate = null;

            //AutoCompleteBox
            this._filterContainConditionReglement.Text = "";

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

            ObservableCollection<Facture_Libre> listToPut = new ObservableCollection<Facture_Libre>(((App)App.Current).mySitaffEntities.Facture_Libre.OrderBy(ccf => ccf.Numero));

            if (this._filterContainNumeroFacture.Text != "")
            {
                listToPut = new ObservableCollection<Facture_Libre>(listToPut.Where(fac => fac.Numero.Trim().ToLower().Contains(this._filterContainNumeroFacture.Text.Trim().ToLower())));
            }
            if (this._filterContainDateFacture.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Facture_Libre>(listToPut.Where(fac => fac.Date_Facture != null));
                listToPut = new ObservableCollection<Facture_Libre>(listToPut.Where(fac => fac.Date_Facture == this._filterContainDateFacture.SelectedDate));
            }
            if (this._filterContainDateEcheance.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Facture_Libre>(listToPut.Where(fac => fac.Date_Echeance != null));
                listToPut = new ObservableCollection<Facture_Libre>(listToPut.Where(fac => fac.Date_Echeance == this._filterContainDateEcheance.SelectedDate));
            }
            if (this._filterContainConditionReglement.Text != "")
            {
                listToPut = new ObservableCollection<Facture_Libre>(listToPut.Where(fac => fac.Condition_Reglement1 != null));
                listToPut = new ObservableCollection<Facture_Libre>(listToPut.Where(fac => fac.Condition_Reglement1.Libelle.Trim().ToLower().Contains(this._filterContainConditionReglement.Text.Trim().ToLower())));
            }
            if (this._filterContainMontant.Text != "")
            {
                double val;
                if (double.TryParse(this._filterContainMontant.Text, out val))
                {
                    listToPut = new ObservableCollection<Facture_Libre>(listToPut.Where(fac => fac.Montant != null));
                    listToPut = new ObservableCollection<Facture_Libre>(listToPut.Where(fac => fac.Montant.ToString().Contains(double.Parse(this._filterContainMontant.Text.Trim()).ToString())));
                }
            }
            if (this._filterContainDateDebutFacture.SelectedDate != null)
            {
                DateTime temp = new DateTime(this._filterContainDateDebutFacture.SelectedDate.Value.Year, this._filterContainDateDebutFacture.SelectedDate.Value.Month, this._filterContainDateDebutFacture.SelectedDate.Value.Day, 00, 00, 00);
                this._filterContainDateDebutFacture.SelectedDate = temp;
                listToPut = new ObservableCollection<Facture_Libre>(listToPut.Where(com => com.Date_Facture != null));
                listToPut = new ObservableCollection<Facture_Libre>(listToPut.Where(com => com.Date_Facture >= this._filterContainDateDebutFacture.SelectedDate));
            }
            if (this._filterContainDateFinFacture.SelectedDate != null)
            {
                DateTime temp = new DateTime(this._filterContainDateFinFacture.SelectedDate.Value.Year, this._filterContainDateFinFacture.SelectedDate.Value.Month, this._filterContainDateFinFacture.SelectedDate.Value.Day, 23, 59, 59);
                this._filterContainDateFinFacture.SelectedDate = temp;
                listToPut = new ObservableCollection<Facture_Libre>(listToPut.Where(com => com.Date_Facture != null));
                listToPut = new ObservableCollection<Facture_Libre>(listToPut.Where(com => com.Date_Facture <= this._filterContainDateFinFacture.SelectedDate));
            }

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            if (this.listFacture.Count() == 0)
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
                if (_filterContainDateDebutFacture.SelectedDate != null || _filterContainDateFinFacture.SelectedDate != null || _filterContainNumeroFacture.Text != "" || _filterContainConditionReglement.Text != "" || _filterContainMontant.Text != "" || _filterContainDateFacture.SelectedDate != null || _filterContainDateEcheance.SelectedDate != null || this.max != this.listFacture.Count())
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

        #endregion

        #region évenements

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
        #endregion

        #endregion

        #region fonctions

        /// <summary>
        /// Recalcul le nombre d'élements maximum
        /// </summary>
        private void recalculMax()
        {
            this.max = ((App)App.Current).mySitaffEntities.Facture.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Facture_Libre fac)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des factures terminée : " + this.listFacture.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listFacture.Add(fac);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture numéro '" + fac.Numero + "' effectué avec succès. Nombre d'élements : " + this.listFacture.Count() + " / " + this.max);
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification de la facture numéro : '" + fac.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listFacture.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listFacture.Remove(fac);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression de la facture numéro : '" + fac.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listFacture.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else if (typeEtat == "Duplicate")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listFacture.Add(fac);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer une facture numéro '" + fac.Numero + "' effectué avec succès. Nombre d'élements : " + this.listFacture.Count() + " / " + this.max);
            }
            else
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des factures terminé : " + this.listFacture.Count() + " / " + this.max);
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
            this.listFacture = new ObservableCollection<Facture_Libre>(this.listFacture.OrderBy(nf => nf.Numero));
        }

        /// <summary>
        /// duplique la facture passée en paramètre
        /// </summary>
        /// <param name="itemToCopy">facture à dupliquer</param>
        private Facture_Libre duplicateCommande(Facture_Libre itemToCopy)
        {
            Facture_Libre tmp = new Facture_Libre();

            tmp.Condition_Reglement1 = itemToCopy.Condition_Reglement1;
            tmp.Date_Facture = itemToCopy.Date_Facture;
            tmp.Numero = itemToCopy.Numero;
            tmp.Tva1 = itemToCopy.Tva1;

            return tmp;
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
