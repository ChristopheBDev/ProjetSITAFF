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
using SitaffRibbon.Windows;
using System.Collections.ObjectModel;
using System.Threading;
using System.ComponentModel;
using SitaffRibbon.Classes;

using SitaffRibbon.Converters;


namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeReservationSalleControl.xaml
    /// </summary>
    public partial class ListeReservationSalleControl : UserControl
    {

        #region Variables
        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_ColonneEntrepriseMere;
        MenuItem MenuItem_ColonneSalle;
        MenuItem MenuItem_ColonneDemandeur;
        MenuItem MenuItem_ColonneNbParticipants;
        MenuItem MenuItem_ColonneDateReservation;
        MenuItem MenuItem_ColonneHeureDebut;
        MenuItem MenuItem_ColonneHeureFin;
        MenuItem MenuItem_ColonneCommentaire;
        MenuItem MenuItem_ColonneObjetReunion;

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Constructeur
        public ListeReservationSalleControl()
        {
            InitializeComponent();
            //initialisation des listes
            this.initialisationFilterZone();

            //initialisation DataGridMain
            this.initialisationDatagridMain(null);

            //Masquer la zone de filtre
            this.AfficherMasquer();

            //Création du menu du clic droit
            this.creationMenuClicDroit();

            
        }

        #region  Initialisation DataGridMain
        private void initialisationDatagridMain(ObservableCollection<Reservation_Salle> listToPut)
        {
            if (listToPut == null)
            {
                this.listReservationSalle = new ObservableCollection<Reservation_Salle>(((App)App.Current).mySitaffEntities.Reservation_Salle.OrderBy(rs => rs.Identifiant));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listReservationSalle = new ObservableCollection<Reservation_Salle>(listToPut);
                this.MiseAJourEtat("Filtrage", null);
            }
            
        }
        #endregion

        #region Initialisation Liste
        private void initialisationFilterZone()
        {
            this.listReservationSalle = new ObservableCollection<Reservation_Salle>(((App)App.Current).mySitaffEntities.Reservation_Salle.OrderBy(res => res.Identifiant));
            this.listSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.OrderBy(s => s.Personne.Nom));
            this.listEntrepriseMere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(ent => ent.Nom));
            this.listSalle = new ObservableCollection<Salle>(((App)App.Current).mySitaffEntities.Salle.OrderBy(sa => sa.Libelle));
            this.initialisationAutoCompleteBox();
            
        }
        private void initialisationAutoCompleteBox()
        {
            List<string> listEnt = new List<string>();
            List<string> listDem = new List<string>();
            List<string> listSal = new List<string>();
            List<string> listNbPart = new List<string>();
            List<string> listObjReunion = new List<string>();
            List<string> listHeureDebut = new List<string>();
            List<string> listHeureFin = new List<string>();

            foreach (Reservation_Salle item in ((App)App.Current).mySitaffEntities.Reservation_Salle)
            {
                if (item.Salarie1 != null)
                {
                    if (item.Salarie1.Personne != null)
                    {
                        if (!listDem.Contains(item.Salarie1.Personne.fullname))
                        {
                            listDem.Add(item.Salarie1.Personne.fullname);
                        }
                    }
                }
                if (item.Entreprise_Mere1 != null)
                {
                    if (!listEnt.Contains(item.Entreprise_Mere1.Nom))
                    {
                        listEnt.Add(item.Entreprise_Mere1.Nom);
                    }
                }
                if (item.Salle1 != null)
                {
                    if (!listSal.Contains(item.Salle1.Libelle))
                    {
                        listSal.Add(item.Salle1.Libelle);
                    }
                }
                if (item.ObjetReunion != null)
                {
                    if (!listObjReunion.Contains(item.ObjetReunion))
                    {
                        listObjReunion.Add(item.ObjetReunion);
                    }
                }
                if (item.Nb_Participants != null)
                {
                    if (!listNbPart.Contains(item.Nb_Participants.ToString()))
                    {
                        listNbPart.Add(item.Nb_Participants.ToString());
                    }
                }
                if (item.Heure_Debut != null)
                {
                    if (!listHeureDebut.Contains(item.Heure_Debut))
                    {
                        listHeureDebut.Add(item.Heure_Debut);
                    }
                }
                if (item.Heure_Fin != null)
                {
                    if (!listHeureFin.Contains(item.Heure_Fin))
                    {
                        listHeureFin.Add(item.Heure_Fin);
                    }
                }
            }
            this._filterContainDemandeur.ItemsSource = listDem;
            this._filterContainEntrepriseMere.ItemsSource = listEnt;
            this._filterContainSalle.ItemsSource = listSal;
            this._filterContainNbParticipant.ItemsSource = listNbPart;
            this._filterContainObjetReunion.ItemsSource = listObjReunion;
            this._filterContainHeureDebut.ItemsSource = listHeureDebut;
            this._filterContainHeureFin.ItemsSource = listHeureFin;
        }
        #endregion

        #region Clic Droit

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
            itemAfficher5.Header = "Dupliquer";
			itemAfficher5.Click += new RoutedEventHandler(delegate { this.Duplicate(); });

            MenuItem itemAfficher8 = RemplirMenuAfficherMasquerColonnes(new MenuItem());
            itemAfficher8.Header = "Afficher / Masquer";

            if (((App)App.Current).securite.VerificationDroitActionsCRUD(this.ToString(), "Look"))
            {
                contextMenu.Items.Add(itemAfficher);
            }
            if (((App)App.Current).securite.VerificationDroitActionsCRUD(this.ToString(), "Add"))
            {
                contextMenu.Items.Add(itemAfficher2);
                contextMenu.Items.Add(itemAfficher5);
            }
            if (((App)App.Current).securite.VerificationDroitActionsCRUD(this.ToString(), "Update"))
            {
                contextMenu.Items.Add(itemAfficher3);
            }
            if (((App)App.Current).securite.VerificationDroitActionsCRUD(this.ToString(), "Remove"))
            {
                contextMenu.Items.Add(itemAfficher4);
            }
            
            contextMenu.Items.Add(new Separator());
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
        
        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {
            this.MenuItem_ColonneDemandeur = new MenuItem();
            this.MenuItem_ColonneDemandeur.IsChecked = false;
            this.MenuItem_ColonneDemandeur.Header = "Demandeur";
            this.MenuItem_ColonneDemandeur.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDemandeur(); });
            this.AffMas_ColonneDemandeur();
            menuItem.Items.Add(this.MenuItem_ColonneDemandeur);

            this.MenuItem_ColonneEntrepriseMere = new MenuItem();
            this.MenuItem_ColonneEntrepriseMere.IsChecked = false;
            this.MenuItem_ColonneEntrepriseMere.Header = "Entreprise Mère";
            this.MenuItem_ColonneEntrepriseMere.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEntrepriseMere(); });
            this.AffMas_ColonneEntrepriseMere();
            menuItem.Items.Add(this.MenuItem_ColonneEntrepriseMere);

            this.MenuItem_ColonneSalle = new MenuItem();
            this.MenuItem_ColonneSalle.IsChecked = false;
            this.MenuItem_ColonneSalle.Header = "Salle";
            this.MenuItem_ColonneSalle.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneSalle(); });
            this.AffMas_ColonneSalle();
            menuItem.Items.Add(this.MenuItem_ColonneSalle);
            
            this.MenuItem_ColonneObjetReunion = new MenuItem();
            this.MenuItem_ColonneObjetReunion.IsChecked = false;
            this.MenuItem_ColonneObjetReunion.Header = "Objet de la Réunion";
            this.MenuItem_ColonneObjetReunion.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneObjetReunion(); });
            this.AffMas_ColonneObjetReunion();
            menuItem.Items.Add(this.MenuItem_ColonneObjetReunion);

            this.MenuItem_ColonneDateReservation = new MenuItem();
            this.MenuItem_ColonneDateReservation.IsChecked = false;
            this.MenuItem_ColonneDateReservation.Header = "Date de Réservation";
            this.MenuItem_ColonneDateReservation.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateReservation(); });
            this.AffMas_ColonneDateReservation();
            menuItem.Items.Add(this.MenuItem_ColonneDateReservation);
            
            this.MenuItem_ColonneHeureDebut = new MenuItem();
            this.MenuItem_ColonneHeureDebut.IsChecked = false;
            this.MenuItem_ColonneHeureDebut.Header = "Heure de Début";
            this.MenuItem_ColonneHeureDebut.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneHeureDebut(); });
            this.AffMas_ColonneHeureDebut();
            menuItem.Items.Add(this.MenuItem_ColonneHeureDebut);

            this.MenuItem_ColonneHeureFin = new MenuItem();
            this.MenuItem_ColonneHeureFin.IsChecked = false;
            this.MenuItem_ColonneHeureFin.Header = "Heure de Fin";
            this.MenuItem_ColonneHeureFin.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneHeureFin(); });
            this.AffMas_ColonneHeureFin();
            menuItem.Items.Add(this.MenuItem_ColonneHeureFin);

            this.MenuItem_ColonneNbParticipants = new MenuItem();
            this.MenuItem_ColonneNbParticipants.IsChecked = false;
            this.MenuItem_ColonneNbParticipants.Header = "Nb Participants";
            this.MenuItem_ColonneNbParticipants.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNbParticipants(); });
            this.AffMas_ColonneNbParticipants();
            menuItem.Items.Add(this.MenuItem_ColonneNbParticipants);

            this.MenuItem_ColonneCommentaire = new MenuItem();
            this.MenuItem_ColonneCommentaire.IsChecked = false;
            this.MenuItem_ColonneCommentaire.Header = "Commentaires";
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


        private void AffMas_ColonneEntrepriseMere()
        {
            if (this.MenuItem_ColonneEntrepriseMere.IsChecked == true)
            {
                this._ColonneEntrepriseMere.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneEntrepriseMere.IsChecked = false;
            }
            else
            {
                this._ColonneEntrepriseMere.Visibility = Visibility.Visible;
                this.MenuItem_ColonneEntrepriseMere.IsChecked = true;
            }
        }

        private void AffMas_ColonneSalle()
        {
            if (this.MenuItem_ColonneSalle.IsChecked == true)
            {
                this._ColonneSalle.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneSalle.IsChecked = false;
            }
            else
            {
                this._ColonneSalle.Visibility = Visibility.Visible;
                this.MenuItem_ColonneSalle.IsChecked = true;
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

        private void AffMas_ColonneNbParticipants()
        {
            if (this.MenuItem_ColonneNbParticipants.IsChecked == true)
            {
                this._ColonneNbParticipants.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNbParticipants.IsChecked = false;
            }
            else
            {
                this._ColonneNbParticipants.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNbParticipants.IsChecked = true;
            }
        }

        private void AffMas_ColonneDateReservation()
        {
            if (this.MenuItem_ColonneDateReservation.IsChecked == true)
            {
                this._ColonneDateReservation.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDateReservation.IsChecked = false;
            }
            else
            {
                this._ColonneDateReservation.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDateReservation.IsChecked = true;
            }
        }

        private void AffMas_ColonneHeureDebut()
        {
            if (this.MenuItem_ColonneHeureDebut.IsChecked == true)
            {
                this._ColonneHeureDebut.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneHeureDebut.IsChecked = false;
            }
            else
            {
                this._ColonneHeureDebut.Visibility = Visibility.Visible;
                this.MenuItem_ColonneHeureDebut.IsChecked = true;
            }
        }

        private void AffMas_ColonneHeureFin()
        {
            if (this.MenuItem_ColonneHeureFin.IsChecked == true)
            {
                this._ColonneHeureFin.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneHeureFin.IsChecked = false;
            }
            else
            {
                this._ColonneHeureFin.Visibility = Visibility.Visible;
                this.MenuItem_ColonneHeureFin.IsChecked = true;
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

        private void AffMas_ColonneObjetReunion()
        {
            if (this.MenuItem_ColonneObjetReunion.IsChecked == true)
            {
                this._ColonneObjetReunion.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneObjetReunion.IsChecked = false;
            }
            else
            {
                this._ColonneObjetReunion.Visibility = Visibility.Visible;
                this.MenuItem_ColonneObjetReunion.IsChecked = true;
            }
        }

        #region Tout

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneEntrepriseMere.IsChecked = false;
            this.MenuItem_ColonneSalle.IsChecked = false;
            this.MenuItem_ColonneDemandeur.IsChecked = false;
            this.MenuItem_ColonneNbParticipants.IsChecked = false;
            this.MenuItem_ColonneDateReservation.IsChecked = false;
            this.MenuItem_ColonneHeureDebut.IsChecked = false;
            this.MenuItem_ColonneHeureFin.IsChecked = false;
            this.MenuItem_ColonneCommentaire.IsChecked = false;
            this.MenuItem_ColonneObjetReunion.IsChecked = false;

            this.AffMas_ColonneEntrepriseMere();
            this.AffMas_ColonneSalle();
            this.AffMas_ColonneDemandeur();
            this.AffMas_ColonneNbParticipants();
            this.AffMas_ColonneDateReservation();
            this.AffMas_ColonneHeureDebut();
            this.AffMas_ColonneHeureFin();
            this.AffMas_ColonneCommentaire();
            this.AffMas_ColonneObjetReunion();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneEntrepriseMere.IsChecked = true;
            this.MenuItem_ColonneSalle.IsChecked = true;
            this.MenuItem_ColonneDemandeur.IsChecked = true;
            this.MenuItem_ColonneNbParticipants.IsChecked = true;
            this.MenuItem_ColonneDateReservation.IsChecked = true;
            this.MenuItem_ColonneHeureDebut.IsChecked = true;
            this.MenuItem_ColonneHeureFin.IsChecked = true;
            this.MenuItem_ColonneCommentaire.IsChecked = true;
            this.MenuItem_ColonneObjetReunion.IsChecked = true;

            this.AffMas_ColonneEntrepriseMere();
            this.AffMas_ColonneSalle();
            this.AffMas_ColonneDemandeur();
            this.AffMas_ColonneNbParticipants();
            this.AffMas_ColonneDateReservation();
            this.AffMas_ColonneHeureDebut();
            this.AffMas_ColonneHeureFin();
            this.AffMas_ColonneCommentaire();
            this.AffMas_ColonneObjetReunion();
        }
        #endregion

        #endregion

        #endregion

        #region Fenêtre Chargée

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((MainWindow)((Grid)((Border)this.Parent).Parent).Parent).stopThread();
        }

        #endregion

        #region Propriétés de dépendance

        #region Reservation_Salle
        public ObservableCollection<Reservation_Salle> listReservationSalle
        {
            get { return (ObservableCollection<Reservation_Salle>)GetValue(listReservationSalleProperty); }
            set { SetValue(listReservationSalleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listReservationSalle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listReservationSalleProperty =
            DependencyProperty.Register("listReservationSalle", typeof(ObservableCollection<Reservation_Salle>), typeof(ListeReservationSalleControl), new UIPropertyMetadata(null));
        #endregion

        #region Entreprise_Mere
        public ObservableCollection<Entreprise_Mere> listEntrepriseMere
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntrepriseMereProperty); }
            set { SetValue(listEntrepriseMereProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntrepriseMere.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseMereProperty =
            DependencyProperty.Register("listEntrepriseMere", typeof(ObservableCollection<Entreprise_Mere>), typeof(ListeReservationSalleControl), new UIPropertyMetadata(null));
        #endregion

        #region Salle
        public ObservableCollection<Salle> listSalle
        {
            get { return (ObservableCollection<Salle>)GetValue(listSalleProperty); }
            set { SetValue(listSalleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalleProperty =
            DependencyProperty.Register("listSalle", typeof(ObservableCollection<Salle>), typeof(ListeReservationSalleControl), new UIPropertyMetadata(null));
        #endregion

        #region Salarié
        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(ListeReservationSalleControl), new UIPropertyMetadata(null));
        #endregion

        #endregion

        #region Fonction

        /// <summary>
        /// Recalcul le nombre d'élements maximum
        /// </summary>
        private void recalculMax()
        {
            this.max = ((App)App.Current).mySitaffEntities.Reservation_Salle.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Reservation_Salle res)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
                ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "filtrage des réservation terminée : " + this.listReservationSalle.Count() + " / " + this.max;
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la réservation dans le linsting
                this.listReservationSalle.Add(res);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Ajout d'une réservation numéro '" + res.Identifiant + "' effectué avec succès. Nombre d'élements : " + this.listReservationSalle.Count() + " / " + this.max;
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
                ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Modification de la réservation numéro : '" + res.Identifiant + "' effectuée avec succès. Nombre d'élements : " + this.listReservationSalle.Count() + " / " + this.max;
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listReservationSalle.Remove(res);
                //Je recalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Suppression de la réservation numéro : '" + res.Identifiant + "' effectuée avec succès. Nombre d'élements : " + this.listReservationSalle.Count() + " / " + this.max;
            }
            else if (typeEtat == "Look")
            {

            }
            else if (typeEtat == "Duplicate")
            {
                //J'ajoute la réservation dans le linsting
                this.listReservationSalle.Add(res);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Dupliquer une réservation numéro '" + res.Identifiant + "' effectué avec succès. Nombre d'élements : " + this.listReservationSalle.Count() + " / " + this.max;
            }
            else
            {
                ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Chargement des réservations terminé : " + this.listReservationSalle.Count() + " / " + this.max;
            }
            //Je retri les données dans le sens par défaut
            this.triDatas();
            //J'arrete la progressbar
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
            this._DataGridMain.Items.Refresh();
        }
        /// <summary>
        /// Tri les données dans le sens par défaut
        /// </summary>
        private void triDatas()
        {
            this.listReservationSalle = new ObservableCollection<Reservation_Salle>(this.listReservationSalle.OrderBy(res => res.Identifiant));
        }

        /// <summary>
        /// duplique la commande passée en paramètre
        /// </summary>
        /// <param name="commande1">commande à dupliquer</param>
        private Reservation_Salle duplicateReservationSalle(Reservation_Salle itemToCopy)
        {
            Reservation_Salle tmp = new Reservation_Salle();


            tmp.Date_Reservation = itemToCopy.Date_Reservation;
            tmp.Date_Reservation_Fin = itemToCopy.Date_Reservation_Fin;
            tmp.Heure_Debut = itemToCopy.Heure_Debut;
            tmp.Heure_Fin = itemToCopy.Heure_Fin;
            tmp.Nb_Participants = itemToCopy.Nb_Participants;
            tmp.ObjetReunion = itemToCopy.ObjetReunion;
            tmp.Commentaire = itemToCopy.Commentaire;
            tmp.Entreprise_Mere1 = itemToCopy.Entreprise_Mere1;
            tmp.Salle1 = itemToCopy.Salle1;
            foreach (Reservation_SalleBesoin_Reservation_Salle item in itemToCopy.Reservation_SalleBesoin_Reservation_Salle)
            {
                Reservation_SalleBesoin_Reservation_Salle toAdd = new Reservation_SalleBesoin_Reservation_Salle();
                toAdd.Quantite = item.Quantite;
                toAdd.Besoin_Reservation_Salle1 = item.Besoin_Reservation_Salle1;
                tmp.Reservation_SalleBesoin_Reservation_Salle.Add(toAdd);
                
            }
            foreach (Reservation_SalleContact_Client_Invite item in itemToCopy.Reservation_SalleContact_Client_Invite)
            {
                Reservation_SalleContact_Client_Invite toAdd = new Reservation_SalleContact_Client_Invite();
                toAdd.Contact1 = item.Contact1;
                tmp.Reservation_SalleContact_Client_Invite.Add(toAdd);
            }
            foreach (Reservation_SalleContact_Fournisseur_Invite item in itemToCopy.Reservation_SalleContact_Fournisseur_Invite)
            {
                Reservation_SalleContact_Fournisseur_Invite toAdd = new Reservation_SalleContact_Fournisseur_Invite();
                toAdd.Contact1 = item.Contact1;
                tmp.Reservation_SalleContact_Fournisseur_Invite.Add(toAdd);
            }
            foreach (Reservation_SalleSalarie_Invite item in itemToCopy.Reservation_SalleSalarie_Invite)
            {
                Reservation_SalleSalarie_Invite toAdd = new Reservation_SalleSalarie_Invite();
                toAdd.Salarie1 = item.Salarie1;
                tmp.Reservation_SalleSalarie_Invite.Add(toAdd);
            }
            tmp.Salarie1 = ((App)App.Current)._connectedUser.Salarie_Interne1.Salarie;
            return tmp;
        }

        #endregion

        #region Commande

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

        #region filtrage

        #region  Bouton Filtrer
        private void _buttonFiltrer_Click(object sender, RoutedEventArgs e)
        {
            this.filtrage();
        }

        private void filtrage()
        {
            ObservableCollection<Reservation_Salle> listToPut = new ObservableCollection<Reservation_Salle>(((App)App.Current).mySitaffEntities.Reservation_Salle.OrderBy(res => res.Identifiant));
            
            if (this._filterContainHeureDebut.Text != "")
            {
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(hDeb => hDeb.Heure_Debut != null));
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(hDeb => hDeb.Heure_Debut.Trim().ToLower().Contains(this._filterContainHeureDebut.Text.Trim().ToLower())));
            }
            if (this._filterContainHeureFin.Text != "")
            {
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(hDeb => hDeb.Heure_Fin != null));
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(hDeb => hDeb.Heure_Fin.Trim().ToLower().Contains(this._filterContainHeureFin.Text.Trim().ToLower())));
            }
            if (this._filterContainDemandeur.SelectedItem != null)
            {
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(dem => dem.Salarie1.Personne.fullname != null));
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(dem => dem.Salarie1.Personne.fullname.ToString().Trim().Contains(this._filterContainDemandeur.Text)));
            }
            if (this._filterContainEntrepriseMere.SelectedItem != null)
            {
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(ent => ent.Entreprise_Mere1.Nom != null));
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(ent => ent.Entreprise_Mere1.Nom.Trim().Contains(this._filterContainEntrepriseMere.Text)));
            }
            if (this._filterContainSalle.SelectedItem != null)
            {
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(sa => sa.Salle1.Libelle != null));
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(sa => sa.Salle1.Libelle.Trim().Contains(this._filterContainSalle.Text)));
            }
            if (this._DatePickerDateReservation.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(date => date.Date_Reservation != null));
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(date => date.Date_Reservation.ToString().Trim().Contains(this._DatePickerDateReservation.SelectedDate.ToString().ToLower())));
            }
            if (this._filterContainObjetReunion.Text != null)
            {
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(obj => obj.ObjetReunion != null));
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(obj => obj.ObjetReunion.ToString().Trim().Contains(this._filterContainObjetReunion.Text.Trim().ToLower())));
            }
            if (this._filterContainNbParticipant.Text != null)
            {
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(nbp => nbp.Nb_Participants != null));
                listToPut = new ObservableCollection<Reservation_Salle>(listToPut.Where(nbp => nbp.Nb_Participants.ToString().Trim().Contains(this._filterContainNbParticipant.Text.Trim().ToLower())));
            }

            this.initialisationDatagridMain(listToPut);

            if (this.listReservationSalle.Count() == 0)
            {
                MessageBox.Show("Aucun résultat ne correspont à votre recherche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Remise à Zéro
        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.RemiseAZero();
        }
        private void RemiseAZero()
        {
            //Remise à Zéro TextBox
            this._filterContainHeureDebut.Text = "";
            this._filterContainHeureFin.Text = "";
            this._filterContainNbParticipant.Text = "";
            this._filterContainObjetReunion.Text = "";
            this._filterContainDemandeur.Text = "";
            this._filterContainEntrepriseMere.Text = "";
            this._filterContainSalle.Text = "";
            //Remise à Zéro DatePicker
            this._DatePickerDateReservation.SelectedDate = null;
            //Rechargement des élements
            this.initialisationDatagridMain(null);
            //Rechargement des autocompletebox
            this.initialisationAutoCompleteBox();
        }
        #endregion

        #region bouton masquer / afficher

        private void ButtonMasqueFiltre_Click(object sender, RoutedEventArgs e)
        {
            this.AfficherMasquer();
        }

        public void AfficherMasquer()
        {
            if (_filterZone.Height != 21)
            {
                this._filterZone.Height = 21;
                this._buttonMasqueFiltre.Content = "Afficher les filtres";
                //Lorsque je masque, je remet à zéro si certains champs sont rempli OU si le nombre d'élements max n'est pas égal au nombre d'éléments actuel
                if (this._filterContainDemandeur.SelectedItem != null ||
                    this._filterContainEntrepriseMere.SelectedItem != null || this._filterContainSalle.SelectedItem != null ||
                    this._DatePickerDateReservation.SelectedDate != null || this._filterContainHeureDebut.Text != "" || this._filterContainHeureFin.Text != "" ||
                    this.max != this.listReservationSalle.Count())
                {
                    this.RemiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._buttonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainDemandeur.Focus();
            }
        }

        #endregion

        #region nullBox

        private void _buttonDateNull_Click(object sender, RoutedEventArgs e)
        {
            this._DatePickerDateReservation.SelectedDate = null;
        }
        #endregion

        #endregion

        #region Evènements

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

        #region CRUD (Create Read Update Delete)

        /// <summary>
        /// Ajoute une nouvelle Réservation à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Reservation_Salle Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Ajout d'une résevation en cours ...";

            //Initialisation de la fenêtre
            ReservationSalleWindow creationreservationSalle = new ReservationSalleWindow();

            //Création de l'objet temporaire
            Reservation_Salle tmp = new Reservation_Salle();
            tmp.Salarie1 = ((App)App.Current)._connectedUser.Salarie_Interne1.Salarie;
            tmp.Entreprise_Mere1 = ((App)App.Current)._connectedUser.Salarie_Interne1.Entreprise_Mere1;
            tmp.Heure_Debut = "xx:xx";
            tmp.Heure_Fin = "xx:xx";
            creationreservationSalle._textBoxHeureDebut.Foreground = Brushes.Gray;
            creationreservationSalle._textBoxHeureFin.Foreground = Brushes.Gray;

            //Mise de l'objet temporaire dans le datacontext
            creationreservationSalle.DataContext = tmp;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = creationreservationSalle.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet commande se trouvant dans le datacontext de la fenêtre
                return (Reservation_Salle)creationreservationSalle.DataContext;
                
            }
            else
            {
                try
                {
                    //On détache tous les élements liés à la réservation Reservation_SalleContact_Client_Invite
                    ObservableCollection<Reservation_SalleContact_Client_Invite> toRemove = new ObservableCollection<Reservation_SalleContact_Client_Invite>();
                    foreach (Reservation_SalleContact_Client_Invite item in ((Reservation_Salle)creationreservationSalle.DataContext).Reservation_SalleContact_Client_Invite)
                    {
                        toRemove.Add(item);
                    }
                    foreach (Reservation_SalleContact_Client_Invite item in toRemove)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    //On détache tous les élements liés à la réservation Reservation_SalleContact_Fournisseur_Invite
                    ObservableCollection<Reservation_SalleContact_Fournisseur_Invite> toRemove1 = new ObservableCollection<Reservation_SalleContact_Fournisseur_Invite>();
                    foreach (Reservation_SalleContact_Fournisseur_Invite item in ((Reservation_Salle)creationreservationSalle.DataContext).Reservation_SalleContact_Fournisseur_Invite)
                    {
                        toRemove1.Add(item);
                    }
                    foreach (Reservation_SalleContact_Fournisseur_Invite item in toRemove1)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    //On détache tout les éléments liés à la réservation Reservation_SalleSalarie_Invite
                    ObservableCollection<Reservation_SalleSalarie_Invite> toRemove2 = new ObservableCollection<Reservation_SalleSalarie_Invite>();
                    foreach (Reservation_SalleSalarie_Invite item in ((Reservation_Salle)creationreservationSalle.DataContext).Reservation_SalleSalarie_Invite)
                    {
                        toRemove2.Add(item);
                    }
                    foreach (Reservation_SalleSalarie_Invite item in toRemove2)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    //On détache tout les éléments liés à la réservation Reservation_SalleBesoin_Reservation_Salle
                    ObservableCollection<Reservation_SalleBesoin_Reservation_Salle> toRemove3 = new ObservableCollection<Reservation_SalleBesoin_Reservation_Salle>();
                    foreach (Reservation_SalleBesoin_Reservation_Salle item in ((Reservation_Salle)creationreservationSalle.DataContext).Reservation_SalleBesoin_Reservation_Salle)
                    {
                        toRemove3.Add(item);
                    }
                    foreach (Reservation_SalleBesoin_Reservation_Salle item in toRemove3)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    //On détache la réservation
                    ((App)App.Current).mySitaffEntities.Detach((Reservation_Salle)creationreservationSalle.DataContext);
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
                ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Ajout d'une réservation annulé : " + this.listReservationSalle.Count() + " / " + this.max;
                return null;
            }
        }
        /// <summary>
        /// Ouvre la réservation séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Reservation_Salle Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Modification d'une réservation en cours ...";

                    //Création de la fenêtre
                    ReservationSalleWindow reservationSalle = new ReservationSalleWindow();

                    //Initialisation du Datacontext en Reservation_Salle et association à la Reservation_Salle sélectionné
                    reservationSalle.DataContext = new Reservation_Salle();
                    reservationSalle.DataContext = (Reservation_Salle)this._DataGridMain.SelectedItem;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = reservationSalle.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet Reservation_Salle dans le datacontext de la fenêtre
                        return (Reservation_Salle)reservationSalle.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Reservation_Salle)(this._DataGridMain.SelectedItem));
                        //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Modification d'une réservation annulé : " + this.listReservationSalle.Count() + " / " + this.max;

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une réservation.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une réservation.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }
        /// <summary>
        /// Supprime la réservation séléctionnée avec une confirmation
        /// </summary>
        public Reservation_Salle Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Suppression d'une réservation en cours ...";

                    if (MessageBox.Show("Voulez-vous rééllement supprimer la réservation séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //On détache tous les élements liés à la réservation Reservation_SalleSalarie_Invite
                        ObservableCollection<Reservation_SalleSalarie_Invite> toRemove = new ObservableCollection<Reservation_SalleSalarie_Invite>();
                        foreach (Reservation_SalleSalarie_Invite item in ((Reservation_Salle)this._DataGridMain.SelectedItem).Reservation_SalleSalarie_Invite)
                        {
                            toRemove.Add(item);
                        }
                        foreach (Reservation_SalleSalarie_Invite item in toRemove)
                        {
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Reservation_SalleSalarie_Invite.DeleteObject(item);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    ((Reservation_Salle)this._DataGridMain.SelectedItem).Reservation_SalleSalarie_Invite.Remove(item);
                                }
                                catch (Exception)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(item);
                                }
                            }
                        }

                        //On détache tous les élements liés à la réservation Reservation_SalleBesoin_Reservation_Salle
                        ObservableCollection<Reservation_SalleBesoin_Reservation_Salle> toRemove1 = new ObservableCollection<Reservation_SalleBesoin_Reservation_Salle>();
                        foreach (Reservation_SalleBesoin_Reservation_Salle item in ((Reservation_Salle)this._DataGridMain.SelectedItem).Reservation_SalleBesoin_Reservation_Salle)
                        {
                            toRemove1.Add(item);
                        }
                        foreach (Reservation_SalleBesoin_Reservation_Salle item in toRemove1)
                        {
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Reservation_SalleBesoin_Reservation_Salle.DeleteObject(item);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    ((Reservation_Salle)this._DataGridMain.SelectedItem).Reservation_SalleBesoin_Reservation_Salle.Remove(item);
                                }
                                catch (Exception)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(item);
                                }
                            }
                        }

                        //On détache tous les élements liés à la réservation Reservation_SalleContact_Client_Invite
                        ObservableCollection<Reservation_SalleContact_Client_Invite> toRemove2 = new ObservableCollection<Reservation_SalleContact_Client_Invite>();
                        foreach (Reservation_SalleContact_Client_Invite item in ((Reservation_Salle)this._DataGridMain.SelectedItem).Reservation_SalleContact_Client_Invite)
                        {
                            toRemove2.Add(item);
                        }
                        foreach (Reservation_SalleContact_Client_Invite item in toRemove2)
                        {
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Reservation_SalleContact_Client_Invite.DeleteObject(item);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    ((Reservation_Salle)this._DataGridMain.SelectedItem).Reservation_SalleContact_Client_Invite.Remove(item);
                                }
                                catch (Exception)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(item);
                                }
                            }
                        }
                        //On détache tous les élements liés à la réservation Reservation_SalleContact_Fournisseur_Invite
                        ObservableCollection<Reservation_SalleContact_Fournisseur_Invite> toRemove3 = new ObservableCollection<Reservation_SalleContact_Fournisseur_Invite>();
                        foreach (Reservation_SalleContact_Fournisseur_Invite item in ((Reservation_Salle)this._DataGridMain.SelectedItem).Reservation_SalleContact_Fournisseur_Invite)
                        {
                            toRemove3.Add(item);
                        }
                        foreach (Reservation_SalleContact_Fournisseur_Invite item in toRemove3)
                        {
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Reservation_SalleContact_Fournisseur_Invite.DeleteObject(item);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    ((Reservation_Salle)this._DataGridMain.SelectedItem).Reservation_SalleContact_Fournisseur_Invite.Remove(item);
                                }
                                catch (Exception)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(item);
                                }
                            }
                        }
                        //Supprimer l'élément
                        return (Reservation_Salle)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Suppression d'une réservation annulé : " + this.listReservationSalle.Count() + " / " + this.max;

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule réservation.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une réservation.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }
        /// <summary>
        /// Ouvre la réservation séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Reservation_Salle Look(Reservation_Salle reservationSalle)
        {
            if (this._DataGridMain.SelectedItem != null || reservationSalle != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1 || reservationSalle != null)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Affichage d'une réservation en cours ...";

                    //Création de la fenêtre
                    ReservationSalleWindow creationreservationWindow = new ReservationSalleWindow();

                    //Initialisation du Datacontext en Reservation_Salle et association à la Reservation_Salle sélectionnée
                    creationreservationWindow.DataContext = new Reservation_Salle();
                    if (reservationSalle != null)
                    {
                        creationreservationWindow.DataContext = reservationSalle;
                    }
                    else
                    {
                        creationreservationWindow.DataContext = (Reservation_Salle)this._DataGridMain.SelectedItem;
                    }

                    //Je positionne la lecture seule sur la fenêtre
                    creationreservationWindow.Lecture_Seule();

                    //J'affiche la fenêtre
                    bool? dialogResult = creationreservationWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Affichage d'une réservation terminé : " + this.listReservationSalle.Count() + " / " + this.max;

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule réservation.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une réservation.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }
        #endregion

        #region Action supplémentaire
        /// <summary>
        /// duplique une Commande_Fournisseur à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Reservation_Salle Duplicate()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "ajout en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer une réservation en cours ...");

                    //Création de la fenêtre
                    ReservationSalleWindow reservationSalleWindow = new ReservationSalleWindow();

                    //Duplication de la commande sélectionnée
                    Reservation_Salle tmp = new Reservation_Salle();
                    tmp = duplicateReservationSalle((Reservation_Salle)this._DataGridMain.SelectedItem);

                    //Association de l'élement dupliqué au datacontext de la fenêtre
                    reservationSalleWindow.DataContext = tmp;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = reservationSalleWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Reservation_Salle)reservationSalleWindow.DataContext;
                    }
                    else
                    {
                        try
                        {
                            //On détache tous les élements liés à la réservation Reservation_SalleContact_Client_Invite
                            ObservableCollection<Reservation_SalleContact_Client_Invite> toRemove = new ObservableCollection<Reservation_SalleContact_Client_Invite>();
                            foreach (Reservation_SalleContact_Client_Invite item in ((Reservation_Salle)reservationSalleWindow.DataContext).Reservation_SalleContact_Client_Invite)
                            {
                                toRemove.Add(item);
                            }
                            foreach (Reservation_SalleContact_Client_Invite item in toRemove)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache tous les élements liés à la réservation Reservation_SalleContact_Fournisseur_Invite
                            ObservableCollection<Reservation_SalleContact_Fournisseur_Invite> toRemove1 = new ObservableCollection<Reservation_SalleContact_Fournisseur_Invite>();
                            foreach (Reservation_SalleContact_Fournisseur_Invite item in ((Reservation_Salle)reservationSalleWindow.DataContext).Reservation_SalleContact_Fournisseur_Invite)
                            {
                                toRemove1.Add(item);
                            }
                            foreach (Reservation_SalleContact_Fournisseur_Invite item in toRemove1)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache tout les éléments liés à la réservation Reservation_SalleSalarie_Invite
                            ObservableCollection<Reservation_SalleSalarie_Invite> toRemove2 = new ObservableCollection<Reservation_SalleSalarie_Invite>();
                            foreach (Reservation_SalleSalarie_Invite item in ((Reservation_Salle)reservationSalleWindow.DataContext).Reservation_SalleSalarie_Invite)
                            {
                                toRemove2.Add(item);
                            }
                            foreach (Reservation_SalleSalarie_Invite item in toRemove2)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache tout les éléments liés à la réservation Reservation_SalleBesoin_Reservation_Salle
                            ObservableCollection<Reservation_SalleBesoin_Reservation_Salle> toRemove3 = new ObservableCollection<Reservation_SalleBesoin_Reservation_Salle>();
                            foreach (Reservation_SalleBesoin_Reservation_Salle item in ((Reservation_Salle)reservationSalleWindow.DataContext).Reservation_SalleBesoin_Reservation_Salle)
                            {
                                toRemove3.Add(item);
                            }
                            foreach (Reservation_SalleBesoin_Reservation_Salle item in toRemove3)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache la réservation
                            ((App)App.Current).mySitaffEntities.Detach((Reservation_Salle)reservationSalleWindow.DataContext);
                        }
                        catch (Exception)
                        {
                        }

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Ajout d'une réservation annulé : " + this.listReservationSalle.Count() + " / " + this.max;
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule réservation.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une réservation.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }
        #endregion

    }
}