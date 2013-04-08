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

namespace SitaffRibbon.Windows.ParametresWindows
{
    /// <summary>
    /// Logique d'interaction pour TypeFraisWindow.xaml
    /// </summary>
    public partial class TypeFraisWindow : Window
    {

        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region Propriété de dépendances

        public ObservableCollection<Plan_Comptable_Tva> listPlanComptableTva
        {
            get { return (ObservableCollection<Plan_Comptable_Tva>)GetValue(listPlanComptableTvaProperty); }
            set { SetValue(listPlanComptableTvaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listTypeFrais.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listPlanComptableTvaProperty =
            DependencyProperty.Register("listPlanComptableTva", typeof(ObservableCollection<Plan_Comptable_Tva>), typeof(TypeFraisWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Plan_Comptable_Imputation> listPlanComptableImputation
        {
            get { return (ObservableCollection<Plan_Comptable_Imputation>)GetValue(listPlanComptableImputationProperty); }
            set { SetValue(listPlanComptableImputationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listTypeFrais.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listPlanComptableImputationProperty =
            DependencyProperty.Register("listPlanComptableImputation", typeof(ObservableCollection<Plan_Comptable_Imputation>), typeof(TypeFraisWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Entreprise_Mere> listEntrepriseMere
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntrepriseMereProperty); }
            set { SetValue(listEntrepriseMereProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listTypeFrais.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseMereProperty =
            DependencyProperty.Register("listEntrepriseMere", typeof(ObservableCollection<Entreprise_Mere>), typeof(TypeFraisWindow), new UIPropertyMetadata(null));

        #endregion

        #region constructeur

        #region Initialisation
        private void initialisationPropDependance()
        {
            this.listPlanComptableTva = new ObservableCollection<Plan_Comptable_Tva>(((App)App.Current).mySitaffEntities.Plan_Comptable_Tva.OrderBy(pct => pct.Tva1.Libelle));
            this.listPlanComptableImputation = new ObservableCollection<Plan_Comptable_Imputation>(((App)App.Current).mySitaffEntities.Plan_Comptable_Imputation.OrderBy(pci => pci.Numero));
            this.listEntrepriseMere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(em => em.Nom).Where(em => em.Entreprise1.Is_Client == true));
        }
        #endregion

        public TypeFraisWindow()
        {
            InitializeComponent();

            this.initialisationPropDependance();
        }

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

        #region Verifications
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxLibelle())
            {
                verif = false;
            }
            if (!this.Verif_TextBoxPourcentage())
            {
                verif = false;
            }
            if (!this.Verif_ComboBoxPlanComptableTva())
            {
                verif = false;
            }
            if (!this.Verif_ComboBoxPlanComptableImputation())
            {
                verif = false;
            }
            if (!this.Verif_ComboBoxEntrepriseMere())
            {
                verif = false;
            }
            if (!this.Verif_ComboBoxPlanComptableImputationEtranger())
            {
                verif = false;
            }
            if (!this.Verif_ComboBoxPlanComptableTvaEtranger())
            {
                verif = false;
            }

            return verif;
        }


        private bool Verif_TextBoxLibelle()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(_TextBoxLibelle, _TextBlockLibelle);
        }

        private void _TextBoxLibelle_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelle();
        }


        private bool Verif_TextBoxPourcentage()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(_TextBoxPourcentage, _TextBlockPourcentage);
        }

        private void _TextBoxPourcentage_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxPourcentage();
        }


        private bool Verif_ComboBoxPlanComptableTva()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(_comboBoxPlanComptableTva, _TextBlockPlanComptableTva);
        }

        private void _comboBoxPlanComptableTva_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxPlanComptableTva();
        }


        private bool Verif_ComboBoxPlanComptableImputation()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(_comboBoxPlanComptableImputation, _TextBlockPlanComptableImputation);
        }

        private void _comboBoxPlanComptableImputation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxPlanComptableImputation();
        }


        private bool Verif_CheckBoxClientObligatoire()
        {
            bool verif = false;
            if (this._comboBoxEntrepriseMere.SelectedItem == null && this._checkBoxClientObligatoire.IsChecked == true)
            {
                ((App)App.Current).verifications.MettreCheckBoxEnCouleur(_checkBoxClientObligatoire, verif);
                this._TextBlockClientObligatoire.Foreground = ((App)App.Current).verifications.Couleur_Textblock_Non_Ok;
            }
            if (this._checkBoxClientObligatoire.IsChecked == false)
            {
                verif = true;
                ((App)App.Current).verifications.MettreCheckBoxEnCouleur(_checkBoxClientObligatoire, verif);
                this._TextBlockClientObligatoire.Foreground = ((App)App.Current).verifications.Couleur_Textblock_Ok;
            }
            Verif_ComboBoxEntrepriseMere();
            return verif;
        }

        private void _CheckBoxClientObligatoire_Click(object sender, RoutedEventArgs e)
        {
            this.Verif_CheckBoxClientObligatoire();
        }


        private bool Verif_ComboBoxEntrepriseMere()
        {
            if (this._checkBoxClientObligatoire.IsChecked == true)
            {
                if (this._comboBoxEntrepriseMere.SelectedItem == null)
                {
                    ((App)App.Current).verifications.MettreCheckBoxEnCouleur(_checkBoxClientObligatoire, false);
                    this._TextBlockClientObligatoire.Foreground = ((App)App.Current).verifications.Couleur_Textblock_Non_Ok;
                }
                else
                {
                    ((App)App.Current).verifications.MettreCheckBoxEnCouleur(_checkBoxClientObligatoire, true);
                    this._TextBlockClientObligatoire.Foreground = ((App)App.Current).verifications.Couleur_Textblock_Ok;
                }
                return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(_comboBoxEntrepriseMere, _TextBlockEntrepriseMere);
            }
            else
            {
                return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(_comboBoxEntrepriseMere, _TextBlockEntrepriseMere);
            }
        }

        private void _comboBoxEntrepriseMere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxEntrepriseMere();
        }


        private bool Verif_ComboBoxPlanComptableTvaEtranger()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(_comboBoxPlanComptableTvaEtranger, _TextBlockPlanComptableTvaEtranger);
        }

        private void _comboBoxPlanComptableTvaEtranger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxPlanComptableImputationEtranger();
        }


        private bool Verif_ComboBoxPlanComptableImputationEtranger()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(_comboBoxPlanComptableImputationEtranger, _TextBlockPlanComptableImputationEtranger);
        }

        private void _comboBoxPlanComptableImputationEtranger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxPlanComptableTvaEtranger();
        }
        #endregion

        #region lecture seule

        public void lectureSeule()
        {
            _TextBoxLibelle.IsReadOnly = false;
            _TextBoxPourcentage.IsReadOnly = false;
            _comboBoxPlanComptableTva.IsReadOnly = false;
            _comboBoxPlanComptableImputation.IsReadOnly = false;
            _comboBoxPlanComptableTvaEtranger.IsReadOnly = false;
            _comboBoxPlanComptableImputationEtranger.IsReadOnly = false;
            _comboBoxEntrepriseMere.IsReadOnly = false;
            _checkBoxClientObligatoire.IsEnabled = true;
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
