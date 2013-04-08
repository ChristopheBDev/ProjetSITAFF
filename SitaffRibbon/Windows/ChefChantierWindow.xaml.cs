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
    /// Logique d'interaction pour ChefChantierWindow.xaml
    /// </summary>
    public partial class ChefChantierWindow : Window
    {
        public ChefChantierWindow()
        {
            InitializeComponent();
            this.listSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.Where(sal => sal.Chantier == true));
        }

        #region proprietés de dependance

        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Charge_Affaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(ChefChantierWindow), new UIPropertyMetadata(null));

        #endregion

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

        #region Verifications

        private bool Verif_Generale()
        {
            bool verif = true;

            if (!this.Verif_ComboBoxChefChantier())
            {
                verif = false;
            }
            if (!this.Verif_DatePickerDateDebut())
            {
                verif = false;
            }
            if (!this.Verif_DatePickerDateFin())
            {
                verif = false;
            }

            return verif;
        }

        private bool Verif_ComboBoxChefChantier()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxChefChantier, this._TextBlockChefChantier);
        }
        private void _ComboBoxChefChantier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxChefChantier();
        }

        private bool Verif_DatePickerDateDebut()
        {
			return ((App)App.Current).verifications.DatePickerSelectionNonObligatoire(this._DatePickerDateDebut, this._TextBlockDateDebut);
        }
        private void _DatePickerDateDebut_SelectedDateChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_DatePickerDateDebut();
        }

        private bool Verif_DatePickerDateFin()
        {
			return ((App)App.Current).verifications.DatePickerSelectionNonObligatoire(this._DatePickerDateFin, this._TextBlockDateFin, this._DatePickerDateDebut);
        }

        private void _DatePickerDateFin_SelectedDateChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_DatePickerDateFin();
        }

        #endregion
    }
}
