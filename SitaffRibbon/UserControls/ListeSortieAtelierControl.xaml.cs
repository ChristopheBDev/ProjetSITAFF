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
using SitaffRibbon.Classes;
using System.Collections.ObjectModel;
using System.Threading;

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeSortieAtelierControl.xaml
    /// </summary>
    public partial class ListeSortieAtelierControl : UserControl
    {
		#region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

		#region Propriétés de dépendances

		public ObservableCollection<Sortie_Atelier> listSorties
		{
			get { return (ObservableCollection<Sortie_Atelier>)GetValue(listSortiesProperty); }
			set { SetValue(listSortiesProperty, value); }
		}

		// Using a DependencyProperty as the backing store for listSorties.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listSortiesProperty =
			DependencyProperty.Register("listSorties", typeof(ObservableCollection<Sortie_Atelier>), typeof(ListeSortieAtelierControl), new UIPropertyMetadata(null));

		#endregion

		#region Constructeur

		public ListeSortieAtelierControl()
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
		
		#region initialisation zone de filtrage

		private void initialisationFilterZone()
		{
			this._filterZone.Height = 21;
			initialisationAutoCompleteBox();
		}

		private void initialisationAutoCompleteBox()
		{
			List<string> listAff = new List<string>();
			List<string> listFour = new List<string>();
			List<string> listSalC = new List<string>();
			List<string> listSalD = new List<string>();
			List<string> listNumero = new List<string>();

            foreach (Sortie_Atelier item in ((App)App.Current).mySitaffEntities.Sortie_Atelier)
			{
				listNumero.Add(item.Numero);

				if (item.Affaire1 != null && item.Affaire1.Numero != null && listAff.Contains(item.Affaire1.Numero.ToString()) == false)
				{
					listAff.Add(item.Affaire1.Numero.ToString());
				}
				if (item.Fournisseur1 != null && item.Fournisseur1.Entreprise != null && listFour.Contains(item.Fournisseur1.Entreprise.Libelle.ToString()) == false)
				{
					listFour.Add(item.Fournisseur1.Entreprise.Libelle);
				}
				if (item.Salarie != null && item.Salarie.Personne != null && listSalC.Contains(item.Salarie.Personne.fullname) == false)
				{
					listSalC.Add(item.Salarie.Personne.fullname);
				}
				if (item.Salarie1 != null && item.Salarie1.Personne != null && listSalD.Contains(item.Salarie1.Personne.ToString()) == false)
				{
					listSalD.Add(item.Salarie1.Personne.fullname);
				}
			}

			this._filterContainAffaire.ItemsSource = listAff;
			this._filterContainDemandeur.ItemsSource = listSalD;
			this._filterContainFournisseur.ItemsSource = listFour;
			this._filterContainNumero.ItemsSource = listNumero;
			this._filterContainSalarie.ItemsSource = listSalC;

		}

		#endregion

		#region initialisation donnés datagridMain

		private void initialisationDataDatagridMain(ObservableCollection<Sortie_Atelier> listToPut)
		{
			if (listToPut == null)
			{
				this.listSorties = new ObservableCollection<Sortie_Atelier>(((App)App.Current).mySitaffEntities.Sortie_Atelier.OrderBy(ord => ord.Date_Sortie));
				this.MiseAJourEtat("", null);
			}
			else
			{
				this.listSorties = new ObservableCollection<Sortie_Atelier>(listToPut);
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

		#endregion

        #endregion

        #region CRUD (Create Read Update Delete)

        /// <summary>
		/// Ajoute une nouvelle entreprise à la liste à l'aide d'une nouvelle fenêtre
		/// </summary>
		public Sortie_Atelier Add()
		{
				//Affichage du message "ajout en cours"
				((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une sortie atelier en cours ...");

				//Initialisation de la fenêtre
				SortieAtelierWindow SortieAtelierWindow = new SortieAtelierWindow();

				//Création de l'objet temporaire
				Sortie_Atelier tmp = new Sortie_Atelier();


				//Mise de l'objet temporaire dans le datacontext
				SortieAtelierWindow.DataContext = tmp;

				//booléen nullable vrai ou faux ou null
				bool? dialogResult = SortieAtelierWindow.ShowDialog();

				if (dialogResult.HasValue && dialogResult.Value == true)
				{
					//Si j'appuie sur le bouton Ok, je renvoi l'objet sortieAtelier dans le datacontext de la fenêtre
					return (Sortie_Atelier)SortieAtelierWindow.DataContext;
				}
				else
				{
					try
					{
						//Detachement de tous les éléments liés à une Mission tiers
						if (((Sortie_Atelier)SortieAtelierWindow.DataContext).Contenu_Sortie_Atelier != null)
						{
							while (((Sortie_Atelier)SortieAtelierWindow.DataContext).Contenu_Sortie_Atelier.Count() > 0)
							{
								((App)App.Current).mySitaffEntities.Detach(((Sortie_Atelier)SortieAtelierWindow.DataContext).Contenu_Sortie_Atelier.First());
							}
							((App)App.Current).mySitaffEntities.Detach(((Sortie_Atelier)SortieAtelierWindow.DataContext).Contenu_Sortie_Atelier);
						}
					}
					catch (Exception)
					{

					}
					//Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
					this.recalculMax();
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une sortie atelier annulé : " + this.listSorties.Count() + " / " + this.max);

					return null;
				}
		}
		
		/// <summary>
		/// Ouvre l'entreprise séléctionnée à l'aide d'une nouvelle fenêtre
		/// </summary>
		public Sortie_Atelier Open()
		{
			if (this._DataGridMain.SelectedItem != null)
			{
				if (this._DataGridMain.SelectedItems.Count == 1)
				{
					//Affichage du message "modification en cours"
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une sortie atelier en cours ...");

					//Création de la fenêtre
					SortieAtelierWindow SortieAtelierWindow = new SortieAtelierWindow();

					//Initialisation du Datacontext en Sortie_Atelier et association à l'Sortie_Atelier sélectionné
					SortieAtelierWindow.DataContext = new Sortie_Atelier();
					SortieAtelierWindow.DataContext = (Sortie_Atelier)this._DataGridMain.SelectedItem;

					//booléen nullable vrai ou faux ou null
					bool? dialogResult = SortieAtelierWindow.ShowDialog();

					if (dialogResult.HasValue && dialogResult.Value == true)
					{
						//Si j'appuie sur le bouton Ok, je renvoi l'objet Sortie_Atelier dans le datacontext de la fenêtre
						return (Sortie_Atelier)SortieAtelierWindow.DataContext;
					}
					else
					{
						//Je récupère les anciennes données de la base sur les modifications effectuées
						((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Sortie_Atelier)(this._DataGridMain.SelectedItem));
						//La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
						((App)App.Current).refreshEDMXSansVidage();
						this.filtrage();

						//Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
						((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
						this.recalculMax();
						((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une sortie atelier annulée : " + this.listSorties.Count() + " / " + this.max);

						return null;
					}
				}
				else
				{
					MessageBox.Show("Vous ne devez sélectionner qu'une seule sortie atelier.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return null;
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner une sortie atelier.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return null;
			}
		}
		
		/// <summary>
		/// Supprime l'entreprise séléctionnée avec une confirmation
		/// </summary>
		public Sortie_Atelier Remove()
		{
			if (this._DataGridMain.SelectedItem != null)
			{
				if (this._DataGridMain.SelectedItems.Count == 1)
				{
					//Affichage du message "suppression en cours"
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une sortie atelier en cours ...");

					bool test = true;

					if (test)
					{
						if (MessageBox.Show("Voulez-vous rééllement supprimer la sortie atelier séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
						{
							if (((Sortie_Atelier)this._DataGridMain.SelectedItem).Contenu_Sortie_Atelier != null)
							{
                                ObservableCollection<Contenu_Sortie_Atelier> toRemove = new ObservableCollection<Contenu_Sortie_Atelier>();
                                foreach (Contenu_Sortie_Atelier item in ((Sortie_Atelier)this._DataGridMain.SelectedItem).Contenu_Sortie_Atelier)
                                {
                                    toRemove.Add(item);
                                }
                                foreach (Contenu_Sortie_Atelier item in toRemove)
                                {
                                    try
                                    {
                                        ((App)App.Current).mySitaffEntities.Contenu_Sortie_Atelier.DeleteObject(item);
                                    }
                                    catch (Exception)
                                    {
                                        try
                                        {
                                            ((Sortie_Atelier)this._DataGridMain.SelectedItem).Contenu_Sortie_Atelier.Remove(item);
                                        }
                                        catch (Exception)
                                        {
                                            ((App)App.Current).mySitaffEntities.Detach(((Sortie_Atelier)this._DataGridMain.SelectedItem).Contenu_Sortie_Atelier.First());
                                        }
                                    }
                                }
							}
							return ((Sortie_Atelier)this._DataGridMain.SelectedItem);
						}
					}

				}
				else
				{
					MessageBox.Show("Vous ne devez sélectionner qu'une seule sortie atelier.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return null;
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner une sortie atelier.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return null;
			}
			return null;
		}

		/// <summary>
		/// Ouvre l'entreprise séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
		/// </summary>
		public Sortie_Atelier Look(Sortie_Atelier sortieAtelier)
		{
			if (this._DataGridMain.SelectedItem != null || sortieAtelier != null)
			{
				if (this._DataGridMain.SelectedItems.Count == 1 || sortieAtelier != null)
				{
					//Affichage du message "affichage en cours"
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une sortie atelier en cours ...");

					//Création de la fenêtre
					SortieAtelierWindow SortieAtelierWindow = new SortieAtelierWindow();

					//Initialisation du Datacontext en ordre de mission et association à l'sortieAtelier sélectionnée
					SortieAtelierWindow.DataContext = new Sortie_Atelier();
					if (sortieAtelier != null)
					{
						SortieAtelierWindow.DataContext = sortieAtelier;
					}
					else
					{
						SortieAtelierWindow.DataContext = (Sortie_Atelier)this._DataGridMain.SelectedItem;
					}

					//Je positionne la lecture seule sur la fenêtre
					SortieAtelierWindow.lectureSeule();

					//J'affiche la fenêtre
					bool? dialogResult = SortieAtelierWindow.ShowDialog();

					//Affichage du message "affichage en cours"
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une sortie atelier terminé : " + this.listSorties.Count() + " / " + this.max);

					//Renvoi null
					return null;
				}
				else
				{
					MessageBox.Show("Vous ne devez sélectionner qu'une seule sortie atelier.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return null;
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner une sortie atelier.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
			this._filterContainAffaire.Text = "";
			this._filterContainDate.SelectedDate = null;
			this._filterContainDemandeur.Text = "";
			this._filterContainFournisseur.Text = "";
			this._filterContainNumero.Text = "";
			this._filterContainSalarie.Text = "";

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
			((App)App.Current)._theMainWindow.parametreMain._mutex.WaitOne();
			((App)App.Current)._theMainWindow.parametreMain.startThread();
			((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
			((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Filtrage en cours ...";

			ObservableCollection<Sortie_Atelier> listToPut = new ObservableCollection<Sortie_Atelier>(((App)App.Current).mySitaffEntities.Sortie_Atelier.OrderBy(DataChangedEventManager => DataChangedEventManager.Date_Sortie));

			if (this._filterContainAffaire.Text != "")
			{
				listToPut = new ObservableCollection<Sortie_Atelier>(listToPut.Where(lis => lis.Affaire1.Numero.Trim().ToLower().Contains(this._filterContainAffaire.Text.Trim().ToLower())));
			}
			if (this._filterContainDate.SelectedDate != null)
			{
				listToPut = new ObservableCollection<Sortie_Atelier>(listToPut.Where(lis => lis.Date_Sortie >= this._filterContainDate.SelectedDate));
			}
			if (this._filterContainDemandeur.Text != "")
			{
				listToPut = new ObservableCollection<Sortie_Atelier>(listToPut.Where(lis => lis.Salarie1.Personne.fullname.Trim().ToLower().Contains(this._filterContainDemandeur.Text.Trim().ToLower())));
			}
			if (this._filterContainFournisseur.Text != "")
			{
				listToPut = new ObservableCollection<Sortie_Atelier>(listToPut.Where(lis => lis.Fournisseur1.Entreprise.Libelle.Trim().ToLower().Contains(this._filterContainFournisseur.Text.Trim().ToLower())));
			}
			if (this._filterContainNumero.Text != "")
			{
				listToPut = new ObservableCollection<Sortie_Atelier>(listToPut.Where(lis => lis.Numero.Trim().ToLower().Contains(this._filterContainNumero.Text.Trim().ToLower())));
			}
			if (this._filterContainSalarie.Text != "")
			{
				listToPut = new ObservableCollection<Sortie_Atelier>(listToPut.Where(lis => lis.Salarie.Personne.fullname.Trim().ToLower().Contains(this._filterContainSalarie.Text.Trim().ToLower())));
			}

			((App)App.Current)._theMainWindow.parametreMain.stopThread();

			((App)App.Current)._theMainWindow.parametreMain.stopThread();


			//Insertion des données dans le datagrid
			this.initialisationDataDatagridMain(listToPut);
			if (this.listSorties.Count() == 0)
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
				if (this._filterContainAffaire.Text != "" || this._filterContainDate.SelectedDate != null || this._filterContainDemandeur.Text != "" || this._filterContainFournisseur.Text != "" || this._filterContainNumero.Text != "" || this._filterContainSalarie.Text != "")
				{
					this.remiseAZero();
				}
			}
			else
			{
				this._filterZone.Height = double.NaN;
				this._ButtonMasqueFiltre.Content = "Masquer les filtres";
				//Je me positionne sur le premier champ
				this._filterContainAffaire.Focus();
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
			((App)App.Current)._theMainWindow.parametreMain._CommandLook.Command.Execute(((App)App.Current)._theMainWindow.parametreMain);
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
				//this.filtrage();
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

		#region Fonction

		/// <summary>
		/// Recalcul le nombre d'élements maximum
		/// </summary>
		private void recalculMax()
		{
			this.max = ((App)App.Current).mySitaffEntities.Sortie_Atelier.Count();
		}

		/// <summary>
		/// Met à jour l'état en bas pour l'utilisateur
		/// </summary>
		/// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
		/// <param name="dao">un objet Sortie_Atelier soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
		public void MiseAJourEtat(string typeEtat, Sortie_Atelier om)
		{
			//Je recalcul le nombre max d'élements
			this.recalculMax();
			//En fonction de l'action, j'affiche le message
			if (typeEtat == "Filtrage")
			{
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Filtrage des sorties atelier terminé : " + this.listSorties.Count() + " / " + this.max);
			}
			else if (typeEtat == "Ajout")
			{
				//J'ajoute sortie atelier dans le linsting
				this.listSorties.Add(om);
				//Je recalcul le nombre max d'élements après l'ajout
				this.recalculMax();
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une sortie atelier effectuée avec succès. Nombre d'élements : " + this.listSorties.Count() + " / " + this.max);
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
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification de la sortie atelier effectuée avec succès. Nombre d'élements : " + this.listSorties.Count() + " / " + this.max);
			}
			else if (typeEtat == "Suppression")
			{
				//Je supprime de mon listing l'élément supprimé
				this.listSorties.Remove(om);
				//Je recalcul le nombre max d'élements après la suppression
				this.recalculMax();
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression de la sortie atelier effectuée avec succès. Nombre d'élements : " + this.listSorties.Count() + " / " + this.max);
			}
			else if (typeEtat == "Look")
			{

			}
			else if (typeEtat == "Duplicate")
			{
				//J'ajoute sortie atelier dans le linsting
				this.listSorties.Add(om);
				//Je recalcul le nombre max d'élements après l'ajout
				this.recalculMax();
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Dupliquer une sortie atelier effectué avec succès. Nombre d'élements : " + this.listSorties.Count() + " / " + this.max);
			}
			else
			{
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des sorties ateliers terminé : " + this.listSorties.Count() + " / " + this.max);
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
			this.listSorties = new ObservableCollection<Sortie_Atelier>(this.listSorties.OrderBy(ord => ord.Date_Sortie));
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
