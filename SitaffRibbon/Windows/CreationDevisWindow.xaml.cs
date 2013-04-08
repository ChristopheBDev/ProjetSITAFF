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
using SitaffRibbon.UserControls;


namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour CreationDevisWindow.xaml
    /// </summary>
    public partial class CreationDevisWindow : Window
    {
        #region attribut

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public CreationDevisWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxCreationDevisTitreDevis.Focus();
        }

        private void initialisationPropDependance()
        {
            this.Charge_Affaire = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.Where(sal => sal.Charge_Affaire == true).OrderBy(sal => sal.Personne.Nom).ThenBy(sal => sal.Personne.Prenom));
            this.Etat_Devis = new ObservableCollection<Devis_Etat>(((App)App.Current).mySitaffEntities.Devis_Etat.OrderBy(eta => eta.Libelle));
            this.Type_Devis = new ObservableCollection<Devis_Type>(((App)App.Current).mySitaffEntities.Devis_Type.OrderBy(typ => typ.Libelle));
            this.listClient = new ObservableCollection<Client>(((App)App.Current).mySitaffEntities.Client.OrderBy(cli => cli.Entreprise.Libelle));
            this.Secteur_Activite = new ObservableCollection<Activite>(((App)App.Current).mySitaffEntities.Activite.OrderBy(act => act.Libelle));
            this.lesTVA = new ObservableCollection<Tva>(((App)App.Current).mySitaffEntities.Tva.OrderBy(tv => tv.Taux));
        }

        #endregion

        #region variables

        public bool creation = false;

        #endregion

        #region Propriétés de dépendances



        public ObservableCollection<Salarie> Charge_Affaire
        {
            get { return (ObservableCollection<Salarie>)GetValue(Charge_AffaireProperty); }
            set { SetValue(Charge_AffaireProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Charge_Affaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Charge_AffaireProperty =
            DependencyProperty.Register("Charge_Affaire", typeof(ObservableCollection<Salarie>), typeof(CreationDevisWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Devis_Etat> Etat_Devis
        {
            get { return (ObservableCollection<Devis_Etat>)GetValue(Etat_DevisProperty); }
            set { SetValue(Etat_DevisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Etat_Devis.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Etat_DevisProperty =
            DependencyProperty.Register("Etat_Devis", typeof(ObservableCollection<Devis_Etat>), typeof(CreationDevisWindow), new UIPropertyMetadata(null));




        public ObservableCollection<Devis_Type> Type_Devis
        {
            get { return (ObservableCollection<Devis_Type>)GetValue(Type_DevisProperty); }
            set { SetValue(Type_DevisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type_Devis.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Type_DevisProperty =
            DependencyProperty.Register("Type_Devis", typeof(ObservableCollection<Devis_Type>), typeof(CreationDevisWindow), new UIPropertyMetadata(null));




        public ObservableCollection<Client> listClient
        {
            get { return (ObservableCollection<Client>)GetValue(listClientProperty); }
            set { SetValue(listClientProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listClient.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listClientProperty =
            DependencyProperty.Register("listClient", typeof(ObservableCollection<Client>), typeof(CreationDevisWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Activite> Secteur_Activite
        {
            get { return (ObservableCollection<Activite>)GetValue(Secteur_ActiviteProperty); }
            set { SetValue(Secteur_ActiviteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Secteur_Activite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Secteur_ActiviteProperty =
            DependencyProperty.Register("Secteur_Activite", typeof(ObservableCollection<Activite>), typeof(CreationDevisWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Tva> lesTVA
        {
            get { return (ObservableCollection<Tva>)GetValue(lesTVAProperty); }
            set { SetValue(lesTVAProperty, value); }
        }

        // Using a DependencyProperty as the backing store for lesTVA.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty lesTVAProperty =
            DependencyProperty.Register("lesTVA", typeof(ObservableCollection<Tva>), typeof(CreationDevisWindow), new UIPropertyMetadata(null));




        #endregion

        #region boutons

        #region boutons ok / annuler

        private void _ButtonCreationDevisValider_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                if (creation)
                {
                    Entreprise_Mere entmere_tmp = ((Salarie)this._ComboBoxCreationDevisCAPrincipal.SelectedItem).Salarie_Interne.Entreprise_Mere1;
                    ObservableCollection<Taux_Horaire> lesTaux = new ObservableCollection<Taux_Horaire>(((App)App.Current).mySitaffEntities.Taux_Horaire.Where(tx => tx.Date_Debut <= DateTime.Today && tx.Date_Fin >= DateTime.Today && tx.Entreprise_Mere1.Identifiant == entmere_tmp.Identifiant));
                    if (lesTaux.Count != 1)
                    {
                        if (lesTaux.Count > 1)
                        {
                            MessageBox.Show("Une erreur est présente. Plus d'un taux horaire est présent pour la date du " + DateTime.Today.Date.ToShortDateString() + " pour l'entreprise mere du chargé d'affaire principal " + entmere_tmp.Nom.ToString() + ". Veuillez d'abord corriger l'erreur avant de faire autre chose. Désolé du dérangement", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        if (lesTaux.Count <= 0)
                        {
                            MessageBox.Show("Aucun taux horaire n'existe pour la date du " + DateTime.Today.Date.ToShortDateString() + " pour l'entreprise mere du chargé d'affaire principal " + entmere_tmp.Nom.ToString() + ". Veuillez d'abord corriger l'erreur avant de faire autre chose. Désolé du dérangement", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        foreach (Taux_Horaire tx in lesTaux)
                        {
                            ((Devis)this.DataContext).Taux_Horaire1 = tx;
                        }
                        this.creationNumDevis();
                        ((Devis)this.DataContext).Utilisateur = ((App)App.Current)._connectedUser;
                        ((Devis)this.DataContext).Entreprise_Mere1 = ((Salarie)this._ComboBoxCreationDevisCAPrincipal.SelectedItem).Salarie_Interne.Entreprise_Mere1;
                        MessageBox.Show("Votre devis sera enregistré sous le numéro " + this._TextBoxCreationDevisNumeroDevis.Text.ToString() + ".", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.DialogResult = true;
                        this.Close();
                    }
                }
                else
                {
                    this.DialogResult = true;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Les données à ajouter ne sont pas conformes.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void _ButtonCreationDevisAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        #region boutons Contacts

        private void _ButtonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridCreationDevisContact.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous devez sélectionner un seul contact à supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (this._dataGridCreationDevisContact.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vous devez sélectionner un seul contact à supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (this._dataGridCreationDevisContact.SelectedItem != null)
            {
                ((Devis)this.DataContext).Devis_Contact.Remove((Devis_Contact)this._dataGridCreationDevisContact.SelectedItem);
            }
        }

        private void _ButtonNouveau_Click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxCreationDevisNom.SelectedItem != null)
            {
                DevisChoixContactWindow devisChoixContactWindow = new DevisChoixContactWindow(((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise);
                devisChoixContactWindow.Contacts = new ObservableCollection<Contact>(((App)App.Current).mySitaffEntities.Contact.Where(con => con.Personne.Entreprise1.Libelle == ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Libelle).OrderBy(con => con.Personne.Nom));

                devisChoixContactWindow.devis = ((Devis)this.DataContext);
                bool? dialogResult = devisChoixContactWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    Devis_Contact devis_contact = new Devis_Contact();
                    devis_contact.Contact1 = (Contact)devisChoixContactWindow.DataContext;
                    ((Devis)this.DataContext).Devis_Contact.Add(devis_contact);
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un client principal afin d'ajouter un contact à votre devis.", "Veuillez sélectionner un client principal", MessageBoxButton.OK);
            }
        }

        #endregion

        #region boutons Versions

        private void _ButtonVersionModifier_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridCreationDevisVersion.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner une version à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridCreationDevisVersion.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'une version à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridCreationDevisVersion.SelectedItem != null)
            {
                DevisAjoutVersionWindow devisAjoutVersionWindow = new DevisAjoutVersionWindow((Devis)this.DataContext);
                devisAjoutVersionWindow.DataContext = (Versions)this._dataGridCreationDevisVersion.SelectedItem;
                ((Versions)devisAjoutVersionWindow.DataContext).Date_Modification = DateTime.Today;


                bool? dialogResult = devisAjoutVersionWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    this._dataGridCreationDevisVersion.Items.Refresh();
                }
            }
        }

        private void _ButtonVersionSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridCreationDevisVersion.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous devez sélectionner une seule version à supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (this._dataGridCreationDevisVersion.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vous devez sélectionner une version à supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (this._dataGridCreationDevisVersion.SelectedItem != null)
            {
                Versions toDelete = (Versions)this._dataGridCreationDevisVersion.SelectedItem;
                try
                {
                    if (toDelete.Commande1 != null)
                    {
                        ((App)App.Current).mySitaffEntities.Commande.DeleteObject(toDelete.Commande1);
                    }
                    ((App)App.Current).mySitaffEntities.Versions.DeleteObject(toDelete);
                }
                catch (Exception)
                {
                    if (((Versions)this._dataGridCreationDevisVersion.SelectedItem).Commande1 != null)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(toDelete.Commande1);
                    }
                    ((App)App.Current).mySitaffEntities.Detach(toDelete);
                }
            }
        }

        private void _ButtonVersionNouveau_Click_1(object sender, RoutedEventArgs e)
        {
            DevisAjoutVersionWindow devisAjoutVersionWindow = new DevisAjoutVersionWindow((Devis)this.DataContext);
            devisAjoutVersionWindow.DataContext = new Versions();
            devisAjoutVersionWindow.creation = true;
            if (this._ComboBoxCreationDevisCAPrincipal.SelectedItem != null)
            {
                ((Versions)devisAjoutVersionWindow.DataContext).Salarie = (Salarie)this._ComboBoxCreationDevisCAPrincipal.SelectedItem;
            }
            ((Versions)devisAjoutVersionWindow.DataContext).Date_Creation = DateTime.Today;
            ((Versions)devisAjoutVersionWindow.DataContext).Date_Modification = DateTime.Today;

            //Temp
            ((Versions)devisAjoutVersionWindow.DataContext).Montant_Options = 0;
            ((Versions)devisAjoutVersionWindow.DataContext).Taux_Remise = 0;
            ((Versions)devisAjoutVersionWindow.DataContext).Remise = 0;
            ((Versions)devisAjoutVersionWindow.DataContext).Montant_Remise = 0;
            ((Versions)devisAjoutVersionWindow.DataContext).Coeff_Difficulte_Taux_Horaire = 0;
            //Fin Temp

            bool? dialogResult = devisAjoutVersionWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                ((Devis)this.DataContext).Versions.Add((Versions)devisAjoutVersionWindow.DataContext);
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Versions)devisAjoutVersionWindow.DataContext);
                }
                catch (Exception) { }
            }
        }

        #endregion

        #region boutons Activités

        private void _ButtonVersDroite_click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxActivite.SelectedItem != null && this._ComboBoxActivite.SelectedItems.Count == 1)
            {
                Devis_Activite temp = new Devis_Activite();
                temp.Activite1 = (Activite)this._ComboBoxActivite.SelectedItem;
                ((Devis)DataContext).Devis_Activite.Add(temp);
                this.Secteur_Activite.Remove((Activite)this._ComboBoxActivite.SelectedItem);
            }
        }

        private void _ButtonVersGauche_click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxActiviteOfDevis.SelectedItem != null && this._ComboBoxActiviteOfDevis.SelectedItems.Count == 1)
            {
                this.Secteur_Activite.Add((Activite)((Devis_Activite)this._ComboBoxActiviteOfDevis.SelectedItem).Activite1);
                ((Devis)DataContext).Devis_Activite.Remove((Devis_Activite)this._ComboBoxActiviteOfDevis.SelectedItem);
            }
        }

        #endregion

        #endregion

        #region Vérification

        private bool VerificationChamps()
        {
            bool test = true;

            if (!this.VerificationPrincipale())
            {
                test = false;
            }
            if (!this.VerificationClient())
            {
                test = false;
            }
            if (!this.VerificationActivites())
            {
                test = false;
            }
            if (!this.VerificationVersions())
            {
                test = false;
            }

            return test;
        }

        #region Verif Principale

        private bool VerificationPrincipale()
        {
            bool test = true;

            //if (!this.Verif_TextBoxCreationDevisNumeroDevis())
            //{
            //    test = false;
            //}
            if (!this.Verif_TextBoxCreationDevisTitreDevis())
            {
                test = false;
            }
            if (!this.Verif_ComboBoxCreationDevisCAPrincipal())
            {
                test = false;
            }
            if (!this.Verif_ComboBoxCreationDevisCAPrincipal())
            {
                test = false;
            }
            if (!this.Verif_ComboBoxCreationDevisChargeEtude())
            {
                test = false;
            }
            if (!this.Verif_ComboBoxCreationDevisEtatDevis())
            {
                test = false;
            }
            if (!this.Verif_ComboBoxCreationDevisTypeDevis())
            {
                test = false;
            }
            if (!this.Verif_ComboBoxCreationDevisCASecondaire())
            {
                test = false;
            }
            if (!this.Verif_ComboBoxCreationDevisTVA())
            {
                test = false;
            }
            if (!this.Verif_TextBoxCreationDevisCommentaire_Devis_Etat())
            {
                test = false;
            }

            return test;
        }

        #region TextBox Numero Devis

        private bool Verif_TextBoxCreationDevisNumeroDevis()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxCreationDevisNumeroDevis, this._TextBlockNumeroDevis,255);
        }

        private void _TextBoxCreationDevisNumeroDevis_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_TextBoxCreationDevisNumeroDevis();
        }

        #endregion

        #region TextBox Titre Devis

        private bool Verif_TextBoxCreationDevisTitreDevis()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxCreationDevisTitreDevis, this._TextBlockTitreDevis, 255); ;
        }

        private void _TextBoxCreationDevisTitreDevis_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_TextBoxCreationDevisTitreDevis();
        }

        #endregion

        #region ComboBox Chargé d'Affaire Principal

        private bool Verif_ComboBoxCreationDevisCAPrincipal()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxCreationDevisCAPrincipal, this._TextBlockCAPrincipal);
        }

        #endregion

        #region ComboBox Chargé d'étude

        private bool Verif_ComboBoxCreationDevisChargeEtude()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._ComboBoxCreationDevisChargeEtude, this._TextBlockChargeEtude);
        }

        private void _ComboBoxCreationDevisChargeEtude_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxCreationDevisChargeEtude();
        }

        #endregion

        #region ComboBox Etat du Devis

        private bool Verif_ComboBoxCreationDevisEtatDevis()
        {
            if (((Devis)this.DataContext).VerifEtatDevis)
            {
				return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxCreationDevisEtatDevis, this._TextBlockEtatDevis);
            }
            else
			{
				return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._ComboBoxCreationDevisEtatDevis, this._TextBlockEtatDevis);
            }
        }

        private void _ComboBoxCreationDevisEtatDevis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxCreationDevisEtatDevis();
        }

        #endregion

        #region ComboBox Type du Devis

        private bool Verif_ComboBoxCreationDevisTypeDevis()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxCreationDevisTypeDevis, this._TextBlocktypeDevis);
        }

        private void _ComboBoxCreationDevisTypeDevis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxCreationDevisTypeDevis();
        }


        #endregion

        #region ComboBox Chargé d'Affaire Secondaire

        private bool Verif_ComboBoxCreationDevisCASecondaire()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._ComboBoxCreationDevisCASecondaire, this._TextBlockCASecondaire);
        }

        private void _ComboBoxCreationDevisCASecondaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxCreationDevisCASecondaire();
        }

        #endregion

        #region ComboBox TVA

        private bool Verif_ComboBoxCreationDevisTVA()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxCreationDevisTVA, this._TextBlockTVA);
        }

        private void _ComboBoxCreationDevisTVA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxCreationDevisTVA();
        }

        #endregion

        #region TextBox Titre Devis

        private bool Verif_TextBoxCreationDevisCommentaire_Devis_Etat()
        {
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxCreationDevisCommentaire_Devis_Etat, this._TextBlockCommentaireDEvis);
        }

        private void _TextBoxCreationDevisCommentaire_Devis_Etat_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_TextBoxCreationDevisCommentaire_Devis_Etat();
        }

        #endregion

        #endregion

        #region Vérif Client

        private bool VerificationClient()
        {
            bool test = true;

            if (!this.Verif_ComboBoxCreationDevisNom())
            {
                test = false;
            }
            if (!this.Verif_ComboBoxCreationDevisNomFact())
            {
                test = false;
            }
            if (!this.Verif_ComboBoxCreationDevisNomLivr())
            {
                test = false;
            }
			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemClient, test);

            return test;
        }


        #region ComboBox Client Principal

        private bool Verif_ComboBoxCreationDevisNom()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxCreationDevisNom, this._TextBlockCPSalarie);
        }

        #endregion

        #region ComboBox Client Facturation

        private bool Verif_ComboBoxCreationDevisNomFact()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxCreationDevisNomFact,this._textBlockCFNom);
        }

        #endregion

        #region ComboBox Client Livraison

        private bool Verif_ComboBoxCreationDevisNomLivr()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxCreationDevisNomLivr, this._textBlockCLNom);;
        }

        #endregion

        #endregion

        #region Verif Activités

        private bool VerificationActivites()
        {
            bool test = true;

			test = this.Verif_ComboBoxActiviteOfDevis();

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemActivites, test);

            return test;
        }

        #region ComboBox Activités

        private bool Verif_ComboBoxActiviteOfDevis()
        {
            bool verif = true;

            if (this._ComboBoxActiviteOfDevis.Items.Count == 0)
            {
                verif = false;
            }

			((App)App.Current).verifications.MettreDataGridEnCouleur(this._ComboBoxActiviteOfDevis, verif);

            return verif;
        }

        #endregion

        #endregion

        #region Versions

        private bool VerificationVersions()
        {
            bool test = true;

            test = this.Verif_DataGridVersions();

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabitemversions, test);

            return test;
        }

        #region ComboBox Activités

        private bool Verif_DataGridVersions()
        {
            bool verif = true;

            if (this._dataGridCreationDevisVersion.Items.Count == 0)
            {
                verif = false;
            }

			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridCreationDevisVersion, verif);

            return verif;
        }

        #endregion

        #endregion

        #endregion

        #region fonctions

        #region Lecture Seule

        public void Lecture_Seule()
        {
            //Entête
            _TextBoxCreationDevisNumeroDevis.IsEnabled = false;
            _TextBoxCreationDevisTitreDevis.IsEnabled = false;
            _ComboBoxCreationDevisCAPrincipal.IsEnabled = false;
            _ComboBoxCreationDevisChargeEtude.IsEnabled = false;
            _ComboBoxCreationDevisEtatDevis.IsEnabled = false;
            _ComboBoxCreationDevisTypeDevis.IsEnabled = false;
            _ComboBoxCreationDevisCASecondaire.IsEnabled = false;
            _ComboBoxCreationDevisTVA.IsEnabled = false;

            //Client Principal
            _ComboBoxCreationDevisNom.IsEnabled = false;
            NewEntreprise.IsEnabled = false;
            LookEntreprise.IsEnabled = false;
            _TextBoxCreationDevisClientAdresse.IsEnabled = false;
            _TextBoxCreationDevisClientVille.IsEnabled = false;
            _TextBoxCreationDevisClientZIPCP.IsEnabled = false;
            _TextBoxCreationDevisClientTelephonne.IsEnabled = false;
            _TextBoxCreationDevisClientFax.IsEnabled = false;
            _TextBoxCreationDevisClientPays.IsEnabled = false;

            //Client Faturation
            _ComboBoxCreationDevisNomFact.IsEnabled = false;
            _TextBoxCreationDevisClientAdresseFact.IsEnabled = false;
            _TextBoxCreationDevisClientVilleFact.IsEnabled = false;
            _TextBoxCreationDevisClientZIPCPFact.IsEnabled = false;
            _TextBoxCreationDevisClientTelephonneFact.IsEnabled = false;
            _TextBoxCreationDevisClientFaxFact.IsEnabled = false;
            _TextBoxCreationDevisClientPaysFact.IsEnabled = false;

            //Client Livraison
            _ComboBoxCreationDevisNomLivr.IsEnabled = false;
            _TextBoxCreationDevisClientAdresseLivr.IsEnabled = false;
            _TextBoxCreationDevisClientVilleLivr.IsEnabled = false;
            _TextBoxCreationDevisClientZIPCPLivr.IsEnabled = false;
            _TextBoxCreationDevisClientTelephonneLivr.IsEnabled = false;
            _TextBoxCreationDevisClientFaxLivr.IsEnabled = false;
            _TextBoxCreationDevisClientPaysLivr.IsEnabled = false;

            //Activité
            _ComboBoxActivite.IsEnabled = false;
            _ButtonVersDroite.IsEnabled = false;
            _ButtonVersGauche.IsEnabled = false;
            //_ComboBoxActiviteOfDevis.IsEnabled = false;
            this.ColonneActivites.IsReadOnly = true;

            //Contact
            //_dataGridCreationDevisContact.IsEnabled = false;
            this.colonneContact.IsReadOnly = true;
            _ButtonNouveau.IsEnabled = false;
            _ButtonSupprimer.IsEnabled = false;

            //Version
            //_dataGridCreationDevisVersion.IsEnabled = false;
            _ButtonVersionNouveau.IsEnabled = false;
            _ButtonVersionModifier.IsEnabled = false;
            _ButtonVersionSupprimer.IsEnabled = false;

        }

        #endregion

        #region creation numero devis

        private void _ComboBoxCreationDevisCAPrincipal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxCreationDevisCAPrincipal();

            if (this.creation)
            {
                if (this._ComboBoxCreationDevisCAPrincipal.SelectedItem != null)
                {
                    this.creationNumDevis();
                }
                else
                {
                    this._TextBoxCreationDevisNumeroDevis.Text = "";
                }
            }
        }

        private void creationNumDevis()
        {
            String numDevis;
            Salarie SalarieSelectionne = (Salarie)this._ComboBoxCreationDevisCAPrincipal.SelectedItem;

            //Ajout de l'identificateur
            numDevis = SalarieSelectionne.Salarie_Interne.Entreprise_Mere1.Identificateur;

            //Ajout de l'année
            int i = 0;
            foreach (Char c in DateTime.Today.Year.ToString())
            {
                if (i == 2 || i == 3)
                {
                    numDevis += c;
                }
                i++;
            }

            //ajout tu tiret "-"
            numDevis += "-";

            //ajout du matricule
            int nbCaracteres = 0;
            foreach (Char c in SalarieSelectionne.Salarie_Interne.Matricule_Interne.ToString())
            {
                nbCaracteres++;
            }
            if (nbCaracteres == 1)
            {
                numDevis += "0";
            }
            numDevis += SalarieSelectionne.Salarie_Interne.Matricule_Interne;

            ////ajout du slash "/"
            //numDevis += "/";
            //ajout du slash "-"
            numDevis += "-";

            //ajout de l'incrémentation
            ObservableCollection<Devis> devisAvecMemeDebutNumero = new ObservableCollection<Devis>(((App)App.Current).mySitaffEntities.Devis.Where(dev => dev.Numero.Contains(numDevis)).OrderBy(dev => dev.Numero));
            if (devisAvecMemeDebutNumero.Count() == 0)
            {
                numDevis += "01";
            }
            else
            {
                ObservableCollection<int> lesEntiersPourIncr = new ObservableCollection<int>();
                int PlusGrand = 0;
                foreach (Devis dev in devisAvecMemeDebutNumero)
                {
                    //PlusGrand = dev;
                    int test;
                    if (int.TryParse(dev.Numero.Replace(numDevis, ""), out test))
                    {
                        lesEntiersPourIncr.Add(int.Parse(dev.Numero.Replace(numDevis, "")));
                    }
                }
                foreach (int entier in lesEntiersPourIncr)
                {
                    if (entier > PlusGrand)
                    {
                        PlusGrand = entier;
                    }
                }
                //String incrementation = (int.Parse(PlusGrand.Numero.ToString().Substring(PlusGrand.Numero.ToString().Length - 2, 2)) + 1).ToString();
                String incrementation = (PlusGrand + 1).ToString();

                //Mise à deux chiffres si seulement un chiffre
                nbCaracteres = 0;
                foreach (Char c in incrementation)
                {
                    nbCaracteres++;
                }
                if (nbCaracteres == 1)
                {
                    numDevis += "0";
                }
                numDevis += incrementation;
            }

            //Mise dans le txtbox du numero de devis
            this._TextBoxCreationDevisNumeroDevis.Text = numDevis;
            ((Devis)this.DataContext).Numero = numDevis;
        }

        #endregion

        #region Adresses Auto

        #region Adresse auto client principal
        private void _ComboBoxCreationDevisNom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxCreationDevisNom();

            if (this._ComboBoxCreationDevisNom.SelectedItem != null)
            {
                if (((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1.Rue != null)
                {
                    this._TextBoxCreationDevisClientAdresse.Text = ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1.Rue;
                }
                else
                {
                    this._TextBoxCreationDevisClientAdresse.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1.Ville1 != null)
                {
                    this._TextBoxCreationDevisClientVille.Text = ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1.Ville1.Libelle;
                }
                else
                {
                    this._TextBoxCreationDevisClientVille.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1.Ville1 != null)
                {
                    this._TextBoxCreationDevisClientZIPCP.Text = ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1.Ville1.Code_Postal;
                }
                else
                {
                    this._TextBoxCreationDevisClientZIPCP.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Telephone != null)
                {
                    this._TextBoxCreationDevisClientTelephonne.Text = ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Telephone;
                }
                else
                {
                    this._TextBoxCreationDevisClientTelephonne.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Fax != null)
                {
                    this._TextBoxCreationDevisClientFax.Text = ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Fax;
                }
                else
                {
                    this._TextBoxCreationDevisClientFax.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1.Ville1 != null && ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1.Ville1.Pays1 != null)
                {
                    this._TextBoxCreationDevisClientPays.Text = ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1.Ville1.Pays1.Libelle;
                }
                else
                {
                    this._TextBoxCreationDevisClientPays.Text = "Non renseigné";
                }
                this._ComboBoxCreationDevisNomFact.SelectedItem = this._ComboBoxCreationDevisNom.SelectedItem;
                this._ComboBoxCreationDevisNomLivr.SelectedItem = this._ComboBoxCreationDevisNom.SelectedItem;
            }
            else
            {
                this._TextBoxCreationDevisClientAdresse.Text = "";
                this._TextBoxCreationDevisClientVille.Text = "";
                this._TextBoxCreationDevisClientZIPCP.Text = "";
                this._TextBoxCreationDevisClientTelephonne.Text = "";
                this._TextBoxCreationDevisClientFax.Text = "";
                this._TextBoxCreationDevisClientPays.Text = "";
            }
        }
        #endregion

        #region Adresse auto client facutration
        public void _ComboBoxCreationDevisNomFact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxCreationDevisNomFact();

            if (this._ComboBoxCreationDevisNomFact.SelectedItem != null)
            {
                if (((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Adresse1.Rue != null)
                {
                    this._TextBoxCreationDevisClientAdresseFact.Text = ((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Adresse1.Rue;
                }
                else
                {
                    this._TextBoxCreationDevisClientAdresseFact.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Adresse1.Ville1 != null)
                {
                    this._TextBoxCreationDevisClientVilleFact.Text = ((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Adresse1.Ville1.Libelle;
                }
                else
                {
                    this._TextBoxCreationDevisClientVilleFact.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Adresse1.Ville1 != null)
                {
                    this._TextBoxCreationDevisClientZIPCPFact.Text = ((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Adresse1.Ville1.Code_Postal;
                }
                else
                {
                    this._TextBoxCreationDevisClientZIPCPFact.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Telephone != null)
                {
                    this._TextBoxCreationDevisClientTelephonneFact.Text = ((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Telephone;
                }
                else
                {
                    this._TextBoxCreationDevisClientTelephonneFact.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Fax != null)
                {
                    this._TextBoxCreationDevisClientFaxFact.Text = ((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Fax;
                }
                else
                {
                    this._TextBoxCreationDevisClientFaxFact.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Adresse1.Ville1 != null && ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1.Ville1.Pays1 != null)
                {
                    this._TextBoxCreationDevisClientPaysFact.Text = ((Client)this._ComboBoxCreationDevisNomFact.SelectedItem).Entreprise.Adresse1.Ville1.Pays1.Libelle;
                }
                else
                {
                    this._TextBoxCreationDevisClientPaysFact.Text = "Non renseigné";
                }
            }
            else
            {
                this._TextBoxCreationDevisClientAdresseFact.Text = "";
                this._TextBoxCreationDevisClientVilleFact.Text = "";
                this._TextBoxCreationDevisClientZIPCPFact.Text = "";
                this._TextBoxCreationDevisClientTelephonneFact.Text = "";
                this._TextBoxCreationDevisClientFaxFact.Text = "";
                this._TextBoxCreationDevisClientPaysFact.Text = "";
            }
        }
        #endregion

        #region Adresse auto client livraison
        private void _ComboBoxCreationDevisNomLivr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxCreationDevisNomLivr();

            if (this._ComboBoxCreationDevisNomLivr.SelectedItem != null)
            {
                if (((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Adresse1.Rue != null)
                {
                    this._TextBoxCreationDevisClientAdresseLivr.Text = ((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Adresse1.Rue;
                }
                else
                {
                    this._TextBoxCreationDevisClientAdresseLivr.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Adresse1.Ville1 != null)
                {
                    this._TextBoxCreationDevisClientVilleLivr.Text = ((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Adresse1.Ville1.Libelle;
                }
                else
                {
                    this._TextBoxCreationDevisClientVilleLivr.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Adresse1.Ville1 != null)
                {
                    this._TextBoxCreationDevisClientZIPCPLivr.Text = ((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Adresse1.Ville1.Code_Postal;
                }
                else
                {
                    this._TextBoxCreationDevisClientZIPCPLivr.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Telephone != null)
                {
                    this._TextBoxCreationDevisClientTelephonneLivr.Text = ((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Telephone;
                }
                else
                {
                    this._TextBoxCreationDevisClientTelephonneLivr.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Fax != null)
                {
                    this._TextBoxCreationDevisClientFaxLivr.Text = ((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Fax;
                }
                else
                {
                    this._TextBoxCreationDevisClientFaxLivr.Text = "Non renseigné";
                }
                if (((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Adresse1 != null && ((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Adresse1.Ville1 != null && ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise.Adresse1.Ville1.Pays1 != null)
                {
                    this._TextBoxCreationDevisClientPaysLivr.Text = ((Client)this._ComboBoxCreationDevisNomLivr.SelectedItem).Entreprise.Adresse1.Ville1.Pays1.Libelle;
                }
                else
                {
                    this._TextBoxCreationDevisClientPaysLivr.Text = "Non renseigné";
                }
            }
            else
            {
                this._TextBoxCreationDevisClientAdresseLivr.Text = "";
                this._TextBoxCreationDevisClientVilleLivr.Text = "";
                this._TextBoxCreationDevisClientZIPCPLivr.Text = "";
                this._TextBoxCreationDevisClientTelephonneLivr.Text = "";
                this._TextBoxCreationDevisClientFaxLivr.Text = "";
                this._TextBoxCreationDevisClientPaysLivr.Text = "";
            }
        }
        #endregion

        #endregion

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #region Entreprise

        private void NewEntreprise_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((App)App.Current).mySitaffEntities.Detach((Devis)this.DataContext);
            }
            catch (Exception)
            {

            }
            ListeEntreprisesControl listeEntrepriseControl = new ListeEntreprisesControl();
            Entreprise entreprise = ((App)App.Current)._theMainWindow.AddEntreprises(listeEntrepriseControl);
            if (entreprise != null)
            {
                if (entreprise.Client != null)
                {
                    this.listClient = new ObservableCollection<Client>(((App)App.Current).mySitaffEntities.Client.OrderBy(cli => cli.Entreprise.Libelle));
                    this._ComboBoxCreationDevisNom.SelectedItem = entreprise.Client;
                }
                else
                {
                    MessageBox.Show("L'entreprise que vous avez ajouté n'a pas été définie en tant que 'client', vous ne pourrez donc pas la sélectionner", "Entreprise non client", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                this._ComboBoxCreationDevisNom.SelectedItem = null;
            }
        }

        private void LookEntreprise_Click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxCreationDevisNom.SelectedItem != null)
            {
                ListeEntreprisesControl listeEntrepriseControl = new ListeEntreprisesControl();
                ((App)App.Current)._theMainWindow.LookEntreprises(listeEntrepriseControl, ((Client)this._ComboBoxCreationDevisNom.SelectedItem).Entreprise);
            }
        }

        #endregion

    }
}
