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

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour FormationWindow.xaml
    /// </summary>
    public partial class FormationWindow : Window
    {
        public FormationWindow()
        {
            InitializeComponent();
        }

        #region Propriétés de dépendances

        public ObservableCollection<Formation> listFormations
        {
            get { return (ObservableCollection<Formation>)GetValue(listFormationsProperty); }
            set { SetValue(listFormationsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listFormations.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFormationsProperty =
            DependencyProperty.Register("listFormations", typeof(ObservableCollection<Formation>), typeof(FormationWindow), new UIPropertyMetadata(null));

        #endregion

        #region Boutons ok et cancel

        /// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Verif_DatePickerDateObtention();
            this.Verif_DatePickerDateValidite();
            this.Verif_ComboBoxSalarieFormation();
            if (this.Verif_ComboBoxSalarieFormation() && this.Verif_DatePickerDateObtention() && this.Verif_DatePickerDateValidite())
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

        #region Verifications

        private bool Verif_ComboBoxSalarieFormation()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxSalarieFormation, this._TextBlockSalariePermis);
        }

        private void _ComboBoxSalariePermis_SelectionChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_ComboBoxSalarieFormation();
            if (this._ComboBoxSalarieFormation.SelectedItem != null)
            {
                ((Salarie_Formation)this.DataContext).Formation1 = (Formation)this._ComboBoxSalarieFormation.SelectedItem;
            }
        }

        private bool Verif_DatePickerDateObtention()
        {
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._DatePickerDateObtention, this._TextBlockDate, DateTime.Today);
        }

        private void _DatePickerDateObtention_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerDateObtention();
        }

        private bool Verif_DatePickerDateValidite()
        {
			return ((App)App.Current).verifications.DatePickerSelectionNonObligatoire(this._DatePickerDateValidite, this._TextBlockDateValidite, DateTime.Today);
        }

        private void _DatePickerDateValidite_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerDateValidite();
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }
    }
}
