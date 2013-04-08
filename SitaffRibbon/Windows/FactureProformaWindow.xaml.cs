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
using System.ComponentModel;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour FactureProformaWindow.xaml
    /// </summary>
    public partial class FactureProformaWindow : Window
    {

        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region Proprietés de dependance
        public ObservableCollection<Fournisseur> listFournisseurs
        {
            get { return (ObservableCollection<Fournisseur>)GetValue(listFournisseursProperty); }
            set { SetValue(listFournisseursProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listFournisseurs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFournisseursProperty =
            DependencyProperty.Register("listFournisseurs", typeof(ObservableCollection<Fournisseur>), typeof(FactureProformaWindow), new UIPropertyMetadata(null));

        #endregion

        #region contructeur
        public FactureProformaWindow()
        {
            InitializeComponent();


            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._textBoxNumeroFacture.Focus();

        }
        #region initialisation

        private void initialisationPropDependance()
        {
            this.listFournisseurs = new ObservableCollection<Fournisseur>(((App)App.Current).mySitaffEntities.Fournisseur.OrderBy(four => four.Entreprise.Libelle));

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

        #region Vérifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_textBoxNumeroFacture())
            {
                verif = false;
            }
            if (!Verif_DatePickerDateFacture())
            {
                verif = false;
            }
            if (!Verif_textBoxMontant())
            {
                verif = false;
            }
            if (!Verif_comboBoxFournisseur())
            {
                verif = false;
            }

            return verif;
        }

        #region Fournisseur

        private bool Verif_comboBoxFournisseur()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxFournisseur, this._textBlockFournisseur);
        }

        private void _comboBoxFournisseur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxFournisseur();
        }

        #endregion

        #region NumeroFacture

        private bool Verif_textBoxNumeroFacture()
        {
            bool verif = true;

            if (this._textBoxNumeroFacture.Text.Trim().Length <= 0)
            {
                verif = false;
            }
            else
            {
                ObservableCollection<Facture_Proforma> facturesSansMoi = new ObservableCollection<Facture_Proforma>();
                foreach (Facture_Proforma fac in ((App)App.Current).mySitaffEntities.Facture_Proforma)
                {
                    if (fac.Identifiant != ((Facture_Proforma)this.DataContext).Identifiant)
                    {
                        facturesSansMoi.Add(fac);
                    }
                }
                if (facturesSansMoi.Where(fac => fac.Numero.Trim() == this._textBoxNumeroFacture.Text.Trim()).Count() == 0)
                {
                    verif = true;
                }
                else
                {
                    verif = false;
                }
            }
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._textBoxNumeroFacture, this._textBlockNumeroFacture, verif);

            return verif;
        }

        private void _textBoxNumeroFacture_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_textBoxNumeroFacture();
        }

        #endregion

        #region DateFacture

        private bool Verif_DatePickerDateFacture()
        {
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._DatePickerDateFacture, this._textBlockDateFacture);
        }

        private void _DatePickerDateFacture_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerDateFacture();
        }

        #endregion

        #region Montant

        private bool Verif_textBoxMontant()
        {
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxMontant, this._textBlockMontant);
        }

        private void _textBoxMontant_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_textBoxMontant();
        }

        #endregion

        #endregion

        #region fenetre chargé
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
            this._comboBoxFournisseur.SelectedItem = ((Facture_Proforma)this.DataContext).Fournisseur1;
            if (((Facture_Proforma)this.DataContext).Fournisseur1 != null && ((Facture_Proforma)this.DataContext).Commande_Fournisseur1 != null)
            {
                this._comboBoxFournisseur.IsEnabled = false;
            }
        }
        #endregion

        #region lecture seule
        public void lectureSeule()
        {
            //TextBox
            this._textBoxNumeroFacture.IsReadOnly = true;
            this._textBoxMontant.IsReadOnly = true;
            this._TextBoxCommentaires.IsReadOnly = true;
            //ComboBox
            this._comboBoxFournisseur.IsEnabled = false;
            //DatePicker
            this._DatePickerDateFacture.IsEnabled = false;
            //CheckBox
            this._checkBoxNormalisee.IsEnabled = false;
        }
        #endregion

    }
}
