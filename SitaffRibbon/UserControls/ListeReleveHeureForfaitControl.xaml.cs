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
    /// Logique d'interaction pour ListeReleveHeureForfaitControl.xaml
    /// </summary>
    public partial class ListeReleveHeureForfaitControl : UserControl
    {
        #region Variables

        long max = 0;

        MenuItem MenuItem_ColonneChefChantier;
        MenuItem MenuItem_ColonneDateDebut;
        MenuItem MenuItem_ColonneNumeroSemaine;
        MenuItem MenuItem_ColonneTotalForfait;
        MenuItem MenuItem_ColonneTotalRegie;
        MenuItem MenuItem_ColonneAffaire;

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Propriétés de dépendances

        public ObservableCollection<Releve_Heure_Forfait> listReleveHeureForfait
        {
            get { return (ObservableCollection<Releve_Heure_Forfait>)GetValue(listReleveHeureForfaitProperty); }
            set { SetValue(listReleveHeureForfaitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listReleveHeureForfait.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listReleveHeureForfaitProperty =
            DependencyProperty.Register("listReleveHeureForfait", typeof(ObservableCollection<Releve_Heure_Forfait>), typeof(ListeReleveHeureForfaitControl), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public ListeReleveHeureForfaitControl()
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
            List<string> listChefChantier = new List<string>();
            List<string> listPersonnelChantier = new List<string>();
            List<string> listAffaire = new List<string>();
            List<string> listBonRegie = new List<string>();
            foreach (Releve_Heure_Forfait item in ((App)App.Current).mySitaffEntities.Releve_Heure_Forfait)
            {
                //Pour remplir les affaires
                if (item.Affaire1 != null)
                {
                    if (!listAffaire.Contains(item.Affaire1.Numero))
                    {
                        listAffaire.Add(item.Affaire1.Numero);
                    }
                }

                //Pour remplir les chef chantier
                if (item.Salarie1 != null)
                {
                    if (item.Salarie1.Personne != null)
                    {
                        if (!listChefChantier.Contains(item.Salarie1.Personne.fullname))
                        {
                            listChefChantier.Add(item.Salarie1.Personne.fullname);
                        }
                    }
                }

                //Pour remplir le personnel chantier
                foreach (Heure_Regie hr in item.Heure_Regie)
                {
                    if (hr.Salarie1.Personne != null)
                    {
                        if (!listPersonnelChantier.Contains(hr.Salarie1.Personne.fullname))
                        {
                            listPersonnelChantier.Add(hr.Salarie1.Personne.fullname);
                        }
                    }
                }
                foreach (Heure_Forfait hf in item.Heure_Forfait)
                {
                    if (hf.Salarie1.Personne != null)
                    {
                        if (!listPersonnelChantier.Contains(hf.Salarie1.Personne.fullname))
                        {
                            listPersonnelChantier.Add(hf.Salarie1.Personne.fullname);
                        }
                    }
                }

                //Pour remplir les numéros de bon de régie
                foreach (Travail_Sur_Regie tsr in item.Travail_Sur_Regie)
                {
                    if (tsr.Regie1 != null)
                    {
                        if (!listBonRegie.Contains(tsr.Regie1.Numero))
                        {
                            listBonRegie.Add(tsr.Regie1.Numero);
                        }
                    }
                }
            }

            this._filterContainPersonnelChantier.ItemsSource = listPersonnelChantier;
            this._filterContainChefChantier.ItemsSource = listChefChantier;
            this._filterContainAffaire.ItemsSource = listAffaire;
            this._filterContainBonRegie.ItemsSource = listBonRegie;
        }

        #endregion

        #region initialisation Donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<Releve_Heure_Forfait> listToPut)
        {
            if (listToPut == null)
            {
                this.listReleveHeureForfait = new ObservableCollection<Releve_Heure_Forfait>(((App)App.Current).mySitaffEntities.Releve_Heure_Forfait.OrderByDescending(rha => rha.Date_Debut).ThenBy(rha => rha.Affaire1.Numero));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listReleveHeureForfait = new ObservableCollection<Releve_Heure_Forfait>(listToPut);
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
            itemAfficher5.Header = "Dupliquer";
            itemAfficher5.Click += new RoutedEventHandler(delegate { this.menuDupliquer(); });

            MenuItem itemAfficher7 = new MenuItem();
            itemAfficher7.Header = "Afficher relevé d'activité / salarié";
            itemAfficher7.Click += new RoutedEventHandler(delegate { this.menuRapportReleveParSalarie(); });            

            MenuItem itemAfficher6 = RemplirMenuAfficherMasquerColonnes(new MenuItem());
            itemAfficher6.Header = "Afficher / Masquer";


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
                contextMenu.Items.Add(itemAfficher5);                
            }
            if (securite.VerificationDroitActionsCRUD(this.ToString(), "Add"))
            {
                contextMenu.Items.Add(new Separator());
                contextMenu.Items.Add(itemAfficher7);
            }
            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(itemAfficher6);
        }

        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {
            this.MenuItem_ColonneChefChantier = new MenuItem();
            this.MenuItem_ColonneChefChantier.IsChecked = false;
            this.MenuItem_ColonneChefChantier.Header = "Chef de chantier";
            this.MenuItem_ColonneChefChantier.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneChefChantier(); });
            this.AffMas_ColonneChefChantier();
            menuItem.Items.Add(this.MenuItem_ColonneChefChantier);

            this.MenuItem_ColonneDateDebut = new MenuItem();
            this.MenuItem_ColonneDateDebut.IsChecked = false;
            this.MenuItem_ColonneDateDebut.Header = "Date début de semaine";
            this.MenuItem_ColonneDateDebut.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateDebut(); });
            this.AffMas_ColonneDateDebut();
            menuItem.Items.Add(this.MenuItem_ColonneDateDebut);

            this.MenuItem_ColonneNumeroSemaine = new MenuItem();
            this.MenuItem_ColonneNumeroSemaine.IsChecked = false;
            this.MenuItem_ColonneNumeroSemaine.Header = "Numéro de semaine";
            this.MenuItem_ColonneNumeroSemaine.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumeroSemaine(); });
            this.AffMas_ColonneNumeroSemaine();
            menuItem.Items.Add(this.MenuItem_ColonneNumeroSemaine);

            this.MenuItem_ColonneTotalForfait = new MenuItem();
            this.MenuItem_ColonneTotalForfait.IsChecked = false;
            this.MenuItem_ColonneTotalForfait.Header = "Nombre d'heures de forfait";
            this.MenuItem_ColonneTotalForfait.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTotalForfait(); });
            this.AffMas_ColonneTotalForfait();
            menuItem.Items.Add(this.MenuItem_ColonneTotalForfait);

            this.MenuItem_ColonneTotalRegie = new MenuItem();
            this.MenuItem_ColonneTotalRegie.IsChecked = false;
            this.MenuItem_ColonneTotalRegie.Header = "Nombre d'heures de régie";
            this.MenuItem_ColonneTotalRegie.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTotalRegie(); });
            this.AffMas_ColonneTotalRegie();
            menuItem.Items.Add(this.MenuItem_ColonneTotalRegie);

            this.MenuItem_ColonneAffaire = new MenuItem();
            this.MenuItem_ColonneAffaire.IsChecked = false;
            this.MenuItem_ColonneAffaire.Header = "Affaire";
            this.MenuItem_ColonneAffaire.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneAffaire(); });
            this.AffMas_ColonneAffaire();
            menuItem.Items.Add(this.MenuItem_ColonneAffaire);

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

        private void menuDupliquer()
        {
            ((App)App.Current)._theMainWindow._CommandDuplicateReleveHeureForfait.Command.Execute(((App)App.Current)._theMainWindow);
        }

        private void menuRapportReleveParSalarie()
        {
            ((App)App.Current)._theMainWindow._CommandRapportReleveActiviteParSalarie.Command.Execute(((App)App.Current)._theMainWindow);
        }

        #region Afficher / Masquer

        #region Tout

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneChefChantier.IsChecked = false;
            this.MenuItem_ColonneDateDebut.IsChecked = false;
            this.MenuItem_ColonneNumeroSemaine.IsChecked = false;
            this.MenuItem_ColonneTotalForfait.IsChecked = false;
            this.MenuItem_ColonneTotalRegie.IsChecked = false;
            this.MenuItem_ColonneAffaire.IsChecked = false;

            this.AffMas_ColonneChefChantier();
            this.AffMas_ColonneDateDebut();
            this.AffMas_ColonneNumeroSemaine();
            this.AffMas_ColonneTotalForfait();
            this.AffMas_ColonneTotalRegie();
            this.AffMas_ColonneAffaire();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneChefChantier.IsChecked = true;
            this.MenuItem_ColonneDateDebut.IsChecked = true;
            this.MenuItem_ColonneNumeroSemaine.IsChecked = true;
            this.MenuItem_ColonneTotalForfait.IsChecked = true;
            this.MenuItem_ColonneTotalRegie.IsChecked = true;
            this.MenuItem_ColonneAffaire.IsChecked = true;

            this.AffMas_ColonneChefChantier();
            this.AffMas_ColonneDateDebut();
            this.AffMas_ColonneNumeroSemaine();
            this.AffMas_ColonneTotalForfait();
            this.AffMas_ColonneTotalRegie();
            this.AffMas_ColonneAffaire();
        }

        #endregion

        private void AffMas_ColonneChefChantier()
        {
            if (this.MenuItem_ColonneChefChantier.IsChecked == true)
            {
                this._ColonneChefChantier.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneChefChantier.IsChecked = false;
            }
            else
            {
                this._ColonneChefChantier.Visibility = Visibility.Visible;
                this.MenuItem_ColonneChefChantier.IsChecked = true;
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

        private void AffMas_ColonneTotalForfait()
        {
            if (this.MenuItem_ColonneTotalForfait.IsChecked == true)
            {
                this._ColonneTotalForfait.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTotalForfait.IsChecked = false;
            }
            else
            {
                this._ColonneTotalForfait.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTotalForfait.IsChecked = true;
            }
        }

        private void AffMas_ColonneTotalRegie()
        {
            if (this.MenuItem_ColonneTotalRegie.IsChecked == true)
            {
                this._ColonneTotalRegie.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTotalRegie.IsChecked = false;
            }
            else
            {
                this._ColonneTotalRegie.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTotalRegie.IsChecked = true;
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

        #endregion

        #endregion

        #region initialisation couleurs datagrid secondaires

        private void _DataGrid_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((DataGrid)sender).RowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridColor;
            ((DataGrid)sender).AlternatingRowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridAlternateColor;
        }

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
        /// Ajoute un nouveau relevé à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Releve_Heure_Forfait Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un relevé d'heures forfait en cours ...");

            //Initialisation de la fenêtre
            ReleveHeureForfaitWindow releveHeureForfaitWindow = new ReleveHeureForfaitWindow();

            //Création de l'objet temporaire
            //Mise de l'objet temporaire dans le datacontext
            releveHeureForfaitWindow.DataContext = new Releve_Heure_Forfait();

            //Options particulières de la fenêtre
            releveHeureForfaitWindow.creation = true;
            releveHeureForfaitWindow.VerrouillerLaFenetre();

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = releveHeureForfaitWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet se trouvant dans le datacontext de la fenêtre
                return (Releve_Heure_Forfait)releveHeureForfaitWindow.DataContext;
            }
            else
            {
                try
                {
                    //On détache tous les élements liés au relevé Heure_Forfait
                    ObservableCollection<Heure_Forfait> toRemove = new ObservableCollection<Heure_Forfait>();
                    foreach (Heure_Forfait item in ((Releve_Heure_Forfait)releveHeureForfaitWindow.DataContext).Heure_Forfait)
                    {
                        toRemove.Add(item);
                    }
                    foreach (Heure_Forfait item in toRemove)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    //On détache tous les élements liés au relevé Heure_Regie
                    ObservableCollection<Heure_Regie> toRemove1 = new ObservableCollection<Heure_Regie>();
                    foreach (Heure_Regie item in ((Releve_Heure_Forfait)releveHeureForfaitWindow.DataContext).Heure_Regie)
                    {
                        toRemove1.Add(item);
                    }
                    foreach (Heure_Regie item in toRemove1)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    //On détache tous les élements liés au relevé Travail_Sur_Regie
                    ObservableCollection<Travail_Sur_Regie> toRemove2 = new ObservableCollection<Travail_Sur_Regie>();
                    foreach (Travail_Sur_Regie item in ((Releve_Heure_Forfait)releveHeureForfaitWindow.DataContext).Travail_Sur_Regie)
                    {
                        toRemove2.Add(item);
                    }
                    foreach (Travail_Sur_Regie item in toRemove2)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    //On détache tous les élements liés au relevé Releve_Heure_Forfait_Bouteille_Gaz
                    ObservableCollection<Releve_Heure_Forfait_Bouteille_Gaz> toRemove3 = new ObservableCollection<Releve_Heure_Forfait_Bouteille_Gaz>();
                    foreach (Releve_Heure_Forfait_Bouteille_Gaz item in ((Releve_Heure_Forfait)releveHeureForfaitWindow.DataContext).Releve_Heure_Forfait_Bouteille_Gaz)
                    {
                        toRemove3.Add(item);
                    }
                    foreach (Releve_Heure_Forfait_Bouteille_Gaz item in toRemove3)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    //On détache le relevé
                    ((App)App.Current).mySitaffEntities.Detach((Releve_Heure_Forfait)releveHeureForfaitWindow.DataContext);
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un relevé d'heures forfait annulé : " + this.listReleveHeureForfait.Count() + " / " + this.max);

                return null;
            }
        }


        /// <summary>
        /// Ouvre le relevé séléctionné à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Releve_Heure_Forfait Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un releve d'heures forfait en cours ...");

                    //Création de la fenêtre
                    ReleveHeureForfaitWindow releveHeureForfaitWindow = new ReleveHeureForfaitWindow();

                    //Initialisation du Datacontext en Relevé heure forfait et association au relevé sélectionnée
                    releveHeureForfaitWindow.DataContext = new Releve_Heure_Forfait();
                    releveHeureForfaitWindow.DataContext = (Releve_Heure_Forfait)this._DataGridMain.SelectedItem;

                    //Mise en place des options particulières
                    releveHeureForfaitWindow.creation = true;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = releveHeureForfaitWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet se trouvant dans le datacontext de la fenêtre
                        return (Releve_Heure_Forfait)releveHeureForfaitWindow.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Releve_Heure_Forfait)(this._DataGridMain.SelectedItem));
                        //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un relevé d'heures forfait annulée : " + this.listReleveHeureForfait.Count() + " / " + this.max);

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
        public Releve_Heure_Forfait Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un relevé d'heures forfait en cours ...");

                    if (MessageBox.Show("Voulez-vous rééllement supprimer le relevé séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //On détache tous les élements liés au relevé Heure_Forfait
                        ObservableCollection<Heure_Forfait> toRemove = new ObservableCollection<Heure_Forfait>();
                        foreach (Heure_Forfait item in ((Releve_Heure_Forfait)this._DataGridMain.SelectedItem).Heure_Forfait)
                        {
                            toRemove.Add(item);
                        }
                        foreach (Heure_Forfait item in toRemove)
                        {
                            ((Releve_Heure_Forfait)this._DataGridMain.SelectedItem).Heure_Forfait.Remove(item);
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }

                        //On détache tous les élements liés au relevé Heure_Regie
                        ObservableCollection<Heure_Regie> toRemove1 = new ObservableCollection<Heure_Regie>();
                        foreach (Heure_Regie item in ((Releve_Heure_Forfait)this._DataGridMain.SelectedItem).Heure_Regie)
                        {
                            toRemove1.Add(item);
                        }
                        foreach (Heure_Regie item in toRemove1)
                        {
                            ((Releve_Heure_Forfait)this._DataGridMain.SelectedItem).Heure_Regie.Remove(item);
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }

                        //On détache tous les élements liés au relevé Travail_Sur_Regie
                        ObservableCollection<Travail_Sur_Regie> toRemove2 = new ObservableCollection<Travail_Sur_Regie>();
                        foreach (Travail_Sur_Regie item in ((Releve_Heure_Forfait)this._DataGridMain.SelectedItem).Travail_Sur_Regie)
                        {
                            toRemove2.Add(item);
                        }
                        foreach (Travail_Sur_Regie item in toRemove2)
                        {
                            ((Releve_Heure_Forfait)this._DataGridMain.SelectedItem).Travail_Sur_Regie.Remove(item);
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }

                        //On détache tous les élements liés au relevé Releve_Heure_Forfait_Bouteille_Gaz
                        ObservableCollection<Releve_Heure_Forfait_Bouteille_Gaz> toRemove3 = new ObservableCollection<Releve_Heure_Forfait_Bouteille_Gaz>();
                        foreach (Releve_Heure_Forfait_Bouteille_Gaz item in ((Releve_Heure_Forfait)this._DataGridMain.SelectedItem).Releve_Heure_Forfait_Bouteille_Gaz)
                        {
                            toRemove3.Add(item);
                        }
                        foreach (Releve_Heure_Forfait_Bouteille_Gaz item in toRemove3)
                        {
                            ((Releve_Heure_Forfait)this._DataGridMain.SelectedItem).Releve_Heure_Forfait_Bouteille_Gaz.Remove(item);
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }

                        //Supprimer l'élément 
                        return (Releve_Heure_Forfait)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un relevé d'heures forfait annulée : " + this.listReleveHeureForfait.Count() + " / " + this.max);

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
        public Releve_Heure_Forfait Look()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un relevé d'heures forfait en cours ...");

                    //Création de la fenêtre
                    ReleveHeureForfaitWindow releveHeureForfaitWindow = new ReleveHeureForfaitWindow();

                    //Initialisation du Datacontext et association à l'objet sélectionnée
                    releveHeureForfaitWindow.DataContext = new Releve_Heure_Forfait();
                    releveHeureForfaitWindow.DataContext = (Releve_Heure_Forfait)this._DataGridMain.SelectedItem;

                    //Je positionne la lecture seule sur la fenêtre
                    releveHeureForfaitWindow.lectureSeule();

                    //J'affiche la fenêtre
                    bool? dialogResult = releveHeureForfaitWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un relevé d'heures forfait terminé : " + this.listReleveHeureForfait.Count() + " / " + this.max);

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

        #region Actions supplémentaires

        /// <summary>
        /// duplique un relevé à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Releve_Heure_Forfait Duplicate()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "ajout en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer un relevé d'heures forfait en cours ...");

                    //Création de la fenêtre
                    ReleveHeureForfaitWindow releveHeureForfaitWindow = new ReleveHeureForfaitWindow();

                    //Duplication de la commande sélectionnée
                    Releve_Heure_Forfait tmp = new Releve_Heure_Forfait();
                    tmp = duplicateReleve((Releve_Heure_Forfait)this._DataGridMain.SelectedItem);

                    //Association de l'élement dupliqué au datacontext de la fenêtre
                    releveHeureForfaitWindow.DataContext = tmp;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = releveHeureForfaitWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet se trouvant dans le datacontext de la fenêtre
                        return (Releve_Heure_Forfait)releveHeureForfaitWindow.DataContext;
                    }
                    else
                    {
                        try
                        {
                            //On détache tous les élements liés au relevé Heure_Forfait
                            ObservableCollection<Heure_Forfait> toRemove = new ObservableCollection<Heure_Forfait>();
                            foreach (Heure_Forfait item in ((Releve_Heure_Forfait)releveHeureForfaitWindow.DataContext).Heure_Forfait)
                            {
                                toRemove.Add(item);
                            }
                            foreach (Heure_Forfait item in toRemove)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache tous les élements liés au relevé Heure_Regie
                            ObservableCollection<Heure_Regie> toRemove1 = new ObservableCollection<Heure_Regie>();
                            foreach (Heure_Regie item in ((Releve_Heure_Forfait)releveHeureForfaitWindow.DataContext).Heure_Regie)
                            {
                                toRemove1.Add(item);
                            }
                            foreach (Heure_Regie item in toRemove1)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache tous les élements liés au relevé Travail_Sur_Regie
                            ObservableCollection<Travail_Sur_Regie> toRemove2 = new ObservableCollection<Travail_Sur_Regie>();
                            foreach (Travail_Sur_Regie item in ((Releve_Heure_Forfait)releveHeureForfaitWindow.DataContext).Travail_Sur_Regie)
                            {
                                toRemove2.Add(item);
                            }
                            foreach (Travail_Sur_Regie item in toRemove2)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache tous les élements liés au relevé Releve_Heure_Forfait_Bouteille_Gaz
                            ObservableCollection<Releve_Heure_Forfait_Bouteille_Gaz> toRemove3 = new ObservableCollection<Releve_Heure_Forfait_Bouteille_Gaz>();
                            foreach (Releve_Heure_Forfait_Bouteille_Gaz item in ((Releve_Heure_Forfait)releveHeureForfaitWindow.DataContext).Releve_Heure_Forfait_Bouteille_Gaz)
                            {
                                toRemove3.Add(item);
                            }
                            foreach (Releve_Heure_Forfait_Bouteille_Gaz item in toRemove3)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache le relevé
                            ((App)App.Current).mySitaffEntities.Detach((Releve_Heure_Forfait)releveHeureForfaitWindow.DataContext);
                        }
                        catch (Exception)
                        {
                        }

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer un relevé d'heures forfait annulé.");

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

        #endregion

        #region filtrages

        #region Remise à Zéro

        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            this._filterContainChefChantier.Text = "";
            this._filterContainPersonnelChantier.Text = "";
            this._filterContainAffaire.Text = "";
            this._filterContainDateDebutSemaine.SelectedDate = null;
            this._filterContainNumeroSemaine.Text = "";
            this._filterContainTotalHeuresForfait.Text = "";
            this._filterContainTotalHeuresRegie.Text = "";
            this._filterContainBonRegie.Text = "";
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

            ObservableCollection<Releve_Heure_Forfait> listToPut = new ObservableCollection<Releve_Heure_Forfait>(((App)App.Current).mySitaffEntities.Releve_Heure_Forfait.OrderByDescending(rha => rha.Date_Debut).ThenBy(rha => rha.Affaire1.Numero));
            
            if (this._filterContainDateDebutSemaine.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Releve_Heure_Forfait>(listToPut.Where(com => com.Date_Debut != null));
                listToPut = new ObservableCollection<Releve_Heure_Forfait>(listToPut.Where(com => com.Date_Debut == this._filterContainDateDebutSemaine.SelectedDate));
            }
            if (this._filterContainChefChantier.Text != "")
            {
                listToPut = new ObservableCollection<Releve_Heure_Forfait>(listToPut.Where(com => com.Salarie1 != null));
                listToPut = new ObservableCollection<Releve_Heure_Forfait>(listToPut.Where(com => com.Salarie1.Personne != null));
                listToPut = new ObservableCollection<Releve_Heure_Forfait>(listToPut.Where(com => com.Salarie1.Personne.fullname.Trim().ToLower().Contains(this._filterContainChefChantier.Text.Trim().ToLower()) || com.Salarie1.Personne.Initiales.Trim().ToLower().Contains(this._filterContainChefChantier.Text.Trim().ToLower())));
            }
            if (this._filterContainAffaire.Text != "")
            {
                listToPut = new ObservableCollection<Releve_Heure_Forfait>(listToPut.Where(com => com.Affaire1 != null));
                listToPut = new ObservableCollection<Releve_Heure_Forfait>(listToPut.Where(com => com.Affaire1.Numero.Trim().ToLower().Contains(this._filterContainAffaire.Text.Trim().ToLower())));
            }
            if (this._filterContainNumeroSemaine.Text != "")
            {
                int val;
                if (int.TryParse(this._filterContainNumeroSemaine.Text.Trim(), out val))
                {
                    listToPut = new ObservableCollection<Releve_Heure_Forfait>(listToPut.Where(com => com.NumeroSemaine != null));
                    listToPut = new ObservableCollection<Releve_Heure_Forfait>(listToPut.Where(com => com.NumeroSemaine.ToString().Contains(double.Parse(this._filterContainNumeroSemaine.Text.Trim()).ToString())));
                }
            }
            if (this._filterContainTotalHeuresForfait.Text != "")
            {
                double val;
                if (double.TryParse(this._filterContainTotalHeuresForfait.Text.Trim(), out val))
                {
                    listToPut = new ObservableCollection<Releve_Heure_Forfait>(listToPut.Where(com => com.Total_Forfait.ToString().Contains(double.Parse(this._filterContainTotalHeuresForfait.Text.Trim()).ToString())));
                }
            }
            if (this._filterContainTotalHeuresRegie.Text != "")
            {
                double val;
                if (double.TryParse(this._filterContainTotalHeuresRegie.Text.Trim(), out val))
                {
                    listToPut = new ObservableCollection<Releve_Heure_Forfait>(listToPut.Where(com => com.Total_Regie.ToString().Contains(double.Parse(this._filterContainTotalHeuresRegie.Text.Trim()).ToString())));
                }
            }
            if (this._filterContainBonRegie.Text != "")
            {
                ObservableCollection<Releve_Heure_Forfait> toRemove = new ObservableCollection<Releve_Heure_Forfait>();
                foreach (Releve_Heure_Forfait item in listToPut)
                {
                    bool test = false;
                    if (!test)
                    {
                        foreach (Travail_Sur_Regie tsr in item.Travail_Sur_Regie)
                        {
                            if (tsr.Regie1 != null)
                            {
                                if (tsr.Regie1.Numero.Trim().ToLower().Contains(this._filterContainBonRegie.Text.Trim().ToLower()))
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
                foreach (Releve_Heure_Forfait item in toRemove)
                {
                    listToPut.Remove(item);
                }
            }
            if (this._filterContainPersonnelChantier.Text != "")
            {
                ObservableCollection<Releve_Heure_Forfait> toRemove = new ObservableCollection<Releve_Heure_Forfait>();
                foreach (Releve_Heure_Forfait item in listToPut)
                {
                    bool test = false;
                    if (!test)
                    {                        
                        foreach (Heure_Forfait hf in item.Heure_Forfait)
                        {
                            if (hf.Salarie1 != null)
                            {
                                if (hf.Salarie1.Personne != null)
                                {
                                    if (hf.Salarie1.Personne.fullname.Trim().ToLower().Contains(this._filterContainPersonnelChantier.Text.Trim().ToLower()) || hf.Salarie1.Personne.Initiales.Trim().ToLower().Contains(this._filterContainPersonnelChantier.Text.Trim().ToLower()))
                                    {
                                        test = true;
                                    }
                                }
                            }
                        }
                    }
                    if (!test)
                    {
                        foreach (Heure_Regie hr in item.Heure_Regie)
                        {
                            if (hr.Salarie1 != null)
                            {
                                if (hr.Salarie1.Personne != null)
                                {
                                    if (hr.Salarie1.Personne.fullname.Trim().ToLower().Contains(this._filterContainPersonnelChantier.Text.Trim().ToLower()) || hr.Salarie1.Personne.Initiales.Trim().ToLower().Contains(this._filterContainPersonnelChantier.Text.Trim().ToLower()))
                                    {
                                        test = true;
                                    }
                                }
                            }
                        }
                    }
                    if (!test)
                    {
                        toRemove.Add(item);
                    }
                }
                foreach (Releve_Heure_Forfait item in toRemove)
                {
                    listToPut.Remove(item);
                }
            }            

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            //Si aucun résultat, j'affiche un message
            if (this.listReleveHeureForfait.Count() == 0)
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
                if (this._filterContainBonRegie.Text != "" || this._filterContainChefChantier.Text != "" || this._filterContainPersonnelChantier.Text != "" || this._filterContainNumeroSemaine.Text != "" || this._filterContainTotalHeuresForfait.Text != "" || this._filterContainTotalHeuresRegie.Text != "" || this._filterContainAffaire.Text != "" || this._filterContainDateDebutSemaine.SelectedDate != null || this.max != this.listReleveHeureForfait.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainChefChantier.Focus();
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

        private void _filterContainTotalHeures_KeyUp_1(object sender, KeyEventArgs e)
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

        #endregion

        #region Fonctions

        /// <summary>
        /// Recalcul le nombre d'élements maximum
        /// </summary>
        private void recalculMax()
        {
            this.max = ((App)App.Current).mySitaffEntities.Releve_Heure_Forfait.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="rhf">un objet Releve_heure_forfait soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Releve_Heure_Forfait rhf)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des Relevés d'heures de chantier terminé : " + this.listReleveHeureForfait.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listReleveHeureForfait.Add(rhf);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un Relevés d'heures de chantier supervisé par '" + rhf.Salarie1.Personne.fullname + "' effectué avec succès. Nombre d'élements : " + this.listReleveHeureForfait.Count() + " / " + this.max);
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification du Relevés d'heures de chantier supervisé par '" + rhf.Salarie1.Personne.fullname + "' effectuée avec succès. Nombre d'élements : " + this.listReleveHeureForfait.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listReleveHeureForfait.Remove(rhf);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression du Relevés d'heures de chantier effectuée avec succès. Nombre d'élements : " + this.listReleveHeureForfait.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else if (typeEtat == "Duplicate")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listReleveHeureForfait.Add(rhf);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer un Relevés d'heures de chantier effectué avec succès. Nombre d'élements : " + this.listReleveHeureForfait.Count() + " / " + this.max);
            }
            else
            {
((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des Relevés d'heures de chantier terminé : " + this.listReleveHeureForfait.Count() + " / " + this.max);
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
            this.listReleveHeureForfait = new ObservableCollection<Releve_Heure_Forfait>(this.listReleveHeureForfait.OrderByDescending(rha => rha.Date_Debut).ThenBy(rha => rha.Affaire1.Numero));
        }

        /// <summary>
        /// duplique la commande passée en paramètre
        /// </summary>
        /// <param name="commande1">commande à dupliquer</param>
        private Releve_Heure_Forfait duplicateReleve(Releve_Heure_Forfait itemToCopy)
        {
            Releve_Heure_Forfait tmp = new Releve_Heure_Forfait();

            tmp.Affaire1 = itemToCopy.Affaire1;
            tmp.Salarie1 = itemToCopy.Salarie1;
            foreach (Heure_Forfait ccf in itemToCopy.Heure_Forfait)
            {
                Heure_Forfait toAdd = new Heure_Forfait();

                toAdd.Salarie1 = ccf.Salarie1;
                toAdd.Heures_Dimanche_Jour = ccf.Heures_Dimanche_Jour;
                toAdd.Heures_Dimanche_Nuit = ccf.Heures_Dimanche_Nuit;
                toAdd.Heures_Jeudi_Jour = ccf.Heures_Jeudi_Jour;
                toAdd.Heures_Jeudi_Nuit = ccf.Heures_Jeudi_Nuit;
                toAdd.Heures_Lundi_Jour = ccf.Heures_Lundi_Jour;
                toAdd.Heures_Lundi_Nuit = ccf.Heures_Lundi_Nuit;
                toAdd.Heures_Mardi_Jour = ccf.Heures_Mardi_Jour;
                toAdd.Heures_Mardi_Nuit = ccf.Heures_Mardi_Nuit;
                toAdd.Heures_Mercredi_Jour = ccf.Heures_Mercredi_Jour;
                toAdd.Heures_Mercredi_Nuit = ccf.Heures_Mercredi_Nuit;
                toAdd.Heures_Samedi_Jour = ccf.Heures_Samedi_Jour;
                toAdd.Heures_Samedi_Nuit = ccf.Heures_Samedi_Nuit;
                toAdd.Heures_Vendredi_Jour = ccf.Heures_Vendredi_Jour;
                toAdd.Heures_Vendredi_Nuit = ccf.Heures_Vendredi_Nuit;
                toAdd.Vehicule_Dimanche = ccf.Vehicule_Dimanche;
                toAdd.Vehicule_Jeudi = ccf.Vehicule_Jeudi;
                toAdd.Vehicule_Lundi = ccf.Vehicule_Lundi;
                toAdd.Vehicule_Mardi = ccf.Vehicule_Mardi;
                toAdd.Vehicule_Mercredi = ccf.Vehicule_Mercredi;
                toAdd.Vehicule_Samedi = ccf.Vehicule_Samedi;
                toAdd.Vehicule_Vendredi = ccf.Vehicule_Vendredi;

                tmp.Heure_Forfait.Add(toAdd);
            }

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
