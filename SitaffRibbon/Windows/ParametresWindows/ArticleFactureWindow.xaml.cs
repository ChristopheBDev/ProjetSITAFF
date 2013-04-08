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
    /// Logique d'interaction pour ArticleFactureWindow.xaml
    /// </summary>
    public partial class ArticleFactureWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region Constructeur

        public ArticleFactureWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxLibelle.Focus();
        }

        #region initialisation

        private void initialisationPropDependance()
        {
            this.listPlanComptableImputation = new ObservableCollection<Plan_Comptable_Imputation>(((App)App.Current).mySitaffEntities.Plan_Comptable_Imputation.OrderBy(pla => pla.Numero));
            this.listPlanComptableTva = new ObservableCollection<Plan_Comptable_Tva>(((App)App.Current).mySitaffEntities.Plan_Comptable_Tva.OrderBy(pla => pla.Numero));
        }

        #endregion

        #endregion

        #region Proprietés de dependances



        public ObservableCollection<Plan_Comptable_Imputation> listPlanComptableImputation
        {
            get { return (ObservableCollection<Plan_Comptable_Imputation>)GetValue(listPlanComptableImputationProperty); }
            set { SetValue(listPlanComptableImputationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listPlanComptableImputation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listPlanComptableImputationProperty =
            DependencyProperty.Register("listPlanComptableImputation", typeof(ObservableCollection<Plan_Comptable_Imputation>), typeof(ArticleFactureWindow), new UIPropertyMetadata(null));




        public ObservableCollection<Plan_Comptable_Tva> listPlanComptableTva
        {
            get { return (ObservableCollection<Plan_Comptable_Tva>)GetValue(listPlanComptableTvaProperty); }
            set { SetValue(listPlanComptableTvaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listPlanComptableTva.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listPlanComptableTvaProperty =
            DependencyProperty.Register("listPlanComptableTva", typeof(ObservableCollection<Plan_Comptable_Tva>), typeof(ArticleFactureWindow), new UIPropertyMetadata(null));

        

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
                if (((App)App.Current).mySitaffEntities.Article_Facture.Where(act => act.Identifiant != ((Article_Facture)this.DataContext).Identifiant).Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxLibelle.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Un article est déjà présent avec ce libellé", "Doublon d'article", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
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

        /// <summary>
        /// Verifie si tous les champs sont bien renseignés.
        /// </summary>
        /// <returns>booléen vrai si tous les champs sont bien renseignés, sinon retourne faux</returns>
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!verif_tab_article())
            {
                verif = false;
            }


            return verif;
        }
        #region Tab article
        private bool verif_tab_article()
        {
            bool test = true;

            if (!Verif_TextBoxLibelle())
            {
                test = false;
            }
            if (!Verif_TextBoxCode())
            {
                test = false;
            }
            if (!Verif_TextBoxCondition())
            {
                test = false;
            }
            if (!Verif_ComboBoxPlanComptableImputation1())
            {
                test = false;
            }
            if (!Verif_ComboBoxPlanComptableImputation2())
            {
                test = false;
            }
            if (!Verif_ComboBoxPlanComptableTva())
            {
                test = false;
            }

            return test;
        }
        #endregion

        #region Libelle
        private bool Verif_TextBoxLibelle()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxLibelle, this._TextBlockLibelle);
        }


        private void _TextBoxLibelle_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelle();
        }
        #endregion

        #region Code
        private bool Verif_TextBoxCode()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxCode, this._TextBlockCode);
        }


        private void _TextBoxCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCode();
        }
        #endregion

        #region Condition

        private bool Verif_TextBoxCondition()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxCondition, this._TextBlockCondition);
        }

        private void _TextBoxCondition_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCondition();
        }

        #endregion

        #region Plan comptable imputation

        private bool Verif_ComboBoxPlanComptableImputation1()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxPlanComptableImputation1, this._TextBlockPlanComptableImputation1);
        }

        private void _ComboBoxPlanComptableImputation1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxPlanComptableImputation1();
        }

        #endregion

        #region Plan comptable imputation exhonere

        private bool Verif_ComboBoxPlanComptableImputation2()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._ComboBoxPlanComptableImputation2, this._TextBlockPlanComptableImputation2);
        }


        private void _ComboBoxPlanComptableImputation2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxPlanComptableImputation2();
        }

        #endregion

        #region Plan comptable tva

        private bool Verif_ComboBoxPlanComptableTva()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxPlanComptableTva, this._TextBlockPlanComptableTva);
        }


        private void _ComboBoxPlanComptableTva_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxPlanComptableTva();
        }

        #endregion

        #endregion

        #region lecture seule

        public void lectureSeule()
        {
            _TextBoxLibelle.IsReadOnly = false;
            _TextBoxCode.IsReadOnly = false;
            _TextBoxCondition.IsReadOnly = false;
            _ComboBoxPlanComptableImputation1.IsEnabled = false;
            _ComboBoxPlanComptableImputation2.IsEnabled = false;
            _ComboBoxPlanComptableTva.IsEnabled = false;
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
