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


namespace SitaffRibbon.Windows.ParametresWindows
{
    /// <summary>
    /// Logique d'interaction pour DepartementWindowParametre.xaml
    /// </summary>
    public partial class DepartementWindowParametre : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region Constructeur

        public DepartementWindowParametre()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxNom.Focus();
        }

        #region initialisation

        private void initialisationPropDependance()
        {
            this.mesRegions = new ObservableCollection<Region>(((App)App.Current).mySitaffEntities.Region.OrderBy(reg => reg.Libelle));
        }

        #endregion

        #endregion

        #region propriété de dépendance

        #region mesRegions
        public ObservableCollection<Region> mesRegions
        {
            get { return (ObservableCollection<Region>)GetValue(mesRegionsProperty); }
            set { SetValue(mesRegionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for mesRegions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty mesRegionsProperty =
            DependencyProperty.Register("mesRegions", typeof(ObservableCollection<Region>), typeof(DepartementWindowParametre), new UIPropertyMetadata(null));

        #endregion

        #endregion

        #region verfication champs
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxNom())
            {
                verif = false;
            }
            if (!Verif_TextBoxNumero())
            {
                verif = false;
            }
            if (!Verif_ComboBoxRegion())
            {
                verif = false;
            }

            return verif;
        }
        #region _TextBoxNom

        private bool Verif_TextBoxNom()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxNom, this.textBlock1);
        }

        private void _TextBoxNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxNom();
        }
        #endregion

        #region _TextBoxNumero

        private bool Verif_TextBoxNumero()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxNumero, this.textBlock2);
        }

        private void _TextBoxNumero_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxNumero();
        }

        #endregion

        #region _ComboBoxRegion

        private bool Verif_ComboBoxRegion()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxRegion, this.textBlock3);
        }

        private void _ComboBoxRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxRegion();
        }

        #endregion



        #endregion

        #region bouton ok et annuler

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                if (((App)App.Current).mySitaffEntities.Departement.Where(act => act.Identifiant != ((Departement)this.DataContext).Identifiant).
                    Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxNom.Text.Trim().ToLower()).
                        Where(num => num.Numero.Trim().ToLower() == this._TextBoxNumero.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Un departement est déjà présent avec ce libellé et ce numéro", "Doublon de departement", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void _ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        #region lecture seule

        public void lectureSeule()
        {
            this._ComboBoxRegion.IsReadOnly = false;
            this._TextBoxNom.IsReadOnly = false;
            this._TextBoxNumero.IsReadOnly = false;
        }

        #endregion

        #region fenetre chargé

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #endregion
    }
}
