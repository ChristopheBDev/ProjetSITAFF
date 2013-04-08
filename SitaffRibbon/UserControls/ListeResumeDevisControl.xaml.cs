using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeResumeDevisControl.xaml
    /// </summary>
    public partial class ListeResumeDevisControl : UserControl
    {

        //TO DO Colonne AffMas
        //TO DO MEttre les colonnes dans le xaml

        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_ColonneNumero;
        MenuItem MenuItem_ColonneLibelle;
        MenuItem MenuItem_ColonneVersionsNumero;
        MenuItem MenuItem_ColonneChargeAffaire;
        MenuItem MenuItem_ColonneMontant;
        MenuItem MenuItem_ColonneMontant_Options;
        MenuItem MenuItem_ColonneTaux_Remise;
        MenuItem MenuItem_ColonneRemise;
        MenuItem MenuItem_ColonneMontant_Remise;
        MenuItem MenuItem_ColonneVersion_TypeLibelle;
        MenuItem MenuItem_ColonneNumero_Commande;
        MenuItem MenuItem_ColonneAffaireNumero;
        MenuItem MenuItem_ColonneQte_Main_Oeuvre;
        MenuItem MenuItem_ColonneDate_Commande;
        MenuItem MenuItem_ColonneTaux_Horaire;
        MenuItem MenuItem_ColonnePrix_Achat_Fourniture;
        MenuItem MenuItem_ColonnePrix_Achat_SsTraitance;
        MenuItem MenuItem_ColonnePrix_Vente_Fourniture;
        MenuItem MenuItem_ColonnePrix_Vente_SsTraitance;
        MenuItem MenuItem_ColonnePrix_Total_Vente_Heure;
        MenuItem MenuItem_ColonneEtatDuDevisVersion;
        MenuItem MenuItem_ColonneTotalFacture;
        MenuItem MenuItem_ColonneTotalRestantAFacture;



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
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(ListeResumeDevisControl), new UIPropertyMetadata(null));

        public ObservableCollection<Entreprise> listEntreprise
        {
            get { return (ObservableCollection<Entreprise>)GetValue(listEntrepriseProperty); }
            set { SetValue(listEntrepriseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listDevis.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseProperty =
            DependencyProperty.Register("listEntreprise", typeof(ObservableCollection<Entreprise>), typeof(ListeResumeDevisControl), new UIPropertyMetadata(null));

        public ObservableCollection<Versions> listVersions
        {
            get { return (ObservableCollection<Versions>)GetValue(listVersionsProperty); }
            set { SetValue(listVersionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listDevis.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listVersionsProperty =
            DependencyProperty.Register("listVersions", typeof(ObservableCollection<Versions>), typeof(ListeResumeDevisControl), new UIPropertyMetadata(null));



        public ObservableCollection<Version_Type> listTypeVersion
        {
            get { return (ObservableCollection<Version_Type>)GetValue(listTypeVersionProperty); }
            set { SetValue(listTypeVersionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listTypeVersion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listTypeVersionProperty =
            DependencyProperty.Register("listTypeVersion", typeof(ObservableCollection<Version_Type>), typeof(ListeResumeDevisControl), new PropertyMetadata(null));



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

        public ListeResumeDevisControl()
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

            MenuItem itemAfficher = RemplirMenuAfficherMasquerColonnes(new MenuItem());
            itemAfficher.Header = "Afficher / Masquer";

            contextMenu.Items.Add(itemAfficher);
        }

        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {
            this.MenuItem_ColonneNumero = new MenuItem();
            this.MenuItem_ColonneNumero.IsChecked = false;
            this.MenuItem_ColonneNumero.Header = "Devis n°";
            this.MenuItem_ColonneNumero.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneNumero, this._ColonneNumero); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneNumero, this._ColonneNumero);
            menuItem.Items.Add(this.MenuItem_ColonneNumero);

            this.MenuItem_ColonneLibelle = new MenuItem();
            this.MenuItem_ColonneLibelle.IsChecked = false;
            this.MenuItem_ColonneLibelle.Header = "Client principal";
            this.MenuItem_ColonneLibelle.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneLibelle, this._ColonneLibelle); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneLibelle, this._ColonneLibelle);
            menuItem.Items.Add(this.MenuItem_ColonneLibelle);

            this.MenuItem_ColonneVersionsNumero = new MenuItem();
            this.MenuItem_ColonneVersionsNumero.IsChecked = false;
            this.MenuItem_ColonneVersionsNumero.Header = "Numero Version";
            this.MenuItem_ColonneVersionsNumero.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneVersionsNumero, this._ColonneVersionsNumero); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneVersionsNumero, this._ColonneVersionsNumero);
            menuItem.Items.Add(this.MenuItem_ColonneVersionsNumero);

            this.MenuItem_ColonneChargeAffaire = new MenuItem();
            this.MenuItem_ColonneChargeAffaire.IsChecked = false;
            this.MenuItem_ColonneChargeAffaire.Header = "Chargé d'affaire";
            this.MenuItem_ColonneChargeAffaire.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneChargeAffaire, this._ColonneChargeAffaire); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneChargeAffaire, this._ColonneChargeAffaire);
            menuItem.Items.Add(this.MenuItem_ColonneChargeAffaire);

            this.MenuItem_ColonneMontant = new MenuItem();
            this.MenuItem_ColonneMontant.IsChecked = false;
            this.MenuItem_ColonneMontant.Header = "Montant";
            this.MenuItem_ColonneMontant.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneMontant, this._ColonneMontant); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneMontant, this._ColonneMontant);
            menuItem.Items.Add(this.MenuItem_ColonneMontant);

            this.MenuItem_ColonneMontant_Options = new MenuItem();
            this.MenuItem_ColonneMontant_Options.IsChecked = true;
            this.MenuItem_ColonneMontant_Options.Header = "Montant options";
            this.MenuItem_ColonneMontant_Options.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneMontant_Options, this._ColonneMontant_Options); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneMontant_Options, this._ColonneMontant_Options);
            menuItem.Items.Add(this.MenuItem_ColonneMontant_Options);

            this.MenuItem_ColonneTaux_Remise = new MenuItem();
            this.MenuItem_ColonneTaux_Remise.IsChecked = true;
            this.MenuItem_ColonneTaux_Remise.Header = "Taux de remise";
            this.MenuItem_ColonneTaux_Remise.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTaux_Remise, this._ColonneTaux_Remise); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTaux_Remise, this._ColonneTaux_Remise);
            menuItem.Items.Add(this.MenuItem_ColonneTaux_Remise);

            this.MenuItem_ColonneRemise = new MenuItem();
            this.MenuItem_ColonneRemise.IsChecked = true;
            this.MenuItem_ColonneRemise.Header = "Montant de la remise";
            this.MenuItem_ColonneRemise.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneRemise, this._ColonneRemise); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneRemise, this._ColonneRemise);
            menuItem.Items.Add(this.MenuItem_ColonneRemise);

            this.MenuItem_ColonneMontant_Remise = new MenuItem();
            this.MenuItem_ColonneMontant_Remise.IsChecked = false;
            this.MenuItem_ColonneMontant_Remise.Header = "Commande remisée";
            this.MenuItem_ColonneMontant_Remise.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneMontant_Remise, this._ColonneMontant_Remise); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneMontant_Remise, this._ColonneMontant_Remise);
            menuItem.Items.Add(this.MenuItem_ColonneMontant_Remise);

            this.MenuItem_ColonneVersion_TypeLibelle = new MenuItem();
            this.MenuItem_ColonneVersion_TypeLibelle.IsChecked = false;
            this.MenuItem_ColonneVersion_TypeLibelle.Header = "Type de version";
            this.MenuItem_ColonneVersion_TypeLibelle.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneVersion_TypeLibelle, this._ColonneVersion_TypeLibelle); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneVersion_TypeLibelle, this._ColonneVersion_TypeLibelle);
            menuItem.Items.Add(this.MenuItem_ColonneVersion_TypeLibelle);

            this.MenuItem_ColonneNumero_Commande = new MenuItem();
            this.MenuItem_ColonneNumero_Commande.IsChecked = true;
            this.MenuItem_ColonneNumero_Commande.Header = "Numéro de commande";
            this.MenuItem_ColonneNumero_Commande.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneNumero_Commande, this._ColonneNumero_Commande); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneNumero_Commande, this._ColonneNumero_Commande);
            menuItem.Items.Add(this.MenuItem_ColonneNumero_Commande);

            this.MenuItem_ColonneAffaireNumero = new MenuItem();
            this.MenuItem_ColonneAffaireNumero.IsChecked = false;
            this.MenuItem_ColonneAffaireNumero.Header = "Affaire";
            this.MenuItem_ColonneAffaireNumero.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneAffaireNumero, this._ColonneAffaireNumero); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneAffaireNumero, this._ColonneAffaireNumero);
            menuItem.Items.Add(this.MenuItem_ColonneAffaireNumero);

            this.MenuItem_ColonneQte_Main_Oeuvre = new MenuItem();
            this.MenuItem_ColonneQte_Main_Oeuvre.IsChecked = true;
            this.MenuItem_ColonneQte_Main_Oeuvre.Header = "Quantité M.O";
            this.MenuItem_ColonneQte_Main_Oeuvre.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneQte_Main_Oeuvre, this._ColonneQte_Main_Oeuvre); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneQte_Main_Oeuvre, this._ColonneQte_Main_Oeuvre);
            menuItem.Items.Add(this.MenuItem_ColonneQte_Main_Oeuvre);

            this.MenuItem_ColonneDate_Commande = new MenuItem();
            this.MenuItem_ColonneDate_Commande.IsChecked = true;
            this.MenuItem_ColonneDate_Commande.Header = "Date de commande";
            this.MenuItem_ColonneDate_Commande.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneDate_Commande, this._ColonneDate_Commande); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneDate_Commande, this._ColonneDate_Commande);
            menuItem.Items.Add(this.MenuItem_ColonneDate_Commande);

            this.MenuItem_ColonneTaux_Horaire = new MenuItem();
            this.MenuItem_ColonneTaux_Horaire.IsChecked = true;
            this.MenuItem_ColonneTaux_Horaire.Header = "Taux horaire";
            this.MenuItem_ColonneTaux_Horaire.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTaux_Horaire, this._ColonneTaux_Horaire); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTaux_Horaire, this._ColonneTaux_Horaire);
            menuItem.Items.Add(this.MenuItem_ColonneTaux_Horaire);

            this.MenuItem_ColonnePrix_Achat_Fourniture = new MenuItem();
            this.MenuItem_ColonnePrix_Achat_Fourniture.IsChecked = true;
            this.MenuItem_ColonnePrix_Achat_Fourniture.Header = "Prix achat fourniture";
            this.MenuItem_ColonnePrix_Achat_Fourniture.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Achat_Fourniture, this._ColonnePrix_Achat_Fourniture); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Achat_Fourniture, this._ColonnePrix_Achat_Fourniture);
            menuItem.Items.Add(this.MenuItem_ColonnePrix_Achat_Fourniture);

            this.MenuItem_ColonnePrix_Achat_SsTraitance = new MenuItem();
            this.MenuItem_ColonnePrix_Achat_SsTraitance.IsChecked = true;
            this.MenuItem_ColonnePrix_Achat_SsTraitance.Header = "Prix achat sous traitance";
            this.MenuItem_ColonnePrix_Achat_SsTraitance.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Achat_SsTraitance, this._ColonnePrix_Achat_SsTraitance); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Achat_SsTraitance, this._ColonnePrix_Achat_SsTraitance);
            menuItem.Items.Add(this.MenuItem_ColonnePrix_Achat_SsTraitance);

            this.MenuItem_ColonnePrix_Vente_Fourniture = new MenuItem();
            this.MenuItem_ColonnePrix_Vente_Fourniture.IsChecked = true;
            this.MenuItem_ColonnePrix_Vente_Fourniture.Header = "Prix vente fourniture";
            this.MenuItem_ColonnePrix_Vente_Fourniture.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Vente_Fourniture, this._ColonnePrix_Vente_Fourniture); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Vente_Fourniture, this._ColonnePrix_Vente_Fourniture);
            menuItem.Items.Add(this.MenuItem_ColonnePrix_Vente_Fourniture);

            this.MenuItem_ColonnePrix_Vente_SsTraitance = new MenuItem();
            this.MenuItem_ColonnePrix_Vente_SsTraitance.IsChecked = true;
            this.MenuItem_ColonnePrix_Vente_SsTraitance.Header = "Prix vente sous traitance";
            this.MenuItem_ColonnePrix_Vente_SsTraitance.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Vente_SsTraitance, this._ColonnePrix_Vente_SsTraitance); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Vente_SsTraitance, this._ColonnePrix_Vente_SsTraitance);
            menuItem.Items.Add(this.MenuItem_ColonnePrix_Vente_SsTraitance);

            this.MenuItem_ColonnePrix_Total_Vente_Heure = new MenuItem();
            this.MenuItem_ColonnePrix_Total_Vente_Heure.IsChecked = true;
            this.MenuItem_ColonnePrix_Total_Vente_Heure.Header = "Prix total vente heures";
            this.MenuItem_ColonnePrix_Total_Vente_Heure.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Total_Vente_Heure, this._ColonnePrix_Total_Vente_Heure); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Total_Vente_Heure, this._ColonnePrix_Total_Vente_Heure);
            menuItem.Items.Add(this.MenuItem_ColonnePrix_Total_Vente_Heure);

            this.MenuItem_ColonneEtatDuDevisVersion = new MenuItem();
            this.MenuItem_ColonneEtatDuDevisVersion.IsChecked = false;
            this.MenuItem_ColonneEtatDuDevisVersion.Header = "Etat de la version";
            this.MenuItem_ColonneEtatDuDevisVersion.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneEtatDuDevisVersion, this._ColonneEtatDuDevisVersion); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneEtatDuDevisVersion, this._ColonneEtatDuDevisVersion);
            menuItem.Items.Add(this.MenuItem_ColonneEtatDuDevisVersion);

            this.MenuItem_ColonneTotalFacture = new MenuItem();
            this.MenuItem_ColonneTotalFacture.IsChecked = false;
            this.MenuItem_ColonneTotalFacture.Header = "Total facturé";
            this.MenuItem_ColonneTotalFacture.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTotalFacture, this._ColonneTotalFacture); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTotalFacture, this._ColonneTotalFacture);
            menuItem.Items.Add(this.MenuItem_ColonneTotalFacture);

            this.MenuItem_ColonneTotalRestantAFacture = new MenuItem();
            this.MenuItem_ColonneTotalRestantAFacture.IsChecked = false;
            this.MenuItem_ColonneTotalRestantAFacture.Header = "Total restant à facturer";
            this.MenuItem_ColonneTotalRestantAFacture.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTotalRestantAFacture, this._ColonneTotalRestantAFacture); });
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTotalRestantAFacture, this._ColonneTotalRestantAFacture);
            menuItem.Items.Add(this.MenuItem_ColonneTotalRestantAFacture);

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

        //TO DO Afficher TOUT - Masquer TOUT

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneNumero.IsChecked = false;
            this.MenuItem_ColonneLibelle.IsChecked = false;
            this.MenuItem_ColonneVersionsNumero.IsChecked = false;
            this.MenuItem_ColonneChargeAffaire.IsChecked = false;
            this.MenuItem_ColonneMontant.IsChecked = false;
            this.MenuItem_ColonneMontant_Options.IsChecked = false;
            this.MenuItem_ColonneTaux_Remise.IsChecked = false;
            this.MenuItem_ColonneRemise.IsChecked = false;
            this.MenuItem_ColonneMontant_Remise.IsChecked = false;
            this.MenuItem_ColonneVersion_TypeLibelle.IsChecked = false;
            this.MenuItem_ColonneNumero_Commande.IsChecked = false;
            this.MenuItem_ColonneQte_Main_Oeuvre.IsChecked = false;
            this.MenuItem_ColonneDate_Commande.IsChecked = false;
            this.MenuItem_ColonneTaux_Horaire.IsChecked = false;
            this.MenuItem_ColonnePrix_Achat_Fourniture.IsChecked = false;
            this.MenuItem_ColonnePrix_Achat_SsTraitance.IsChecked = false;
            this.MenuItem_ColonnePrix_Vente_Fourniture.IsChecked = false;
            this.MenuItem_ColonnePrix_Vente_SsTraitance.IsChecked = false;
            this.MenuItem_ColonnePrix_Total_Vente_Heure.IsChecked = false;
            this.MenuItem_ColonneEtatDuDevisVersion.IsChecked = false;
            this.MenuItem_ColonneTotalFacture.IsChecked = false;
            this.MenuItem_ColonneTotalRestantAFacture.IsChecked = false;
            this.MenuItem_ColonneAffaireNumero.IsChecked = false;

            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneNumero, this._ColonneNumero);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneLibelle, this._ColonneLibelle);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneVersionsNumero, this._ColonneVersionsNumero);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneChargeAffaire, this._ColonneChargeAffaire);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneMontant, this._ColonneMontant);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneMontant_Options, this._ColonneMontant_Options);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTaux_Remise, this._ColonneTaux_Remise);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneRemise, this._ColonneRemise);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneMontant_Remise, this._ColonneMontant_Remise);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneVersion_TypeLibelle, this._ColonneVersion_TypeLibelle);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneNumero_Commande, this._ColonneNumero_Commande);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneQte_Main_Oeuvre, this._ColonneQte_Main_Oeuvre);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneDate_Commande, this._ColonneDate_Commande);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTaux_Horaire, this._ColonneTaux_Horaire);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Achat_Fourniture, this._ColonnePrix_Achat_Fourniture);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Achat_SsTraitance, this._ColonnePrix_Achat_SsTraitance);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Vente_Fourniture, this._ColonnePrix_Vente_Fourniture);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Vente_SsTraitance, this._ColonnePrix_Vente_SsTraitance);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Total_Vente_Heure, this._ColonnePrix_Total_Vente_Heure);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneEtatDuDevisVersion, this._ColonneEtatDuDevisVersion);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTotalFacture, this._ColonneTotalFacture);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTotalRestantAFacture, this._ColonneTotalRestantAFacture);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneAffaireNumero, this._ColonneAffaireNumero);
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneNumero.IsChecked = true;
            this.MenuItem_ColonneLibelle.IsChecked = true;
            this.MenuItem_ColonneVersionsNumero.IsChecked = true;
            this.MenuItem_ColonneChargeAffaire.IsChecked = true;
            this.MenuItem_ColonneMontant.IsChecked = true;
            this.MenuItem_ColonneMontant_Options.IsChecked = true;
            this.MenuItem_ColonneTaux_Remise.IsChecked = true;
            this.MenuItem_ColonneRemise.IsChecked = true;
            this.MenuItem_ColonneMontant_Remise.IsChecked = true;
            this.MenuItem_ColonneVersion_TypeLibelle.IsChecked = true;
            this.MenuItem_ColonneNumero_Commande.IsChecked = true;
            this.MenuItem_ColonneQte_Main_Oeuvre.IsChecked = true;
            this.MenuItem_ColonneDate_Commande.IsChecked = true;
            this.MenuItem_ColonneTaux_Horaire.IsChecked = true;
            this.MenuItem_ColonnePrix_Achat_Fourniture.IsChecked = true;
            this.MenuItem_ColonnePrix_Achat_SsTraitance.IsChecked = true;
            this.MenuItem_ColonnePrix_Vente_Fourniture.IsChecked = true;
            this.MenuItem_ColonnePrix_Vente_SsTraitance.IsChecked = true;
            this.MenuItem_ColonnePrix_Total_Vente_Heure.IsChecked = true;
            this.MenuItem_ColonneEtatDuDevisVersion.IsChecked = true;
            this.MenuItem_ColonneTotalFacture.IsChecked = true;
            this.MenuItem_ColonneTotalRestantAFacture.IsChecked = true;
            this.MenuItem_ColonneAffaireNumero.IsChecked = true;

            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneNumero, this._ColonneNumero);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneLibelle, this._ColonneLibelle);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneVersionsNumero, this._ColonneVersionsNumero);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneChargeAffaire, this._ColonneChargeAffaire);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneMontant, this._ColonneMontant);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneMontant_Options, this._ColonneMontant_Options);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTaux_Remise, this._ColonneTaux_Remise);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneRemise, this._ColonneRemise);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneMontant_Remise, this._ColonneMontant_Remise);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneVersion_TypeLibelle, this._ColonneVersion_TypeLibelle);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneNumero_Commande, this._ColonneNumero_Commande);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneQte_Main_Oeuvre, this._ColonneQte_Main_Oeuvre);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneDate_Commande, this._ColonneDate_Commande);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTaux_Horaire, this._ColonneTaux_Horaire);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Achat_Fourniture, this._ColonnePrix_Achat_Fourniture);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Achat_SsTraitance, this._ColonnePrix_Achat_SsTraitance);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Vente_Fourniture, this._ColonnePrix_Vente_Fourniture);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Vente_SsTraitance, this._ColonnePrix_Vente_SsTraitance);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonnePrix_Total_Vente_Heure, this._ColonnePrix_Total_Vente_Heure);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneEtatDuDevisVersion, this._ColonneEtatDuDevisVersion);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTotalFacture, this._ColonneTotalFacture);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneTotalRestantAFacture, this._ColonneTotalRestantAFacture);
            ((App)App.Current)._afficherMasquer.AffMas_Colonne(this.MenuItem_ColonneAffaireNumero, this._ColonneAffaireNumero);
        }

        #endregion

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
            List<string> listAffaire = new List<string>();

            foreach (Versions item in ((App)App.Current).mySitaffEntities.Versions.Where(ver => ver.Devis1 != null))
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
                if (item.Devis1 != null)
                {
                    if (item.Devis1.Client2 != null)
                    {
                        if (item.Devis1.Client2.Entreprise != null)
                        {
                            if (!listClient.Contains(item.Devis1.Client2.Entreprise.Libelle))
                            {
                                listClient.Add(item.Devis1.Client2.Entreprise.Libelle);
                            }
                        }
                    }
                }

                //Pour remplir les affaires
                if (item.Affaire1 != null)
                {
                    if (item.Affaire1.Numero != null)
                    {
                        if (!listAffaire.Contains(item.Affaire1.Numero))
                        {
                            listAffaire.Add(item.Affaire1.Numero);
                        }
                    }
                }

            }

            _filterContainChargeAffaire.ItemsSource = listChargeAffaire;
            _filterContainClient.ItemsSource = listClient;
            _filterContainAffaire.ItemsSource = listAffaire;
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

        private void initialisationComboBox()
        {
            this.listTypeVersion = new ObservableCollection<Version_Type>(((App)App.Current).mySitaffEntities.Version_Type.OrderBy(vt => vt.Libelle));
        }

        #endregion

        #region initialisation Donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<Versions> listToPut)
        {
            if (listToPut == null)
            {
                this.listVersions = new ObservableCollection<Versions>(((App)App.Current).mySitaffEntities.Versions.Where(ccf => ccf.Devis1 != null).OrderBy(ccf => ccf.Devis1.Numero));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listVersions = new ObservableCollection<Versions>(listToPut);
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

        #region filtrage

        #region remise a zero

        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            _filterContainTypeVersion.Text = "";
            _filterContainChargeAffaire.Text = "";
            _filterContainEtat.SelectedItem = null;
            _filterContainClient.Text = "";
            _filterContainLibelle.Text = "";
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
                if (_filterContainTypeVersion.Text != "" || _filterContainChargeAffaire.Text != "" || _filterContainClient.Text != "" || _filterContainEtat.SelectedItem != null || _filterContainLibelle.Text != "" || this.max != this.listVersions.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainTypeVersion.Focus();
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

            ObservableCollection<Versions> listToPut = new ObservableCollection<Versions>(((App)App.Current).mySitaffEntities.Versions.Where(dev => dev.Devis1 != null).OrderBy(dev => dev.Numero));

            if (this._filterContainTypeVersion.SelectedItem != null)
            {
                listToPut = new ObservableCollection<Versions>(listToPut.Where(dev => dev.Version_Type1 != null));
                listToPut = new ObservableCollection<Versions>(listToPut.Where(dev => dev.Version_Type1.Identifiant == ((Version_Type)this._filterContainTypeVersion.SelectedItem).Identifiant));
            }
            if (this._filterContainChargeAffaire.Text != "")
            {
                listToPut = new ObservableCollection<Versions>(listToPut.Where(dev => dev.Salarie != null));
                listToPut = new ObservableCollection<Versions>(listToPut.Where(dev => dev.Salarie.Personne != null));
                listToPut = new ObservableCollection<Versions>(listToPut.Where(dev => dev.Salarie.Personne.fullname.Trim().ToLower().Contains(this._filterContainChargeAffaire.Text.Trim().ToLower()) || dev.Salarie.Personne.Initiales.Trim().ToLower().Contains(this._filterContainChargeAffaire.Text.Trim().ToLower())));
            }
            if (this._filterContainClient.Text != "")
            {
                listToPut = new ObservableCollection<Versions>(listToPut.Where(dev => dev.Devis1.Client != null));
                listToPut = new ObservableCollection<Versions>(listToPut.Where(dev => dev.Devis1.Client.Entreprise != null));
                listToPut = new ObservableCollection<Versions>(listToPut.Where(dev => dev.Devis1.Client.Entreprise.Libelle.Trim().ToLower().Contains(this._filterContainClient.Text.Trim().ToLower())));
            }
            if (this._filterContainLibelle.Text != "")
            {
                listToPut = new ObservableCollection<Versions>(listToPut.Where(dev => dev.Commentaire.Trim().ToLower().Contains(this._filterContainLibelle.Text.Trim().ToLower())));
            }
            if (this._filterContainEtat.SelectedItem != null)
            {
                listToPut = new ObservableCollection<Versions>(listToPut.Where(dev => dev.EtatDuDevisVersion == ((EtatDevis)this._filterContainEtat.SelectedItem).chaine));
            }
            if (this._filterContainAffaire.Text != "")
            {
                listToPut = new ObservableCollection<Versions>(listToPut.Where(dev => dev.Affaire1 != null));
                listToPut = new ObservableCollection<Versions>(listToPut.Where(dev => dev.Affaire1.Numero != null));
                listToPut = new ObservableCollection<Versions>(listToPut.Where(dev => dev.Affaire1.Numero.Trim().ToLower().Contains(this._filterContainAffaire.Text.Trim().ToLower())));
            }
            if (this._filterContainMontant.Text != "")
            {
                double val;
                if (double.TryParse(this._filterContainMontant.Text.Trim(), out val))
                {
                    if (double.Parse(this._filterContainMontant.Text.Trim()) == 0)
                    {
                        listToPut = new ObservableCollection<Versions>(listToPut.Where(com => com.Montant == 0 || com.Montant_Remise == 0));
                    }
                    else
                    {
                        listToPut = new ObservableCollection<Versions>(listToPut.Where(com => com.Montant.ToString().Contains(double.Parse(this._filterContainMontant.Text.Trim()).ToString()) || com.Montant_Remise.ToString().Contains(double.Parse(this._filterContainMontant.Text.Trim()).ToString())));
                    }
                }
            }

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            if (this.listVersions.Count() == 0)
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
            this.max = ((App)App.Current).mySitaffEntities.Versions.Count();
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
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des versions devis terminé : " + this.listVersions.Count() + " / " + this.max);
            }
            else
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des versions devis terminé : " + this.listVersions.Count() + " / " + this.max);
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
            this.listVersions = new ObservableCollection<Versions>(this.listVersions.Where(dev => dev.Devis1 != null).OrderBy(nf => nf.Devis1.Numero));
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
