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
    /// Logique d'interaction pour SortieAtelierWindow.xaml
    /// </summary>
    public partial class SortieAtelierWindow : Window
    {

        #region Attributs

        public bool soloLecture = false;

        public ShopCommandeWindow shopCommandeWindow = null;
        public ImportCommandeWindow importCommandeWindow = null;

        #endregion

        #region Propd



        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(SortieAtelierWindow), new PropertyMetadata(null));



        public ObservableCollection<Fournisseur> listFournisseur
        {
            get { return (ObservableCollection<Fournisseur>)GetValue(listFournisseurProperty); }
            set { SetValue(listFournisseurProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listFournisseur.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFournisseurProperty =
            DependencyProperty.Register("listFournisseur", typeof(ObservableCollection<Fournisseur>), typeof(SortieAtelierWindow), new PropertyMetadata(null));



        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(SortieAtelierWindow), new PropertyMetadata(null));



        public ObservableCollection<Personne> listPersonne
        {
            get { return (ObservableCollection<Personne>)GetValue(listPersonneProperty); }
            set { SetValue(listPersonneProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listPersonne.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listPersonneProperty =
            DependencyProperty.Register("listPersonne", typeof(ObservableCollection<Personne>), typeof(SortieAtelierWindow), new PropertyMetadata(null));



        #endregion

        #region Constructeur

        public SortieAtelierWindow()
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
            this.listFournisseur = new ObservableCollection<Fournisseur>(((App)App.Current).mySitaffEntities.Fournisseur.OrderBy(fou => fou.Entreprise.Libelle));
            this.listPersonne = new ObservableCollection<Personne>(((App)App.Current).mySitaffEntities.Personne.OrderBy(per => per.Nom).ThenBy(per => per.Prenom));
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

        private void _buttonOk_Click(object sender, RoutedEventArgs e)
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
                this.DialogResult = true;
                try
                {
                    this.shopCommandeWindow.Close();
                    this.shopCommandeWindow = null;
                }
                catch (Exception) { }
                try
                {
                    this.importCommandeWindow.Close();
                    this.importCommandeWindow = null;
                }
                catch (Exception) { }
                this.Close();
            }

        }

        private void _buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            try
            {
                this.shopCommandeWindow.Close();
                this.shopCommandeWindow = null;
            }
            catch (Exception) { }
            try
            {
                this.importCommandeWindow.Close();
                this.importCommandeWindow = null;
            }
            catch (Exception) { }
            this.Close();
        }

        #endregion

        #region Boutons datagrid

        private void _ButtonSupprimer_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._dataGridContenu.SelectedItem != null && this._dataGridContenu.SelectedItems.Count == 1)
            {
                try
                {
                    ((Contenu_Sortie_Atelier)this._dataGridContenu.SelectedItem).Reference = "";
                    ((Contenu_Sortie_Atelier)this._dataGridContenu.SelectedItem).Designation = "";
                    ((Contenu_Sortie_Atelier)this._dataGridContenu.SelectedItem).Quantite = 0;
                    ((Contenu_Sortie_Atelier)this._dataGridContenu.SelectedItem).Prix = 0;
                    ((Contenu_Sortie_Atelier)this._dataGridContenu.SelectedItem).Prix_Total = 0;
                    ((Contenu_Sortie_Atelier)this._dataGridContenu.SelectedItem).Prix_Remise = 0;
                }
                catch (Exception) { }
                Contenu_Sortie_Atelier item = (Contenu_Sortie_Atelier)this._dataGridContenu.SelectedItem;
                try
                {
                    ((Sortie_Atelier)this.DataContext).Contenu_Sortie_Atelier.Remove(item);
                    item.Sortie_Atelier1 = null;
                    ((App)App.Current).mySitaffEntities.Contenu_Sortie_Atelier.DeleteObject(item);
                }
                catch (Exception)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Contenu_Sortie_Atelier.DeleteObject(item);
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
                    ObservableCollection<Contenu_Sortie_Atelier> toRemove = new ObservableCollection<Contenu_Sortie_Atelier>();
                    foreach (Contenu_Sortie_Atelier item in this._dataGridContenu.SelectedItems.OfType<Contenu_Sortie_Atelier>())
                    {
                        toRemove.Add(item);
                    }
                    foreach (Contenu_Sortie_Atelier item in toRemove)
                    {
                        try
                        {
                            item.Reference = "";
                            item.Designation = "";
                            item.Quantite = 0;
                            item.Prix = 0;
                            item.Prix_Total = 0;
                            item.Prix_Remise = 0;
                        }
                        catch (Exception) { }
                        try
                        {
                            ((Sortie_Atelier)this.DataContext).Contenu_Sortie_Atelier.Remove(item);
                            item.Sortie_Atelier1 = null;
                            ((App)App.Current).mySitaffEntities.Contenu_Sortie_Atelier.DeleteObject(item);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Contenu_Sortie_Atelier.DeleteObject(item);
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
            ObservableCollection<Contenu_Sortie_Atelier> listToAdd = ClassPaste.PasteDataSortieAtelierWindow();
            if (listToAdd != null)
            {
                foreach (Contenu_Sortie_Atelier ccf in listToAdd)
                {
                    //((Commande_Fournisseur)this.DataContext).Contenu_Commande_Fournisseur.Add(ccf);
                    //((App)App.Current).mySitaffEntities.AddToContenu_Commande_Fournisseur(ccf);
                    ccf.Sortie_Atelier1 = (Sortie_Atelier)this.DataContext;
                }
                this._dataGridContenu.Items.Refresh();
                this.calculer();
            }
        }

        private void _buttonCalculer_Click(object sender, RoutedEventArgs e)
        {
            this.calculer();
        }

        private void _buttonShop_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                this.shopCommandeWindow.Close();
                this.shopCommandeWindow = null;
            }
            catch (Exception) { }
            this.shopCommandeWindow = new ShopCommandeWindow();
            try
            {
                this.shopCommandeWindow._ComboBoxFournisseur.SelectedItem = this._comboBoxFournisseur.SelectedItem;
            }
            catch (Exception) { }
            this.shopCommandeWindow.sortieAtelierWindow = this;
            this.shopCommandeWindow.Show();
        }

        private void _buttonContenuCommande_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                this.importCommandeWindow.Close();
                this.importCommandeWindow = null;
            }
            catch (Exception) { }
            this.importCommandeWindow = new ImportCommandeWindow();
            try
            {
                this.importCommandeWindow._ComboBoxFournisseur.SelectedItem = this._comboBoxFournisseur.SelectedItem;
            }
            catch (Exception) { }
            this.importCommandeWindow.sortieAtelierWindow = this;
            this.importCommandeWindow.Show();
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
            if (!this.Verif_comboBoxFournisseur())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxContact())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxDemandeur())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxSalarie())
            {
                verif = false;
            }
            if (!this.Verif_textBoxDate_Sortie())
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
            if (this._checkBoxAffaire.IsChecked == true)
            {
                return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxAffaire, this._textBlockAffaire);
            }
            else
            {
                return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxAffaire, this._textBlockAffaire);
            }
        }

        private void _comboBoxAffaire_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxAffaire();
        }

        #endregion

        #region Fournisseur

        private bool Verif_comboBoxFournisseur()
        {
            if (this._checkBoxFournisseur.IsChecked == true)
            {
                return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxFournisseur, this._textBlockFournisseur);
            }
            else
            {
                return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxFournisseur, this._textBlockFournisseur);
            }
        }

        private void _comboBoxFournisseur_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxFournisseur();
            if (this._comboBoxFournisseur.SelectedItem != null)
            {
                this._comboBoxContact.IsEnabled = true;
                //Renseigne les contacts du fournisseur
                this.listPersonne = new ObservableCollection<Personne>(((Fournisseur)this._comboBoxFournisseur.SelectedItem).Entreprise.Personne.Where(per => per.Contact != null));
            }
            else
            {
                this._comboBoxContact.IsEnabled = false;
            }
        }

        #endregion

        #region Contact fournisseur

        private bool Verif_comboBoxContact()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._comboBoxContact.SelectedItem == null)
            {
                if (this._checkBoxFournisseur.IsChecked == true)
                {
                    verif = false;
                    this._textBlockContact.Foreground = Brushes.Red;
                    this._comboBoxContact.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._textBlockContact.Foreground = Brushes.Green;
                    this._comboBoxContact.Background = vert;
                }
            }
            else
            {
                verif = true;
                this._textBlockContact.Foreground = Brushes.Green;
                this._comboBoxContact.Background = vert;
            }

            return verif;
        }

        private void _comboBoxContact_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxContact();
        }

        #endregion

        #region Demandeur

        private bool Verif_comboBoxDemandeur()
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
                this._textBlockDemandeur.Foreground = Brushes.Red;
                this._comboBoxDemandeur.Background = rouge;
            }
            else
            {
                verif = true;
                this._textBlockDemandeur.Foreground = Brushes.Green;
                this._comboBoxDemandeur.Background = vert;
            }

            return verif;
        }

        private void _comboBoxDemandeur_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxDemandeur();
        }

        #endregion

        #region Salarie

        private bool Verif_comboBoxSalarie()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._comboBoxSalarie.SelectedItem == null)
            {
                verif = false;
                this._textBlockSalarie.Foreground = Brushes.Red;
                this._comboBoxSalarie.Background = rouge;
            }
            else
            {
                verif = true;
                this._textBlockSalarie.Foreground = Brushes.Green;
                this._comboBoxSalarie.Background = vert;
            }

            return verif;
        }

        private void _comboBoxSalarie_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxSalarie();
        }

        #endregion

        #region Date sortie

        private bool Verif_textBoxDate_Sortie()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._textBoxDate_Sortie.SelectedDate == null)
            {
                verif = false;
                this._textBlockDate_Sortie.Foreground = Brushes.Red;
                this._textBoxDate_Sortie.Background = rouge;
            }
            else
            {
                verif = true;
                this._textBlockDate_Sortie.Foreground = Brushes.Green;
                this._textBoxDate_Sortie.Background = vert;
            }

            return verif;
        }

        private void _textBoxDate_Sortie_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_textBoxDate_Sortie();
        }

        #endregion

        #region Designation

        private bool Verif_textBoxDesignation()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._textBoxDesignation.Text.Trim().Length == 0)
            {
                verif = false;
                this._textBlockDesignation.Foreground = Brushes.Red;
                this._textBoxDesignation.Background = rouge;
            }
            else
            {
                verif = true;
                this._textBlockDesignation.Foreground = Brushes.Green;
                this._textBoxDesignation.Background = vert;
            }

            return verif;
        }

        private void _textBoxDesignation_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            this.Verif_textBoxDesignation();
        }

        #endregion

        #region Numéro

        private bool Verif_textBoxNumero()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._textBoxNumero.Text.Trim().Length == 0)
            {
                verif = false;
                this._textBlockNumero.Foreground = Brushes.Red;
                this._textBoxNumero.Background = rouge;
            }
            else
            {
                verif = true;
                this._textBlockNumero.Foreground = Brushes.Green;
                this._textBoxNumero.Background = vert;
            }

            return verif;
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

            foreach (Contenu_Sortie_Atelier cont in this._dataGridContenu.Items.OfType<Contenu_Sortie_Atelier>())
            {
                try
                {
                    cont.Prix_Total = cont.Quantite * cont.Prix_Remise;
                    total = total + double.Parse(cont.Prix_Total.ToString());
                }
                catch (Exception) { }
            }
            this._textBoxMontant.Text = total.ToString();
            ((Sortie_Atelier)this.DataContext).Montant = total;
        }

        #endregion

        public void generationNumero()
        {
            if (((Sortie_Atelier)this.DataContext).Numero == null || ((Sortie_Atelier)this.DataContext).Numero == "")
            {
                String numTemp = "";
                numTemp = "SA" + "-";

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

                ObservableCollection<Sortie_Atelier> toTest = new ObservableCollection<Sortie_Atelier>(((App)App.Current).mySitaffEntities.Sortie_Atelier.Where(com => com.Numero.Contains(numTemp)));
                if (toTest.Count() == 0)
                {

                }
                else
                {
                    ObservableCollection<int> lesEntiersPourIncr = new ObservableCollection<int>();
                    int PlusGrand = 0;
                    foreach (Sortie_Atelier item in toTest)
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

                ((Sortie_Atelier)this.DataContext).Numero = numTemp;
                this._textBoxNumero.Text = numTemp;
            }
        }

        #endregion

        #region Lecture seule

        public void lectureSeule()
        {
            this._checkBoxAffaire.IsEnabled = false;
            this._checkBoxFournisseur.IsEnabled = false;

            this._comboBoxAffaire.IsEnabled = false;
            this._comboBoxContact.IsEnabled = false;
            this._comboBoxFournisseur.IsEnabled = false;
            this._comboBoxSalarie.IsEnabled = false;
            this._comboBoxDemandeur.IsEnabled = false;

            this._textBoxNumero.IsReadOnly = true;
            this._textBoxMontant.IsReadOnly = true;

            this._textBoxDate_Sortie.IsEnabled = false;

            this._buttonCalculer.IsEnabled = false;
            this._ButtonColler.IsEnabled = false;
            this._buttonContenuCommande.IsEnabled = false;
            this._buttonShop.IsEnabled = false;
            this._ButtonSupprimer.IsEnabled = false;

            this._dataGridContenu.IsReadOnly = true;
        }

        #endregion

        #region CheckBox

        private void _checkBoxAffaire_Checked_1(object sender, RoutedEventArgs e)
        {
            //Décoche l'autre
            this._checkBoxFournisseur.IsChecked = false;
            //Verrouille l'autre
            this._comboBoxContact.SelectedItem = null;
            this._comboBoxContact.IsEnabled = false;
            this._comboBoxFournisseur.SelectedItem = null;
            this._comboBoxFournisseur.IsEnabled = false;
            //Se déverrouille
            this._comboBoxAffaire.IsEnabled = true;
        }

        private void _checkBoxAffaire_Unchecked_1(object sender, RoutedEventArgs e)
        {
            //Se vérrouille
            this._comboBoxAffaire.SelectedItem = null;
            this._comboBoxAffaire.IsEnabled = false;
        }

        private void _checkBoxFournisseur_Checked_1(object sender, RoutedEventArgs e)
        {
            //Décoche l'autre
            this._checkBoxAffaire.IsChecked = false;
            //Vérrouille l'autre
            this._comboBoxAffaire.SelectedItem = null;
            this._comboBoxAffaire.IsEnabled = false;
            //Se déverrouille
            this._comboBoxFournisseur.IsEnabled = true;
        }

        private void _checkBoxFournisseur_Unchecked_1(object sender, RoutedEventArgs e)
        {
            //Se vérrouille
            this._comboBoxContact.SelectedItem = null;
            this._comboBoxContact.IsEnabled = false;
            this._comboBoxFournisseur.SelectedItem = null;
            this._comboBoxFournisseur.IsEnabled = false;
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
                        case "P.U. Remisé":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        case "Prix Total Remisé":
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
