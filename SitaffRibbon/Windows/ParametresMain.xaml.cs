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
using System.Windows.Shapes;
using Microsoft.Windows.Controls.Ribbon;
using SitaffRibbon.Windows.ParametresUserControls;
using SitaffRibbon.Windows.ParametresWindows;
/* 
 * Using pour utilisation des IObservableCollection (afin d'éviter de mettre
 * System.Collections.ObjectModel.IObservableCollection en entier)
 */
using System.Collections.ObjectModel;
using System.Threading;
using SitaffRibbon.Classes;
using SitaffRibbon.UserControls;

namespace SitaffRibbon.Windows
{
    public partial class ParametresMain : RibbonWindow
    {

        #region Attributs

        MainWindow laMainWindow;
        public Mutex _mutex = new Mutex();
        Securite securite = new Securite();

        #endregion

        #region Constructeur

        public ParametresMain(MainWindow mainwindow)
        {
            InitializeComponent();
            this.laMainWindow = mainwindow;
        }

        #endregion

        #region Commandes

        #region filter

        private void _CommandFiltrage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            UIElement uIElement = this._BorderContent.Child;

            switch (uIElement.ToString())
            {
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeFraisControl":
                    this.FiltreFraisControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMoyenReglementControl":
                    this.FiltreMoyenReglementControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreArticleFactureControl":
                    this.FiltreArticleFactureControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl":
                    this.FiltreDistanceVilleControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl":
                    this.FiltreTypeRemboursementControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEvenementRemboursementControl":
                    this.FiltreEvenementRemboursementControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableImputationControl":
                    this.FiltrePlanComptableImputationControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableTvaControl":
                    this.FiltrePlanComptableTvaControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl":
                    this.FiltreCiviliteControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl":
                    this.FiltreTypeCommandeControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeReceptionControl":
                    this.FiltreTypeReceptionControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePermisControl":
                    this.FiltrePermisControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreNiveauSecuriteControl":
                    this.FiltreNiveauSecuriteControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreUtilisateurControl":
                    this.FiltreUtilisateurControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTauxHoraireControl":
                    this.FiltreTauxHoraireControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDepartementControl":
                    this.FiltreDepartementControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDeviseControl":
                    this.FiltreDeviseControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreFormationControl":
                    this.FiltreFormationControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreRegionControl":
                    this.FiltreRegionControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreConditionReglementControl":
                    this.FiltreConditionReglementControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl":
                    this.FiltreEntrepriseMereControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreVersionTypeControl":
                    this.FiltreVersionTypeControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDocTypeSalarieControl":
                    this.FiltreDocTypeSalarieControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContratControl":
                    this.FiltreContratControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeEntrepriseControl":
                    this.FiltreTypeEntrepriseControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDomaineControl":
                    this.FiltreDomaineControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreLitigeControl":
                    this.FiltreLitigeControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreModeFacturationControl":
                    this.FiltreModeFacturationControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl":
                    this.FiltreGroupeControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreVilleControl":
                    this.FiltreVilleControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl":
                    this.FiltreActiviteControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePaysControl":
                    this.FiltrePaysControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreHabilitationControl":
                    this.FiltreHabilitationControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreQualificationControl":
                    this.FiltreQualificationControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl":
                    this.FiltreContactFonctionControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl":
                    this.FiltreContactServiceControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDiplomeControl":
                    this.FiltreDiplomeControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreBanqueControl":
                    this.FiltreBanqueControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEtatDevisControl":
                    this.FiltreEtatDevisControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeDevisControl":
                    this.FiltreTypeDevisControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreExerciceControl":
                    this.FiltreExerciceControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreOutillageControl":
                    this.FiltreOutillageControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTvaControl":
                    this.FiltreTvaControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTacheAtelierControl":
                    this.FiltreTacheAtelierControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifDemandeCongeControl":
                    this.FiltreMotifDemandeCongeControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifRefusCongeControl":
                    this.FiltreMotifRefusCongeControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreJourFerieControl":
                    this.FiltreJourFerieControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePieceAdministrativeControl":
                    this.FiltrePieceAdministrativeControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifMissionControl":
                    this.FiltreMotifMissionControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreSalleControl":
                    this.FiltreSalleControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreBesoinReservationSalleControl":
                    this.FiltreBesoinReservationSalleControl(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreStatutControl":
                    this.FiltreStatutControl(uIElement);
                    break;
                default:
                    break;
            }
        }

        #region Fonctions des filtres

        public void FiltreFraisControl(UIElement uIElement)
        {
            try
            {
                ((ParametreTypeFraisControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreMoyenReglementControl(UIElement uIElement)
        {
            try
            {
                ((ParametreMoyenReglementControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreArticleFactureControl(UIElement uIElement)
        {
            try
            {
                ((ParametreArticleFactureControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreBesoinReservationSalleControl(UIElement uIElement)
        {
            try
            {
                ((ParametreBesoinReservationSalleControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreSalleControl(UIElement uIElement)
        {
            try
            {
                ((ParametreSalleControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreStatutControl(UIElement uIElement)
        {
            try
            {
                ((ParametreStatutControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreDistanceVilleControl(UIElement uIElement)
        {
            try
            {
                ((ParametreDistanceVilleControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreEvenementRemboursementControl(UIElement uIElement)
        {
            try
            {
                ((ParametreEvenementRemboursementControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreTypeRemboursementControl(UIElement uIElement)
        {
            try
            {
                ((ParametreTypeRemboursementControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltrePieceAdministrativeControl(UIElement uIElement)
        {
            try
            {
                ((ParametrePieceAdministrativeControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreMotifMissionControl(UIElement uIElement)
        {
            try
            {
                ((ParametreMotifMissionControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltrePlanComptableImputationControl(UIElement uIElement)
        {
            try
            {
                ((ParametrePlanComptableImputationControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltrePlanComptableTvaControl(UIElement uIElement)
        {
            try
            {
                ((ParametrePlanComptableTvaControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreCiviliteControl(UIElement uIElement)
        {
            try
            {
                ((ParametreCiviliteControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreTypeCommandeControl(UIElement uIElement)
        {
            try
            {
                ((ParametreTypeCommandeControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreTypeReceptionControl(UIElement uIElement)
        {
            try
            {
                ((ParametreTypeReceptionControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltrePermisControl(UIElement uIElement)
        {
            try
            {
                ((ParametrePermisControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreNiveauSecuriteControl(UIElement uIElement)
        {
            try
            {
                ((ParametreNiveauSecuriteControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreUtilisateurControl(UIElement uIElement)
        {
            try
            {
                ((ParametreUtilisateurControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreTauxHoraireControl(UIElement uIElement)
        {
            try
            {
                ((ParametreTauxHoraireControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreDepartementControl(UIElement uIElement)
        {
            try
            {
                ((ParametreDepartementControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreDeviseControl(UIElement uIElement)
        {
            try
            {
                ((ParametreDeviseControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreFormationControl(UIElement uIElement)
        {
            try
            {
                ((ParametreFormationControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreRegionControl(UIElement uIElement)
        {
            try
            {
                ((ParametreRegionControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreConditionReglementControl(UIElement uIElement)
        {
            try
            {
                ((ParametreConditionReglementControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreEntrepriseMereControl(UIElement uIElement)
        {
            try
            {
                ((ParametreEntrepriseMereControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreVersionTypeControl(UIElement uIElement)
        {
            try
            {
                ((ParametreVersionTypeControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreDocTypeSalarieControl(UIElement uIElement)
        {
            try
            {
                ((ParametreDocTypeSalarieControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreContratControl(UIElement uIElement)
        {
            try
            {
                ((ParametreContratControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreTypeEntrepriseControl(UIElement uIElement)
        {
            try
            {
                ((ParametreTypeEntrepriseControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreDomaineControl(UIElement uIElement)
        {
            try
            {
                ((ParametreDomaineControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreLitigeControl(UIElement uIElement)
        {
            try
            {
                ((ParametreLitigeControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreModeFacturationControl(UIElement uIElement)
        {
            try
            {
                ((ParametreModeFacturationControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreGroupeControl(UIElement uIElement)
        {
            try
            {
                ((ParametreGroupeControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreVilleControl(UIElement uIElement)
        {
            try
            {
                ((ParametreVilleControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreActiviteControl(UIElement uIElement)
        {
            try
            {
                ((ParametreActiviteControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltrePaysControl(UIElement uIElement)
        {
            try
            {
                ((ParametrePaysControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreHabilitationControl(UIElement uIElement)
        {
            try
            {
                ((ParametreHabilitationControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreQualificationControl(UIElement uIElement)
        {
            try
            {
                ((ParametreQualificationControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreContactFonctionControl(UIElement uIElement)
        {
            try
            {
                ((ParametreContactFonctionControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreContactServiceControl(UIElement uIElement)
        {
            try
            {
                ((ParametreContactServiceControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreDiplomeControl(UIElement uIElement)
        {
            try
            {
                ((ParametreDiplomeControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreBanqueControl(UIElement uIElement)
        {
            try
            {
                ((ParametreBanqueControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreEtatDevisControl(UIElement uIElement)
        {
            try
            {
                ((ParametreEtatDevisControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreTypeDevisControl(UIElement uIElement)
        {
            try
            {
                ((ParametreTypeDevisControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreExerciceControl(UIElement uIElement)
        {
            try
            {
                ((ParametreExerciceControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreOutillageControl(UIElement uIElement)
        {
            try
            {
                ((ParametreOutillageControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreTvaControl(UIElement uIElement)
        {
            try
            {
                ((ParametreTvaControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreTacheAtelierControl(UIElement uIElement)
        {
            try
            {
                ((ParametreTacheAtelierControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreMotifDemandeCongeControl(UIElement uIElement)
        {
            try
            {
                ((ParametreMotifDemandeCongeControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreMotifRefusCongeControl(UIElement uIElement)
        {
            try
            {
                ((ParametreMotifRefusCongeControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreJourFerieControl(UIElement uIElement)
        {
            try
            {
                ((ParametreJourFerieControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        #endregion

        private void _CommandFiltrage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region delete

        private void _CommandDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            UIElement uIElement = this._BorderContent.Child;

            switch (uIElement.ToString())
            {
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeFraisControl":
                    this.DeleteFrais(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreArticleFactureControl":
                    this.DeleteArticleFacture(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMoyenReglementControl":
                    this.DeleteMoyenReglement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl":
                    this.DeleteDistanceVille(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreBesoinReservationSalleControl":
                    this.DeleteBesoinReservationSalle(uIElement);
                    break;  
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl":
                    this.DeleteTypeRemboursement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEvenementRemboursementControl":
                    this.DeleteEvenementRemboursement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableImputationControl":
                    this.DeletePlanComptableImputation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableTvaControl":
                    this.DeletePlanComptableTva(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl":
                    this.DeleteCivilite(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeReceptionControl":
                    this.DeleteTypeReception(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePermisControl":
                    this.DeletePermis(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreNiveauSecuriteControl":
                    this.DeleteNiveauSecurite(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreUtilisateurControl":
                    this.DeleteUtilisateur(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTauxHoraireControl":
                    this.DeleteTauxHoraire(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDepartementControl":
                    this.DeleteDepartement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDeviseControl":
                    this.DeleteDevise(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreFormationControl":
                    this.DeleteFormation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreRegionControl":
                    this.DeleteRegion(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreConditionReglementControl":
                    this.DeleteConditionReglement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl":
                    this.DeleteEntrepriseMere(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreVersionTypeControl":
                    this.DeleteVersionType(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDocTypeSalarieControl":
                    this.DeleteDocTypeSalarie(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContratControl":
                    this.DeleteContrat(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeEntrepriseControl":
                    this.DeleteTypeEntreprise(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDomaineControl":
                    this.DeleteDomaine(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreLitigeControl":
                    this.DeleteLitige(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreModeFacturationControl":
                    this.DeleteModeFacturation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl":
                    this.DeleteGroupe(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreVilleControl":
                    this.DeleteVille(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl":
                    this.DeleteActivite(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePaysControl":
                    this.DeletePays(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreHabilitationControl":
                    this.DeleteHabilitation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreQualificationControl":
                    this.DeleteQualification(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl":
                    this.DeleteContactFonction(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl":
                    this.DeleteContactService(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDiplomeControl":
                    this.DeleteDiplome(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreBanqueControl":
                    this.DeleteBanque(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEtatDevisControl":
                    this.DeleteEtatDevis(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeDevisControl":
                    this.DeleteTypeDevis(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreExerciceControl":
                    this.DeleteExercice(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreOutillageControl":
                    this.DeleteOutillage(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl":
                    this.DeleteTypeCommande(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTvaControl":
                    this.DeleteTva(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTacheAtelierControl":
                    this.DeleteTacheAtelier(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifDemandeCongeControl":
                    this.DeleteMotifDemandeConge(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifRefusCongeControl":
                    this.DeleteMotifRefusConge(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreJourFerieControl":
                    this.DeleteJourFerie(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePieceAdministrativeControl":
                    this.DeletePieceAdministrative(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifMissionControl":
                    this.DeleteMotifMission(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreSalleControl":
                    this.DeleteSalle(uIElement);
                    break;
                default:
                    MessageBox.Show("Vous n'avez normalement pas à cliquer sur ce bouton, il devrait être grisé. Si vous recevez cette erreur, merci d'envoyer un mail à l'administrateur avec une image de l'erreur, le nom du bouton ainsi que le nom de votre utilisateur.");
                    break;
            }

            this.Cursor = ((App)App.Current)._mainCursor;
        }

        #region Fonctions des delete

        private void DeleteFrais(UIElement uIElement)
        {
            Type_Frais frais = ((ParametreTypeFraisControl)uIElement).Remove();

            if (frais != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Type_Frais.DeleteObject(frais);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeFraisControl)uIElement).MiseAJourEtat("Suppression", frais);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un frais");
                }
            }
        }

        private void DeleteArticleFacture(UIElement uIElement)
        {
            Article_Facture articlefacture = ((ParametreArticleFactureControl)uIElement).Remove();

            if (articlefacture != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Article_Facture.DeleteObject(articlefacture);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreArticleFactureControl)uIElement).MiseAJourEtat("Suppression", articlefacture);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un article");
                }
            }
        }

        private void DeleteMoyenReglement(UIElement uIElement)
        {
            Moyen_Reglement moyenreglement = ((ParametreMoyenReglementControl)uIElement).Remove();

            if (moyenreglement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Moyen_Reglement.DeleteObject(moyenreglement);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreMoyenReglementControl)uIElement).MiseAJourEtat("Suppression", moyenreglement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un moyen de règlement");
                }
            }
        }

        private void DeleteBesoinReservationSalle(UIElement uIElement)
        {
            Besoin_Reservation_Salle besoinreservationsalle = ((ParametreBesoinReservationSalleControl)uIElement).Remove();

            if (besoinreservationsalle != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Besoin_Reservation_Salle.DeleteObject(besoinreservationsalle);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreBesoinReservationSalleControl)uIElement).MiseAJourEtat("Suppression", besoinreservationsalle);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un besoin de réservation");
                }
            }
        }

        private void DeleteSalle(UIElement uIElement)
        {
            Salle salle = ((ParametreSalleControl)uIElement).Remove();

            if (salle != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Salle.DeleteObject(salle);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreSalleControl)uIElement).MiseAJourEtat("Suppression", salle);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une salle");
                }
            }
        }

        private void DeleteDistanceVille(UIElement uIElement)
        {
            Distance_Ville distance_ville = ((ParametreDistanceVilleControl)uIElement).Remove();

            if (distance_ville != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Distance_Ville.DeleteObject(distance_ville);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDistanceVilleControl)uIElement).MiseAJourEtat("Suppression", distance_ville);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une distance ville");
                }
            }
        }

        private void DeleteEvenementRemboursement(UIElement uIElement)
        {
            Evenement_Remboursement evenement_remboursement = ((ParametreEvenementRemboursementControl)uIElement).Remove();

            if (evenement_remboursement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Evenement_Remboursement.DeleteObject(evenement_remboursement);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreEvenementRemboursementControl)uIElement).MiseAJourEtat("Suppression", evenement_remboursement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un evenement remboursement");
                }
            }
        }

        private void DeleteTypeRemboursement(UIElement uIElement)
        {
            Type_Remboursement type_remboursement = ((ParametreTypeRemboursementControl)uIElement).Remove();

            if (type_remboursement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Type_Remboursement.DeleteObject(type_remboursement);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeRemboursementControl)uIElement).MiseAJourEtat("Suppression", type_remboursement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un type remboursement");
                }
            }
        }

        private void DeletePieceAdministrative(UIElement uIElement)
        {
            Piece_Administrative pie = ((ParametrePieceAdministrativeControl)uIElement).Remove();

            if (pie != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Piece_Administrative.DeleteObject(pie);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePieceAdministrativeControl)uIElement).MiseAJourEtat("Suppression", pie);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une pièce administrative");
                }
            }
        }

        private void DeleteMotifMission(UIElement uIElement)
        {
            Motif_Mission mot = ((ParametreMotifMissionControl)uIElement).Remove();

            if (mot != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Motif_Mission.DeleteObject(mot);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreMotifMissionControl)uIElement).MiseAJourEtat("Suppression", mot);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un motif mission");
                }
            }
        }

        private void DeleteJourFerie(UIElement uIElement)
        {
            JourFerie jou = ((ParametreJourFerieControl)uIElement).Remove();

            if (jou != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.JourFerie.DeleteObject(jou);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreJourFerieControl)uIElement).MiseAJourEtat("Suppression", jou);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un jour ferié");
                }
            }
        }

        private void DeleteMotifRefusConge(UIElement uIElement)
        {
            Motif_Refus mot = ((ParametreMotifRefusCongeControl)uIElement).Remove();

            if (mot != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Motif_Refus.DeleteObject(mot);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreMotifRefusCongeControl)uIElement).MiseAJourEtat("Suppression", mot);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un motif de refus");
                }
            }
        }

        private void DeleteMotifDemandeConge(UIElement uIElement)
        {
            Motif_Demande mot = ((ParametreMotifDemandeCongeControl)uIElement).Remove();

            if (mot != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Motif_Demande.DeleteObject(mot);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreMotifDemandeCongeControl)uIElement).MiseAJourEtat("Suppression", mot);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un motif de demande");
                }
            }
        }

        private void DeleteTacheAtelier(UIElement uIElement)
        {
            Tache_Atelier tac = ((ParametreTacheAtelierControl)uIElement).Remove();

            if (tac != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Tache_Atelier.DeleteObject(tac);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTacheAtelierControl)uIElement).MiseAJourEtat("Suppression", tac);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une tache");
                }
            }
        }

        private void DeleteTva(UIElement uIElement)
        {
            Tva tva = ((ParametreTvaControl)uIElement).Remove();

            if (tva != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Tva.DeleteObject(tva);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTvaControl)uIElement).MiseAJourEtat("Suppression", tva);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une Tva");
                }
            }
        }

        private void DeletePlanComptableImputation(UIElement uIElement)
        {
            Plan_Comptable_Imputation plancomptableimputation = ((ParametrePlanComptableImputationControl)uIElement).Remove();

            if (plancomptableimputation != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Plan_Comptable_Imputation.DeleteObject(plancomptableimputation);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePlanComptableImputationControl)uIElement).MiseAJourEtat("Suppression", plancomptableimputation);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un plan comptable Imputation");
                }
            }
        }

        private void DeletePlanComptableTva(UIElement uIElement)
        {
            Plan_Comptable_Tva plancomptabletva = ((ParametrePlanComptableTvaControl)uIElement).Remove();

            if (plancomptabletva != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Plan_Comptable_Tva.DeleteObject(plancomptabletva);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePlanComptableTvaControl)uIElement).MiseAJourEtat("Suppression", plancomptabletva);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un plan comptable TVA");
                }
            }
        }

        private void DeleteTypeCommande(UIElement uIElement)
        {
            Type_Commande typecommande = ((ParametreTypeCommandeControl)uIElement).Remove();

            if (typecommande != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Type_Commande.DeleteObject(typecommande);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeCommandeControl)uIElement).MiseAJourEtat("Suppression", typecommande);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un type de commande");
                }
            }
        }

        private void DeleteOutillage(UIElement uIElement)
        {
            Outillage outillage = ((ParametreOutillageControl)uIElement).Remove();

            if (outillage != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Outillage.DeleteObject(outillage);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreOutillageControl)uIElement).MiseAJourEtat("Suppression", outillage);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un outillage");
                }
            }
        }

        private void DeleteExercice(UIElement uIElement)
        {
            Exercice exercice = ((ParametreExerciceControl)uIElement).Remove();

            if (exercice != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Exercice.DeleteObject(exercice);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreExerciceControl)uIElement).MiseAJourEtat("Suppression", exercice);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un exercice");
                }
            }
        }

        private void DeleteQualification(UIElement uIElement)
        {
            Qualification qualification = ((ParametreQualificationControl)uIElement).Remove();

            if (qualification != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Qualification.DeleteObject(qualification);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreQualificationControl)uIElement).MiseAJourEtat("Suppression", qualification);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une qualification");
                }
            }
        }

        private void DeleteBanque(UIElement uIElement)
        {
            Banque banque = ((ParametreBanqueControl)uIElement).Remove();

            if (banque != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Banque.DeleteObject(banque);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreBanqueControl)uIElement).MiseAJourEtat("Suppression", banque);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une banque");
                }
            }
        }

        private void DeleteEtatDevis(UIElement uIElement)
        {
            Devis_Etat etatdevis = ((ParametreEtatDevisControl)uIElement).Remove();

            if (etatdevis != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Devis_Etat.DeleteObject(etatdevis);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreEtatDevisControl)uIElement).MiseAJourEtat("Suppression", etatdevis);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un etat de devis");
                }
            }
        }

        private void DeleteTypeDevis(UIElement uIElement)
        {
            Devis_Type typedevis = ((ParametreTypeDevisControl)uIElement).Remove();

            if (typedevis != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Devis_Type.DeleteObject(typedevis);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeDevisControl)uIElement).MiseAJourEtat("Suppression", typedevis);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un type de devis");
                }
            }
        }

        private void DeleteHabilitation(UIElement uIElement)
        {
            Habilitation habilitation = ((ParametreHabilitationControl)uIElement).Remove();

            if (habilitation != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Habilitation.DeleteObject(habilitation);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreHabilitationControl)uIElement).MiseAJourEtat("Suppression", habilitation);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une habilitation");
                }
            }
        }

        private void DeleteActivite(UIElement uIElement)
        {
            Activite activite = ((ParametreActiviteControl)uIElement).Remove();

            if (activite != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Activite.DeleteObject(activite);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreActiviteControl)uIElement).MiseAJourEtat("Suppression", activite);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une activité");
                }
            }
        }

        private void DeletePays(UIElement uIElement)
        {
            Pays pays = ((ParametrePaysControl)uIElement).Remove();

            if (pays != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Pays.DeleteObject(pays);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePaysControl)uIElement).MiseAJourEtat("Suppression", pays);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un pays");
                }
            }
        }

        private void DeleteDepartement(UIElement uIElement)
        {
            Departement departement = ((ParametreDepartementControl)uIElement).Remove();

            if (departement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Departement.DeleteObject(departement);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDepartementControl)uIElement).MiseAJourEtat("Suppression", departement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un département");
                }
            }
        }

        private void DeleteVille(UIElement uIElement)
        {
            Ville ville = ((ParametreVilleControl)uIElement).Remove();

            if (ville != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Ville.DeleteObject(ville);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreVilleControl)uIElement).MiseAJourEtat("Suppression", ville);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un ville");
                }
            }
        }

        private void DeletePermis(UIElement uIElement)
        {
            Permis permis = ((ParametrePermisControl)uIElement).Remove();

            if (permis != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Permis.DeleteObject(permis);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePermisControl)uIElement).MiseAJourEtat("Suppression", permis);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un permis");
                }
            }
        }

        private void DeleteGroupe(UIElement uIElement)
        {
            Groupe groupe = ((ParametreGroupeControl)uIElement).Remove();

            if (groupe != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Groupe.DeleteObject(groupe);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreGroupeControl)uIElement).MiseAJourEtat("Suppression", groupe);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un groupe");
                }
            }
        }

        private void DeleteModeFacturation(UIElement uIElement)
        {
            Mode_Facturation modefacturation = ((ParametreModeFacturationControl)uIElement).Remove();

            if (modefacturation != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Mode_Facturation.DeleteObject(modefacturation);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreModeFacturationControl)uIElement).MiseAJourEtat("Suppression", modefacturation);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un mode de facturation");
                }
            }
        }

        private void DeleteLitige(UIElement uIElement)
        {
            Litige litige = ((ParametreLitigeControl)uIElement).Remove();

            if (litige != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Litige.DeleteObject(litige);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreLitigeControl)uIElement).MiseAJourEtat("Suppression", litige);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un litige");
                }
            }
        }

        private void DeleteDomaine(UIElement uIElement)
        {
            Domaine domaine = ((ParametreDomaineControl)uIElement).Remove();

            if (domaine != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Domaine.DeleteObject(domaine);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDomaineControl)uIElement).MiseAJourEtat("Suppression", domaine);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un domaine");
                }
            }
        }

        private void DeleteTypeEntreprise(UIElement uIElement)
        {
            Type_Entreprise typeentreprise = ((ParametreTypeEntrepriseControl)uIElement).Remove();

            if (typeentreprise != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Type_Entreprise.DeleteObject(typeentreprise);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeEntrepriseControl)uIElement).MiseAJourEtat("Suppression", typeentreprise);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un type d'entreprise");
                }
            }
        }

        private void DeleteContrat(UIElement uIElement)
        {
            Contrat contrat = ((ParametreContratControl)uIElement).Remove();

            if (contrat != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Contrat.DeleteObject(contrat);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreContratControl)uIElement).MiseAJourEtat("Suppression", contrat);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un contrat");
                }
            }
        }

        private void DeleteDocTypeSalarie(UIElement uIElement)
        {
            Document_Type_Salarie doctypesal = ((ParametreDocTypeSalarieControl)uIElement).Remove();

            if (doctypesal != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Document_Type_Salarie.DeleteObject(doctypesal);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDocTypeSalarieControl)uIElement).MiseAJourEtat("Suppression", doctypesal);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un document type salarié");
                }
            }
        }

        private void DeleteTypeReception(UIElement uIElement)
        {
            Type_Reception typereception = ((ParametreTypeReceptionControl)uIElement).Remove();

            if (typereception != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Type_Reception.DeleteObject(typereception);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeReceptionControl)uIElement).MiseAJourEtat("Suppression", typereception);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un type de reception");
                }
            }
        }

        private void DeleteContactFonction(UIElement uIElement)
        {
            Contact_Fonction contactfonction = ((ParametreContactFonctionControl)uIElement).Remove();

            if (contactfonction != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Contact_Fonction.DeleteObject(contactfonction);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreContactFonctionControl)uIElement).MiseAJourEtat("Suppression", contactfonction);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une fonction");
                }
            }
        }

        private void DeleteContactService(UIElement uIElement)
        {
            Contact_Service contactservice = ((ParametreContactServiceControl)uIElement).Remove();

            if (contactservice != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Contact_Service.DeleteObject(contactservice);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreContactServiceControl)uIElement).MiseAJourEtat("Suppression", contactservice);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un service");
                }
            }
        }

        private void DeleteNiveauSecurite(UIElement uIElement)
        {
            Niveau_Securite niveau_securite = ((ParametreNiveauSecuriteControl)uIElement).Remove();

            if (niveau_securite != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Niveau_Securite.DeleteObject(niveau_securite);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreNiveauSecuriteControl)uIElement).MiseAJourEtat("Suppression", niveau_securite);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un niveau de sécurité");
                }
            }
        }

        private void DeleteTauxHoraire(UIElement uIElement)
        {
            Taux_Horaire taux_horaire = ((ParametreTauxHoraireControl)uIElement).Remove();

            if (taux_horaire != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Taux_Horaire.DeleteObject(taux_horaire);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTauxHoraireControl)uIElement).MiseAJourEtat("Suppression", taux_horaire);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un taux horaire");
                }
            }
        }

        private void DeleteUtilisateur(UIElement uIElement)
        {
            Utilisateur utilisateur = ((ParametreUtilisateurControl)uIElement).Remove();

            if (utilisateur != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Utilisateur.DeleteObject(utilisateur);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreUtilisateurControl)uIElement).MiseAJourEtat("Suppression", utilisateur);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un utilisateur");
                }
            }
        }

        private void DeleteDiplome(UIElement uIElement)
        {
            Diplome diplome = ((ParametreDiplomeControl)uIElement).Remove();

            if (diplome != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Diplome.DeleteObject(diplome);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDiplomeControl)uIElement).MiseAJourEtat("Suppression", diplome);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un diplôme");

                }
            }
        }

        private void DeleteDevise(UIElement uIElement)
        {
            Devise devise = ((ParametreDeviseControl)uIElement).Remove();

            if (devise != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Devise.DeleteObject(devise);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDeviseControl)uIElement).MiseAJourEtat("Suppression", devise);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une devise");
                }
            }
        }

        private void DeleteFormation(UIElement uIElement)
        {
            Formation formation = ((ParametreFormationControl)uIElement).Remove();

            if (formation != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Formation.DeleteObject(formation);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreFormationControl)uIElement).MiseAJourEtat("Suppression", formation);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une formation");
                }
            }
        }

        private void DeleteRegion(UIElement uIElement)
        {
            Region region = ((ParametreRegionControl)uIElement).Remove();

            if (region != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Region.DeleteObject(region);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreRegionControl)uIElement).MiseAJourEtat("Suppression", region);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une région");
                }
            }
        }

        private void DeleteConditionReglement(UIElement uIElement)
        {
            Condition_Reglement conditionreglement = ((ParametreConditionReglementControl)uIElement).Remove();

            if (conditionreglement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Condition_Reglement.DeleteObject(conditionreglement);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreConditionReglementControl)uIElement).MiseAJourEtat("Suppression", conditionreglement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une condition de réglement");
                }
            }
        }

        private void DeleteEntrepriseMere(UIElement uIElement)
        {
            Entreprise_Mere entreprise_mere = ((ParametreEntrepriseMereControl)uIElement).Remove();

            if (entreprise_mere != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Entreprise_Mere.DeleteObject(entreprise_mere);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreEntrepriseMereControl)uIElement).MiseAJourEtat("Suppression", entreprise_mere);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une entreprise mère");
                }
            }
        }

        private void DeleteCivilite(UIElement uIElement)
        {
            Civilite civilite = ((ParametreCiviliteControl)uIElement).Remove();

            if (civilite != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Civilite.DeleteObject(civilite);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreCiviliteControl)uIElement).MiseAJourEtat("Suppression", civilite);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une civilité");
                }
            }
        }

        private void DeleteVersionType(UIElement uIElement)
        {
            Version_Type version_type = ((ParametreVersionTypeControl)uIElement).Remove();

            if (version_type != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Version_Type.DeleteObject(version_type);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreVersionTypeControl)uIElement).MiseAJourEtat("Suppression", version_type);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un type de version");
                }
            }
        }

        #endregion

        private void _CommandDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this._BorderContent != null && this._BorderContent.Child != null)
            {
                UIElement uIElement = this._BorderContent.Child;
                if (this.securite.ControlsRemoveAuthorizedParameter.Contains(uIElement.ToString()))
                {
                    e.CanExecute = this.securite.VerificationDroitActionsCRUDParameters(uIElement.ToString(), "Remove");
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region update

        private void _CommandUpdate_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            UIElement uIElement = this._BorderContent.Child;

            switch (uIElement.ToString())
            {
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeFraisControl":
                    this.UpdateFrais(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMoyenReglementControl":
                    this.UpdateMoyenReglement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreArticleFactureControl":
                    this.UpdateArticleFacture(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl":
                    this.UpdateDistanceVille(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreBesoinReservationSalleControl":
                    this.UpdateBesoinReservationSalle(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl":
                    this.UpdateTypeRemboursement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEvenementRemboursementControl":
                    this.UpdateEvenementRemboursement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableTvaControl":
                    this.UpdatePlanComptableTva(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableImputationControl":
                    this.UpdatePlanComptableImputation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl":
                    this.UpdateCivilite(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl":
                    this.UpdateTypeCommande(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeReceptionControl":
                    this.UpdateTypeReception(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePermisControl":
                    this.UpdatePermis(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreNiveauSecuriteControl":
                    this.UpdateNiveauSecurite(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreUtilisateurControl":
                    this.UpdateUtilisateur(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTauxHoraireControl":
                    this.UpdateTauxHoraire(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDepartementControl":
                    this.UpdateDepartement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDeviseControl":
                    this.UpdateDevise(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreFormationControl":
                    this.UpdateFormation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreRegionControl":
                    this.UpdateRegion(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreConditionReglementControl":
                    this.UpdateConditionReglement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl":
                    this.UpdateEntrepriseMere(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreVersionTypeControl":
                    this.UpdateVersionType(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDocTypeSalarieControl":
                    this.UpdateDocTypeSalarie(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContratControl":
                    this.UpdateContrat(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeEntrepriseControl":
                    this.UpdateTypeEntreprise(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDomaineControl":
                    this.UpdateDomaine(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreLitigeControl":
                    this.UpdateLitige(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreModeFacturationControl":
                    this.UpdateModeFacturation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl":
                    this.UpdateGroupe(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreVilleControl":
                    this.UpdateVille(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl":
                    this.UpdateActivite(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePaysControl":
                    this.UpdatePays(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreHabilitationControl":
                    this.UpdateHabilitation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreQualificationControl":
                    this.UpdateQualification(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl":
                    this.UpdateContactFonction(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl":
                    this.UpdateContactService(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDiplomeControl":
                    this.UpdateDiplome(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreBanqueControl":
                    this.UpdateBanque(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEtatDevisControl":
                    this.UpdateEtatDevis(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeDevisControl":
                    this.UpdateTypeDevis(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreExerciceControl":
                    this.UpdateExercice(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreOutillageControl":
                    this.UpdateOutillage(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTvaControl":
                    this.UpdateTva(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTacheAtelierControl":
                    this.UpdateTacheAtelier(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifDemandeCongeControl":
                    this.UpdateMotifDemandeConge(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifRefusCongeControl":
                    this.UpdateMotifRefusConge(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreJourFerieControl":
                    this.UpdateJourFerie(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePieceAdministrativeControl":
                    this.UpdatePieceAdministrative(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifMissionControl":
                    this.UpdateMotifMission(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreSalleControl":
                    this.UpdateSalle(uIElement);
                    break;
                default:
                    MessageBox.Show("Vous n'avez normalement pas à cliquer sur ce bouton, il devrait être grisé. Si vous recevez cette erreur, merci d'envoyer un mail à l'administrateur avec une image de l'erreur, le nom du bouton ainsi que le nom de votre utilisateur.");
                    break;
            }

            this.Cursor = ((App)App.Current)._mainCursor;
        }

        #region Fonctions des Update

        private void UpdateFrais(UIElement uIElement)
        {
            Type_Frais frais = ((ParametreTypeFraisControl)uIElement).Open();

            if (frais != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeFraisControl)uIElement).MiseAJourEtat("Modification", frais);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un frais");
                }
            }
        }

        private void UpdateMoyenReglement(UIElement uIElement)
        {
            Moyen_Reglement moyenreglement = ((ParametreMoyenReglementControl)uIElement).Open();

            if (moyenreglement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreMoyenReglementControl)uIElement).MiseAJourEtat("Modification", moyenreglement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un moyen de règlement");
                }
            }
        }

        private void UpdateArticleFacture(UIElement uIElement)
        {
            Article_Facture articlefacture = ((ParametreArticleFactureControl)uIElement).Open();

            if (articlefacture != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreArticleFactureControl)uIElement).MiseAJourEtat("Modification", articlefacture);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un article");
                }
            }
        }


        private void UpdateBesoinReservationSalle(UIElement uIElement)
        {
            Besoin_Reservation_Salle besoinreservationsalle = ((ParametreBesoinReservationSalleControl)uIElement).Open();

            if (besoinreservationsalle != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreBesoinReservationSalleControl)uIElement).MiseAJourEtat("Modification", besoinreservationsalle);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un besoin de réservation");
                }
            }
        }

        private void UpdateSalle(UIElement uIElement)
        {
            Salle salle = ((ParametreSalleControl)uIElement).Open();

            if (salle != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreSalleControl)uIElement).MiseAJourEtat("Modification", salle);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une salle");
                }
            }
        }

        private void UpdateDistanceVille(UIElement uIElement)
        {
            Distance_Ville distance_ville = ((ParametreDistanceVilleControl)uIElement).Open();

            if (distance_ville != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDistanceVilleControl)uIElement).MiseAJourEtat("Modification", distance_ville);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une distance ville");
                }
            }
        }

        private void UpdateEvenementRemboursement(UIElement uIElement)
        {
            Evenement_Remboursement evenement_remboursement = ((ParametreEvenementRemboursementControl)uIElement).Open();

            if (evenement_remboursement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreEvenementRemboursementControl)uIElement).MiseAJourEtat("Modification", evenement_remboursement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un evenement remboursement");
                }
            }
        }

        private void UpdateTypeRemboursement(UIElement uIElement)
        {
            Type_Remboursement type_remboursement = ((ParametreTypeRemboursementControl)uIElement).Open();

            if (type_remboursement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeRemboursementControl)uIElement).MiseAJourEtat("Modification", type_remboursement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un type remboursement");
                }
            }
        }

        private void UpdatePieceAdministrative(UIElement uIElement)
        {
            Piece_Administrative pie = ((ParametrePieceAdministrativeControl)uIElement).Open();

            if (pie != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePieceAdministrativeControl)uIElement).MiseAJourEtat("Modification", pie);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une pièce administrative");
                }
            }
        }

        private void UpdateMotifMission(UIElement uIElement)
        {
            Motif_Mission mot = ((ParametreMotifMissionControl)uIElement).Open();

            if (mot != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreMotifMissionControl)uIElement).MiseAJourEtat("Modification", mot);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un motif mission");
                }
            }
        }

        private void UpdateJourFerie(UIElement uIElement)
        {
            JourFerie jou = ((ParametreJourFerieControl)uIElement).Open();

            if (jou != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreJourFerieControl)uIElement).MiseAJourEtat("Modification", jou);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un jour ferié");
                }
            }
        }

        private void UpdateMotifRefusConge(UIElement uIElement)
        {
            Motif_Refus mot = ((ParametreMotifRefusCongeControl)uIElement).Open();

            if (mot != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreMotifRefusCongeControl)uIElement).MiseAJourEtat("Modification", mot);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un motif de refus");
                }
            }
        }

        private void UpdateMotifDemandeConge(UIElement uIElement)
        {
            Motif_Demande mot = ((ParametreMotifDemandeCongeControl)uIElement).Open();

            if (mot != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreMotifDemandeCongeControl)uIElement).MiseAJourEtat("Modification", mot);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un motif de demande");
                }
            }
        }

        private void UpdateTacheAtelier(UIElement uIElement)
        {
            Tache_Atelier tac = ((ParametreTacheAtelierControl)uIElement).Open();

            if (tac != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTacheAtelierControl)uIElement).MiseAJourEtat("Modification", tac);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une tache");
                }
            }
        }

        private void UpdateTva(UIElement uIElement)
        {
            Tva tva = ((ParametreTvaControl)uIElement).Open();

            if (tva != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTvaControl)uIElement).MiseAJourEtat("Modification", tva);

                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une Tva");
                }
            }
        }

        private void UpdatePlanComptableImputation(UIElement uIElement)
        {
            Plan_Comptable_Imputation plancomptableimputation = ((ParametrePlanComptableImputationControl)uIElement).Open();

            if (plancomptableimputation != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePlanComptableImputationControl)uIElement).MiseAJourEtat("Modification", plancomptableimputation);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un plan comptable Imputation");
                }
            }
        }

        private void UpdatePlanComptableTva(UIElement uIElement)
        {
            Plan_Comptable_Tva plancomptabletva = ((ParametrePlanComptableTvaControl)uIElement).Open();

            if (plancomptabletva != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePlanComptableTvaControl)uIElement).MiseAJourEtat("Modification", plancomptabletva);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un plan comptable tva");
                }
            }
        }

        private void UpdateTypeCommande(UIElement uIElement)
        {
            Type_Commande typecommande = ((ParametreTypeCommandeControl)uIElement).Open();

            if (typecommande != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeCommandeControl)uIElement).MiseAJourEtat("Modification", typecommande);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un type de commande");
                }
            }
        }

        private void UpdateOutillage(UIElement uIElement)
        {
            Outillage outillage = ((ParametreOutillageControl)uIElement).Open();

            if (outillage != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreOutillageControl)uIElement).MiseAJourEtat("Modification", outillage);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un outillage");
                }
            }
        }

        private void UpdateExercice(UIElement uIElement)
        {
            Exercice exercice = ((ParametreExerciceControl)uIElement).Open();

            if (exercice != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreExerciceControl)uIElement).MiseAJourEtat("Modification", exercice);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un exercice");
                }
            }
        }

        private void UpdateActivite(UIElement uIElement)
        {
            Activite activite = ((ParametreActiviteControl)uIElement).Open();

            if (activite != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreActiviteControl)uIElement).MiseAJourEtat("Modification", activite);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une activité");
                }
            }
        }

        private void UpdateBanque(UIElement uIElement)
        {
            Banque banque = ((ParametreBanqueControl)uIElement).Open();

            if (banque != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreBanqueControl)uIElement).MiseAJourEtat("Modification", banque);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une banque");
                }
            }
        }

        private void UpdateTypeDevis(UIElement uIElement)
        {
            Devis_Type typedevis = ((ParametreTypeDevisControl)uIElement).Open();

            if (typedevis != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeDevisControl)uIElement).MiseAJourEtat("Modification", typedevis);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un type de devis");
                }
            }
        }

        private void UpdateEtatDevis(UIElement uIElement)
        {
            Devis_Etat etatdevis = ((ParametreEtatDevisControl)uIElement).Open();

            if (etatdevis != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreEtatDevisControl)uIElement).MiseAJourEtat("Modification", etatdevis);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un état de devis");
                }
            }
        }

        private void UpdateQualification(UIElement uIElement)
        {
            Qualification qualification = ((ParametreQualificationControl)uIElement).Open();

            if (qualification != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreQualificationControl)uIElement).MiseAJourEtat("Modification", qualification);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une qualification");
                }
            }
        }

        private void UpdateHabilitation(UIElement uIElement)
        {
            Habilitation habilitation = ((ParametreHabilitationControl)uIElement).Open();

            if (habilitation != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreHabilitationControl)uIElement).MiseAJourEtat("Modification", habilitation);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une habilitation");
                }
            }
        }

        private void UpdateUtilisateur(UIElement uIElement)
        {
            Utilisateur utilisateur = ((ParametreUtilisateurControl)uIElement).Open();

            if (utilisateur != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreUtilisateurControl)uIElement).MiseAJourEtat("Modification", utilisateur);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un utilisateur");
                }
            }
        }

        private void UpdatePays(UIElement uIElement)
        {
            Pays pays = ((ParametrePaysControl)uIElement).Open();

            if (pays != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePaysControl)uIElement).MiseAJourEtat("Modification", pays);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un pays");
                }
            }
        }

        private void UpdateVille(UIElement uIElement)
        {
            Ville ville = ((ParametreVilleControl)uIElement).Open();

            if (ville != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreVilleControl)uIElement).MiseAJourEtat("Modification", ville);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une ville");
                }
            }
        }

        private void UpdateModeFacturation(UIElement uIElement)
        {
            Mode_Facturation modefacturation = ((ParametreModeFacturationControl)uIElement).Open();

            if (modefacturation != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreModeFacturationControl)uIElement).MiseAJourEtat("Modification", modefacturation);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un mode de facturation");
                }
            }
        }

        private void UpdatePermis(UIElement uIElement)
        {
            Permis permis = ((ParametrePermisControl)uIElement).Open();

            if (permis != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePermisControl)uIElement).MiseAJourEtat("Modification", permis);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un permis");
                }
            }
        }

        private void UpdateTypeEntreprise(UIElement uIElement)
        {
            Type_Entreprise typeentreprise = ((ParametreTypeEntrepriseControl)uIElement).Open();

            if (typeentreprise != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeEntrepriseControl)uIElement).MiseAJourEtat("Modification", typeentreprise);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un type d'entreprise");
                }
            }
        }

        private void UpdateGroupe(UIElement uIElement)
        {
            Groupe groupe = ((ParametreGroupeControl)uIElement).Open();

            if (groupe != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreGroupeControl)uIElement).MiseAJourEtat("Modification", groupe);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un groupe");
                }
            }
        }

        private void UpdateLitige(UIElement uIElement)
        {
            Litige litige = ((ParametreLitigeControl)uIElement).Open();

            if (litige != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreLitigeControl)uIElement).MiseAJourEtat("Modification", litige);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un litige");
                }
            }
        }

        private void UpdateDomaine(UIElement uIElement)
        {
            Domaine domaine = ((ParametreDomaineControl)uIElement).Open();

            if (domaine != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDomaineControl)uIElement).MiseAJourEtat("Modification", domaine);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un domaine");
                }
            }
        }

        private void UpdateContrat(UIElement uIElement)
        {
            Contrat contrat = ((ParametreContratControl)uIElement).Open();

            if (contrat != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreContratControl)uIElement).MiseAJourEtat("Modification", contrat);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un contrat");
                }
            }
        }

        private void UpdateDocTypeSalarie(UIElement uIElement)
        {
            Document_Type_Salarie doctypesal = ((ParametreDocTypeSalarieControl)uIElement).Open();

            if (doctypesal != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDocTypeSalarieControl)uIElement).MiseAJourEtat("Modification", doctypesal);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un document type salarié");
                }
            }
        }

        private void UpdateContactFonction(UIElement uIElement)
        {
            Contact_Fonction contactfonction = ((ParametreContactFonctionControl)uIElement).Open();

            if (contactfonction != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreContactFonctionControl)uIElement).MiseAJourEtat("Modification", contactfonction);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une fonction");
                }
            }
        }

        private void UpdateContactService(UIElement uIElement)
        {
            Contact_Service contactservice = ((ParametreContactServiceControl)uIElement).Open();

            if (contactservice != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreContactServiceControl)uIElement).MiseAJourEtat("Modification", contactservice);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un service");
                }
            }
        }

        private void UpdateDepartement(UIElement uIElement)
        {
            Departement departement = ((ParametreDepartementControl)uIElement).Open();

            if (departement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDepartementControl)uIElement).MiseAJourEtat("Modification", departement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un département");
                }
            }
        }

        private void UpdateNiveauSecurite(UIElement uIElement)
        {
            Niveau_Securite nivueau_securite = ((ParametreNiveauSecuriteControl)uIElement).Open();

            if (nivueau_securite != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreNiveauSecuriteControl)uIElement).MiseAJourEtat("Modification", nivueau_securite);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un niveau de sécurité");
                }
            }
        }

        private void UpdateTauxHoraire(UIElement uIElement)
        {
            Taux_Horaire taux_horaire = ((ParametreTauxHoraireControl)uIElement).Open();

            if (taux_horaire != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTauxHoraireControl)uIElement).MiseAJourEtat("Modification", taux_horaire);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un taux horaire");
                }
            }
        }

        private void UpdateDiplome(UIElement uIElement)
        {
            Diplome diplome = ((ParametreDiplomeControl)uIElement).Open();

            if (diplome != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDiplomeControl)uIElement).MiseAJourEtat("Modification", diplome);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un diplome");
                }
            }
        }

        private void UpdateTypeReception(UIElement uIElement)
        {
            Type_Reception typereception = ((ParametreTypeReceptionControl)uIElement).Open();

            if (typereception != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeReceptionControl)uIElement).MiseAJourEtat("Modification", typereception);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un type de reception");
                }
            }
        }

        private void UpdateDevise(UIElement uIElement)
        {
            Devise devise = ((ParametreDeviseControl)uIElement).Open();

            if (devise != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDeviseControl)uIElement).MiseAJourEtat("Modification", devise);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une devise");
                }
            }
        }

        private void UpdateFormation(UIElement uIElement)
        {
            Formation formation = ((ParametreFormationControl)uIElement).Open();

            if (formation != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreFormationControl)uIElement).MiseAJourEtat("Modification", formation);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une formation");
                }
            }
        }

        private void UpdateRegion(UIElement uIElement)
        {
            Region region = ((ParametreRegionControl)uIElement).Open();

            if (region != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreRegionControl)uIElement).MiseAJourEtat("Modification", region);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une région");
                }
            }
        }

        private void UpdateConditionReglement(UIElement uIElement)
        {
            Condition_Reglement conditionreglement = ((ParametreConditionReglementControl)uIElement).Open();

            if (conditionreglement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreConditionReglementControl)uIElement).MiseAJourEtat("Modification", conditionreglement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une condition de réglement");
                }
            }
        }

        private void UpdateEntrepriseMere(UIElement uIElement)
        {
            Entreprise_Mere entreprise_mere = ((ParametreEntrepriseMereControl)uIElement).Open();

            if (entreprise_mere != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreEntrepriseMereControl)uIElement).MiseAJourEtat("Modification", entreprise_mere);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une entreprise mère");
                }
            }
        }

        private void UpdateVersionType(UIElement uIElement)
        {
            Version_Type version_type = ((ParametreVersionTypeControl)uIElement).Open();

            if (version_type != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreVersionTypeControl)uIElement).MiseAJourEtat("Modification", version_type);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un type de version");
                }
            }
        }

        private void UpdateCivilite(UIElement uIElement)
        {
            Civilite civilite = ((ParametreCiviliteControl)uIElement).Open();

            if (civilite != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreCiviliteControl)uIElement).MiseAJourEtat("Modification", civilite);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une civilité");
                }
            }
        }

        #endregion

        private void _CommandUpdate_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this._BorderContent != null && this._BorderContent.Child != null)
            {
                UIElement uIElement = this._BorderContent.Child;
                if (this.securite.ControlsUpdateAuthorizedParameter.Contains(uIElement.ToString()))
                {
                    e.CanExecute = this.securite.VerificationDroitActionsCRUDParameters(uIElement.ToString(), "Update");
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region add

        private void _CommandAdd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            UIElement uIElement = this._BorderContent.Child;

            switch (uIElement.ToString())
            {
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeFraisControl":
                    this.AddFrais(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMoyenReglementControl":
                    this.AddMoyenReglement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreArticleFactureControl":
                    this.AddArticleFacture(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl":
                    this.AddDistanceVille(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreBesoinReservationSalleControl":
                    this.AddBesoinReservationSalle(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreSalleControl":
                    this.AddSalle(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl":
                    this.AddTypeRemboursement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEvenementRemboursementControl":
                    this.AddEvenementRemboursement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableImputationControl":
                    this.AddPlanComptableImputation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableTvaControl":
                    this.AddPlanComptableTva(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl":
                    this.AddCivilite(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl":
                    this.AddTypeCommande(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeReceptionControl":
                    this.AddTypeReception(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePermisControl":
                    this.AddPermis(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreNiveauSecuriteControl":
                    this.AddNiveauSecurite(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreUtilisateurControl":
                    this.AddUtilisateur(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTauxHoraireControl":
                    this.AddTauxHoraire(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDepartementControl":
                    this.AddDepartement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDeviseControl":
                    this.AddDevise(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreFormationControl":
                    this.AddFormation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreRegionControl":
                    this.AddRegion(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreConditionReglementControl":
                    this.AddConditionReglement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl":
                    this.AddEntrepriseMere(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreVersionTypeControl":
                    this.AddVersionType(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDocTypeSalarieControl":
                    this.AddDocTypeSalarie(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContratControl":
                    this.AddContrat(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeEntrepriseControl":
                    this.AddTypeEntreprise(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDomaineControl":
                    this.AddDomaine(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreLitigeControl":
                    this.AddLitige(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreModeFacturationControl":
                    this.AddModeFacturation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl":
                    this.AddGroupe(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreVilleControl":
                    this.AddVille(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl":
                    this.AddActivite(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePaysControl":
                    this.AddPays(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreHabilitationControl":
                    this.AddHabilitation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreQualificationControl":
                    this.AddQualification(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl":
                    this.AddContactFonction(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl":
                    this.AddContactService(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDiplomeControl":
                    this.AddDiplome(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreBanqueControl":
                    this.AddBanque(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEtatDevisControl":
                    this.AddEtatDevis(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeDevisControl":
                    this.AddTypeDevis(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreExerciceControl":
                    this.AddExercice(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreOutillageControl":
                    this.AddOutillage(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTvaControl":
                    this.AddTva(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTacheAtelierControl":
                    this.AddTacheAtelier(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifDemandeCongeControl":
                    this.AddMotifDemandeConge(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifRefusCongeControl":
                    this.AddMotifRefusConge(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePieceAdministrativeControl":
                    this.AddPieceAdministrative(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifMissionControl":
                    this.AddMotifMission(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreJourFerieControl":
                    this.AddJourFerie(uIElement);
                    break;
                default:
                    MessageBox.Show("Vous n'avez normalement pas à cliquer sur ce bouton, il devrait être grisé. Si vous recevez cette erreur, merci d'envoyer un mail à l'administrateur avec une image de l'erreur, le nom du bouton ainsi que le nom de votre utilisateur.");
                    break;
            }

            this.Cursor = ((App)App.Current)._mainCursor;
        }

        #region fonctions des ajouts

        public Type_Frais AddFrais(UIElement uIElement)
        {
            Type_Frais frais = ((ParametreTypeFraisControl)uIElement).Add();

            if (frais != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToType_Frais(frais);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeFraisControl)uIElement).MiseAJourEtat("Ajout", frais);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un frais");
                }
            }

            return frais;
        }

        public Article_Facture AddArticleFacture(UIElement uIElement)
        {
            Article_Facture articlefacture = ((ParametreArticleFactureControl)uIElement).Add();

            if (articlefacture != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToArticle_Facture(articlefacture);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreArticleFactureControl)uIElement).MiseAJourEtat("Ajout", articlefacture);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un article");
                }
            }

            return articlefacture;
        }

        public Moyen_Reglement AddMoyenReglement(UIElement uIElement)
        {
            Moyen_Reglement moyenreglement = ((ParametreMoyenReglementControl)uIElement).Add();

            if (moyenreglement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToMoyen_Reglement(moyenreglement);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreMoyenReglementControl)uIElement).MiseAJourEtat("Ajout", moyenreglement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un moyen de règlement");
                }
            }

            return moyenreglement;
        }

        public Besoin_Reservation_Salle AddBesoinReservationSalle(UIElement uIElement)
        {
            Besoin_Reservation_Salle besoinreservationsalle = ((ParametreBesoinReservationSalleControl)uIElement).Add();

            if (besoinreservationsalle != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToBesoin_Reservation_Salle(besoinreservationsalle);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreBesoinReservationSalleControl)uIElement).MiseAJourEtat("Ajout", besoinreservationsalle);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un besoin de réservation");
                }
            }

            return besoinreservationsalle;
        }

        public Salle AddSalle(UIElement uIElement)
        {
            Salle salle = ((ParametreSalleControl)uIElement).Add();

            if (salle != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToSalle(salle);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreSalleControl)uIElement).MiseAJourEtat("Ajout", salle);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une salle");
                }
            }

            return salle;
        }

        public Distance_Ville AddDistanceVille(UIElement uIElement)
        {
            Distance_Ville distance_ville = ((ParametreDistanceVilleControl)uIElement).Add();

            if (distance_ville != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToDistance_Ville(distance_ville);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDistanceVilleControl)uIElement).MiseAJourEtat("Ajout", distance_ville);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une distance ville");
                }
            }

            return distance_ville;
        }

        public Evenement_Remboursement AddEvenementRemboursement(UIElement uIElement)
        {
            Evenement_Remboursement evenement_remboursement = ((ParametreEvenementRemboursementControl)uIElement).Add();

            if (evenement_remboursement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToEvenement_Remboursement(evenement_remboursement);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreEvenementRemboursementControl)uIElement).MiseAJourEtat("Ajout", evenement_remboursement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un evenement remboursement");
                }
            }

            return evenement_remboursement;
        }

        public Type_Remboursement AddTypeRemboursement(UIElement uIElement)
        {
            Type_Remboursement type_remboursement = ((ParametreTypeRemboursementControl)uIElement).Add();

            if (type_remboursement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToType_Remboursement(type_remboursement);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeRemboursementControl)uIElement).MiseAJourEtat("Ajout", type_remboursement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un type remboursement");
                }
            }

            return type_remboursement;
        }

        public Motif_Mission AddMotifMission(UIElement uIElement)
        {
            Motif_Mission mot = ((ParametreMotifMissionControl)uIElement).Add();

            if (mot != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToMotif_Mission(mot);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreMotifMissionControl)uIElement).MiseAJourEtat("Ajout", mot);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un motif mission");
                }
            }

            return mot;
        }

        public Piece_Administrative AddPieceAdministrative(UIElement uIElement)
        {
            Piece_Administrative pie = ((ParametrePieceAdministrativeControl)uIElement).Add();

            if (pie != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToPiece_Administrative(pie);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePieceAdministrativeControl)uIElement).MiseAJourEtat("Ajout", pie);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une pièce administrative");
                }
            }

            return pie;
        }

        public JourFerie AddJourFerie(UIElement uIElement)
        {
            JourFerie jou = ((ParametreJourFerieControl)uIElement).Add();

            if (jou != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToJourFerie(jou);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreJourFerieControl)uIElement).MiseAJourEtat("Ajout", jou);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un jour ferié");
                }
            }

            return jou;
        }

        public Motif_Refus AddMotifRefusConge(UIElement uIElement)
        {
            Motif_Refus mot = ((ParametreMotifRefusCongeControl)uIElement).Add();

            if (mot != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToMotif_Refus(mot);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreMotifRefusCongeControl)uIElement).MiseAJourEtat("Ajout", mot);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un motif de refus");
                }
            }

            return mot;
        }

        public Motif_Demande AddMotifDemandeConge(UIElement uIElement)
        {
            Motif_Demande mot = ((ParametreMotifDemandeCongeControl)uIElement).Add();

            if (mot != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToMotif_Demande(mot);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreMotifDemandeCongeControl)uIElement).MiseAJourEtat("Ajout", mot);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un motif de demande");
                }
            }

            return mot;
        }

        public Tache_Atelier AddTacheAtelier(UIElement uIElement)
        {
            Tache_Atelier tac = ((ParametreTacheAtelierControl)uIElement).Add();

            if (tac != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToTache_Atelier(tac);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTacheAtelierControl)uIElement).MiseAJourEtat("Ajout", tac);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une tache");
                }
            }

            return tac;
        }

        public Tva AddTva(UIElement uIElement)
        {
            Tva tva = ((ParametreTvaControl)uIElement).Add();

            if (tva != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToTva(tva);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTvaControl)uIElement).MiseAJourEtat("Ajout", tva);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une Tva");
                }
            }

            return tva;
        }

        public Plan_Comptable_Imputation AddPlanComptableImputation(UIElement uIElement)
        {
            Plan_Comptable_Imputation plancomptableimputation = ((ParametrePlanComptableImputationControl)uIElement).Add();

            if (plancomptableimputation != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToPlan_Comptable_Imputation(plancomptableimputation);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePlanComptableImputationControl)uIElement).MiseAJourEtat("Ajout", plancomptableimputation);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un plan comptable Imputation");
                }
            }

            return plancomptableimputation;
        }

        public Plan_Comptable_Tva AddPlanComptableTva(UIElement uIElement)
        {
            Plan_Comptable_Tva plancomptabletva = ((ParametrePlanComptableTvaControl)uIElement).Add();

            if (plancomptabletva != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToPlan_Comptable_Tva(plancomptabletva);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePlanComptableTvaControl)uIElement).MiseAJourEtat("Ajout", plancomptabletva);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un plan comptable tva");
                }
            }

            return plancomptabletva;
        }

        public Type_Commande AddTypeCommande(UIElement uIElement)
        {
            Type_Commande typecommande = ((ParametreTypeCommandeControl)uIElement).Add();

            if (typecommande != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToType_Commande(typecommande);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeCommandeControl)uIElement).MiseAJourEtat("Ajout", typecommande);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau type de commande");
                }
            }

            return typecommande;
        }

        public Outillage AddOutillage(UIElement uIElement)
        {
            Outillage outillage = ((ParametreOutillageControl)uIElement).Add();

            if (outillage != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToOutillage(outillage);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreOutillageControl)uIElement).MiseAJourEtat("Ajout", outillage);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouvel outillage");
                }
            }

            return outillage;
        }

        public Exercice AddExercice(UIElement uIElement)
        {
            Exercice exercice = ((ParametreExerciceControl)uIElement).Add();

            if (exercice != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToExercice(exercice);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreExerciceControl)uIElement).MiseAJourEtat("Ajout", exercice);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouvel exercice");
                }
            }

            return exercice;
        }

        public Diplome AddDiplome(UIElement uIElement)
        {
            Diplome diplome = ((ParametreDiplomeControl)uIElement).Add();

            if (diplome != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToDiplome(diplome);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDiplomeControl)uIElement).MiseAJourEtat("Ajout", diplome);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau diplôme");
                }
            }

            return diplome;
        }

        public Banque AddBanque(UIElement uIElement)
        {
            Banque banque = ((ParametreBanqueControl)uIElement).Add();

            if (banque != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToBanque(banque);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreBanqueControl)uIElement).MiseAJourEtat("Ajout", banque);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle banque");
                }
            }

            return banque;
        }

        public Devis_Etat AddEtatDevis(UIElement uIElement)
        {
            Devis_Etat etatdevis = ((ParametreEtatDevisControl)uIElement).Add();

            if (etatdevis != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToDevis_Etat(etatdevis);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreEtatDevisControl)uIElement).MiseAJourEtat("Ajout", etatdevis);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouvel etat de devis");
                }
            }

            return etatdevis;
        }

        public Devis_Type AddTypeDevis(UIElement uIElement)
        {
            Devis_Type typedevis = ((ParametreTypeDevisControl)uIElement).Add();

            if (typedevis != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToDevis_Type(typedevis);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeDevisControl)uIElement).MiseAJourEtat("Ajout", typedevis);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau type de devis");
                }
            }

            return typedevis;
        }

        public Contact_Service AddContactService(UIElement uIElement)
        {
            Contact_Service contactservive = ((ParametreContactServiceControl)uIElement).Add();

            if (contactservive != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToContact_Service(contactservive);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreContactServiceControl)uIElement).MiseAJourEtat("Ajout", contactservive);

                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau service");
                }
            }

            return contactservive;
        }

        public Contact_Fonction AddContactFonction(UIElement uIElement)
        {
            Contact_Fonction contactfonction = ((ParametreContactFonctionControl)uIElement).Add();

            if (contactfonction != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToContact_Fonction(contactfonction);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreContactFonctionControl)uIElement).MiseAJourEtat("Ajout", contactfonction);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle fonction");
                }

            }
            return contactfonction;
        }

        public Qualification AddQualification(UIElement uIElement)
        {
            Qualification qualification = ((ParametreQualificationControl)uIElement).Add();

            if (qualification != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToQualification(qualification);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreQualificationControl)uIElement).MiseAJourEtat("Ajout", qualification);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle qualification");
                }
            }
            return qualification;
        }

        public Habilitation AddHabilitation(UIElement uIElement)
        {
            Habilitation habiliatation = ((ParametreHabilitationControl)uIElement).Add();

            if (habiliatation != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToHabilitation(habiliatation);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreHabilitationControl)uIElement).MiseAJourEtat("Ajout", habiliatation);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle habilitation");
                }
            }

            return habiliatation;
        }

        public Pays AddPays(UIElement uIElement)
        {
            Pays pays = ((ParametrePaysControl)uIElement).Add();

            if (pays != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToPays(pays);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePaysControl)uIElement).MiseAJourEtat("Ajout", pays);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau pays");
                }
            }

            return pays;
        }

        public Activite AddActivite(UIElement uIElement)
        {
            Activite activite = ((ParametreActiviteControl)uIElement).Add();

            if (activite != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToActivite(activite);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreActiviteControl)uIElement).MiseAJourEtat("Ajout", activite);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle activité");
                }
            }

            return activite;
        }

        public Ville AddVille(UIElement uIElement)
        {
            Ville ville = ((ParametreVilleControl)uIElement).Add();

            if (ville != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToVille(ville);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreVilleControl)uIElement).MiseAJourEtat("Ajout", ville);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle ville");
                }
            }

            return ville;
        }

        public Groupe AddGroupe(UIElement uIElement)
        {
            Groupe groupe = ((ParametreGroupeControl)uIElement).Add();

            if (groupe != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToGroupe(groupe);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreGroupeControl)uIElement).MiseAJourEtat("Ajout", groupe);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau groupe");
                }
            }

            return groupe;
        }

        public Mode_Facturation AddModeFacturation(UIElement uIElement)
        {
            Mode_Facturation modefacturation = ((ParametreModeFacturationControl)uIElement).Add();

            if (modefacturation != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToMode_Facturation(modefacturation);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreModeFacturationControl)uIElement).MiseAJourEtat("Ajout", modefacturation);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau mode de facturation");
                }
            }

            return modefacturation;
        }

        public Litige AddLitige(UIElement uIElement)
        {
            Litige litige = ((ParametreLitigeControl)uIElement).Add();

            if (litige != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToLitige(litige);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreLitigeControl)uIElement).MiseAJourEtat("Ajout", litige);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau litige");
                }
            }

            return litige;
        }

        public Domaine AddDomaine(UIElement uIElement)
        {
            Domaine domaine = ((ParametreDomaineControl)uIElement).Add();

            if (domaine != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToDomaine(domaine);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDomaineControl)uIElement).MiseAJourEtat("Ajout", domaine);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau domaine");
                }
            }

            return domaine;
        }

        public Type_Entreprise AddTypeEntreprise(UIElement uIElement)
        {
            Type_Entreprise typeentreprise = ((ParametreTypeEntrepriseControl)uIElement).Add();

            if (typeentreprise != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToType_Entreprise(typeentreprise);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeEntrepriseControl)uIElement).MiseAJourEtat("Ajout", typeentreprise);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau type d'entreprise");
                }
            }

            return typeentreprise;
        }

        public Contrat AddContrat(UIElement uIElement)
        {
            Contrat contrat = ((ParametreContratControl)uIElement).Add();

            if (contrat != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToContrat(contrat);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreContratControl)uIElement).MiseAJourEtat("Ajout", contrat);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau contrat");
                }
            }
            return contrat;
        }

        public Document_Type_Salarie AddDocTypeSalarie(UIElement uIElement)
        {
            Document_Type_Salarie doctypesal = ((ParametreDocTypeSalarieControl)uIElement).Add();

            if (doctypesal != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToDocument_Type_Salarie(doctypesal);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDocTypeSalarieControl)uIElement).MiseAJourEtat("Ajout", doctypesal);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau document type salarié");
                }
            }

            return doctypesal;
        }

        public Type_Reception AddTypeReception(UIElement uIElement)
        {
            Type_Reception typerecption = ((ParametreTypeReceptionControl)uIElement).Add();

            if (typerecption != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToType_Reception(typerecption);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTypeReceptionControl)uIElement).MiseAJourEtat("Ajout", typerecption);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau type de reception");
                }
            }

            return typerecption;
        }

        public Permis AddPermis(UIElement uIElement)
        {
            Permis permis = ((ParametrePermisControl)uIElement).Add();

            if (permis != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToPermis(permis);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametrePermisControl)uIElement).MiseAJourEtat("Ajout", permis);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau permis");
                }
            }

            return permis;
        }

        public Niveau_Securite AddNiveauSecurite(UIElement uIElement)
        {
            Niveau_Securite niveau_securite = ((ParametreNiveauSecuriteControl)uIElement).Add();

            if (niveau_securite != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToNiveau_Securite(niveau_securite);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreNiveauSecuriteControl)uIElement).MiseAJourEtat("Ajout", niveau_securite);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau niveau de sécurité");
                }
            }

            return niveau_securite;
        }

        public Utilisateur AddUtilisateur(UIElement uIElement)
        {
            Utilisateur utilisateur = ((ParametreUtilisateurControl)uIElement).Add();

            if (utilisateur != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToUtilisateur(utilisateur);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreUtilisateurControl)uIElement).MiseAJourEtat("Ajout", utilisateur);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouvel utilisateur");
                }
            }
            return utilisateur;
        }

        public Taux_Horaire AddTauxHoraire(UIElement uIElement)
        {
            Taux_Horaire taux_horaire = ((ParametreTauxHoraireControl)uIElement).Add();

            if (taux_horaire != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToTaux_Horaire(taux_horaire);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreTauxHoraireControl)uIElement).MiseAJourEtat("Ajout", taux_horaire);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau taux horaire");
                }
            }

            return taux_horaire;
        }

        public Departement AddDepartement(UIElement uIElement)
        {
            Departement departement = ((ParametreDepartementControl)uIElement).Add();

            if (departement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToDepartement(departement);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDepartementControl)uIElement).MiseAJourEtat("Ajout", departement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau département");
                }
            }
            return departement;
        }

        public Devise AddDevise(UIElement uIElement)
        {
            Devise devise = ((ParametreDeviseControl)uIElement).Add();

            if (devise != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToDevise(devise);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreDeviseControl)uIElement).MiseAJourEtat("Ajout", devise);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle devise");
                }
            }
            return devise;
        }

        public Formation AddFormation(UIElement uIElement)
        {
            Formation formation = ((ParametreFormationControl)uIElement).Add();

            if (formation != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToFormation(formation);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreFormationControl)uIElement).MiseAJourEtat("Ajout", formation);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle formation");
                }
            }

            return formation;
        }

        public Region AddRegion(UIElement uIElement)
        {
            Region region = ((ParametreRegionControl)uIElement).Add();

            if (region != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToRegion(region);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreRegionControl)uIElement).MiseAJourEtat("Ajout", region);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle région");
                }
            }

            return region;
        }

        public Condition_Reglement AddConditionReglement(UIElement uIElement)
        {
            Condition_Reglement conditionreglement = ((ParametreConditionReglementControl)uIElement).Add();

            if (conditionreglement != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToCondition_Reglement(conditionreglement);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreConditionReglementControl)uIElement).MiseAJourEtat("Ajout", conditionreglement);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle condition de réglement");
                }
            }

            return conditionreglement;
        }

        public Entreprise_Mere AddEntrepriseMere(UIElement uIElement)
        {
            Entreprise_Mere entreprise_mere = ((ParametreEntrepriseMereControl)uIElement).Add();

            if (entreprise_mere != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToEntreprise_Mere(entreprise_mere);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreEntrepriseMereControl)uIElement).MiseAJourEtat("Ajout", entreprise_mere);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle entreprise mère");
                }
            }

            return entreprise_mere;
        }

        public Version_Type AddVersionType(UIElement uIElement)
        {
            Version_Type version_type = ((ParametreVersionTypeControl)uIElement).Add();

            if (version_type != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToVersion_Type(version_type);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreVersionTypeControl)uIElement).MiseAJourEtat("Ajout", version_type);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle version type");
                }
            }

            return version_type;
        }

        public Civilite AddCivilite(UIElement uIElement)
        {
            Civilite civilte = ((ParametreCiviliteControl)uIElement).Add();

            if (civilte != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToCivilite(civilte);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ParametreCiviliteControl)uIElement).MiseAJourEtat("Ajout", civilte);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle civilité");
                }
            }
            return civilte;
        }

        #endregion

        private void _CommandAdd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this._BorderContent != null && this._BorderContent.Child != null)
            {
                UIElement uIElement = this._BorderContent.Child;
                if (this.securite.ControlsAddAuthorizedParameter.Contains(uIElement.ToString()))
                {
                    e.CanExecute = this.securite.VerificationDroitActionsCRUDParameters(uIElement.ToString(), "Add");
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region look

        private void _CommandLook_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            UIElement uIElement = this._BorderContent.Child;

            switch (uIElement.ToString())
            {
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreSalleControl":
                    this.LookSalle(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeFraisControl":
                    this.LookFrais(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMoyenReglementControl":
                    this.LookMoyenReglement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreArticleFactureControl":
                    this.LookArticleFacture(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreBesoinReservationSalleControl":
                    this.LookBesoinReservationSalle(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl":
                    this.LookDistanceVille(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl":
                    this.LookTypeRemboursement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEvenementRemboursementControl":
                    this.LookEvenementRemboursement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableTvaControl":
                    this.LookPlanComptableTva(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableImputationControl":
                    this.LookPlanComptableImputation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl":
                    this.LookTypeCommande(uIElement, null);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl":
                    this.LookCivilite(uIElement, null);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeReceptionControl":
                    this.LookTypeReception(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePermisControl":
                    this.LookPermis(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreNiveauSecuriteControl":
                    this.LookNiveauSecurite(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreUtilisateurControl":
                    this.LookUtilisateur(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTauxHoraireControl":
                    this.LookTauxHoraire(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDepartementControl":
                    this.LookDepartement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDeviseControl":
                    this.LookDevise(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreFormationControl":
                    this.LookFormation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreRegionControl":
                    this.LookRegion(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreConditionReglementControl":
                    this.LookConditionReglement(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl":
                    this.LookEntrepriseMere(uIElement, null);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreVersionTypeControl":
                    this.LookVersionType(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDocTypeSalarieControl":
                    this.LookDocTypeSalarie(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContratControl":
                    this.LookContrat(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeEntrepriseControl":
                    this.LookTypeEntreprise(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDomaineControl":
                    this.LookDomaine(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreLitigeControl":
                    this.LookLitige(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreModeFacturationControl":
                    this.LookModeFacturation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl":
                    this.LookGroupe(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreVilleControl":
                    this.LookVille(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl":
                    this.LookActivite(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePaysControl":
                    this.LookPays(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreHabilitationControl":
                    this.LookHabilitation(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreQualificationControl":
                    this.LookQualification(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl":
                    this.LookContactFonction(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl":
                    this.LookContactService(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreDiplomeControl":
                    this.LookDiplome(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreBanqueControl":
                    this.LookBanque(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreEtatDevisControl":
                    this.LookEtatDevis(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTypeDevisControl":
                    this.LookTypeDevis(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreExerciceControl":
                    this.LookExercice(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreOutillageControl":
                    this.LookOutillage(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTvaControl":
                    this.LookTva(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreTacheAtelierControl":
                    this.LookTacheAtelier(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifDemandeCongeControl":
                    this.LookMotifDemandeConge(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifRefusCongeControl":
                    this.LookMotifRefusConge(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreJourFerieControl":
                    this.LookJourFerie(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametrePieceAdministrativeControl":
                    this.LookPieceAdministrative(uIElement);
                    break;
                case "SitaffRibbon.Windows.ParametresUserControls.ParametreMotifMissionControl":
                    this.LookMotifMission(uIElement);
                    break;
                default:
                    MessageBox.Show("Vous n'avez normalement pas à cliquer sur ce bouton, il devrait être grisé. Si vous recevez cette erreur, merci d'envoyer un mail à l'administrateur avec une image de l'erreur, le nom du bouton ainsi que le nom de votre utilisateur.");
                    break;
            }

            this.Cursor = ((App)App.Current)._mainCursor;
        }

        #region Fonctions des Looks

        public void LookFrais(UIElement uIElement)
        {
            ((ParametreTypeFraisControl)uIElement).Look();
        }

        public void LookArticleFacture(UIElement uIElement)
        {
            ((ParametreArticleFactureControl)uIElement).Look();
        }

        public void LookMoyenReglement(UIElement uIElement)
        {
            ((ParametreMoyenReglementControl)uIElement).Look();
        }

        public void LookBesoinReservationSalle(UIElement uIElement)
        {
            ((ParametreBesoinReservationSalleControl)uIElement).Look();
        }

        public void LookSalle(UIElement uIElement)
        {
            ((ParametreSalleControl)uIElement).Look();
        }

        public void LookDistanceVille(UIElement uIElement)
        {
            ((ParametreDistanceVilleControl)uIElement).Look();
        }

        public void LookTypeRemboursement(UIElement uIElement)
        {
            ((ParametreTypeRemboursementControl)uIElement).Look();
        }

        public void LookEvenementRemboursement(UIElement uIElement)
        {
            ((ParametreEvenementRemboursementControl)uIElement).Look();
        }

        public void LookMotifMission(UIElement uIElement)
        {
            ((ParametreMotifMissionControl)uIElement).Look();
        }

        public void LookPieceAdministrative(UIElement uIElement)
        {
            ((ParametrePieceAdministrativeControl)uIElement).Look();
        }

        public void LookJourFerie(UIElement uIElement)
        {
            ((ParametreJourFerieControl)uIElement).Look();
        }

        public void LookMotifDemandeConge(UIElement uIElement)
        {
            ((ParametreMotifDemandeCongeControl)uIElement).Look();
        }

        public void LookMotifRefusConge(UIElement uIElement)
        {
            ((ParametreMotifRefusCongeControl)uIElement).Look();
        }

        public void LookTacheAtelier(UIElement uIElement)
        {
            ((ParametreTacheAtelierControl)uIElement).Look();
        }

        public void LookTva(UIElement uIElement)
        {
            ((ParametreTvaControl)uIElement).Look();
        }

        public void LookPlanComptableImputation(UIElement uIElement)
        {
            ((ParametrePlanComptableImputationControl)uIElement).Look();
        }

        public void LookPlanComptableTva(UIElement uIElement)
        {
            ((ParametrePlanComptableTvaControl)uIElement).Look();
        }

        public void LookTypeCommande(UIElement uIElement, Type_Commande type_commandeToLook)
        {
            ((ParametreTypeCommandeControl)uIElement).Look(type_commandeToLook);
        }

        public void LookOutillage(UIElement uIElement)
        {
            ((ParametreOutillageControl)uIElement).Look();
        }

        public void LookExercice(UIElement uIElement)
        {
            ((ParametreExerciceControl)uIElement).Look();
        }

        public void LookDiplome(UIElement uIElement)
        {
            ((ParametreDiplomeControl)uIElement).Look();
        }

        public void LookBanque(UIElement uIElement)
        {
            ((ParametreBanqueControl)uIElement).Look();
        }

        public void LookEtatDevis(UIElement uIElement)
        {
            ((ParametreEtatDevisControl)uIElement).Look();
        }

        public void LookTypeDevis(UIElement uIElement)
        {
            ((ParametreTypeDevisControl)uIElement).Look();
        }

        public void LookContactService(UIElement uIElement)
        {
            ((ParametreContactServiceControl)uIElement).Look();
        }

        public void LookContactFonction(UIElement uIElement)
        {
            ((ParametreContactFonctionControl)uIElement).Look(null);
        }

        public void LookQualification(UIElement uIElement)
        {
            ((ParametreQualificationControl)uIElement).Look();
        }

        public void LookHabilitation(UIElement uIElement)
        {
            ((ParametreHabilitationControl)uIElement).Look();
        }

        public void LookPays(UIElement uIElement)
        {
            ((ParametrePaysControl)uIElement).Look();
        }

        public void LookActivite(UIElement uIElement)
        {
            ((ParametreActiviteControl)uIElement).Look();
        }

        public void LookVille(UIElement uIElement)
        {
            ((ParametreVilleControl)uIElement).Look();
        }

        public void LookGroupe(UIElement uIElement)
        {
            ((ParametreGroupeControl)uIElement).Look(null);
        }

        public void LookModeFacturation(UIElement uIElement)
        {
            ((ParametreModeFacturationControl)uIElement).Look();
        }

        public void LookLitige(UIElement uIElement)
        {
            ((ParametreLitigeControl)uIElement).Look();
        }

        public void LookDomaine(UIElement uIElement)
        {
            ((ParametreDomaineControl)uIElement).Look();
        }

        public void LookTypeEntreprise(UIElement uIElement)
        {
            ((ParametreTypeEntrepriseControl)uIElement).Look();
        }

        public void LookContrat(UIElement uIElement)
        {
            ((ParametreContratControl)uIElement).Look();
        }

        public void LookDocTypeSalarie(UIElement uIElement)
        {
            ((ParametreDocTypeSalarieControl)uIElement).Look();
        }

        public void LookTypeReception(UIElement uIElement)
        {
            ((ParametreTypeReceptionControl)uIElement).Look();
        }

        public void LookPermis(UIElement uIElement)
        {
            ((ParametrePermisControl)uIElement).Look();
        }

        public void LookNiveauSecurite(UIElement uIElement)
        {
            ((ParametreNiveauSecuriteControl)uIElement).Look();
        }

        public void LookUtilisateur(UIElement uIElement)
        {
            ((ParametreUtilisateurControl)uIElement).Look();
        }

        public void LookTauxHoraire(UIElement uIElement)
        {
            ((ParametreTauxHoraireControl)uIElement).Look();
        }

        public void LookDepartement(UIElement uIElement)
        {
            ((ParametreDepartementControl)uIElement).Look();
        }

        public void LookDevise(UIElement uIElement)
        {
            ((ParametreDeviseControl)uIElement).Look();
        }

        public void LookFormation(UIElement uIElement)
        {
            ((ParametreFormationControl)uIElement).Look();
        }

        public void LookRegion(UIElement uIElement)
        {
            ((ParametreRegionControl)uIElement).Look();
        }

        public void LookConditionReglement(UIElement uIElement)
        {
            ((ParametreConditionReglementControl)uIElement).Look();
        }

        public void LookEntrepriseMere(UIElement uIElement, Entreprise_Mere entreprisemereToLook)
        {
            ((ParametreEntrepriseMereControl)uIElement).Look(entreprisemereToLook);
        }

        public void LookVersionType(UIElement uIElement)
        {
            ((ParametreVersionTypeControl)uIElement).Look();
        }

        public void LookCivilite(UIElement uIElement, Civilite civiliteToLook)
        {
            ((ParametreCiviliteControl)uIElement).Look(civiliteToLook);
        }

        #endregion

        private void _CommandLook_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this._BorderContent != null && this._BorderContent.Child != null)
            {
                UIElement uIElement = this._BorderContent.Child;
                if (this.securite.ControlsLookAuthorizedParameter.Contains(uIElement.ToString()))
                {
                    e.CanExecute = this.securite.VerificationDroitActionsCRUDParameters(uIElement.ToString(), "Look");
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region Lister

        #region bouton afficher civilite

        private void _CommandAfficherCivilites_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des civilites en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreCiviliteControl parametreCiviliteControl = new ParametreCiviliteControl();

            this._BorderContent.Child = parametreCiviliteControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Civilités.Background = ((App)App.Current).SaveFocusedBackground;
            this.Civilités.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherCivilites_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher permis

        private void _CommandAfficherPermis_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des permis en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametrePermisControl parametrePermisControl = new ParametrePermisControl();

            this._BorderContent.Child = parametrePermisControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Permis.Background = ((App)App.Current).SaveFocusedBackground;
            this.Permis.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherPermis_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametrePermisControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametrePermisControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher villes

        private void _CommandAfficherVilles_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des villes en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreVilleControl parametreVilleControl = new ParametreVilleControl();

            this._BorderContent.Child = parametreVilleControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Ville.Background = ((App)App.Current).SaveFocusedBackground;
            this.Ville.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherVilles_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreVilleControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreVilleControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }


        #endregion

        #region bouton afficher pays

        private void _CommandAfficherPays_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des pays en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametrePaysControl parametrePaysControl = new ParametrePaysControl();

            this._BorderContent.Child = parametrePaysControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Pays.Background = ((App)App.Current).SaveFocusedBackground;
            this.Pays.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherPays_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametrePaysControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametrePaysControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher habilitation

        private void _CommandAfficherHabilitation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des habilitations en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreHabilitationControl parametreHabilitationControl = new ParametreHabilitationControl();

            this._BorderContent.Child = parametreHabilitationControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Habilitation.Background = ((App)App.Current).SaveFocusedBackground;
            this.Habilitation.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherHabilitation_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreHabilitationControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreHabilitationControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }


        #endregion

        #region bouton afficher qualification

        private void _CommandAfficherQualification_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des qualifications en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreQualificationControl parametreQualificationControl = new ParametreQualificationControl();

            this._BorderContent.Child = parametreQualificationControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Qualification.Background = ((App)App.Current).SaveFocusedBackground;
            this.Qualification.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherQualification_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreQualificationControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreQualificationControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }


        #endregion

        #region bouton afficher diplomes

        private void _CommandAfficherDiplomes_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des diplômes en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreDiplomeControl parametreDiplomeControl = new ParametreDiplomeControl();

            this._BorderContent.Child = parametreDiplomeControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Diplome.Background = ((App)App.Current).SaveFocusedBackground;
            this.Diplome.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherDiplomes_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreDiplomeControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreDiplomeControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }


        #endregion

        #region bouton afficher formation

        private void _CommandAfficherFormation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des formations en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreFormationControl parametreFormationControl = new ParametreFormationControl();

            this._BorderContent.Child = parametreFormationControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Formation.Background = ((App)App.Current).SaveFocusedBackground;
            this.Formation.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherFormation_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreFormationControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreFormationControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }


        #endregion

        #region bouton afficher groupe

        private void _CommandAfficherGroupe_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des groupes en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreGroupeControl parametreGroupeControl = new ParametreGroupeControl();

            this._BorderContent.Child = parametreGroupeControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Groupe.Background = ((App)App.Current).SaveFocusedBackground;
            this.Groupe.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherGroupe_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher mode de facturation

        private void _CommandAfficherModeFacturation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des modes de facturation en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreModeFacturationControl parametreModeFacturationControl = new ParametreModeFacturationControl();

            this._BorderContent.Child = parametreModeFacturationControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.ModeFacturation.Background = ((App)App.Current).SaveFocusedBackground;
            this.ModeFacturation.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherModeFacturation_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreModeFacturationControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreModeFacturationControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher devise

        private void _CommandAfficherDevise_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des devises en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreDeviseControl parametreDeviseControl = new ParametreDeviseControl();

            this._BorderContent.Child = parametreDeviseControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Devise.Background = ((App)App.Current).SaveFocusedBackground;
            this.Devise.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherDevise_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreDeviseControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreDeviseControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher litige

        private void _CommandAfficherLitige_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des litiges en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreLitigeControl parametreLitigeControl = new ParametreLitigeControl();

            this._BorderContent.Child = parametreLitigeControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Litige.Background = ((App)App.Current).SaveFocusedBackground;
            this.Litige.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherLitige_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreLitigeControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreLitigeControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher domaine

        private void _CommandAfficherDomaine_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des domaines en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreDomaineControl parametreDomaineControl = new ParametreDomaineControl();

            this._BorderContent.Child = parametreDomaineControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Domaine.Background = ((App)App.Current).SaveFocusedBackground;
            this.Domaine.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherDomaine_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreDomaineControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreDomaineControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher activite

        private void _CommandAfficherActivite_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des activités en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreActiviteControl parametreActiviteControl = new ParametreActiviteControl();

            this._BorderContent.Child = parametreActiviteControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Activite.Background = ((App)App.Current).SaveFocusedBackground;
            this.Activite.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherActivite_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher type d'entreprise

        private void _CommandAfficherTypeEntreprise_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des types d'entreprise en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreTypeEntrepriseControl parametretypeentreprisecontrol = new ParametreTypeEntrepriseControl();

            this._BorderContent.Child = parametretypeentreprisecontrol;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.TypeEntreprise.Background = ((App)App.Current).SaveFocusedBackground;
            this.TypeEntreprise.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherTypeEntreprise_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeEntrepriseControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeEntrepriseControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher contrat

        private void _CommandAfficherContrat_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des contrats en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreContratControl parametreContratControl = new ParametreContratControl();

            this._BorderContent.Child = parametreContratControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Contrat.Background = ((App)App.Current).SaveFocusedBackground;
            this.Contrat.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherContrat_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreContratControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreContratControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher document type salarié

        private void _CommandAfficherDocTypeSalarie_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des documents type salarié en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreDocTypeSalarieControl parametreDocTypeSalarieControl = new ParametreDocTypeSalarieControl();

            this._BorderContent.Child = parametreDocTypeSalarieControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            //this.resetCouleurs();
            //this.DocTypeSalarie.Background = ((App)App.Current).SaveFocusedBackground;
            //this.DocTypeSalarie.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherDocTypeSalarie_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreDocTypeSalarieControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreDocTypeSalarieControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher contact service

        private void _CommandAfficherContactService_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des services en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreContactServiceControl parametreContactServiceControl = new ParametreContactServiceControl();

            this._BorderContent.Child = parametreContactServiceControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.ContactService.Background = ((App)App.Current).SaveFocusedBackground;
            this.ContactService.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherContactService_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher contact fonction

        private void _CommandAfficherContactFonction_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des fonctions en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreContactFonctionControl parametreContactFonctionControl = new ParametreContactFonctionControl();

            this._BorderContent.Child = parametreContactFonctionControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.ContactFonction.Background = ((App)App.Current).SaveFocusedBackground;
            this.ContactFonction.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherContactFonction_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher type de reception

        private void _CommandAfficherTypeReception_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des types de reception en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreTypeReceptionControl parametreTypeReceptionControl = new ParametreTypeReceptionControl();

            this._BorderContent.Child = parametreTypeReceptionControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.TypeReception.Background = ((App)App.Current).SaveFocusedBackground;
            this.TypeReception.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherTypeRecepetion_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeReceptionControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeReceptionControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher departement

        private void _CommandAfficherDepartement_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des departements  en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreDepartementControl parametreDepartementControl = new ParametreDepartementControl();

            this._BorderContent.Child = parametreDepartementControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Departement.Background = ((App)App.Current).SaveFocusedBackground;
            this.Departement.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherDepartement_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreDepartementControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreDepartementControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher region

        private void _CommandAfficherRegion_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des régions  en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreRegionControl parametreRegionControl = new ParametreRegionControl();

            this._BorderContent.Child = parametreRegionControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Region.Background = ((App)App.Current).SaveFocusedBackground;
            this.Region.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherRegion_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreRegionControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreRegionControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher condition de réglement

        private void _CommandAfficherConditionReglement_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des conditions de réglement en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreConditionReglementControl parametreConditionReglementControl = new ParametreConditionReglementControl();

            this._BorderContent.Child = parametreConditionReglementControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.ConditionReglement.Background = ((App)App.Current).SaveFocusedBackground;
            this.ConditionReglement.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherConditionReglement_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreConditionReglementControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreConditionReglementControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher niveau de sécurité

        private void _CommandAfficherNiveauSecurite_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des niveaux de sécurité en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreNiveauSecuriteControl parametreNiveauSecuriteControl = new ParametreNiveauSecuriteControl();

            this._BorderContent.Child = parametreNiveauSecuriteControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Niveau_Securite.Background = ((App)App.Current).SaveFocusedBackground;
            this.Niveau_Securite.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherNiveauSecurite_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreNiveauSecuriteControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreNiveauSecuriteControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher utilisateur

        private void _CommandAfficherUtilisateur_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des utilisateurs en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreUtilisateurControl parametreUtilisateurControl = new ParametreUtilisateurControl();

            this._BorderContent.Child = parametreUtilisateurControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Utilisateur.Background = ((App)App.Current).SaveFocusedBackground;
            this.Utilisateur.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherUtilisateur_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreUtilisateurControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreUtilisateurControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher entreprise mere

        private void _CommandAfficherEntrepriseMere_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des entreprises mères en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreEntrepriseMereControl parametreEntrepriseMereControl = new ParametreEntrepriseMereControl();

            this._BorderContent.Child = parametreEntrepriseMereControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.EntrepriseMere.Background = ((App)App.Current).SaveFocusedBackground;
            this.EntrepriseMere.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherEntrepriseMere_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher taux horaire

        private void _CommandAfficherTauxHoraire_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des taux horaires en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreTauxHoraireControl parametreTauxHoraireControl = new ParametreTauxHoraireControl();

            this._BorderContent.Child = parametreTauxHoraireControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.TauxHoraire.Background = ((App)App.Current).SaveFocusedBackground;
            this.TauxHoraire.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherTauxHoraire_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreTauxHoraireControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreTauxHoraireControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher version type

        private void _CommandAfficherVersionType_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des versions types en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreVersionTypeControl parametreVersionTypeControl = new ParametreVersionTypeControl();

            this._BorderContent.Child = parametreVersionTypeControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.VersionType.Background = ((App)App.Current).SaveFocusedBackground;
            this.VersionType.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherVersionType_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreVersionTypeControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreVersionTypeControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region boutons afficher banques

        private void _CommandAfficherBanques_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des banques en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreBanqueControl parametreBanqueControl = new ParametreBanqueControl();

            this._BorderContent.Child = parametreBanqueControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Banques.Background = ((App)App.Current).SaveFocusedBackground;
            this.Banques.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherBanques_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreBanqueControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreBanqueControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher type devis

        private void _CommandAfficherType_Devis_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des types de devis en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreTypeDevisControl parametreTypeDevisControl = new ParametreTypeDevisControl();

            this._BorderContent.Child = parametreTypeDevisControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Type_Devis.Background = ((App)App.Current).SaveFocusedBackground;
            this.Type_Devis.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherType_Devis_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeDevisControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeDevisControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher etat devis

        private void _CommandAfficherEtat_Devis_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des etats des devis en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreEtatDevisControl parametreEtatDevisControl = new ParametreEtatDevisControl();

            this._BorderContent.Child = parametreEtatDevisControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Etat_Devis.Background = ((App)App.Current).SaveFocusedBackground;
            this.Etat_Devis.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherEtat_Devis_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreEtatDevisControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreEtatDevisControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher exercice

        private void _CommandAfficherExercice_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des exercices en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreExerciceControl parametreExerciceControl = new ParametreExerciceControl();

            this._BorderContent.Child = parametreExerciceControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Exercice.Background = ((App)App.Current).SaveFocusedBackground;
            this.Exercice.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherExercice_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreExerciceControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreExerciceControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher outillage

        private void _CommandAfficherOutillage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des outillages en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreOutillageControl parametreOutillageControl = new ParametreOutillageControl();

            this._BorderContent.Child = parametreOutillageControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Outillage.Background = ((App)App.Current).SaveFocusedBackground;
            this.Outillage.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherOutillage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreOutillageControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreOutillageControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher type commande

        private void _CommandAfficherTypeCommande_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des types de commande en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreTypeCommandeControl parametreTypeCommandeControl = new ParametreTypeCommandeControl();

            this._BorderContent.Child = parametreTypeCommandeControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.TypeCommande.Background = ((App)App.Current).SaveFocusedBackground;
            this.TypeCommande.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherTypeCommande_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher plan comptable tva
        private void _CommandAfficherPlanComptableTVA_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des plan comptable TVA en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametrePlanComptableTvaControl parametrePlanComptableTvaControl = new ParametrePlanComptableTvaControl();

            this._BorderContent.Child = parametrePlanComptableTvaControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.PlanComptableTva.Background = ((App)App.Current).SaveFocusedBackground;
            this.PlanComptableTva.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherPlanComptableTVA_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableTvaControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableTvaControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher plan comptable imputation
        private void _CommandAfficherPlanComptableImputation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des plans comptable imputation en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametrePlanComptableImputationControl parametrePlanComptableImputationControl = new ParametrePlanComptableImputationControl();

            this._BorderContent.Child = parametrePlanComptableImputationControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.PlanComptableImputation.Background = ((App)App.Current).SaveFocusedBackground;
            this.PlanComptableImputation.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherPlanComptableImputation_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableImputationControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametrePlanComptableImputationControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher tva

        private void _CommandAfficherTva_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des Tva en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreTvaControl parametreTvaControl = new ParametreTvaControl();

            this._BorderContent.Child = parametreTvaControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Tva.Background = ((App)App.Current).SaveFocusedBackground;
            this.Tva.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherTva_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreTvaControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreTvaControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher statut

        private void _CommandAfficherStatut_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des statuts en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreStatutControl parametreStatutControl = new ParametreStatutControl();

            this._BorderContent.Child = parametreStatutControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Statut.Background = ((App)App.Current).SaveFocusedBackground;
            this.Statut.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherStatut_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreStatutControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreStatutControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region  bouton afficher tache atelier

        private void _CommandAfficherTacheAtelier_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des taches en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreTacheAtelierControl parametretacheatelierControl = new ParametreTacheAtelierControl();

            this._BorderContent.Child = parametretacheatelierControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.TacheAtelier.Background = ((App)App.Current).SaveFocusedBackground;
            this.TacheAtelier.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherTacheAtelier_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreTacheAtelierControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreTacheAtelierControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher motif refus conge


        private void _CommandAfficherMotifRefusConge_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des motif de refus en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreMotifRefusCongeControl parametremotifrefuscongeControl = new ParametreMotifRefusCongeControl();

            this._BorderContent.Child = parametremotifrefuscongeControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.RefusConge.Background = ((App)App.Current).SaveFocusedBackground;
            this.RefusConge.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherMotifRefusConge_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifRefusCongeControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifRefusCongeControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher motif demande de conges

        private void _CommandAfficherMotifDemandeConge_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des motif de demande en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreMotifDemandeCongeControl parametremotifdemandecongeControl = new ParametreMotifDemandeCongeControl();

            this._BorderContent.Child = parametremotifdemandecongeControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.DemandeConge.Background = ((App)App.Current).SaveFocusedBackground;
            this.DemandeConge.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherMotifDemandeConge_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifDemandeCongeControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifDemandeCongeControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher jour ferie

        private void _CommandAfficherJourFerie_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des jours feriés en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreJourFerieControl parametrejourferieControl = new ParametreJourFerieControl();

            this._BorderContent.Child = parametrejourferieControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.JourFerie.Background = ((App)App.Current).SaveFocusedBackground;
            this.JourFerie.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherJourFerie_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreJourFerieControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreJourFerieControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher piece administrative

        private void _CommandAfficherPieceAdministrative_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des pièces administratives en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametrePieceAdministrativeControl parametrePieceAdministrativeControl = new ParametrePieceAdministrativeControl();

            this._BorderContent.Child = parametrePieceAdministrativeControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.PieceAdministrative.Background = ((App)App.Current).SaveFocusedBackground;
            this.PieceAdministrative.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherPieceAdministrative_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametrePieceAdministrativeControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametrePieceAdministrativeControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher motif mission

        private void _CommandAfficherMotifMission_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des motifs mission en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreMotifMissionControl parametreMotifMissionControl = new ParametreMotifMissionControl();

            this._BorderContent.Child = parametreMotifMissionControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.MotifMission.Background = ((App)App.Current).SaveFocusedBackground;
            this.MotifMission.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherMotifMission_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifMissionControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreMotifMissionControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region Bouton afficher type remboursement

        private void _CommandAfficherTypeRemboursement_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des type remboursement en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreTypeRemboursementControl parametreTypeRemboursementControl = new ParametreTypeRemboursementControl();

            this._BorderContent.Child = parametreTypeRemboursementControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.TypeRemboursement.Background = ((App)App.Current).SaveFocusedBackground;
            this.TypeRemboursement.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherTypeRemboursement_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region Bouton afficher evenement remboursement

        private void _CommandAfficherEvenementRemboursement_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des evenements remboursement en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreEvenementRemboursementControl parametreEvenementRemboursementControl = new ParametreEvenementRemboursementControl();

            this._BorderContent.Child = parametreEvenementRemboursementControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.EvenementRemboursement.Background = ((App)App.Current).SaveFocusedBackground;
            this.EvenementRemboursement.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherEvenementRemboursement_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeRemboursementControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher distance ville

        private void _CommandAfficherDistanceVille_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des distance ville en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreDistanceVilleControl parametreDistanceVilleControl = new ParametreDistanceVilleControl();

            this._BorderContent.Child = parametreDistanceVilleControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.DistanceVille.Background = ((App)App.Current).SaveFocusedBackground;
            this.DistanceVille.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherDistanceVille_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher salle

        private void _CommandAfficherSalle_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des salles en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreSalleControl parametreSalleControl = new ParametreSalleControl();

            this._BorderContent.Child = parametreSalleControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Salle.Background = ((App)App.Current).SaveFocusedBackground;
            this.Salle.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherSalle_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreSalleControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreSalleControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher besoin reservation salle

        private void _CommandAfficherBesoinReservation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des besoins de réservation en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreBesoinReservationSalleControl parametreBesoinReservationSalleControl = new ParametreBesoinReservationSalleControl();

            this._BorderContent.Child = parametreBesoinReservationSalleControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Besoin_reservation.Background = ((App)App.Current).SaveFocusedBackground;
            this.Besoin_reservation.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherBesoinReservation_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreBesoinReservationSalleControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreBesoinReservationSalleControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher frais
        private void _CommandAfficherFrais_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des frais en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreTypeFraisControl parametrefraisControl = new ParametreTypeFraisControl();

            this._BorderContent.Child = parametrefraisControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Frais.Background = ((App)App.Current).SaveFocusedBackground;
            this.Frais.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherFrais_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeFraisControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeFraisControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher moyen reglement
        private void _CommandAfficherMoyenReglement_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des moyens de règlement en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreMoyenReglementControl parametremoyenReglementControl = new ParametreMoyenReglementControl();

            this._BorderContent.Child = parametremoyenReglementControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.MoyenReglement.Background = ((App)App.Current).SaveFocusedBackground;
            this.MoyenReglement.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherMoyenReglement_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreMoyenReglementControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreMoyenReglementControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #region bouton afficher article facture
        private void _CommandAfficherArticleFacture_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.textBlockMainWindow.Text = "Chargement des articles en cours ...";

            ((App)App.Current).refreshEDMX();
            ParametreArticleFactureControl parametrearticleFactureControl = new ParametreArticleFactureControl();

            this._BorderContent.Child = parametrearticleFactureControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.ArticleFacture.Background = ((App)App.Current).SaveFocusedBackground;
            this.ArticleFacture.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherArticleFacture_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorizedParameter.Contains("SitaffRibbon.Windows.ParametresUserControls.ParametreArticleFactureControl"))
            {
                if (this.securite.VerificationDroitListingParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreArticleFactureControl"))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        #endregion

        #endregion

        #region Fonctions

        #region Gestion du thread de chargement

        public void startThread()
        {
            this.Cursor = Cursors.Wait;
        }

        public void stopThread()
        {
            this.Cursor = ((App)App.Current)._mainCursor;
        }

        #endregion

        /// <summary>
        /// Fonction qui appelle la fenêtre d'erreur (lors de la levée d'une exception)
        /// </summary>
        /// <param name="ex">Exception relevée</param>
        private void ErreurSauvegardeBase(Exception ex, String messagePrincipal)
        {
            if (ex.InnerException != null)
            {
                ErrorMessageBox.Show(messagePrincipal + ". Message de l'exception : " + ex.Message, ex.InnerException.Message, "Erreur");
            }
            else
            {
                ErrorMessageBox.Show(messagePrincipal + ".", ex.Message, "Erreur");
            }
            this.progressBarMainWindow.IsIndeterminate = false;
            ((App)App.Current).refreshEDMX();
        }

        public void viderBorderContent()
        {
            try
            {
                if (((App)App.Current).personnalisation.styleVide != null)
                {
                    if (((App)App.Current).personnalisation.styleVide == "1")
                    {
                        this.viderBorderContentImage();
                    }
                    else if (((App)App.Current).personnalisation.styleVide == "2")
                    {
                        this.viderBorderContentRondsAnimes();
                    }
                    else if (((App)App.Current).personnalisation.styleVide == "3")
                    {
                        this.viderBorderContentCubeTournant();
                    }
                    else
                    {
                        this.viderBorderContentImage();
                    }
                }
                else
                {
                    this.viderBorderContentImage();
                }
            }
            catch (Exception)
            {
                this.viderBorderContentImage();
            }
        }

        #region styles

        public void viderBorderContentImage()
        {
            //Photo
            Image ImageBack = new Image();
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("Images/icone-programme-large.png", UriKind.Relative);
            bi3.EndInit();
            ImageBack.Source = bi3;
            ImageBack.Stretch = Stretch.None;
            ImageBack.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            ImageBack.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            this._BorderContent.Child = ImageBack;
        }

        public void viderBorderContentRondsAnimes()
        {
            UIElement uIElement = this._BorderContent.Child;

            //Bulles animées
            if (uIElement is VideControl)
            {

            }
            else
            {
                VideControl videControl = new VideControl();
                videControl.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                videControl.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

                this._BorderContent.Child = videControl;
            }
        }

        public void viderBorderContentCubeTournant()
        {
            UIElement uIElement = this._BorderContent.Child;

            //Cube tournant
            if (uIElement is CubeTournantControl)
            {

            }
            else
            {
                CubeTournantControl cubeTournantControl = new CubeTournantControl();
                cubeTournantControl.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                cubeTournantControl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;

                this._BorderContent.Child = cubeTournantControl;
            }
        }

        #endregion

        private void resetCouleurs()
        {
            //Gestion Personne
            this.Permis.Background = System.Windows.Media.Brushes.Transparent;
            this.Habilitation.Background = System.Windows.Media.Brushes.Transparent;
            this.Diplome.Background = System.Windows.Media.Brushes.Transparent;
            this.Formation.Background = System.Windows.Media.Brushes.Transparent;
            this.Contrat.Background = System.Windows.Media.Brushes.Transparent;
            this.Qualification.Background = System.Windows.Media.Brushes.Transparent;
            this.Outillage.Background = System.Windows.Media.Brushes.Transparent;
            this.Civilités.Background = System.Windows.Media.Brushes.Transparent;
            this.ContactService.Background = System.Windows.Media.Brushes.Transparent;
            this.ContactFonction.Background = System.Windows.Media.Brushes.Transparent;
            this.MotifMission.Background = System.Windows.Media.Brushes.Transparent;
            this.PieceAdministrative.Background = System.Windows.Media.Brushes.Transparent;                        
            //Adresse
            this.Ville.Background = System.Windows.Media.Brushes.Transparent;
            this.Departement.Background = System.Windows.Media.Brushes.Transparent;
            this.Region.Background = System.Windows.Media.Brushes.Transparent;
            this.Pays.Background = System.Windows.Media.Brushes.Transparent;
            //Entreprise
            this.Groupe.Background = System.Windows.Media.Brushes.Transparent;
            this.Domaine.Background = System.Windows.Media.Brushes.Transparent;
            this.Activite.Background = System.Windows.Media.Brushes.Transparent;
            this.TypeEntreprise.Background = System.Windows.Media.Brushes.Transparent;
            this.EntrepriseMere.Background = System.Windows.Media.Brushes.Transparent;
            this.Statut.Background = System.Windows.Media.Brushes.Transparent;
            //Appel d'offre
            this.TypeReception.Background = System.Windows.Media.Brushes.Transparent;
            this.ZoneARiques.Background = System.Windows.Media.Brushes.Transparent;
            //Affaire 
            this.ChapitreClause.Background = System.Windows.Media.Brushes.Transparent;
            this.VersionType.Background = System.Windows.Media.Brushes.Transparent;
            this.Type_Devis.Background = System.Windows.Media.Brushes.Transparent;
            this.Etat_Devis.Background = System.Windows.Media.Brushes.Transparent;
            //Compta
            this.Litige.Background = System.Windows.Media.Brushes.Transparent;
            this.ModeFacturation.Background = System.Windows.Media.Brushes.Transparent;
            this.Devise.Background = System.Windows.Media.Brushes.Transparent;
            this.Banques.Background = System.Windows.Media.Brushes.Transparent;
            this.ConditionReglement.Background = System.Windows.Media.Brushes.Transparent;
            this.PlanComptableTva.Background = System.Windows.Media.Brushes.Transparent;
            this.PlanComptableImputation.Background = System.Windows.Media.Brushes.Transparent;
            this.Tva.Background = System.Windows.Media.Brushes.Transparent;
            this.Frais.Background = System.Windows.Media.Brushes.Transparent;
            this.MoyenReglement.Background = System.Windows.Media.Brushes.Transparent;
            this.ArticleFacture.Background = System.Windows.Media.Brushes.Transparent;
            //User
            this.Niveau_Securite.Background = System.Windows.Media.Brushes.Transparent;
            this.Utilisateur.Background = System.Windows.Media.Brushes.Transparent;
            //Exercice
            this.Exercice.Background = System.Windows.Media.Brushes.Transparent;
            this.TauxHoraire.Background = System.Windows.Media.Brushes.Transparent;
            this.JourFerie.Background = System.Windows.Media.Brushes.Transparent;
            //Heure
            this.TacheAtelier.Background = System.Windows.Media.Brushes.Transparent;
            this.TypeRemboursement.Background = System.Windows.Media.Brushes.Transparent;
            this.EvenementRemboursement.Background = System.Windows.Media.Brushes.Transparent;
            this.DistanceVille.Background = System.Windows.Media.Brushes.Transparent;
            this.DemandeConge.Background = System.Windows.Media.Brushes.Transparent;
            this.RefusConge.Background = System.Windows.Media.Brushes.Transparent;
            //Technique
            this.TypeCommande.Background = System.Windows.Media.Brushes.Transparent;
            //Divers
            this.Besoin_reservation.Background = System.Windows.Media.Brushes.Transparent;
            this.Salle.Background = System.Windows.Media.Brushes.Transparent;


            //Gestion Personne
            this.Permis.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Habilitation.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Diplome.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Formation.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Contrat.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Qualification.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Outillage.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Civilités.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.ContactService.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.ContactFonction.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.MotifMission.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.PieceAdministrative.BorderBrush = System.Windows.Media.Brushes.Transparent;                        
            //Adresse
            this.Ville.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Departement.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Region.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Pays.BorderBrush = System.Windows.Media.Brushes.Transparent;
            //Entreprise
            this.Groupe.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Domaine.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Activite.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.TypeEntreprise.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.EntrepriseMere.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Statut.BorderBrush = System.Windows.Media.Brushes.Transparent;
            //Appel d'offre
            this.TypeReception.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.ZoneARiques.BorderBrush = System.Windows.Media.Brushes.Transparent;
            //Affaire 
            this.ChapitreClause.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.VersionType.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Type_Devis.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Etat_Devis.BorderBrush = System.Windows.Media.Brushes.Transparent;
            //Compta
            this.Litige.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.ModeFacturation.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Devise.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Banques.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.ConditionReglement.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.PlanComptableTva.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.PlanComptableImputation.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Tva.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Frais.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.ArticleFacture.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.MoyenReglement.BorderBrush = System.Windows.Media.Brushes.Transparent;
            //User
            this.Niveau_Securite.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Utilisateur.BorderBrush = System.Windows.Media.Brushes.Transparent;
            //Exercice
            this.Exercice.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.TauxHoraire.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.JourFerie.BorderBrush = System.Windows.Media.Brushes.Transparent;
            //Heure
            this.TacheAtelier.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.TypeRemboursement.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.EvenementRemboursement.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.DistanceVille.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.DemandeConge.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.RefusConge.BorderBrush = System.Windows.Media.Brushes.Transparent;
            //Technique
            this.TypeCommande.BorderBrush = System.Windows.Media.Brushes.Transparent;
            //Divers
            this.Besoin_reservation.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Salle.BorderBrush = System.Windows.Media.Brushes.Transparent;
        }

        #endregion

        #region Evenements

        /// <summary>
        /// Rafraichir la fenetre principale lorsque l'on change d'onglet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.viderBorderContent();
        }

        #region extension de la fonction quitter

        /// <summary>
        /// Lorsque l'on quitte l'application (alt+F4 ou croix ...)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            laMainWindow.progressBarMainWindow.IsIndeterminate = false;
            laMainWindow.textBlockMainWindow.Text = "Fenêtre des paramètres fermée ...";
        }

        #endregion

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #endregion
    }
}