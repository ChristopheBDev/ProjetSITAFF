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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using SitaffRibbon.Classes;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour ReleveHeureAtelierWindow.xaml
    /// </summary>
    public partial class ReleveHeureAtelierWindow : Window
    {

        #region Attributs

        public bool creation = false;
        public bool editionTerminee = false;

        #endregion

        #region Propriétés de dépendances



        public ObservableCollection<Tache_Atelier> listTacheAtelier
        {
            get { return (ObservableCollection<Tache_Atelier>)GetValue(listTacheAtelierProperty); }
            set { SetValue(listTacheAtelierProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listTacheAtelier.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listTacheAtelierProperty =
            DependencyProperty.Register("listTacheAtelier", typeof(ObservableCollection<Tache_Atelier>), typeof(ReleveHeureAtelierWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(ReleveHeureAtelierWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(ReleveHeureAtelierWindow), new UIPropertyMetadata(null));



        #endregion

        #region Constructeur

        public ReleveHeureAtelierWindow()
        {
            InitializeComponent();

            //Création du menu de clic droit sur le datagrid
            this.creationMenuClicDroit();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Initialisation de la sécurité
            this.initialisationSecurite();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._comboBoxSalarie.Focus();
        }

        #region initialisation

        private void initialisationPropDependance()
        {
            this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
            this.listSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.Where(sal => sal.Chantier == true).OrderBy(sal => sal.Personne.Nom).ThenBy(sal => sal.Personne.Prenom));
            this.listTacheAtelier = new ObservableCollection<Tache_Atelier>(((App)App.Current).mySitaffEntities.Tache_Atelier.OrderBy(ta => ta.Libelle));
        }

        private void initialisationSecurite()
        {
        }

        #endregion

        #endregion

        #region boutons

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                this.VerificationSuppressionConneries(true);
                this.CalculTotaux();
                this.assuranceChiffres();
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

        #region Verifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!this.Verif_ObservationGenerale())
            {
                verif = false;
            }
            if (!this.Verif_RepasRouteObservation())
            {
                verif = false;
            }
            if (!Verif_EnTete())
            {
                verif = false;
            }

            return verif;
        }

        #region En tête

        private bool Verif_EnTete()
        {
            bool verif = true;

            if (!this.Verif_comboBoxSalarie())
            {
                verif = false;
            }
            if (!this.Verif_textBoxNumeroSemaine())
            {
                verif = false;
            }
            if (!this.Verif_datePickerDate_Debut())
            {
                verif = false;
            }

            return verif;
        }

        #region Salarie

        private bool Verif_comboBoxSalarie()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxSalarie, this._textBlockSalarie);
        }

        private void _comboBoxSalarie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Verif_comboBoxSalarie() && this.Verif_textBoxNumeroSemaine())
            {
                if (creation)
                {
                    if (!this.verifExistance())
                    {
                        this.VerrouillerLaFenetre();
                        MessageBox.Show("Un relevé d'atelier est déjà présent pour la semaine selectionnée et le salarié sélectionné. Vous ne pourrez pas valider votre relevé. Modifier le relevé voulu ou modifier les informations salarié ou semaine si elles ne sont pas bonnes", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        this.DeVerrouillerLaFenetre();
                    }
                }
            }
            else
            {
                if (creation)
                {
                    this.VerrouillerLaFenetre();
                }
            }
        }

        #endregion

        #region NumeroSemaine

        private bool Verif_textBoxNumeroSemaine()
        {
            try
            {
                ((Releve_Heure_Atelier)this.DataContext).NumeroSemaine = int.Parse(this._textBoxNumerosemaine.Text);
            }
            catch (Exception) { }
            if (this._textBoxNumerosemaine.Text.Length != 0 && this._comboBoxSalarie.SelectedItem != null)
            {
                if (creation)
                {
                    if (!this.verifExistance())
                    {
                        this.VerrouillerLaFenetre();
                        MessageBox.Show("Un relevé d'atelier est déjà présent pour la semaine selectionnée et le salarié sélectionné. Vous ne pourrez pas valider votre relevé. Modifier le relevé voulu ou modifier les informations salarié ou semaine si elles ne sont pas bonnes", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                        return false;
                    }
                    else
                    {
                        this.DeVerrouillerLaFenetre();
                    }
                }
            }
            else
            {
                if (creation)
                {
                    this.VerrouillerLaFenetre();
                    return false;
                }
            }            
            return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxNumerosemaine, this._textBlockNumero_Semaine);
        }

        private void _textBoxNumerosemaine_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.Verif_textBoxNumeroSemaine() && this.Verif_comboBoxSalarie())
            {
                if (creation)
                {
                    if (!this.verifExistance())
                    {
                        this.VerrouillerLaFenetre();
                        MessageBox.Show("Un relevé d'atelier est déjà présent pour la semaine selectionnée et le salarié sélectionné. Vous ne pourrez pas valider votre relevé. Modifier le relevé voulu ou modifier les informations salarié ou semaine si elles ne sont pas bonnes", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        this.DeVerrouillerLaFenetre();
                    }
                }
            }
            else
            {
                if (creation)
                {
                    this.VerrouillerLaFenetre();
                }
            }
            try
            {
                ((Releve_Heure_Atelier)this.DataContext).NumeroSemaine = int.Parse(this._textBoxNumerosemaine.Text);
            }
            catch (Exception) { }
        }

        #endregion

        #region Date Debut

        private bool Verif_datePickerDate_Debut()
        {
            return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDate_Debut, this._textBlockDate_Debut);
        }

        private void _datePickerDate_Debut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Verif_datePickerDate_Debut())
            {
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                System.Globalization.Calendar calendar = dfi.Calendar;
                int retour = calendar.GetWeekOfYear((DateTime)this._datePickerDate_Debut.SelectedDate, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                this._textBoxNumerosemaine.Text = retour.ToString();
                while (((DateTime)this._datePickerDate_Debut.SelectedDate).DayOfWeek != DayOfWeek.Monday)
                {
                    this._datePickerDate_Debut.SelectedDate = ((DateTime)this._datePickerDate_Debut.SelectedDate).AddDays(-1);
                }
                this._datePickerDate_Fin.SelectedDate = (DateTime)this._datePickerDate_Debut.SelectedDate;
                while (((DateTime)this._datePickerDate_Fin.SelectedDate).DayOfWeek != DayOfWeek.Friday)
                {
                    this._datePickerDate_Fin.SelectedDate = ((DateTime)this._datePickerDate_Fin.SelectedDate).AddDays(1);
                }

            }
        }

        #endregion

        #endregion

        #region Heures sur affaire
        //DataGrid Donc pas de verif (autovérifié)
        #endregion

        #region Heures sur tâche d'atelier
        //DataGrid Donc pas de verif (autovérifié)
        #endregion

        #region Tableau du total d'heures

        //Pas de verifs car en lectures seules

        #endregion

        #region Tableau repas - route - obs

        private bool Verif_RepasRouteObservation()
        {
            bool verif = true;

            if (!this.Verif_textBoxRoute_Lundi())
            {
                verif = false;
            }
            if (!this.Verif_textBoxRoute_Mardi())
            {
                verif = false;
            }
            if (!this.Verif_textBoxRoute_Mercredi())
            {
                verif = false;
            }
            if (!this.Verif_textBoxRoute_Jeudi())
            {
                verif = false;
            }
            if (!this.Verif_textBoxRoute_Vendredi())
            {
                verif = false;
            }
            if (!this.Verif_textBoxRoute_Samedi())
            {
                verif = false;
            }
            if (!this.Verif_textBoxObservation_Lundi())
            {
                verif = false;
            }
            if (!this.Verif_textBoxObservation_Mardi())
            {
                verif = false;
            }
            if (!this.Verif_textBoxObservation_Mercredi())
            {
                verif = false;
            }
            if (!this.Verif_textBoxObservation_Jeudi())
            {
                verif = false;
            }
            if (!this.Verif_textBoxObservation_Vendredi())
            {
                verif = false;
            }
            if (!this.Verif_textBoxObservation_Samedi())
            {
                verif = false;
            }

            return verif;
        }

        #region Route Lundi

        private bool Verif_textBoxRoute_Lundi()
        {
            return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxRoute_Lundi, 24);
        }

        private void _textBoxRoute_Lundi_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxRoute_Lundi();
        }

        #endregion

        #region Route Mardi

        private bool Verif_textBoxRoute_Mardi()
        {
            return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxRoute_Mardi, 24);
        }

        private void _textBoxRoute_Mardi_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxRoute_Mardi();
        }

        #endregion

        #region Route Mercredi

        private bool Verif_textBoxRoute_Mercredi()
        {
            return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxRoute_Mercredi, 24);
        }

        private void _textBoxRoute_Mercredi_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxRoute_Mercredi();
        }

        #endregion

        #region Route Jeudi

        private bool Verif_textBoxRoute_Jeudi()
        {
            return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxRoute_Jeudi, 24);
        }

        private void _textBoxRoute_Jeudi_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxRoute_Jeudi();
        }

        #endregion

        #region Route Vendredi

        private bool Verif_textBoxRoute_Vendredi()
        {
            return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxRoute_Vendredi, 24);
        }

        private void _textBoxRoute_Vendredi_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxRoute_Vendredi();
        }

        #endregion

        #region Route Samedi

        private bool Verif_textBoxRoute_Samedi()
        {
            return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxRoute_Vendredi, 24);
        }

        private void _textBoxRoute_Samedi_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxRoute_Samedi();
        }

        #endregion

        #region Observation Lundi

        private bool Verif_textBoxObservation_Lundi()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxObservation_Lundi, this._textBlockObservation);
        }

        private void _textBoxObservation_Lundi_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxObservation_Lundi();
        }

        #endregion

        #region Observation Mardi

        private bool Verif_textBoxObservation_Mardi()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxObservation_Mardi, this._textBlockObservation);
        }

        private void _textBoxObservation_Mardi_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxObservation_Mardi();
        }

        #endregion

        #region Observation Mercredi

        private bool Verif_textBoxObservation_Mercredi()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxObservation_Mercredi, this._textBlockObservation);
        }

        private void _textBoxObservation_Mercredi_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxObservation_Mercredi();
        }

        #endregion

        #region Observation Jeudi

        private bool Verif_textBoxObservation_Jeudi()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxObservation_Jeudi, this._textBlockObservation);
        }

        private void _textBoxObservation_Jeudi_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxObservation_Jeudi();
        }

        #endregion

        #region Observation Vendredi

        private bool Verif_textBoxObservation_Vendredi()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxObservation_Vendredi, this._textBlockObservation);
        }

        private void _textBoxObservation_Vendredi_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxObservation_Vendredi();
        }

        #endregion

        #region Observation Samedi

        private bool Verif_textBoxObservation_Samedi()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxObservation_Samedi, this._textBlockObservation);
        }

        private void _textBoxObservation_Samedi_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxObservation_Samedi();
        }

        #endregion

        #endregion

        #region Observation générale

        private bool Verif_ObservationGenerale()
        {
            bool verif = true;

            if (!this.Verif_textBoxObservation())
            {
                verif = false;
            }

            return verif;
        }

        #region Observation

        private bool Verif_textBoxObservation()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxObservation, this._textBlockObservation);
        }

        private void _textBoxObservation_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxObservation();
        }

        #endregion

        #endregion

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;

            this.VerificationSuppressionConneries(false);
            this.editionTerminee = false;
        }

        #endregion

        #region Evenements

        #region checkBoxs

        #region Lundi

        private void _checkBoxRepas_Lundi_Checked(object sender, RoutedEventArgs e)
        {
            ((Releve_Heure_Atelier)this.DataContext).Repas_Lundi = true;
        }

        private void _checkBoxRepas_Lundi_Unchecked(object sender, RoutedEventArgs e)
        {
            ((Releve_Heure_Atelier)this.DataContext).Repas_Lundi = false;
        }

        #endregion

        #region Mardi

        private void _checkBoxRepas_Mardi_Checked(object sender, RoutedEventArgs e)
        {
            ((Releve_Heure_Atelier)this.DataContext).Repas_Mardi = true;
        }

        private void _checkBoxRepas_Mardi_Unchecked(object sender, RoutedEventArgs e)
        {
            ((Releve_Heure_Atelier)this.DataContext).Repas_Mardi = false;
        }

        #endregion

        #region Mercredi

        private void _checkBoxRepas_Mercredi_Checked(object sender, RoutedEventArgs e)
        {
            ((Releve_Heure_Atelier)this.DataContext).Repas_Mercredi = true;
        }

        private void _checkBoxRepas_Mercredi_Unchecked(object sender, RoutedEventArgs e)
        {
            ((Releve_Heure_Atelier)this.DataContext).Repas_Mercredi = false;
        }

        #endregion

        #region Jeudi

        private void _checkBoxRepas_Jeudi_Checked(object sender, RoutedEventArgs e)
        {
            ((Releve_Heure_Atelier)this.DataContext).Repas_Jeudi = true;
        }

        private void _checkBoxRepas_Jeudi_Unchecked(object sender, RoutedEventArgs e)
        {
            ((Releve_Heure_Atelier)this.DataContext).Repas_Jeudi = false;
        }

        #endregion

        #region Vendredi

        private void _checkBoxRepas_Vendredi_Checked(object sender, RoutedEventArgs e)
        {
            ((Releve_Heure_Atelier)this.DataContext).Repas_Vendredi = true;
        }

        private void _checkBoxRepas_Vendredi_Unchecked(object sender, RoutedEventArgs e)
        {
            ((Releve_Heure_Atelier)this.DataContext).Repas_Vendredi = false;
        }

        #endregion

        #region Samedi

        private void _checkBoxRepas_Samedi_Checked(object sender, RoutedEventArgs e)
        {
            ((Releve_Heure_Atelier)this.DataContext).Repas_Samedi = true;
        }

        private void _checkBoxRepas_Samedi_Unchecked(object sender, RoutedEventArgs e)
        {
            ((Releve_Heure_Atelier)this.DataContext).Repas_Samedi = false;
        }

        #endregion

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region ComboBox

        private void lesAffaires_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((ComboBox)sender).SelectedItem != null)
                {
                    ((Heure_Atelier)((ComboBox)sender).DataContext).Affaire1 = (Affaire)((ComboBox)sender).SelectedItem;
                }
                else
                {
                    ((Heure_Atelier)((ComboBox)sender).DataContext).Affaire1 = null;
                }
            }
            catch (Exception)
            {
            }
        }

        private void lesRegies_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((ComboBox)sender).SelectedItem != null)
                {
                    ((Heure_Atelier)((ComboBox)sender).DataContext).Regie1 = (Regie)((ComboBox)sender).SelectedItem;
                }
                else
                {
                    ((Heure_Atelier)((ComboBox)sender).DataContext).Regie1 = null;
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region KeyUp

        private void _textBox_KeyUp(object sender, KeyEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
        }

        private void _DataGridHeure_KeyUp_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Lundi":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Mardi":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Mercredi":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Jeudi":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Vendredi":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Samedi":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Dimanche":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void _DataGridHeure_Autre_KeyUp_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Lundi":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Mardi":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Mercredi":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Jeudi":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Vendredi":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Samedi":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Dimanche":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #endregion

        #region Fonctions

        #region Totaux

        private void VerificationSuppressionConneries(bool Remove)
        {
            double total_lundi = 0;
            double total_mardi = 0;
            double total_mercredi = 0;
            double total_jeudi = 0;
            double total_vendredi = 0;
            double total_samedi = 0;
            double total_complet = 0;

            ObservableCollection<Heure_Atelier> toRemoveHeure_Atelier = new ObservableCollection<Heure_Atelier>();

            foreach (Heure_Atelier ha in this._DataGridHeure.Items.OfType<Heure_Atelier>())
            {
                if (ha.Affaire1 == null || ha.Designation == null || ha.Designation == "")
                {
                    toRemoveHeure_Atelier.Add(ha);
                }
                else
                {
                    total_complet += ha.Heures_Lundi + ha.Heures_Mardi + ha.Heures_Mercredi + ha.Heures_Jeudi + ha.Heures_Vendredi + ha.Heures_Samedi;
                    ha.Total = ha.Heures_Lundi + ha.Heures_Mardi + ha.Heures_Mercredi + ha.Heures_Jeudi + ha.Heures_Vendredi + ha.Heures_Samedi;
                    total_lundi += ha.Heures_Lundi;
                    total_mardi += ha.Heures_Mardi;
                    total_mercredi += ha.Heures_Mercredi;
                    total_jeudi += ha.Heures_Jeudi;
                    total_vendredi += ha.Heures_Vendredi;
                    total_samedi += ha.Heures_Samedi;
                }
                if (ha.Affaire1 != null && ha.Regie1 != null)
                {
                    if (!ha.Affaire1.Regie.Contains<Regie>(ha.Regie1))
                    {
                        ha.Regie1 = null;
                    }
                }
            }

            if (editionTerminee)
            {
                int count = 0;
                foreach (Heure_Atelier ha in toRemoveHeure_Atelier)
                {
                    //TODO
                    if (Remove)
                    {
                        ((Releve_Heure_Atelier)this.DataContext).Heure_Atelier.Remove(ha);
                        //this._DataGridHeure.Items.Remove(ha);
                        count++;
                    }
                }

                if (count != 0)
                {
                    MessageBox.Show("Travail sur affaire supprimé car vous n'avez pas associé d'affaire ou mis de désignation, désolé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            ObservableCollection<Heure_Atelier_Autre> toRemoveHeure_Atelier_Autre = new ObservableCollection<Heure_Atelier_Autre>();

            foreach (Heure_Atelier_Autre ha in this._DataGridHeure_Autre.Items.OfType<Heure_Atelier_Autre>())
            {
                if (ha.Tache_Atelier1 == null)
                {
                    toRemoveHeure_Atelier_Autre.Add(ha);
                }
                else
                {
                    total_complet += ha.Heures_Lundi + ha.Heures_Mardi + ha.Heures_Mercredi + ha.Heures_Jeudi + ha.Heures_Vendredi + ha.Heures_Samedi;
                    ha.Total = ha.Heures_Lundi + ha.Heures_Mardi + ha.Heures_Mercredi + ha.Heures_Jeudi + ha.Heures_Vendredi + ha.Heures_Samedi;
                    total_lundi += ha.Heures_Lundi;
                    total_mardi += ha.Heures_Mardi;
                    total_mercredi += ha.Heures_Mercredi;
                    total_jeudi += ha.Heures_Jeudi;
                    total_vendredi += ha.Heures_Vendredi;
                    total_samedi += ha.Heures_Samedi;
                }
            }
            if (editionTerminee)
            {
                int count2 = 0;
                foreach (Heure_Atelier_Autre ha in toRemoveHeure_Atelier_Autre)
                {
                    ((Releve_Heure_Atelier)this.DataContext).Heure_Atelier_Autre.Remove(ha);
                    //this._DataGridHeure_Autre.Items.Remove(ha);
                    count2++;
                }

                if (count2 != 0)
                {
                    MessageBox.Show("Travail sur tâche atelier supprimé car vous n'avez pas associé de tâche d'atelier, désolé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            ((Releve_Heure_Atelier)this.DataContext).Total_Lundi = total_lundi;
            this._textBoxTotal_Lundi.Text = total_lundi.ToString();
            ((Releve_Heure_Atelier)this.DataContext).Total_Mardi = total_mardi;
            this._textBoxTotal_Mardi.Text = total_mardi.ToString();
            ((Releve_Heure_Atelier)this.DataContext).Total_Mercredi = total_mercredi;
            this._textBoxTotal_Mercredi.Text = total_mercredi.ToString();
            ((Releve_Heure_Atelier)this.DataContext).Total_Jeudi = total_jeudi;
            this._textBoxTotal_Jeudi.Text = total_jeudi.ToString();
            ((Releve_Heure_Atelier)this.DataContext).Total_Vendredi = total_vendredi;
            this._textBoxTotal_Vendredi.Text = total_vendredi.ToString();
            ((Releve_Heure_Atelier)this.DataContext).Total_Samedi = total_samedi;
            this._textBoxTotal_Samedi.Text = total_samedi.ToString();
            this._textBoxTotal_Semaine.Text = total_complet.ToString();

            //this._DataGridHeure.Items.Refresh();
            //this._DataGridHeure_Autre.Items.Refresh();
        }

        private void CalculTotaux()
        {
            double total_lundi = 0;
            double total_mardi = 0;
            double total_mercredi = 0;
            double total_jeudi = 0;
            double total_vendredi = 0;
            double total_samedi = 0;
            double total_complet = 0;

            foreach (Heure_Atelier ha in this._DataGridHeure.Items.OfType<Heure_Atelier>())
            {
                if (ha.Affaire1 == null || ha.Designation == null || ha.Designation == "")
                {

                }
                else
                {
                    total_complet += ha.Heures_Lundi + ha.Heures_Mardi + ha.Heures_Mercredi + ha.Heures_Jeudi + ha.Heures_Vendredi + ha.Heures_Samedi;
                    ha.Total = ha.Heures_Lundi + ha.Heures_Mardi + ha.Heures_Mercredi + ha.Heures_Jeudi + ha.Heures_Vendredi + ha.Heures_Samedi;
                    total_lundi += ha.Heures_Lundi;
                    total_mardi += ha.Heures_Mardi;
                    total_mercredi += ha.Heures_Mercredi;
                    total_jeudi += ha.Heures_Jeudi;
                    total_vendredi += ha.Heures_Vendredi;
                    total_samedi += ha.Heures_Samedi;
                }
            }

            foreach (Heure_Atelier_Autre ha in this._DataGridHeure_Autre.Items.OfType<Heure_Atelier_Autre>())
            {
                if (ha.Tache_Atelier1 == null)
                {

                }
                else
                {
                    total_complet += ha.Heures_Lundi + ha.Heures_Mardi + ha.Heures_Mercredi + ha.Heures_Jeudi + ha.Heures_Vendredi + ha.Heures_Samedi;
                    ha.Total = ha.Heures_Lundi + ha.Heures_Mardi + ha.Heures_Mercredi + ha.Heures_Jeudi + ha.Heures_Vendredi + ha.Heures_Samedi;
                    total_lundi += ha.Heures_Lundi;
                    total_mardi += ha.Heures_Mardi;
                    total_mercredi += ha.Heures_Mercredi;
                    total_jeudi += ha.Heures_Jeudi;
                    total_vendredi += ha.Heures_Vendredi;
                    total_samedi += ha.Heures_Samedi;
                }
            }

            ((Releve_Heure_Atelier)this.DataContext).Total_Lundi = total_lundi;
            this._textBoxTotal_Lundi.Text = total_lundi.ToString();
            ((Releve_Heure_Atelier)this.DataContext).Total_Mardi = total_mardi;
            this._textBoxTotal_Mardi.Text = total_mardi.ToString();
            ((Releve_Heure_Atelier)this.DataContext).Total_Mercredi = total_mercredi;
            this._textBoxTotal_Mercredi.Text = total_mercredi.ToString();
            ((Releve_Heure_Atelier)this.DataContext).Total_Jeudi = total_jeudi;
            this._textBoxTotal_Jeudi.Text = total_jeudi.ToString();
            ((Releve_Heure_Atelier)this.DataContext).Total_Vendredi = total_vendredi;
            this._textBoxTotal_Vendredi.Text = total_vendredi.ToString();
            ((Releve_Heure_Atelier)this.DataContext).Total_Samedi = total_samedi;
            this._textBoxTotal_Samedi.Text = total_samedi.ToString();
            this._textBoxTotal_Semaine.Text = total_complet.ToString();
        }

        public void assuranceChiffres()
        {
            ((Releve_Heure_Atelier)this.DataContext).Route_Lundi = double.Parse(this._textBoxRoute_Lundi.Text);
            ((Releve_Heure_Atelier)this.DataContext).Total_Lundi = double.Parse(this._textBoxTotal_Lundi.Text);
            ((Releve_Heure_Atelier)this.DataContext).Route_Mardi = double.Parse(this._textBoxRoute_Mardi.Text);
            ((Releve_Heure_Atelier)this.DataContext).Total_Mardi = double.Parse(this._textBoxTotal_Mardi.Text);
            ((Releve_Heure_Atelier)this.DataContext).Route_Mercredi = double.Parse(this._textBoxRoute_Mercredi.Text);
            ((Releve_Heure_Atelier)this.DataContext).Total_Mercredi = double.Parse(this._textBoxTotal_Mercredi.Text);
            ((Releve_Heure_Atelier)this.DataContext).Route_Jeudi = double.Parse(this._textBoxRoute_Jeudi.Text);
            ((Releve_Heure_Atelier)this.DataContext).Total_Jeudi = double.Parse(this._textBoxTotal_Jeudi.Text);
            ((Releve_Heure_Atelier)this.DataContext).Route_Vendredi = double.Parse(this._textBoxRoute_Vendredi.Text);
            ((Releve_Heure_Atelier)this.DataContext).Total_Vendredi = double.Parse(this._textBoxTotal_Vendredi.Text);
            ((Releve_Heure_Atelier)this.DataContext).Route_Samedi = double.Parse(this._textBoxRoute_Samedi.Text);
            ((Releve_Heure_Atelier)this.DataContext).Total_Samedi = double.Parse(this._textBoxTotal_Samedi.Text);
        }

        private void _DataGridHeure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.VerificationSuppressionConneries(false);
            this.editionTerminee = false;
        }

        private void _DataGridHeure_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            this.VerificationSuppressionConneries(false);
            this.editionTerminee = true;
        }

        #endregion

        #region delete Lignes

        private void deleteLigneHeureAtelier()
        {
            if (this._DataGridHeure_Autre.SelectedItems.Count != 0)
            {
                ObservableCollection<Heure_Atelier> toRemove = new ObservableCollection<Heure_Atelier>();
                foreach (Heure_Atelier item in this._DataGridHeure.SelectedItems.OfType<Heure_Atelier>())
                {
                    toRemove.Add(item);
                }
                foreach (Heure_Atelier item in toRemove)
                {
                    try
                    {
                        item.Heures_Jeudi = 0;
                        item.Heures_Lundi = 0;
                        item.Heures_Mardi = 0;
                        item.Heures_Mercredi = 0;
                        item.Heures_Samedi = 0;
                        item.Heures_Vendredi = 0;
                    }
                    catch (Exception) { }
                    try
                    {
                        ((Releve_Heure_Atelier)this.DataContext).Heure_Atelier.Remove(item);
                        ((App)App.Current).mySitaffEntities.Heure_Atelier.DeleteObject(item);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            ((Releve_Heure_Atelier)this.DataContext).Heure_Atelier.Remove(item);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                this._DataGridHeure.Items.Remove(item);
                            }
                            catch (Exception) { }
                        }
                    }
                }
            }
        }

        private void deleteLigneHeureAtelierAutre()
        {
            if (this._DataGridHeure_Autre.SelectedItems.Count != 0)
            {
                ObservableCollection<Heure_Atelier_Autre> toRemove = new ObservableCollection<Heure_Atelier_Autre>();
                foreach (Heure_Atelier_Autre item in this._DataGridHeure_Autre.SelectedItems.OfType<Heure_Atelier_Autre>())
                {
                    toRemove.Add(item);
                }
                foreach (Heure_Atelier_Autre item in toRemove)
                {
                    try
                    {
                        item.Heures_Jeudi = 0;
                        item.Heures_Lundi = 0;
                        item.Heures_Mardi = 0;
                        item.Heures_Mercredi = 0;
                        item.Heures_Samedi = 0;
                        item.Heures_Vendredi = 0;
                    }
                    catch (Exception) { }
                    try
                    {
                        ((Releve_Heure_Atelier)this.DataContext).Heure_Atelier_Autre.Remove(item);
                        ((App)App.Current).mySitaffEntities.Heure_Atelier_Autre.DeleteObject(item);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            ((Releve_Heure_Atelier)this.DataContext).Heure_Atelier_Autre.Remove(item);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                this._DataGridHeure_Autre.Items.Remove(item);
                            }
                            catch (Exception) { }
                        }
                    }
                }
            }
        }

        #endregion

        private bool verifExistance()
        {
            bool verif = true;

            foreach (Releve_Heure_Atelier rha in ((App)App.Current).mySitaffEntities.Releve_Heure_Atelier)
            {
                if (rha.Identifiant != ((Releve_Heure_Atelier)this.DataContext).Identifiant)
                {
                    DateTime testt;
                    if (DateTime.TryParse(rha.Date_Debut.ToString(), out testt) && DateTime.TryParse(this._datePickerDate_Debut.SelectedDate.ToString(), out testt))
                    {
                        if (rha.NumeroSemaine == ((Releve_Heure_Atelier)this.DataContext).NumeroSemaine && rha.Salarie1 == ((Releve_Heure_Atelier)this.DataContext).Salarie1 && DateTime.Parse(rha.Date_Debut.ToString()).Year == DateTime.Parse(this._datePickerDate_Debut.SelectedDate.ToString()).Year)
                        {
                            verif = false;
                        }
                    }
                    else if (rha.NumeroSemaine == ((Releve_Heure_Atelier)this.DataContext).NumeroSemaine && rha.Salarie1 == ((Releve_Heure_Atelier)this.DataContext).Salarie1)
                    {
                        verif = false;
                    }
                }
            }

            return verif;
        }

        #endregion

        #region Lecture seule

        public void VerrouillerLaFenetre()
        {
            this._ButtonOk.IsEnabled = false;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            this._textBlockSalarie.Foreground = Brushes.Red;
            this._comboBoxSalarie.Background = rouge;

            this._textBlockNumero_Semaine.Foreground = Brushes.Red;
            this._textBoxNumerosemaine.Background = rouge;

            this._textBlockDate_Debut.Foreground = Brushes.Red;
            this._datePickerDate_Debut.Background = rouge;
        }

        private void DeVerrouillerLaFenetre()
        {
            this._ButtonOk.IsEnabled = true;
        }

        public void lectureSeule()
        {
            //ComboBox
            this._comboBoxSalarie.IsEnabled = false;

            //DataPicker
            this._datePickerDate_Debut.IsEnabled = false;

            //TextBox Observation
            this._textBoxObservation.IsReadOnly = true;
            this._textBoxObservation_Lundi.IsReadOnly = true;
            this._textBoxObservation_Mardi.IsReadOnly = true;
            this._textBoxObservation_Mercredi.IsReadOnly = true;
            this._textBoxObservation_Jeudi.IsReadOnly = true;
            this._textBoxObservation_Vendredi.IsReadOnly = true;
            this._textBoxObservation_Samedi.IsReadOnly = true;

            //CheckBox
            this._checkBoxRepas_Lundi.IsEnabled = false;
            this._checkBoxRepas_Mardi.IsEnabled = false;
            this._checkBoxRepas_Mercredi.IsEnabled = false;
            this._checkBoxRepas_Jeudi.IsEnabled = false;
            this._checkBoxRepas_Vendredi.IsEnabled = false;
            this._checkBoxRepas_Samedi.IsEnabled = false;

            //TextBox
            this._textBoxRoute_Lundi.IsReadOnly = true;
            this._textBoxRoute_Mardi.IsReadOnly = true;
            this._textBoxRoute_Mercredi.IsReadOnly = true;
            this._textBoxRoute_Jeudi.IsReadOnly = true;
            this._textBoxRoute_Vendredi.IsReadOnly = true;
            this._textBoxRoute_Samedi.IsReadOnly = true;

            //DataGrid
            this._DataGridHeure.IsReadOnly = true;
            this._DataGridHeure_Autre.IsReadOnly = true;

            //ContextMenu
            this._DataGridHeure.ContextMenu = null;
            this._DataGridHeure_Autre.ContextMenu = null;
        }

        #endregion

        #region clic droit

        #region Contenu Commande

        private void creationMenuClicDroit()
        {
            ContextMenu contextMenu = new ContextMenu();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorToPut = "#A3D0D8E8";
            Brush colorMenu = (Brush)converter.ConvertFrom(colorToPut);
            contextMenu.Background = colorMenu;
            this._DataGridHeure.ContextMenu = contextMenu;

            MenuItem itemAfficher = new MenuItem();
            itemAfficher.Header = "Supprimer";
            itemAfficher.Click += new RoutedEventHandler(delegate { this.menuDeleteHA(); });

            contextMenu.Items.Add(itemAfficher);

            ContextMenu contextMenu2 = new ContextMenu();
            contextMenu.Background = colorMenu;
            this._DataGridHeure_Autre.ContextMenu = contextMenu2;

            MenuItem itemAfficher2 = new MenuItem();
            itemAfficher2.Header = "Supprimer";
            itemAfficher2.Click += new RoutedEventHandler(delegate { this.menuDeleteHAA(); });

            contextMenu2.Items.Add(itemAfficher2);
        }

        private void menuDeleteHA()
        {
            this.deleteLigneHeureAtelier();
        }

        private void menuDeleteHAA()
        {
            this.deleteLigneHeureAtelier();
        }

        #endregion

        private void _TextBoxDescription_LostFocus_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ((Contenu_Commande_Fournisseur)((TextBox)sender).DataContext).Description = ((TextBox)sender).Text;
            }
            catch (Exception) { }
        }

        #endregion

    }
}
