using SitaffRibbon.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour RetourChantierWindow.xaml
    /// </summary>
    public partial class RetourChantierWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;
        public ImportRetourChantier importRetourChantier = null;

        #endregion

        #region Propd

        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(RetourChantierWindow), new PropertyMetadata(null));



        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(RetourChantierWindow), new PropertyMetadata(null));

        #endregion

        #region Constructeur

        public RetourChantierWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Initialisation de la sécurité
            this.initialisationSecurite();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);
        }

        #region initialisation

        private void initialisationPropDependance()
        {
            this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
            this.listSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.OrderBy(sal => sal.Personne.Nom).ThenBy(sal => sal.Personne.Prenom));
        }

        private void initialisationSecurite()
        {

        }

        #endregion

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
            if (this.soloLecture)
            {
                this.lectureSeule();
            }
            this.generationNumero();
        }

        #endregion

        #region Boutons

        #region Boutons OK et Annuler

        private void _buttonOK_Click_1(object sender, RoutedEventArgs e)
        {
            //Validation si cellule en édition
            try
            {
                this._dataGridContenu.CommitEdit();
            }
            catch (Exception)
            {

            }

            //Recalcul du contenu
            this.calculer();

            if (this.Verif_Generale())
            {
                try
                {
                    this.importRetourChantier.Close();
                    this.importRetourChantier = null;
                }
                catch (Exception) { }
                this.DialogResult = true;
                this.Close();
            }

        }

        private void _buttonAnnuler_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                this.importRetourChantier.Close();
                this.importRetourChantier = null;
            }
            catch (Exception) { }
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        #region Boutons datagrid

        private void _ButtonSupprimer_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._dataGridContenu.SelectedItem != null && this._dataGridContenu.SelectedItems.Count == 1 && this._dataGridContenu.SelectedItem.GetType() == typeof(Contenu_Retour_Chantier))
            {
                try
                {
                    ((Contenu_Retour_Chantier)this._dataGridContenu.SelectedItem).Reference = "";
                    ((Contenu_Retour_Chantier)this._dataGridContenu.SelectedItem).Designation = "";
                    ((Contenu_Retour_Chantier)this._dataGridContenu.SelectedItem).Quantite = 0;
                    ((Contenu_Retour_Chantier)this._dataGridContenu.SelectedItem).Prix = 0;
                    ((Contenu_Retour_Chantier)this._dataGridContenu.SelectedItem).Prix_Total = 0;
                }
                catch (Exception) { }
                Contenu_Retour_Chantier item = (Contenu_Retour_Chantier)this._dataGridContenu.SelectedItem;
                try
                {
                    ((Retour_Chantier)this.DataContext).Contenu_Retour_Chantier.Remove(item);
                    item.Retour_Chantier = null;
                    ((App)App.Current).mySitaffEntities.Contenu_Retour_Chantier.DeleteObject(item);
                }
                catch (Exception)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Contenu_Retour_Chantier.DeleteObject(item);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            this._dataGridContenu.Items.Remove(item);
                        }
                        catch (Exception) { }
                    }
                }
            }
            else
            {
                if (this._dataGridContenu.SelectedItems.Count != 0)
                {
                    ObservableCollection<Contenu_Retour_Chantier> toRemove = new ObservableCollection<Contenu_Retour_Chantier>();
                    foreach (Contenu_Retour_Chantier item in this._dataGridContenu.SelectedItems.OfType<Contenu_Retour_Chantier>())
                    {
                        toRemove.Add(item);
                    }
                    foreach (Contenu_Retour_Chantier item in toRemove)
                    {
                        try
                        {
                            item.Reference = "";
                            item.Designation = "";
                            item.Quantite = 0;
                            item.Prix = 0;
                            item.Prix_Total = 0;
                        }
                        catch (Exception) { }
                        try
                        {
                            ((Retour_Chantier)this.DataContext).Contenu_Retour_Chantier.Remove(item);
                            item.Retour_Chantier1 = null;
                            ((App)App.Current).mySitaffEntities.Contenu_Retour_Chantier.DeleteObject(item);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Contenu_Retour_Chantier.DeleteObject(item);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    this._dataGridContenu.Items.Remove(item);
                                }
                                catch (Exception) { }
                            }
                        }
                    }
                }
            }
            this._dataGridContenu.Items.Refresh();
        }

        private void _ButtonColler_Click_1(object sender, RoutedEventArgs e)
        {
            CopierColler ClassPaste = new CopierColler();
            ObservableCollection<Contenu_Retour_Chantier> listToAdd = ClassPaste.PasteDataRetourChantierWindow();
            if (listToAdd != null)
            {
                foreach (Contenu_Retour_Chantier ccf in listToAdd)
                {
                    //((Commande_Fournisseur)this.DataContext).Contenu_Commande_Fournisseur.Add(ccf);
                    //((App)App.Current).mySitaffEntities.AddToContenu_Commande_Fournisseur(ccf);
                    ccf.Retour_Chantier1 = (Retour_Chantier)this.DataContext;
                }
                this._dataGridContenu.Items.Refresh();
                this.calculer();
            }
        }

        private void _buttonCalculer_Click_1(object sender, RoutedEventArgs e)
        {
            this.calculer();
        }

        #endregion

        #endregion

        #region Vérifications

        private bool Verif_Generale()
        {
            bool verif = true;

            if (!this.Verif_comboBoxAffaire())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxResponsableChantier())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxSalarie())
            {
                verif = false;
            }
            if (!this.Verif_textBoxDate_Retour())
            {
                verif = false;
            }
            if (!this.Verif_textBoxDesignation())
            {
                verif = false;
            }
            if (!this.Verif_textBoxNumero())
            {
                verif = false;
            }

            return verif;
        }

        #region En-tête

        #region Affaire

        private bool Verif_comboBoxAffaire()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxAffaire, this._textBlockAffaire);
        }

        private void _comboBoxAffaire_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxAffaire();
        }

        #endregion

        #region Salarie

        private bool Verif_comboBoxSalarie()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxSalarie, this._textBlockSalarie);
        }

        private void _comboBoxSalarie_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxSalarie();
        }

        #endregion

        #region Responsable chantier

        private bool Verif_comboBoxResponsableChantier()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxResponsableChantier, this._textBlockResponsableChantier);
        }

        private void _comboBoxResponsableChantier_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxResponsableChantier();
        }

        #endregion

        #region Date retour

        private bool Verif_textBoxDate_Retour()
        {
            return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._textBoxDate_Retour, this._textBlockDate_Retour);
        }

        private void _textBoxDate_Retour_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_textBoxDate_Retour();
        }

        #endregion

        #region Designation

        private bool Verif_textBoxDesignation()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxDesignation, this._textBlockDesignation);
        }

        private void _textBoxDesignation_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            this.Verif_textBoxDesignation();
        }

        #endregion

        #region Numéro

        private bool Verif_textBoxNumero()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxNumero, this._textBlockNumero);
        }

        private void _textBoxNumero_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            this.Verif_textBoxNumero();
        }

        #endregion

        #endregion

        #endregion

        #region Fonctions

        #region Calculer

        public void calculer()
        {
            double total = 0;

            foreach (Contenu_Retour_Chantier cont in this._dataGridContenu.Items.OfType<Contenu_Retour_Chantier>())
            {
                try
                {
                    cont.Prix_Total = cont.Quantite * cont.Prix;
                    total = total + double.Parse(cont.Prix_Total.ToString());
                }
                catch (Exception) { }
            }
            this._textBoxMontant.Text = total.ToString();
            ((Retour_Chantier)this.DataContext).Montant = total;
        }

        #endregion

        public void generationNumero()
        {
            if (((Retour_Chantier)this.DataContext).Numero == null || ((Retour_Chantier)this.DataContext).Numero == "")
            {
                String numTemp = "";
                numTemp = "RC" + "-";

                String year = DateTime.Today.Year.ToString();
                String mois = DateTime.Today.Month.ToString();
                int i = 1;
                foreach (char c in year)
                {
                    if (i == 3 || i == 4)
                    {
                        numTemp = numTemp + c;
                    }
                    i = i + 1;
                }
                int nbCaracteres = 0;
                foreach (Char c in mois)
                {
                    nbCaracteres++;
                }
                if (nbCaracteres == 1)
                {
                    numTemp += "0";
                }
                numTemp = numTemp + mois + "-";

                String incrementToPut = "001";

                ObservableCollection<Retour_Chantier> toTest = new ObservableCollection<Retour_Chantier>(((App)App.Current).mySitaffEntities.Retour_Chantier.Where(com => com.Numero.Contains(numTemp)));
                if (toTest.Count() == 0)
                {

                }
                else
                {
                    ObservableCollection<int> lesEntiersPourIncr = new ObservableCollection<int>();
                    int PlusGrand = 0;
                    foreach (Retour_Chantier item in toTest)
                    {
                        int test;
                        if (int.TryParse(item.Numero.Replace(numTemp, ""), out test))
                        {
                            lesEntiersPourIncr.Add(int.Parse(item.Numero.Replace(numTemp, "")));
                        }
                    }
                    foreach (int entier in lesEntiersPourIncr)
                    {
                        if (entier > PlusGrand)
                        {
                            PlusGrand = entier;
                        }
                    }
                    PlusGrand = PlusGrand + 1;
                    incrementToPut = PlusGrand.ToString();
                    String tempIncrement = "";
                    nbCaracteres = 0;
                    foreach (Char c in incrementToPut)
                    {
                        nbCaracteres++;
                    }
                    if (nbCaracteres == 1)
                    {
                        tempIncrement += "00";
                    }
                    if (nbCaracteres == 2)
                    {
                        tempIncrement += "0";
                    }
                    tempIncrement = tempIncrement + incrementToPut;
                    incrementToPut = tempIncrement;
                }
                numTemp = numTemp + incrementToPut;

                ((Retour_Chantier)this.DataContext).Numero = numTemp;
                this._textBoxNumero.Text = numTemp;
            }
        }

        private void _buttonImporter_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._comboBoxAffaire.SelectedItem != null)
            {
                try
                {
                    this.importRetourChantier.Close();
                    this.importRetourChantier = null;
                }
                catch (Exception) { }
                this.importRetourChantier = new ImportRetourChantier(((Affaire)this._comboBoxAffaire.SelectedItem).Identifiant);
                this.importRetourChantier.retourChantierWindow = this;
                this.importRetourChantier.Show();
            }
            else
            {
                MessageBox.Show("Veuillez au préalable renseigner une affaire");
            }
        }

        #endregion

        #region Lecture seule

        public void lectureSeule()
        {
            this._comboBoxAffaire.IsEnabled = false;
            this._comboBoxSalarie.IsEnabled = false;
            this._comboBoxResponsableChantier.IsEnabled = false;

            this._textBoxNumero.IsReadOnly = true;
            this._textBoxMontant.IsReadOnly = true;

            this._textBoxDate_Retour.IsEnabled = false;

            this._buttonCalculer.IsEnabled = false;
            this._ButtonColler.IsEnabled = false;
            this._ButtonSupprimer.IsEnabled = false;

            this._dataGridContenu.IsReadOnly = true;
        }

        #endregion

        #region Evenements

        private void _dataGridContenuSupplementaire_KeyUp(object sender, KeyEventArgs e)
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
                        case "P.U.":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Prix Total":
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

    }
}
