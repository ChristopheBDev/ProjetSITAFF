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
    /// Logique d'interaction pour ImportRetourChantier.xaml
    /// </summary>
    public partial class ImportRetourChantier : Window
    {
        #region Attributs

        public RetourChantierWindow retourChantierWindow;
        public long identifiantAffaire;

        #endregion

        #region Propd

        public ObservableCollection<ImportPourRetourChantier> listContenu
        {
            get { return (ObservableCollection<ImportPourRetourChantier>)GetValue(listContenuProperty); }
            set { SetValue(listContenuProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listContenuCommande.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listContenuProperty =
            DependencyProperty.Register("listContenu", typeof(ObservableCollection<ImportPourRetourChantier>), typeof(ImportRetourChantier), new PropertyMetadata(null));

        public ObservableCollection<Fournisseur> listFournisseur
        {
            get { return (ObservableCollection<Fournisseur>)GetValue(listFournisseurProperty); }
            set { SetValue(listFournisseurProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listFournisseur.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFournisseurProperty =
            DependencyProperty.Register("listFournisseur", typeof(ObservableCollection<Fournisseur>), typeof(ImportRetourChantier), new UIPropertyMetadata(null));

        #endregion

        #region Constructeur

        public ImportRetourChantier(long idAffaire)
        {
            InitializeComponent();

            this.identifiantAffaire = idAffaire;

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
            this.listContenu = new ObservableCollection<ImportPourRetourChantier>(this.remplissageDataGrid());
        }

        private void initialisationAutoCompleteBox()
        {

        }

        #endregion

        #endregion

        #region Fonctions

        private void import()
        {
            if (this.retourChantierWindow != null)
            {
                foreach (ImportPourRetourChantier item in this._dataGridFournisseur.SelectedItems.OfType<ImportPourRetourChantier>())
                {
                    try
                    {
                        Contenu_Retour_Chantier newItem = new Contenu_Retour_Chantier();
                        newItem.Reference = item.Reference;
                        newItem.Designation = item.Designation;
                        newItem.Quantite = 1;
                        newItem.Prix = item.PrixUnitaireRemise;
                        ((Retour_Chantier)this.retourChantierWindow.DataContext).Contenu_Retour_Chantier.Add(newItem);
                    }
                    catch (Exception) { }
                }
                try
                {
                    this.retourChantierWindow._dataGridContenu.Items.Refresh();
                }
                catch (Exception) { }
            }
        }

        private ObservableCollection<ImportPourRetourChantier> remplissageDataGrid()
        {
            ObservableCollection<ImportPourRetourChantier> toReturn = new ObservableCollection<ImportPourRetourChantier>();

            foreach (Facture_Fournisseur_Contenu item in ((App)App.Current).mySitaffEntities.Facture_Fournisseur_Contenu)
            {
                if (item.Affaire1 != null)
                {
                    if (item.Affaire1.Identifiant == identifiantAffaire)
                    {
                        ImportPourRetourChantier tmp = new ImportPourRetourChantier();
                        tmp.Designation = item.Designation;
                        tmp.Reference = item.Reference_Fournisseur;
                        tmp.PrixUnitaireRemise = item.Prix_Unitaire_Facture_HT;
                        if (item.Facture_Fournisseur1 != null)
                        {
                            tmp.Provenance = "Facture fournisseur n° " + item.Facture_Fournisseur1.Numero;
                            if (item.Facture_Fournisseur1.Fournisseur1 != null)
                            {
                                if (item.Facture_Fournisseur1.Fournisseur1.Entreprise != null)
                                {
                                    tmp.Fournisseur = item.Facture_Fournisseur1.Fournisseur1.Entreprise.Libelle;
                                }
                            }
                        }
                        toReturn.Add(tmp);
                    }
                }
            }

            foreach (Contenu_Sortie_Atelier item in ((App)App.Current).mySitaffEntities.Contenu_Sortie_Atelier)
            {
                if (item.Sortie_Atelier1 != null)
                {
                    if (item.Sortie_Atelier1.Affaire1 != null)
                    {
                        if (item.Sortie_Atelier1.Affaire1.Identifiant == identifiantAffaire)
                        {
                            ImportPourRetourChantier tmp = new ImportPourRetourChantier();
                            tmp.Designation = item.Designation;
                            tmp.Reference = item.Reference;
                            try
                            {
                                tmp.PrixUnitaireRemise = double.Parse(item.Prix_Remise.ToString());
                            }
                            catch (Exception)
                            {
                                tmp.PrixUnitaireRemise = 0;
                            }
                            if (item.Sortie_Atelier1 != null)
                            {
                                tmp.Provenance = "Sortie atelier n° " + item.Sortie_Atelier1.Numero;
                            }
                            toReturn.Add(tmp);
                        }
                    }
                }
            }

            return toReturn;
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
            ObservableCollection<ImportPourRetourChantier> listToPut = new ObservableCollection<ImportPourRetourChantier>(this.remplissageDataGrid());

            if (this._ComboBoxFournisseur.SelectedItem != null)
            {
                listToPut = new ObservableCollection<ImportPourRetourChantier>(listToPut.Where(com => com.Fournisseur == ((Fournisseur)this._ComboBoxFournisseur.SelectedItem).Entreprise.Libelle));
            }
            if (this._TextBoxDesignation.Text != "")
            {
                listToPut = new ObservableCollection<ImportPourRetourChantier>(listToPut.Where(com => com.Designation.Trim().ToLower().Contains(this._TextBoxDesignation.Text.Trim().ToLower())));
            }
            if (this._TextBoxReference.Text.Trim() != "")
            {
                ObservableCollection<ImportPourRetourChantier> toPutOnline = new ObservableCollection<ImportPourRetourChantier>();
                foreach (ImportPourRetourChantier item in listToPut)
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
