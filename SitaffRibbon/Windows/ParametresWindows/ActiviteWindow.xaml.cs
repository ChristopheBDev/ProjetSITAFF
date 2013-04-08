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
    /// Logique d'interaction pour ActiviteWindow.xaml
    /// </summary>
    public partial class ActiviteWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region contructeur

        public ActiviteWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxActivite.Focus();
           
        }

        #region initialisation

        private void initialisationPropDependance()
        {
            this.mesDomaines = new ObservableCollection<Domaine>(((App)App.Current).mySitaffEntities.Domaine.OrderBy(dom => dom.Libelle));
        }

        #endregion

        #endregion

        #region propriété de dépendance

        public ObservableCollection<Domaine> mesDomaines
        {
            get { return (ObservableCollection<Domaine>)GetValue(mesDomainesProperty); }
            set { SetValue(mesDomainesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for mesDoamaines.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty mesDomainesProperty =
            DependencyProperty.Register("mesDomaines", typeof(ObservableCollection<Domaine>), typeof(ActiviteWindow), new UIPropertyMetadata(null));

        #endregion

        #region Verfication champs
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxActivite())
            {
                verif = false;
            }
            if (!Verif_TextBoxCode())
            {
                verif = false;
            }
            if (!Verif_ComboBoxDomaine())
            {
                verif = false;
            }


            return verif;
        }
        #region _TextBoxActivite

        private bool Verif_TextBoxActivite()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxActivite, this.label1);
        }

        private void _TextBoxActivite_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxActivite();
        }

        #endregion

        #region _TextBoxCode

        private bool Verif_TextBoxCode()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxCode, this.label2);
        }

        private void _TextBoxCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCode();
        }

        #endregion

        #region _ComboBoxDomaine

        private bool Verif_ComboBoxDomaine()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxDomaine, this.textBlock1);
        }

        private void _ComboBoxDomaine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxDomaine();
        }

        #endregion

        #endregion

        #region bouton ok et annuler

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                if (((App)App.Current).mySitaffEntities.Activite.Where(act => act.Identifiant != ((Activite)this.DataContext).Identifiant).Where(act => act.Libelle.Trim().ToLower() == this._TextBoxActivite.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Une activité est déjà présente avec ce libellé", "Doublon d'activité", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            this._TextBoxCode.IsReadOnly = false;
            this._TextBoxActivite.IsReadOnly = false;
            this._ComboBoxDomaine.IsReadOnly = false;
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
