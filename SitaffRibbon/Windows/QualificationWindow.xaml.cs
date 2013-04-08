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
    /// Logique d'interaction pour QualificationWindow.xaml
    /// </summary>
    public partial class QualificationWindow : Window
    {

        #region Propriétés de dépendances

        public ObservableCollection<Qualification> listQualification
        {
            get { return (ObservableCollection<Qualification>)GetValue(listQualificationProperty); }
            set { SetValue(listQualificationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listQualification.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listQualificationProperty =
            DependencyProperty.Register("listQualification", typeof(ObservableCollection<Qualification>), typeof(QualificationWindow), new UIPropertyMetadata(null));

        #endregion

        public QualificationWindow()
        {
            InitializeComponent();
        }

        #region Boutons

        /// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.Verif_ComboBoxSalarieQualification())
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

        private bool Verif_ComboBoxSalarieQualification()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxSalarieQualification, this._TextBlockSalarieQualification);
        }

        private void _ComboBoxSalarieQualification_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxSalarieQualification();
            if (this._ComboBoxSalarieQualification.SelectedItem != null)
            {
                this.DataContext = (Qualification)this._ComboBoxSalarieQualification.SelectedItem;
            }
        }

        #endregion

        public void lectureSeule()
        {
            _ComboBoxSalarieQualification.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

    }
}
