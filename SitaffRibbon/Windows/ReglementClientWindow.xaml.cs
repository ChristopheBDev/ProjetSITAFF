using SitaffRibbon.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour ReglementClientWindow.xaml
    /// </summary>
    public partial class ReglementClientWindow : Window
    {

        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region Propd

        public ObservableCollection<Client> listClient
        {
            get { return (ObservableCollection<Client>)GetValue(listClientProperty); }
            set { SetValue(listClientProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listClient.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listClientProperty =
            DependencyProperty.Register("listClient", typeof(ObservableCollection<Client>), typeof(ReglementClientWindow), new PropertyMetadata(null));

        public ObservableCollection<Banque> listBanque
        {
            get { return (ObservableCollection<Banque>)GetValue(listBanqueProperty); }
            set { SetValue(listBanqueProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listBanque.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listBanqueProperty =
            DependencyProperty.Register("listBanque", typeof(ObservableCollection<Banque>), typeof(ReglementClientWindow), new PropertyMetadata(null));

        public ObservableCollection<Moyen_Reglement> listMoyenRglt
        {
            get { return (ObservableCollection<Moyen_Reglement>)GetValue(listMoyenRgltProperty); }
            set { SetValue(listMoyenRgltProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listMoyenRglt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listMoyenRgltProperty =
            DependencyProperty.Register("listMoyenRglt", typeof(ObservableCollection<Moyen_Reglement>), typeof(ReglementClientWindow), new PropertyMetadata(null));

        public ObservableCollection<Facture> listFacture
        {
            get { return (ObservableCollection<Facture>)GetValue(listFactureProperty); }
            set { SetValue(listFactureProperty, value); }
        }
        // Using a DependencyProperty as the backing store for listFacture.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFactureProperty =
            DependencyProperty.Register("listFacture", typeof(ObservableCollection<Facture>), typeof(ReglementClientWindow), new PropertyMetadata(null));

        #endregion

        #region Constructeur

        public ReglementClientWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Initialisation de la sécurité
            this.initialisationSecurite();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._comboBoxClient.Focus();
        }

        #region initialisation

        private void initialisationPropDependance()
        {
            listClient = new ObservableCollection<Client>(((App)App.Current).mySitaffEntities.Client.OrderBy(cli => cli.Entreprise.Libelle));
            listBanque = new ObservableCollection<Banque>(((App)App.Current).mySitaffEntities.Banque.OrderBy(ban => ban.Libelle));
            listMoyenRglt = new ObservableCollection<Moyen_Reglement>(((App)App.Current).mySitaffEntities.Moyen_Reglement.OrderBy(moy => moy.Libelle));
            listFacture = new ObservableCollection<Facture>();
            foreach (Facture item in ((App)App.Current).mySitaffEntities.Facture.Where(fac => fac.Facture_Client != null && fac.Proforma_Client == null).OrderBy(fac => fac.Numero))
            {
                if (item.restantDu != 0)
                {
                    listFacture.Add(item);
                }
            }
        }

        private void initialisationSecurite()
        {
            //Mise en place des droits sur les boutons et tabs   
            if (soloLecture)
            {
                lectureSeule();
            }
        }

        #endregion

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
            MAJListFacture();
            DateAuto();
            Calculer();
            AutoReference();
        }

        #endregion

        #region Boutons

        #region Boutons OK et Annuler

        private void _buttonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void _buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        #region Bouton Afficher Facture

        private void _buttonAfficherFacture_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._dataGridFacture.SelectedItems.Count == 1)
            {
                FactureWindow f = new FactureWindow();
                f.DataContext = (Facture)this._dataGridFacture.SelectedItem;
                f.soloLecture = true;

                f.ShowDialog();
            }
        }

        #endregion

        #region Calculer

        private void _buttonCalculer_Click_1(object sender, RoutedEventArgs e)
        {
            Calculer();
        }

        #endregion

        #region Bouton vidoir/Dévidoir

        private void _buttonGaucheDroite_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._dataGridFacture.SelectedItem != null && this._dataGridFacture.SelectedItems.Count > 0)
            {
				foreach (Facture item in this._dataGridFacture.SelectedItems)
				{
					Reglement_Client_Facture tmp = new Reglement_Client_Facture();
					tmp.Facture1 = item;
					tmp.Reglement_Client1 = (Reglement_Client)this.DataContext;
					tmp.Montant = tmp.Facture1.Net_A_Payer;
				}
                
                MAJListFacture();
                AutoReference();
                Calculer();
            }
        }

        private void _buttonDroiteGauche_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._dataGridRglt.SelectedItem != null && this._dataGridRglt.SelectedItems.Count > 0)
            {
				ObservableCollection<Reglement_Client_Facture> tmp = new ObservableCollection<Reglement_Client_Facture>();
				foreach (Reglement_Client_Facture item in this._dataGridRglt.SelectedItems)
				{
					tmp.Add(item);
				}
				foreach (Reglement_Client_Facture item in tmp)
				{
					this.listFacture.Add(item.Facture1);
					((Reglement_Client)this.DataContext).Reglement_Client_Facture.Remove(item);
				}
                
                this._dataGridRglt.Items.Refresh();
                AutoReference();
            }
        }

        #endregion

        #endregion

        #region Verifications

        private bool VerificationChamps()
        {
            bool verif = true;

            Calculer();

            if (!Verif_Banque())
            {
                verif = false;
            }
            if (!Verif_Client())
            {
                verif = false;
            }
            if (!Verif_DataGridRglt())
            {
                verif = false;
            }
            if (!Verif_Date())
            {
                verif = false;
            }
            if (!Verif_ModeRglt())
            {
                verif = false;
            }
            if (!Verif_MontantCalcule())
            {
                verif = false;
            }
            if (!Verif_MontantRglt())
            {
                verif = false;
            }
            if (!Verif_Reference())
            {
                verif = false;
            }

            return verif;
        }

        #region Champ Client

        private bool Verif_Client()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxClient, this._textBlockClient);
        }

        private void _comboBoxClient_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (Verif_Client())
            {
                MAJListFacture();
                AutoReference();
            }
        }

        #endregion

        #region Champ Date

        private bool Verif_Date()
        {
            return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateRglt, this._textBlockDateRglt);
        }

        private void _datePickerDateRglt_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Verif_Date();
        }

        #endregion

        #region Champ MontantRglt

        private bool Verif_MontantRglt()
        {
            return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxMontRglt, this._textBlockMontantRglt);
        }

        private void _textBoxMontRglt_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Verif_MontantRglt();
        }

        #endregion

        #region Champ Banque

        private bool Verif_Banque()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxBanque, this._textBlockBanque);
        }

        private void _comboBoxBanque_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Verif_Banque();
            AutoReference();
        }

        #endregion

        #region Champ ModeRglt

        private bool Verif_ModeRglt()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxModeRglt, this._textBlockModeRglt);
        }

        private void _comboBoxModeRglt_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Verif_ModeRglt();
            AutoReference();
        }

        #endregion

        #region Champ Reference

        private bool Verif_Reference()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxReference, this._textBlockReference);
        }

        private void _textBoxReference_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Verif_Reference();
        }

        #endregion

        #region Champ DataGrid

        private bool Verif_DataGridRglt()
        {
            bool verif = true;

            if (this._dataGridRglt.Items.Count == 0)
            {
                verif = false;
            }

            double tmp = 0;
            foreach (Reglement_Client_Facture item in ((Reglement_Client)this.DataContext).Reglement_Client_Facture)
            {
                if (item.Pourcentage != null)
                {
                    tmp += (double)item.Pourcentage;
                }
            }
            if (tmp > 100.001)
            {
                verif = false;
            }
            ((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridRglt, verif);

            return verif;
        }

        #endregion

        #region Champ MontantCalcule

        private bool Verif_MontantCalcule()
        {
            return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxMontantCalcule, this._textBlockMontantCalcule, this._textBoxMontRglt);
        }

        private void _textBoxMontantCalcule_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Verif_MontantCalcule();
        }

        #endregion

        #endregion

        #region Lecture seule

        public void lectureSeule()
        {
            this._textBoxMontantCalcule.IsEnabled = false;
            this._textBoxMontantRestant.IsEnabled = false;
            this._textBoxMontRglt.IsEnabled = false;
            this._textBoxReference.IsEnabled = false;

            this._comboBoxBanque.IsEnabled = false;
            this._comboBoxClient.IsEnabled = false;
            this._comboBoxModeRglt.IsEnabled = false;

            this._datePickerDateRglt.IsEnabled = false;

            this._buttonAfficherFacture.IsEnabled = false;
            this._buttonDroiteGauche.IsEnabled = false;
            this._buttonGaucheDroite.IsEnabled = false;
            this._buttonCalculer.IsEnabled = false;
        }

        #endregion

        #region Evenements

        #region KeyUp

        private void _textBoxMontant_KeyUp_1(object sender, KeyEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
        }

        #endregion

        private void _dataGridRglt_KeyUp_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Montant":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Pourcentage":
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

        private void _textBoxMontRglt_LostFocus_1(object sender, RoutedEventArgs e)
        {
            //if (Verif_MontantRglt() && ((Reglement_Client)this.DataContext).Reglement_Client_Facture.Count == 1 && this.listFacture.Count == 0)
            //{
            //    ((Reglement_Client)this.DataContext).Reglement_Client_Facture.First().Montant = double.Parse(this._textBoxMontRglt.Text);
            //}
        }
		
        #endregion

        #region Fonctions

        private void Calculer()
        {
            double result = 0;
            double result2 = 0;
            if (((Reglement_Client)this.DataContext).Montant != null)
            {
                foreach (Reglement_Client_Facture item in ((Reglement_Client)this.DataContext).Reglement_Client_Facture)
                {
                    if (item.Pourcentage != null && item.Pourcentage <= 100 && item.Pourcentage >= 0 && item.Montant.ToString() == "")
                    {
                        item.Montant = item.Pourcentage * ((Reglement_Client)this.DataContext).Montant / 100;
                    }
                    if (item.Montant.ToString() != "" && Verif_MontantRglt())
                    {
                        item.Pourcentage = item.Montant * 100 / ((Reglement_Client)this.DataContext).Montant;
                    }
                    if (item.Montant != null)
                    {
                        result += (double)item.Montant;
                    }
                }
                foreach (Facture item in this.listFacture)
                {
                    result2 += (double)item.restantDu;
                }
            }
            double temp = (result * 100);
            int tmp = (int)(temp);
            result = (double)tmp / 100;
            double tempp = result2 * 100;
            tmp = (int)(tempp);
            result2 = (double)tmp / 100;

            this._textBoxMontantCalcule.Text = result.ToString();
            this._textBoxMontantRestant.Text = result2.ToString(System.Globalization.CultureInfo.CurrentCulture);

            try
            {
                this._dataGridFacture.Items.Refresh();
            }
            catch (Exception) { }
            try
            {
                this._dataGridRglt.Items.Refresh();
            }
            catch (Exception) { }

            Verif_DataGridRglt();
            AutoReference();
        }

        private void DateAuto()
        {
            if (((Reglement_Client)this.DataContext).Date_Reglement == null)
            {
                ((Reglement_Client)this.DataContext).Date_Reglement = DateTime.Today;
            }
        }

        private void AutoReference()
        {
            ((Reglement_Client)this.DataContext).Commentaire = "";
            if (this._comboBoxClient.SelectedItem != null && this._comboBoxModeRglt.SelectedItem != null && this._comboBoxBanque.SelectedItem != null)
            {
                ((Reglement_Client)this.DataContext).Commentaire = ((Client)this._comboBoxClient.SelectedItem).Entreprise.Libelle + " "
                    + ((Moyen_Reglement)this._comboBoxModeRglt.SelectedItem).Libelle + " "
                    + ((Banque)this._comboBoxBanque.SelectedItem).Libelle;
            }
            else if (this._comboBoxClient.SelectedItem != null && this._comboBoxModeRglt.SelectedItem != null)
            {
                ((Reglement_Client)this.DataContext).Commentaire = ((Client)this._comboBoxClient.SelectedItem).Entreprise.Libelle + " " + ((Moyen_Reglement)this._comboBoxModeRglt.SelectedItem).Libelle;
            }
            if (((Reglement_Client)this.DataContext).Reglement_Client_Facture != null && ((Reglement_Client)this.DataContext).Reglement_Client_Facture.Count > 0)
            {
                foreach (Reglement_Client_Facture item in ((Reglement_Client)this.DataContext).Reglement_Client_Facture)
                {
                    ((Reglement_Client)this.DataContext).Commentaire += " " + item.Facture1.Numero;
                }
            }
        }

        #region Reglages PropDep

        private void MAJListFacture()
        {
            if (this._comboBoxClient.SelectedItem != null)
            {
                listFacture = new ObservableCollection<Facture>();
                foreach (Facture item in ((App)App.Current).mySitaffEntities.Facture.
                    Where(fac => fac.Facture_Client != null && fac.Proforma_Client == null).
                    Where(fac => fac.Client1.Identifiant == ((Client)this._comboBoxClient.SelectedItem).Identifiant).
                    OrderBy(fac => fac.Numero))
                {
                    if (item.restantDu != 0)
                    {
                        listFacture.Add(item);
                    }
                }
                if (((Reglement_Client)this.DataContext).Reglement_Client_Facture != null &&
                ((Reglement_Client)this.DataContext).Reglement_Client_Facture.Count > 0 &&
                ((Reglement_Client)this.DataContext).Reglement_Client_Facture.First() != null &&
                ((Reglement_Client)this.DataContext).Reglement_Client_Facture.First().Facture1 != null &&
                ((Reglement_Client)this.DataContext).Reglement_Client_Facture.First().Facture1.Client1 != null &&
                ((Reglement_Client)this.DataContext).Reglement_Client_Facture.First().Facture1.Client1.Identifiant != ((Client)this._comboBoxClient.SelectedItem).Identifiant)
                {
                    while (((Reglement_Client)this.DataContext).Reglement_Client_Facture.Count > 0)
                    {
                        ((Reglement_Client)this.DataContext).Reglement_Client_Facture.Remove(((Reglement_Client)this.DataContext).Reglement_Client_Facture.First());
                    }
                }
            }

            foreach (Reglement_Client_Facture item in ((Reglement_Client)this.DataContext).Reglement_Client_Facture)
            {
                if (((Reglement_Client)this.DataContext).Client1 == null)
                {
                    ((Reglement_Client)this.DataContext).Client1 = (Client)item.Facture1.Client1;
                }
                listFacture.Remove(item.Facture1);
            }
        }

        #endregion

        #endregion

    }
}
