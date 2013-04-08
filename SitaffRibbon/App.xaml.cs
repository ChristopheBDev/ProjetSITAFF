using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using System.Data.Objects.DataClasses;
using System.Collections.ObjectModel;
using System.Globalization;
using SitaffRibbon.Classes;
using System.Windows.Media;
using System.Windows.Threading;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace SitaffRibbon
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region RoutedCommands

        #region CRUD

        public static RoutedCommand Look = new RoutedCommand("Look", typeof(App));

        public static RoutedCommand Add = new RoutedCommand("Add", typeof(App), new InputGestureCollection(new List<InputGesture>() { new KeyGesture(Key.N, ModifierKeys.Control, "CTRL+N") }));

        public static RoutedCommand Update = new RoutedCommand("Update", typeof(App), new InputGestureCollection(new List<InputGesture>() { new KeyGesture(Key.U, ModifierKeys.Control, "CTRL+U") }));

        public static RoutedCommand Delete = new RoutedCommand("Delete", typeof(App));

        #endregion

        #region Listing

        #region Main

        public static RoutedCommand AfficherFrais = new RoutedCommand("AfficherFrais", typeof(App));

        public static RoutedCommand AfficherResumeConge = new RoutedCommand("AfficherResumeConge", typeof(App));

        public static RoutedCommand AfficherEntreprises = new RoutedCommand("AfficherEntreprises", typeof(App));

        public static RoutedCommand AfficherReservationSalle = new RoutedCommand("AfficherReservationSalle", typeof(App));

        public static RoutedCommand AfficherFactures = new RoutedCommand("AfficherFactures", typeof(App));

        public static RoutedCommand AfficherFacturesProforma = new RoutedCommand("AfficherFacturesProforma", typeof(App));

        public static RoutedCommand AfficherFacturesProformaClient = new RoutedCommand("AfficherFacturesProformaClient", typeof(App));

        public static RoutedCommand AfficherAffaires = new RoutedCommand("AfficherAffaires", typeof(App));

        public static RoutedCommand AfficherAvances = new RoutedCommand("AfficherAvances", typeof(App));

        public static RoutedCommand AfficherContacts = new RoutedCommand("AfficherContacts", typeof(App));

        public static RoutedCommand AfficherSalaries = new RoutedCommand("AfficherSalaries", typeof(App));

        public static RoutedCommand AfficherFactureFournisseur = new RoutedCommand("AfficherFactureFournisseur", typeof(App));

        public static RoutedCommand AfficherAppelOffres = new RoutedCommand("AfficherAppelOffres", typeof(App));

        public static RoutedCommand AfficherParametres = new RoutedCommand("AfficherParametres", typeof(App));

        public static RoutedCommand AfficherDevis = new RoutedCommand("AfficherDevis", typeof(App));

        public static RoutedCommand AfficherDailly = new RoutedCommand("AfficherDailly", typeof(App));

        public static RoutedCommand AfficherDAO = new RoutedCommand("AfficherDAO", typeof(App));

        public static RoutedCommand AfficherHeuresAtelier = new RoutedCommand("AfficherHeuresAtelier", typeof(App));

        public static RoutedCommand AfficherHeuresForfait = new RoutedCommand("AfficherHeuresForfait", typeof(App));

        public static RoutedCommand AfficherCommande_Fournisseur = new RoutedCommand("AfficherCommande_Fournisseur", typeof(App));

        public static RoutedCommand AfficherConge = new RoutedCommand("AfficherConge", typeof(App));

        public static RoutedCommand AfficherOrdreMission = new RoutedCommand("AfficherOrdreMission", typeof(App));

        public static RoutedCommand AfficherBonLivraison = new RoutedCommand("AfficherBonLivraison", typeof(App));

        public static RoutedCommand AfficherSortieAtelier = new RoutedCommand("AfficherSortieAtelier", typeof(App));

        public static RoutedCommand AfficherRetourChantier = new RoutedCommand("AfficherRetourChantier", typeof(App));

        public static RoutedCommand AfficherResumeDevis = new RoutedCommand("AfficherResumeDevis", typeof(App));

        #endregion

        #region paramètres

        public static RoutedCommand AfficherParametreFrais = new RoutedCommand("AfficherFrais", typeof(App));

        public static RoutedCommand AfficherMoyenReglement = new RoutedCommand("AfficherMoyenReglement", typeof(App));

        public static RoutedCommand AfficherArticleFacture = new RoutedCommand("AfficherArticleFacture", typeof(App));

        public static RoutedCommand AfficherBesoinReservation = new RoutedCommand("AfficherBesoinReservation", typeof(App));

        public static RoutedCommand AfficherSalle = new RoutedCommand("AfficherSalle", typeof(App));

        public static RoutedCommand AfficherMotifDemandeConge = new RoutedCommand("AfficherMotifDemandeConge", typeof(App));

        public static RoutedCommand AfficherMotifRefusConge = new RoutedCommand("AfficherMotifRefusConge", typeof(App));

        public static RoutedCommand AfficherStatut = new RoutedCommand("AfficherStatut", typeof(App));

        public static RoutedCommand AfficherTypeRemboursement = new RoutedCommand("AfficherStatut", typeof(App));

        public static RoutedCommand AfficherEvenementRemboursement = new RoutedCommand("AfficherStatut", typeof(App));

        public static RoutedCommand AfficherJourFerie = new RoutedCommand("AfficherJourFerie", typeof(App));

        public static RoutedCommand AfficherTacheAtelier = new RoutedCommand("AfficherTacheAtelier", typeof(App));

        public static RoutedCommand AfficherPieceAdministrative = new RoutedCommand("AfficherPieceAdministrative", typeof(App));

        public static RoutedCommand AfficherMotifMission = new RoutedCommand("AfficherMotifMission", typeof(App));

        public static RoutedCommand AfficherPlanComptableTVA = new RoutedCommand("AfficherPlanComptableTVA", typeof(App));

        public static RoutedCommand AfficherPlanComptableImputation = new RoutedCommand("AfficherPlanComptableImputation", typeof(App));

        public static RoutedCommand AfficherTypeCommande = new RoutedCommand("AfficherTypeCommande", typeof(App));

        public static RoutedCommand AfficherOutillage = new RoutedCommand("AfficherOutillage", typeof(App));

        public static RoutedCommand AfficherExercice = new RoutedCommand("AfficherExercice", typeof(App));

        public static RoutedCommand AfficherReglement_Client = new RoutedCommand("AfficherReglement_Client", typeof(App));

        public static RoutedCommand AfficherType_Devis = new RoutedCommand("AfficherType_Devis", typeof(App));

        public static RoutedCommand AfficherEtat_Devis = new RoutedCommand("AfficherEtat_Devis", typeof(App));

        public static RoutedCommand AfficherBanques = new RoutedCommand("AfficherBanques", typeof(App));

        public static RoutedCommand AfficherPermis = new RoutedCommand("AfficherPermis", typeof(App));

        public static RoutedCommand AfficherPays = new RoutedCommand("AfficherPays", typeof(App));

        public static RoutedCommand AfficherHabilitation = new RoutedCommand("AfficherHabilitation", typeof(App));

        public static RoutedCommand AfficherCivilites = new RoutedCommand("AfficherCivilites", typeof(App));

        public static RoutedCommand AfficherQualification = new RoutedCommand("AfficherQualification", typeof(App));

        public static RoutedCommand AfficherVilles = new RoutedCommand("AfficherVilles", typeof(App));

        public static RoutedCommand AfficherDiplomes = new RoutedCommand("AfficherDiplomes", typeof(App));

        public static RoutedCommand AfficherFormation = new RoutedCommand("AfficherFormation", typeof(App));

        public static RoutedCommand AfficherGroupe = new RoutedCommand("AfficherGroupe", typeof(App));

        public static RoutedCommand AfficherModeFacturation = new RoutedCommand("AfficherModeFacturation", typeof(App));

        public static RoutedCommand AfficherDevise = new RoutedCommand("AfficherDevise", typeof(App));

        public static RoutedCommand AfficherLitige = new RoutedCommand("AfficherLitige", typeof(App));

        public static RoutedCommand AfficherTva = new RoutedCommand("AfficherTva", typeof(App));

        public static RoutedCommand AfficherDomaine = new RoutedCommand("AfficherDomaine", typeof(App));

        public static RoutedCommand AfficherActivite = new RoutedCommand("AfficherActivite", typeof(App));

        public static RoutedCommand AfficherTypeEntreprise = new RoutedCommand("AfficherTypeEntreprise", typeof(App));

        public static RoutedCommand AfficherContrat = new RoutedCommand("AfficherContrat", typeof(App));

        public static RoutedCommand AfficherDocTypeSalarie = new RoutedCommand("AfficherDocTypeSalarie", typeof(App));

        public static RoutedCommand AfficherContactService = new RoutedCommand("AfficherContactService", typeof(App));

        public static RoutedCommand AfficherRessources = new RoutedCommand("AfficherRessources", typeof(App));

        public static RoutedCommand AfficherTypeReception = new RoutedCommand("AfficherTypeReception", typeof(App));

        public static RoutedCommand AfficherContactFonction = new RoutedCommand("AfficherContactFonction", typeof(App));

        public static RoutedCommand AfficherDocAppelOffre = new RoutedCommand("AfficherDocAppelOffre", typeof(App));

        public static RoutedCommand AfficherChapitreClause = new RoutedCommand("AfficherChapitreClause", typeof(App));

        public static RoutedCommand AfficherDepartement = new RoutedCommand("AfficherDepartement", typeof(App));

        public static RoutedCommand AfficherConditionReglement = new RoutedCommand("AfficherConditionReglement", typeof(App));

        public static RoutedCommand AfficherRegion = new RoutedCommand("AfficherRegion", typeof(App));

        public static RoutedCommand AfficherRegie = new RoutedCommand("AfficherRegie", typeof(App));

        public static RoutedCommand AfficherNiveauSecurite = new RoutedCommand("AfficherNiveauSecurite", typeof(App));

        public static RoutedCommand AfficherUtilisateur = new RoutedCommand("AfficherUtilisateur", typeof(App));

        public static RoutedCommand AfficherEntrepriseMere = new RoutedCommand("AfficherEntrepriseMere", typeof(App));

        public static RoutedCommand AfficherTauxHoraire = new RoutedCommand("AfficherTauxHoraire", typeof(App));

        public static RoutedCommand AfficherVersionType = new RoutedCommand("AfficherVersionType", typeof(App));

        public static RoutedCommand AfficherDistanceVille = new RoutedCommand("AfficherDistanceVille", typeof(App));

        #endregion

        #endregion

        #region Dupliquer

        public static RoutedCommand DuplicateCommande = new RoutedCommand("DuplicateCommande", typeof(App));

        public static RoutedCommand DuplicateFacture = new RoutedCommand("DuplicateFacture", typeof(App));

        public static RoutedCommand DuplicateReleveHeureForfait = new RoutedCommand("DuplicateReleveHeureForfait", typeof(App));

        public static RoutedCommand DuplicateOrdreMission = new RoutedCommand("DuplicateOrdreMission", typeof(App));

        #endregion

        #region Rapports

        public static RoutedCommand RapportImprimerConge = new RoutedCommand("RapportImprimerConge", typeof(App));

        public static RoutedCommand RapportImprimerCommande = new RoutedCommand("RapportImprimerCommande", typeof(App));

        public static RoutedCommand RapportImprimerCommandeSansPrix = new RoutedCommand("RapportImprimerCommandeSansPrix", typeof(App));

        public static RoutedCommand RapportImprimerDossierAffaire = new RoutedCommand("RapportImprimerDossierAffaire", typeof(App));

        public static RoutedCommand RapportReleveFactureFournisseur = new RoutedCommand("RapportReleveFactureFournisseur", typeof(App));

        public static RoutedCommand RapportReleveActiviteParSalarie = new RoutedCommand("RapportReleveActiviteParSalarie", typeof(App));

        #endregion

        #region Aide

        public static RoutedCommand AfficherAide = new RoutedCommand("AfficherAide", typeof(App), new InputGestureCollection(new List<InputGesture>() { new KeyGesture(Key.F1) }));

        #endregion

        #region Autre

        public static RoutedCommand AddTime = new RoutedCommand("AddTime", typeof(App));

        public static RoutedCommand FusionnerAffaire = new RoutedCommand("FusionnerAffaire", typeof(App));

        public static RoutedCommand PasserAffaire = new RoutedCommand("PasserAffaire", typeof(App));

        public static RoutedCommand RepondreConge = new RoutedCommand("RepondreConge", typeof(App));

        public static RoutedCommand FaireDemandeConge = new RoutedCommand("FaireDemandeConge", typeof(App));

        public static RoutedCommand ModifierMotDePasse = new RoutedCommand("ModifierMotDePasse", typeof(App));

        public static RoutedCommand OpenShop = new RoutedCommand("OpenShop", typeof(App));

        public static RoutedCommand Customize = new RoutedCommand("Customize", typeof(App));

        public static RoutedCommand Plus = new RoutedCommand("Plus", typeof(App));

        public static RoutedCommand Moins = new RoutedCommand("Moins", typeof(App));

        public static RoutedCommand Filtrage = new RoutedCommand("Filtrage", typeof(App), new InputGestureCollection(new List<InputGesture>() { new KeyGesture(Key.F, ModifierKeys.Control, "CTRL+F") }));

        public static RoutedCommand Coller = new RoutedCommand("Coller", typeof(App), new InputGestureCollection(new List<InputGesture>() { new KeyGesture(Key.V, ModifierKeys.Control, "CTRL+V") }));

        public static RoutedCommand Quit = new RoutedCommand("Quit", typeof(App));

        #endregion

        #endregion

        #region Variables

        public Thread threadVerifConnexion;

        private sitaff2011Entities _mySitaffEntities;

        private Securite _securite;

        private Verifications _verifications;

        private Personnalisation _personnalisation;

        public MainWindow _theMainWindow = null;

        public Utilisateur _connectedUser;

        public Cursor _mainCursor;

        public Splash _splash;

        public Actions _actions;

        public AfficherMasquer _afficherMasquer;

        public MenuClicDroit _menuClicDroit;

        public DimensionnementFenetre _dimensionnementFenetre;

        #region backgroundMainWindow

        public Brush SaveFocusedBackground;
        public Brush SaveFocusedBorderBrush;

        #endregion

        #endregion

        #region Get/Set

        public sitaff2011Entities mySitaffEntities
        {
            get { return _mySitaffEntities; }
            set { _mySitaffEntities = value; }
        }

        public Securite securite
        {
            get { return _securite; }
            set { _securite = value; }
        }

        public Verifications verifications
        {
            get { return _verifications; }
            set { _verifications = value; }
        }

        public Personnalisation personnalisation
        {
            get { return _personnalisation; }
            set { _personnalisation = value; }
        }

        #endregion

        #region Constructeurs

        public App()
        {
            //_splash._TextBlockEnCours.Text = "Initialisation de la personnalisation utilisateur";
            this.personnalisation = new Personnalisation();

            _splash = new Splash();
            _splash.progressBarLoading.IsIndeterminate = true;
            _splash.Show();

            _splash._TextBlockEnCours.Text = "Connexion à la base de données...";
            this.mySitaffEntities = new sitaff2011Entities();
			if (!this.mySitaffEntities.DatabaseExists())
			{
				this.mySitaffEntities.CreateDatabase();
			}

            _splash._TextBlockEnCours.Text = "Initialisation du vérificateur de réseau...";
            this.startThreadVerifConnexion();

            _splash._TextBlockEnCours.Text = "Initialisation de la sécurité...";
            this._securite = new Securite();

            _splash._TextBlockEnCours.Text = "Initialisation des contraintes de vérifications...";
            this._verifications = new Verifications();

            _splash._TextBlockEnCours.Text = "Initialisation des fonctions d'afficher / masquer les colonnes...";
            this._afficherMasquer = new AfficherMasquer();

            _splash._TextBlockEnCours.Text = "Initialisation des menu clic droit...";
            this._menuClicDroit = new MenuClicDroit();

            _splash._TextBlockEnCours.Text = "Initialisation du dimensionnement automatique...";
            this._dimensionnementFenetre = new DimensionnementFenetre();
        }

        #endregion

        #region Fonctions

        #region Thread verification connexion

        private void startThreadVerifConnexion()
        {
            threadVerifConnexion = new Thread(new ThreadStart(VerifEDMX));
            threadVerifConnexion.Start();
        }

        public void VerifEDMX()
        {
            Mutex _mutex = new Mutex();
            bool test = true;

            while (true)
            {
                Thread.Sleep(200);
                _mutex.WaitOne();

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    //IAsyncResult result = socket.BeginConnect("192.168.0.4", 80, null, null);
                    //bool success = result.AsyncWaitHandle.WaitOne(100, true);
                    //if (success)
                    //{
                    Ping pingSender = new Ping();

                    // Create a buffer of 32 bytes of data to be transmitted.
                    int timeout = 100;
                    PingReply reply = pingSender.Send("192.168.0.4", timeout);
                    if (reply.Status == IPStatus.Success)
                    {
                        test = true;
                        this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => this.mettreConnectionActive()));
                    }
                    else
                    {
                        if (test == true)
                        {
                            //MessageBox.Show("Une coupure réseau vient de survenir. Veuillez vérifier votre câble réseau. Si aucun problème n'est apparent, ceci peut être dû à une microcoupure. Si vous étiez en cours d'ajout ou de modification, celle-ci risque de ne pas s'effectuer. Pardonnez-nous du dérangement.", "Coupure Réseau", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        test = false;
                        this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => this.mettreConnectionInactive()));
                    }
                }
                catch (Exception)
                {
                    if (test == true)
                    {
                        //MessageBox.Show("Une coupure réseau vient de survenir. Veuillez vérifier votre câble réseau. Si aucun problème n'est apparent, ceci peut être dû à une microcoupure. Si vous étiez en cours d'ajout ou de modification, celle-ci risque de ne pas s'effectuer. Pardonnez-nous du dérangement.", "Coupure Réseau", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    test = false;
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => this.mettreConnectionInactive()));
                }
                finally
                {
                    socket.Close();
                }

                _mutex.ReleaseMutex();
            }
        }

        public void mettreConnectionInactive()
        {
            if (this._theMainWindow != null)
            {
                this._theMainWindow.textBlockConnection.Text = "Etat de la connexion : Coupée";
                this._theMainWindow.textBlockConnection.Foreground = Brushes.Red;
            }
        }

        public void mettreConnectionActive()
        {
            if (this._theMainWindow != null)
            {
                this._theMainWindow.textBlockConnection.Text = "Etat de la connexion : Active";
                this._theMainWindow.textBlockConnection.Foreground = Brushes.Green;
            }
        }

        #endregion

        public void refreshEDMX()
        {
            this._theMainWindow.viderBorderContent();

            this.refreshEDMXSansVidage();
        }

        public void refreshEDMXSansVidage()
        {
            long identifiant = this._connectedUser.Identifiant;

            this.mySitaffEntities.Dispose();
            this.mySitaffEntities = new sitaff2011Entities();

            ObservableCollection<Utilisateur> _listOfUsers = new ObservableCollection<Utilisateur>(((App)App.Current).mySitaffEntities.Utilisateur.Where(us => us.Identifiant == identifiant));
            if (_listOfUsers.Count() == 1)
            {
                this._connectedUser = _listOfUsers.First();
            }
        }

        #endregion

    }
}