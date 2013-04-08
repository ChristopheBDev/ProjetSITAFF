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
    /// Logique d'interaction pour ListeCongesControl.xaml
    /// </summary>
    public partial class ListeCongesControl : UserControl
    {

        #region Variables

        long max = 0;

        MenuItem MenuItem_ColonneSalarie;
        MenuItem MenuItem_ColonneMotifDemande;
        MenuItem MenuItem_ColonneNbJour;
        MenuItem MenuItem_ColonneDateDebut;
        MenuItem MenuItem_ColonneDateFin;
        MenuItem MenuItem_ColonneEtat;
        MenuItem MenuItem_ColonneDateDemande;
        MenuItem MenuItem_ColonneDateReponse;
        MenuItem MenuItem_ColonneMotifRefus;
        MenuItem MenuItem_ColonneCommentaire;


        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Propriétés de dépendances

        public ObservableCollection<Motif_Demande> listMotif
        {
            get { return (ObservableCollection<Motif_Demande>)GetValue(listMotifProperty); }
            set { SetValue(listMotifProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listConge.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listMotifProperty =
            DependencyProperty.Register("listMotif", typeof(ObservableCollection<Motif_Demande>), typeof(ListeCongesControl), new UIPropertyMetadata(null));

        public ObservableCollection<Conge> listConge
        {
            get { return (ObservableCollection<Conge>)GetValue(listCongeProperty); }
            set { SetValue(listCongeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listConge.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listCongeProperty =
            DependencyProperty.Register("listConge", typeof(ObservableCollection<Conge>), typeof(ListeCongesControl), new UIPropertyMetadata(null));

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

        #region Constructeur

        public ListeCongesControl()
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

        #region initialisation zone de filtrage

        private void initialisationFilterZone()
        {
            this.initialisationComboBox();
            this.initialisationAutoCompleteBox();
            this._filterZone.Height = 21;
        }

        private void initialisationAutoCompleteBox()
        {
            List<string> listSalarie = new List<string>();
            List<string> listEntrepriseMere = new List<string>();

            foreach (Conge item in ((App)App.Current).mySitaffEntities.Conge)
            {
                if (!listSalarie.Contains(item.Salarie1.Personne.fullname))
                {
                    listSalarie.Add(item.Salarie1.Personne.fullname);
                }
            }

            foreach (Entreprise_Mere item in ((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(em => em.Nom))
            {
                listEntrepriseMere.Add(item.Nom);
            }

            _filterContainSalarie.ItemsSource = listSalarie;
            _filterContainEntrepriseMere.ItemsSource = listEntrepriseMere;

        }

        private void initialisationComboBox()
        {
            ObservableCollection<EtatDeConge> listEtatConge = new ObservableCollection<EtatDeConge>();
            listEtatConge.Add(new EtatDeConge("En cours"));
            listEtatConge.Add(new EtatDeConge("Validé"));
            listEtatConge.Add(new EtatDeConge("Refusé"));
            this._filterContainEtatConge.ItemsSource = listEtatConge;
        }

        #endregion

        #region initialisation donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<Conge> listToPut)
        {
            if (listToPut == null)
            {
                this.listConge = new ObservableCollection<Conge>(((App)App.Current).mySitaffEntities.Conge.OrderByDescending(dat => dat.Date_Debut));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listConge = new ObservableCollection<Conge>(listToPut);
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

            MenuItem itemAfficher5 = new MenuItem();
            itemAfficher5.Header = "Répondre à la demande";
            itemAfficher5.Click += new RoutedEventHandler(delegate { this.RepondreAuConge(); });

            MenuItem itemAfficher6 = new MenuItem();
            itemAfficher6.Header = "Imprimer";
            itemAfficher6.Click += new RoutedEventHandler(delegate { this.ImprimerConge(); });

            MenuItem itemAfficher7 = RemplirMenuAfficherMasquerColonnes(new MenuItem());
            itemAfficher7.Header = "Afficher / Masquer";

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

            contextMenu.Items.Add(itemAfficher6);

            contextMenu.Items.Add(new Separator());

            contextMenu.Items.Add(itemAfficher7);
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

        private void RepondreAuConge()
        {
            ((App)App.Current)._theMainWindow._CommandRepondreConge.Command.Execute(((App)App.Current)._theMainWindow);
        }

        private void ImprimerConge()
        {
            ((App)App.Current)._theMainWindow._CommandRapportImprimerConge.Command.Execute(((App)App.Current)._theMainWindow);
        }

        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {
            this.MenuItem_ColonneSalarie = new MenuItem();
            this.MenuItem_ColonneSalarie.IsChecked = false;
            this.MenuItem_ColonneSalarie.Header = "Salarié";
            this.MenuItem_ColonneSalarie.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneSalarie(); });
            this.AffMas_ColonneSalarie();
            menuItem.Items.Add(this.MenuItem_ColonneSalarie);

            this.MenuItem_ColonneEtat = new MenuItem();
            this.MenuItem_ColonneEtat.IsChecked = false;
            this.MenuItem_ColonneEtat.Header = "Etat";
            this.MenuItem_ColonneEtat.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEtat(); });
            this.AffMas_ColonneEtat();
            menuItem.Items.Add(this.MenuItem_ColonneEtat);

            this.MenuItem_ColonneMotifDemande = new MenuItem();
            this.MenuItem_ColonneMotifDemande.IsChecked = false;
            this.MenuItem_ColonneMotifDemande.Header = "Motif demande";
            this.MenuItem_ColonneMotifDemande.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneMotifDemande(); });
            this.AffMas_ColonneMotifDemande();
            menuItem.Items.Add(this.MenuItem_ColonneMotifDemande);

            this.MenuItem_ColonneNbJour = new MenuItem();
            this.MenuItem_ColonneNbJour.IsChecked = false;
            this.MenuItem_ColonneNbJour.Header = "Nombre de jour";
            this.MenuItem_ColonneNbJour.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNbJour(); });
            this.AffMas_ColonneNbJour();
            menuItem.Items.Add(this.MenuItem_ColonneNbJour);

            this.MenuItem_ColonneDateDebut = new MenuItem();
            this.MenuItem_ColonneDateDebut.IsChecked = false;
            this.MenuItem_ColonneDateDebut.Header = "Date de début";
            this.MenuItem_ColonneDateDebut.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateDebut(); });
            this.AffMas_ColonneDateDebut();
            menuItem.Items.Add(this.MenuItem_ColonneDateDebut);

            this.MenuItem_ColonneDateFin = new MenuItem();
            this.MenuItem_ColonneDateFin.IsChecked = false;
            this.MenuItem_ColonneDateFin.Header = "Date de fin";
            this.MenuItem_ColonneDateFin.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateFin(); });
            this.AffMas_ColonneDateFin();
            menuItem.Items.Add(this.MenuItem_ColonneDateFin);

            this.MenuItem_ColonneDateDemande = new MenuItem();
            this.MenuItem_ColonneDateDemande.IsChecked = true;
            this.MenuItem_ColonneDateDemande.Header = "Date de demande";
            this.MenuItem_ColonneDateDemande.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateDemande(); });
            this.AffMas_ColonneDateDemande();
            menuItem.Items.Add(this.MenuItem_ColonneDateDemande);

            this.MenuItem_ColonneDateReponse = new MenuItem();
            this.MenuItem_ColonneDateReponse.IsChecked = true;
            this.MenuItem_ColonneDateReponse.Header = "Date de réponse";
            this.MenuItem_ColonneDateReponse.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateReponse(); });
            this.AffMas_ColonneDateReponse();
            menuItem.Items.Add(this.MenuItem_ColonneDateReponse);

            this.MenuItem_ColonneMotifRefus = new MenuItem();
            this.MenuItem_ColonneMotifRefus.IsChecked = true;
            this.MenuItem_ColonneMotifRefus.Header = "Motif de refus";
            this.MenuItem_ColonneMotifRefus.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneMotifRefus(); });
            this.AffMas_ColonneMotifRefus();
            menuItem.Items.Add(this.MenuItem_ColonneMotifRefus);

            this.MenuItem_ColonneCommentaire = new MenuItem();
            this.MenuItem_ColonneCommentaire.IsChecked = true;
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
            this.MenuItem_ColonneSalarie.IsChecked = false;
            this.MenuItem_ColonneMotifDemande.IsChecked = false;
            this.MenuItem_ColonneNbJour.IsChecked = false;
            this.MenuItem_ColonneDateDebut.IsChecked = false;
            this.MenuItem_ColonneDateFin.IsChecked = false;
            this.MenuItem_ColonneEtat.IsChecked = false;
            this.MenuItem_ColonneDateDemande.IsChecked = false;
            this.MenuItem_ColonneDateReponse.IsChecked = false;
            this.MenuItem_ColonneMotifRefus.IsChecked = false;
            this.MenuItem_ColonneCommentaire.IsChecked = false;

            this.AffMas_ColonneSalarie();
            this.AffMas_ColonneMotifDemande();
            this.AffMas_ColonneNbJour();
            this.AffMas_ColonneDateDebut();
            this.AffMas_ColonneDateFin();
            this.AffMas_ColonneEtat();
            this.AffMas_ColonneDateDemande();
            this.AffMas_ColonneDateReponse();
            this.AffMas_ColonneMotifRefus();
            this.AffMas_ColonneCommentaire();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneSalarie.IsChecked = true;
            this.MenuItem_ColonneMotifDemande.IsChecked = true;
            this.MenuItem_ColonneNbJour.IsChecked = true;
            this.MenuItem_ColonneDateDebut.IsChecked = true;
            this.MenuItem_ColonneDateFin.IsChecked = true;
            this.MenuItem_ColonneEtat.IsChecked = true;
            this.MenuItem_ColonneDateDemande.IsChecked = true;
            this.MenuItem_ColonneDateReponse.IsChecked = true;
            this.MenuItem_ColonneMotifRefus.IsChecked = true;
            this.MenuItem_ColonneCommentaire.IsChecked = true;

            this.AffMas_ColonneSalarie();
            this.AffMas_ColonneMotifDemande();
            this.AffMas_ColonneNbJour();
            this.AffMas_ColonneDateDebut();
            this.AffMas_ColonneDateFin();
            this.AffMas_ColonneEtat();
            this.AffMas_ColonneDateDemande();
            this.AffMas_ColonneDateReponse();
            this.AffMas_ColonneMotifRefus();
            this.AffMas_ColonneCommentaire();
        }

        #endregion

        private void AffMas_ColonneSalarie()
        {
            if (this.MenuItem_ColonneSalarie.IsChecked == true)
            {
                this._ColonneSalarie.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneSalarie.IsChecked = false;
            }
            else
            {
                this._ColonneSalarie.Visibility = Visibility.Visible;
                this.MenuItem_ColonneSalarie.IsChecked = true;
            }
        }

        private void AffMas_ColonneMotifDemande()
        {
            if (this.MenuItem_ColonneMotifDemande.IsChecked == true)
            {
                this._ColonneMotifDemande.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneMotifDemande.IsChecked = false;
            }
            else
            {
                this._ColonneMotifDemande.Visibility = Visibility.Visible;
                this.MenuItem_ColonneMotifDemande.IsChecked = true;
            }
        }

        private void AffMas_ColonneNbJour()
        {
            if (this.MenuItem_ColonneNbJour.IsChecked == true)
            {
                this._ColonneNbJour.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNbJour.IsChecked = false;
            }
            else
            {
                this._ColonneNbJour.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNbJour.IsChecked = true;
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

        private void AffMas_ColonneDateDemande()
        {
            if (this.MenuItem_ColonneDateDemande.IsChecked == true)
            {
                this._ColonneDateDemande.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDateDemande.IsChecked = false;
            }
            else
            {
                this._ColonneDateDemande.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDateDemande.IsChecked = true;
            }
        }

        private void AffMas_ColonneDateReponse()
        {
            if (this.MenuItem_ColonneDateReponse.IsChecked == true)
            {
                this._ColonneDateReponse.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDateReponse.IsChecked = false;
            }
            else
            {
                this._ColonneDateReponse.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDateReponse.IsChecked = true;
            }
        }

        private void AffMas_ColonneMotifRefus()
        {
            if (this.MenuItem_ColonneMotifRefus.IsChecked == true)
            {
                this._ColonneMotifRefus.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneMotifRefus.IsChecked = false;
            }
            else
            {
                this._ColonneMotifRefus.Visibility = Visibility.Visible;
                this.MenuItem_ColonneMotifRefus.IsChecked = true;
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

        #region CRUD (Create Read Update Delete)

        /// <summary>
        /// Ajoute un nouveau congé à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Conge Add(bool verrouillerSalarie)
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une demande de congé en cours ...");

            //Initialisation de la fenêtre
            CongeWindow congeWindow = new CongeWindow(verrouillerSalarie);

            //Création de l'objet temporaire
            Conge tmp = new Conge();
            tmp.Demande_Fait_Le = DateTime.Today;
            tmp.Utilisateur = ((App)App.Current)._connectedUser;

            //Mise de l'objet temporaire dans le datacontext
            congeWindow.DataContext = tmp;

            //Mise en place d'options spéciales sur la fenêtre
            congeWindow.creation = true;
            congeWindow.demande = true;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = congeWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet commande se trouvant dans le datacontext de la fenêtre
                return (Conge)congeWindow.DataContext;
            }
            else
            {
                try
                {
                    //On détache le congé
                    ((App)App.Current).mySitaffEntities.Detach((Conge)congeWindow.DataContext);
                }
                catch (Exception) { }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une demande de congé annulé : " + this.listConge.Count() + " / " + this.max);

                return null;
            }
        }

        /// <summary>
        /// Ouvre le congé séléctionné à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Conge Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    if (((Conge)this._DataGridMain.SelectedItem).Salarie1 == ((App)App.Current)._connectedUser.Salarie_Interne1.Salarie || ((Conge)this._DataGridMain.SelectedItem).Utilisateur == ((App)App.Current)._connectedUser)
                    {
                        if (((Conge)this._DataGridMain.SelectedItem).EtatConge == "En cours")
                        {
                            //Affichage du message "modification en cours"
                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une demande de congé en cours ...");

                            //Création de la fenêtre
                            CongeWindow congeWindow = new CongeWindow();

                            //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                            congeWindow.DataContext = new Conge();
                            congeWindow.DataContext = this._DataGridMain.SelectedItem;

                            //Mise en place des options spéciales de la fenêtere
                            congeWindow.creation = true;
                            congeWindow.demande = true;

                            //booléen nullable vrai ou faux ou null
                            bool? dialogResult = congeWindow.ShowDialog();

                            if (dialogResult.HasValue && dialogResult.Value == true)
                            {
                                //Si j'appuie sur le bouton Ok, je renvoi l'objet commande se trouvant dans le datacontext de la fenêtre
                                return (Conge)congeWindow.DataContext;
                            }
                            else
                            {
                                //Je récupère les anciennes données de la base sur les modifications effectuées
                                ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Conge)(this._DataGridMain.SelectedItem));
                                //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                                ((App)App.Current).refreshEDMXSansVidage();
                                this.filtrage();

                                //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une demande de congé annulée : " + this.listConge.Count() + " / " + this.max);

                                return null;
                            }
                        }
                        else
                        {
                            if (((Conge)this._DataGridMain.SelectedItem).EtatConge == "Validé")
                            {
                                MessageBox.Show("La demande de congé a été validée, vous ne pouvez donc pas la modifier.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                            }
                            if (((Conge)this._DataGridMain.SelectedItem).EtatConge == "Refusé")
                            {
                                MessageBox.Show("La demande de congé a été refusée, vous ne pouvez donc pas la modifier.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                            return null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vous n'êtes pas la personne qui a fait la demande, vous ne pouvez donc pas la modifier.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul congé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un congé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre le congé séléctionné pour répondre à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Conge Repondre()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Création de la fenêtre
                    CongeWindow congeWindow = new CongeWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    congeWindow.DataContext = new Conge();
                    congeWindow.DataContext = this._DataGridMain.SelectedItem;

                    //Vérification que l'utilisateur peut répondre
                    bool CanReponse = false;
                    foreach (Salarie_Repondeur sr in ((Conge)congeWindow.DataContext).Salarie1.Salarie_Repondeur)
                    {
                        if (sr.Salarie2 == ((App)App.Current)._connectedUser.Salarie_Interne1.Salarie)
                        {
                            CanReponse = true;
                        }
                    }
                    if (CanReponse)
                    {
                        //Affichage du message "modification en cours"
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Réponse à demande de congé en cours ...");

                        //Mise en place des options particulières de la fenêtre
                        congeWindow.demande = false;
                        congeWindow.creation = true;

                        bool? dialogResult = congeWindow.ShowDialog();
                        if (dialogResult.HasValue && dialogResult.Value == true)
                        {
                            ((Conge)congeWindow.DataContext).Reponse_Fait_Le = DateTime.Today;
                            ((Conge)congeWindow.DataContext).Utilisateur = ((App)App.Current)._connectedUser;
                            //Si j'appuie sur le bouton Ok, je renvoi l'objet commande se trouvant dans le datacontext de la fenêtre
                            return (Conge)congeWindow.DataContext;
                        }
                        else
                        {
                            //Je récupère les anciennes données de la base sur les modifications effectuées
                            ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Conge)(this._DataGridMain.SelectedItem));
                            //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                            ((App)App.Current).refreshEDMXSansVidage();
                            this.filtrage();

                            //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Réponse à une demande de congé annulée : " + this.listConge.Count() + " / " + this.max);

                            return null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vous ne pouvez pas répondre à ce congé car vous ne faites pas parti des personnes qui peuvent répondre aux demandes de ce salarié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return null;
                    }


                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul congé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un congé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime le congé séléctionné avec une confirmation
        /// </summary>
        public Conge Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    if (((Conge)this._DataGridMain.SelectedItem).Salarie1 == ((App)App.Current)._connectedUser.Salarie_Interne1.Salarie)
                    {
                        if (((Conge)this._DataGridMain.SelectedItem).EtatConge == "En cours")
                        {
                            //Affichage du message "suppression en cours"
                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un congé en cours ...");

                            if (MessageBox.Show("Voulez-vous rééllement supprimer le congé séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                return (Conge)this._DataGridMain.SelectedItem;

                            }
                            else
                            {
                                //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un congé annulé : " + this.listConge.Count() + " / " + this.max);

                                return null;
                            }
                        }
                        else
                        {
                            if (((Conge)this._DataGridMain.SelectedItem).EtatConge == "Validé")
                            {
                                MessageBox.Show("La demande de congé a été validée, vous ne pouvez donc pas la supprimer.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                            }
                            if (((Conge)this._DataGridMain.SelectedItem).EtatConge == "Refusé")
                            {
                                MessageBox.Show("La demande de congé a été refusée, vous ne pouvez donc pas la supprimer.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                            return null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vous n'êtes pas la personne qui a fait la demande, vous ne pouvez donc pas la supprimer.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul congé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un congé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre le congé séléctionné en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Conge Look(Conge conge)
        {
            if (this._DataGridMain.SelectedItem != null || conge != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1 || conge != null)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un congé en cours ...");

                    //Création de la fenêtre
                    CongeWindow congeWindow = new CongeWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    congeWindow.DataContext = new Conge();
                    if (conge != null)
                    {
                        congeWindow.DataContext = conge;
                    }
                    else
                    {
                        congeWindow.DataContext = (Conge)this._DataGridMain.SelectedItem;
                    }

                    //Je positionne la lecture seule sur la fenêtre
                    congeWindow.lectureSeule();
                    congeWindow.soloLecture = true;

                    //J'affiche la fenêtre
                    bool? dialogResult = congeWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un congé terminé : " + this.listConge.Count() + " / " + this.max);

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul congé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un congé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre le rapport congé séléctionné
        /// </summary>
        public Conge RapportImprimer()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    ReportingWindow reportingWindow = new ReportingWindow();
                    long toShow = ((Conge)this._DataGridMain.SelectedItem).Identifiant;
                    reportingWindow._webBrowser.Source = new Uri("http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fCONGES&rs:Command=Render&ReportParameter1=" + toShow);
                    reportingWindow.Title = "Rapport pour impression : congé de - " + ((Conge)this._DataGridMain.SelectedItem).Salarie1.Personne.fullname + "-";

                    reportingWindow.Show();
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul congé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un congé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }
        #endregion

        #region Actions supplémentaires

        #endregion

        #region filtrages

        #region remise a zero

        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            _filterContainDateDebut.SelectedDate = null;
            _filterContainDateFin.SelectedDate = null;
            _filterContainDateAPartir.SelectedDate = null;
            _filterContainDateJusquau.SelectedDate = null;


            _filterContainNombreJour.Text = "";
            _filterContainSalarie.Text = "";


            _filterContainMotif.SelectedItem = null;
            _filterContainEtatConge.SelectedItem = null;

            this.initialisationDataDatagridMain(null);
        }

        #endregion

        #region bouton filtrage

        //Voir quelle bouton sont rempli ou non
        private void filtrage()
        {
            ((App)App.Current)._theMainWindow._mutex.WaitOne();
            ((App)App.Current)._theMainWindow.startThread();
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Filtrage en cours ...");

            ObservableCollection<Conge> listToPut = new ObservableCollection<Conge>(((App)App.Current).mySitaffEntities.Conge.OrderByDescending(dat => dat.Date_Debut));


            if (_filterContainSalarie.Text != "" && listToPut.Count != 0)
            {
                listToPut = new ObservableCollection<Conge>(listToPut.Where(con => con.Salarie1 != null));
                listToPut = new ObservableCollection<Conge>(listToPut.Where(ent => ent.Salarie1.Personne.fullname.Trim().ToLower().Contains(this._filterContainSalarie.Text.Trim().ToLower())));
            }
            if (_filterContainMotif.SelectedItem != null && listToPut.Count != 0)
            {
                listToPut = new ObservableCollection<Conge>(listToPut.Where(ent => ent.Motif_Demande1 != null));
                listToPut = new ObservableCollection<Conge>(listToPut.Where(ent => ent.Motif_Demande1.Identifiant == ((Motif_Demande)_filterContainMotif.SelectedItem).Identifiant));
            }
            if (_filterContainEtatConge.SelectedItem != null && listToPut.Count != 0)
            {
                listToPut = new ObservableCollection<Conge>(listToPut.Where(ent => ent.EtatConge.Contains(((EtatDeConge)_filterContainEtatConge.SelectedItem).etat)));
            }
            if (_filterContainNombreJour.Text != "" && listToPut.Count != 0)
            {
                double val;
                if (double.TryParse(_filterContainNombreJour.Text, out val))
                {
                    listToPut = new ObservableCollection<Conge>(listToPut.Where(ent => ent.Nombre_Jours == double.Parse(_filterContainNombreJour.Text)));
                }
            }
            if (_filterContainDateDebut.SelectedDate != null && listToPut.Count != 0)
            {
                listToPut = new ObservableCollection<Conge>(listToPut.Where(ent => ent.Date_Debut == _filterContainDateDebut.SelectedDate));
            }
            if (_filterContainDateFin.SelectedDate != null && listToPut.Count != 0)
            {
                listToPut = new ObservableCollection<Conge>(listToPut.Where(ent => ent.Date_Debut == _filterContainDateFin.SelectedDate));
            }
            if (_filterContainEntrepriseMere.Text != "" && listToPut.Count != 0)
            {
                listToPut = new ObservableCollection<Conge>(listToPut.Where(ent => ent.Salarie1 != null));
                listToPut = new ObservableCollection<Conge>(listToPut.Where(ent => ent.Salarie1.Salarie_Interne != null));
                listToPut = new ObservableCollection<Conge>(listToPut.Where(ent => ent.Salarie1.Salarie_Interne.Entreprise_Mere1 != null));
                listToPut = new ObservableCollection<Conge>(listToPut.Where(ent => ent.Salarie1.Salarie_Interne.Entreprise_Mere1.Nom.ToLower().Trim().Contains(_filterContainEntrepriseMere.Text.ToLower().Trim())));
            }
            if (this._filterContainDateAPartir.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Conge>(listToPut.Where(com => com.Date_Debut != null && com.Date_Fin != null));
                listToPut = new ObservableCollection<Conge>(listToPut.Where(com => (this._filterContainDateAPartir.SelectedDate.Value.Date <= com.Date_Debut.Value.Date) || (this._filterContainDateAPartir.SelectedDate.Value.Date <= com.Date_Fin.Value.Date)));
            }
            if (this._filterContainDateJusquau.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Conge>(listToPut.Where(com => com.Date_Debut != null && com.Date_Fin != null));
                listToPut = new ObservableCollection<Conge>(listToPut.Where(com => (this._filterContainDateJusquau.SelectedDate.Value.Date >= com.Date_Debut.Value.Date) || (this._filterContainDateJusquau.SelectedDate.Value.Date >= com.Date_Fin.Value.Date)));
            }

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            if (this.listConge.Count() == 0)
            {
                MessageBox.Show("Aucun résultat ne correspont à votre recherche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Evenement Click Bouton, qui permet d'appliquer les filtres
        private void _buttonFiltrer_Click(object sender, RoutedEventArgs e)
        {
            this.filtrage();
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
                if (_filterContainDateAPartir.SelectedDate != null || _filterContainDateJusquau.SelectedDate != null || _filterContainSalarie.Text != "" || _filterContainMotif.SelectedItem != null || _filterContainEtatConge.SelectedItem != null || _filterContainNombreJour.Text != "" || _filterContainDateDebut.SelectedDate != null || _filterContainDateFin.SelectedDate != null || this.max != this.listConge.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainSalarie.Focus();
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
            this.max = ((App)App.Current).mySitaffEntities.Conge.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Conge con)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
((App)App.Current)._theMainWindow.changementTexteStatusBar("Filtrage des congés terminé : " + this.listConge.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute le congé dans le linsting
                this.listConge.Add(con);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un congé effectué avec succès. Nombre d'élements : " + this.listConge.Count() + " / " + this.max);
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification du congé effectué avec succès. Nombre d'élements : " + this.listConge.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listConge.Remove(con);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression du congé effectué avec succès. Nombre d'élements : " + this.listConge.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else if (typeEtat == "Reponse")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Réponse à un congé effectué avec succès. Nombre d'élements : " + this.listConge.Count() + " / " + this.max);
            }
            else
            {
((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des congés terminé : " + this.listConge.Count() + " / " + this.max);
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
            this.listConge = new ObservableCollection<Conge>(this.listConge.OrderByDescending(dat => dat.Date_Debut));
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
