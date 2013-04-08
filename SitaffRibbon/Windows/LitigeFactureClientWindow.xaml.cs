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
    /// Logique d'interaction pour LitigeFactureClientWindow.xaml
    /// </summary>
    public partial class LitigeFactureClientWindow : Window
    {

		#region Propriété de dépendance

		public ObservableCollection<Litige> listLitige
		{
			get { return (ObservableCollection<Litige>)GetValue(listLitigeProperty); }
			set { SetValue(listLitigeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for mesPays.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listLitigeProperty =
			DependencyProperty.Register("listLitige", typeof(ObservableCollection<Litige>), typeof(LitigeFactureClientWindow), new UIPropertyMetadata(null));

		#endregion
		
		#region Constructeur
		
		public LitigeFactureClientWindow()
        {
            InitializeComponent();

			this.listLitige = new ObservableCollection<Litige>(((App)App.Current).mySitaffEntities.Litige.OrderBy(lib => lib.Libelle));
        }

		#endregion
		
        #region Verifications
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxCommentaire())
            {
                verif = false;
			}
			if (!Verif_ComboBoxLitige())
			{
				verif = false;
			}
			if (!Verif_Date())
			{
				verif = false;
			}
            return verif;
        }

        #region libelle

        private bool Verif_TextBoxCommentaire()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxCommentaire, this._TextBlockCommentaire);
        }

        private void _TextBoxCommentaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCommentaire();
        }

        #endregion

        #region Litige

        private bool Verif_ComboBoxLitige()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxLitige, this._TextBlockLitige);
        }

        private void _ComboBoxLitige_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxLitige();
        }

        #endregion

		#region Date

		private bool Verif_Date()
		{
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDate_Litige, _TextBlockDateLitige);
		}

		private void _datePickerDate_Litige_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_Date();
		}

		#endregion

		#endregion

		#region FenêtreChargée

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
		}

		#endregion

		#region bouton ok et annuler

		private void _ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			if (this.VerificationChamps())
			{
				this.DialogResult = true;
				this.Close();
			}
		}

		private void _ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}

		#endregion

    }
}
