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
    /// Logique d'interaction pour CiviliteWindow.xaml
    /// </summary>
    public partial class EntrepriseMereWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region contructeur

        public EntrepriseMereWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxEntrepseMere.Focus();
         
        }

        #region initialisation

        private void initialisationPropDependance()
        {
            this.ListEntreprise = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.OrderBy(ent => ent.Libelle));
        }

        #endregion

        #endregion

        #region proprietés de dependances

        public ObservableCollection<Entreprise> ListEntreprise
        {
            get { return (ObservableCollection<Entreprise>)GetValue(ListEntrepriseProperty); }
            set { SetValue(ListEntrepriseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListEntreprise.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListEntrepriseProperty =
            DependencyProperty.Register("ListEntreprise", typeof(ObservableCollection<Entreprise>), typeof(EntrepriseMereWindow), new UIPropertyMetadata(null));

        #endregion

        #region Verfication champs
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxEntrepseMere())
            {
                verif = false;
            }
            if (!Verif_TextBoxDescription())
            {
                verif = false;
            }
            if (!Verif_TextBoxIdentificateur())
            {
                verif = false;
            }
            if (!Verif_ComboBoxEntreprise())
            {
                verif = false;
            }
            if (!Verif_TextBoxAdresseEMail())
            {
                verif = false;
            }
            if (!Verif_TextBoxLogo())
            {
                verif = false;
            }

            return verif;
        }
        #region _TextBoxEntrepseMere

        private bool Verif_TextBoxEntrepseMere()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxEntrepseMere, this.label1, 50);
        }

        private void _TextBoxEntrepseMere_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxEntrepseMere();
        }

        #endregion

        #region _TextBoxDescription

        private bool Verif_TextBoxDescription()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxDescription, this.label2, 20);
        }

        private void _TextBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxDescription();
        }

        #endregion

        #region _TextBoxIdentificateur

        private bool Verif_TextBoxIdentificateur()
        {
            bool verif = true;

            if (this._TextBoxIdentificateur.Text.Trim().Length == 2)
            {
                int test;

                if (int.TryParse(this._TextBoxIdentificateur.Text, out test))
                {
                    verif = true;
                }
                else
                {
                    verif = false;
                }
            }
            else
            {
                verif = false;
            }

			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxIdentificateur, this.label3, verif);
            return verif;
        }

        private void _TextBoxIdentificateur_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxIdentificateur();
        }

        #endregion

        #region Entreprise

        private bool Verif_ComboBoxEntreprise()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxEntreprise, this._TextBlockEntreprise);
        }

        private void _ComboBoxEntreprise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxEntreprise();
        }

        #endregion

        #region adresse email

        private bool Verif_TextBoxAdresseEMail()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxAdresseEMail, this.textBlockAdresseEMail);
        }

        private void _TextBoxAdresseEMail_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxAdresseEMail();
        }

        #endregion

        #region Logo

        private bool Verif_TextBoxLogo()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxLogo, this.textBlockLogo);
        }

        private void _TextBoxLogo_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLogo();
        }

        #endregion


        #endregion

        #region bouton ok et annuler

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                if (((App)App.Current).mySitaffEntities.Entreprise_Mere.Where(act => act.Identifiant != ((Entreprise_Mere)this.DataContext).Identifiant).Where(nom => nom.Nom.Trim().ToLower() == this._TextBoxEntrepseMere.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Une entreprise mère est déjà présente avec ce libellé", "Doublon d'entreprise mère", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            this._TextBoxDescription.IsReadOnly = false;
            this._TextBoxEntrepseMere.IsReadOnly = false;
            this._TextBoxIdentificateur.IsReadOnly = false;
            this._ComboBoxEntreprise.IsReadOnly = false;
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
