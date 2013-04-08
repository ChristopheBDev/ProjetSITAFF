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
using SitaffRibbon.Classes;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour RegieWindow.xaml
    /// </summary>
    public partial class RegieWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;
        public bool creation = false;

        #endregion

        #region Propriétés de dépendances

        public ObservableCollection<Versions> listVersions
        {
            get { return (ObservableCollection<Versions>)GetValue(listVersionsProperty); }
            set { SetValue(listVersionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listVersions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listVersionsProperty =
            DependencyProperty.Register("listVersions", typeof(ObservableCollection<Versions>), typeof(RegieWindow), new PropertyMetadata(null));

        
        public ObservableCollection<Contact> listContacts
        {
            get { return (ObservableCollection<Contact>)GetValue(listContactsProperty); }
            set { SetValue(listContactsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listContacts.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listContactsProperty =
            DependencyProperty.Register("listContacts", typeof(ObservableCollection<Contact>), typeof(RegieWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Salarie> listSalaries
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalariesProperty); }
            set { SetValue(listSalariesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalaries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalariesProperty =
            DependencyProperty.Register("listSalaries", typeof(ObservableCollection<Salarie>), typeof(RegieWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Affaire> listAffaires
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffairesProperty); }
            set { SetValue(listAffairesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaires.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffairesProperty =
            DependencyProperty.Register("listAffaires", typeof(ObservableCollection<Affaire>), typeof(RegieWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Client> listClients
        {
            get { return (ObservableCollection<Client>)GetValue(listClientsProperty); }
            set { SetValue(listClientsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listClients.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listClientsProperty =
            DependencyProperty.Register("listClients", typeof(ObservableCollection<Client>), typeof(RegieWindow), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public RegieWindow()
        {
            InitializeComponent();
            this.listAffaires = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
            this.listClients = new ObservableCollection<Client>(((App)App.Current).mySitaffEntities.Client.OrderBy(cli => cli.Entreprise.Libelle));
            this.listSalaries = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.Where(sal => sal.Chantier == true).OrderBy(sal => sal.Personne.Nom).ThenBy(sal => sal.Personne.Prenom));
            this.listVersions = new ObservableCollection<Versions>(((App)App.Current).mySitaffEntities.Versions.Where(ver => ver.Affaire1 != null));
            if (this._comboBoxClient.SelectedItem != null)
            {
                this.listContacts = new ObservableCollection<Contact>(((App)App.Current).mySitaffEntities.Contact.Where(con => ((Client)this._comboBoxClient.SelectedItem).Entreprise.Personne.Contains(con.Personne)).OrderBy(con => con.Personne.Nom).ThenBy(con => con.Personne.Prenom));
            }
            else
            {
                this.listContacts = new ObservableCollection<Contact>();
            }
        }

        #endregion

        #region boutons

        private void _buttonCalculer_Click(object sender, RoutedEventArgs e)
        {
            this.calculerPrix();
            this.calculAutoTotal();
        }

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.calculerPrix();
            this.calculAutoTotal();
            this.assuranceChiffres();
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

        #region Verifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!this.Verif_EnTete())
            {
                verif = false;
            }
            if (!Verif_Heures())
            {
                verif = false;
            }

            return verif;
        }

        #region En tête

        private bool Verif_EnTete()
        {
            bool verif = true;

            if (!this.Verif_datePickerDate_Debut())
            {
                verif = false;
            }
            if (!this.Verif_datePickerDate_Fin())
            {
                verif = false;
            }
            if (!this.Verif_textBoxNumero())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxAffaire())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxClient())
            {
                verif = false;
            }
            if (!Verif_comboBoxResponsable())
            {
                verif = false;
            }
            if (!Verif_textBoxNomClient())
            {
                verif = false;
            }
            if (!Verif_comboBoxVersions())
            {
                verif = false;
            }

            return verif;
        }

        #region Date Debut

        private bool Verif_datePickerDate_Debut()
        {
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDate_Debut, this._textBlockDate_Debut);
        }

        private void _datePickerDate_Debut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Verif_datePickerDate_Debut())
            {
                if (creation)
                {
                    this._datePickerDate_Fin.SelectedDate = (DateTime)this._datePickerDate_Debut.SelectedDate;
                    while (((DateTime)this._datePickerDate_Fin.SelectedDate).DayOfWeek != DayOfWeek.Friday)
                    {
                        this._datePickerDate_Fin.SelectedDate = ((DateTime)this._datePickerDate_Fin.SelectedDate).AddDays(1);
                    }
                }
            }
        }

        #endregion

        #region Date Fin

        private bool Verif_datePickerDate_Fin()
        {
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDate_Fin, this._textBlockDate_Fin, this._datePickerDate_Debut);
        }

        private void _datePickerDate_Fin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_datePickerDate_Fin();
        }

        #endregion

        #region Numero

        private bool Verif_textBoxNumero()
        {
            bool verif = true;

            if (this._textBoxNumero.Text.Trim().Length == 0)
            {
                verif = false;
            }
            else
            {
                foreach (Regie reg in ((App)App.Current).mySitaffEntities.Regie)
                {
                    if (((Regie)this.DataContext).Identifiant != reg.Identifiant || (Regie)this.DataContext != reg)
                    {
                        if (((Regie)this.DataContext).Numero != null)
                        {
                            if (((Regie)this.DataContext).Numero.ToLower() == reg.Numero.ToLower())
                            {
                                verif = false;
                            }
                        }
                    }
                }
			}
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._textBoxNumero, this._textBlockNumero, verif);

            return verif;
        }

        private void _textBoxNumero_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxNumero();
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
                this.miseAutoClient();
                this.listVersions = new ObservableCollection<Versions>(((Affaire)this._comboBoxAffaire.SelectedItem).Versions);
            }
        }

        private void miseAutoClient()
        {
            if (this._comboBoxAffaire.SelectedItem == null)
            {
                this._comboBoxClient.SelectedItem = null;
            }
            else
            {
                foreach (Versions v in ((Affaire)this._comboBoxAffaire.SelectedItem).Versions)
                {
                    this._comboBoxClient.SelectedItem = v.Devis1.Client2;
                }
            }
        }

        #endregion

        #region Client

        private bool Verif_comboBoxClient()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxClient, this._textBlockClient);
        }

        private void _comboBoxClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Verif_comboBoxClient())
            {
                if (((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1.Rue != null)
                {
                    this._textBoxClientAdresse.Text = ((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1.Rue;
                }
                else
                {
                    this._textBoxClientAdresse.Text = "Non renseigné";
                }
                if (((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1.Complement_Adresse != null)
                {
                    this._textBoxClientComplement.Text = ((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1.Complement_Adresse;
                }
                else
                {
                    this._textBoxClientComplement.Text = "";
                }
                if (((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1.Ville1 != null)
                {
                    this._textBoxClientVille.Text = ((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1.Ville1.Code_Postal + " - " + ((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1.Ville1.Libelle;
                }
                else
                {
                    this._textBoxClientVille.Text = "Non renseigné";
                }
                if (((Client)this._comboBoxClient.SelectedItem).Entreprise.Telephone != null)
                {
                    this._textBoxClientTelephone.Text = ((Client)this._comboBoxClient.SelectedItem).Entreprise.Telephone;
                }
                else
                {
                    this._textBoxClientTelephone.Text = "Non renseigné";
                }
                if (((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1.Ville1 != null && ((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1.Ville1.Pays1 != null)
                {
                    this._textBoxClientPays.Text = ((Client)this._comboBoxClient.SelectedItem).Entreprise.Adresse1.Ville1.Pays1.Libelle;
                }
                else
                {
                    this._textBoxClientPays.Text = "Non renseigné";
                }
            }
            else
            {
                this._textBoxClientAdresse.Text = "";
                this._textBoxClientComplement.Text = "";
                this._textBoxClientVille.Text = "";
                this._textBoxClientTelephone.Text = "";
                this._textBoxClientPays.Text = "";
            }
            this.listContacts = new ObservableCollection<Contact>();
            if (this._comboBoxClient.SelectedItem != null)
            {
                foreach (Personne c in ((Client)this._comboBoxClient.SelectedItem).Entreprise.Personne.Where(per => per.Contact != null))
                {
                    this.listContacts.Add(c.Contact);
                }
            }
        }

        #endregion

        #region NomClient

        private bool Verif_textBoxNomClient()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxContact, this._textBlockNomClient);
        }

        private void _comboBoxContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_textBoxNomClient();
        }

        #endregion

        #region Reponsable

        private bool Verif_comboBoxResponsable()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxResponsable, this._textBlockResponsable);
        }

        private void _comboBoxResponsable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxResponsable();
        }

        #endregion

        #region Versions

        private bool Verif_comboBoxVersions()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxVersions, this._textBlockVersions);
        }

        private void _comboBoxVersions_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxVersions();
            if (this._comboBoxVersions.SelectedItem != null)
            {
                if (((Versions)this._comboBoxVersions.SelectedItem).Affaire1 != null)
                {
                    this._comboBoxAffaire.SelectedItem = ((Versions)this._comboBoxVersions.SelectedItem).Affaire1;
                }
            }
        }

        #endregion

        #endregion

        #region Contenu

        #endregion

        #region Heures

        private bool Verif_Heures()
        {
            bool verif = true;

            if (!this.Verif_textBoxHeuresNormales())
            {
                verif = false;
            }
            if (!this.Verif_textBoxHeures25pct())
            {
                verif = false;
            }
            if (!this.Verif_textBoxHeures50pct())
            {
                verif = false;
            }
            if (!this.Verif_textBoxHeures100pct())
            {
                verif = false;
            }
            if (!this.Verif_textBoxHeures_Totales())
            {
                verif = false;
            }
            if (!this.Verif_textBoxTauxHoraire())
            {
                verif = false;
            }

            return verif;
        }

        #region HeuresNormales

        private bool Verif_textBoxHeuresNormales()
        {
            bool verif = true;

            if (this._textBoxHeuresNormales.Text.Trim().Length == 0)
            {
                verif = false;
                this._textBoxHeuresNormales.Text = "0";
            }
            else
            {
                double val;
                if (double.TryParse(this._textBoxHeuresNormales.Text, out val))
                {
                    verif = true;
                    int temp = (int)(val * 100);
                    this._textBoxHeuresNormales.Text = double.Parse((temp / 100.0).ToString()).ToString().Replace(".", ",");
                }
                else
                {
                    verif = false;
                }
            }
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._textBoxHeuresNormales, this._textBlockHeuresNormales, verif);
            return verif;
        }

        private void _textBoxHeuresNormales_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxHeuresNormales();
            calculAutoTotal();
        }

        #endregion

        #region Heures25pct

        private bool Verif_textBoxHeures25pct()
        {
            bool verif = true;

            if (this._textBoxHeures25pct.Text.Trim().Length == 0)
            {
                verif = false;
                this._textBoxHeures25pct.Text = "0";
            }
            else
            {
                double val;
                if (double.TryParse(this._textBoxHeures25pct.Text, out val))
                {
                    verif = true;
                    int temp = (int)(val * 100);
                    this._textBoxHeures25pct.Text = double.Parse((temp / 100.0).ToString()).ToString().Replace(".", ",");
                }
                else
                {
                    verif = false;
                }
            }
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._textBoxHeures25pct, this._textBlockHeures25pct, verif);
            return verif;
        }

        private void _textBoxHeures25pct_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxHeures25pct();
            calculAutoTotal();
        }

        #endregion

        #region Heures50pct

        private bool Verif_textBoxHeures50pct()
        {
            bool verif = true;

            if (this._textBoxHeures50pct.Text.Trim().Length == 0)
            {
                verif = false;
                this._textBoxHeures50pct.Text = "0";
            }
            else
            {
                double val;
                if (double.TryParse(this._textBoxHeures50pct.Text, out val))
                {
                    verif = true;
                    int temp = (int)(val * 100);
                    this._textBoxHeures50pct.Text = double.Parse((temp / 100.0).ToString()).ToString().Replace(".", ",");
                }
                else
                {
                    verif = false;
                }
            }
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._textBoxHeures50pct, this._textBlockHeures50pct, verif);
            return verif;
        }

        private void _textBoxHeures50pct_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxHeures50pct();
            calculAutoTotal();
        }

        #endregion

        #region Heures100pct

        private bool Verif_textBoxHeures100pct()
        {
            bool verif = true;

            if (this._textBoxHeures100pct.Text.Trim().Length == 0)
            {
                verif = false;
                this._textBoxHeures100pct.Text = "0";
            }
            else
            {
                double val;
                if (double.TryParse(this._textBoxHeures100pct.Text, out val))
                {
                    verif = true;
                    int temp = (int)(val * 100);
                    this._textBoxHeures100pct.Text = double.Parse((temp / 100.0).ToString()).ToString().Replace(".", ",");
                }
                else
                {
                    verif = false;
                }
            }
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._textBoxHeures100pct, this._textBlockHeures100pct, verif);

            return verif;
        }

        private void _textBoxHeures100pct_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxHeures100pct();
            calculAutoTotal();
        }

        #endregion

        #region Heures_Totales

        private bool Verif_textBoxHeures_Totales()
        {
            bool verif = true;

            if (this._textBoxHeuresTotales.Text.Trim().Length == 0)
            {
                verif = false;
            }
            else
            {
                double val;
                if (double.TryParse(this._textBoxHeuresTotales.Text, out val))
                {
                    if (double.TryParse(this._textBoxHeuresNormales.Text, out val) && double.TryParse(this._textBoxHeures25pct.Text, out val) && double.TryParse(this._textBoxHeures50pct.Text, out val) && double.TryParse(this._textBoxHeures100pct.Text, out val))
                    {
                        double total = double.Parse(this._textBoxHeuresNormales.Text) + double.Parse(this._textBoxHeures25pct.Text) + double.Parse(this._textBoxHeures50pct.Text) + double.Parse(this._textBoxHeures100pct.Text);
                        if (total == double.Parse(this._textBoxHeuresTotales.Text))
                        {
                            verif = true;   
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

			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._textBoxHeuresTotales, this._textBlockHeuresTotales, verif);

            return verif;
        }

        private void _textBoxHeures_Totales_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxHeures_Totales();
        }

        #endregion

        #region Taux_Horaire

        private bool Verif_textBoxTauxHoraire()
        {
            bool verif = true;

            if (this._textBoxTauxHoraire.Text.Trim().Length == 0)
            {
                verif = false;
                this._textBoxTauxHoraire.Text = "0";
            }
            else
            {
                double val;
                if (double.TryParse(this._textBoxTauxHoraire.Text, out val))
                {
                    verif = true;
                    int temp = (int)(val * 100);
                    this._textBoxTauxHoraire.Text = double.Parse((temp / 100.0).ToString()).ToString().Replace(".", ",");
                }
                else
                {
                    verif = false;
                }
            }
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._textBoxTauxHoraire, this._textBlockTauxHoraire, verif);
            return verif;
        }

        private void _textBoxTauxHoraire_LostFocus_1(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxTauxHoraire();
            calculAutoTotal();
        }

        #endregion

        #endregion

        #endregion

        #region Chechboxs

        #region Terminé

        private void _checkBoxTermine_Checked(object sender, RoutedEventArgs e)
        {
            ((Regie)this.DataContext).Termine = true;
        }

        private void _checkBoxTermine_Unchecked(object sender, RoutedEventArgs e)
        {
            ((Regie)this.DataContext).Termine = false;
        }

        #endregion

        #region Signe

        private void _checkBoxSigne_Checked(object sender, RoutedEventArgs e)
        {
            ((Regie)this.DataContext).Signe = true;
        }

        private void _checkBoxSigne_Unchecked(object sender, RoutedEventArgs e)
        {
            ((Regie)this.DataContext).Signe = false;
        }

        #endregion

        #endregion

        #region keyup

        private void _textBoxHeuresNormales_KeyUp_1(object sender, KeyEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
        }

        #endregion

        #region Fonctions

        private void calculerPrix()
        {
            double prixTotal = 0;
            double nbHeures = 0;

            foreach (Travail item in this._DataGridTravaux.Items.OfType<Travail>())
            {
                
                if (item.Prix != null)
                {
                    double val;
                    if (double.TryParse(item.Prix.ToString(), out val))
                    {
                        prixTotal = prixTotal + double.Parse(item.Prix.ToString());
                    }
                }

                if (item.Quantite_Heure != null)
                {
                    double val;
                    if (double.TryParse(item.Quantite_Heure.ToString(), out val))
                    {
                        nbHeures = nbHeures + double.Parse(item.Quantite_Heure.ToString());
                    }
                }
                
            }

            ((Regie)this.DataContext).Heures_Totales = nbHeures;
            ((Regie)this.DataContext).Prix_Total = prixTotal;
            this.Verif_textBoxHeures_Totales();
        }

        private void calculAutoTotal()
        {
            double result = 0;
            double val;
            if (double.TryParse(this._textBoxTauxHoraire.Text, out val))
            {
                if (this.Verif_textBoxHeures_Totales())
                {
                    double tx = double.Parse(this._textBoxTauxHoraire.Text);
                    result = (tx * double.Parse(this._textBoxHeuresNormales.Text)) + (tx * double.Parse(this._textBoxHeures25pct.Text) * 1.25) + (tx * double.Parse(this._textBoxHeures50pct.Text) * 1.50) + (tx * double.Parse(this._textBoxHeures100pct.Text) * 2);
                    ((Regie)this.DataContext).Prix_Heures = result;
                    //this._textBoxTotalHeures.Text = result.ToString();
                }
                else
                {
                    ((Regie)this.DataContext).Prix_Heures = 0;
                    //this._textBoxTotalHeures.Text = "0";
                }
            }
            else
            {
                ((Regie)this.DataContext).Prix_Heures = 0;
            }
        }

        private void assuranceChiffres()
        {
            //On s'assure que tous les chiffres sont bien enregistrés
            double val;
            if (Double.TryParse(_textBoxHeuresNormales.Text.Replace(".", ",").Replace(" ", ""), out val))
            {
                ((Regie)this.DataContext).Heures_Normales = Double.Parse(_textBoxHeuresNormales.Text.Replace(".", ",").Replace(" ", ""));
            }
            if (Double.TryParse(_textBoxHeures25pct.Text.Replace(".", ",").Replace(" ", ""), out val))
            {
                ((Regie)this.DataContext).Heures_25pct = Double.Parse(_textBoxHeures25pct.Text.Replace(".", ",").Replace(" ", ""));
            }
            if (Double.TryParse(_textBoxHeures50pct.Text.Replace(".", ",").Replace(" ", ""), out val))
            {
                ((Regie)this.DataContext).Heures_50pct = Double.Parse(_textBoxHeures50pct.Text.Replace(".", ",").Replace(" ", ""));
            }
            if (Double.TryParse(_textBoxHeures100pct.Text.Replace(".", ",").Replace(" ", ""), out val))
            {
                ((Regie)this.DataContext).Heures_100pct = Double.Parse(_textBoxHeures100pct.Text.Replace(".", ",").Replace(" ", ""));
            }
            if (Double.TryParse(_textBoxHeuresTotales.Text.Replace(".", ",").Replace(" ", ""), out val))
            {
                ((Regie)this.DataContext).Heures_Totales = Double.Parse(_textBoxHeuresTotales.Text.Replace(".", ",").Replace(" ", ""));
            }
        }

        #endregion

        #region lecture seule

        public void lectureSeule()
        {
            this._DataGridTravaux.IsReadOnly = true;

            this._textBoxHeuresNormales.IsReadOnly = true;
            this._textBoxHeures25pct.IsReadOnly = true;
            this._textBoxHeures50pct.IsReadOnly = true;
            this._textBoxHeures100pct.IsReadOnly = true;
            this._textBoxTauxHoraire.IsReadOnly = true;

            this._textBoxNumero.IsReadOnly = true;
            this._comboBoxContact.IsEnabled = true;

            this._datePickerDate_Debut.IsEnabled = false;
            this._datePickerDate_Fin.IsEnabled = false;

            this._comboBoxAffaire.IsEnabled = false;
            this._comboBoxClient.IsEnabled = false;

            this._checkBoxSigne.IsEnabled = false;
            this._checkBoxTermine.IsEnabled = false;
        }

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
            try
            {
                if (((Regie)this.DataContext).Utilisateur1 == null)
                {
                    ((Regie)this.DataContext).Utilisateur1 = ((App)App.Current)._connectedUser;
                }
            }
            catch (Exception) { }
            if (this._textBoxTauxHoraire.Text == "")
            {
                try
                {
                    ObservableCollection<Taux_Horaire> listTx = new ObservableCollection<Taux_Horaire>(((App)App.Current).mySitaffEntities.Taux_Horaire.Where(tx => tx.Date_Debut < DateTime.Today && tx.Date_Fin > DateTime.Today && tx.Entreprise_Mere1.Identifiant == (((App)App.Current)._connectedUser.Salarie_Interne1.Entreprise_Mere1.Identifiant)));
                    if (listTx.Count() > 0)
                    {
                        foreach (Taux_Horaire item in listTx)
                        {
                            this._textBoxTauxHoraire.Text = item.Valeur.ToString();
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        #endregion        

        private void _DataGridTravaux_KeyUp_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Quantite heures":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Quantite fourniture":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Prix Total fourniture":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
