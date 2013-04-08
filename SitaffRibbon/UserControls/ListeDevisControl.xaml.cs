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
using SitaffRibbon.Classes;
/* 
 * Using pour utilisation des IObservableCollection (afin d'éviter de mettre
 * System.Collections.ObjectModel.IObservableCollection en entier)
 */
using System.Collections.ObjectModel;
using System.Threading;

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeDevisControl.xaml
    /// </summary>
    public partial class ListeDevisControl : UserControl
    {

        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_ColonneNumeroDevis;
        MenuItem MenuItem_ColonneClient;
        MenuItem MenuItem_ColonneChargeAffaire;
        MenuItem MenuItem_ColonneDateFin;
        MenuItem MenuItem_ColonneDateDebut;
        MenuItem MenuItem_ColonneEtat;
        MenuItem MenuItem_ColonneType;
        MenuItem MenuItem_ColonneLibelle;
        MenuItem MenuItem_ColonneTva;
        MenuItem MenuItem_ColonneSaisie;
        MenuItem MenuItem_ColonneEntrepriseMere;
        MenuItem MenuItem_ColonneChargeAffaireSecondaire;
        MenuItem MenuItem_ColonneChargeEtude;
        MenuItem MenuItem_ColonneTauxHoraire;
        MenuItem MenuItem_ColonneClientFacturation;
        MenuItem MenuItem_ColonneClientLivraison;
        MenuItem MenuItem_ColonneTotalVersions;
        MenuItem MenuItem_ColonneMontantVersions;
        MenuItem MenuItem_ColonneCommentaire;

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region proprièté de dépendance

        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listDevis.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(ListeDevisControl), new UIPropertyMetadata(null));

        public ObservableCollection<Entreprise> listEntreprise
        {
            get { return (ObservableCollection<Entreprise>)GetValue(listEntrepriseProperty); }
            set { SetValue(listEntrepriseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listDevis.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseProperty =
            DependencyProperty.Register("listEntreprise", typeof(ObservableCollection<Entreprise>), typeof(ListeDevisControl), new UIPropertyMetadata(null));

        public ObservableCollection<Devis> listDevis
        {
            get { return (ObservableCollection<Devis>)GetValue(listDevisProperty); }
            set { SetValue(listDevisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listDevis.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listDevisProperty =
            DependencyProperty.Register("listDevis", typeof(ObservableCollection<Devis>), typeof(ListeDevisControl), new UIPropertyMetadata(null));


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

        #region constructeur

        public ListeDevisControl()
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
            itemAfficher5.Header = "Passer en commande";
            itemAfficher5.Click += new RoutedEventHandler(delegate { this.passerEnAffaire(); });

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
            contextMenu.Items.Add(new Separator());

            contextMenu.Items.Add(itemAfficher5);

            contextMenu.Items.Add(new Separator());

            contextMenu.Items.Add(itemAfficher6);
        }

        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {
            this.MenuItem_ColonneNumeroDevis = new MenuItem();
            this.MenuItem_ColonneNumeroDevis.IsChecked = false;
            this.MenuItem_ColonneNumeroDevis.Header = "Numéro devis";
            this.MenuItem_ColonneNumeroDevis.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumeroDevis(); });
            this.AffMas_ColonneNumeroDevis();
            menuItem.Items.Add(this.MenuItem_ColonneNumeroDevis);

            this.MenuItem_ColonneChargeAffaire = new MenuItem();
            this.MenuItem_ColonneChargeAffaire.IsChecked = false;
            this.MenuItem_ColonneChargeAffaire.Header = "Chargé d'Affaire principal";
            this.MenuItem_ColonneChargeAffaire.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneChargeAffaire(); });
            this.AffMas_ColonneChargeAffaire();
            menuItem.Items.Add(this.MenuItem_ColonneChargeAffaire);

            this.MenuItem_ColonneChargeAffaireSecondaire = new MenuItem();
            this.MenuItem_ColonneChargeAffaireSecondaire.IsChecked = true;
            this.MenuItem_ColonneChargeAffaireSecondaire.Header = "Chargé d'Affaire secondaire";
            this.MenuItem_ColonneChargeAffaireSecondaire.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneChargeAffaireSecondaire(); });
            this.AffMas_ColonneChargeAffaireSecondaire();
            menuItem.Items.Add(this.MenuItem_ColonneChargeAffaireSecondaire);

            this.MenuItem_ColonneChargeEtude = new MenuItem();
            this.MenuItem_ColonneChargeEtude.IsChecked = true;
            this.MenuItem_ColonneChargeEtude.Header = "Chargé d'étude";
            this.MenuItem_ColonneChargeEtude.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneChargeEtude(); });
            this.AffMas_ColonneChargeEtude();
            menuItem.Items.Add(this.MenuItem_ColonneChargeEtude);

            this.MenuItem_ColonneClient = new MenuItem();
            this.MenuItem_ColonneClient.IsChecked = false;
            this.MenuItem_ColonneClient.Header = "Client principal";
            this.MenuItem_ColonneClient.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneClient(); });
            this.AffMas_ColonneClient();
            menuItem.Items.Add(this.MenuItem_ColonneClient);

            this.MenuItem_ColonneClientFacturation = new MenuItem();
            this.MenuItem_ColonneClientFacturation.IsChecked = true;
            this.MenuItem_ColonneClientFacturation.Header = "Client facturation";
            this.MenuItem_ColonneClientFacturation.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneClientFacturation(); });
            this.AffMas_ColonneClientFacturation();
            menuItem.Items.Add(this.MenuItem_ColonneClientFacturation);

            this.MenuItem_ColonneClientLivraison = new MenuItem();
            this.MenuItem_ColonneClientLivraison.IsChecked = true;
            this.MenuItem_ColonneClientLivraison.Header = "Client livraison";
            this.MenuItem_ColonneClientLivraison.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneClientLivraison(); });
            this.AffMas_ColonneClientLivraison();
            menuItem.Items.Add(this.MenuItem_ColonneClientLivraison);

            this.MenuItem_ColonneLibelle = new MenuItem();
            this.MenuItem_ColonneLibelle.IsChecked = false;
            this.MenuItem_ColonneLibelle.Header = "Libelle";
            this.MenuItem_ColonneLibelle.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneLibelle(); });
            this.AffMas_ColonneLibelle();
            menuItem.Items.Add(this.MenuItem_ColonneLibelle);

            this.MenuItem_ColonneEtat = new MenuItem();
            this.MenuItem_ColonneEtat.IsChecked = false;
            this.MenuItem_ColonneEtat.Header = "Etat";
            this.MenuItem_ColonneEtat.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEtat(); });
            this.AffMas_ColonneEtat();
            menuItem.Items.Add(this.MenuItem_ColonneEtat);

            this.MenuItem_ColonneType = new MenuItem();
            this.MenuItem_ColonneType.IsChecked = false;
            this.MenuItem_ColonneType.Header = "Type";
            this.MenuItem_ColonneType.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneType(); });
            this.AffMas_ColonneType();
            menuItem.Items.Add(this.MenuItem_ColonneType);

            this.MenuItem_ColonneDateDebut = new MenuItem();
            this.MenuItem_ColonneDateDebut.IsChecked = true;
            this.MenuItem_ColonneDateDebut.Header = "Date debut de chantier";
            this.MenuItem_ColonneDateDebut.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateDebut(); });
            this.AffMas_ColonneDateDebut();
            menuItem.Items.Add(this.MenuItem_ColonneDateDebut);

            this.MenuItem_ColonneDateFin = new MenuItem();
            this.MenuItem_ColonneDateFin.IsChecked = true;
            this.MenuItem_ColonneDateFin.Header = "Date fin de chantier";
            this.MenuItem_ColonneDateFin.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDateFin(); });
            this.AffMas_ColonneDateFin();
            menuItem.Items.Add(this.MenuItem_ColonneDateFin);

            this.MenuItem_ColonneTva = new MenuItem();
            this.MenuItem_ColonneTva.IsChecked = true;
            this.MenuItem_ColonneTva.Header = "Tva";
            this.MenuItem_ColonneTva.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTva(); });
            this.AffMas_ColonneTva();
            menuItem.Items.Add(this.MenuItem_ColonneTva);

            this.MenuItem_ColonneTauxHoraire = new MenuItem();
            this.MenuItem_ColonneTauxHoraire.IsChecked = true;
            this.MenuItem_ColonneTauxHoraire.Header = "Taux Horaire";
            this.MenuItem_ColonneTauxHoraire.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTauxHoraire(); });
            this.AffMas_ColonneTauxHoraire();
            menuItem.Items.Add(this.MenuItem_ColonneTauxHoraire);

            this.MenuItem_ColonneSaisie = new MenuItem();
            this.MenuItem_ColonneSaisie.IsChecked = true;
            this.MenuItem_ColonneSaisie.Header = "Saisie par";
            this.MenuItem_ColonneSaisie.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneSaisie(); });
            this.AffMas_ColonneSaisie();
            menuItem.Items.Add(this.MenuItem_ColonneSaisie);

            this.MenuItem_ColonneEntrepriseMere = new MenuItem();
            this.MenuItem_ColonneEntrepriseMere.IsChecked = true;
            this.MenuItem_ColonneEntrepriseMere.Header = "Entreprise Mère";
            this.MenuItem_ColonneEntrepriseMere.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEntrepriseMere(); });
            this.AffMas_ColonneEntrepriseMere();
            menuItem.Items.Add(this.MenuItem_ColonneEntrepriseMere);

            this.MenuItem_ColonneTotalVersions = new MenuItem();
            this.MenuItem_ColonneTotalVersions.IsChecked = true;
            this.MenuItem_ColonneTotalVersions.Header = "Total Versions";
            this.MenuItem_ColonneTotalVersions.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTotalVersions(); });
            this.AffMas_ColonneTotalVersions();
            menuItem.Items.Add(this.MenuItem_ColonneTotalVersions);

            this.MenuItem_ColonneMontantVersions = new MenuItem();
            this.MenuItem_ColonneMontantVersions.IsChecked = false;
            this.MenuItem_ColonneMontantVersions.Header = "Montant dernière version";
            this.MenuItem_ColonneMontantVersions.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneMontantVersions(); });
            this.AffMas_ColonneMontantVersions();
            menuItem.Items.Add(this.MenuItem_ColonneMontantVersions);

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

        private void passerEnAffaire()
        {
            ((App)App.Current)._theMainWindow._CommandPasserAffaire.Command.Execute(((App)App.Current)._theMainWindow);
        }

        private void imprimer()
        {
            try
            {
                //this._DataGridMain.SelectedItem = null;
                //PrintDialog printDialog = new PrintDialog();
                //if (printDialog.ShowDialog() == true)
                //{
                //    printDialog.PrintVisual(this._DataGridMain, "Salariés");
                //}
                PrintDialog Printdlg = new System.Windows.Controls.PrintDialog();
                if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
                {
                    Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                    // sizing of the element.
                    this._DataGridMain.Measure(pageSize);
                    this._DataGridMain.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));
                    Printdlg.PrintVisual(this._DataGridMain, "Salariés");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Impression raté. Veuillez contacter votre administrateur système.      :    " + e.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            //System.Windows.Controls.PrintDialog test2 = new System.Windows.Controls.PrintDialog();
            //test2.PageRangeSelection = PageRangeSelection.AllPages;
            //test2.UserPageRangeEnabled = true;

            //bool? print = test2.ShowDialog();
            //if (print)
            //{
            //    XpsDocument xpsDocument = new XpsDocument
            //}

        }

        #region Afficher / Masquer

        #region Tout

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneNumeroDevis.IsChecked = false;
            this.MenuItem_ColonneClient.IsChecked = false;
            this.MenuItem_ColonneChargeAffaire.IsChecked = false;
            this.MenuItem_ColonneDateFin.IsChecked = false;
            this.MenuItem_ColonneDateDebut.IsChecked = false;
            this.MenuItem_ColonneEtat.IsChecked = false;
            this.MenuItem_ColonneType.IsChecked = false;
            this.MenuItem_ColonneLibelle.IsChecked = false;
            this.MenuItem_ColonneTva.IsChecked = false;
            this.MenuItem_ColonneSaisie.IsChecked = false;
            this.MenuItem_ColonneEntrepriseMere.IsChecked = false;
            this.MenuItem_ColonneChargeAffaireSecondaire.IsChecked = false;
            this.MenuItem_ColonneChargeEtude.IsChecked = false;
            this.MenuItem_ColonneTauxHoraire.IsChecked = false;
            this.MenuItem_ColonneClientFacturation.IsChecked = false;
            this.MenuItem_ColonneClientLivraison.IsChecked = false;
            this.MenuItem_ColonneMontantVersions.IsChecked = false;
            this.MenuItem_ColonneTotalVersions.IsChecked = false;
            this.MenuItem_ColonneCommentaire.IsChecked = false;


            this.AffMas_ColonneNumeroDevis();
            this.AffMas_ColonneClient();
            this.AffMas_ColonneChargeAffaire();
            this.AffMas_ColonneDateFin();
            this.AffMas_ColonneDateDebut();
            this.AffMas_ColonneEtat();
            this.AffMas_ColonneType();
            this.AffMas_ColonneLibelle();
            this.AffMas_ColonneTva();
            this.AffMas_ColonneSaisie();
            this.AffMas_ColonneEntrepriseMere();
            this.AffMas_ColonneChargeAffaireSecondaire();
            this.AffMas_ColonneChargeEtude();
            this.AffMas_ColonneTauxHoraire();
            this.AffMas_ColonneClientFacturation();
            this.AffMas_ColonneClientLivraison();
            this.AffMas_ColonneMontantVersions();
            this.AffMas_ColonneTotalVersions();
            this.AffMas_ColonneCommentaire();

        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneNumeroDevis.IsChecked = true;
            this.MenuItem_ColonneClient.IsChecked = true;
            this.MenuItem_ColonneChargeAffaire.IsChecked = true;
            this.MenuItem_ColonneDateFin.IsChecked = true;
            this.MenuItem_ColonneDateDebut.IsChecked = true;
            this.MenuItem_ColonneEtat.IsChecked = true;
            this.MenuItem_ColonneType.IsChecked = true;
            this.MenuItem_ColonneLibelle.IsChecked = true;
            this.MenuItem_ColonneTva.IsChecked = true;
            this.MenuItem_ColonneSaisie.IsChecked = true;
            this.MenuItem_ColonneEntrepriseMere.IsChecked = true;
            this.MenuItem_ColonneChargeAffaireSecondaire.IsChecked = true;
            this.MenuItem_ColonneChargeEtude.IsChecked = true;
            this.MenuItem_ColonneTauxHoraire.IsChecked = true;
            this.MenuItem_ColonneClientFacturation.IsChecked = true;
            this.MenuItem_ColonneClientLivraison.IsChecked = true;
            this.MenuItem_ColonneMontantVersions.IsChecked = true;
            this.MenuItem_ColonneTotalVersions.IsChecked = true;
            this.MenuItem_ColonneCommentaire.IsChecked = true;


            this.AffMas_ColonneNumeroDevis();
            this.AffMas_ColonneClient();
            this.AffMas_ColonneChargeAffaire();
            this.AffMas_ColonneDateFin();
            this.AffMas_ColonneDateDebut();
            this.AffMas_ColonneEtat();
            this.AffMas_ColonneType();
            this.AffMas_ColonneLibelle();
            this.AffMas_ColonneTva();
            this.AffMas_ColonneSaisie();
            this.AffMas_ColonneEntrepriseMere();
            this.AffMas_ColonneChargeAffaireSecondaire();
            this.AffMas_ColonneChargeEtude();
            this.AffMas_ColonneTauxHoraire();
            this.AffMas_ColonneClientFacturation();
            this.AffMas_ColonneClientLivraison();
            this.AffMas_ColonneMontantVersions();
            this.AffMas_ColonneTotalVersions();
            this.AffMas_ColonneCommentaire();

        }

        #endregion

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

        private void AffMas_ColonneMontantVersions()
        {
            if (this.MenuItem_ColonneMontantVersions.IsChecked == true)
            {
                this._ColonneMontantVersions.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneMontantVersions.IsChecked = false;
            }
            else
            {
                this._ColonneMontantVersions.Visibility = Visibility.Visible;
                this.MenuItem_ColonneMontantVersions.IsChecked = true;
            }
        }

        private void AffMas_ColonneTotalVersions()
        {
            if (this.MenuItem_ColonneTotalVersions.IsChecked == true)
            {
                this._ColonneTotalVersions.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTotalVersions.IsChecked = false;
            }
            else
            {
                this._ColonneTotalVersions.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTotalVersions.IsChecked = true;
            }
        }

        private void AffMas_ColonneSaisie()
        {
            if (this.MenuItem_ColonneSaisie.IsChecked == true)
            {
                this._ColonneSaisie.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneSaisie.IsChecked = false;
            }
            else
            {
                this._ColonneSaisie.Visibility = Visibility.Visible;
                this.MenuItem_ColonneSaisie.IsChecked = true;
            }
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

        private void AffMas_ColonneChargeAffaireSecondaire()
        {
            if (this.MenuItem_ColonneChargeAffaireSecondaire.IsChecked == true)
            {
                this._ColonneChargeAffaireSecondaire.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneChargeAffaireSecondaire.IsChecked = false;
            }
            else
            {
                this._ColonneChargeAffaireSecondaire.Visibility = Visibility.Visible;
                this.MenuItem_ColonneChargeAffaireSecondaire.IsChecked = true;
            }
        }

        private void AffMas_ColonneChargeEtude()
        {
            if (this.MenuItem_ColonneChargeEtude.IsChecked == true)
            {
                this._ColonneChargeEtude.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneChargeEtude.IsChecked = false;
            }
            else
            {
                this._ColonneChargeEtude.Visibility = Visibility.Visible;
                this.MenuItem_ColonneChargeEtude.IsChecked = true;
            }
        }

        private void AffMas_ColonneTauxHoraire()
        {
            if (this.MenuItem_ColonneTauxHoraire.IsChecked == true)
            {
                this._ColonneTauxHoraire.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTauxHoraire.IsChecked = false;
            }
            else
            {
                this._ColonneTauxHoraire.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTauxHoraire.IsChecked = true;
            }
        }

        private void AffMas_ColonneClientFacturation()
        {
            if (this.MenuItem_ColonneClientFacturation.IsChecked == true)
            {
                this._ColonneClientFacturation.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneClientFacturation.IsChecked = false;
            }
            else
            {
                this._ColonneClientFacturation.Visibility = Visibility.Visible;
                this.MenuItem_ColonneClientFacturation.IsChecked = true;
            }
        }

        private void AffMas_ColonneClientLivraison()
        {
            if (this.MenuItem_ColonneClientLivraison.IsChecked == true)
            {
                this._ColonneClientLivraison.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneClientLivraison.IsChecked = false;
            }
            else
            {
                this._ColonneClientLivraison.Visibility = Visibility.Visible;
                this.MenuItem_ColonneClientLivraison.IsChecked = true;
            }
        }

        private void AffMas_ColonneNumeroDevis()
        {
            if (this.MenuItem_ColonneNumeroDevis.IsChecked == true)
            {
                this._ColonneNumeroDevis.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNumeroDevis.IsChecked = false;
            }
            else
            {
                this._ColonneNumeroDevis.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNumeroDevis.IsChecked = true;
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

        private void AffMas_ColonneChargeAffaire()
        {
            if (this.MenuItem_ColonneChargeAffaire.IsChecked == true)
            {
                this._ColonneChargeAffaire.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneChargeAffaire.IsChecked = false;
            }
            else
            {
                this._ColonneChargeAffaire.Visibility = Visibility.Visible;
                this.MenuItem_ColonneChargeAffaire.IsChecked = true;
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

        private void AffMas_ColonneType()
        {
            if (this.MenuItem_ColonneType.IsChecked == true)
            {
                this._ColonneType.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneType.IsChecked = false;
            }
            else
            {
                this._ColonneType.Visibility = Visibility.Visible;
                this.MenuItem_ColonneType.IsChecked = true;
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

        private void AffMas_ColonneTva()
        {
            if (this.MenuItem_ColonneTva.IsChecked == true)
            {
                this._ColonneTva.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTva.IsChecked = false;
            }
            else
            {
                this._ColonneTva.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTva.IsChecked = true;
            }
        }
        #endregion

        #endregion

        #region initialisation Zone de filtrage

        private void initialisationFilterZone()
        {
            this.initialisationComboBoxOuiNon();
            this.initialisationAutoCompleteBox();
            this._filterZone.Height = 21;
        }

        private void initialisationAutoCompleteBox()
        {
            List<string> listChargeAffaire = new List<string>();
            List<string> listClient = new List<string>();
            foreach (Devis item in ((App)App.Current).mySitaffEntities.Devis)
            {
                //Pour remplir les chargés d'affaire
                if (item.Salarie != null)
                {
                    if (item.Salarie.Personne != null)
                    {
                        if (!listChargeAffaire.Contains(item.Salarie.Personne.fullname))
                        {
                            listChargeAffaire.Add(item.Salarie.Personne.fullname);
                        }
                    }
                }

                //Pour remplir les client
                if (item.Client2 != null)
                {
                    if (item.Client2.Entreprise != null)
                    {
                        if (!listClient.Contains(item.Client2.Entreprise.Libelle))
                        {
                            listClient.Add(item.Client2.Entreprise.Libelle);
                        }
                    }
                }

            }

            _filterContainChargeAffaire.ItemsSource = listChargeAffaire;
            _filterContainClient.ItemsSource = listClient;

        }

        private void initialisationComboBoxOuiNon()
        {
            ObservableCollection<EtatDevis> listEtat = new ObservableCollection<EtatDevis>();
            foreach (Devis_Etat de in ((App)App.Current).mySitaffEntities.Devis_Etat)
            {
                listEtat.Add(new EtatDevis(de.Libelle));
            }
            listEtat.Add(new EtatDevis("En Affaire"));
            listEtat.Add(new EtatDevis("Etat inconnu, veuillez le renseigner"));
            this._filterContainEtat.ItemsSource = listEtat;
        }

        #endregion

        #region initialisation Donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<Devis> listToPut)
        {
            if (listToPut == null)
            {
                this.listDevis = new ObservableCollection<Devis>(((App)App.Current).mySitaffEntities.Devis.OrderBy(ccf => ccf.Numero));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listDevis = new ObservableCollection<Devis>(listToPut);
                this.MiseAJourEtat("Filtrage", null);
            }
        }

        #endregion

        #region initialisation couleurs datagrid secondaires

        private void _DataGrid_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((DataGrid)sender).RowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridColor;
            ((DataGrid)sender).AlternatingRowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridAlternateColor;
        }

        #endregion

        #endregion

        #region CRUD (Create Read Update Delete)

        /// <summary>
        /// Ajoute un nouveau devis à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Devis PasserAffaire()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Passage en affaire d'un devis en cours ...");

                    PassageAffaireWindow passageAffaireWindow = new PassageAffaireWindow();
                    passageAffaireWindow.DataContext = new Versions();
                    passageAffaireWindow.devis = (Devis)this._DataGridMain.SelectedItem;

                    bool? dialogResult = passageAffaireWindow.ShowDialog();
                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Devis)passageAffaireWindow.devis;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Devis)(this._DataGridMain.SelectedItem));
                        //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Passage en affaire d'un devis annulé : " + this.listDevis.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ajoute une nouvelle Devis à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Devis Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un devis en cours ...");

            //Initialisation de la fenêtre
            CreationDevisWindow creationdevisWindow = new CreationDevisWindow();

            //Création de l'objet temporaire
            Devis tmp = new Devis();

            //Mise de l'objet temporaire dans le datacontext
            creationdevisWindow.DataContext = tmp;

            //Mise en place des paramètres de la fenêtre
            creationdevisWindow.creation = true;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = creationdevisWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet commande se trouvant dans le datacontext de la fenêtre
                return (Devis)creationdevisWindow.DataContext;
            }
            else
            {
                try
                {
                    //On détache tous les élements liés à la commande Devis
                    ObservableCollection<Devis_Activite> toRemove = new ObservableCollection<Devis_Activite>();
                    foreach (Devis_Activite item in ((Devis)creationdevisWindow.DataContext).Devis_Activite)
                    {
                        toRemove.Add(item);
                    }
                    foreach (Devis_Activite item in toRemove)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    //On détache tous les élements liés à la commande devis_clause
                    ObservableCollection<Devis_Clause> toRemove1 = new ObservableCollection<Devis_Clause>();
                    foreach (Devis_Clause item in ((Devis)creationdevisWindow.DataContext).Devis_Clause)
                    {
                        toRemove1.Add(item);
                    }
                    foreach (Devis_Clause item in toRemove1)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    //On détache tous les élements liés à la commande devis_contact
                    ObservableCollection<Devis_Contact> toRemove2 = new ObservableCollection<Devis_Contact>();
                    foreach (Devis_Contact item in ((Devis)creationdevisWindow.DataContext).Devis_Contact)
                    {
                        toRemove2.Add(item);
                    }
                    foreach (Devis_Contact item in toRemove2)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    //On détache tous les élements liés à la devis Versions
                    ObservableCollection<Versions> toRemove3 = new ObservableCollection<Versions>();
                    foreach (Versions item in ((Devis)this._DataGridMain.SelectedItem).Versions)
                    {
                        toRemove3.Add(item);
                    }
                    foreach (Versions item in toRemove3)
                    {
                        if (item.Commande1 != null)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item.Commande1);
                        }
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }


                    //On détache la commande
                    ((App)App.Current).mySitaffEntities.Detach((Devis)creationdevisWindow.DataContext);
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un devis annulé : " + this.listDevis.Count() + " / " + this.max);

                return null;
            }
        }

        /// <summary>
        /// Ouvre le devis séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Devis Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un devis en cours ...");

                    //Création de la fenêtre
                    CreationDevisWindow creationdevisWindow = new CreationDevisWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la devis sélectionnée
                    creationdevisWindow.DataContext = new Devis();
                    creationdevisWindow.DataContext = (Devis)this._DataGridMain.SelectedItem;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = creationdevisWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet devis se trouvant dans le datacontext de la fenêtre
                        return (Devis)creationdevisWindow.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Devis)(this._DataGridMain.SelectedItem));
                        //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un devis annulé : " + this.listDevis.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime le devis séléctionnée avec une confirmation
        /// </summary>
        public Devis Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un devis en cours ...");

                    if (MessageBox.Show("Voulez-vous rééllement supprimer le devis séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //On détache tous les élements liés au devis devis_activité
                        ObservableCollection<Devis_Activite> toRemove = new ObservableCollection<Devis_Activite>();
                        foreach (Devis_Activite item in ((Devis)this._DataGridMain.SelectedItem).Devis_Activite)
                        {
                            toRemove.Add(item);
                        }
                        foreach (Devis_Activite item in toRemove)
                        {
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Devis_Activite.DeleteObject(item);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    ((Devis)this._DataGridMain.SelectedItem).Devis_Activite.Remove(item);
                                }
                                catch (Exception)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(item);
                                }
                            }
                        }

                        //On détache tous les élements liés à la devis devis_clause
                        ObservableCollection<Devis_Clause> toRemove1 = new ObservableCollection<Devis_Clause>();
                        foreach (Devis_Clause item in ((Devis)this._DataGridMain.SelectedItem).Devis_Clause)
                        {
                            toRemove1.Add(item);
                        }
                        foreach (Devis_Clause item in toRemove1)
                        {
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Devis_Clause.DeleteObject(item);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    ((Devis)this._DataGridMain.SelectedItem).Devis_Clause.Remove(item);
                                }
                                catch (Exception)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(item);
                                }
                            }
                        }

                        //On détache tous les élements liés à la devis devis_contact
                        ObservableCollection<Devis_Contact> toRemove2 = new ObservableCollection<Devis_Contact>();
                        foreach (Devis_Contact item in ((Devis)this._DataGridMain.SelectedItem).Devis_Contact)
                        {
                            toRemove2.Add(item);
                        }
                        foreach (Devis_Contact item in toRemove2)
                        {
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Devis_Contact.DeleteObject(item);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    ((Devis)this._DataGridMain.SelectedItem).Devis_Contact.Remove(item);
                                }
                                catch (Exception)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(item);
                                }
                            }
                        }

                        //On détache tous les élements liés à la devis Versions
                        ObservableCollection<Versions> toRemove3 = new ObservableCollection<Versions>();
                        foreach (Versions item in ((Devis)this._DataGridMain.SelectedItem).Versions)
                        {
                            toRemove3.Add(item);
                        }
                        foreach (Versions item in toRemove3)
                        {
                            if (item.Commande1 != null)
                            {
                                try
                                {
                                    ((App)App.Current).mySitaffEntities.Commande.DeleteObject(item.Commande1);
                                }
                                catch (Exception)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(item.Commande1);
                                }
                            }

                            try
                            {
                                ((App)App.Current).mySitaffEntities.Versions.DeleteObject(item);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    ((Devis)this._DataGridMain.SelectedItem).Versions.Remove(item);
                                }
                                catch (Exception)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(item);
                                }
                            }
                        }

                        //Supprimer l'élément
                        return (Devis)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un devis annulé : " + this.listDevis.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre la commande fournisseur séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Devis Look(Devis devis)
        {
            if (this._DataGridMain.SelectedItem != null || devis != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1 || devis != null)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une commande fournisseur en cours ...");

                    //Création de la fenêtre
                    CreationDevisWindow creationdevisWindow = new CreationDevisWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    creationdevisWindow.DataContext = new Devis();
                    if (devis != null)
                    {
                        creationdevisWindow.DataContext = devis;
                    }
                    else
                    {
                        creationdevisWindow.DataContext = (Devis)this._DataGridMain.SelectedItem;
                    }

                    //Je positionne la lecture seule sur la fenêtre
                    creationdevisWindow.Lecture_Seule();
                    creationdevisWindow.soloLecture = true;

                    //J'affiche la fenêtre
                    bool? dialogResult = creationdevisWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un devis terminé : " + this.listDevis.Count() + " / " + this.max);

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            _filterContainNumeroDevis.Text = "";
            _filterContainChargeAffaire.Text = "";
            _filterContainEtat.SelectedItem = null;
            _filterContainClient.Text = "";
            _filterContainLibelle.Text = "";
            _filterContainMontant.Text = "";
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
                if (_filterContainNumeroDevis.Text != "" || _filterContainChargeAffaire.Text != "" || _filterContainClient.Text != "" || _filterContainEtat.SelectedItem != null || _filterContainLibelle.Text != "" || _filterContainMontant.Text != "" || this.max != this.listDevis.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainNumeroDevis.Focus();
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

            ObservableCollection<Devis> listToPut = new ObservableCollection<Devis>(((App)App.Current).mySitaffEntities.Devis.OrderBy(dev => dev.Numero));

            if (this._filterContainNumeroDevis.Text != "")
            {
                listToPut = new ObservableCollection<Devis>(listToPut.Where(dev => dev.Numero.Trim().ToLower().Contains(this._filterContainNumeroDevis.Text.Trim().ToLower())));
            }
            if (this._filterContainChargeAffaire.Text != "")
            {
                listToPut = new ObservableCollection<Devis>(listToPut.Where(dev => dev.Salarie.Personne.fullname.Trim().ToLower().Contains(this._filterContainChargeAffaire.Text.Trim().ToLower()) || dev.Salarie.Personne.Initiales.Trim().ToLower().Contains(this._filterContainChargeAffaire.Text.Trim().ToLower())));
            }
            if (this._filterContainClient.Text != "")
            {
                listToPut = new ObservableCollection<Devis>(listToPut.Where(dev => dev.Client.Entreprise.Libelle.Trim().ToLower().Contains(this._filterContainClient.Text.Trim().ToLower())));
            }
            if (this._filterContainLibelle.Text != "")
            {
                listToPut = new ObservableCollection<Devis>(listToPut.Where(dev => dev.Libelle.Trim().ToLower().Contains(this._filterContainLibelle.Text.Trim().ToLower())));
            }
            if (this._filterContainEtat.SelectedItem != null)
            {
                listToPut = new ObservableCollection<Devis>(listToPut.Where(dev => dev.EtatDuDevis == ((EtatDevis)this._filterContainEtat.SelectedItem).chaine));
            }
            if (this._filterContainMontant.Text != "")
            {
                double val;
                if (double.TryParse(this._filterContainMontant.Text.Trim(), out val))
                {
                    if (double.Parse(this._filterContainMontant.Text.Trim()) == 0)
                    {
                        listToPut = new ObservableCollection<Devis>(listToPut.Where(com => com.montantDerniereVersions == 0));
                    }
                    else
                    {
                        listToPut = new ObservableCollection<Devis>(listToPut.Where(com => com.montantDerniereVersions.ToString().Contains(double.Parse(this._filterContainMontant.Text.Trim()).ToString())));
                    }
                }
            }

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            if (this.listDevis.Count() == 0)
            {
                MessageBox.Show("Aucun résultat ne correspont à votre recherche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
            this.max = ((App)App.Current).mySitaffEntities.Devis.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Devis dev)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des devis terminé : " + this.listDevis.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listDevis.Add(dev);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un devis numéro '" + dev.Numero + "' effectué avec succès. Nombre d'élements : " + this.listDevis.Count() + " / " + this.max);
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification du devis numéro : '" + dev.Numero + "' effectué avec succès. Nombre d'élements : " + this.listDevis.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listDevis.Remove(dev);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression du devis numéro : '" + dev.Numero + "' effectué avec succès. Nombre d'élements : " + this.listDevis.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else if (typeEtat == "PassageAffaire")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Passage en affaire du devis numéro : '" + dev.Numero + "' effectué avec succès. Nombre d'élements : " + this.listDevis.Count() + " / " + this.max);
            }
            else
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des devis terminé : " + this.listDevis.Count() + " / " + this.max);
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
            this.listDevis = new ObservableCollection<Devis>(this.listDevis.OrderBy(nf => nf.Numero));
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
