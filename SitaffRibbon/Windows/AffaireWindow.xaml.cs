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
/* 
 * Using pour utilisation des IObservableCollection (afin d'éviter de mettre
 * System.Collections.ObjectModel.IObservableCollection en entier)
 */
using System.Collections.ObjectModel;
//Using pour utiliser le type TypeConverter pour la conversion de couleur
using System.ComponentModel;
using SitaffRibbon.UserControls;
using SitaffRibbon.Windows.ParametresWindows;
using SitaffRibbon.Windows.ParametresUserControls;
using SitaffRibbon.Classes;


namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour AffaireWindow.xaml
    /// </summary>
    public partial class AffaireWindow : Window
	{

		#region Attributs

		public bool soloLecture = false;

		#endregion

		#region Propiétés de dependances

		public ObservableCollection<Entreprise_Mere> listEntreprise_Mere
		{
			get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntreprise_MereProperty); }
			set { SetValue(listEntreprise_MereProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listEntreprise_Mere.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listEntreprise_MereProperty =
			DependencyProperty.Register("listEntreprise_Mere", typeof(ObservableCollection<Entreprise_Mere>), typeof(AffaireWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Salarie> Charge_Affaire
		{
			get { return (ObservableCollection<Salarie>)GetValue(Charge_AffaireProperty); }
			set { SetValue(Charge_AffaireProperty, value); }
		}
		// Using a DependencyProperty as the backing store for Charge_Affaire.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty Charge_AffaireProperty =
			DependencyProperty.Register("Charge_Affaire", typeof(ObservableCollection<Salarie>), typeof(AffaireWindow), new UIPropertyMetadata(null));

		#endregion

		#region Constructeurs

		public AffaireWindow()
		{
			InitializeComponent();

			initialisationPropDependance();

			initilisationSecurite();

            ((App)App.Current)._dimensionnementFenetre.InitTailles(this);
		}

		#region Initialisation

		private void initialisationPropDependance()
		{
			//Initialisation des propd
			this.listEntreprise_Mere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(em => em.Nom));
			this.Charge_Affaire = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.Where(sal => sal.Charge_Affaire == true).OrderBy(sal => sal.Personne.Nom).ThenBy(sal => sal.Personne.Prenom));
		}
		
		private void initilisationSecurite()
		{
			//Mise en place des droits sur les boutons et tabs
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeSalarieControl", "Add"))
			{
				this.NewChargeAffaire.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeSalarieControl", "Look"))
			{
				this.LookChargeAffaire.Visibility = Visibility.Collapsed;
			}

			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl", "Add"))
			{
				this.NewEntrepriseMere.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreEntrepriseMereControl", "Look"))
			{
				this.LookEntrepriseMere.Visibility = Visibility.Collapsed;
			}

			//Sécurité pour Version
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeDevisControl", "Add"))
			{
				this._ButtonAffaireVersionsNouveau.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeDevisControl", "Update"))
			{
				this._ButtonAffaireVersionsModifier.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeDevisControl", "Delete"))
			{
				this._ButtonAffaireVersionsSupprimer.Visibility = Visibility.Collapsed;
			}

			//Sécurité pour Facture
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeFactureControl", "Add"))
			{
				this._ButtonAffaireFactureNouveau.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeFactureControl", "Update"))
			{
				this._ButtonAffaireFactureModifier.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeFactureControl", "Delete"))
			{
				this._ButtonAffaireFactureSupprimer.Visibility = Visibility.Collapsed;
			}

			//Sécurité pour Commande
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeCommandeControl", "Add"))
			{
				this._ButtonAffaireCommandeNouveau.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeCommandeControl", "Update"))
			{
				this._ButtonAffaireCommandeModifier.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeCommandeControl", "Delete"))
			{
				this._ButtonAffaireCommandeSupprimer.Visibility = Visibility.Collapsed;
			}

			//Sécurité pour DAO
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeDAOControl", "Add"))
			{
				this._ButtonAffaireDAONouveau.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeDAOControl", "Update"))
			{
				this._ButtonAffaireDAOModifier.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeDAOControl", "Delete"))
			{
				this._ButtonAffaireDOASupprimer.Visibility = Visibility.Collapsed;
			}

			//Sécurité pour Doc Tech
			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl", "Add"))
			{
				this._ButtonAffaireDocumentTechniqueNouveau.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl", "Update"))
			{
				this._ButtonAffaireDocumentTechniqueModifier.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreTypeCommandeControl", "Delete"))
			{
				this._ButtonAffaireDocumentTechniqueSupprimer.Visibility = Visibility.Collapsed;
			}

			//Sécurité pour Régie
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeRegieControl", "Add"))
			{
				this._ButtonAffaireRegieNouveau.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeRegieControl", "Update"))
			{
				this._ButtonAffaireRegieModifier.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeRegieControl", "Delete"))
			{
				this._ButtonAffaireRegieSupprimer.Visibility = Visibility.Collapsed;
			}

			//Sécurité pour Heures Atelier
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeReleveHeureAtelierControl", "Add"))
			{
				this._ButtonAffaireAtelierNouveau.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeReleveHeureAtelierControl", "Update"))
			{
				this._ButtonAffaireAtelierModifier.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeReleveHeureAtelierControl", "Delete"))
			{
				this._ButtonAffaireAtelierSupprimer.Visibility = Visibility.Collapsed;
			}

			//Sécurité pour Heures Forfait
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeReleveHeureForfaitControl", "Add"))
			{
				this._ButtonAffaireChantierNouveau.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeReleveHeureForfaitControl", "Update"))
			{
				this._ButtonAffaireChantierModifier.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeReleveHeureForfaitControl", "Delete"))
			{
				this._ButtonAffaireChantierSupprimer.Visibility = Visibility.Collapsed;
			}
			

		}

		#endregion


		#endregion

		#region Lecture Seule

		private void lectureSeule()
		{
			//Button
			this._ButtonAffaireAtelierModifier.IsEnabled = false;
			this._ButtonAffaireAtelierNouveau.IsEnabled = false;
			this._ButtonAffaireAtelierSupprimer.IsEnabled = false;
			this._ButtonAffaireChantierModifier.IsEnabled = false;
			this._ButtonAffaireChantierNouveau.IsEnabled = false;
			this._ButtonAffaireChantierSupprimer.IsEnabled = false;
			this._ButtonAffaireChefChantierModifier.IsEnabled = false;
			this._ButtonAffaireChefChantierNouveau.IsEnabled = false;
			this._ButtonAffaireChefChantierSupprimer.IsEnabled = false;
			this._ButtonAffaireCommandeModifier.IsEnabled = false;
			this._ButtonAffaireCommandeNouveau.IsEnabled = false;
			this._ButtonAffaireCommandeSupprimer.IsEnabled = false;
			this._ButtonAffaireDAOModifier.IsEnabled = false;
			this._ButtonAffaireDAONouveau.IsEnabled = false;
			this._ButtonAffaireDOASupprimer.IsEnabled = false;
			this._ButtonAffaireDocumentTechniqueModifier.IsEnabled = false;
			this._ButtonAffaireDocumentTechniqueNouveau.IsEnabled = false;
			this._ButtonAffaireDocumentTechniqueSupprimer.IsEnabled = false;
			this._ButtonAffaireFactureModifier.IsEnabled = false;
			this._ButtonAffaireFactureNouveau.IsEnabled = false;
			this._ButtonAffaireFactureSupprimer.IsEnabled = false;
			this._ButtonAffaireRegieModifier.IsEnabled = false;
			this._ButtonAffaireRegieNouveau.IsEnabled = false;
			this._ButtonAffaireRegieSupprimer.IsEnabled = false;
			this._ButtonAffaireVersionsModifier.IsEnabled = false;
			this._ButtonAffaireVersionsNouveau.IsEnabled = false;
			this._ButtonAffaireVersionsSupprimer.IsEnabled = false;

			//ComboBox
			this._comboBoxAvancementReel.IsEnabled = false;
			this._ComboBoxChargeAffaire.IsEnabled = false;
			this._ComboBoxEntrepriseMere.IsEnabled = false;

			//DatePicker
			this._DatePickerDate_Fin_Effective.IsEnabled = false;
			this._DatePickerDateAcceptation.IsEnabled = false;

			//TextBox
			this._TextBoxNumeroAffaire.IsEnabled = false;
			
		}

		#endregion

		#region Fenêtre chargée

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (this.soloLecture)
			{
				this.lectureSeule();
			}

			((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
		}

		#endregion

		#region Boutons

		#region Boutons ok et cancel

		/// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.Verif_Generale())
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        /// <summary>
        /// Fonction lancée après clic sur Annuler
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        #region bouton versions

        private void _ButtonAffaireVersionsNouveau_Click(object sender, RoutedEventArgs e)
        {
            SelectionDevisWindow selectionDevisWindow = new SelectionDevisWindow();

            bool? dialogResult = selectionDevisWindow.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                PassageAffaireWindow passageAffaireWindow = new PassageAffaireWindow();
                passageAffaireWindow.DataContext = new Versions();
                passageAffaireWindow.devis = selectionDevisWindow.devis;

                bool? dialogResult2 = passageAffaireWindow.ShowDialog();

                if (dialogResult2.HasValue && dialogResult2.Value == true)
                {
					((Affaire)this.DataContext).Versions.Add((Versions)passageAffaireWindow.DataContext);
                }
                else
                {
                    ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, ((Devis)passageAffaireWindow.devis));
                    ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, ((Devis)passageAffaireWindow.devis).Versions);
                }
            }
            else
            {
            }
            this._dataGridVersions.Items.Refresh();
        }

        private void _ButtonAffaireVersionsModifier_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridVersions.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner une version à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridVersions.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'une version à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridVersions.SelectedItem != null)
            {
                PassageAffaireWindow passageAffaireWindow = new PassageAffaireWindow();
                passageAffaireWindow.DataContext = new Versions();
                passageAffaireWindow.devis = (Devis)((Versions)this._dataGridVersions.SelectedItem).Devis1;

                bool? dialogResult = passageAffaireWindow.ShowDialog();
                if (dialogResult.HasValue && dialogResult.Value == true)
                {
					this._dataGridVersions.SelectedItem = (Versions)passageAffaireWindow.DataContext;
                }
                else
                {
                    try
                    {
                        foreach (Versions v in ((Versions)passageAffaireWindow.DataContext).Devis1.Versions)
                        {
                            ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, v);
                            ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, v.Commande1);
                            if (v.Affaire1.EntityState == System.Data.EntityState.Added)
                            {
                                ((App)App.Current).mySitaffEntities.Detach(v.Affaire1);
                            }
                        }
                    }
                    catch (Exception) { }
                }
                this._dataGridVersions.Items.Refresh();
            }
        }

        private void _ButtonAffaireVersionsSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridVersions.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une version à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridVersions.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les versions à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    try
					{
						((App)App.Current).mySitaffEntities.Versions.DeleteObject((Versions)this._dataGridVersions.SelectedItem);
                        ((Versions)this._dataGridVersions.SelectedItem).Commande1 = null;
                        ((Versions)this._dataGridVersions.SelectedItem).Affaire1 = null;
                    }
                    catch (Exception)
					{
						try
						{
							((Versions)this._dataGridVersions.SelectedItem).Commande1 = null;
							((Versions)this._dataGridVersions.SelectedItem).Affaire1 = null;
							((App)App.Current).mySitaffEntities.Versions.DeleteObject((Versions)this._dataGridVersions.SelectedItem);
						}
						catch (Exception)
						{
						}
                    }
                }
            }
        }

        #endregion

        #region bouton facture

        private void _ButtonAffaireFactureNouveau_Click(object sender, RoutedEventArgs e)
        {
            FactureWindow factureWindow = new FactureWindow();
            factureWindow.DataContext = new Facture();
            ((Facture)factureWindow.DataContext).Affaire1 = (Affaire)this.DataContext;
            factureWindow._comboBoxAffaire.IsEnabled = false;

            bool? dialogResult = factureWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
				((Affaire)this.DataContext).Facture.Add(((Facture)factureWindow.DataContext));
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Facture)factureWindow.DataContext);
                }
                catch (Exception)
                {
                }
            }
            this._dataGridFacture.Items.Refresh();
        }

        private void _ButtonAffaireFactureModifier_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridFacture.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner une facture à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridFacture.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'une facture à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridFacture.SelectedItem != null)
            {
                FactureWindow factureWindow = new FactureWindow();
                factureWindow.DataContext = (Facture)this._dataGridFacture.SelectedItem;


                bool? dialogResult = factureWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
					this._dataGridFacture.SelectedItem = ((Facture)factureWindow.DataContext);
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Facture)factureWindow.DataContext);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            this._dataGridFacture.Items.Refresh();
        }

        private void _ButtonAffaireFactureSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridFacture.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une facture à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridFacture.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les factures à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    try
                    {
						((App)App.Current).mySitaffEntities.Facture.DeleteObject((Facture)this._dataGridFacture.SelectedItem);
                        ((Affaire)this.DataContext).Facture.Remove((Facture)this._dataGridFacture.SelectedItem);
                    }
                    catch (Exception)
                    {
						try
						{
							((Affaire)this.DataContext).Facture.Remove((Facture)this._dataGridFacture.SelectedItem);
							((App)App.Current).mySitaffEntities.Facture.DeleteObject((Facture)this._dataGridFacture.SelectedItem);
						}
						catch (Exception)
						{
						}
                    }
                }
            }
        }

        #endregion

        #region bouton DAO

        private void _ButtonAffaireDAOSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridDAO.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une DAO à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridDAO.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les DAO à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    try
                    {
						((App)App.Current).mySitaffEntities.DAO.DeleteObject((DAO)this._dataGridDAO.SelectedItem);
                        ((Affaire)this.DataContext).DAO.Remove((DAO)this._dataGridDAO.SelectedItem);
                    }
                    catch (Exception)
                    {
						try
						{
							((Affaire)this.DataContext).DAO.Remove((DAO)this._dataGridDAO.SelectedItem);
							((App)App.Current).mySitaffEntities.DAO.DeleteObject((DAO)this._dataGridDAO.SelectedItem);
						}
						catch (Exception)
						{
						}
                    }
                }
            }
        }

        private void _ButtonAffaireDAOModifier_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridDAO.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner une DAO à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridDAO.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'une DAO à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridDAO.SelectedItem != null)
            {
                DAOWindow daoWindow = new DAOWindow();
                daoWindow.DataContext = (DAO)this._dataGridDAO.SelectedItem;
				
                bool? dialogResult = daoWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    this._dataGridDAO.SelectedItem = (DAO)daoWindow.DataContext;
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (DAO)daoWindow.DataContext);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
        }

        private void _ButtonAffaireDAONouveau_Click(object sender, RoutedEventArgs e)
        {
            DAOWindow daoWindow = new DAOWindow();
            DAO tmp = new DAO();
            tmp.Utilisateur = ((App)App.Current)._connectedUser;
            tmp.Date_Creation = DateTime.Today;
            daoWindow.DataContext = tmp;

            daoWindow._checkBoxSurAffaire.IsEnabled = false;
            daoWindow._checkBoxSurDevis.IsEnabled = false;
            daoWindow._comboBoxAffaire.IsEnabled = false;
            daoWindow._comboBoxDevis.IsEnabled = false;

            ((DAO)daoWindow.DataContext).Affaire1 = (Affaire)this.DataContext;

            daoWindow.creation = true;

            bool? dialogResult = daoWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
				((Affaire)this.DataContext).DAO.Add((DAO)daoWindow.DataContext);
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((DAO)daoWindow.DataContext);
                }
                catch (Exception)
                {
                }
            }

            this._dataGridDAO.Items.Refresh();
        }

        #endregion

        #region bouton regie

        private void _ButtonAffaireRegieNouveau_Click(object sender, RoutedEventArgs e)
        {
            RegieWindow regieWindow = new RegieWindow();
            regieWindow.DataContext = new Regie();
            ((Regie)regieWindow.DataContext).Affaire1 = (Affaire)this.DataContext;

            bool? dialogResult = regieWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
				((Affaire)this.DataContext).Regie.Add((Regie)regieWindow.DataContext);
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Regie)regieWindow.DataContext);
                }
                catch (Exception)
                {
                }
            }
            this._dataGridRegie.Items.Refresh();
        }

        private void _ButtonAffaireRegieModifier_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridRegie.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner une régie à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridRegie.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'une régie à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridRegie.SelectedItem != null)
            {
                RegieWindow regieWindow = new RegieWindow();
                regieWindow.DataContext = (Regie)this._dataGridRegie.SelectedItem;


                bool? dialogResult = regieWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
					this._dataGridRegie.SelectedItem = (Regie)regieWindow.DataContext;
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Regie)regieWindow.DataContext);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
            this._dataGridRegie.Items.Refresh();
        }

        private void _ButtonAffaireRegieSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridRegie.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une régie à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridRegie.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les régies à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    try
                    {
						((App)App.Current).mySitaffEntities.Regie.DeleteObject((Regie)this._dataGridRegie.SelectedItem);
                        ((Affaire)this.DataContext).Regie.Remove((Regie)this._dataGridRegie.SelectedItem);
                    }
                    catch (Exception)
                    {
						try
						{
							((Affaire)this.DataContext).Regie.Remove((Regie)this._dataGridRegie.SelectedItem);
							((App)App.Current).mySitaffEntities.Regie.DeleteObject((Regie)this._dataGridRegie.SelectedItem);
						}
						catch (Exception)
						{
						}
                    }
                }
            }
        }

        #endregion

        #region Commande

        private void _ButtonAffaireCommandeNouveau_Click(object sender, RoutedEventArgs e)
        {
            CommandeWindow commandeWindow = new CommandeWindow();
            commandeWindow.DataContext = new Commande_Fournisseur();
            ((Commande_Fournisseur)commandeWindow.DataContext).Affaire1 = (Affaire)this.DataContext;
            commandeWindow._comboBoxAffaire.IsEnabled = false;

            bool? dialogResult = commandeWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
				((Affaire)this.DataContext).Commande_Fournisseur.Add((Commande_Fournisseur)commandeWindow.DataContext);
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Commande_Fournisseur)commandeWindow.DataContext);
                }
                catch (Exception)
                {

                }
            }
            this._dataGridCommande.Items.Refresh();
        }

        private void _ButtonAffaireCommandeModifier_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridCommande.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner une commande à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridCommande.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'une commande à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridCommande.SelectedItem != null)
            {
                CommandeWindow commandeWindow = new CommandeWindow();
                commandeWindow.DataContext = (Commande_Fournisseur)this._dataGridCommande.SelectedItem;


                bool? dialogResult = commandeWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
					this._dataGridCommande.SelectedItem = (Commande_Fournisseur)commandeWindow.DataContext;
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Commande_Fournisseur)commandeWindow.DataContext);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
            this._dataGridCommande.Items.Refresh();
        }

        private void _ButtonAffaireCommandeSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridCommande.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une commande à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridCommande.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les commandes à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    try
                    {
						((App)App.Current).mySitaffEntities.Commande_Fournisseur.DeleteObject((Commande_Fournisseur)this._dataGridCommande.SelectedItem);
                        ((Affaire)this.DataContext).Commande_Fournisseur.Remove((Commande_Fournisseur)this._dataGridCommande.SelectedItem);
                    }
                    catch (Exception)
                    {
						try
						{
							((Affaire)this.DataContext).Commande_Fournisseur.Remove((Commande_Fournisseur)this._dataGridCommande.SelectedItem);
							((App)App.Current).mySitaffEntities.Commande_Fournisseur.DeleteObject((Commande_Fournisseur)this._dataGridCommande.SelectedItem);
						}
						catch (Exception)
						{
						}
                    }
                }
            }
        }

        #endregion

        #region bouton chef chantier

        private void _ButtonAffaireChefChantierNouveau_Click(object sender, RoutedEventArgs e)
        {
            ChefChantierWindow chefChantierWindow = new ChefChantierWindow();
            chefChantierWindow.DataContext = new Affaire_Chef_Chantier();
            ((Affaire_Chef_Chantier)chefChantierWindow.DataContext).Affaire1 = (Affaire)this.DataContext;

            bool? dialogResult = chefChantierWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
				((Affaire)this.DataContext).Affaire_Chef_Chantier.Add((Affaire_Chef_Chantier)chefChantierWindow.DataContext);
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Affaire_Chef_Chantier)chefChantierWindow.DataContext);
                }
                catch (Exception)
                {
                }
            }
            this._dataGridChefChantier.Items.Refresh();
        }

        private void _ButtonAffaireChefChantierModifier_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridChefChantier.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner un chef de chantier à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridChefChantier.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'un chef de chantier à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridChefChantier.SelectedItem != null)
            {
                ChefChantierWindow chefChantierWindow = new ChefChantierWindow();
                chefChantierWindow.DataContext = (Affaire_Chef_Chantier)this._dataGridChefChantier.SelectedItem;


                bool? dialogResult = chefChantierWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
					this._dataGridChefChantier.SelectedItem = (Affaire_Chef_Chantier)chefChantierWindow.DataContext;
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Affaire_Chef_Chantier)chefChantierWindow.DataContext);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
            this._dataGridChefChantier.Items.Refresh();
        }

        private void _ButtonAffaireChefChantierSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridChefChantier.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un chef de chantier à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridChefChantier.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les chefs de chantier à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    try
                    {
						((App)App.Current).mySitaffEntities.Affaire_Chef_Chantier.DeleteObject((Affaire_Chef_Chantier)this._dataGridChefChantier.SelectedItem);
                        ((Affaire)this.DataContext).Affaire_Chef_Chantier.Remove((Affaire_Chef_Chantier)this._dataGridChefChantier.SelectedItem);
                    }
                    catch (Exception)
                    {
						try
						{
							((Affaire)this.DataContext).Affaire_Chef_Chantier.Remove((Affaire_Chef_Chantier)this._dataGridChefChantier.SelectedItem);
							((App)App.Current).mySitaffEntities.Affaire_Chef_Chantier.DeleteObject((Affaire_Chef_Chantier)this._dataGridChefChantier.SelectedItem);
						}
						catch (Exception)
						{
						}
                    }
                }
            }
        }

        #endregion

        #region bouton Type commande

        private void _ButtonAffaireDocumentTechniqueSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridDocumentTechnique.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une doc technique à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridDocumentTechnique.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les docs technique à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    try
                    {
						((App)App.Current).mySitaffEntities.Affaire_Type_Commande.DeleteObject((Affaire_Type_Commande)this._dataGridDocumentTechnique.SelectedItem);
                        ((Affaire)this.DataContext).Affaire_Type_Commande.Remove((Affaire_Type_Commande)this._dataGridDocumentTechnique.SelectedItem);
                    }
                    catch (Exception)
                    {
						try
						{
							((Affaire)this.DataContext).Affaire_Type_Commande.Remove((Affaire_Type_Commande)this._dataGridDocumentTechnique.SelectedItem);
							((App)App.Current).mySitaffEntities.Affaire_Type_Commande.DeleteObject((Affaire_Type_Commande)this._dataGridDocumentTechnique.SelectedItem);
						}
						catch (Exception)
						{
						}
                    }
                }
            }
        }

        private void _ButtonAffaireDocumentTechniqueModifier_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridDocumentTechnique.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner une doc technique à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridDocumentTechnique.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'une doc technique à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridDocumentTechnique.SelectedItem != null)
            {
                TypeDocTechniqueWindow typedocTechniqueWindow = new TypeDocTechniqueWindow();
                typedocTechniqueWindow.DataContext = (Affaire_Type_Commande)this._dataGridDocumentTechnique.SelectedItem;


                bool? dialogResult = typedocTechniqueWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
					this._dataGridDocumentTechnique.SelectedItem = (Affaire_Type_Commande)typedocTechniqueWindow.DataContext;
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Affaire_Type_Commande)typedocTechniqueWindow.DataContext);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
            this._dataGridDocumentTechnique.Items.Refresh();
        }

        private void _ButtonAffaireDocumentTechniqueNouveau_Click(object sender, RoutedEventArgs e)
        {
            TypeDocTechniqueWindow typedocTechniqueWindow = new TypeDocTechniqueWindow();
            typedocTechniqueWindow.DataContext = new Affaire_Type_Commande();
            ((Affaire_Type_Commande)typedocTechniqueWindow.DataContext).Affaire1 = (Affaire)this.DataContext;

            bool? dialogResult = typedocTechniqueWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
				((Affaire)this.DataContext).Affaire_Type_Commande.Add((Affaire_Type_Commande)typedocTechniqueWindow.DataContext);
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Affaire_Type_Commande)typedocTechniqueWindow.DataContext);
                }
                catch (Exception)
                {

                }
            }
            this._dataGridDocumentTechnique.Items.Refresh();
        }

        #endregion

        #region bouton atelier

        private void _ButtonAffaireAtelierSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridAtelier.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un atelier à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridAtelier.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les ateliers à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    try
                    {
						((App)App.Current).mySitaffEntities.Heure_Atelier.DeleteObject((Heure_Atelier)this._dataGridAtelier.SelectedItem);
                        ((Affaire)this.DataContext).Heure_Atelier.Remove((Heure_Atelier)this._dataGridAtelier.SelectedItem);
                    }
                    catch (Exception)
                    {
						try
						{
							((Affaire)this.DataContext).Heure_Atelier.Remove((Heure_Atelier)this._dataGridAtelier.SelectedItem);
							((App)App.Current).mySitaffEntities.Heure_Atelier.DeleteObject((Heure_Atelier)this._dataGridAtelier.SelectedItem);
						}
						catch (Exception)
						{
						}
                    }
                }
            }
            this._dataGridAtelier.Items.Refresh();
        }

        private void _ButtonAffaireAtelierModifier_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridAtelier.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner un atelier à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridAtelier.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'un atelier à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridChantier.SelectedItem != null)
            {
                ReleveHeureAtelierWindow releveHeureAtelierWindow = new ReleveHeureAtelierWindow();
                releveHeureAtelierWindow.DataContext = ((Heure_Atelier)this._dataGridAtelier.SelectedItem);


                bool? dialogResult = releveHeureAtelierWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
					this._dataGridAtelier.SelectedItem = (Heure_Atelier)releveHeureAtelierWindow.DataContext;
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Releve_Heure_Forfait)releveHeureAtelierWindow.DataContext);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
            this._dataGridAtelier.Items.Refresh();
        }

        private void _ButtonAffaireAtelierNouveau_Click(object sender, RoutedEventArgs e)
        {
            ReleveHeureAtelierWindow releveheureAtelierWindow = new ReleveHeureAtelierWindow();
            releveheureAtelierWindow.DataContext = new Heure_Atelier();
            ((Heure_Atelier)releveheureAtelierWindow.DataContext).Affaire1 = (Affaire)this.DataContext;

            bool? dialogResult = releveheureAtelierWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
				((Affaire)this.DataContext).Heure_Atelier.Add((Heure_Atelier)releveheureAtelierWindow.DataContext);
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Heure_Atelier)releveheureAtelierWindow.DataContext);
                }
                catch (Exception)
                {

                }
            }
            this._dataGridAtelier.Items.Refresh();
        }

        #endregion

        #region bouton chantier

        private void _ButtonAffaireChantierNouveau_Click(object sender, RoutedEventArgs e)
        {
            ReleveHeureForfaitWindow releveheureForfaitWindow = new ReleveHeureForfaitWindow();
            releveheureForfaitWindow.DataContext = new Releve_Heure_Forfait();
            ((Releve_Heure_Forfait)releveheureForfaitWindow.DataContext).Affaire1 = (Affaire)this.DataContext;

            bool? dialogResult = releveheureForfaitWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
				((Affaire)this.DataContext).Releve_Heure_Forfait.Add((Releve_Heure_Forfait)releveheureForfaitWindow.DataContext);
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Releve_Heure_Forfait)releveheureForfaitWindow.DataContext);
                }
                catch (Exception)
                {

                }
            }
            this._dataGridChantier.Items.Refresh();
        }

        private void _ButtonAffaireChantierModifier_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridChantier.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner un chantier à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridChantier.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'un chantier à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridChantier.SelectedItem != null)
            {
                ReleveHeureForfaitWindow releveheureForfaitWindow = new ReleveHeureForfaitWindow();
                releveheureForfaitWindow.DataContext = (Releve_Heure_Forfait)this._dataGridChantier.SelectedItem;


                bool? dialogResult = releveheureForfaitWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
					this._dataGridChantier.SelectedItem = (Releve_Heure_Forfait)releveheureForfaitWindow.DataContext;
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Releve_Heure_Forfait)releveheureForfaitWindow.DataContext);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
        }

        private void _ButtonAffaireChantierSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridChantier.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un chantier à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridChantier.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les chantiers à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    try
                    {
						((App)App.Current).mySitaffEntities.Releve_Heure_Forfait.DeleteObject((Releve_Heure_Forfait)this._dataGridChantier.SelectedItem);
                        ((Affaire)this.DataContext).Releve_Heure_Forfait.Remove((Releve_Heure_Forfait)this._dataGridChantier.SelectedItem);
                    }
                    catch (Exception)
                    {
						try
						{
							((Affaire)this.DataContext).Releve_Heure_Forfait.Remove((Releve_Heure_Forfait)this._dataGridChantier.SelectedItem);
							((App)App.Current).mySitaffEntities.Releve_Heure_Forfait.DeleteObject((Releve_Heure_Forfait)this._dataGridChantier.SelectedItem);
						}
						catch (Exception)
						{
						}
                    }
                }
            }
        }

        #endregion

        #region boutons combobox

        #region chargeaffaire

        private void NewChargeAffaire_Click(object sender, RoutedEventArgs e)
        {
            ListeSalarieControl listeSalarieControl = new ListeSalarieControl();
            Personne personne = ((App)App.Current)._theMainWindow.AddSalarie(listeSalarieControl);
            this.Charge_Affaire = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.Where(sal => sal.Charge_Affaire == true).OrderBy(sal => sal.Personne.Nom).ThenBy(sal => sal.Personne.Prenom));
            if (personne == null)
            {
                this._ComboBoxChargeAffaire.SelectedItem = personne;
            }
            else
            {
                if (personne.Salarie != null)
                {
                    if (personne.Salarie.Charge_Affaire == true)
                    {
                        this._ComboBoxChargeAffaire.SelectedItem = personne;
                    }
                }
            }
        }

        private void LookChargeAffaire_Click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxChargeAffaire.SelectedItem != null)
            {
                ListeSalarieControl listeSalarieControl = new ListeSalarieControl();
                ((App)App.Current)._theMainWindow.LookSalarie(listeSalarieControl, ((Salarie)this._ComboBoxChargeAffaire.SelectedItem).Personne);
            }
        }

        #endregion

        #region EntrepriseMere

        private void NewEntrepriseMere_Click(object sender, RoutedEventArgs e)
        {
            ParametreEntrepriseMereControl parametreEntrepriseMereControl = new ParametreEntrepriseMereControl();
            ParametresMain parametresMain = new ParametresMain(((App)App.Current)._theMainWindow);
            Entreprise_Mere entreprise_mere = parametresMain.AddEntrepriseMere(parametreEntrepriseMereControl);
            this.listEntreprise_Mere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(ent => ent.Nom));
            this._ComboBoxEntrepriseMere.SelectedItem = entreprise_mere;
        }

        private void LookEntrepriseMere_Click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxEntrepriseMere.SelectedItem != null)
            {
                ParametreEntrepriseMereControl parametreEntrepriseMereControl = new ParametreEntrepriseMereControl();
                ParametresMain parametresMain = new ParametresMain(((App)App.Current)._theMainWindow);
                parametresMain.LookEntrepriseMere(parametreEntrepriseMereControl, (Entreprise_Mere)this._ComboBoxEntrepriseMere.SelectedItem);
            }
        }

        #endregion

        #endregion

        #endregion
		
        #region Verifications

        private bool Verif_Generale()
		{
            bool verif = true;

            if (!this.Verif_TextBoxNumeroAffaire())
            {
                verif = false;
            }
            if (!this._verif_ChargeAffaire())
            {
                verif = false;
            }
            if (!this.Verif_ComboBoxEntrepriseMere())
            {
                verif = false;
            }
            if (!this.Verif_DatePickerDateDebut())
            {
                verif = false;
            }
            if (!this.Verif_DatePickerDate_Fin_Effective())
            {
                verif = false;
            }

            return verif;
        }

        #region Numero Affaire

        private bool Verif_TextBoxNumeroAffaire()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxNumeroAffaire, this._TextBlockNumeroAffaire);
        }

        private void _TextBoxNumeroAffaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxNumeroAffaire();
        }

        #endregion

        #region ChargeAffaire
        private bool _verif_ChargeAffaire()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxChargeAffaire, this._TextBlockChargeAffaire);
        }

        private void _ComboBoxChargeAffaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._verif_ChargeAffaire();
        }
        #endregion

        #region ComboBox Entreprise Mere
        private bool Verif_ComboBoxEntrepriseMere()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxEntrepriseMere, this._TextBlockEntrepriseMere);
        }

        private void _ComboBoxEntrepriseMere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxEntrepriseMere();
        }
        #endregion

        #region Date début

        private bool Verif_DatePickerDateDebut()
        {
			return ((App)App.Current).verifications.DatePickerSelectionNonObligatoire(this._DatePickerDateAcceptation, this._TextBlockDateAcceptation);
        }

        private void _DatePickerDateDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerDateDebut();
        }

        #endregion

        #region Date fin effective

        private bool Verif_DatePickerDate_Fin_Effective()
        {
			return ((App)App.Current).verifications.DatePickerSelectionNonObligatoire(this._DatePickerDate_Fin_Effective, this._TextBlockDate_Fin_Effective, this._DatePickerDateAcceptation);
        }

        private void _DatePickerDate_Fin_Effective_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerDate_Fin_Effective();
        }

        #endregion

        #endregion

	}
}