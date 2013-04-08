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
using System.IO;

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeAffaireControl.xaml
    /// </summary>
    public partial class ListeAffaireControl : UserControl
    {
        #region Variables

        long max = 0;

        //Les MenuItems Afficher/Masquer
        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Propriete de dependances

        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(ListeAffaireControl), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public ListeAffaireControl()
        {
            InitializeComponent();

            this._filterZone.Height = 21;

            //Initialisation des datas
            this.initialisationDataGridMain(null);

            this.initialisationAutoCompleteBox();

            this.creationMenuClicDroit();
        }

        #region Initialisation

        private void initialisationAutoCompleteBox()
        {
            //Variables temporaires
            List<String> listEntrepriseMere = new List<string>();
            List<String> listChargeAffaire = new List<string>();

            //Récupération des données
            foreach (Affaire item in listAffaire)
            {
                if (item.Salarie != null && item.Salarie.Personne != null && !listChargeAffaire.Contains(item.Salarie.Personne.fullname))
                {
                    listChargeAffaire.Add(item.Salarie.Personne.fullname);
                }
                if (item.Entreprise_Mere1 != null && !listEntrepriseMere.Contains(item.Entreprise_Mere1.Nom))
                {
                    listEntrepriseMere.Add(item.Entreprise_Mere1.Nom);
                }
            }

            //Assignation des valeurs
            this._filterContainChargeAffaire.ItemsSource = listChargeAffaire;
            this._filterContainEntrepriseMere.ItemsSource = listEntrepriseMere;
        }

        private void initialisationDataGridMain(ObservableCollection<Affaire> listToPut)
        {
            if (listToPut == null)
            {
                this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
                this.MiseAJourEtat("Filtrage", null);
            }
        }

        #region initialisation couleurs datagrid secondaires

        private void _DataGrid_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((DataGrid)sender).RowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridColor;
            ((DataGrid)sender).AlternatingRowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridAlternateColor;
        }

        #endregion

        #endregion

        #region clic droit

        private void creationMenuClicDroit()
        {
            //Création du menu
            ContextMenu contextMenu = ((App)App.Current)._menuClicDroit.creationMenuClicDroitMain(this);

            //Afficher Masquer
            contextMenu.Items.Add(new Separator());
            MenuItem menuItemAffMas = ((App)App.Current)._menuClicDroit.creationAfficherMasquer(this._DataGridMain.Columns);

            menuItemAffMas.Items.Add(new Separator());

            this.MenuItem_AfficherTout = new MenuItem();
            this.MenuItem_AfficherTout.Header = "Afficher tout";
            this.MenuItem_AfficherTout.Click += new RoutedEventHandler(delegate { this.AffMas_AfficherTout(); });
            menuItemAffMas.Items.Add(this.MenuItem_AfficherTout);

            this.MenuItem_MasquerTout = new MenuItem();
            this.MenuItem_MasquerTout.Header = "Masquer tout";
            this.MenuItem_MasquerTout.Click += new RoutedEventHandler(delegate { this.AffMas_MasquerTout(); });
            menuItemAffMas.Items.Add(this.MenuItem_MasquerTout);

            contextMenu.Items.Add(menuItemAffMas);

            contextMenu.Items.Add(new Separator());

            MenuItem itemAfficher20 = new MenuItem();
            itemAfficher20.Header = "Fusionner l'affaire";
            itemAfficher20.Click += new RoutedEventHandler(delegate { this.fusionAffaire(); });

            MenuItem itemAfficher5 = new MenuItem();
            itemAfficher5.Header = "Ouvrir le dossier";
            itemAfficher5.Click += new RoutedEventHandler(delegate { this.ouvrirDossier(); });

            MenuItem itemAfficher6 = new MenuItem();
            itemAfficher6.Header = "Imprimer le dossier de chantier";
            itemAfficher6.Click += new RoutedEventHandler(delegate { this.menuImprimerDossier(); });

            MenuItem itemAfficher7 = new MenuItem();
            itemAfficher7.Header = "DOE";

            MenuItem itemAfficher8 = new MenuItem();
            itemAfficher8.Header = "Liste fournisseurs DOE";
            itemAfficher8.Click += new RoutedEventHandler(delegate { this.RapportImprimerDOE(); });
            itemAfficher7.Items.Add(itemAfficher8);

            MenuItem itemAfficher9 = new MenuItem();
            itemAfficher9.Header = "Voir rendements";
            itemAfficher9.Click += new RoutedEventHandler(delegate { this.afficherRendements(); });

            if (((App)App.Current)._connectedUser.Niveau_Securite1.AffaireFusion == true)
            {
                contextMenu.Items.Add(itemAfficher20);
            }
            contextMenu.Items.Add(itemAfficher5);
            contextMenu.Items.Add(itemAfficher6);
            contextMenu.Items.Add(itemAfficher7);
            contextMenu.Items.Add(itemAfficher9);
            //Association du menu

            this._DataGridMain.ContextMenu = contextMenu;
        }

        private void AffMas_AfficherTout()
        {
            foreach (DataGridColumn item in this._DataGridMain.Columns)
            {
                item.Visibility = Visibility.Visible;
            }
            this.creationMenuClicDroit();
        }

        private void AffMas_MasquerTout()
        {
            foreach (DataGridColumn item in this._DataGridMain.Columns)
            {
                item.Visibility = Visibility.Collapsed;
            }
            this.creationMenuClicDroit();
        }

        private void fusionAffaire()
        {
            ((App)App.Current)._theMainWindow._CommandFusionnerAffaire.Command.Execute(((App)App.Current)._theMainWindow);
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

        private void menuImprimerDossier()
        {
            ((App)App.Current)._theMainWindow._CommandRapportImprimerDossierAffaire.Command.Execute(((App)App.Current)._theMainWindow);
        }

        private void ouvrirDossier()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    try
                    {
                        DossierAffaire dossierAffaire = new DossierAffaire();
                        if (!(new DirectoryInfo(dossierAffaire + @"\" + ((Affaire)this._DataGridMain.SelectedItem).Numero)).Exists)
                        {
                            dossierAffaire.DeplacerDossierAffaire(((Affaire)this._DataGridMain.SelectedItem).Numero);
                        }
                        dossierAffaire.OuvrirDossier(((Affaire)this._DataGridMain.SelectedItem).Numero);
                    }
                    catch (Exception) { }
                }
            }
        }

        #endregion

        #endregion

        #region Fenetre chargee

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
        /// Ajoute une nouvelle affaire à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Affaire Add()
        {
            AffaireWindow affaireWindow = new AffaireWindow();
            Affaire tmp = new Affaire();
            affaireWindow.DataContext = tmp;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = affaireWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                return (Affaire)affaireWindow.DataContext;
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Affaire)affaireWindow.DataContext);
                }
                catch (Exception)
                {
                }
                return null;
            }
        }

        /// <summary>
        /// Ouvre l'affaire séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Affaire Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    AffaireWindow affaireWindow = new AffaireWindow();
                    Affaire tmp = new Affaire();
                    affaireWindow.DataContext = tmp;

                    affaireWindow.DataContext = this._DataGridMain.SelectedItem;

                    bool? dialogResult = affaireWindow.ShowDialog();
                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Affaire)affaireWindow.DataContext;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule affaire.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une affaire.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        public Affaire RapportImprimer()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    ReportingWindow reportingWindow = new ReportingWindow();
                    long toShow = ((Affaire)this._DataGridMain.SelectedItem).Identifiant;
                    reportingWindow._webBrowser.Source = new Uri("http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fAFFAIRE%2fFiche+de+dossier&rs:Command=Render&N_AFFAIRE=" + toShow);
                    reportingWindow.Title = "Rapport pour impression : dossier d'affaire numéro - " + ((Affaire)this._DataGridMain.SelectedItem).Numero + "-";

                    reportingWindow.Show();

                    ((Affaire)this._DataGridMain.SelectedItem).Printed = true;
                    try
                    {
                        ((App)App.Current).mySitaffEntities.SaveChanges();
                    }
                    catch (Exception) { }

                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule affaire.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une affaire.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        #endregion

        #region filtrages

        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            _filterContaineNumeroAffaire.Text = "";
            _filterContainEntrepriseMere.SelectedItem = null;
            _filterContainChargeAffaire.SelectedItem = null;
            this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
        }

        private void _buttonFiltrer_Click(object sender, RoutedEventArgs e)
        {
            this.filtrage();
        }

        //Voir quelle bouton sont rempli ou non
        private void filtrage()
        {
            ((App)App.Current)._theMainWindow._mutex.WaitOne();
            ((App)App.Current)._theMainWindow.startThread();
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Filtrage en cours ...");
            this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
            if (_filterContaineNumeroAffaire.Text != "")
            {
                this.listAffaire = new ObservableCollection<Affaire>(this.listAffaire.Where(aff => aff.Numero.Trim().ToLower().Contains(_filterContaineNumeroAffaire.Text.Trim().ToLower())));
            }
            if (this._filterContainEntrepriseMere.Text != "")
            {
                this.listAffaire = new ObservableCollection<Affaire>(this.listAffaire.Where(aff => aff.Entreprise_Mere1 != null));
                this.listAffaire = new ObservableCollection<Affaire>(this.listAffaire.Where(aff => aff.Entreprise_Mere1.Nom == this._filterContainEntrepriseMere.Text));
            }
            if (this._filterContainChargeAffaire.Text != "")
            {
                this.listAffaire = new ObservableCollection<Affaire>(this.listAffaire.Where(aff => aff.Salarie != null).Where(aff => aff.Salarie.Personne != null));
                this.listAffaire = new ObservableCollection<Affaire>(this.listAffaire.Where(aff => aff.Salarie.Personne.fullname.Trim().ToLower().Contains(this._filterContainChargeAffaire.Text.ToLower().Trim()) || aff.Salarie.Personne.Initiales.Trim().ToLower().Contains(this._filterContainChargeAffaire.Text.ToLower().Trim())));
            }
            ((App)App.Current)._theMainWindow.stopThread();
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Filtrage terminé ...");
            Thread.Sleep(20);
            if (this.listAffaire.Count() == 0)
            {
                MessageBox.Show("Aucun résultat ne correspont à votre recherche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region bouton masquer / afficher

        private void _buttonMasqueFiltre_Click(object sender, RoutedEventArgs e)
        {
            AfficherMasquer();
        }

        public void AfficherMasquer()
        {

            if (_filterZone.Height != 21)
            {
                this._filterZone.Height = 21;
                this._buttonMasqueFiltre.Content = "Afficher les filtres";
                this.remiseAZero();
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._buttonMasqueFiltre.Content = "Masquer les filtres";
            }
        }

        #endregion

        #endregion

        #region Evènements

        #region DoubleClick

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

        public Affaire FusionnerAffaire()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Fusion d'une affaire en cours ...");

                    FusionnerAffaireWindow fusionnerAffaireWindow = new FusionnerAffaireWindow();
                    fusionnerAffaireWindow._ComboBoxAffaireAInclure.SelectedItem = (Affaire)this._DataGridMain.SelectedItem;

                    bool? dialogResult = fusionnerAffaireWindow.ShowDialog();
                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        Affaire affairePrincipale = (Affaire)fusionnerAffaireWindow._ComboBoxAffairePrincipale.SelectedItem;
                        Affaire affaireAInclure = (Affaire)fusionnerAffaireWindow._ComboBoxAffaireAInclure.SelectedItem;

                        if (MessageBox.Show("Voulez-vous rééllement fusionner l'affaire n° " + affaireAInclure.Numero + " dans la n° " + affairePrincipale.Numero + " ? Cette action sera irreversible !", "Fusion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {                            

                            ObservableCollection<Affaire_Chef_Chantier> listToMove1 = new ObservableCollection<Affaire_Chef_Chantier>();
                            foreach (Affaire_Chef_Chantier item in affaireAInclure.Affaire_Chef_Chantier)
                            {
                                listToMove1.Add(item);
                            }
                            foreach (Affaire_Chef_Chantier item in listToMove1)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Affaire_Type_Commande> listToMove2 = new ObservableCollection<Affaire_Type_Commande>();
                            foreach (Affaire_Type_Commande item in affaireAInclure.Affaire_Type_Commande)
                            {
                                listToMove2.Add(item);
                            }
                            foreach (Affaire_Type_Commande item in listToMove2)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Bon_Livraison> listToMove3 = new ObservableCollection<Bon_Livraison>();
                            foreach (Bon_Livraison item in affaireAInclure.Bon_Livraison)
                            {
                                listToMove3.Add(item);
                            }
                            foreach (Bon_Livraison item in listToMove3)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Commande_Fournisseur> listToMove4 = new ObservableCollection<Commande_Fournisseur>();
                            foreach (Commande_Fournisseur item in affaireAInclure.Commande_Fournisseur)
                            {
                                listToMove4.Add(item);
                            }
                            foreach (Commande_Fournisseur item in listToMove4)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<DAO> listToMove5 = new ObservableCollection<DAO>();
                            foreach (DAO item in affaireAInclure.DAO)
                            {
                                listToMove5.Add(item);
                            }
                            foreach (DAO item in listToMove5)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Facture> listToMove6 = new ObservableCollection<Facture>();
                            foreach (Facture item in affaireAInclure.Facture)
                            {
                                listToMove6.Add(item);
                            }
                            foreach (Facture item in listToMove6)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Facture_Fournisseur_Contenu> listToMove7 = new ObservableCollection<Facture_Fournisseur_Contenu>();
                            foreach (Facture_Fournisseur_Contenu item in affaireAInclure.Facture_Fournisseur_Contenu)
                            {
                                listToMove7.Add(item);
                            }
                            foreach (Facture_Fournisseur_Contenu item in listToMove7)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Facture_Proforma> listToMove8 = new ObservableCollection<Facture_Proforma>();
                            foreach (Facture_Proforma item in affaireAInclure.Facture_Proforma)
                            {
                                listToMove8.Add(item);
                            }
                            foreach (Facture_Proforma item in listToMove8)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Frais_Km> listToMove9 = new ObservableCollection<Frais_Km>();
                            foreach (Frais_Km item in affaireAInclure.Frais_Km)
                            {
                                listToMove9.Add(item);
                            }
                            foreach (Frais_Km item in listToMove9)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Heure_Atelier> listToMove10 = new ObservableCollection<Heure_Atelier>();
                            foreach (Heure_Atelier item in affaireAInclure.Heure_Atelier)
                            {
                                listToMove10.Add(item);
                            }
                            foreach (Heure_Atelier item in listToMove10)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Ligne_Fiche_Frais> listToMove11 = new ObservableCollection<Ligne_Fiche_Frais>();
                            foreach (Ligne_Fiche_Frais item in affaireAInclure.Ligne_Fiche_Frais)
                            {
                                listToMove11.Add(item);
                            }
                            foreach (Ligne_Fiche_Frais item in listToMove11)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Ordre_Mission> listToMove12 = new ObservableCollection<Ordre_Mission>();
                            foreach (Ordre_Mission item in affaireAInclure.Ordre_Mission)
                            {
                                listToMove12.Add(item);
                            }
                            foreach (Ordre_Mission item in listToMove12)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Regie> listToMove13 = new ObservableCollection<Regie>();
                            foreach (Regie item in affaireAInclure.Regie)
                            {
                                listToMove13.Add(item);
                            }
                            foreach (Regie item in listToMove13)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Relance_Facture> listToMove14 = new ObservableCollection<Relance_Facture>();
                            foreach (Relance_Facture item in affaireAInclure.Relance_Facture)
                            {
                                listToMove14.Add(item);
                            }
                            foreach (Relance_Facture item in listToMove14)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Releve_Heure_Forfait> listToMove15 = new ObservableCollection<Releve_Heure_Forfait>();
                            foreach (Releve_Heure_Forfait item in affaireAInclure.Releve_Heure_Forfait)
                            {
                                listToMove15.Add(item);
                            }
                            foreach (Releve_Heure_Forfait item in listToMove15)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Retour_Chantier> listToMove16 = new ObservableCollection<Retour_Chantier>();
                            foreach (Retour_Chantier item in affaireAInclure.Retour_Chantier)
                            {
                                listToMove16.Add(item);
                            }
                            foreach (Retour_Chantier item in listToMove16)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Sortie_Atelier> listToMove17 = new ObservableCollection<Sortie_Atelier>();
                            foreach (Sortie_Atelier item in affaireAInclure.Sortie_Atelier)
                            {
                                listToMove17.Add(item);
                            }
                            foreach (Sortie_Atelier item in listToMove17)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            ObservableCollection<Versions> listToMove18 = new ObservableCollection<Versions>();
                            foreach (Versions item in affaireAInclure.Versions)
                            {
                                listToMove18.Add(item);
                            }
                            foreach (Versions item in listToMove18)
                            {
                                item.Affaire1 = affairePrincipale;
                            }

                            return affaireAInclure;
                        }
                        else
                        {
                            //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
                            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Fusion d'une affaire annulée : " + this.listAffaire.Count() + " / " + this.max);

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

        public void RapportImprimerDOE()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    ReportingWindow reportingWindow = new ReportingWindow();
                    long toShow = ((Affaire)this._DataGridMain.SelectedItem).Identifiant;
                    reportingWindow._webBrowser.Source = new Uri("http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fAFFAIRE%2fDOE%2fListe_Fournisseur_DOE&rs:Command=Render&Affaire=" + toShow);
                    reportingWindow.Title = "Liste fournisseurs DOE";

                    reportingWindow.Show();
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

        public void afficherRendements()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    try
                    {
                        ReportingWindow reportingWindow = new ReportingWindow();
                        long toShowEntreprise_Mere = ((Affaire)this._DataGridMain.SelectedItem).Entreprise_Mere1.Identifiant;
                        long toShowCharge_Affaire = ((Affaire)this._DataGridMain.SelectedItem).Salarie.Identifiant;
                        long? toShowExercice = null;
                        foreach (Exercice item in ((App)App.Current).mySitaffEntities.Exercice)
                        {
                            if (item.Date_Debut <= DateTime.Now && item.Date_Fin >= DateTime.Now)
                            {
                                toShowExercice = item.Identifiant;
                            }
                        }
                        if (toShowExercice == null)
                        {
                            foreach (Exercice item in ((App)App.Current).mySitaffEntities.Exercice)
                            {
                                toShowExercice = item.Identifiant;
                            }
                        }
                        long toShowAffaire = ((Affaire)this._DataGridMain.SelectedItem).Identifiant;
                        reportingWindow._webBrowser.Source = new Uri("http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fRENDEMENTS%2fRendements+Alexandre&rs:Command=Render&Entreprise_Mere=" + toShowEntreprise_Mere + "&Charge_Affaire=" + toShowCharge_Affaire + "&Exercice=" + toShowExercice + "&Affaire=" + toShowAffaire + "&TEC_AU:IsNull=True");
                        reportingWindow.Title = "Rendements de l'affaire : " + ((Affaire)this._DataGridMain.SelectedItem).Numero;

                        reportingWindow.Show();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erreur, impossible d'afficher le rapport ! Vérifiez que l'affaire est bien reliée à une entreprise mère, qu'un chargé d'affaire soit bien renseigné. Sinon, contactez l'administrateur du logiciel.");
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

        /// <summary>
        /// Recalcul le nombre d'élements maximum
        /// </summary>
        private void recalculMax()
        {
            this.max = ((App)App.Current).mySitaffEntities.Affaire.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Ordre_Mission soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Affaire om)
        {
            //Je recalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Filtrage des affaires terminé : " + this.listAffaire.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute affaire dans le linsting
                this.listAffaire.Add(om);
                //Je recalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une affaire effectué avec succès. Nombre d'élements : " + this.listAffaire.Count() + " / " + this.max);
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
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification de l'affaire effectué avec succès. Nombre d'élements : " + this.listAffaire.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listAffaire.Remove(om);
                //Je recalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression de l'affaire effectué avec succès. Nombre d'élements : " + this.listAffaire.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else if (typeEtat == "Fusion")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listAffaire.Remove(om);
                //Je recalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Fusion de l'affaire effectué avec succès.");
            }
            else
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des affaires terminé : " + this.listAffaire.Count() + " / " + this.max);
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
            this.listAffaire = new ObservableCollection<Affaire>(this.listAffaire.OrderBy(ord => ord.Numero));
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
