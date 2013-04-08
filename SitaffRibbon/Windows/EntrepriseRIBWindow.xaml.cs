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
//Using pour utiliser le type TypeConverter pour la conversion de couleur
using System.ComponentModel;
using SitaffRibbon.Classes;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour EntrepriseRIBWindow.xaml
    /// </summary>
    public partial class EntrepriseRIBWindow : Window
    {
        #region constructeur

        public EntrepriseRIBWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Initialisation de la sécurité
            this.initialisationSecurite();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);
        }

        #region initialisation

        private void initialisationPropDependance()
        {
            this.listBanque = new ObservableCollection<Banque>(((App)App.Current).mySitaffEntities.Banque.OrderBy(lib => lib.Libelle));
        }

        private void initialisationSecurite()
        {
            //Mise en place des droits sur les boutons et tabs
            
        }

        #endregion

        #endregion

        #region verifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_ComboBoxBanque())
            {
                verif = false;
            }
            if (!Verif_TextBoxBIC())
            {
                verif = false;
            }
            if (this._TextBoxIBAN1.Text.Trim() != "" || this._TextBoxIBAN2.Text.Trim() != "" || this._TextBoxIBAN3.Text.Trim() != "" || this._TextBoxIBAN4.Text.Trim() != "" || this._TextBoxIBAN5.Text.Trim() != "" || this._TextBoxIBAN6.Text.Trim() != "" || this._TextBoxIBAN7.Text.Trim() != "")
            {
                if (!Verif_TextBoxIBAN1())
                {
                    verif = false;
                }
                if (!Verif_TextBoxIBAN2())
                {
                    verif = false;
                }
                if (!Verif_TextBoxIBAN3())
                {
                    verif = false;
                }
                if (!Verif_TextBoxIBAN4())
                {
                    verif = false;
                }
                if (!Verif_TextBoxIBAN5())
                {
                    verif = false;
                }
                if (!Verif_TextBoxIBAN6())
                {
                    verif = false;
                }
                if (!Verif_TextBoxIBAN7())
                {
                    verif = false;
                }
            }
            else
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
                string colorHexavert = "#8900CE00";
                Brush vert = (Brush)converter.ConvertFrom(colorHexavert);

                this._TextBoxIBAN1.Background = vert;
                this._TextBoxIBAN2.Background = vert;
                this._TextBoxIBAN3.Background = vert;
                this._TextBoxIBAN4.Background = vert;
                this._TextBoxIBAN5.Background = vert;
                this._TextBoxIBAN6.Background = vert;
                this._TextBoxIBAN7.Background = vert;
            }
            if (this._TextBoxEtablissement.Text.Trim() != "" || this._TextBoxGuichet.Text.Trim() != "" || this._TextBoxCompte.Text.Trim() != "" || this._TextBoxCle.Text.Trim() != "")
            {
                if (!Verif_TextBoxEtablissement())
                {
                    verif = false;
                }
                if (!Verif_TextBoxGuichet())
                {
                    verif = false;
                }
                if (!Verif_TextBoxCompte())
                {
                    verif = false;
                }
                if (!Verif_TextBoxCle())
                {
                    verif = false;
                }
            }
            else
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
                string colorHexavert = "#8900CE00";
                Brush vert = (Brush)converter.ConvertFrom(colorHexavert);

                this._TextBoxEtablissement.Background = vert;
                this._TextBoxGuichet.Background = vert;
                this._TextBoxCompte.Background = vert;
                this._TextBoxCle.Background = vert;

                this._TextBlockEtablissement.Foreground = Brushes.Green;
                this._TextBlockGuichet.Foreground = Brushes.Green;
                this._TextBlockCompte.Foreground = Brushes.Green;
                this._TextBlockCle.Foreground = Brushes.Green;
            }

            return verif;
        }

        #region Banque

        private bool Verif_ComboBoxBanque()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxBanque, this._TextBlockBanque);
        }

        private void _ComboBoxBanque_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxBanque();
        }

        #endregion

        #region BIC

        private bool Verif_TextBoxBIC()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxBIC, this._TextBlockBIC);
        }

        private void _TextBoxBIC_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxBIC();
        }

        #endregion

        #region IBAN1

        private bool Verif_TextBoxIBAN1()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxIBAN1, 4);
        }

        private void _TextBoxIBAN1_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxIBAN1();
        }

        #endregion

        #region IBAN2

        private bool Verif_TextBoxIBAN2()
		{
            return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxIBAN2,4);
        }

        private void _TextBoxIBAN2_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxIBAN2();
        }

        #endregion

        #region IBAN3

        private bool Verif_TextBoxIBAN3()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxIBAN3, 4);
        }

        private void _TextBoxIBAN3_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxIBAN3();
        }

        #endregion

        #region IBAN4

        private bool Verif_TextBoxIBAN4()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxIBAN4, 4);
        }

        private void _TextBoxIBAN4_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxIBAN4();
        }

        #endregion

        #region IBAN5

        private bool Verif_TextBoxIBAN5()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxIBAN5, 4);
        }

        private void _TextBoxIBAN5_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxIBAN5();
        }

        #endregion

        #region IBAN6

        private bool Verif_TextBoxIBAN6()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxIBAN6, 4);
        }

        private void _TextBoxIBAN6_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxIBAN6();
        }

        #endregion

        #region IBAN7

        private bool Verif_TextBoxIBAN7()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxIBAN7, 3);
        }

        private void _TextBoxIBAN7_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxIBAN7();
        }

        #endregion

        #region Etablissement

        private bool Verif_TextBoxEtablissement()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxEtablissement, 5);
        }

        private void _TextBoxEtablissement_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxEtablissement();
        }

        #endregion

        #region Guichet

        private bool Verif_TextBoxGuichet()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxGuichet, 5);
        }

        private void _TextBoxGuichet_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxGuichet();
        }

        #endregion

        #region Compte

        private bool Verif_TextBoxCompte()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxCompte, 11);
        }

        private void _TextBoxCompte_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCompte();
        }

        #endregion

        #region Cle

        private bool Verif_TextBoxCle()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxCle, 2);
        }

        private void _TextBoxCle_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCle();
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

        #region proprieté de dependance

        public ObservableCollection<Banque> listBanque
        {
            get { return (ObservableCollection<Banque>)GetValue(listBanqueProperty); }
            set { SetValue(listBanqueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listBanque.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listBanqueProperty =
            DependencyProperty.Register("listBanque", typeof(ObservableCollection<Banque>), typeof(EntrepriseRIBWindow), new UIPropertyMetadata(null));

        #endregion

        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #endregion

        #region Lecture seule

        public void lectureSeule()
        {
            _ComboBoxBanque.IsEnabled = false;
            _TextBoxBIC.IsReadOnly = true;
            _TextBoxIBAN1.IsReadOnly = true;
            _TextBoxIBAN2.IsReadOnly = true;
            _TextBoxIBAN3.IsReadOnly = true;
            _TextBoxIBAN4.IsReadOnly = true;
            _TextBoxIBAN5.IsReadOnly = true;
            _TextBoxIBAN6.IsReadOnly = true;
            _TextBoxIBAN7.IsReadOnly = true;
            _TextBoxEtablissement.IsReadOnly = true;
            _TextBoxGuichet.IsReadOnly = true;
            _TextBoxCompte.IsReadOnly = true;
            _TextBoxCle.IsReadOnly = true;
        }

        #endregion

    }
}
