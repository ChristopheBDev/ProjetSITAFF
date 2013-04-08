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
    /// Logique d'interaction pour ListeProformaClientControl.xaml
    /// </summary>
    public partial class ListeProformaClientControl : UserControl
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
			DependencyProperty.Register("listFacture", typeof(ObservableCollection<Facture>), typeof(ListeProformaClientControl), new UIPropertyMetadata(null));

		#endregion

		#region constructeur

        public ListeProformaClientControl()
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
			List<string> listConditionReglement = new List<string>();
			List<string> listClient = new List<string>();
			List<string> listChargeAffaire = new List<string>();

			foreach (Facture item in ((App)App.Current).mySitaffEntities.Facture)
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
				if (item.Commande1 != null)
				{
					if (!listCommande.Contains(item.Commande1.Numero_Commande))
					{
						listCommande.Add(item.Commande1.Numero_Commande);
					}
				}

				//Pour remplir les clients
				if (item.Client1 != null)
				{
					if (item.Client1.Entreprise != null)
					{
						if (!listClient.Contains(item.Client1.Entreprise.Libelle))
						{
							listClient.Add(item.Client1.Entreprise.Libelle);
						}
					}
				}


				//Pour remplir les conditions de reglement
				if (item.Condition_Reglement1 != null)
				{
					if (!listConditionReglement.Contains(item.Condition_Reglement1.Libelle))
					{
						listConditionReglement.Add(item.Condition_Reglement1.Libelle);
					}
				}

				//Pour remplir les chargés d'affaire
				if (item.Salarie != null && item.Salarie.Personne != null)
				{
					if (!listChargeAffaire.Contains(item.Salarie.Personne.fullname))
					{
						listChargeAffaire.Add(item.Salarie.Personne.fullname);
					}
				}
			}

			_filterContainConditionReglement.ItemsSource = listConditionReglement;
			_filterContainNumeroAffaire.ItemsSource = listAffaire;
			_filterContainNumeroCommande.ItemsSource = listCommande;
			_filterContainClient.ItemsSource = listClient;
			_filterContainChargeAffaire.ItemsSource = listChargeAffaire;
		}

		#endregion

		#region initialisation Donnés datagridMain

		private void initialisationDataDatagridMain(ObservableCollection<Facture> listToPut)
		{
			if (listToPut == null)
			{
				this.listFacture = new ObservableCollection<Facture>(((App)App.Current).mySitaffEntities.Facture.Where(fac => fac.Facture_Client == null && fac.Proforma_Client != null).OrderBy(ccf => ccf.Numero));
				this.MiseAJourEtat("", null);
			}
			else
			{
				this.listFacture = new ObservableCollection<Facture>(listToPut);
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

        #region Fenetre chargée

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
		public Facture Add()
		{
			//Affichage du message "ajout en cours"
			((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
			((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture en cours ...");

			//Initialisation de la fenêtre
			ProformaClientWindow factureWindow = new ProformaClientWindow();

			//Création de l'objet temporaire
			Facture tmp = new Facture();
			tmp.Proforma_Client = new Proforma_Client();

			//Mise de l'objet temporaire dans le datacontext
			factureWindow.DataContext = tmp;


			//booléen nullable vrai ou faux ou null
			bool? dialogResult = factureWindow.ShowDialog();

			if (dialogResult.HasValue && dialogResult.Value == true)
			{
				//Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
				return (Facture)factureWindow.DataContext;
			}
			else
			{
				try
				{
					//On détache la commande
					((App)App.Current).mySitaffEntities.Detach((Facture)factureWindow.DataContext);
				}
				catch (Exception)
				{
				}

				//Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
				((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
				this.recalculMax();
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture annulé : " + this.listFacture.Count() + " / " + this.max);

				return null;
			}
		}

		/// <summary>
		/// Ouvre la facture séléctionnée à l'aide d'une nouvelle fenêtre
		/// </summary>
		public Facture Open()
		{
			if (this._DataGridMain.SelectedItem != null)
			{
				if (this._DataGridMain.SelectedItems.Count == 1)
				{
					//Affichage du message "modification en cours"
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une facture en cours ...");

					//Création de la fenêtre
					ProformaClientWindow factureWindow = new ProformaClientWindow();

					//Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
					factureWindow.DataContext = new Facture();
					factureWindow.DataContext = (Facture)this._DataGridMain.SelectedItem;

					//booléen nullable vrai ou faux ou null
					bool? dialogResult = factureWindow.ShowDialog();

					if (dialogResult.HasValue && dialogResult.Value == true)
					{
						//Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
						return (Facture)factureWindow.DataContext;
					}
					else
					{
						//Je récupère les anciennes données de la base sur les modifications effectuées
						((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Facture)(this._DataGridMain.SelectedItem));
						//La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
						((App)App.Current).refreshEDMXSansVidage();
						this.filtrage();

						//Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
						((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
						this.recalculMax();
						((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une facture annulée : " + this.listFacture.Count() + " / " + this.max);

						return null;
					}
				}
				else
				{
					MessageBox.Show("Vous ne devez sélectionner qu'une seule facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return null;
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner une facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return null;
			}
		}

		/// <summary>
		/// Supprime la facture séléctionnée avec une confirmation
		/// </summary>
		public Facture Remove()
		{
			if (this._DataGridMain.SelectedItem != null)
			{
				if (this._DataGridMain.SelectedItems.Count == 1)
				{
					//Affichage du message "suppression en cours"
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une facture en cours ...");
					if (((Facture)this.DataContext).Reglement_Client_Facture == null || ((Facture)this.DataContext).Reglement_Client_Facture.Count > 0)
					{
						MessageBox.Show("Des règlements sont associés à cette facture, suppression impossible.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						return null;
					}
					else if (MessageBox.Show("Voulez-vous rééllement supprimer la facture séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
					{
						ObservableCollection<Contenu_Facture> toRemove = new ObservableCollection<Contenu_Facture>();
						foreach (Contenu_Facture item in ((Facture)this._DataGridMain.SelectedItem).Contenu_Facture)
						{
							toRemove.Add(item);
						}
						foreach (Contenu_Facture item in toRemove)
						{
							try
							{
								((App)App.Current).mySitaffEntities.Contenu_Facture.DeleteObject(item);
								((Facture)this._DataGridMain.SelectedItem).Contenu_Facture.Remove(item);
							}
							catch (Exception)
							{
								try
								{
									((Facture)this._DataGridMain.SelectedItem).Contenu_Facture.Remove(item);
									((App)App.Current).mySitaffEntities.Contenu_Facture.DeleteObject(item);
								}
								catch (Exception) { }
							}
						}

						try
						{
							((App)App.Current).mySitaffEntities.DeleteObject(((Facture)this._DataGridMain.SelectedItem).Facture_Client);
							((App)App.Current).mySitaffEntities.Detach(((Facture)this._DataGridMain.SelectedItem).Facture_Client);
						}
						catch (Exception)
						{
							try
							{
								((App)App.Current).mySitaffEntities.Detach(((Facture)this._DataGridMain.SelectedItem).Facture_Client);
								((App)App.Current).mySitaffEntities.DeleteObject(((Facture)this._DataGridMain.SelectedItem).Facture_Client);
							}
							catch (Exception) { }
						}

						while (((Facture)this.DataContext).Relance_Facture.Count > 0)
						{
							try
							{
								((App)App.Current).mySitaffEntities.Detach(((Facture)this.DataContext).Relance_Facture.First());
								((App)App.Current).mySitaffEntities.DeleteObject(((Facture)this.DataContext).Relance_Facture.First());
							}
							catch (Exception)
							{
								try
								{
									((App)App.Current).mySitaffEntities.DeleteObject(((Facture)this.DataContext).Relance_Facture.First());
									((App)App.Current).mySitaffEntities.Detach(((Facture)this.DataContext).Relance_Facture.First());
								}
								catch (Exception)
								{
								}
							}
						}

						while (((Facture)this.DataContext).Litige_Facture.Count > 0)
						{
							try
							{
								((App)App.Current).mySitaffEntities.Detach(((Facture)this.DataContext).Litige_Facture.First());
								((App)App.Current).mySitaffEntities.DeleteObject(((Facture)this.DataContext).Litige_Facture.First());
							}
							catch (Exception)
							{
								try
								{
									((App)App.Current).mySitaffEntities.DeleteObject(((Facture)this.DataContext).Litige_Facture.First());
									((App)App.Current).mySitaffEntities.Detach(((Facture)this.DataContext).Litige_Facture.First());
								}
								catch (Exception)
								{
								}
							}
						}

						//Supprimer l'élément 
						return (Facture)this._DataGridMain.SelectedItem;
					}
					else
					{
						//Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
						((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
						this.recalculMax();
						((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une facture annulée : " + this.listFacture.Count() + " / " + this.max);

						return null;
					}

				}
				else
				{
					MessageBox.Show("Vous ne devez sélectionner qu'une seule facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return null;
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner une facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return null;
			}
		}

		/// <summary>
		/// Ouvre la facture séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
		/// </summary>
		public Facture Look()
		{
			if (this._DataGridMain.SelectedItem != null)
			{
				if (this._DataGridMain.SelectedItems.Count == 1)
				{
					//Affichage du message "affichage en cours"
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une facture en cours ...");

					//Création de la fenêtre
					ProformaClientWindow factureWindow = new ProformaClientWindow();

					//Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
					factureWindow.DataContext = new Facture();
					factureWindow.DataContext = (Facture)this._DataGridMain.SelectedItem;

					//Je positionne la lecture seule sur la fenêtre
					factureWindow.lectureSeule();
					factureWindow.soloLecture = true;

					//J'affiche la fenêtre
					bool? dialogResult = factureWindow.ShowDialog();

					//Affichage du message "affichage en cours"
					((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
					((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une facture terminé : " + this.listFacture.Count() + " / " + this.max);

					//Renvoi null
					return null;
				}
				else
				{
					MessageBox.Show("Vous ne devez sélectionner qu'une seule facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return null;
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner une facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
			//Text
			_filterContainMontant.Text = "";
			_filterContainNumeroFacture.Text = "";

			//Dates
			this._filterContainDateEcheance.SelectedDate = null;
			this._filterContainDateFacture.SelectedDate = null;
			this._filterContainDateDebutFacture.SelectedDate = null;
			this._filterContainDateFinFacture.SelectedDate = null;

			//AutoCompleteBox
			this._filterContainConditionReglement.Text = "";
			this._filterContainNumeroCommande.Text = "";
			this._filterContainNumeroAffaire.Text = "";
			this._filterContainClient.Text = "";

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

			ObservableCollection<Facture> listToPut = new ObservableCollection<Facture>(((App)App.Current).mySitaffEntities.Facture.Where(fac => fac.Proforma_Client != null && fac.Facture_Client == null).OrderBy(ccf => ccf.Numero));

			if (this._filterContainNumeroFacture.Text != "")
			{
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Numero.Trim().ToLower().Contains(this._filterContainNumeroFacture.Text.Trim().ToLower())));
			}
			if (this._filterContainNumeroAffaire.Text != "")
			{
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Affaire1 != null));
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Affaire1.Numero.Trim().ToLower().Contains(this._filterContainNumeroAffaire.Text.Trim().ToLower())));
			}
			if (this._filterContainNumeroCommande.Text != "")
			{
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Commande1 != null));
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Commande1.Numero_Commande.Trim().ToLower().Contains(this._filterContainNumeroCommande.Text.Trim().ToLower())));
			}
			if (this._filterContainDateFacture.SelectedDate != null)
			{
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Date_Facture != null));
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Date_Facture == this._filterContainDateFacture.SelectedDate));
			}
			if (this._filterContainDateEcheance.SelectedDate != null)
			{
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Date_Echeance != null));
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Date_Echeance == this._filterContainDateEcheance.SelectedDate));
			}
			if (this._filterContainConditionReglement.Text != "")
			{
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Condition_Reglement1 != null));
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Condition_Reglement1.Libelle.Trim().ToLower().Contains(this._filterContainConditionReglement.Text.Trim().ToLower())));
			}
			if (this._filterContainClient.Text != "")
			{
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Commande1 != null));
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Commande1.Versions.Count != 0));
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Commande1.Versions.First().Devis1 != null));
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Commande1.Versions.First().Devis1.Client1 != null));
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Commande1.Versions.First().Devis1.Client1.Entreprise != null));
				listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Commande1.Versions.First().Devis1.Client1.Entreprise.Libelle.Trim().ToLower().Contains(this._filterContainClient.Text.Trim().ToLower())));
			}
			if (this._filterContainMontant.Text != "")
			{
				double val;
				if (double.TryParse(this._filterContainMontant.Text, out val))
				{
					listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Montant != null));
					listToPut = new ObservableCollection<Facture>(listToPut.Where(fac => fac.Montant.ToString().Contains(double.Parse(this._filterContainMontant.Text.Trim()).ToString())));
				}
			}
			if (this._filterContainDateDebutFacture.SelectedDate != null)
			{
				DateTime temp = new DateTime(this._filterContainDateDebutFacture.SelectedDate.Value.Year, this._filterContainDateDebutFacture.SelectedDate.Value.Month, this._filterContainDateDebutFacture.SelectedDate.Value.Day, 00, 00, 00);
				this._filterContainDateDebutFacture.SelectedDate = temp;
				listToPut = new ObservableCollection<Facture>(listToPut.Where(com => com.Date_Facture != null));
				listToPut = new ObservableCollection<Facture>(listToPut.Where(com => com.Date_Facture >= this._filterContainDateDebutFacture.SelectedDate));
			}
			if (this._filterContainDateFinFacture.SelectedDate != null)
			{
				DateTime temp = new DateTime(this._filterContainDateFinFacture.SelectedDate.Value.Year, this._filterContainDateFinFacture.SelectedDate.Value.Month, this._filterContainDateFinFacture.SelectedDate.Value.Day, 23, 59, 59);
				this._filterContainDateFinFacture.SelectedDate = temp;
				listToPut = new ObservableCollection<Facture>(listToPut.Where(com => com.Date_Facture != null));
				listToPut = new ObservableCollection<Facture>(listToPut.Where(com => com.Date_Facture <= this._filterContainDateFinFacture.SelectedDate));
			}

			((App)App.Current)._theMainWindow.stopThread();

			//Insertion des données dans le datagrid
			this.initialisationDataDatagridMain(listToPut);

			if (this.listFacture.Count() == 0)
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
				if (_filterContainDateDebutFacture.SelectedDate != null || _filterContainDateFinFacture.SelectedDate != null || _filterContainNumeroFacture.Text != "" || _filterContainNumeroCommande.Text != "" || _filterContainNumeroAffaire.Text != "" || _filterContainConditionReglement.Text != "" || _filterContainMontant.Text != "" || _filterContainDateFacture.SelectedDate != null || _filterContainDateEcheance.SelectedDate != null || this.max != this.listFacture.Count())
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
			this.max = ((App)App.Current).mySitaffEntities.Facture.Where(fac => fac.Proforma_Client != null && fac.Facture_Client == null).Count();
		}

		/// <summary>
		/// Met à jour l'état en bas pour l'utilisateur
		/// </summary>
		/// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
		/// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
		public void MiseAJourEtat(string typeEtat, Facture fac)
		{
			//Je racalcul le nombre max d'élements
			this.recalculMax();
			//En fonction de l'action, j'affiche le message
			if (typeEtat == "Filtrage")
			{
				((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des factures terminée : " + this.listFacture.Count() + " / " + this.max);
			}
			else if (typeEtat == "Ajout")
			{
				//J'ajoute la commande_fournisseur dans le linsting
				this.listFacture.Add(fac);
				//Je racalcul le nombre max d'élements après l'ajout
				this.recalculMax();
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une facture numéro '" + fac.Numero + "' effectué avec succès. Nombre d'élements : " + this.listFacture.Count() + " / " + this.max);
			}
			else if (typeEtat == "Modification")
			{
				//Je raffraichis mon datagrid
				this._DataGridMain.Items.Refresh();
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification de la facture numéro : '" + fac.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listFacture.Count() + " / " + this.max);
			}
			else if (typeEtat == "Suppression")
			{
				//Je supprime de mon listing l'élément supprimé
				this.listFacture.Remove(fac);
				//Je racalcul le nombre max d'élements après la suppression
				this.recalculMax();
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression de la facture numéro : '" + fac.Numero + "' effectuée avec succès. Nombre d'élements : " + this.listFacture.Count() + " / " + this.max);
			}
			else if (typeEtat == "Look")
			{

			}
			else
			{
				((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des factures terminé : " + this.listFacture.Count() + " / " + this.max);
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
			this.listFacture = new ObservableCollection<Facture>(this.listFacture.OrderBy(nf => nf.Numero));
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

