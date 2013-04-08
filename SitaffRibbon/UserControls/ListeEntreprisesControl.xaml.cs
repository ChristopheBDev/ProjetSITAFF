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
/* 
 * Using pour utilisation des IObservableCollection (afin d'éviter de mettre
 * System.Collections.ObjectModel.IObservableCollection en entier)
 */
using System.Collections.ObjectModel;
using System.Threading;
using System.ComponentModel;
using SitaffRibbon.Classes;

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeEntreprisesControl.xaml
    /// </summary>
    public partial class ListeEntreprisesControl : UserControl
    {

        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_ColonneTypeEntreprise;
        MenuItem MenuItem_ColonneLibelle;
        MenuItem MenuItem_ColonneAdresse;
        MenuItem MenuItem_ColonneCodePostal;
        MenuItem MenuItem_ColonneVille;
        MenuItem MenuItem_ColonneTelephone;
        MenuItem MenuItem_ColonneFax;
        MenuItem MenuItem_ColonneClientFournisseur;
        MenuItem MenuItem_ColonneSiteWeb;
        MenuItem MenuItem_ColonneEMail;
        MenuItem MenuItem_ColonneSiret;
        MenuItem MenuItem_ColonneAPE;
        MenuItem MenuItem_ColonneDevise;
        MenuItem MenuItem_ColonneGroupe;
        MenuItem MenuItem_ColonneModeFacturation;
        MenuItem MenuItem_ColonneFournisseurDelaisLivraison;
        MenuItem MenuItem_ColonneFournisseurDelaisConsultation;
        MenuItem MenuItem_ColonneFournisseurDelaisIncertitude;
        MenuItem MenuItem_ColonneFournisseurRemiseCommerciale;
        MenuItem MenuItem_ColonneFournisseurEscompte;
        MenuItem MenuItem_ColonneFournisseurCode;
        MenuItem MenuItem_ColonneFournisseurFraisDePort;
        MenuItem MenuItem_ColonneFournisseurTVA;
        MenuItem MenuItem_ColonneClientCommandeMinimum;
        MenuItem MenuItem_ColonneClientRemiseCommerciale;
        MenuItem MenuItem_ColonneClientEscompte;
        MenuItem MenuItem_ColonneClientNbExemplaireFacture;
        MenuItem MenuItem_ColonneClientCode;
        MenuItem MenuItem_ColonneClientTVA;


        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Propriétés de dépendances

        public ObservableCollection<Entreprise> Entreprises
        {
            get { return (ObservableCollection<Entreprise>)GetValue(EntreprisesProperty); }
            set { SetValue(EntreprisesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Entreprises.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EntreprisesProperty =
            DependencyProperty.Register("Entreprises", typeof(ObservableCollection<Entreprise>), typeof(ListeEntreprisesControl), new UIPropertyMetadata(null));



        public ObservableCollection<Statut> listStatut
        {
            get { return (ObservableCollection<Statut>)GetValue(listStatutProperty); }
            set { SetValue(listStatutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listStatut.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listStatutProperty =
            DependencyProperty.Register("listStatut", typeof(ObservableCollection<Statut>), typeof(ListeEntreprisesControl), new PropertyMetadata(null));



        #endregion

        #region Constructeur

        public ListeEntreprisesControl()
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
            this.listStatut = new ObservableCollection<Statut>(((App)App.Current).mySitaffEntities.Statut.OrderBy(sta => sta.Libelle));

            List<string> listGroupe = new List<string>();
            List<string> listVille = new List<string>();
            List<string> listPays = new List<string>();
            List<string> listActivite = new List<string>();
            foreach (Entreprise item in ((App)App.Current).mySitaffEntities.Entreprise)
            {
                //Pour remplir les groupes
                if (item.Groupe1 != null)
                {
                    if (!listGroupe.Contains(item.Groupe1.Libelle))
                    {
                        listGroupe.Add(item.Groupe1.Libelle);
                    }
                }

                //Pour remplir les villes
                if (item.Adresse1 != null)
                {
                    if (item.Adresse1.Ville1 != null)
                    {
                        if (!listVille.Contains(item.Adresse1.Ville1.Libelle))
                        {
                            listVille.Add(item.Adresse1.Ville1.Libelle);
                        }
                    }
                }

                //Pour remplir les pays
                if (item.Adresse1 != null)
                {
                    if (item.Adresse1.Ville1 != null)
                    {
                        if (item.Adresse1.Ville1.Pays1 != null)
                        {
                            if (!listPays.Contains(item.Adresse1.Ville1.Pays1.Libelle))
                            {
                                listPays.Add(item.Adresse1.Ville1.Pays1.Libelle);
                            }
                        }
                    }
                }

                //Pour remplir les activite
                foreach (Entreprise_Activite itemea in item.Entreprise_Activite)
                {
                    if (itemea.Activite1 != null)
                    {
                        if (!listActivite.Contains(itemea.Activite1.Libelle))
                        {
                            listActivite.Add(itemea.Activite1.Libelle);
                        }
                    }
                }
            }
            this._filterContainGroupe.ItemsSource = listGroupe;
            this._filterContainVille.ItemsSource = listVille;
            this._filterContainPays.ItemsSource = listPays;
            this._filterContainActivite.ItemsSource = listActivite;
        }

        private void initialisationComboBoxOuiNon()
        {
            ObservableCollection<ItemOuiNon> listClientFournisseur = new ObservableCollection<ItemOuiNon>();
            listClientFournisseur.Add(new ItemOuiNon("Fournisseur"));
            listClientFournisseur.Add(new ItemOuiNon("Client"));
            listClientFournisseur.Add(new ItemOuiNon("Client - Fournisseur"));
            listClientFournisseur.Add(new ItemOuiNon("Entreprise ni cliente ni fournisseur"));
            this._filterContainClientFournisseur.ItemsSource = listClientFournisseur;
        }

        #endregion

        #region initialisation Donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<Entreprise> listToPut)
        {
            if (listToPut == null)
            {
                this.Entreprises = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.OrderBy(ent => ent.Libelle));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.Entreprises = new ObservableCollection<Entreprise>(listToPut);
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
            itemAfficher5.Header = "Différences prix";

            MenuItem itemAfficher6 = new MenuItem();
            itemAfficher6.Header = "Différences de prix produit général";
            itemAfficher6.Click += new RoutedEventHandler(delegate { this.RapportImprimerdifferenceprixgeneral(); });
            itemAfficher5.Items.Add(itemAfficher6);

            MenuItem itemAfficher7 = new MenuItem();
            itemAfficher7.Header = "Différences de prix produit fournisseur";
            itemAfficher7.Click += new RoutedEventHandler(delegate { this.RapportImprimerdifferenceprix(); });
            itemAfficher5.Items.Add(itemAfficher7);

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
            this.MenuItem_ColonneTypeEntreprise = new MenuItem();
            this.MenuItem_ColonneTypeEntreprise.IsChecked = true;
            this.MenuItem_ColonneTypeEntreprise.Header = "Type d'entreprise";
            this.MenuItem_ColonneTypeEntreprise.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTypeEntreprise(); });
            this.AffMas_ColonneTypeEntreprise();
            menuItem.Items.Add(this.MenuItem_ColonneTypeEntreprise);

            this.MenuItem_ColonneLibelle = new MenuItem();
            this.MenuItem_ColonneLibelle.IsChecked = false;
            this.MenuItem_ColonneLibelle.Header = "Nom de l'entreprise";
            this.MenuItem_ColonneLibelle.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneLibelle(); });
            this.AffMas_ColonneLibelle();
            menuItem.Items.Add(this.MenuItem_ColonneLibelle);

            this.MenuItem_ColonneAdresse = new MenuItem();
            this.MenuItem_ColonneAdresse.IsChecked = false;
            this.MenuItem_ColonneAdresse.Header = "Adresse";
            this.MenuItem_ColonneAdresse.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneAdresse(); });
            this.AffMas_ColonneAdresse();
            menuItem.Items.Add(this.MenuItem_ColonneAdresse);

            this.MenuItem_ColonneCodePostal = new MenuItem();
            this.MenuItem_ColonneCodePostal.IsChecked = false;
            this.MenuItem_ColonneCodePostal.Header = "Code postal";
            this.MenuItem_ColonneCodePostal.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneCodePostal(); });
            this.AffMas_ColonneCodePostal();
            menuItem.Items.Add(this.MenuItem_ColonneCodePostal);

            this.MenuItem_ColonneVille = new MenuItem();
            this.MenuItem_ColonneVille.IsChecked = false;
            this.MenuItem_ColonneVille.Header = "Ville";
            this.MenuItem_ColonneVille.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneVille(); });
            this.AffMas_ColonneVille();
            menuItem.Items.Add(this.MenuItem_ColonneVille);

            this.MenuItem_ColonneTelephone = new MenuItem();
            this.MenuItem_ColonneTelephone.IsChecked = false;
            this.MenuItem_ColonneTelephone.Header = "N° Tel";
            this.MenuItem_ColonneTelephone.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTelephone(); });
            this.AffMas_ColonneTelephone();
            menuItem.Items.Add(this.MenuItem_ColonneTelephone);

            this.MenuItem_ColonneFax = new MenuItem();
            this.MenuItem_ColonneFax.IsChecked = false;
            this.MenuItem_ColonneFax.Header = "N° Fax";
            this.MenuItem_ColonneFax.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFax(); });
            this.AffMas_ColonneFax();
            menuItem.Items.Add(this.MenuItem_ColonneFax);

            this.MenuItem_ColonneClientFournisseur = new MenuItem();
            this.MenuItem_ColonneClientFournisseur.IsChecked = false;
            this.MenuItem_ColonneClientFournisseur.Header = "Client/Fournisseur";
            this.MenuItem_ColonneClientFournisseur.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneClientFournisseur(); });
            this.AffMas_ColonneClientFournisseur();
            menuItem.Items.Add(this.MenuItem_ColonneClientFournisseur);

            this.MenuItem_ColonneSiteWeb = new MenuItem();
            this.MenuItem_ColonneSiteWeb.IsChecked = true;
            this.MenuItem_ColonneSiteWeb.Header = "Site Web";
            this.MenuItem_ColonneSiteWeb.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneSiteWeb(); });
            this.AffMas_ColonneSiteWeb();
            menuItem.Items.Add(this.MenuItem_ColonneSiteWeb);

            this.MenuItem_ColonneEMail = new MenuItem();
            this.MenuItem_ColonneEMail.IsChecked = true;
            this.MenuItem_ColonneEMail.Header = "Email";
            this.MenuItem_ColonneEMail.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEMail(); });
            this.AffMas_ColonneEMail();
            menuItem.Items.Add(this.MenuItem_ColonneEMail);

            this.MenuItem_ColonneSiret = new MenuItem();
            this.MenuItem_ColonneSiret.IsChecked = true;
            this.MenuItem_ColonneSiret.Header = "N° Siret";
            this.MenuItem_ColonneSiret.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneSiret(); });
            this.AffMas_ColonneSiret();
            menuItem.Items.Add(this.MenuItem_ColonneSiret);

            this.MenuItem_ColonneAPE = new MenuItem();
            this.MenuItem_ColonneAPE.IsChecked = true;
            this.MenuItem_ColonneAPE.Header = "N° APE";
            this.MenuItem_ColonneAPE.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneAPE(); });
            this.AffMas_ColonneAPE();
            menuItem.Items.Add(this.MenuItem_ColonneAPE);

            this.MenuItem_ColonneDevise = new MenuItem();
            this.MenuItem_ColonneDevise.IsChecked = true;
            this.MenuItem_ColonneDevise.Header = "Devise";
            this.MenuItem_ColonneDevise.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDevise(); });
            this.AffMas_ColonneDevise();
            menuItem.Items.Add(this.MenuItem_ColonneDevise);

            this.MenuItem_ColonneGroupe = new MenuItem();
            this.MenuItem_ColonneGroupe.IsChecked = true;
            this.MenuItem_ColonneGroupe.Header = "Groupe";
            this.MenuItem_ColonneGroupe.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneGroupe(); });
            this.AffMas_ColonneGroupe();
            menuItem.Items.Add(this.MenuItem_ColonneGroupe);

            this.MenuItem_ColonneModeFacturation = new MenuItem();
            this.MenuItem_ColonneModeFacturation.IsChecked = true;
            this.MenuItem_ColonneModeFacturation.Header = "Mode de facturation";
            this.MenuItem_ColonneModeFacturation.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneModeFacturation(); });
            this.AffMas_ColonneModeFacturation();
            menuItem.Items.Add(this.MenuItem_ColonneModeFacturation);

            //Fournisseur
            MenuItem menuItemFournisseur = new MenuItem();
            menuItemFournisseur.Header = "Fournisseur";
            menuItem.Items.Add(menuItemFournisseur);


            this.MenuItem_ColonneFournisseurDelaisLivraison = new MenuItem();
            this.MenuItem_ColonneFournisseurDelaisLivraison.IsChecked = true;
            this.MenuItem_ColonneFournisseurDelaisLivraison.Header = "Fournisseur -> Delais Liraison";
            this.MenuItem_ColonneFournisseurDelaisLivraison.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFournisseurDelaisLivraison(); });
            this.AffMas_ColonneFournisseurDelaisLivraison();
            menuItemFournisseur.Items.Add(this.MenuItem_ColonneFournisseurDelaisLivraison);

            this.MenuItem_ColonneFournisseurDelaisConsultation = new MenuItem();
            this.MenuItem_ColonneFournisseurDelaisConsultation.IsChecked = true;
            this.MenuItem_ColonneFournisseurDelaisConsultation.Header = "Fournisseur -> Delais Consultation";
            this.MenuItem_ColonneFournisseurDelaisConsultation.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFournisseurDelaisConsultation(); });
            this.AffMas_ColonneFournisseurDelaisConsultation();
            menuItemFournisseur.Items.Add(this.MenuItem_ColonneFournisseurDelaisConsultation);

            this.MenuItem_ColonneFournisseurDelaisIncertitude = new MenuItem();
            this.MenuItem_ColonneFournisseurDelaisIncertitude.IsChecked = true;
            this.MenuItem_ColonneFournisseurDelaisIncertitude.Header = "Fournisseur -> Delais Incertitude";
            this.MenuItem_ColonneFournisseurDelaisIncertitude.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFournisseurDelaisIncertitude(); });
            this.AffMas_ColonneFournisseurDelaisIncertitude();
            menuItemFournisseur.Items.Add(this.MenuItem_ColonneFournisseurDelaisIncertitude);

            this.MenuItem_ColonneFournisseurRemiseCommerciale = new MenuItem();
            this.MenuItem_ColonneFournisseurRemiseCommerciale.IsChecked = true;
            this.MenuItem_ColonneFournisseurRemiseCommerciale.Header = "Fournisseur -> Remise commerciale";
            this.MenuItem_ColonneFournisseurRemiseCommerciale.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFournisseurRemiseCommerciale(); });
            this.AffMas_ColonneFournisseurRemiseCommerciale();
            menuItemFournisseur.Items.Add(this.MenuItem_ColonneFournisseurRemiseCommerciale);

            this.MenuItem_ColonneFournisseurEscompte = new MenuItem();
            this.MenuItem_ColonneFournisseurEscompte.IsChecked = true;
            this.MenuItem_ColonneFournisseurEscompte.Header = "Fournisseur -> Escompte";
            this.MenuItem_ColonneFournisseurEscompte.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFournisseurEscompte(); });
            this.AffMas_ColonneFournisseurEscompte();
            menuItemFournisseur.Items.Add(this.MenuItem_ColonneFournisseurEscompte);

            this.MenuItem_ColonneFournisseurCode = new MenuItem();
            this.MenuItem_ColonneFournisseurCode.IsChecked = true;
            this.MenuItem_ColonneFournisseurCode.Header = "Fournisseur -> Code";
            this.MenuItem_ColonneFournisseurCode.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFournisseurCode(); });
            this.AffMas_ColonneFournisseurCode();
            menuItemFournisseur.Items.Add(this.MenuItem_ColonneFournisseurCode);

            this.MenuItem_ColonneFournisseurFraisDePort = new MenuItem();
            this.MenuItem_ColonneFournisseurFraisDePort.IsChecked = true;
            this.MenuItem_ColonneFournisseurFraisDePort.Header = "Fournisseur -> Frais De Port";
            this.MenuItem_ColonneFournisseurFraisDePort.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFournisseurFraisDePort(); });
            this.AffMas_ColonneFournisseurFraisDePort();
            menuItemFournisseur.Items.Add(this.MenuItem_ColonneFournisseurFraisDePort);

            this.MenuItem_ColonneFournisseurTVA = new MenuItem();
            this.MenuItem_ColonneFournisseurTVA.IsChecked = true;
            this.MenuItem_ColonneFournisseurTVA.Header = "Fournisseur -> TVA";
            this.MenuItem_ColonneFournisseurTVA.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFournisseurTVA(); });
            this.AffMas_ColonneFournisseurTVA();
            menuItemFournisseur.Items.Add(this.MenuItem_ColonneFournisseurTVA);

            //Client
            MenuItem menuItemClient = new MenuItem();
            menuItemClient.Header = "Client";
            menuItem.Items.Add(menuItemClient);


            this.MenuItem_ColonneClientCommandeMinimum = new MenuItem();
            this.MenuItem_ColonneClientCommandeMinimum.IsChecked = true;
            this.MenuItem_ColonneClientCommandeMinimum.Header = "Client -> Commande Minimum";
            this.MenuItem_ColonneClientCommandeMinimum.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneClientCommandeMinimum(); });
            this.AffMas_ColonneClientCommandeMinimum();
            menuItemClient.Items.Add(this.MenuItem_ColonneClientCommandeMinimum);

            this.MenuItem_ColonneClientRemiseCommerciale = new MenuItem();
            this.MenuItem_ColonneClientRemiseCommerciale.IsChecked = true;
            this.MenuItem_ColonneClientRemiseCommerciale.Header = "Client -> Remise Commerciale";
            this.MenuItem_ColonneClientRemiseCommerciale.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneClientRemiseCommerciale(); });
            this.AffMas_ColonneClientRemiseCommerciale();
            menuItemClient.Items.Add(this.MenuItem_ColonneClientRemiseCommerciale);

            this.MenuItem_ColonneClientEscompte = new MenuItem();
            this.MenuItem_ColonneClientEscompte.IsChecked = true;
            this.MenuItem_ColonneClientEscompte.Header = "Client -> Escompte";
            this.MenuItem_ColonneClientEscompte.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneClientEscompte(); });
            this.AffMas_ColonneClientEscompte();
            menuItemClient.Items.Add(this.MenuItem_ColonneClientEscompte);

            this.MenuItem_ColonneClientNbExemplaireFacture = new MenuItem();
            this.MenuItem_ColonneClientNbExemplaireFacture.IsChecked = true;
            this.MenuItem_ColonneClientNbExemplaireFacture.Header = "Client -> Nombre d'exemplaire facture";
            this.MenuItem_ColonneClientNbExemplaireFacture.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneClientNbExemplaireFacture(); });
            this.AffMas_ColonneClientNbExemplaireFacture();
            menuItemClient.Items.Add(this.MenuItem_ColonneClientNbExemplaireFacture);

            this.MenuItem_ColonneClientCode = new MenuItem();
            this.MenuItem_ColonneClientCode.IsChecked = true;
            this.MenuItem_ColonneClientCode.Header = "Client -> Code";
            this.MenuItem_ColonneClientCode.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneClientCode(); });
            this.AffMas_ColonneClientCode();
            menuItemClient.Items.Add(this.MenuItem_ColonneClientCode);

            this.MenuItem_ColonneClientTVA = new MenuItem();
            this.MenuItem_ColonneClientTVA.IsChecked = true;
            this.MenuItem_ColonneClientTVA.Header = "Client -> TVA";
            this.MenuItem_ColonneClientTVA.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneClientTVA(); });
            this.AffMas_ColonneClientTVA();
            menuItemClient.Items.Add(this.MenuItem_ColonneClientTVA);

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

        private void RapportImprimerdifferenceprixgeneral()
        {
            ReportingWindow reportingWindow = new ReportingWindow();
            reportingWindow._webBrowser.Source = new Uri("http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fRapportGeneralPrixFournisseur&rs:Command=Render");
            reportingWindow.Title = "Différences prix produits fournisseur";

            reportingWindow.Show();
        }

        private void RapportImprimerdifferenceprix()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    if (((Entreprise)this._DataGridMain.SelectedItem).Fournisseur != null)
                    {
                        ReportingWindow reportingWindow = new ReportingWindow();
                        long toShow = ((Entreprise)this._DataGridMain.SelectedItem).Identifiant;
                        reportingWindow._webBrowser.Source = new Uri("http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fRapportPrixFournisseur&rs:Command=Render&Fournisseur=" + toShow);
                        reportingWindow.Title = "Différences prix produits fournisseur";

                        reportingWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("L'entreprise n'est pas fournisseur. Le rapport n'est donc pas disponible.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule affaire.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une affaire.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        #region Afficher / Masquer

        #region Tout

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneTypeEntreprise.IsChecked = false;
            this.MenuItem_ColonneLibelle.IsChecked = false;
            this.MenuItem_ColonneAdresse.IsChecked = false;
            this.MenuItem_ColonneCodePostal.IsChecked = false;
            this.MenuItem_ColonneVille.IsChecked = false;
            this.MenuItem_ColonneTelephone.IsChecked = false;
            this.MenuItem_ColonneFax.IsChecked = false;
            this.MenuItem_ColonneClientFournisseur.IsChecked = false;
            this.MenuItem_ColonneSiteWeb.IsChecked = false;
            this.MenuItem_ColonneEMail.IsChecked = false;
            this.MenuItem_ColonneSiret.IsChecked = false;
            this.MenuItem_ColonneAPE.IsChecked = false;
            this.MenuItem_ColonneDevise.IsChecked = false;
            this.MenuItem_ColonneGroupe.IsChecked = false;
            this.MenuItem_ColonneModeFacturation.IsChecked = false;
            this.MenuItem_ColonneFournisseurDelaisLivraison.IsChecked = false;
            this.MenuItem_ColonneFournisseurDelaisConsultation.IsChecked = false;
            this.MenuItem_ColonneFournisseurDelaisIncertitude.IsChecked = false;
            this.MenuItem_ColonneFournisseurRemiseCommerciale.IsChecked = false;
            this.MenuItem_ColonneFournisseurEscompte.IsChecked = false;
            this.MenuItem_ColonneFournisseurCode.IsChecked = false;
            this.MenuItem_ColonneFournisseurFraisDePort.IsChecked = false;
            this.MenuItem_ColonneFournisseurTVA.IsChecked = false;
            this.MenuItem_ColonneClientCommandeMinimum.IsChecked = false;
            this.MenuItem_ColonneClientRemiseCommerciale.IsChecked = false;
            this.MenuItem_ColonneClientEscompte.IsChecked = false;
            this.MenuItem_ColonneClientNbExemplaireFacture.IsChecked = false;
            this.MenuItem_ColonneClientCode.IsChecked = false;
            this.MenuItem_ColonneClientTVA.IsChecked = false;

            this.AffMas_ColonneTypeEntreprise();
            this.AffMas_ColonneLibelle();
            this.AffMas_ColonneAdresse();
            this.AffMas_ColonneCodePostal();
            this.AffMas_ColonneVille();
            this.AffMas_ColonneTelephone();
            this.AffMas_ColonneFax();
            this.AffMas_ColonneClientFournisseur();
            this.AffMas_ColonneSiteWeb();
            this.AffMas_ColonneEMail();
            this.AffMas_ColonneSiret();
            this.AffMas_ColonneAPE();
            this.AffMas_ColonneDevise();
            this.AffMas_ColonneGroupe();
            this.AffMas_ColonneModeFacturation();
            this.AffMas_ColonneFournisseurDelaisLivraison();
            this.AffMas_ColonneFournisseurDelaisConsultation();
            this.AffMas_ColonneFournisseurDelaisIncertitude();
            this.AffMas_ColonneFournisseurRemiseCommerciale();
            this.AffMas_ColonneFournisseurEscompte();
            this.AffMas_ColonneFournisseurCode();
            this.AffMas_ColonneFournisseurFraisDePort();
            this.AffMas_ColonneFournisseurTVA();
            this.AffMas_ColonneClientCommandeMinimum();
            this.AffMas_ColonneClientRemiseCommerciale();
            this.AffMas_ColonneClientEscompte();
            this.AffMas_ColonneClientNbExemplaireFacture();
            this.AffMas_ColonneClientCode();
            this.AffMas_ColonneClientTVA();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneTypeEntreprise.IsChecked = true;
            this.MenuItem_ColonneLibelle.IsChecked = true;
            this.MenuItem_ColonneAdresse.IsChecked = true;
            this.MenuItem_ColonneCodePostal.IsChecked = true;
            this.MenuItem_ColonneVille.IsChecked = true;
            this.MenuItem_ColonneTelephone.IsChecked = true;
            this.MenuItem_ColonneFax.IsChecked = true;
            this.MenuItem_ColonneClientFournisseur.IsChecked = true;
            this.MenuItem_ColonneSiteWeb.IsChecked = true;
            this.MenuItem_ColonneEMail.IsChecked = true;
            this.MenuItem_ColonneSiret.IsChecked = true;
            this.MenuItem_ColonneAPE.IsChecked = true;
            this.MenuItem_ColonneDevise.IsChecked = true;
            this.MenuItem_ColonneGroupe.IsChecked = true;
            this.MenuItem_ColonneModeFacturation.IsChecked = true;
            this.MenuItem_ColonneFournisseurDelaisLivraison.IsChecked = true;
            this.MenuItem_ColonneFournisseurDelaisConsultation.IsChecked = true;
            this.MenuItem_ColonneFournisseurDelaisIncertitude.IsChecked = true;
            this.MenuItem_ColonneFournisseurRemiseCommerciale.IsChecked = true;
            this.MenuItem_ColonneFournisseurEscompte.IsChecked = true;
            this.MenuItem_ColonneFournisseurCode.IsChecked = true;
            this.MenuItem_ColonneFournisseurFraisDePort.IsChecked = true;
            this.MenuItem_ColonneFournisseurTVA.IsChecked = true;
            this.MenuItem_ColonneClientCommandeMinimum.IsChecked = true;
            this.MenuItem_ColonneClientRemiseCommerciale.IsChecked = true;
            this.MenuItem_ColonneClientEscompte.IsChecked = true;
            this.MenuItem_ColonneClientNbExemplaireFacture.IsChecked = true;
            this.MenuItem_ColonneClientCode.IsChecked = true;
            this.MenuItem_ColonneClientTVA.IsChecked = true;

            this.AffMas_ColonneTypeEntreprise();
            this.AffMas_ColonneLibelle();
            this.AffMas_ColonneAdresse();
            this.AffMas_ColonneCodePostal();
            this.AffMas_ColonneVille();
            this.AffMas_ColonneTelephone();
            this.AffMas_ColonneFax();
            this.AffMas_ColonneClientFournisseur();
            this.AffMas_ColonneSiteWeb();
            this.AffMas_ColonneEMail();
            this.AffMas_ColonneSiret();
            this.AffMas_ColonneAPE();
            this.AffMas_ColonneDevise();
            this.AffMas_ColonneGroupe();
            this.AffMas_ColonneModeFacturation();
            this.AffMas_ColonneFournisseurDelaisLivraison();
            this.AffMas_ColonneFournisseurDelaisConsultation();
            this.AffMas_ColonneFournisseurDelaisIncertitude();
            this.AffMas_ColonneFournisseurRemiseCommerciale();
            this.AffMas_ColonneFournisseurEscompte();
            this.AffMas_ColonneFournisseurCode();
            this.AffMas_ColonneFournisseurFraisDePort();
            this.AffMas_ColonneFournisseurTVA();
            this.AffMas_ColonneClientCommandeMinimum();
            this.AffMas_ColonneClientRemiseCommerciale();
            this.AffMas_ColonneClientEscompte();
            this.AffMas_ColonneClientNbExemplaireFacture();
            this.AffMas_ColonneClientCode();
            this.AffMas_ColonneClientTVA();
        }

        #endregion

        private void AffMas_ColonneTypeEntreprise()
        {
            if (this.MenuItem_ColonneTypeEntreprise.IsChecked == true)
            {
                this._ColonneTypeEntreprise.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTypeEntreprise.IsChecked = false;
            }
            else
            {
                this._ColonneTypeEntreprise.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTypeEntreprise.IsChecked = true;
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

        private void AffMas_ColonneAdresse()
        {
            if (this.MenuItem_ColonneAdresse.IsChecked == true)
            {
                this._ColonneAdresse.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneAdresse.IsChecked = false;
            }
            else
            {
                this._ColonneAdresse.Visibility = Visibility.Visible;
                this.MenuItem_ColonneAdresse.IsChecked = true;
            }
        }

        private void AffMas_ColonneCodePostal()
        {
            if (this.MenuItem_ColonneCodePostal.IsChecked == true)
            {
                this._ColonneCodePostal.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneCodePostal.IsChecked = false;
            }
            else
            {
                this._ColonneCodePostal.Visibility = Visibility.Visible;
                this.MenuItem_ColonneCodePostal.IsChecked = true;
            }
        }

        private void AffMas_ColonneVille()
        {
            if (this.MenuItem_ColonneVille.IsChecked == true)
            {
                this._ColonneVille.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneVille.IsChecked = false;
            }
            else
            {
                this._ColonneVille.Visibility = Visibility.Visible;
                this.MenuItem_ColonneVille.IsChecked = true;
            }
        }

        private void AffMas_ColonneTelephone()
        {
            if (this.MenuItem_ColonneTelephone.IsChecked == true)
            {
                this._ColonneTelephone.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTelephone.IsChecked = false;
            }
            else
            {
                this._ColonneTelephone.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTelephone.IsChecked = true;
            }
        }

        private void AffMas_ColonneFax()
        {
            if (this.MenuItem_ColonneFax.IsChecked == true)
            {
                this._ColonneFax.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFax.IsChecked = false;
            }
            else
            {
                this._ColonneFax.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFax.IsChecked = true;
            }
        }

        private void AffMas_ColonneClientFournisseur()
        {
            if (this.MenuItem_ColonneClientFournisseur.IsChecked == true)
            {
                this._ColonneClientFournisseur.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneClientFournisseur.IsChecked = false;
            }
            else
            {
                this._ColonneClientFournisseur.Visibility = Visibility.Visible;
                this.MenuItem_ColonneClientFournisseur.IsChecked = true;
            }
        }

        private void AffMas_ColonneSiteWeb()
        {
            if (this.MenuItem_ColonneSiteWeb.IsChecked == true)
            {
                this._ColonneSiteWeb.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneSiteWeb.IsChecked = false;
            }
            else
            {
                this._ColonneSiteWeb.Visibility = Visibility.Visible;
                this.MenuItem_ColonneSiteWeb.IsChecked = true;
            }
        }

        private void AffMas_ColonneEMail()
        {
            if (this.MenuItem_ColonneEMail.IsChecked == true)
            {
                this._ColonneEMail.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneEMail.IsChecked = false;
            }
            else
            {
                this._ColonneEMail.Visibility = Visibility.Visible;
                this.MenuItem_ColonneEMail.IsChecked = true;
            }
        }

        private void AffMas_ColonneSiret()
        {
            if (this.MenuItem_ColonneSiret.IsChecked == true)
            {
                this._ColonneSiret.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneSiret.IsChecked = false;
            }
            else
            {
                this._ColonneSiret.Visibility = Visibility.Visible;
                this.MenuItem_ColonneSiret.IsChecked = true;
            }
        }

        private void AffMas_ColonneAPE()
        {
            if (this.MenuItem_ColonneAPE.IsChecked == true)
            {
                this._ColonneAPE.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneAPE.IsChecked = false;
            }
            else
            {
                this._ColonneAPE.Visibility = Visibility.Visible;
                this.MenuItem_ColonneAPE.IsChecked = true;
            }
        }

        private void AffMas_ColonneDevise()
        {
            if (this.MenuItem_ColonneDevise.IsChecked == true)
            {
                this._ColonneDevise.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDevise.IsChecked = false;
            }
            else
            {
                this._ColonneDevise.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDevise.IsChecked = true;
            }
        }

        private void AffMas_ColonneGroupe()
        {
            if (this.MenuItem_ColonneGroupe.IsChecked == true)
            {
                this._ColonneGroupe.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneGroupe.IsChecked = false;
            }
            else
            {
                this._ColonneGroupe.Visibility = Visibility.Visible;
                this.MenuItem_ColonneGroupe.IsChecked = true;
            }
        }

        private void AffMas_ColonneModeFacturation()
        {
            if (this.MenuItem_ColonneModeFacturation.IsChecked == true)
            {
                this._ColonneModeFacturation.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneModeFacturation.IsChecked = false;
            }
            else
            {
                this._ColonneModeFacturation.Visibility = Visibility.Visible;
                this.MenuItem_ColonneModeFacturation.IsChecked = true;
            }
        }

        private void AffMas_ColonneFournisseurDelaisLivraison()
        {
            if (this.MenuItem_ColonneFournisseurDelaisLivraison.IsChecked == true)
            {
                this._ColonneFournisseurDelaisLivraison.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFournisseurDelaisLivraison.IsChecked = false;
            }
            else
            {
                this._ColonneFournisseurDelaisLivraison.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFournisseurDelaisLivraison.IsChecked = true;
            }
        }

        private void AffMas_ColonneFournisseurDelaisConsultation()
        {
            if (this.MenuItem_ColonneFournisseurDelaisConsultation.IsChecked == true)
            {
                this._ColonneFournisseurDelaisConsultation.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFournisseurDelaisConsultation.IsChecked = false;
            }
            else
            {
                this._ColonneFournisseurDelaisConsultation.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFournisseurDelaisConsultation.IsChecked = true;
            }
        }

        private void AffMas_ColonneFournisseurDelaisIncertitude()
        {
            if (this.MenuItem_ColonneFournisseurDelaisIncertitude.IsChecked == true)
            {
                this._ColonneFournisseurDelaisIncertitude.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFournisseurDelaisIncertitude.IsChecked = false;
            }
            else
            {
                this._ColonneFournisseurDelaisIncertitude.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFournisseurDelaisIncertitude.IsChecked = true;
            }
        }

        private void AffMas_ColonneFournisseurRemiseCommerciale()
        {
            if (this.MenuItem_ColonneFournisseurRemiseCommerciale.IsChecked == true)
            {
                this._ColonneFournisseurRemiseCommerciale.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFournisseurRemiseCommerciale.IsChecked = false;
            }
            else
            {
                this._ColonneFournisseurRemiseCommerciale.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFournisseurRemiseCommerciale.IsChecked = true;
            }
        }

        private void AffMas_ColonneFournisseurEscompte()
        {
            if (this.MenuItem_ColonneFournisseurEscompte.IsChecked == true)
            {
                this._ColonneFournisseurEscompte.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFournisseurEscompte.IsChecked = false;
            }
            else
            {
                this._ColonneFournisseurEscompte.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFournisseurEscompte.IsChecked = true;
            }
        }

        private void AffMas_ColonneFournisseurCode()
        {
            if (this.MenuItem_ColonneFournisseurCode.IsChecked == true)
            {
                this._ColonneFournisseurCode.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFournisseurCode.IsChecked = false;
            }
            else
            {
                this._ColonneFournisseurCode.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFournisseurCode.IsChecked = true;
            }
        }

        private void AffMas_ColonneFournisseurFraisDePort()
        {
            if (this.MenuItem_ColonneFournisseurFraisDePort.IsChecked == true)
            {
                this._ColonneFournisseurFraisDePort.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFournisseurFraisDePort.IsChecked = false;
            }
            else
            {
                this._ColonneFournisseurFraisDePort.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFournisseurFraisDePort.IsChecked = true;
            }
        }

        private void AffMas_ColonneFournisseurTVA()
        {
            if (this.MenuItem_ColonneFournisseurTVA.IsChecked == true)
            {
                this._ColonneFournisseurTVA.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFournisseurTVA.IsChecked = false;
            }
            else
            {
                this._ColonneFournisseurTVA.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFournisseurTVA.IsChecked = true;
            }
        }

        private void AffMas_ColonneClientCommandeMinimum()
        {
            if (this.MenuItem_ColonneClientCommandeMinimum.IsChecked == true)
            {
                this._ColonneClientCommandeMinimum.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneClientCommandeMinimum.IsChecked = false;
            }
            else
            {
                this._ColonneClientCommandeMinimum.Visibility = Visibility.Visible;
                this.MenuItem_ColonneClientCommandeMinimum.IsChecked = true;
            }
        }

        private void AffMas_ColonneClientRemiseCommerciale()
        {
            if (this.MenuItem_ColonneClientRemiseCommerciale.IsChecked == true)
            {
                this._ColonneClientRemiseCommerciale.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneClientRemiseCommerciale.IsChecked = false;
            }
            else
            {
                this._ColonneClientRemiseCommerciale.Visibility = Visibility.Visible;
                this.MenuItem_ColonneClientRemiseCommerciale.IsChecked = true;
            }
        }

        private void AffMas_ColonneClientEscompte()
        {
            if (this.MenuItem_ColonneClientEscompte.IsChecked == true)
            {
                this._ColonneClientEscompte.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneClientEscompte.IsChecked = false;
            }
            else
            {
                this._ColonneClientEscompte.Visibility = Visibility.Visible;
                this.MenuItem_ColonneClientEscompte.IsChecked = true;
            }
        }

        private void AffMas_ColonneClientNbExemplaireFacture()
        {
            if (this.MenuItem_ColonneClientNbExemplaireFacture.IsChecked == true)
            {
                this._ColonneClientNbExemplaireFacture.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneClientNbExemplaireFacture.IsChecked = false;
            }
            else
            {
                this._ColonneClientNbExemplaireFacture.Visibility = Visibility.Visible;
                this.MenuItem_ColonneClientNbExemplaireFacture.IsChecked = true;
            }
        }

        private void AffMas_ColonneClientCode()
        {
            if (this.MenuItem_ColonneClientCode.IsChecked == true)
            {
                this._ColonneClientCode.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneClientCode.IsChecked = false;
            }
            else
            {
                this._ColonneClientCode.Visibility = Visibility.Visible;
                this.MenuItem_ColonneClientCode.IsChecked = true;
            }
        }

        private void AffMas_ColonneClientTVA()
        {
            if (this.MenuItem_ColonneClientTVA.IsChecked == true)
            {
                this._ColonneClientTVA.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneClientTVA.IsChecked = false;
            }
            else
            {
                this._ColonneClientTVA.Visibility = Visibility.Visible;
                this.MenuItem_ColonneClientTVA.IsChecked = true;
            }
        }

        #endregion

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
            ((MainWindow)((Grid)((Border)this.Parent).Parent).Parent).stopThread();
        }

        #endregion

        #region CRUD (Create Read Update Delete)

        /// <summary>
        /// Ajoute une nouvelle entreprise à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Entreprise Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une entreprise en cours ...");

            //Initialisation de la fenêtre
            EntrepriseWindow entrepriseWindow = new EntrepriseWindow();

            //Création de l'objet temporaire
            Entreprise tmp = new Entreprise();
            tmp.Adresse1 = new Adresse();
            tmp.Adresse2 = new Adresse();
            tmp.Client = new Client();
            tmp.Is_Client = false;
            tmp.Fournisseur = new Fournisseur();
            tmp.Is_Fournisseur = false;

            //Mise de l'objet temporaire dans le datacontext
            entrepriseWindow.DataContext = tmp;
            entrepriseWindow.creation = true;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = entrepriseWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet commande se trouvant dans le datacontext de la fenêtre
                return (Entreprise)entrepriseWindow.DataContext;
            }
            else
            {
                try
                {
                    try
                    {
                        ObservableCollection<Entreprise_Activite> toRemove = new ObservableCollection<Entreprise_Activite>();
                        foreach (Entreprise_Activite item in ((Entreprise)entrepriseWindow.DataContext).Entreprise_Activite)
                        {
                            toRemove.Add(item);
                        }
                        foreach (Entreprise_Activite item in toRemove)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        ObservableCollection<Entreprise_Litige> toRemove2 = new ObservableCollection<Entreprise_Litige>();
                        foreach (Entreprise_Litige item in ((Entreprise)entrepriseWindow.DataContext).Entreprise_Litige)
                        {
                            toRemove2.Add(item);
                        }
                        foreach (Entreprise_Litige item in toRemove2)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        ObservableCollection<Entreprise_RIB> toRemove3 = new ObservableCollection<Entreprise_RIB>();
                        foreach (Entreprise_RIB item in ((Entreprise)entrepriseWindow.DataContext).Entreprise_RIB)
                        {
                            toRemove3.Add(item);
                        }
                        foreach (Entreprise_RIB item in toRemove3)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        ObservableCollection<Numero_Tva_Intraco> toRemove4 = new ObservableCollection<Numero_Tva_Intraco>();
                        foreach (Numero_Tva_Intraco item in ((Entreprise)entrepriseWindow.DataContext).Numero_Tva_Intraco)
                        {
                            toRemove4.Add(item);
                        }
                        foreach (Numero_Tva_Intraco item in toRemove4)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        ObservableCollection<Fournisseur_Cle_Comptable> toRemove4 = new ObservableCollection<Fournisseur_Cle_Comptable>();
                        foreach (Fournisseur_Cle_Comptable item in ((Entreprise)entrepriseWindow.DataContext).Fournisseur.Fournisseur_Cle_Comptable)
                        {
                            toRemove4.Add(item);
                        }
                        foreach (Fournisseur_Cle_Comptable item in toRemove4)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        ObservableCollection<Fournisseur_Condition_Reglement> toRemove4 = new ObservableCollection<Fournisseur_Condition_Reglement>();
                        foreach (Fournisseur_Condition_Reglement item in ((Entreprise)entrepriseWindow.DataContext).Fournisseur.Fournisseur_Condition_Reglement)
                        {
                            toRemove4.Add(item);
                        }
                        foreach (Fournisseur_Condition_Reglement item in toRemove4)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        ObservableCollection<Client_Condition_Reglement> toRemove4 = new ObservableCollection<Client_Condition_Reglement>();
                        foreach (Client_Condition_Reglement item in ((Entreprise)entrepriseWindow.DataContext).Client.Client_Condition_Reglement)
                        {
                            toRemove4.Add(item);
                        }
                        foreach (Client_Condition_Reglement item in toRemove4)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        ObservableCollection<Client_Cle_Comptable> toRemove4 = new ObservableCollection<Client_Cle_Comptable>();
                        foreach (Client_Cle_Comptable item in ((Entreprise)entrepriseWindow.DataContext).Client.Client_Cle_Comptable)
                        {
                            toRemove4.Add(item);
                        }
                        foreach (Client_Cle_Comptable item in toRemove4)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        try
                        {
                            ((App)App.Current).mySitaffEntities.Detach(((Entreprise)entrepriseWindow.DataContext).Fournisseur.Agence_Interimaire);
                        }
                        catch (Exception) { }
                        try
                        {
                            ((App)App.Current).mySitaffEntities.Detach(((Entreprise)entrepriseWindow.DataContext).Fournisseur.Sous_Traitant);
                        }
                        catch (Exception) { }
                        ((App)App.Current).mySitaffEntities.Detach(((Entreprise)entrepriseWindow.DataContext).Fournisseur);
                    }
                    catch (Exception) { }

                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(((Entreprise)entrepriseWindow.DataContext).Client);
                    }
                    catch (Exception) { }

                    ((App)App.Current).mySitaffEntities.Detach((Entreprise)entrepriseWindow.DataContext);
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une entreprise annulé : " + this.Entreprises.Count() + " / " + this.max);

                return null;
            }
        }


        /// <summary>
        /// Ouvre l'entreprise séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Entreprise Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une entreprise en cours ...");

                    //Création de la fenêtre
                    EntrepriseWindow entrepriseWindow = new EntrepriseWindow();

                    //Initialisation du Datacontext en entreprise et association à la entreprise sélectionnée
                    entrepriseWindow.DataContext = new Entreprise();
                    entrepriseWindow.DataContext = this._DataGridMain.SelectedItem;

                    if (((Entreprise)entrepriseWindow.DataContext).Client == null)
                    {
                        ((Entreprise)entrepriseWindow.DataContext).Client = new Client();
                    }
                    if (((Entreprise)entrepriseWindow.DataContext).Fournisseur == null)
                    {
                        ((Entreprise)entrepriseWindow.DataContext).Fournisseur = new Fournisseur();
                    }
                    if (((Entreprise)entrepriseWindow.DataContext).Adresse1 == null)
                    {
                        ((Entreprise)entrepriseWindow.DataContext).Adresse1 = new Adresse();
                    }
                    else
                    {
                        Adresse tmp = new Adresse();
                        tmp = ((Entreprise)entrepriseWindow.DataContext).Adresse1;
                        ((Entreprise)entrepriseWindow.DataContext).Adresse1 = tmp;
                    }
                    if (((Entreprise)entrepriseWindow.DataContext).Adresse2 == null)
                    {
                        ((Entreprise)entrepriseWindow.DataContext).Adresse2 = new Adresse();
                    }
                    else
                    {
                        Adresse tmp = new Adresse();
                        tmp = ((Entreprise)entrepriseWindow.DataContext).Adresse2;
                        ((Entreprise)entrepriseWindow.DataContext).Adresse2 = tmp;
                    }


                    bool? dialogResult = entrepriseWindow.ShowDialog();
                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet se trouvant dans le datacontext de la fenêtre
                        return (Entreprise)entrepriseWindow.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Entreprise)(this._DataGridMain.SelectedItem));
                        //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une entreprise annulée : " + this.Entreprises.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule entreprise.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une entreprise.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime l'entreprise séléctionnée avec une confirmation
        /// </summary>
        public Entreprise Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une entreprise en cours ...");

                    bool test = true;
                    if (test)
                    {
                        if (((Entreprise)this._DataGridMain.SelectedItem).Personne.Count() != 0)
                        {
                            test = false;
                            MessageBox.Show("Vous ne pouvez supprimer cette entreprise car des contacts ou des salariés y sont associés", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
                            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression impossible, cause : entreprise possède des contacts ou salariés : " + this.Entreprises.Count() + " / " + this.max);
                        }
                    }
                    if (test)
                    {
                        if (((Entreprise)this._DataGridMain.SelectedItem).Fournisseur != null && ((Entreprise)this._DataGridMain.SelectedItem).Is_Fournisseur == true)
                        {
                            if (((Entreprise)this._DataGridMain.SelectedItem).Fournisseur.Commande_Fournisseur.Count() != 0)
                            {
                                test = false;
                                MessageBox.Show("Vous ne pouvez supprimer cette entreprise car des commandes fournisseur y sont associés", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                                this.recalculMax();
                                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression impossible, cause : entreprise possède commandes fournisseurs : " + this.Entreprises.Count() + " / " + this.max);
                            }
                        }
                    }
                    if (test)
                    {
                        if (((Entreprise)this._DataGridMain.SelectedItem).Fournisseur != null && ((Entreprise)this._DataGridMain.SelectedItem).Is_Fournisseur == true)
                        {
                            if (((Entreprise)this._DataGridMain.SelectedItem).Fournisseur.Facture_Fournisseur.Count() != 0)
                            {
                                test = false;
                                MessageBox.Show("Vous ne pouvez supprimer cette entreprise car des factures fournisseur y sont associés", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                                this.recalculMax();
                                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression impossible, cause : entreprise possède factures fournisseurs : " + this.Entreprises.Count() + " / " + this.max);
                            }
                        }
                    }
                    if (test)
                    {
                        if (((Entreprise)this._DataGridMain.SelectedItem).Fournisseur != null && ((Entreprise)this._DataGridMain.SelectedItem).Is_Fournisseur == true)
                        {
                            if (((Entreprise)this._DataGridMain.SelectedItem).Fournisseur.Bon_Livraison.Count() != 0)
                            {
                                test = false;
                                MessageBox.Show("Vous ne pouvez supprimer cette entreprise car des bons de livraison y sont associés", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                                this.recalculMax();
                                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression impossible, cause : entreprise possède bons de livraison : " + this.Entreprises.Count() + " / " + this.max);
                            }
                        }
                    }
                    if (test)
                    {
                        if (((Entreprise)this._DataGridMain.SelectedItem).Client != null && ((Entreprise)this._DataGridMain.SelectedItem).Is_Client == true)
                        {
                            if (((Entreprise)this._DataGridMain.SelectedItem).Client.Devis.Count() != 0 || ((Entreprise)this._DataGridMain.SelectedItem).Client.Devis1.Count() != 0 || ((Entreprise)this._DataGridMain.SelectedItem).Client.Devis2.Count() != 0)
                            {
                                test = false;
                                MessageBox.Show("Vous ne pouvez supprimer cette entreprise car des devis y sont associés", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                                this.recalculMax();
                                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression impossible, cause : entreprise possède devis : " + this.Entreprises.Count() + " / " + this.max);
                            }
                        }
                    }
                    if (test)
                    {
                        if (((Entreprise)this._DataGridMain.SelectedItem).Client != null && ((Entreprise)this._DataGridMain.SelectedItem).Is_Client == true)
                        {
                            if (((Entreprise)this._DataGridMain.SelectedItem).Client.DAO.Count() != 0)
                            {
                                test = false;
                                MessageBox.Show("Vous ne pouvez supprimer cette entreprise car des dessins y sont associés", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                                this.recalculMax();
                                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression impossible, cause : entreprise possède dessins : " + this.Entreprises.Count() + " / " + this.max);
                            }
                        }
                    }
                    if (test)
                    {
                        if (((Entreprise)this._DataGridMain.SelectedItem).Client != null && ((Entreprise)this._DataGridMain.SelectedItem).Is_Client == true)
                        {
                            if (((Entreprise)this._DataGridMain.SelectedItem).Client.Regie.Count() != 0)
                            {
                                test = false;
                                MessageBox.Show("Vous ne pouvez supprimer cette entreprise car des régies y sont associés", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                                this.recalculMax();
                                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression impossible, cause : entreprise possède régies : " + this.Entreprises.Count() + " / " + this.max);
                            }
                        }
                    }

                    if (test)
                    {
                        if (MessageBox.Show("Voulez-vous rééllement supprimer l'entreprise séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            Entreprise toDelete = (Entreprise)this._DataGridMain.SelectedItem;

                            try
                            {
                                ObservableCollection<Entreprise_Activite> toRemove = new ObservableCollection<Entreprise_Activite>();
                                foreach (Entreprise_Activite item in toDelete.Entreprise_Activite)
                                {
                                    toRemove.Add(item);
                                }
                                foreach (Entreprise_Activite item in toRemove)
                                {
                                    try
                                    {
                                        ((App)App.Current).mySitaffEntities.Entreprise_Activite.DeleteObject(item);
                                    }
                                    catch (Exception)
                                    {
                                        ((App)App.Current).mySitaffEntities.Detach(item);
                                    }
                                }
                            }
                            catch (Exception) { }

                            try
                            {
                                ObservableCollection<Entreprise_Litige> toRemove2 = new ObservableCollection<Entreprise_Litige>();
                                foreach (Entreprise_Litige item in toDelete.Entreprise_Litige)
                                {
                                    toRemove2.Add(item);
                                }
                                foreach (Entreprise_Litige item in toRemove2)
                                {
                                    try
                                    {
                                        ((App)App.Current).mySitaffEntities.Entreprise_Litige.DeleteObject(item);
                                    }
                                    catch (Exception)
                                    {
                                        ((App)App.Current).mySitaffEntities.Detach(item);
                                    }
                                }
                            }
                            catch (Exception) { }

                            try
                            {
                                ObservableCollection<Entreprise_RIB> toRemove3 = new ObservableCollection<Entreprise_RIB>();
                                foreach (Entreprise_RIB item in toDelete.Entreprise_RIB)
                                {
                                    toRemove3.Add(item);
                                }
                                foreach (Entreprise_RIB item in toRemove3)
                                {
                                    try
                                    {
                                        ((App)App.Current).mySitaffEntities.Entreprise_RIB.DeleteObject(item);
                                    }
                                    catch (Exception)
                                    {
                                        ((App)App.Current).mySitaffEntities.Detach(item);
                                    }
                                }
                            }
                            catch (Exception) { }

                            try
                            {
                                ObservableCollection<Numero_Tva_Intraco> toRemove4 = new ObservableCollection<Numero_Tva_Intraco>();
                                foreach (Numero_Tva_Intraco item in toDelete.Numero_Tva_Intraco)
                                {
                                    toRemove4.Add(item);
                                }
                                foreach (Numero_Tva_Intraco item in toRemove4)
                                {
                                    try
                                    {
                                        ((App)App.Current).mySitaffEntities.Numero_Tva_Intraco.DeleteObject(item);
                                    }
                                    catch (Exception)
                                    {
                                        ((App)App.Current).mySitaffEntities.Detach(item);
                                    }
                                }
                            }
                            catch (Exception) { }

                            try
                            {
                                ObservableCollection<Fournisseur_Cle_Comptable> toRemove4 = new ObservableCollection<Fournisseur_Cle_Comptable>();
                                foreach (Fournisseur_Cle_Comptable item in toDelete.Fournisseur.Fournisseur_Cle_Comptable)
                                {
                                    toRemove4.Add(item);
                                }
                                foreach (Fournisseur_Cle_Comptable item in toRemove4)
                                {
                                    try
                                    {
                                        ((App)App.Current).mySitaffEntities.Fournisseur_Cle_Comptable.DeleteObject(item);
                                    }
                                    catch (Exception)
                                    {
                                        ((App)App.Current).mySitaffEntities.Detach(item);
                                    }
                                }
                            }
                            catch (Exception) { }

                            try
                            {
                                ObservableCollection<Fournisseur_Condition_Reglement> toRemove4 = new ObservableCollection<Fournisseur_Condition_Reglement>();
                                foreach (Fournisseur_Condition_Reglement item in toDelete.Fournisseur.Fournisseur_Condition_Reglement)
                                {
                                    toRemove4.Add(item);
                                }
                                foreach (Fournisseur_Condition_Reglement item in toRemove4)
                                {
                                    try
                                    {
                                        ((App)App.Current).mySitaffEntities.Fournisseur_Condition_Reglement.DeleteObject(item);
                                    }
                                    catch (Exception)
                                    {
                                        ((App)App.Current).mySitaffEntities.Detach(item);
                                    }
                                }
                            }
                            catch (Exception) { }

                            try
                            {
                                ObservableCollection<Client_Condition_Reglement> toRemove4 = new ObservableCollection<Client_Condition_Reglement>();
                                foreach (Client_Condition_Reglement item in toDelete.Client.Client_Condition_Reglement)
                                {
                                    toRemove4.Add(item);
                                }
                                foreach (Client_Condition_Reglement item in toRemove4)
                                {
                                    try
                                    {
                                        ((App)App.Current).mySitaffEntities.Client_Condition_Reglement.DeleteObject(item);
                                    }
                                    catch (Exception)
                                    {
                                        ((App)App.Current).mySitaffEntities.Detach(item);
                                    }
                                }
                            }
                            catch (Exception) { }

                            try
                            {
                                ObservableCollection<Client_Cle_Comptable> toRemove4 = new ObservableCollection<Client_Cle_Comptable>();
                                foreach (Client_Cle_Comptable item in toDelete.Client.Client_Cle_Comptable)
                                {
                                    toRemove4.Add(item);
                                }
                                foreach (Client_Cle_Comptable item in toRemove4)
                                {
                                    try
                                    {
                                        ((App)App.Current).mySitaffEntities.Client_Cle_Comptable.DeleteObject(item);
                                    }
                                    catch (Exception)
                                    {
                                        ((App)App.Current).mySitaffEntities.Detach(item);
                                    }
                                }
                            }
                            catch (Exception) { }

                            try
                            {
                                try
                                {
                                    try
                                    {
                                        ((App)App.Current).mySitaffEntities.Sous_Traitant.DeleteObject(toDelete.Fournisseur.Sous_Traitant);
                                    }
                                    catch (Exception)
                                    {
                                        ((App)App.Current).mySitaffEntities.Detach(toDelete.Fournisseur.Agence_Interimaire);
                                    }
                                }
                                catch (Exception) { }
                                try
                                {
                                    try
                                    {
                                        ((App)App.Current).mySitaffEntities.Agence_Interimaire.DeleteObject(toDelete.Fournisseur.Agence_Interimaire);
                                    }
                                    catch (Exception)
                                    {
                                        ((App)App.Current).mySitaffEntities.Detach(toDelete.Fournisseur.Agence_Interimaire);
                                    }
                                }
                                catch (Exception) { }
                                try
                                {
                                    ((App)App.Current).mySitaffEntities.Fournisseur.DeleteObject(toDelete.Fournisseur);
                                }
                                catch (Exception)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(toDelete.Fournisseur);
                                }
                            }
                            catch (Exception) { }

                            try
                            {
                                ((App)App.Current).mySitaffEntities.Detach(toDelete.Client);
                            }
                            catch (Exception) { }

                            //Supprimer l'élément 
                            return (Entreprise)this._DataGridMain.SelectedItem;
                        }
                        else
                        {
                            //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
                            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une entreprise annulée : " + this.Entreprises.Count() + " / " + this.max);

                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule entreprise.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une entreprise.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre l'entreprise séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Entreprise Look(Entreprise entreprise)
        {
            if (this._DataGridMain.SelectedItem != null || entreprise != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1 || entreprise != null)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une entreprise en cours ...");

                    //Création de la fenêtre
                    EntrepriseWindow entrepriseWindow = new EntrepriseWindow();

                    //Initialisation du Datacontext en entreprise et association à la entreprise sélectionnée
                    entrepriseWindow.DataContext = new Entreprise();
                    if (entreprise != null)
                    {
                        entrepriseWindow.DataContext = entreprise;
                    }
                    else
                    {
                        entrepriseWindow.DataContext = this._DataGridMain.SelectedItem;
                    }

                    //Je positionne la lecture seule sur la fenêtre
                    entrepriseWindow.creation = false;
                    entrepriseWindow.SoloLecture = true;
                    entrepriseWindow.lectureSeule();

                    //J'affiche la fenêtre
                    bool? dialogResult = entrepriseWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une entreprise terminé : " + this.Entreprises.Count() + " / " + this.max);

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule entreprise.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une entreprise.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            //TextBox et autocompletebox
            this._filterContainLibelle.Text = "";
            this._filterContainGroupe.Text = "";
            this._filterContainVille.Text = "";
            this._filterContainPays.Text = "";
            this._filterContainCP.Text = "";
            this._filterContainActivite.Text = "";
            this._filterContainCodeClient.Text = "";
            this._filterContainCodeFournisseur.Text = "";

            //comboBox
            this._filterContainClientFournisseur.SelectedItem = null;
            this._filterContainStatut.SelectedItem = null;

            //CheckBox
            this._filterContainSousTraitant.IsChecked = false;
            this._filterContainAgenceInterimaire.IsChecked = false;

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

            ObservableCollection<Entreprise> listToPut = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.OrderBy(ent => ent.Libelle));

            if (this._filterContainLibelle.Text != "")
            {
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Libelle.Trim().ToLower().Contains(this._filterContainLibelle.Text.Trim().ToLower())));
            }
            if (this._filterContainGroupe.Text != "")
            {
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Groupe1 != null));
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Groupe1.Libelle.ToLower().Trim().Contains(this._filterContainGroupe.Text.ToLower().Trim())));
            }
            if (this._filterContainVille.Text != "")
            {
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Adresse1 != null));
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Adresse1.Ville1 != null));
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Adresse1.Ville1.Libelle.ToLower().Trim().Contains(this._filterContainVille.Text.ToLower().Trim())));
            }
            if (this._filterContainPays.Text != "")
            {
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Adresse1 != null));
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Adresse1.Ville1 != null));
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Adresse1.Ville1.Pays1 != null));
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Adresse1.Ville1.Pays1.Libelle.ToLower().Trim().Contains(this._filterContainPays.Text.ToLower().Trim())));
            }
            if (this._filterContainSousTraitant.IsChecked == true)
            {
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Fournisseur != null && ent.Is_Fournisseur == true));
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Fournisseur.Sous_Traitant != null));
            }
            if (this._filterContainAgenceInterimaire.IsChecked == true)
            {
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Fournisseur != null && ent.Is_Fournisseur == true));
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Fournisseur.Agence_Interimaire != null));
            }
            if (this._filterContainCodeClient.Text != "")
            {
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Client != null && ent.Is_Client == true));
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Client.Code != null));
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Client.Code.ToLower().Trim().Contains(this._filterContainCodeFournisseur.Text.ToLower().Trim())));
            }
            if (this._filterContainCodeFournisseur.Text != "")
            {
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Fournisseur != null && ent.Is_Fournisseur == true));
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Fournisseur.Code != null));
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Fournisseur.Code.ToLower().Trim().Contains(this._filterContainCodeFournisseur.Text.ToLower().Trim())));
            }
            if (this._filterContainClientFournisseur.SelectedItem != null)
            {
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.TypeEntrepriseClientFournisseur == ((ItemOuiNon)this._filterContainClientFournisseur.SelectedItem).chaine));
            }
            if (this._filterContainActivite.Text != "")
            {
                ObservableCollection<Entreprise> listTmp = new ObservableCollection<Entreprise>();
                foreach (Entreprise item in listToPut)
                {
                    bool test = false;
                    foreach (Entreprise_Activite itemea in item.Entreprise_Activite)
                    {
                        if (itemea.Activite1 != null && !test)
                        {
                            if (itemea.Activite1.Libelle.ToLower().Trim().Contains(this._filterContainActivite.Text.ToLower().Trim()))
                            {
                                test = true;
                            }
                        }
                    }
                    if (test)
                    {
                        listTmp.Add(item);
                    }
                }
                listToPut = listTmp;
            }
            if (this._filterContainStatut.SelectedItem != null)
            {
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Statut1 != null));
                listToPut = new ObservableCollection<Entreprise>(listToPut.Where(ent => ent.Statut1 == (Statut)this._filterContainStatut.SelectedItem));
            }

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            //Si aucun résultat, j'affiche un message
            if (this.Entreprises.Count() == 0)
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
                if (this.max != this.Entreprises.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainLibelle.Focus();
            }
        }

        #endregion

        #region Mettre a null

        private void NullClientFournisseur_Click_1(object sender, RoutedEventArgs e)
        {
            this._filterContainClientFournisseur.SelectedItem = null;
        }

        private void NullStatut_Click_1(object sender, RoutedEventArgs e)
        {
            this._filterContainStatut.SelectedItem = null;
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
            this.max = ((App)App.Current).mySitaffEntities.Entreprise.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Entreprise ent)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des entreprises terminée : " + this.Entreprises.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.Entreprises.Add(ent);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une entreprise '" + ent.Libelle + "' effectué avec succès. Nombre d'élements : " + this.Entreprises.Count() + " / " + this.max);
                try
                {
                    this._DataGridMain.SelectedItem = ent;
                }
                catch (Exception) { }
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification de l'entreprise : '" + ent.Libelle + "' effectuée avec succès. Nombre d'élements : " + this.Entreprises.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.Entreprises.Remove(ent);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression de l'entreprise : '" + ent.Libelle + "' effectuée avec succès. Nombre d'élements : " + this.Entreprises.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des entreprises terminé : " + this.Entreprises.Count() + " / " + this.max);
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
            this.Entreprises = new ObservableCollection<Entreprise>(this.Entreprises.OrderBy(ent => ent.Libelle));
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
