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
    /// Logique d'interaction pour UtilisateurWindow.xaml
    /// </summary>
    public partial class UtilisateurWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;
        public bool creation = false;

        #endregion

        #region constructeur

        public UtilisateurWindow()
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
            this.mesSalariesInternes = new ObservableCollection<Salarie_Interne>(((App)App.Current).mySitaffEntities.Salarie_Interne.OrderBy(sal => sal.Salarie.Personne.Nom).ThenBy(sal => sal.Salarie.Personne.Prenom));
            this.mesNiveauxSecurite = new ObservableCollection<Niveau_Securite>(((App)App.Current).mySitaffEntities.Niveau_Securite.OrderBy(niv => niv.Libelle));
        }

        #endregion

        #endregion

        #region propriété de dépendance

        #region mesSalariesInternes


        public ObservableCollection<Salarie_Interne> mesSalariesInternes
        {
            get { return (ObservableCollection<Salarie_Interne>)GetValue(mesSalariesInternesProperty); }
            set { SetValue(mesSalariesInternesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for mesSalariesInternes. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty mesSalariesInternesProperty =
            DependencyProperty.Register("mesSalariesInternes", typeof(ObservableCollection<Salarie_Interne>), typeof(UtilisateurWindow), new UIPropertyMetadata(null));

        
        #endregion

        #region mesNiveauxSecurite


        public ObservableCollection<Niveau_Securite> mesNiveauxSecurite
        {
            get { return (ObservableCollection<Niveau_Securite>)GetValue(mesNiveauxSecuriteProperty); }
            set { SetValue(mesNiveauxSecuriteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for mesNiveauxSecurite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty mesNiveauxSecuriteProperty =
            DependencyProperty.Register("mesNiveauxSecurite", typeof(ObservableCollection<Niveau_Securite>), typeof(UtilisateurWindow), new UIPropertyMetadata(null));

        
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
            if (!Verif_TextBoxMotDePasse())
            {
                verif = false;
            }
            if (!Verif_ComboBoxSalarieInterne())
            {
                verif = false;
            }
            if (!Verif_ComboBoxNiveauSecurite())
            {
                verif = false;
            }
            if (!Verif_TextBoxSignature())
            {
                verif = false;
            }


            return verif;
        }

        #region _TextBoxNom

        private bool Verif_TextBoxNom()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(_TextBoxNom, textBlock1, 150);
        }

        private void _TextBoxNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxNom();
        }
        #endregion

        #region _TextBoxMotDePasse

        private bool Verif_TextBoxMotDePasse()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(_TextBoxMotDePasse, textBlock2, 255);
        }

        private void _TextBoxMotDePasse_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxMotDePasse();
        }

        #endregion

        #region _ComboBoxSalarieInterne

        private bool Verif_ComboBoxSalarieInterne()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(_ComboBoxSalarieInterne, textBlock3);
        }

        private void _ComboBoxSalarieInterne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxSalarieInterne();
        }

        #endregion

        #region _ComboBoxNiveauSecurite

        private bool Verif_ComboBoxNiveauSecurite()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(_ComboBoxNiveauSecurite, textBlock4);
        }

        private void _ComboBoxNiveauSecurite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxNiveauSecurite();
        }

        #endregion

        #region Textbox Signature

        private bool Verif_TextBoxSignature()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(_TextBoxSignature, textBlockSignature, 255);
        }

        private void _TextBoxSignature_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSignature();
        }
        
        #endregion

        #endregion

        #region bouton ok et annuler

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                if (((App)App.Current).mySitaffEntities.Utilisateur.Where(act => act.Identifiant != ((Utilisateur)this.DataContext).Identifiant).
                    Where(nom => nom.Nom_Utilisateur.Trim().ToLower() == this._TextBoxNom.Text.Trim().ToLower() || nom.Salarie_Interne1.Identifiant == ((Salarie_Interne)this._ComboBoxSalarieInterne.SelectedItem).Identifiant).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Un utilisateur est déjà présent avec ce nom ou ce salarié à déjà un compte", "Doublon d'utilisateur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            this._ComboBoxSalarieInterne.IsEnabled = false;
            this._ComboBoxNiveauSecurite.IsEnabled = false;
            this._TextBoxMotDePasse.IsReadOnly = false;
            this._TextBoxNom.IsReadOnly = false;
            this._TextBoxSignature.IsReadOnly = false;
        }

        #endregion

        #region fenetre chargé

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
            if (!creation)
            {
                this._TextBoxMotDePasse.Visibility = Visibility.Collapsed;
                this.textBlock2.Visibility = Visibility.Collapsed;
            }
        }
        #endregion


    }
}
