using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour FusionnerAffaireWindow.xaml
    /// </summary>
    public partial class FusionnerAffaireWindow : Window
    {

        #region Attributs

        #endregion

        #region Propriétés de dépendances


        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(FusionnerAffaireWindow), new PropertyMetadata(null));


        #endregion

        #region Constructeur

        public FusionnerAffaireWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);
        }

        #region initialisation

        private void initialisationPropDependance()
        {
            this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
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

        #endregion

        #region Verifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!this.Verif_ComboBoxAffairePrincipale())
            {
                verif = false;
            }
            if (!this.Verif_ComboBoxAffaireAInclure())
            {
                verif = false;
            }

            return verif;
        }

        #region Affaire principale

        private bool Verif_ComboBoxAffairePrincipale()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxAffairePrincipale, this._TextBlockAffairePrincipale);
        }

        private void _ComboBoxAffairePrincipale_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxAffairePrincipale();
        }

        #endregion

        #region Affaire à inclure

        private bool Verif_ComboBoxAffaireAInclure()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxAffaireAInclure, this._TextBlockAffaireAInclure);
        }

        private void _ComboBoxAffaireAInclure_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxAffaireAInclure();
        }

        #endregion

        #endregion

    }
}
