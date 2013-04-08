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
using System.Globalization;
using SitaffRibbon.Classes;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour ReleveHeureForfaitWindow.xaml
    /// </summary>
    public partial class ReleveHeureForfaitWindow : Window
    {

        #region Attributs

        public bool creation = false;
        public bool editionTerminee = false;
        private ObservableCollection<Heure_Forfait> heure_forfait_copy = new ObservableCollection<Heure_Forfait>();
        private ObservableCollection<Heure_Regie> heure_regie_copy = new ObservableCollection<Heure_Regie>();

        #endregion

        #region Propriétés de dépendances



        public ObservableCollection<Salarie> listCharge
        {
            get { return (ObservableCollection<Salarie>)GetValue(listChargeProperty); }
            set { SetValue(listChargeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listCharge.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listChargeProperty =
            DependencyProperty.Register("listCharge", typeof(ObservableCollection<Salarie>), typeof(ReleveHeureForfaitWindow), new PropertyMetadata(null));

        

        public ObservableCollection<Regie> listRegie
        {
            get { return (ObservableCollection<Regie>)GetValue(listRegieProperty); }
            set { SetValue(listRegieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listRegie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listRegieProperty =
            DependencyProperty.Register("listRegie", typeof(ObservableCollection<Regie>), typeof(ReleveHeureForfaitWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Bouteille_Gaz> listBouteillesGaz
        {
            get { return (ObservableCollection<Bouteille_Gaz>)GetValue(listBouteillesGazProperty); }
            set { SetValue(listBouteillesGazProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listBouteillesGaz.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listBouteillesGazProperty =
            DependencyProperty.Register("listBouteillesGaz", typeof(ObservableCollection<Bouteille_Gaz>), typeof(ReleveHeureForfaitWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Client> listClient
        {
            get { return (ObservableCollection<Client>)GetValue(listClientProperty); }
            set { SetValue(listClientProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listClient.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listClientProperty =
            DependencyProperty.Register("listClient", typeof(ObservableCollection<Client>), typeof(ReleveHeureForfaitWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(ReleveHeureForfaitWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(ReleveHeureForfaitWindow), new UIPropertyMetadata(null));



        #endregion

        #region Constructeur

        public ReleveHeureForfaitWindow()
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
            this.listCharge = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.OrderBy(sal => sal.Personne.Nom).ThenBy(sal => sal.Personne.Prenom));
            this.listBouteillesGaz = new ObservableCollection<Bouteille_Gaz>(((App)App.Current).mySitaffEntities.Bouteille_Gaz.OrderBy(bg => bg.Libelle));
            this.listClient = new ObservableCollection<Client>(((App)App.Current).mySitaffEntities.Client.OrderBy(cli => cli.Entreprise.Libelle));
        }

        private void initialisationSecurite()
        {
            //Mise en place des droits sur les boutons et tabs

            if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeRegieControl", "Add"))
            {
                this._newRegie.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        #endregion

        #region boutons

        #region Boutons ok / annuler

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                String test = this.VerificationMultiplePersonne();
                if (test == "")
                {
                    this.CalculTotaux();
                    this.assuranceChiffres();
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    if (MessageBox.Show("Attention !" + test + " Voulez-vous enregistrer les informations de cette manière avec un risque d'erreur ?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                    {
                        this.CalculTotaux();
                        this.assuranceChiffres();
                        this.DialogResult = true;
                        this.Close();
                    }
                }
            }
        }

        private void _ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        #region boutons régies

        private void _ButtonVersDroiteRegie_Click(object sender, RoutedEventArgs e)
        {
            if (this._ListBoxBonsRegie.SelectedItem != null && this._ListBoxBonsRegie.SelectedItems.Count == 1)
            {
                Travail_Sur_Regie temp = new Travail_Sur_Regie();
                temp.Regie1 = (Regie)this._ListBoxBonsRegie.SelectedItem;
                ((Releve_Heure_Forfait)this.DataContext).Travail_Sur_Regie.Add(temp);
                this.listRegie.Remove((Regie)this._ListBoxBonsRegie.SelectedItem);
            }
        }

        private void _ButtonVersGaucheRegie_Click(object sender, RoutedEventArgs e)
        {
            if (this._DataGridRegie.SelectedItem != null && this._DataGridRegie.SelectedItems.Count == 1)
            {
                this.listRegie.Add((Regie)((Travail_Sur_Regie)this._DataGridRegie.SelectedItem).Regie1);
                ((Releve_Heure_Forfait)this.DataContext).Travail_Sur_Regie.Remove((Travail_Sur_Regie)this._DataGridRegie.SelectedItem);
            }
        }

        //Ajouter une régie
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Initialisation de la fenêtre
            RegieWindow regieWindow = new RegieWindow();

            //Création de l'objet temporaire            
            Regie tmp = new Regie();
            tmp.Termine = false;
            tmp.Signe = false;

            //Mise de l'objet temporaire dans le datacontext
            regieWindow.DataContext = tmp;

            //booléen nullable vrai ou faux ou null
            regieWindow.creation = true;
            try
            {
                regieWindow._comboBoxAffaire.SelectedItem = (Affaire)this._comboBoxAffaire.SelectedItem;
            }
            catch (Exception) { }
            bool? dialogResult = regieWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                this.miseEnPlaceOuverture();
                try
                {
                    if (!this.listRegie.Contains((Regie)regieWindow.DataContext))
                    {
                        this.listRegie.Add((Regie)regieWindow.DataContext);
                    }
                }
                catch (Exception) { }
            }
            else
            {
                try
                {
                    ObservableCollection<Travail> toRemove = new ObservableCollection<Travail>();
                    foreach (Travail item in ((Regie)regieWindow.DataContext).Travail)
                    {
                        toRemove.Add(item);
                    }
                    foreach (Travail item in toRemove)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }

                    ((App)App.Current).mySitaffEntities.Detach((Regie)regieWindow.DataContext);
                }
                catch (Exception)
                {
                }
            }            
        }

        #endregion

        #region boutons bouteilles gaz

        private void _ButtonVersDroiteBouteillesGaz_Click(object sender, RoutedEventArgs e)
        {
            if (this._ListBoxBouteillesGaz.SelectedItem != null && this._ListBoxBouteillesGaz.SelectedItems.Count == 1)
            {
                Releve_Heure_Forfait_Bouteille_Gaz temp = new Releve_Heure_Forfait_Bouteille_Gaz();
                temp.Bouteille_Gaz1 = (Bouteille_Gaz)this._ListBoxBouteillesGaz.SelectedItem;
                ((Releve_Heure_Forfait)this.DataContext).Releve_Heure_Forfait_Bouteille_Gaz.Add(temp);
                this.listBouteillesGaz.Remove((Bouteille_Gaz)this._ListBoxBouteillesGaz.SelectedItem);
            }
        }

        private void _ButtonVersGaucheBouteillesGaz_Click(object sender, RoutedEventArgs e)
        {
            if (this._DataGridBouteillesGaz.SelectedItem != null && this._DataGridBouteillesGaz.SelectedItems.Count == 1)
            {
                this.listBouteillesGaz.Add((Bouteille_Gaz)((Releve_Heure_Forfait_Bouteille_Gaz)this._DataGridBouteillesGaz.SelectedItem).Bouteille_Gaz1);
                ((Releve_Heure_Forfait)this.DataContext).Releve_Heure_Forfait_Bouteille_Gaz.Remove((Releve_Heure_Forfait_Bouteille_Gaz)this._DataGridBouteillesGaz.SelectedItem);
            }
        }

        #endregion

        #endregion

        #region Verifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!this.Verif_ObservationGenerale())
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
            if (!this.Verif_comboBoxAffaire())
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
            if (this.Verif_textBoxNumeroSemaine() && this.Verif_comboBoxSalarie() && this.Verif_comboBoxAffaire())
            {
                if (creation)
                {
                    if (!this.verifExistance())
                    {
                        this.VerrouillerLaFenetre();
                        MessageBox.Show("Un relevé hebdomadaire est déjà présent pour la semaine selectionnée, l'affaire sélectionnée et le salarié sélectionné. Vous ne pourrez pas valider votre relevé. Modifier le relevé voulu ou modifier les informations salarié ou semaine si elles ne sont pas bonnes", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
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

        #region Client

        private void _comboBoxClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this._comboBoxClient.SelectedItem != null)
            {
                if (((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1.Ville1 != null)
                {
                    this._textBoxLieu.Text = ((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1.Ville1.Libelle;
                }
                else
                {
                    this._textBoxLieu.Text = "";
                }
            }
            else
            {
                this._textBoxLieu.Text = "";
            }
        }

        #endregion

        #region Affaire

        private bool Verif_comboBoxAffaire()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxAffaire, this._textBlockAffaire);
        }

        private void _comboBoxAffaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Verif_comboBoxAffaire())
            {
                if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.Count() != 0)
                {
                    try
                    {
                        this._comboBoxClient.SelectedItem = ((Versions)((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First()).Devis1.Client1;
                        this._comboBoxChargeAffaire.SelectedItem = ((Affaire)this._comboBoxAffaire.SelectedItem).Salarie;
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    this._comboBoxClient.SelectedItem = null;
                    this._comboBoxChargeAffaire.SelectedItem = null;
                }
                this.listRegie = new ObservableCollection<Regie>(((App)App.Current).mySitaffEntities.Regie.Where(reg => reg.Affaire1.Identifiant == ((Affaire)this._comboBoxAffaire.SelectedItem).Identifiant).OrderBy(reg => reg.Numero));
                ObservableCollection<Travail_Sur_Regie> toRemoveTSR = new ObservableCollection<Travail_Sur_Regie>();
                foreach (Travail_Sur_Regie tsr in ((Releve_Heure_Forfait)this.DataContext).Travail_Sur_Regie)
                {
                    if (this.listRegie.Where(reg => reg.Identifiant == tsr.Regie1.Identifiant).Count() != 0)
                    {
                        this.listRegie.Remove(tsr.Regie1);
                    }
                    else
                    {
                        toRemoveTSR.Add(tsr);
                    }
                }
                foreach (Travail_Sur_Regie item in toRemoveTSR)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Travail_Sur_Regie.DeleteObject(item);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            ((Releve_Heure_Forfait)this.DataContext).Travail_Sur_Regie.Remove(item);
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                ((Releve_Heure_Forfait)this.DataContext).Travail_Sur_Regie.Remove(item);
                            }
                            catch (Exception) { }
                        }
                    }
                }
            }
            if (this.Verif_textBoxNumeroSemaine() && this.Verif_comboBoxSalarie() && this.Verif_comboBoxAffaire())
            {
                if (creation)
                {
                    if (!this.verifExistance())
                    {
                        this.VerrouillerLaFenetre();
                        MessageBox.Show("Un relevé hebdomadaire est déjà présent pour la semaine selectionnée, l'affaire sélectionnée et le salarié sélectionné. Vous ne pourrez pas valider votre relevé. Modifier le relevé voulu ou modifier les informations salarié ou semaine si elles ne sont pas bonnes", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
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
            bool verif = true;

            if (this._textBoxNumerosemaine.Text.Trim().Length == 0)
            {
                verif = false;
            }
            else
            {
                double val;
                if (double.TryParse(this._textBoxNumerosemaine.Text, out val))
                {
                    verif = true;
                    ((Releve_Heure_Forfait)this.DataContext).NumeroSemaine = (int)val;
                }
                else
                {
                    verif = false;
                }
            }
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._textBoxNumerosemaine, this._textBlockNumero_Semaine, verif);
            return verif;
        }

        private void _textBoxNumerosemaine_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.Verif_textBoxNumeroSemaine() && this.Verif_comboBoxSalarie() && this.Verif_comboBoxAffaire())
            {
                if (creation)
                {
                    if (!this.verifExistance())
                    {
                        this.VerrouillerLaFenetre();
                        MessageBox.Show("Un relevé hebdomadaire est déjà présent pour la semaine selectionnée, l'affaire sélectionnée et le salarié sélectionné. Vous ne pourrez pas valider votre relevé. Modifier le relevé voulu ou modifier les informations salarié ou semaine si elles ne sont pas bonnes", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
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
                while (((DateTime)this._datePickerDate_Fin.SelectedDate).DayOfWeek != DayOfWeek.Sunday)
                {
                    this._datePickerDate_Fin.SelectedDate = ((DateTime)this._datePickerDate_Fin.SelectedDate).AddDays(1);
                }

            }
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
            this.miseEnPlaceOuverture();
            this.CalculTotaux();
        }

        #endregion

        #region Lecture Seule

        public void lectureSeule()
        {
            //comboBox
            this._comboBoxSalarie.IsEnabled = false;
            this._comboBoxAffaire.IsEnabled = false;

            //Datagrid
            this._DataGridBouteillesGaz.IsReadOnly = true;
            this._DataGridHeureForfait.IsReadOnly = true;
            this._DataGridHeureRegie.IsReadOnly = true;
            this._DataGridRegie.IsReadOnly = true;

            //Boutons
            this._ButtonVersDroiteBouteillesGaz.IsEnabled = false;
            this._ButtonVersGaucheBouteillesGaz.IsEnabled = false;
            this._ButtonVersDroiteRegie.IsEnabled = false;
            this._ButtonVersGaucheRegie.IsEnabled = false;
            this._button_collerForfait.IsEnabled = false;
            this._button_copierForfait.IsEnabled = false;
            this._button_copierRegie.IsEnabled = false;
            this._button_collerRegie.IsEnabled = false;
            this._button_supprimerForfait.IsEnabled = false;
            this._button_supprimerRegie.IsEnabled = false;
            this._newRegie.IsEnabled = false;

            //Dates
            this._datePickerDate_Debut.IsEnabled = false;

            //TextBox
            this._textBoxObservation.IsReadOnly = true;

            //contextMenus
            this._DataGridHeureForfait.ContextMenu = null;
            this._DataGridHeureRegie.ContextMenu = null;
        }

        #endregion

        #region Fonctions

        private void miseEnPlaceOuverture()
        {
            if (this._comboBoxAffaire.SelectedItem != null)
            {
                if (((Affaire)this._comboBoxAffaire.SelectedItem).Versions.Count() != 0)
                {
                    try
                    {
                        this._comboBoxClient.SelectedItem = ((Versions)((Affaire)this._comboBoxAffaire.SelectedItem).Versions.First()).Devis1.Client1;
                        this._comboBoxChargeAffaire.SelectedItem = ((Affaire)this._comboBoxAffaire.SelectedItem).Salarie;
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    this._comboBoxClient.SelectedItem = null;
                    this._comboBoxChargeAffaire.SelectedItem = null;
                }
                this.listRegie = new ObservableCollection<Regie>(((App)App.Current).mySitaffEntities.Regie.Where(reg => reg.Affaire1.Identifiant == ((Affaire)this._comboBoxAffaire.SelectedItem).Identifiant).OrderBy(reg => reg.Numero));
                foreach (Travail_Sur_Regie tsr in ((Releve_Heure_Forfait)this.DataContext).Travail_Sur_Regie)
                {
                    if (this.listRegie.Where(reg => reg.Identifiant == tsr.Regie1.Identifiant).Count() != 0)
                    {
                        this.listRegie.Remove(tsr.Regie1);
                    }
                    else
                    {
                        ((Releve_Heure_Forfait)this.DataContext).Travail_Sur_Regie.Remove(tsr);
                    }
                }
            }
        }

        private bool verifExistance()
        {
            bool verif = true;

            foreach (Releve_Heure_Forfait rha in ((App)App.Current).mySitaffEntities.Releve_Heure_Forfait)
            {
                if (rha.Identifiant != ((Releve_Heure_Forfait)this.DataContext).Identifiant)
                {
                    DateTime testt;
                    if (DateTime.TryParse(rha.Date_Debut.ToString(), out testt) && DateTime.TryParse(rha.Date_Debut.ToString(), out testt))
                    {
                        if (rha.NumeroSemaine == ((Releve_Heure_Forfait)this.DataContext).NumeroSemaine && DateTime.Parse(rha.Date_Debut.ToString()).Year == DateTime.Parse(((Releve_Heure_Forfait)this.DataContext).Date_Debut.ToString()).Year && rha.Salarie1 == ((Releve_Heure_Forfait)this.DataContext).Salarie1 && rha.Affaire1 == ((Releve_Heure_Forfait)this.DataContext).Affaire1)
                        {
                            verif = false;
                        }
                    }
                    else
                    {
                        if (rha.NumeroSemaine == ((Releve_Heure_Forfait)this.DataContext).NumeroSemaine && rha.Salarie1 == ((Releve_Heure_Forfait)this.DataContext).Salarie1 && rha.Affaire1 == ((Releve_Heure_Forfait)this.DataContext).Affaire1)
                        {
                            verif = false;
                        }
                    }
                }
            }

            return verif;
        }

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

            this._textBlockAffaire.Foreground = Brushes.Red;
            this._comboBoxAffaire.Background = rouge;
        }

        private void DeVerrouillerLaFenetre()
        {
            this._ButtonOk.IsEnabled = true;
        }

        public void assuranceChiffres()
        {

        }

        private void VerificationSuppressionConneries()
        {
            double total_regie = 0;
            double total_forfait = 0;

            ObservableCollection<Heure_Regie> toRemoveHeure_Regie = new ObservableCollection<Heure_Regie>();

            foreach (Heure_Regie hr in this._DataGridHeureRegie.Items.OfType<Heure_Regie>())
            {
                if (hr.Salarie1 == null)
                {
                    toRemoveHeure_Regie.Add(hr);
                }
                else
                {
                    total_regie += hr.Heures_Lundi_Jour + hr.Heures_Mardi_Jour + hr.Heures_Mercredi_Jour + hr.Heures_Jeudi_Jour + hr.Heures_Vendredi_Jour + hr.Heures_Samedi_Jour + hr.Heures_Dimanche_Jour + hr.Heures_Lundi_Nuit + hr.Heures_Mardi_Nuit + hr.Heures_Mercredi_Nuit + hr.Heures_Jeudi_Nuit + hr.Heures_Vendredi_Nuit + hr.Heures_Samedi_Nuit + hr.Heures_Dimanche_Nuit;
                }
            }

            if (editionTerminee)
            {
                int count = 0;
                foreach (Heure_Regie hr in toRemoveHeure_Regie)
                {
                    ((Releve_Heure_Forfait)this.DataContext).Heure_Regie.Remove(hr);
                    count++;
                }

                if (count != 0)
                {
                    MessageBox.Show("Travail sur régie supprimé car vous n'avez pas associé de salarié, désolé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            ObservableCollection<Heure_Forfait> toRemoveHeure_Forfait = new ObservableCollection<Heure_Forfait>();

            foreach (Heure_Forfait hr in this._DataGridHeureForfait.Items.OfType<Heure_Forfait>())
            {
                if (hr.Salarie1 == null)
                {
                    toRemoveHeure_Forfait.Add(hr);
                }
                else
                {
                    total_forfait += hr.Heures_Lundi_Jour + hr.Heures_Mardi_Jour + hr.Heures_Mercredi_Jour + hr.Heures_Jeudi_Jour + hr.Heures_Vendredi_Jour + hr.Heures_Samedi_Jour + hr.Heures_Dimanche_Jour + hr.Heures_Lundi_Nuit + hr.Heures_Mardi_Nuit + hr.Heures_Mercredi_Nuit + hr.Heures_Jeudi_Nuit + hr.Heures_Vendredi_Nuit + hr.Heures_Samedi_Nuit + hr.Heures_Dimanche_Nuit;
                }
            }

            if (editionTerminee)
            {
                int count2 = 0;
                foreach (Heure_Forfait ha in toRemoveHeure_Forfait)
                {
                    ((Releve_Heure_Forfait)this.DataContext).Heure_Forfait.Remove(ha);
                    count2++;
                }

                if (count2 != 0)
                {
                    MessageBox.Show("Forfait supprimé car vous n'avez pas associé de salarié, désolé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            ((Releve_Heure_Forfait)this.DataContext).Total_Regie = total_regie;
            this._textBoxTotal_Regie.Text = total_regie.ToString();
            ((Releve_Heure_Forfait)this.DataContext).Total_Forfait = total_forfait;
            this._textBoxTotal_Forfait.Text = total_forfait.ToString();
        }

        private void CalculTotaux()
        {
            double total_regie = 0;
            double total_forfait = 0;

            foreach (Heure_Regie hr in this._DataGridHeureRegie.Items.OfType<Heure_Regie>())
            {
                if (hr.Salarie1 == null)
                {

                }
                else
                {
                    total_regie += hr.Heures_Lundi_Jour + hr.Heures_Mardi_Jour + hr.Heures_Mercredi_Jour + hr.Heures_Jeudi_Jour + hr.Heures_Vendredi_Jour + hr.Heures_Samedi_Jour + hr.Heures_Dimanche_Jour + hr.Heures_Lundi_Nuit + hr.Heures_Mardi_Nuit + hr.Heures_Mercredi_Nuit + hr.Heures_Jeudi_Nuit + hr.Heures_Vendredi_Nuit + hr.Heures_Samedi_Nuit + hr.Heures_Dimanche_Nuit;
                }
            }

            foreach (Heure_Forfait hr in this._DataGridHeureForfait.Items.OfType<Heure_Forfait>())
            {
                if (hr.Salarie1 == null)
                {

                }
                else
                {
                    total_forfait += hr.Heures_Lundi_Jour + hr.Heures_Mardi_Jour + hr.Heures_Mercredi_Jour + hr.Heures_Jeudi_Jour + hr.Heures_Vendredi_Jour + hr.Heures_Samedi_Jour + hr.Heures_Dimanche_Jour + hr.Heures_Lundi_Nuit + hr.Heures_Mardi_Nuit + hr.Heures_Mercredi_Nuit + hr.Heures_Jeudi_Nuit + hr.Heures_Vendredi_Nuit + hr.Heures_Samedi_Nuit + hr.Heures_Dimanche_Nuit;
                }
            }

            ((Releve_Heure_Forfait)this.DataContext).Total_Regie = total_regie;
            this._textBoxTotal_Regie.Text = total_regie.ToString();
            ((Releve_Heure_Forfait)this.DataContext).Total_Forfait = total_forfait;
            this._textBoxTotal_Forfait.Text = total_forfait.ToString();
        }

        private String VerificationMultiplePersonne()
        {
            String result = "";

            bool testRegie = false;
            bool testForfait = false;

            foreach (Heure_Regie hr in this._DataGridHeureRegie.Items.OfType<Heure_Regie>())
            {
                if (!testRegie)
                {
                    if (this._DataGridHeureRegie.Items.OfType<Heure_Regie>().Where(heure => heure.Salarie1.Identifiant == hr.Salarie1.Identifiant).Count() > 1)
                    {
                        result += " Des salariés sont présents plusieurs fois dans les heures de régie.";
                        testRegie = true;
                    }
                }
            }

            foreach (Heure_Forfait hr in this._DataGridHeureForfait.Items.OfType<Heure_Forfait>())
            {
                if (!testForfait)
                {
                    if (this._DataGridHeureForfait.Items.OfType<Heure_Forfait>().Where(heure => heure.Salarie1.Identifiant == hr.Salarie1.Identifiant).Count() > 1)
                    {
                        result += " Des salariés sont présents plusieurs fois dans les heures de forfait.";
                        testForfait = true;
                    }
                }
            }

            return result;
        }

        #endregion

        #region Evenements

        #region Datagrids

        private void _DataGridHeureRegie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.VerificationSuppressionConneries();
            this.editionTerminee = false;
        }

        private void _DataGridHeureRegie_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            this.VerificationSuppressionConneries();
            this.editionTerminee = true;
        }

        private void _DataGridHeureForfait_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.VerificationSuppressionConneries();
            this.editionTerminee = false;
        }

        private void _DataGridHeureForfait_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            this.VerificationSuppressionConneries();
            this.editionTerminee = true;
        }

        #endregion

        #region KeyUp

        private void _DataGridHeureRegie_KeyUp_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Lundi J":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Mardi J":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Mercredi J":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Jeudi J":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Vendredi J":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Samedi J":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Dimanche J":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Lundi N":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Mardi N":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Mercredi N":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Jeudi N":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Vendredi N":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Samedi N":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Dimanche N":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void _DataGridBouteillesGaz_KeyUp_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Quantité":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void _DataGridRegie_KeyUp_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Quantité":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void _DataGridHeureForfait_KeyUp_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Lundi J":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Mardi J":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Mercredi J":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Jeudi J":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Vendredi J":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Samedi J":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Dimanche J":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Lundi N":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Mardi N":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Mercredi N":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Jeudi N":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Vendredi N":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Samedi N":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Dimanche N":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        default:
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

        #region Copier / Coller / Supprimer

        #region Heures forfait

        private void deleteLigne()
        {
            if (this._DataGridHeureForfait.SelectedItems.Count != 0)
            {
                ObservableCollection<Heure_Forfait> toRemove = new ObservableCollection<Heure_Forfait>();
                foreach (Heure_Forfait item in this._DataGridHeureForfait.SelectedItems.OfType<Heure_Forfait>())
                {
                    toRemove.Add(item);
                }
                foreach (Heure_Forfait item in toRemove)
                {
                    try
                    {
                        item.Heures_Dimanche_Jour = 0;
                        item.Heures_Jeudi_Jour = 0;
                        item.Heures_Lundi_Jour = 0;
                        item.Heures_Mardi_Jour = 0;
                        item.Heures_Mercredi_Jour = 0;
                        item.Heures_Samedi_Jour = 0;
                        item.Heures_Vendredi_Jour = 0;
                        item.Heures_Dimanche_Nuit = 0;
                        item.Heures_Jeudi_Nuit = 0;
                        item.Heures_Lundi_Nuit = 0;
                        item.Heures_Mardi_Nuit = 0;
                        item.Heures_Mercredi_Nuit = 0;
                        item.Heures_Samedi_Nuit = 0;
                        item.Heures_Vendredi_Nuit = 0;
                    }
                    catch (Exception) { }
                    try
                    {
                        item.Releve_Heure_Forfait1 = null;
                        try
                        {
                            ((Releve_Heure_Forfait)this.DataContext).Heure_Forfait.Remove(item);
                        }
                        catch (Exception) { }
                        ((App)App.Current).mySitaffEntities.Heure_Forfait.DeleteObject(item);
                    }
                    catch (Exception)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
            }
            this._DataGridHeureForfait.Items.Refresh();
        }

        private void copier()
        {
            this.heure_forfait_copy = new ObservableCollection<Heure_Forfait>();
            foreach (Heure_Forfait hf in this._DataGridHeureForfait.SelectedItems.OfType<Heure_Forfait>())
            {
                Heure_Forfait temp = new Heure_Forfait();
                temp.Salarie1 = hf.Salarie1;
                temp.Heures_Lundi_Jour = hf.Heures_Lundi_Jour;
                temp.Heures_Mardi_Jour = hf.Heures_Mardi_Jour;
                temp.Heures_Mercredi_Jour = hf.Heures_Mercredi_Jour;
                temp.Heures_Jeudi_Jour = hf.Heures_Jeudi_Jour;
                temp.Heures_Vendredi_Jour = hf.Heures_Vendredi_Jour;
                temp.Heures_Samedi_Jour = hf.Heures_Samedi_Jour;
                temp.Heures_Dimanche_Jour = hf.Heures_Dimanche_Jour;
                temp.Heures_Lundi_Nuit = hf.Heures_Lundi_Nuit;
                temp.Heures_Mardi_Nuit = hf.Heures_Mardi_Nuit;
                temp.Heures_Mercredi_Nuit = hf.Heures_Mercredi_Nuit;
                temp.Heures_Jeudi_Nuit = hf.Heures_Jeudi_Nuit;
                temp.Heures_Vendredi_Nuit = hf.Heures_Vendredi_Nuit;
                temp.Heures_Samedi_Nuit = hf.Heures_Samedi_Nuit;
                temp.Heures_Dimanche_Nuit = hf.Heures_Dimanche_Nuit;
                temp.Vehicule_Lundi = hf.Vehicule_Lundi;
                temp.Vehicule_Mardi = hf.Vehicule_Mardi;
                temp.Vehicule_Mercredi = hf.Vehicule_Mercredi;
                temp.Vehicule_Jeudi = hf.Vehicule_Jeudi;
                temp.Vehicule_Vendredi = hf.Vehicule_Vendredi;
                temp.Vehicule_Samedi = hf.Vehicule_Samedi;
                temp.Vehicule_Dimanche = hf.Vehicule_Dimanche;
                this.heure_forfait_copy.Add(temp);
            }
        }

        private void coller()
        {
            foreach (Heure_Forfait hf in this.heure_forfait_copy)
            {
                ((Releve_Heure_Forfait)this.DataContext).Heure_Forfait.Add(hf);
            }
            this._DataGridHeureForfait.Items.Refresh();
        }

        private void _button_copierForfait_Click(object sender, RoutedEventArgs e)
        {
            this.copier();
        }

        private void _button_collerForfait_Click(object sender, RoutedEventArgs e)
        {
            this.coller();
        }

        private void _button_supprimerForfait_Click_1(object sender, RoutedEventArgs e)
        {
            this.deleteLigne();
        }

        #endregion

        #region Heures régies

        private void deleteLigneRegie()
        {
            if (this._DataGridHeureRegie.SelectedItems.Count != 0)
            {
                ObservableCollection<Heure_Regie> toRemove = new ObservableCollection<Heure_Regie>();
                foreach (Heure_Regie item in this._DataGridHeureRegie.SelectedItems.OfType<Heure_Regie>())
                {
                    toRemove.Add(item);
                }
                foreach (Heure_Regie item in toRemove)
                {
                    try
                    {
                        item.Heures_Dimanche_Jour = 0;
                        item.Heures_Jeudi_Jour = 0;
                        item.Heures_Lundi_Jour = 0;
                        item.Heures_Mardi_Jour = 0;
                        item.Heures_Mercredi_Jour = 0;
                        item.Heures_Samedi_Jour = 0;
                        item.Heures_Vendredi_Jour = 0;
                        item.Heures_Dimanche_Nuit = 0;
                        item.Heures_Jeudi_Nuit = 0;
                        item.Heures_Lundi_Nuit = 0;
                        item.Heures_Mardi_Nuit = 0;
                        item.Heures_Mercredi_Nuit = 0;
                        item.Heures_Samedi_Nuit = 0;
                        item.Heures_Vendredi_Nuit = 0;
                    }
                    catch (Exception) { }
                    try
                    {
                        item.Releve_Heure_Forfait1 = null;
                        try
                        {
                            ((Releve_Heure_Forfait)this.DataContext).Heure_Regie.Remove(item);
                        }
                        catch (Exception) { }
                        ((App)App.Current).mySitaffEntities.Heure_Regie.DeleteObject(item);
                    }
                    catch (Exception)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
            }
            this._DataGridHeureRegie.Items.Refresh();
        }

        private void copierRegie()
        {
            this.heure_regie_copy = new ObservableCollection<Heure_Regie>();
            foreach (Heure_Regie hf in this._DataGridHeureRegie.SelectedItems.OfType<Heure_Regie>())
            {
                Heure_Regie temp = new Heure_Regie();
                temp.Salarie1 = hf.Salarie1;
                temp.Heures_Lundi_Jour = hf.Heures_Lundi_Jour;
                temp.Heures_Mardi_Jour = hf.Heures_Mardi_Jour;
                temp.Heures_Mercredi_Jour = hf.Heures_Mercredi_Jour;
                temp.Heures_Jeudi_Jour = hf.Heures_Jeudi_Jour;
                temp.Heures_Vendredi_Jour = hf.Heures_Vendredi_Jour;
                temp.Heures_Samedi_Jour = hf.Heures_Samedi_Jour;
                temp.Heures_Dimanche_Jour = hf.Heures_Dimanche_Jour;
                temp.Heures_Lundi_Nuit = hf.Heures_Lundi_Nuit;
                temp.Heures_Mardi_Nuit = hf.Heures_Mardi_Nuit;
                temp.Heures_Mercredi_Nuit = hf.Heures_Mercredi_Nuit;
                temp.Heures_Jeudi_Nuit = hf.Heures_Jeudi_Nuit;
                temp.Heures_Vendredi_Nuit = hf.Heures_Vendredi_Nuit;
                temp.Heures_Samedi_Nuit = hf.Heures_Samedi_Nuit;
                temp.Heures_Dimanche_Nuit = hf.Heures_Dimanche_Nuit;
                temp.Vehicule_Lundi = hf.Vehicule_Lundi;
                temp.Vehicule_Mardi = hf.Vehicule_Mardi;
                temp.Vehicule_Mercredi = hf.Vehicule_Mercredi;
                temp.Vehicule_Jeudi = hf.Vehicule_Jeudi;
                temp.Vehicule_Vendredi = hf.Vehicule_Vendredi;
                temp.Vehicule_Samedi = hf.Vehicule_Samedi;
                temp.Vehicule_Dimanche = hf.Vehicule_Dimanche;
                this.heure_regie_copy.Add(temp);
            }
        }

        private void collerRegie()
        {
            foreach (Heure_Regie hf in this.heure_regie_copy)
            {
                ((Releve_Heure_Forfait)this.DataContext).Heure_Regie.Add(hf);
            }
            this._DataGridHeureRegie.Items.Refresh();
        }

        private void _button_copierRegie_Click(object sender, RoutedEventArgs e)
        {
            this.copierRegie();
        }

        private void _button_collerRegie_Click(object sender, RoutedEventArgs e)
        {
            this.collerRegie();
        }

        private void _button_supprimerRegie_Click_1(object sender, RoutedEventArgs e)
        {
            this.deleteLigneRegie();
        }

        #endregion

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
            this._DataGridHeureForfait.ContextMenu = contextMenu;

            MenuItem itemAfficher = new MenuItem();
            itemAfficher.Header = "Copier";
            itemAfficher.Click += new RoutedEventHandler(delegate { this.copier(); });

            MenuItem itemAfficher2 = new MenuItem();
            itemAfficher2.Header = "Coller";
            itemAfficher2.Click += new RoutedEventHandler(delegate { this.coller(); });

            MenuItem itemAfficher3 = new MenuItem();
            itemAfficher3.Header = "Supprimer";
            itemAfficher3.Click += new RoutedEventHandler(delegate { this.deleteLigne(); });


            contextMenu.Items.Add(itemAfficher);
            contextMenu.Items.Add(itemAfficher2);
            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(itemAfficher3);

            ContextMenu contextMenu2 = new ContextMenu();
            contextMenu2.Background = colorMenu;
            this._DataGridHeureRegie.ContextMenu = contextMenu2;

            MenuItem itemAfficher4 = new MenuItem();
            itemAfficher4.Header = "Copier";
            itemAfficher4.Click += new RoutedEventHandler(delegate { this.copierRegie(); });

            MenuItem itemAfficher5 = new MenuItem();
            itemAfficher5.Header = "Coller";
            itemAfficher5.Click += new RoutedEventHandler(delegate { this.collerRegie(); });

            MenuItem itemAfficher6 = new MenuItem();
            itemAfficher6.Header = "Supprimer";
            itemAfficher6.Click += new RoutedEventHandler(delegate { this.deleteLigneRegie(); });


            contextMenu2.Items.Add(itemAfficher4);
            contextMenu2.Items.Add(itemAfficher5);
            contextMenu2.Items.Add(new Separator());
            contextMenu2.Items.Add(itemAfficher6);
        }

        #endregion        

        #endregion

    }
}
