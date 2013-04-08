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
//Using pour utiliser le type TypeConverter pour la conversion de couleur
using System.ComponentModel;
using System.Diagnostics;
using SitaffRibbon.Classes;
using SitaffRibbon.UserControls;
using SitaffRibbon.Windows.ParametresUserControls;
using System.Threading;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour CongeWindow.xaml
    /// </summary>
    public partial class CongeWindow : Window
    {

        #region attribut

        public bool soloLecture = false;
        public bool demande = true;
        public bool creation = false;
        public bool verrouillerSalarie = false;

        #endregion

        #region Propriétés de dépendances



        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(CongeWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Motif_Demande> listMotif_Demande
        {
            get { return (ObservableCollection<Motif_Demande>)GetValue(listMotif_DemandeProperty); }
            set { SetValue(listMotif_DemandeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listMotif_Demande.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listMotif_DemandeProperty =
            DependencyProperty.Register("listMotif_Demande", typeof(ObservableCollection<Motif_Demande>), typeof(CongeWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Motif_Refus> listMotif_Refus
        {
            get { return (ObservableCollection<Motif_Refus>)GetValue(listMotif_RefusProperty); }
            set { SetValue(listMotif_RefusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listMotif_Refus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listMotif_RefusProperty =
            DependencyProperty.Register("listMotif_Refus", typeof(ObservableCollection<Motif_Refus>), typeof(CongeWindow), new UIPropertyMetadata(null));



        #endregion

        #region constructeur

        public CongeWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            this._datePickerDateDemande.Focus();
        }



        public CongeWindow(bool verrouillerSalarie)
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependanceVerrouiller();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);
            this.verrouillerSalarie = verrouillerSalarie;

        }

        private void initialisationPropDependance()
        {
            this.listMotif_Demande = new ObservableCollection<Motif_Demande>(((App)App.Current).mySitaffEntities.Motif_Demande.OrderBy(md => md.Libelle));
            this.listMotif_Refus = new ObservableCollection<Motif_Refus>(((App)App.Current).mySitaffEntities.Motif_Refus.OrderBy(md => md.Libelle));
            this.listSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.OrderBy(sal => sal.Personne.Nom));

        }


        private void initialisationPropDependanceVerrouiller()
        {
            this.listMotif_Demande = new ObservableCollection<Motif_Demande>(((App)App.Current).mySitaffEntities.Motif_Demande.OrderBy(md => md.Libelle));
            this.listMotif_Refus = new ObservableCollection<Motif_Refus>(((App)App.Current).mySitaffEntities.Motif_Refus.OrderBy(md => md.Libelle));
            this.listSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.OrderBy(sal => sal.Personne.Nom));

        }
        #endregion

        #region boutons

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                //((Conge)this.DataContext).Nombre_Jours = (((DateTime)this._datePickerDateFinDemande.SelectedDate).Date - ((DateTime)this._datePickerDateDebutDemande.SelectedDate).Date).Days + 1;
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
            if (this.demande)
            {
                if (!this.Verif_Demande())
                {
                    verif = false;
                }
            }
            else
            {
                if (!this.Verif_Reponse())
                {
                    verif = false;
                }
            }

            return verif;
        }

        #region Demande

        private bool Verif_Demande()
        {
            bool verif = true;

            if (!this.Verif_comboBoxSalarie())
            {
                verif = false;
            }
            if (!this.Verif_datePickerDateDebut())
            {
                verif = false;
            }
            if (!this.Verif_datePickerDateFin())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxMotifDemande())
            {
                verif = false;
            }
            if (!this.Verif_textBoxCommentaire())
            {
                verif = false;
            }
            if (!this.Verif_datePickerDateDemande())
            {
                verif = false;
            }
            if (!this.Verif_textBoxNbJours())
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
            this.Verif_comboBoxSalarie();
        }

        #endregion

        #region Date_Debut

        private bool Verif_datePickerDateDebut()
        {
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateDebutDemande, this._textBlockDateDebutDemande);
        }

        private void _datePickerDateDebutDemande_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_datePickerDateDebut();
            this.Verif_datePickerDateFin();
        }

        #endregion

        #region Date_Fin

        private bool Verif_datePickerDateFin()
        {
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateFinDemande, this._textBlockDateFinDemande, this._datePickerDateDebutDemande);
        }

        private void _datePickerDateFinDemande_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_datePickerDateDebut();
            this.Verif_datePickerDateFin();
        }

        #endregion

        #region Motif

        private bool Verif_comboBoxMotifDemande()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxMotifDemande, this._textBlockMotifDemande);
        }

        private void _comboBoxMotifDemande_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxMotifDemande();
        }

        #endregion

        #region Commentaire

        private bool Verif_textBoxCommentaire()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxCommentaire, this._textBlockCommentaire); ;
        }

        private void _textBoxCommentaire_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxCommentaire();
        }

        #endregion

        #region Date Demande

        private bool Verif_datePickerDateDemande()
        {
			return ((App)App.Current).verifications.DatePickerSelectionNonObligatoire(this._datePickerDateDemande, this._textBlockDateDemande);
        }

        private void _datePickerDateDemande_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Verif_datePickerDateDemande())
            {
                ((Conge)this.DataContext).Demande_Fait_Le = this._datePickerDateDemande.SelectedDate;
            }

        }

        #endregion

        #region Nombre de jours

        private bool Verif_textBoxNbJours()
        {
            bool verif = true;

            double val;

            if (this._textBoxNbJours.Text.Trim().Length == 0)
            {
                verif = false;
            }
            else
            {
                this._textBoxNbJours.Text.Replace(".", ",");
                if (double.TryParse(this._textBoxNbJours.Text.Trim(), out val))
                {
                    int test;
                    test = (int)(double.Parse(this._textBoxNbJours.Text.Trim()) * 100);
                    int test2;
                    test2 = (int)(double.Parse(this._textBoxNbJours.Text.Trim()));
                    if (test == test2 * 100 || test == (test2 * 100) + 50)
                    {
                        verif = true;
                        if (test == test2 * 100)
                        {
                            //((Conge)this.DataContext).Nombre_Jours = double.Parse(this._textBoxNbJours.Text.Trim());
                            ((Conge)this.DataContext).Nombre_Jours = (double)test2;
                            this._textBoxNbJours.Text = ((Conge)this.DataContext).Nombre_Jours.ToString();
                            this._textBoxNbJours.Text.Replace(".", ",");
                        }
                        if (test == (test2 * 100) + 50)
                        {
                            ((Conge)this.DataContext).Nombre_Jours = ((double)((test2 * 100) + 50) / 100);
                            this._textBoxNbJours.Text = ((Conge)this.DataContext).Nombre_Jours.ToString();
                            this._textBoxNbJours.Text.Replace(".", ",");
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
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._textBoxNbJours, this._textBlockNbJours, verif);
            return verif;
        }

        private void _textBoxNbJours_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxNbJours();
        }

        #endregion

        #endregion

        #region Reponse

        private bool Verif_Reponse()
        {
            bool verif = true;

            if (!this.Verif_comboBoxMotifRefus())
            {
                verif = false;
            }
            if (!this.Verif_checkBoxs())
            {
                verif = false;
            }
            if (!this.Verif_textBoxDetailReponse())
            {
                verif = false;
            }

            return verif;
        }

        #region checkboxs

        private bool Verif_checkBoxs()
        {
            bool verif = true;

            if (this._checkBoxAccepteNon.IsChecked == false && this._checkBoxAccepteOui.IsChecked == false)
            {
                verif = false;
            }
            else
            {
                verif = true;
			}
			((App)App.Current).verifications.MettreCheckBoxEnCouleur(this._checkBoxAccepteNon, this._textBlockAccepte, verif);
			((App)App.Current).verifications.MettreCheckBoxEnCouleur(this._checkBoxAccepteOui, this._textBlockAccepte, verif);

            return verif;
        }

        #endregion

        #region Motif Refus

        private bool Verif_comboBoxMotifRefus()
        {
            bool verif = true;

            if (this._checkBoxAccepteNon.IsChecked == true)
            {
				verif = ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxMotifRefus, this._textBlockMotifRefus);
            }
            else
            {
				verif = ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxMotifRefus, this._textBlockMotifRefus);
			}

			((App)App.Current).verifications.MettreComboxEnCouleur(this._comboBoxMotifRefus, this._textBlockMotifRefus, verif);

            return verif;
        }

        private void _comboBoxMotifRefus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxMotifRefus();
        }

        #endregion

        #region Date

        #endregion

        #region Réponse

        private bool Verif_textBoxDetailReponse()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxDetailReponse, this._textBlockDetailReponse);
        }

        private void _textBoxDetailReponse_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_textBoxDetailReponse();
        }

        #endregion

        #endregion

        #endregion

        #region Verrouillages

        public void lectureSeule()
        {
            this.verrouillageDemande();
            this.verrouillageReponse();
        }

        public void verrouillageDemande()
        {
            this._datePickerDateDebutDemande.IsEnabled = false;
            this._datePickerDateFinDemande.IsEnabled = false;

            this._comboBoxMotifDemande.IsEnabled = false;
            this._comboBoxSalarie.IsEnabled = false;

            this._textBoxCommentaire.IsReadOnly = true;
        }

        public void verrouillageReponse()
        {
            this._checkBoxAccepteOui.IsEnabled = false;
            this._checkBoxAccepteNon.IsEnabled = false;

            this._comboBoxMotifRefus.IsEnabled = false;
            this._textBoxDetailReponse.IsReadOnly = true;
        }

        #endregion

        #region CheckBox

        private void _checkBoxAccepteOui_Checked(object sender, RoutedEventArgs e)
        {
            ((Conge)this.DataContext).Accepte = true;
            if (_checkBoxAccepteNon.IsChecked == true)
            {
                _checkBoxAccepteNon.IsChecked = false;
            }
            this._comboBoxMotifRefus.SelectedItem = null;
            this._comboBoxMotifRefus.IsEnabled = false;
            this.Verif_checkBoxs();
        }

        private void _checkBoxAccepteOui_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this._checkBoxAccepteNon.IsChecked == false)
            {
                ((Conge)this.DataContext).Accepte = null;
                this._comboBoxMotifRefus.SelectedItem = null;
                this._comboBoxMotifRefus.IsEnabled = false;
            }
            else
            {
                ((Conge)this.DataContext).Accepte = false;
                this._comboBoxMotifRefus.SelectedItem = null;
                this._comboBoxMotifRefus.IsEnabled = true;
            }
            this.Verif_checkBoxs();
        }

        private void _checkBoxAccepteNon_Checked(object sender, RoutedEventArgs e)
        {
            ((Conge)this.DataContext).Accepte = false;
            if (_checkBoxAccepteOui.IsChecked == true)
            {
                _checkBoxAccepteOui.IsChecked = false;
            }
            this._comboBoxMotifRefus.SelectedItem = null;
            this._comboBoxMotifRefus.IsEnabled = true;
            this.Verif_checkBoxs();
        }

        private void _checkBoxAccepteNon_Unchecked(object sender, RoutedEventArgs e)
        {
            ((Conge)this.DataContext).Accepte = null;
            if (this._checkBoxAccepteOui.IsChecked == false)
            {
                ((Conge)this.DataContext).Accepte = null;
            }
            else
            {
                ((Conge)this.DataContext).Accepte = true;
            }
            this._comboBoxMotifRefus.SelectedItem = null;
            this._comboBoxMotifRefus.IsEnabled = false;
            this.Verif_checkBoxs();
        }

        #endregion

        #region fenetre chargé

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;

            if (((Conge)this.DataContext).Accepte == true)
            {
                this._checkBoxAccepteOui.IsChecked = true;
            }
            if (((Conge)this.DataContext).Accepte == false)
            {
                this._checkBoxAccepteNon.IsChecked = true;
            }

            if (creation)
            {
                if (this.demande)
                {
                    ((Conge)this.DataContext).Demande_Fait_Le = DateTime.Today;
                    this.verrouillageReponse();
                    this._comboBoxSalarie.SelectedItem = ((App)App.Current)._connectedUser.Salarie_Interne1.Salarie;
                    ((Conge)this.DataContext).Salarie1 = ((App)App.Current)._connectedUser.Salarie_Interne1.Salarie;
                }
                else
                {
                    ((Conge)this.DataContext).Reponse_Fait_Le = DateTime.Today;
                    this.verrouillageDemande();
                    ((Conge)this.DataContext).Utilisateur = ((App)App.Current)._connectedUser;
                }
            }
            this._datePickerDateReponse.SelectedDate = ((Conge)this.DataContext).Reponse_Fait_Le;
            this._datePickerDateDemande.SelectedDate = ((Conge)this.DataContext).Demande_Fait_Le;
            this._textBoxNbJours.Text = ((Conge)this.DataContext).Nombre_Jours.ToString();

            if (this.verrouillerSalarie == true)
            {
                this._comboBoxSalarie.IsEnabled = false;
            }
        }
        #endregion

        #region fonctions

        private void Calculer_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._datePickerDateDebutDemande.SelectedDate != null && this._datePickerDateFinDemande.SelectedDate != null)
            {
                ObservableCollection<JourFerie> listToTest = new ObservableCollection<JourFerie>(((App)App.Current).mySitaffEntities.JourFerie.Where(jf => jf.Date_Fin != null));
                int nbDays = 0;
                DateTime tmp = DateTime.Parse(this._datePickerDateDebutDemande.SelectedDate.ToString());
                DateTime fin = DateTime.Parse(this._datePickerDateFinDemande.SelectedDate.ToString());
                while (tmp.Date <= fin.Date)
                {
                    bool test = true;
                    //if (tmp.DayOfWeek == DayOfWeek.Monday)
                    //{
                    //    test = true;
                    //}
                    //if (tmp.DayOfWeek == DayOfWeek.Tuesday)
                    //{
                    //    test = true;
                    //}
                    //if (tmp.DayOfWeek == DayOfWeek.Wednesday)
                    //{
                    //    test = true;
                    //}
                    //if (tmp.DayOfWeek == DayOfWeek.Thursday)
                    //{
                    //    test = true;
                    //}
                    //if (tmp.DayOfWeek == DayOfWeek.Friday)
                    //{
                    //    test = true;
                    //}
                    if (tmp.DayOfWeek == DayOfWeek.Saturday)
                    {
                        test = false;
                    }
                    if (tmp.DayOfWeek == DayOfWeek.Sunday)
                    {
                        test = false;
                    }
                    if (listToTest.Where(jf => jf.Date_Fin.Value.Year == tmp.Year && jf.Date_Fin.Value.Month == tmp.Month && jf.Date_Fin.Value.Day == tmp.Day).Count() > 0)
                    {
                        test = false;
                    }
                    if (test)
                    {
                        nbDays = nbDays + 1;
                    }
                    tmp = tmp.AddDays(1);
                }
                this._textBoxNbJours.Text = nbDays.ToString();
            }
        }

        #endregion

    }
}
