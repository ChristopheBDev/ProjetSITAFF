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
    /// Logique d'interaction pour ListeReveleHeureAtelierControl.xaml
    /// </summary>
    public partial class ListeReveleHeureAtelierControl : UserControl
    {

        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_ColonneSalarie;
        MenuItem MenuItem_ColonneNumeroSemaine;
        MenuItem MenuItem_ColonneDateDebut;
        MenuItem MenuItem_ColonneTotalSemaine;
        MenuItem MenuItem_ColonneHeureLundi;
        MenuItem MenuItem_ColonneHeureMardi;
        MenuItem MenuItem_ColonneHeureMercredi;
        MenuItem MenuItem_ColonneHeureJeudi;
        MenuItem MenuItem_ColonneHeureVendredi;
        MenuItem MenuItem_ColonneHeureSamedi;
        MenuItem MenuItem_ColonneObservationLundi;
        MenuItem MenuItem_ColonneObservationMardi;
        MenuItem MenuItem_ColonneObservationMercredi;
        MenuItem MenuItem_ColonneObservationJeudi;
        MenuItem MenuItem_ColonneObservationVendredi;
        MenuItem MenuItem_ColonneObservationSamedi;
        MenuItem MenuItem_ColonneRouteLundi;
        MenuItem MenuItem_ColonneRouteMardi;
        MenuItem MenuItem_ColonneRouteMercredi;
        MenuItem MenuItem_ColonneRouteJeudi;
        MenuItem MenuItem_ColonneRouteVendredi;
        MenuItem MenuItem_ColonneRouteSamedi;
        MenuItem MenuItem_ColonneRepasLundi;
        MenuItem MenuItem_ColonneRepasMardi;
        MenuItem MenuItem_ColonneRepasMercredi;
        MenuItem MenuItem_ColonneRepasJeudi;
        MenuItem MenuItem_ColonneRepasVendredi;
        MenuItem MenuItem_ColonneRepasSamedi;

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Propriétés de dépendances

        public ObservableCollection<Releve_Heure_Atelier> listReleve
        {
            get { return (ObservableCollection<Releve_Heure_Atelier>)GetValue(listReleveProperty); }
            set { SetValue(listReleveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listReleve.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listReleveProperty =
            DependencyProperty.Register("listReleve", typeof(ObservableCollection<Releve_Heure_Atelier>), typeof(ListeReveleHeureAtelierControl), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public ListeReveleHeureAtelierControl()
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
            List<string> listSalarie = new List<string>();
            List<string> listAffaire = new List<string>();
            List<string> listRegie = new List<string>();

            foreach (Releve_Heure_Atelier item in ((App)App.Current).mySitaffEntities.Releve_Heure_Atelier)
            {

                //Pour remplir les salariés
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

                //Pour remplir les affaires et les régies
                foreach (Heure_Atelier hr in item.Heure_Atelier)
                {
                    if (hr.Affaire1 != null)
                    {
                        if (!listAffaire.Contains(hr.Affaire1.Numero))
                        {
                            listAffaire.Add(hr.Affaire1.Numero);
                        }
                    }

                    if (hr.Regie1 != null)
                    {
                        if (!listRegie.Contains(hr.Regie1.Numero))
                        {
                            listRegie.Add(hr.Regie1.Numero);
                        }
                    }
                }

            }

            this._filterContainSalarie.ItemsSource = listSalarie;
            this._filterContainAffaire.ItemsSource = listAffaire;
            this._filterContainRegie.ItemsSource = listRegie;
        }

        #endregion

        #region initialisation Donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<Releve_Heure_Atelier> listToPut)
        {
            if (listToPut == null)
            {
                this.listReleve = new ObservableCollection<Releve_Heure_Atelier>(((App)App.Current).mySitaffEntities.Releve_Heure_Atelier.OrderByDescending(rha => rha.Date_Debut).ThenBy(rha => rha.Salarie1.Personne.Nom));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listReleve = new ObservableCollection<Releve_Heure_Atelier>(listToPut);
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

            MenuItem itemAfficher7 = new MenuItem();
            itemAfficher7.Header = "Afficher relevé d'activité / salarié";
            itemAfficher7.Click += new RoutedEventHandler(delegate { this.menuRapportReleveParSalarie(); });   

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
            if (securite.VerificationDroitActionsCRUD(this.ToString(), "Add"))
            {
                contextMenu.Items.Add(new Separator());
                contextMenu.Items.Add(itemAfficher7);
            }
            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(itemAfficher8);
        }

        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {
            this.MenuItem_ColonneSalarie = new MenuItem();
            this.MenuItem_ColonneSalarie.IsChecked = false;
            this.MenuItem_ColonneSalarie.Header = "Salarié";
            this.MenuItem_ColonneSalarie.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneSalarie(); });
            this.AffMas_ColonneSalarie();
            menuItem.Items.Add(this.MenuItem_ColonneSalarie);

            this.MenuItem_ColonneNumeroSemaine = new MenuItem();
            this.MenuItem_ColonneNumeroSemaine.IsChecked = false;
            this.MenuItem_ColonneNumeroSemaine.Header = "Numéro de semaine";
            this.MenuItem_ColonneNumeroSemaine.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumeroSemaine(); });
            this.AffMas_ColonneNumeroSemaine();
            menuItem.Items.Add(this.MenuItem_ColonneNumeroSemaine);

            this.MenuItem_ColonneDateDebut = new MenuItem();
            this.MenuItem_ColonneDateDebut.IsChecked = false;
            this.MenuItem_ColonneDateDebut.Header = "Date Début";
            this.MenuItem_ColonneDateDebut.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateDebut(); });
            this.AffMas_ColonneDateDebut();
            menuItem.Items.Add(this.MenuItem_ColonneDateDebut);

            this.MenuItem_ColonneTotalSemaine = new MenuItem();
            this.MenuItem_ColonneTotalSemaine.IsChecked = false;
            this.MenuItem_ColonneTotalSemaine.Header = "Nombre d'heures";
            this.MenuItem_ColonneTotalSemaine.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTotalSemaine(); });
            this.AffMas_ColonneTotalSemaine();
            menuItem.Items.Add(this.MenuItem_ColonneTotalSemaine);

            this.MenuItem_ColonneHeureLundi = new MenuItem();
            this.MenuItem_ColonneHeureLundi.IsChecked = true;
            this.MenuItem_ColonneHeureLundi.Header = "Heures Lundi";
            this.MenuItem_ColonneHeureLundi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneHeureLundi(); });
            this.AffMas_ColonneHeureLundi();
            menuItem.Items.Add(this.MenuItem_ColonneHeureLundi);

            this.MenuItem_ColonneHeureMardi = new MenuItem();
            this.MenuItem_ColonneHeureMardi.IsChecked = true;
            this.MenuItem_ColonneHeureMardi.Header = "Heures Mardi";
            this.MenuItem_ColonneHeureMardi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneHeureMardi(); });
            this.AffMas_ColonneHeureMardi();
            menuItem.Items.Add(this.MenuItem_ColonneHeureMardi);

            this.MenuItem_ColonneHeureMercredi = new MenuItem();
            this.MenuItem_ColonneHeureMercredi.IsChecked = true;
            this.MenuItem_ColonneHeureMercredi.Header = "Heures Mercredi";
            this.MenuItem_ColonneHeureMercredi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneHeureMercredi(); });
            this.AffMas_ColonneHeureMercredi();
            menuItem.Items.Add(this.MenuItem_ColonneHeureMercredi);

            this.MenuItem_ColonneHeureJeudi = new MenuItem();
            this.MenuItem_ColonneHeureJeudi.IsChecked = true;
            this.MenuItem_ColonneHeureJeudi.Header = "Heures Jeudi";
            this.MenuItem_ColonneHeureJeudi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneHeureJeudi(); });
            this.AffMas_ColonneHeureJeudi();
            menuItem.Items.Add(this.MenuItem_ColonneHeureJeudi);

            this.MenuItem_ColonneHeureVendredi = new MenuItem();
            this.MenuItem_ColonneHeureVendredi.IsChecked = true;
            this.MenuItem_ColonneHeureVendredi.Header = "Heures Vendredi";
            this.MenuItem_ColonneHeureVendredi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneHeureVendredi(); });
            this.AffMas_ColonneHeureVendredi();
            menuItem.Items.Add(this.MenuItem_ColonneHeureVendredi);

            this.MenuItem_ColonneHeureSamedi = new MenuItem();
            this.MenuItem_ColonneHeureSamedi.IsChecked = true;
            this.MenuItem_ColonneHeureSamedi.Header = "Heures Samedi";
            this.MenuItem_ColonneHeureSamedi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneHeureSamedi(); });
            this.AffMas_ColonneHeureSamedi();
            menuItem.Items.Add(this.MenuItem_ColonneHeureSamedi);

            this.MenuItem_ColonneObservationLundi = new MenuItem();
            this.MenuItem_ColonneObservationLundi.IsChecked = true;
            this.MenuItem_ColonneObservationLundi.Header = "Observation Lundi";
            this.MenuItem_ColonneObservationLundi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneObservationLundi(); });
            this.AffMas_ColonneObservationLundi();
            menuItem.Items.Add(this.MenuItem_ColonneObservationLundi);

            this.MenuItem_ColonneObservationMardi = new MenuItem();
            this.MenuItem_ColonneObservationMardi.IsChecked = true;
            this.MenuItem_ColonneObservationMardi.Header = "Observation Mardi";
            this.MenuItem_ColonneObservationMardi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneObservationMardi(); });
            this.AffMas_ColonneObservationMardi();
            menuItem.Items.Add(this.MenuItem_ColonneObservationMardi);

            this.MenuItem_ColonneObservationMercredi = new MenuItem();
            this.MenuItem_ColonneObservationMercredi.IsChecked = true;
            this.MenuItem_ColonneObservationMercredi.Header = "Observation Mercredi";
            this.MenuItem_ColonneObservationMercredi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneObservationMercredi(); });
            this.AffMas_ColonneObservationMercredi();
            menuItem.Items.Add(this.MenuItem_ColonneObservationMercredi);

            this.MenuItem_ColonneObservationJeudi = new MenuItem();
            this.MenuItem_ColonneObservationJeudi.IsChecked = true;
            this.MenuItem_ColonneObservationJeudi.Header = "Observation Jeudi";
            this.MenuItem_ColonneObservationJeudi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneObservationJeudi(); });
            this.AffMas_ColonneObservationJeudi();
            menuItem.Items.Add(this.MenuItem_ColonneObservationJeudi);

            this.MenuItem_ColonneObservationVendredi = new MenuItem();
            this.MenuItem_ColonneObservationVendredi.IsChecked = true;
            this.MenuItem_ColonneObservationVendredi.Header = "Observation Vendredi";
            this.MenuItem_ColonneObservationVendredi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneObservationVendredi(); });
            this.AffMas_ColonneObservationVendredi();
            menuItem.Items.Add(this.MenuItem_ColonneObservationVendredi);

            this.MenuItem_ColonneObservationSamedi = new MenuItem();
            this.MenuItem_ColonneObservationSamedi.IsChecked = true;
            this.MenuItem_ColonneObservationSamedi.Header = "Observation Samedi";
            this.MenuItem_ColonneObservationSamedi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneObservationSamedi(); });
            this.AffMas_ColonneObservationSamedi();
            menuItem.Items.Add(this.MenuItem_ColonneObservationSamedi);

            this.MenuItem_ColonneRouteLundi = new MenuItem();
            this.MenuItem_ColonneRouteLundi.IsChecked = true;
            this.MenuItem_ColonneRouteLundi.Header = "Route Lundi";
            this.MenuItem_ColonneRouteLundi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneRouteLundi(); });
            this.AffMas_ColonneRouteLundi();
            menuItem.Items.Add(this.MenuItem_ColonneRouteLundi);

            this.MenuItem_ColonneRouteMardi = new MenuItem();
            this.MenuItem_ColonneRouteMardi.IsChecked = true;
            this.MenuItem_ColonneRouteMardi.Header = "Route Mardi";
            this.MenuItem_ColonneRouteMardi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneRouteMardi(); });
            this.AffMas_ColonneRouteMardi();
            menuItem.Items.Add(this.MenuItem_ColonneRouteMardi);

            this.MenuItem_ColonneRouteMercredi = new MenuItem();
            this.MenuItem_ColonneRouteMercredi.IsChecked = true;
            this.MenuItem_ColonneRouteMercredi.Header = "Route Mercredi";
            this.MenuItem_ColonneRouteMercredi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneRouteMercredi(); });
            this.AffMas_ColonneRouteMercredi();
            menuItem.Items.Add(this.MenuItem_ColonneRouteMercredi);

            this.MenuItem_ColonneRouteJeudi = new MenuItem();
            this.MenuItem_ColonneRouteJeudi.IsChecked = true;
            this.MenuItem_ColonneRouteJeudi.Header = "Route Jeudi";
            this.MenuItem_ColonneRouteJeudi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneRouteJeudi(); });
            this.AffMas_ColonneRouteJeudi();
            menuItem.Items.Add(this.MenuItem_ColonneRouteJeudi);

            this.MenuItem_ColonneRouteVendredi = new MenuItem();
            this.MenuItem_ColonneRouteVendredi.IsChecked = true;
            this.MenuItem_ColonneRouteVendredi.Header = "Route Vendredi";
            this.MenuItem_ColonneRouteVendredi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneRouteVendredi(); });
            this.AffMas_ColonneRouteVendredi();
            menuItem.Items.Add(this.MenuItem_ColonneRouteVendredi);

            this.MenuItem_ColonneRouteSamedi = new MenuItem();
            this.MenuItem_ColonneRouteSamedi.IsChecked = true;
            this.MenuItem_ColonneRouteSamedi.Header = "Route Samedi";
            this.MenuItem_ColonneRouteSamedi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneRouteSamedi(); });
            this.AffMas_ColonneRouteSamedi();
            menuItem.Items.Add(this.MenuItem_ColonneRouteSamedi);

            this.MenuItem_ColonneRepasLundi = new MenuItem();
            this.MenuItem_ColonneRepasLundi.IsChecked = true;
            this.MenuItem_ColonneRepasLundi.Header = "Repas Lundi";
            this.MenuItem_ColonneRepasLundi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneRepasLundi(); });
            this.AffMas_ColonneRepasLundi();
            menuItem.Items.Add(this.MenuItem_ColonneRepasLundi);

            this.MenuItem_ColonneRepasMardi = new MenuItem();
            this.MenuItem_ColonneRepasMardi.IsChecked = true;
            this.MenuItem_ColonneRepasMardi.Header = "Repas Mardi";
            this.MenuItem_ColonneRepasMardi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneRepasMardi(); });
            this.AffMas_ColonneRepasMardi();
            menuItem.Items.Add(this.MenuItem_ColonneRepasMardi);

            this.MenuItem_ColonneRepasMercredi = new MenuItem();
            this.MenuItem_ColonneRepasMercredi.IsChecked = true;
            this.MenuItem_ColonneRepasMercredi.Header = "Repas Mercredi";
            this.MenuItem_ColonneRepasMercredi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneRepasMercredi(); });
            this.AffMas_ColonneRepasMercredi();
            menuItem.Items.Add(this.MenuItem_ColonneRepasMercredi);

            this.MenuItem_ColonneRepasJeudi = new MenuItem();
            this.MenuItem_ColonneRepasJeudi.IsChecked = true;
            this.MenuItem_ColonneRepasJeudi.Header = "Repas Jeudi";
            this.MenuItem_ColonneRepasJeudi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneRepasJeudi(); });
            this.AffMas_ColonneRepasJeudi();
            menuItem.Items.Add(this.MenuItem_ColonneRepasJeudi);

            this.MenuItem_ColonneRepasVendredi = new MenuItem();
            this.MenuItem_ColonneRepasVendredi.IsChecked = true;
            this.MenuItem_ColonneRepasVendredi.Header = "Repas Vendredi";
            this.MenuItem_ColonneRepasVendredi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneRepasVendredi(); });
            this.AffMas_ColonneRepasVendredi();
            menuItem.Items.Add(this.MenuItem_ColonneRepasVendredi);

            this.MenuItem_ColonneRepasSamedi = new MenuItem();
            this.MenuItem_ColonneRepasSamedi.IsChecked = true;
            this.MenuItem_ColonneRepasSamedi.Header = "Repas Samedi";
            this.MenuItem_ColonneRepasSamedi.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneRepasSamedi(); });
            this.AffMas_ColonneRepasSamedi();
            menuItem.Items.Add(this.MenuItem_ColonneRepasSamedi);

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

        private void menuRapportReleveParSalarie()
        {
            ((App)App.Current)._theMainWindow._CommandRapportReleveActiviteParSalarie.Command.Execute(((App)App.Current)._theMainWindow);
        }

        #region Afficher / Masquer

        #region Tout

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneSalarie.IsChecked = false;
            this.MenuItem_ColonneNumeroSemaine.IsChecked = false;
            this.MenuItem_ColonneDateDebut.IsChecked = false;
            this.MenuItem_ColonneTotalSemaine.IsChecked = false;
            this.MenuItem_ColonneHeureLundi.IsChecked = false;
            this.MenuItem_ColonneHeureMardi.IsChecked = false;
            this.MenuItem_ColonneHeureMercredi.IsChecked = false;
            this.MenuItem_ColonneHeureJeudi.IsChecked = false;
            this.MenuItem_ColonneHeureVendredi.IsChecked = false;
            this.MenuItem_ColonneHeureSamedi.IsChecked = false;
            this.MenuItem_ColonneObservationLundi.IsChecked = false;
            this.MenuItem_ColonneObservationMardi.IsChecked = false;
            this.MenuItem_ColonneObservationMercredi.IsChecked = false;
            this.MenuItem_ColonneObservationJeudi.IsChecked = false;
            this.MenuItem_ColonneObservationVendredi.IsChecked = false;
            this.MenuItem_ColonneObservationSamedi.IsChecked = false;
            this.MenuItem_ColonneRouteLundi.IsChecked = false;
            this.MenuItem_ColonneRouteMardi.IsChecked = false;
            this.MenuItem_ColonneRouteMercredi.IsChecked = false;
            this.MenuItem_ColonneRouteJeudi.IsChecked = false;
            this.MenuItem_ColonneRouteVendredi.IsChecked = false;
            this.MenuItem_ColonneRouteSamedi.IsChecked = false;
            this.MenuItem_ColonneRepasLundi.IsChecked = false;
            this.MenuItem_ColonneRepasMardi.IsChecked = false;
            this.MenuItem_ColonneRepasMercredi.IsChecked = false;
            this.MenuItem_ColonneRepasJeudi.IsChecked = false;
            this.MenuItem_ColonneRepasVendredi.IsChecked = false;
            this.MenuItem_ColonneRepasSamedi.IsChecked = false;

            this.AffMas_ColonneSalarie();
            this.AffMas_ColonneNumeroSemaine();
            this.AffMas_ColonneDateDebut();
            this.AffMas_ColonneTotalSemaine();
            this.AffMas_ColonneHeureLundi();
            this.AffMas_ColonneHeureMardi();
            this.AffMas_ColonneHeureMercredi();
            this.AffMas_ColonneHeureJeudi();
            this.AffMas_ColonneHeureVendredi();
            this.AffMas_ColonneHeureSamedi();
            this.AffMas_ColonneObservationLundi();
            this.AffMas_ColonneObservationMardi();
            this.AffMas_ColonneObservationMercredi();
            this.AffMas_ColonneObservationJeudi();
            this.AffMas_ColonneObservationVendredi();
            this.AffMas_ColonneObservationSamedi();
            this.AffMas_ColonneRouteLundi();
            this.AffMas_ColonneRouteMardi();
            this.AffMas_ColonneRouteMercredi();
            this.AffMas_ColonneRouteJeudi();
            this.AffMas_ColonneRouteVendredi();
            this.AffMas_ColonneRouteSamedi();
            this.AffMas_ColonneRepasLundi();
            this.AffMas_ColonneRepasMardi();
            this.AffMas_ColonneRepasMercredi();
            this.AffMas_ColonneRepasJeudi();
            this.AffMas_ColonneRepasVendredi();
            this.AffMas_ColonneRepasSamedi();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneSalarie.IsChecked = true;
            this.MenuItem_ColonneNumeroSemaine.IsChecked = true;
            this.MenuItem_ColonneDateDebut.IsChecked = true;
            this.MenuItem_ColonneTotalSemaine.IsChecked = true;
            this.MenuItem_ColonneHeureLundi.IsChecked = true;
            this.MenuItem_ColonneHeureMardi.IsChecked = true;
            this.MenuItem_ColonneHeureMercredi.IsChecked = true;
            this.MenuItem_ColonneHeureJeudi.IsChecked = true;
            this.MenuItem_ColonneHeureVendredi.IsChecked = true;
            this.MenuItem_ColonneHeureSamedi.IsChecked = true;
            this.MenuItem_ColonneObservationLundi.IsChecked = true;
            this.MenuItem_ColonneObservationMardi.IsChecked = true;
            this.MenuItem_ColonneObservationMercredi.IsChecked = true;
            this.MenuItem_ColonneObservationJeudi.IsChecked = true;
            this.MenuItem_ColonneObservationVendredi.IsChecked = true;
            this.MenuItem_ColonneObservationSamedi.IsChecked = true;
            this.MenuItem_ColonneRouteLundi.IsChecked = true;
            this.MenuItem_ColonneRouteMardi.IsChecked = true;
            this.MenuItem_ColonneRouteMercredi.IsChecked = true;
            this.MenuItem_ColonneRouteJeudi.IsChecked = true;
            this.MenuItem_ColonneRouteVendredi.IsChecked = true;
            this.MenuItem_ColonneRouteSamedi.IsChecked = true;
            this.MenuItem_ColonneRepasLundi.IsChecked = true;
            this.MenuItem_ColonneRepasMardi.IsChecked = true;
            this.MenuItem_ColonneRepasMercredi.IsChecked = true;
            this.MenuItem_ColonneRepasJeudi.IsChecked = true;
            this.MenuItem_ColonneRepasVendredi.IsChecked = true;
            this.MenuItem_ColonneRepasSamedi.IsChecked = true;

            this.AffMas_ColonneSalarie();
            this.AffMas_ColonneNumeroSemaine();
            this.AffMas_ColonneDateDebut();
            this.AffMas_ColonneTotalSemaine();
            this.AffMas_ColonneHeureLundi();
            this.AffMas_ColonneHeureMardi();
            this.AffMas_ColonneHeureMercredi();
            this.AffMas_ColonneHeureJeudi();
            this.AffMas_ColonneHeureVendredi();
            this.AffMas_ColonneHeureSamedi();
            this.AffMas_ColonneObservationLundi();
            this.AffMas_ColonneObservationMardi();
            this.AffMas_ColonneObservationMercredi();
            this.AffMas_ColonneObservationJeudi();
            this.AffMas_ColonneObservationVendredi();
            this.AffMas_ColonneObservationSamedi();
            this.AffMas_ColonneRouteLundi();
            this.AffMas_ColonneRouteMardi();
            this.AffMas_ColonneRouteMercredi();
            this.AffMas_ColonneRouteJeudi();
            this.AffMas_ColonneRouteVendredi();
            this.AffMas_ColonneRouteSamedi();
            this.AffMas_ColonneRepasLundi();
            this.AffMas_ColonneRepasMardi();
            this.AffMas_ColonneRepasMercredi();
            this.AffMas_ColonneRepasJeudi();
            this.AffMas_ColonneRepasVendredi();
            this.AffMas_ColonneRepasSamedi();
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

        private void AffMas_ColonneNumeroSemaine()
        {
            if (this.MenuItem_ColonneNumeroSemaine.IsChecked == true)
            {
                this._ColonneNumeroSemaine.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNumeroSemaine.IsChecked = false;
            }
            else
            {
                this._ColonneNumeroSemaine.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNumeroSemaine.IsChecked = true;
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

        private void AffMas_ColonneTotalSemaine()
        {
            if (this.MenuItem_ColonneTotalSemaine.IsChecked == true)
            {
                this._ColonneTotalSemaine.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTotalSemaine.IsChecked = false;
            }
            else
            {
                this._ColonneTotalSemaine.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTotalSemaine.IsChecked = true;
            }
        }

        private void AffMas_ColonneHeureLundi()
        {
            if (this.MenuItem_ColonneHeureLundi.IsChecked == true)
            {
                this._ColonneHeureLundi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneHeureLundi.IsChecked = false;
            }
            else
            {
                this._ColonneHeureLundi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneHeureLundi.IsChecked = true;
            }
        }

        private void AffMas_ColonneHeureMardi()
        {
            if (this.MenuItem_ColonneHeureMardi.IsChecked == true)
            {
                this._ColonneHeureMardi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneHeureMardi.IsChecked = false;
            }
            else
            {
                this._ColonneHeureMardi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneHeureMardi.IsChecked = true;
            }
        }

        private void AffMas_ColonneHeureMercredi()
        {
            if (this.MenuItem_ColonneHeureMercredi.IsChecked == true)
            {
                this._ColonneHeureMercredi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneHeureMercredi.IsChecked = false;
            }
            else
            {
                this._ColonneHeureMercredi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneHeureMercredi.IsChecked = true;
            }
        }

        private void AffMas_ColonneHeureJeudi()
        {
            if (this.MenuItem_ColonneHeureJeudi.IsChecked == true)
            {
                this._ColonneHeureJeudi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneHeureJeudi.IsChecked = false;
            }
            else
            {
                this._ColonneHeureJeudi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneHeureJeudi.IsChecked = true;
            }
        }

        private void AffMas_ColonneHeureVendredi()
        {
            if (this.MenuItem_ColonneHeureVendredi.IsChecked == true)
            {
                this._ColonneHeureVendredi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneHeureVendredi.IsChecked = false;
            }
            else
            {
                this._ColonneHeureVendredi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneHeureVendredi.IsChecked = true;
            }
        }

        private void AffMas_ColonneHeureSamedi()
        {
            if (this.MenuItem_ColonneHeureSamedi.IsChecked == true)
            {
                this._ColonneHeureSamedi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneHeureSamedi.IsChecked = false;
            }
            else
            {
                this._ColonneHeureSamedi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneHeureSamedi.IsChecked = true;
            }
        }

        private void AffMas_ColonneObservationLundi()
        {
            if (this.MenuItem_ColonneObservationLundi.IsChecked == true)
            {
                this._ColonneObservationLundi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneObservationLundi.IsChecked = false;
            }
            else
            {
                this._ColonneObservationLundi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneObservationLundi.IsChecked = true;
            }
        }

        private void AffMas_ColonneObservationMardi()
        {
            if (this.MenuItem_ColonneObservationMardi.IsChecked == true)
            {
                this._ColonneObservationMardi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneObservationMardi.IsChecked = false;
            }
            else
            {
                this._ColonneObservationMardi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneObservationMardi.IsChecked = true;
            }
        }

        private void AffMas_ColonneObservationMercredi()
        {
            if (this.MenuItem_ColonneObservationMercredi.IsChecked == true)
            {
                this._ColonneObservationMercredi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneObservationMercredi.IsChecked = false;
            }
            else
            {
                this._ColonneObservationMercredi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneObservationMercredi.IsChecked = true;
            }
        }

        private void AffMas_ColonneObservationJeudi()
        {
            if (this.MenuItem_ColonneObservationJeudi.IsChecked == true)
            {
                this._ColonneObservationJeudi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneObservationJeudi.IsChecked = false;
            }
            else
            {
                this._ColonneObservationJeudi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneObservationJeudi.IsChecked = true;
            }
        }

        private void AffMas_ColonneObservationVendredi()
        {
            if (this.MenuItem_ColonneObservationVendredi.IsChecked == true)
            {
                this._ColonneObservationVendredi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneObservationVendredi.IsChecked = false;
            }
            else
            {
                this._ColonneObservationVendredi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneObservationVendredi.IsChecked = true;
            }
        }

        private void AffMas_ColonneObservationSamedi()
        {
            if (this.MenuItem_ColonneObservationSamedi.IsChecked == true)
            {
                this._ColonneObservationSamedi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneObservationSamedi.IsChecked = false;
            }
            else
            {
                this._ColonneObservationSamedi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneObservationSamedi.IsChecked = true;
            }
        }

        private void AffMas_ColonneRouteLundi()
        {
            if (this.MenuItem_ColonneRouteLundi.IsChecked == true)
            {
                this._ColonneRouteLundi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneRouteLundi.IsChecked = false;
            }
            else
            {
                this._ColonneRouteLundi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneRouteLundi.IsChecked = true;
            }
        }

        private void AffMas_ColonneRouteMardi()
        {
            if (this.MenuItem_ColonneRouteMardi.IsChecked == true)
            {
                this._ColonneRouteMardi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneRouteMardi.IsChecked = false;
            }
            else
            {
                this._ColonneRouteMardi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneRouteMardi.IsChecked = true;
            }
        }

        private void AffMas_ColonneRouteMercredi()
        {
            if (this.MenuItem_ColonneRouteMercredi.IsChecked == true)
            {
                this._ColonneRouteMercredi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneRouteMercredi.IsChecked = false;
            }
            else
            {
                this._ColonneRouteMercredi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneRouteMercredi.IsChecked = true;
            }
        }

        private void AffMas_ColonneRouteJeudi()
        {
            if (this.MenuItem_ColonneRouteJeudi.IsChecked == true)
            {
                this._ColonneRouteJeudi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneRouteJeudi.IsChecked = false;
            }
            else
            {
                this._ColonneRouteJeudi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneRouteJeudi.IsChecked = true;
            }
        }

        private void AffMas_ColonneRouteVendredi()
        {
            if (this.MenuItem_ColonneRouteVendredi.IsChecked == true)
            {
                this._ColonneRouteVendredi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneRouteVendredi.IsChecked = false;
            }
            else
            {
                this._ColonneRouteVendredi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneRouteVendredi.IsChecked = true;
            }
        }

        private void AffMas_ColonneRouteSamedi()
        {
            if (this.MenuItem_ColonneRouteSamedi.IsChecked == true)
            {
                this._ColonneRouteSamedi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneRouteSamedi.IsChecked = false;
            }
            else
            {
                this._ColonneRouteSamedi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneRouteSamedi.IsChecked = true;
            }
        }

        private void AffMas_ColonneRepasLundi()
        {
            if (this.MenuItem_ColonneRepasLundi.IsChecked == true)
            {
                this._ColonneRepasLundi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneRepasLundi.IsChecked = false;
            }
            else
            {
                this._ColonneRepasLundi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneRepasLundi.IsChecked = true;
            }
        }

        private void AffMas_ColonneRepasMardi()
        {
            if (this.MenuItem_ColonneRepasMardi.IsChecked == true)
            {
                this._ColonneRepasMardi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneRepasMardi.IsChecked = false;
            }
            else
            {
                this._ColonneRepasMardi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneRepasMardi.IsChecked = true;
            }
        }

        private void AffMas_ColonneRepasMercredi()
        {
            if (this.MenuItem_ColonneRepasMercredi.IsChecked == true)
            {
                this._ColonneRepasMercredi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneRepasMercredi.IsChecked = false;
            }
            else
            {
                this._ColonneRepasMercredi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneRepasMercredi.IsChecked = true;
            }
        }

        private void AffMas_ColonneRepasJeudi()
        {
            if (this.MenuItem_ColonneRepasJeudi.IsChecked == true)
            {
                this._ColonneRepasJeudi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneRepasJeudi.IsChecked = false;
            }
            else
            {
                this._ColonneRepasJeudi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneRepasJeudi.IsChecked = true;
            }
        }

        private void AffMas_ColonneRepasVendredi()
        {
            if (this.MenuItem_ColonneRepasVendredi.IsChecked == true)
            {
                this._ColonneRepasVendredi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneRepasVendredi.IsChecked = false;
            }
            else
            {
                this._ColonneRepasVendredi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneRepasVendredi.IsChecked = true;
            }
        }

        private void AffMas_ColonneRepasSamedi()
        {
            if (this.MenuItem_ColonneRepasSamedi.IsChecked == true)
            {
                this._ColonneRepasSamedi.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneRepasSamedi.IsChecked = false;
            }
            else
            {
                this._ColonneRepasSamedi.Visibility = Visibility.Visible;
                this.MenuItem_ColonneRepasSamedi.IsChecked = true;
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

        #region date

        private void _filterContainDateDebutSemaine_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (this._filterContainDateDebutSemaine.SelectedDate != null)
            {
                while (((DateTime)this._filterContainDateDebutSemaine.SelectedDate).DayOfWeek != DayOfWeek.Monday)
                {
                    this._filterContainDateDebutSemaine.SelectedDate = ((DateTime)this._filterContainDateDebutSemaine.SelectedDate).AddDays(-1);
                }
            }
        }

        #endregion

        #region datagrid

        private void _DataGrid_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((DataGrid)sender).RowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridColor;
            ((DataGrid)sender).AlternatingRowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridAlternateColor;
        }

        #endregion

        #endregion

        #region CRUD (Create Read Update Delete)

        /// <summary>
        /// Ajoute un nouveau relevé à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Releve_Heure_Atelier Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un relevé d'heures atelier en cours ...");

            //Initialisation de la fenêtre
            ReleveHeureAtelierWindow releveHeureAtelierWindow = new ReleveHeureAtelierWindow();

            //Création de l'objet temporaire
            //Mise de l'objet temporaire dans le datacontext
            Releve_Heure_Atelier tmp = new Releve_Heure_Atelier();
            releveHeureAtelierWindow.DataContext = tmp;

            //Options particulières de la fenêtre
            releveHeureAtelierWindow.creation = true;
            releveHeureAtelierWindow.VerrouillerLaFenetre();

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = releveHeureAtelierWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet se trouvant dans le datacontext de la fenêtre
                return (Releve_Heure_Atelier)releveHeureAtelierWindow.DataContext;
            }
            else
            {
                try
                {
                    //On détache tous les élements liés au relevé Heure_Atelier
                    ObservableCollection<Heure_Atelier> toRemove = new ObservableCollection<Heure_Atelier>();
                    foreach (Heure_Atelier item in ((Releve_Heure_Atelier)releveHeureAtelierWindow.DataContext).Heure_Atelier)
                    {
                        toRemove.Add(item);
                    }
                    foreach (Heure_Atelier item in toRemove)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    //On détache tous les élements liés au relevé Heure_Atelier_Autre
                    ObservableCollection<Heure_Atelier_Autre> toRemove1 = new ObservableCollection<Heure_Atelier_Autre>();
                    foreach (Heure_Atelier_Autre item in ((Releve_Heure_Atelier)releveHeureAtelierWindow.DataContext).Heure_Atelier_Autre)
                    {
                        toRemove1.Add(item);
                    }
                    foreach (Heure_Atelier_Autre item in toRemove1)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    //On détache le relevé
                    ((App)App.Current).mySitaffEntities.Detach((Releve_Heure_Atelier)releveHeureAtelierWindow.DataContext);
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un relevé d'heures atelier annulé : " + this.listReleve.Count() + " / " + this.max);

                return null;
            }
        }


        /// <summary>
        /// Ouvre le relevé séléctionné à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Releve_Heure_Atelier Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un releve d'heures atelier en cours ...");

                    //Création de la fenêtre
                    ReleveHeureAtelierWindow releveHeureAtelierWindow = new ReleveHeureAtelierWindow();

                    //Initialisation du Datacontext en relevé heure atelier et association au relevé sélectionnée
                    releveHeureAtelierWindow.DataContext = new Releve_Heure_Atelier();
                    releveHeureAtelierWindow.DataContext = this._DataGridMain.SelectedItem;

                    //Mise en place des options particulières
                    releveHeureAtelierWindow.creation = true;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = releveHeureAtelierWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet se trouvant dans le datacontext de la fenêtre
                        return (Releve_Heure_Atelier)releveHeureAtelierWindow.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Releve_Heure_Atelier)(this._DataGridMain.SelectedItem));
                        //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un relevé d'heures atelier annulée : " + this.listReleve.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul relevé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un relevé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime le relevé séléctionné avec une confirmation
        /// </summary>
        public Releve_Heure_Atelier Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un relevé d'heures atelier en cours ...");

                    if (MessageBox.Show("Voulez-vous rééllement supprimer le relevé séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //On détache tous les élements liés au relevé Heure_Atelier
                        ObservableCollection<Heure_Atelier> toRemove = new ObservableCollection<Heure_Atelier>();
                        foreach (Heure_Atelier item in ((Releve_Heure_Atelier)this._DataGridMain.SelectedItem).Heure_Atelier)
                        {
                            toRemove.Add(item);
                        }
                        foreach (Heure_Atelier item in toRemove)
                        {
                            ((Releve_Heure_Atelier)this._DataGridMain.SelectedItem).Heure_Atelier.Remove(item);
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Heure_Atelier.DeleteObject(item);
                            }
                            catch (Exception) { }
                        }

                        //On détache tous les élements liés au relevé Heure_Atelier_Autre
                        ObservableCollection<Heure_Atelier_Autre> toRemove2 = new ObservableCollection<Heure_Atelier_Autre>();
                        foreach (Heure_Atelier_Autre item in ((Releve_Heure_Atelier)this._DataGridMain.SelectedItem).Heure_Atelier_Autre)
                        {
                            toRemove2.Add(item);
                        }
                        foreach (Heure_Atelier_Autre item in toRemove2)
                        {
                            ((Releve_Heure_Atelier)this._DataGridMain.SelectedItem).Heure_Atelier_Autre.Remove(item);
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Heure_Atelier_Autre.DeleteObject(item);
                            }
                            catch (Exception) { }
                        }

                        //Supprimer l'élément 
                        return (Releve_Heure_Atelier)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un relevé d'heures atelier annulée : " + this.listReleve.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul relevé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un relevé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre le relevé séléctionné en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Releve_Heure_Atelier Look()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un relevé d'heures atelier en cours ...");

                    //Création de la fenêtre
                    ReleveHeureAtelierWindow releveHeureAtelierWindow = new ReleveHeureAtelierWindow();
                    
                    //Initialisation du Datacontext et association à l'objet sélectionnée
                    releveHeureAtelierWindow.DataContext = new Releve_Heure_Atelier();
                    releveHeureAtelierWindow.DataContext = this._DataGridMain.SelectedItem;

                    //Je positionne la lecture seule sur la fenêtre
                    releveHeureAtelierWindow.lectureSeule();

                    //J'affiche la fenêtre
                    bool? dialogResult = releveHeureAtelierWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un relevé d'heures atelier terminé : " + this.listReleve.Count() + " / " + this.max);

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul relevé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un relevé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre le rapport séléctionné
        /// </summary>
        public void RapportReleveActiviteParSalarie()
        {
            ReportingWindow reportingWindow = new ReportingWindow();
            reportingWindow._webBrowser.Source = new Uri("http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fHEURES%2freleve+activite&rs:Command=Render");
            reportingWindow.Title = "Rapport pour relevé d'activité";

            reportingWindow.Show();
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
            this._filterContainSalarie.Text = "";
            this._filterContainNumeroSemaine.Text = "";
            this._filterContainRegie.Text = "";
            this._filterContainAffaire.Text = "";

            //Dates
            this._filterContainDateDebutSemaine.SelectedDate = null;

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

            ObservableCollection<Releve_Heure_Atelier> listToPut = new ObservableCollection<Releve_Heure_Atelier>(((App)App.Current).mySitaffEntities.Releve_Heure_Atelier.OrderByDescending(rha => rha.Date_Debut).ThenBy(rha => rha.Salarie1.Personne.Nom));

            if (this._filterContainDateDebutSemaine.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Releve_Heure_Atelier>(listToPut.Where(com => com.Date_Debut != null));
                listToPut = new ObservableCollection<Releve_Heure_Atelier>(listToPut.Where(com => com.Date_Debut == this._filterContainDateDebutSemaine.SelectedDate));
            }
            if (this._filterContainSalarie.Text != "")
            {
                listToPut = new ObservableCollection<Releve_Heure_Atelier>(listToPut.Where(com => com.Salarie1 != null));
                listToPut = new ObservableCollection<Releve_Heure_Atelier>(listToPut.Where(com => com.Salarie1.Personne != null));
                listToPut = new ObservableCollection<Releve_Heure_Atelier>(listToPut.Where(com => com.Salarie1.Personne.fullname.Trim().ToLower().Contains(this._filterContainSalarie.Text.Trim().ToLower()) || com.Salarie1.Personne.Initiales.Trim().ToLower().Contains(this._filterContainSalarie.Text.Trim().ToLower())));
            }
            if (this._filterContainNumeroSemaine.Text != "")
            {
                int val;
                if (int.TryParse(this._filterContainNumeroSemaine.Text.Trim(), out val))
                {
                    listToPut = new ObservableCollection<Releve_Heure_Atelier>(listToPut.Where(com => com.NumeroSemaine != null));
                    listToPut = new ObservableCollection<Releve_Heure_Atelier>(listToPut.Where(com => com.NumeroSemaine.ToString().Contains(double.Parse(this._filterContainNumeroSemaine.Text.Trim()).ToString())));
                }
            }
            if (this._filterContainRegie.Text != "")
            {
                ObservableCollection<Releve_Heure_Atelier> toRemove = new ObservableCollection<Releve_Heure_Atelier>();
                foreach (Releve_Heure_Atelier item in listToPut)
                {
                    bool test = false;
                    if (!test)
                    {
                        foreach (Heure_Atelier tsr in item.Heure_Atelier)
                        {
                            if (tsr.Regie1 != null)
                            {
                                if (tsr.Regie1.Numero.Trim().ToLower().Contains(this._filterContainRegie.Text.Trim().ToLower()))
                                {
                                    test = true;
                                }
                            }
                        }
                    }
                    if (!test)
                    {
                        toRemove.Add(item);
                    }
                }
                foreach (Releve_Heure_Atelier item in toRemove)
                {
                    listToPut.Remove(item);
                }
            }
            if (this._filterContainAffaire.Text != "")
            {
                ObservableCollection<Releve_Heure_Atelier> toRemove = new ObservableCollection<Releve_Heure_Atelier>();
                foreach (Releve_Heure_Atelier item in listToPut)
                {
                    bool test = false;
                    if (!test)
                    {
                        foreach (Heure_Atelier tsr in item.Heure_Atelier)
                        {
                            if (tsr.Affaire1 != null)
                            {
                                if (tsr.Affaire1.Numero.Trim().ToLower().Contains(this._filterContainAffaire.Text.Trim().ToLower()))
                                {
                                    test = true;
                                }
                            }
                        }
                    }
                    if (!test)
                    {
                        toRemove.Add(item);
                    }
                }
                foreach (Releve_Heure_Atelier item in toRemove)
                {
                    listToPut.Remove(item);
                }
            }

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            //Si aucun résultat, j'affiche un message
            if (this.listReleve.Count() == 0)
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
                if (this._filterContainAffaire.Text != "" || this._filterContainRegie.Text != "" || this._filterContainDateDebutSemaine.SelectedDate != null || this._filterContainNumeroSemaine.Text != "" || this._filterContainSalarie.Text != "" || this.max != this.listReleve.Count())
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

        #region Fonctions

        /// <summary>
        /// Recalcul le nombre d'élements maximum
        /// </summary>
        private void recalculMax()
        {
            this.max = ((App)App.Current).mySitaffEntities.Releve_Heure_Atelier.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Releve_Heure_Atelier soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Releve_Heure_Atelier rha)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des commandes fournisseur terminée : " + this.listReleve.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listReleve.Add(rha);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un relevé d'atelier effectué avec succès. Nombre d'élements : " + this.listReleve.Count() + " / " + this.max);
                try
                {
                    this._DataGridMain.SelectedItem = rha;
                }
                catch (Exception) { }
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification du relevé d'atelier effectuée avec succès. Nombre d'élements : " + this.listReleve.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listReleve.Remove(rha);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression du relevé d'atelier effectuée avec succès. Nombre d'élements : " + this.listReleve.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else if (typeEtat == "Duplicate")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listReleve.Add(rha);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer un relevé atelier numéro effectué avec succès. Nombre d'élements : " + this.listReleve.Count() + " / " + this.max);
            }
            else
            {
((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des relevés d'atelier terminé : " + this.listReleve.Count() + " / " + this.max);
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
            this.listReleve = new ObservableCollection<Releve_Heure_Atelier>(this.listReleve.OrderByDescending(rha => rha.Date_Debut).ThenBy(rha => rha.Salarie1.Personne.Nom));
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
