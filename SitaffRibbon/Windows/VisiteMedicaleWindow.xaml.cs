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
    /// Logique d'interaction pour VisiteMedicaleWindow.xaml
    /// </summary>
    public partial class VisiteMedicaleWindow : Window
    {

        #region Propriétés de dépendance



        public ObservableCollection<Pays> listPaysPerso
        {
            get { return (ObservableCollection<Pays>)GetValue(listPaysPersoProperty); }
            set { SetValue(listPaysPersoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listPaysPerso.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listPaysPersoProperty =
            DependencyProperty.Register("listPaysPerso", typeof(ObservableCollection<Pays>), typeof(VisiteMedicaleWindow), new UIPropertyMetadata(null));




        public ObservableCollection<Ville> listVillePerso
        {
            get { return (ObservableCollection<Ville>)GetValue(listVillePersoProperty); }
            set { SetValue(listVillePersoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listVillePerso.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listVillePersoProperty =
            DependencyProperty.Register("listVillePerso", typeof(ObservableCollection<Ville>), typeof(VisiteMedicaleWindow), new UIPropertyMetadata(null));

        

        #endregion

        public VisiteMedicaleWindow()
        {
            InitializeComponent();
            this.listPaysPerso = new ObservableCollection<Pays>(((App)App.Current).mySitaffEntities.Pays.OrderBy(pa => pa.Libelle));
            this.listVillePerso = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));
        }

        #region Boutons

        #region Boutons OK & cancel
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


        #endregion

        #region Verifications

        #region VerificationChamps

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_DatePickerDateVisiteMedicale())
            {
                verif = false;
            }
            if (!Verif_TextBoxSalarieAdressePerso())
            {
                verif = false;
            }
            if (!Verif_TextBoxSalarieAdresseComplementairePerso())
            {
                verif = false;
            }
            if (!Verif_ComboBoxSalarieVillePerso())
            {
                verif = false;
            }
            if (!Verif_TextBoxSalarieCodePostalPerso())
            {
                verif = false;
            }
            if (!Verif_ComboBoxSalariePaysPerso())
            {
                verif = false;
            }
            return verif;
        }

        #endregion

        #region Verif Date visite medicale

        private bool Verif_DatePickerDateVisiteMedicale()
        {
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._DatePickerDateVisiteMedicale, this._TextBlockDateVisiteMedicale);
        }

        private void _DatePickerDateVisiteMedicale_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerDateVisiteMedicale();
        }
        
        #endregion

        #region Champs Adresse
        private bool Verif_TextBoxSalarieAdressePerso()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxSalarieAdressePerso, this._TextBlockSalarieAdressePerso);
        }

        private void _TextBoxSalarieAdressePerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieAdressePerso();
        }

        #endregion

        #region Champs Adresse complementaire
        private bool Verif_TextBoxSalarieAdresseComplementairePerso()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxSalarieAdresseComplementairePerso, this._TextBlockSalarieAdresseComplementairePerso);
        }

        private void _TextBoxSalarieAdresseComplementairePerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieAdresseComplementairePerso();
        }

        #endregion

        #region Champs Ville
        private bool Verif_ComboBoxSalarieVillePerso()
        {
            bool verif = true;

            if (this._ComboBoxSalarieVillePerso.SelectedItem == null)
            {
                verif = false;
            }
            else
            {
                this._TextBoxSalarieCodePostalPerso.Text = ((Ville)this._ComboBoxSalarieVillePerso.SelectedItem).Code_Postal;
                this._ComboBoxSalariePaysPerso.SelectedItem = ((Ville)this._ComboBoxSalarieVillePerso.SelectedItem).Pays1;
                verif = true;
            }
			((App)App.Current).verifications.MettreComboxEnCouleur(this._ComboBoxSalarieVillePerso, this._TextBlockSalarieVillePerso, verif);
            return verif;
        }

        private void _ComboBoxCoordonneesVille_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxSalarieVillePerso();
        }

        #endregion

        #region Champs Code postal
        private bool Verif_TextBoxSalarieCodePostalPerso()
        {
            bool verif = true;

			verif = ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._TextBoxSalarieCodePostalPerso, this._TextBlockSalarieCodePostalPerso, 5);

            this.listVillePerso = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville);
            if (this._TextBoxSalarieCodePostalPerso.Text.Trim().Length != 0)
            {
                this.listVillePerso = new ObservableCollection<Ville>(this.listVillePerso.Where(vil => vil.Code_Postal == this._TextBoxSalarieCodePostalPerso.Text.Trim()));
            }
            if (this._ComboBoxSalariePaysPerso.SelectedItem != null)
            {
                this.listVillePerso = new ObservableCollection<Ville>(this.listVillePerso.Where(vil => vil.Pays1 == (Pays)this._ComboBoxSalariePaysPerso.SelectedItem));
            }

            return verif;
        }

        private void _TextBoxSalarieCodePostalPerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieCodePostalPerso();
        }

        #endregion

        #region Champs Pays
        private bool Verif_ComboBoxSalariePaysPerso()
        {
            bool verif = true;

			verif = ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxSalariePaysPerso, this._TextBlockSalariePaysPerso);
            this.listVillePerso = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville);
            if (this._TextBoxSalarieCodePostalPerso.Text.Trim().Length != 0)
            {
                this.listVillePerso = new ObservableCollection<Ville>(this.listVillePerso.Where(vil => vil.Code_Postal == this._TextBoxSalarieCodePostalPerso.Text.Trim()));
            }
            if (this._ComboBoxSalariePaysPerso.SelectedItem != null)
            {
                this.listVillePerso = new ObservableCollection<Ville>(this.listVillePerso.Where(vil => vil.Pays1 == (Pays)this._ComboBoxSalariePaysPerso.SelectedItem));
            }

            return verif;
        }

        private void _ComboBoxCoordonneesPays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxSalariePaysPerso();
        }

        #endregion

        #region Verif Observation


        #endregion

        #endregion

        #region adresse auto

        private void _ComboBoxCoordonneesVille_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.listVillePerso.Count == 0)
            {
                MessageBox.Show("Attention, aucune ville ne correspond à votre numéro de code postal et/ou votre pays.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }
    }
}
