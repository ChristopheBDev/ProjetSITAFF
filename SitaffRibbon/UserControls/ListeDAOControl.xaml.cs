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
using System.ComponentModel;
using System.Windows.Threading;
using SitaffRibbon.Classes;

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeDAOControl.xaml
    /// </summary>
    public partial class ListeDAOControl : UserControl
    {

        #region Attributs

        long max = 0;

        MenuItem MenuItem_ColonnePC;
        MenuItem MenuItem_ColonneNumero;
        MenuItem MenuItem_ColonneAffaire;
        MenuItem MenuItem_ColonneDevis;
        MenuItem MenuItem_ColonneClient;
        MenuItem MenuItem_ColonneDemandeur;
        MenuItem MenuItem_ColonneLibelle;
        MenuItem MenuItem_ColonneDateCreation;
        MenuItem MenuItem_ColonneCreePar;
        MenuItem MenuItem_ColonneHeuresPassees;
        MenuItem MenuItem_ColonneCommentaire;

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Propriétés de dépendances

        public ObservableCollection<DAO> listDAO
        {
            get { return (ObservableCollection<DAO>)GetValue(listDAOProperty); }
            set { SetValue(listDAOProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listDAO.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listDAOProperty =
            DependencyProperty.Register("listDAO", typeof(ObservableCollection<DAO>), typeof(ListeDAOControl), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public ListeDAOControl()
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
            this.initialisationComboBoxPC();
            this._filterZone.Height = 21;
        }

        private void initialisationComboBoxPC()
        {
            ObservableCollection<PCDao> listType = new ObservableCollection<PCDao>();
            listType.Add(new PCDao("P"));
            listType.Add(new PCDao("C"));
            listType.Add(new PCDao("P et C - Normalement pas possible !"));
            listType.Add(new PCDao("ni P ni C - Erreur !"));
            this._filterContainPC.ItemsSource = listType;
        }

        private void initialisationAutoCompleteBox()
        {
            List<string> listEntreprise = new List<string>();
            List<string> listAffaire = new List<string>();
            List<string> listDevis = new List<string>();
            List<string> listDemandeur = new List<string>();
            List<string> listCreateur = new List<string>();
            foreach (DAO item in ((App)App.Current).mySitaffEntities.DAO)
            {
                //Pour remplir les clients
                if (item.Client1 != null)
                {
                    if (item.Client1.Entreprise != null)
                    {
                        if (!listEntreprise.Contains(item.Client1.Entreprise.Libelle))
                        {
                            listEntreprise.Add(item.Client1.Entreprise.Libelle);
                        }
                    }
                }

                //Pour remplir les affaires
                if (item.Affaire1 != null)
                {
                    if (!listAffaire.Contains(item.Affaire1.Numero))
                    {
                        listAffaire.Add(item.Affaire1.Numero);
                    }
                }

                //Pour remplir les Devis
                if (item.Devis1 != null)
                {
                    if (!listDevis.Contains(item.Devis1.Numero))
                    {
                        listDevis.Add(item.Devis1.Numero);
                    }
                }

                //Pour remplir les Demandeur
                if (item.Salarie != null)
                {
                    if (item.Salarie.Personne != null)
                    {
                        if (!listDemandeur.Contains(item.Salarie.Personne.fullname))
                        {
                            listDemandeur.Add(item.Salarie.Personne.fullname);
                        }
                    }
                }

                //Pour remplir les createurs
                if (item.Utilisateur != null)
                {
                    if (item.Utilisateur.Salarie_Interne1 != null)
                    {
                        if (item.Utilisateur.Salarie_Interne1.Salarie != null)
                        {
                            if (item.Utilisateur.Salarie_Interne1.Salarie.Personne != null)
                            {
                                if (!listCreateur.Contains(item.Utilisateur.Salarie_Interne1.Salarie.Personne.fullname))
                                {
                                    listCreateur.Add(item.Utilisateur.Salarie_Interne1.Salarie.Personne.fullname);
                                }
                            }
                        }
                    }
                }

            }
            _filterContainClient.ItemsSource = listEntreprise;
            _filterContainAffaire.ItemsSource = listAffaire;
            _filterContainNumeroDevis.ItemsSource = listDevis;
            _filterContainCree.ItemsSource = listCreateur;
            _filterContainDemandeur.ItemsSource = listDemandeur;
        }

        #endregion

        #region initialisation Donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<DAO> listToPut)
        {
            if (listToPut == null)
            {
                this.listDAO = new ObservableCollection<DAO>(((App)App.Current).mySitaffEntities.DAO.OrderByDescending(dao => dao.Annee).ThenByDescending(dao => dao.Mois).ThenByDescending(dao => dao.Increment));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listDAO = new ObservableCollection<DAO>(listToPut);
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
            itemAfficher5.Header = "Ajouter du temps";
            itemAfficher5.Click += new RoutedEventHandler(delegate { this.menuAddTime(); });

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
            if (securite.VerificationDroitActionsCRUD(this.ToString(), "Update"))
            {
                contextMenu.Items.Add(itemAfficher5);
                contextMenu.Items.Add(new Separator());
            }
            contextMenu.Items.Add(itemAfficher8);
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

        private void menuAddTime()
        {
            ((App)App.Current)._theMainWindow._CommandAddTime.Command.Execute(((App)App.Current)._theMainWindow);
        }

        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {
            this.MenuItem_ColonnePC = new MenuItem();
            this.MenuItem_ColonnePC.IsChecked = false;
            this.MenuItem_ColonnePC.Header = "P / C";
            this.MenuItem_ColonnePC.Click += new RoutedEventHandler(delegate { this.AffMas_ColonnePC(); });
            this.AffMas_ColonnePC();
            menuItem.Items.Add(this.MenuItem_ColonnePC);

            this.MenuItem_ColonneNumero = new MenuItem();
            this.MenuItem_ColonneNumero.IsChecked = false;
            this.MenuItem_ColonneNumero.Header = "Numéro de plan";
            this.MenuItem_ColonneNumero.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumero(); });
            this.AffMas_ColonneNumero();
            menuItem.Items.Add(this.MenuItem_ColonneNumero);

            this.MenuItem_ColonneAffaire = new MenuItem();
            this.MenuItem_ColonneAffaire.IsChecked = false;
            this.MenuItem_ColonneAffaire.Header = "Affaire";
            this.MenuItem_ColonneAffaire.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneAffaire(); });
            this.AffMas_ColonneAffaire();
            menuItem.Items.Add(this.MenuItem_ColonneAffaire);

            this.MenuItem_ColonneDevis = new MenuItem();
            this.MenuItem_ColonneDevis.IsChecked = false;
            this.MenuItem_ColonneDevis.Header = "Devis";
            this.MenuItem_ColonneDevis.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDevis(); });
            this.AffMas_ColonneDevis();
            menuItem.Items.Add(this.MenuItem_ColonneDevis);

            this.MenuItem_ColonneClient = new MenuItem();
            this.MenuItem_ColonneClient.IsChecked = false;
            this.MenuItem_ColonneClient.Header = "Client";
            this.MenuItem_ColonneClient.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneClient(); });
            this.AffMas_ColonneClient();
            menuItem.Items.Add(this.MenuItem_ColonneClient);

            this.MenuItem_ColonneDemandeur = new MenuItem();
            this.MenuItem_ColonneDemandeur.IsChecked = false;
            this.MenuItem_ColonneDemandeur.Header = "Demandeur";
            this.MenuItem_ColonneDemandeur.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDemandeur(); });
            this.AffMas_ColonneDemandeur();
            menuItem.Items.Add(this.MenuItem_ColonneDemandeur);

            this.MenuItem_ColonneLibelle = new MenuItem();
            this.MenuItem_ColonneLibelle.IsChecked = false;
            this.MenuItem_ColonneLibelle.Header = "Désignation du dessin";
            this.MenuItem_ColonneLibelle.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneLibelle(); });
            this.AffMas_ColonneLibelle();
            menuItem.Items.Add(this.MenuItem_ColonneLibelle);

            this.MenuItem_ColonneDateCreation = new MenuItem();
            this.MenuItem_ColonneDateCreation.IsChecked = false;
            this.MenuItem_ColonneDateCreation.Header = "Date de création";
            this.MenuItem_ColonneDateCreation.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateCreation(); });
            this.AffMas_ColonneDateCreation();
            menuItem.Items.Add(this.MenuItem_ColonneDateCreation);

            this.MenuItem_ColonneCreePar = new MenuItem();
            this.MenuItem_ColonneCreePar.IsChecked = false;
            this.MenuItem_ColonneCreePar.Header = "Crée par";
            this.MenuItem_ColonneCreePar.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneCreePar(); });
            this.AffMas_ColonneCreePar();
            menuItem.Items.Add(this.MenuItem_ColonneCreePar);

            this.MenuItem_ColonneHeuresPassees = new MenuItem();
            this.MenuItem_ColonneHeuresPassees.IsChecked = false;
            this.MenuItem_ColonneHeuresPassees.Header = "Heures passées";
            this.MenuItem_ColonneHeuresPassees.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneHeuresPassees(); });
            this.AffMas_ColonneHeuresPassees();
            menuItem.Items.Add(this.MenuItem_ColonneHeuresPassees);

            this.MenuItem_ColonneCommentaire = new MenuItem();
            this.MenuItem_ColonneCommentaire.IsChecked = false;
            this.MenuItem_ColonneCommentaire.Header = "Commentaire";
            this.MenuItem_ColonneCommentaire.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneCommentaire(); });
            this.AffMas_ColonneCommentaire();
            menuItem.Items.Add(this.MenuItem_ColonneCommentaire);

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
            this.MenuItem_ColonnePC.IsChecked = false;
            this.MenuItem_ColonneNumero.IsChecked = false;
            this.MenuItem_ColonneAffaire.IsChecked = false;
            this.MenuItem_ColonneDevis.IsChecked = false;
            this.MenuItem_ColonneClient.IsChecked = false;
            this.MenuItem_ColonneDemandeur.IsChecked = false;
            this.MenuItem_ColonneLibelle.IsChecked = false;
            this.MenuItem_ColonneDateCreation.IsChecked = false;
            this.MenuItem_ColonneCreePar.IsChecked = false;
            this.MenuItem_ColonneHeuresPassees.IsChecked = false;
            this.MenuItem_ColonneCommentaire.IsChecked = false;

            this.AffMas_ColonnePC();
            this.AffMas_ColonneNumero();
            this.AffMas_ColonneAffaire();
            this.AffMas_ColonneDevis();
            this.AffMas_ColonneClient();
            this.AffMas_ColonneDemandeur();
            this.AffMas_ColonneLibelle();
            this.AffMas_ColonneDateCreation();
            this.AffMas_ColonneCreePar();
            this.AffMas_ColonneHeuresPassees();
            this.AffMas_ColonneCommentaire();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonnePC.IsChecked = true;
            this.MenuItem_ColonneNumero.IsChecked = true;
            this.MenuItem_ColonneAffaire.IsChecked = true;
            this.MenuItem_ColonneDevis.IsChecked = true;
            this.MenuItem_ColonneClient.IsChecked = true;
            this.MenuItem_ColonneDemandeur.IsChecked = true;
            this.MenuItem_ColonneLibelle.IsChecked = true;
            this.MenuItem_ColonneDateCreation.IsChecked = true;
            this.MenuItem_ColonneCreePar.IsChecked = true;
            this.MenuItem_ColonneHeuresPassees.IsChecked = true;
            this.MenuItem_ColonneCommentaire.IsChecked = true;

            this.AffMas_ColonnePC();
            this.AffMas_ColonneNumero();
            this.AffMas_ColonneAffaire();
            this.AffMas_ColonneDevis();
            this.AffMas_ColonneClient();
            this.AffMas_ColonneDemandeur();
            this.AffMas_ColonneLibelle();
            this.AffMas_ColonneDateCreation();
            this.AffMas_ColonneCreePar();
            this.AffMas_ColonneHeuresPassees();
            this.AffMas_ColonneCommentaire();
        }

        #endregion

        private void AffMas_ColonnePC()
        {
            if (this.MenuItem_ColonnePC.IsChecked == true)
            {
                this._ColonnePC.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonnePC.IsChecked = false;
            }
            else
            {
                this._ColonnePC.Visibility = Visibility.Visible;
                this.MenuItem_ColonnePC.IsChecked = true;
            }
        }

        private void AffMas_ColonneNumero()
        {
            if (this.MenuItem_ColonneNumero.IsChecked == true)
            {
                this._ColonneNumero.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNumero.IsChecked = false;
            }
            else
            {
                this._ColonneNumero.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNumero.IsChecked = true;
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

        private void AffMas_ColonneDevis()
        {
            if (this.MenuItem_ColonneDevis.IsChecked == true)
            {
                this._ColonneDevis.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDevis.IsChecked = false;
            }
            else
            {
                this._ColonneDevis.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDevis.IsChecked = true;
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

        private void AffMas_ColonneDemandeur()
        {
            if (this.MenuItem_ColonneDemandeur.IsChecked == true)
            {
                this._ColonneDemandeur.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDemandeur.IsChecked = false;
            }
            else
            {
                this._ColonneDemandeur.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDemandeur.IsChecked = true;
            }
        }

        private void AffMas_ColonneLibelle()
        {
            if (this.MenuItem_ColonneLibelle.IsChecked == true)
            {
                this._ColonneLibelle.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneLibelle.IsChecked = false;
            }
            else
            {
                this._ColonneLibelle.Visibility = Visibility.Visible;
                this.MenuItem_ColonneLibelle.IsChecked = true;
            }
        }

        private void AffMas_ColonneDateCreation()
        {
            if (this.MenuItem_ColonneDateCreation.IsChecked == true)
            {
                this._ColonneDateCreation.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDateCreation.IsChecked = false;
            }
            else
            {
                this._ColonneDateCreation.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDateCreation.IsChecked = true;
            }
        }

        private void AffMas_ColonneCreePar()
        {
            if (this.MenuItem_ColonneCreePar.IsChecked == true)
            {
                this._ColonneCreePar.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneCreePar.IsChecked = false;
            }
            else
            {
                this._ColonneCreePar.Visibility = Visibility.Visible;
                this.MenuItem_ColonneCreePar.IsChecked = true;
            }
        }

        private void AffMas_ColonneHeuresPassees()
        {
            if (this.MenuItem_ColonneHeuresPassees.IsChecked == true)
            {
                this._ColonneHeuresPassees.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneHeuresPassees.IsChecked = false;
            }
            else
            {
                this._ColonneHeuresPassees.Visibility = Visibility.Visible;
                this.MenuItem_ColonneHeuresPassees.IsChecked = true;
            }
        }

        private void AffMas_ColonneCommentaire()
        {
            if (this.MenuItem_ColonneCommentaire.IsChecked == true)
            {
                this._ColonneCommentaire.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneCommentaire.IsChecked = false;
            }
            else
            {
                this._ColonneCommentaire.Visibility = Visibility.Visible;
                this.MenuItem_ColonneCommentaire.IsChecked = true;
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
        /// Ajoute une nouvelle DAO à l'aide d'une nouvelle fenêtre
        /// </summary>
        /// <returns>moi-même (un DAO)</returns>
        public DAO Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une DAO en cours ...");

            //Initialisation de la fenêtre
            DAOWindow daoWindow = new DAOWindow();

            //Création de l'objet temporaire
            DAO tmp = new DAO();
            tmp.Utilisateur = ((App)App.Current)._connectedUser;
            tmp.Date_Creation = DateTime.Today;

            //Mise de l'objet temporaire dans le datacontext
            daoWindow.DataContext = tmp;

            //mise en place du booléen création à vrai
            daoWindow.creation = true;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = daoWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                return (DAO)daoWindow.DataContext;
            }
            else
            {
                //Si j'appuie sur le bouton annuler, je détache mon objet de la base de données
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((DAO)daoWindow.DataContext);
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une DAO annulé.");

                //Je renvoi null car j'oublie la dao à ajouter
                return null;
            }
        }

        /// <summary>
        /// Ouvre la DAO séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        /// <returns>moi-même (un DAO)</returns>
        public DAO Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une DAO en cours ...");

                    //Création de la fenêtre
                    DAOWindow daoWindow = new DAOWindow();

                    //Initialisation du Datacontext en DAO et association à la DAO sélectionnée
                    daoWindow.DataContext = new DAO();
                    daoWindow.DataContext = this._DataGridMain.SelectedItem;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = daoWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                        return (DAO)daoWindow.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (DAO)(this._DataGridMain.SelectedItem));

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une DAO annulée.");

                        //Si j'appuie sur le bouton annuler, je renvoi null
                        return null;
                    }

                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule DAO.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une DAO.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime la DAO séléctionnée avec une confirmation
        /// </summary>
        /// <returns>la DAO à supprimer</returns>
        public DAO Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une DAO en cours ...");

                    //Vérification "voulez-vous vraiment supprimer ??????"
                    if (MessageBox.Show("Voulez-vous rééllement supprimer la DAO séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //renvoi l'élement à supprimer
                        return (DAO)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une DAO annulée.");

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule DAO.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une DAO.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre la DAO séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public DAO Look()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une DAO en cours ...");

                    //Création de la fenêtre
                    DAOWindow daoWindow = new DAOWindow();

                    //Initialisation du Datacontext en DAO et association à la DAO sélectionnée
                    daoWindow.DataContext = new DAO();
                    daoWindow.DataContext = this._DataGridMain.SelectedItem;

                    //Je positionne la lecture seule sur la fenêtre
                    daoWindow.lectureSeule();

                    //Je met un titre à la fenêtre
                    daoWindow.Title = "DAO : " + ((DAO)daoWindow.DataContext).getNumero;

                    //J'affiche la fenêtre
                    bool? dialogResult = daoWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une DAO terminé.");

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule DAO.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une DAO.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        #endregion

        #region Actions Supplémentaires

        /// <summary>
        /// Ajouter du temps au DAO
        /// </summary>
        /// <returns>moi-même (un dao)</returns>
        public DAO AddTime()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "Ajout de temps"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout de temps à un dessin en cours ...");

                    //Création de la fenêtre
                    AjoutHeuresDAO ajoutHeuresDAO = new AjoutHeuresDAO();

                    //Création d'un objet temporaire temp de type DAO
                    DAO temp = (DAO)this._DataGridMain.SelectedItem;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = ajoutHeuresDAO.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur Ok, je renvoi la DAO avec le temps modifié
                        temp.Heures_Passees = temp.Heures_Passees + ajoutHeuresDAO.timeToAdd;
                        return temp;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (DAO)(this._DataGridMain.SelectedItem));

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout de temps à un dessin annulé.");

                        //Si j'appuie sur le bouton annuler, je renvoi null
                        return null;
                    }


                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule DAO.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une DAO.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        #endregion

        #region filtrages

        #region Remise à Zéro

        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            //Remise à zéro de tous les champs de filtrage
            //Text
            _filterContainAffaire.Text = "";
            _filterContainCree.Text = "";
            _filterContainNumeroDevis.Text = "";
            _filterContainNumeroDAO.Text = "";
            _filterContainClient.Text = "";
            _filterContainDemandeur.Text = "";
            _filterContainDesignation.Text = "";
            _filterContainHeure.Text = "";
            //Dates
            _filterContainDateCreation.SelectedDate = null;
            //ComboBox
            _filterContainPC.SelectedItem = null;

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

            ObservableCollection<DAO> listToPut = new ObservableCollection<DAO>(((App)App.Current).mySitaffEntities.DAO.OrderByDescending(dao => dao.Annee).ThenByDescending(dao => dao.Mois).ThenByDescending(dao => dao.Increment));

            if (this._filterContainNumeroDAO.Text != "")
            {
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.getNumero.Contains(this._filterContainNumeroDAO.Text.Trim())));
            }

            if (this._filterContainDesignation.Text != "")
            {
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Libelle.ToLower().Trim().Contains(this._filterContainDesignation.Text.ToLower().Trim())));
            }

            if (this._filterContainAffaire.Text != "")
            {
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Affaire1 != null));
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Affaire1.Numero.ToLower().Trim().Contains(this._filterContainAffaire.Text.ToLower().Trim())));
            }

            if (this._filterContainDateCreation.SelectedDate != null)
            {
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Date_Creation == this._filterContainDateCreation.SelectedDate));
            }

            if (this._filterContainNumeroDevis.Text != "")
            {
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Devis1 != null));
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Devis1.Numero.ToLower().Trim().Contains(this._filterContainNumeroDevis.Text.ToLower().Trim())));
            }

            if (this._filterContainCree.Text != "")
            {
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Utilisateur != null));
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Utilisateur.Salarie_Interne1 != null));
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Utilisateur.Salarie_Interne1.Salarie != null));
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Utilisateur.Salarie_Interne1.Salarie.Personne != null));
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Utilisateur.Salarie_Interne1.Salarie.Personne.fullname.ToLower().Trim().Contains(this._filterContainCree.Text.ToLower().Trim())));
            }

            if (this._filterContainClient.Text != "")
            {
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Client1 != null));
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Client1.Entreprise != null));
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Client1.Entreprise.Libelle.ToLower().Trim().Contains(this._filterContainClient.Text.ToLower().Trim())));
            }

            if (this._filterContainHeure.Text != "")
            {
                double val;
                if (double.TryParse(this._filterContainHeure.Text.Trim(), out val))
                {
                    listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Heures_Passees == double.Parse(this._filterContainHeure.Text.Trim())));
                }
            }

            if (this._filterContainDemandeur.Text != "")
            {
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Salarie != null));
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Salarie.Personne != null));
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.Salarie.Personne.fullname.ToLower().Trim().Contains(this._filterContainDemandeur.Text.ToLower().Trim()) || dao.Salarie.Personne.Initiales.Trim().ToLower().Contains(this._filterContainDemandeur.Text.Trim().ToLower())));
            }

            if (this._filterContainPC.SelectedItem != null)
            {
                listToPut = new ObservableCollection<DAO>(listToPut.Where(dao => dao.getType == ((PCDao)this._filterContainPC.SelectedItem).chaine));
            }

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            //Si aucun résultat, j'affiche un message
            if (this.listDAO.Count() == 0)
            {
                MessageBox.Show("Aucun résultat ne correspont à votre recherche.", "Aucun résultat", MessageBoxButton.OK);
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
                if (_filterContainAffaire.Text != "" || _filterContainCree.Text != "" || _filterContainNumeroDevis.Text != "" || _filterContainNumeroDAO.Text != "" || _filterContainClient.Text != "" || _filterContainDemandeur.Text != "" || _filterContainDesignation.Text != "" || _filterContainHeure.Text != "" || _filterContainDateCreation.SelectedDate != null || _filterContainPC.SelectedItem != null || this.max != this.listDAO.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainNumeroDAO.Focus();
            }
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

        #endregion

        #endregion

        #region Fonctions

        /// <summary>
        /// Recalcul le nombre d'élements maximum
        /// </summary>
        private void recalculMax()
        {
            this.max = ((App)App.Current).mySitaffEntities.DAO.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet DAO soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, DAO dao)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des DAO terminée : " + this.listDAO.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la dao dans le linsting
                this.listDAO.Add(dao);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une DAO numéro '" + dao.getNumero + "' effectué avec succès. Nombre d'élements : " + this.listDAO.Count() + " / " + this.max);
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification de la DAO numéro : '" + dao.getNumero + "' effectuée avec succès. Nombre d'élements : " + this.listDAO.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listDAO.Remove(dao);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression de la DAO numéro : '" + dao.getNumero + "' effectuée avec succès. Nombre d'élements : " + this.listDAO.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else if (typeEtat == "AddTime")
            {
                this._DataGridMain.Items.Refresh();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout de temps au dessin numéro : '" + dao.getNumero + "' effectué avec succès. Nombre d'élements : " + this.listDAO.Count() + " / " + this.max);
            }
            else
            {
((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des DAO terminé : " + this.listDAO.Count() + " / " + this.max);
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
            this.listDAO = new ObservableCollection<DAO>(this.listDAO.OrderByDescending(dao => dao.Annee).ThenByDescending(dao => dao.Mois).ThenByDescending(dao => dao.Increment));
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
