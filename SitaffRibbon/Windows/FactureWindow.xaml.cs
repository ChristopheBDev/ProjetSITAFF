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
using System.Collections.ObjectModel;
using System.ComponentModel;
using SitaffRibbon.Classes;
using SitaffRibbon.UserControls;
using SitaffRibbon.Windows.ParametresUserControls;
using SitaffRibbon.Windows.ParametresWindows;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour FactureWindow.xaml
    /// </summary>
    public partial class FactureWindow : Window
    {

        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region Propriétés de dépendances

        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(FactureWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Commande> listCommande
        {
            get { return (ObservableCollection<Commande>)GetValue(listCommandeProperty); }
            set { SetValue(listCommandeProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listCommande.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listCommandeProperty =
            DependencyProperty.Register("listCommande", typeof(ObservableCollection<Commande>), typeof(FactureWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Tva> listTVA
        {
            get { return (ObservableCollection<Tva>)GetValue(listTVAProperty); }
            set { SetValue(listTVAProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listTVA.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listTVAProperty =
            DependencyProperty.Register("listTVA", typeof(ObservableCollection<Tva>), typeof(FactureWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Condition_Reglement> listCondition_Reglement
        {
            get { return (ObservableCollection<Condition_Reglement>)GetValue(listCondition_ReglementProperty); }
            set { SetValue(listCondition_ReglementProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listCondition_Reglement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listCondition_ReglementProperty =
            DependencyProperty.Register("listCondition_Reglement", typeof(ObservableCollection<Condition_Reglement>), typeof(FactureWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Client> listClient
		{
			get { return (ObservableCollection<Client>)GetValue(listClientProperty); }
			set { SetValue(listClientProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listClient.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listClientProperty =
			DependencyProperty.Register("listClient", typeof(ObservableCollection<Client>), typeof(FactureWindow), new PropertyMetadata(null));

		public ObservableCollection<Salarie> listChargeAffaire
		{
			get { return (ObservableCollection<Salarie>)GetValue(listChargeAffaireProperty); }
			set { SetValue(listChargeAffaireProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listChargeAffaire.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listChargeAffaireProperty =
			DependencyProperty.Register("listChargeAffaire", typeof(ObservableCollection<Salarie>), typeof(FactureWindow), new PropertyMetadata(null));

		public ObservableCollection<Entreprise_Mere> listEntrepriseMere
		{
			get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntrepriseMereProperty); }
			set { SetValue(listEntrepriseMereProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listEntrepriseMere.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listEntrepriseMereProperty =
			DependencyProperty.Register("listEntrepriseMere", typeof(ObservableCollection<Entreprise_Mere>), typeof(FactureWindow), new PropertyMetadata(null));

		public ObservableCollection<Contact> listContact
		{
			get { return (ObservableCollection<Contact>)GetValue(listContactProperty); }
			set { SetValue(listContactProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listContact.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listContactProperty =
			DependencyProperty.Register("listContact", typeof(ObservableCollection<Contact>), typeof(FactureWindow), new PropertyMetadata(null));

		public ObservableCollection<Article_Facture> listArticleFacture
		{
			get { return (ObservableCollection<Article_Facture>)GetValue(listArticleFactureProperty); }
			set { SetValue(listArticleFactureProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listArticleFacture.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listArticleFactureProperty =
			DependencyProperty.Register("listArticleFacture", typeof(ObservableCollection<Article_Facture>), typeof(FactureWindow), new PropertyMetadata(null));

		public ObservableCollection<Proforma_Client> listProformaClient
		{
			get { return (ObservableCollection<Proforma_Client>)GetValue(listProformaClientProperty); }
			set { SetValue(listProformaClientProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listProformaClient.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listProformaClientProperty =
			DependencyProperty.Register("listProformaClient", typeof(ObservableCollection<Proforma_Client>), typeof(FactureWindow), new PropertyMetadata(null));

        #endregion

        #region Constructeur

        public FactureWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Initialisation de la sécurité
            this.initialisationSecurite();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._comboBoxClient.Focus();
        }

        #region initialisation

        private void initialisationPropDependance()
        {
			this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
			this.listArticleFacture = new ObservableCollection<Article_Facture>(((App)App.Current).mySitaffEntities.Article_Facture.OrderBy(art => art.Code));
			this.listChargeAffaire = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.Where(sal => sal.Charge_Affaire != null && sal.Charge_Affaire == true).Where(sal => sal.Personne != null).OrderBy(sal => sal.Personne.Nom).ThenBy(sal => sal.Personne.Prenom));
			this.listClient = new ObservableCollection<Client>(((App)App.Current).mySitaffEntities.Client.OrderBy(cli => cli.Entreprise.Libelle));
			this.listCommande = new ObservableCollection<Commande>(((App)App.Current).mySitaffEntities.Commande.Where(com => com.Numero_Commande != "" && com.Numero_Commande != null).OrderBy(com => com.Numero_Commande));
			this.listCondition_Reglement = new ObservableCollection<Condition_Reglement>(((App)App.Current).mySitaffEntities.Condition_Reglement.OrderBy(con => con.Libelle));
			this.listEntrepriseMere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(ent => ent.Nom));
			this.listTVA = new ObservableCollection<Tva>(((App)App.Current).mySitaffEntities.Tva.OrderBy(tv => tv.Taux));
        }

        private void initialisationSecurite()
        {
            //Mise en place des droits sur les boutons et tabs
			
        }

        #endregion

        #endregion

        #region Boutons

		#region Bouton Ok/Annuler
		
		/// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
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

		#region Contenu
		
		private void _buttonCalculer_Click_1(object sender, RoutedEventArgs e)
		{
			this.calculer();			
		}

		private void _buttonSupprimer_Click_1(object sender, RoutedEventArgs e)
		{
			if (this._dataGridContenuFacture.SelectedItems.Count == 1)
			{
				((Facture)this.DataContext).Contenu_Facture.Remove((Contenu_Facture)this._dataGridContenuFacture.SelectedItem);
			}
		}

		#endregion

		#region Reglements
		
		private void _ButtonRgltClientNouveau_Click_1(object sender, RoutedEventArgs e)
		{
			//Création de l'objet temporaire
			Reglement_Client tmp = new Reglement_Client();
			tmp.Reglement_Client_Facture = new System.Data.Objects.DataClasses.EntityCollection<Reglement_Client_Facture>();

			Reglement_Client_Facture tmpbis = new Reglement_Client_Facture();
			tmpbis.Facture1 = (Facture)this.DataContext;
			tmpbis.Montant = ((Facture)this.DataContext).Net_A_Payer;
			tmp.Reglement_Client_Facture.Add(tmpbis);

			//Initialisation de la fenêtre
			ReglementClientWindow reglementClientWindow = new ReglementClientWindow();

			//Mise de l'objet temporaire dans le datacontext
			reglementClientWindow.DataContext = tmp;

			//booléen nullable vrai ou faux ou null
			bool? dialogResult = reglementClientWindow.ShowDialog();

			if (dialogResult.HasValue && dialogResult.Value == true)
			{
				foreach (Reglement_Client_Facture item in ((Reglement_Client)reglementClientWindow.DataContext).Reglement_Client_Facture.Where(reg => reg.Facture1.Identifiant == ((Facture)this.DataContext).Identifiant))
				{
					((Facture)this.DataContext).Reglement_Client_Facture.Add(item);
				}
			}
			else
			{
				try
				{
					//On détache l'avance
					((Reglement_Client)reglementClientWindow.DataContext).Reglement_Client_Facture = null;
					foreach (Reglement_Client_Facture item in ((Reglement_Client)reglementClientWindow.DataContext).Reglement_Client_Facture)
					{
						((App)App.Current).mySitaffEntities.Detach((Reglement_Client_Facture)item);
					}
					((App)App.Current).mySitaffEntities.Detach((Reglement_Client)reglementClientWindow.DataContext);
				}
				catch (Exception)
				{
				}
			}
			this._dataGridRgltClient.Items.Refresh();
		}

		private void _ButtonRgltClientModifier_Click_1(object sender, RoutedEventArgs e)
		{
			if (this._dataGridRgltClient.SelectedItem != null)
			{
				if (this._dataGridRgltClient.SelectedItems.Count == 1)
				{
					//Création de la fenêtre
					ReglementClientWindow reglementClientWindow = new ReglementClientWindow();

					//Initialisation du Datacontext en Avance et association à la Avance sélectionnée
					reglementClientWindow.DataContext = new Reglement_Client();
					reglementClientWindow.DataContext = ((Reglement_Client_Facture)this._dataGridRgltClient.SelectedItem).Reglement_Client1;

					//booléen nullable vrai ou faux ou null
					bool? dialogResult = reglementClientWindow.ShowDialog();

					if (dialogResult.HasValue && dialogResult.Value == true)
					{
					}
					else
					{
						try
						{
							foreach (Reglement_Client_Facture item in ((Reglement_Client)reglementClientWindow.DataContext).Reglement_Client_Facture)
							{
								//Je récupère les anciennes données de la base sur les modifications effectuées
								((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Reglement_Client_Facture)item);
							}
						}
						catch (Exception)
						{
						}
						
					}
				}
				else
				{
					MessageBox.Show("Vous ne devez sélectionner qu'un seul réglement.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner un réglement.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
			this._dataGridRgltClient.Items.Refresh();
		}

		private void _ButtonRgltClientSupprimer_Click_1(object sender, RoutedEventArgs e)
		{
			if (this._dataGridRgltClient.SelectedItem != null)
			{
				if (this._dataGridRgltClient.SelectedItems.Count == 1)
				{
					if (MessageBox.Show("Voulez-vous rééllement supprimer le réglement séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
					{
						try
						{
							Reglement_Client tmp = ((Reglement_Client_Facture)this._dataGridRgltClient.SelectedItem).Reglement_Client1;
							((App)App.Current).mySitaffEntities.Reglement_Client_Facture.DeleteObject((Reglement_Client_Facture)this._dataGridRgltClient.SelectedItem);
							if (tmp.Reglement_Client_Facture.Count == 0)
							{
								((App)App.Current).mySitaffEntities.Reglement_Client.DeleteObject((Reglement_Client)tmp);
							}
						}
						catch (Exception)
						{
							try
							{
								((Facture)this.DataContext).Reglement_Client_Facture.Remove((Reglement_Client_Facture)this._dataGridRgltClient.SelectedItem);
							}
							catch (Exception)
							{
								try
								{
									((App)App.Current).mySitaffEntities.Detach((Reglement_Client_Facture)this._dataGridRgltClient.SelectedItem);
								}
								catch (Exception)
								{

								}
							}
						}
					}
				}
				else
				{
					MessageBox.Show("Vous ne devez sélectionner qu'un seul réglement.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner un réglement.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
			this._dataGridRgltClient.Items.Refresh();
		}
		
		#endregion

		#region Proforma
		
		private void _buttonDroiteGauche_Click_1(object sender, RoutedEventArgs e)
		{
			if (this._dataGridFactureProforma2.SelectedItem != null && this._dataGridFactureProforma2.SelectedItems.Count == 1)
			{
				this.listProformaClient.Add((Proforma_Client)this._dataGridFactureProforma1.SelectedItem);
				((Facture)this.DataContext).Facture_Client.Proforma_Client.Remove((Proforma_Client)this._dataGridFactureProforma1.SelectedItem);
			}
		}

		private void _buttonGaucheDroite_Click_1(object sender, RoutedEventArgs e)
		{
			if (this._dataGridFactureProforma1.SelectedItem != null && this._dataGridFactureProforma1.SelectedItems.Count == 1)
			{
				((Facture)this.DataContext).Facture_Client.Proforma_Client.Add((Proforma_Client)this._dataGridFactureProforma1.SelectedItem);
				this.listProformaClient.Remove((Proforma_Client)this._dataGridFactureProforma1.SelectedItem);
			}
		}

		#endregion

		#region Litiges

		private void _ButtonLitigeNouveau_Click_1(object sender, RoutedEventArgs e)
		{
			LitigeFactureClientWindow litigeWindow = new LitigeFactureClientWindow();
			litigeWindow.DataContext = new Litige_Facture();
			((Litige_Facture)litigeWindow.DataContext).Facture1 = (Facture)this.DataContext;
			((Litige_Facture)litigeWindow.DataContext).Date_Litige = DateTime.Today;

			bool? dialogResult = litigeWindow.ShowDialog();

			if (dialogResult.HasValue && dialogResult == true)
			{
				((Facture)this.DataContext).Litige_Facture.Add((Litige_Facture)litigeWindow.DataContext);
			}
			else
			{
				try
				{
					((App)App.Current).mySitaffEntities.Detach((Litige_Facture)litigeWindow.DataContext);
				}
				catch (Exception)
				{
				}
			}

			this._dataGridLitigeFactureClient.Items.Refresh();

		}

		private void _ButtonLitigeModifier_Click_1(object sender, RoutedEventArgs e)
		{
			if (this._dataGridLitigeFactureClient.SelectedItems.Count == 1)
			{
				LitigeFactureClientWindow litigeWindow = new LitigeFactureClientWindow();
				litigeWindow.DataContext = (Litige_Facture)this._dataGridLitigeFactureClient.SelectedItem;

				bool? dialogResult = litigeWindow.ShowDialog();

				if (dialogResult.HasValue && dialogResult == true)
				{
				}
				else
				{
					//Je récupère les anciennes données de la base sur les modifications effectuées
					((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Litige_Facture)(litigeWindow.DataContext));
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner un litige.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		private void _ButtonLitigeSupprimer_Click_1(object sender, RoutedEventArgs e)
		{
			if (this._dataGridLitigeFactureClient.SelectedItems.Count == 1)
			{
				if (MessageBox.Show("Voulez-vous rééllement supprimer le litige séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					try
					{
						((App)App.Current).mySitaffEntities.Litige_Facture.DeleteObject((Litige_Facture)this._dataGridLitigeFactureClient.SelectedItem);
					}
					catch (Exception)
					{
						try
						{
							((Facture)this.DataContext).Litige_Facture.Remove((Litige_Facture)this._dataGridLitigeFactureClient.SelectedItem);
						}
						catch (Exception)
						{
							try
							{
								((App)App.Current).mySitaffEntities.Detach((Litige_Facture)this._dataGridLitigeFactureClient.SelectedItem);
							}
							catch (Exception)
							{

							}
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner un litige.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
			this._dataGridLitigeFactureClient.Items.Refresh();
		}

		#endregion

		#region Relance

		private void _ButtonRelanceNouveau_Click_1(object sender, RoutedEventArgs e)
		{
			Relance_Facture relance = new Relance_Facture();
			relance.Facture1 = (Facture)this.DataContext;

			RelanceFactureWindow relanceWindow = new RelanceFactureWindow();
			relance.Facture1 = (Facture)this.DataContext;
			relance.Affaire1 = ((Facture)this.DataContext).Affaire1;
			relance.Client1 = ((Facture)this.DataContext).Client1;
			relanceWindow.DataContext = (Relance_Facture)relance;

			bool? dialogResult = relanceWindow.ShowDialog();

			if (dialogResult.HasValue && dialogResult.Value == true)
			{
				((Facture)this.DataContext).Relance_Facture.Add((Relance_Facture)relanceWindow.DataContext);
			}
			else
			{
				try
				{
					((App)App.Current).mySitaffEntities.Detach(relanceWindow.DataContext);
				}
				catch (Exception)
				{
					try
					{
						((App)App.Current).mySitaffEntities.Detach(relanceWindow.DataContext);
					}
					catch (Exception)
					{
					}
				}
			}

		}

		private void _ButtonRelanceModifier_Click_1(object sender, RoutedEventArgs e)
		{
			if (this._dataGridRelanceFactureClient.SelectedItems.Count == 1)
			{
				RelanceFactureWindow relanceWindow = new RelanceFactureWindow();
				relanceWindow.DataContext = (Relance_Facture)this._dataGridRelanceFactureClient.SelectedItem;

				bool? dialogResult = relanceWindow.ShowDialog();

				if (dialogResult.HasValue && dialogResult == true)
				{
				}
				else
				{
					try
					{						
						//Je récupère les anciennes données de la base sur les modifications effectuées
						((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Relance_Facture)(relanceWindow.DataContext));
					}
					catch (Exception)
					{
					}
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner une relance.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		private void _ButtonRelanceSupprimer_Click_1(object sender, RoutedEventArgs e)
		{
			if (this._dataGridRelanceFactureClient.SelectedItems.Count == 1)
			{
				if (MessageBox.Show("Voulez-vous rééllement supprimer la relance séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					try
					{
						((App)App.Current).mySitaffEntities.Relance_Facture.DeleteObject((Relance_Facture)this._dataGridRelanceFactureClient.SelectedItem);
					}
					catch (Exception)
					{
						try
						{
							((Facture)this.DataContext).Relance_Facture.Remove((Relance_Facture)this._dataGridRelanceFactureClient.SelectedItem);
						}
						catch (Exception)
						{
							try
							{
								((App)App.Current).mySitaffEntities.Detach((Relance_Facture)this._dataGridRelanceFactureClient.SelectedItem);
							}
							catch (Exception)
							{

							}
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner une relance.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
			this._dataGridRelanceFactureClient.Items.Refresh();
		}

		#endregion
		
		#endregion

		#region Vérifications

		private bool VerificationChamps()
		{
			bool verif = true;

			if (!Verif_Affaire())
			{
				verif = false;
			}
			if (!Verif_TotalTTC())
			{
				verif = false;
			}
			if (!Verif_TotalHT())
			{
				verif = false;
			}
			if (!Verif_DateEchance())
			{
				verif = false;
			}
			if (!Verif_ConditionRglt())
			{
				verif = false;
			}
			if (!Verif_Commentaire())
			{
				verif = false;
			}
			if (!Verif_DateFacture())
			{
				verif = false;
			}
			if (!Verif_EntrepriseMere())
			{
				verif = false;
			}
			if (!Verif_Contact())
			{
				verif = false;
			}
			if (!Verif_ChargeAffaire())
			{
				verif = false;
			}
			if (!Verif_Commande())
			{
				verif = false;
			}
			if (!Verif_Client())
			{
				verif = false;
			}
			if (!Verif_Facture())
			{
				verif = false;
			}
			if (!Verif_Acompte())
			{
				verif = false;
			}
			if (!Verif_MontantTVA())
			{
				verif = false;
			}
			if (!Verif_NetAPayer())
			{
				verif = false;
			}
			if (!Verif_Contenu())
			{
				verif = false;
			}
			if (!Verif_DataGridFactureProforma())
			{
				verif = false;
			}
			if (!Verif_DataGridLitiges())
			{
				verif = false;
			}
			if (!Verif_DataGridRelances())
			{
				verif = false;
			}
			if (!Verif_DataGridRgltClients())
			{
				verif = false;
			}
			if (!Verif_EnTeteContenu())
			{
				verif = false;
			}
			if (!VerifNumeroUnique())
			{
				verif = false;
			}
			
			return verif;
		}

		#region Affaire

		private bool Verif_Affaire()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxAffaire, this._textBlockAffaire);
		}

		private void _comboBoxAffaire_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			if (Verif_Affaire() && this._comboBoxAffaire.SelectedItem != null)
			{
				//Selection du client
				if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.Count > 0)
				{
					if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First() != null)
					{
						if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First().Devis1 != null)
						{
							if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First().Devis1.Client != null)
							{								
								this._comboBoxClient.SelectedItem = ((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First().Devis1.Client;								
							}
						}						
					}
				}

				//Selection Chargé affaire				
				if (((Affaire)this._comboBoxAffaire.SelectedItem).Salarie != null)
				{
					this._comboBoxChargeAffaire.SelectedItem = ((Affaire)this._comboBoxAffaire.SelectedItem).Salarie;
				}				

				//Selection Contact
				if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.Count > 0)
				{
					if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First() != null)
					{
						if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First().Devis1 != null)
						{
							if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First().Devis1.Devis_Contact.Count > 0)
							{
								if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First().Devis1.Devis_Contact.First() != null)
								{
									this._comboBoxContact.SelectedItem = ((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First().Devis1.Devis_Contact.First();
								}
							}
						}
					}
				}

				//Selection Entreprise Mere
				if (((Affaire)this._comboBoxAffaire.SelectedItem).Entreprise_Mere1 != null)
				{
					this._comboBoxEntrepriseMere.SelectedItem = ((Affaire)this._comboBoxAffaire.SelectedItem).Entreprise_Mere1;
				}				

				//Selection Commande
				if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.Count > 0)
				{
					if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First().Devis1 != null)
					{
						if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First().Commande1 != null)
						{
							MAJListCommande();
							this._comboBoxCommande.SelectedItem = ((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First().Commande1;
						}
					}
				}

				MAJListProformaClient();
			}
		}

		#endregion

		#region Facture

		private bool Verif_Facture()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxFacture, this._textBlockFacture);
		}

		private void _textBoxFacture_LostFocus_1(object sender, RoutedEventArgs e)
		{
			if (Verif_Facture())
			{				
				VerifNumeroUnique();
			}
		}

		#endregion

		#region Client

		private bool Verif_Client()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxClient, this._textBlockClient);
		}

		private void _comboBoxClient_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			if (Verif_Client())
			{
				MAJListContact();
				MAJListProformaClient();
			}
		}

		#endregion

		#region Commande

		private bool Verif_Commande()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxCommande, this._textBlockCommande);
		}

		private void _comboBoxCommande_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			if (Verif_Commande())
			{
				if (this._comboBoxCommande.SelectedItem != null)
				{
					if (((Commande)this._comboBoxCommande.SelectedItem).Versions != null && ((Commande)this._comboBoxCommande.SelectedItem).Versions.Count > 0)
					{
						if (((Commande)this._comboBoxCommande.SelectedItem).Versions.First() != null)
						{
							if (((Commande)this._comboBoxCommande.SelectedItem).Versions.First().Devis1 != null)
							{
								if (((Commande)this._comboBoxCommande.SelectedItem).Versions.First().Devis1.Devis_Contact != null && ((Commande)this._comboBoxCommande.SelectedItem).Versions.First().Devis1.Devis_Contact.Count > 0)
								{
									if (((Commande)this._comboBoxCommande.SelectedItem).Versions.First().Devis1.Devis_Contact.First() != null)
									{
										if (((Commande)this._comboBoxCommande.SelectedItem).Versions.First().Devis1.Devis_Contact.First().Contact1 != null)
										{
											this._comboBoxContact.SelectedItem = ((Commande)this._comboBoxCommande.SelectedItem).Versions.First().Devis1.Devis_Contact.First().Contact1;
										}
									}
								}
							}
						}
					}
					MAJListProformaClient();
				}								
			}
		}

		#endregion

		#region ChargeAffaire

		private bool Verif_ChargeAffaire()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxChargeAffaire, this._textBlockChargeAffaire);
		}

		private void _comboBoxChargeAffaire_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_ChargeAffaire();
		}

		#endregion

		#region Contact

		private bool Verif_Contact()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxContact, this._textBlockContact);
		}

		private void _comboBoxContact_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_Contact();			
		}

		#endregion

		#region EntrepriseMere

		private bool Verif_EntrepriseMere()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxEntrepriseMere, this._textBlockEntrepriseMere);
		}

		private void _comboBoxEntrepriseMere_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_EntrepriseMere();
		}

		#endregion

		#region DateFacture

		private bool Verif_DateFacture()
		{
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateFacture, this._textBlockDateFacture);
		}

		private void _datePickerDateFacture_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_DateFacture();
		}

		#endregion

		#region Commentaire

		private bool Verif_Commentaire()
		{
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxCommentaire, this._textBlockCommentaire);
		}

		private void _textBoxCommentaire_TextChanged_1(object sender, TextChangedEventArgs e)
		{
			Verif_Commentaire();
		}
		
		#endregion

		#region Condition Rglt

		private bool Verif_ConditionRglt()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxConditionRglt, this._textBlockConditionRglt);
		}

		private void _comboBoxConditionRglt_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_ConditionRglt();
		}

		#endregion

		#region DateEchance

		private bool Verif_DateEchance()
		{
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateEcheance, this._textBlockDateEcheance, this._datePickerDateFacture);
		}

		private void _datePickerDateEcheance_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_DateEchance();
		}

		#endregion
		
		#region TotalHT

		private bool Verif_TotalHT()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxTotalHT, this._textBlockTotalHT);
		}

		private void _textBoxTotalHT_TextChanged_1(object sender, TextChangedEventArgs e)
		{
			Verif_TotalHT();
		}

		#endregion

		#region TotalTTC

		private bool Verif_TotalTTC()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxTotalTTC, this._textBlockTotalTTC);
		}

		private void _textBoxTotalTTC_TextChanged_1(object sender, TextChangedEventArgs e)
		{
			Verif_TotalTTC();
		}

		#endregion

		#region Acompte

		private bool Verif_Acompte()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxAcompte, this._textBlockAcompte);
		}

		private void _textBoxAcompte_TextChanged_1(object sender, TextChangedEventArgs e)
		{
			Verif_Acompte();
		}

		#endregion

		#region MontantTVA

		private bool Verif_MontantTVA()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxMontantTVA, this._textBlockMontantTVA);
		}

		private void _textBoxMontantTVA_TextChanged_1(object sender, TextChangedEventArgs e)
		{
			Verif_MontantTVA();
		}

		#endregion

		#region Net A Payer

		private bool Verif_NetAPayer()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxNetAPayer,this._textBlockNetAPayer);
		}

		private void _textBoxNetAPayer_TextChanged_1(object sender, TextChangedEventArgs e)
		{
			Verif_NetAPayer();
		}

		#endregion

		#region DataGridContenu

		private bool Verif_Contenu()
		{
			bool verif = true;
			if (((Facture)this.DataContext).Contenu_Facture.Count == 0)
			{
				verif = false;
			}
			else
			{
				if (this._checkBoxExonere.IsChecked == true)
				{
					foreach (Contenu_Facture item in ((Facture)this.DataContext).Contenu_Facture)
					{
						if (item.Article_Facture1 != null)
						{
							if (item.Article_Facture1.Plan_Comptable_Imputation2 == null)
							{
								item.Article_Facture1 = null;
							}
						}
					}
				}

				foreach (Contenu_Facture item in ((Facture)this.DataContext).Contenu_Facture)
				{
					if (item.Article_Facture1 == null)
					{
						verif = false;
					}
				}
			}

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemContenuFacture, verif);
			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridContenuFacture, verif);

			return verif;
		}

		#endregion

		#region DataGridFactureProforma

		private bool Verif_DataGridFactureProforma()
		{
			bool verif = true;

			((App)App.Current).verifications.MettreTabItemEnCouleur(_tabItemFactureProforma, verif);

			return verif;
		}

		#endregion

		#region DataGridLitiges

		private bool Verif_DataGridLitiges()
		{
			bool verif = true;

			((App)App.Current).verifications.MettreTabItemEnCouleur(_tabItemLitigeFactureClient, verif);

			return verif;
		}

		#endregion

		#region DataGridRelances

		private bool Verif_DataGridRelances()
		{
			bool verif = true;

			((App)App.Current).verifications.MettreTabItemEnCouleur(_tabItemRelanceFactureClient, verif);

			return verif;
		}

		#endregion

		#region DataGridRgltClients

		private bool Verif_DataGridRgltClients()
		{
			bool verif = true;

			((App)App.Current).verifications.MettreTabItemEnCouleur(_tabItemRgltClient, verif);

			return verif;
		}

		#endregion

		#region OngletEnTeteContenu

		private bool Verif_EnTeteContenu()
		{
			bool verif = true;

			((App)App.Current).verifications.MettreTabItemEnCouleur(_tabItemCommentaireContenu, verif);

			return verif;
		}

		#endregion

		#endregion

		#region Lecture Seule

		public void lectureSeule()
        {
			//Button
			this._buttonCalculer.IsEnabled = false;
			this._buttonDroiteGauche.IsEnabled = false;
			this._buttonGaucheDroite.IsEnabled = false;
			this._ButtonLitigeModifier.IsEnabled = false;
			this._ButtonLitigeNouveau.IsEnabled = false;
			this._ButtonLitigeSupprimer.IsEnabled = false;
			this._ButtonRelanceModifier.IsEnabled = false;
			this._ButtonRelanceNouveau.IsEnabled = false;
			this._ButtonRelanceSupprimer.IsEnabled = false;
			this._ButtonRgltClientModifier.IsEnabled = false;
			this._ButtonRgltClientNouveau.IsEnabled = false;
			this._ButtonRgltClientSupprimer.IsEnabled = false;
			this._buttonSupprimer.IsEnabled = false;

			//Checkbox
			this._checkBoxExonere.IsEnabled = false;
			this._checkBoxVerouiller.IsEnabled = false;

			//ComboBox
			this._comboBoxAffaire.IsEnabled = false;
			this._comboBoxChargeAffaire.IsEnabled = false;
			this._comboBoxClient.IsEnabled = false;
			this._comboBoxCommande.IsEnabled = false;
			this._comboBoxConditionRglt.IsEnabled = false;
			this._comboBoxContact.IsEnabled = false;
			this._comboBoxEntrepriseMere.IsEnabled = false;

			//DataGrid
			this._dataGridContenuFacture.IsEnabled = false;

			//DatePicker
			this._datePickerDateEcheance.IsEnabled = false;
			this._datePickerDateFacture.IsEnabled = false;
			
			//TextBox
			this._textBoxAcompte.IsEnabled = false;
			this._textBoxCommentaire.IsEnabled = false;
			this._TextBoxDescription.IsEnabled = false;
			this._textBoxFacture.IsEnabled = false;
			this._textBoxMontantTVA.IsEnabled = false;
			this._textBoxNetAPayer.IsEnabled = false;
			this._textBoxTotalHT.IsEnabled = false;
			this._textBoxTotalTTC.IsEnabled = false;
        }

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;

			DateAuto();
			
			if (soloLecture || ((Facture)this.DataContext).Exporte_Maxima)
			{
				lectureSeule();
			}
        }

        #endregion

        #region Evenements

        #region KeyUp

        private void _textBoxMontant_KeyUp(object sender, KeyEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
        }

        #endregion

		private void _TextBoxDescription_LostFocus_1(object sender, RoutedEventArgs e)
		{
			try
			{
				((Contenu_Facture)((TextBox)sender).DataContext).Description = ((TextBox)sender).Text;
			}
			catch (Exception) { }
		}

		private void _dataGridContenuFacture_KeyUp_1(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.Key != Key.Tab)
				{
					ReglageDecimales reg = new ReglageDecimales();
					switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
					{
						case "Quantité":
							reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
							break;
						case "Prix Unitaire":
							reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
							break;
						default:
							break;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void _checkBoxExonere_Checked_1(object sender, RoutedEventArgs e)
		{
			calculer();

			this.listArticleFacture = new ObservableCollection<Article_Facture>(((App)App.Current).mySitaffEntities.Article_Facture.Where(art => art.Plan_Comptable_Imputation2 != null).OrderBy(art => art.Code));
		}

		private void _checkBoxExonere_Unchecked_1(object sender, RoutedEventArgs e)
		{
			calculer();

			this.listArticleFacture = new ObservableCollection<Article_Facture>(((App)App.Current).mySitaffEntities.Article_Facture.OrderBy(art => art.Libelle));
		}

        #endregion

		#region Fonctions
		
		public void calculer()
		{
			Verif_Contenu();

			double totalHT = 0;
			double totalTVA = 0;
			double totalTTC = 0;
			double totalAcompte = 0;

			foreach (Contenu_Facture item in ((Facture)this.DataContext).Contenu_Facture)
			{
				if (item.Prix_Unitaire != null && item.Quantite != null)
				{
					item.Prix_Total = item.Prix_Unitaire * item.Quantite;
					totalHT = totalHT + item.Prix_Total;

					if (item.Article_Facture1 != null)
					{
						if (item.Article_Facture1.Plan_Comptable_Tva1 != null)
						{
							if (item.Article_Facture1.Plan_Comptable_Tva1.Tva1 != null)
							{
								if (item.Article_Facture1.Plan_Comptable_Tva1.Tva1.Taux != null)
								{
									item.Tva = item.Article_Facture1.Plan_Comptable_Tva1.Tva1.Taux;
									if (((Facture)this.DataContext).Exonere == false)
									{
										totalTVA = totalTVA + (item.Prix_Total * (item.Article_Facture1.Plan_Comptable_Tva1.Tva1.Taux / 100));
									}
								}
							}
						}
						if (item.Article_Facture1.Condition != null)
						{
							item.Conditions = item.Article_Facture1.Condition;
						}
					}
				}
			}

			foreach (Proforma_Client item in ((Facture)this.DataContext).Facture_Client.Proforma_Client)
			{
				double test;
				if (double.TryParse(item.Facture.Net_A_Payer.ToString(), out test))
				{
					totalAcompte = totalAcompte + double.Parse(item.Facture.Net_A_Payer.ToString());
				}
			}

			totalTTC = totalHT + totalTVA;
			((Facture)this.DataContext).Montant = totalHT;
			((Facture)this.DataContext).Montant_TVA = totalTVA;
			((Facture)this.DataContext).Montant_TTC = totalTTC;
			((Facture)this.DataContext).Facture_Client.Acompte = totalAcompte;
			((Facture)this.DataContext).Net_A_Payer = totalTTC - totalAcompte;

			try
			{
				this._dataGridContenuFacture.Items.Refresh();
			}
			catch (Exception)
			{
			}
		}

		private void DateAuto()
		{
			if (((Facture)this.DataContext).Date_Facture == null)
			{
				((Facture)this.DataContext).Date_Facture = DateTime.Today;
			}
		}

		private bool VerifNumeroUnique()
		{
			bool verif = true;

			if (((App)App.Current).mySitaffEntities.Facture.Where(fac => fac.Identifiant != ((Facture)this.DataContext).Identifiant).
                    Where(fac => fac.Numero.Trim().ToLower() == this._textBoxFacture.Text.Trim().ToLower()).Count() != 0)
			{
				verif = false;
			}

			((App)App.Current).verifications.MettreTextBoxEnCouleur(_textBoxFacture, _textBlockFacture, verif);

			return verif; ;
		}

		#region ReglagesPropDep

		private void MAJListCommande()
		{
			if (this._comboBoxAffaire.SelectedItem != null)
			{
				this.listCommande = new ObservableCollection<Commande>();
				foreach (Versions item in ((Affaire)this._comboBoxAffaire.SelectedItem).Versions)
				{
					if (item.Commande1 != null)
					{
						this.listCommande.Add(item.Commande1);
					}
				}
			}
		}

		private void MAJListContact()
		{
			this.listContact = new ObservableCollection<Contact>();

			if (this._comboBoxClient.SelectedItem != null)
			{
				foreach (Personne item in ((Client)this._comboBoxClient.SelectedItem).Entreprise.Personne)
				{
					if (item.Contact != null)
					{
						this.listContact.Add(item.Contact);
					}
				}
			}
		}

		private void MAJListProformaClient()
		{
			if (this._comboBoxCommande.SelectedItem != null && this._comboBoxClient.SelectedItem != null && this._comboBoxAffaire.SelectedItem != null)
			{
				this.listProformaClient = new ObservableCollection<Proforma_Client>(((App)App.Current).mySitaffEntities.Proforma_Client.
					Where(pro => pro.Facture.Affaire1.Identifiant == ((Affaire)this._comboBoxAffaire.SelectedItem).Identifiant).
					Where(pro => pro.Facture.Commande1.Identifiant == ((Commande)this._comboBoxCommande.SelectedItem).Identifiant).
					Where(pro => pro.Facture.Client1.Identifiant == ((Client)this._comboBoxClient.SelectedItem).Identifiant).
					OrderBy(pro => pro.Facture.Numero));
			}
			else if (this._comboBoxCommande.SelectedItem != null && this._comboBoxAffaire.SelectedItem != null)
			{
				this.listProformaClient = new ObservableCollection<Proforma_Client>(((App)App.Current).mySitaffEntities.Proforma_Client.
					Where(pro => pro.Facture.Affaire1.Identifiant == ((Affaire)this._comboBoxAffaire.SelectedItem).Identifiant).
					Where(pro => pro.Facture.Commande1.Identifiant == ((Commande)this._comboBoxCommande.SelectedItem).Identifiant).
					OrderBy(pro => pro.Facture.Numero));
			}
			else if (this._comboBoxClient.SelectedItem != null)
			{
				this.listProformaClient = new ObservableCollection<Proforma_Client>(((App)App.Current).mySitaffEntities.Proforma_Client.
					Where(pro => pro.Facture.Client1.Identifiant == ((Client)this._comboBoxClient.SelectedItem).Identifiant).
					OrderBy(pro => pro.Facture.Numero));
			}	
		}

		#endregion

		#endregion
		
	}
}
