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
    /// Logique d'interaction pour DiplomeWindow.xaml
    /// </summary>
    public partial class DiplomeWindow : Window
    {

        #region Propriétés de dépendances

        public ObservableCollection<Diplome> listDiplome
        {
            get { return (ObservableCollection<Diplome>)GetValue(listDiplomeProperty); }
            set { SetValue(listDiplomeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listDiplome.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listDiplomeProperty =
            DependencyProperty.Register("listDiplome", typeof(ObservableCollection<Diplome>), typeof(DiplomeWindow), new UIPropertyMetadata(null));

        #endregion

        public DiplomeWindow()
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
            if (this.Verif_ComboBoxDiplome())
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

        #region _ComboBoxDiplome
        private bool Verif_ComboBoxDiplome()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxDiplome, this._TextBlockSalarieDiplome);
        }

        private void _ComboBoxDiplome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxDiplome();
            if (this._ComboBoxDiplome.SelectedItem != null)
            {
                this.DataContext = (Diplome)this._ComboBoxDiplome.SelectedItem;
            }
        }

        #endregion

        #endregion

        public void lectureSeule()
        {
            this._ComboBoxDiplome.IsEnabled = false;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }
    }
}
