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
    /// Logique d'interaction pour NumeroIntraCoWindow.xaml
    /// </summary>
    public partial class NumeroIntraCoWindow : Window
    {
        public NumeroIntraCoWindow()
        {
            InitializeComponent();
            this.listeDePays = new ObservableCollection<Pays>(((App)App.Current).mySitaffEntities.Pays.OrderBy(pa => pa.Libelle));
        }

        #region Proprietés de dependances



        public ObservableCollection<Pays> listeDePays
        {
            get { return (ObservableCollection<Pays>)GetValue(listeDePaysProperty); }
            set { SetValue(listeDePaysProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listeDePays.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listeDePaysProperty =
            DependencyProperty.Register("listeDePays", typeof(ObservableCollection<Pays>), typeof(NumeroIntraCoWindow), new UIPropertyMetadata(null));

        

        #endregion

        #region Verifications

        /// <summary>
        /// Verifie si tous les champs sont bien renseignés.
        /// </summary>
        /// <returns>booléen vrai si tous les champs sont bien renseignés, sinon retourne faux</returns>
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_ComboBoxPays())
            {
                verif = false;
            }
            if (!Verif_TextBoxNumero())
            {
                verif = false;
            }

            return verif;
        }

        #region Combobox Pays

        private bool Verif_ComboBoxPays()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxPays, this._TextBlockPays);
        }

        private void _ComboBoxPays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxPays();
        }

        #endregion

        #region Champs Numero

        private bool Verif_TextBoxNumero()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxNumero, this._TextBlockNumero);
        }

        private void _TextBoxNumero_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxNumero();
        }

        #endregion

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

    }
}
