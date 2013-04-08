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

namespace SitaffRibbon.Windows.ParametresWindows
{
    /// <summary>
    /// Logique d'interaction pour TypeRemboursementWindow.xaml
    /// </summary>
    public partial class TypeRemboursementWindow : Window
	{
        #region Attributs

        public bool soloLecture = false;

        #endregion

		#region Constructeur

		public TypeRemboursementWindow()
		{
			InitializeComponent();
			
			initialisationPropDep();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._textBoxDistanceDebut.Focus();
		}

		private void initialisationPropDep()
		{
			this.listEntreprise = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(ent => ent.Nom));
			this.listEvent = new ObservableCollection<Evenement_Remboursement>(((App)App.Current).mySitaffEntities.Evenement_Remboursement.OrderBy(eve => eve.Libelle));
		}

		#endregion

		#region Boutons

		/// <summary>
		/// Fonction lancée après clic sur Ok
		/// </summary>
		/// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
		/// <param name="e"></param>
		private void _ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			if (this.VerificationChamps())
			{
                if (((App)App.Current).mySitaffEntities.Type_Remboursement.Where(act => act.Identifiant != ((Type_Remboursement)this.DataContext).Identifiant).Where(lib => lib.Reference.Trim().ToLower() == this._textBoxReference.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Un type de remboursement est déjà présent avec ce libellé", "Doublon de type de remboursement", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
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

		private void _buttonEntrepriseMereNull_Click(object sender, RoutedEventArgs e)
		{
			this._comboBoxEntrepriseMere.SelectedItem = null;
		}

		#endregion

		#region Verifications

		private bool VerificationChamps()
		{
			bool verif = true;

			if (!this.Verif_DateDebut())
			{
				verif = false;
			}
			if (!this.Verif_DateFin())
			{
				verif = false;
			}
			if (!this.Verif_DistanceDebut())
			{
				verif = false;
			}
			if (!this.Verif_EventDeplacement())
			{
				verif = false;
			}
			if (!this.Verif_EventKm())
			{
				verif = false;
			}
			if (!this.Verif_EventRepas())
			{
				verif = false;
			}
			if (!this.Verif_EventTicket())
			{
				verif = false;
			}
			if (!this.Verif_MontantDeplacement())
			{
				verif = false;
			}
			if (!this.Verif_MontantRepas())
			{
				verif = false;
			}
			if (!this.Verif_NbTicket())
			{
				verif = false;
			}
			if (!this.Verif_Plafond())
			{
				verif = false;
			}
			if (!this.Verif_Reference())
			{
				verif = false;
			}
			if (!this.Verif_DistanceFin())
			{
				verif = false;
			}
			if (!this.Verif_TextBoxMontantTicket())
			{
				verif = false;
			}

			return verif;
		}

		private bool Verif_DistanceDebut()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(_textBoxDistanceDebut, _textBlockDistanceDebut);
		}

		private void _textBoxDistanceDebut_TextChanged(object sender, TextChangedEventArgs e)
		{
			Verif_DistanceDebut();
		}

		private bool Verif_DistanceFin()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(_textBoxDistanceFin, _textBlockDistanceFin);
		}

		private void _textBoxDistanceFin_TextChanged(object sender, TextChangedEventArgs e)
		{
			Verif_DistanceFin();
		}

		private bool Verif_Reference()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(_textBoxReference, _textBlockReference);
		}

		private void _textBoxReference_TextChanged(object sender, TextChangedEventArgs e)
		{
			Verif_Reference();
		}

		private bool Verif_MontantDeplacement()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(_textBoxMontantDeplacement, _textBlockMontantDeplacement);
		}

		private void _textBoxMontantDeplacement_TextChanged(object sender, TextChangedEventArgs e)
		{
			Verif_MontantDeplacement();
		}

		private bool Verif_Plafond()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(_textBoxPlafond, _textBlockPlafond);
		}

		private void _textBoxPlafond_TextChanged(object sender, TextChangedEventArgs e)
		{
			Verif_Plafond();
		}

		private bool Verif_MontantRepas()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(_textBoxMontantRepas, _textBlockMontantRepas);
		}

		private void _textBoxMontantRepas_TextChanged(object sender, TextChangedEventArgs e)
		{
			Verif_MontantRepas();
		}

		private bool Verif_NbTicket()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(_textBoxNbTicket, _textBlockNbTicket);
		}

		private void _textBoxNbTicket_TextChanged(object sender, TextChangedEventArgs e)
		{
			Verif_NbTicket();
		}

		private bool Verif_EventKm()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(_comboBoxEventKm, _textBlockEventKm);
		}

		private void _comboBoxEventKm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Verif_EventKm();
		}

		private bool Verif_EventRepas()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(_comboBoxEventRepas, _textBlockEventRepas);
		}

		private void _comboBoxEventRepas_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Verif_EventRepas();
		}

		private bool Verif_EventTicket()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(_comboBoxEventTicket, _textBlockEventTicket);
		}

		private void _comboBoxEventTicket_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Verif_EventTicket();
		}

		private bool Verif_EventDeplacement()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(_comboBoxEventDeplacement, _textBlockEventDeplacement);
		}

		private void _comboBoxEventDeplacement_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Verif_EventDeplacement();
		}

		private bool Verif_DateDebut()
		{
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(_datePickerDateDebut, _textBlockDateDebut);
		}

		private void _datePickerDateDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			Verif_DateDebut();
		}

		private bool Verif_DateFin()
		{
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(_datePickerDateFin, _textBlockDateFin, _datePickerDateDebut);
		}

		private void _datePickerDateFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			Verif_DateFin();
		}

		private bool Verif_TextBoxMontantTicket()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxMontantTicket, this._textBlockMontantTicket);
		}

		private void _textBoxMontantTicket_TextChanged(object sender, TextChangedEventArgs e)
		{
			Verif_TextBoxMontantTicket();
		}

		#endregion

		#region Propriétés de dépendances

		public ObservableCollection<Entreprise_Mere> listEntreprise
		{
			get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntrepriseProperty); }
			set { SetValue(listEntrepriseProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listEntreprise.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listEntrepriseProperty =
			DependencyProperty.Register("listEntreprise", typeof(ObservableCollection<Entreprise_Mere>), typeof(TypeRemboursementWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Evenement_Remboursement> listEvent
		{
			get { return (ObservableCollection<Evenement_Remboursement>)GetValue(listEventProperty); }
			set { SetValue(listEventProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listEvent.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listEventProperty =
			DependencyProperty.Register("listEvent", typeof(ObservableCollection<Evenement_Remboursement>), typeof(TypeRemboursementWindow), new UIPropertyMetadata(null));

		#endregion

		#region Evènements
		
		#region KeyUp

		private void _textBox_KeyUp(object sender, KeyEventArgs e)
		{
			ReglageDecimales reg = new ReglageDecimales();
			reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
		}

		#endregion
		
		#endregion

		#region lecture seule

		public void lectureSeule()
		{
			this._buttonEntrepriseMereNull.IsEnabled = false;
			this._comboBoxEntrepriseMere.IsEnabled = false;
			this._comboBoxEventDeplacement.IsEnabled = false;
			this._comboBoxEventKm.IsEnabled = false;
			this._comboBoxEventRepas.IsEnabled = false;
			this._comboBoxEventTicket.IsEnabled = false;
			this._datePickerDateDebut.IsEnabled = false;
			this._datePickerDateFin.IsEnabled = false;
			this._textBoxDistanceDebut.IsReadOnly = false;
            this._textBoxDistanceFin.IsReadOnly = false;
            this._textBoxMontantDeplacement.IsReadOnly = false;
            this._textBoxMontantRepas.IsReadOnly = false;
            this._textBoxNbTicket.IsReadOnly = false;
            this._textBoxPlafond.IsReadOnly = false;
            this._textBoxReference.IsReadOnly = false;			
		}

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }
        #endregion
    }
}
