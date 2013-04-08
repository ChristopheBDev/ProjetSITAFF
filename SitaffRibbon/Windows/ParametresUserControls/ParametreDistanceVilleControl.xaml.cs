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
using SitaffRibbon.Windows.ParametresWindows;
using System.Collections.ObjectModel;
using System.Threading;
using SitaffRibbon.Classes;

namespace SitaffRibbon.Windows.ParametresUserControls
{
    /// <summary>
    /// Logique d'interaction pour ParametreDistanceVilleControl.xaml
    /// </summary>
    public partial class ParametreDistanceVilleControl : UserControl
    {

		#region Variables

		long max = 0;

		//Les MenuItems Afficher / Masquer

		MenuItem MenuItem_AfficherTout;
		MenuItem MenuItem_MasquerTout;

		#endregion

		#region Constructeur

        public ParametreDistanceVilleControl()
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
			List<string> listVil = new List<string>();
			foreach (Ville item in ((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle))
			{
				listVil.Add(item.Libelle);
			}

            this._filterContainVille2.ItemsSource = listVil;
			this._filterContainVille.ItemsSource = listVil;
		}

		#endregion

		#region initialisation donnés datagridMain

		private void initialisationDataDatagridMain(ObservableCollection<Distance_Ville> listToPut)
		{
			if (listToPut == null)
			{
				this.listDistanceVille = new ObservableCollection<Distance_Ville>(((App)App.Current).mySitaffEntities.Distance_Ville.OrderBy(typ => typ.Ville.Libelle));
				this.MiseAJourEtat("", null);
			}
			else
			{
				this.listDistanceVille = new ObservableCollection<Distance_Ville>(listToPut);
				this.MiseAJourEtat("Filtrage", null);
			}
		}

		#endregion

        #region clic droit

        private void creationMenuClicDroit()
        {
            //Création du menu
            ContextMenu contextMenu = ((App)App.Current)._menuClicDroit.creationMenuClicDroitParameters(this);

            //Zone actions particulières

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
		/// <summary>
		/// Fin du chargement de la fenêtre (pour fermer la fenêtre de chargement)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
			((App)App.Current)._theMainWindow.parametreMain.stopThread();
		}

		#endregion

		#region Propriétés de dépendances

		public ObservableCollection<Distance_Ville> listDistanceVille
		{
			get { return (ObservableCollection<Distance_Ville>)GetValue(listDistanceVilleProperty); }
			set { SetValue(listDistanceVilleProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listDistanceVille.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listDistanceVilleProperty =
			DependencyProperty.Register("listDistanceVille", typeof(ObservableCollection<Distance_Ville>), typeof(DistanceVilleWindow), new UIPropertyMetadata(null));
		
		#endregion

		#region CRUD
		
		/// <summary>
		/// Ajoute une nouvelle civilité à la liste à l'aide d'une nouvelle fenêtre
		/// </summary>
		public Distance_Ville Add()
		{
			//Affichage du message "ajout en cours"
			((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
			((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Ajout d'un distance ville en cours ...";

			//Initialisation de la fenêtre
			DistanceVilleWindow DistanceVilleWindow = new DistanceVilleWindow();

			//Création de l'objet temporaire
			Distance_Ville tmp = new Distance_Ville();
			tmp.Ref_type = "null";
			tmp.Type_Distance = "null";

			//Mise de l'objet temporaire dans le datacontext
			DistanceVilleWindow.DataContext = tmp;

			//booléen nullable vrai ou faux ou null
			bool? dialogResult = DistanceVilleWindow.ShowDialog();

			if (dialogResult.HasValue && dialogResult.Value == true)
			{
				//Si j'appuie sur le bouton Ok, je renvoi l'objet banque se trouvant dans le datacontext de la fenêtre
				return (Distance_Ville)DistanceVilleWindow.DataContext;
			}
			else
			{
				try
				{
					//On détache la commande
					((App)App.Current).mySitaffEntities.Detach((Distance_Ville)DistanceVilleWindow.DataContext);
				}
				catch (Exception)
				{
				}

				//Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
				((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
				this.recalculMax();
				((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Ajout d'une distance ville annulé : " + this.listDistanceVille.Count() + " / " + this.max;

				return null;
			}
		}

		/// <summary>
		/// Ajoute une nouvelle civilité à la liste à l'aide d'une nouvelle fenêtre
		/// </summary>
		public Distance_Ville Add(Distance_Ville tmp)
		{
			//Affichage du message "ajout en cours"
			((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
			((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Ajout d'un distance ville en cours ...";

			//Initialisation de la fenêtre
			DistanceVilleWindow DistanceVilleWindow = new DistanceVilleWindow();

			//Création de l'objet temporaire
			tmp.Ref_type = "null";
			tmp.Type_Distance = "null";

			//Mise de l'objet temporaire dans le datacontext
			DistanceVilleWindow.DataContext = tmp;

			//booléen nullable vrai ou faux ou null
			bool? dialogResult = DistanceVilleWindow.ShowDialog();

			if (dialogResult.HasValue && dialogResult.Value == true)
			{
				//Si j'appuie sur le bouton Ok, je renvoi l'objet banque se trouvant dans le datacontext de la fenêtre
				return (Distance_Ville)DistanceVilleWindow.DataContext;
			}
			else
			{
				try
				{
					//On détache la commande
					((App)App.Current).mySitaffEntities.Detach((Distance_Ville)DistanceVilleWindow.DataContext);
				}
				catch (Exception)
				{
				}

				//Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
				((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
				this.recalculMax();
				((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Ajout d'une distance ville annulé : " + this.listDistanceVille.Count() + " / " + this.max;

				return null;
			}
		}
			
		/// <summary>
		/// Ouvre le motif séléctionnée à l'aide d'une nouvelle fenêtre
		/// </summary>
		public Distance_Ville Open()
		{
			if (this._DataGridMain.SelectedItem != null)
			{
				if (this._DataGridMain.SelectedItems.Count == 1)
				{
					//Affichage du message "modification en cours"
					((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Modification d'une distance ville en cours ...";

					//Création de la fenêtre
					DistanceVilleWindow DistanceVilleWindow = new DistanceVilleWindow();

					//Initialisation du Datacontext
					DistanceVilleWindow.DataContext = new Distance_Ville();
					DistanceVilleWindow.DataContext = (Distance_Ville)this._DataGridMain.SelectedItem;

					//booléen nullable vrai ou faux ou null
					bool? dialogResult = DistanceVilleWindow.ShowDialog();

					if (dialogResult.HasValue && dialogResult.Value == true)
					{
						//Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
						return (Distance_Ville)DistanceVilleWindow.DataContext;
					}
					else
					{
						//Je récupère les anciennes données de la base sur les modifications effectuées
						((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Distance_Ville)(this._DataGridMain.SelectedItem));
						//La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
						((App)App.Current).refreshEDMXSansVidage();
						this.filtrage();

						//Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
						((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
						this.recalculMax();
						((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Modification d'une distance ville annulé : " + this.listDistanceVille.Count() + " / " + this.max;

						return null;
					}
				}
				else
				{
					MessageBox.Show("Vous ne devez sélectionner qu'une seule distance ville.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return null;
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner une distance ville.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return null;
			}
		}
		
		/// <summary>
		/// Supprime le motif séléctionné avec une confirmation
		/// </summary>
		public Distance_Ville Remove()
		{
			if (this._DataGridMain.SelectedItem != null)
			{
				if (this._DataGridMain.SelectedItems.Count == 1)
				{
					//Affichage du message "suppression en cours"
					((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Suppression d'une distance ville en cours ...";

					if (MessageBox.Show("Voulez-vous rééllement supprimer la distance ville séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
					{
						//Supprimer l'élément 
						return (Distance_Ville)this._DataGridMain.SelectedItem;
					}
					else
					{
						//Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
						((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
						this.recalculMax();
						((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Suppression d'une distance ville annulé : " + this.listDistanceVille.Count() + " / " + this.max;

						return null;
					}

				}
				else
				{
					MessageBox.Show("Vous ne devez sélectionner qu'une seule distance ville.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return null;
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner une distance ville.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return null;
			}
		}
		
		/// <summary>
		/// Ouvre le motif séléctionné à l'aide d'une nouvelle fenêtre
		/// </summary>
		public Distance_Ville Look()
		{
			if (this._DataGridMain.SelectedItem != null)
			{
				if (this._DataGridMain.SelectedItems.Count == 1)
				{
					//Affichage du message "affichage en cours"
					((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Affichage d'une distance ville en cours ...";

					//Création de la fenêtre
					DistanceVilleWindow DistanceVilleWindow = new DistanceVilleWindow();

					//Initialisation du Datacontext
					DistanceVilleWindow.DataContext = new Distance_Ville();
					DistanceVilleWindow.DataContext = (Distance_Ville)this._DataGridMain.SelectedItem;

					//Je positionne la lecture seule sur la fenêtre
					DistanceVilleWindow.lectureSeule();

					//J'affiche la fenêtre
					bool? dialogResult = DistanceVilleWindow.ShowDialog();

					//Affichage du message "affichage en cours"
					((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
					((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Affichage d'une distance ville terminé : " + this.listDistanceVille.Count() + " / " + this.max;

					//Renvoi null
					return null;
				}
				else
				{
					MessageBox.Show("Vous ne devez sélectionner qu'une seule distance ville.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return null;
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner une distance ville.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return null;
			}
		}

		#endregion

		#region Filtrage

		#region remise a zero

		private void _buttonRaz_Click(object sender, RoutedEventArgs e)
		{
			this.remiseAZero();
		}

		private void remiseAZero()
		{
			_filterContainVille.Text = "";
            _filterContainVille2.Text = "";
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

			ObservableCollection<Distance_Ville> listToPut = new ObservableCollection<Distance_Ville>(((App)App.Current).mySitaffEntities.Distance_Ville.OrderBy(typ => typ.Ville.Libelle));

			if (this._filterContainVille.Text != "")
			{
				listToPut = new ObservableCollection<Distance_Ville>(listToPut.Where(lib => lib.Ville.Libelle.Trim().ToLower().Contains(this._filterContainVille.Text.Trim().ToLower())));
			}
            if (this._filterContainVille2.Text != "")
            {
                listToPut = new ObservableCollection<Distance_Ville>(listToPut.Where(lib => lib.Ville3.Libelle.Trim().ToLower().Contains(this._filterContainVille2.Text.Trim().ToLower())));
            }

			((App)App.Current)._theMainWindow.parametreMain.stopThread();

			((App)App.Current)._theMainWindow.parametreMain.stopThread();


			//Insertion des données dans le datagrid
			this.initialisationDataDatagridMain(listToPut);
			if (this.listDistanceVille.Count() == 0)
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
                if (_filterContainVille.Text != "" || _filterContainVille2.Text != "" || this.max != this.listDistanceVille.Count())
				{
					this.remiseAZero();
				}
			}
			else
			{
				this._filterZone.Height = double.NaN;
				this._ButtonMasqueFiltre.Content = "Masquer les filtres";
				//Je me positionne sur le premier champ
				this._filterContainVille.Focus();
			}
		}

		#endregion

		#endregion

		#region Evenements

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
				this.filtrage();
			}
		}

		#endregion

		#endregion

		#region Fonctions

		/// <summary>
		/// Recalcul le nombre d'élements maximum
		/// </summary>
		private void recalculMax()
		{
			this.max = ((App)App.Current).mySitaffEntities.Distance_Ville.Count();
		}

		/// <summary>
		/// Met à jour l'état en bas pour l'utilisateur
		/// </summary>
		/// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
		/// <param name="dao">un objet Distance_Villesoit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
		public void MiseAJourEtat(string typeEtat, Distance_Ville lib)
		{
			//Je racalcul le nombre max d'élements
			this.recalculMax();
			//En fonction de l'libion, j'affiche le message
			if (typeEtat == "Filtrage")
			{
				((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "filtrage des distances ville terminé : " + this.listDistanceVille.Count() + " / " + this.max;
			}
			else if (typeEtat == "Ajout")
			{
				//J'ajoute la commande_fournisseur dans le linsting
				this.listDistanceVille.Add(lib);
				//Je racalcul le nombre max d'élements après l'ajout
				this.recalculMax();
				this._DataGridMain.Items.Refresh();
				((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Ajout d'une distance ville effectué avec succès. Nombre d'élements : " + this.listDistanceVille.Count() + " / " + this.max;
			}
			else if (typeEtat == "Modification")
			{
				//Je raffraichis mon datagrid
				this._DataGridMain.Items.Refresh();
				((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Modification d'une distance ville effectué avec succès. Nombre d'élements : " + this.listDistanceVille.Count() + " / " + this.max;
			}
			else if (typeEtat == "Suppression")
			{
				//Je supprime de mon listing l'élément supprimé
				this.listDistanceVille.Remove(lib);
				//Je racalcul le nombre max d'élements après la suppression
				this.recalculMax();
				this._DataGridMain.Items.Refresh();
				((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Suppression d'une distance ville effectué avec succès. Nombre d'élements : " + this.listDistanceVille.Count() + " / " + this.max;
			}
			else if (typeEtat == "Look")
			{

			}
			else
			{
				((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Chargement des distances ville terminé : " + this.listDistanceVille.Count() + " / " + this.max;
			}
			//Je retri les données dans le sens par défaut
			this.triDatas();
			//J'arrete la progressbar
			((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
		}

		/// <summary>
		/// Tri les données dans le sens par défaut
		/// </summary>
		private void triDatas()
		{
			this.listDistanceVille = new ObservableCollection<Distance_Ville>(this.listDistanceVille.OrderBy(lib => lib.Ville.Libelle));
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
