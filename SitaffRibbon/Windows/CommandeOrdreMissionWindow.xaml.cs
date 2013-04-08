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
    /// Logique d'interaction pour CommandeOrdreMissionWindow.xaml
    /// </summary>
    public partial class CommandeOrdreMissionWindow : Window
    {

		#region Attributs

		public bool commande = true;
		public bool soloLecture = false;
		public double totalReelTemp = 0;
		public bool ventil = false;
		public bool creation = false;
		
		#endregion

		#region Propriétés de dépendances

		public ObservableCollection<Condition_Reglement> list_Condition_Reglement
		{
			get { return (ObservableCollection<Condition_Reglement>)GetValue(list_Condition_ReglementProperty); }
			set { SetValue(list_Condition_ReglementProperty, value); }
		}

		// Using a DependencyProperty as the backing store for list_Condition_Reglement.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty list_Condition_ReglementProperty =
			DependencyProperty.Register("list_Condition_Reglement", typeof(ObservableCollection<Condition_Reglement>), typeof(CommandeWindow), new UIPropertyMetadata(null));

		#endregion

		#region Contructeur

		public CommandeOrdreMissionWindow()
		{
			InitializeComponent();

			initialisationPropDependance();

		}

		#region Initialisation

		private void initialisationPropDependance()
		{
			this.list_Condition_Reglement = new ObservableCollection<Condition_Reglement>(((App)App.Current).mySitaffEntities.Condition_Reglement.OrderBy(cr => cr.Libelle));
		}

		#endregion

		#endregion

		#region Fenêtre chargée

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//Génération du numéro de commande
			generationNumeroCommande();
			
		}

		#endregion

		#region Boutons

		#region Boutons OK et Annuler

		private void _buttonOk_Click(object sender, RoutedEventArgs e)
		{
			if (this.verification_generale())
			{
				this.Calculer();
				this.DialogResult = true;
				this.Close();
			}
		}

		private void _buttonAnnuler_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}

		#endregion

		#region Conditions règlement

		private void _buttonGaucheDroite_Click(object sender, RoutedEventArgs e)
		{
			if (this._listBoxCondReglementGauche.SelectedItem != null && this._listBoxCondReglementGauche.SelectedItems.Count == 1)
			{
				Commande_Fournisseur_Condition_Reglement temp = new Commande_Fournisseur_Condition_Reglement();
				temp.Condition_Reglement1 = (Condition_Reglement)this._listBoxCondReglementGauche.SelectedItem;

				((Commande_Fournisseur)this.DataContext).Commande_Fournisseur_Condition_Reglement.Add(temp);
			}
			this.verif_Conditions();
		}

		private void _buttonDroiteGauche_Click(object sender, RoutedEventArgs e)
		{
			if (this._dataGridCommandeFournisseurConditionReglement.SelectedItem != null && this._dataGridCommandeFournisseurConditionReglement.SelectedItems.Count == 1)
			{
				Commande_Fournisseur_Condition_Reglement itemToRemove = (Commande_Fournisseur_Condition_Reglement)this._dataGridCommandeFournisseurConditionReglement.SelectedItem;
				try
				{
					itemToRemove.Commande_Fournisseur1 = null;
					((Commande_Fournisseur)this.DataContext).Commande_Fournisseur_Condition_Reglement.Remove(itemToRemove);
					((App)App.Current).mySitaffEntities.Commande_Fournisseur_Condition_Reglement.DeleteObject(itemToRemove);
					//((App)App.Current).mySitaffEntities.Detach(itemToRemove);                    
				}
				catch (Exception)
				{
					((App)App.Current).mySitaffEntities.Detach(itemToRemove);
					((Commande_Fournisseur)this.DataContext).Commande_Fournisseur_Condition_Reglement.Remove(itemToRemove);
				}
			}
			this._dataGridCommandeFournisseurConditionReglement.Items.Refresh();
			this.verif_Conditions();
		}

		private void _importerConditions_Click(object sender, RoutedEventArgs e)
		{
			if (((Commande_Fournisseur)this.DataContext).Mission_Tiers.Count() > 0)
			{
				if (((Commande_Fournisseur)this.DataContext).Mission_Tiers.First() != null)
				{
					if (((Commande_Fournisseur)this.DataContext).Mission_Tiers.First().Entreprise1 != null)
					{
						if (((Commande_Fournisseur)this.DataContext).Mission_Tiers.First().Entreprise1.Fournisseur != null)
						{
							if (((Commande_Fournisseur)this.DataContext).Mission_Tiers.First().Entreprise1.Fournisseur.Fournisseur_Condition_Reglement != null && ((Commande_Fournisseur)this.DataContext).Mission_Tiers.First().Entreprise1.Fournisseur.Fournisseur_Condition_Reglement.Count() != 0)
							{
								foreach (Fournisseur_Condition_Reglement item in ((Commande_Fournisseur)this.DataContext).Mission_Tiers.First().Entreprise1.Fournisseur.Fournisseur_Condition_Reglement)
								{
									Commande_Fournisseur_Condition_Reglement temp = new Commande_Fournisseur_Condition_Reglement();
									temp.Condition_Reglement1 = item.Condition_Reglement1;
									temp.Commentaire = item.Commentaire;
									temp.Pourcentage = item.Pourcentage;

									((Commande_Fournisseur)this.DataContext).Commande_Fournisseur_Condition_Reglement.Add(temp);
								}
							}
							else
							{
								MessageBox.Show("Aucune condition de réglement n'est enregistrée pour ce fournisseur.");
							}
						}
						else
						{
							MessageBox.Show("Aucune fournisseur sélectionné.");
						}
					}
				}				
			}			
		}

		#endregion
		
		#region Contenu

		private void _buttonSupprimer_Click(object sender, RoutedEventArgs e)
		{
			if (this._dataGridContenuCommande.SelectedItem != null && this._dataGridContenuCommande.SelectedItems.Count == 1)
			{
				try
				{
					Contenu_Commande_Fournisseur item = (Contenu_Commande_Fournisseur)this._dataGridContenuCommande.SelectedItem;
					item.Commande_Fournisseur1 = null;
					((Commande_Fournisseur)this.DataContext).Contenu_Commande_Fournisseur.Remove(item);
					((App)App.Current).mySitaffEntities.Contenu_Commande_Fournisseur.DeleteObject(item);
				}
				catch (Exception)
				{ }
				this._dataGridContenuCommande.Items.Refresh();
			}
		}

		#endregion

		#endregion

		#region Vérifications

		private bool verification_generale()
		{
			bool verif = true;

			if (!this.verif_datePicker())
			{
				verif = false;
			}
			if (!this.verif_Total())
			{
				verif = false;
			}
			if (!this.verif_Contenu())
			{
				verif = false;
			}
			if (!this.verif_Conditions())
			{
				verif = false;
			}

			return verif;
		}

		#region Champ datepicker

		private bool verif_datePicker()
		{
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateCommande, this._textBlockDateCommande);
		}

		private void _datePickerDateCommande_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			this.verif_datePicker();
		}

		#endregion
		
		#region TabControl

		#region Contenu

		private bool verif_Contenu()
		{
			bool verif = true;

			if (((Commande_Fournisseur)this.DataContext).Contenu_Commande_Fournisseur.Count() == 0)
			{
				verif = false;
			}
			
			if (!verif_Total())
			{
				verif = false;
			}
			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemContenu, verif);

			return verif;
		}

		#region Champ Total commande

		private bool verif_Total()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxTotalCommande, this._textBlockTotalCommande);
		}

		#endregion

		#endregion

		#region Conditions de règlements

		private bool verif_Conditions()
		{
			bool verif = true;

			if (((Commande_Fournisseur)this.DataContext).Commande_Fournisseur_Condition_Reglement.Count() == 0)
			{
				verif = false;
			}

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemCondReglement, verif);

			return verif;
		}

		#endregion

		#endregion

		#endregion

		#region Evénements

		#region KeyUp

		private void _dataGridContenuCommande_KeyUp(object sender, KeyEventArgs e)
		{

		}

		private void _dataGridCommandeFournisseurConditionReglement_KeyUp(object sender, KeyEventArgs e)
		{

		}


		#endregion
		
		#endregion

		#region Fonctions

		private void generationNumeroCommande()
		{
			if (((Commande_Fournisseur)this.DataContext).Numero == null || ((Commande_Fournisseur)this.DataContext).Numero == "")
			{
				this.creation = true;
				String numTemp = "";
				if (((Commande_Fournisseur)this.DataContext).Mission_Tiers.Count() > 0)
				{
					if (((Commande_Fournisseur)this.DataContext).Mission_Tiers.First() != null)
					{
						if (((Commande_Fournisseur)this.DataContext).Mission_Tiers.First().Ordre_Mission.Count() > 0)
						{
							if (((Commande_Fournisseur)this.DataContext).Mission_Tiers.First().Ordre_Mission.First() != null)
							{
								if (((Commande_Fournisseur)this.DataContext).Mission_Tiers.First().Ordre_Mission.First().Affaire1 != null & ((Commande_Fournisseur)this.DataContext).Mission_Tiers.First().Ordre_Mission.First().Affaire1.Numero != "")
								{
									numTemp = ((Commande_Fournisseur)this.DataContext).Mission_Tiers.First().Ordre_Mission.First().Affaire1.Numero + "-";
								}
							}
							else
							{
								numTemp = "Autre-Erreur" + "-";
							}
						}
						else
						{
							numTemp = "Autre-Erreur" + "-";
						}
					}
					else
					{
						numTemp = "Autre-Erreur" + "-";
					}
				}
				else
				{
					numTemp = "Autre-Erreur" + "-";
				}
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
				numTemp = numTemp + mois + "-MO";

				String incrementToPut = "001";

				ObservableCollection<Commande_Fournisseur> toTest = new ObservableCollection<Commande_Fournisseur>(((App)App.Current).mySitaffEntities.Commande_Fournisseur.Where(com => com.Numero.Contains(numTemp)));
				if (toTest.Count() != 0)
				{
					ObservableCollection<int> lesEntiersPourIncr = new ObservableCollection<int>();
					int PlusGrand = 0;
					foreach (Commande_Fournisseur item in toTest)
					{
						int test;
						if (int.TryParse(item.Numero.Replace(numTemp, ""), out test))
						{
							lesEntiersPourIncr.Add(int.Parse(item.Numero.Replace(numTemp, "")));
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
					tempIncrement += incrementToPut;
					incrementToPut = tempIncrement;
				}
				numTemp += incrementToPut;

				((Commande_Fournisseur)this.DataContext).Numero = numTemp;
				this._textBoxNumeroCommande.Text = numTemp;
			}
		}

		private void Calculer()
		{
			double total = 0;
			foreach (Contenu_Commande_Fournisseur item in ((Commande_Fournisseur)this.DataContext).Contenu_Commande_Fournisseur)
			{
				try
				{
					item.Prix_Total = item.Quantite * item.Prix_Unitaire;
                    try
                    {
                        item.Prix_Remise = item.Prix_Unitaire;
                    }
                    catch (Exception)
                    {

                    }
					total += item.Prix_Total;
				}
				catch (Exception)
				{
				}
			}
			((Commande_Fournisseur)this.DataContext).Total_Commande = total;
		}

		#endregion

	}
}
