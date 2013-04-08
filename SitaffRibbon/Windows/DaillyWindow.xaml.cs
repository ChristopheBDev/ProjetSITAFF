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
    /// Logique d'interaction pour DaillyWindow.xaml
    /// </summary>
    public partial class DaillyWindow : Window
	{
		#region Attributs

		public bool soloLecture = false;

		#endregion

		#region Propriétés de dépendances

		public ObservableCollection<Entreprise_Mere> listEntrepriseMere
		{
			get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntrepriseMereProperty); }
			set { SetValue(listEntrepriseMereProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listEntrepriseMere.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listEntrepriseMereProperty =
			DependencyProperty.Register("listEntrepriseMere", typeof(ObservableCollection<Entreprise_Mere>), typeof(DaillyWindow), new PropertyMetadata(null));

        public ObservableCollection<Client> listClient
        {
            get { return (ObservableCollection<Client>)GetValue(listClientProperty); }
            set { SetValue(listClientProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listClient.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listClientProperty =
            DependencyProperty.Register("listClient", typeof(ObservableCollection<Client>), typeof(DaillyWindow), new PropertyMetadata(null));

        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(DaillyWindow), new PropertyMetadata(null));

		public ObservableCollection<Banque> listBanque
		{
			get { return (ObservableCollection<Banque>)GetValue(listBanqueProperty); }
			set { SetValue(listBanqueProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listBanque.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listBanqueProperty =
			DependencyProperty.Register("listBanque", typeof(ObservableCollection<Banque>), typeof(DaillyWindow), new PropertyMetadata(null));

		public ObservableCollection<Commande> listCommande
		{
			get { return (ObservableCollection<Commande>)GetValue(listCommandeProperty); }
			set { SetValue(listCommandeProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listCommande.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listCommandeProperty =
			DependencyProperty.Register("listCommande", typeof(ObservableCollection<Commande>), typeof(DaillyWindow), new PropertyMetadata(null));

		#endregion

		#region Construteur

		public DaillyWindow()
		{
			InitializeComponent();

			//Initialisation de la sécurité
			InitialisationSecurite();

			//Initialisation des dépendances
			InitialisationPropDep();

			//Placement du curseur
			this._textBoxNumeroDailly.Focus();
		}

		#region Initialisation

		private void InitialisationSecurite()
		{
			
		}

		private void InitialisationPropDep()
		{
			listBanque = new ObservableCollection<Banque>(((App)App.Current).mySitaffEntities.Banque.OrderBy(ban => ban.Libelle));
			listCommande = new ObservableCollection<Commande>(((App)App.Current).mySitaffEntities.Commande.Where(com => com.Dailly1 == null).Where(com => com.Numero_Commande != null && com.Numero_Commande != "").OrderBy(com => com.Numero_Commande));
            this.listAffaire = new ObservableCollection<Affaire>();
            this.listClient = new ObservableCollection<Client>();
			this.listEntrepriseMere = new ObservableCollection<Entreprise_Mere>();
            foreach (Commande item in listCommande)
            {
                if (item.getAffaire != null)
                {
                    if (listAffaire.Where(aff => aff.Identifiant == item.getAffaire.Identifiant).Count() == 0)
                    {
                        listAffaire.Add(item.getAffaire);
						if (listEntrepriseMere.Where(ent => ent.Identifiant == item.getAffaire.Entreprise_Mere1.Identifiant).Count() == 0)
						{
							listEntrepriseMere.Add(item.getAffaire.Entreprise_Mere1);
						}
                    }
                }
                if (item.getClient != null)
                {
                    if (listClient.Where(aff => aff.Identifiant == item.getClient.Identifiant).Count() == 0)
                    {
                        listClient.Add(item.getClient);
                    }
                }
            }
            listAffaire = new ObservableCollection<Affaire>(listAffaire.OrderBy(aff => aff.Numero));
            listClient = new ObservableCollection<Client>(listClient.OrderBy(aff => aff.Entreprise.Libelle));
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

		#region Vidoir
		
		//Ajoute un élément
		private void _buttonGaucheDroite_Click_1(object sender, RoutedEventArgs e)
		{
			if (this._dataGridCommande.SelectedItem != null && this._dataGridCommande.SelectedItems.Count > 0)
			{
				ObservableCollection<Commande> tmp = new ObservableCollection<Commande>();
				foreach (Commande item in this._dataGridCommande.SelectedItems)
				{
					tmp.Add(item);
				}
				foreach (Commande item in tmp)
				{
					((Dailly)this.DataContext).Commande.Add(item);
					this.listCommande.Remove(item);
				}
			}
		}

		//Supprime un élément
		private void _buttonDroiteGauche_Click_1(object sender, RoutedEventArgs e)
		{
			if (this._dataGridCommandeCedee.SelectedItem != null && this._dataGridCommandeCedee.SelectedItems.Count > 0)
			{
				ObservableCollection<Commande> tmp = new ObservableCollection<Commande>();
				foreach (Commande item in this._dataGridCommandeCedee.SelectedItems)
				{
					tmp.Add(item);
				}
				foreach (Commande item in tmp)
				{
					this.listCommande.Add(item);
					((Dailly)this.DataContext).Commande.Remove(item);
				}
			}
		}

		#endregion

		#region Facture

		private void _buttonDaillyFactureNouveau_Click_1(object sender, RoutedEventArgs e)
		{
			DaillyFactureWindow daillyFactureWindow = new DaillyFactureWindow(new ObservableCollection<Commande>(((Dailly)this.DataContext).Commande));
			daillyFactureWindow.DataContext = new Dailly_Cession_Facture();

			bool? dialogResult = daillyFactureWindow.ShowDialog();

			if (dialogResult.HasValue && dialogResult == true)
			{
				((Dailly)this.DataContext).Dailly_Cession_Facture.Add((Dailly_Cession_Facture)daillyFactureWindow.DataContext);
			}
			else
			{
				try
				{
					((App)App.Current).mySitaffEntities.Detach((Dailly_Cession_Facture)daillyFactureWindow.DataContext);
				}
				catch (Exception)
				{
				}
			}

			this._dataGridFacture.Items.Refresh();
		}

		private void _buttonDaillyFactureModifier_Click_1(object sender, RoutedEventArgs e)
		{
			if (this._dataGridFacture.SelectedItems.Count == 1)
			{
				DaillyFactureWindow daillyFactureWindow = new DaillyFactureWindow(new ObservableCollection<Commande>(((Dailly)this.DataContext).Commande));
				daillyFactureWindow.DataContext = (Dailly_Cession_Facture)this._dataGridFacture.SelectedItem;

				bool? dialogResult = daillyFactureWindow.ShowDialog();

				if (dialogResult.HasValue && dialogResult == true)
				{
				}
				else
				{
					//Je récupère les anciennes données de la base sur les modifications effectuées
					((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Dailly_Cession_Facture)(daillyFactureWindow.DataContext));
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner un litige.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		private void _buttonDaillyFactureSupprimer_Click_1(object sender, RoutedEventArgs e)
		{
			if (this._dataGridFacture.SelectedItems.Count == 1)
			{
				if (MessageBox.Show("Voulez-vous rééllement supprimer la facture séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					try
					{
						((App)App.Current).mySitaffEntities.Dailly_Cession_Facture.DeleteObject((Dailly_Cession_Facture)this._dataGridFacture.SelectedItem);
					}
					catch (Exception)
					{
						try
						{
							((App)App.Current).mySitaffEntities.Detach((Dailly_Cession_Facture)this._dataGridFacture.SelectedItem);
						}
						catch (Exception)
						{
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Vous devez sélectionner une facture.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
			this._dataGridFacture.Items.Refresh();
		}

		#endregion

		#endregion

		#region Vérifications

		private bool VerificationChamps()
		{
			bool verif = true;

			if (!Verif_Banque())
			{
				verif = false;
			}
			if (!Verif_CheckBox())
			{
				verif = false;
			}
			if (!Verif_Dailly())
			{
				verif = false;
			}
			if (!Verif_DaillyInterne())
			{
				verif = false;
			}
			if (!Verif_DataGrids())
			{
				verif = false;
			}
			if (!Verif_Date())
			{
				verif = false;
			}

			return verif;

		}

		#region Champs DaillyInterne

		private bool Verif_DaillyInterne()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxNumeroDaillyInterne, this._textBlockNumeroDaillyInterne);
		}

		private void _textBoxNumeroDaillyInterne_TextChanged_1(object sender, TextChangedEventArgs e)
		{
			Verif_DaillyInterne();
		}

		#endregion

		#region Champs Dailly

		private bool Verif_Dailly()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxNumeroDailly, this._textBlockNumeroDailly);
		}

		private void _textBoxNumeroDailly_TextChanged_1(object sender, TextChangedEventArgs e)
		{
			Verif_Dailly();
		}

		#endregion

		#region Champs Banque

		private bool Verif_Banque()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxBanque, this._textBlockBanque);
		}

		private void _comboBoxBanque_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_Banque();
		}

		#endregion

		#region Champs Date

		private bool Verif_Date()
		{
			if (this._checkBoxAccepte.IsChecked == true)
			{
				return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateAcceptation, this._textBlockDateAcceptation);
			}
			return ((App)App.Current).verifications.DatePickerSelectionNonObligatoire(this._datePickerDateAcceptation, this._textBlockDateAcceptation);
		}

		private void _datePickerDateAcceptation_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
		{
			if (this._checkBoxAccepte.IsChecked == false && this._datePickerDateAcceptation.SelectedDate != null)
			{
				this._checkBoxAccepte.IsChecked = true;
			}
			Verif_Date();
		}

		private void _checkBoxAccepte_Checked_1(object sender, RoutedEventArgs e)
		{
			Verif_Date();
		}

		private void _checkBoxAccepte_Unchecked_1(object sender, RoutedEventArgs e)
		{
			this._datePickerDateAcceptation.SelectedDate = null;
			Verif_Date();
		}

		#endregion

		#region Champs DataGrid

		private bool Verif_DataGrids()
		{
			bool verif = true;

			if (this._dataGridCommandeCedee.Items.Count == 0 && this._dataGridFacture.Items.Count == 0)
			{
				verif = false;
			}

			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridCommandeCedee, verif);
			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridFacture, verif);

			return verif;
		}

		#endregion

		#region CheckBox

		private bool Verif_CheckBox()
		{
			bool verif = true;

			((App)App.Current).verifications.MettreCheckBoxEnCouleur(this._checkBoxMarche, this._textBlockMarche, verif);
			((App)App.Current).verifications.MettreCheckBoxEnCouleur(this._checkBoxPublic, this._textBlockPublic, verif);

			return verif;
		}

		private void _checkBoxMarche_Checked_1(object sender, RoutedEventArgs e)
		{
			Verif_CheckBox();
		}

		private void _checkBoxPublic_Checked_1(object sender, RoutedEventArgs e)
		{
			Verif_CheckBox();
		}

		#endregion

		#endregion

		#region LectureSeule

		private void lectureSeule()
		{
			this._buttonDaillyFactureModifier.IsEnabled = false;
			this._buttonDaillyFactureNouveau.IsEnabled = false;
			this._buttonDaillyFactureSupprimer.IsEnabled = false;
			this._buttonDroiteGauche.IsEnabled = false;
			this._buttonGaucheDroite.IsEnabled = false;
			this._comboBoxBanque.IsEnabled = false;
			this._datePickerDateAcceptation.IsEnabled = false;
			this._textBoxNumeroDailly.IsEnabled = false;
			this._textBoxNumeroDaillyInterne.IsEnabled = false;
		}

		#endregion

		#region Fenêtre chargée

		private void Window_Loaded_1(object sender, RoutedEventArgs e)
		{
			if (soloLecture == true)
			{
				lectureSeule();
			}
			if (((Dailly)this.DataContext).Numero_Interne == null || ((Dailly)this.DataContext).Numero_Interne == "")
			{
				GenerateDaillyInterne();
			}
		}

		#endregion

		#region Fonctions

        #region Filtrage commande

        private void _mettreANullClient_Click_1(object sender, RoutedEventArgs e)
        {
            this._comboBoxFilterClient.SelectedItem = null;
        }

        private void _mettreANullAffaire_Click_1(object sender, RoutedEventArgs e)
        {
            this._comboBoxFilterAffaire.SelectedItem = null;
        }

		private void _mettreANullEntrepriseMere_Click_1(object sender, RoutedEventArgs e)
		{
			this._comboBoxFilterEntrepriseMere.SelectedItem = null;
		}

        private void _comboBoxFilterClient_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.filtrerCommande();
        }

        private void _comboBoxFilterAffaire_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.filtrerCommande();
        }

		private void _comboBoxFilterEntrepriseMere_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			this.filtrerAffaire();
		}

        private void filtrerCommande()
        {
            listCommande = new ObservableCollection<Commande>(((App)App.Current).mySitaffEntities.Commande.Where(com => com.Dailly1 == null).Where(com => com.Numero_Commande != null && com.Numero_Commande != "").OrderBy(com => com.Numero_Commande));

            if (this._comboBoxFilterAffaire.SelectedItem != null)
            {
                listCommande = new ObservableCollection<Commande>(listCommande.Where(com => com.getAffaire != null));
                listCommande = new ObservableCollection<Commande>(listCommande.Where(com => com.getAffaire.Identifiant == ((Affaire)this._comboBoxFilterAffaire.SelectedItem).Identifiant));
            }

            if (this._comboBoxFilterClient.SelectedItem != null)
            {
                listCommande = new ObservableCollection<Commande>(listCommande.Where(com => com.getClient != null));
                listCommande = new ObservableCollection<Commande>(listCommande.Where(com => com.getClient.Identifiant == ((Client)this._comboBoxFilterClient.SelectedItem).Identifiant));
            }
        }

		private void filtrerAffaire()
		{
			if (this._comboBoxFilterEntrepriseMere.SelectedItem != null)
			{
				this.listAffaire = new ObservableCollection<Affaire>();
				this.listCommande = new ObservableCollection<Commande>();
				foreach (Commande item in ((App)App.Current).mySitaffEntities.Commande.Where(com => com.Dailly1 == null).Where(com => com.Numero_Commande != null && com.Numero_Commande != ""))
				{
					if (item.getAffaire != null)
					{
						if (listAffaire.Where(aff => aff.Identifiant == item.getAffaire.Identifiant).Count() == 0)
						{
							if (item.getAffaire.Entreprise_Mere1 == _comboBoxFilterEntrepriseMere.SelectedItem)
							{
								listAffaire.Add(item.getAffaire);
								listCommande.Add(item);
							}
						}
					}
				}
				listAffaire = new ObservableCollection<Affaire>(listAffaire.OrderBy(aff => aff.Numero));
			}
			else
			{
				filtrerCommande();
				foreach (Commande item in listCommande)
				{
					if (item.getAffaire != null)
					{
						if (listAffaire.Where(aff => aff.Identifiant == item.getAffaire.Identifiant).Count() == 0)
						{
							listAffaire.Add(item.getAffaire);
						}
					}
					if (item.getClient != null)
					{
						if (listClient.Where(aff => aff.Identifiant == item.getClient.Identifiant).Count() == 0)
						{
							listClient.Add(item.getClient);
						}
					}
				}
				listAffaire = new ObservableCollection<Affaire>(listAffaire.OrderBy(aff => aff.Numero));
				listClient = new ObservableCollection<Client>(listClient.OrderBy(aff => aff.Entreprise.Libelle));
			}
		}

        #endregion

        private void GenerateDaillyInterne()
		{
			String numTemp = "";
			numTemp = "D" + "-";

			String year = DateTime.Today.Year.ToString();
			String mois = DateTime.Today.Month.ToString();
			int i = 1;
			foreach (char c in year)
			{
				if (i == 3 || i == 4)
				{
					numTemp = numTemp + c;
				}
				i = i + 1;
			}
			int nbCaracteres = 0;
			foreach (Char c in mois)
			{
				nbCaracteres++;
			}
			if (nbCaracteres == 1)
			{
				numTemp += "0";
			}
			numTemp = numTemp + mois + "-";

			String incrementToPut = "001";

			ObservableCollection<Dailly> toTest = new ObservableCollection<Dailly>(((App)App.Current).mySitaffEntities.Dailly.Where(com => com.Numero_Interne.Contains(numTemp)));
			if (toTest.Count() == 0)
			{

			}
			else
			{
				ObservableCollection<int> lesEntiersPourIncr = new ObservableCollection<int>();
				int PlusGrand = 0;
				foreach (Dailly item in toTest)
				{
					int test;
					if (int.TryParse(item.Numero_Interne.Replace(numTemp, ""), out test))
					{
                        lesEntiersPourIncr.Add(int.Parse(item.Numero_Interne.Replace(numTemp, "")));
					}
				}
				foreach (int entier in lesEntiersPourIncr)
				{
					if (entier > PlusGrand)
					{
						PlusGrand = entier;
					}
				}
				PlusGrand = PlusGrand + 1;
				incrementToPut = PlusGrand.ToString();
				String tempIncrement = "";
				nbCaracteres = 0;
				foreach (Char c in incrementToPut)
				{
					nbCaracteres++;
				}
				if (nbCaracteres == 1)
				{
					tempIncrement += "00";
				}
				if (nbCaracteres == 2)
				{
					tempIncrement += "0";
				}
				tempIncrement = tempIncrement + incrementToPut;
				incrementToPut = tempIncrement;
			}
			numTemp = numTemp + incrementToPut;

			((Dailly)this.DataContext).Numero_Interne = numTemp;
		}

		#endregion


        

	}
}
