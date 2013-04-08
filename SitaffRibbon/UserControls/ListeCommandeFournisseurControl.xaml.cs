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
    /// Logique d'interaction pour ListeCommandeFournisseurControl.xaml
    /// </summary>
    public partial class ListeCommandeFournisseurControl : UserControl
    {

        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Propriétés de dépendance

        public ObservableCollection<Commande_Fournisseur> listCommandes
        {
            get { return (ObservableCollection<Commande_Fournisseur>)GetValue(listCommandesProperty); }
            set { SetValue(listCommandesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listCommandes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listCommandesProperty =
            DependencyProperty.Register("listCommandes", typeof(ObservableCollection<Commande_Fournisseur>), typeof(ListeCommandeFournisseurControl), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public ListeCommandeFournisseurControl()
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
            List<string> listLieuLivraison = new List<string>();
            List<string> listAffaire = new List<string>();
            List<string> listFournisseur = new List<string>();
            List<string> listDonneurOrdre = new List<string>();
            foreach (Commande_Fournisseur item in ((App)App.Current).mySitaffEntities.Commande_Fournisseur)
            {
                //Pour remplir les entreprise de livraison
                if (item.Entreprise1 != null)
                {
                    if (!listLieuLivraison.Contains(item.Entreprise1.Libelle))
                    {
                        listLieuLivraison.Add(item.Entreprise1.Libelle);
                    }
                }

                //Pour remplir les affaires
                if (item.Affaire1 != null)
                {
                    if (!listAffaire.Contains(item.Affaire1.Numero))
                    {
                        listAffaire.Add(item.Affaire1.Numero);
                    }
                }

                //Pour remplir les fournisseurs
                if (item.Fournisseur1 != null)
                {
                    if (item.Fournisseur1.Entreprise != null)
                    {
                        if (!listFournisseur.Contains(item.Fournisseur1.Entreprise.Libelle))
                        {
                            listFournisseur.Add(item.Fournisseur1.Entreprise.Libelle);
                        }
                    }
                }

                //Pour remplir les donneurs d'ordre
                if (item.Salarie != null)
                {
                    if (item.Salarie.Personne != null)
                    {
                        if (!listDonneurOrdre.Contains(item.Salarie.Personne.fullname))
                        {
                            listDonneurOrdre.Add(item.Salarie.Personne.fullname);
                        }
                    }
                }
            }

            _filterContainLieuLivraison.ItemsSource = listLieuLivraison;
            _filterContainNumeroAffaire.ItemsSource = listAffaire;
            _filterContainFournisseur.ItemsSource = listFournisseur;
            _filterContainDonneurOrdre.ItemsSource = listDonneurOrdre;
        }

        private void initialisationComboBoxOuiNon()
        {
            ObservableCollection<ItemOuiNon> listItemOuiNon = new ObservableCollection<ItemOuiNon>();
            listItemOuiNon.Add(new ItemOuiNon("Oui"));
            listItemOuiNon.Add(new ItemOuiNon("Non"));
            this._filterContainToutFacture.ItemsSource = listItemOuiNon;
            this._filterContainToutLivre.ItemsSource = listItemOuiNon;
        }

        #endregion

        #region initialisation Donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<Commande_Fournisseur> listToPut)
        {
            if (listToPut == null)
            {
                this.listCommandes = new ObservableCollection<Commande_Fournisseur>(((App)App.Current).mySitaffEntities.Commande_Fournisseur.Where(com => com.Mission_Tiers.Count() == 0).OrderByDescending(ccf => ccf.Date_Commande).ThenByDescending(ccf => ccf.Identifiant));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listCommandes = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Mission_Tiers.Count() == 0));
                this.MiseAJourEtat("Filtrage", null);
            }
        }

        #endregion

        #region clic droit

        private void creationMenuClicDroit()
        {
            //Création du menu
            ContextMenu contextMenu = ((App)App.Current)._menuClicDroit.creationMenuClicDroitMain(this);

            //Zone actions particulières

            MenuItem itemAfficher5 = new MenuItem();
            itemAfficher5.Header = "Dupliquer";
            itemAfficher5.Click += new RoutedEventHandler(delegate { ((App)App.Current)._theMainWindow._CommandDuplicateCommande.Command.Execute(((App)App.Current)._theMainWindow); });

            MenuItem itemAfficherCommandeAvecPrix = new MenuItem();
            itemAfficherCommandeAvecPrix.Header = "Commande avec prix";

            MenuItem itemAfficher6 = new MenuItem();
            itemAfficher6.Header = "Voir / Imprimer";
            itemAfficher6.Click += new RoutedEventHandler(delegate { ((App)App.Current)._theMainWindow._CommandRapportImprimerCommande.Command.Execute(((App)App.Current)._theMainWindow); });
            itemAfficherCommandeAvecPrix.Items.Add(itemAfficher6);

            //TO DO
            //MenuItem itemAfficher8 = new MenuItem();
            //itemAfficher8.Header = "Envoyer par mail";
            //itemAfficher8.Click += new RoutedEventHandler(delegate { this.envoyerParMailCommandeAvecPrix(); });
            //itemAfficherCommandeAvecPrix.Items.Add(itemAfficher8);

            MenuItem itemAfficher9 = new MenuItem();
            itemAfficher9.Header = "Sauvegarder en pdf";
            itemAfficher9.Click += new RoutedEventHandler(delegate { this.sauvegarderCommandeAvecPrix(); });
            itemAfficherCommandeAvecPrix.Items.Add(itemAfficher9);

            MenuItem itemAfficher7 = new MenuItem();
            itemAfficher7.Header = "Imprimer sans les prix";
            itemAfficher7.Click += new RoutedEventHandler(delegate { ((App)App.Current)._theMainWindow._CommandRapportImprimerCommandeSansPrix.Command.Execute(((App)App.Current)._theMainWindow); });

            contextMenu.Items.Add(new Separator());
            if (((App)App.Current).securite.VerificationDroitActionsCRUD(this.ToString(), "Add"))
            {
                contextMenu.Items.Add(itemAfficher5);
                contextMenu.Items.Add(new Separator());
            }
            contextMenu.Items.Add(itemAfficherCommandeAvecPrix);
            contextMenu.Items.Add(itemAfficher7);

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

        #endregion

        #endregion

        #region Fenêtre chargée

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
            ((App)App.Current)._theMainWindow.stopThread();
        }

        #endregion

        #region CRUD (Create Read Update Delete)

        /// <summary>
        /// Ajoute une nouvelle Commande_Fournisseur à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Commande_Fournisseur Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une commande fournisseur en cours ...");

            //Initialisation de la fenêtre
            ChoixTypeCommandeWindow choixTypeCommandeWindows = new ChoixTypeCommandeWindow();
            CommandeWindow commandeWindow = new CommandeWindow();

            //Création de l'objet temporaire
            Commande_Fournisseur tmp = new Commande_Fournisseur();
            tmp.Utilisateur1 = ((App)App.Current)._connectedUser;

            //Mise de l'objet temporaire dans le datacontext
            commandeWindow.DataContext = tmp;

            //Définition du type de commande
            bool? dialogChoix = choixTypeCommandeWindows.ShowDialog();

            if (dialogChoix.HasValue && dialogChoix.Value == true)
            {
                if (choixTypeCommandeWindows.affaire != null)
                {
                    ((Commande_Fournisseur)commandeWindow.DataContext).Affaire1 = choixTypeCommandeWindows.affaire;
                }
                if (choixTypeCommandeWindows.stock == true)
                {
                    ((Commande_Fournisseur)commandeWindow.DataContext).Stock = true;
                }
                if (choixTypeCommandeWindows.divers == true)
                {
                    ((Commande_Fournisseur)commandeWindow.DataContext).Divers = true;
                }
                ((Commande_Fournisseur)commandeWindow.DataContext).Entreprise_Mere1 = choixTypeCommandeWindows.entreprise_mere;

                //booléen nullable vrai ou faux ou null
                bool? dialogResult = commandeWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    //Si j'appuie sur le bouton Ok, je renvoi l'objet commande se trouvant dans le datacontext de la fenêtre
                    return (Commande_Fournisseur)commandeWindow.DataContext;
                }
                else
                {
                    try
                    {
                        //On détache tous les élements liés à la commande Commande_Fournisseur_Condition_Reglement
                        ObservableCollection<Commande_Fournisseur_Condition_Reglement> toRemove = new ObservableCollection<Commande_Fournisseur_Condition_Reglement>();
                        foreach (Commande_Fournisseur_Condition_Reglement item in ((Commande_Fournisseur)commandeWindow.DataContext).Commande_Fournisseur_Condition_Reglement)
                        {
                            toRemove.Add(item);
                        }
                        foreach (Commande_Fournisseur_Condition_Reglement item in toRemove)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }

                        //On détache tous les élements liés à la commande Contenu_Commande_Fournisseur
                        ObservableCollection<Contenu_Commande_Fournisseur> toRemove1 = new ObservableCollection<Contenu_Commande_Fournisseur>();
                        foreach (Contenu_Commande_Fournisseur item in ((Commande_Fournisseur)commandeWindow.DataContext).Contenu_Commande_Fournisseur)
                        {
                            toRemove1.Add(item);
                        }
                        foreach (Contenu_Commande_Fournisseur item in toRemove1)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }

                        //On détache tous les élements liés à la commande Bon_Livraison
                        ObservableCollection<Bon_Livraison> toRemove2 = new ObservableCollection<Bon_Livraison>();
                        foreach (Bon_Livraison item in ((Commande_Fournisseur)commandeWindow.DataContext).Bon_Livraison)
                        {
                            toRemove2.Add(item);
                        }
                        foreach (Bon_Livraison item in toRemove2)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }

                        //On détache tous les élements liés à la commande Facture_Proforma
                        ObservableCollection<Facture_Proforma> toRemove3 = new ObservableCollection<Facture_Proforma>();
                        foreach (Facture_Proforma item in ((Commande_Fournisseur)commandeWindow.DataContext).Facture_Proforma)
                        {
                            toRemove3.Add(item);
                        }
                        foreach (Facture_Proforma item in toRemove3)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }

                        //On détache la commande
                        ((App)App.Current).mySitaffEntities.Detach((Commande_Fournisseur)commandeWindow.DataContext);
                    }
                    catch (Exception)
                    {
                    }

                    //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    this.recalculMax();
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une commande fournisseur annulé : " + this.listCommandes.Count() + " / " + this.max);

                    return null;
                }
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Commande_Fournisseur)commandeWindow.DataContext);
                    //Pas besoin de détacher le reste car ici l'utilisateur annule avant qu'il n'ait pu insérer quoi que ce soit
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une commande fournisseur annulé : " + this.listCommandes.Count() + " / " + this.max);

                return null;
            }
        }

        /// <summary>
        /// Ouvre la commande fournisseur séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Commande_Fournisseur Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une commande fournisseur en cours ...");

                    //Création de la fenêtre
                    CommandeWindow commandeWindow = new CommandeWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    commandeWindow.DataContext = new Commande_Fournisseur();
                    commandeWindow.DataContext = (Commande_Fournisseur)this._DataGridMain.SelectedItem;
                    ((Commande_Fournisseur)commandeWindow.DataContext).Utilisateur1 = ((App)App.Current)._connectedUser;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = commandeWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                        return (Commande_Fournisseur)commandeWindow.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Commande_Fournisseur)(this._DataGridMain.SelectedItem));
                        //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une commande fournisseur annulée : " + this.listCommandes.Count() + " / " + this.max);

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
        /// Supprime la commande fournisseur séléctionnée avec une confirmation
        /// </summary>
        public Commande_Fournisseur Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une commande fournisseur en cours ...");

                    bool test = true;
                    if (test)
                    {
                        if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Bon_Livraison.Count() != 0)
                        {
                            test = false;
                            MessageBox.Show("Vous ne pouvez supprimer cette commande car des bon de livraisons y sont associés", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
                            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression impossible, cause : commande possède des bons de livraison : " + this.listCommandes.Count() + " / " + this.max);
                        }
                    }
                    if (test)
                    {
                        if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Facture_Proforma.Count() != 0)
                        {
                            test = false;
                            MessageBox.Show("Vous ne pouvez supprimer cette commande car des factures proforma y sont associés", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
                            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression impossible, cause : commande possède des factures proforma : " + this.listCommandes.Count() + " / " + this.max);
                        }
                    }
                    if (test)
                    {
                        foreach (Contenu_Commande_Fournisseur item in ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Contenu_Commande_Fournisseur)
                        {
                            if (item.Facture_Fournisseur_Contenu.Count() > 0 && test)
                            {
                                test = false;
                                MessageBox.Show("Vous ne pouvez supprimer cette commande car du contenu est associé à des factures fournisseur y sont associés", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                                this.recalculMax();
                                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression impossible, cause : contenu associé à des factures fournisseur : " + this.listCommandes.Count() + " / " + this.max);
                            }
                        }
                    }

                    if (test)
                    {
                        if (MessageBox.Show("Voulez-vous rééllement supprimer la commande fournisseur séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            //On détache tous les élements liés à la commande Commande_Fournisseur_Condition_Reglement
                            ObservableCollection<Commande_Fournisseur_Condition_Reglement> toRemove = new ObservableCollection<Commande_Fournisseur_Condition_Reglement>();
                            foreach (Commande_Fournisseur_Condition_Reglement item in ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Commande_Fournisseur_Condition_Reglement)
                            {
                                toRemove.Add(item);
                            }
                            foreach (Commande_Fournisseur_Condition_Reglement item in toRemove)
                            {
                                ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Commande_Fournisseur_Condition_Reglement.Remove(item);
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache tous les élements liés à la commande Contenu_Commande_Fournisseur
                            ObservableCollection<Contenu_Commande_Fournisseur> toRemove1 = new ObservableCollection<Contenu_Commande_Fournisseur>();
                            foreach (Contenu_Commande_Fournisseur item in ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Contenu_Commande_Fournisseur)
                            {
                                toRemove1.Add(item);
                            }
                            foreach (Contenu_Commande_Fournisseur item in toRemove1)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache tous les élements liés à la commande Bon_Livraison
                            ObservableCollection<Bon_Livraison> toRemove2 = new ObservableCollection<Bon_Livraison>();
                            foreach (Bon_Livraison item in ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Bon_Livraison)
                            {
                                toRemove2.Add(item);
                            }
                            foreach (Bon_Livraison item in toRemove2)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache tous les élements liés à la commande Facture_Proforma
                            ObservableCollection<Facture_Proforma> toRemove3 = new ObservableCollection<Facture_Proforma>();
                            foreach (Facture_Proforma item in ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Facture_Proforma)
                            {
                                toRemove3.Add(item);
                            }
                            foreach (Facture_Proforma item in toRemove3)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //Supprimer l'élément 
                            return (Commande_Fournisseur)this._DataGridMain.SelectedItem;
                        }
                        else
                        {
                            //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
                            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une commande fournisseur annulée : " + this.listCommandes.Count() + " / " + this.max);

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

        /// <summary>
        /// Ouvre la commande fournisseur séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Commande_Fournisseur Look(Commande_Fournisseur commande_fournisseur)
        {
            if (this._DataGridMain.SelectedItem != null || commande_fournisseur != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1 || commande_fournisseur != null)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une commande fournisseur en cours ...");

                    //Création de la fenêtre
                    CommandeWindow commandeWindow = new CommandeWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    commandeWindow.DataContext = new Commande_Fournisseur();
                    if (commande_fournisseur != null)
                    {
                        commandeWindow.DataContext = commande_fournisseur;
                    }
                    else
                    {
                        commandeWindow.DataContext = (Commande_Fournisseur)this._DataGridMain.SelectedItem;
                    }

                    //Je positionne la lecture seule sur la fenêtre
                    commandeWindow.lectureSeule();
                    commandeWindow.soloLecture = true;

                    //J'affiche la fenêtre
                    bool? dialogResult = commandeWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une commande fournisseur terminé : " + this.listCommandes.Count() + " / " + this.max);

                    //Renvoi null
                    return null;
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

        #endregion

        #region Actions supplémentaires

        /// <summary>
        /// duplique une Commande_Fournisseur à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Commande_Fournisseur Duplicate()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "ajout en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer une commande fournisseur en cours ...");

                    //Création de la fenêtre
                    CommandeWindow commandeWindow = new CommandeWindow();

                    //Duplication de la commande sélectionnée
                    Commande_Fournisseur tmp = new Commande_Fournisseur();
                    tmp = duplicateCommande((Commande_Fournisseur)this._DataGridMain.SelectedItem);

                    //Activation de la comboBox Affaire si la commande sélectionnée était sur une affaire
                    if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Affaire1 != null)
                    {
                        commandeWindow._comboBoxAffaire.IsEnabled = true;
                    }

                    //Association de l'élement dupliqué au datacontext de la fenêtre
                    commandeWindow.DataContext = tmp;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = commandeWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Commande_Fournisseur)commandeWindow.DataContext;
                    }
                    else
                    {
                        try
                        {
                            //On détache tous les élements liés à la commande Commande_Fournisseur_Condition_Reglement
                            ObservableCollection<Commande_Fournisseur_Condition_Reglement> toRemove = new ObservableCollection<Commande_Fournisseur_Condition_Reglement>();
                            foreach (Commande_Fournisseur_Condition_Reglement item in ((Commande_Fournisseur)commandeWindow.DataContext).Commande_Fournisseur_Condition_Reglement)
                            {
                                toRemove.Add(item);
                            }
                            foreach (Commande_Fournisseur_Condition_Reglement item in toRemove)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache tous les élements liés à la commande Contenu_Commande_Fournisseur
                            ObservableCollection<Contenu_Commande_Fournisseur> toRemove1 = new ObservableCollection<Contenu_Commande_Fournisseur>();
                            foreach (Contenu_Commande_Fournisseur item in ((Commande_Fournisseur)commandeWindow.DataContext).Contenu_Commande_Fournisseur)
                            {
                                toRemove1.Add(item);
                            }
                            foreach (Contenu_Commande_Fournisseur item in toRemove1)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache tous les élements liés à la commande Bon_Livraison
                            ObservableCollection<Bon_Livraison> toRemove2 = new ObservableCollection<Bon_Livraison>();
                            foreach (Bon_Livraison item in ((Commande_Fournisseur)commandeWindow.DataContext).Bon_Livraison)
                            {
                                toRemove2.Add(item);
                            }
                            foreach (Bon_Livraison item in toRemove2)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache tous les élements liés à la commande Facture_Proforma
                            ObservableCollection<Facture_Proforma> toRemove3 = new ObservableCollection<Facture_Proforma>();
                            foreach (Facture_Proforma item in ((Commande_Fournisseur)commandeWindow.DataContext).Facture_Proforma)
                            {
                                toRemove3.Add(item);
                            }
                            foreach (Facture_Proforma item in toRemove3)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }

                            //On détache la commande
                            ((App)App.Current).mySitaffEntities.Detach((Commande_Fournisseur)commandeWindow.DataContext);
                        }
                        catch (Exception)
                        {
                        }

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer une commande fournisseur annulé : " + this.listCommandes.Count() + " / " + this.max);

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
        /// Ouvre le rapport commande séléctionné
        /// </summary>
        public Commande_Fournisseur RapportImprimer()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    ReportingWindow reportingWindow = new ReportingWindow();
                    long toShow = ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Identifiant;
                    reportingWindow._webBrowser.Source = new Uri("http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fACHATS%2fCOMMANDE+FOURNITURE&rs:Command=Render&Commande_Fournisseur=" + toShow + "&affichage_montant=true");
                    reportingWindow.Title = "Rapport pour impression : commande n° - " + ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Numero + "-";

                    reportingWindow.Show();
                    return null;
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

        public void envoyerParMailCommandeAvecPrix()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    DownloadFileURL downloadFileURL = new DownloadFileURL();
                    long toShow = ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Identifiant;
                    downloadFileURL.ModificationTexte("Commande fournisseur n ° " + ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Numero);
                    downloadFileURL.urlToDownload = "http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fACHATS%2fCOMMANDE+FOURNITURE&rs:Command=Render&Commande_Fournisseur=" + toShow + "&affichage_montant=true&rs:Format=PDF";
                    downloadFileURL.nomFichier = "Commande fournisseur n ° " + ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Numero;
                    downloadFileURL.TelechargementFichier();
                    try
                    {
                        downloadFileURL.ShowDialog();
                    }
                    catch (Exception) { }

                    string A = "";
                    if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Contact1 != null)
                    {
                        if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Contact1.Personne.EMail_Pro != null)
                        {
                            A = ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Contact1.Personne.EMail_Pro;
                        }
                        else
                        {
                            if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Contact1.Personne.EMail != null)
                            {
                                A = ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Contact1.Personne.EMail;
                            }
                            else
                            {
                                if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Contact1.Personne.Entreprise1 != null)
                                {
                                    if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Contact1.Personne.Entreprise1.EMail != null)
                                    {
                                        A = ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Contact1.Personne.Entreprise1.EMail;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Fournisseur1 != null)
                        {
                            if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Fournisseur1.Entreprise != null)
                            {
                                if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Fournisseur1.Entreprise.EMail != null)
                                {
                                    A = ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Fournisseur1.Entreprise.EMail;
                                }
                            }
                        }
                    }

                    EnvoyerMail envoyerMail = new EnvoyerMail();
                    envoyerMail._textBoxA.Text = A;
                    envoyerMail._textBoxPJ.Text = "n° " + ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Numero;
                    envoyerMail._textBoxObjet.Text = "Commande n ° " + ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Numero;
                    envoyerMail.pj = downloadFileURL.PositionFichier + @"\" + downloadFileURL.nomFichier + ".pdf";
                    try
                    {
                        //TO DO
                        envoyerMail.adresseAMettre = "";
                    }
                    catch (Exception) { }
                    if (((App)App.Current)._connectedUser.Salarie_Interne1.Salarie != ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Salarie)
                    {
                        if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Salarie != null)
                        {
                            if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Salarie.Personne != null)
                            {
                                if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Salarie.Personne.EMail_Pro != null && ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Salarie.Personne.EMail_Pro != "")
                                {
                                    try
                                    {
                                        envoyerMail.cc = ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Salarie.Personne.EMail_Pro;
                                    }
                                    catch (Exception) { }
                                }
                                else
                                {
                                    if (((Commande_Fournisseur)this._DataGridMain.SelectedItem).Salarie.Personne.EMail != null && ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Salarie.Personne.EMail != "")
                                    {
                                        try
                                        {
                                            envoyerMail.cc = ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Salarie.Personne.EMail;
                                        }
                                        catch (Exception) { }
                                    }
                                }
                            }
                        }
                    }
                    envoyerMail.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule commande fournisseur.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une commande fournisseur.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            //MessageBox.Show("En cours de réalisation");
        }

        public void sauvegarderCommandeAvecPrix()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    DownloadFileURL downloadFileURL = new DownloadFileURL();
                    long toShow = ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Identifiant;
                    downloadFileURL.ModificationTexte("Commande fournisseur n ° " + ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Numero);
                    downloadFileURL.urlToDownload = "http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fACHATS%2fCOMMANDE+FOURNITURE&rs:Command=Render&Commande_Fournisseur=" + toShow + "&affichage_montant=true&rs:Format=PDF";
                    downloadFileURL.nomFichier = "Commande fournisseur n ° " + ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Numero;
                    downloadFileURL.PositionFichier = "download";
                    downloadFileURL.TelechargementFichier();
                    try
                    {
                        downloadFileURL.ShowDialog();
                    }
                    catch (Exception) { }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule commande fournisseur.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une commande fournisseur.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Ouvre le rapport commande séléctionné
        /// </summary>
        public Commande_Fournisseur RapportImprimerSansPrix()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    ReportingWindow reportingWindow = new ReportingWindow();
                    long toShow = ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Identifiant;
                    reportingWindow._webBrowser.Source = new Uri("http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fACHATS%2fCOMMANDE+FOURNITURE&rs:Command=Render&Commande_Fournisseur=" + toShow + "&affichage_montant=false");
                    reportingWindow.Title = "Rapport pour impression : commande n° - " + ((Commande_Fournisseur)this._DataGridMain.SelectedItem).Numero + "-";

                    reportingWindow.Show();
                    return null;
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
            //Text
            _filterContainNumeroCommande.Text = "";
            _filterContainLieuLivraison.Text = "";
            _filterContainNumeroAffaire.Text = "";
            _filterContainNatureCommande.Text = "";
            _filterContainFournisseur.Text = "";
            _filterContainDonneurOrdre.Text = "";
            _filterContainMontant.Text = "";

            //Dates
            this._filterContainDateCommande.SelectedDate = null;
            this._filterContainDateDebutCommande.SelectedDate = null;
            this._filterContainDateFinCommande.SelectedDate = null;

            //ComboBox
            this._filterContainToutLivre.SelectedItem = null;
            this._filterContainToutFacture.SelectedItem = null;

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

            ObservableCollection<Commande_Fournisseur> listToPut = new ObservableCollection<Commande_Fournisseur>(((App)App.Current).mySitaffEntities.Commande_Fournisseur.OrderBy(ccf => ccf.Numero));

            if (this._filterContainNumeroCommande.Text != "")
            {
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Numero.Trim().ToLower().Contains(this._filterContainNumeroCommande.Text.Trim().ToLower())));
            }
            if (this._filterContainLieuLivraison.Text != "")
            {
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Entreprise1 != null));
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Entreprise1.Libelle.Trim().ToLower().Contains(this._filterContainLieuLivraison.Text.Trim().ToLower())));
            }
            if (this._filterContainNumeroAffaire.Text != "")
            {
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Affaire1 != null));
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Affaire1.Numero.Trim().ToLower().Contains(this._filterContainNumeroAffaire.Text.Trim().ToLower())));
            }
            if (this._filterContainNatureCommande.Text != "")
            {
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Nature != null));
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Nature.Trim().ToLower().Contains(this._filterContainNatureCommande.Text.Trim().ToLower())));
            }
            if (this._filterContainFournisseur.Text != "")
            {
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Fournisseur1 != null));
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Fournisseur1.Entreprise != null));
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Fournisseur1.Entreprise.Libelle.Trim().ToLower().Contains(this._filterContainFournisseur.Text.Trim().ToLower())));
            }
            if (this._filterContainDonneurOrdre.Text != "")
            {
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Salarie != null));
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Salarie.Personne != null));
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Salarie.Personne.fullname.Trim().ToLower().Contains(this._filterContainDonneurOrdre.Text.Trim().ToLower()) || com.Salarie.Personne.Initiales.Trim().ToLower().Contains(this._filterContainDonneurOrdre.Text.Trim().ToLower())));
            }
            if (this._filterContainMontant.Text != "")
            {
                double val;
                if (double.TryParse(this._filterContainMontant.Text.Trim(), out val))
                {
                    listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.MontantCommande != null));
                    listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.MontantCommande.ToString().Contains(double.Parse(this._filterContainMontant.Text.Trim()).ToString())));
                }
            }
            if (this._filterContainToutFacture.SelectedItem != null)
            {
                bool toTest;
                if (((ItemOuiNon)this._filterContainToutFacture.SelectedItem).chaine == "Oui")
                {
                    toTest = false;
                }
                else
                {
                    toTest = true;
                }
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.ToutPasseEnFacture != toTest));
            }
            if (this._filterContainToutLivre.SelectedItem != null)
            {
                bool toTest;
                if (((ItemOuiNon)this._filterContainToutLivre.SelectedItem).chaine == "Oui")
                {
                    toTest = true;
                }
                else
                {
                    toTest = false;
                }
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.ToutPasseEnBL != toTest));
            }
            if (this._filterContainDateCommande.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Date_Commande != null));
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Date_Commande.Value.Year == this._filterContainDateCommande.SelectedDate.Value.Year && com.Date_Commande.Value.Month == this._filterContainDateCommande.SelectedDate.Value.Month && com.Date_Commande.Value.Day == this._filterContainDateCommande.SelectedDate.Value.Day));
            }
            if (this._filterContainDateDebutCommande.SelectedDate != null)
            {
                DateTime temp = new DateTime(this._filterContainDateDebutCommande.SelectedDate.Value.Year, this._filterContainDateDebutCommande.SelectedDate.Value.Month, this._filterContainDateDebutCommande.SelectedDate.Value.Day, 00, 00, 00);
                this._filterContainDateDebutCommande.SelectedDate = temp;
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Date_Commande != null));
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Date_Commande >= this._filterContainDateDebutCommande.SelectedDate));
            }
            if (this._filterContainDateFinCommande.SelectedDate != null)
            {
                DateTime temp = new DateTime(this._filterContainDateFinCommande.SelectedDate.Value.Year, this._filterContainDateFinCommande.SelectedDate.Value.Month, this._filterContainDateFinCommande.SelectedDate.Value.Day, 23, 59, 59);
                this._filterContainDateFinCommande.SelectedDate = temp;
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Date_Commande != null));
                listToPut = new ObservableCollection<Commande_Fournisseur>(listToPut.Where(com => com.Date_Commande <= this._filterContainDateFinCommande.SelectedDate));
            }

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            //Si aucun résultat, j'affiche un message
            if (this.listCommandes.Count() == 0)
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
                if (_filterContainNumeroCommande.Text != "" || _filterContainLieuLivraison.Text != "" || _filterContainNumeroAffaire.Text != "" || _filterContainNatureCommande.Text != "" || _filterContainFournisseur.Text != "" || _filterContainDonneurOrdre.Text != "" || _filterContainMontant.Text != "" || _filterContainToutFacture.SelectedItem != null || _filterContainToutLivre.SelectedItem != null || _filterContainDateCommande.SelectedDate != null || _filterContainDateDebutCommande.SelectedDate != null || _filterContainDateFinCommande.SelectedDate != null || this.max != this.listCommandes.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainNumeroCommande.Focus();
            }
        }

        #endregion

        #region null comboBox

        private void NullToutLivre_Click_1(object sender, RoutedEventArgs e)
        {
            this._filterContainToutLivre.SelectedItem = null;
        }

        private void NullToutFacture_Click_1(object sender, RoutedEventArgs e)
        {
            this._filterContainToutFacture.SelectedItem = null;
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
            this.max = ((App)App.Current).mySitaffEntities.Commande_Fournisseur.Where(com => com.Mission_Tiers.Count() == 0).Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Commande_Fournisseur cf)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des commandes fournisseur terminée : " + this.listCommandes.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listCommandes.Add(cf);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une commande fournisseur numéro '" + cf.Numero + "' effectué avec succès. Nombre d'élements : " + this.listCommandes.Count() + " / " + this.max);
                try
                {
                    this._DataGridMain.SelectedItem = cf;
                }
                catch (Exception) { }
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification de la commande fournisseur numéro : '" + cf.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listCommandes.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listCommandes.Remove(cf);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression de la commande fournisseur numéro : '" + cf.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listCommandes.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else if (typeEtat == "Duplicate")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listCommandes.Add(cf);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer une commande fournisseur numéro '" + cf.Numero + "' effectué avec succès. Nombre d'élements : " + this.listCommandes.Count() + " / " + this.max);
            }
            else
            {
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des commandes fournisseur terminé : " + this.listCommandes.Count() + " / " + this.max);
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
            this.listCommandes = new ObservableCollection<Commande_Fournisseur>(this.listCommandes.OrderByDescending(ccf => ccf.Date_Commande).ThenByDescending(ccf => ccf.Identifiant));
        }

        /// <summary>
        /// duplique la commande passée en paramètre
        /// </summary>
        /// <param name="commande1">commande à dupliquer</param>
        private Commande_Fournisseur duplicateCommande(Commande_Fournisseur itemToCopy)
        {
            Commande_Fournisseur tmp = new Commande_Fournisseur();

            tmp.Affaire1 = itemToCopy.Affaire1;
            tmp.Contact1 = itemToCopy.Contact1;
            tmp.Entreprise = itemToCopy.Entreprise;
            tmp.Entreprise1 = itemToCopy.Entreprise1;
            tmp.Adresse = itemToCopy.Adresse;
            tmp.Fournisseur1 = itemToCopy.Fournisseur1;
            tmp.Salarie = itemToCopy.Salarie;
            tmp.Personne = itemToCopy.Personne;
            tmp.Total_Commande = itemToCopy.Total_Commande;
            tmp.Total_Ramene_A = itemToCopy.Total_Ramene_A;
            tmp.Remise = itemToCopy.Remise;
            tmp.Nature = itemToCopy.Nature;
            tmp.Type_Commande1 = itemToCopy.Type_Commande1;
            tmp.Franco = itemToCopy.Franco;
            tmp.Contact2 = itemToCopy.Contact2;
            tmp.Remise_Somme = itemToCopy.Remise_Somme;
            tmp.Liv_Atelier = itemToCopy.Liv_Atelier;
            tmp.Liv_Autre = itemToCopy.Liv_Autre;
            tmp.Liv_Chantier = itemToCopy.Liv_Chantier;
            tmp.Divers = itemToCopy.Divers;
            tmp.Stock = itemToCopy.Stock;
            tmp.Entreprise_Mere1 = itemToCopy.Entreprise_Mere1;
            foreach (Contenu_Commande_Fournisseur ccf in itemToCopy.Contenu_Commande_Fournisseur)
            {
                Contenu_Commande_Fournisseur toAdd = new Contenu_Commande_Fournisseur();
                toAdd.Reference = ccf.Reference;
                toAdd.Designation = ccf.Designation;
                toAdd.Quantite = ccf.Quantite;
                toAdd.Prix_Remise = ccf.Prix_Remise;
                toAdd.Prix_Unitaire = ccf.Prix_Unitaire;
                toAdd.Prix_Total = ccf.Prix_Total;
                toAdd.Taux_Remise = ccf.Taux_Remise;
                toAdd.Description = ccf.Description;
                tmp.Contenu_Commande_Fournisseur.Add(toAdd);
            }
            foreach (Commande_Fournisseur_Condition_Reglement cr in itemToCopy.Commande_Fournisseur_Condition_Reglement)
            {
                Commande_Fournisseur_Condition_Reglement toAdd = new Commande_Fournisseur_Condition_Reglement();
                toAdd.Condition_Reglement1 = cr.Condition_Reglement1;
                toAdd.Commentaire = cr.Commentaire;
                tmp.Commande_Fournisseur_Condition_Reglement.Add(toAdd);
            }
            tmp.Utilisateur1 = ((App)App.Current)._connectedUser;

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
