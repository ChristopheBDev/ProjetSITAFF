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
using SitaffRibbon.UserControls;
using SitaffRibbon.Windows.ParametresWindows;
using SitaffRibbon.Windows.ParametresUserControls;
using SitaffRibbon.Classes;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour ReservationSalleSelectionSalarieWindow.xaml
    /// </summary>
    public partial class ReservationSalleSelectionSalarieWindow : Window
    {
        public ReservationSalleSelectionSalarieWindow()
        {
            InitializeComponent();
            this.listEntrepriseMere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(em => em.Nom));
        }

        #region Propriété de Dépendance

        #region Entreprise Mere

        public ObservableCollection<Entreprise_Mere> listEntrepriseMere
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntrepriseMereProperty); }
            set { SetValue(listEntrepriseMereProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listEntrepriseMere.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseMereProperty =
            DependencyProperty.Register("listEntrepriseMere", typeof(ObservableCollection<Entreprise_Mere>), typeof(ReservationSalleSelectionSalarieWindow), new UIPropertyMetadata(null));

        #endregion

        #region Salarie

        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(ReservationSalleSelectionSalarieWindow), new UIPropertyMetadata(null));

        #endregion

        #endregion

        #region Boutons

        #region Bouton Ok et Cancel
        /// <summary>
        /// Fonction lancée après clic sur Annuler
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (Verif_General())
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

        #endregion

        #region Vérifications

        private bool Verif_General()
        {
            bool verif = true;
            if (!this.Verif_ComboBoxEntrepriseMere())
            {
                verif = false;
            }
            if (!this.Verif_ComboBoxSalarie())
            {
                verif = false;
            }
            return verif;
        }

        #region ComboBox

        #region Entreprise Mere
        private bool Verif_ComboBoxEntrepriseMere()
        {
            bool verif = true;
            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._comboBoxEntrepriseMere.SelectedItem == null)
            {
                verif = false;
                this._comboBoxEntrepriseMere.Background = rouge;
            }
            else
            {
                verif = true;
                this._comboBoxEntrepriseMere.Background = vert;
            }
            return verif;
        }
        private void _ComboBoxEntrepriseMere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxEntrepriseMere();
            this.listSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.Where(s => s.Salarie_Interne.Entreprise_Mere1.Identifiant == ((Entreprise_Mere)this._comboBoxEntrepriseMere.SelectedItem).Identifiant).OrderBy(sa => sa.Personne.Nom));

           
            
        }
        #endregion

        #region Contact
        private bool Verif_ComboBoxSalarie()
        {
            bool verif = true;
            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._comboxBoxSalarie.SelectedItem == null)
            {
                verif = false;
                this._comboxBoxSalarie.Background = rouge;
            }
            else
            {
                verif = true;
                this._comboxBoxSalarie.Background = vert;
            }
            return verif;
        }
        private void _ComboBoxSalarie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxSalarie();
        }
        #endregion

        #endregion

        #endregion
    }
}
