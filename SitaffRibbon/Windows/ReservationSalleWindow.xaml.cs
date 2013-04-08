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
    /// Logique d'interaction pour ReservationSalleWindow.xaml
    /// </summary>
    public partial class ReservationSalleWindow : Window
    {
        public ReservationSalleWindow()
        {

            InitializeComponent();

            this.listEntreprise_Mere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(em => em.Nom));
            this.listSalle = new ObservableCollection<Salle>(((App)App.Current).mySitaffEntities.Salle.OrderBy(sal => sal.Libelle));
            this.listSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.Where(sa => sa.Salarie_Interne != null).OrderBy(sa => sa.Personne.Nom).ThenBy(sal => sal.Personne.Prenom));
            this.listBesoin_Reservation_Salle = new ObservableCollection<Besoin_Reservation_Salle>(((App)App.Current).mySitaffEntities.Besoin_Reservation_Salle.OrderBy(brs => brs.Libelle));
        }

        #region LectureSeule
        public void Lecture_Seule()
        {
            this._comboBoxEntrepriseMere.IsEnabled = false;
            this._comboBoxSalle.IsEnabled = false;
            this._comboBoxDemandeur.IsEnabled = false;
            this._textBoxAdresseMail.IsEnabled = false;
            this._datePickerDateDebut.IsEnabled = false;
            this._textBoxHeureDebut.IsEnabled = false;
            this._textBoxHeureFin.IsEnabled = false;
            this._textBoxObjetReunion.IsEnabled = false;
            this._textBoxNbParticipant.IsEnabled = false;
            this._textBoxCommentaire.IsEnabled = false;
            this._dataGridBesoinGauche.IsEnabled = false;
            this._dataGridBesoinDroite.IsEnabled = false;
            this._dataGridClient.IsEnabled = false;
            this._dataGridFournisseur.IsEnabled = false;
            this._dataGridSalaries.IsEnabled = false;
            this._buttonBesoinsMettreADroite.IsEnabled = false;
            this._buttonBesoinsMettreAGauche.IsEnabled = false;
            this._buttonClientsAjouter.IsEnabled = false;
            this._buttonClientsSupprimer.IsEnabled = false;
            this._buttonFournisseurAjouter.IsEnabled = false;
            this._buttonSupprimerFournisseur.IsEnabled = false;
            this._buttonSalariesAjouter.IsEnabled = false;
            this._buttonSalariesSupprimer.IsEnabled = false;
        }
        #endregion

        #region propriété de dépendance

        #region Entreprise Mere
        public ObservableCollection<Entreprise_Mere> listEntreprise_Mere
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntreprise_MereProperty); }
            set { SetValue(listEntreprise_MereProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntreprise_Mere.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntreprise_MereProperty =
            DependencyProperty.Register("listEntreprise_Mere", typeof(ObservableCollection<Entreprise_Mere>), typeof(ReservationSalleWindow), new UIPropertyMetadata(null));
        #endregion

        #region Salle
        public ObservableCollection<Salle> listSalle
        {
            get { return (ObservableCollection<Salle>)GetValue(listSalleProperty); }
            set { SetValue(listSalleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntreprise_Mere.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalleProperty =
            DependencyProperty.Register("listSalle", typeof(ObservableCollection<Salle>), typeof(ReservationSalleWindow), new UIPropertyMetadata(null));
        #endregion

        #region Besoins

        public ObservableCollection<Besoin_Reservation_Salle> listBesoin_Reservation_Salle
        {
            get { return (ObservableCollection<Besoin_Reservation_Salle>)GetValue(listBesoin_Reservation_SalleProperty); }
            set { SetValue(listBesoin_Reservation_SalleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListBesoin_Reservation_Salle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listBesoin_Reservation_SalleProperty =
            DependencyProperty.Register("ListBesoin_Reservation_Salle", typeof(ObservableCollection<Besoin_Reservation_Salle>), typeof(ReservationSalleWindow), new UIPropertyMetadata(null));

        #endregion

        #region Salarie
        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(ReservationSalleWindow), new UIPropertyMetadata(null));
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
            if (this.Verif_General())
            {
                this._dataGridBesoinDroite.CommitEdit();
                bool verif = true;
                foreach (Reservation_Salle item in ((App)App.Current).mySitaffEntities.Reservation_Salle.Where(sa => sa.Salle1.Identifiant == ((Salle)this._comboBoxSalle.SelectedItem).Identifiant))
                {

                    if (((Reservation_Salle)this.DataContext).Identifiant == item.Identifiant)
                    {
                        if (((Reservation_Salle)this.DataContext).Heure_Debut != item.Heure_Debut)
                        {
                            verif = ValidOk();
                        }
                        if (((Reservation_Salle)this.DataContext).Heure_Fin != item.Heure_Fin && verif == true)
                        {
                            verif = ValidOk();
                        }
                    }
                    else
                    {
                        verif = ValidOk();
                    }
                }
                if (verif == true)
                {
                    ((Reservation_Salle)this.DataContext).Nb_Participants = int.Parse(this._textBoxNbParticipant.Text);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cette salle est déjà réservée au même moment");
                }
            }
            else
            {
                MessageBox.Show("Tout les champs ne sont pas bien renseignés");
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

        #region Bouton besoins

        private void _ButtonBesoinsMettreADroite(object sender, RoutedEventArgs e)
        {
         
            bool verif = true;
            if (this._dataGridBesoinGauche.SelectedItem != null)
            {
                foreach (Besoin_Reservation_Salle item1 in this._dataGridBesoinGauche.SelectedItems)
                {
                    Reservation_SalleBesoin_Reservation_Salle tmp = new Reservation_SalleBesoin_Reservation_Salle();
                    tmp.Besoin_Reservation_Salle1 = (Besoin_Reservation_Salle)item1;

                    foreach (Reservation_SalleBesoin_Reservation_Salle item in ((Reservation_Salle)this.DataContext).Reservation_SalleBesoin_Reservation_Salle)
                    {
                        if (item.Besoin_Reservation_Salle1.Identifiant == tmp.Besoin_Reservation_Salle1.Identifiant)
                        {
                            verif = false;
                        }
                    }
                    if (verif == true)
                    {
                        tmp.Reservation_Salle1 = (Reservation_Salle)this.DataContext;
                        tmp.Quantite = 1;
                    }
                }
            }
        }

        private void _ButtonBesoinsMettreAGauche(object sender, RoutedEventArgs e)
        {
            if (this._dataGridBesoinDroite.SelectedItem != null)
            {
                if (this._dataGridBesoinDroite.SelectedItems.Count == 1)
                {
                    Reservation_SalleBesoin_Reservation_Salle tmp = new Reservation_SalleBesoin_Reservation_Salle();
                    tmp = (Reservation_SalleBesoin_Reservation_Salle)this._dataGridBesoinDroite.SelectedItem;
                    ((Reservation_Salle)this.DataContext).Reservation_SalleBesoin_Reservation_Salle.Remove(tmp);
                }
            }
        }
		
        private void _newBesoin_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("En cours de développement");
        }
		

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Initialisation de la fenêtre
            BesoinReservationSalleWindow besoinReservationSalleWindow = new BesoinReservationSalleWindow();

            //Création de l'objet temporaire            
            Besoin_Reservation_Salle tmp = new Besoin_Reservation_Salle();

            //Mise de l'objet temporaire dans le datacontext
            besoinReservationSalleWindow.DataContext = tmp;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = besoinReservationSalleWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                try
                {
                    if (!this.listBesoin_Reservation_Salle.Contains((Besoin_Reservation_Salle)besoinReservationSalleWindow.DataContext))
                    {
                        this.listBesoin_Reservation_Salle.Add((Besoin_Reservation_Salle)besoinReservationSalleWindow.DataContext);
                    }
                }
                catch (Exception) { }
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Besoin_Reservation_Salle)besoinReservationSalleWindow.DataContext);
                }
                catch (Exception)
                {
                }
            }            
        }

        #endregion

        #region Bouton Clients

        private void _ButtonClientsAjouter_Click(object sender, RoutedEventArgs e)
        {
            //Initialisation de la fenêtre
            ReservationSalleSelectionClientWindow reservationSalleSelectionClientWindow = new ReservationSalleSelectionClientWindow();

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = reservationSalleSelectionClientWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                bool verif = true;

                Reservation_SalleContact_Client_Invite tmp = new Reservation_SalleContact_Client_Invite();
                tmp.Contact1 = (Contact)reservationSalleSelectionClientWindow._comboBoxContact.SelectedItem;

                foreach (Reservation_SalleContact_Client_Invite item in this._dataGridClient.Items)
                {
                    if (tmp.Contact1.Personne.Identifiant == item.Contact1.Personne.Identifiant && tmp.Contact1.Personne.Identifiant != 0)
                    {
                        verif = false;
                    }
                }
                if (verif == true)
                {
                    tmp.Reservation_Salle1 = ((Reservation_Salle)this.DataContext);
                }
                else
                {
                    MessageBox.Show(tmp.Contact1.Personne.Nom + " " + tmp.Contact1.Personne.Prenom + " est déjà invité(e)");
                }
            }
            else
            {
            }
            this._dataGridClient.Items.Refresh();
            Verif_NbParticipant();
        }

        private void _ButtonClientsSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridClient.SelectedItem != null)
            {
                if (this._dataGridClient.SelectedItems.Count ==1)
                {
                    Reservation_SalleContact_Client_Invite tmp = new Reservation_SalleContact_Client_Invite();
                    tmp = (Reservation_SalleContact_Client_Invite)this._dataGridClient.SelectedItem;
                    ((Reservation_Salle)this.DataContext).Reservation_SalleContact_Client_Invite.Remove(tmp);
                }
            }
            Verif_NbParticipant();
        }

        #endregion

        #region Bouton Fournisseur

        private void _ButtonFournisseurAjouter_Click(object sender, RoutedEventArgs e)
        {
            //Initialisation de la fenêtre
            ReservationSalleSelectionFournisseurWindow reservationSalleSelectionFournisseurWindow = new ReservationSalleSelectionFournisseurWindow();

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = reservationSalleSelectionFournisseurWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                Reservation_SalleContact_Fournisseur_Invite tmp = new Reservation_SalleContact_Fournisseur_Invite();
                tmp.Contact1 = (Contact)reservationSalleSelectionFournisseurWindow._comboBoxContact.SelectedItem;
                bool verif = true;
                foreach (Reservation_SalleContact_Fournisseur_Invite item in this._dataGridFournisseur.Items)
                {
                    if (item.Contact1.Personne.Identifiant == tmp.Contact1.Personne.Identifiant && tmp.Contact1.Personne.Identifiant != 0)
                    {
                        verif = false;   
                    }
                }
                if (verif == true)
                {
                    tmp.Reservation_Salle1 = ((Reservation_Salle)this.DataContext);
                }
                else
                {
                    MessageBox.Show(tmp.Contact1.Personne.Nom + " " + tmp.Contact1.Personne.Prenom + " est déjà invité(e)");
                }
            }
            else
            {

            }
            this._dataGridFournisseur.Items.Refresh();
            Verif_NbParticipant();
        }

        private void _buttonFournisseurSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridFournisseur.SelectedItem != null)
            {
                if (this._dataGridFournisseur.SelectedItems.Count == 1)
                {
                    Reservation_SalleContact_Fournisseur_Invite tmp = new Reservation_SalleContact_Fournisseur_Invite();
                    tmp = (Reservation_SalleContact_Fournisseur_Invite)this._dataGridFournisseur.SelectedItem;
                    ((Reservation_Salle)this.DataContext).Reservation_SalleContact_Fournisseur_Invite.Remove(tmp);
                }
            }
            Verif_NbParticipant();
        }

        #endregion

        #region Bouton Salaries

        private void _ButtonSalariesAjouter_Click(object sender, RoutedEventArgs e)
        {
            //Initialisation de la fenêtre
            ReservationSalleSelectionSalarieWindow reservation_SalleSalarie_Invite = new ReservationSalleSelectionSalarieWindow();

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = reservation_SalleSalarie_Invite.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                Reservation_SalleSalarie_Invite tmp = new Reservation_SalleSalarie_Invite();
                tmp.Salarie1 = (Salarie)reservation_SalleSalarie_Invite._comboxBoxSalarie.SelectedItem;
                bool verif = true;
                foreach (Reservation_SalleSalarie_Invite item in this._dataGridSalaries.Items)
                {
                    if (item.Salarie1.Identifiant == tmp.Salarie1.Identifiant)
                    {
                        verif = false;
                    }
                }
                if (verif == true)
                {
                    tmp.Reservation_Salle1 = ((Reservation_Salle)this.DataContext);
                }
                else
                {
                    MessageBox.Show(tmp.Salarie1.Personne.Nom + " " + tmp.Salarie1.Personne.Prenom + " est déjà invité(e)");
                }
            }
            else
            {

            }
            this._dataGridSalaries.Items.Refresh();
            Verif_NbParticipant();
        }

        private void _ButtonSalariesSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridSalaries.SelectedItem != null)
            {
                if (this._dataGridSalaries.SelectedItems.Count == 1)
                {
                    Reservation_SalleSalarie_Invite tmp = new Reservation_SalleSalarie_Invite();
                    tmp = (Reservation_SalleSalarie_Invite)this._dataGridSalaries.SelectedItem;
                    ((Reservation_Salle)this.DataContext).Reservation_SalleSalarie_Invite.Remove(tmp);
                }
            }
            Verif_NbParticipant();
        }

        #endregion

        #endregion

        #region Verifications

        private bool Verif_General()
        {
            bool verif = true;
            if (!this.Verif_ComboBoxEntrepriseMere())
            {
                verif = false;
            }
            if (!this.Verif_ComboBoxSalle())
            {
                verif = false;
            }
            if (!this.Verif_ComboBoxDemandeur())
            {
                verif = false;
            }
            if (!this.Verif_TextBoxHeureDebut())
            {
                verif = false;
            }
            if (!this.Verif_TextBoxHeureFin())
            {
                verif = false;
            }
            if (!this.Verif_TextBoxAdresseMail())
            {
                verif = false;
            }
            if (!this.Verif_TextBoxNbParticipant())
            {
                verif = false;
            }
            if (!this.Verif_TextBoxObjetReunion())
            {
                verif = false;
            }
            if (!this.Verif_DatePickerDateDebut())
            {
                verif = false;
            }
            if (!this.Verif_DatePickerDateFin())
            {
                verif = false;
            }
            if (!this.Verif_NbParticipants_Ok())
            {
                verif = false;
            }
            return verif;
        }

        #region ComboBox

        #region ComboBox Entrprise Mere

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
            this.listSalle = new ObservableCollection<Salle>(((App)App.Current).mySitaffEntities.Salle.Where(sa => sa.Entreprise_Mere1.Nom == ((Entreprise_Mere)this._comboBoxEntrepriseMere.SelectedItem).Nom));
        }

        #endregion

        #region ComboBox Salle

        private bool Verif_ComboBoxSalle()
        {
            bool verif = true;
            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._comboBoxSalle.SelectedItem == null)
            {
                verif = false;
                this._comboBoxSalle.Background = rouge;
            }
            else
            {
                verif = true;
                this._comboBoxSalle.Background = vert;
            }
            return verif;
        }

        private void _ComboBoxSalle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._comboBoxEntrepriseMere.SelectedItem = ((Salle)this._comboBoxSalle.SelectedItem).Entreprise_Mere1;
            if (this._comboBoxSalle.SelectedItem != null)
            {
                this._textBlockMaximumParticipant.Text = ((Salle)this._comboBoxSalle.SelectedItem).Nb_Places.ToString();
                if (this._textBoxNbParticipant.Text != null)
                {
                    this.Verif_NbParticipant();
                }
            }
            else
            {
                this._textBlockMaximumParticipant.Text = "_";
                this._textBoxNbParticipant.Background = null;
            }
            this.Verif_ComboBoxSalle();
            Verif_NbParticipant();
        }
        #endregion

        #region ComboBox Demandeur
        private bool Verif_ComboBoxDemandeur()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._comboBoxDemandeur.SelectedItem == null)
            {
                verif = false;
                this._comboBoxDemandeur.Background = rouge;
            }
            else
            {
                verif = true;
                this._comboBoxDemandeur.Background = vert;
            }
            if (_comboBoxDemandeur.IsEnabled == false)
            {
                this._comboBoxDemandeur.Foreground = Brushes.Gray;
            }
            return verif;
        }
        private void _ComboBoxDemandeur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxDemandeur();
            try
            {
                this._textBoxAdresseMail.Text = ((Salarie)this._comboBoxDemandeur.SelectedValue).Personne.EMail_Pro.ToString();
            }
            catch
            {
                this._textBoxAdresseMail.Text = "";
            }
            if (this._textBoxNbParticipant.Text.Length == 0)
            {
                ((Reservation_Salle)this.DataContext).Nb_Participants = 1;
            }
        }
        #endregion

        #region DatePicker Date Debut

        private bool Verif_DatePickerDateDebut()
        {
            bool verif = true;
            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._datePickerDateDebut.SelectedDate == null)
            {
                verif = false;
            }
            if (verif == true)
            {
                this._datePickerDateDebut.Background = vert;
            }
            else
            {
                this._datePickerDateDebut.Background = rouge;
            }
            if (this._datePickerDateDebut.IsEnabled == false)
            {
                this._datePickerDateDebut.Background = Brushes.White;
            }
            return verif;
        }

        private void _DatePickerDateDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerDateDebut();
        }

        #endregion

        #region DatePicker Date Fin
        private bool Verif_DatePickerDateFin()
        {
            bool verif = true;
            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._datePickerDateFin.SelectedDate == null)
            {
                verif = false;
            }

            if (verif == true)
            {
                this._datePickerDateFin.Background = vert;
            }
            else
            {
                this._datePickerDateFin.Background = rouge;
            }
            if (this._datePickerDateFin.IsEnabled == false)
            {
                this._datePickerDateFin.Background = Brushes.White;
            }
            return verif;
        }

        private void _DatePickerDateFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerDateFin();
        }
        #endregion

        #endregion

        #region TextBox

        #region TextBox AdresseMail
        private bool Verif_TextBoxAdresseMail()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            //TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            //string colorHexavert = "#8900CE00";
            //Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            //string colorHexarouge = "#89FF0000";
            //Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            //if (this._textBoxAdresseMail.Text.Trim().Length == 0)
            //{
            //    verif = false;
            //    this._textBoxAdresseMail.Foreground = Brushes.Red;
            //    this._textBoxAdresseMail.Background = rouge;
            //}
            //else
            //{
            //    verif = true;
            //    this._textBoxAdresseMail.Foreground = Brushes.Green;
            //    this._textBoxAdresseMail.Background = vert;
            //}

            return verif;
        }
        private void _TextBoxAdresseMail_TextChanged(object sender, TextChangedEventArgs e)
        {
            //this.Verif_TextBoxAdresseMail();
        }
        #endregion

        #region TextBox Nombre Participant
        private bool Verif_TextBoxNbParticipant()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._textBoxNbParticipant.Text.Trim().Length == 0)
            {
                verif = false;
                this._textBoxNbParticipant.Background = rouge;
            }
            else
            {
                verif = true;
                this._textBoxNbParticipant.Background = vert;
            }

            return verif;
        }
        private void _TextBoxNbParticipant_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxNbParticipant();
        }
        #endregion

        #region TextBox Objet Reunion
        private bool Verif_TextBoxObjetReunion()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._textBoxObjetReunion.Text.Trim().Length == 0)
            {
                verif = false;
                this._textBoxObjetReunion.Background = rouge;
            }
            else
            {
                verif = true;
                this._textBoxObjetReunion.Background = vert;
            }

            return verif;
        }
        private void _TextBoxObjetReunion_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxObjetReunion();
        }
        #endregion

        #region TextBox Commentaire
        //private bool Verif_TextBoxCommentaire()
        //{
        //    bool verif = true;
        //    //Initialisation des couleurs avec transparence
        //    TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
        //    string colorHexavert = "#8900CE00";
        //    Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
        //    string colorHexarouge = "#89FF0000";
        //    Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

        //    if (this._textBoxCommentaire.Text.Trim().Length == 0)
        //    {
        //        verif = false;
        //        this._textBoxCommentaire.Foreground = Brushes.Red;
        //        this._textBoxCommentaire.Background = rouge;
        //    }
        //    else
        //    {
        //        verif = true;
        //        this._textBoxCommentaire.Foreground = Brushes.Green;
        //        this._textBoxCommentaire.Background = vert;
        //    }

        //    return verif;
        //}
        //private void _TextBoxCommentaire_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    this.Verif_TextBoxCommentaire();
        //}
        #endregion

        #region TextBox Heure

        #region Heure Debut
        private bool Verif_TextBoxHeureDebut()
        {
            bool verif = true;
            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._textBoxHeureDebut.Text.Trim().Length == 0)
            {
                verif = false;
                this._textBoxHeureDebut.Background = rouge;
            }
            else
            {
                verif = true;
                this._textBoxHeureDebut.Background = vert;
            }
            if (Verif_Heure())
            {
                this._textBoxHeureDebut.Background = vert;
                this._textBlockHeureDebut.Foreground = vert;
                this._textBoxHeureFin.Background = vert;
                this._textBlockHeureFin.Foreground = vert;
            }
            else
            {
                this._textBoxHeureDebut.Background = rouge;
                this._textBlockHeureDebut.Foreground = rouge;
                this._textBoxHeureFin.Background = rouge;
                this._textBlockHeureFin.Foreground = rouge;
                verif = false;
            }
            return verif;

        }
        private void _TextBoxHeureDebut_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this._textBoxHeureDebut.Text.Trim().Length != 0)
            {
                if (this._textBoxHeureDebut.Text.Substring(0, 1) != "x")
                {
                    this._textBoxHeureDebut.Text = Verif_Format_Heure(this._textBoxHeureDebut.Text);
                    Verifications tmp = new Verifications();
                    tmp.TextBoxHeureObligatoire(this._textBoxHeureDebut, this._textBlockHeureDebut);
                    this.Verif_TextBoxHeureDebut();
                }
            }
        }
        private void _TextBoxHeureDebut_GotFocus(Object sender, EventArgs e)
        {
            if (this._textBoxHeureDebut.Text == "xx:xx")
            {
                this._textBoxHeureDebut.Text = "";
                this._textBoxHeureDebut.Foreground = Brushes.Black;
            }
        }
        #endregion

        #region Heure Fin
        private bool Verif_TextBoxHeureFin()
        {
            bool verif = true;
            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._textBoxHeureFin.Text.Trim().Length == 0)
            {
                verif = false;
                this._textBoxHeureFin.Background = rouge;
            }
            else
            {
                verif = true;
                this._textBoxHeureFin.Background = vert;
            }
            if (Verif_Heure())
            {
                this._textBoxHeureDebut.Background = vert;
                this._textBlockHeureDebut.Foreground = vert;
                this._textBoxHeureFin.Background = vert;
                this._textBlockHeureFin.Foreground = vert;
            }
            else
            {
                this._textBoxHeureDebut.Background = rouge;
                this._textBlockHeureDebut.Foreground = rouge;
                this._textBoxHeureFin.Background = rouge;
                this._textBlockHeureFin.Foreground = rouge;
                verif = false;
            }
            if (this._textBoxHeureFin.IsEnabled == false)
            {
                this._textBlockHeureDebut.Foreground = Brushes.Black;
                this._textBlockHeureFin.Foreground = Brushes.Black;
            }
            return verif;
        }
        private void _TextBoxHeureFin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this._textBoxHeureFin.Text.Trim().Length != 0)
            {
                if (this._textBoxHeureFin.Text.Substring(0, 1) != "x")
                {
                    this._textBoxHeureFin.Text = Verif_Format_Heure(this._textBoxHeureFin.Text);
                    Verifications tmp = new Verifications();
                    tmp.TextBoxHeureObligatoire(this._textBoxHeureFin, this._textBlockHeureFin);
                    this.Verif_TextBoxHeureFin();
                }
            }
        }
        private void _TextBoxHeureFin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this._textBoxHeureFin.Text.Trim().Length == 5 && this._textBoxHeureFin.Text.Substring(0,1) != "x")
            {
                Verifications tmp = new Verifications();
                tmp.TextBoxHeureObligatoire(this._textBoxHeureFin, this._textBlockHeureFin);
                this.Verif_TextBoxHeureFin();
            }
        }
        private void _TextBoxHeureFin_GotFocus(Object sender, EventArgs e)
        {
            if (this._textBoxHeureFin.Text == "xx:xx")
            {
                this._textBoxHeureFin.Text = "";
                this._textBoxHeureFin.Foreground = Brushes.Black;
            }
        }
        #endregion

        #endregion

        #endregion

        #region Verif Nombre Participant

        private void Verif_NbParticipant()
        {
            int temp;
            if (this._textBlockMaximumParticipant.Text != "_")
            {
                temp = this._dataGridSalaries.Items.Count + this._dataGridFournisseur.Items.Count + this._dataGridClient.Items.Count + 1;
                this._textBoxNbParticipant.Text = temp.ToString();

                //Initialisation des couleurs avec transparence
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
                string colorHexavert = "#8900CE00";
                Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
                string colorHexarouge = "#89FF0000";
                Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

                if (temp <= int.Parse(this._textBlockMaximumParticipant.Text))
                {
                    this._textBoxNbParticipant.Background = vert;
                }
                else
                {
                    this._textBoxNbParticipant.Background = rouge;
                }
            }
            else
            {
                temp = this._dataGridSalaries.Items.Count + this._dataGridFournisseur.Items.Count + this._dataGridClient.Items.Count + 1;
                this._textBoxNbParticipant.Text = temp.ToString();
            }
            if (this._textBlockMaximumParticipant.Text != "_")
            {
                if (this._textBoxNbParticipant.Text.Trim().Length == 0)
                {
                    ((Reservation_Salle)this.DataContext).Nb_Participants = int.Parse(this._textBoxNbParticipant.Text);
                }
            }
            if (this._textBoxNbParticipant.IsEnabled == false)
            {
                this._textBoxNbParticipant.Background = Brushes.White;
            }
            

        }

        #endregion

        #region Verif Heure

        private bool Verif_Heure()
        {
            bool verif = true;
            try
            {
                if (this._textBoxHeureDebut.Text.Trim().Length == 5)
                {
                    if (this._textBoxHeureFin.Text.Trim().Length == 5)
                    {
                        string hDeb = this._textBoxHeureDebut.Text;
                        string hFin = this._textBoxHeureFin.Text;
                        if (int.Parse(hDeb.Substring(0, 2)) <= int.Parse(hFin.Substring(0, 2)))
                        {
                            if (int.Parse(hDeb.Substring(0, 2)) == int.Parse(hFin.Substring(0, 2)))
                            {
                                if (int.Parse(hDeb.Substring(3, 2)) >= int.Parse(hFin.Substring(3, 2)))
                                {
                                    verif = false;
                                }
                            }
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
                }
                else
                {
                    verif = false;
                }
            }
            catch
            {
                verif = false;
            }
            
            return verif;
        }
        #endregion

        #region Verif Heure pour bouton Ok

        private bool ValidOk()
        {
            bool verif = false;

            ObservableCollection<Reservation_Salle> listValid = new ObservableCollection<Reservation_Salle>(((App)App.Current).mySitaffEntities.Reservation_Salle.Where(sa => sa.Salle1.Identifiant == ((Salle)this._comboBoxSalle.SelectedItem).Identifiant && sa.Date_Reservation == this._datePickerDateDebut.SelectedDate && sa.Identifiant != ((Reservation_Salle)this.DataContext).Identifiant ));

            if (listValid.Count == 0)
            {
                verif = true;
            }

            foreach (Reservation_Salle item in listValid)
            {
                    
                        if (int.Parse(this._textBoxHeureDebut.Text.Substring(0, 2)) < int.Parse(item.Heure_Debut.Substring(0, 2)) && int.Parse(this._textBoxHeureFin.Text.Substring(0, 2)) < int.Parse(item.Heure_Debut.Substring(0, 2)) )
                        {
                            verif = true;
                        }
                        if (int.Parse(this._textBoxHeureDebut.Text.Substring(0, 2)) > int.Parse(item.Heure_Fin.Substring(0, 2)) && int.Parse(this._textBoxHeureFin.Text.Substring(0, 2)) > int.Parse(item.Heure_Fin.Substring(0, 2)) )
                        {
                            verif = true;
                        }
                        if (int.Parse(this._textBoxHeureDebut.Text.Substring(0, 2)) == int.Parse(item.Heure_Debut.Substring(0, 2)) && int.Parse(this._textBoxHeureFin.Text.Substring(0, 2)) == int.Parse(item.Heure_Debut.Substring(0, 2)) )
                        {
                            if (int.Parse(this._textBoxHeureFin.Text.Substring(3, 2)) < int.Parse(item.Heure_Debut.Substring(3, 2)))
                            {

                                verif = true;
                            }
                        }
                        if (int.Parse(this._textBoxHeureDebut.Text.Substring(0, 2)) == int.Parse(item.Heure_Fin.Substring(0, 2)) && int.Parse(this._textBoxHeureFin.Text.Substring(0, 2)) == int.Parse(item.Heure_Fin.Substring(0, 2)) )
                        {
                            if (int.Parse(item.Heure_Fin.Substring(3, 2)) < int.Parse(this._textBoxHeureDebut.Text.Substring(3, 2)))
                            {
                                verif = true;
                            }
                        }
                        if (int.Parse(this._textBoxHeureDebut.Text.Substring(0, 2)) == int.Parse(item.Heure_Fin.Substring(0, 2)) && int.Parse(this._textBoxHeureFin.Text.Substring(0, 2)) > int.Parse(item.Heure_Fin.Substring(0, 2)) )
                        {
                            if (int.Parse(item.Heure_Fin.Substring(3, 2)) < int.Parse(this._textBoxHeureDebut.Text.Substring(3, 2)))
                            {
                                verif = true;
                            }
                        }
                        if (int.Parse(this._textBoxHeureDebut.Text.Substring(0, 2)) < int.Parse(item.Heure_Debut.Substring(0, 2)) && int.Parse(this._textBoxHeureFin.Text.Substring(0, 2)) == int.Parse(item.Heure_Debut.Substring(0, 2)) && ((Reservation_Salle)this.DataContext).Date_Reservation != item.Date_Reservation)
                        {
                            if (int.Parse(item.Heure_Debut.Substring(3, 2)) > int.Parse(this._textBoxHeureFin.Text.Substring(3, 2)))
                            {
                                verif = true;
                            }
                        }
                
                }
            return verif;
        }

        private bool Valid_Reservation()
        {
            bool verif = true;
            string datedeb = this._datePickerDateDebut.SelectedDate.ToString().Substring(0, 9);
            string datefin = this._datePickerDateFin.SelectedDate.ToString().Substring(0, 9);
            //variable date de début
            string jourdebut = this._datePickerDateDebut.ToString().Substring(0, 2);
            string moisdebut = this._datePickerDateDebut.ToString().Substring(3, 2);
            string anneedebut = this._datePickerDateDebut.ToString().Substring(6, 4);
            //variable date de fin
            string jourfin = this._datePickerDateFin.ToString().Substring(0, 2);
            string moisfin = this._datePickerDateFin.ToString().Substring(3, 2);
            string anneefin = this._datePickerDateFin.ToString().Substring(6, 4);
            ObservableCollection<Reservation_Salle> listValid = new ObservableCollection<Reservation_Salle>(((App)App.Current).mySitaffEntities.Reservation_Salle.Where(sa => sa.Salle1.Identifiant == ((Salle)this._comboBoxSalle.SelectedItem).Identifiant && sa.Identifiant != ((Reservation_Salle)this.DataContext).Identifiant ));
            if (listValid.Count == 0)
            {
                verif = false;
            }
            foreach (Reservation_Salle item in listValid)
            {
                //Variable date de début
                string debjourdebut = item.Date_Reservation.ToString().Substring(0, 2);
                string debmoisdebut = item.Date_Reservation.ToString().Substring(3, 2);
                string debanneedebut = item.Date_Reservation.ToString().Substring(6, 4);
                //Variable date de fin
                string finjourfin = item.Date_Reservation_Fin.ToString().Substring(0, 2);
                string finmoisfin = item.Date_Reservation_Fin.ToString().Substring(3, 2);
                string finanneefin = item.Date_Reservation_Fin.ToString().Substring(6, 4);
                //Variable heure
                string itemheuredeb = item.Heure_Debut;
                string itemheurefin = item.Heure_Fin;


                if (true)
                {
                    
                }

            }
            return verif;
        }


        #endregion

        #region Verif Modif
        //Si cette page est utilisé pour modifier une réservation
        private void Verif_Modif()
        {
            if (((Reservation_Salle)this.DataContext).Heure_Fin != null)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
                string colorHexavert = "#8900CE00";
                Brush vert = (Brush)converter.ConvertFrom(colorHexavert);

                this._textBoxHeureDebut.Background = vert;
                this._textBlockHeureDebut.Foreground = vert;
                this._textBoxHeureFin.Background = vert;
                this._textBlockHeureFin.Foreground = vert;
            }
        }
        #endregion

        #region verif format heure
        private string Verif_Format_Heure(string heure)
        {

            if (heure.Length == 4 && heure.Substring(1,1) == ":")
            {
                heure = "0" + heure;
            }
            if (heure.Length == 3)
            {
                heure = "0" + heure.Substring(0, 1) + ":" + heure.Substring(1, 2);
            }
            if (heure.Length == 1)
            {
                heure = "0" + heure + ":00";
            }
            return heure;
        }
        #endregion

        #region Verif Nb Participant Verif General
        private bool Verif_NbParticipants_Ok()
        {
            bool verif = true;
            try
            {
                if (int.Parse(this._textBoxNbParticipant.Text) > int.Parse(this._textBlockMaximumParticipant.Text))
                {
                    verif = false;
                    Verif_NbParticipant();
                }
            }
            catch (Exception)
            {

                verif = false;
            }
            return verif;
        }
        #endregion

        #endregion

        #region Key Up
        private void _dataGridBesoinDroite_KeyUp(object sender, KeyEventArgs e)
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
            catch (Exception) { }
        }
        #endregion

    }
}