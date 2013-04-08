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
    /// Logique d'interaction pour RepondeurCongeWindow.xaml
    /// </summary>
    public partial class RepondeurCongeWindow : Window
    {
        public RepondeurCongeWindow()
        {
            InitializeComponent();
        }

        #region Propriétés de dépendances

        public ObservableCollection<Salarie> listSalaries
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalariesProperty); }
            set { SetValue(listSalariesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalariesProperty =
            DependencyProperty.Register("listSalaries", typeof(ObservableCollection<Salarie>), typeof(SalarieWindow), new UIPropertyMetadata(null));

        #endregion

        #region Boutons

        /// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.Verif_ComboBoxSalarie())
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

        #region _ComboBoxSalarie
        private bool Verif_ComboBoxSalarie()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxSalarie,this._TextBlockSalarie);
        }

        private void _ComboBoxSalarie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxSalarie();
        }

        #endregion

        #endregion

        public void lectureSeule()
        {
            this._ComboBoxSalarie.IsEnabled = false;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

    }
}
