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

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeOrdreMissionControl.xaml
    /// </summary>
    public partial class ListeOrdreMissionControl : UserControl
    {

        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_ColonneNumeroContrat;
        MenuItem MenuItem_ColonneMotifMission;
        MenuItem MenuItem_ColonneDateDebut;
        MenuItem MenuItem_ColonneDateFin;
        MenuItem MenuItem_ColonneHeureRDV;
        MenuItem MenuItem_ColonneCommentaire;
        MenuItem MenuItem_ColonneEntrepriseMere;
        MenuItem MenuItem_ColonneNumeroAffaire;
        MenuItem MenuItem_ColonneDonneurOrdre;
        MenuItem MenuItem_ColonneContactMission_Personnel;
        MenuItem MenuItem_ColonneContactMission_Client;
        MenuItem MenuItem_ColonneLieuMission;
        MenuItem MenuItem_ColonneSalarieAbs;
        MenuItem MenuItem_ColonneIntTauxHoraire;
        MenuItem MenuItem_ColonneIntDureeHebdo;
        MenuItem MenuItem_ColonneIntMontant;
        MenuItem MenuItem_ColonneIntAccoss;
        MenuItem MenuItem_ColonneIntTemps_Deplacement;
        MenuItem MenuItem_ColonneIntDistance_Deplacement;
        MenuItem MenuItem_ColonneIntMontant_Deplacement;
        MenuItem MenuItem_ColonneIntInterimaire;
        MenuItem MenuItem_ColonneIntEntreprise;
        MenuItem MenuItem_ColonneIntContact;
        MenuItem MenuItem_ColonneIntEventRemboursement;
        MenuItem MenuItem_ColonneEquTauxHoraire;
        MenuItem MenuItem_ColonneEquDureeHebdo;
        MenuItem MenuItem_ColonneEquMontant;
        MenuItem MenuItem_ColonneEquEntreprise;
        MenuItem MenuItem_ColonneEquContact;
        MenuItem MenuItem_ColonneEquCommande;

        MenuItem MenuItem_AfficherInterimaire;
        MenuItem MenuItem_MasquerInterimaire;

        MenuItem MenuItem_AfficherEquipe;
        MenuItem MenuItem_MasquerEquipe;

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Propriétés de dépendances

        public ObservableCollection<Ordre_Mission> OrdresMission
        {
            get { return (ObservableCollection<Ordre_Mission>)GetValue(OrdresMissionProperty); }
            set { SetValue(OrdresMissionProperty, value); }
        }
        // Using a DependencyProperty as the backing store for OrdresMission.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdresMissionProperty =
            DependencyProperty.Register("OrdresMission", typeof(ObservableCollection<Ordre_Mission>), typeof(ListeOrdreMissionControl), new UIPropertyMetadata(null));

        public ObservableCollection<Entreprise_Mere> listEntrepriseMere
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntrepriseMereProperty); }
            set { SetValue(listEntrepriseMereProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listEntrepriseMere.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseMereProperty =
            DependencyProperty.Register("listEntrepriseMere", typeof(ObservableCollection<Entreprise_Mere>), typeof(ListeOrdreMissionControl), new UIPropertyMetadata(null));

        public ObservableCollection<Motif_Mission> listMotifMission
        {
            get { return (ObservableCollection<Motif_Mission>)GetValue(lisMotifMissionProperty); }
            set { SetValue(lisMotifMissionProperty, value); }
        }
        //Using a DependencyProperty as the backing store for lisMotifMission.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty lisMotifMissionProperty =
            DependencyProperty.Register("lisMotifMission", typeof(ObservableCollection<Motif_Mission>), typeof(ListeOrdreMissionControl), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public ListeOrdreMissionControl()
        {
            InitializeComponent();

            //Initialisation de la zone de filtrage
            this.initialisationFilterZone();

            //Initialisation des datas
            this.initialisationDataGridMain(null);

            //Masquer la zone de filtre
            this.AfficherMasquer();

            this.creationMenuClicDroit();
        }

        #region Initialisation Zone de filtrage

        private void initialisationFilterZone()
        {
            this.initialisationAutoCompleteBox();
            this.initialisationComboBoxInt_Equ();
            this.initialisationPropDependance();
        }

        private void initialisationAutoCompleteBox()
        {
            //Variables temporaires
            List<string> listEntreprise = new List<string>();
            List<string> listAffaire = new List<string>();
            List<string> listLieuMission = new List<string>();
            List<string> listContrat = new List<string>();
            List<string> listInterimaire = new List<string>();
            List<string> listCommande = new List<string>();
            List<string> listDonneurOrdre = new List<string>();
            List<string> listContact = new List<string>();
            List<string> listContactMission_Client = new List<string>();
            List<string> listContactMission_Personnel = new List<string>();

            //Récupération des données
            foreach (Ordre_Mission item in ((App)App.Current).mySitaffEntities.Ordre_Mission)
            {
                //Rapport à une mission intérimaire
                if (item.Mission_Interimaire1 != null)
                {
                    //Liste entreprise
                    if (item.Mission_Interimaire1.Entreprise1 != null && listEntreprise.Contains(item.Mission_Interimaire1.Entreprise1.Libelle) == false)
                    {
                        listEntreprise.Add(item.Mission_Interimaire1.Entreprise1.Libelle);
                    }
                    //Liste Intérimaire
                    if (item.Mission_Interimaire1.Salarie1 != null && listInterimaire.Contains(item.Mission_Interimaire1.Salarie1.Personne.fullname) == false)
                    {
                        listInterimaire.Add(item.Mission_Interimaire1.Salarie1.Personne.fullname);
                    }
                    //Liste contact
                    if (item.Mission_Interimaire1.Contact1 != null && listContact.Contains(item.Mission_Interimaire1.Contact1.Personne.fullname) == false)
                    {
                        listContact.Add(item.Mission_Interimaire1.Contact1.Personne.fullname);
                    }
                }

                //Rapport à une mission tiers
                if (item.Mission_Tiers1 != null)
                {
                    //Liste Commande
                    if (item.Mission_Tiers1.Commande_Fournisseur1 != null && !listCommande.Contains(item.Mission_Tiers1.Commande_Fournisseur1.Numero))
                    {
                        listCommande.Add(item.Mission_Tiers1.Commande_Fournisseur1.Numero);
                    }
                    //Liste contact
                    if (item.Mission_Tiers1.Contact1 != null && !listContact.Contains(item.Mission_Tiers1.Contact1.Personne.fullname))
                    {
                        listContact.Add(item.Mission_Tiers1.Contact1.Personne.fullname);
                    }
                }

                //Liste affaire
                if (item.Affaire1 != null && !listAffaire.Contains(item.Affaire1.Numero))
                {
                    listAffaire.Add(item.Affaire1.Numero);
                }

                //Liste LieuMission
                if (item.Entreprise1 != null && !listLieuMission.Contains(item.Entreprise1.Libelle))
                {
                    listLieuMission.Add(item.Entreprise1.Libelle);
                }

                //Liste numéro contrat
                if (item.Numero_Contrat != null && !listContrat.Contains(item.Numero_Contrat))
                {
                    listContrat.Add(item.Numero_Contrat);
                }

                //Liste Donneur d'ordre
                if (item.Salarie1 != null && !listDonneurOrdre.Contains(item.Salarie1.Personne.fullname))
                {
                    listDonneurOrdre.Add(item.Salarie1.Personne.fullname);
                }


                //Liste contact Mission personnel
                if (item.Salarie != null && !listContactMission_Personnel.Contains(item.Salarie.Personne.fullname))
                {
                    listContactMission_Personnel.Add(item.Salarie.Personne.fullname);
                }


                //Liste Contact mission client
                if (item.Contact1 != null && !listContactMission_Client.Contains(item.Contact1.Personne.fullname))
                {
                    listContactMission_Client.Add(item.Contact1.Personne.fullname);
                }
            }

            //Assignation des valeurs
            this._filterContainAffaire.ItemsSource = listAffaire;
            this._filterContainCommande.ItemsSource = listCommande;
            this._filterContainContact.ItemsSource = listContact;
            this._filterContainContactMission_Client.ItemsSource = listContactMission_Client;
            this._filterContainContactMission_Personnel.ItemsSource = listContactMission_Personnel;
            this._filterContainDonneurOrdre.ItemsSource = listDonneurOrdre;
            this._filterContainEntreprise.ItemsSource = listEntreprise;
            this._filterContainInterimaire.ItemsSource = listInterimaire;
            this._filterContainLieuMission.ItemsSource = listLieuMission;
            this._filterContainNumeroContrat.ItemsSource = listContrat;
        }

        private void initialisationComboBoxInt_Equ()
        {
            ObservableCollection<ItemOuiNon> listInt_Equ = new ObservableCollection<ItemOuiNon>();
            listInt_Equ.Add(new ItemOuiNon("Intérimaire & Tiers"));
            listInt_Equ.Add(new ItemOuiNon("Tiers"));
            listInt_Equ.Add(new ItemOuiNon("Intérimaire"));
            this._filterContainInt_Equ.ItemsSource = listInt_Equ;
        }

        private void initialisationPropDependance()
        {
            this.listEntrepriseMere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(ent => ent.Nom));
            this.listMotifMission = new ObservableCollection<Motif_Mission>(((App)App.Current).mySitaffEntities.Motif_Mission.OrderBy(mot => mot.Libelle));
        }

        #endregion

        #region Initialisation Données DataGridMain

        private void initialisationDataGridMain(ObservableCollection<Ordre_Mission> listToPut)
        {
            if (listToPut == null)
            {
                this.OrdresMission = new ObservableCollection<Ordre_Mission>(((App)App.Current).mySitaffEntities.Ordre_Mission.OrderBy(ord => ord.Date_Debut));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.OrdresMission = new ObservableCollection<Ordre_Mission>(listToPut);
                this.MiseAJourEtat("Filtrage", null);
            }
        }

        #endregion

        #region Clic droit

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
            itemAfficher5.Click += new RoutedEventHandler(delegate { this.clicDroitDupliquer(); });

            MenuItem itemAfficher8 = RemplirMenuAfficherMasquerColonnes(new MenuItem());
            itemAfficher8.Header = "Afficher / Masquer";

            if (((App)App.Current).securite.VerificationDroitActionsCRUD(this.ToString(), "Look"))
            {
                contextMenu.Items.Add(itemAfficher);
            }
            if (((App)App.Current).securite.VerificationDroitActionsCRUD(this.ToString(), "Add"))
            {
                contextMenu.Items.Add(itemAfficher2);
            }
            if (((App)App.Current).securite.VerificationDroitActionsCRUD(this.ToString(), "Update"))
            {
                contextMenu.Items.Add(itemAfficher3);
            }
            if (((App)App.Current).securite.VerificationDroitActionsCRUD(this.ToString(), "Remove"))
            {
                contextMenu.Items.Add(itemAfficher4);
            }
            if (((App)App.Current).securite.VerificationDroitActionsCRUD(this.ToString(), "Add"))
            {
                contextMenu.Items.Add(new Separator());
                contextMenu.Items.Add(itemAfficher5);
            }
            MenuItem itemAfficher9 = new MenuItem();
            itemAfficher9.Header = "Imprimer sans les prix";
            itemAfficher9.Click += new RoutedEventHandler(delegate { this.RapportImprimerSansPrix(); });

            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(itemAfficher9);

            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(itemAfficher8);
        }

        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {

            this.MenuItem_ColonneLieuMission = new MenuItem();
            this.MenuItem_ColonneLieuMission.IsChecked = false;
            this.MenuItem_ColonneLieuMission.Header = "Lieu de mission";
            this.MenuItem_ColonneLieuMission.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneLieuMission(); });
            this.AffMas_ColonneLieuMission();
            menuItem.Items.Add(this.MenuItem_ColonneLieuMission);

            this.MenuItem_ColonneSalarieAbs = new MenuItem();
            this.MenuItem_ColonneSalarieAbs.IsChecked = true;
            this.MenuItem_ColonneSalarieAbs.Header = "Salarié absent";
            this.MenuItem_ColonneSalarieAbs.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneSalarieAbs(); });
            this.AffMas_ColonneSalarieAbs();
            menuItem.Items.Add(this.MenuItem_ColonneSalarieAbs);

            this.MenuItem_ColonneMotifMission = new MenuItem();
            this.MenuItem_ColonneMotifMission.IsChecked = true;
            this.MenuItem_ColonneMotifMission.Header = "Motif de mission";
            this.MenuItem_ColonneMotifMission.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneMotifMission(); });
            this.AffMas_ColonneMotifMission();
            menuItem.Items.Add(this.MenuItem_ColonneMotifMission);

            this.MenuItem_ColonneNumeroAffaire = new MenuItem();
            this.MenuItem_ColonneNumeroAffaire.IsChecked = true;
            this.MenuItem_ColonneNumeroAffaire.Header = "Numéro d'affaire";
            this.MenuItem_ColonneNumeroAffaire.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumeroAffaire(); });
            this.AffMas_ColonneNumeroAffaire();
            menuItem.Items.Add(this.MenuItem_ColonneNumeroAffaire);

            this.MenuItem_ColonneNumeroContrat = new MenuItem();
            this.MenuItem_ColonneNumeroContrat.IsChecked = false;
            this.MenuItem_ColonneNumeroContrat.Header = "Numéro de contrat";
            this.MenuItem_ColonneNumeroContrat.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumeroContrat(); });
            this.AffMas_ColonneNumeroContrat();
            menuItem.Items.Add(this.MenuItem_ColonneNumeroContrat);

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

            this.MenuItem_ColonneHeureRDV = new MenuItem();
            this.MenuItem_ColonneHeureRDV.IsChecked = true;
            this.MenuItem_ColonneHeureRDV.Header = "Date RDV";
            this.MenuItem_ColonneHeureRDV.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneHeureRDV(); });
            this.AffMas_ColonneHeureRDV();
            menuItem.Items.Add(this.MenuItem_ColonneHeureRDV);

            this.MenuItem_ColonneEntrepriseMere = new MenuItem();
            this.MenuItem_ColonneEntrepriseMere.IsChecked = true;
            this.MenuItem_ColonneEntrepriseMere.Header = "Entreprise mère";
            this.MenuItem_ColonneEntrepriseMere.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEntrepriseMere(); });
            this.AffMas_ColonneEntrepriseMere();
            menuItem.Items.Add(this.MenuItem_ColonneEntrepriseMere);

            this.MenuItem_ColonneDonneurOrdre = new MenuItem();
            this.MenuItem_ColonneDonneurOrdre.IsChecked = true;
            this.MenuItem_ColonneDonneurOrdre.Header = "Donneur d'ordre";
            this.MenuItem_ColonneDonneurOrdre.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDonneurOrdre(); });
            this.AffMas_ColonneDonneurOrdre();
            menuItem.Items.Add(this.MenuItem_ColonneDonneurOrdre);

            this.MenuItem_ColonneCommentaire = new MenuItem();
            this.MenuItem_ColonneCommentaire.IsChecked = true;
            this.MenuItem_ColonneCommentaire.Header = "Commentaire";
            this.MenuItem_ColonneCommentaire.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneCommentaire(); });
            this.AffMas_ColonneCommentaire();
            menuItem.Items.Add(this.MenuItem_ColonneCommentaire);

            this.MenuItem_ColonneContactMission_Personnel = new MenuItem();
            this.MenuItem_ColonneContactMission_Personnel.IsChecked = true;
            this.MenuItem_ColonneContactMission_Personnel.Header = "Contact personnel sur chantier";
            this.MenuItem_ColonneContactMission_Personnel.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneContactMission_Personnel(); });
            this.AffMas_ColonneContactMission_Personnel();
            menuItem.Items.Add(this.MenuItem_ColonneContactMission_Personnel);

            this.MenuItem_ColonneContactMission_Client = new MenuItem();
            this.MenuItem_ColonneContactMission_Client.IsChecked = true;
            this.MenuItem_ColonneContactMission_Client.Header = "Contact client sur chantier";
            this.MenuItem_ColonneContactMission_Client.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneContactMission_Client(); });
            this.AffMas_ColonneContactMission_Client();
            menuItem.Items.Add(this.MenuItem_ColonneContactMission_Client);


            //Intérimaire
            MenuItem menuItemInterimaire = new MenuItem();
            menuItemInterimaire.Header = "Intérimaire";
            menuItem.Items.Add(menuItemInterimaire);

            this.MenuItem_ColonneIntTauxHoraire = new MenuItem();
            this.MenuItem_ColonneIntTauxHoraire.IsChecked = true;
            this.MenuItem_ColonneIntTauxHoraire.Header = "Intérimaire => Taux horaire";
            this.MenuItem_ColonneIntTauxHoraire.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneIntTauxHoraire(); });
            this.AffMas_ColonneIntTauxHoraire();
            menuItemInterimaire.Items.Add(this.MenuItem_ColonneIntTauxHoraire);

            this.MenuItem_ColonneIntDureeHebdo = new MenuItem();
            this.MenuItem_ColonneIntDureeHebdo.IsChecked = true;
            this.MenuItem_ColonneIntDureeHebdo.Header = "Intérimaire => Durée hebdomadaire";
            this.MenuItem_ColonneIntDureeHebdo.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneIntDureeHebdo(); });
            this.AffMas_ColonneIntDureeHebdo();
            menuItemInterimaire.Items.Add(this.MenuItem_ColonneIntDureeHebdo);

            this.MenuItem_ColonneIntMontant = new MenuItem();
            this.MenuItem_ColonneIntMontant.IsChecked = true;
            this.MenuItem_ColonneIntMontant.Header = "Intérimaire => Montant";
            this.MenuItem_ColonneIntMontant.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneIntMontant(); });
            this.AffMas_ColonneIntMontant();
            menuItemInterimaire.Items.Add(this.MenuItem_ColonneIntMontant);

            this.MenuItem_ColonneIntAccoss = new MenuItem();
            this.MenuItem_ColonneIntAccoss.IsChecked = true;
            this.MenuItem_ColonneIntAccoss.Header = "Intérimaire => Accoss";
            this.MenuItem_ColonneIntAccoss.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneIntAccoss(); });
            this.AffMas_ColonneIntAccoss();
            menuItemInterimaire.Items.Add(this.MenuItem_ColonneIntAccoss);

            this.MenuItem_ColonneIntTemps_Deplacement = new MenuItem();
            this.MenuItem_ColonneIntTemps_Deplacement.IsChecked = true;
            this.MenuItem_ColonneIntTemps_Deplacement.Header = "Intérimaire => Temps déplacement";
            this.MenuItem_ColonneIntTemps_Deplacement.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneIntTemps_Deplacement(); });
            this.AffMas_ColonneIntTemps_Deplacement();
            menuItemInterimaire.Items.Add(this.MenuItem_ColonneIntTemps_Deplacement);

            this.MenuItem_ColonneIntDistance_Deplacement = new MenuItem();
            this.MenuItem_ColonneIntDistance_Deplacement.IsChecked = true;
            this.MenuItem_ColonneIntDistance_Deplacement.Header = "Intérimaire => Distance déplacement";
            this.MenuItem_ColonneIntDistance_Deplacement.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneIntDistance_Deplacement(); });
            this.AffMas_ColonneIntDistance_Deplacement();
            menuItemInterimaire.Items.Add(this.MenuItem_ColonneIntDistance_Deplacement);

            this.MenuItem_ColonneIntMontant_Deplacement = new MenuItem();
            this.MenuItem_ColonneIntMontant_Deplacement.IsChecked = true;
            this.MenuItem_ColonneIntMontant_Deplacement.Header = "Intérimaire => Montant déplacement";
            this.MenuItem_ColonneIntMontant_Deplacement.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneIntMontant_Deplacement(); });
            this.AffMas_ColonneIntMontant_Deplacement();
            menuItemInterimaire.Items.Add(this.MenuItem_ColonneIntMontant_Deplacement);

            this.MenuItem_ColonneIntInterimaire = new MenuItem();
            this.MenuItem_ColonneIntInterimaire.IsChecked = false;
            this.MenuItem_ColonneIntInterimaire.Header = "Intérimaire";
            this.MenuItem_ColonneIntInterimaire.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneIntInterimaire(); });
            this.AffMas_ColonneIntInterimaire();
            menuItemInterimaire.Items.Add(this.MenuItem_ColonneIntInterimaire);

            this.MenuItem_ColonneIntEntreprise = new MenuItem();
            this.MenuItem_ColonneIntEntreprise.IsChecked = false;
            this.MenuItem_ColonneIntEntreprise.Header = "Intérimaire => Entreprise";
            this.MenuItem_ColonneIntEntreprise.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneIntEntreprise(); });
            this.AffMas_ColonneIntEntreprise();
            menuItemInterimaire.Items.Add(this.MenuItem_ColonneIntEntreprise);

            this.MenuItem_ColonneIntContact = new MenuItem();
            this.MenuItem_ColonneIntContact.IsChecked = false;
            this.MenuItem_ColonneIntContact.Header = "Intérimaire => Contact";
            this.MenuItem_ColonneIntContact.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneIntContact(); });
            this.AffMas_ColonneIntContact();
            menuItemInterimaire.Items.Add(this.MenuItem_ColonneIntContact);

            this.MenuItem_ColonneIntEventRemboursement = new MenuItem();
            this.MenuItem_ColonneIntEventRemboursement.IsChecked = true;
            this.MenuItem_ColonneIntEventRemboursement.Header = "Intérimaire => Evenènement remboursement";
            this.MenuItem_ColonneIntEventRemboursement.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneIntEventRemboursement(); });
            this.AffMas_ColonneIntEventRemboursement();
            menuItemInterimaire.Items.Add(this.MenuItem_ColonneIntEventRemboursement);


            //Equipe
            MenuItem menuItemEquipe = new MenuItem();
            menuItemEquipe.Header = "Equipe";
            menuItem.Items.Add(menuItemEquipe);

            this.MenuItem_ColonneEquTauxHoraire = new MenuItem();
            this.MenuItem_ColonneEquTauxHoraire.IsChecked = true;
            this.MenuItem_ColonneEquTauxHoraire.Header = "Equipe => Taux horaire";
            this.MenuItem_ColonneEquTauxHoraire.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEquTauxHoraire(); });
            this.AffMas_ColonneEquTauxHoraire();
            menuItemEquipe.Items.Add(this.MenuItem_ColonneEquTauxHoraire);

            this.MenuItem_ColonneEquDureeHebdo = new MenuItem();
            this.MenuItem_ColonneEquDureeHebdo.IsChecked = true;
            this.MenuItem_ColonneEquDureeHebdo.Header = "Equipe => Durée journalière";
            this.MenuItem_ColonneEquDureeHebdo.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEquDureeHebdo(); });
            this.AffMas_ColonneEquDureeHebdo();
            menuItemEquipe.Items.Add(this.MenuItem_ColonneEquDureeHebdo);

            this.MenuItem_ColonneEquMontant = new MenuItem();
            this.MenuItem_ColonneEquMontant.IsChecked = true;
            this.MenuItem_ColonneEquMontant.Header = "Equipe => Montant";
            this.MenuItem_ColonneEquMontant.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEquMontant(); });
            this.AffMas_ColonneEquMontant();
            menuItemEquipe.Items.Add(this.MenuItem_ColonneEquMontant);

            this.MenuItem_ColonneEquEntreprise = new MenuItem();
            this.MenuItem_ColonneEquEntreprise.IsChecked = false;
            this.MenuItem_ColonneEquEntreprise.Header = "Equipe => Entreprise";
            this.MenuItem_ColonneEquEntreprise.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEquEntreprise(); });
            this.AffMas_ColonneEquEntreprise();
            menuItemEquipe.Items.Add(this.MenuItem_ColonneEquEntreprise);

            this.MenuItem_ColonneEquContact = new MenuItem();
            this.MenuItem_ColonneEquContact.IsChecked = false;
            this.MenuItem_ColonneEquContact.Header = "Equipe => Contact";
            this.MenuItem_ColonneEquContact.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEquContact(); });
            this.AffMas_ColonneEquContact();
            menuItemEquipe.Items.Add(this.MenuItem_ColonneEquContact);

            this.MenuItem_ColonneEquCommande = new MenuItem();
            this.MenuItem_ColonneEquCommande.IsChecked = true;
            this.MenuItem_ColonneEquCommande.Header = "Equipe => Commande";
            this.MenuItem_ColonneEquCommande.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneEquCommande(); });
            this.AffMas_ColonneEquCommande();
            menuItemEquipe.Items.Add(this.MenuItem_ColonneEquCommande);

            menuItem.Items.Add(new Separator());

            MenuItem menuItemAfficherTout = new MenuItem();
            menuItemAfficherTout.Header = "Afficher tout ";
            menuItem.Items.Add(menuItemAfficherTout);

            this.MenuItem_AfficherTout = new MenuItem();
            this.MenuItem_AfficherTout.Header = "Afficher tout";
            this.MenuItem_AfficherTout.Click += new RoutedEventHandler(delegate { this.AffMas_AfficherTout(); });
            menuItemAfficherTout.Items.Add(this.MenuItem_AfficherTout);

            this.MenuItem_AfficherInterimaire = new MenuItem();
            this.MenuItem_AfficherInterimaire.Header = "Afficher Intérimaire";
            this.MenuItem_AfficherInterimaire.Click += new RoutedEventHandler(delegate { this.AffMas_AfficherInterimaire(); });
            menuItemAfficherTout.Items.Add(this.MenuItem_AfficherInterimaire);

            this.MenuItem_AfficherEquipe = new MenuItem();
            this.MenuItem_AfficherEquipe.Header = "Afficher Equipe";
            this.MenuItem_AfficherEquipe.Click += new RoutedEventHandler(delegate { this.AffMas_AfficherEquipe(); });
            menuItemAfficherTout.Items.Add(this.MenuItem_AfficherEquipe);

            MenuItem menuItemMasquerTout = new MenuItem();
            menuItemMasquerTout.Header = "Masquer tout ";
            menuItem.Items.Add(menuItemMasquerTout);

            this.MenuItem_MasquerTout = new MenuItem();
            this.MenuItem_MasquerTout.Header = "Masquer tout";
            this.MenuItem_MasquerTout.Click += new RoutedEventHandler(delegate { this.AffMas_MasquerTout(); });
            menuItemMasquerTout.Items.Add(this.MenuItem_MasquerTout);

            this.MenuItem_MasquerInterimaire = new MenuItem();
            this.MenuItem_MasquerInterimaire.Header = "Masquer Intérimaire";
            this.MenuItem_MasquerInterimaire.Click += new RoutedEventHandler(delegate { this.AffMas_MasquerInterimaire(); });
            menuItemMasquerTout.Items.Add(this.MenuItem_MasquerInterimaire);

            this.MenuItem_MasquerEquipe = new MenuItem();
            this.MenuItem_MasquerEquipe.Header = "Masquer Equipe";
            this.MenuItem_MasquerEquipe.Click += new RoutedEventHandler(delegate { this.AffMas_MasquerEquipe(); });
            menuItemMasquerTout.Items.Add(this.MenuItem_MasquerEquipe);


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

        private void clicDroitDupliquer()
        {
            ((App)App.Current)._theMainWindow._CommandDuplicateOrdreMission.Command.Execute(((App)App.Current)._theMainWindow);
        }

        #region Afficher / Masquer

        #region Tout

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneCommentaire.IsChecked = false;
            this.MenuItem_ColonneContactMission_Client.IsChecked = false;
            this.MenuItem_ColonneContactMission_Personnel.IsChecked = false;
            this.MenuItem_ColonneDateDebut.IsChecked = false;
            this.MenuItem_ColonneDateFin.IsChecked = false;
            this.MenuItem_ColonneHeureRDV.IsChecked = false;
            this.MenuItem_ColonneDonneurOrdre.IsChecked = false;
            this.MenuItem_ColonneEntrepriseMere.IsChecked = false;
            this.MenuItem_ColonneEquCommande.IsChecked = false;
            this.MenuItem_ColonneEquContact.IsChecked = false;
            this.MenuItem_ColonneEquDureeHebdo.IsChecked = false;
            this.MenuItem_ColonneEquEntreprise.IsChecked = false;
            this.MenuItem_ColonneEquMontant.IsChecked = false;
            this.MenuItem_ColonneEquTauxHoraire.IsChecked = false;
            this.MenuItem_ColonneIntAccoss.IsChecked = false;
            this.MenuItem_ColonneIntDistance_Deplacement.IsChecked = false;
            this.MenuItem_ColonneIntDureeHebdo.IsChecked = false;
            this.MenuItem_ColonneIntEntreprise.IsChecked = false;
            this.MenuItem_ColonneIntEventRemboursement.IsChecked = false;
            this.MenuItem_ColonneIntInterimaire.IsChecked = false;
            this.MenuItem_ColonneIntContact.IsChecked = false;
            this.MenuItem_ColonneIntMontant.IsChecked = false;
            this.MenuItem_ColonneIntMontant_Deplacement.IsChecked = false;
            this.MenuItem_ColonneIntTauxHoraire.IsChecked = false;
            this.MenuItem_ColonneIntTemps_Deplacement.IsChecked = false;
            this.MenuItem_ColonneLieuMission.IsChecked = false;
            this.MenuItem_ColonneSalarieAbs.IsChecked = false;
            this.MenuItem_ColonneMotifMission.IsChecked = false;
            this.MenuItem_ColonneNumeroAffaire.IsChecked = false;
            this.MenuItem_ColonneNumeroContrat.IsChecked = false;

            this.AffMas_ColonneCommentaire();
            this.AffMas_ColonneContactMission_Client();
            this.AffMas_ColonneContactMission_Personnel();
            this.AffMas_ColonneDateDebut();
            this.AffMas_ColonneDateFin();
            this.AffMas_ColonneHeureRDV();
            this.AffMas_ColonneDonneurOrdre();
            this.AffMas_ColonneEntrepriseMere();
            this.AffMas_ColonneEquCommande();
            this.AffMas_ColonneEquContact();
            this.AffMas_ColonneEquDureeHebdo();
            this.AffMas_ColonneEquEntreprise();
            this.AffMas_ColonneEquMontant();
            this.AffMas_ColonneEquTauxHoraire();
            this.AffMas_ColonneIntAccoss();
            this.AffMas_ColonneIntDistance_Deplacement();
            this.AffMas_ColonneIntDureeHebdo();
            this.AffMas_ColonneIntEntreprise();
            this.AffMas_ColonneIntEventRemboursement();
            this.AffMas_ColonneIntInterimaire();
            this.AffMas_ColonneIntContact();
            this.AffMas_ColonneIntMontant();
            this.AffMas_ColonneIntMontant_Deplacement();
            this.AffMas_ColonneIntTauxHoraire();
            this.AffMas_ColonneIntTemps_Deplacement();
            this.AffMas_ColonneLieuMission();
            this.AffMas_ColonneSalarieAbs();
            this.AffMas_ColonneMotifMission();
            this.AffMas_ColonneNumeroAffaire();
            this.AffMas_ColonneNumeroContrat();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneCommentaire.IsChecked = true;
            this.MenuItem_ColonneContactMission_Client.IsChecked = true;
            this.MenuItem_ColonneContactMission_Personnel.IsChecked = true;
            this.MenuItem_ColonneDateDebut.IsChecked = true;
            this.MenuItem_ColonneDateFin.IsChecked = true;
            this.MenuItem_ColonneHeureRDV.IsChecked = true;
            this.MenuItem_ColonneDonneurOrdre.IsChecked = true;
            this.MenuItem_ColonneEntrepriseMere.IsChecked = true;
            this.MenuItem_ColonneEquCommande.IsChecked = true;
            this.MenuItem_ColonneEquContact.IsChecked = true;
            this.MenuItem_ColonneEquDureeHebdo.IsChecked = true;
            this.MenuItem_ColonneEquEntreprise.IsChecked = true;
            this.MenuItem_ColonneEquMontant.IsChecked = true;
            this.MenuItem_ColonneEquTauxHoraire.IsChecked = true;
            this.MenuItem_ColonneIntAccoss.IsChecked = true;
            this.MenuItem_ColonneIntDistance_Deplacement.IsChecked = true;
            this.MenuItem_ColonneIntDureeHebdo.IsChecked = true;
            this.MenuItem_ColonneIntEntreprise.IsChecked = true;
            this.MenuItem_ColonneIntEventRemboursement.IsChecked = true;
            this.MenuItem_ColonneIntInterimaire.IsChecked = true;
            this.MenuItem_ColonneIntContact.IsChecked = true;
            this.MenuItem_ColonneIntMontant.IsChecked = true;
            this.MenuItem_ColonneIntMontant_Deplacement.IsChecked = true;
            this.MenuItem_ColonneIntTauxHoraire.IsChecked = true;
            this.MenuItem_ColonneIntTemps_Deplacement.IsChecked = true;
            this.MenuItem_ColonneLieuMission.IsChecked = true;
            this.MenuItem_ColonneSalarieAbs.IsChecked = true;
            this.MenuItem_ColonneMotifMission.IsChecked = true;
            this.MenuItem_ColonneNumeroAffaire.IsChecked = true;
            this.MenuItem_ColonneNumeroContrat.IsChecked = true;

            this.AffMas_ColonneCommentaire();
            this.AffMas_ColonneContactMission_Client();
            this.AffMas_ColonneContactMission_Personnel();
            this.AffMas_ColonneDateDebut();
            this.AffMas_ColonneDateFin();
            this.AffMas_ColonneHeureRDV();
            this.AffMas_ColonneDonneurOrdre();
            this.AffMas_ColonneEntrepriseMere();
            this.AffMas_ColonneEquCommande();
            this.AffMas_ColonneEquContact();
            this.AffMas_ColonneEquDureeHebdo();
            this.AffMas_ColonneEquEntreprise();
            this.AffMas_ColonneEquMontant();
            this.AffMas_ColonneEquTauxHoraire();
            this.AffMas_ColonneIntAccoss();
            this.AffMas_ColonneIntDistance_Deplacement();
            this.AffMas_ColonneIntDureeHebdo();
            this.AffMas_ColonneIntEntreprise();
            this.AffMas_ColonneIntEventRemboursement();
            this.AffMas_ColonneIntInterimaire();
            this.AffMas_ColonneIntContact();
            this.AffMas_ColonneIntMontant();
            this.AffMas_ColonneIntMontant_Deplacement();
            this.AffMas_ColonneIntTauxHoraire();
            this.AffMas_ColonneIntTemps_Deplacement();
            this.AffMas_ColonneLieuMission();
            this.AffMas_ColonneSalarieAbs();
            this.AffMas_ColonneMotifMission();
            this.AffMas_ColonneNumeroAffaire();
            this.AffMas_ColonneNumeroContrat();
        }

        #endregion

        #region Bonus

        private void AffMas_AfficherInterimaire()
        {
            this.MenuItem_ColonneIntAccoss.IsChecked = false;
            this.MenuItem_ColonneIntDistance_Deplacement.IsChecked = false;
            this.MenuItem_ColonneIntDureeHebdo.IsChecked = false;
            this.MenuItem_ColonneIntEntreprise.IsChecked = false;
            this.MenuItem_ColonneIntEventRemboursement.IsChecked = false;
            this.MenuItem_ColonneIntInterimaire.IsChecked = false;
            this.MenuItem_ColonneIntContact.IsChecked = false;
            this.MenuItem_ColonneIntMontant.IsChecked = false;
            this.MenuItem_ColonneIntMontant_Deplacement.IsChecked = false;
            this.MenuItem_ColonneIntTauxHoraire.IsChecked = false;
            this.MenuItem_ColonneIntTemps_Deplacement.IsChecked = false;

            this.AffMas_ColonneIntAccoss();
            this.AffMas_ColonneIntDistance_Deplacement();
            this.AffMas_ColonneIntDureeHebdo();
            this.AffMas_ColonneIntEntreprise();
            this.AffMas_ColonneIntEventRemboursement();
            this.AffMas_ColonneIntInterimaire();
            this.AffMas_ColonneIntContact();
            this.AffMas_ColonneIntMontant();
            this.AffMas_ColonneIntMontant_Deplacement();
            this.AffMas_ColonneIntTauxHoraire();
            this.AffMas_ColonneIntTemps_Deplacement();
        }

        private void AffMas_MasquerInterimaire()
        {
            this.MenuItem_ColonneIntAccoss.IsChecked = true;
            this.MenuItem_ColonneIntDistance_Deplacement.IsChecked = true;
            this.MenuItem_ColonneIntDureeHebdo.IsChecked = true;
            this.MenuItem_ColonneIntEntreprise.IsChecked = true;
            this.MenuItem_ColonneIntEventRemboursement.IsChecked = true;
            this.MenuItem_ColonneIntInterimaire.IsChecked = true;
            this.MenuItem_ColonneIntContact.IsChecked = true;
            this.MenuItem_ColonneIntMontant.IsChecked = true;
            this.MenuItem_ColonneIntMontant_Deplacement.IsChecked = true;
            this.MenuItem_ColonneIntTauxHoraire.IsChecked = true;
            this.MenuItem_ColonneIntTemps_Deplacement.IsChecked = true;

            this.AffMas_ColonneIntAccoss();
            this.AffMas_ColonneIntDistance_Deplacement();
            this.AffMas_ColonneIntDureeHebdo();
            this.AffMas_ColonneIntEntreprise();
            this.AffMas_ColonneIntEventRemboursement();
            this.AffMas_ColonneIntInterimaire();
            this.AffMas_ColonneIntContact();
            this.AffMas_ColonneIntMontant();
            this.AffMas_ColonneIntMontant_Deplacement();
            this.AffMas_ColonneIntTauxHoraire();
            this.AffMas_ColonneIntTemps_Deplacement();
        }

        private void AffMas_AfficherEquipe()
        {
            this.MenuItem_ColonneEquCommande.IsChecked = false;
            this.MenuItem_ColonneEquContact.IsChecked = false;
            this.MenuItem_ColonneEquDureeHebdo.IsChecked = false;
            this.MenuItem_ColonneEquEntreprise.IsChecked = false;
            this.MenuItem_ColonneEquMontant.IsChecked = false;
            this.MenuItem_ColonneEquTauxHoraire.IsChecked = false;

            this.AffMas_ColonneEquCommande();
            this.AffMas_ColonneEquContact();
            this.AffMas_ColonneEquDureeHebdo();
            this.AffMas_ColonneEquEntreprise();
            this.AffMas_ColonneEquMontant();
            this.AffMas_ColonneEquTauxHoraire();
        }

        private void AffMas_MasquerEquipe()
        {
            this.MenuItem_ColonneEquCommande.IsChecked = true;
            this.MenuItem_ColonneEquContact.IsChecked = true;
            this.MenuItem_ColonneEquDureeHebdo.IsChecked = true;
            this.MenuItem_ColonneEquEntreprise.IsChecked = true;
            this.MenuItem_ColonneEquMontant.IsChecked = true;
            this.MenuItem_ColonneEquTauxHoraire.IsChecked = true;

            this.AffMas_ColonneEquCommande();
            this.AffMas_ColonneEquContact();
            this.AffMas_ColonneEquDureeHebdo();
            this.AffMas_ColonneEquEntreprise();
            this.AffMas_ColonneEquMontant();
            this.AffMas_ColonneEquTauxHoraire();
        }

        #endregion

        private void AffMas_ColonneNumeroContrat()
        {
            if (this.MenuItem_ColonneNumeroContrat.IsChecked == true)
            {
                this._ColonneNumeroContrat.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNumeroContrat.IsChecked = false;
            }
            else
            {
                this._ColonneNumeroContrat.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNumeroContrat.IsChecked = true;
            }
        }

        private void AffMas_ColonneSalarieAbs()
        {
            if (this.MenuItem_ColonneSalarieAbs.IsChecked == true)
            {
                this._ColonneSalarieAbs.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneSalarieAbs.IsChecked = false;
            }
            else
            {
                this._ColonneSalarieAbs.Visibility = Visibility.Visible;
                this.MenuItem_ColonneSalarieAbs.IsChecked = true;
            }
        }

        private void AffMas_ColonneNumeroAffaire()
        {
            if (this.MenuItem_ColonneNumeroAffaire.IsChecked == true)
            {
                this._ColonneNumeroAffaire.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNumeroAffaire.IsChecked = false;
            }
            else
            {
                this._ColonneNumeroAffaire.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNumeroAffaire.IsChecked = true;
            }
        }

        private void AffMas_ColonneEquCommande()
        {
            if (this.MenuItem_ColonneEquCommande.IsChecked == true)
            {
                this._ColonneEquCommande.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneEquCommande.IsChecked = false;
            }
            else
            {
                this._ColonneEquCommande.Visibility = Visibility.Visible;
                this.MenuItem_ColonneEquCommande.IsChecked = true;
            }
        }

        private void AffMas_ColonneEquContact()
        {
            if (this.MenuItem_ColonneEquContact.IsChecked == true)
            {
                this._ColonneEquContact.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneEquContact.IsChecked = false;
            }
            else
            {
                this._ColonneEquContact.Visibility = Visibility.Visible;
                this.MenuItem_ColonneEquContact.IsChecked = true;
            }
        }

        private void AffMas_ColonneEquEntreprise()
        {
            if (this.MenuItem_ColonneEquEntreprise.IsChecked == true)
            {
                this._ColonneEquEntreprise.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneEquEntreprise.IsChecked = false;
            }
            else
            {
                this._ColonneEquEntreprise.Visibility = Visibility.Visible;
                this.MenuItem_ColonneEquEntreprise.IsChecked = true;
            }
        }

        private void AffMas_ColonneEquMontant()
        {
            if (this.MenuItem_ColonneEquMontant.IsChecked == true)
            {
                this._ColonneEquMontant.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneEquMontant.IsChecked = false;
            }
            else
            {
                this._ColonneEquMontant.Visibility = Visibility.Visible;
                this.MenuItem_ColonneEquMontant.IsChecked = true;
            }
        }

        private void AffMas_ColonneEquDureeHebdo()
        {
            if (this.MenuItem_ColonneEquDureeHebdo.IsChecked == true)
            {
                this._ColonneEquDureeHebdo.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneEquDureeHebdo.IsChecked = false;
            }
            else
            {
                this._ColonneEquDureeHebdo.Visibility = Visibility.Visible;
                this.MenuItem_ColonneEquDureeHebdo.IsChecked = true;
            }
        }

        private void AffMas_ColonneEquTauxHoraire()
        {
            if (this.MenuItem_ColonneEquTauxHoraire.IsChecked == true)
            {
                this._ColonneEquTauxHoraire.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneEquTauxHoraire.IsChecked = false;
            }
            else
            {
                this._ColonneEquTauxHoraire.Visibility = Visibility.Visible;
                this.MenuItem_ColonneEquTauxHoraire.IsChecked = true;
            }
        }

        private void AffMas_ColonneIntEventRemboursement()
        {
            if (this.MenuItem_ColonneIntEventRemboursement.IsChecked == true)
            {
                this._ColonneIntEventRemboursement.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneIntEventRemboursement.IsChecked = false;
            }
            else
            {
                this._ColonneIntEventRemboursement.Visibility = Visibility.Visible;
                this.MenuItem_ColonneIntEventRemboursement.IsChecked = true;
            }
        }

        private void AffMas_ColonneIntEntreprise()
        {
            if (this.MenuItem_ColonneIntEntreprise.IsChecked == true)
            {
                this._ColonneIntEntreprise.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneIntEntreprise.IsChecked = false;
            }
            else
            {
                this._ColonneIntEntreprise.Visibility = Visibility.Visible;
                this.MenuItem_ColonneIntEntreprise.IsChecked = true;
            }
        }

        private void AffMas_ColonneIntInterimaire()
        {
            if (this.MenuItem_ColonneIntInterimaire.IsChecked == true)
            {
                this._ColonneIntInterimaire.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneIntInterimaire.IsChecked = false;
            }
            else
            {
                this._ColonneIntInterimaire.Visibility = Visibility.Visible;
                this.MenuItem_ColonneIntInterimaire.IsChecked = true;
            }
        }

        private void AffMas_ColonneIntMontant_Deplacement()
        {
            if (this.MenuItem_ColonneIntMontant_Deplacement.IsChecked == true)
            {
                this._ColonneIntMontant_Deplacement.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneIntMontant_Deplacement.IsChecked = false;
            }
            else
            {
                this._ColonneIntMontant_Deplacement.Visibility = Visibility.Visible;
                this.MenuItem_ColonneIntMontant_Deplacement.IsChecked = true;
            }
        }

        private void AffMas_ColonneIntDistance_Deplacement()
        {
            if (this.MenuItem_ColonneIntDistance_Deplacement.IsChecked == true)
            {
                this._ColonneIntDistance_Deplacement.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneIntDistance_Deplacement.IsChecked = false;
            }
            else
            {
                this._ColonneIntDistance_Deplacement.Visibility = Visibility.Visible;
                this.MenuItem_ColonneIntDistance_Deplacement.IsChecked = true;
            }
        }

        private void AffMas_ColonneIntTemps_Deplacement()
        {
            if (this.MenuItem_ColonneIntTemps_Deplacement.IsChecked == true)
            {
                this._ColonneIntTemps_Deplacement.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneIntTemps_Deplacement.IsChecked = false;
            }
            else
            {
                this._ColonneIntTemps_Deplacement.Visibility = Visibility.Visible;
                this.MenuItem_ColonneIntTemps_Deplacement.IsChecked = true;
            }
        }

        private void AffMas_ColonneIntAccoss()
        {
            if (this.MenuItem_ColonneIntAccoss.IsChecked == true)
            {
                this._ColonneIntAccoss.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneIntAccoss.IsChecked = false;
            }
            else
            {
                this._ColonneIntAccoss.Visibility = Visibility.Visible;
                this.MenuItem_ColonneIntAccoss.IsChecked = true;
            }
        }

        private void AffMas_ColonneIntContact()
        {
            if (this.MenuItem_ColonneIntContact.IsChecked == true)
            {
                this._ColonneIntContact.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneIntContact.IsChecked = false;
            }
            else
            {
                this._ColonneIntContact.Visibility = Visibility.Visible;
                this.MenuItem_ColonneIntContact.IsChecked = true;
            }
        }

        private void AffMas_ColonneIntMontant()
        {
            if (this.MenuItem_ColonneIntMontant.IsChecked == true)
            {
                this._ColonneIntMontant.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneIntMontant.IsChecked = false;
            }
            else
            {
                this._ColonneIntMontant.Visibility = Visibility.Visible;
                this.MenuItem_ColonneIntMontant.IsChecked = true;
            }
        }

        private void AffMas_ColonneIntDureeHebdo()
        {
            if (this.MenuItem_ColonneIntDureeHebdo.IsChecked == true)
            {
                this._ColonneIntDureeHebdo.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneIntDureeHebdo.IsChecked = false;
            }
            else
            {
                this._ColonneIntDureeHebdo.Visibility = Visibility.Visible;
                this.MenuItem_ColonneIntDureeHebdo.IsChecked = true;
            }
        }

        private void AffMas_ColonneIntTauxHoraire()
        {
            if (this.MenuItem_ColonneIntTauxHoraire.IsChecked == true)
            {
                this._ColonneIntTauxHoraire.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneIntTauxHoraire.IsChecked = false;
            }
            else
            {
                this._ColonneIntTauxHoraire.Visibility = Visibility.Visible;
                this.MenuItem_ColonneIntTauxHoraire.IsChecked = true;
            }
        }

        private void AffMas_ColonneLieuMission()
        {
            if (this.MenuItem_ColonneLieuMission.IsChecked == true)
            {
                this._ColonneLieuMission.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneLieuMission.IsChecked = false;
            }
            else
            {
                this._ColonneLieuMission.Visibility = Visibility.Visible;
                this.MenuItem_ColonneLieuMission.IsChecked = true;
            }
        }

        private void AffMas_ColonneContactMission_Client()
        {
            if (this.MenuItem_ColonneContactMission_Client.IsChecked == true)
            {
                this._ColonneContactMission_Client.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneContactMission_Client.IsChecked = false;
            }
            else
            {
                this._ColonneContactMission_Client.Visibility = Visibility.Visible;
                this.MenuItem_ColonneContactMission_Client.IsChecked = true;
            }
        }

        private void AffMas_ColonneContactMission_Personnel()
        {
            if (this.MenuItem_ColonneContactMission_Personnel.IsChecked == true)
            {
                this._ColonneContactMission_Personnel.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneContactMission_Personnel.IsChecked = false;
            }
            else
            {
                this._ColonneContactMission_Personnel.Visibility = Visibility.Visible;
                this.MenuItem_ColonneContactMission_Personnel.IsChecked = true;
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

        private void AffMas_ColonneDonneurOrdre()
        {
            if (this.MenuItem_ColonneDonneurOrdre.IsChecked == true)
            {
                this._ColonneDonneurOrdre.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDonneurOrdre.IsChecked = false;
            }
            else
            {
                this._ColonneDonneurOrdre.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDonneurOrdre.IsChecked = true;
            }
        }

        private void AffMas_ColonneMotifMission()
        {
            if (this.MenuItem_ColonneMotifMission.IsChecked == true)
            {
                this._ColonneMotifMission.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneMotifMission.IsChecked = false;
            }
            else
            {
                this._ColonneMotifMission.Visibility = Visibility.Visible;
                this.MenuItem_ColonneMotifMission.IsChecked = true;
            }
        }

        private void AffMas_ColonneHeureRDV()
        {
            if (this.MenuItem_ColonneHeureRDV.IsChecked == true)
            {
                this._ColonneHeureRDV.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneHeureRDV.IsChecked = false;
            }
            else
            {
                this._ColonneHeureRDV.Visibility = Visibility.Visible;
                this.MenuItem_ColonneHeureRDV.IsChecked = true;
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

        #endregion

        #endregion

        #endregion

        #region Fenêtre chargée

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((MainWindow)((Grid)((Border)this.Parent).Parent).Parent).stopThread();
        }

        #endregion

        #region CRUD (Create Read Update Delete)

        /// <summary>
        /// Ajoute une nouvelle entreprise à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Ordre_Mission Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Choix d'un type d'ordre de mission en cours ...");

            //Initialisation de la fenêtre
            TypeOrdreMissionWindow typeOrdreMissionWindow = new TypeOrdreMissionWindow();

            //Définition du type de commande
            bool? dialogChoix = typeOrdreMissionWindow.ShowDialog();

            if (dialogChoix.HasValue && dialogChoix.Value == true)
            {
                //Affichage du résultat du choix
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Choix réalisé");

                //Affichage du message "ajout en cours"
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un ordre de mission en cours ...");

                //Initialisation de la fenêtre
                OrdreMissionWindow ordreMissionWindow = new OrdreMissionWindow();

                //Création de l'objet temporaire
                Ordre_Mission tmp = new Ordre_Mission();

                if (typeOrdreMissionWindow.interimaire == true)
                {
                    tmp.Mission_Interimaire1 = new Mission_Interimaire();
                    tmp.Interimaire = true;
                }
                else if (typeOrdreMissionWindow.equipe == true)
                {
                    tmp.Mission_Tiers1 = new Mission_Tiers();
                    tmp.Equipe_Tiers = true;
                }

                //Mise de l'objet temporaire dans le datacontext
                ordreMissionWindow.DataContext = tmp;
                ordreMissionWindow.creation = true;

                //booléen nullable vrai ou faux ou null
                bool? dialogResult = ordreMissionWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    //Si j'appuie sur le bouton Ok, je renvoi l'objet ordreMission dans le datacontext de la fenêtre
                    return (Ordre_Mission)ordreMissionWindow.DataContext;
                }
                else
                {
                    try
                    {
                        //Detachement de tous les éléments liés à une Mission tiers
                        if (((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Tiers1 != null)
                        {
                            while (((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Tiers1.Mission_TiersQualification.Count() > 0)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Tiers1.Mission_TiersQualification.First());
                            }
                            ((App)App.Current).mySitaffEntities.Detach(((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Tiers1);
                        }

                        //Detachement de tous les éléments liés à une Mission intérimaire
                        if (((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Interimaire1 != null)
                        {
                            while (((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Interimaire1.Mission_InterimaireQualification.Count() > 0)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Interimaire1.Mission_InterimaireQualification.First());
                            }
                            ((App)App.Current).mySitaffEntities.Detach(((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Interimaire1);
                        }

                        ((App)App.Current).mySitaffEntities.Detach((Ordre_Mission)ordreMissionWindow.DataContext);
                    }
                    catch (Exception)
                    {

                    }
                    //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    this.recalculMax();
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un ordre de mission annulé : " + this.OrdresMission.Count() + " / " + this.max);

                    return null;
                }
            }
            //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
            this.recalculMax();
            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un ordre de mission annulé : " + this.OrdresMission.Count() + " / " + this.max);

            return null;
        }

        /// <summary>
        /// Ouvre l'entreprise séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Ordre_Mission Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un ordre de mission en cours ...");

                    //Création de la fenêtre
                    OrdreMissionWindow ordreMissionWindow = new OrdreMissionWindow();

                    //Initialisation du Datacontext en Ordre_Mission et association à l'Ordre_Mission sélectionné
                    ordreMissionWindow.DataContext = new Ordre_Mission();
                    ordreMissionWindow.DataContext = (Ordre_Mission)this._DataGridMain.SelectedItem;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = ordreMissionWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet Ordre_Mission dans le datacontext de la fenêtre
                        return (Ordre_Mission)ordreMissionWindow.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Ordre_Mission)(this._DataGridMain.SelectedItem));
                        //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un ordre de mission annulé : " + this.OrdresMission.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un ordre de mission.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un ordre de mission.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime l'entreprise séléctionnée avec une confirmation
        /// </summary>
        public Ordre_Mission Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un ordre de mission en cours ...");

                    bool test = true;

                    if (test)
                    {
                        if (MessageBox.Show("Voulez-vous rééllement supprimer l'ordre de mission séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            if (((Ordre_Mission)this._DataGridMain.SelectedItem).Mission_Tiers1 != null)
                            {
                                try
                                {
                                    ((App)App.Current).mySitaffEntities.Commande_Fournisseur.DeleteObject(((Ordre_Mission)this._DataGridMain.SelectedItem).Mission_Tiers1.Commande_Fournisseur1);
                                }
                                catch (Exception)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(((Ordre_Mission)this._DataGridMain.SelectedItem).Mission_Tiers1.Commande_Fournisseur1);
                                }
                                try
                                {
                                    ((App)App.Current).mySitaffEntities.Mission_Tiers.DeleteObject(((Ordre_Mission)this._DataGridMain.SelectedItem).Mission_Tiers1);
                                }
                                catch (Exception)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(((Ordre_Mission)this._DataGridMain.SelectedItem).Mission_Tiers1);
                                }
                            }
                            if (((Ordre_Mission)this._DataGridMain.SelectedItem).Mission_Interimaire1 != null)
                            {
                                try
                                {
                                    ((App)App.Current).mySitaffEntities.Mission_Interimaire.DeleteObject(((Ordre_Mission)this._DataGridMain.SelectedItem).Mission_Interimaire1);
                                }
                                catch (Exception)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(((Ordre_Mission)this._DataGridMain.SelectedItem).Mission_Tiers1);
                                }
                            }
                            return ((Ordre_Mission)this._DataGridMain.SelectedItem);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul ordre de mission.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un ordre de mission.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
            return null;
        }

        /// <summary>
        /// Ouvre l'entreprise séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Ordre_Mission Look(Ordre_Mission ordreMission)
        {
            if (this._DataGridMain.SelectedItem != null || ordreMission != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1 || ordreMission != null)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un ordre de mission en cours ...");

                    //Création de la fenêtre
                    OrdreMissionWindow ordreMissionWindow = new OrdreMissionWindow();

                    //Initialisation du Datacontext en ordre de mission et association à l'ordreMission sélectionnée
                    ordreMissionWindow.DataContext = new Ordre_Mission();
                    if (ordreMission != null)
                    {
                        ordreMissionWindow.DataContext = ordreMission;
                    }
                    else
                    {
                        ordreMissionWindow.DataContext = (Ordre_Mission)this._DataGridMain.SelectedItem;
                    }

                    //Je positionne la lecture seule sur la fenêtre
                    ordreMissionWindow.lectureSeule();

                    //J'affiche la fenêtre
                    bool? dialogResult = ordreMissionWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un ordre de mission terminé : " + this.OrdresMission.Count() + " / " + this.max);

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul ordre de mission.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un ordre de mission.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        #endregion

        #region Actions suplémentaires

        public Commande_Fournisseur RapportImprimerSansPrix()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    try
                    {
                        ReportingWindow reportingWindow = new ReportingWindow();
                        long toShow = ((Ordre_Mission)this._DataGridMain.SelectedItem).Mission_Tiers1.Commande_Fournisseur1.Identifiant;
                        reportingWindow._webBrowser.Source = new Uri("http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fACHATS%2fCOMMANDE+FOURNITURE&rs:Command=Render&Commande_Fournisseur=" + toShow + "&affichage_montant=false");
                        reportingWindow.Title = "Rapport pour impression : commande n° - " + ((Ordre_Mission)this._DataGridMain.SelectedItem).Mission_Tiers1.Commande_Fournisseur1.Numero + "-";

                        reportingWindow.Show();
                        return null;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("L'ordre de mission n'a pas de commande");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule commande fournisseur.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une commande fournisseur.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// duplique un ordre de mission à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Ordre_Mission Duplicate()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "ajout en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer un ordre de mission en cours ...");

                    //Création de la fenêtre
                    OrdreMissionWindow ordreMissionWindow = new OrdreMissionWindow();

                    //Duplication de la commande sélectionnée
                    Ordre_Mission tmp = new Ordre_Mission();
                    tmp = duplicateOrdreMission((Ordre_Mission)this._DataGridMain.SelectedItem);


                    //Association de l'élement dupliqué au datacontext de la fenêtre
                    ordreMissionWindow.DataContext = tmp;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = ordreMissionWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Ordre_Mission)ordreMissionWindow.DataContext;
                    }
                    else
                    {
                        try
                        {
                            //Detachement de tous les éléments liés à une Mission tiers
                            if (((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Tiers1 != null)
                            {
                                while (((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Tiers1.Mission_TiersQualification.Count() > 0)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Tiers1.Mission_TiersQualification.First());
                                    ((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Tiers1.Mission_TiersQualification.Remove(((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Tiers1.Mission_TiersQualification.First());
                                }
                                ((App)App.Current).mySitaffEntities.Detach(((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Tiers1);
                            }

                            //Detachement de tous les éléments liés à une Mission intérimaire
                            if (((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Interimaire1 != null)
                            {
                                while (((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Interimaire1.Mission_InterimaireQualification.Count() > 0)
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Interimaire1.Mission_InterimaireQualification.First());
                                    ((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Interimaire1.Mission_InterimaireQualification.Remove(((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Interimaire1.Mission_InterimaireQualification.First());
                                }
                                ((App)App.Current).mySitaffEntities.Detach(((Ordre_Mission)ordreMissionWindow.DataContext).Mission_Interimaire1);
                            }

                            ((App)App.Current).mySitaffEntities.Detach((Ordre_Mission)ordreMissionWindow.DataContext);
                        }
                        catch (Exception)
                        {

                        }
                        //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Duplication d'un ordre de mission annulé : " + this.OrdresMission.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul ordre de mission.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un ordre de mission.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        #endregion

        #region Filtrages

        #region Remise à zéro

        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            //Remise à zéro de tous les champs de filtrage
            //Text
            this._filterContainAffaire.Text = "";
            this._filterContainCommande.Text = "";
            this._filterContainContact.Text = "";
            this._filterContainContactMission_Client.Text = "";
            this._filterContainContactMission_Personnel.Text = "";
            this._filterContainDonneurOrdre.Text = "";
            this._filterContainEntreprise.Text = "";
            this._filterContainInterimaire.Text = "";
            this._filterContainLieuMission.Text = "";
            this._filterContainNumeroContrat.Text = "";

            //Dates
            _datePickerDateDebut.SelectedDate = null;
            _datePickerDateFin.SelectedDate = null;

            //ComboBox
            this._filterContainMotifMission.SelectedItem = null;
            this._filterContainEntrepriseMere.SelectedItem = null;
            this._filterContainInt_Equ.SelectedIndex = 0;

            //Rechargement des élements
            this.initialisationDataGridMain(null);
        }

        #endregion

        #region Button Filtrer

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

            ObservableCollection<Ordre_Mission> listToPut = new ObservableCollection<Ordre_Mission>(((App)App.Current).mySitaffEntities.Ordre_Mission.OrderBy(ord => ord.Date_Debut));
            if (this._filterContainAffaire.Text != "")
            {
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Affaire1 != null));
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Affaire1.Numero.Trim().ToLower().Contains(this._filterContainAffaire.Text.Trim().ToLower())));
            }
            if (this._filterContainContactMission_Personnel.Text != "")
            {
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Salarie != null));
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Salarie.Personne.fullname.Trim().ToLower().Contains(this._filterContainContactMission_Personnel.Text.Trim().ToLower())));
            }
            if (this._filterContainContactMission_Client.Text != "")
            {
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Contact1 != null));
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Contact1.Personne.fullname.Trim().ToLower().Contains(this._filterContainContactMission_Client.Text.Trim().ToLower())));
            }
            if (this._filterContainDonneurOrdre.Text != "")
            {
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Salarie1 != null));
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Salarie1.Personne.fullname.Trim().ToLower().Contains(this._filterContainDonneurOrdre.Text.Trim().ToLower())));
            }
            if (this._filterContainEntrepriseMere.SelectedItem != null)
            {
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Entreprise_Mere1 != null));
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Entreprise_Mere1 == this._filterContainEntrepriseMere.SelectedItem));
            }
            if (this._filterContainLieuMission.Text != "")
            {
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Entreprise1 != null));
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Entreprise1.Libelle.Trim().ToLower().Contains(this._filterContainLieuMission.Text.Trim().ToLower())));
            }
            if (this._filterContainMotifMission.SelectedItem != null)
            {
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Motif_Mission1 != null));
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Motif_Mission1 == this._filterContainMotifMission.SelectedItem));
            }
            if (this._filterContainNumeroContrat.Text != "")
            {
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Numero_Contrat != null));
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Numero_Contrat.Trim().ToLower().Contains(this._filterContainNumeroContrat.Text.Trim().ToLower())));
            }
            if (this._datePickerDateDebut.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Date_Debut >= this._datePickerDateDebut.SelectedDate).Where(ord => ord.Date_Fin >= this._datePickerDateDebut.SelectedDate));
            }
            if (this._datePickerDateFin.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Date_Debut <= this._datePickerDateFin.SelectedDate).Where(ord => ord.Date_Fin <= this._datePickerDateFin.SelectedDate));
            }
            switch (this._filterContainInt_Equ.SelectedIndex)
            {
                //Disposition du switch relatif à initialisationComboBoxInt_Equ()
                case 1: //Uniquement les tiers

                    listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Tiers1 != null));

                    if (this._filterContainEntreprise.Text != "")
                    {
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Tiers1.Entreprise1.Libelle.Trim().ToLower().Contains(this._filterContainEntreprise.Text.Trim().ToLower())));
                    }
                    if (this._filterContainContact.Text != "")
                    {
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Tiers1.Contact1.Personne.fullname.Trim().ToLower().Contains(this._filterContainContact.Text.Trim().ToLower())));
                    }
                    if (this._filterContainCommande.Text != "")
                    {
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Tiers1.Commande_Fournisseur1 != null));
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Tiers1.Commande_Fournisseur1.Numero.Trim().ToLower().Contains(this._filterContainCommande.Text.Trim().ToLower())));
                    }
                    break;
                case 2: //Uniquement les inérimaires

                    listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Interimaire1 != null));

                    if (this._filterContainEntreprise.Text != "")
                    {
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Interimaire1.Entreprise1.Libelle.Trim().ToLower().Contains(this._filterContainEntreprise.Text.Trim().ToLower())));
                    }
                    if (this._filterContainContact.Text != "")
                    {
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Interimaire1.Contact1.Personne.fullname.Trim().ToLower().Contains(this._filterContainContact.Text.Trim().ToLower())));
                    }
                    if (this._filterContainInterimaire.Text != "")
                    {
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Interimaire1.Salarie1 != null));
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Interimaire1.Salarie1.Personne.fullname.Trim().ToLower().Contains(this._filterContainInterimaire.Text.Trim().ToLower())));
                    }
                    break;
                default://Tous ( selectedindex == 0)
                    //Récupère tous les ordres de missions liés à des intérimaires

                    if (this._filterContainEntreprise.Text != "")
                    {
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Interimaire1 != null));
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Interimaire1.Entreprise1.Libelle.Trim().ToLower().Contains(this._filterContainEntreprise.Text.Trim().ToLower())));
                    }
                    if (this._filterContainContact.Text != "")
                    {
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Interimaire1 != null));
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Interimaire1.Contact1.Personne.fullname.Trim().ToLower().Contains(this._filterContainContact.Text.Trim().ToLower())));
                    }
                    if (this._filterContainInterimaire.Text != "")
                    {
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Interimaire1 != null));
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Interimaire1.Salarie1 != null));
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Interimaire1.Salarie1.Personne.fullname.Trim().ToLower().Contains(this._filterContainInterimaire.Text.Trim().ToLower())));
                    }

                    //Récupère tous les ordres de missions liés à des equipes
                    if (this._filterContainEntreprise.Text != "")
                    {
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Tiers1 != null));
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Tiers1.Entreprise1.Libelle.Trim().ToLower().Contains(this._filterContainEntreprise.Text.Trim().ToLower())));
                    }
                    if (this._filterContainContact.Text != "")
                    {
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Tiers1 != null));
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Tiers1.Contact1.Personne.fullname.Trim().ToLower().Contains(this._filterContainContact.Text.Trim().ToLower())));
                    }
                    if (this._filterContainCommande.Text != "")
                    {
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Tiers1 != null));
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Tiers1.Commande_Fournisseur1 != null));
                        listToPut = new ObservableCollection<Ordre_Mission>(listToPut.Where(ord => ord.Mission_Tiers1.Commande_Fournisseur1.Numero.Trim().ToLower().Contains(this._filterContainCommande.Text.Trim().ToLower())));
                    }


                    break;
            }
            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataGridMain(listToPut);

            //Si aucun résultat, j'affiche un message
            if (this.OrdresMission.Count() == 0)
            {
                MessageBox.Show("Aucun résultat ne correspont à votre recherche.", "Aucun résultat", MessageBoxButton.OK);
            }
        }

        #endregion

        #region bouton masquer / afficher

        private void _buttonMasqueFiltre_Click(object sender, RoutedEventArgs e)
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
                if (this._filterContainAffaire.Text != "" || this._filterContainCommande.Text != "" || this._filterContainContact.Text != "" ||
                    this._filterContainContactMission_Client.Text != "" || this._filterContainContactMission_Personnel.Text != "" || this._filterContainDonneurOrdre.Text != "" ||
                    this._filterContainEntreprise.Text != "" || this._filterContainEntrepriseMere.SelectedItem != null || this._filterContainInt_Equ.SelectedIndex != 0 ||
                    this._filterContainInterimaire.Text != "" || this._filterContainLieuMission.Text != "" || this._filterContainMotifMission.SelectedItem != null ||
                    this._filterContainNumeroContrat.Text != "" || this._datePickerDateDebut.SelectedDate != null || this._datePickerDateFin.SelectedDate != null ||
                    this.max != this.OrdresMission.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._buttonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainEntreprise.Focus();
            }
        }

        #endregion

        #region nullBox

        private void _buttonDateDebutNull_Click(object sender, RoutedEventArgs e)
        {
            this._datePickerDateDebut.SelectedDate = null;
        }

        private void _buttonDateFinNull_Click(object sender, RoutedEventArgs e)
        {
            this._datePickerDateFin.SelectedDate = null;
        }

        private void _buttonMotifMissionNull_Click(object sender, RoutedEventArgs e)
        {
            this._filterContainMotifMission.SelectedItem = null;
        }

        private void _buttonEntrepriseMereNull_Click(object sender, RoutedEventArgs e)
        {
            this._filterContainEntrepriseMere.SelectedItem = null;
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

        #region Fonctions

        /// <summary>
        /// Recalcul le nombre d'élements maximum
        /// </summary>
        private void recalculMax()
        {
            this.max = ((App)App.Current).mySitaffEntities.Ordre_Mission.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Ordre_Mission soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Ordre_Mission om)
        {
            //Je recalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Filtrage des ordres de mission terminé : " + this.OrdresMission.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute ordre de mission dans le linsting
                this.OrdresMission.Add(om);
                //Je recalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un ordre de mission effectué avec succès. Nombre d'élements : " + this.OrdresMission.Count() + " / " + this.max);
                try
                {
                    this._DataGridMain.SelectedItem = om;
                }
                catch (Exception) { }
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification de l'ordre de mission effectué avec succès. Nombre d'élements : " + this.OrdresMission.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.OrdresMission.Remove(om);
                //Je recalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression de l'ordre de mission effectué avec succès. Nombre d'élements : " + this.OrdresMission.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else if (typeEtat == "Duplicate")
            {
                //J'ajoute ordre de mission dans le linsting
                this.OrdresMission.Add(om);
                //Je recalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer un ordre de mission effectué avec succès. Nombre d'élements : " + this.OrdresMission.Count() + " / " + this.max);
            }
            else
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des ordres de missions terminé : " + this.OrdresMission.Count() + " / " + this.max);
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
            this.OrdresMission = new ObservableCollection<Ordre_Mission>(this.OrdresMission.OrderBy(ord => ord.Date_Debut));
        }

        /// <summary>
        /// Duplique l'ordre de mission passé en paramètre
        /// </summary>
        /// <param name="commande1">ordre de mission à dupliquer</param>
        private Ordre_Mission duplicateOrdreMission(Ordre_Mission itemToCopy)
        {
            Ordre_Mission tmp = new Ordre_Mission();

            //Partie générale
            tmp.Affaire1 = itemToCopy.Affaire1;
            tmp.Atelier = itemToCopy.Atelier;
            tmp.Autre = itemToCopy.Autre;
            tmp.Chantier = itemToCopy.Chantier;
            tmp.Client = itemToCopy.Client;
            tmp.Commentaire = itemToCopy.Commentaire;
            tmp.Contact1 = itemToCopy.Contact1;
            tmp.Date_Debut = itemToCopy.Date_Debut;
            tmp.Date_Fin = itemToCopy.Date_Fin;
            tmp.Date_RDV = itemToCopy.Date_RDV;
            tmp.Entreprise_Mere1 = itemToCopy.Entreprise_Mere1;
            tmp.Entreprise1 = itemToCopy.Entreprise1;
            tmp.Equipe_Tiers = itemToCopy.Equipe_Tiers;
            tmp.Heure_RDV = itemToCopy.Heure_RDV;
            tmp.Interimaire = itemToCopy.Interimaire;
            tmp.Libelle_Ordre_Mission = itemToCopy.Libelle_Ordre_Mission;
            tmp.Motif_Mission1 = itemToCopy.Motif_Mission1;
            tmp.Numero_Contrat = itemToCopy.Numero_Contrat;
            tmp.Personnel = itemToCopy.Personnel;
            tmp.Remplacement = itemToCopy.Remplacement;
            tmp.Salarie = itemToCopy.Salarie;
            tmp.Salarie1 = itemToCopy.Salarie1;
            tmp.Salarie2 = itemToCopy.Salarie2;

            //Partie intérimaire
            if (itemToCopy.Interimaire == true)
            {
                tmp.Mission_Interimaire1 = itemToCopy.Mission_Interimaire1;
            }

            //Partie tiers
            if (itemToCopy.Equipe_Tiers == true)
            {
                tmp.Mission_Tiers1 = itemToCopy.Mission_Tiers1;
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
