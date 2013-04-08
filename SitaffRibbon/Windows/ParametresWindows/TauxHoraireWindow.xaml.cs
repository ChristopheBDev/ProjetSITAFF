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
    public partial class TauxHoraireWindow : Window
    {

        #region propriété de dépendance

        public ObservableCollection<Entreprise_Mere> mesEntreprisesMeres
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(mesEntreprisesMeresProperty); }
            set { SetValue(mesEntreprisesMeresProperty, value); }
        }

        // Using a DependencyProperty as the backing store for mesEntreprisesMeres.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty mesEntreprisesMeresProperty =
            DependencyProperty.Register("mesEntreprisesMeres", typeof(ObservableCollection<Entreprise_Mere>), typeof(TauxHoraireWindow), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public TauxHoraireWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxValeur.Focus();            
        }

        #region Initialisation

        private void initialisationPropDependance()
        {
            this.mesEntreprisesMeres = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(em => em.Nom));
        }

        #endregion

        #endregion

        #region verfication champs
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxValeur())
            {
                verif = false;
            }

            if (!Verif_DatePickerDateDebut())
            {
                verif = false;
            }

            if (!Verif_DatePickerDateFin())
            {
                verif = false;
            }

            if (!Verif_ComboBoxEntrepriseMere())
            {
                verif = false;
            }


            return verif;
        }
        #region _TextBoxValeur

        private bool Verif_TextBoxValeur()
        {
            bool verif = true;
            char[] masque = new char[] { '&', '"', '\'', '(', '_', ')', '=', '~', '#', '{', '[', '|', '`', '\\', '^', '@', ']', '}', '*', '+', ',', ';', ':', '!', '?', '.', '/', '§', '¨', '%', '£', 'µ', '$', '¤', '<', '>', 'a', 'z', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'q', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'w', 'x', 'c', 'v', 'b', 'n' };
            int j = 0;
            char i;

            if (this._TextBoxValeur.Text.Trim().Length > 0)
            {
                //foreach (char i in masque)
                while ((j < masque.Length) && (verif))
                {
                    i = masque[j];
                    if (this._TextBoxValeur.Text.Contains(i))
                    {
                        verif = false;
                    }
                    else
                    {
                        verif = true;
                    }
                    j++;
                }
            }
            else
            {
                verif = false;
            }

			((App)App.Current).verifications.MettreTextBoxEnCouleur(_TextBoxValeur, textBlock1, verif);
            return verif;
        }

        private void _TextBoxValeur_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxValeur();
        }
        #endregion

        #region _DatePickerDateDebut

        private bool Verif_DatePickerDateDebut()
        {
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(_DatePickerDateDebut, textBlock2);
        }

        private void _DatePickerDateDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerDateDebut();
        }

        #endregion

        #region _DatePickerDateFin

        private bool Verif_DatePickerDateFin()
        {
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(_DatePickerDateFin, textBlock3, _DatePickerDateDebut);
        }

        private void _DatePickerDateFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerDateFin();
        }

        #endregion

        #region _ComboBoxEntrepriseMere

        private bool Verif_ComboBoxEntrepriseMere()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(_ComboBoxEntrepriseMere, textBlock4);
        }

        private void _ComboBoxEntrepriseMere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxEntrepriseMere();
        }

        #endregion

        #endregion

        #region bouton ok et annuler

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

        #region Lecture seule

        public void lectureSeule()
        {
            this._TextBoxValeur.IsReadOnly = true;

            this._DatePickerDateDebut.IsEnabled = false;
            this._DatePickerDateFin.IsEnabled = false;

            this._ComboBoxEntrepriseMere.IsEnabled = false;

        }

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #endregion

    }
}
