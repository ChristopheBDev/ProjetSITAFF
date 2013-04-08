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
using SitaffRibbon.Classes;

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeContactsControl.xaml
    /// </summary>
    public partial class ListeContactsControl : UserControl
    {

        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_ColonneInitiale;
        MenuItem MenuItem_ColonneCivilite;
        MenuItem MenuItem_ColonneNom;
        MenuItem MenuItem_ColonnePrenom;
        MenuItem MenuItem_ColonneAdresse;
        MenuItem MenuItem_ColonneAdressePro;
        MenuItem MenuItem_ColonneEntreprise;
        MenuItem MenuItem_ColonneTel;
        MenuItem MenuItem_ColonneTelPro;
        MenuItem MenuItem_ColonnePortable;
        MenuItem MenuItem_ColonnePortablePro;
        MenuItem MenuItem_ColonneFax;
        MenuItem MenuItem_ColonneFaxPro;
        MenuItem MenuItem_ColonneEmail;
        MenuItem MenuItem_ColonneEmailPro;
        MenuItem MenuItem_ColonneCentreInteret;
        MenuItem MenuItem_ColonneFonction;
        MenuItem MenuItem_ColonneService;
        MenuItem MenuItem_ColonneCadeau;
        MenuItem MenuItem_ColonneCadeauLibelle;
        MenuItem MenuItem_ColonneAmis;

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Propriétés de dépendances

        public ObservableCollection<Personne> Contacts
        {
            get { return (ObservableCollection<Personne>)GetValue(ContactsProperty); }
            set { SetValue(ContactsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contacts.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactsProperty =
            DependencyProperty.Register("Contacts", typeof(ObservableCollection<Personne>), typeof(ListeContactsControl), new UIPropertyMetadata(null));


        public ObservableCollection<Entreprise> entreprise
        {
            get { return (ObservableCollection<Entreprise>)GetValue(entrepriseProperty); }
            set { SetValue(entrepriseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for entreprise.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty entrepriseProperty =
            DependencyProperty.Register("entreprise", typeof(ObservableCollection<Entreprise>), typeof(ListeContactsControl), new UIPropertyMetadata(null));




        public ObservableCollection<Ville> Ville
        {
            get { return (ObservableCollection<Ville>)GetValue(VilleProperty); }
            set { SetValue(VilleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Ville.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VilleProperty =
            DependencyProperty.Register("Ville", typeof(ObservableCollection<Ville>), typeof(ListeContactsControl), new UIPropertyMetadata(null));



        public ObservableCollection<Pays> ListPays
        {
            get { return (ObservableCollection<Pays>)GetValue(ListPaysProperty); }
            set { SetValue(ListPaysProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListPays.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListPaysProperty =
            DependencyProperty.Register("ListPays", typeof(ObservableCollection<Pays>), typeof(ListeContactsControl), new UIPropertyMetadata(null));


        #endregion

        #region constructeur

        public ListeContactsControl()
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

        #region initialisation de la zone de filtrage

        private void initialisationFilterZone()
        {
            this.initialisationAutoCompleteBox();
            this._filterZone.Height = 21;
        }

        private void initialisationAutoCompleteBox()
        {
            List<string> listVille = new List<string>();
            List<string> listPays = new List<string>();
            List<string> listEntreprise = new List<string>();
            List<string> listFonction = new List<string>();
            foreach (Personne item in ((App)App.Current).mySitaffEntities.Personne)
            {
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


                //Pour remplir les entreprise
                if (item.Entreprise1 != null)
                {
                    if (!listEntreprise.Contains(item.Entreprise1.Libelle))
                    {
                        listEntreprise.Add(item.Entreprise1.Libelle);
                    }
                }

                //Pour remplir les fonctions
                if (item.Contact != null)
                {
                    if (item.Contact.Contact_Fonction1 != null)
                    {

                        if (!listFonction.Contains(item.Contact.Contact_Fonction1.Libelle))
                        {
                            listFonction.Add(item.Contact.Contact_Fonction1.Libelle);
                        }
                    }
                }
            }

            _filterContainPays.ItemsSource = listPays;
            _filterContainFonction.ItemsSource = listFonction;
            _filterContainVille.ItemsSource = listVille;
            _filterContainStatut.ItemsSource = listEntreprise;
        }

        #endregion

        #region initialisation donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<Personne> listToPut)
        {
            if (listToPut == null)
            {
                this.Contacts = new ObservableCollection<Personne>(((App)App.Current).mySitaffEntities.Personne.Where(con => con.Contact != null).OrderBy(per => per.Nom));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.Contacts = new ObservableCollection<Personne>(listToPut);
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

        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {
            this.MenuItem_ColonneCivilite = new MenuItem();
            this.MenuItem_ColonneCivilite.IsChecked = true;
            this.MenuItem_ColonneCivilite.Header = "Civilité";
            this.MenuItem_ColonneCivilite.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneCivilite(); });
            this.AffMas_ColonneCivilite();
            menuItem.Items.Add(this.MenuItem_ColonneCivilite);

            this.MenuItem_ColonneNom = new MenuItem();
            this.MenuItem_ColonneNom.IsChecked = false;
            this.MenuItem_ColonneNom.Header = "Nom";
            this.MenuItem_ColonneNom.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNom(); });
            this.AffMas_ColonneNom();
            menuItem.Items.Add(this.MenuItem_ColonneNom);

            this.MenuItem_ColonnePrenom = new MenuItem();
            this.MenuItem_ColonnePrenom.IsChecked = false;
            this.MenuItem_ColonnePrenom.Header = "Prénom";
            this.MenuItem_ColonnePrenom.Click += new RoutedEventHandler(delegate { this.AffMas_ColonnePrenom(); });
            this.AffMas_ColonnePrenom();
            menuItem.Items.Add(this.MenuItem_ColonnePrenom);

            this.MenuItem_ColonneInitiale = new MenuItem();
            this.MenuItem_ColonneInitiale.IsChecked = true;
            this.MenuItem_ColonneInitiale.Header = "Initiales";
            this.MenuItem_ColonneInitiale.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneInitiale(); });
            this.AffMas_ColonneInitiale();
            menuItem.Items.Add(this.MenuItem_ColonneInitiale);

            this.MenuItem_ColonneAdresse = new MenuItem();
            this.MenuItem_ColonneAdresse.IsChecked = true;
            this.MenuItem_ColonneAdresse.Header = "Adresse";
            this.MenuItem_ColonneAdresse.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneAdresse(); });
            this.AffMas_ColonneAdresse();
            menuItem.Items.Add(this.MenuItem_ColonneAdresse);

            this.MenuItem_ColonneAdressePro = new MenuItem();
            this.MenuItem_ColonneAdressePro.IsChecked = true;
            this.MenuItem_ColonneAdressePro.Header = "Adresse Pro";
            this.MenuItem_ColonneAdressePro.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneAdressePro(); });
            this.AffMas_ColonneAdressePro();
            menuItem.Items.Add(this.MenuItem_ColonneAdressePro);

            this.MenuItem_ColonneEntreprise = new MenuItem();
            this.MenuItem_ColonneEntreprise.IsChecked = false;
            this.MenuItem_ColonneEntreprise.Header = "Entreprise";
            this.MenuItem_ColonneEntreprise.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEntreprise(); });
            this.AffMas_ColonneEntreprise();
            menuItem.Items.Add(this.MenuItem_ColonneEntreprise);

            this.MenuItem_ColonneFonction = new MenuItem();
            this.MenuItem_ColonneFonction.IsChecked = false;
            this.MenuItem_ColonneFonction.Header = "Fonction";
            this.MenuItem_ColonneFonction.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFonction(); });
            this.AffMas_ColonneFonction();
            menuItem.Items.Add(this.MenuItem_ColonneFonction);

            this.MenuItem_ColonneService = new MenuItem();
            this.MenuItem_ColonneService.IsChecked = false;
            this.MenuItem_ColonneService.Header = "Service";
            this.MenuItem_ColonneService.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneService(); });
            this.AffMas_ColonneService();
            menuItem.Items.Add(this.MenuItem_ColonneService);

            this.MenuItem_ColonneTel = new MenuItem();
            this.MenuItem_ColonneTel.IsChecked = true;
            this.MenuItem_ColonneTel.Header = "Tel";
            this.MenuItem_ColonneTel.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTel(); });
            this.AffMas_ColonneTel();
            menuItem.Items.Add(this.MenuItem_ColonneTel);

            this.MenuItem_ColonneTelPro = new MenuItem();
            this.MenuItem_ColonneTelPro.IsChecked = false;
            this.MenuItem_ColonneTelPro.Header = "Tel Pro";
            this.MenuItem_ColonneTelPro.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTelPro(); });
            this.AffMas_ColonneTelPro();
            menuItem.Items.Add(this.MenuItem_ColonneTelPro);

            this.MenuItem_ColonnePortable = new MenuItem();
            this.MenuItem_ColonnePortable.IsChecked = true;
            this.MenuItem_ColonnePortable.Header = "Portable";
            this.MenuItem_ColonnePortable.Click += new RoutedEventHandler(delegate { this.AffMas_ColonnePortable(); });
            this.AffMas_ColonnePortable();
            menuItem.Items.Add(this.MenuItem_ColonnePortable);

            this.MenuItem_ColonnePortablePro = new MenuItem();
            this.MenuItem_ColonnePortablePro.IsChecked = false;
            this.MenuItem_ColonnePortablePro.Header = "Portable Pro";
            this.MenuItem_ColonnePortablePro.Click += new RoutedEventHandler(delegate { this.AffMas_ColonnePortablePro(); });
            this.AffMas_ColonnePortablePro();
            menuItem.Items.Add(this.MenuItem_ColonnePortablePro);

            this.MenuItem_ColonneFax = new MenuItem();
            this.MenuItem_ColonneFax.IsChecked = true;
            this.MenuItem_ColonneFax.Header = "Fax";
            this.MenuItem_ColonneFax.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFax(); });
            this.AffMas_ColonneFax();
            menuItem.Items.Add(this.MenuItem_ColonneFax);

            this.MenuItem_ColonneFaxPro = new MenuItem();
            this.MenuItem_ColonneFaxPro.IsChecked = true;
            this.MenuItem_ColonneFaxPro.Header = "Fax Pro";
            this.MenuItem_ColonneFaxPro.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFaxPro(); });
            this.AffMas_ColonneFaxPro();
            menuItem.Items.Add(this.MenuItem_ColonneFaxPro);

            this.MenuItem_ColonneEmail = new MenuItem();
            this.MenuItem_ColonneEmail.IsChecked = true;
            this.MenuItem_ColonneEmail.Header = "Email";
            this.MenuItem_ColonneEmail.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEmail(); });
            this.AffMas_ColonneEmail();
            menuItem.Items.Add(this.MenuItem_ColonneEmail);

            this.MenuItem_ColonneEmailPro = new MenuItem();
            this.MenuItem_ColonneEmailPro.IsChecked = false;
            this.MenuItem_ColonneEmailPro.Header = "Email Pro";
            this.MenuItem_ColonneEmailPro.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEmailPro(); });
            this.AffMas_ColonneEmailPro();
            menuItem.Items.Add(this.MenuItem_ColonneEmailPro);

            this.MenuItem_ColonneCentreInteret = new MenuItem();
            this.MenuItem_ColonneCentreInteret.IsChecked = true;
            this.MenuItem_ColonneCentreInteret.Header = "Centre d'interet";
            this.MenuItem_ColonneCentreInteret.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneCentreInteret(); });
            this.AffMas_ColonneCentreInteret();
            menuItem.Items.Add(this.MenuItem_ColonneCentreInteret);

            this.MenuItem_ColonneCadeau = new MenuItem();
            this.MenuItem_ColonneCadeau.IsChecked = true;
            this.MenuItem_ColonneCadeau.Header = "Cadeau";
            this.MenuItem_ColonneCadeau.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneCadeau(); });
            this.AffMas_ColonneCadeau();
            menuItem.Items.Add(this.MenuItem_ColonneCadeau);

            this.MenuItem_ColonneCadeauLibelle = new MenuItem();
            this.MenuItem_ColonneCadeauLibelle.IsChecked = true;
            this.MenuItem_ColonneCadeauLibelle.Header = "Libelle cadeau";
            this.MenuItem_ColonneCadeauLibelle.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneCadeauLibelle(); });
            this.AffMas_ColonneCadeauLibelle();
            menuItem.Items.Add(this.MenuItem_ColonneCadeauLibelle);

            this.MenuItem_ColonneAmis = new MenuItem();
            this.MenuItem_ColonneAmis.IsChecked = true;
            this.MenuItem_ColonneAmis.Header = "Amis";
            this.MenuItem_ColonneAmis.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneAmis(); });
            this.AffMas_ColonneAmis();
            menuItem.Items.Add(this.MenuItem_ColonneAmis);

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

        private void menuAddTime()
        {
            ((App)App.Current)._theMainWindow._CommandAddTime.Command.Execute(((App)App.Current)._theMainWindow);
        }

        #endregion

        #region Afficher / Masquer

        #region Tout

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneInitiale.IsChecked = false;
            this.MenuItem_ColonneCivilite.IsChecked = false;
            this.MenuItem_ColonneNom.IsChecked = false;
            this.MenuItem_ColonnePrenom.IsChecked = false;
            this.MenuItem_ColonneAdresse.IsChecked = false;
            this.MenuItem_ColonneAdressePro.IsChecked = false;
            this.MenuItem_ColonneEntreprise.IsChecked = false;
            this.MenuItem_ColonneTel.IsChecked = false;
            this.MenuItem_ColonneTelPro.IsChecked = false;
            this.MenuItem_ColonnePortable.IsChecked = false;
            this.MenuItem_ColonnePortablePro.IsChecked = false;
            this.MenuItem_ColonneFax.IsChecked = false;
            this.MenuItem_ColonneFaxPro.IsChecked = false;
            this.MenuItem_ColonneEmail.IsChecked = false;
            this.MenuItem_ColonneEmailPro.IsChecked = false;
            this.MenuItem_ColonneCentreInteret.IsChecked = false;
            this.MenuItem_ColonneFonction.IsChecked = false;
            this.MenuItem_ColonneService.IsChecked = false;
            this.MenuItem_ColonneCadeau.IsChecked = false;
            this.MenuItem_ColonneCadeauLibelle.IsChecked = false;
            this.MenuItem_ColonneAmis.IsChecked = false;


            this.AffMas_ColonneInitiale();
            this.AffMas_ColonneCivilite();
            this.AffMas_ColonneNom();
            this.AffMas_ColonnePrenom();
            this.AffMas_ColonneAdresse();
            this.AffMas_ColonneAdressePro();
            this.AffMas_ColonneEntreprise();
            this.AffMas_ColonneTel();
            this.AffMas_ColonneTelPro();
            this.AffMas_ColonnePortable();
            this.AffMas_ColonnePortablePro();
            this.AffMas_ColonneFax();
            this.AffMas_ColonneFaxPro();
            this.AffMas_ColonneEmail();
            this.AffMas_ColonneEmailPro();
            this.AffMas_ColonneCentreInteret();
            this.AffMas_ColonneFonction();
            this.AffMas_ColonneService();
            this.AffMas_ColonneCadeau();
            this.AffMas_ColonneCadeauLibelle();
            this.AffMas_ColonneAmis();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneInitiale.IsChecked = true;
            this.MenuItem_ColonneCivilite.IsChecked = true;
            this.MenuItem_ColonneNom.IsChecked = true;
            this.MenuItem_ColonnePrenom.IsChecked = true;
            this.MenuItem_ColonneAdresse.IsChecked = true;
            this.MenuItem_ColonneAdressePro.IsChecked = true;
            this.MenuItem_ColonneEntreprise.IsChecked = true;
            this.MenuItem_ColonneTel.IsChecked = true;
            this.MenuItem_ColonneTelPro.IsChecked = true;
            this.MenuItem_ColonnePortable.IsChecked = true;
            this.MenuItem_ColonnePortablePro.IsChecked = true;
            this.MenuItem_ColonneFax.IsChecked = true;
            this.MenuItem_ColonneFaxPro.IsChecked = true;
            this.MenuItem_ColonneEmail.IsChecked = true;
            this.MenuItem_ColonneEmailPro.IsChecked = true;
            this.MenuItem_ColonneCentreInteret.IsChecked = true;
            this.MenuItem_ColonneFonction.IsChecked = true;
            this.MenuItem_ColonneService.IsChecked = true;
            this.MenuItem_ColonneCadeau.IsChecked = true;
            this.MenuItem_ColonneCadeauLibelle.IsChecked = true;
            this.MenuItem_ColonneAmis.IsChecked = true;


            this.AffMas_ColonneInitiale();
            this.AffMas_ColonneCivilite();
            this.AffMas_ColonneNom();
            this.AffMas_ColonnePrenom();
            this.AffMas_ColonneAdresse();
            this.AffMas_ColonneAdressePro();
            this.AffMas_ColonneEntreprise();
            this.AffMas_ColonneTel();
            this.AffMas_ColonneTelPro();
            this.AffMas_ColonnePortable();
            this.AffMas_ColonnePortablePro();
            this.AffMas_ColonneFax();
            this.AffMas_ColonneFaxPro();
            this.AffMas_ColonneEmail();
            this.AffMas_ColonneEmailPro();
            this.AffMas_ColonneCentreInteret();
            this.AffMas_ColonneFonction();
            this.AffMas_ColonneService();
            this.AffMas_ColonneCadeau();
            this.AffMas_ColonneCadeauLibelle();
            this.AffMas_ColonneAmis();

        }

        #endregion

        private void AffMas_ColonneInitiale()
        {
            if (this.MenuItem_ColonneInitiale.IsChecked == true)
            {
                this._ColonneInitiale.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneInitiale.IsChecked = false;
            }
            else
            {
                this._ColonneInitiale.Visibility = Visibility.Visible;
                this.MenuItem_ColonneInitiale.IsChecked = true;
            }
        }

        private void AffMas_ColonneCivilite()
        {
            if (this.MenuItem_ColonneCivilite.IsChecked == true)
            {
                this._ColonneCivilite.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneCivilite.IsChecked = false;
            }
            else
            {
                this._ColonneCivilite.Visibility = Visibility.Visible;
                this.MenuItem_ColonneCivilite.IsChecked = true;
            }
        }

        private void AffMas_ColonneNom()
        {
            if (this.MenuItem_ColonneNom.IsChecked == true)
            {
                this._ColonneNom.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNom.IsChecked = false;
            }
            else
            {
                this._ColonneNom.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNom.IsChecked = true;
            }
        }

        private void AffMas_ColonnePrenom()
        {
            if (this.MenuItem_ColonnePrenom.IsChecked == true)
            {
                this._ColonnePrenom.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonnePrenom.IsChecked = false;
            }
            else
            {
                this._ColonnePrenom.Visibility = Visibility.Visible;
                this.MenuItem_ColonnePrenom.IsChecked = true;
            }
        }

        private void AffMas_ColonneTel()
        {
            if (this.MenuItem_ColonneTel.IsChecked == true)
            {
                this._ColonneTel.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTel.IsChecked = false;
            }
            else
            {
                this._ColonneTel.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTel.IsChecked = true;
            }
        }

        private void AffMas_ColonneTelPro()
        {
            if (this.MenuItem_ColonneTelPro.IsChecked == true)
            {
                this._ColonneTelPro.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTelPro.IsChecked = false;
            }
            else
            {
                this._ColonneTelPro.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTelPro.IsChecked = true;
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

        private void AffMas_ColonneFaxPro()
        {
            if (this.MenuItem_ColonneFaxPro.IsChecked == true)
            {
                this._ColonneFaxPro.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFaxPro.IsChecked = false;
            }
            else
            {
                this._ColonneFaxPro.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFaxPro.IsChecked = true;
            }
        }

        private void AffMas_ColonneEmail()
        {
            if (this.MenuItem_ColonneEmail.IsChecked == true)
            {
                this._ColonneEmail.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneEmail.IsChecked = false;
            }
            else
            {
                this._ColonneEmail.Visibility = Visibility.Visible;
                this.MenuItem_ColonneEmail.IsChecked = true;
            }
        }

        private void AffMas_ColonneEmailPro()
        {
            if (this.MenuItem_ColonneEmailPro.IsChecked == true)
            {
                this._ColonneEmailPro.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneEmailPro.IsChecked = false;
            }
            else
            {
                this._ColonneEmailPro.Visibility = Visibility.Visible;
                this.MenuItem_ColonneEmailPro.IsChecked = true;
            }
        }

        private void AffMas_ColonneCentreInteret()
        {
            if (this.MenuItem_ColonneCentreInteret.IsChecked == true)
            {
                this._ColonneCentreInteret.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneCentreInteret.IsChecked = false;
            }
            else
            {
                this._ColonneCentreInteret.Visibility = Visibility.Visible;
                this.MenuItem_ColonneCentreInteret.IsChecked = true;
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

        private void AffMas_ColonneAdressePro()
        {
            if (this.MenuItem_ColonneAdressePro.IsChecked == true)
            {
                this._ColonneAdressePro.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneAdressePro.IsChecked = false;
            }
            else
            {
                this._ColonneAdressePro.Visibility = Visibility.Visible;
                this.MenuItem_ColonneAdressePro.IsChecked = true;
            }
        }

        private void AffMas_ColonneEntreprise()
        {
            if (this.MenuItem_ColonneEntreprise.IsChecked == true)
            {
                this._ColonneEntreprise.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneEntreprise.IsChecked = false;
            }
            else
            {
                this._ColonneEntreprise.Visibility = Visibility.Visible;
                this.MenuItem_ColonneEntreprise.IsChecked = true;
            }
        }

        private void AffMas_ColonneFonction()
        {
            if (this.MenuItem_ColonneFonction.IsChecked == true)
            {
                this._ColonneFonction.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFonction.IsChecked = false;
            }
            else
            {
                this._ColonneFonction.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFonction.IsChecked = true;
            }
        }

        private void AffMas_ColonneService()
        {
            if (this.MenuItem_ColonneService.IsChecked == true)
            {
                this._ColonneService.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneService.IsChecked = false;
            }
            else
            {
                this._ColonneService.Visibility = Visibility.Visible;
                this.MenuItem_ColonneService.IsChecked = true;
            }
        }

        private void AffMas_ColonneCadeau()
        {
            if (this.MenuItem_ColonneCadeau.IsChecked == true)
            {
                this._ColonneCadeau.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneCadeau.IsChecked = false;
            }
            else
            {
                this._ColonneCadeau.Visibility = Visibility.Visible;
                this.MenuItem_ColonneCadeau.IsChecked = true;
            }
        }

        private void AffMas_ColonneCadeauLibelle()
        {
            if (this.MenuItem_ColonneCadeauLibelle.IsChecked == true)
            {
                this._ColonneCadeauLibelle.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneCadeauLibelle.IsChecked = false;
            }
            else
            {
                this._ColonneCadeauLibelle.Visibility = Visibility.Visible;
                this.MenuItem_ColonneCadeauLibelle.IsChecked = true;
            }
        }

        private void AffMas_ColonneAmis()
        {
            if (this.MenuItem_ColonneAmis.IsChecked == true)
            {
                this._ColonneAmis.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneAmis.IsChecked = false;
            }
            else
            {
                this._ColonneAmis.Visibility = Visibility.Visible;
                this.MenuItem_ColonneAmis.IsChecked = true;
            }
        }

        private void AffMas_ColonnePortable()
        {
            if (this.MenuItem_ColonnePortable.IsChecked == true)
            {
                this._ColonnePortable.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonnePortable.IsChecked = false;
            }
            else
            {
                this._ColonnePortable.Visibility = Visibility.Visible;
                this.MenuItem_ColonnePortable.IsChecked = true;
            }
        }

        private void AffMas_ColonnePortablePro()
        {
            if (this.MenuItem_ColonnePortablePro.IsChecked == true)
            {
                this._ColonnePortablePro.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonnePortablePro.IsChecked = false;
            }
            else
            {
                this._ColonnePortablePro.Visibility = Visibility.Visible;
                this.MenuItem_ColonnePortablePro.IsChecked = true;
            }
        }
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
        /// Ajoute une nouvelle Facture à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Personne Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un contact en cours ...");

            //Initialisation de la fenêtre
            ContactWindow contactWindow = new ContactWindow();

            //Création de l'objet temporaire
            Personne tmp = new Personne();
            tmp.Contact = new Contact();

            //Mise de l'objet temporaire dans le datacontext
            contactWindow.DataContext = tmp;


            //booléen nullable vrai ou faux ou null
            bool? dialogResult = contactWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                return (Personne)contactWindow.DataContext;
            }
            else
            {
                try
                {
                    //On détache le contact
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(((Personne)contactWindow.DataContext).Contact);
                    }
                    catch (Exception) { }
                    ((App)App.Current).mySitaffEntities.Detach((Personne)contactWindow.DataContext);
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un contact annulé : " + this.Contacts.Count() + " / " + this.max);

                return null;
            }
        }

        /// <summary>
        /// Ouvre la facture séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Personne Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un contact en cours ...");

                    //Création de la fenêtre
                    ContactWindow contactWindow = new ContactWindow();

                    //Initialisation du Datacontext en contact et association à la personne sélectionnée
                    contactWindow.DataContext = new Personne();
                    contactWindow.DataContext = (Personne)this._DataGridMain.SelectedItem;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = contactWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                        return (Personne)contactWindow.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Personne)(this._DataGridMain.SelectedItem));
                        //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un contact annulé : " + this.Contacts.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul contact.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un contact.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime le  contact séléctionnée avec une confirmation
        /// </summary>
        public Personne Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un contact en cours ...");

                    bool test = true;
                    if (test)
                    {
                        if (((Personne)this._DataGridMain.SelectedItem).Contact != null)
                        {
                            if (((Personne)this._DataGridMain.SelectedItem).Contact.Commande_Fournisseur.Count() != 0 || ((Personne)this._DataGridMain.SelectedItem).Contact.Commande_Fournisseur1.Count() != 0)
                            {
                                test = false;
                                MessageBox.Show("Vous ne pouvez supprimer ce contact car il est lié à des commandes fournisseur", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression impossible, cause : contact possède des liaisons à commande fournisseur");
                            }
                        }
                    }
                    if (test)
                    {
                        if (((Personne)this._DataGridMain.SelectedItem).Contact != null)
                        {
                            if (((Personne)this._DataGridMain.SelectedItem).Contact.Devis_Contact.Count() != 0)
                            {
                                test = false;
                                MessageBox.Show("Vous ne pouvez supprimer ce contact car il est lié à des devis", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression impossible, cause : contact possède des liaisons à devis");
                            }
                        }
                    }

                    if (test)
                    {
                        if (MessageBox.Show("Voulez-vous rééllement supprimer le contact séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            //Supprimer l'élément 
                            return (Personne)this._DataGridMain.SelectedItem;
                        }
                        else
                        {
                            //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un contact annulé : " + this.Contacts.Count() + " / " + this.max);

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
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul contact.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un contact.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre la facture séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Personne Look(Personne personneToLook)
        {
            if (this._DataGridMain.SelectedItem != null || personneToLook != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1 || personneToLook != null)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un contact en cours ...");

                    //Création de la fenêtre
                    ContactWindow contactWindow = new ContactWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    contactWindow.DataContext = new Personne();
                    if (personneToLook != null)
                    {
                        contactWindow.DataContext = personneToLook;
                    }
                    else
                    {
                        contactWindow.DataContext = (Personne)this._DataGridMain.SelectedItem;
                    }

                    //Je positionne la lecture seule sur la fenêtre
                    contactWindow.lectureSeule();
                    contactWindow.soloLecture = true;

                    //J'affiche la fenêtre
                    bool? dialogResult = contactWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un contact terminé : " + this.Contacts.Count() + " / " + this.max);

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul contact.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un contact.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        #endregion

        #region Actions supplémentaires

        #endregion

        #region Filtrage

        private void _ButtonMasqueFiltre_Click(object sender, RoutedEventArgs e)
        {
            if (_filterZone.Height != 21)
            {
                this._filterZone.Height = 21;
                this._ButtonMasqueFiltre.Content = "Afficher les filtres";
                this.remiseAZero();
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
            }
        }

        //Voir quelle bouton sont rempli ou non
        private void filtrage()
        {
            ((App)App.Current)._theMainWindow._mutex.WaitOne();
            ((App)App.Current)._theMainWindow.startThread();
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Filtrage en cours ...");
            ObservableCollection<Personne> listToPut = new ObservableCollection<Personne>(((App)App.Current).mySitaffEntities.Personne.Where(con => con.Contact != null).OrderBy(con => con.Nom));
            if (this._filterContainName.Text != "")
            {
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Nom != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Nom.Trim().ToLower().Contains(this._filterContainName.Text.Trim().ToLower())));
            }
            if (this._filterContainprenom.Text != "")
            {
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Prenom != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Prenom.Trim().ToLower().Contains(this._filterContainprenom.Text.Trim().ToLower())));
            }
            if (this._filterContainStatut.Text != "")
            {
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Entreprise1 != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Entreprise1.Libelle.Trim().ToLower().Contains(this._filterContainStatut.Text.Trim().ToLower())));
            }
            if (this._filterContainTelPro.Text != "")
            {
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.NumTelFixeProAvecEspaces != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.NumTelFixeProAvecEspaces.Trim().Replace(" ", "").Replace(".", "").ToLower().Contains(this._filterContainTelPro.Text.Trim().Replace(" ", "").Replace(".", "").ToLower())));
            }
            if (this._filterContainPortablePro.Text != "")
            {
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.NumTelPortProAvecEspaces != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.NumTelPortProAvecEspaces.Trim().Replace(" ", "").Replace(".", "").ToLower().Contains(this._filterContainPortablePro.Text.Trim().Replace(" ", "").Replace(".", "").ToLower())));
            }
            if (this._filterContainFaxPro.Text != "")
            {
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.NumFaxProAvecEspaces != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.NumFaxProAvecEspaces.Trim().Replace(" ", "").Replace(".", "").ToLower().Contains(this._filterContainFaxPro.Text.Trim().Replace(" ", "").Replace(".", "").ToLower())));
            }
            if (this._filterContainEmail.Text != "")
            {
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.EMail_Pro != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.EMail_Pro.Trim().ToLower().Contains(this._filterContainEmail.Text.Trim().ToLower())));
            }
            if (this._filterContainVille.Text != "")
            {
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Adresse1 != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Adresse1.Ville1 != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Adresse1.Ville1.Libelle == ((Ville)this._filterContainVille.SelectedItem).Libelle));
            }
            if (this._filterContainPays.Text != "")
            {
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Adresse1 != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Adresse1.Ville1 != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Adresse1.Ville1.Pays1 != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Adresse1.Ville1.Pays1.Libelle == ((Pays)this._filterContainPays.SelectedItem).Libelle));
            }
            if (this._filterContainCP.Text != "")
            {
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Adresse1 != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Adresse1.Ville1 != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Adresse1.Ville1.Code_Postal.Trim().ToLower().Contains(this._filterContainCP.Text.Trim().ToLower())));
            }
            if (this._filterContainFonction.Text != "")
            {
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Contact != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Contact.Contact_Fonction1 != null));
                listToPut = new ObservableCollection<Personne>(listToPut.Where(con => con.Contact.Contact_Fonction1.Libelle.Trim().ToLower().Contains(this._filterContainFonction.Text.Trim().ToLower())));
            }
            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            if (this.Contacts.Count() == 0)
            {
                MessageBox.Show("Aucun résultat ne correspont à votre recherche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //Evenement Click Bouton, qui permet d'appliquer les filtres
        private void _buttonFiltrer_Click(object sender, RoutedEventArgs e)
        {
            this.filtrage();
        }

        #region RàZ

        //Evènement CLick Bouton, de suprimmer tous filtres existant
        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            _filterContainName.Text = "";
            _filterContainprenom.Text = "";
            _filterContainVille.Text = "";
            _filterContainPays.Text = "";
            _filterContainCP.Text = "";
            _filterContainStatut.Text = "";
            _filterContainTelPro.Text = "";
            _filterContainFaxPro.Text = "";
            _filterContainPortablePro.Text = "";
            _filterContainEmail.Text = "";
            _filterContainFonction.Text = "";
            //Rechargement des élements
            this.initialisationDataDatagridMain(null);
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
                if (_filterContainName.Text != "" || _filterContainprenom.Text != "" || _filterContainVille.SelectedItem != null || _filterContainPays.SelectedItem != null || _filterContainCP.Text != "" || _filterContainStatut.SelectedItem != null || _filterContainTelPro.Text != "" || _filterContainFaxPro.Text != "" || _filterContainPortablePro.Text != "" || _filterContainEmail.Text != "" || this.max != this.Contacts.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainName.Focus();
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
            this.max = ((App)App.Current).mySitaffEntities.Contact.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Personne per)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des contacts terminée : " + this.Contacts.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.Contacts.Add(per);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un contact dénommé '" + per.Nom + "' effectué avec succès. Nombre d'élements : " + this.Contacts.Count() + " / " + this.max);
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification du contact dénommé : '" + per.Nom + "' effectué avec succès. Nombre d'élements : " + this.Contacts.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.Contacts.Remove(per);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression du contact dénommé : '" + per.Nom + "' effectué avec succès. Nombre d'élements : " + this.Contacts.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else
            {
((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des contacts terminé : " + this.Contacts.Count() + " / " + this.max);
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
            this.Contacts = new ObservableCollection<Personne>(this.Contacts.OrderBy(con => con.Nom));
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
