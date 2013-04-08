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
    /// Logique d'interaction pour VilleWindow.xaml
    /// </summary>
    public partial class VilleWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public VilleWindow()
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
            this.mesPays = new ObservableCollection<Pays>(((App)App.Current).mySitaffEntities.Pays.OrderBy(pay => pay.Libelle));
        }

        #endregion

        #endregion

        #region propriété de dépendance

        public ObservableCollection<Pays> mesPays
        {
            get { return (ObservableCollection<Pays>)GetValue(mesPaysProperty); }
            set { SetValue(mesPaysProperty, value); }
        }

        // Using a DependencyProperty as the backing store for mesPays.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty mesPaysProperty =
            DependencyProperty.Register("mesPays", typeof(ObservableCollection<Pays>), typeof(VilleWindow), new UIPropertyMetadata(null));

        #endregion

        #region verfication champs

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxNom())
            {
                verif = false;
            }
            if (!Verif_TextBoxCodePostal())
            {
                verif = false;
            }
            if (!Verif_ComboBoxPays())
            {
                verif = false;
            }

            return verif;
        }

        #region _TextBoxNom

        private bool Verif_TextBoxNom()
        {            
			return ((App)App.Current).verifications.TextBoxObligatoire(_TextBoxNom, textBlock1);
        }

        private void _TextBoxNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxNom();
        }
        #endregion

        #region _TextBoxCodePostal

        private bool Verif_TextBoxCodePostal()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxCodePostal, textBlock2);
        }

        private void _TextBoxCodePostal_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCodePostal();
        }

        #endregion

        #region _ComboBoxPays

        private bool Verif_ComboBoxPays()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(_ComboBoxPays, textBlock3);
        }

        private void _ComboBoxPays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxPays();
        }

        #endregion

        #endregion

        #region bouton ok et annuler
        
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                if (((App)App.Current).mySitaffEntities.Ville.Where(act => act.Identifiant != ((Ville)this.DataContext).Identifiant).Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxNom.Text.Trim().ToLower() && lib.Code_Postal.Trim().ToLower() == this._TextBoxCodePostal.Text.Trim().ToLower() && lib.Pays1.Identifiant == ((Pays)this._ComboBoxPays.SelectedItem).Identifiant).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Une ville est déjà présente avec ce libellé", "Doublon de ville", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            this._ComboBoxPays.IsEnabled = false;
            this._TextBoxCodePostal.IsReadOnly = false;
            this._TextBoxNom.IsReadOnly = false;
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
