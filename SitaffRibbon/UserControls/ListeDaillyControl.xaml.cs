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

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeDaillyControl.xaml
    /// </summary>
    public partial class ListeDaillyControl : UserControl
    {

		#region Variables

		long max = 0;

		//Les MenuItems Afficher / Masquer
		MenuItem MenuItem_AfficherTout;
		MenuItem MenuItem_MasquerTout;
		
		#endregion

		#region Propriétés de dépendances

		public ObservableCollection<Facture> listFacture
		{
			get { return (ObservableCollection<Facture>)GetValue(listFactureProperty); }
			set { SetValue(listFactureProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listFactureProperty =
			DependencyProperty.Register("listFacture", typeof(ObservableCollection<Facture>), typeof(ListeDaillyControl), new UIPropertyMetadata(null));

		public ObservableCollection<Dailly> listDailly
		{
			get { return (ObservableCollection<Dailly>)GetValue(listDaillyProperty); }
			set { SetValue(listDaillyProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listDailly.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listDaillyProperty =
			DependencyProperty.Register("listDailly", typeof(ObservableCollection<Dailly>), typeof(ListeDaillyControl), new PropertyMetadata(null));

		#endregion

		#region Constructeur

		public ListeDaillyControl()
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
			List<string> listBanque = new List<string>();
			List<string> listNumeroAffaire = new List<string>();
            List<string> listCommande = new List<string>();
            foreach (Dailly item in ((App)App.Current).mySitaffEntities.Dailly)
            {
                if (item.Banque1 != null)
                {
                    if (!listBanque.Contains(item.Banque1.Libelle))
                    {
                        listBanque.Add(item.Banque1.Libelle);
                    }
                }
                foreach (Commande itemCom in item.Commande)
                {
                    if (itemCom.getAffaire != null)
                    {
                        if (!listNumeroAffaire.Contains(itemCom.getAffaire.Numero))
                        {
                            listNumeroAffaire.Add(itemCom.getAffaire.Numero);
                        }
                    }
                    if (!listCommande.Contains(itemCom.Numero_Commande))
                    {
                        listCommande.Add(itemCom.Numero_Commande);
                    }
                }
            }			
			this._filterContainAffaire.ItemsSource = listNumeroAffaire;
            this._filterContainCommande.ItemsSource = listCommande;
			this._filterContainBanque.ItemsSource = listBanque;
		}

		#endregion

		#region initialisation Donnés datagridMain

		private void initialisationDataDatagridMain(ObservableCollection<Dailly> listToPut)
		{
			if (listToPut == null)
			{
				this.listDailly = new ObservableCollection<Dailly>(((App)App.Current).mySitaffEntities.Dailly.OrderBy(ccf => ccf.Numero));
				this.MiseAJourEtat("", null);
			}
			else
			{
				this.listDailly = new ObservableCollection<Dailly>(listToPut);
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
		
		#region Fenêtre chargée
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
		/// Ajoute un nouveau dailly à la liste à l'aide d'une nouvelle fenêtre
		/// </summary>
		public Dailly Add()
		{
			//Affichage du message "ajout en cours"
			((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
			((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un dailly en cours ...");

			//Initialisation de la fenêtre
			DaillyWindow daillyWindow = new DaillyWindow();

			//Création de l'objet temporaire
			Dailly tmp = new Dailly();

			//Mise de l'objet temporaire dans le datacontext
			daillyWindow.DataContext = tmp;


			//booléen nullable vrai ou faux ou null
			bool? dialogResult = daillyWindow.ShowDialog();

			if (dialogResult.HasValue && dialogResult.Value == true)
			{
				//Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
				return (Dailly)daillyWindow.DataContext;
			}
			else
			{
				try
				{
					//On détache la commande
					((App)App.Current).mySitaffEntities.Detach((Dailly)daillyWindow.DataContext);
				}
				catch (Exception)
				{
				}

				//Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
				((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
				this.recalculMax();
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un dailly annulé : " + this.listDailly.Count() + " / " + this.max);

				return null;
			}
		}

		/// <summary>
		/// Ouvre le dailly séléctionné à l'aide d'une nouvelle fenêtre
		/// </summary>
		public Dailly Open()
		{
			if (this._DataGridMain.SelectedItem != null)
			{
				if (this._DataGridMain.SelectedItems.Count == 1)
				{
					//Affichage du message "modification en cours"
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un dailly en cours ...");

					//Création de la fenêtre
					DaillyWindow daillyWindow = new DaillyWindow();

					//Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
					daillyWindow.DataContext = new Dailly();
					daillyWindow.DataContext = ((Dailly)this._DataGridMain.SelectedItem);

					//booléen nullable vrai ou faux ou null
					bool? dialogResult = daillyWindow.ShowDialog();

					if (dialogResult.HasValue && dialogResult.Value == true)
					{
						//Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
						return (Dailly)daillyWindow.DataContext;
					}
					else
					{
						//Je récupère les anciennes données de la base sur les modifications effectuées
						((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Dailly)(this._DataGridMain.SelectedItem));
						//La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
						((App)App.Current).refreshEDMXSansVidage();
						this.filtrage();

						//Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
						((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
						this.recalculMax();
						((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'un dailly annulée : " + this.listDailly.Count() + " / " + this.max);

						return null;
					}
				}
				else
				{
					MessageBox.Show("Vous ne devez sélectionner qu'un seul dailly.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return null;
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner un dailly.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return null;
			}
		}

		/// <summary>
		/// Supprime le dailly séléctionné avec une confirmation
		/// </summary>
		public Dailly Remove()
		{
			if (this._DataGridMain.SelectedItem != null)
			{
				if (this._DataGridMain.SelectedItems.Count == 1)
				{
					//Affichage du message "suppression en cours"
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un dailly en cours ...");
					if (MessageBox.Show("Voulez-vous rééllement supprimer le dailly séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
					{
						((Dailly)this._DataGridMain.SelectedItem).Commande = null;
						ObservableCollection<Dailly_Cession_Facture> toRemove = new ObservableCollection<Dailly_Cession_Facture>();
						foreach (Dailly_Cession_Facture item in ((Dailly)this._DataGridMain.SelectedItem).Dailly_Cession_Facture)
						{
							toRemove.Add(item);
						}
						foreach (Dailly_Cession_Facture item in toRemove)
						{
							((Dailly)this._DataGridMain.SelectedItem).Dailly_Cession_Facture.Remove(item);
						}
												

						//Supprimer l'élément 
						return (Dailly)this._DataGridMain.SelectedItem;
					}
					else
					{
						//Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
						((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
						this.recalculMax();
						((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'un dailly annulée : " + this.listDailly.Count() + " / " + this.max);

						return null;
					}

				}
				else
				{
					MessageBox.Show("Vous ne devez sélectionner qu'un seul dailly.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return null;
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner un dailly.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return null;
			}
		}

		/// <summary>
		/// Ouvre la dailly séléctionné en lecture seule à l'aide d'une nouvelle fenêtre
		/// </summary>
		public Dailly Look(Dailly d)
		{
			if (this._DataGridMain.SelectedItem != null)
			{
				if (this._DataGridMain.SelectedItems.Count == 1)
				{
					//Affichage du message "affichage en cours"
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un dailly en cours ...");

					//Création de la fenêtre
					DaillyWindow daillyWindow = new DaillyWindow();

					//Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
					daillyWindow.DataContext = new Dailly();
					daillyWindow.DataContext = (Dailly)this._DataGridMain.SelectedItem;

					//Je positionne la lecture seule sur la fenêtre
					daillyWindow.soloLecture = true;

					//J'affiche la fenêtre
					bool? dialogResult = daillyWindow.ShowDialog();

					//Affichage du message "affichage en cours"
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un dailly terminé : " + this.listDailly.Count() + " / " + this.max);

					//Renvoi null
					return null;
				}
				else
				{
					MessageBox.Show("Vous ne devez sélectionner qu'un seul dailly.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return null;
				}
			}
			else if (d != null)
			{
				//Affichage du message "affichage en cours"
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un dailly en cours ...");

					//Création de la fenêtre
					DaillyWindow daillyWindow = new DaillyWindow();

					//Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
					daillyWindow.DataContext = new Dailly();
					daillyWindow.DataContext = d;

					//Je positionne la lecture seule sur la fenêtre
					daillyWindow.soloLecture = true;

					//J'affiche la fenêtre
					bool? dialogResult = daillyWindow.ShowDialog();

					//Affichage du message "affichage en cours"
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'un dailly terminé : " + this.listDailly.Count() + " / " + this.max);

					//Renvoi null
					return null;
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner un dailly.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
			this._filterContainAccepte.IsChecked = false;
			this._filterContainBanque.Text = "";
			this._filterContainMarche.IsChecked = false;
			this._filterContainNumeroDailly.Text = "";
			this._filterContainNumeroDaillyInterne.Text = "";
			this._filterContainPublic.IsChecked = false;
			this._filterContainAffaire.Text = "";

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

			((App)App.Current)._theMainWindow.stopThread();

			ObservableCollection<Dailly> listToPut = new ObservableCollection<Dailly>(((App)App.Current).mySitaffEntities.Dailly.OrderBy(dai => dai.Numero));

			if (this._filterContainAccepte.IsChecked == true)
			{
				listToPut = new ObservableCollection<Dailly>(listToPut.Where(dai => dai.Accepte == true));
			}
			if (this._filterContainBanque.Text != "")
			{
				listToPut = new ObservableCollection<Dailly>(listToPut.Where(dai => dai.Banque1 != null));
				listToPut = new ObservableCollection<Dailly>(listToPut.Where(dai => dai.Banque1.Libelle.Trim().ToLower().Contains(this._filterContainBanque.Text.Trim().ToLower())));
			}
			if (this._filterContainMarche.IsChecked == true)
			{
				listToPut = new ObservableCollection<Dailly>(listToPut.Where(dai => dai.Marche == true));
			}
			if (this._filterContainNumeroDailly.Text != "")
			{
				listToPut = new ObservableCollection<Dailly>(listToPut.Where(dai => dai.Numero.Trim().ToLower().Contains(this._filterContainNumeroDailly.Text.Trim().ToLower())));
			}
			if (this._filterContainNumeroDaillyInterne.Text != "")
			{
				listToPut = new ObservableCollection<Dailly>(listToPut.Where(dai => dai.Numero_Interne.Trim().ToLower().Contains(this._filterContainNumeroDaillyInterne.Text.Trim().ToLower())));
			}
			if (this._filterContainPublic.IsChecked == true)
			{
				listToPut = new ObservableCollection<Dailly>(listToPut.Where(dai => dai.Publique == true));
			}
			if (this._filterContainAffaire.Text != "")
			{
				ObservableCollection<Dailly> toRemove = new ObservableCollection<Dailly>();

				foreach (Dailly item in listToPut)
				{
					bool test = false;

					foreach (Commande item1 in item.Commande)
					{
                        if (item1.getAffaire != null)
                        {
                            if (item1.getAffaire.Numero.Trim().ToLower().Contains(this._filterContainAffaire.Text.Trim().ToLower()))
                            {
                                test = true;
                            }
                        }
					}
					if (!test)
					{
						toRemove.Add(item);
					}
				}
				foreach (Dailly item in toRemove)
				{
					listToPut.Remove(item);
				}
			}
            if (this._filterContainCommande.Text != "")
            {
                ObservableCollection<Dailly> toRemove = new ObservableCollection<Dailly>();

                foreach (Dailly item in listToPut)
                {
                    bool test = false;

                    foreach (Commande item1 in item.Commande)
                    {
                        if (item1.Numero_Commande != null)
                        {
                            if (item1.Numero_Commande.Trim().ToLower().Contains(this._filterContainCommande.Text.Trim().ToLower()))
                            {
                                test = true;
                            }
                        }
                    }
                    if (!test)
                    {
                        toRemove.Add(item);
                    }
                }
                foreach (Dailly item in toRemove)
                {
                    listToPut.Remove(item);
                }
            }

			//Insertion des données dans le datagrid
			this.initialisationDataDatagridMain(listToPut);

			if (this.listDailly.Count() == 0)
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
				
				this.remiseAZero();
				
			}
			else
			{
				this._filterZone.Height = double.NaN;
				this._ButtonMasqueFiltre.Content = "Masquer les filtres";
				//Je me positionne sur le premier champ
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
			this.max = ((App)App.Current).mySitaffEntities.Dailly.Count();
		}

		/// <summary>
		/// Met à jour l'état en bas pour l'utilisateur
		/// </summary>
		/// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
		/// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
		public void MiseAJourEtat(string typeEtat, Dailly dai)
		{
			//Je racalcul le nombre max d'élements
			this.recalculMax();
			//En fonction de l'action, j'affiche le message
			if (typeEtat == "Filtrage")
			{
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Filtrage des daillys terminé : " + this.listDailly.Count() + " / " + this.max);
			}
			else if (typeEtat == "Ajout")
			{
				//J'ajoute la commande_fournisseur dans le linsting
				this.listDailly.Add(dai);
				//Je racalcul le nombre max d'élements après l'ajout
				this.recalculMax();
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'un dailly numéro '" + dai.Numero + "' effectué avec succès. Nombre d'élements : " + this.listDailly.Count() + " / " + this.max);
			}
			else if (typeEtat == "Modification")
			{
				//Je raffraichis mon datagrid
				this._DataGridMain.Items.Refresh();
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification du dailly numéro : '" + dai.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listDailly.Count() + " / " + this.max);
			}
			else if (typeEtat == "Suppression")
			{
				//Je supprime de mon listing l'élément supprimé
				this.listDailly.Remove(dai);
				//Je racalcul le nombre max d'élements après la suppression
				this.recalculMax();
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression du dailly numéro : '" + dai.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listDailly.Count() + " / " + this.max);
			}
			else if (typeEtat == "Look")
			{

			}
			else
			{
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des daillys terminé : " + this.listDailly.Count() + " / " + this.max);
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
			this.listDailly = new ObservableCollection<Dailly>(this.listDailly.OrderBy(nf => nf.Numero));
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
