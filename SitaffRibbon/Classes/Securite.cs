using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SitaffRibbon.Classes
{
    public class Securite
    {

        #region Variables

        private ObservableCollection<String> _ControlsAddAuthorized = new ObservableCollection<string>();

        public ObservableCollection<String> ControlsAddAuthorized
        {
            get { return _ControlsAddAuthorized; }
        }

        private ObservableCollection<String> _ControlsUpdateAuthorized = new ObservableCollection<string>();

        public ObservableCollection<String> ControlsUpdateAuthorized
        {
            get { return _ControlsUpdateAuthorized; }
        }

        private ObservableCollection<String> _ControlsRemoveAuthorized = new ObservableCollection<string>();

        public ObservableCollection<String> ControlsRemoveAuthorized
        {
            get { return _ControlsRemoveAuthorized; }
        }

        private ObservableCollection<String> _ControlsLookAuthorized = new ObservableCollection<string>();

        public ObservableCollection<String> ControlsLookAuthorized
        {
            get { return _ControlsLookAuthorized; }
        }

        private ObservableCollection<String> _ControlsListAuthorized = new ObservableCollection<string>();

        public ObservableCollection<String> ControlsListAuthorized
        {
            get { return _ControlsListAuthorized; }
        }


        private ObservableCollection<String> _ControlsAddAuthorizedParameter = new ObservableCollection<string>();

        public ObservableCollection<String> ControlsAddAuthorizedParameter
        {
            get { return _ControlsAddAuthorizedParameter; }
        }

        private ObservableCollection<String> _ControlsUpdateAuthorizedParameter = new ObservableCollection<string>();

        public ObservableCollection<String> ControlsUpdateAuthorizedParameter
        {
            get { return _ControlsUpdateAuthorizedParameter; }
        }

        private ObservableCollection<String> _ControlsRemoveAuthorizedParameter = new ObservableCollection<string>();

        public ObservableCollection<String> ControlsRemoveAuthorizedParameter
        {
            get { return _ControlsRemoveAuthorizedParameter; }
        }

        private ObservableCollection<String> _ControlsLookAuthorizedParameter = new ObservableCollection<string>();

        public ObservableCollection<String> ControlsLookAuthorizedParameter
        {
            get { return _ControlsLookAuthorizedParameter; }
        }

        private ObservableCollection<String> _ControlsListAuthorizedParameter = new ObservableCollection<string>();

        public ObservableCollection<String> ControlsListAuthorizedParameter
        {
            get { return _ControlsListAuthorizedParameter; }
        }

        #endregion

        public Securite()
        {
            this.InitializeControlsAddAuthorized();
            this.InitializeControlsLookAuthorized();
            this.InitializeControlsRemoveAuthorized();
            this.InitializeControlsUpdateAuthorized();
            this.InitializeControlsListAuthorized();
            this.InitializeControlsAddAuthorizedParameter();
            this.InitializeControlsLookAuthorizedParameter();
            this.InitializeControlsRemoveAuthorizedParameter();
            this.InitializeControlsUpdateAuthorizedParameter();
            this.InitializeControlsListAuthorizedParameter();
        }

        #region Instanciation des variables des controls authorisés

        private void InitializeControlsAddAuthorized()
        {
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeReleveHeureForfaitControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeCongesControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeCommandeFournisseurControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeReveleHeureAtelierControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeDAOControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeRegieControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeAffaireControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeEntreprisesControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeContactsControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeSalarieControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeDevisControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeAppelOffresControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeFactureControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeBonLivraisonControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeFactureProformaControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeFactureFournisseurControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeAvanceControl");
			this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeReglementClientControl");
			this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeOrdreMissionControl");
			this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeSortieAtelierControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeReservationSalleControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeFraisControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeProformaClientControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeRetourChantierControl");
            this._ControlsAddAuthorized.Add("SitaffRibbon.UserControls.ListeDaillyControl"); 
        }

        private void InitializeControlsUpdateAuthorized()
        {
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeReleveHeureForfaitControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeCongesControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeCommandeFournisseurControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeReveleHeureAtelierControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeDAOControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeRegieControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeAffaireControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeEntreprisesControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeContactsControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeSalarieControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeDevisControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeAppelOffresControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeFactureControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeBonLivraisonControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeFactureProformaControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeFactureFournisseurControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeAvanceControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeReglementClientControl");
			this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeOrdreMissionControl");
			this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeSortieAtelierControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeReservationSalleControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeFraisControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeProformaClientControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeRetourChantierControl");
            this._ControlsUpdateAuthorized.Add("SitaffRibbon.UserControls.ListeDaillyControl"); 
        }

        private void InitializeControlsRemoveAuthorized()
        {
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeReleveHeureForfaitControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeCongesControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeCommandeFournisseurControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeReveleHeureAtelierControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeDAOControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeRegieControl");
            //this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeAffaireControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeEntreprisesControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeContactsControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeSalarieControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeDevisControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeAppelOffresControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeFactureControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeBonLivraisonControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeFactureProformaControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeFactureFournisseurControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeAvanceControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeReglementClientControl");
			this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeOrdreMissionControl");
			this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeSortieAtelierControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeReservationSalleControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeFraisControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeProformaClientControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeRetourChantierControl");
            this._ControlsRemoveAuthorized.Add("SitaffRibbon.UserControls.ListeDaillyControl"); 
        }

        private void InitializeControlsLookAuthorized()
        {
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeReleveHeureForfaitControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeCongesControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeCommandeFournisseurControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeReveleHeureAtelierControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeDAOControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeRegieControl");
            //this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeAffaireControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeEntreprisesControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeContactsControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeSalarieControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeDevisControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeAppelOffresControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeFactureControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeBonLivraisonControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeFactureProformaControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeFactureFournisseurControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeAvanceControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeReglementClientControl");
			this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeOrdreMissionControl");
			this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeSortieAtelierControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeReservationSalleControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeFraisControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeProformaClientControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeRetourChantierControl");
            this._ControlsLookAuthorized.Add("SitaffRibbon.UserControls.ListeDaillyControl"); 
        }

        private void InitializeControlsListAuthorized()
        {
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeReleveHeureForfaitControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeCongesControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeCommandeFournisseurControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeReveleHeureAtelierControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeDAOControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeRegieControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeAffaireControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeEntreprisesControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeContactsControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeSalarieControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeDevisControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeAppelOffresControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeFactureControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeBonLivraisonControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeFactureProformaControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeFactureFournisseurControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeAvanceControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeReglementClientControl");
			this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeOrdreMissionControl");
			this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeSortieAtelierControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeResumeDevisControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeReservationSalleControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeFraisControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeProformaClientControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeRetourChantierControl");
            this._ControlsListAuthorized.Add("SitaffRibbon.UserControls.ListeDaillyControl"); 
        }

        private void InitializeControlsAddAuthorizedParameter()
        {
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeReceptionControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePermisControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreNiveauSecuriteControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreUtilisateurControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTauxHoraireControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDepartementControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDeviseControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreFormationControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreRegionControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreConditionReglementControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreVersionTypeControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDocTypeSalarieControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContratControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeEntrepriseControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDomaineControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreLitigeControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreModeFacturationControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreVilleControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePaysControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreHabilitationControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreQualificationControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDiplomeControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreBanqueControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEtatDevisControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeDevisControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreExerciceControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreOutillageControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableImputationControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableTvaControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTvaControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTacheAtelierControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifDemandeCongeControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifRefusCongeControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreStatutControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreJourFerieControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePieceAdministrativeControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifMissionControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEvenementRemboursementControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreSalleControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreBesoinReservationSalleControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeFraisControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreArticleFactureControl");
            this._ControlsAddAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMoyenReglementControl");
        }

        private void InitializeControlsUpdateAuthorizedParameter()
        {
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeReceptionControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePermisControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreNiveauSecuriteControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreUtilisateurControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTauxHoraireControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDepartementControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDeviseControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreFormationControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreRegionControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreConditionReglementControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreVersionTypeControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDocTypeSalarieControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContratControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeEntrepriseControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDomaineControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreLitigeControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreModeFacturationControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreVilleControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePaysControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreHabilitationControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreQualificationControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDiplomeControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreBanqueControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEtatDevisControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeDevisControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreExerciceControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreOutillageControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableImputationControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableTvaControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTvaControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTacheAtelierControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifDemandeCongeControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifRefusCongeControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreStatutControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreJourFerieControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePieceAdministrativeControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifMissionControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEvenementRemboursementControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreSalleControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreBesoinReservationSalleControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeFraisControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreArticleFactureControl");
            this._ControlsUpdateAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMoyenReglementControl");
        }

        private void InitializeControlsRemoveAuthorizedParameter()
        {
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeReceptionControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePermisControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreNiveauSecuriteControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreUtilisateurControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTauxHoraireControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDepartementControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDeviseControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreFormationControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreRegionControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreConditionReglementControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreVersionTypeControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDocTypeSalarieControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContratControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeEntrepriseControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDomaineControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreLitigeControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreModeFacturationControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreVilleControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePaysControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreHabilitationControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreQualificationControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDiplomeControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreBanqueControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEtatDevisControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeDevisControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreExerciceControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreOutillageControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableImputationControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableTvaControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTvaControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTacheAtelierControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifDemandeCongeControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifRefusCongeControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreStatutControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreJourFerieControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePieceAdministrativeControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifMissionControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEvenementRemboursementControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreSalleControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreBesoinReservationSalleControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeFraisControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreArticleFactureControl");
            this._ControlsRemoveAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMoyenReglementControl");
        }

        private void InitializeControlsLookAuthorizedParameter()
        {
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeReceptionControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePermisControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreNiveauSecuriteControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreUtilisateurControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTauxHoraireControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDepartementControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDeviseControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreFormationControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreRegionControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreConditionReglementControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreVersionTypeControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDocTypeSalarieControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContratControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeEntrepriseControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDomaineControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreLitigeControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreModeFacturationControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreVilleControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePaysControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreHabilitationControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreQualificationControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDiplomeControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreBanqueControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEtatDevisControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeDevisControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreExerciceControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreOutillageControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableImputationControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableTvaControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTvaControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTacheAtelierControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifDemandeCongeControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifRefusCongeControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreStatutControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreJourFerieControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePieceAdministrativeControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifMissionControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEvenementRemboursementControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreSalleControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreBesoinReservationSalleControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeFraisControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreArticleFactureControl");
            this._ControlsLookAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMoyenReglementControl");
        }

        private void InitializeControlsListAuthorizedParameter()
        {
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeReceptionControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePermisControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreNiveauSecuriteControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreUtilisateurControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTauxHoraireControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDepartementControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDeviseControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreFormationControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreRegionControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreConditionReglementControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreVersionTypeControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDocTypeSalarieControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContratControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeEntrepriseControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDomaineControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreLitigeControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreModeFacturationControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreVilleControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePaysControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreHabilitationControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreQualificationControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDiplomeControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreBanqueControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEtatDevisControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeDevisControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreExerciceControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreOutillageControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableImputationControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableTvaControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTvaControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTacheAtelierControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifDemandeCongeControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifRefusCongeControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreStatutControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreJourFerieControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametrePieceAdministrativeControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifMissionControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreEvenementRemboursementControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreSalleControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreBesoinReservationSalleControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeFraisControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreArticleFactureControl");
            this._ControlsListAuthorizedParameter.Add("SitaffRibbon.Windows.ParametresUserControls.ParametreMoyenReglementControl");
        }

        #endregion


        /// <summary>
        /// Permet de savoir si l'utilisateur connecté a le droit d'executer la fonction correspondante
        /// </summary>
        /// <param name="userControlEnCours">Nom du userControl en cours</param>
        /// <param name="action">Action demandé ("Look", "Add", "Update", "Remove")</param>
        /// <returns>bool (vrai si droit)</returns>
        public bool VerificationDroitActionsCRUD(String userControlEnCours, String action)
        {
            bool droits = true;

            switch (userControlEnCours)
			{
                case "SitaffRibbon.UserControls.ListeDaillyControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DaillyLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DaillyAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DaillyUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DaillyRemove;
                            break;
                    }
                    break; 
                case "SitaffRibbon.UserControls.ListeRetourChantierControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.RetourChantierLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.RetourChantierAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.RetourChantierUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.RetourChantierRemove;
                            break;
                    }
                    break; 
                case "SitaffRibbon.UserControls.ListeProformaClientControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ProformaClientLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ProformaClientAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ProformaClientUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ProformaClientRemove;
                            break;
                    }
                    break; 
                case "SitaffRibbon.UserControls.ListeFraisControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FraisLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FraisAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FraisUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FraisRemove;
                            break;
                    }
                    break;      
                case "SitaffRibbon.UserControls.ListeReservationSalleControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReservationSalleLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReservationSalleAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReservationSalleUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReservationSalleRemove;
                            break;
                    }
                    break;                       
                case "SitaffRibbon.UserControls.ListeSortieAtelierControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.SortieAtelierLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.SortieAtelierAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.SortieAtelierUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.SortieAtelierRemove;
                            break;
                    }
                    break;   
                case "SitaffRibbon.UserControls.ListeOrdreMissionControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.OrdreMissionLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.OrdreMissionAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.OrdreMissionUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.OrdreMissionRemove;
                            break;
                    }
                    break;      
                case "SitaffRibbon.UserControls.ListeReglementClientControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReglementClientLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReglementClientAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReglementClientUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReglementClientRemove;
                            break;
                    }
                    break;                          
                case "SitaffRibbon.UserControls.ListeAvanceControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AvanceLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AvanceAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AvanceUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AvanceRemove;
                            break;
                    }
                    break;                    
                case "SitaffRibbon.UserControls.ListeFactureProformaControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureProformaLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureProformaAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureProformaUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureProformaRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeFactureControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeFactureFournisseurControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureFournisseurLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureFournisseurAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureFournisseurUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureFournisseurRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeBonLivraisonControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.BonLivraisonLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.BonLivraisonAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.BonLivraisonUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.BonLivraisonRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeReleveHeureForfaitControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReleveHeureForfaitLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReleveHeureForfaitAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReleveHeureForfaitUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReleveHeureForfaitRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeCongesControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.CongeLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.CongeAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.CongeUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.CongeRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeCommandeFournisseurControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.CommandeFournisseurLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.CommandeFournisseurAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.CommandeFournisseurUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.CommandeFournisseurRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeReveleHeureAtelierControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReleveHeureAtelierLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReleveHeureAtelierAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReleveHeureAtelierUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReleveHeureAtelierRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeDAOControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DAOLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DAOAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DAOUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DAORemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeRegieControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.RegieLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.RegieAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.RegieUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.RegieRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeAffaireControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AffaireLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AffaireAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AffaireUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AffaireRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeEntreprisesControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.EntrepriseLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.EntrepriseAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.EntrepriseUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.EntrepriseRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeContactsControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ContactLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ContactAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ContactUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ContactRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeSalarieControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.SalarieLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.SalarieAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.SalarieUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.SalarieRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeDevisControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DevisLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DevisAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DevisUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DevisRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.UserControls.ListeAppelOffresControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AppelOffreLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AppelOffreAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AppelOffreUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AppelOffreRemove;
                            break;
                    }
                    break;
            }

            return droits;
        }

        /// <summary>
        /// Permet de savoir si l'utilisateur connecté a le droit d'executer la fonction correspondante
        /// </summary>
        /// <param name="userControlAAfficher">Nom du userControl en cours</param>
        /// <returns>bool (vrai si droit)</returns>
        public bool VerificationDroitListing(String userControlAAfficher)
        {
            bool droits = true;

            switch (userControlAAfficher)
			{
                case "SitaffRibbon.UserControls.ListeDaillyControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DaillyList;
                    break;
                case "SitaffRibbon.UserControls.ListeRetourChantierControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.RetourChantierList;
                    break;
                case "SitaffRibbon.UserControls.ListeProformaClientControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ProformaClientList;
                    break;
                case "SitaffRibbon.UserControls.ListeFraisControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FraisList;
                    break;
                case "SitaffRibbon.UserControls.ListeReservationSalleControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReservationSalleList;
                    break;                    
                case "SitaffRibbon.UserControls.ListeResumeDevisControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ResumeDevisList;
                    break;
                case "SitaffRibbon.UserControls.ListeSortieAtelierControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.SortieAtelierList;
                    break;   
                case "SitaffRibbon.UserControls.ListeReglementClientControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReglementClientList;
                    break;                        
                case "SitaffRibbon.UserControls.ListeAvanceControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AvanceList;
                    break;
                case "SitaffRibbon.UserControls.ListeOrdreMissionControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.OrdreMissionList;
                    break;                        
                case "SitaffRibbon.UserControls.ListeFactureProformaControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureProformaList;
                    break;
                case "SitaffRibbon.UserControls.ListeReleveHeureForfaitControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReleveHeureForfaitList;
                    break;
                case "SitaffRibbon.UserControls.ListeCongesControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.CongeList;
                    break;
                case "SitaffRibbon.UserControls.ListeCommandeFournisseurControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.CommandeFournisseurList;
                    break;
                case "SitaffRibbon.UserControls.ListeReveleHeureAtelierControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ReleveHeureAtelierList;
                    break;
                case "SitaffRibbon.UserControls.ListeDAOControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DAOList;
                    break;
                case "SitaffRibbon.UserControls.ListeRegieControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.RegieList;
                    break;
                case "SitaffRibbon.UserControls.ListeAffaireControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AffaireList;
                    break;
                case "SitaffRibbon.UserControls.ListeEntreprisesControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.EntrepriseList;
                    break;
                case "SitaffRibbon.UserControls.ListeContactsControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ContactList;
                    break;
                case "SitaffRibbon.UserControls.ListeSalarieControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.SalarieList;
                    break;
                case "SitaffRibbon.UserControls.ListeDevisControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.DevisList;
                    break;
                case "SitaffRibbon.UserControls.ListeAppelOffresControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.AppelOffreList;
                    break;
                case "SitaffRibbon.UserControls.ListeFactureControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureList;
                    break;
                case "SitaffRibbon.UserControls.ListeFactureFournisseurControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.FactureFournisseurList;
                    break;
            }

            return droits;
        }

        /// <summary>
        /// Permet de savoir si l'utilisateur connecté a le droit d'executer la fonction correspondante
        /// </summary>
        /// <param name="userControlEnCours">Nom du userControl en cours</param>
        /// <param name="action">Action demandé ("Look", "Add", "Update", "Remove")</param>
        /// <returns>bool (vrai si droit)</returns>
        public bool VerificationDroitActionsCRUDParameters(String userControlEnCours, String action)
        {
            bool droits = true;

            switch (userControlEnCours)
            {
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeFraisControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeFraisLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeFraisAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeFraisUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeFraisRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreArticleFactureControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreArticleFactureLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreArticleFactureAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreArticleFactureUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreArticleFactureRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMoyenReglementControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMoyenReglementLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMoyenReglementAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMoyenReglementUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMoyenReglementRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreBesoinReservationSalleControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreBesoinReservationSalleLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreBesoinReservationSalleAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreBesoinReservationSalleUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreBesoinReservationSalleRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreSalleControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreSalleLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreSalleAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreSalleUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreSalleRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDistanceVilleLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDistanceVilleAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDistanceVilleUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDistanceVilleRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEvenementRemboursementControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEvenementRemboursementLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEvenementRemboursementAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEvenementRemboursementUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEvenementRemboursementRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeRemboursementLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeRemboursementAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeRemboursementUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeRemboursementRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreCiviliteLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreCiviliteAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreCiviliteUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreCiviliteRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeReceptionControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDeReceptionLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDeReceptionAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDeReceptionUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDeReceptionRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePermisControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePermisLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePermisAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePermisUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePermisRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreNiveauSecuriteControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreNiveauDeSecuriteLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreNiveauDeSecuriteAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreNiveauDeSecuriteUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreNiveauDeSecuriteRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreUtilisateurControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreUtilisateurLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreUtilisateurAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreUtilisateurUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreUtilisateurRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTauxHoraireControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTauxHoraireLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTauxHoraireAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTauxHoraireUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTauxHoraireRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDepartementControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDepartementLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDepartementAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDepartementUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDepartementRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDeviseControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDeviseLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDeviseAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDeviseUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDeviseRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreFormationControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreFormationLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreFormationAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreFormationUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreFormationRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreRegionControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreRegionLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreRegionAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreRegionUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreRegionRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreConditionReglementControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreConditionDeReglementLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreConditionDeReglementAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreConditionDeReglementUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreConditionDeReglementRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEntrepriseMereLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEntrepriseMereAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEntrepriseMereUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEntrepriseMereRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreVersionTypeControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreVersionTypeLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreVersionTypeAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreVersionTypeUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreVersionTypeRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDocTypeSalarieControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDocumentTypeSalarieLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDocumentTypeSalarieAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDocumentTypeSalarieUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDocumentTypeSalarieRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContratControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreContratLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreContratAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreContratUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreContratRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeEntrepriseControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeEntrepriseLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeEntrepriseAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeEntrepriseUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeEntrepriseRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDomaineControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDomaineLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDomaineAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDomaineUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDomaineRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreLitigeControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreLitigeLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreLitigeAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreLitigeUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreLitigeRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreModeFacturationControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreModeDeFacturationLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreModeDeFacturationAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreModeDeFacturationUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreModeDeFacturationRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreGroupeLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreGroupeAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreGroupeUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreGroupeRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreVilleControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreVilleLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreVilleAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreVilleUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreVilleRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreActiviteLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreActiviteAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreActiviteUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreActiviteRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePaysControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePaysLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePaysAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePaysUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePaysRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreHabilitationControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreHabilitationLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreHabilitationAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreHabilitationUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreHabilitationRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreQualificationControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreQualificationLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreQualificationAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreQualificationUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreQualificationRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreFonctionLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreFonctionAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreFonctionUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreFonctionRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreServiceLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreServiceAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreServiceUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreServiceRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDiplomeControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDiplomeLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDiplomeAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDiplomeUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDiplomeRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreBanqueControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreBanqueLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreBanqueAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreBanqueUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreBanqueRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEtatDevisControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEtatDevisLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEtatDevisAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEtatDevisUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEtatDevisRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeDevisControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDevisLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDevisAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDevisUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDevisRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreExerciceControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreExerciceLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreExerciceAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreExerciceUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreExerciceRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreOutillageControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreOutillageLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreOutillageAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreOutillageUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreOutillageRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDeCommandeLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDeCommandeAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDeCommandeUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDeCommandeRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableImputationControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePlanComptableImputationLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePlanComptableImputationAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePlanComptableImputationUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePlanComptableImputationRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableTvaControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePlanComptableTvaLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePlanComptableTvaAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePlanComptableTvaUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePlanComptableTvaRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTvaControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTvaLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTvaAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTvaUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTvaRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTacheAtelierControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTacheAtelierLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTacheAtelierAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTacheAtelierUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTacheAtelierRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifDemandeCongeControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifDemandeCongeLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifDemandeCongeAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifDemandeCongeUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifDemandeCongeRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifRefusCongeControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifRefusCongeLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifRefusCongeAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifRefusCongeUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifRefusCongeRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreStatutControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreStatutLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreStatutAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreStatutUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreStatutRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreJourFerieControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreJourFerieLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreJourFerieAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreJourFerieUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreJourFerieRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePieceAdministrativeControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePieceAdministrativeLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePieceAdministrativeAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePieceAdministrativeUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePieceAdministrativeRemove;
                            break;
                    }
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifMissionControl":
                    switch (action)
                    {
                        case "Look":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifMissionLook;
                            break;
                        case "Add":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifMissionAdd;
                            break;
                        case "Update":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifMissionUpdate;
                            break;
                        case "Remove":
                            droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifMissionRemove;
                            break;
                    }
                    break;
            }

            return droits;
        }
        
        /// <summary>
        /// Permet de savoir si l'utilisateur connecté a le droit d'executer la fonction correspondante
        /// </summary>
        /// <param name="userControlAAfficher">Nom du userControl en cours</param>
        /// <returns>bool (vrai si droit)</returns>
        public bool VerificationDroitListingParameters(String userControlAAfficher)
        {
            bool droits = true;

            switch (userControlAAfficher)
            {
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeFraisControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeFraisList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreArticleFactureControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreArticleFactureList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMoyenReglementControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMoyenReglementList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreSalleControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreSalleList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreBesoinReservationSalleControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreBesoinReservationSalleList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDistanceVilleList;
                    break;                    
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEvenementRemboursementControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEvenementRemboursementList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeRemboursementList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreCiviliteList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeReceptionControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDeReceptionList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePermisControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePermisList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreNiveauSecuriteControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreNiveauDeSecuriteList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreUtilisateurControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreUtilisateurList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTauxHoraireControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTauxHoraireList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDepartementControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDepartementList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDeviseControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDeviseList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreFormationControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreFormationList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreRegionControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreRegionList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreConditionReglementControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreConditionDeReglementList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEntrepriseMereList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreVersionTypeControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreVersionTypeList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDocTypeSalarieControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDocumentTypeSalarieList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContratControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreContratList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeEntrepriseControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeEntrepriseList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDomaineControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDomaineList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreLitigeControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreLitigeList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreModeFacturationControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreModeDeFacturationList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreGroupeList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreVilleControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreVilleList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreActiviteList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePaysControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePaysList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreHabilitationControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreHabilitationList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreQualificationControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreQualificationList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreFonctionList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreServiceList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDiplomeControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreDiplomeList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreBanqueControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreBanqueList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEtatDevisControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreEtatDevisList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeDevisControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDevisList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreExerciceControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreExerciceList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreOutillageControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreOutillageList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTypeDeCommandeList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableImputationControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePlanComptableImputationList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableTvaControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePlanComptableTvaList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTvaControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTvaList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTacheAtelierControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreTacheAtelierList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifDemandeCongeControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifDemandeCongeList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifRefusCongeControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifRefusCongeList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreStatutControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreStatutList;
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreJourFerieControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreJourFerieList;
                    break;   
                 case "SitaffRibbon.Windows.ParametresUserControls.ParametrePieceAdministrativeControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametrePieceAdministrativeList;
                    break;       
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifMissionControl":
                    droits = ((App)App.Current)._connectedUser.Niveau_Securite1.ParametreMotifMissionList;
                    break;       
            }

            return droits;
        }

        public bool VerificationDroitOuvrirParametres()
        {
            return ((App)App.Current)._connectedUser.Niveau_Securite1.OpenParametres;
        }

        public bool VerificationDroitOuvrirShop()
        {
            return ((App)App.Current)._connectedUser.Niveau_Securite1.OpenShop;
        }

    }
}
