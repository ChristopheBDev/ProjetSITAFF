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
using SitaffRibbon.Classes;
using SitaffRibbon.Windows.ParametresUserControls;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour EntrepriseWindow.xaml
    /// </summary>
    public partial class EntrepriseWindow : Window
    {

        #region Attributs

        public Boolean SoloLecture = false;
        public Boolean creation = false;

        #endregion

        #region Propriétés de dépendances



        public ObservableCollection<Statut> listStatut
        {
            get { return (ObservableCollection<Statut>)GetValue(listStatutProperty); }
            set { SetValue(listStatutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listStatut.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listStatutProperty =
            DependencyProperty.Register("listStatut", typeof(ObservableCollection<Statut>), typeof(EntrepriseWindow), new PropertyMetadata(null));



        public ObservableCollection<Condition_Reglement> list_Condition_Reglement
        {
            get { return (ObservableCollection<Condition_Reglement>)GetValue(list_Condition_ReglementProperty); }
            set { SetValue(list_Condition_ReglementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for list_Condition_Reglement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty list_Condition_ReglementProperty =
            DependencyProperty.Register("list_Condition_Reglement", typeof(ObservableCollection<Condition_Reglement>), typeof(EntrepriseWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Condition_Reglement> list_Condition_Reglement_Client
        {
            get { return (ObservableCollection<Condition_Reglement>)GetValue(list_Condition_Reglement_ClientProperty); }
            set { SetValue(list_Condition_Reglement_ClientProperty, value); }
        }

        // Using a DependencyProperty as the backing store for list_Condition_Reglement_Client.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty list_Condition_Reglement_ClientProperty =
            DependencyProperty.Register("list_Condition_Reglement_Client", typeof(ObservableCollection<Condition_Reglement>), typeof(EntrepriseWindow), new PropertyMetadata(null));



        public ObservableCollection<Activite> Secteur_Activite
        {
            get { return (ObservableCollection<Activite>)GetValue(Secteur_ActiviteProperty); }
            set { SetValue(Secteur_ActiviteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Secteur_Activite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Secteur_ActiviteProperty =
            DependencyProperty.Register("Secteur_Activite", typeof(ObservableCollection<Activite>), typeof(EntrepriseWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Mode_Facturation> ModeFacturationList
        {
            get { return (ObservableCollection<Mode_Facturation>)GetValue(ModeFacturationListProperty); }
            set { SetValue(ModeFacturationListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ModeFacturationList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeFacturationListProperty =
            DependencyProperty.Register("ModeFacturationList", typeof(ObservableCollection<Mode_Facturation>), typeof(EntrepriseWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Personne> ContactsList
        {
            get { return (ObservableCollection<Personne>)GetValue(ContactsListProperty); }
            set { SetValue(ContactsListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContactsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactsListProperty =
            DependencyProperty.Register("ContactsList", typeof(ObservableCollection<Personne>), typeof(EntrepriseWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Devise> DeviseList
        {
            get { return (ObservableCollection<Devise>)GetValue(DeviseListProperty); }
            set { SetValue(DeviseListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeviseList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeviseListProperty =
            DependencyProperty.Register("DeviseList", typeof(ObservableCollection<Devise>), typeof(EntrepriseWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Tva> TvaList
        {
            get { return (ObservableCollection<Tva>)GetValue(TvaListProperty); }
            set { SetValue(TvaListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TvaList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TvaListProperty =
            DependencyProperty.Register("TvaList", typeof(ObservableCollection<Tva>), typeof(EntrepriseWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Groupe> GroupList
        {
            get { return (ObservableCollection<Groupe>)GetValue(GroupListProperty); }
            set { SetValue(GroupListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GroupList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupListProperty =
            DependencyProperty.Register("GroupList", typeof(ObservableCollection<Groupe>), typeof(EntrepriseWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Pays> PaysList
        {
            get { return (ObservableCollection<Pays>)GetValue(PaysListProperty); }
            set { SetValue(PaysListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PaysList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaysListProperty =
            DependencyProperty.Register("PaysList", typeof(ObservableCollection<Pays>), typeof(EntrepriseWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Pays> PaysList2
        {
            get { return (ObservableCollection<Pays>)GetValue(PaysList2Property); }
            set { SetValue(PaysList2Property, value); }
        }

        // Using a DependencyProperty as the backing store for PaysList2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaysList2Property =
            DependencyProperty.Register("PaysList2", typeof(ObservableCollection<Pays>), typeof(EntrepriseWindow), new PropertyMetadata(null));

        

        public ObservableCollection<Statut> StatutList
        {
            get { return (ObservableCollection<Statut>)GetValue(StatutListProperty); }
            set { SetValue(StatutListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StatutList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatutListProperty =
            DependencyProperty.Register("StatutList", typeof(ObservableCollection<Statut>), typeof(EntrepriseWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Type_Entreprise> TypeList
        {
            get { return (ObservableCollection<Type_Entreprise>)GetValue(TypeListProperty); }
            set { SetValue(TypeListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TypeList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeListProperty =
            DependencyProperty.Register("TypeList", typeof(ObservableCollection<Type_Entreprise>), typeof(EntrepriseWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Ville> VilleList
        {
            get { return (ObservableCollection<Ville>)GetValue(VilleListProperty); }
            set { SetValue(VilleListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VilleList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VilleListProperty =
            DependencyProperty.Register("VilleList", typeof(ObservableCollection<Ville>), typeof(EntrepriseWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Ville> VilleList2
        {
            get { return (ObservableCollection<Ville>)GetValue(VilleList2Property); }
            set { SetValue(VilleList2Property, value); }
        }

        // Using a DependencyProperty as the backing store for VilleList2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VilleList2Property =
            DependencyProperty.Register("VilleList2", typeof(ObservableCollection<Ville>), typeof(EntrepriseWindow), new PropertyMetadata(null));

        

        #endregion

        #region Constructeur

        public EntrepriseWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Initialisation de la sécurité
            this.initialisationSecurite();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxCoordonnéesNom.Focus();
        }

        #region initialisation

        private void initialisationPropDependance()
        {
            this.VilleList = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));
            this.VilleList2 = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));
            this.Secteur_Activite = new ObservableCollection<Activite>(((App)App.Current).mySitaffEntities.Activite.OrderBy(act => act.Libelle));
            this.list_Condition_Reglement = new ObservableCollection<Condition_Reglement>(((App)App.Current).mySitaffEntities.Condition_Reglement.OrderBy(cr => cr.Libelle));
            this.list_Condition_Reglement_Client = new ObservableCollection<Condition_Reglement>(((App)App.Current).mySitaffEntities.Condition_Reglement.OrderBy(cr => cr.Libelle));
            this.GroupList = new ObservableCollection<Groupe>(((App)App.Current).mySitaffEntities.Groupe.OrderBy(gro => gro.Libelle));
            this.StatutList = new ObservableCollection<Statut>(((App)App.Current).mySitaffEntities.Statut.OrderBy(stat => stat.Libelle));
            this.TypeList = new ObservableCollection<Type_Entreprise>(((App)App.Current).mySitaffEntities.Type_Entreprise.OrderBy(type => type.Libelle));
            this.PaysList = new ObservableCollection<Pays>(((App)App.Current).mySitaffEntities.Pays.Where(pays => pays.Ville.Count() >= 1).OrderBy(pays => pays.Libelle));
            this.PaysList2 = new ObservableCollection<Pays>(((App)App.Current).mySitaffEntities.Pays.Where(pays => pays.Ville.Count() >= 1).OrderBy(pays => pays.Libelle));
            this.TvaList = new ObservableCollection<Tva>(((App)App.Current).mySitaffEntities.Tva.OrderBy(Tva => Tva.Libelle));
            this.ModeFacturationList = new ObservableCollection<Mode_Facturation>(((App)App.Current).mySitaffEntities.Mode_Facturation.OrderBy(Mode_Facturation => Mode_Facturation.Libelle));
            this.DeviseList = new ObservableCollection<Devise>(((App)App.Current).mySitaffEntities.Devise.OrderBy(Devise => Devise.Libelle));
            this.listStatut = new ObservableCollection<Statut>(((App)App.Current).mySitaffEntities.Statut.OrderBy(sta => sta.Libelle));
        }

        private void initialisationSecurite()
        {
            //Mise en place des droits sur les boutons et tabs

            if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl", "Add"))
            {
                this.NewGroupe.Visibility = Visibility.Collapsed;
            }
			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreGroupeControl", "Look"))
            {
                this.LookGroupe.Visibility = Visibility.Collapsed;
            }

			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreActiviteControl", "Add"))
            {
                this._newActivite.Visibility = Visibility.Collapsed;
            }

            if (!((App)App.Current)._connectedUser.Niveau_Securite1.EntrerpiseAdresseFacturation)
            {
                this._AdresseFacturation.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        #endregion

        #region Lecture Seule

        public void lectureSeule()
        {
            //TextBox
            _TextBoxCoordonnéesNom.IsReadOnly = true;
            _TextBoxCoordonnéesSIRET.IsReadOnly = true;
            _TextBoxCoordonnéesSiteWeb.IsReadOnly = true;
            _TextBoxCoordonnéesAPE.IsReadOnly = true;
            _TextBoxCommentaires.IsReadOnly = true;
            _TextBoxCoordonnéesAdresse.IsReadOnly = true;
            _TextBoxCoordonnéesBP.IsReadOnly = true;
            _TextBoxCoordonnéesCP.IsReadOnly = true;
            _TextBoxCoordonnéesTEL.IsReadOnly = true;
            _TextBoxCoordonnéesFAX.IsReadOnly = true;
            _TextBoxCoordonnéesMail.IsReadOnly = true;
            _TextBoxCodeClient.IsReadOnly = true;
            _TextBoxEscompteClient.IsReadOnly = true;
            _TextBoxRemiseClient.IsReadOnly = true;
            _TextBoxTarifMini.IsReadOnly = true;
            _TextBoxNbFacture.IsReadOnly = true;
            _TextBoxCodeFournisseur.IsReadOnly = true;
            _TextBoxCoefDelegation.IsReadOnly = true;
            _TextBoxCoefGestion.IsReadOnly = true;
            _TextBoxDelaiNormatif.IsReadOnly = true;
            _TextBoxIncertitudeFournisseur.IsReadOnly = true;
            _TextBoxDelaiRetourConsult.IsReadOnly = true;
            _TextBoxCommentairesLivraisonDelais.IsReadOnly = true;
            _TextBoxEscompte.IsReadOnly = true;
            _TextBoxRemiseCommerciale.IsReadOnly = true;
            _textBoxFraisDePort.IsReadOnly = true;

            //ComboBox
            _ComboBoxCoordonneesStatut.IsEnabled = false;
            _ComboBoxCoordonneesGroupe.IsEnabled = false;
            _ComboBoxCoordonneesTypeEntreprise.IsEnabled = false;
            _ComboBoxCoordonneesVille.IsEnabled = false;
            _ComboBoxCoordonneesPays.IsEnabled = false;
            _ComboBoxModeFacturation.IsEnabled = false;
            _ComboBoxDevise.IsEnabled = false;
            _ComboBoxTauxTVAClient.IsEnabled = false;
            _ComboBoxTauxTVA.IsEnabled = false;

            //DataGrid
            _dataGridContact.IsReadOnly = true;
            _ComboBoxActiviteEntreprise.IsReadOnly = true;
            _dataGridRIB.IsReadOnly = true;
            _dataGridNumIntraco.IsReadOnly = true;
            _dataGridEntrepriseConditionReglementClient.IsReadOnly = true;
            _dataGridClientCleComptable.IsReadOnly = true;
            _dataGridEntrepriseConditionReglement.IsReadOnly = true;
            _dataGridFournisseurtCleComptable.IsReadOnly = true;

            //Boutons
            _ButtonContactNouveau.IsEnabled = false;
            _ButtonContactModifier.IsEnabled = false;
            _ButtonContactSupprimer.IsEnabled = false;
            _ButtonVersDroite.IsEnabled = false;
            _ButtonVersGauche.IsEnabled = false;
            _buttonAjouterRIB.IsEnabled = false;
            _buttonModifierRIB.IsEnabled = false;
            _buttonSupprimerRIB.IsEnabled = false;
            _ButtonNumIntracoNouveau.IsEnabled = false;
            _ButtonNumIntracoModifier.IsEnabled = false;
            _ButtonNumIntracoSupprimer.IsEnabled = false;
            _buttonGaucheDroiteClient.IsEnabled = false;
            _buttonDroiteGaucheClient.IsEnabled = false;
            _buttonAjouterCleComptableClient.IsEnabled = false;
            _buttonModifierCleComptableClient.IsEnabled = false;
            _buttonSupprimerCleComptableClient.IsEnabled = false;
            _buttonGaucheDroite.IsEnabled = false;
            _buttonDroiteGauche.IsEnabled = false;
            _buttonAjouterCleComptableFournisseur.IsEnabled = false;
            _buttonModifierCleComptableFournisseur.IsEnabled = false;
            _buttonSupprimerCleComptableFournisseur.IsEnabled = false;

            //CheckBox
            _CheckBoxSousTraitant.IsEnabled = false;
            _CheckBoxInterimaire.IsEnabled = false;
            Bouton_Client.IsEnabled = false;
            _Bouton_Fournisseur.IsEnabled = false;
        }

        #endregion

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ReglageProprietesDependances();

            this.AfficherMasquerFenetre();

            if (this.SoloLecture)
            {
                this.lectureSeule();
            }

            //Remise du curseur normal
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #endregion

        #region Boutons

        #region Boutons ok / annuler

        /// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                if (_TextBoxCoordonnéesAdresseFact.Text == "" && this._TextBoxCoordonnéesBPFact.Text == "" && this._TextBoxCoordonnéesCPFact.Text == "" && this._ComboBoxCoordonneesVilleFact.SelectedItem == null && this._ComboBoxCoordonneesPaysFact.SelectedItem == null)
                {
                    ((Entreprise)this.DataContext).Adresse2 = null;
                }
                if (((App)App.Current).mySitaffEntities.Entreprise.Where(ent => ent.Identifiant != ((Entreprise)this.DataContext).Identifiant).Where(ent => ent.Libelle.ToLower() == ((Entreprise)this.DataContext).Libelle.ToLower()).Count() > 0)
                {
                    if (MessageBox.Show("Une entreprise est déjà présente avec ce nom, souhaitez-vous rééllement l'ajouter ? Nous vous conseillons de vérifier auparavant l'entreprise déjà existante sous ce nom.", "Doublon d'entreprise", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                    {
                        this.VerifOptionsAvantEnregistrement();
                        this.DialogResult = true;
                        this.Close();
                    }
                }
                else
                {
                    this.VerifOptionsAvantEnregistrement();
                    this.DialogResult = true;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Les données à,ajouter ne sont pas conformes.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

        #region boutons numero intraco

        private void _ButtonNumIntracoSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridNumIntraco.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous devez sélectionner un seule numéro de tva intracommunautaire à supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (this._dataGridNumIntraco.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vous devez sélectionner un numéro de tva intracommunautaire à supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (this._dataGridNumIntraco.SelectedItem != null)
            {
                ((Numero_Tva_Intraco)this._dataGridNumIntraco.SelectedItem).Entreprise1 = null;
            }
        }

        private void _ButtonNumIntracoModifier_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridNumIntraco.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner un numéro de tva intracommunautaire à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridNumIntraco.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'un numéro de tva intracommunautaire à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridNumIntraco.SelectedItem != null)
            {
                NumeroIntraCoWindow numeroIntraCoWindow = new NumeroIntraCoWindow();
                numeroIntraCoWindow.DataContext = (Numero_Tva_Intraco)this._dataGridNumIntraco.SelectedItem;

                bool? dialogResult = numeroIntraCoWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    this._dataGridNumIntraco.Items.Refresh();
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Numero_Tva_Intraco)numeroIntraCoWindow.DataContext);
                        this._dataGridNumIntraco.Items.Refresh();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void _ButtonNumIntracoNouveau_Click(object sender, RoutedEventArgs e)
        {
            NumeroIntraCoWindow numeroIntraCoWindow = new NumeroIntraCoWindow();
            numeroIntraCoWindow.DataContext = new Numero_Tva_Intraco();

            bool? dialogResult = numeroIntraCoWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                ((Numero_Tva_Intraco)numeroIntraCoWindow.DataContext).Entreprise1 = (Entreprise)this.DataContext;
            }
            else
            {
                ((App)App.Current).mySitaffEntities.Detach((Numero_Tva_Intraco)numeroIntraCoWindow.DataContext);
            }

            this._dataGridNumIntraco.Items.Refresh();
        }

        #endregion

        #region bouton contact

        private void _ButtonContactNouveau_Click(object sender, RoutedEventArgs e)
        {
            if (this._TextBoxCoordonnéesNom.Text.Trim() != "")
            {
                ContactWindow contactWindow = new ContactWindow();
                contactWindow.DataContext = new Personne();

                ObservableCollection<Entreprise> toPutOnComboBox = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.OrderBy(ent => ent.Libelle));
                if (!toPutOnComboBox.Contains((Entreprise)this.DataContext))
                {
                    toPutOnComboBox.Add((Entreprise)this.DataContext);
                }
                contactWindow.EntrepriseList = new ObservableCollection<Entreprise>(toPutOnComboBox.OrderBy(ent => ent.Libelle));

                ((Personne)contactWindow.DataContext).Entreprise1 = (Entreprise)this.DataContext;
                ((Personne)contactWindow.DataContext).Contact = new Contact();
                contactWindow._ComboBoxContactEntreprise.IsEnabled = false;
                bool? dialogResult = contactWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    ((Entreprise)this.DataContext).Personne.Add((Personne)contactWindow.DataContext);
                    this.ContactsList.Add((Personne)contactWindow.DataContext);
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach((Personne)contactWindow.DataContext);
                        ((Entreprise)this.DataContext).Personne.Remove((Personne)contactWindow.DataContext);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            ((Entreprise)this.DataContext).Personne.Remove((Personne)contactWindow.DataContext);
                            ((App)App.Current).mySitaffEntities.Detach((Personne)contactWindow.DataContext);
                        }
                        catch (Exception) { }
                    }
                }
                try
                {
                    this._dataGridContact.Items.Refresh();
                }
                catch (Exception) { }
            }
            else
            {
                MessageBox.Show("Veuillez donner un nom à votre entreprise s'il vous plait avant d'ajouter un contact", "pré-requis", MessageBoxButton.OK);
            }
        }

        private void _ButtonContactModifier_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridContact.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner un contact à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridContact.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'un contact à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridContact.SelectedItem != null)
            {
                ContactWindow contactWindow = new ContactWindow();
                contactWindow.DataContext = (Personne)this._dataGridContact.SelectedItem;
                if (!contactWindow.EntrepriseList.Contains((Entreprise)this.DataContext))
                {
                    contactWindow.EntrepriseList.Add((Entreprise)this.DataContext);
                    contactWindow.EntrepriseList = new ObservableCollection<Entreprise>(contactWindow.EntrepriseList.OrderBy(ent => ent.Libelle));
                }
                contactWindow._ComboBoxContactEntreprise.IsEnabled = false;

                bool? dialogResult = contactWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    this._dataGridContact.Items.Refresh();
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Personne)contactWindow.DataContext);
                        this._dataGridContact.Items.Refresh();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void _ButtonContactSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridContact.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous devez sélectionner un seul contact à supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (this._dataGridContact.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vous devez sélectionner un contact à supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (this._dataGridContact.SelectedItem != null)
            {
                //((Personne)this._dataGridContact.SelectedItem).Entreprise1 = null;
                ((Entreprise)this.DataContext).Personne.Remove((Personne)this._dataGridContact.SelectedItem);
                this.ContactsList.Remove((Personne)this._dataGridContact.SelectedItem);
            }
        }

        #endregion

        #region boutons Activités

        private void _ButtonVersDroite_click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxActivite.SelectedItem != null && this._ComboBoxActivite.SelectedItems.Count == 1)
            {
                Entreprise_Activite temp = new Entreprise_Activite();
                temp.Activite1 = (Activite)this._ComboBoxActivite.SelectedItem;
                ((Entreprise)DataContext).Entreprise_Activite.Add(temp);
                this.Secteur_Activite.Remove((Activite)this._ComboBoxActivite.SelectedItem);
            }
        }

        private void _ButtonVersGauche_click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxActiviteEntreprise.SelectedItem != null && this._ComboBoxActiviteEntreprise.SelectedItems.Count == 1)
            {
                this.Secteur_Activite.Add((Activite)((Entreprise_Activite)this._ComboBoxActiviteEntreprise.SelectedItem).Activite1);
                ((Entreprise)DataContext).Entreprise_Activite.Remove((Entreprise_Activite)this._ComboBoxActiviteEntreprise.SelectedItem);
            }
        }

        #endregion

        #region Boutons Conditions réglements

        #region Fournisseur

        private void _buttonGaucheDroite_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._listBoxCondReglementGauche.SelectedItem != null && this._listBoxCondReglementGauche.SelectedItems.Count == 1)
            {
                if (((Entreprise)this.DataContext).Fournisseur != null)
                {
                    Fournisseur_Condition_Reglement temp = new Fournisseur_Condition_Reglement();
                    temp.Condition_Reglement1 = (Condition_Reglement)this._listBoxCondReglementGauche.SelectedItem;

                    ((Entreprise)this.DataContext).Fournisseur.Fournisseur_Condition_Reglement.Add(temp);
                    this.list_Condition_Reglement.Remove((Condition_Reglement)this._listBoxCondReglementGauche.SelectedItem);
                }
            }
            this._dataGridEntrepriseConditionReglement.Items.Refresh();
        }

        private void _buttonDroiteGauche_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridEntrepriseConditionReglement.SelectedItem != null && this._dataGridEntrepriseConditionReglement.SelectedItems.Count == 1)
            {
                if (((Entreprise)this.DataContext).Fournisseur != null)
                {
                    this.list_Condition_Reglement.Add((Condition_Reglement)((Fournisseur_Condition_Reglement)this._dataGridEntrepriseConditionReglement.SelectedItem).Condition_Reglement1);
                    ((Fournisseur_Condition_Reglement)this._dataGridEntrepriseConditionReglement.SelectedItem).Condition_Reglement1 = null;
                    ((Entreprise)this.DataContext).Fournisseur.Fournisseur_Condition_Reglement.Remove((Fournisseur_Condition_Reglement)this._dataGridEntrepriseConditionReglement.SelectedItem);
                }
            }
        }

        #endregion

        #region Client

        private void _buttonGaucheDroite_Click_2(object sender, RoutedEventArgs e)
        {
            if (this._listBoxCondReglementGaucheClient.SelectedItem != null && this._listBoxCondReglementGaucheClient.SelectedItems.Count == 1)
            {
                if (((Entreprise)this.DataContext).Client != null)
                {
                    Client_Condition_Reglement temp = new Client_Condition_Reglement();
                    temp.Condition_Reglement1 = (Condition_Reglement)this._listBoxCondReglementGaucheClient.SelectedItem;

                    ((Entreprise)this.DataContext).Client.Client_Condition_Reglement.Add(temp);
                    this.list_Condition_Reglement_Client.Remove((Condition_Reglement)this._listBoxCondReglementGauche.SelectedItem);
                }
            }
            this._dataGridEntrepriseConditionReglementClient.Items.Refresh();
        }

        private void _buttonDroiteGauche_Click_2(object sender, RoutedEventArgs e)
        {
            if (this._dataGridEntrepriseConditionReglementClient.SelectedItem != null && this._dataGridEntrepriseConditionReglementClient.SelectedItems.Count == 1)
            {
                if (((Entreprise)this.DataContext).Client != null)
                {
                    this.list_Condition_Reglement_Client.Add((Condition_Reglement)((Client_Condition_Reglement)this._dataGridEntrepriseConditionReglementClient.SelectedItem).Condition_Reglement1);
                    ((Client_Condition_Reglement)this._dataGridEntrepriseConditionReglementClient.SelectedItem).Condition_Reglement1 = null;
                    ((Entreprise)this.DataContext).Client.Client_Condition_Reglement.Remove((Client_Condition_Reglement)this._dataGridEntrepriseConditionReglementClient.SelectedItem);
                }
            }
        }

        #endregion

        #endregion

        #region Boutons Cle Comptable Client

        private void _buttonModifierCleComptableClient_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._dataGridClientCleComptable.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner une clé comptable à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridClientCleComptable.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'une clé comptable à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridClientCleComptable.SelectedItem != null)
            {
                ObservableCollection<Entreprise_Mere> listToRemove = new ObservableCollection<Entreprise_Mere>();
                foreach (Client_Cle_Comptable item in ((Entreprise)this.DataContext).Client.Client_Cle_Comptable)
                {
                    if (item.Entreprise_Mere1 != ((Client_Cle_Comptable)this._dataGridClientCleComptable.SelectedItem).Entreprise_Mere1)
                    {
                        listToRemove.Add(item.Entreprise_Mere1);
                    }
                }

                EntrepriseCleComptableWindow entrepriseCleComptableWindow = new EntrepriseCleComptableWindow(listToRemove);
                entrepriseCleComptableWindow.DataContext = (Client_Cle_Comptable)this._dataGridClientCleComptable.SelectedItem;

                bool? dialogResult = entrepriseCleComptableWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    this._dataGridClientCleComptable.Items.Refresh();
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, ((Client_Cle_Comptable)entrepriseCleComptableWindow.DataContext));
                    }
                    catch (Exception)
                    {
                    }
                }
                this._dataGridClientCleComptable.Items.Refresh();
            }
        }

        private void _buttonAjouterCleComptableClient_Click_1(object sender, RoutedEventArgs e)
        {
            //Je récupère les éléments à enlever
            ObservableCollection<Entreprise_Mere> listToRemove = new ObservableCollection<Entreprise_Mere>();
            foreach (Client_Cle_Comptable item in ((Entreprise)this.DataContext).Client.Client_Cle_Comptable)
            {
                listToRemove.Add(item.Entreprise_Mere1);
            }

            EntrepriseCleComptableWindow entrepriseCleComptableWindow = new EntrepriseCleComptableWindow(listToRemove);
            entrepriseCleComptableWindow.DataContext = new Client_Cle_Comptable();
            ((Client_Cle_Comptable)entrepriseCleComptableWindow.DataContext).Client1 = ((Entreprise)this.DataContext).Client;

            bool? dialogResult = entrepriseCleComptableWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {

            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Client_Cle_Comptable)entrepriseCleComptableWindow.DataContext);
                }
                catch (Exception)
                {

                }
            }
            this._dataGridClientCleComptable.Items.Refresh();
        }

        private void _buttonSupprimerCleComptableClient_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._dataGridClientCleComptable.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une clé comptable à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridClientCleComptable.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les clés comptable à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ((Entreprise)this.DataContext).Client.Client_Cle_Comptable.Remove((Client_Cle_Comptable)this._dataGridClientCleComptable.SelectedItem);
                    this._dataGridClientCleComptable.Items.Refresh();
                }
            }
        }

        #endregion

        #region Boutons clé comptable Fournisseur

        private void _buttonAjouterCleComptableFournisseur_Click_1(object sender, RoutedEventArgs e)
        {
            //Je récupère les éléments à enlever
            ObservableCollection<Entreprise_Mere> listToRemove = new ObservableCollection<Entreprise_Mere>();
            foreach (Fournisseur_Cle_Comptable item in ((Entreprise)this.DataContext).Fournisseur.Fournisseur_Cle_Comptable)
            {
                listToRemove.Add(item.Entreprise_Mere1);
            }

            EntrepriseCleComptableWindow entrepriseCleComptableWindow = new EntrepriseCleComptableWindow(listToRemove);
            entrepriseCleComptableWindow.DataContext = new Fournisseur_Cle_Comptable();
            ((Fournisseur_Cle_Comptable)entrepriseCleComptableWindow.DataContext).Fournisseur1 = ((Entreprise)this.DataContext).Fournisseur;

            bool? dialogResult = entrepriseCleComptableWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {

            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Fournisseur_Cle_Comptable)entrepriseCleComptableWindow.DataContext);
                }
                catch (Exception)
                {

                }
            }
            this._dataGridFournisseurtCleComptable.Items.Refresh();
        }

        private void _buttonModifierCleComptableFournisseur_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._dataGridFournisseurtCleComptable.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner une clé comptable à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridFournisseurtCleComptable.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'une clé comptable à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridFournisseurtCleComptable.SelectedItem != null)
            {
                ObservableCollection<Entreprise_Mere> listToRemove = new ObservableCollection<Entreprise_Mere>();
                foreach (Fournisseur_Cle_Comptable item in ((Entreprise)this.DataContext).Fournisseur.Fournisseur_Cle_Comptable)
                {
                    if (item.Entreprise_Mere1 != ((Fournisseur_Cle_Comptable)this._dataGridFournisseurtCleComptable.SelectedItem).Entreprise_Mere1)
                    {
                        listToRemove.Add(item.Entreprise_Mere1);
                    }
                }

                EntrepriseCleComptableWindow entrepriseCleComptableWindow = new EntrepriseCleComptableWindow(listToRemove);
                entrepriseCleComptableWindow.DataContext = (Fournisseur_Cle_Comptable)this._dataGridFournisseurtCleComptable.SelectedItem;

                bool? dialogResult = entrepriseCleComptableWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    this._dataGridFournisseurtCleComptable.Items.Refresh();
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, ((Fournisseur_Cle_Comptable)entrepriseCleComptableWindow.DataContext));
                    }
                    catch (Exception)
                    {
                    }
                }
                this._dataGridFournisseurtCleComptable.Items.Refresh();
            }
        }

        private void _buttonSupprimerCleComptableFournisseur_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._dataGridFournisseurtCleComptable.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une clé comptable à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridFournisseurtCleComptable.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les clés comptable à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ((Entreprise)this.DataContext).Fournisseur.Fournisseur_Cle_Comptable.Remove((Fournisseur_Cle_Comptable)this._dataGridFournisseurtCleComptable.SelectedItem);
                    this._dataGridFournisseurtCleComptable.Items.Refresh();
                }
            }
        }

        #endregion

        #region Boutons RIBs

        private void _buttonAjouterRIB_Click_1(object sender, RoutedEventArgs e)
        {
            EntrepriseRIBWindow entrepriseRIBWindow = new EntrepriseRIBWindow();
            entrepriseRIBWindow.DataContext = new Entreprise_RIB();

            bool? dialogResult = entrepriseRIBWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                ((Entreprise_RIB)entrepriseRIBWindow.DataContext).Entreprise1 = (Entreprise)this.DataContext;
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Entreprise_RIB)entrepriseRIBWindow.DataContext);
                }
                catch (Exception)
                {

                }
            }
            this._dataGridRIB.Items.Refresh();
        }

        private void _buttonModifierRIB_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._dataGridRIB.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner un RIB à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridRIB.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'un RIB à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridRIB.SelectedItem != null)
            {
                EntrepriseRIBWindow entrepriseRIBWindow = new EntrepriseRIBWindow();
                entrepriseRIBWindow.DataContext = (Entreprise_RIB)this._dataGridRIB.SelectedItem;

                bool? dialogResult = entrepriseRIBWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    this._dataGridRIB.Items.Refresh();
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, ((Entreprise_RIB)entrepriseRIBWindow.DataContext));
                    }
                    catch (Exception)
                    {
                    }
                }
                this._dataGridRIB.Items.Refresh();
            }
        }

        private void _buttonSupprimerRIB_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._dataGridRIB.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un RIB à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridRIB.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les RIBs à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ((Entreprise)this.DataContext).Entreprise_RIB.Remove((Entreprise_RIB)this._dataGridRIB.SelectedItem);
                    this._dataGridRIB.Items.Refresh();
                }
            }
        }

        #endregion

        #region Groupe
        private void NewGroupe_Click_1(object sender, RoutedEventArgs e)
        {
            ParametreGroupeControl parametreGroupeControl = new ParametreGroupeControl();
            Groupe groupe = parametreGroupeControl.Add();
            if (groupe != null)
            {
                this.GroupList.Add(groupe);
                this.GroupList = new ObservableCollection<Groupe>(this.GroupList.OrderBy(civ => civ.Libelle));
                this._ComboBoxCoordonneesGroupe.SelectedItem = groupe;
            }
            else
            {
                this._ComboBoxCoordonneesGroupe.SelectedItem = null;
            }
        }

        private void LookGroupe_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxCoordonneesGroupe.SelectedItem != null)
            {
                ParametreGroupeControl parametreGroupeControl = new ParametreGroupeControl();
                parametreGroupeControl.Look((Groupe)this._ComboBoxCoordonneesGroupe.SelectedItem);
            }
        }

        private void NullGroupe_Click_1(object sender, RoutedEventArgs e)
        {
            this._ComboBoxCoordonneesGroupe.SelectedItem = null;
        }
        #endregion

        #region Activité

        private void _newActivite_Click_1(object sender, RoutedEventArgs e)
        {
            ParametreActiviteControl parametreActiviteControl = new ParametreActiviteControl();
            Activite activite = parametreActiviteControl.Add();
            if (activite != null)
            {
                this.Secteur_Activite.Add(activite);
                this.Secteur_Activite = new ObservableCollection<Activite>(this.Secteur_Activite.OrderBy(civ => civ.Libelle));
            }
            else
            {

            }
        }

        #endregion

        #endregion

        #region Verifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!verificationGeneral())
            {
                verif = false;
            }
            if (!VerifClient())
            {
                verif = false;
            }
            if (!VerifFournisseur())
            {
                verif = false;
            }


            return verif;
        }

        #region Tab General

        public bool verificationGeneral()
        {
            bool test = true;

            if (!VerificationEnTete())
            {
                test = false;
            }
            if (!VerificationGroupBoxAdresse())
            {
                test = false;
            }
            if (!VerificationGroupBoxContacts())
            {
                test = false;
            }
            if (!VerificationsTabsGeneral())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemGeneral, test);
            return test;

        }

        #region En-tête

        private bool VerificationEnTete()
        {
            bool verif = true;

            if (!Verif_ComboBoxCoordonneesGroupe())
            {
                verif = false;
            }
            if (!Verif_ComboBoxCoordonneesTypeEntreprise())
            {
                verif = false;
            }
            if (!Verif_ComboBoxCoordonneesStatut())
            {
                verif = false;
            }
            if (!Verif_TextBoxCoordonnéesNom())
            {
                verif = false;
            }
            if (!Verif_TextBoxCoordonnéesSIRET())
            {
                verif = false;
            }
            if (!Verif_TextBoxCoordonnéesSiteWeb())
            {
                verif = false;
            }
            if (!Verif_TextBoxCoordonnéesAPE())
            {
                verif = false;
            }
            if (!Verif_TextBoxCommentaires())
            {
                verif = false;
            }
			if (!Verif_CheckBox())
            {
                verif = false;
            }
			
            return verif;
        }

        #region Champs Groupe
        private bool Verif_ComboBoxCoordonneesGroupe()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._ComboBoxCoordonneesGroupe, this._TextBlockCoordonneesGroupe);
        }
        private void _ComboBoxCoordonneesGroupe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Verif_ComboBoxCoordonneesGroupe();
        }
        #endregion

        #region Champs Type Entreprise
        private bool Verif_ComboBoxCoordonneesTypeEntreprise()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._ComboBoxCoordonneesTypeEntreprise, this._TextBlockCoordonneesTypeEntreprise);
        }
        private void _ComboBoxCoordonneesTypeEntreprise_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Champs Status
        private bool Verif_ComboBoxCoordonneesStatut()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._ComboBoxCoordonneesStatut, this._TextBlockCoordonneesStatut);
        }
        private void _ComboBoxCoordonneesStatut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Verif_ComboBoxCoordonneesStatut();
        }
        #endregion

        #region Champs Nom
        private bool Verif_TextBoxCoordonnéesNom()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxCoordonnéesNom, this._TextBlockCoordonnéesNom);
        }

        private void _TextBoxCoordonnéesNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCoordonnéesNom();
        }

        private void _TextBoxCoordonnéesNom_LostFocus_1(object sender, RoutedEventArgs e)
        {
            _TextBoxCoordonnéesNom.Text = _TextBoxCoordonnéesNom.Text.Trim().ToUpper(System.Globalization.CultureInfo.CurrentCulture);
        }
        #endregion

        #region Champs Siret
        private bool Verif_TextBoxCoordonnéesSIRET()
        {
            bool verif = true;
            double test = 0;
            if (this._TextBoxCoordonnéesSIRET.Text.Trim().Length != 0)
            {
                if (this._TextBoxCoordonnéesSIRET.Text.Trim().Length < 14 || this._TextBoxCoordonnéesSIRET.Text.Trim().Length > 14)
                {
                    verif = false;
                }
                else
                {
                    if (double.TryParse(this._TextBoxCoordonnéesSIRET.Text.Trim(), out test))
                    {
                        verif = true;
                    }
                    else
                    {
                        verif = false;
                    }
                }
            }
            else
            {
                verif = true;
            }
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxCoordonnéesSIRET, this._TextBlockCoordonnéesSIRET, verif);
            return verif;
        }

        private void _TextBoxCoordonnéesSIRET_TextChanged(object sender, TextChangedEventArgs e)
        {
            Verif_TextBoxCoordonnéesSIRET();
        }
        #endregion

        #region Champs Site Web
        private bool Verif_TextBoxCoordonnéesSiteWeb()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxCoordonnéesSiteWeb, this._TextBlockCoordonnéesSiteWeb, 255);
        }

        private void _TextBoxCoordonnéesSiteWeb_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCoordonnéesSiteWeb();
        }
        #endregion

        #region Champs Ape
        private bool Verif_TextBoxCoordonnéesAPE()
        {
            bool verif = true;

            if (this._TextBoxCoordonnéesAPE.Text.Trim().Length >= 6)
            {
                verif = false;
            }
            else
            {
                verif = true;
            }
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxCoordonnéesAPE, this._TextBlockCoordonnéesAPE, verif);
            return verif;
        }

        private void _TextBoxCoordonnéesAPE_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCoordonnéesAPE();
        }
        #endregion

        #region Champs Commentaires
        private bool Verif_TextBoxCommentaires()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxCommentaires, this._TextBlockCommentaires);
        }

        private void _TextBoxCommentaires_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCommentaires();
        }
        #endregion

		#region CheckBox

		private bool Verif_CheckBox()
		{
			bool verif = true;

			if (Bouton_Client.IsChecked == false && _Bouton_Fournisseur.IsChecked == false)
			{
				verif = false;

			}
			((App)App.Current).verifications.MettreCheckBoxEnCouleur(Bouton_Client, verif);
			((App)App.Current).verifications.MettreCheckBoxEnCouleur(_Bouton_Fournisseur, verif);

			return verif;
		}

		#endregion

		#endregion

		#region GroupBox Adresse

		private bool VerificationGroupBoxAdresse()
        {
            bool verif = true;

            if (!Verif_TextBoxCoordonnéesAdresse())
            {
                verif = false;
            }
            if (!Verif_TextBoxCoordonnéesBP())
            {
                verif = false;
            }
            if (!Verif_TextBoxCoordonnéesCP())
            {
                verif = false;
            }
            if (!Verif_ComboBoxCoordonneesVille())
            {
                verif = false;
            }
            if (!Verif_ComboBoxCoordonneesPays())
            {
                verif = false;
            }

            if (verif)
            {
                this._groupBoxAdresse.Foreground = Brushes.Green;
            }
            else
            {
                this._groupBoxAdresse.Foreground = Brushes.Red;
            }

            return verif;
        }

        #region Champs Adresse
        private bool Verif_TextBoxCoordonnéesAdresse()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxCoordonnéesAdresse, this._TextBlockCoordonnéesAdresse);
        }

        private void _TextBoxCoordonnéesAdresse_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCoordonnéesAdresse();
        }
        #endregion

        #region Champs Adresse Complémentaire
        private bool Verif_TextBoxCoordonnéesBP()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxCoordonnéesBP, this._TextBlockCoordonnéesBP, 255);
        }

        private void _TextBoxCoordonnéesBP_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCoordonnéesBP();
        }
        #endregion

        #region Champs Code Postal
        private bool Verif_TextBoxCoordonnéesCP()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxCoordonnéesCP, this._TextBlockCoordonnéesCP);
        }

        private void _TextBoxCoordonnéesCP_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.VilleList = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));

			if (this.Verif_TextBoxCoordonnéesCP())
			{
				this.VilleList = new ObservableCollection<Ville>(this.VilleList.Where(vil => vil.Code_Postal == this._TextBoxCoordonnéesCP.Text.Trim()).OrderBy(vil => vil.Libelle));
				if (this.Verif_ComboBoxCoordonneesPays())
				{
					this.VilleList = new ObservableCollection<Ville>(this.VilleList.Where(vil => vil.Pays1 == (Pays)this._ComboBoxCoordonneesPays.SelectedItem).OrderBy(vil => vil.Libelle));
				}
			}
        }
        #endregion

        #region Champs Ville
        private bool Verif_ComboBoxCoordonneesVille()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxCoordonneesVille, this._TextBlockCoordonneesVille);
        }
        private void _ComboBoxCoordonneesVille_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			if (Verif_ComboBoxCoordonneesVille())
			{
				this._TextBoxCoordonnéesCP.Text = ((Ville)this._ComboBoxCoordonneesVille.SelectedItem).Code_Postal;
				this._ComboBoxCoordonneesPays.SelectedItem = ((Ville)this._ComboBoxCoordonneesVille.SelectedItem).Pays1;
			}
        }
        #endregion

        #region Champs Pays
        private bool Verif_ComboBoxCoordonneesPays()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxCoordonneesPays, this._TextBlockCoordonneesPays);
        }

        private void _ComboBoxCoordonneesPays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			if (Verif_ComboBoxCoordonneesPays())
			{
				this._TextBoxIndicatif.Text = ((Pays)this._ComboBoxCoordonneesPays.SelectedItem).Code_Telephonique;

				this.VilleList = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));
                if (this._TextBoxCoordonnéesCP.Text.Length > 0)
				{
					this.VilleList = new ObservableCollection<Ville>(this.VilleList.Where(vil => vil.Code_Postal == this._TextBoxCoordonnéesCP.Text.Trim()).OrderBy(vil => vil.Libelle));
				}
				
				this.VilleList = new ObservableCollection<Ville>(this.VilleList.Where(vil => vil.Pays1 == (Pays)this._ComboBoxCoordonneesPays.SelectedItem).OrderBy(vil => vil.Libelle));
				
			}
        }
        #endregion

        #endregion

        #region GroupBox Contacts

        private bool VerificationGroupBoxContacts()
        {
            bool verif = true;

            if (!Verif_TextBoxCoordonnéesTEL())
            {
                verif = false;
            }
            if (!Verif_TextBoxCoordonnéesFAX())
            {
                verif = false;
            }
            if (!Verif_TextBoxCoordonnéesMail())
            {
                verif = false;
            }

            if (verif)
            {
                this._groupBoxContacts.Foreground = Brushes.Green;
            }
            else
            {
                this._groupBoxContacts.Foreground = Brushes.Red;
            }

            return verif;
        }

        #region Champs Telephone
        private bool Verif_TextBoxCoordonnéesTEL()
        {
			return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._TextBoxCoordonnéesTEL, this._TextBlockCoordonnéesTEL);
        }

        private void _TextBoxCoordonnéesTEL_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCoordonnéesTEL();
        }
        #endregion

        #region Champs Fax
        private bool Verif_TextBoxCoordonnéesFAX()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxCoordonnéesFAX, this._TextBlockCoordonnéesFAX);
        }

        private void _TextBoxCoordonnéesFAX_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCoordonnéesFAX();
        }
        #endregion

        #region Champs Mail
        private bool Verif_TextBoxCoordonnéesMail()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoireMail(this._TextBoxCoordonnéesMail, this._TextBlockCoordonnéesMail);
        }

        private void _TextBoxCoordonnéesMail_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCoordonnéesMail();
        }
        #endregion

        #endregion

        #region Tabs

        private bool VerificationsTabsGeneral()
        {
            bool verif = true;

            if (!VerificationTabGeneralActivites())
            {
                verif = false;
            }
            if (!VerificationTabGeneralContacts())
            {
                verif = false;
            }
            if (!VerificationTabGeneralBancaires())
            {
                verif = false;
            }
            if (!VerificationTabTvaIntraco())
            {
                verif = false;
            }
            if (!VerificationTabGeneralFacturation())
            {
                verif = false;
            }

            return verif;
        }

        #region Tab Activités

        private bool VerificationTabGeneralActivites()
        {
            bool test = true;

            if (!Verif_ComboBoxActiviteEntreprise())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemActivites, test);
            return test;
        }

        #region Activités nombre

        private bool Verif_ComboBoxActiviteEntreprise()
        {
            bool verif = true;

            if (this._ComboBoxActiviteEntreprise.Items.Count == 0)
            {
                verif = false;
            }
            else
            {
                verif = true;
            }
			((App)App.Current).verifications.MettreDataGridEnCouleur(this._ComboBoxActiviteEntreprise, verif);
            return verif;
        }

        #endregion

        #endregion

        #region Tab Contacts

        private bool VerificationTabGeneralContacts()
        {
            bool test = true;

            if (!Verif_dataGridContact())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemContacts, test);

            return test;
        }

        #region Contacts nombre

        private bool Verif_dataGridContact()
		{
            bool verif = true;

            if (this._dataGridContact.Items.Count == 0)
            {
                verif = true;
            }
            else
            {
                verif = true;
			}
			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridContact, true);
            return verif;
        }

        #endregion

        #endregion

        #region Tab Coordonnées bancaires

        private bool VerificationTabGeneralBancaires()
        {
            bool test = true;

            if (!Verif_dataGridRIB())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemCoordonneesBanquaire, test);

            return test;
        }

        #region RIBs nombre

        private bool Verif_dataGridRIB()
		{
            bool verif = true;

            if (this._dataGridRIB.Items.Count == 0)
            {
                verif = true;
            }
            else
            {
                verif = true;
			}
			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridRIB, verif);

            return verif;
        }

        #endregion

        #endregion

        #region Tab Tva Intraco

        private bool VerificationTabTvaIntraco()
        {
            bool test = true;

            if (!Verif_dataGridNumIntraco())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemNTva, test);

            return test;
        }

        #region Contacts n° tva intraco

        private bool Verif_dataGridNumIntraco()
		{
            bool verif = true;

            if (this._dataGridNumIntraco.Items.Count == 0)
            {
                verif = true;
            }
            else
            {
                verif = true;
			}
			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridNumIntraco, verif);

            return verif;
        }

        #endregion

        #endregion

        #region Tab Facturation

        private bool VerificationTabGeneralFacturation()
        {
            bool test = true;

            if (!Verif_ComboBoxModeFacturation())
            {
                test = false;
            }
            if (!Verif_ComboBoxDevise())
            {
                test = false;
            }
            if (!Verif_TextBoxCoordonnéesAdresseFact())
            {
                test = false;
            }
            if (!Verif_TextBoxCoordonnéesBPFact())
            {
                test = false;
            }
            if (!Verif_TextBoxCoordonnéesCPFact())
            {
                test = false;
            }
            if (!Verif_ComboBoxCoordonneesVilleFact())
            {
                test = false;
            }
            if (!Verif_ComboBoxCoordonneesPaysFact())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemGeneralFacturation, test);

            return test;
        }

        #region Champs Mode Facturation
        private bool Verif_ComboBoxModeFacturation()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._ComboBoxModeFacturation, this._TextBlockModeFacturation);
        }
        private void ComboBoxModeFacturation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Verif_ComboBoxModeFacturation();
        }
        #endregion

        #region Champs Devise
        private bool Verif_ComboBoxDevise()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._ComboBoxDevise, this._TextBlockDevise);
        }
        private void ComboBoxDevise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Verif_ComboBoxDevise();
        }
        #endregion

        #region Champs Adresse
        private bool Verif_TextBoxCoordonnéesAdresseFact()
        {
            if (_TextBoxCoordonnéesAdresseFact.Text != "" || this._TextBoxCoordonnéesBPFact.Text != "" || this._TextBoxCoordonnéesCPFact.Text != "" || this._ComboBoxCoordonneesVilleFact.SelectedItem != null || this._ComboBoxCoordonneesPaysFact.SelectedItem != null)
            {
                return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxCoordonnéesAdresseFact, this._TextBlockCoordonnéesAdresseFact);
            }
            else
            {
                return true;
            }
        }

        private void _TextBoxCoordonnéesAdresseFact_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCoordonnéesAdresseFact();
        }
        #endregion

        #region Champs Adresse Complémentaire
        private bool Verif_TextBoxCoordonnéesBPFact()
        {
            if (_TextBoxCoordonnéesAdresseFact.Text != "" || this._TextBoxCoordonnéesBPFact.Text != "" || this._TextBoxCoordonnéesCPFact.Text != "" || this._ComboBoxCoordonneesVilleFact.SelectedItem != null || this._ComboBoxCoordonneesPaysFact.SelectedItem != null)
            {
                return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxCoordonnéesBPFact, this._TextBlockCoordonnéesBPFact, 255);
            }
            else
            {
                return true;
            }
        }

        private void _TextBoxCoordonnéesBPFact_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCoordonnéesBPFact();
        }
        #endregion

        #region Champs Code Postal
        private bool Verif_TextBoxCoordonnéesCPFact()
        {
            if (_TextBoxCoordonnéesAdresseFact.Text != "" || this._TextBoxCoordonnéesBPFact.Text != "" || this._TextBoxCoordonnéesCPFact.Text != "" || this._ComboBoxCoordonneesVilleFact.SelectedItem != null || this._ComboBoxCoordonneesPaysFact.SelectedItem != null)
            {
                return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxCoordonnéesCPFact, this._TextBlockCoordonnéesCPFact);
            }
            else
            {
                return true;
            }
        }

        private void _TextBoxCoordonnéesCPFact_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.VilleList2 = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));

            if (this.Verif_TextBoxCoordonnéesCPFact())
            {
                this.VilleList2 = new ObservableCollection<Ville>(this.VilleList.Where(vil => vil.Code_Postal == this._TextBoxCoordonnéesCPFact.Text.Trim()).OrderBy(vil => vil.Libelle));
                if (this.Verif_ComboBoxCoordonneesPaysFact())
                {
                    this.VilleList2 = new ObservableCollection<Ville>(this.VilleList.Where(vil => vil.Pays1 == (Pays)this._ComboBoxCoordonneesPaysFact.SelectedItem).OrderBy(vil => vil.Libelle));
                }
            }
        }
        #endregion

        #region Champs Ville
        private bool Verif_ComboBoxCoordonneesVilleFact()
        {
            if (_TextBoxCoordonnéesAdresseFact.Text != "" || this._TextBoxCoordonnéesBPFact.Text != "" || this._TextBoxCoordonnéesCPFact.Text != "" || this._ComboBoxCoordonneesVilleFact.SelectedItem != null || this._ComboBoxCoordonneesPaysFact.SelectedItem != null)
            {
                return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxCoordonneesVilleFact, this._TextBlockCoordonneesVilleFact);
            }
            else
            {
                return true;
            }
        }

        private void _ComboBoxCoordonneesVilleFact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Verif_ComboBoxCoordonneesVilleFact())
            {
                this._TextBoxCoordonnéesCPFact.Text = ((Ville)this._ComboBoxCoordonneesVilleFact.SelectedItem).Code_Postal;
                this._ComboBoxCoordonneesPaysFact.SelectedItem = ((Ville)this._ComboBoxCoordonneesVilleFact.SelectedItem).Pays1;
            }
        }
        #endregion

        #region Champs Pays
        private bool Verif_ComboBoxCoordonneesPaysFact()
        {
            if (_TextBoxCoordonnéesAdresseFact.Text != "" || this._TextBoxCoordonnéesBPFact.Text != "" || this._TextBoxCoordonnéesCPFact.Text != "" || this._ComboBoxCoordonneesVilleFact.SelectedItem != null || this._ComboBoxCoordonneesPaysFact.SelectedItem != null)
            {
                return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxCoordonneesPaysFact, this._TextBlockCoordonneesPaysFact);
            }
            else
            {
                return true;
            }
        }

        private void _ComboBoxCoordonneesPaysFact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Verif_ComboBoxCoordonneesPaysFact())
            {
                this.VilleList2 = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));
                if (this._TextBoxCoordonnéesCPFact.Text.Length > 0)
                {
                    this.VilleList = new ObservableCollection<Ville>(this.VilleList.Where(vil => vil.Code_Postal == this._TextBoxCoordonnéesCPFact.Text.Trim()).OrderBy(vil => vil.Libelle));
                }

                this.VilleList2 = new ObservableCollection<Ville>(this.VilleList.Where(vil => vil.Pays1 == (Pays)this._ComboBoxCoordonneesPaysFact.SelectedItem).OrderBy(vil => vil.Libelle));

            }
        }
        #endregion

        #endregion

        #endregion

        #endregion

        #region Tab Client

        private bool VerifClient()
        {
            bool test = true;

            if (((Entreprise)this.DataContext).Client != null && ((Entreprise)this.DataContext).Is_Client == true)
            {
                if (!VerificationEnTeteClient())
                {
                    test = false;
                }
                if (!VerificationsTabsClient())
                {
                    test = false;
                }
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemClient, test);

            return test;
        }

        #region En-tête

        private bool VerificationEnTeteClient()
        {
            bool verif = true;

            if (!Verif_ComboBoxTauxTVAClient())
            {
                verif = false;
            }
            if (!Verif_TextBoxCodeClient())
            {
                verif = false;
            }

            return verif;
        }

        #region Champs Taux TVA Client
        private bool Verif_ComboBoxTauxTVAClient()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._ComboBoxTauxTVAClient, this._TextBlockTauxTVAClient);
        }
        private void _ComboBoxTauxTVAClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Verif_ComboBoxTauxTVAClient();
        }
        #endregion

        #region Champs Code Client
        private bool Verif_TextBoxCodeClient()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxCodeClient, this._TextBlockCodeClient);
        }

        private void _TextBoxCodeClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCodeClient();
        }
        #endregion

        #endregion

        #region Tabs

        private bool VerificationsTabsClient()
        {
            bool verif = true;

            if (!VerificationTabClientConditionsReglement())
            {
                verif = false;
            }
            if (!VerificationTabClientFacturation())
            {
                verif = false;
            }
            if (!VerificationTabClientCleComptable())
            {
                verif = false;
            }

            return verif;
        }

        #region Tab Conditions de réglements

        private bool VerificationTabClientConditionsReglement()
        {
            bool test = true;

            if (!Verif_dataGridEntrepriseConditionReglementClient())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this.ClientConditionsReglement, test);

            return test;
        }

        #region conditions reg nombre

        private bool Verif_dataGridEntrepriseConditionReglementClient()
        {
            bool verif = true;

            if (this._dataGridEntrepriseConditionReglementClient.Items.Count == 0)
            {
                verif = true;
            }
            else
            {
                verif = true;
			}
			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridEntrepriseConditionReglementClient, verif);
            return verif;
        }

        #endregion

        #endregion

        #region Tab Facturation

        private bool VerificationTabClientFacturation()
        {
            bool test = true;

            if (!Verif_TextBoxEscompteClient())
            {
                test = false;
            }
            if (!Verif_TextBoxRemiseClient())
            {
                test = false;
            }
            if (!Verif_TextBoxTarifMini())
            {
                test = false;
            }
            if (!Verif_TextBoxNbFacture())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this.FacturationClient, test);

            return test;
        }

        #region Champs Escompte Client
        private bool Verif_TextBoxEscompteClient()
        {
			return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._TextBoxEscompteClient, this._TextBlockEscompteClient);
        }

        private void _TextBoxEscompteClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxEscompteClient();
        }
        #endregion

        #region Champs Remise Client
        private bool Verif_TextBoxRemiseClient()
        {
			return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._TextBoxRemiseClient, this._TextBlockRemiseClient);
        }

        private void _TextBoxRemiseClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxRemiseClient();
        }
        #endregion

        #region Champs Tarif Minimum Commande
        private bool Verif_TextBoxTarifMini()
        {
			return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._TextBoxTarifMini, this._TextBlockTarifMini);
        }

        private void _TextBoxTarifMini_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxTarifMini();
        }
        #endregion

        #region Champs Nombre exemplaire facture
        private bool Verif_TextBoxNbFacture()
        {
			return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._TextBoxNbFacture, this._TextBlockNbFacture);
        }

        private void _TextBoxNbFacture_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxNbFacture();
        }
        #endregion

        #endregion

        #region Tab Clés comptable

        private bool VerificationTabClientCleComptable()
        {
            bool test = true;

            if (!Verif_dataGridClientCleComptable())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this.ClesComptableClient, test);

            return test;
        }

        #region clés comptable client nombre

        private bool Verif_dataGridClientCleComptable()
		{
            bool verif = true;

            if (this._dataGridClientCleComptable.Items.Count == 0)
            {
                verif = true;
            }
            else
            {
                verif = true;
			}
			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridClientCleComptable, verif);
            return verif;
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region Tab Fournisseur

        private bool VerifFournisseur()
        {
            bool test = true;

            if (((Entreprise)this.DataContext).Fournisseur != null && ((Entreprise)this.DataContext).Is_Fournisseur == true)
            {
                if (!VerificationEnTeteFournisseur())
                {
                    test = false;
                }
                if (!VerificationsTabsFournisseur())
                {
                    test = false;
                }
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemFournisseur, test);

            return test;
        }

        #region En-tête

        private bool VerificationEnTeteFournisseur()
        {
            bool verif = true;

            if (!Verif_ComboBoxTauxTVAFournisseur())
            {
                verif = false;
            }
            if (!Verif_TextBoxCodeFournisseur())
            {
                verif = false;
            }
            if (this._CheckBoxInterimaire.IsChecked == true)
            {
                if (!Verif_TextBoxCoefDelegation())
                {
                    verif = false;
                }
                if (!Verif_TextBoxCoefGestion())
                {
                    verif = false;
                }
            }

            return verif;
        }

        #region Champs Taux TVA Fournisseur
        private bool Verif_ComboBoxTauxTVAFournisseur()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._ComboBoxTauxTVA, this._TextBlockTauxTVA);
        }
        private void _ComboBoxTauxTVAFournisseur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Verif_ComboBoxTauxTVAFournisseur();
        }
        #endregion

        #region Champs Code Fournisseur
        private bool Verif_TextBoxCodeFournisseur()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxCodeFournisseur, this._TextBlockCodeFournisseur);
        }
        private void _TextBoxCodeFournisseur_TextChanged(object sender, TextChangedEventArgs e)
        {
            Verif_TextBoxCodeFournisseur();
        }
        #endregion

        #region Champs Coeficient Délégation
        private bool Verif_TextBoxCoefDelegation()
        {
            this._TextBoxCoefDelegation.Text = this._TextBoxCoefDelegation.Text.Trim();

			return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._TextBoxCoefDelegation, this._TextBlockCoefDelegation);
        }

        private void _TextBoxCoefDelegation_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCoefDelegation();
        }
        #endregion

        #region Champs Coeficient Gestion
        private bool Verif_TextBoxCoefGestion()
        {
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._TextBoxCoefGestion, this._TextBlockCoefGestion);
        }

        private void _TextBoxCoefGestion_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCoefGestion();
        }
        #endregion

        #endregion

        #region Tabs

        private bool VerificationsTabsFournisseur()
        {
            bool verif = true;

            if (!VerificationTabFournisseurConditionsReglement())
            {
                verif = false;
            }
            if (!VerificationTabFournisseurDelaisLivraison())
            {
                verif = false;
            }
            if (!VerificationTabFournisseurFacturation())
            {
                verif = false;
            }
            if (!VerificationTabFournisseurCleComptable())
            {
                verif = false;
            }

            return verif;
        }

        #region Tab Conditions de réglements

        private bool VerificationTabFournisseurConditionsReglement()
        {
            bool test = true;

            if (!Verif_dataGridEntrepriseConditionReglementFournisseur())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this.FournisseurConditionsReglement, test);

            return test;
        }

        #region conditions reg nombre

        private bool Verif_dataGridEntrepriseConditionReglementFournisseur()
        {
            bool verif = true;

            if (this._dataGridEntrepriseConditionReglement.Items.Count == 0)
            {
                verif = true;
            }
            else
            {
                verif = true;
			}
			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridEntrepriseConditionReglement, verif);

            return verif;
        }

        #endregion

        #endregion

        #region Tab Clés comptable

        private bool VerificationTabFournisseurCleComptable()
        {
            bool test = true;

            if (!Verif_dataGridFournisseurtCleComptable())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this.ClesComptableFournisseur, test);

            return test;
        }

        #region clés comptable fournisseur nombre

        private bool Verif_dataGridFournisseurtCleComptable()
		{
            bool verif = true;

            if (this._dataGridFournisseurtCleComptable.Items.Count == 0)
            {
                verif = true;
            }
            else
            {
                verif = true;
			}
			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridFournisseurtCleComptable, verif);
            return verif;
        }

        #endregion

        #endregion

        #region Tab Délais de livraisons

        private bool VerificationTabFournisseurDelaisLivraison()
        {
            bool test = true;

            if (!Verif_TextBoxDelaiNormatif())
            {
                test = false;
            }
            if (!Verif_TextBoxIncertitudeFournisseur())
            {
                test = false;
            }
            if (!Verif_TextBoxDelaiRetourConsult())
            {
                test = false;
            }
            if (!Verif_TextBoxCommentairesLivraisonDelais())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this.DelaisLivraisonFournisseur, test);

            return test;
        }

        #region Champs Delais Normatif
        private bool Verif_TextBoxDelaiNormatif()
        {
            this._TextBoxDelaiNormatif.Text = this._TextBoxDelaiNormatif.Text.Trim();

			return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._TextBoxDelaiNormatif, this._TextBlockDelaiNormatif);
        }

        private void _TextBoxDelaiNormatif_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxDelaiNormatif();
        }
        #endregion

        #region Champs Incertitude Fournisseur
        private bool Verif_TextBoxIncertitudeFournisseur()
        {
            this._TextBoxIncertitudeFournisseur.Text = this._TextBoxIncertitudeFournisseur.Text.Trim();

			return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._TextBoxIncertitudeFournisseur, this._TextBlockIncertitudeFournisseur);
        }

        private void _TextBoxIncertitudeFournisseur_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxIncertitudeFournisseur();
        }
        #endregion

        #region Champs Delais Retour Consultation
        private bool Verif_TextBoxDelaiRetourConsult()
        {
            this._TextBoxDelaiRetourConsult.Text = this._TextBoxDelaiRetourConsult.Text.Trim();

			return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._TextBoxDelaiRetourConsult, this._TextBlockDelaiRetourConsult);
        }

        private void _TextBoxDelaiRetourConsult_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxDelaiRetourConsult();
        }
        #endregion

        #region Champs Commentaires
        private bool Verif_TextBoxCommentairesLivraisonDelais()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxCommentairesLivraisonDelais, this._TextBlockCommentairesLivraisonDelais);
        }

        private void _TextBoxCommentairesLivraisonDelais_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCommentairesLivraisonDelais();
        }
        #endregion

        #endregion

        #region Tab Facturation

        private bool VerificationTabFournisseurFacturation()
        {
            bool test = true;

            if (!Verif_TextBoxEscompte())
            {
                test = false;
            }
            if (!Verif_TextBoxRemiseCommerciale())
            {
                test = false;
            }
            if (!Verif_ComboBoxFraisDePort())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this.FacturationFournisseur, test);

            return test;
        }

        #region Champs Escompte Fournisseur
        private bool Verif_TextBoxEscompte()
        {
            this._TextBoxEscompte.Text = this._TextBoxEscompte.Text.Trim();

			return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._TextBoxEscompte, this._TextBlockEscompte);
        }

        private void _TextBoxEscompte_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxEscompte();
        }
        #endregion

        #region Champs Remise Commercial Fournisseur
        private bool Verif_TextBoxRemiseCommerciale()
        {
            this._TextBoxRemiseCommerciale.Text = this._TextBoxRemiseCommerciale.Text.Trim();

			return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._TextBoxRemiseCommerciale, this._TextBlockRemiseCommerciale);
        }

        private void _TextBoxRemiseCommerciale_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxRemiseCommerciale();
        }
        #endregion

        #region Champs Frais de port
        private bool Verif_ComboBoxFraisDePort()
        {
            this._textBoxFraisDePort.Text = this._textBoxFraisDePort.Text.Trim();

			return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._textBoxFraisDePort, this._TextBlockFraisDePort);
        }

        private void _textBoxFraisDePort_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_ComboBoxFraisDePort();
        }
        #endregion

        #endregion

        #endregion

        #endregion

        #endregion

        #region Fonctions

        private void AfficherMasquerFenetre()
        {
            //Afficher ou masquer client
            if (((Entreprise)this.DataContext).Client != null && ((Entreprise)this.DataContext).Is_Client == true)
            {
                this._tabItemClient.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this._tabItemClient.Visibility = System.Windows.Visibility.Collapsed;
            }

            //Afficher ou masquer fournisseur
            if (((Entreprise)this.DataContext).Fournisseur != null && ((Entreprise)this.DataContext).Is_Fournisseur == true)
            {
                this._tabItemFournisseur.Visibility = System.Windows.Visibility.Visible;
                if (((Entreprise)this.DataContext).Fournisseur.Agence_Interimaire != null)
                {
                    this._groupBoxAgenceInterimaire.Visibility = System.Windows.Visibility.Visible;
                    this._CheckBoxInterimaire.IsChecked = true;
                }
                if (((Entreprise)this.DataContext).Fournisseur.Sous_Traitant != null)
                {
                    this._CheckBoxSousTraitant.IsChecked = true;
                }
            }
            else
            {
                this._tabItemFournisseur.Visibility = System.Windows.Visibility.Collapsed;
                this._groupBoxAgenceInterimaire.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void VerifOptionsAvantEnregistrement()
        {
            //En fonctions des liens d'héritage on fait les suppressions ... nécessaires
            if (((Entreprise)this.DataContext).Is_Client == false)
            {
                if (((Entreprise)this.DataContext).Client != null)
                {
                    ObservableCollection<Client_Condition_Reglement> toRemove = new ObservableCollection<Client_Condition_Reglement>();
                    foreach (Client_Condition_Reglement item in ((Entreprise)this.DataContext).Client.Client_Condition_Reglement)
                    {
                        toRemove.Add(item);
                    }
                    foreach (Client_Condition_Reglement item in toRemove)
                    {
                        try
                        {
                            ((App)App.Current).mySitaffEntities.Client_Condition_Reglement.DeleteObject(item);
                        }
                        catch (Exception)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }
                    }
                }
                try
                {
                    ((App)App.Current).mySitaffEntities.Client.DeleteObject(((Entreprise)this.DataContext).Client);
                }
                catch (Exception)
                {
                    ((App)App.Current).mySitaffEntities.Detach(((Entreprise)this.DataContext).Client);
                }
                ((Entreprise)this.DataContext).Client = null;
            }
            if (((Entreprise)this.DataContext).Is_Fournisseur == false)
            {
                if (((Entreprise)this.DataContext).Fournisseur.Sous_Traitant != null)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Sous_Traitant.DeleteObject(((Entreprise)this.DataContext).Fournisseur.Sous_Traitant);
                    }
                    catch (Exception)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(((Entreprise)this.DataContext).Fournisseur.Sous_Traitant);
                    }
                    ((Entreprise)this.DataContext).Fournisseur.Sous_Traitant = null;
                }
                if (((Entreprise)this.DataContext).Fournisseur.Agence_Interimaire != null)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Agence_Interimaire.DeleteObject(((Entreprise)this.DataContext).Fournisseur.Agence_Interimaire);
                    }
                    catch (Exception)
                    {
                        ((App)App.Current).mySitaffEntities.Detach(((Entreprise)this.DataContext).Fournisseur.Agence_Interimaire);
                    }
                    ((Entreprise)this.DataContext).Fournisseur.Agence_Interimaire = null;
                }
                if (((Entreprise)this.DataContext).Fournisseur != null)
                {
                    ObservableCollection<Fournisseur_Condition_Reglement> toRemove = new ObservableCollection<Fournisseur_Condition_Reglement>();
                    foreach (Fournisseur_Condition_Reglement item in ((Entreprise)this.DataContext).Fournisseur.Fournisseur_Condition_Reglement)
                    {
                        toRemove.Add(item);
                    }
                    foreach (Fournisseur_Condition_Reglement item in toRemove)
                    {
                        try
                        {
                            ((App)App.Current).mySitaffEntities.Fournisseur_Condition_Reglement.DeleteObject(item);
                        }
                        catch (Exception)
                        {
                            ((App)App.Current).mySitaffEntities.Detach(item);
                        }
                    }
                }
                try
                {
                    ((App)App.Current).mySitaffEntities.Fournisseur.DeleteObject(((Entreprise)this.DataContext).Fournisseur);
                }
                catch (Exception)
                {
                    ((App)App.Current).mySitaffEntities.Detach(((Entreprise)this.DataContext).Fournisseur);
                }
                ((Entreprise)this.DataContext).Fournisseur = null;
            }
        }

        private void ReglageProprietesDependances()
        {
            if (((Entreprise)this.DataContext).Fournisseur != null)
            {
                //On retire les conditions de réglements fournisseur déjà enregistrés
                foreach (Fournisseur_Condition_Reglement item in ((Entreprise)this.DataContext).Fournisseur.Fournisseur_Condition_Reglement)
                {
                    try
                    {
                        this.list_Condition_Reglement.Remove(item.Condition_Reglement1);
                    }
                    catch (Exception) { }
                }
            }

            //Actions à réaliser si l'entreprise est fournisseur
            if (((Entreprise)this.DataContext).Client != null)
            {
                //On retire les conditions de réglements client déjà enregistrés
                foreach (Client_Condition_Reglement item in ((Entreprise)this.DataContext).Client.Client_Condition_Reglement)
                {
                    try
                    {
                        this.list_Condition_Reglement_Client.Remove(item.Condition_Reglement1);
                    }
                    catch (Exception) { }
                }
            }

            //Actions à réaliser en général sur le fenêtre entreprise à l'ouverture
            //On construit la liste des contacts            
            this.ContactsList = new ObservableCollection<Personne>(((Entreprise)this.DataContext).Personne.Where(pe => pe.Contact != null));

            //On enlève les activités déjà présentes
            foreach (Entreprise_Activite item in ((Entreprise)this.DataContext).Entreprise_Activite)
            {
                this.Secteur_Activite.Remove(item.Activite1);
            }
        }

        #endregion

        #region Evenements

        #region fonction got focus sur la ville
        private void _ComboBoxCoordonneesVille_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.VilleList.Count == 0)
            {
                MessageBox.Show("Attention, aucune ville ne correspond à votre numéro de code postal et/ou votre pays.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void _ComboBoxCoordonneesVilleFact_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.VilleList2.Count == 0)
            {
                MessageBox.Show("Attention, aucune ville ne correspond à votre numéro de code postal et/ou votre pays.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region CheckBoxs

        #region Client

        private void Bouton_Client_Checked_1(object sender, RoutedEventArgs e)
        {
            this._tabItemClient.Visibility = System.Windows.Visibility.Visible;
            if (((Entreprise)this.DataContext).Client == null)
            {
                ((Entreprise)this.DataContext).Client = new Client();
			}
			Verif_CheckBox();
        }

        private void Bouton_Client_Unchecked_1(object sender, RoutedEventArgs e)
        {
            bool test = true;
            if (((Entreprise)this.DataContext).Client.Client_Condition_Reglement.Count() > 0 && test)
            {
                test = false;
                MessageBox.Show("Vous ne pouvez pas enlever votre entreprise du mode 'client' car elle est associée à des conditions de réglements client", "Impossible d'enlever le mode 'Client'", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            if ((((Entreprise)this.DataContext).Client.Devis.Count() > 0 || ((Entreprise)this.DataContext).Client.Devis1.Count() > 0 || ((Entreprise)this.DataContext).Client.Devis2.Count() > 0) && test)
            {
                test = false;
                MessageBox.Show("Vous ne pouvez pas enlever votre entreprise du mode 'client' car elle est associée à des devis", "Impossible d'enlever le mode 'Client'", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            if (!test)
            {
                this.Bouton_Client.IsChecked = true;
            }
            else
            {
                this._tabItemClient.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        #endregion

        #region Fournisseur

        private void Bouton_Fournisseur_Checked_1(object sender, RoutedEventArgs e)
        {
            this._tabItemFournisseur.Visibility = System.Windows.Visibility.Visible;
            if (((Entreprise)this.DataContext).Fournisseur == null)
            {
                ((Entreprise)this.DataContext).Fournisseur = new Fournisseur();
			}
			Verif_CheckBox();
        }

        private void Bouton_Fournisseur_Unchecked_1(object sender, RoutedEventArgs e)
        {
            bool test = true;
            if (((Entreprise)this.DataContext).Fournisseur != null)
            {
                if (((Entreprise)this.DataContext).Fournisseur.Fournisseur_Condition_Reglement.Count() > 0 && test)
                {
                    test = false;
                    MessageBox.Show("Vous ne pouvez pas enlever votre entreprise du mode 'fournisseur' car il est associé à des conditions de réglements fournisseur", "Impossible d'enlever le mode 'Fournisseur'", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                if (((Entreprise)this.DataContext).Fournisseur.Facture_Fournisseur.Count() > 0 && test)
                {
                    test = false;
                    MessageBox.Show("Vous ne pouvez pas enlever votre entreprise du mode 'fournisseur' car il est associé à des factures", "Impossible d'enlever le mode 'Fournisseur'", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                if (((Entreprise)this.DataContext).Fournisseur.Commande_Fournisseur.Count() > 0 && test)
                {
                    test = false;
                    MessageBox.Show("Vous ne pouvez pas enlever votre entreprise du mode 'fournisseur' car il est associé à des commandes", "Impossible d'enlever le mode 'Fournisseur'", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                if (((Entreprise)this.DataContext).Fournisseur.Bon_Livraison.Count() > 0 && test)
                {
                    test = false;
                    MessageBox.Show("Vous ne pouvez pas enlever votre entreprise du mode 'fournisseur' car il est associé à des BL", "Impossible d'enlever le mode 'Fournisseur'", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                if (((Entreprise)this.DataContext).Fournisseur.Facture_Proforma.Count() > 0 && test)
                {
                    test = false;
                    MessageBox.Show("Vous ne pouvez pas enlever votre entreprise du mode 'fournisseur' car il est associé à des factures proforma", "Impossible d'enlever le mode 'Fournisseur'", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                if (((Entreprise)this.DataContext).Fournisseur.Sous_Traitant != null)
                {
                    if (((Entreprise)this.DataContext).Fournisseur.Sous_Traitant.Tiers.Count() > 0 && test)
                    {
                        test = false;
                        MessageBox.Show("Vous ne pouvez pas enlever votre entreprise du mode 'fournisseur' car il est sous-traitant et possède des tiers associés", "Impossible d'enlever le mode 'Fournisseur'", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                if (((Entreprise)this.DataContext).Fournisseur.Agence_Interimaire != null)
                {
                    if (((Entreprise)this.DataContext).Fournisseur.Agence_Interimaire.Interimaire.Count() > 0 && test)
                    {
                        test = false;
                        MessageBox.Show("Vous ne pouvez pas enlever votre entreprise du mode 'fournisseur' car il est agence interimaire et possède des interimaires associés", "Impossible d'enlever le mode 'Fournisseur'", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
            if (!test)
            {
                this._Bouton_Fournisseur.IsChecked = true;
            }
            else
            {
                this._tabItemFournisseur.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        #endregion

        #region Fournisseur.Sous_Traitant

        private void _CheckBoxSousTraitant_Unchecked(object sender, RoutedEventArgs e)
        {
            if (((Entreprise)this.DataContext).Fournisseur.Sous_Traitant != null)
            {
                ((Entreprise)this.DataContext).Fournisseur.Sous_Traitant = null;
            }
        }

        private void _CheckBoxSousTraitant_Checked(object sender, RoutedEventArgs e)
        {
            if (((Entreprise)this.DataContext).Fournisseur.Sous_Traitant == null)
            {
                ((Entreprise)this.DataContext).Fournisseur.Sous_Traitant = new Sous_Traitant();
            }
        }

        #endregion

        #region Fournisseur.Agence_Interimaire

        private void _CheckBoxInterimaire_Checked(object sender, RoutedEventArgs e)
        {
            if (((Entreprise)this.DataContext).Fournisseur != null && ((Entreprise)this.DataContext).Is_Fournisseur == true)
            {
                if (((Entreprise)this.DataContext).Fournisseur.Agence_Interimaire == null)
                {
                    ((Entreprise)this.DataContext).Fournisseur.Agence_Interimaire = new Agence_Interimaire();
                }
                this._groupBoxAgenceInterimaire.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void _CheckBoxInterimaire_Unchecked(object sender, RoutedEventArgs e)
        {
            this._groupBoxAgenceInterimaire.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        #endregion

        #region KeyUp

        private void _TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
        }

        #endregion

        #endregion

    }
}
