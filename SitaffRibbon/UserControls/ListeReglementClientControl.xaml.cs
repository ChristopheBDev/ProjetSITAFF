using SitaffRibbon.Classes;
using SitaffRibbon.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeReglementClientControl.xaml
    /// </summary>
    public partial class ListeReglementClientControl : UserControl
    {
		
        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;
		
        #endregion

        #region Propriétés de dépendances

		public ObservableCollection<Reglement_Client> listRgltClient
		{
			get { return (ObservableCollection<Reglement_Client>)GetValue(listRgltClientProperty); }
			set { SetValue(listRgltClientProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listRgltClient.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listRgltClientProperty = 
			DependencyProperty.Register("listRgltClient", typeof(ObservableCollection<Reglement_Client>), typeof(ListeReglementClientControl), new PropertyMetadata(null));
		
        #endregion

        #region Constructeur

        public ListeReglementClientControl()
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
            List<string> listClient = new List<string>();

            foreach (Reglement_Client item in ((App)App.Current).mySitaffEntities.Reglement_Client)
            {
               
            }

            _filterContainClient.ItemsSource = listClient;
        }

        #endregion

        #region initialisation Donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<Reglement_Client> listToPut)
        {
            if (listToPut == null)
            {
				this.listRgltClient = new ObservableCollection<Reglement_Client>(((App)App.Current).mySitaffEntities.Reglement_Client.OrderBy(rglt => rglt.Date_Reglement));
                this.MiseAJourEtat("", null);
            }
            else
            {
				this.listRgltClient = new ObservableCollection<Reglement_Client>(listToPut);
                this.MiseAJourEtat("Filtrage", null);
            }
        }

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
        /// Ajoute une nouvelle Reglement_Client à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Reglement_Client Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un réglement en cours ...");

            //Initialisation de la fenêtre
            ReglementClientWindow rgltClientWindow = new ReglementClientWindow();

            //Création de l'objet temporaire
            Reglement_Client tmp = new Reglement_Client();
			tmp.Reglement_Client_Facture = new System.Data.Objects.DataClasses.EntityCollection<Reglement_Client_Facture>();

            //Mise de l'objet temporaire dans le datacontext
            rgltClientWindow.DataContext = tmp;


            //booléen nullable vrai ou faux ou null
            bool? dialogResult = rgltClientWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                return (Reglement_Client)rgltClientWindow.DataContext;
            }
            else
            {
                try
                {
                    //On détache la commande
                    ((App)App.Current).mySitaffEntities.Detach((Reglement_Client)rgltClientWindow.DataContext);
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un réglement annulé : " + this.listRgltClient.Count() + " / " + this.max);

                return null;
            }
        }

        /// <summary>
        /// Ouvre le Reglement_Client séléctionné à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Reglement_Client Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un réglement en cours ...");
					//Création de la fenêtre
                    ReglementClientWindow rgltClientWindow = new ReglementClientWindow();
					rgltClientWindow.DataContext = (Reglement_Client)this._DataGridMain.SelectedItem;
					
                    bool? dialogResult = rgltClientWindow.ShowDialog();
					if (dialogResult.HasValue && dialogResult.Value == true)
					{
						//Si j'appuie sur le bouton Ok, je renvoi l'objet se trouvant dans le datacontext de la fenêtre
						return (Reglement_Client)rgltClientWindow.DataContext;
					}
					else
					{
						//Je récupère les anciennes données de la base sur les modifications effectuées
						((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Reglement_Client)(this._DataGridMain.SelectedItem));
						//La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
						((App)App.Current).refreshEDMXSansVidage();
						this.filtrage();

						//Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
						((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
						this.recalculMax();
						((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un règlement annulée : " + this.listRgltClient.Count() + " / " + this.max);

						return null;
					}
                }
                else
                {
					MessageBox.Show("Vous ne devez sélectionner qu'un seul réglement.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
				MessageBox.Show("Vous devez sélectionner un réglement.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
		/// Supprime le Reglement_Client séléctionné avec une confirmation
        /// </summary>
        public Reglement_Client Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un règlement en cours ...");
					if (MessageBox.Show("Voulez-vous rééllement supprimer le règlement séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
					{
                        ObservableCollection<Reglement_Client_Facture> toRemove = new ObservableCollection<Reglement_Client_Facture>();
                        foreach (Reglement_Client_Facture item in ((Reglement_Client)this._DataGridMain.SelectedItem).Reglement_Client_Facture)
                        {
                            toRemove.Add(item);
                        }
                        foreach (Reglement_Client_Facture item in toRemove)
                        {
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Reglement_Client_Facture.DeleteObject(item);
                            }
                            catch (Exception)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                            }
                        }
					}

					return (Reglement_Client)this._DataGridMain.SelectedItem;

                }
                else
                {
					MessageBox.Show("Vous ne devez sélectionner qu'un seul règlement.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
				MessageBox.Show("Vous devez sélectionner un règlement.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
		/// Ouvre le Reglement_Client séléctionné en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Reglement_Client Look(Reglement_Client reg)
        {
            if (this._DataGridMain.SelectedItem != null || reg != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un réglement en cours ...");

                    //Création de la fenêtre
                    ReglementClientWindow rgltClientWindow = new ReglementClientWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    rgltClientWindow.DataContext = new Reglement_Client();
					if (reg == null)
					{
						rgltClientWindow.DataContext = (Reglement_Client)this._DataGridMain.SelectedItem;
					}
					else
					{
						rgltClientWindow.DataContext = reg;
					}

                    //Je positionne la lecture seule sur la fenêtre
                    rgltClientWindow.lectureSeule();
                    rgltClientWindow.soloLecture = true;

                    //J'affiche la fenêtre
                    bool? dialogResult = rgltClientWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un réglement terminé : " + this.listRgltClient.Count() + " / " + this.max);

                    //Renvoi null
                    return null;
                }
                else
                {
					MessageBox.Show("Vous ne devez sélectionner qu'un seul réglement.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
				MessageBox.Show("Vous devez sélectionner un réglement.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        #endregion

        #region filtrage

        #region remise a zero
        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {

            this._filterContainClient.Text = "";
			this._filterContainDateDebutRglt.SelectedDate = null;
			this._filterContainDateFinRglt.SelectedDate = null;
			this._filterContainMontantMaxi.Text = "";
			this._filterContainMontantMini.Text = "";
			this._filterContainMoyenRglt.Text = "";
			this._filterContainNumeroFacture.Text = "";
			this._filterContainReference.Text = "";
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

            ObservableCollection<Reglement_Client> listToPut = new ObservableCollection<Reglement_Client>(((App)App.Current).mySitaffEntities.Reglement_Client.OrderBy(reg => reg.Date_Reglement));

            if (this._filterContainNumeroFacture.Text != "")
            {
				ObservableCollection<Reglement_Client> listToPutBis = new ObservableCollection<Reglement_Client>();
				foreach (Reglement_Client item in listToPut)
				{
					foreach (Reglement_Client_Facture item2 in item.Reglement_Client_Facture)
					{
						if (item2.Facture1.Numero.Trim().ToLower().Contains(this._filterContainNumeroFacture.Text.Trim().ToLower()))
						{
							listToPutBis.Add(item);
						}
					}
				}
				listToPut = new ObservableCollection<Reglement_Client>(listToPutBis);
            }
            if (this._filterContainClient.Text != "")
            {
				listToPut = new ObservableCollection<Reglement_Client>(listToPut.Where(reg => reg.Client1 != null));
				listToPut = new ObservableCollection<Reglement_Client>(listToPut.Where(reg => reg.Client1.Entreprise.Libelle.Trim().ToLower().Contains(_filterContainClient.Text.Trim().ToLower())));
			}
			if (this._filterContainMoyenRglt.Text != "")
			{
				listToPut = new ObservableCollection<Reglement_Client>(listToPut.Where(reg => reg.Moyen_Reglement1.Libelle.Trim().ToLower().Contains(_filterContainMoyenRglt.Text.Trim().ToLower())));
			}
			if (this._filterContainReference.Text != "")
			{
				listToPut = new ObservableCollection<Reglement_Client>(listToPut.Where(reg => reg.Commentaire.Trim().ToLower().Contains(_filterContainReference.Text.Trim().ToLower())));
			}
			if (this._filterContainMontantMini.Text != "")
			{
				double val;
				if (double.TryParse(this._filterContainMontantMini.Text, out val))
				{
					listToPut = new ObservableCollection<Reglement_Client>(listToPut.Where(fac => fac.Montant != null));
					listToPut = new ObservableCollection<Reglement_Client>(listToPut.Where(fac => fac.Montant >= (double.Parse(this._filterContainMontantMini.Text.Trim()))));
				}
			}
			if (this._filterContainMontantMaxi.Text != "")
			{
				double val;
				if (double.TryParse(this._filterContainMontantMini.Text, out val))
				{
					listToPut = new ObservableCollection<Reglement_Client>(listToPut.Where(fac => fac.Montant != null));
					listToPut = new ObservableCollection<Reglement_Client>(listToPut.Where(fac => fac.Montant <= (double.Parse(this._filterContainMontantMaxi.Text.Trim()))));
				}
			}
            if (this._filterContainDateDebutRglt.SelectedDate != null)
            {
				DateTime temp = new DateTime(this._filterContainDateDebutRglt.SelectedDate.Value.Year, this._filterContainDateDebutRglt.SelectedDate.Value.Month, this._filterContainDateDebutRglt.SelectedDate.Value.Day, 00, 00, 00);
				this._filterContainDateDebutRglt.SelectedDate = temp;
                listToPut = new ObservableCollection<Reglement_Client>(listToPut.Where(com => com.Date_Reglement != null));
				listToPut = new ObservableCollection<Reglement_Client>(listToPut.Where(com => com.Date_Reglement >= this._filterContainDateDebutRglt.SelectedDate));
            }
            if (this._filterContainDateFinRglt.SelectedDate != null)
            {
				DateTime temp = new DateTime(this._filterContainDateFinRglt.SelectedDate.Value.Year, this._filterContainDateFinRglt.SelectedDate.Value.Month, this._filterContainDateFinRglt.SelectedDate.Value.Day, 23, 59, 59);
				this._filterContainDateFinRglt.SelectedDate = temp;
                listToPut = new ObservableCollection<Reglement_Client>(listToPut.Where(com => com.Date_Reglement != null));
				listToPut = new ObservableCollection<Reglement_Client>(listToPut.Where(com => com.Date_Reglement <= this._filterContainDateFinRglt.SelectedDate));
            }

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            if (this.listRgltClient.Count() == 0)
            {
                MessageBox.Show("Aucun résultat ne correspont à votre recherche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
                this.remiseAZero();
                
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainClient.Focus();
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
			this.max = ((App)App.Current).mySitaffEntities.Reglement_Client.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Reglement_Client rglt)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
				((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des factures terminée : " + this.listRgltClient.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listRgltClient.Add(rglt);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un réglement effectué avec succès. Nombre d'élements : " + this.listRgltClient.Count() + " / " + this.max);
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification du réglement effectuée avec succès. Nombre d'élements : " + this.listRgltClient.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listRgltClient.Remove(rglt);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression du réglement effectuée avec succès. Nombre d'élements : " + this.listRgltClient.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else
            {
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des réglements terminé : " + this.listRgltClient.Count() + " / " + this.max);
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
            this.listRgltClient = new ObservableCollection<Reglement_Client>(this.listRgltClient.OrderBy(reg => reg.Date_Reglement));
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
