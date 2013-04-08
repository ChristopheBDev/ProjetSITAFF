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
using Microsoft.Windows.Controls.Ribbon;
using SitaffRibbon.UserControls;
using SitaffRibbon.Windows;
/* 
 * Using pour utilisation des IObservableCollection (afin d'éviter de mettre
 * System.Collections.ObjectModel.IObservableCollection en entier)
 */
using System.Collections.ObjectModel;
using System.Threading;
using SitaffRibbon.Classes;
using System.Data.Objects;
using System.ComponentModel;
using SitaffRibbon.UserControls;

namespace SitaffRibbon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow, IDisposable
    {

        #region Variables

        //Thread myThread;
        public ParametresMain parametreMain = null;
        public Mutex _mutex = new Mutex();
        private Securite securite = new Securite();

        #endregion

        #region Constructeurs

        public MainWindow()
        {
            InitializeComponent();
            this.parametreMain = new ParametresMain(this);
        }

        #endregion

        #region Commandes

        #region filter

        private void _CommandFiltrage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            UIElement uIElement = this._BorderContent.Child;

            switch (uIElement.ToString())
            {
                case "SitaffRibbon.UserControls.ListeDaillyControl":
                    this.FiltreDailly(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeRetourChantierControl":
                    this.FiltreRetourChantier(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeProformaClientControl":
                    this.FiltreProformaClient(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeAffaireControl":
                    this.FiltreAffaire(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeOrdreMissionControl":
                    this.FiltreOrdreMission(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFraisControl":
                    this.FiltreFrais(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReservationSalleControl":
                    this.FiltreReservationSalle(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeResumeDevisControl":
                    this.FiltreResumeDevis(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeSortieAtelierControl":
                    this.FiltreSortieAtelier(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReglementClientControl":
                    this.FiltreReglementClient(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeAvanceControl":
                    this.FiltreReleveHeureForfait(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReleveHeureForfaitControl":
                    this.FiltreReleveHeureForfait(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeBonLivraisonControl":
                    this.FiltreBL(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeCongesControl":
                    this.FiltreConge(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeCommandeFournisseurControl":
                    this.FiltreCommandeFournisseur(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReveleHeureAtelierControl":
                    this.FiltreReleveHeureAtelier(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeDAOControl":
                    this.FiltreDAO(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeRegieControl":
                    this.FiltreRegie(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeEntreprisesControl":
                    this.FiltreEntreprise(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeContactsControl":
                    this.FiltreContact(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeSalarieControl":
                    break;
                case "SitaffRibbon.UserControls.ListeDevisControl":
                    this.FiltreDevis(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFactureControl":
                    this.FiltreFacture(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFactureProformaControl":
                    break;
                case "SitaffRibbon.UserControls.ListeFactureFournisseurControl":
                    this.FiltreFactureFournisseur(uIElement);
                    break;
                default:
                    break;
            }
        }

        #region Fonctions des filtres

        public void FiltreDailly(UIElement uIElement)
        {
            try
            {
                ((ListeDaillyControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreRetourChantier(UIElement uIElement)
        {
            try
            {
                ((ListeRetourChantierControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreProformaClient(UIElement uIElement)
        {
            try
            {
                ((ListeProformaClientControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreAffaire(UIElement uIElement)
        {
            try
            {
                ((ListeAffaireControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreOrdreMission(UIElement uIElement)
        {
            try
            {
                ((ListeOrdreMissionControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreFrais(UIElement uIElement)
        {
            try
            {
                ((ListeFraisControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreReservationSalle(UIElement uIElement)
        {
            try
            {
                ((ListeReservationSalleControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreResumeDevis(UIElement uIElement)
        {
            try
            {
                ((ListeResumeDevisControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreSortieAtelier(UIElement uIElement)
        {
            try
            {
                ((ListeSortieAtelierControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreReglementClient(UIElement uIElement)
        {
            //try
            //{
            //    ((ListeReglementClientControl)uIElement).AfficherMasquer();
            //}
            //catch (Exception) { }
        }

        public void FiltreAvance(UIElement uIElement)
        {
            try
            {
                ((ListeAvanceControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreConge(UIElement uIElement)
        {
            try
            {
                ((ListeCongesControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreRegie(UIElement uIElement)
        {
            try
            {
                ((ListeRegieControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreEntreprise(UIElement uIElement)
        {
            try
            {
                ((ListeEntreprisesControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreDevis(UIElement uIElement)
        {
            try
            {
                ((ListeDevisControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreReleveHeureAtelier(UIElement uIElement)
        {
            try
            {
                ((ListeReveleHeureAtelierControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreContact(UIElement uIElement)
        {
            try
            {
                ((ListeContactsControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreReleveHeureForfait(UIElement uIElement)
        {
            try
            {
                ((ListeReleveHeureForfaitControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreFactureFournisseur(UIElement uIElement)
        {
            try
            {
                ((ListeFactureFournisseurControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreFacture(UIElement uIElement)
        {
            try
            {
                ((ListeFactureControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreDAO(UIElement uIElement)
        {
            try
            {
                ((ListeDAOControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreBL(UIElement uIElement)
        {
            try
            {
                ((ListeBonLivraisonControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        public void FiltreCommandeFournisseur(UIElement uIElement)
        {
            try
            {
                ((ListeCommandeFournisseurControl)uIElement).AfficherMasquer();
            }
            catch (Exception) { }
        }

        #endregion

        private void _CommandFiltrage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region bouton afficher

        public void _CommandLook_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            UIElement uIElement = this._BorderContent.Child;

            switch (uIElement.ToString())
            {
                case "SitaffRibbon.UserControls.ListeDaillyControl":
                    this.LookDailly(uIElement, null);
                    break;
                case "SitaffRibbon.UserControls.ListeRetourChantierControl":
                    this.LookRetourChantier(uIElement, null);
                    break;
                case "SitaffRibbon.UserControls.ListeProformaClientControl":
                    this.LookProformaClient(uIElement, null);
                    break;
                case "SitaffRibbon.UserControls.ListeFraisControl":
                    this.LookFrais(uIElement, null);
                    break;
                case "SitaffRibbon.UserControls.ListeReservationSalleControl":
                    this.LookReservationSalle(uIElement, null);
                    break;
                case "SitaffRibbon.UserControls.ListeSortieAtelierControl":
                    this.LookSortieAtelier(uIElement, null);
                    break;
                case "SitaffRibbon.UserControls.ListeOrdreMissionControl":
                    this.LookOrdreMission(uIElement, null);
                    break;
                case "SitaffRibbon.UserControls.ListeReglementClientControl":
                    this.LookReglementClient(uIElement, null);
                    break;
                case "SitaffRibbon.UserControls.ListeAvanceControl":
                    this.LookAvance(uIElement, null);
                    break;
                case "SitaffRibbon.UserControls.ListeReleveHeureForfaitControl":
                    this.LookReleveHeureForfait(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeBonLivraisonControl":
                    this.LookBonLivraison(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeCongesControl":
                    this.LookConges(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeCommandeFournisseurControl":
                    this.LookCommandeFournisseur(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReveleHeureAtelierControl":
                    this.LookReveleHeureAtelier(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeDAOControl":
                    this.LookDAO(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeRegieControl":
                    this.LookRegie(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeEntreprisesControl":
                    this.LookEntreprises(uIElement, null);
                    break;
                case "SitaffRibbon.UserControls.ListeContactsControl":
                    this.LookContacts(uIElement, null);
                    break;
                case "SitaffRibbon.UserControls.ListeSalarieControl":
                    this.LookSalarie(uIElement, null);
                    break;
                case "SitaffRibbon.UserControls.ListeDevisControl":
                    this.LookDevis(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFactureControl":
                    this.LookFacture(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFactureProformaControl":
                    this.LookFactureProforma(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFactureFournisseurControl":
                    this.LookFactureFournisseur(uIElement);
                    break;
                default:
                    MessageBox.Show("Vous n'avez normalement pas à cliquer sur ce bouton, il devrait être grisé. Si vous recevez cette erreur, merci d'envoyer un mail à l'administrateur avec une image de l'erreur, le nom du bouton ainsi que le nom de votre utilisateur.");
                    break;
            }

            this.Cursor = ((App)App.Current)._mainCursor;
        }

        #region Fonctions des looks

        public void LookDailly(UIElement uIElement, Dailly daillyToLook)
        {
            ((ListeDaillyControl)uIElement).Look(daillyToLook);
        }

        public void LookRetourChantier(UIElement uIElement, Retour_Chantier retToLook)
        {
            ((ListeRetourChantierControl)uIElement).Look(retToLook);
        }

        public void LookProformaClient(UIElement uIElement, Facture facToLook)
        {
            ((ListeProformaClientControl)uIElement).Look();
        }

        public void LookFrais(UIElement uIElement, Frais FraisToLook)
        {
            ((ListeFraisControl)uIElement).Look(FraisToLook);
        }

        public void LookReservationSalle(UIElement uIElement, Reservation_Salle reservationToLook)
        {
            ((ListeReservationSalleControl)uIElement).Look(reservationToLook);
        }

        public void LookSortieAtelier(UIElement uIElement, Sortie_Atelier sortieToLook)
        {
            ((ListeSortieAtelierControl)uIElement).Look(sortieToLook);
        }

        public void LookOrdreMission(UIElement uIElement, Ordre_Mission ordreToLook)
        {
            ((ListeOrdreMissionControl)uIElement).Look(ordreToLook);
        }

        public void LookBonLivraison(UIElement uIElement)
        {
            ((ListeBonLivraisonControl)uIElement).Look(null);
        }

        public void LookFactureFournisseur(UIElement uIElement)
        {
            ((ListeFactureFournisseurControl)uIElement).Look();
        }

        public void LookFactureProforma(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Affichage d'une facture proforma en cours ...");
            Facture_Proforma fact = ((ListeFactureProformaControl)uIElement).Look();
            this.progressBarMainWindow.IsIndeterminate = false;
            this.changementTexteStatusBar("Affichage d'une facture proforma terminé.");
        }

        public void LookAppelOffres(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Affichage d'un appel d'offre en cours ...");
            Appel_Offre appeloffre = ((ListeAppelOffresControl)uIElement).Look();
            this.progressBarMainWindow.IsIndeterminate = false;
            this.changementTexteStatusBar("Affichage d'un appel d'offre terminé.");
        }

        public void LookSalarie(UIElement uIElement, Personne salarieToLook)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Affichage d'un salarié en cours ...");
            Personne salarie = ((ListeSalarieControl)uIElement).Look(salarieToLook);
            this.progressBarMainWindow.IsIndeterminate = false;
            this.changementTexteStatusBar("Affichage d'un salarié terminé.");
        }

        public void LookDevis(UIElement uIElement)
        {
            ((ListeDevisControl)uIElement).Look(null);
        }

        public void LookContacts(UIElement uIElement, Personne personneToLook)
        {
            ((ListeContactsControl)uIElement).Look(personneToLook);
        }

        public void LookReglementClient(UIElement uIElement, Reglement_Client reglementClientToLook)
        {
            ((ListeReglementClientControl)uIElement).Look(reglementClientToLook);
        }

        public void LookAvance(UIElement uIElement, Avance avanceToLook)
        {
            ((ListeAvanceControl)uIElement).Look(avanceToLook);
        }

        public void LookEntreprises(UIElement uIElement, Entreprise entrepriseToLook)
        {
            ((ListeEntreprisesControl)uIElement).Look(entrepriseToLook);
        }

        public void LookRegie(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Affichage d'une régie en cours ...");
            Regie regie = ((ListeRegieControl)uIElement).Look();
            this.progressBarMainWindow.IsIndeterminate = false;
            this.changementTexteStatusBar("Affichage d'une régie terminé.");
        }

        public void LookDAO(UIElement uIElement)
        {
            ((ListeDAOControl)uIElement).Look();
        }

        public void LookReveleHeureAtelier(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Affichage d'un relevé d'atelier en cours ...");
            Releve_Heure_Atelier rha = ((ListeReveleHeureAtelierControl)uIElement).Look();
            this.progressBarMainWindow.IsIndeterminate = false;
            this.changementTexteStatusBar("Affichage d'un relevé d'atelier terminé.");
        }

        public void LookCommandeFournisseur(UIElement uIElement)
        {
            ((ListeCommandeFournisseurControl)uIElement).Look(null);
        }

        public void LookConges(UIElement uIElement)
        {
            ((ListeCongesControl)uIElement).Look(null);
        }

        public void LookReleveHeureForfait(UIElement uIElement)
        {
            ((ListeReleveHeureForfaitControl)uIElement).Look();
        }

        public void LookFacture(UIElement uIElement)
        {
            ((ListeFactureControl)uIElement).Look();
        }

        #endregion

        private void _CommandLook_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this._BorderContent != null && this._BorderContent.Child != null)
            {
                UIElement uIElement = this._BorderContent.Child;
                if (this.securite.ControlsLookAuthorized.Contains(uIElement.ToString()))
                {
                    e.CanExecute = this.securite.VerificationDroitActionsCRUD(uIElement.ToString(), "Look");
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

        #region bouton ajouter

        private void _CommandAdd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            UIElement uIElement = this._BorderContent.Child;

            switch (uIElement.ToString())
            {
                case "SitaffRibbon.UserControls.ListeDaillyControl":
                    this.AddDailly(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeRetourChantierControl":
                    this.AddRetourChantier(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeProformaClientControl":
                    this.AddProformaClient(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFraisControl":
                    this.AddFrais(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReservationSalleControl":
                    this.AddReservationSalle(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeSortieAtelierControl":
                    this.AddSortieAtelier(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeOrdreMissionControl":
                    this.AddOrdreMission(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReglementClientControl":
                    this.AddReglementClient(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReleveHeureForfaitControl":
                    this.AddReleveHeureForfait(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeBonLivraisonControl":
                    this.AddBonLivraison(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeAvanceControl":
                    this.AddAvance(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeCongesControl":
                    this.AddConges(uIElement, false);
                    break;
                case "SitaffRibbon.UserControls.ListeCommandeFournisseurControl":
                    this.AddCommandeFournisseur(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReveleHeureAtelierControl":
                    this.AddReleveHeureAtelier(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeDAOControl":
                    this.AddDAO(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeRegieControl":
                    this.AddRegie(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeAffaireControl":
                    this.AddAffaire(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeEntreprisesControl":
                    this.AddEntreprises(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeContactsControl":
                    this.AddContacts(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeSalarieControl":
                    this.AddSalarie(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeDevisControl":
                    this.AddDevis(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFactureControl":
                    this.AddFacture(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFactureProformaControl":
                    this.AddFactureProforma(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFactureFournisseurControl":
                    this.AddFactureFournisseur(uIElement);
                    break;
                default:
                    MessageBox.Show("Vous n'avez normalement pas à cliquer sur ce bouton, il devrait être grisé. Si vous recevez cette erreur, merci d'envoyer un mail à l'administrateur avec une image de l'erreur, le nom du bouton ainsi que le nom de votre utilisateur.");
                    break;
            }

            this.Cursor = ((App)App.Current)._mainCursor;
        }

        #region Fonctions des ajouts

        public Dailly AddDailly(UIElement uIElement)
        {
            Dailly dailly = ((ListeDaillyControl)uIElement).Add();

            if (dailly != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToDailly(dailly);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeDaillyControl)uIElement).MiseAJourEtat("Ajout", dailly);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un dailly");
                }
            }

            return dailly;
        }

        public Retour_Chantier AddRetourChantier(UIElement uIElement)
        {
            Retour_Chantier retour_chantier = ((ListeRetourChantierControl)uIElement).Add();

            if (retour_chantier != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToRetour_Chantier(retour_chantier);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeRetourChantierControl)uIElement).MiseAJourEtat("Ajout", retour_chantier);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un retour chantier");
                }
            }

            return retour_chantier;
        }

        public Facture AddProformaClient(UIElement uIElement)
        {
            Facture facture = ((ListeProformaClientControl)uIElement).Add();

            if (facture != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToFacture(facture);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeProformaClientControl)uIElement).MiseAJourEtat("Ajout", facture);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une facture proforma client");
                }
            }

            return facture;
        }

        public Frais AddFrais(UIElement uIElement)
        {
            Frais frais = ((ListeFraisControl)uIElement).Add();

            if (frais != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToFrais(frais);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeFraisControl)uIElement).MiseAJourEtat("Ajout", frais);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout de frais");
                }
            }

            return frais;
        }

        public Reservation_Salle AddReservationSalle(UIElement uIElement)
        {
            Reservation_Salle reservation_salle = ((ListeReservationSalleControl)uIElement).Add();

            if (reservation_salle != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToReservation_Salle(reservation_salle);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeReservationSalleControl)uIElement).MiseAJourEtat("Ajout", reservation_salle);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle réservation salle");
                }
            }

            return reservation_salle;
        }

        public Sortie_Atelier AddSortieAtelier(UIElement uIElement)
        {
            Sortie_Atelier sa = ((ListeSortieAtelierControl)uIElement).Add();

            if (sa != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToSortie_Atelier(sa);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeSortieAtelierControl)uIElement).MiseAJourEtat("Ajout", sa);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle sortie atelier");
                }
            }

            return sa;
        }

        public Ordre_Mission AddOrdreMission(UIElement uIElement)
        {
            Ordre_Mission om = ((ListeOrdreMissionControl)uIElement).Add();

            if (om != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToOrdre_Mission(om);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeOrdreMissionControl)uIElement).MiseAJourEtat("Ajout", om);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouvel ordre de mission");
                }
            }

            return om;
        }

        public Reglement_Client AddReglementClient(UIElement uIElement)
        {
			Reglement_Client rc = ((ListeReglementClientControl)uIElement).Add();

			if (rc != null)
			{
				try
				{
					((App)App.Current).mySitaffEntities.AddToReglement_Client(rc);
					((App)App.Current).mySitaffEntities.SaveChanges();
					((ListeReglementClientControl)uIElement).MiseAJourEtat("Ajout", rc);
				}
				catch (Exception ex)
				{
					this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouveau réglement client");
				}
			}

			return rc;
        }

        public Avance AddAvance(UIElement uIElement)
        {
            Avance ava = ((ListeAvanceControl)uIElement).Add();

            if (ava != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToAvance(ava);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeAvanceControl)uIElement).MiseAJourEtat("Ajout", ava);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle avance");
                }
            }

            return ava;
        }

        public Bon_Livraison AddBonLivraison(UIElement uIElement)
        {
            Bon_Livraison bl = ((ListeBonLivraisonControl)uIElement).Add();

            if (bl != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToBon_Livraison(bl);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeBonLivraisonControl)uIElement).MiseAJourEtat("Ajout", bl);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau BL");
                }
            }

            return bl;
        }

        public Devis AddDevis(UIElement uIElement)
        {
            Devis dev = ((ListeDevisControl)uIElement).Add();

            if (dev != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToDevis(dev);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeDevisControl)uIElement).MiseAJourEtat("Ajout", dev);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau devis");
                }
            }

            return dev;
        }

        public Personne AddSalarie(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Ajout d'un salarié en cours ...");
            Personne salarie = ((ListeSalarieControl)uIElement).Add();

            if (salarie != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToPersonne(salarie);
                    ((ListeSalarieControl)uIElement).listSalarie.Add(salarie);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Ajout d'un salarié dénommée : '" + salarie.fullname + "' effectué avec succès.");
                }
                catch (Exception ex)
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Une errreur s'est produite lors de l'ajout d'un salarié. Pardonnez-nous du dérangement. Contactez l'administrateur du logiciel pour plus d'informations.");
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau salarié");
                }
            }
            else
            {
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Ajout d'un salarié annulé.");
            }
            try
            {
                ((ListeSalarieControl)uIElement).listSalarie.OrderBy(sal => sal.Nom);
            }
            catch (Exception) { }

            return salarie;
        }

        public Personne AddContacts(UIElement uIElement)
        {
            Personne contact = ((ListeContactsControl)uIElement).Add();

            if (contact != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToPersonne(contact);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeContactsControl)uIElement).MiseAJourEtat("Ajout", contact);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau contact");
                }
            }

            return contact;
        }

        public Entreprise AddEntreprises(UIElement uIElement)
        {
            Entreprise entreprise = ((ListeEntreprisesControl)uIElement).Add();

            if (entreprise != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToEntreprise(entreprise);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeEntreprisesControl)uIElement).MiseAJourEtat("Ajout", entreprise);
                }
                catch (Exception ex)
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle entreprise");
                }
            }

            return entreprise;
        }

        public Affaire AddAffaire(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Ajout d'une affaire en cours ...");
            Affaire affaire = ((ListeAffaireControl)uIElement).Add();

            if (affaire != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToAffaire(affaire);
                    ((ListeAffaireControl)uIElement).listAffaire.Add(affaire);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Ajout d'une affaire numero '" + affaire.Numero + "' effectué avec succès.");
                }
                catch (Exception ex)
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Une errreur s'est produite lors de l'ajout d'une affaire. Pardonnez-nous du dérangement. Contactez l'administrateur du logiciel pour plus d'informations.");
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle affaire");
                }
            }
            else
            {
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Ajout d'une affaire annulé.");
            }
            try
            {
                ((ListeAffaireControl)uIElement).listAffaire = new ObservableCollection<Affaire>(((ListeAffaireControl)uIElement).listAffaire.OrderBy(aff => aff.Numero));
            }
            catch (Exception) { }

            return affaire;
        }

        public Regie AddRegie(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Ajout d'une régie en cours ...");
            Regie regie = ((ListeRegieControl)uIElement).Add();

            if (regie != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToRegie(regie);
                    ((ListeRegieControl)uIElement).listRegie.Add(regie);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Ajout d'une régie numero '" + regie.Numero + "' effectué avec succès.");
                }
                catch (Exception ex)
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Une errreur s'est produite lors de l'ajout d'une régie. Pardonnez-nous du dérangement. Contactez l'administrateur du logiciel pour plus d'informations.");
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle régie");
                }
            }
            else
            {
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Ajout d'une régie annulé.");
            }
            try
            {
                ((ListeRegieControl)uIElement).listRegie = new ObservableCollection<Regie>(((ListeRegieControl)uIElement).listRegie.OrderBy(reg => reg.Numero));
            }
            catch (Exception) { }

            return regie;
        }

        public Facture_Proforma AddFactureProforma(UIElement uIElement)
        {
            Facture_Proforma fac = ((ListeFactureProformaControl)uIElement).Add();

            if (fac != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToFacture_Proforma(fac);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeFactureProformaControl)uIElement).MiseAJourEtat("Ajout", fac);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle facture proforma");
                }
            }

            return fac;
        }

        public Facture_Fournisseur AddFactureFournisseur(UIElement uIElement)
        {
            Facture_Fournisseur fac = ((ListeFactureFournisseurControl)uIElement).Add();

            if (fac != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToFacture_Fournisseur(fac);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeFactureFournisseurControl)uIElement).MiseAJourEtat("Ajout", fac);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle facture fournisseur");
                }
            }

            return fac;
        }

        public DAO AddDAO(UIElement uIElement)
        {
            DAO dao = ((ListeDAOControl)uIElement).Add();

            if (dao != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToDAO(dao);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeDAOControl)uIElement).MiseAJourEtat("Ajout", dao);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une nouvelle DAO");
                }
            }

            return dao;
        }

        public Releve_Heure_Atelier AddReleveHeureAtelier(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Ajout d'un relevé d'atelier en cours ...");
            Releve_Heure_Atelier rha = ((ListeReveleHeureAtelierControl)uIElement).Add();

            if (rha != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToReleve_Heure_Atelier(rha);
                    ((ListeReveleHeureAtelierControl)uIElement).listReleve.Add(rha);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Ajout du relevé d'atelier de '" + rha.Salarie1.Personne.fullname + "' sur la semaine n° '" + rha.NumeroSemaine + "' de l'année '" + rha.Date_Debut.Value.Year + "' effectué avec succès.");
                }
                catch (Exception ex)
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Une errreur s'est produite lors de l'ajout d'un relevé d'atelier. Pardonnez-nous du dérangement. Contactez l'administrateur du logiciel pour plus d'informations.");
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau relevé d'atelier");
                }
            }
            else
            {
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Ajout d'un relevé d'atelier annulé.");
            }
            try
            {
                ((ListeReveleHeureAtelierControl)uIElement).listReleve = new ObservableCollection<Releve_Heure_Atelier>(((App)App.Current).mySitaffEntities.Releve_Heure_Atelier.OrderByDescending(rhaz => rhaz.Date_Debut).ThenBy(rhaz => rhaz.Salarie1.Personne.Nom));
            }
            catch (Exception) { }

            return rha;

        }

        public Commande_Fournisseur AddCommandeFournisseur(UIElement uIElement)
        {
            Commande_Fournisseur commande_fournisseur = ((ListeCommandeFournisseurControl)uIElement).Add();

            if (commande_fournisseur != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToCommande_Fournisseur(commande_fournisseur);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeCommandeFournisseurControl)uIElement).MiseAJourEtat("Ajout", commande_fournisseur);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une commande fournisseur");
                }
            }

            return commande_fournisseur;
        }

        public Conge AddConges(UIElement uIElement, bool verrouillerSalarie)
        {
            Conge con = ((ListeCongesControl)uIElement).Add(verrouillerSalarie);

            if (con != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToConge(con);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeCongesControl)uIElement).MiseAJourEtat("Ajout", con);
                    try
                    {
                        this.EnvoiMailAuxRepondeursConge(con);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Votre demande de congé a bien été enregistrée mais l'envoi de mails aux personnes qui doivent y répondre à échoué. Si vous souhaitez que la réponse à la demande soit plus rapide, n'hésitez pas à en faire part à votre supérieur hiérarchique", "Echec de l'envoi de mail", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un nouveau congé");
                }
            }

            return con;
        }

        public Releve_Heure_Forfait AddReleveHeureForfait(UIElement uIElement)
        {
            Releve_Heure_Forfait rhf = ((ListeReleveHeureForfaitControl)uIElement).Add();

            if (rhf != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToReleve_Heure_Forfait(rhf);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeReleveHeureForfaitControl)uIElement).MiseAJourEtat("Ajout", rhf);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'un relevé hebdomadaire");
                }
            }

            return rhf;
        }

        public Facture AddFacture(UIElement uIElement)
        {
            Facture f = ((ListeFactureControl)uIElement).Add();

            if (f != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.AddToFacture(f);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeFactureControl)uIElement).MiseAJourEtat("Ajout", f);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de l'ajout d'une facture");
                }
            }

            return f;
        }

        #endregion

        private void _CommandAdd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this._BorderContent != null && this._BorderContent.Child != null)
            {
                UIElement uIElement = this._BorderContent.Child;
                if (this.securite.ControlsAddAuthorized.Contains(uIElement.ToString()))
                {
                    e.CanExecute = this.securite.VerificationDroitActionsCRUD(uIElement.ToString(), "Add");
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

        #region bouton modifier
        private void _CommandUpdate_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            UIElement uIElement = this._BorderContent.Child;

            switch (uIElement.ToString())
            {
                case "SitaffRibbon.UserControls.ListeDaillyControl":
                    this.UpdateDailly(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeRetourChantierControl":
                    this.UpdateRetourChantier(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeProformaClientControl":
                    this.UpdateProformaClient(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFraisControl":
                    this.UpdateFrais(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReservationSalleControl":
                    this.UpdateReservationSalle(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeSortieAtelierControl":
                    this.UpdateSortieAtelier(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeOrdreMissionControl":
                    this.UpdateOrdreMission(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReglementClientControl":
                    this.UpdateReglementClient(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReleveHeureForfaitControl":
                    this.UpdateReleveHeureForfait(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeBonLivraisonControl":
                    this.UpdateBonLivraison(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeAvanceControl":
                    this.UpdateAvance(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeCongesControl":
                    this.UpdateConges(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeCommandeFournisseurControl":
                    this.UpdateCommandeFournisseur(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReveleHeureAtelierControl":
                    this.UpdateReveleHeureAtelier(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeDAOControl":
                    this.UpdateDAO(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeRegieControl":
                    this.UpdateRegie(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeAffaireControl":
                    this.UpdateAffaire(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeEntreprisesControl":
                    this.UpdateEntreprises(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeContactsControl":
                    this.UpdateContacts(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeSalarieControl":
                    this.UpdateSalarie(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeDevisControl":
                    this.UpdateDevis(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFactureControl":
                    this.UpdateFacture(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFactureProformaControl":
                    this.UpdateFactureProforma(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFactureFournisseurControl":
                    this.UpdateFactureFournisseur(uIElement);
                    break;
                default:
                    MessageBox.Show("Vous n'avez normalement pas à cliquer sur ce bouton, il devrait être grisé. Si vous recevez cette erreur, merci d'envoyer un mail à l'administrateur avec une image de l'erreur, le nom du bouton ainsi que le nom de votre utilisateur.");
                    break;
            }

            this.Cursor = ((App)App.Current)._mainCursor;
        }

        #region Fonctions des modifications

        private void UpdateDailly(UIElement uIElement)
        {
            Dailly dailly = ((ListeDaillyControl)uIElement).Open();

            if (dailly != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeDaillyControl)uIElement).MiseAJourEtat("Modification", dailly);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un dailly");
                }
            }
        }

        private void UpdateRetourChantier(UIElement uIElement)
        {
            Retour_Chantier retour_chantier = ((ListeRetourChantierControl)uIElement).Open();

            if (retour_chantier != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeRetourChantierControl)uIElement).MiseAJourEtat("Modification", retour_chantier);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un retour chantier");
                }
            }
        }

        private void UpdateProformaClient(UIElement uIElement)
        {
            Facture facture = ((ListeProformaClientControl)uIElement).Open();

            if (facture != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeProformaClientControl)uIElement).MiseAJourEtat("Modification", facture);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une facture proforma client");
                }
            }
        }

        private void UpdateFrais(UIElement uIElement)
        {
            Frais frais = ((ListeFraisControl)uIElement).Open();

            if (frais != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeFraisControl)uIElement).MiseAJourEtat("Modification", frais);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification de frais");
                }
            }
        }

        private void UpdateReservationSalle(UIElement uIElement)
        {
            Reservation_Salle reservation_salle = ((ListeReservationSalleControl)uIElement).Open();

            if (reservation_salle != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeReservationSalleControl)uIElement).MiseAJourEtat("Modification", reservation_salle);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une réservation salle");
                }
            }
        }

        private void UpdateSortieAtelier(UIElement uIElement)
        {
            Sortie_Atelier sa = ((ListeSortieAtelierControl)uIElement).Open();

            if (sa != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeSortieAtelierControl)uIElement).MiseAJourEtat("Modification", sa);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une sortie atelier");
                }
            }
        }

        private void UpdateOrdreMission(UIElement uIElement)
        {
            Ordre_Mission om = ((ListeOrdreMissionControl)uIElement).Open();

            if (om != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeOrdreMissionControl)uIElement).MiseAJourEtat("Modification", om);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un ordre mission");
                }
            }
        }

        private void UpdateReglementClient(UIElement uIElement)
        {
			Reglement_Client rc = ((ListeReglementClientControl)uIElement).Open();

			if (rc != null)
			{
				try
				{
					((App)App.Current).mySitaffEntities.SaveChanges();
					((ListeReglementClientControl)uIElement).MiseAJourEtat("Modification", rc);
				}
				catch (Exception ex)
				{
					this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un réglement client");
				}
			}
        }

        private void UpdateAvance(UIElement uIElement)
        {
            Avance ava = ((ListeAvanceControl)uIElement).Open();

            if (ava != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeAvanceControl)uIElement).MiseAJourEtat("Modification", ava);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une avance");
                }
            }
        }

        private void UpdateBonLivraison(UIElement uIElement)
        {
            Bon_Livraison bl = ((ListeBonLivraisonControl)uIElement).Open();

            if (bl != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeBonLivraisonControl)uIElement).MiseAJourEtat("Modification", bl);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un BL");
                }
            }
        }

        private void UpdateAppelOffres(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Modification d'un appel d'offre en cours ...");
            Appel_Offre appeloffre = ((ListeAppelOffresControl)uIElement).Open();

            if (appeloffre != null)
            {
                try
                {
                    //((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, entreprise);
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Modification de l'appel d'offre dénommée : '" + appeloffre + "' effectuée avec succès.");
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                }
                catch (Exception ex)
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Une errreur s'est produite lors de la modification d'un appel d'offre. Pardonnez-nous du dérangement. Contactez l'administrateur du logiciel pour plus d'informations.");
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un appel offre");
                }
            }
            else
            {
                if (((ListeAppelOffresControl)this._BorderContent.Child)._DataGridMain.SelectedItems.Count == 1)
                {
                    ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Appel_Offre)(((ListeAppelOffresControl)this._BorderContent.Child)._DataGridMain.SelectedItem));
                }
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Modification d'un appel offre annulée.");
            }
            try
            {
                ((ListeAppelOffresControl)uIElement).AppelOffres.OrderBy(ao => ao.Date_Reception);
                ((ListeAppelOffresControl)this._BorderContent.Child)._DataGridMain.Items.Refresh();
            }
            catch (Exception) { }
        }

        private void UpdateSalarie(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Modification d'un salarié en cours ...");
            Personne salarie = ((ListeSalarieControl)uIElement).Open();

            if (salarie != null)
            {
                try
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Modification du salarié dénommée : '" + salarie.fullname + "' effectuée avec succès.");
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                }
                catch (Exception ex)
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Une errreur s'est produite lors de la modification d'un salarié. Pardonnez-nous du dérangement. Contactez l'administrateur du logiciel pour plus d'informations.");
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un salarié");
                }
            }
            else
            {
                if (((ListeSalarieControl)this._BorderContent.Child)._DataGridMain.SelectedItems.Count == 1)
                {
                    ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Personne)(((ListeSalarieControl)this._BorderContent.Child)._DataGridMain.SelectedItem));
                }
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Modification d'un salarié annulée.");
            }
            try
            {
                ((ListeSalarieControl)uIElement).listSalarie.OrderBy(sal => sal.Nom);
                ((ListeSalarieControl)this._BorderContent.Child)._DataGridMain.Items.Refresh();
            }
            catch (Exception) { }
        }

        private void UpdateDevis(UIElement uIElement)
        {
            Devis dev = ((ListeDevisControl)uIElement).Open();

            if (dev != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeDevisControl)uIElement).MiseAJourEtat("Modification", dev);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un devis");
                }
            }
        }

        private void UpdateContacts(UIElement uIElement)
        {
            Personne contact = ((ListeContactsControl)uIElement).Open();

            if (contact != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeContactsControl)uIElement).MiseAJourEtat("Modification", contact);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un contact");
                }

            }
        }

        private void UpdateEntreprises(UIElement uIElement)
        {
            Entreprise entreprise = ((ListeEntreprisesControl)uIElement).Open();

            if (entreprise != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeEntreprisesControl)uIElement).MiseAJourEtat("Modification", entreprise);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une entreprise");
                }

            }
        }

        private void UpdateAffaire(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Modification d'une affaire en cours ...");
            Affaire affaire = ((ListeAffaireControl)uIElement).Open();

            if (affaire != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Modification de l'affaire dénommée : '" + affaire.Numero + "' effectuée avec succès.");
                }
                catch (Exception ex)
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Une errreur s'est produite lors de la modification d'une affaire. Pardonnez-nous du dérangement. Contactez l'administrateur du logiciel pour plus d'informations.");
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une affaire");
                }

            }
            else
            {
                if (((ListeAffaireControl)this._BorderContent.Child)._DataGridMain.SelectedItems.Count == 1)
                {
                    ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Affaire)(((ListeAffaireControl)this._BorderContent.Child)._DataGridMain.SelectedItem));
                }
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Modification d'une affaire annulée.");
            }
            try
            {
                ((ListeAffaireControl)uIElement).listAffaire = new ObservableCollection<Affaire>(((ListeAffaireControl)uIElement).listAffaire.OrderBy(aff => aff.Numero));
                ((ListeAffaireControl)this._BorderContent.Child)._DataGridMain.Items.Refresh();
            }
            catch (Exception) { }
        }

        private void UpdateRegie(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Modification d'une régie en cours ...");
            Regie regie = ((ListeRegieControl)uIElement).Open();

            if (regie != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Modification de la régie numéro : '" + regie.Numero + "' effectuée avec succès.");
                }
                catch (Exception ex)
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Une errreur s'est produite lors de la modification d'une régie. Pardonnez-nous du dérangement. Contactez l'administrateur du logiciel pour plus d'informations.");
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une régie");
                }

            }
            else
            {
                if (((ListeRegieControl)this._BorderContent.Child)._DataGridMain.SelectedItems.Count == 1)
                {
                    ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Regie)(((ListeRegieControl)this._BorderContent.Child)._DataGridMain.SelectedItem));
                    ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, ((Regie)(((ListeRegieControl)this._BorderContent.Child)._DataGridMain.SelectedItem)).Travail);
                }
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Modification d'une régie annulée.");
            }
            try
            {
                ((ListeRegieControl)uIElement).listRegie = new ObservableCollection<Regie>(((ListeRegieControl)uIElement).listRegie.OrderBy(reg => reg.Numero));
                ((ListeRegieControl)this._BorderContent.Child)._DataGridMain.Items.Refresh();
            }
            catch (Exception) { }
        }

        private void UpdateDAO(UIElement uIElement)
        {
            DAO dao = ((ListeDAOControl)uIElement).Open();

            if (dao != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeDAOControl)uIElement).MiseAJourEtat("Modification", dao);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une DAO");
                }

            }
        }

        private void UpdateFactureProforma(UIElement uIElement)
        {
            Facture_Proforma fac = ((ListeFactureProformaControl)uIElement).Open();

            if (fac != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeFactureProformaControl)uIElement).MiseAJourEtat("Modification", fac);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une proforma");
                }

            }
        }

        private void UpdateFactureFournisseur(UIElement uIElement)
        {
            Facture_Fournisseur fac = ((ListeFactureFournisseurControl)uIElement).Open();

            if (fac != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeFactureFournisseurControl)uIElement).MiseAJourEtat("Modification", fac);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une facture fournisseur");
                }

            }
        }

        private void UpdateReveleHeureAtelier(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Modification d'un relevé d'atelier en cours ...");
            Releve_Heure_Atelier rha = ((ListeReveleHeureAtelierControl)uIElement).Open();

            if (rha != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Modification du relevé d'atelier de '" + rha.Salarie1.Personne.fullname + "' sur la semaine n° '" + rha.NumeroSemaine + "' de l'année '" + rha.Date_Debut.Value.Year + "' effectuée avec succès.");
                }
                catch (Exception ex)
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Une errreur s'est produite lors de la modification d'un relevé d'heure d'atelier. Pardonnez-nous du dérangement. Contactez l'administrateur du logiciel pour plus d'informations.");
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un relevé d'heures d'atelier");
                }

            }
            else
            {
                if (((ListeReveleHeureAtelierControl)this._BorderContent.Child)._DataGridMain.SelectedItems.Count == 1)
                {
                    ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Releve_Heure_Atelier)(((ListeReveleHeureAtelierControl)this._BorderContent.Child)._DataGridMain.SelectedItem));
                }
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Modification d'un relevé d'atelier annulée.");
            }
            try
            {
                ((ListeReveleHeureAtelierControl)uIElement).listReleve = new ObservableCollection<Releve_Heure_Atelier>(((App)App.Current).mySitaffEntities.Releve_Heure_Atelier.OrderByDescending(rhaz => rhaz.Date_Debut).ThenBy(rhaz => rhaz.Salarie1.Personne.Nom));
                ((ListeReveleHeureAtelierControl)this._BorderContent.Child)._DataGridMain.Items.Refresh();
            }
            catch (Exception) { }
        }

        private void UpdateCommandeFournisseur(UIElement uIElement)
        {
            Commande_Fournisseur commande_Fournisseur = ((ListeCommandeFournisseurControl)uIElement).Open();

            if (commande_Fournisseur != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeCommandeFournisseurControl)uIElement).MiseAJourEtat("Modification", commande_Fournisseur);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une commande fournisseur");
                }
            }
        }

        private void UpdateConges(UIElement uIElement)
        {
            Conge con = ((ListeCongesControl)uIElement).Open();

            if (con != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeCongesControl)uIElement).MiseAJourEtat("Modification", con);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un congé");
                }
            }
        }

        private void UpdateReleveHeureForfait(UIElement uIElement)
        {
            Releve_Heure_Forfait rhf = ((ListeReleveHeureForfaitControl)uIElement).Open();

            if (rhf != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeReleveHeureForfaitControl)uIElement).MiseAJourEtat("Modification", rhf);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'un relevé hebdomadaire");
                }

            }
        }

        private void UpdateFacture(UIElement uIElement)
        {
            Facture f = ((ListeFactureControl)uIElement).Open();

            if (f != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeFactureControl)uIElement).MiseAJourEtat("Modification", f);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification d'une facture");
                }
            }

        }

        #endregion

        private void _CommandUpdate_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this._BorderContent != null && this._BorderContent.Child != null)
            {
                UIElement uIElement = this._BorderContent.Child;
                if (this.securite.ControlsUpdateAuthorized.Contains(uIElement.ToString()))
                {
                    e.CanExecute = this.securite.VerificationDroitActionsCRUD(uIElement.ToString(), "Update");
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

        #region bouton supprimer
        private void _CommandDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            UIElement uIElement = this._BorderContent.Child;

            switch (uIElement.ToString())
            {
                case "SitaffRibbon.UserControls.ListeDaillyControl":
                    this.DeleteDailly(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeRetourChantierControl":
                    this.DeleteRetourChantier(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeProformaClientControl":
                    this.DeleteProformaClient(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFraisControl":
                    this.DeleteFrais(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReservationSalleControl":
                    this.DeleteReservationSalle(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeSortieAtelierControl":
                    this.DeleteSortieAtelier(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeOrdreMissionControl":
                    this.DeleteOrdreMission(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReglementClientControl":
                    this.DeleteReglementClient(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReleveHeureForfaitControl":
                    this.DeleteReleveHeureForfait(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeBonLivraisonControl":
                    this.DeleteBonLivraison(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeAvanceControl":
                    this.DeleteAvance(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeCongesControl":
                    this.DeleteConges(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeCommandeFournisseurControl":
                    this.DeleteCommandeFournisseur(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeReveleHeureAtelierControl":
                    this.DeleteReleveHeureAtelier(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeDAOControl":
                    this.DeleteDAO(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeRegieControl":
                    this.DeleteRegie(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeEntreprisesControl":
                    this.DeleteEntreprises(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeContactsControl":
                    this.DeleteContacts(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeSalarieControl":
                    this.DeleteSalarie(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeDevisControl":
                    this.DeleteDevis(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFactureControl":
                    this.DeleteFacture(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFactureProformaControl":
                    this.DeleteFactureProforma(uIElement);
                    break;
                case "SitaffRibbon.UserControls.ListeFactureFournisseurControl":
                    this.DeleteFactureFournisseur(uIElement);
                    break;
                default:
                    MessageBox.Show("Vous n'avez normalement pas à cliquer sur ce bouton, il devrait être grisé. Si vous recevez cette erreur, merci d'envoyer un mail à l'administrateur avec une image de l'erreur, le nom du bouton ainsi que le nom de votre utilisateur.");
                    break;
            }

            this.Cursor = ((App)App.Current)._mainCursor;
        }

        #region Fonctions des suppressions

        private void DeleteDailly(UIElement uIElement)
        {
            Dailly dailly = ((ListeDaillyControl)uIElement).Remove();

            if (dailly != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Dailly.DeleteObject(dailly);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeDaillyControl)uIElement).MiseAJourEtat("Suppression", dailly);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un dailly");
                }
            }
        }

        private void DeleteRetourChantier(UIElement uIElement)
        {
            Retour_Chantier retour_chantier = ((ListeRetourChantierControl)uIElement).Remove();

            if (retour_chantier != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Retour_Chantier.DeleteObject(retour_chantier);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeRetourChantierControl)uIElement).MiseAJourEtat("Suppression", retour_chantier);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un retour chantier");
                }
            }
        }

        private void DeleteProformaClient(UIElement uIElement)
        {
            Facture facture = ((ListeProformaClientControl)uIElement).Remove();

            if (facture != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Facture.DeleteObject(facture);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeProformaClientControl)uIElement).MiseAJourEtat("Suppression", facture);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une facture proforma");
                }
            }
        }

        private void DeleteFrais(UIElement uIElement)
        {
            Frais frais = ((ListeFraisControl)uIElement).Remove();

            if (frais != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Frais.DeleteObject(frais);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeFraisControl)uIElement).MiseAJourEtat("Suppression", frais);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression de frais");
                }
            }
        }

        private void DeleteReservationSalle(UIElement uIElement)
        {
            Reservation_Salle reservation_salle = ((ListeReservationSalleControl)uIElement).Remove();

            if (reservation_salle != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Reservation_Salle.DeleteObject(reservation_salle);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeReservationSalleControl)uIElement).MiseAJourEtat("Suppression", reservation_salle);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une réservation salle");
                }
            }
        }

        private void DeleteSortieAtelier(UIElement uIElement)
        {
            Sortie_Atelier sa = ((ListeSortieAtelierControl)uIElement).Remove();

            if (sa != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Sortie_Atelier.DeleteObject(sa);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeSortieAtelierControl)uIElement).MiseAJourEtat("Suppression", sa);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une sortie atelier");
                }
            }
        }

        private void DeleteOrdreMission(UIElement uIElement)
        {
            Ordre_Mission om = ((ListeOrdreMissionControl)uIElement).Remove();

            if (om != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Ordre_Mission.DeleteObject(om);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeOrdreMissionControl)uIElement).MiseAJourEtat("Suppression", om);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un ordre de mission");
                }
            }
        }

        private void DeleteReglementClient(UIElement uIElement)
        {
			Reglement_Client rc = ((ListeReglementClientControl)uIElement).Remove();

			if (rc != null)
			{
				try
				{
					((App)App.Current).mySitaffEntities.Reglement_Client.DeleteObject(rc);
					((App)App.Current).mySitaffEntities.SaveChanges();
					((ListeReglementClientControl)uIElement).MiseAJourEtat("Suppression", rc);
				}
				catch (Exception ex)
				{
					this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un réglement client");
				}
			}
        }

        private void DeleteAvance(UIElement uIElement)
        {
            Avance ava = ((ListeAvanceControl)uIElement).Remove();

            if (ava != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Avance.DeleteObject(ava);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeAvanceControl)uIElement).MiseAJourEtat("Suppression", ava);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une avance");
                }
            }
        }

        private void DeleteBonLivraison(UIElement uIElement)
        {
            Bon_Livraison bl = ((ListeBonLivraisonControl)uIElement).Remove();

            if (bl != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Bon_Livraison.DeleteObject(bl);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeBonLivraisonControl)uIElement).MiseAJourEtat("Suppression", bl);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un BL");
                }
            }
        }

        private void DeleteDevis(UIElement uIElement)
        {
            Devis dev = ((ListeDevisControl)uIElement).Remove();

            if (dev != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Devis.DeleteObject(dev);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeDevisControl)uIElement).MiseAJourEtat("Suppression", dev);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un devis");
                }
            }
        }

        private void DeleteSalarie(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Suppression d'un salarié en cours ...");
            Personne salarie = ((ListeSalarieControl)uIElement).Remove();

            if (salarie != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Personne.DeleteObject(salarie);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeSalarieControl)uIElement).listSalarie.Remove(salarie);
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Suppression du salarié dénommé : '" + salarie.fullname + "' effectuée avec succès.");
                }
                catch (Exception ex)
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Une errreur s'est produite lors de la suppression d'un salarié. Pardonnez-nous du dérangement. Contactez l'administrateur du logiciel pour plus d'informations.");
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un salarié");
                }
            }
            else
            {
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Suppression d'un salarié annulée.");
            }
        }

        private void DeleteContacts(UIElement uIElement)
        {
            Personne contact = ((ListeContactsControl)uIElement).Remove();

            if (contact != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Personne.DeleteObject(contact);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeContactsControl)uIElement).MiseAJourEtat("Suppression", contact);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un contact");
                }
            }
        }

        private void DeleteEntreprises(UIElement uIElement)
        {
            Entreprise entreprise = ((ListeEntreprisesControl)uIElement).Remove();

            if (entreprise != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Entreprise.DeleteObject(entreprise);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeEntreprisesControl)uIElement).MiseAJourEtat("Suppression", entreprise);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une entreprise");
                }
            }
        }

        private void DeleteRegie(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Suppression d'une régie en cours ...");
            Regie regie = ((ListeRegieControl)uIElement).Remove();

            if (regie != null)
            {
                try
                {
                    foreach (Travail item in regie.Travail)
                    {
                        ((App)App.Current).mySitaffEntities.Travail.DeleteObject(item);
                    }
                    ((App)App.Current).mySitaffEntities.Regie.DeleteObject(regie);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeRegieControl)uIElement).listRegie.Remove(regie);
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Suppression de la régie numéro : '" + regie.Numero + "' effectuée avec succès.");
                }
                catch (Exception ex)
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Une errreur s'est produite lors de la suppression d'une régie. Pardonnez-nous du dérangement. Contactez l'administrateur du logiciel pour plus d'informations.");
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une régie");
                }
            }
            else
            {
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Suppression d'une régie annulée.");
            }
        }

        private void DeleteFactureProforma(UIElement uIElement)
        {
            Facture_Proforma fac = ((ListeFactureProformaControl)uIElement).Remove();

            if (fac != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Facture_Proforma.DeleteObject(fac);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeFactureProformaControl)uIElement).MiseAJourEtat("Suppression", fac);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une facture proforma");
                }
            }
        }

        private void DeleteDAO(UIElement uIElement)
        {
            DAO dao = ((ListeDAOControl)uIElement).Remove();

            if (dao != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.DAO.DeleteObject(dao);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeDAOControl)uIElement).MiseAJourEtat("Suppression", dao);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une DAO");
                }
            }
        }

        private void DeleteReleveHeureAtelier(UIElement uIElement)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Suppression d'un relevé d'atelier en cours ...");
            Releve_Heure_Atelier rha = ((ListeReveleHeureAtelierControl)uIElement).Remove();

            if (rha != null)
            {
                try
                {
                    ObservableCollection<Heure_Atelier> toRemoveHA = new ObservableCollection<Heure_Atelier>();
                    foreach (Heure_Atelier ha in rha.Heure_Atelier)
                    {
                        toRemoveHA.Add(ha);
                    }
                    foreach (Heure_Atelier ha in toRemoveHA)
                    {
                        ((App)App.Current).mySitaffEntities.Heure_Atelier.DeleteObject(ha);
                    }
                    ObservableCollection<Heure_Atelier_Autre> toRemoveHAT = new ObservableCollection<Heure_Atelier_Autre>();
                    foreach (Heure_Atelier_Autre hat in rha.Heure_Atelier_Autre)
                    {
                        toRemoveHAT.Add(hat);
                    }
                    foreach (Heure_Atelier_Autre hat in toRemoveHAT)
                    {
                        ((App)App.Current).mySitaffEntities.Heure_Atelier_Autre.DeleteObject(hat);
                    }
                    ((App)App.Current).mySitaffEntities.Releve_Heure_Atelier.DeleteObject(rha);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeReveleHeureAtelierControl)uIElement).listReleve.Remove(rha);
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Suppression du relevé d'atelier effectuée avec succès.");
                }
                catch (Exception ex)
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Une errreur s'est produite lors de la suppression d'un relevé d'atelier. Pardonnez-nous du dérangement. Contactez l'administrateur du logiciel pour plus d'informations.");
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un relevé d'atelier");
                }
            }
            else
            {
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Suppression d'un relevé d'atelier annulée.");
            }
        }

        private void DeleteCommandeFournisseur(UIElement uIElement)
        {
            Commande_Fournisseur commande_fournisseur = ((ListeCommandeFournisseurControl)uIElement).Remove();

            if (commande_fournisseur != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Commande_Fournisseur.DeleteObject(commande_fournisseur);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeCommandeFournisseurControl)uIElement).MiseAJourEtat("Suppression", commande_fournisseur);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une commande fournisseur");
                }
            }
        }

        private void DeleteFactureFournisseur(UIElement uIElement)
        {
            Facture_Fournisseur facture_fournisseur = ((ListeFactureFournisseurControl)uIElement).Remove();

            if (facture_fournisseur != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Facture_Fournisseur.DeleteObject(facture_fournisseur);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeFactureFournisseurControl)uIElement).MiseAJourEtat("Suppression", facture_fournisseur);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une facture fournisseur");
                }
            }
        }

        private void DeleteConges(UIElement uIElement)
        {
            Conge con = ((ListeCongesControl)uIElement).Remove();

            if (con != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Conge.DeleteObject(con);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeCongesControl)uIElement).MiseAJourEtat("Suppression", con);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un congé");
                }
            }
        }

        private void DeleteReleveHeureForfait(UIElement uIElement)
        {
            Releve_Heure_Forfait rhf = ((ListeReleveHeureForfaitControl)uIElement).Remove();

            if (rhf != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Releve_Heure_Forfait.DeleteObject(rhf);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeReleveHeureForfaitControl)uIElement).MiseAJourEtat("Suppression", rhf);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'un relevé d'heures forfait");
                }
            }
        }

        private void DeleteFacture(UIElement uIElement)
        {
            Facture f = ((ListeFactureControl)uIElement).Remove();

            if (f != null)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Facture.DeleteObject(f);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    ((ListeFactureControl)uIElement).MiseAJourEtat("Suppression", f);
                }
                catch (Exception ex)
                {
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la suppression d'une facture");
                }
            }

        }

        #endregion

        private void _CommandDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this._BorderContent != null && this._BorderContent.Child != null)
            {
                UIElement uIElement = this._BorderContent.Child;
                if (this.securite.ControlsRemoveAuthorized.Contains(uIElement.ToString()))
                {
                    e.CanExecute = this.securite.VerificationDroitActionsCRUD(uIElement.ToString(), "Remove");
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

        #region bouton afficher salaries

        private void _CommandAfficher_AfficherSalaries_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des salariés en cours ...");

            ((App)App.Current).refreshEDMX();
            ListeSalarieControl listeSalariesControl = new ListeSalarieControl();

            this._BorderContent.Child = listeSalariesControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Salaries.Background = ((App)App.Current).SaveFocusedBackground;
            this.Salaries.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficher_AfficherSalaries_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeSalarieControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeSalarieControl"))
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

        #region bouton afficher entreprises
        private void _CommandAficher_Entreprises_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des entreprises en cours ...");

            ((App)App.Current).refreshEDMX();
            ListeEntreprisesControl listeEntreprisesControl = new ListeEntreprisesControl();

            this._BorderContent.Child = listeEntreprisesControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Entreprises.Background = ((App)App.Current).SaveFocusedBackground;
            this.Entreprises.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAficher_Entreprises_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeEntreprisesControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeEntreprisesControl"))
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

        #region bouton afficher contacts
        private void _CommandAfficherContacts_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des contacts en cours ...");

            ((App)App.Current).refreshEDMX();
            ListeContactsControl listeContactsControl = new ListeContactsControl();

            this._BorderContent.Child = listeContactsControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Contacts.Background = ((App)App.Current).SaveFocusedBackground;
            this.Contacts.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherContacts_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeContactsControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeContactsControl"))
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

        #region bouton afficher appel offres
        private void _CommandAfficherAppelOffres_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des appel d'offre en cours ...");

            ((App)App.Current).refreshEDMX();
            ListeAppelOffresControl listeAppelOffresControl = new ListeAppelOffresControl();

            this._BorderContent.Child = listeAppelOffresControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.AppelOffres.Background = ((App)App.Current).SaveFocusedBackground;
            this.AppelOffres.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherAppelOffres_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeAppelOffresControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeAppelOffresControl"))
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

        #region bouton Afficher Shop

        private void _CommandOpenShop_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            this.resetCouleurs();

            this.viderBorderContent();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Affichage du Shop en cours ...");

            ShopCommandeWindow shopCommandeWindow = new ShopCommandeWindow();
            shopCommandeWindow._BoutonImporter.Visibility = Visibility.Collapsed;
            shopCommandeWindow.ShowDialog();

            this.progressBarMainWindow.IsIndeterminate = false;
            this.changementTexteStatusBar("Affichage du Shop terminé.");
        }

        private void _CommandOpenShop_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.VerificationDroitOuvrirShop())
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher parametres

        private void _CommandAfficher_AfficherParametres_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.resetCouleurs();

            this.parametreMain = new ParametresMain(this);
            this.viderBorderContent();
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Fenêtre des paramètres ouverte ... Vous devez fermer la fenêtre des paramètres pour revenir à l’application.");
            this.parametreMain.ShowDialog();
        }

        private void _CommandAfficher_AfficherParametres_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.VerificationDroitOuvrirParametres())
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region bouton afficher affaires

        private void _CommandAfficherAffaires_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des affaires en cours ...");

            ((App)App.Current).refreshEDMX();
            ListeAffaireControl listeAffaireControl = new ListeAffaireControl();

            this._BorderContent.Child = listeAffaireControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Affaires.Background = ((App)App.Current).SaveFocusedBackground;
            this.Affaires.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherAffaires_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeAffaireControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeAffaireControl"))
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

        #region afficher Devis
        private void _CommandAfficherDevis_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des devis en cours ...");

            ((App)App.Current).refreshEDMX();
            ListeDevisControl listeDevisControl = new ListeDevisControl();

            this._BorderContent.Child = listeDevisControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Devis.Background = ((App)App.Current).SaveFocusedBackground;
            this.Devis.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherDevis_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeDevisControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeDevisControl"))
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

        #region afficher Regie

        private void _CommandAfficherRegie_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des régies en cours ...");

            ((App)App.Current).refreshEDMX();
            ListeRegieControl listeRegieControl = new ListeRegieControl();

            this._BorderContent.Child = listeRegieControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Regies.Background = ((App)App.Current).SaveFocusedBackground;
            this.Regies.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherRegie_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeRegieControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeRegieControl"))
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

        #region afficher DAO

        private void _CommandAfficherDAO_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //J'attends d'avoir la main pour lancer l'affichage (attente du mutex) pour ne pas avoir 2 chargements en même temps
            this._mutex.WaitOne();
            this.startThread();

            //Affichage du chargement en bas à gauche de la fenêtre (indicateur d'avancement utilisateur)
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des DAO en cours ...");

            //Instanciation du UserControl DAO
            ((App)App.Current).refreshEDMX();
            ListeDAOControl listeDAOControl = new ListeDAOControl();

            //Insertion du UserControl dans la fenêtre
            this._BorderContent.Child = listeDAOControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.dao.Background = ((App)App.Current).SaveFocusedBackground;
            this.dao.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherDAO_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeDAOControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeDAOControl"))
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

        #region afficher BonLivraison

        private void _CommandAfficherBonLivraison_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des BL en cours ...");

            //((App)App.Current).refreshEDMX();
            ListeBonLivraisonControl listeBonLivraisonControl = new ListeBonLivraisonControl();

            this._BorderContent.Child = listeBonLivraisonControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.BonLivraison.Background = ((App)App.Current).SaveFocusedBackground;
            this.BonLivraison.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherBonLivraison_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeBonLivraisonControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeBonLivraisonControl"))
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

        #region afficher relevés atelier

        private void _CommandAfficherHeuresAtelier_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des Relevés d'atelier en cours ...");

            ((App)App.Current).refreshEDMX();
            ListeReveleHeureAtelierControl listeReleveHeureAtelierControl = new ListeReveleHeureAtelierControl();

            this._BorderContent.Child = listeReleveHeureAtelierControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.HeuresAtelier.Background = ((App)App.Current).SaveFocusedBackground;
            this.HeuresAtelier.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherHeuresAtelier_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeReveleHeureAtelierControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeReveleHeureAtelierControl"))
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

        #region afficher relevé forfait

        private void _CommandAfficherHeuresForfait_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des heures forfait en cours ...");

            ((App)App.Current).refreshEDMX();
            ListeReleveHeureForfaitControl listeReleveHeureForfaitControl = new ListeReleveHeureForfaitControl();

            this._BorderContent.Child = listeReleveHeureForfaitControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.HeuresForfait.Background = ((App)App.Current).SaveFocusedBackground;
            this.HeuresForfait.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherHeuresForfait_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeReleveHeureForfaitControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeReleveHeureForfaitControl"))
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

        #region afficher factures proforma

        private void _CommandAfficherFacturesProforma_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des factures proforma en cours ...");

            ((App)App.Current).refreshEDMX();
            ListeFactureProformaControl listeFactureProformaControl = new ListeFactureProformaControl();

            this._BorderContent.Child = listeFactureProformaControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.FacturesProforma.Background = ((App)App.Current).SaveFocusedBackground;
            this.FacturesProforma.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherFacturesProforma_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeFactureProformaControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeFactureProformaControl"))
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

        #region afficher factures proforma client

        private void _CommandAfficherFacturesProformaClient_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des factures proforma client en cours ...");

            ((App)App.Current).refreshEDMX();
            ListeProformaClientControl listeProformaClientControl = new ListeProformaClientControl();

            this._BorderContent.Child = listeProformaClientControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Proformaclient.Background = ((App)App.Current).SaveFocusedBackground;
            this.Proformaclient.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherFacturesProformaClient_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeProformaClientControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeProformaClientControl"))
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

        #region afficher congés

        private void _CommandAfficherConge_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des Congés en cours ...");

            ((App)App.Current).refreshEDMX();
            ListeCongesControl listeCongeControl = new ListeCongesControl();

            this._BorderContent.Child = listeCongeControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Conges.Background = ((App)App.Current).SaveFocusedBackground;
            this.Conges.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherConge_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeCongesControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeCongesControl"))
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

        #region afficher Commande_Fournisseur

        private void _CommandAfficherCommande_Fournisseur_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //J'attends d'avoir la main pour lancer l'affichage (attente du mutex) pour ne pas avoir 2 chargements en même temps
            this._mutex.WaitOne();
            this.startThread();

            //Affichage du chargement en bas à gauche de la fenêtre (indicateur d'avancement utilisateur)
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des Commandes Fournisseur en cours ...");

            //Instanciation du UserControl Commande fournisseur
            ((App)App.Current).refreshEDMX();
            ListeCommandeFournisseurControl listeCommandeFournisseurControl = new ListeCommandeFournisseurControl();

            //Insertion du UserControl dans la fenêtre
            this._BorderContent.Child = listeCommandeFournisseurControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.CommandesClients.Background = ((App)App.Current).SaveFocusedBackground;
            this.CommandesClients.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherCommande_Fournisseur_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeCommandeFournisseurControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeCommandeFournisseurControl"))
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

        #region afficher resume conge

        private void _CommandAfficherResumeConge_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._mutex.WaitOne();
            this.startThread();

            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des Congés en cours ...");

            ((App)App.Current).refreshEDMX();
            ListeResumeCongesControl listeResumeCongeControl = new ListeResumeCongesControl();

            this._BorderContent.Child = listeResumeCongeControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.MesDemandes.Background = ((App)App.Current).SaveFocusedBackground;
            this.MesDemandes.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherResumeConge_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region afficher Facture
        private void _CommandAfficherFactures_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //J'attends d'avoir la main pour lancer l'affichage (attente du mutex) pour ne pas avoir 2 chargements en même temps
            this._mutex.WaitOne();
            this.startThread();

            //Affichage du chargement en bas à gauche de la fenêtre (indicateur d'avancement utilisateur)
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des factures en cours ...");

            //Instanciation du UserControl DAO
            ((App)App.Current).refreshEDMX();
            ListeFactureControl listeFactureControl = new ListeFactureControl();

            //Insertion du UserControl dans la fenêtre
            this._BorderContent.Child = listeFactureControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Factures.Background = ((App)App.Current).SaveFocusedBackground;
            this.Factures.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherFactures_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeFactureControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeFactureControl"))
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

        #region Reglement client

        private void _CommandAfficherReglement_Client_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            //J'attends d'avoir la main pour lancer l'affichage (attente du mutex) pour ne pas avoir 2 chargements en même temps
            this._mutex.WaitOne();
            this.startThread();

            //Affichage du chargement en bas à gauche de la fenêtre (indicateur d'avancement utilisateur)
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des réglements client en cours ...");

            //Instanciation du UserControl DAO
            ((App)App.Current).refreshEDMX();
            ListeReglementClientControl listeReglementClientControl = new ListeReglementClientControl();

            //Insertion du UserControl dans la fenêtre
            this._BorderContent.Child = listeReglementClientControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.ReglementClient.Background = ((App)App.Current).SaveFocusedBackground;
            this.ReglementClient.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherReglement_Client_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeReglementClientControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeReglementClientControl"))
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

        #region afficher Facture Fournisseur
        private void _CommandAfficherFactureFournisseur_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //J'attends d'avoir la main pour lancer l'affichage (attente du mutex) pour ne pas avoir 2 chargements en même temps
            this._mutex.WaitOne();
            this.startThread();

            //Affichage du chargement en bas à gauche de la fenêtre (indicateur d'avancement utilisateur)
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des factures fournisseur en cours ...");

            //Instanciation du UserControl FactureFournisseur
            ListeFactureFournisseurControl listeFactureFournisseurControl = new ListeFactureFournisseurControl();

            //Insertion du UserControl dans la fenêtre
            this._BorderContent.Child = listeFactureFournisseurControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.FacturesFournisseur.Background = ((App)App.Current).SaveFocusedBackground;
            this.FacturesFournisseur.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherFactureFournisseur_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeFactureFournisseurControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeFactureFournisseurControl"))
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

        #region Avances

        private void _CommandAfficherAvances_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            //J'attends d'avoir la main pour lancer l'affichage (attente du mutex) pour ne pas avoir 2 chargements en même temps
            this._mutex.WaitOne();
            this.startThread();

            //Affichage du chargement en bas à gauche de la fenêtre (indicateur d'avancement utilisateur)
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des avances en cours ...");

            //Instanciation du UserControl FactureFournisseur
            ListeAvanceControl listeAvanceControl = new ListeAvanceControl();

            //Insertion du UserControl dans la fenêtre
            this._BorderContent.Child = listeAvanceControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.AvanceFrais.Background = ((App)App.Current).SaveFocusedBackground;
            this.AvanceFrais.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherAvances_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeAvanceControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeAvanceControl"))
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

        #region Ordre de mission

        private void _CommandAfficherOrdreMission_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            //J'attends d'avoir la main pour lancer l'affichage (attente du mutex) pour ne pas avoir 2 chargements en même temps
            this._mutex.WaitOne();
            this.startThread();

            //Affichage du chargement en bas à gauche de la fenêtre (indicateur d'avancement utilisateur)
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des ordres de mission en cours ...");

            //Instanciation du UserControl FactureFournisseur
            ListeOrdreMissionControl listeOrdreMissionControl = new ListeOrdreMissionControl();

            //Insertion du UserControl dans la fenêtre
            this._BorderContent.Child = listeOrdreMissionControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.OrdreMission.Background = ((App)App.Current).SaveFocusedBackground;
            this.OrdreMission.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherOrdreMission_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeOrdreMissionControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeOrdreMissionControl"))
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

        #region afficher Sortie Atelier

        private void _CommandAfficherSortieAtelier_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            //J'attends d'avoir la main pour lancer l'affichage (attente du mutex) pour ne pas avoir 2 chargements en même temps
            this._mutex.WaitOne();
            this.startThread();

            //Affichage du chargement en bas à gauche de la fenêtre (indicateur d'avancement utilisateur)
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des sorties atelier en cours ...");

            //Instanciation du UserControl FactureFournisseur
            ListeSortieAtelierControl listeSortieAtelierControl = new ListeSortieAtelierControl();

            //Insertion du UserControl dans la fenêtre
            this._BorderContent.Child = listeSortieAtelierControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.SortieAtelier.Background = ((App)App.Current).SaveFocusedBackground;
            this.SortieAtelier.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherSortieAtelier_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeSortieAtelierControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeSortieAtelierControl"))
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

        #region afficher retour chantier

        private void _CommandAfficherRetourChantier_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            //J'attends d'avoir la main pour lancer l'affichage (attente du mutex) pour ne pas avoir 2 chargements en même temps
            this._mutex.WaitOne();
            this.startThread();

            //Affichage du chargement en bas à gauche de la fenêtre (indicateur d'avancement utilisateur)
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des retours chantier en cours ...");

            //Instanciation du UserControl retourchantier
            ListeRetourChantierControl listeRetourChantierControl = new ListeRetourChantierControl();

            //Insertion du UserControl dans la fenêtre
            this._BorderContent.Child = listeRetourChantierControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.RetourChantier.Background = ((App)App.Current).SaveFocusedBackground;
            this.RetourChantier.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherRetourChantier_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeRetourChantierControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeRetourChantierControl"))
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

        #region Resume devis

        private void _CommandAfficherResumeDevis_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            //J'attends d'avoir la main pour lancer l'affichage (attente du mutex) pour ne pas avoir 2 chargements en même temps
            this._mutex.WaitOne();
            this.startThread();

            //Affichage du chargement en bas à gauche de la fenêtre (indicateur d'avancement utilisateur)
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des versions de devis en cours ...");

            //Instanciation du UserControl FactureFournisseur
            ListeResumeDevisControl listeResumeDevisControl = new ListeResumeDevisControl();

            //Insertion du UserControl dans la fenêtre
            this._BorderContent.Child = listeResumeDevisControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.ResumeDevis.Background = ((App)App.Current).SaveFocusedBackground;
            this.ResumeDevis.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherResumeDevis_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeResumeDevisControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeResumeDevisControl"))
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

        #region Reservation salle

        private void _CommandAfficherReservationSalle_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            //J'attends d'avoir la main pour lancer l'affichage (attente du mutex) pour ne pas avoir 2 chargements en même temps
            this._mutex.WaitOne();
            this.startThread();

            //Affichage du chargement en bas à gauche de la fenêtre (indicateur d'avancement utilisateur)
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des reservations de salle en cours ...");

            //Instanciation du UserControl FactureFournisseur
            ListeReservationSalleControl listeReservationSalleControl = new ListeReservationSalleControl();

            //Insertion du UserControl dans la fenêtre
            this._BorderContent.Child = listeReservationSalleControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.ReservationSalle.Background = ((App)App.Current).SaveFocusedBackground;
            this.ReservationSalle.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherReservationSalle_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeReservationSalleControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeReservationSalleControl"))
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

        #region Frais

        private void _CommandAfficherFrais_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            //J'attends d'avoir la main pour lancer l'affichage (attente du mutex) pour ne pas avoir 2 chargements en même temps
            this._mutex.WaitOne();
            this.startThread();

            //Affichage du chargement en bas à gauche de la fenêtre (indicateur d'avancement utilisateur)
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des frais en cours ...");

            //Instanciation du UserControl FactureFournisseur
            ListeFraisControl listeFraisControl = new ListeFraisControl();

            //Insertion du UserControl dans la fenêtre
            this._BorderContent.Child = listeFraisControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Frais.Background = ((App)App.Current).SaveFocusedBackground;
            this.Frais.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherFrais_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeFraisControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeFraisControl"))
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

        #region Dailly

        private void _CommandAfficherDailly_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            //J'attends d'avoir la main pour lancer l'affichage (attente du mutex) pour ne pas avoir 2 chargements en même temps
            this._mutex.WaitOne();
            this.startThread();

            //Affichage du chargement en bas à gauche de la fenêtre (indicateur d'avancement utilisateur)
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Chargement des dailly en cours ...");

            //Instanciation du UserControl retourchantier
            ListeDaillyControl listeDaillyControl = new ListeDaillyControl();

            //Insertion du UserControl dans la fenêtre
            this._BorderContent.Child = listeDaillyControl;

            //Mise en couleur de l'icone pour plus de clarté (remise à zéro de toutes les icones + mise en couleur)
            this.resetCouleurs();
            this.Dailly.Background = ((App)App.Current).SaveFocusedBackground;
            this.Dailly.BorderBrush = ((App)App.Current).SaveFocusedBorderBrush;
        }

        private void _CommandAfficherDailly_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.securite.ControlsListAuthorized.Contains("SitaffRibbon.UserControls.ListeDaillyControl"))
            {
                if (this.securite.VerificationDroitListing("SitaffRibbon.UserControls.ListeDaillyControl"))
                {
                    e.CanExecute = true;
                    Tab_GestionAffaire_Group_Dailly.Visibility = Visibility.Visible;
                }
                else
                {
                    e.CanExecute = false;
                    Tab_GestionAffaire_Group_Dailly.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                e.CanExecute = false;
                Tab_GestionAffaire_Group_Dailly.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        #endregion

        #region Customize

        private void _CommandCustomize_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PersonnalisationWindow personnalisationWindow = new PersonnalisationWindow();
            personnalisationWindow.ShowDialog();
        }

        private void _CommandCustomize_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region aide

        private void _CommandAfficherAide_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //ReportingWindow reportingWindow = new ReportingWindow();
            //reportingWindow._webBrowser.Source = new Uri("http://srv-linux/aidesitaff/AideSitaff.htm");
            //reportingWindow.Title = "Aide Sitaff";
            //reportingWindow.Show();

            //Tous les fichiers dans le dossier Help doivent être configuré de la manière suivante :
            //cliquer dessus pour voir les propriétés et mettre :
            // - Action de génération : Contenu
            // - Copier dans le répertoire de sortie : Copier si plus récent
            System.Windows.Forms.Help.ShowHelp(null, @".\Help\aide.chm");
        }

        private void _CommandAfficherAide_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region Rapports

        #region Conge
        private void _CommandRapportImprimerConge_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //instructions à inscrire si la commande Update peut s'executer
            UIElement uIElement = this._BorderContent.Child;

            //Modification si on se trouve sur la liste des congés
            if (uIElement is ListeCongesControl)
            {
                this.progressBarMainWindow.IsIndeterminate = true;
                this.changementTexteStatusBar("Affichage du congé pour impression en cours ...");
                Conge con = ((ListeCongesControl)uIElement).RapportImprimer();
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Affichage du congé pour impression terminé.");
            }
        }

        private void _CommandRapportImprimerConge_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region Commande_Fournisseur

        #region Avec Prix

        private void _CommandRapportImprimerCommande_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //instructions à inscrire si la commande Update peut s'executer
            UIElement uIElement = this._BorderContent.Child;

            //Modification si on se trouve sur la liste des congés
            if (uIElement is ListeCommandeFournisseurControl)
            {
                this.progressBarMainWindow.IsIndeterminate = true;
                this.changementTexteStatusBar("Affichage de la commande pour impression en cours ...");
                Commande_Fournisseur com = ((ListeCommandeFournisseurControl)uIElement).RapportImprimer();
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Affichage de la commande pour impression terminé.");
            }
        }

        private void _CommandRapportImprimerCommande_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region Sans Prix

        private void _CommandRapportImprimerCommandeSansPrix_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //instructions à inscrire si la commande Update peut s'executer
            UIElement uIElement = this._BorderContent.Child;

            //Modification si on se trouve sur la liste des congés
            if (uIElement is ListeCommandeFournisseurControl)
            {
                this.progressBarMainWindow.IsIndeterminate = true;
                this.changementTexteStatusBar("Affichage de la commande pour impression en cours ...");
                Commande_Fournisseur com = ((ListeCommandeFournisseurControl)uIElement).RapportImprimerSansPrix();
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Affichage de la commande pour impression terminé.");
            }
        }

        private void _CommandRapportImprimerCommandeSansPrix_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #endregion

        #region Affaire

        private void _CommandRapportImprimerDossierAffaire_CanExecute(object sender, ExecutedRoutedEventArgs e)
        {
            //instructions à inscrire si la commande Update peut s'executer
            UIElement uIElement = this._BorderContent.Child;

            //Modification si on se trouve sur la liste des congés
            if (uIElement is ListeAffaireControl)
            {
                this.progressBarMainWindow.IsIndeterminate = true;
                this.changementTexteStatusBar("Affichage du dossier d'affaire pour impression en cours ...");
                Affaire con = ((ListeAffaireControl)uIElement).RapportImprimer();
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Affichage du dossier d'affaire pour impression terminé.");
            }
        }

        private void _CommandRapportImprimerDossierAffaire_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region Facture fournisseur

        private void _CommandRapportReleveFactureFournisseur_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            //instructions à inscrire si la commande Update peut s'executer
            UIElement uIElement = this._BorderContent.Child;

            //Modification si on se trouve sur la liste des congés
            if (uIElement is ListeFactureFournisseurControl)
            {
                this.progressBarMainWindow.IsIndeterminate = true;
                this.changementTexteStatusBar("Affichage de relevé de facture(s) en cours ...");
                ((ListeFactureFournisseurControl)uIElement).RapportReleveFacture();
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Affichage de relevé de facture(s) terminé.");
            }
        }

        private void _CommandRapportReleveFactureFournisseur_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region Heures

        private void _CommandRapportReleveActiviteParSalarie_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            //instructions à inscrire si la commande Update peut s'executer
            UIElement uIElement = this._BorderContent.Child;


            if (uIElement is ListeReleveHeureForfaitControl)
            {
                this.progressBarMainWindow.IsIndeterminate = true;
                this.changementTexteStatusBar("Affichage d'un relevé d'activité en cours ...");
                ((ListeReleveHeureForfaitControl)uIElement).RapportReleveActiviteParSalarie();
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Affichage d'un relevé d'activité terminé.");
            }
            if (uIElement is ListeReveleHeureAtelierControl)
            {
                this.progressBarMainWindow.IsIndeterminate = true;
                this.changementTexteStatusBar("Affichage d'un relevé d'activité en cours ...");
                ((ListeReveleHeureAtelierControl)uIElement).RapportReleveActiviteParSalarie();
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Affichage d'un relevé d'activité terminé.");
            }
        }

        private void _CommandRapportReleveActiviteParSalarie_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #endregion

        #region Actions particulières

        #region Modifier Mot de Passe

        private void _CommandModifierMotDePasse_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.progressBarMainWindow.IsIndeterminate = true;
            this.changementTexteStatusBar("Modification de son mot de passe en cours ...");

            MotDePasseWindow motDePasseWindow = new MotDePasseWindow();

            bool? dialogResult = motDePasseWindow.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Modification du mot de passe de l'utilisateur '" + ((App)App.Current)._connectedUser.Nom_Utilisateur + "' effectuée");
                }
                catch (Exception ex)
                {
                    this.progressBarMainWindow.IsIndeterminate = false;
                    this.changementTexteStatusBar("Une errreur s'est produite lors de la modification du mot de passe. Pardonnez-nous du dérangement. Contactez l'administrateur du logiciel pour plus d'informations.");
                    this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la modification du mot de passe");

                }
            }
            else
            {
                this.progressBarMainWindow.IsIndeterminate = false;
                this.changementTexteStatusBar("Modification de son mot de passe annulée.");
            }
        }

        private void _CommandModifierMotDePasse_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region Répondre congé

        private void _CommandRepondreConge_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            UIElement uIElement = this._BorderContent.Child;

            //Modification si on se trouve sur la liste des congés
            if (uIElement is ListeCongesControl)
            {
                Conge con = ((ListeCongesControl)uIElement).Repondre();

                if (con != null)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.SaveChanges();
                        this.EnvoiMailReponseConge(con);
                        ((ListeCongesControl)uIElement).MiseAJourEtat("Reponse", con);
                    }
                    catch (Exception ex)
                    {
                        this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors d'une réponse à un congé");
                    }
                }
            }
        }

        private void _CommandRepondreConge_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region Faire une demande de congé

        private void _CommandFaireDemandeConge_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            //instructions à inscrire si la commande Update peut s'executer
            UIElement uIElement = this._BorderContent.Child;

            ListeCongesControl listeCongesControl = new ListeCongesControl();

            this.AddConges(listeCongesControl, true);

            this.Cursor = ((App)App.Current)._mainCursor;
        }

        private void _CommandFaireDemandeConge_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region Ajouter du temps à un DAO

        private void _CommandAddTime_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            //instructions à inscrire si la commande Update peut s'executer
            UIElement uIElement = this._BorderContent.Child;

            //Modification si on se trouve sur la liste des DAO
            if (uIElement is ListeDAOControl)
            {
                DAO da = ((ListeDAOControl)uIElement).AddTime();

                if (da != null)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.SaveChanges();
                        ((ListeDAOControl)uIElement).MiseAJourEtat("AddTime", da);
                    }
                    catch (Exception ex)
                    {
                        this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors d'un ajout de temps à un dessin");
                    }

                }
            }

            this.Cursor = ((App)App.Current)._mainCursor;
        }

        private void _CommandAddTime_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region fusionner affaire

        private void _CommandFusionnerAffaire_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            //instructions à inscrire si la commande Update peut s'executer
            UIElement uIElement = this._BorderContent.Child;

            //Modification si on se trouve sur la liste des DAO
            if (uIElement is ListeAffaireControl)
            {
                Affaire affaire = ((ListeAffaireControl)uIElement).FusionnerAffaire();

                if (affaire != null)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Affaire.DeleteObject(affaire);
                        ((App)App.Current).mySitaffEntities.SaveChanges();
                        ((ListeAffaireControl)uIElement).MiseAJourEtat("Fusion", affaire);
                    }
                    catch (Exception ex)
                    {
                        this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la fusion d'une affaire");
                    }
                }
            }

            this.Cursor = ((App)App.Current)._mainCursor;
        }

        private void _CommandFusionnerAffaire_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (((App)App.Current)._connectedUser.Niveau_Securite1.AffaireFusion == true)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region passer affaire

        private void _CommandPasserAffaire_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            //instructions à inscrire si la commande Update peut s'executer
            UIElement uIElement = this._BorderContent.Child;

            //Modification si on se trouve sur la liste des deviss
            if (uIElement is ListeDevisControl)
            {
                Devis devis = ((ListeDevisControl)uIElement).PasserAffaire();

                if (devis != null)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.SaveChanges();
                        ((ListeDevisControl)uIElement).MiseAJourEtat("PassageAffaire", devis);
                    }
                    catch (Exception ex)
                    {
                        this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors du passage en affaire");
                    }
                }
            }

            this.Cursor = ((App)App.Current)._mainCursor;
        }

        private void _CommandPasserAffaire_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region Dupliquer

        #region Commande_Fournisseur

        private void _CommandDuplicateCommande_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            UIElement uIElement = this._BorderContent.Child;
            if (uIElement.ToString() == "SitaffRibbon.UserControls.ListeCommandeFournisseurControl")
            {
                Commande_Fournisseur commande_fournisseur = ((ListeCommandeFournisseurControl)uIElement).Duplicate();

                if (commande_fournisseur != null)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.AddToCommande_Fournisseur(commande_fournisseur);
                        ((App)App.Current).mySitaffEntities.SaveChanges();
                        ((ListeCommandeFournisseurControl)uIElement).MiseAJourEtat("Duplicate", commande_fournisseur);
                    }
                    catch (Exception ex)
                    {
                        this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la duplication d'une commande fournisseur");
                    }
                }
            }

        }

        private void _CommandDuplicateCommande_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this._BorderContent != null && this._BorderContent.Child != null)
            {
                UIElement uIElement = this._BorderContent.Child;
                e.CanExecute = this.securite.VerificationDroitActionsCRUD(uIElement.ToString(), "Add");
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region Facture

        private void _CommandDuplicateFacture_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            UIElement uIElement = this._BorderContent.Child;
            if (uIElement.ToString() == "SitaffRibbon.UserControls.ListeFactureControl")
            {
                Facture facture = ((ListeFactureControl)uIElement).Duplicate();

                if (facture != null)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.AddToFacture(facture);
                        ((App)App.Current).mySitaffEntities.SaveChanges();
                        ((ListeFactureControl)uIElement).MiseAJourEtat("Duplicate", facture);
                    }
                    catch (Exception ex)
                    {
                        this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la duplication d'une commande fournisseur");
                    }
                }
            }
        }

        private void _CommandDuplicateFacture_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this._BorderContent != null && this._BorderContent.Child != null)
            {
                UIElement uIElement = this._BorderContent.Child;
                e.CanExecute = this.securite.VerificationDroitActionsCRUD(uIElement.ToString(), "Add");
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region Relevé heure forfait

        private void _CommandDuplicateReleveHeureForfait_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            UIElement uIElement = this._BorderContent.Child;
            if (uIElement.ToString() == "SitaffRibbon.UserControls.ListeReleveHeureForfaitControl")
            {
                Releve_Heure_Forfait releve_heure_forfait = ((ListeReleveHeureForfaitControl)uIElement).Duplicate();

                if (releve_heure_forfait != null)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.AddToReleve_Heure_Forfait(releve_heure_forfait);
                        ((App)App.Current).mySitaffEntities.SaveChanges();
                        ((ListeReleveHeureForfaitControl)uIElement).MiseAJourEtat("Duplicate", releve_heure_forfait);
                    }
                    catch (Exception ex)
                    {
                        this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la duplication d'un relevé d'heures forfait");
                    }
                }
            }
        }

        private void _CommandDuplicateReleveHeureForfait_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this._BorderContent != null && this._BorderContent.Child != null)
            {
                UIElement uIElement = this._BorderContent.Child;
                e.CanExecute = this.securite.VerificationDroitActionsCRUD(uIElement.ToString(), "Add");
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region Ordre de mission

        private void _CommandDuplicateOrdreMission_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            UIElement uIElement = this._BorderContent.Child;
            if (uIElement.ToString() == "SitaffRibbon.UserControls.ListeOrdreMissionControl")
            {
                Ordre_Mission ordre_mission = ((ListeOrdreMissionControl)uIElement).Duplicate();

                if (ordre_mission != null)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.AddToOrdre_Mission(ordre_mission);
                        ((App)App.Current).mySitaffEntities.SaveChanges();
                        ((ListeOrdreMissionControl)uIElement).MiseAJourEtat("Duplicate", ordre_mission);
                    }
                    catch (Exception ex)
                    {
                        this.ErreurSauvegardeBase(ex, "Une erreur s'est produite lors de la duplication d'un ordre de mission");
                    }
                }
            }
        }

        private void _CommandDuplicateOrdreMission_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this._BorderContent != null && this._BorderContent.Child != null)
            {
                UIElement uIElement = this._BorderContent.Child;
                e.CanExecute = this.securite.VerificationDroitActionsCRUD(uIElement.ToString(), "Add");
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region Fonctions

        #region Gestion du thread de chargement

        public static void OpenLoadingForm()
        {
            Loading _loading = new Loading();
            _loading.progressBarLoading.IsIndeterminate = true;
            _loading.ShowDialog();
        }

        public void startThread()
        {
            //if (myThread == null)
            //{
            //    myThread = new Thread(new ThreadStart(OpenLoadingForm));
            //    myThread.SetApartmentState(ApartmentState.STA);
            this.Cursor = Cursors.Wait;
            //    myThread.Start();
            //}
        }

        public void stopThread()
        {
            //if (this.myThread.IsAlive)
            //{
            //    try
            //    {
            //        this.myThread.Abort();
            //        this.myThread = null;
            //        Thread.Sleep(20);
            this.Cursor = ((App)App.Current)._mainCursor;
            //        this._mutex.ReleaseMutex();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message + ". Pardonnez nous pour le dérangement, veuillez contacter votre administrateur système.", "Erreur !");
            //    }
            //}
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
            this.changementTexteStatusBar(messagePrincipal + ". Pardonnez-nous du dérangement. Contactez l'administrateur du logiciel pour plus d'informations.");
            ((App)App.Current).refreshEDMX();
        }

        #region Emails Congé

        private void EnvoiMailAuxRepondeursConge(Conge conge)
        {
            //Envoi des mails aux répondeurs
            foreach (Salarie_Repondeur sr in conge.Salarie1.Salarie_Repondeur)
            {
                String message = "Une demande de congé a été demandée par " + conge.Salarie1.Personne.fullname + ". La demande est pour les dates suivantes : " + conge.Date_Debut + " - " + conge.Date_Fin + " - " + conge.Nombre_Jours + " jours. Le motif de la demande est le suivant : " + conge.Motif_Demande1.Libelle + ". Commentaire de cette demande : '" + conge.Observation + "'. Merci d'y répondre au plus vite.";
                String objet = "Demande de congé de la part de : " + conge.Salarie1.Personne.fullname;
                Mail mail = new Mail();
                if (sr.Salarie2.Personne.EMail != null || sr.Salarie2.Personne.EMail_Pro != null)
                {
                    if (sr.Salarie2.Personne.EMail_Pro != null && sr.Salarie2.Personne.EMail_Pro != "")
                    {
                        mail.EnvoiMessage(sr.Salarie2.Personne.EMail_Pro, null, message, objet);
                    }
                    else
                    {
                        if (sr.Salarie2.Personne.EMail != null && sr.Salarie2.Personne.EMail != "")
                        {
                            mail.EnvoiMessage(sr.Salarie2.Personne.EMail, null, message, objet);
                        }
                    }
                }
                if (conge.Salarie1.Personne.EMail == null && conge.Salarie1.Personne.EMail_Pro == null)
                {
                    mail.EnvoiMessage("jlesnault-sit@groupesit.com", null, "Le personne " + conge.Salarie1.Personne.fullname + " n'a pas d'e-mail renseigné dans sa fiche salarié. Pensez à lui faire part de votre réponse ou à renseigner son adresse e-mail afin qu'il reçoive sa réponse de congé.", "Manque adresse e-mail");
                }
            }
            //Envoi d'un mail à la personne si la personne qui a demandée n'est pas la personne du congé
            if (conge.Utilisateur.Salarie_Interne1.Salarie != conge.Salarie1)
            {
                String message = "Une demande de congé a été demandée par " + conge.Utilisateur.Salarie_Interne1.Salarie.Personne.fullname + " pour vous pour les dates suivantes : " + conge.Date_Debut + " - " + conge.Date_Fin + " - " + conge.Nombre_Jours + " jours. Le motif de la demande est le suivant : " + conge.Motif_Demande1.Libelle + ". Commentaire de cette demande : '" + conge.Observation + "'. Si cette demande est exacte et provient bien d'une demande de votre part à cette personne, votre demande sera répondue au plus vite. Si seulement ce n'est pas le cas et que cette demande est une erreur, envoyez un e-mail au plus vite à M. Jean-Loup Esnault et à la personne qui a fait la demande pour vous dans les 3 jours ouvrés.";
                String objet = "Demande de congé de la part de : " + conge.Utilisateur.Salarie_Interne1.Salarie.Personne.fullname + "pour vous";
                Mail mail = new Mail();
                if (conge.Salarie1.Personne.EMail_Pro != null && conge.Salarie1.Personne.EMail_Pro != "")
                {
                    mail.EnvoiMessage(conge.Salarie1.Personne.EMail_Pro, null, message, objet);
                    if (conge.Salarie1.Personne.EMail == null && conge.Salarie1.Personne.EMail_Pro == null)
                    {
                        mail.EnvoiMessage(conge.Salarie1.Personne.EMail_Pro, null, "Le personne pour qui vous venez de faire une demande de congé " + conge.Salarie1.Personne.fullname + " n'a pas d'e-mail renseigné dans sa fiche salarié. Pensez à lui faire part de votre réponse ou à renseigner son adresse e-mail afin qu'il reçoive sa réponse de congé.", "Manque adresse e-mail");
                    }
                }
                else
                {
                    if (conge.Salarie1.Personne.EMail != null && conge.Salarie1.Personne.EMail != "")
                    {
                        mail.EnvoiMessage(conge.Salarie1.Personne.EMail, null, message, objet);
                        if (conge.Salarie1.Personne.EMail == null && conge.Salarie1.Personne.EMail_Pro == null)
                        {
                            mail.EnvoiMessage(conge.Salarie1.Personne.EMail, null, "Le personne pour qui vous venez de faire une demande de congé " + conge.Salarie1.Personne.fullname + " n'a pas d'e-mail renseigné dans sa fiche salarié. Pensez à lui faire part de votre réponse ou à renseigner son adresse e-mail afin qu'il reçoive sa réponse de congé.", "Manque adresse e-mail");
                        }
                    }
                }
            }
        }

        private void EnvoiMailReponseConge(Conge conge)
        {
            if (conge.Accepte != null)
            {
                String message = "Une réponse à été donnée à votre demande de congé des dates suivantes : " + conge.Date_Debut + " - " + conge.Date_Fin + " - " + conge.Nombre_Jours + " jours. Le motif de la demande était : " + conge.Motif_Demande1.Libelle + ". Votre demande a été ";
                if (conge.Accepte == true)
                {
                    message = message + "acceptée par " + conge.Utilisateur.Salarie_Interne1.Salarie.Personne.fullname;
                }
                else
                {
                    if (conge.Accepte == false)
                    {
                        message = message + "refusée par " + conge.Utilisateur.Salarie_Interne1.Salarie.Personne.fullname + ".";
                    }
                }
                if (conge.Commentaire != null)
                {
                    if (conge.Commentaire != "")
                    {
                        message = message + " Un commentaire a été donné à la réponse : " + conge.Commentaire;
                    }
                }
                String objet = "Réponse à votre demande de congé par " + conge.Utilisateur.Salarie_Interne1.Salarie.Personne.fullname;
                Mail mail = new Mail();
                if (conge.Salarie1.Personne.EMail != "" || conge.Salarie1.Personne.EMail_Pro != "" || conge.Salarie1.Personne.EMail != null || conge.Salarie1.Personne.EMail_Pro != null)
                {
                    if (conge.Salarie1.Personne.EMail_Pro != null && conge.Salarie1.Personne.EMail_Pro != "")
                    {
                        mail.EnvoiMessage(conge.Salarie1.Personne.EMail_Pro, null, message, objet);
                    }
                    else
                    {
                        if (conge.Salarie1.Personne.EMail != null && conge.Salarie1.Personne.EMail != "")
                        {
                            mail.EnvoiMessage(conge.Salarie1.Personne.EMail, null, message, objet);
                        }
                    }
                }
            }
        }

        #endregion

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
                    else if (((App)App.Current).personnalisation.styleVide == "4")
                    {
                        this.viderBorderContentGoutteEau();
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

        public void viderBorderContentGoutteEau()
        {
            UIElement uIElement = this._BorderContent.Child;

            //Bulles animées
            if (uIElement is GoutteEauControl)
            {

            }
            else
            {
                GoutteEauControl goutteEauControl = new GoutteEauControl();
                goutteEauControl.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                goutteEauControl.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

                this._BorderContent.Child = goutteEauControl;
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
            //Gestion d'affaires
            this.Devis.Background = System.Windows.Media.Brushes.Transparent;
            this.ResumeDevis.Background = System.Windows.Media.Brushes.Transparent;
            this.AppelOffres.Background = System.Windows.Media.Brushes.Transparent;
            this.Regies.Background = System.Windows.Media.Brushes.Transparent;
            this.Affaires.Background = System.Windows.Media.Brushes.Transparent;
            this.CommandesClients.Background = System.Windows.Media.Brushes.Transparent;
            this.BonLivraison.Background = System.Windows.Media.Brushes.Transparent;
            this.FacturesProforma.Background = System.Windows.Media.Brushes.Transparent;
            this.FacturesFournisseur.Background = System.Windows.Media.Brushes.Transparent;
            this.Factures.Background = System.Windows.Media.Brushes.Transparent;
            this.Proformaclient.Background = System.Windows.Media.Brushes.Transparent;
            this.ReglementClient.Background = System.Windows.Media.Brushes.Transparent;
            this.dao.Background = System.Windows.Media.Brushes.Transparent;
            this.Dailly.Background = System.Windows.Media.Brushes.Transparent;

            this.Devis.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.ResumeDevis.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.AppelOffres.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Regies.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Affaires.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.CommandesClients.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.BonLivraison.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.FacturesProforma.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.FacturesFournisseur.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Factures.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Proformaclient.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.ReglementClient.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.dao.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Dailly.BorderBrush = System.Windows.Media.Brushes.Transparent;

            //Gestion du personnel
            this.Salaries.Background = System.Windows.Media.Brushes.Transparent;
            this.HeuresAtelier.Background = System.Windows.Media.Brushes.Transparent;
            this.HeuresForfait.Background = System.Windows.Media.Brushes.Transparent;
            this.OrdreMission.Background = System.Windows.Media.Brushes.Transparent;
            this.Conges.Background = System.Windows.Media.Brushes.Transparent;
            this.AvanceFrais.Background = System.Windows.Media.Brushes.Transparent;
            this.Frais.Background = System.Windows.Media.Brushes.Transparent;

            this.Salaries.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.HeuresAtelier.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.HeuresForfait.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.OrdreMission.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Conges.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.AvanceFrais.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Frais.BorderBrush = System.Windows.Media.Brushes.Transparent;

            //Répertoire
            this.Entreprises.Background = System.Windows.Media.Brushes.Transparent;
            this.Contacts.Background = System.Windows.Media.Brushes.Transparent;

            this.Entreprises.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Contacts.BorderBrush = System.Windows.Media.Brushes.Transparent;

            //Mon compte
            this.ModifierMotDePasse.Background = System.Windows.Media.Brushes.Transparent;
            this.Customiser.Background = System.Windows.Media.Brushes.Transparent;
            this.FaireDemandeConge.Background = System.Windows.Media.Brushes.Transparent;
            this.MesDemandes.Background = System.Windows.Media.Brushes.Transparent;

            this.ModifierMotDePasse.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Customiser.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.FaireDemandeConge.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.MesDemandes.BorderBrush = System.Windows.Media.Brushes.Transparent;

            //Gestion atelier
            this.SortieAtelier.Background = System.Windows.Media.Brushes.Transparent;
            this.RetourChantier.Background = System.Windows.Media.Brushes.Transparent;
            this.ReservationSalle.Background = System.Windows.Media.Brushes.Transparent;

            this.SortieAtelier.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.RetourChantier.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.ReservationSalle.BorderBrush = System.Windows.Media.Brushes.Transparent;
        }

        public void changementTexteStatusBar(String actionChanged)
        {
            this.textBlockMainWindow.Text = actionChanged;
            ((App)App.Current)._actions.Actions1 += "%@%@" + System.Environment.NewLine + "\n" + DateTime.Now + " : " + actionChanged;
        }

        #endregion

        #region Evénements

        #region extension de la fonction quitter

        /// <summary>
        /// Lorsque l'on quitte l'application (alt+F4 ou croix ...)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            if (MessageBox.Show("Voulez-vous vraiment quitter l'application Sitaff ?", "Quitter", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                    ((App)App.Current)._actions.Utilisateur1 = ((App)App.Current)._connectedUser;
                    ((App)App.Current)._actions.Date_Connexion_Fin = DateTime.Now;
                    ((App)App.Current).mySitaffEntities.AddToActions(((App)App.Current)._actions);
                    ((App)App.Current).mySitaffEntities.SaveChanges();
                }
                catch (Exception) { }
                try
                {
                    this.parametreMain.Close();
                }
                catch (Exception) { }
                try
                {
                    this.parametreMain = null;
                }
                catch (Exception) { }
                try
                {
                    ((App)App.Current).threadVerifConnexion.Abort();
                    ((App)App.Current).threadVerifConnexion = null;
                }
                catch (Exception) { }
            }
        }

        #region bouton Quitter

        private void _CommandQuit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void _CommandQuit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #endregion

        #endregion

        #region Fenêtre chargée

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow = this;
            ((App)App.Current).SaveFocusedBackground = this.Devis.FocusedBackground;
            ((App)App.Current).SaveFocusedBorderBrush = this.Devis.FocusedBorderBrush;

            this.Ribbon.ShowQuickAccessToolBarOnTop = true;
            if (this.Ribbon.QuickAccessToolBar == null)
            {
                this.Ribbon.QuickAccessToolBar = new RibbonQuickAccessToolBar();
            }


            this.viderBorderContent();
        }

        #endregion

        public void Dispose()
        {
            try
            {
                this._mutex.Dispose();
            }
            catch (Exception) { }
        }

    }
}
