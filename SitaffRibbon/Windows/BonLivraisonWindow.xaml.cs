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
    /// Logique d'interaction pour BonLivraisonWindow.xaml
    /// </summary>
    public partial class BonLivraisonWindow : Window
    {

        #region Attributs

        public bool soloLecture = false;
        public bool bonLivraison = true;

        public ShopCommandeWindow shopCommandeWindow = null;

        #endregion

        #region propd

        public ObservableCollection<Fournisseur> listFournisseurs
        {
            get { return (ObservableCollection<Fournisseur>)GetValue(listFournisseursProperty); }
            set { SetValue(listFournisseursProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listFournisseurs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFournisseursProperty =
            DependencyProperty.Register("listFournisseurs", typeof(ObservableCollection<Fournisseur>), typeof(BonLivraisonWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Contenu_Commande_Fournisseur> listContenu
        {
            get { return (ObservableCollection<Contenu_Commande_Fournisseur>)GetValue(listContenuProperty); }
            set { SetValue(listContenuProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listContenu.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listContenuProperty =
            DependencyProperty.Register("listContenu", typeof(ObservableCollection<Contenu_Commande_Fournisseur>), typeof(BonLivraisonWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Bon_Livraison_Contenu_Commande> listProduits
        {
            get { return (ObservableCollection<Bon_Livraison_Contenu_Commande>)GetValue(listProduitsProperty); }
            set { SetValue(listProduitsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listProduits.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listProduitsProperty =
            DependencyProperty.Register("listProduits", typeof(ObservableCollection<Bon_Livraison_Contenu_Commande>), typeof(BonLivraisonWindow), new UIPropertyMetadata(null));


        #endregion

        #region Constructeur

        public BonLivraisonWindow()
        {
            InitializeComponent();

            //Création du menu de clic droit sur le datagrid
            this.creationMenuClicDroit();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._textBoxNumero.Focus();
        }

        #region initialisations

        private void initialisationPropDependance()
        {
            this.listFournisseurs = new ObservableCollection<Fournisseur>(((App)App.Current).mySitaffEntities.Fournisseur.OrderBy(fou => fou.Entreprise.Libelle));
        }

        #endregion

        #endregion

        #region Boutons

        #region Boutons OK et Annuler

        private void _buttonOkBonLivraison_Click(object sender, RoutedEventArgs e)
        {
            this.calculDataGridContenuSupp();
            if (this.VerificationChamps())
            {
                this.DialogResult = true;
                try
                {
                    this.shopCommandeWindow.Close();
                }
                catch (Exception) { }
                this.Close();
            }
        }

        private void _buttonAnnulerBonLivraison_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            try
            {
                this.shopCommandeWindow.Close();
            }
            catch (Exception) { }
            this.Close();
        }

        #endregion

        #region Boutons Flèches

        private void _buttonGaucheDroite_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridGauche.SelectedItem != null && this._dataGridGauche.SelectedItems.Count == 1)
            {
                Bon_Livraison_Contenu_Commande temp = new Bon_Livraison_Contenu_Commande();
                temp.Contenu_Commande_Fournisseur = (Contenu_Commande_Fournisseur)this._dataGridGauche.SelectedItem;
                temp.Quantite = ((Contenu_Commande_Fournisseur)this._dataGridGauche.SelectedItem).QuantiteRestante;
                ((Bon_Livraison)this.DataContext).Bon_Livraison_Contenu_Commande.Add(temp);
            }
            if (this._dataGridGauche.SelectedItem != null && this._dataGridGauche.SelectedItems.Count > 1)
            {
                foreach (Contenu_Commande_Fournisseur item in this._dataGridGauche.SelectedItems)
                {
                    Bon_Livraison_Contenu_Commande temp = new Bon_Livraison_Contenu_Commande();
                    temp.Contenu_Commande_Fournisseur = item;
                    temp.Quantite = item.QuantiteRestante;
                    ((Bon_Livraison)this.DataContext).Bon_Livraison_Contenu_Commande.Add(temp);
                }
            }
            this.RefreshDGDroit();
            this.listContenu = ((Bon_Livraison)this.DataContext).ListeContenuDisponible;
            this._dataGridGauche.Items.Refresh();
        }

        private void _buttonDroiteGauche_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridDroit.SelectedItem != null && this._dataGridDroit.SelectedItems.Count == 1)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Bon_Livraison_Contenu_Commande.DeleteObject((Bon_Livraison_Contenu_Commande)this._dataGridDroit.SelectedItem);
                }
                catch (Exception)
                {
                    try
                    {
                        ((Bon_Livraison)this.DataContext).Bon_Livraison_Contenu_Commande.Remove((Bon_Livraison_Contenu_Commande)this._dataGridDroit.SelectedItem);
                        ((App)App.Current).mySitaffEntities.Detach((Bon_Livraison_Contenu_Commande)this._dataGridDroit.SelectedItem);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            ((App)App.Current).mySitaffEntities.Detach((Bon_Livraison_Contenu_Commande)this._dataGridDroit.SelectedItem);
                            ((Bon_Livraison)this.DataContext).Bon_Livraison_Contenu_Commande.Remove((Bon_Livraison_Contenu_Commande)this._dataGridDroit.SelectedItem);
                        }
                        catch (Exception) { }
                    }
                }
            }
            this.RefreshDGDroit();
            this.listContenu = ((Bon_Livraison)this.DataContext).ListeContenuDisponible;
            this._dataGridGauche.Items.Refresh();
        }

        #endregion

        #region Bouton Coller

        private void _ButtonColler_Click_1(object sender, RoutedEventArgs e)
        {
            CopierColler ClassPaste = new CopierColler();
            ObservableCollection<Bon_Livraison_Contenu_Commande_Supplementaire> listToAdd = ClassPaste.PasteDataBonLivraisonWindow();
            if (listToAdd != null)
            {
                foreach (Bon_Livraison_Contenu_Commande_Supplementaire blccs in listToAdd)
                {
                    ((Bon_Livraison)this.DataContext).Bon_Livraison_Contenu_Commande_Supplementaire.Add(blccs);
                }
                this._dataGridContenuSupplementaire.Items.Refresh();
                this.calculDataGridContenuSupp();
            }
        }

        #endregion

        #region Bouton Supprimer

        private void _ButtonSupprimer_Click_1(object sender, RoutedEventArgs e)
        {
            this.deleteLigne();
        }

        #endregion

        #region bouton shop

        private void _buttonShop_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                this.shopCommandeWindow.Close();
            }
            catch (Exception) { }
            this.shopCommandeWindow = new ShopCommandeWindow();
            this.shopCommandeWindow._ComboBoxFournisseur.SelectedItem = ((Bon_Livraison)this.DataContext).Fournisseur1;
            this.shopCommandeWindow.bonLivraisonWindow = this;
            this.shopCommandeWindow.Show();
        }

        #endregion

        #region bouton Calculer

        private void _ButtonCalculer_Click_1(object sender, RoutedEventArgs e)
        {
            this.RefreshDGDroit();
            this.listContenu = ((Bon_Livraison)this.DataContext).ListeContenuDisponible;
            this._dataGridGauche.Items.Refresh();
        }

        #endregion

        #endregion

        #region Verifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!this.Verif_BonLivraison())
            {
                verif = false;
            }

            return verif;
        }

        #region BonLivraison

        private bool Verif_BonLivraison()
        {
            bool verif = true;

            if (!this.Verif_datePickerDateEnvoi())
            {
                verif = false;
            }
            if (!this.Verif_datePickerDateReception())
            {
                verif = false;
            }
            if (!this.Verif_textBoxMontant())
            {
                verif = false;
            }
            if (!this.Verif_comboBoxFournisseur())
            {
                verif = false;
            }

            return verif;
        }

        #region DateEnvoi

        private bool Verif_datePickerDateEnvoi()
        {
            return ((App)App.Current).verifications.DatePickerSelectionNonObligatoire(this._datePickerDateEnvoi, this._textBlockDateEnvoi);
        }

        private void _datePickerDateEnvoi_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_datePickerDateEnvoi();
            this.Verif_datePickerDateReception();
        }

        #endregion

        #region DateReception

        private bool Verif_datePickerDateReception()
        {
            return ((App)App.Current).verifications.DatePickerSelectionNonObligatoire(this._datePickerDateReception, this._textBlockDateReception, this._datePickerDateEnvoi);
        }

        private void _datePickerDateReception_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_datePickerDateEnvoi();
            this.Verif_datePickerDateReception();
        }

        #endregion

        #region Montant

        private bool Verif_textBoxMontant()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxMontant, this._textBlockMontant);
        }

        private void _textBoxMontant_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxMontant();
        }

        #endregion

        #region Fournisseur

        private bool Verif_comboBoxFournisseur()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxFournisseur, this._textBlockFournisseur);
        }

        private void _comboBoxFournisseur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_comboBoxFournisseur();
        }

        #endregion

        #endregion

        #region CheckBox

        private void _checkBoxRecu_Checked(object sender, RoutedEventArgs e)
        {
            /*((Bon_Livraison)this.DataContext).Recu = true;*/
        }

        private void _checkBoxRecu_Unchecked(object sender, RoutedEventArgs e)
        {
            /*((Bon_Livraison)this.DataContext).Recu = false;*/
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.listContenu = ((Bon_Livraison)this.DataContext).ListeContenuDisponible;
            this._comboBoxFournisseur.SelectedItem = ((Bon_Livraison)this.DataContext).Fournisseur1;
        }

        #endregion

        #region Evenements

        #region Datagrid Droit

        private void _dataGridDroit_LostFocus(object sender, RoutedEventArgs e)
        {
            this.RefreshDGDroit();
        }

        private void _dataGridDroit_CurrentCellChanged(object sender, EventArgs e)
        {
            //this.RefreshDGDroit();
        }

        private void _dataGridDroit_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //this.RefreshDGDroit();
        }


        private void _dataGridDroit_KeyUp(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Quantité Livrée":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            this.RefreshDGDroit();
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
            this.RefreshDGDroit();
        }

        #endregion

        #region Datagrid Supplémentaire

        private void _dataGridContenuSupplementaire_CurrentCellChanged(object sender, EventArgs e)
        {
            //this.calculDataGridContenuSupp();
        }

        private void _dataGridContenuSupplementaire_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //this.calculDataGridContenuSupp();
        }

        private void _dataGridContenuSupplementaire_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Quantité livrée":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            this.calculDataGridContenuSupp();
                            break;
                        case "P.U. Remisé":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            this.calculDataGridContenuSupp();
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

        #region Fonctions

        #region refresh et calculs

        public void RefreshDGDroit()
        {
            double total = 0;
            ObservableCollection<Bon_Livraison_Contenu_Commande> toTest = new ObservableCollection<Bon_Livraison_Contenu_Commande>(this._dataGridDroit.Items.OfType<Bon_Livraison_Contenu_Commande>());
            foreach (Bon_Livraison_Contenu_Commande temp in toTest)
            {
                if (temp.QuantiteRestante < -0.001)
                {
                    ((Bon_Livraison)this.DataContext).Bon_Livraison_Contenu_Commande.Remove(temp);
                    MessageBox.Show("Vous avez mis trop d'un élément à être reçu, cela n'est pas possible");
                }
                this.listContenu = ((Bon_Livraison)this.DataContext).ListeContenuDisponible;
                this._dataGridGauche.Items.Refresh();
            }
            foreach (Bon_Livraison_Contenu_Commande item in toTest)
            {
                try
                {
                    total = total + (item.Contenu_Commande_Fournisseur.Prix_Remise * item.Quantite);
                }
                catch (Exception)
                {
                }
            }
            foreach (Bon_Livraison_Contenu_Commande_Supplementaire cont in this._dataGridContenuSupplementaire.Items.OfType<Bon_Livraison_Contenu_Commande_Supplementaire>())
            {
                try
                {
                    total = total + (cont.Prix_Remise * cont.Quantite_Livree);
                }
                catch (Exception) { }
            }
            //int tempInt = (int)(total * 100);
            ((Bon_Livraison)this.DataContext).Montant = ((double)((int)(total * 100)) / 100);
            this.listContenu = ((Bon_Livraison)this.DataContext).ListeContenuDisponible;
            this._dataGridGauche.Items.Refresh();
            this._dataGridContenuSupplementaire.Items.Refresh();
        }

        private void calculDataGridContenuSupp()
        {
            foreach (Bon_Livraison_Contenu_Commande_Supplementaire cont in this._dataGridContenuSupplementaire.Items.OfType<Bon_Livraison_Contenu_Commande_Supplementaire>())
            {
                try
                {
                    cont.Prix_Unitaire = cont.Prix_Remise;
                    cont.Quantite = cont.Quantite_Livree;
                    cont.Prix_Total = cont.Prix_Unitaire * cont.Quantite;
                }
                catch (Exception)
                {
                }
            }
            try
            {
                this._dataGridContenuSupplementaire.Items.Refresh();
            }
            catch (Exception) { }
            this.RefreshDGDroit();
        }

        #endregion

        private void deleteLigne()
        {
            if (this._dataGridContenuSupplementaire.SelectedItem != null && this._dataGridContenuSupplementaire.SelectedItems.Count == 1)
            {
                try
                {
                    ((Bon_Livraison_Contenu_Commande_Supplementaire)this._dataGridContenuSupplementaire.SelectedItem).Quantite_Livree = 0;
                    ((Bon_Livraison_Contenu_Commande_Supplementaire)this._dataGridContenuSupplementaire.SelectedItem).Reference = "";
                    ((Bon_Livraison_Contenu_Commande_Supplementaire)this._dataGridContenuSupplementaire.SelectedItem).Designation = "";
                    ((Bon_Livraison_Contenu_Commande_Supplementaire)this._dataGridContenuSupplementaire.SelectedItem).Quantite = 0;
                    ((Bon_Livraison_Contenu_Commande_Supplementaire)this._dataGridContenuSupplementaire.SelectedItem).Prix_Remise = 0;
                    ((Bon_Livraison_Contenu_Commande_Supplementaire)this._dataGridContenuSupplementaire.SelectedItem).Prix_Total = 0;
                    ((Bon_Livraison_Contenu_Commande_Supplementaire)this._dataGridContenuSupplementaire.SelectedItem).Prix_Unitaire = 0;
                }
                catch (Exception) { }
                Bon_Livraison_Contenu_Commande_Supplementaire item = new Bon_Livraison_Contenu_Commande_Supplementaire();
                try
                {
                    item = (Bon_Livraison_Contenu_Commande_Supplementaire)this._dataGridContenuSupplementaire.SelectedItem;
                    item.Bon_Livraison1 = null;
                    ((Bon_Livraison)this.DataContext).Bon_Livraison_Contenu_Commande_Supplementaire.Remove(item);
                    ((App)App.Current).mySitaffEntities.Detach(item);
                }
                catch (Exception)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        ((Bon_Livraison)this.DataContext).Bon_Livraison_Contenu_Commande_Supplementaire.Remove(item);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            this._dataGridContenuSupplementaire.Items.Remove(item);
                        }
                        catch (Exception) { }
                    }
                }
            }
            else
            {
                if (this._dataGridContenuSupplementaire.SelectedItems.Count != 0)
                {
                    ObservableCollection<Bon_Livraison_Contenu_Commande_Supplementaire> toRemove = new ObservableCollection<Bon_Livraison_Contenu_Commande_Supplementaire>();
                    foreach (Bon_Livraison_Contenu_Commande_Supplementaire item in this._dataGridContenuSupplementaire.SelectedItems.OfType<Bon_Livraison_Contenu_Commande_Supplementaire>())
                    {
                        toRemove.Add(item);
                    }
                    foreach (Bon_Livraison_Contenu_Commande_Supplementaire item in toRemove)
                    {
                        try
                        {
                            item.Quantite_Livree = 0;
                            item.Reference = "";
                            item.Designation = "";
                            item.Quantite = 0;
                            item.Prix_Remise = 0;
                            item.Prix_Total = 0;
                            item.Prix_Unitaire = 0;
                        }
                        catch (Exception) { }
                        try
                        {
                            item.Bon_Livraison1 = null;
                            ((Bon_Livraison)this.DataContext).Bon_Livraison_Contenu_Commande_Supplementaire.Remove(item);
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Detach(item);
                                ((Bon_Livraison)this.DataContext).Bon_Livraison_Contenu_Commande_Supplementaire.Remove(item);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    this._dataGridContenuSupplementaire.Items.Remove(item);
                                }
                                catch (Exception) { }
                            }
                        }
                    }
                }
            }
            this.calculDataGridContenuSupp();
        }

        #endregion

        #region Lecture seule

        public void lectureSeule()
        {
            //TextBox
            this._textBoxMontant.IsReadOnly = true;
            this._textBoxNumero.IsReadOnly = true;

            //Date
            this._datePickerDateEnvoi.IsEnabled = false;
            this._datePickerDateReception.IsEnabled = false;

            //Datagrid
            this._dataGridDroit.IsReadOnly = true;
            this._dataGridGauche.IsReadOnly = true;
            this._dataGridContenuSupplementaire.IsEnabled = false;

            //Boutons
            this._buttonDroiteGauche.IsEnabled = false;
            this._buttonGaucheDroite.IsEnabled = false;
            this._ButtonColler.IsEnabled = false;

            //CheckBox
            this._checkBoxRecu.IsEnabled = false;

            //ComboBox
            this._comboBoxFournisseur.IsEnabled = false;

            //contextMenus
            this._dataGridContenuSupplementaire.ContextMenu = null;
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
            this._dataGridContenuSupplementaire.ContextMenu = contextMenu;

            MenuItem itemAfficher = new MenuItem();
            itemAfficher.Header = "Supprimer";
            itemAfficher.Click += new RoutedEventHandler(delegate { this.menuDelete(); });


            contextMenu.Items.Add(itemAfficher);
        }

        private void menuDelete()
        {
            this.deleteLigne();
        }

        #endregion

        #endregion
    }
}