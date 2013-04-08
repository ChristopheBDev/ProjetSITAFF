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
    /// Logique d'interaction pour DaillyFactureWindow.xaml
    /// </summary>
    public partial class DaillyFactureWindow : Window
	{

		#region Attributs

		private bool soloLecture = false;
		public ObservableCollection<Commande> listCommande;

		#endregion

		#region Propriété de dépendances

		public ObservableCollection<Facture> listFacture
		{
			get { return (ObservableCollection<Facture>)GetValue(listFactureProperty); }
			set { SetValue(listFactureProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listFacture.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listFactureProperty =
			DependencyProperty.Register("listFacture", typeof(ObservableCollection<Facture>), typeof(DaillyFactureWindow), new PropertyMetadata(null));

		#endregion

		#region Constructeur

		public DaillyFactureWindow(ObservableCollection<Commande> listToPut)
		{
			InitializeComponent();

			listCommande = new ObservableCollection<Commande>(listToPut);

			InitialisationSecurite();

			InitialisationPropDep();

			this._datePickerDateCession.Focus();
		}

		#region Initialisation

		private void InitialisationSecurite()
		{

		}

		private void InitialisationPropDep()
		{
			this.listFacture = new ObservableCollection<Facture>();
			if (this.listCommande != null && this.listCommande.Count > 0)
			{
				foreach (Commande item2 in this.listCommande)
				{
					foreach (Facture item in ((App)App.Current).mySitaffEntities.Facture.Where(fac => fac.Facture_Client != null && fac.Commande1.Identifiant == item2.Identifiant))
					{
						if (item.Dailly_Cession_Facture1 == null)
						{
							this.listFacture.Add(item);
						}
					}
				}
				this.listFacture = new ObservableCollection<Facture>(this.listFacture.OrderBy(fac => fac.Numero));
			}
			
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
			if (this._dataGridFacture.SelectedItem != null && this._dataGridFacture.SelectedItems.Count > 0)
			{
				ObservableCollection<Facture> tmp = new ObservableCollection<Facture>();
				foreach (Facture item in this._dataGridFacture.SelectedItems)
				{
					tmp.Add(item);
				}
				foreach (Facture item in tmp)
				{
					((Dailly_Cession_Facture)this.DataContext).Facture.Add(item);
					this.listFacture.Remove(item);
				}
			}
		}

		//Supprime un élément
		private void _buttonDroiteGauche_Click_1(object sender, RoutedEventArgs e)
		{
			if (this._dataGridFactureDailly.SelectedItem != null && this._dataGridFactureDailly.SelectedItems.Count == 1)
			{
				ObservableCollection<Facture> tmp = new ObservableCollection<Facture>();
				foreach (Facture item in this._dataGridFactureDailly.SelectedItems)
				{
					tmp.Add(item);
				}
				foreach (Facture item in tmp)
				{					
					this.listFacture.Add(item);
					((Dailly_Cession_Facture)this.DataContext).Facture.Remove(item);
				}
			}
		}

		#endregion
		
		#endregion

		#region Vérifications

		private bool VerificationChamps()
		{
			bool verif = true;

			if (!Verif_DataGrid())
			{
				verif = false;
			}
			if (!Verif_Date())
			{
				verif = false;
			}

			return verif;
		}

		#region Champs Date

		private bool Verif_Date()
		{
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateCession, this._textBlockDateCessione);
		}

		private void _datePickerDateCession_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_Date();
		}

		#endregion

		#region Champs DataGrid

		private bool Verif_DataGrid()
		{
			bool verif = true;

			if (this._dataGridFactureDailly.Items.Count == 0)
			{
				verif = false;
			}

			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridFactureDailly, verif);

			return verif;
		}

		#endregion

		#endregion

		#region LectureSeule

		private void lectureSeule()
		{
			this._datePickerDateCession.IsEnabled = false;
			this._buttonDroiteGauche.IsEnabled = false;
			this._buttonGaucheDroite.IsEnabled = false;
		}

		#endregion

		#region Fenêtre chargée

		private void Window_Loaded_1(object sender, RoutedEventArgs e)
		{
			if (soloLecture)
			{
				lectureSeule();
			}
			if (((Dailly_Cession_Facture)this.DataContext).Date_Cession == null)
			{
				((Dailly_Cession_Facture)this.DataContext).Date_Cession = DateTime.Today;
			}
		}

		#endregion

    }
}
