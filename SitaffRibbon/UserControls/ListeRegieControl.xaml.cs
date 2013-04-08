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
using System.ComponentModel;

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeRegieControl.xaml
    /// </summary>
    public partial class ListeRegieControl : UserControl
    {
        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_ColonneRegie;
        MenuItem MenuItem_ColonneAffaire;
        MenuItem MenuItem_ColonneClient;
        MenuItem MenuItem_ColonneEtat;
        MenuItem MenuItem_ColonneHeure;
        MenuItem MenuItem_ColonneDateDebut;
        MenuItem MenuItem_ColonneDateFin;
        MenuItem MenuItem_ColonnePrixTotal;
        MenuItem MenuItem_ColonneVersionDevis;
        MenuItem MenuItem_ColonneEtatRegie;
        MenuItem MenuItem_ColonneTemps;
        MenuItem MenuItem_ColonneUtilisateur;
        MenuItem MenuItem_ColonnePrixTotalHeures;

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Propriétés de dépendances

        public ObservableCollection<Regie> listRegie
        {
            get { return (ObservableCollection<Regie>)GetValue(listRegieProperty); }
            set { SetValue(listRegieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listRegie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listRegieProperty =
            DependencyProperty.Register("listRegie", typeof(ObservableCollection<Regie>), typeof(ListeRegieControl), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public ListeRegieControl()
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
            this.initialisationComboBox();
            this.initialisationAutoCompleteBox();
            this._filterZone.Height = 21;
        }

        private void initialisationAutoCompleteBox()
        {
            List<string> listAffaire = new List<string>();
            List<string> listClient = new List<string>();
            foreach (Regie item in ((App)App.Current).mySitaffEntities.Regie)
            {
                //Pour remplir les affaires
                if (item.Affaire1 != null)
                {
                    if (!listAffaire.Contains(item.Affaire1.Numero))
                    {
                        listAffaire.Add(item.Affaire1.Numero);
                    }
                }

                //Pour remplir les clients
                if (item.Client1 != null)
                {
                    if (item.Client1.Entreprise != null)
                    {
                        if (!listClient.Contains(item.Client1.Entreprise.Libelle))
                        {
                            listClient.Add(item.Client1.Entreprise.Libelle);
                        }
                    }
                }
            }

            _filterContainAffaire.ItemsSource = listAffaire;
            _filterContainClient.ItemsSource = listClient;
        }

        private void initialisationComboBox()
        {
            ObservableCollection<EtatDeRegie> listEtatRegie = new ObservableCollection<EtatDeRegie>();
            //listEtatRegie.Add(new EtatDeRegie("Erreur"));
            listEtatRegie.Add(new EtatDeRegie("En attente de valorisation"));
            listEtatRegie.Add(new EtatDeRegie("En attente de chiffrage"));
            listEtatRegie.Add(new EtatDeRegie("En attente BC client"));
            listEtatRegie.Add(new EtatDeRegie("A facturer"));
            listEtatRegie.Add(new EtatDeRegie("Facturé"));
            this._filterContainEtatRegie.ItemsSource = listEtatRegie;
        }
        #endregion

        #region initialisation donnés datagridMain
        private void initialisationDataDatagridMain(ObservableCollection<Regie> listToPut)
        {
            if (listToPut == null)
            {
                this.listRegie = new ObservableCollection<Regie>(((App)App.Current).mySitaffEntities.Regie.OrderBy(reg => reg.Numero));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listRegie = new ObservableCollection<Regie>(listToPut);
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

        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {
            this.MenuItem_ColonneRegie = new MenuItem();
            this.MenuItem_ColonneRegie.IsChecked = false;
            this.MenuItem_ColonneRegie.Header = "Numéro Regie";
            this.MenuItem_ColonneRegie.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneRegie(); });
            this.AffMas_ColonneRegie();
            menuItem.Items.Add(this.MenuItem_ColonneRegie);

            this.MenuItem_ColonneAffaire = new MenuItem();
            this.MenuItem_ColonneAffaire.IsChecked = false;
            this.MenuItem_ColonneAffaire.Header = "Numéro d'Affaire";
            this.MenuItem_ColonneAffaire.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneAffaire(); });
            this.AffMas_ColonneAffaire();
            menuItem.Items.Add(this.MenuItem_ColonneAffaire);

            this.MenuItem_ColonneClient = new MenuItem();
            this.MenuItem_ColonneClient.IsChecked = false;
            this.MenuItem_ColonneClient.Header = "Client";
            this.MenuItem_ColonneClient.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneClient(); });
            this.AffMas_ColonneClient();
            menuItem.Items.Add(this.MenuItem_ColonneClient);

            this.MenuItem_ColonneEtat = new MenuItem();
            this.MenuItem_ColonneEtat.IsChecked = false;
            this.MenuItem_ColonneEtat.Header = "Etat Regie";
            this.MenuItem_ColonneEtat.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEtat(); });
            this.AffMas_ColonneEtat();
            menuItem.Items.Add(this.MenuItem_ColonneEtat);

            this.MenuItem_ColonneHeure = new MenuItem();
            this.MenuItem_ColonneHeure.IsChecked = false;
            this.MenuItem_ColonneHeure.Header = "Nombre d'heure";
            this.MenuItem_ColonneHeure.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneHeure(); });
            this.AffMas_ColonneHeure();
            menuItem.Items.Add(this.MenuItem_ColonneHeure);

            this.MenuItem_ColonneDateDebut = new MenuItem();
            this.MenuItem_ColonneDateDebut.IsChecked = false;
            this.MenuItem_ColonneDateDebut.Header = "Date début";
            this.MenuItem_ColonneDateDebut.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateDebut(); });
            this.AffMas_ColonneDateDebut();
            menuItem.Items.Add(this.MenuItem_ColonneDateDebut);

            this.MenuItem_ColonneDateFin = new MenuItem();
            this.MenuItem_ColonneDateFin.IsChecked = false;
            this.MenuItem_ColonneDateFin.Header = "Date fin";
            this.MenuItem_ColonneDateFin.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateFin(); });
            this.AffMas_ColonneDateFin();
            menuItem.Items.Add(this.MenuItem_ColonneDateFin);

            this.MenuItem_ColonnePrixTotal = new MenuItem();
            this.MenuItem_ColonnePrixTotal.IsChecked = false;
            this.MenuItem_ColonnePrixTotal.Header = "Prix total fourniture";
            this.MenuItem_ColonnePrixTotal.Click += new RoutedEventHandler(delegate { this.AffMas_ColonnePrixTotal(); });
            this.AffMas_ColonnePrixTotal();
            menuItem.Items.Add(this.MenuItem_ColonnePrixTotal);

            this.MenuItem_ColonnePrixTotalHeures = new MenuItem();
            this.MenuItem_ColonnePrixTotalHeures.IsChecked = false;
            this.MenuItem_ColonnePrixTotalHeures.Header = "Prix total heures";
            this.MenuItem_ColonnePrixTotalHeures.Click += new RoutedEventHandler(delegate { this.AffMas_ColonnePrixTotalHeures(); });
            this.AffMas_ColonnePrixTotalHeures();
            menuItem.Items.Add(this.MenuItem_ColonnePrixTotalHeures);

            this.MenuItem_ColonneVersionDevis = new MenuItem();
            this.MenuItem_ColonneVersionDevis.IsChecked = false;
            this.MenuItem_ColonneVersionDevis.Header = "Version devis";
            this.MenuItem_ColonneVersionDevis.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneVersionDevis(); });
            this.AffMas_ColonneVersionDevis();
            menuItem.Items.Add(this.MenuItem_ColonneVersionDevis);

            this.MenuItem_ColonneEtatRegie = new MenuItem();
            this.MenuItem_ColonneEtatRegie.IsChecked = false;
            this.MenuItem_ColonneEtatRegie.Header = "Etat régie";
            this.MenuItem_ColonneEtatRegie.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEtatRegie(); });
            this.AffMas_ColonneEtatRegie();
            menuItem.Items.Add(this.MenuItem_ColonneEtatRegie);

            this.MenuItem_ColonneTemps = new MenuItem();
            this.MenuItem_ColonneTemps.IsChecked = false;
            this.MenuItem_ColonneTemps.Header = "Heures associées";
            this.MenuItem_ColonneTemps.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTemps(); });
            this.AffMas_ColonneTemps();
            menuItem.Items.Add(this.MenuItem_ColonneTemps);

            this.MenuItem_ColonneUtilisateur = new MenuItem();
            this.MenuItem_ColonneUtilisateur.IsChecked = true;
            this.MenuItem_ColonneUtilisateur.Header = "Utilisateur";
            this.MenuItem_ColonneUtilisateur.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneUtilisateur(); });
            this.AffMas_ColonneUtilisateur();
            menuItem.Items.Add(this.MenuItem_ColonneUtilisateur);

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

        #region Afficher / Masquer

        #region Tout

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneRegie.IsChecked = false;
            this.MenuItem_ColonneAffaire.IsChecked = false;
            this.MenuItem_ColonneClient.IsChecked = false;
            this.MenuItem_ColonneEtat.IsChecked = false;
            this.MenuItem_ColonneHeure.IsChecked = false;
            this.MenuItem_ColonneDateDebut.IsChecked = false;
            this.MenuItem_ColonneDateFin.IsChecked = false;
            this.MenuItem_ColonnePrixTotal.IsChecked = false;
            this.MenuItem_ColonneVersionDevis.IsChecked = false;
            this.MenuItem_ColonneEtatRegie.IsChecked = false;
            this.MenuItem_ColonneTemps.IsChecked = false;
            this.MenuItem_ColonneUtilisateur.IsChecked = false;
            this.MenuItem_ColonnePrixTotalHeures.IsChecked = false;

            this.AffMas_ColonneRegie();
            this.AffMas_ColonneAffaire();
            this.AffMas_ColonneClient();
            this.AffMas_ColonneEtat();
            this.AffMas_ColonneHeure();
            this.AffMas_ColonneDateDebut();
            this.AffMas_ColonneDateFin();
            this.AffMas_ColonnePrixTotal();
            this.AffMas_ColonneVersionDevis();
            this.AffMas_ColonneEtatRegie();
            this.AffMas_ColonneTemps();
            this.AffMas_ColonneUtilisateur();
            this.AffMas_ColonnePrixTotalHeures();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneRegie.IsChecked = true;
            this.MenuItem_ColonneAffaire.IsChecked = true;
            this.MenuItem_ColonneClient.IsChecked = true;
            this.MenuItem_ColonneEtat.IsChecked = true;
            this.MenuItem_ColonneHeure.IsChecked = true;
            this.MenuItem_ColonneDateDebut.IsChecked = true;
            this.MenuItem_ColonneDateFin.IsChecked = true;
            this.MenuItem_ColonnePrixTotal.IsChecked = true;
            this.MenuItem_ColonneVersionDevis.IsChecked = true;
            this.MenuItem_ColonneEtatRegie.IsChecked = true;
            this.MenuItem_ColonneTemps.IsChecked = true;
            this.MenuItem_ColonneUtilisateur.IsChecked = true;
            this.MenuItem_ColonnePrixTotalHeures.IsChecked = true;

            this.AffMas_ColonneRegie();
            this.AffMas_ColonneAffaire();
            this.AffMas_ColonneClient();
            this.AffMas_ColonneEtat();
            this.AffMas_ColonneHeure();
            this.AffMas_ColonneDateDebut();
            this.AffMas_ColonneDateFin();
            this.AffMas_ColonnePrixTotal();
            this.AffMas_ColonneVersionDevis();
            this.AffMas_ColonneEtatRegie();
            this.AffMas_ColonneTemps();
            this.AffMas_ColonneUtilisateur();
            this.AffMas_ColonnePrixTotalHeures();
        }

        #endregion

        private void AffMas_ColonneRegie()
        {
            if (this.MenuItem_ColonneRegie.IsChecked == true)
            {
                this._ColonneRegie.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneRegie.IsChecked = false;
            }
            else
            {
                this._ColonneRegie.Visibility = Visibility.Visible;
                this.MenuItem_ColonneRegie.IsChecked = true;
            }
        }

        private void AffMas_ColonneAffaire()
        {
            if (this.MenuItem_ColonneAffaire.IsChecked == true)
            {
                this._ColonneAffaire.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneAffaire.IsChecked = false;
            }
            else
            {
                this._ColonneAffaire.Visibility = Visibility.Visible;
                this.MenuItem_ColonneAffaire.IsChecked = true;
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

        private void AffMas_ColonneEtat()
        {
            if (this.MenuItem_ColonneEtat.IsChecked == true)
            {
                this._ColonneEtat.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneEtat.IsChecked = false;
            }
            else
            {
                this._ColonneEtat.Visibility = Visibility.Visible;
                this.MenuItem_ColonneEtat.IsChecked = true;
            }
        }

        private void AffMas_ColonneHeure()
        {
            if (this.MenuItem_ColonneHeure.IsChecked == true)
            {
                this._ColonneHeure.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneHeure.IsChecked = false;
            }
            else
            {
                this._ColonneHeure.Visibility = Visibility.Visible;
                this.MenuItem_ColonneHeure.IsChecked = true;
            }
        }

        private void AffMas_ColonneDateDebut()
        {
            if (this.MenuItem_ColonneDateDebut.IsChecked == true)
            {
                this._ColonneDateDebut.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDateDebut.IsChecked = false;
            }
            else
            {
                this._ColonneDateDebut.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDateDebut.IsChecked = true;
            }
        }

        private void AffMas_ColonneDateFin()
        {
            if (this.MenuItem_ColonneDateFin.IsChecked == true)
            {
                this._ColonneDateFin.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDateFin.IsChecked = false;
            }
            else
            {
                this._ColonneDateFin.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDateFin.IsChecked = true;
            }
        }

        private void AffMas_ColonnePrixTotal()
        {
            if (this.MenuItem_ColonnePrixTotal.IsChecked == true)
            {
                this._ColonnePrixTotal.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonnePrixTotal.IsChecked = false;
            }
            else
            {
                this._ColonnePrixTotal.Visibility = Visibility.Visible;
                this.MenuItem_ColonnePrixTotal.IsChecked = true;
            }
        }

        private void AffMas_ColonnePrixTotalHeures()
        {
            if (this.MenuItem_ColonnePrixTotalHeures.IsChecked == true)
            {
                this._ColonnePrixTotalHeures.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonnePrixTotalHeures.IsChecked = false;
            }
            else
            {
                this._ColonnePrixTotalHeures.Visibility = Visibility.Visible;
                this.MenuItem_ColonnePrixTotalHeures.IsChecked = true;
            }
        }

        private void AffMas_ColonneVersionDevis()
        {
            if (this.MenuItem_ColonneVersionDevis.IsChecked == true)
            {
                this._ColonneVersionDevis.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneVersionDevis.IsChecked = false;
            }
            else
            {
                this._ColonneVersionDevis.Visibility = Visibility.Visible;
                this.MenuItem_ColonneVersionDevis.IsChecked = true;
            }
        }

        private void AffMas_ColonneEtatRegie()
        {
            if (this.MenuItem_ColonneEtatRegie.IsChecked == true)
            {
                this._ColonneEtatRegie.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneEtatRegie.IsChecked = false;
            }
            else
            {
                this._ColonneEtatRegie.Visibility = Visibility.Visible;
                this.MenuItem_ColonneEtatRegie.IsChecked = true;
            }
        }

        private void AffMas_ColonneTemps()
        {
            if (this.MenuItem_ColonneTemps.IsChecked == true)
            {
                this._ColonneTemps.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTemps.IsChecked = false;
            }
            else
            {
                this._ColonneTemps.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTemps.IsChecked = true;
            }
        }

        private void AffMas_ColonneUtilisateur()
        {
            if (this.MenuItem_ColonneUtilisateur.IsChecked == true)
            {
                this._ColonneUtilisateur.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneUtilisateur.IsChecked = false;
            }
            else
            {
                this._ColonneUtilisateur.Visibility = Visibility.Visible;
                this.MenuItem_ColonneUtilisateur.IsChecked = true;
            }
        }

        #endregion

        #endregion

        #endregion

        #region fenetre chargé
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
        /// Ajoute une nouvelle régie à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Regie Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une régie en cours ...");

            //Initialisation de la fenêtre
            RegieWindow regieWindow = new RegieWindow();

            //Création de l'objet temporaire
            Regie tmp = new Regie();
            tmp.Termine = false;
            tmp.Signe = false;

            //Mise de l'objet temporaire dans le datacontext
            regieWindow.DataContext = tmp;

            //booléen nullable vrai ou faux ou null
            regieWindow.creation = true;
            bool? dialogResult = regieWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                return (Regie)regieWindow.DataContext;
            }
            else
            {
                try
                {
                    //On détache la regie
                    ((App)App.Current).mySitaffEntities.Detach((Regie)regieWindow.DataContext);
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une régie annulé : " + this.listRegie.Count() + " / " + this.max);

                return null;
            }
        }


        /// <summary>
        /// Ouvre la régie séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Regie Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une régie en cours ...");

                    //Création de la fenêtre
                    RegieWindow regieWindow = new RegieWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    regieWindow.DataContext = new Regie();
                    regieWindow.DataContext = (Regie)this._DataGridMain.SelectedItem;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = regieWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                        return (Regie)regieWindow.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Regie)(this._DataGridMain.SelectedItem));
                        //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une régie annulée : " + this.listRegie.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule régie.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une régie.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime la régie séléctionnée avec une confirmation
        /// </summary>
        public Regie Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une régie en cours ...");

                    if (MessageBox.Show("Voulez-vous rééllement supprimer la régie séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //Supprimer l'élément 
                        return (Regie)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une régie annulée : " + this.listRegie.Count() + " / " + this.max);

                        return null;
                    }

                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule régie.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une régie.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre l'entreprise séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Regie Look()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une régie en cours ...");

                    //Création de la fenêtre
                    RegieWindow regieWindow = new RegieWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    regieWindow.DataContext = new Regie();
                    regieWindow.DataContext = (Regie)this._DataGridMain.SelectedItem;

                    //Je positionne la lecture seule sur la fenêtre
                    regieWindow.lectureSeule();
                    regieWindow.soloLecture = true;

                    //J'affiche la fenêtre
                    bool? dialogResult = regieWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une régie terminé : " + this.listRegie.Count() + " / " + this.max);

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule régie.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

        #endregion

        #region filtrage

        #region remise a zero

        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            _filterContainDateDebut.SelectedDate = null;
            _filterContainDateFin.SelectedDate = null;
            _filterContainNumeroRegie.Text = "";
            _filterContainNombreHeure.Text = "";
            _filterContainEtatRegie.SelectedItem = null;
            _filterContainClient.Text = "";
            _filterContainAffaire.Text = "";
            _filterContainEcartHeures.IsChecked = false;

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

            ObservableCollection<Regie> listToPut = new ObservableCollection<Regie>(((App)App.Current).mySitaffEntities.Regie.OrderBy(num => num.Numero));

            if (this._filterContainNumeroRegie.Text != "")
            {
                listToPut = new ObservableCollection<Regie>(listToPut.Where(reg => reg.Numero.Trim().ToLower().Contains(this._filterContainNumeroRegie.Text.Trim().ToLower())));
            }
            if (this._filterContainAffaire.Text != "")
            {
                listToPut = new ObservableCollection<Regie>(listToPut.Where(reg => reg.Affaire1 != null));
                listToPut = new ObservableCollection<Regie>(listToPut.Where(reg => reg.Affaire1.Numero.Trim().ToLower().Contains(this._filterContainAffaire.Text.Trim().ToLower())));
            }
            if (this._filterContainClient.Text != "")
            {
                listToPut = new ObservableCollection<Regie>(listToPut.Where(reg => reg.Client1 != null));
                listToPut = new ObservableCollection<Regie>(listToPut.Where(reg => reg.Client1.Entreprise != null));
                listToPut = new ObservableCollection<Regie>(listToPut.Where(reg => reg.Client1.Entreprise.Libelle.Trim().ToLower().Contains(this._filterContainClient.Text.Trim().ToLower())));
            }
            if (this._filterContainEtatRegie.SelectedItem != null)
            {
                listToPut = new ObservableCollection<Regie>(listToPut.Where(reg => reg.EtatRegieCommande == ((EtatDeRegie)this._filterContainEtatRegie.SelectedItem).chaine));
            }
            if (this._filterContainNombreHeure.Text != "")
            {
                double val;
                if (double.TryParse(this._filterContainNombreHeure.Text, out val))
                {
                    listToPut = new ObservableCollection<Regie>(listToPut.Where(reg => reg.Heures_Totales == double.Parse(this._filterContainNombreHeure.Text)));
                }
            }
            if (this._filterContainDateDebut.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Regie>(listToPut.Where(reg => reg.Date_Debut == this._filterContainDateDebut.SelectedDate));
            }
            if (this._filterContainDateFin.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Regie>(listToPut.Where(reg => reg.Date_Fin == this._filterContainDateFin.SelectedDate));
            }
            if (this._filterContainEcartHeures.IsChecked == true)
            {
                listToPut = new ObservableCollection<Regie>(listToPut.Where(reg => reg.HeureAssociees - reg.Heures_Totales != 0));
            }
            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);
            if (this.listRegie.Count() == 0)
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
                if (_filterContainEcartHeures.IsChecked == true || _filterContainNumeroRegie.Text != "" || _filterContainAffaire.Text != "" || _filterContainNombreHeure.Text != "" || _filterContainClient.Text != "" || _filterContainEtatRegie.SelectedItem != null || _filterContainDateDebut.SelectedDate != null || _filterContainDateFin.SelectedDate != null || this.max != this.listRegie.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainNumeroRegie.Focus();
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
            this.max = ((App)App.Current).mySitaffEntities.Regie.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Regie num)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des régies terminée : " + this.listRegie.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listRegie.Add(num);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une régie numéro '" + num.Numero + "' effectué avec succès. Nombre d'élements : " + this.listRegie.Count() + " / " + this.max);
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification de la régie numéro : '" + num.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listRegie.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listRegie.Remove(num);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression de la régie numéro : '" + num.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listRegie.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des régies terminé : " + this.listRegie.Count() + " / " + this.max);
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
            this.listRegie = new ObservableCollection<Regie>(this.listRegie.OrderBy(num => num.Numero));
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
