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

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour DevisAjoutVersionWindow.xaml
    /// </summary>
    public partial class DevisAjoutVersionWindow : Window
    {
        Devis tmp;
        public Boolean creation = false;

        public DevisAjoutVersionWindow(Devis dev)
        {
            InitializeComponent();
            tmp = dev;
            this.listDevis_Etat = new ObservableCollection<Devis_Etat>(((App)App.Current).mySitaffEntities.Devis_Etat.OrderBy(de => de.Libelle));
            this.Type_Version = new ObservableCollection<Version_Type>(((App)App.Current).mySitaffEntities.Version_Type.OrderBy(ver => ver.Code));
            this.Charge_Affaire = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.Where(sal => sal.Charge_Affaire == true).OrderBy(sal => sal.Personne.Nom).ThenBy(sal => sal.Personne.Prenom));
        }

        # region Propriétés de dépendances



        public ObservableCollection<Devis_Etat> listDevis_Etat
        {
            get { return (ObservableCollection<Devis_Etat>)GetValue(listDevis_EtatProperty); }
            set { SetValue(listDevis_EtatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listDevis_Etat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listDevis_EtatProperty =
            DependencyProperty.Register("listDevis_Etat", typeof(ObservableCollection<Devis_Etat>), typeof(DevisAjoutVersionWindow), new UIPropertyMetadata(null));

        

        public ObservableCollection<Version_Type> Type_Version
        {
            get { return (ObservableCollection<Version_Type>)GetValue(Type_VersionProperty); }
            set { SetValue(Type_VersionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type_Version.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Type_VersionProperty =
            DependencyProperty.Register("Type_Version", typeof(ObservableCollection<Version_Type>), typeof(DevisAjoutVersionWindow), new UIPropertyMetadata(null));




        public ObservableCollection<Salarie> Charge_Affaire
        {
            get { return (ObservableCollection<Salarie>)GetValue(Charge_AffaireProperty); }
            set { SetValue(Charge_AffaireProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Charge_Affaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Charge_AffaireProperty =
            DependencyProperty.Register("Charge_Affaire", typeof(ObservableCollection<Salarie>), typeof(DevisAjoutVersionWindow), new UIPropertyMetadata(null));




        #endregion

        #region Verification

        public bool verificationChamps()
        {
            bool test = true;

            if (!_verif_typeVersion())
            {
                test = false;
            }
            if (!_verif_ChargeAffaire())
            {
                test = false;
            }
            if (!_verif_Montant())
            {
                test = false;
            }
            if (!_verif_Commentaire())
            {
                test = false;
            }
            if (!this.Verif_ComboBoxDevisAJoutVersionDevis_Etat())
            {
                test = false;
            }            

            return test;
        }

        #region TypeVersion
        private bool _verif_typeVersion()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxDevisAJoutVersionTypeVersion, this._TextBlockDevisAJoutVersionTypeVersion);
        }

        private void _ComboBoxDevisAJoutVersionTypeVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._verif_typeVersion();
        }
        #endregion

        #region ChargeAffaire
        private bool _verif_ChargeAffaire()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxDevisAJoutVersionCA, this._TextBlockDevisAJoutVersionCA);
        }

        private void _ComboBoxDevisAJoutVersionCA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._verif_ChargeAffaire();
        }
        #endregion

        #region Montant
        private bool _verif_Montant()
        {
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._TextBoxDevisAjoutVersionMontant, this._TextBlockDevisAjoutVersionMontant);
        }

        private void _TextBoxDevisAjoutVersionMontant_TextChanged(object sender, TextChangedEventArgs e)
        {
            this._verif_Montant();
        }
        #endregion

        #region Commentaire
        private bool _verif_Commentaire()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxDevisAjoutVersionDesignation, this._TextBlockDevisAjoutVersionDesignation);
        }

        private void _TextBoxDevisAjoutVersionDesignation_TextChanged(object sender, TextChangedEventArgs e)
        {
            this._verif_Commentaire();
        }
        #endregion

        #region Etat devis

        private bool Verif_ComboBoxDevisAJoutVersionDevis_Etat()
        {
            bool verif = true;

            if (((Versions)this.DataContext).VerifEtatDevisVersion)
            {
				verif = ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxDevisAJoutVersionDevis_Etat, this._TextBlockDevisAJoutVersionDevis_Etat);
            }
            else
            {
				this._ComboBoxDevisAJoutVersionDevis_Etat.SelectedItem = null;

				verif = ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._ComboBoxDevisAJoutVersionDevis_Etat, this._TextBlockDevisAJoutVersionDevis_Etat);
            }

            return verif;
        }

        private void _ComboBoxDevisAJoutVersionDevis_Etat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxDevisAJoutVersionDevis_Etat();
        }

        #endregion

        #endregion

        #region boutons ok / annuler

        private void _ButtonDevisAjoutVersionValider_Click(object sender, RoutedEventArgs e)
        {
            if (this.verificationChamps())
            {
                if (creation)
                {
                    ObservableCollection<Versions> lesVersions = new ObservableCollection<Versions>(this.tmp.Versions.Where(ve => ve.Version_Type1.Identifiant == ((Version_Type)this._ComboBoxDevisAJoutVersionTypeVersion.SelectedItem).Identifiant).OrderBy(ve => ve.Numero));
                    String tmpChaine = "";
                    if (lesVersions.Count() <= 0)
                    {
                        ((Versions)this.DataContext).Numero = ((Version_Type)this._ComboBoxDevisAJoutVersionTypeVersion.SelectedItem).Code + "1";
                        _TextBoxDevisAjoutVersionNumero.Text = ((Version_Type)this._ComboBoxDevisAJoutVersionTypeVersion.SelectedItem).Code + "1";
                    }
                    else
                    {
                        int test = 0;
                        foreach (Versions v in lesVersions)
                        {                            
                            if (int.Parse(v.Numero.Replace(((Version_Type)this._ComboBoxDevisAJoutVersionTypeVersion.SelectedItem).Code, "")) > test)
                            {
                                test = int.Parse(v.Numero.Replace(((Version_Type)this._ComboBoxDevisAJoutVersionTypeVersion.SelectedItem).Code, ""));
                                tmpChaine = v.Numero;
                            }
                        }
                        tmpChaine.Replace(((Version_Type)this._ComboBoxDevisAJoutVersionTypeVersion.SelectedItem).Code, "");
                        String lastChaine = "";
                        foreach (char c in tmpChaine)
                        {
                            if (((Version_Type)this._ComboBoxDevisAJoutVersionTypeVersion.SelectedItem).Code.ToCharArray().Contains(c))
                            {

                            }
                            else
                            {
                                lastChaine += c;
                            }
                        }
                        ((Versions)this.DataContext).Numero = ((Version_Type)this._ComboBoxDevisAJoutVersionTypeVersion.SelectedItem).Code + (int.Parse(lastChaine) + 1).ToString();
                        _TextBoxDevisAjoutVersionNumero.Text = ((Version_Type)this._ComboBoxDevisAJoutVersionTypeVersion.SelectedItem).Code + (int.Parse(lastChaine) + 1).ToString();
                    }
                }
                ((Versions)this.DataContext).Commande1 = new Commande();
                this.DialogResult = true;
                this.Close();
            }
        }

        private void _ButtonDevisAjoutVersionAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!creation)
            {
                this._TextBoxDevisAjoutVersionMontant.Text = "";
                this._ComboBoxDevisAJoutVersionTypeVersion.IsEnabled = false;
            }
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

    }
}
