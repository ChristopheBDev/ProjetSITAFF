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
    /// Logique d'interaction pour ImportCommandeWindow.xaml
    /// </summary>
    public partial class ImportCommandeWindow : Window
    {

        #region Attributs

        public SortieAtelierWindow sortieAtelierWindow;

        #endregion

        #region Propd

        public ObservableCollection<Contenu_Commande_Fournisseur> listContenuCommande
        {
            get { return (ObservableCollection<Contenu_Commande_Fournisseur>)GetValue(listContenuCommandeProperty); }
            set { SetValue(listContenuCommandeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listContenuCommande.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listContenuCommandeProperty =
            DependencyProperty.Register("listContenuCommande", typeof(ObservableCollection<Contenu_Commande_Fournisseur>), typeof(ImportCommandeWindow), new PropertyMetadata(null));

        public ObservableCollection<Fournisseur> listFournisseur
        {
            get { return (ObservableCollection<Fournisseur>)GetValue(listFournisseurProperty); }
            set { SetValue(listFournisseurProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listFournisseur.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFournisseurProperty =
            DependencyProperty.Register("listFournisseur", typeof(ObservableCollection<Fournisseur>), typeof(ImportCommandeWindow), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public ImportCommandeWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Initialisation des autocompleteBox
            this.initialisationAutoCompleteBox();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);
        }

        #region Initialisation PropD

        private void initialisationPropDependance()
        {
            this.listFournisseur = new ObservableCollection<Fournisseur>(((App)App.Current).mySitaffEntities.Fournisseur.Where(fou => fou.Commande_Fournisseur.Count() > 0).OrderBy(fou => fou.Entreprise.Libelle));
            //this.listContenuCommande = new ObservableCollection<Contenu_Commande_Fournisseur>(((App)App.Current).mySitaffEntities.Contenu_Commande_Fournisseur.OrderBy(vsc => vsc.Designation));
        }

        private void initialisationAutoCompleteBox()
        {
            List<string> listCommande = new List<string>();
            foreach (Commande_Fournisseur item in ((App)App.Current).mySitaffEntities.Commande_Fournisseur.Where(cf => cf.Contenu_Commande_Fournisseur.Count > 0))
            {
                listCommande.Add(item.Numero);
            }
            this._TextBoxCommande.ItemsSource = listCommande;
        }

        #endregion

        #endregion

        #region Fonctions

        private void import()
        {
            if (this.sortieAtelierWindow != null)
            {
                foreach (Contenu_Commande_Fournisseur item in this._dataGridFournisseur.SelectedItems.OfType<Contenu_Commande_Fournisseur>())
                {
                    try
                    {
                        Contenu_Sortie_Atelier newItem = new Contenu_Sortie_Atelier();
                        newItem.Reference = item.Reference;
                        newItem.Designation = item.Designation;
                        newItem.Quantite = 1;
                        newItem.Prix = double.Parse(item.Prix_Unitaire.ToString());
                        newItem.Prix_Remise = double.Parse(item.Prix_Remise.ToString());
                        ((Sortie_Atelier)this.sortieAtelierWindow.DataContext).Contenu_Sortie_Atelier.Add(newItem);
                    }
                    catch (Exception) { }
                }
                try
                {
                    this.sortieAtelierWindow._dataGridContenu.Items.Refresh();
                }
                catch (Exception) { }
            }
        }

        #endregion

        #region Boutons

        #region importer

        private void Importer_Click(object sender, RoutedEventArgs e)
        {
            this.import();
        }

        #endregion

        #region null

        private void NullFournisseur_Click(object sender, RoutedEventArgs e)
        {
            this._ComboBoxFournisseur.SelectedItem = null;
        }

        #endregion

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #endregion

        #region Evenement fournisseur modifié

        private void _mettreANull_Click(object sender, RoutedEventArgs e)
        {
            this._ComboBoxFournisseur.SelectedItem = null;
            this.filtrage();
        }

        private void _TextBoxDesignation_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.filtrage();
            }
        }

        private void _ComboBoxFournisseur_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            this.filtrage();
        }

        #endregion

        #region filtrage

        private void filtrage()
        {
            ObservableCollection<Contenu_Commande_Fournisseur> listToPut = new ObservableCollection<Contenu_Commande_Fournisseur>(((App)App.Current).mySitaffEntities.Contenu_Commande_Fournisseur.OrderBy(ccf => ccf.Designation));

            if (this._ComboBoxFournisseur.SelectedItem != null)
            {
                listToPut = new ObservableCollection<Contenu_Commande_Fournisseur>(listToPut.Where(com => com.Commande_Fournisseur1 != null));
                listToPut = new ObservableCollection<Contenu_Commande_Fournisseur>(listToPut.Where(com => com.Commande_Fournisseur1.Fournisseur1 != null));
                listToPut = new ObservableCollection<Contenu_Commande_Fournisseur>(listToPut.Where(com => com.Commande_Fournisseur1.Fournisseur1.Identifiant == ((Fournisseur)this._ComboBoxFournisseur.SelectedItem).Identifiant));
            }
            if (this._TextBoxDesignation.Text != "")
            {
                listToPut = new ObservableCollection<Contenu_Commande_Fournisseur>(listToPut.Where(com => com.Designation != null));
                listToPut = new ObservableCollection<Contenu_Commande_Fournisseur>(listToPut.Where(com => com.Designation.Trim().ToLower().Contains(this._TextBoxDesignation.Text.Trim().ToLower())));
            }
            if (this._TextBoxCommande.Text != "")
            {
                listToPut = new ObservableCollection<Contenu_Commande_Fournisseur>(listToPut.Where(com => com.Commande_Fournisseur1 != null));
                listToPut = new ObservableCollection<Contenu_Commande_Fournisseur>(listToPut.Where(com => com.Commande_Fournisseur1.Numero != null));
                listToPut = new ObservableCollection<Contenu_Commande_Fournisseur>(listToPut.Where(com => com.Commande_Fournisseur1.Numero.Trim().ToLower().Contains(this._TextBoxCommande.Text.Trim().ToLower())));
            }
            if (this._TextBoxReference.Text.Trim() != "")
            {
                ObservableCollection<Contenu_Commande_Fournisseur> toPutOnline = new ObservableCollection<Contenu_Commande_Fournisseur>();
                foreach (Contenu_Commande_Fournisseur item in listToPut)
                {
                    if (this._TextBoxReference.Text.Contains(";"))
                    {
                        ObservableCollection<String> listRef = new ObservableCollection<string>(this._TextBoxReference.Text.Split(';'));
                        bool test2 = false;
                        foreach (String mot in listRef)
                        {
                            if (item.Reference != null)
                            {
                                if (item.Reference.ToLower().Trim().Contains(mot.ToLower().Trim()))
                                {
                                    test2 = true;
                                }
                            }
                        }
                        if (test2)
                        {
                            toPutOnline.Add(item);
                        }
                    }
                    else
                    {
                        if (item.Reference != null)
                        {
                            if (item.Reference.ToLower().Trim().Contains(this._TextBoxReference.Text.ToLower().Trim()))
                            {
                                toPutOnline.Add(item);
                            }
                        }
                    }
                }
                this._dataGridFournisseur.ItemsSource = toPutOnline;
            }
            else
            {
                this._dataGridFournisseur.ItemsSource = listToPut;
            }
        }

        #endregion

    }
}
