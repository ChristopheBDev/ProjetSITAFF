using SitaffRibbon.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour AvanceWindow.xaml
    /// </summary>
    public partial class AvanceWindow : Window
    {

        #region Propd

        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(AvanceWindow), new PropertyMetadata(null));

        #endregion

        #region Constructeur

        public AvanceWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Initialisation de la sécurité
            this.initialisationSecurite();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._comboBoxSalarie.Focus();
        }

        #region initialisation

        private void initialisationPropDependance()
        {
            this.listSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.OrderBy(sal => sal.Personne.Nom).ThenBy(sal => sal.Personne.Prenom));
        }

        private void initialisationSecurite()
        {
            //Mise en place des droits sur les boutons et tabs            
        }

        #endregion

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #endregion

        #region Boutons

        #region Boutons OK et Annuler

        private void _buttonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
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

        #endregion

        #region Verifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!this.Verif_comboBoxSalarie())
            {
                verif = false;
            }
            if (!this.Verif_textBoxSomme())
            {
                verif = false;
            }
            if (!this.Verif_datePickerDate_Avance())
            {
                verif = false;
            }            

            return verif;
        }

        #region Salarie

        private bool Verif_comboBoxSalarie()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxSalarie, this._textBlockSalarie);
        }

        private void _comboBoxSalarie_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxSalarie();
        }

        #endregion

        #region Somme

        private bool Verif_textBoxSomme()
        {
            this._textBoxSomme.Text = this._textBoxSomme.Text.Trim();

			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxSomme, this._textBlockSomme);
        }

        private void _textBoxSomme_LostFocus_1(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxSomme();
        }

        #endregion

        #region Date_Avance

        private bool Verif_datePickerDate_Avance()
        {
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDate_Avance, this._textBlockDate_Avance);
        }

        private void _datePickerDate_Avance_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_datePickerDate_Avance();
        }

        #endregion

        #endregion

        #region Lecture seule

        public void lectureSeule()
        {
            this._comboBoxSalarie.IsEnabled = false;
            this._textBoxSomme.IsReadOnly = true;
            this._datePickerDate_Avance.IsEnabled = false;
        }

        #endregion

        #region Evenements

        #region KeyUp

        private void _textBoxSomme_KeyUp_1(object sender, KeyEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
        }

        #endregion

        #endregion

    }
}
