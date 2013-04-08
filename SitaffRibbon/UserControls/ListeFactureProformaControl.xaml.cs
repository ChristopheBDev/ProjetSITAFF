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
using System.Threading;
using System.Collections.ObjectModel;
using SitaffRibbon.Windows;
using SitaffRibbon.Classes;


namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeFactureProformaControl.xaml
    /// </summary>
    public partial class ListeFactureProformaControl : UserControl
    {

        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_ColonneNumeroFacture;
        MenuItem MenuItem_ColonneNumeroAffaire;
        MenuItem MenuItem_ColonneNumeroCommande;
        MenuItem MenuItem_ColonneDate;
        MenuItem MenuItem_ColonneMontant;
        MenuItem MenuItem_ColonneNormalisee;
        MenuItem MenuItem_ColonneFournisseur;

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region proprieté de dependances

        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(ListeFactureProformaControl), new UIPropertyMetadata(null));



        public ObservableCollection<Commande_Fournisseur> listCommande
        {
            get { return (ObservableCollection<Commande_Fournisseur>)GetValue(listCommandeProperty); }
            set { SetValue(listCommandeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listCommande.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listCommandeProperty =
            DependencyProperty.Register("listCommande", typeof(ObservableCollection<Commande_Fournisseur>), typeof(ListeFactureProformaControl), new UIPropertyMetadata(null));



        public ObservableCollection<Facture_Proforma> listFactureProforma
        {
            get { return (ObservableCollection<Facture_Proforma>)GetValue(listFactureProformaProperty); }
            set { SetValue(listFactureProformaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listFactureProforma.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFactureProformaProperty =
            DependencyProperty.Register("listFactureProforma", typeof(ObservableCollection<Facture_Proforma>), typeof(ListeFactureProformaControl), new UIPropertyMetadata(null));

        #endregion

        #region constructeur

        public ListeFactureProformaControl()
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
            this._filterZone.Height = 21;
        }


        private void initialisationAutoCompleteBox()
        {
            List<string> listAffaire = new List<string>();
            List<string> listCommande = new List<string>();
            foreach (Facture_Proforma item in ((App)App.Current).mySitaffEntities.Facture_Proforma)
            {
                //Pour remplir les affaires
                if (item.Affaire1 != null)
                {
                    if (!listAffaire.Contains(item.Affaire1.Numero))
                    {
                        listAffaire.Add(item.Affaire1.Numero);
                    }
                }

                //Pour remplir les commandes
                if (item.Fournisseur1 != null)
                {
                    if (!listCommande.Contains(item.Fournisseur1.Entreprise.Libelle))
                    {
                        listCommande.Add(item.Fournisseur1.Entreprise.Libelle);
                    }
                }


            }

            _filterContainAffaire.ItemsSource = listAffaire;
            _filterContainCommande.ItemsSource = listCommande;
        }

        #endregion

        #region initialisation Donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<Facture_Proforma> listToPut)
        {
            if (listToPut == null)
            {
                this.listFactureProforma = new ObservableCollection<Facture_Proforma>(((App)App.Current).mySitaffEntities.Facture_Proforma.OrderBy(ccf => ccf.Numero));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listFactureProforma = new ObservableCollection<Facture_Proforma>(listToPut);
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
            this.MenuItem_ColonneNumeroFacture = new MenuItem();
            this.MenuItem_ColonneNumeroFacture.IsChecked = false;
            this.MenuItem_ColonneNumeroFacture.Header = "Numéro facture";
            this.MenuItem_ColonneNumeroFacture.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumeroFacture(); });
            this.AffMas_ColonneNumeroFacture();
            menuItem.Items.Add(this.MenuItem_ColonneNumeroFacture);

            this.MenuItem_ColonneMontant = new MenuItem();
            this.MenuItem_ColonneMontant.IsChecked = false;
            this.MenuItem_ColonneMontant.Header = "Montant";
            this.MenuItem_ColonneMontant.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneMontant(); });
            this.AffMas_ColonneMontant();
            menuItem.Items.Add(this.MenuItem_ColonneMontant);

            this.MenuItem_ColonneNumeroAffaire = new MenuItem();
            this.MenuItem_ColonneNumeroAffaire.IsChecked = false;
            this.MenuItem_ColonneNumeroAffaire.Header = "Numéro de l'affaire";
            this.MenuItem_ColonneNumeroAffaire.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumeroAffaire(); });
            this.AffMas_ColonneNumeroAffaire();
            menuItem.Items.Add(this.MenuItem_ColonneNumeroAffaire);

            this.MenuItem_ColonneNumeroCommande = new MenuItem();
            this.MenuItem_ColonneNumeroCommande.IsChecked = false;
            this.MenuItem_ColonneNumeroCommande.Header = "Numéro de commande";
            this.MenuItem_ColonneNumeroCommande.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNumeroCommande(); });
            this.AffMas_ColonneNumeroCommande();
            menuItem.Items.Add(this.MenuItem_ColonneNumeroCommande);

            this.MenuItem_ColonneNormalisee = new MenuItem();
            this.MenuItem_ColonneNormalisee.IsChecked = false;
            this.MenuItem_ColonneNormalisee.Header = "Normalisée ?";
            this.MenuItem_ColonneNormalisee.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNormalisee(); });
            this.AffMas_ColonneNormalisee();
            menuItem.Items.Add(this.MenuItem_ColonneNormalisee);

            this.MenuItem_ColonneDate = new MenuItem();
            this.MenuItem_ColonneDate.IsChecked = false;
            this.MenuItem_ColonneDate.Header = "Date";
            this.MenuItem_ColonneDate.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDate(); });
            this.AffMas_ColonneDate();
            menuItem.Items.Add(this.MenuItem_ColonneDate);

            this.MenuItem_ColonneFournisseur = new MenuItem();
            this.MenuItem_ColonneFournisseur.IsChecked = false;
            this.MenuItem_ColonneFournisseur.Header = "Fournisseur";
            this.MenuItem_ColonneFournisseur.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFournisseur(); });
            this.AffMas_ColonneFournisseur();
            menuItem.Items.Add(this.MenuItem_ColonneFournisseur);


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

        #endregion

        #region afficher / masquer

        #region Tout

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneNumeroCommande.IsChecked = false;
            this.MenuItem_ColonneNumeroAffaire.IsChecked = false;
            this.MenuItem_ColonneMontant.IsChecked = false;
            this.MenuItem_ColonneDate.IsChecked = false;
            this.MenuItem_ColonneNormalisee.IsChecked = false;
            this.MenuItem_ColonneNumeroFacture.IsChecked = false;
            this.MenuItem_ColonneFournisseur.IsChecked = false;

            this.AffMas_ColonneNumeroCommande();
            this.AffMas_ColonneNumeroAffaire();
            this.AffMas_ColonneMontant();
            this.AffMas_ColonneNumeroFacture();
            this.AffMas_ColonneDate();
            this.AffMas_ColonneNormalisee();
            this.AffMas_ColonneFournisseur();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneNumeroCommande.IsChecked = true;
            this.MenuItem_ColonneNumeroAffaire.IsChecked = true;
            this.MenuItem_ColonneMontant.IsChecked = true;
            this.MenuItem_ColonneDate.IsChecked = true;
            this.MenuItem_ColonneNormalisee.IsChecked = true;
            this.MenuItem_ColonneNumeroFacture.IsChecked = true;
            this.MenuItem_ColonneFournisseur.IsChecked = true;


            this.AffMas_ColonneNumeroCommande();
            this.AffMas_ColonneNumeroAffaire();
            this.AffMas_ColonneMontant();
            this.AffMas_ColonneNumeroFacture();
            this.AffMas_ColonneDate();
            this.AffMas_ColonneNormalisee();
            this.AffMas_ColonneFournisseur();


        }

        #endregion

        private void AffMas_ColonneNumeroCommande()
        {
            if (this.MenuItem_ColonneNumeroCommande.IsChecked == true)
            {
                this._ColonneNumeroCommande.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNumeroCommande.IsChecked = false;
            }
            else
            {
                this._ColonneNumeroCommande.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNumeroCommande.IsChecked = true;
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

        private void AffMas_ColonneMontant()
        {
            if (this.MenuItem_ColonneMontant.IsChecked == true)
            {
                this._ColonneMontant.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneMontant.IsChecked = false;
            }
            else
            {
                this._ColonneMontant.Visibility = Visibility.Visible;
                this.MenuItem_ColonneMontant.IsChecked = true;
            }
        }

        private void AffMas_ColonneNumeroFacture()
        {
            if (this.MenuItem_ColonneNumeroFacture.IsChecked == true)
            {
                this._ColonneNumeroFacture.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNumeroFacture.IsChecked = false;
            }
            else
            {
                this._ColonneNumeroFacture.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNumeroFacture.IsChecked = true;
            }
        }

        private void AffMas_ColonneDate()
        {
            if (this.MenuItem_ColonneDate.IsChecked == true)
            {
                this._ColonneDate.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDate.IsChecked = false;
            }
            else
            {
                this._ColonneDate.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDate.IsChecked = true;
            }
        }

        private void AffMas_ColonneNormalisee()
        {
            if (this.MenuItem_ColonneNormalisee.IsChecked == true)
            {
                this._ColonneNormalisee.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNormalisee.IsChecked = false;
            }
            else
            {
                this._ColonneNormalisee.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNormalisee.IsChecked = true;
            }
        }

        private void AffMas_ColonneFournisseur()
        {
            if (this.MenuItem_ColonneFournisseur.IsChecked == true)
            {
                this._ColonneFournisseur.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFournisseur.IsChecked = false;
            }
            else
            {
                this._ColonneFournisseur.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFournisseur.IsChecked = true;
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
        public Facture_Proforma Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture proforma en cours ...");

            //Initialisation de la fenêtre
            FactureProformaWindow factureproformaWindow = new FactureProformaWindow();

            //Création de l'objet temporaire
            Facture_Proforma tmp = new Facture_Proforma();

            //Mise de l'objet temporaire dans le datacontext
            factureproformaWindow.DataContext = tmp;

            MessageBoxResult resultatDemande = MessageBox.Show("Une commande est-elle associée à votre facture proforma ?", "Association commande ?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultatDemande == MessageBoxResult.Yes)
            {
                SelectionTypeBL selectionTypeBL = new SelectionTypeBL();
                selectionTypeBL.Title = "Sélection commande";
                selectionTypeBL.testSupp = true;
                selectionTypeBL._comboBoxAffaire.Visibility = Visibility.Collapsed;
                selectionTypeBL._comboBoxDonneurOrdre.Visibility = Visibility.Collapsed;
                selectionTypeBL._textBlockAffaire.Visibility = Visibility.Collapsed;
                selectionTypeBL._textBlockDonneurOrdre.Visibility = Visibility.Collapsed;
                bool? dialogDemandeTypeBL = selectionTypeBL.ShowDialog();

                if (dialogDemandeTypeBL.HasValue && dialogDemandeTypeBL.Value == true)
                {
                    ((Facture_Proforma)factureproformaWindow.DataContext).Commande_Fournisseur1 = selectionTypeBL.commande_fournisseur;
                    if (selectionTypeBL.commande_fournisseur.Affaire1 != null)
                    {
                        ((Facture_Proforma)factureproformaWindow.DataContext).Affaire1 = selectionTypeBL.commande_fournisseur.Affaire1;
                    }
                    ((Facture_Proforma)factureproformaWindow.DataContext).Fournisseur1 = selectionTypeBL.commande_fournisseur.Fournisseur1;
                }
                else
                {
                    //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture proforma annulé : " + this.listFactureProforma.Count() + " / " + this.max);

                    return null;
                }
            }
            else if (resultatDemande == MessageBoxResult.No)
            {
                ChoixTypeCommandeWindow choixTypeCommandeWindows = new ChoixTypeCommandeWindow();
                choixTypeCommandeWindows.Title = "Sélection type facture proforma";
                bool? dialogChoix = choixTypeCommandeWindows.ShowDialog();

                if (dialogChoix.HasValue && dialogChoix.Value == true)
                {
                    if (choixTypeCommandeWindows.affaire != null)
                    {
                        ((Facture_Proforma)factureproformaWindow.DataContext).Affaire1 = choixTypeCommandeWindows.affaire;
                    }
                    if (choixTypeCommandeWindows.stock == true)
                    {
                        ((Facture_Proforma)factureproformaWindow.DataContext).Stock = true;
                    }
                    if (choixTypeCommandeWindows.divers == true)
                    {
                        ((Facture_Proforma)factureproformaWindow.DataContext).Divers = true;
                    }
                    ((Facture_Proforma)factureproformaWindow.DataContext).Entreprise_Mere1 = choixTypeCommandeWindows.entreprise_mere;
                }
                else
                {
                    //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture proforma annulé : " + this.listFactureProforma.Count() + " / " + this.max);

                    return null;
                }
            }
            else
            {
                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture proforma annulé : " + this.listFactureProforma.Count() + " / " + this.max);

                return null;
            }

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = factureproformaWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet facture se trouvant dans le datacontext de la fenêtre
                return (Facture_Proforma)factureproformaWindow.DataContext;
            }
            else
            {
                try
                {
                    //On détache la facture proforma
                    ((App)App.Current).mySitaffEntities.Detach((Facture_Proforma)factureproformaWindow.DataContext);
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture proforma annulé : " + this.listFactureProforma.Count() + " / " + this.max);

                return null;
            }
        }

        /// <summary>
        /// Ouvre la facture séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Facture_Proforma Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une facture proforma en cours ...");

                    //Création de la fenêtre
                    FactureProformaWindow factureproformaWindow = new FactureProformaWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    factureproformaWindow.DataContext = new Facture_Proforma();
                    factureproformaWindow.DataContext = (Facture_Proforma)this._DataGridMain.SelectedItem;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = factureproformaWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet facture se trouvant dans le datacontext de la fenêtre
                        return (Facture_Proforma)factureproformaWindow.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Facture_Proforma)(this._DataGridMain.SelectedItem));
                        //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une facture proforma annulée : " + this.listFactureProforma.Count() + " / " + this.max);

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule facture proforma.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une facture proforma.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime la facture séléctionnée avec une confirmation
        /// </summary>
        public Facture_Proforma Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une facture proforma en cours ...");

                    if (MessageBox.Show("Voulez-vous rééllement supprimer la facture proforma séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //Supprimer l'élément 
                        return (Facture_Proforma)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une facture proforma annulée : " + this.listFactureProforma.Count() + " / " + this.max);

                        return null;
                    }

                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule facture proforma.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une facture proforma.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre la facture séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Facture_Proforma Look()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une facture proforma en cours ...");

                    //Création de la fenêtre
                    FactureProformaWindow factureproformaWindow = new FactureProformaWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la facture proforma sélectionnée
                    factureproformaWindow.DataContext = new Facture_Proforma();
                    factureproformaWindow.DataContext = (Facture_Proforma)this._DataGridMain.SelectedItem;

                    //Je positionne la lecture seule sur la fenêtre
                    factureproformaWindow.lectureSeule();
                    factureproformaWindow.soloLecture = true;

                    //J'affiche la fenêtre
                    bool? dialogResult = factureproformaWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une facture proforma terminée : " + this.listFactureProforma.Count() + " / " + this.max);

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule facture proforma.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une facture proforma.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        #endregion

        #region actions supplementaires

        #endregion

        #region Filtrage

        #region remise a zero
        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            _filterContainNumeroFacture.Text = "";
            _filterContainMontant.Text = "";
            _filterContainAffaire.Text = "";
            _filterContainCommande.Text = "";

            _filterContainDate.SelectedDate = null;

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

            ObservableCollection<Facture_Proforma> listToPut = new ObservableCollection<Facture_Proforma>(((App)App.Current).mySitaffEntities.Facture_Proforma.OrderBy(ccf => ccf.Numero));

            if (this._filterContainCommande.Text != "")
            {
                listToPut = new ObservableCollection<Facture_Proforma>(listToPut.Where(com => com.Numero.Trim().ToLower().Contains(this._filterContainCommande.Text.Trim().ToLower())));
            }

            if (this._filterContainAffaire.Text != "")
            {
                listToPut = new ObservableCollection<Facture_Proforma>(listToPut.Where(aff => aff.Affaire1 != null));
                listToPut = new ObservableCollection<Facture_Proforma>(listToPut.Where(aff => aff.Affaire1.Numero.Trim().ToLower().Contains(this._filterContainAffaire.Text.Trim().ToLower())));
            }

            if (this._filterContainNumeroFacture.Text != "")
            {
                listToPut = new ObservableCollection<Facture_Proforma>(listToPut.Where(fac => fac.Numero.Trim().ToLower().Contains(this._filterContainNumeroFacture.Text.Trim().ToLower())));
            }

            if (this._filterContainMontant.Text != "")
            {
                double val;
                if (double.TryParse(this._filterContainMontant.Text.Trim(), out val))
                {
                    listToPut = new ObservableCollection<Facture_Proforma>(listToPut.Where(mon => mon.Montant != null));
                    listToPut = new ObservableCollection<Facture_Proforma>(listToPut.Where(mon => mon.Montant.ToString().Contains(double.Parse(this._filterContainMontant.Text.Trim()).ToString())));
                }
            }
            if (this._filterContainDate.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Facture_Proforma>(listToPut.Where(fac => fac.Date_Facture != null));
                listToPut = new ObservableCollection<Facture_Proforma>(listToPut.Where(fac => fac.Date_Facture == this._filterContainDate.SelectedDate));
            }

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            //Si aucun résultat, j'affiche un message
            if (this.listFactureProforma.Count() == 0)
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
                if (_filterContainNumeroFacture.Text != "" || _filterContainMontant.Text != "" || _filterContainAffaire.Text != "" || _filterContainCommande.Text != "" || _filterContainDate.SelectedDate != null || this.max != this.listFactureProforma.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainNumeroFacture.Focus();
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
            this.max = ((App)App.Current).mySitaffEntities.Facture_Proforma.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Facture_Proforma soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Facture_Proforma facp)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des factures proforma terminée : " + this.listFactureProforma.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listFactureProforma.Add(facp);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture proforma numéro '" + facp.Numero + "' effectué avec succès. Nombre d'élements : " + this.listFactureProforma.Count() + " / " + this.max);
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification de la facture proforma numéro : '" + facp.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listFactureProforma.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listFactureProforma.Remove(facp);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression de la facture proforma numéro : '" + facp.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listFactureProforma.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else
            {
((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des factures proforma terminé : " + this.listFactureProforma.Count() + " / " + this.max);
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
            this.listFactureProforma = new ObservableCollection<Facture_Proforma>(this.listFactureProforma.OrderBy(nf => nf.Numero));
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
