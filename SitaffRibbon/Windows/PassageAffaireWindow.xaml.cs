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
using System.Windows.Navigation;
using System.Windows.Shapes;
/* 
 * Using pour utilisation des IObservableCollection (afin d'éviter de mettre
 * System.Collections.ObjectModel.IObservableCollection en entier)
 */
using System.Collections.ObjectModel;
//Using pour utiliser le type TypeConverter pour la conversion de couleur
using System.ComponentModel;
using SitaffRibbon.Classes;


namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour PassageAffaireWindow.xaml
    /// </summary>
    public partial class PassageAffaireWindow : Window
    {

        #region Attributs

        public Devis devis;
        public Versions temp = null;
        public Versions tempToPutOnDataContext;
        public Affaire temporaire;
        public bool message = true;

        #endregion

        #region Propriétés de dépendances


        public ObservableCollection<Condition_Reglement> ListConditionsReglements
        {
            get { return (ObservableCollection<Condition_Reglement>)GetValue(ListConditionsReglementsProperty); }
            set { SetValue(ListConditionsReglementsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListConditionsReglements.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListConditionsReglementsProperty =
            DependencyProperty.Register("ListConditionsReglements", typeof(ObservableCollection<Condition_Reglement>), typeof(PassageAffaireWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Affaire> listAffaires
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffairesProperty); }
            set { SetValue(listAffairesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaires.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffairesProperty =
            DependencyProperty.Register("listAffaires", typeof(ObservableCollection<Affaire>), typeof(PassageAffaireWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Entreprise_Mere> listEntreprisesMere
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntreprisesMereProperty); }
            set { SetValue(listEntreprisesMereProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntreprisesMere.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntreprisesMereProperty =
            DependencyProperty.Register("listEntreprisesMere", typeof(ObservableCollection<Entreprise_Mere>), typeof(PassageAffaireWindow), new UIPropertyMetadata(null));



        #endregion

        #region Constructeur

        public PassageAffaireWindow()
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
            this.listAffaires = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(add => add.Numero));
            this.listEntreprisesMere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(em => em.Nom));
            this.ListConditionsReglements = new ObservableCollection<Condition_Reglement>(((App)App.Current).mySitaffEntities.Condition_Reglement.OrderBy(cr => cr.Libelle));
        }

        private void initialisationSecurite()
        {
            //Mise en place des droits sur les boutons et tabs
            Securite securite = new Securite();
        }

        #endregion

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.verrouillerVersions();
            this._TreeViewPassageAffaire.ItemsSource = devis.Versions;
            this.temporaire = new Affaire();
            this.temporaire.Numero = this.devis.Numero;
            this.listAffaires = new ObservableCollection<Affaire>();
            if (((App)App.Current).mySitaffEntities.Affaire.Where(aff => aff.Numero == this.temporaire.Numero).Count() < 1)
            {
                this.listAffaires.Add(this.temporaire);
            }
            foreach (Affaire aff in ((App)App.Current).mySitaffEntities.Affaire.OrderBy(affa => affa.Numero))
            {
                this.listAffaires.Add(aff);
            }
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #endregion

        #region Boutons

        #region boutons ok / annuler

        /// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerifGenerale())
            {
                this.assuranceChiffresEnregistres();
                this.DialogResult = true;
                this.Close();
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

        #region boutons Conditions Réglements

        private void _ButtonVersDroite_click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxActivite.SelectedItem != null && this._ComboBoxActivite.SelectedItems.Count == 1)
            {
                Versions_Condition_Reglement temp = new Versions_Condition_Reglement();
                temp.Condition_Reglement1 = (Condition_Reglement)this._ComboBoxActivite.SelectedItem;
                ((Versions)this.DataContext).Versions_Condition_Reglement.Add(temp);
                //this.ListConditionsReglements.Remove((Condition_Reglement)this._ComboBoxActivite.SelectedItem);
            }
        }

        private void _ButtonVersGauche_click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxActiviteOfDevis.SelectedItem != null && this._ComboBoxActiviteOfDevis.SelectedItems.Count == 1)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Versions_Condition_Reglement.DeleteObject((Versions_Condition_Reglement)this._ComboBoxActiviteOfDevis.SelectedItem);
                }
                catch (Exception)
                {
                    try
                    {
                        ((Versions)this.DataContext).Versions_Condition_Reglement.Remove((Versions_Condition_Reglement)this._ComboBoxActiviteOfDevis.SelectedItem);
                        ((App)App.Current).mySitaffEntities.Detach((Versions_Condition_Reglement)this._ComboBoxActiviteOfDevis.SelectedItem);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            ((App)App.Current).mySitaffEntities.Detach((Versions_Condition_Reglement)this._ComboBoxActiviteOfDevis.SelectedItem);
                            ((Versions)this.DataContext).Versions_Condition_Reglement.Remove((Versions_Condition_Reglement)this._ComboBoxActiviteOfDevis.SelectedItem);
                        }
                        catch (Exception) { }
                    }
                }
            }
        }

        private void _importerConditions_Click_1(object sender, RoutedEventArgs e)
        {
            if ((this.DataContext) != null)
            {
                if (((Versions)this.DataContext).Devis1 != null)
                {
                    if (((Versions)this.DataContext).Devis1.Client != null)
                    {
                        if (((Versions)this.DataContext).Devis1.Client.Client_Condition_Reglement.Count() != 0)
                        {
                            foreach (Client_Condition_Reglement item in ((Versions)this.DataContext).Devis1.Client.Client_Condition_Reglement)
                            {
                                Versions_Condition_Reglement temp = new Versions_Condition_Reglement();
                                temp.Condition_Reglement1 = item.Condition_Reglement1;
                                temp.Commentaire = item.Commentaire;
                                temp.Pourcentage = item.Pourcentage;

                                ((Versions)this.DataContext).Versions_Condition_Reglement.Add(temp);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Aucune condition de réglement n'est enregistrée pour ce client.");
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Verifications

        private bool VerifGenerale()
        {
            bool test = true;

            if (!this.Verif_TabCommentaire())
            {
                test = false;
            }
            if (!this.VerifEnTete())
            {
                test = false;
            }
            if (!this.Verif_TabConditionsReglements())
            {
                test = false;
            }
            if (!this.VerifBudgetDevis())
            {
                test = false;
            }
            if (!Verif_TabSuivi())
            {
                test = false;
            }

            return test;
        }

        #region Verifications pour validation

        #region En-tête

        private bool VerifEnTete()
        {
            bool test = true;

            if (!this._verif_Affaire())
            {
                test = false;
            }
            if (!this._verif_numeroCommande())
            {
                test = false;
            }

            return test;
        }

        #region combobox Affaire

        private bool _verif_Affaire()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            this._ComboBoxNumeroAffaire.Background = vert;

            return verif;
        }

        #endregion

        #region Numero Commande

        private bool _verif_numeroCommande()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxBonCommandeClient.Text.Length != 0)
            {
                foreach (Commande cmd in ((App)App.Current).mySitaffEntities.Commande.Where(com => com.Versions.Count() != 0))
                {
                    if (cmd.Identifiant != ((Versions)this.DataContext).Commande1.Identifiant)
                    {
                        if (cmd.Numero_Commande == this._TextBoxBonCommandeClient.Text.Trim())
                        {
                            this._TextBoxBonCommandeClient.Background = rouge;
                            verif = false;
                            if (this.message)
                            {
                                MessageBox.Show("Le numéro de bon de commande existe déjà", "Erreur sur numéro de commande", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.message = false;
                            }
                        }
                    }
                }
            }
            else
            {
                this._TextBoxBonCommandeClient.Background = vert;
            }

            if (verif)
            {
                this._TextBoxBonCommandeClient.Background = vert;
            }
            else
            {
                this._TextBoxBonCommandeClient.Background = rouge;
            }

            return verif;
        }

        private void _TextBoxBonCommandeClient_LostFocus(object sender, RoutedEventArgs e)
        {
            this._verif_numeroCommande();
        }

        #endregion

        #endregion

        #region Tab Budget du devis

        private bool VerifBudgetDevis()
        {
            bool test = true;

            if (!this._verif_TextBoxAchatsFourniture())
            {
                test = false;
            }
            if (!this._verif_TextBoxAchatsSousTraitance())
            {
                test = false;
            }
            if (!this._verif_TextBoxAchatsMontant())
            {
                test = false;
            }
            if (!this._verif_TextBoxVentesFourniture())
            {
                test = false;
            }
            if (!this._verif_TextBoxVentesSousTraitance())
            {
                test = false;
            }
            if (!this._verif_TextBoxVentesMontant())
            {
                test = false;
            }
            if (!this._verif_TextBoxMargesFourniture())
            {
                test = false;
            }
            if (!this._verif_TextBoxMargesSousTraitance())
            {
                test = false;
            }
            if (!this._verif_TextBoxMargeMontant())
            {
                test = false;
            }
            if (!this._verif_TextBoxNbHeures())
            {
                test = false;
            }
            if (!this._verif_TextBoxTauxHoraire())
            {
                test = false;
            }
            if (!this._verif_TextBoxTotalVenteHeure())
            {
                test = false;
            }
            if (!this._verif_TextBoxMontantDevisInitial())
            {
                test = false;
            }
            if (!this._verif_TextBoxRemise())
            {
                test = false;
            }
            if (!this._verif_TextBoxMontantDevisNegoc())
            {
                test = false;
            }
            if (!_verif_TextBoxMontantRemise())
            {
                test = false;
            }

            if (test == true)
            {
                this._tabItemBudget.Background = Brushes.Green;
            }
            else
            {
                this._tabItemBudget.Background = Brushes.Red;
            }

            return test;
        }

        private bool _verif_TextBoxAchatsFourniture()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxAchatsFourniture.Text.Length > 0 && !Double.TryParse(_TextBoxAchatsFourniture.Text, out val))
            {
                test = false;
                this._TextBoxAchatsFourniture.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxAchatsFourniture.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxAchatsSousTraitance()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxAchatsSousTraitance.Text.Length > 0 && !Double.TryParse(_TextBoxAchatsSousTraitance.Text, out val))
            {
                test = false;
                this._TextBoxAchatsSousTraitance.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxAchatsSousTraitance.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxAchatsMontant()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxAchatsMontant.Text.Length > 0 && !Double.TryParse(_TextBoxAchatsMontant.Text, out val))
            {
                test = false;
                this._TextBoxAchatsMontant.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxAchatsMontant.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxVentesFourniture()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxVentesFourniture.Text.Length > 0 && !Double.TryParse(_TextBoxVentesFourniture.Text, out val))
            {
                test = false;
                this._TextBoxVentesFourniture.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxVentesFourniture.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxVentesSousTraitance()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxVentesSousTraitance.Text.Length > 0 && !Double.TryParse(_TextBoxVentesSousTraitance.Text, out val))
            {
                test = false;
                this._TextBoxVentesSousTraitance.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxVentesSousTraitance.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxVentesMontant()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxVentesMontant.Text.Length > 0 && !Double.TryParse(_TextBoxVentesMontant.Text, out val))
            {
                test = false;
                this._TextBoxVentesMontant.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxVentesMontant.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxMargesFourniture()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxMargesFourniture.Text.Length > 0 && !Double.TryParse(_TextBoxMargesFourniture.Text, out val))
            {
                test = false;
                this._TextBoxMargesFourniture.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxMargesFourniture.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxMargesSousTraitance()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxMargesSousTraitance.Text.Length > 0 && !Double.TryParse(_TextBoxMargesSousTraitance.Text, out val))
            {
                test = false;
                this._TextBoxMargesSousTraitance.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxMargesSousTraitance.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxMargeMontant()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxMargeMontant.Text.Length > 0 && !Double.TryParse(_TextBoxMargeMontant.Text, out val))
            {
                test = false;
                this._TextBoxMargeMontant.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxMargeMontant.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxNbHeures()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxNbHeures.Text.Length > 0 && !Double.TryParse(_TextBoxNbHeures.Text, out val))
            {
                test = false;
                this._TextBoxNbHeures.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxNbHeures.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxTauxHoraire()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxTauxHoraire.Text.Length > 0 && !Double.TryParse(_TextBoxTauxHoraire.Text, out val))
            {
                test = false;
                this._TextBoxTauxHoraire.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxTauxHoraire.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxTotalVenteHeure()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxTotalVenteHeure.Text.Length > 0 && !Double.TryParse(_TextBoxTotalVenteHeure.Text, out val))
            {
                test = false;
                this._TextBoxTotalVenteHeure.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxTotalVenteHeure.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxMontantDevisInitial()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxMontantDevisInitial.Text.Length > 0 && !Double.TryParse(_TextBoxMontantDevisInitial.Text, out val))
            {
                test = false;
                this._TextBoxMontantDevisInitial.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxMontantDevisInitial.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxRemise()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxRemise.Text.Length > 0 && !Double.TryParse(_TextBoxRemise.Text, out val))
            {
                test = false;
                this._TextBoxRemise.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxRemise.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxMontantDevisNegoc()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxMontantDevisNegoc.Text.Length > 0 && !Double.TryParse(_TextBoxMontantDevisNegoc.Text, out val))
            {
                test = false;
                this._TextBoxMontantDevisNegoc.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxMontantDevisNegoc.Background = vert;
            }

            return test;
        }

        private bool _verif_TextBoxMontantRemise()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            double val;
            if (_TextBoxMontantRemise.Text.Length > 0 && !Double.TryParse(_TextBoxMontantRemise.Text, out val))
            {
                test = false;
                this._TextBoxMontantRemise.Background = rouge;
            }
            else
            {
                test = true;
                this._TextBoxMontantRemise.Background = vert;
            }

            return test;
        }

        #endregion

        #region Tab Conditions de réglements

        private bool Verif_TabConditionsReglements()
        {
            bool test = true;

            if (this._TreeViewPassageAffaire.SelectedItem != null)
            {
                if (!Verif_DataGridConditionReglement())
                {
                    test = false;
                }
            }

            if (test == true)
            {
                this._tabItemConditionsReglements.Background = Brushes.Green;
            }
            else
            {
                this._tabItemConditionsReglements.Background = Brushes.Red;
            }

            return test;
        }

        #region DataGrid Condition de reglement

        private bool Verif_DataGridConditionReglement()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxActiviteOfDevis.Items.Count <= 0)
            {
                verif = false;
                this._ComboBoxActiviteOfDevis.Background = rouge;
            }
            else
            {
                verif = true;
                this._ComboBoxActiviteOfDevis.Background = vert;
            }

            return verif;
        }

        #endregion

        #endregion

        #region Tab Commentaire

        private bool Verif_TabCommentaire()
        {
            bool test = true;

            if (!this._verif_commentaire())
            {
                test = false;
            }

            if (test == true)
            {
                this._tabItemCommentaire.Background = Brushes.Green;
            }
            else
            {
                this._tabItemCommentaire.Background = Brushes.Red;
            }

            return test;
        }

        #region Champ Commentaire

        private bool _verif_commentaire()
        {
            bool test = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxCommentaire.Text.Trim().Length < 0)
            {
                test = true;
                this._TextBoxCommentaire.Background = vert;
            }
            else
            {
                test = true;
                this._TextBoxCommentaire.Background = vert;
            }

            return test;
        }

        #endregion

        #endregion

        #region Tab Suivi

        private bool Verif_TabSuivi()
        {
            bool test = true;

            if (test == true)
            {
                this._tabItemSuivi.Background = Brushes.Green;
            }
            else
            {
                this._tabItemSuivi.Background = Brushes.Red;
            }

            return test;
        }

        #endregion

        #endregion

        private bool TextBoxVerifNumeric(object sender, TextChangedEventArgs e)
        {
            //Booléen à retourner à vrai qui deviendra faux si un champ n'est pas bon dans les vérification
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            TextBox tb = (TextBox)sender;
            double val;
            if (tb.Text.Length > 0 && !Double.TryParse(tb.Text, out val))
            {
                verif = false;
                tb.Background = rouge;
            }
            else
            {
                if (tb.Text.Length == 0)
                {
                    tb.Background = Brushes.White;
                }
                else
                {
                    tb.Background = vert;
                }
            }

            return verif;
        }

        private bool TextBoxVerifNumeric(object sender, RoutedEventArgs e)
        {
            //Booléen à retourner à vrai qui deviendra faux si un champ n'est pas bon dans les vérification
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            TextBox tb = (TextBox)sender;
            double val;
            if (tb.Text.Length > 0 && !Double.TryParse(tb.Text, out val))
            {
                verif = false;
                tb.Background = rouge;
            }
            else
            {
                if (tb.Text.Length == 0)
                {
                    tb.Background = Brushes.White;
                }
                else
                {
                    tb.Background = vert;
                }
            }

            return verif;
        }

        #region Verifications pour chaque champs

        private void _TextBoxPourcReglement_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxVerifNumeric(sender, e);
        }

        #endregion

        #endregion

        #region Fonctions

        private void assuranceChiffresEnregistres()
        {
            if (((Versions)this.DataContext).Commande1 == null)
            {
                ((Versions)this.DataContext).Commande1 = new Commande();
            }
            //On s'assure que tous les chiffres sont bien enregistrés
            double val;
            if (Double.TryParse(_TextBoxMontantDevisNegoc.Text, out val))
            {
                ((Versions)this.DataContext).Montant_Remise = Double.Parse(_TextBoxMontantDevisNegoc.Text);
            }
            else
            {
                ((Versions)this.DataContext).Montant_Remise = 0;
            }
            if (Double.TryParse(_TextBoxNbHeures.Text, out val))
            {
                ((Versions)this.DataContext).Commande1.Qte_Main_Oeuvre = Double.Parse(_TextBoxNbHeures.Text);
            }
            else
            {
                ((Versions)this.DataContext).Commande1.Qte_Main_Oeuvre = 0;
            }
            if (Double.TryParse(_TextBoxTauxHoraire.Text, out val))
            {
                ((Versions)this.DataContext).Commande1.Taux_Horaire = Double.Parse(_TextBoxTauxHoraire.Text);
            }
            else
            {
                ((Versions)this.DataContext).Commande1.Taux_Horaire = 0;
            }
            if (Double.TryParse(_TextBoxMargeMontant.Text, out val))
            {
                ((Versions)this.DataContext).Taux_Marge = Double.Parse(_TextBoxMargeMontant.Text);
            }
            else
            {
                ((Versions)this.DataContext).Taux_Marge = 0;
            }
            if (Double.TryParse(_TextBoxAchatsFourniture.Text, out val))
            {
                ((Versions)this.DataContext).Commande1.Prix_Achat_Fourniture = Double.Parse(_TextBoxAchatsFourniture.Text);
            }
            else
            {
                ((Versions)this.DataContext).Commande1.Prix_Achat_Fourniture = 0;
            }
            if (Double.TryParse(_TextBoxAchatsSousTraitance.Text, out val))
            {
                ((Versions)this.DataContext).Commande1.Prix_Achat_SsTraitance = Double.Parse(_TextBoxAchatsSousTraitance.Text);
            }
            else
            {
                ((Versions)this.DataContext).Commande1.Prix_Achat_SsTraitance = 0;
            }
            if (Double.TryParse(_TextBoxVentesSousTraitance.Text, out val))
            {
                ((Versions)this.DataContext).Commande1.Prix_Vente_SsTraitance = Double.Parse(_TextBoxVentesSousTraitance.Text);
            }
            else
            {
                ((Versions)this.DataContext).Commande1.Prix_Vente_SsTraitance = 0;
            }
            if (Double.TryParse(_TextBoxVentesFourniture.Text, out val))
            {
                ((Versions)this.DataContext).Commande1.Prix_Vente_Fourniture = Double.Parse(_TextBoxVentesFourniture.Text);
            }
            else
            {
                ((Versions)this.DataContext).Commande1.Prix_Vente_Fourniture = 0;
            }
            if (Double.TryParse(_TextBoxMontantDevisInitial.Text, out val))
            {
                ((Versions)this.DataContext).Montant = Double.Parse(_TextBoxMontantDevisInitial.Text);
            }
            else
            {
                ((Versions)this.DataContext).Montant = 0;
            }
            if (Double.TryParse(_TextBoxRemise.Text, out val))
            {
                ((Versions)this.DataContext).Taux_Remise = Double.Parse(_TextBoxRemise.Text);
            }
            else
            {
                ((Versions)this.DataContext).Taux_Remise = 0;
            }
            ((Versions)this.DataContext).Montant_Options = 0;
            if (Double.TryParse(_TextBoxMontantRemise.Text, out val))
            {
                ((Versions)this.DataContext).Remise = Double.Parse(_TextBoxMontantRemise.Text);
            }
            else
            {
                ((Versions)this.DataContext).Remise = 0;
            }
            if (Double.TryParse(_TextBoxTotalVenteHeure.Text, out val))
            {
                ((Versions)this.DataContext).Commande1.Prix_Total_Vente_Heure = Double.Parse(_TextBoxTotalVenteHeure.Text);
            }
            else
            {
                ((Versions)this.DataContext).Commande1.Prix_Total_Vente_Heure = 0;
            }
        }

        #region Calculs

        public void CalculMargeFourniture()
        {
            double val;
            if (!Double.TryParse(_TextBoxAchatsFourniture.Text, out val))
            {
                _TextBoxAchatsFourniture.Text = "0";
            }
            if (!Double.TryParse(_TextBoxVentesFourniture.Text, out val))
            {
                _TextBoxVentesFourniture.Text = "0";
            }
            if (Double.TryParse(_TextBoxAchatsFourniture.Text, out val) && Double.TryParse(_TextBoxVentesFourniture.Text, out val))
            {
                double Result = 0;
                if (double.Parse(_TextBoxAchatsFourniture.Text) != 0)
                {
                    Result = (1 - (double.Parse(_TextBoxAchatsFourniture.Text) / double.Parse(_TextBoxVentesFourniture.Text))) * 100;
                }
                _TextBoxMargesFourniture.Text = Result.ToString();
            }
            else
            {
                _TextBoxMargesFourniture.Text = "0";
            }
        }

        public void CalculMargeSousTraitance()
        {
            double val;
            if (!Double.TryParse(_TextBoxAchatsSousTraitance.Text, out val))
            {
                _TextBoxAchatsSousTraitance.Text = "0";
            }
            if (!Double.TryParse(_TextBoxVentesSousTraitance.Text, out val))
            {
                _TextBoxVentesSousTraitance.Text = "0";
            }
            if (Double.TryParse(_TextBoxAchatsSousTraitance.Text, out val) && Double.TryParse(_TextBoxVentesSousTraitance.Text, out val))
            {
                double Result = 0;
                if (double.Parse(_TextBoxAchatsSousTraitance.Text) != 0)
                {
                    Result = (1 - (double.Parse(_TextBoxAchatsSousTraitance.Text) / double.Parse(_TextBoxVentesSousTraitance.Text))) * 100;
                }
                _TextBoxMargesSousTraitance.Text = Result.ToString();
            }
            else
            {
                _TextBoxMargesSousTraitance.Text = "0";
            }
        }

        public void CalculTotalVenteHeures()
        {
            double val;
            if (Double.TryParse(_TextBoxNbHeures.Text, out val) && Double.TryParse(_TextBoxTauxHoraire.Text, out val))
            {
                double Result;
                Result = double.Parse(_TextBoxNbHeures.Text) * double.Parse(_TextBoxTauxHoraire.Text);
                _TextBoxTotalVenteHeure.Text = Result.ToString();
            }
            else
            {
                _TextBoxTotalVenteHeure.Text = "";
            }
        }

        public void CalculMargeTotalAchatsVentes()
        {
            double val;
            if (!Double.TryParse(_TextBoxAchatsMontant.Text, out val))
            {
                _TextBoxAchatsMontant.Text = "0";
            }
            if (!Double.TryParse(_TextBoxVentesMontant.Text, out val))
            {
                _TextBoxVentesMontant.Text = "0";
            }
            if (Double.TryParse(_TextBoxAchatsMontant.Text, out val) && Double.TryParse(_TextBoxVentesMontant.Text, out val))
            {
                double Result = 0;
                if (double.Parse(_TextBoxAchatsMontant.Text) != 0)
                {
                    Result = (1 - (double.Parse(_TextBoxAchatsMontant.Text) / double.Parse(_TextBoxVentesMontant.Text))) * 100;
                }
                _TextBoxMargeMontant.Text = Result.ToString();
            }
            else
            {
                _TextBoxMargeMontant.Text = "0";
            }
        }

        public void CalculRemise()
        {
            double val;
            if (!Double.TryParse(_TextBoxMontantDevisNegoc.Text, out val))
            {
                _TextBoxAchatsMontant.Text = "0";
            }
            if (!Double.TryParse(_TextBoxMontantDevisInitial.Text, out val))
            {
                _TextBoxVentesMontant.Text = "0";
            }
            if (Double.TryParse(_TextBoxMontantDevisNegoc.Text, out val) && Double.TryParse(_TextBoxMontantDevisInitial.Text, out val))
            {
                double Result = 0;
                if (double.Parse(_TextBoxMontantDevisInitial.Text) != 0)
                {
                    Result = (1 - (double.Parse(_TextBoxMontantDevisNegoc.Text) / double.Parse(_TextBoxMontantDevisInitial.Text))) * 100;
                }
                _TextBoxRemise.Text = Result.ToString();
            }
            else
            {
                _TextBoxRemise.Text = "";
            }
        }

        public void CalculMontantRemise()
        {
            double val;
            if (Double.TryParse(_TextBoxMontantDevisNegoc.Text, out val) && Double.TryParse(_TextBoxMontantDevisInitial.Text, out val))
            {
                double Result;
                Result = double.Parse(_TextBoxMontantDevisInitial.Text) - double.Parse(_TextBoxMontantDevisNegoc.Text);
                _TextBoxMontantRemise.Text = Result.ToString();
            }
            else
            {
                _TextBoxMontantRemise.Text = "";
            }
        }

        public void CalculMontantVentes()
        {
            double val;
            Double resultat = 0;
            if (Double.TryParse(_TextBoxVentesFourniture.Text, out val))
            {
                resultat = resultat + Double.Parse(_TextBoxVentesFourniture.Text);
            }
            if (Double.TryParse(_TextBoxVentesSousTraitance.Text, out val))
            {
                resultat = resultat + Double.Parse(_TextBoxVentesSousTraitance.Text);
            }
            _TextBoxVentesMontant.Text = resultat.ToString();
        }

        public void CalculMontantAchats()
        {
            double val;
            Double resultat = 0;
            if (Double.TryParse(_TextBoxAchatsSousTraitance.Text, out val))
            {
                resultat = resultat + Double.Parse(_TextBoxAchatsSousTraitance.Text);
            }
            if (Double.TryParse(_TextBoxAchatsFourniture.Text, out val))
            {
                resultat = resultat + Double.Parse(_TextBoxAchatsFourniture.Text);
            }
            _TextBoxAchatsMontant.Text = resultat.ToString();
        }

        public void CalculTauxHoraire()
        {
            double val;
            if (!Double.TryParse(_TextBoxTotalVenteHeure.Text, out val))
            {
                _TextBoxAchatsMontant.Text = "0";
            }
            if (!Double.TryParse(_TextBoxNbHeures.Text, out val))
            {
                _TextBoxVentesMontant.Text = "0";
            }
            if (Double.TryParse(_TextBoxTotalVenteHeure.Text, out val) && Double.TryParse(_TextBoxNbHeures.Text, out val))
            {
                Double Result = 0;
                if (double.Parse(_TextBoxNbHeures.Text) != 0)
                {
                    Result = (Double.Parse(_TextBoxTotalVenteHeure.Text) / Double.Parse(_TextBoxNbHeures.Text));
                }
                _TextBoxTauxHoraire.Text = Result.ToString();
            }
            else
            {
                _TextBoxTauxHoraire.Text = "0";
            }
        }

        public void CalculMontantDevisNegocie()
        {
            double val;
            Double resultat = 0;
            if (Double.TryParse(_TextBoxVentesMontant.Text, out val))
            {
                resultat = resultat + Double.Parse(_TextBoxVentesMontant.Text);
            }
            if (Double.TryParse(_TextBoxTotalVenteHeure.Text, out val))
            {
                resultat = resultat + Double.Parse(_TextBoxTotalVenteHeure.Text);
            }
            _TextBoxMontantDevisNegoc.Text = resultat.ToString();
        }

        #endregion

        #endregion

        #region Evenements

        private void _TreeViewPassageAffaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this._TreeViewPassageAffaire.SelectedItems.Count > 1)
            {
                this._TreeViewPassageAffaire.SelectedItem = null;
                this.verrouillerVersions();
            }
            else
            {
                if (this._TreeViewPassageAffaire.SelectedItems.Count == 0)
                {
                    this._TreeViewPassageAffaire.SelectedItem = null;
                    this.verrouillerVersions();
                }
                else
                {
                    this.tempToPutOnDataContext = (Versions)this._TreeViewPassageAffaire.SelectedItem;
                    if (tempToPutOnDataContext != temp && this.temp != null)
                    {
                        if (this.VerifGenerale())
                        {
                            if (this.temp != null)
                            {
                                this.assuranceChiffresEnregistres();
                            }
                            //On modifie le datacontext
                            this.DataContext = (Versions)this._TreeViewPassageAffaire.SelectedItem;
                            //this.ListConditionsReglements = new ObservableCollection<Condition_Reglement>(((App)App.Current).mySitaffEntities.Condition_Reglement.OrderBy(cr => cr.Libelle));
                            //foreach (Versions_Condition_Reglement vcr in ((Versions)this.DataContext).Versions_Condition_Reglement)
                            //{
                            //    this.ListConditionsReglements.Remove(vcr.Condition_Reglement1);
                            //}
                            temp = (Versions)this.DataContext;
                            this.CalculMontantAchats();
                            this.CalculMontantVentes();
                            this.CalculMargeTotalAchatsVentes();
                            this.CalculMargeFourniture();
                            this.CalculMargeSousTraitance();
                            this.CalculMontantRemise();
                            this.CalculTotalVenteHeures();
                            this.deverrouillerVersions();
                            this.VerifGenerale();
                            this.message = true;
                        }
                        else
                        {
                            MessageBox.Show("Vous n'avez pas bien renseigné toutes les informations, veuillez bien les renseigner avant d'aller sur une autre version.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            this.DataContext = temp;
                            this._TreeViewPassageAffaire.SelectedItem = temp;
                        }
                    }
                    else
                    {
                        this.DataContext = (Versions)this._TreeViewPassageAffaire.SelectedItem;
                        temp = (Versions)this.DataContext;
                        this.CalculMontantAchats();
                        this.CalculMontantVentes();
                        this.CalculMargeTotalAchatsVentes();
                        this.CalculMargeFourniture();
                        this.CalculMargeSousTraitance();
                        this.CalculMontantRemise();
                        this.CalculTotalVenteHeures();
                        this.deverrouillerVersions();
                        this.VerifGenerale();
                        this.message = true;

                    }
                }
            }
        }

        private void _DatePickerRelance_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this._DatePickerRelance.SelectedDate == null)
            {
                this._TextBoxCommentaireRelance.IsEnabled = false;
                this._TextBoxCommentaireRelance.Text = "";
            }
            else
            {
                this._TextBoxCommentaireRelance.IsEnabled = true;
            }
        }

        private void _ComboBoxNumeroAffaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this._ComboBoxNumeroAffaire.SelectedItem != null)
            {
                if (((Affaire)this._ComboBoxNumeroAffaire.SelectedItem).Salarie == null)
                {
                    ((Affaire)this._ComboBoxNumeroAffaire.SelectedItem).Salarie = ((Versions)this.DataContext).Salarie;
                }
                if (((Affaire)this._ComboBoxNumeroAffaire.SelectedItem).Entreprise_Mere1 == null)
                {
                    if (((Versions)this.DataContext).Salarie != null)
                    {
                        if (((Versions)this.DataContext).Salarie.Salarie_Interne != null)
                        {
                            if (((Versions)this.DataContext).Salarie.Salarie_Interne.Entreprise_Mere1 != null)
                            {
                                ((Affaire)this._ComboBoxNumeroAffaire.SelectedItem).Entreprise_Mere1 = ((Versions)this.DataContext).Salarie.Salarie_Interne.Entreprise_Mere1;
                            }
                        }
                    }
                }
                if (((Affaire)this._ComboBoxNumeroAffaire.SelectedItem).Avancement_Reel == null)
                {
                    ((Affaire)this._ComboBoxNumeroAffaire.SelectedItem).Avancement_Reel = 1;
                }
                if ((Versions)this.DataContext != null)
                {
                    ObservableCollection<Regie> toRemove = new ObservableCollection<Regie>();
                    foreach (Regie item in ((Versions)this.DataContext).Regie)
                    {
                        if (item.Affaire1 != ((Affaire)this._ComboBoxNumeroAffaire.SelectedItem))
                        {
                            toRemove.Add(item);
                        }
                    }
                    foreach (Regie item in toRemove)
                    {
                        item.Versions1 = null;
                    }
                }
                ObservableCollection<Regie> toput = new ObservableCollection<Regie>(((Affaire)this._ComboBoxNumeroAffaire.SelectedItem).Regie.OrderBy(reg => reg.Numero));
                ObservableCollection<Regie> toRemoveRegie = new ObservableCollection<Regie>();
                foreach (Regie item in toput)
                {
                    if (item.Versions1 != null)
                    {
                        toRemoveRegie.Add(item);
                    }
                }
                foreach (Regie item in toRemoveRegie)
                {
                    toput.Remove(item);
                }
                //if ((Versions)this.DataContext != null)
                //{
                //    foreach (Regie item in ((Versions)this.DataContext).Regie)
                //    {
                //        if (toput.Contains(item))
                //        {
                //            try
                //            {
                //                toput.Remove(item);
                //            }
                //            catch (Exception) { }
                //        }
                //    }
                //}
                this._dataGridRegiesGauche.ItemsSource = toput;
            }
            else
            {
                this._dataGridRegiesGauche.ItemsSource = new ObservableCollection<Regie>();
            }
        }

        #region TextChanged

        private void _TextBoxAchatsFourniture_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_TextChanged((TextBox)e.OriginalSource, e);
            TextBoxVerifNumeric(sender, e);
            this.CalculMargeFourniture();
            this.CalculMontantAchats();
        }

        private void _TextBoxVentesFourniture_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_TextChanged((TextBox)e.OriginalSource, e);
            TextBoxVerifNumeric(sender, e);
            this.CalculMargeFourniture();
            this.CalculMontantVentes();
        }

        private void _TextBoxAchatsSousTraitance_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_TextChanged((TextBox)e.OriginalSource, e);
            TextBoxVerifNumeric(sender, e);
            this.CalculMargeSousTraitance();
            this.CalculMontantAchats();
        }

        private void _TextBoxVentesSousTraitance_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_TextChanged((TextBox)e.OriginalSource, e);
            TextBoxVerifNumeric(sender, e);
            this.CalculMargeSousTraitance();
            this.CalculMontantVentes();
        }

        private void _TextBoxAchatsMontant_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_TextChanged((TextBox)e.OriginalSource, e);
            this.CalculMargeTotalAchatsVentes();
        }

        private void _TextBoxVentesMontant_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_TextChanged((TextBox)e.OriginalSource, e);
            this.CalculMargeTotalAchatsVentes();
            this.CalculMontantDevisNegocie();
        }

        private void _TextBoxNbHeures_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_TextChanged((TextBox)e.OriginalSource, e);
            TextBoxVerifNumeric(sender, e);
            this.CalculTauxHoraire();
        }

        private void _TextBoxTotalVenteHeure_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_TextChanged((TextBox)e.OriginalSource, e);
            TextBoxVerifNumeric(sender, e);
            this.CalculTauxHoraire();
            this.CalculMontantDevisNegocie();
        }

        private void _TextBoxMontantDevisInitial_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_TextChanged((TextBox)e.OriginalSource, e);
            this.CalculRemise();
            this.CalculMontantRemise();
        }

        private void _TextBoxMontantDevisNegoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_TextChanged((TextBox)e.OriginalSource, e);
            this.CalculRemise();
            this.CalculMontantRemise();
        }

        #endregion

        #region KeyUp (virgule)

        private void _TextBoxVirgule_KeyUp(object sender, KeyEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
        }

        #endregion

        #endregion

        #region Lecture seule

        private void verrouillerVersions()
        {
            this._ComboBoxNumeroAffaire.IsEnabled = false;
            this._DatePickerCommandeClient.IsEnabled = false;

            this._TextBoxBonCommandeClient.IsReadOnly = true;
            this.ColonneCommentaire.IsReadOnly = true;
            this._TextBoxCommentaire.IsReadOnly = true;

            this._TextBoxAchatsFourniture.IsReadOnly = true;
            this._TextBoxAchatsSousTraitance.IsReadOnly = true;
            this._TextBoxVentesFourniture.IsReadOnly = true;
            this._TextBoxVentesSousTraitance.IsReadOnly = true;
            this._TextBoxNbHeures.IsReadOnly = true;
            this._TextBoxTauxHoraire.IsReadOnly = true;
            this._TextBoxTotalVenteHeure.IsReadOnly = true;
            this._TextBoxMontantDevisInitial.IsReadOnly = true;
            this._TextBoxRemise.IsReadOnly = true;

            this._DatePickerFax.IsEnabled = false;
            this._DatePickerMail.IsEnabled = false;
            this._DatePickerCourrier.IsEnabled = false;
            this._DatePickerRelance.IsEnabled = false;

            this._ButtonVersDroite.IsEnabled = false;
            this._ButtonVersGauche.IsEnabled = false;
        }

        private void deverrouillerVersions()
        {
            this._ComboBoxNumeroAffaire.IsEnabled = true;
            this._DatePickerCommandeClient.IsEnabled = true;

            this._TextBoxBonCommandeClient.IsReadOnly = false;
            this.ColonneCommentaire.IsReadOnly = false;
            this._TextBoxCommentaire.IsReadOnly = false;

            this._TextBoxAchatsFourniture.IsReadOnly = false;
            this._TextBoxAchatsSousTraitance.IsReadOnly = false;
            this._TextBoxVentesFourniture.IsReadOnly = false;
            this._TextBoxVentesSousTraitance.IsReadOnly = false;
            this._TextBoxNbHeures.IsReadOnly = false;
            this._TextBoxTauxHoraire.IsReadOnly = false;
            this._TextBoxTotalVenteHeure.IsReadOnly = false;
            this._TextBoxMontantDevisInitial.IsReadOnly = false;
            this._TextBoxRemise.IsReadOnly = false;

            this._DatePickerFax.IsEnabled = true;
            this._DatePickerMail.IsEnabled = true;
            this._DatePickerCourrier.IsEnabled = true;
            this._DatePickerRelance.IsEnabled = true;

            this._ButtonVersDroite.IsEnabled = true;
            this._ButtonVersGauche.IsEnabled = true;
        }

        #endregion

        private void _buttonGaucheDroite_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridRegiesGauche.SelectedItem != null && this._dataGridRegiesGauche.SelectedItems.Count == 1)
            {
                ((Regie)this._dataGridRegiesGauche.SelectedItem).Versions1 = ((Versions)this.DataContext);
                ObservableCollection<Regie> toput = new ObservableCollection<Regie>(this._dataGridRegiesGauche.ItemsSource.OfType<Regie>());
                toput.Remove((Regie)this._dataGridRegiesGauche.SelectedItem);
                this._dataGridRegiesGauche.ItemsSource = toput;
            }
        }

        private void _buttonDroiteGauche_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridRegiesDroit.SelectedItem != null && this._dataGridRegiesDroit.SelectedItems.Count == 1)
            {
                ObservableCollection<Regie> toput = new ObservableCollection<Regie>(this._dataGridRegiesGauche.ItemsSource.OfType<Regie>());
                toput.Add((Regie)this._dataGridRegiesDroit.SelectedItem);
                ((Regie)this._dataGridRegiesDroit.SelectedItem).Versions1 = null;
                this._dataGridRegiesGauche.ItemsSource = toput;
            }
        }

        private void NullAffaire_Click_1(object sender, RoutedEventArgs e)
        {
            this._ComboBoxNumeroAffaire.SelectedItem = null;
        }

    }
}