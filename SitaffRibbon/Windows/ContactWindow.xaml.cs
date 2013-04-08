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
/* 
 * Using pour utilisation des IObservableCollection (afin d'éviter de mettre
 * System.Collections.ObjectModel.IObservableCollection en entier)
 */
using System.Collections.ObjectModel;
//Using pour utiliser le type TypeConverter pour la conversion de couleur
using System.ComponentModel;
using System.Diagnostics;
using SitaffRibbon.Classes;
using SitaffRibbon.UserControls;
using SitaffRibbon.Windows.ParametresUserControls;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {

        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public ContactWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Initialisation de la sécurité
            this.initialisationSecurite();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._ComboBoxContactCivilité.Focus();

        }

        private void initialisationPropDependance()
        {
            //Initialisation des propd
            this.CivilityList = new ObservableCollection<Civilite>(((App)App.Current).mySitaffEntities.Civilite.OrderBy(civ => civ.Libelle_Long));
            this.CFonctionList = new ObservableCollection<Contact_Fonction>(((App)App.Current).mySitaffEntities.Contact_Fonction.OrderBy(cf => cf.Libelle));
            this.CServiceList = new ObservableCollection<Contact_Service>(((App)App.Current).mySitaffEntities.Contact_Service.OrderBy(cs => cs.Libelle));
            this.EntrepriseList = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.OrderBy(ent => ent.Libelle));
            this.GroupList = new ObservableCollection<Groupe>(((App)App.Current).mySitaffEntities.Groupe.OrderBy(gp => gp.Libelle));
            this.PaysList = new ObservableCollection<Pays>(((App)App.Current).mySitaffEntities.Pays.OrderBy(gp => gp.Libelle));
            this.VilleList = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(gp => gp.Libelle));
            this.AgenceList = new ObservableCollection<Agence>(((App)App.Current).mySitaffEntities.Agence.OrderBy(age => age.Libelle));

        }

        private void initialisationSecurite()
        {
            //Mise en place des droits sur les boutons et tabs
            if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeEntreprisesControl", "Add"))
            {
                this.NewEntreprise.Visibility = Visibility.Collapsed;
            }
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeEntreprisesControl", "Look"))
            {
                this.LookEntreprise.Visibility = Visibility.Collapsed;
            }

			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl", "Add"))
            {
                this.NewCivilite.Visibility = Visibility.Collapsed;
            }
			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreCiviliteControl", "Look"))
            {
                this.LookCivilite.Visibility = Visibility.Collapsed;
            }

			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl", "Add"))
            {
                this.NewFonction.Visibility = Visibility.Collapsed;
            }
			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreContactFonctionControl", "Look"))
            {
                this.LookFonction.Visibility = Visibility.Collapsed;
            }

			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl", "Add"))
            {
                this.NewService.Visibility = Visibility.Collapsed;
            }
			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreContactServiceControl", "Look"))
            {
                this.LookService.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Persopriétés de dépendances

        /*
         * civilité
         */
        public ObservableCollection<Civilite> CivilityList
        {
            get { return (ObservableCollection<Civilite>)GetValue(CivilityListPersoperty); }
            set { SetValue(CivilityListPersoperty, value); }
        }

        // Using a DependencyPersoperty as the backing store for CivilityList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CivilityListPersoperty =
            DependencyProperty.Register("CivilityList", typeof(ObservableCollection<Civilite>), typeof(ContactWindow), new UIPropertyMetadata(null));

        /*
         * groupe
         */
        public ObservableCollection<Groupe> GroupList
        {
            get { return (ObservableCollection<Groupe>)GetValue(GroupListPersoperty); }
            set { SetValue(GroupListPersoperty, value); }
        }

        // Using a DependencyPersoperty as the backing store for GroupList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupListPersoperty =
            DependencyProperty.Register("GroupList", typeof(ObservableCollection<Groupe>), typeof(ContactWindow), new UIPropertyMetadata(null));


        /*
        * entreprise
        */
        public ObservableCollection<Entreprise> EntrepriseList
        {
            get { return (ObservableCollection<Entreprise>)GetValue(EntrepriseListPersoperty); }
            set { SetValue(EntrepriseListPersoperty, value); }
        }

        // Using a DependencyPersoperty as the backing store for EntrepriseList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EntrepriseListPersoperty =
            DependencyProperty.Register("EntrepriseList", typeof(ObservableCollection<Entreprise>), typeof(ContactWindow), new UIPropertyMetadata(null));

        /*
         * Agence
         */
        public ObservableCollection<Agence> AgenceList
        {
            get { return (ObservableCollection<Agence>)GetValue(AgenceListPersoperty); }
            set { SetValue(AgenceListPersoperty, value); }
        }

        // Using a DependencyPersoperty as the backing store for AgenceList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AgenceListPersoperty =
            DependencyProperty.Register("AgenceList", typeof(ObservableCollection<Agence>), typeof(ContactWindow), new UIPropertyMetadata(null));

        /*
         * Contact fonction
         */
        public ObservableCollection<Contact_Fonction> CFonctionList
        {
            get { return (ObservableCollection<Contact_Fonction>)GetValue(CFonctionListPersoperty); }
            set { SetValue(CFonctionListPersoperty, value); }
        }

        // Using a DependencyPersoperty as the backing store for CFonctionList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CFonctionListPersoperty =
            DependencyProperty.Register("CFonctionList", typeof(ObservableCollection<Contact_Fonction>), typeof(ContactWindow), new UIPropertyMetadata(null));

        /*
         * contact_service
         */
        public ObservableCollection<Contact_Service> CServiceList
        {
            get { return (ObservableCollection<Contact_Service>)GetValue(CServiceListPersoperty); }
            set { SetValue(CServiceListPersoperty, value); }
        }

        // Using a DependencyPersoperty as the backing store for CServiceList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CServiceListPersoperty =
            DependencyProperty.Register("CServiceList", typeof(ObservableCollection<Contact_Service>), typeof(ContactWindow), new UIPropertyMetadata(null));

        /*
         *  ville
         */
        public ObservableCollection<Ville> VilleList
        {
            get { return (ObservableCollection<Ville>)GetValue(VilleListPersoperty); }
            set { SetValue(VilleListPersoperty, value); }
        }

        // Using a DependencyPersoperty as the backing store for VilleList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VilleListPersoperty =
            DependencyProperty.Register("VilleList", typeof(ObservableCollection<Ville>), typeof(ContactWindow), new UIPropertyMetadata(null));

        /*
         * Pays
         */
        public ObservableCollection<Pays> PaysList
        {
            get { return (ObservableCollection<Pays>)GetValue(PaysListPersoperty); }
            set { SetValue(PaysListPersoperty, value); }
        }

        // Using a DependencyPersoperty as the backing store for PaysList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaysListPersoperty =
            DependencyProperty.Register("PaysList", typeof(ObservableCollection<Pays>), typeof(ContactWindow), new UIPropertyMetadata(null));




        #endregion

        #region Boutons

        /// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a Persovoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                ObservableCollection<Personne> personnesSansMoi = new ObservableCollection<Personne>();
                foreach (Personne per in ((App)App.Current).mySitaffEntities.Personne.Where(per => per.Nom.ToLower().Trim() == ((Personne)this.DataContext).Nom && per.Prenom.ToLower().Trim() == ((Personne)this.DataContext).Prenom && per.Entreprise1.Identifiant == ((Personne)this.DataContext).Entreprise1.Identifiant && per.Contact != null))
                {
                    if (per.Identifiant != ((Personne)this.DataContext).Identifiant)
                    {
                        personnesSansMoi.Add(per);
                    }
                }
                if (personnesSansMoi.Count() > 0)
                {
                    if (MessageBox.Show("Un contact avec le même nom et le même prénom est déjà existant pour cette entreprise. Voulez-vous l'ajouter tout de même ?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                    {
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
        }

        /// <summary>
        /// Fonction lancée après clic sur Annuler
        /// </summary>
        /// <param name="sender">Objet qui a Persovoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        #region Verifications

        /// <summary>
        /// Verifie si tous les champs sont bien renseignés.
        /// </summary>
        /// <returns>booléen vrai si tous les champs sont bien renseignés, sinon retourne faux</returns>
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!verif_tabCoordonnees())
            {
                verif = false;
            }
            if (!verif_tabCorrespondance())
            {
                verif = false;
            }
            if (!verif_tabDivers())
            {
                verif = false;
            }
            if (!verif_tabEntreprise())
            {
                verif = false;
            }
            if (!verif_tabLocalisation())
            {
                verif = false;
            }

            return verif;
        }

        #region tab Coordonnées

        private bool verif_tabCoordonnees()
        {
            bool test = true;

            if (!Verif_TextBoxContactNom())
            {
                test = false;
            }
            if (!Verif_TextBoxContactNomJeuneFille())
            {
                test = false;
            }
            if (!Verif_TextBoxContactPrenom())
            {
                test = false;
            }
            if (!Verif_TextBoxContactInitiales())
            {
                test = false;
            }
            if (!Verif_ComboBoxContactCivilite())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tab_Coordonnees, test);
            return test;
        }

        #region ComboBox Civilite

        private bool Verif_ComboBoxContactCivilite()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxContactCivilité, this._TextBlockContactCivilité);
        }

        private void _ComboBoxContactCivilité_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxContactCivilite();
        }

        #endregion

        #region champ Nom
        private bool Verif_TextBoxContactNom()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxContactNom, this._TextBlockContactNom);
        }

        private void _TextBoxContactNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactNom();
            this.constructionInitiales();
        }

        private void _TextBoxContactNom_LostFocus(object sender, RoutedEventArgs e)
        {
            this._TextBoxContactNom.Text = this._TextBoxContactNom.Text.ToUpper();
        }
        #endregion

        #region champ nomJeuneFille
        private bool Verif_TextBoxContactNomJeuneFille()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactNomJeuneFille, this._TextBlockContactNomJeuneFille);
        }

        private void _TextBoxContactNomJeuneFille_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactNomJeuneFille();
        }

        private void _TextBoxContactNomJeuneFille_LostFocus(object sender, RoutedEventArgs e)
        {
            this._TextBoxContactNomJeuneFille.Text = this._TextBoxContactNomJeuneFille.Text.ToUpper();
        }
        #endregion

        #region champ prenom
        private bool Verif_TextBoxContactPrenom()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactPrenom, this._TextBlockContactPrenom);
        }

        private void _TextBoxContactPrenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactPrenom();
            this.constructionInitiales();
        }
        #endregion

        #region Initiales
        private bool Verif_TextBoxContactInitiales()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactInitiales, this._TextBlockContactInitiales);
        }

        private void _TextBoxContactInitiales_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactInitiales();
        }
        #endregion

        #endregion

        #region tab Correspondance

        private bool verif_tabCorrespondance()
        {
            bool test = true;

            if (!verif_tabProfessionnelle())
            {
                test = false;
            }
            if (!verif_tabPersonnelle())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tab_Correspondance, test);
            return test;
        }

        #region tab Professionnelle

        private bool verif_tabProfessionnelle()
        {
            bool test = true;

            if (!Verif_TextBoxContactTelPortPro())
            {
                test = false;
            }
            if (!Verif_TextBoxContactTelFixePro())
            {
                test = false;
            }
            if (!Verif_TextBoxContactEmailPro())
            {
                test = false;
            }
            if (!Verif_TextBoxContactFaxPro())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tab_Correspondance_Profesionnelle, test);

            return test;
        }

        #region portable
        private bool Verif_TextBoxContactTelPortPro()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactTelPortPro, this._TextBlockContactTelPortPro);
        }

        private void _TextBoxContactTelPortPro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactTelPortPro();
        }
        #endregion

        #region fixe
        private bool Verif_TextBoxContactTelFixePro()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactTelFixePro, this._TextBlockContactTelFixePro);
        }

        private void _TextBoxContactTelFixePro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactTelFixePro();
        }
        #endregion

        #region email
        private bool Verif_TextBoxContactEmailPro()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoireMail(this._TextBoxContactEmailPro, this._TextBlockContactEmailPro); ;
        }

        private void _TextBoxContactEmailPro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this._TextBoxContactEmailPro.Text = this._TextBoxContactEmailPro.Text.Trim();
            this.Verif_TextBoxContactEmailPro();
        }
        #endregion

        #region fax
        private bool Verif_TextBoxContactFaxPro()
		{
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactFaxPro, this._TextBlockContactFaxPro);
        }

        private void _TextBoxContactFaxPro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactFaxPro();
        }
        #endregion

        #endregion

        #region tab personnelle

        private bool verif_tabPersonnelle()
        {
            bool test = true;

            if (!Verif_TextBoxContactTelPortPerso())
            {
                test = false;
            }
            if (!Verif_TextBoxContactTelFixePerso())
            {
                test = false;
            }
            if (!Verif_TextBoxContactEmailPerso())
            {
                test = false;
            }
            if (!Verif_TextBoxContactFaxPerso())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tab_Correspondance_Personnelle, test);

            return test;
        }

        #region portable
        private bool Verif_TextBoxContactTelPortPerso()
		{
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactTelPortPerso, this._TextBlockContactTelPortPerso);
        }

        private void _TextBoxContactTelPortPerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactTelPortPerso();
        }
        #endregion

        #region fixe
        private bool Verif_TextBoxContactTelFixePerso()
		{
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactTelFixePerso, this._TextBlockContactTelFixePerso);
        }

        private void _TextBoxContactTelFixePerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactTelFixePerso();
        }
        #endregion

        #region mail
        private bool Verif_TextBoxContactEmailPerso()
		{
			return ((App)App.Current).verifications.TextBoxNonObligatoireMail(this._TextBoxContactEmailPerso, this._TextBlockContactEmailPerso);
        }

        private void _TextBoxContactEmailPerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactEmailPerso();
        }
        #endregion

        #region fax
        private bool Verif_TextBoxContactFaxPerso()
		{
            return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactFaxPerso, this._TextBlockContactFaxPerso);
        }

        private void _TextBoxContactFaxPerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactFaxPerso();
        }
        #endregion

        #endregion

        #endregion

        #region tab Entreprise

        private bool verif_tabEntreprise()
        {
            bool test = true;

            if (!Verif_ComboBoxContactEntreprise())
            {
                test = false;
            }
            if (!Verif_ComboBoxContactEntrepriseFonction())
            {
                test = false;
            }
            if (!Verif_ComboBoxContactEntrepriseService())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tab_Entreprise, test);

            return test;
        }

        #region entreprise
        private bool Verif_ComboBoxContactEntreprise()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxContactEntreprise, this._TextBlockContactEntreprise);
        }

        private void _ComboBoxContactEntreprise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxContactEntreprise();
        }
        #endregion

        #region fonction
        private bool Verif_ComboBoxContactEntrepriseFonction()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._ComboBoxContactEntrepriseFonction,this._TextBlockContactEntrepriseFonction);
        }

        private void _ComboBoxContactEntrepriseFonction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxContactEntrepriseFonction();
        }
        #endregion

        #region service
        private bool Verif_ComboBoxContactEntrepriseService()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._ComboBoxContactEntrepriseService, this._TextBlockContactEntrepriseService);
        }

        private void _ComboBoxContactEntrepriseService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxContactEntrepriseService();
        }
        #endregion

        #endregion

        #region tab Divers

        private bool verif_tabDivers()
        {
            bool test = true;

            if (!Verif_TextBoxContactCommentaires())
            {
                test = false;
            }
            if (!Verif_TextBoxContactCentresInterets())
            {
                test = false;
            }
            if (!Verif_TextBoxContactCadeaux())
            {
                test = false;
            }
            if (!Verif_TextBoxContactAmis())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tab_Divers, test);

            return test;
        }

        #region Commentaires
        private bool Verif_TextBoxContactCommentaires()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactCommentaires, this._TextBlockContactCommentaires);
        }

        private void _TextBoxContactCommentaires_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactCommentaires();
        }
        #endregion

        #region Centres d'interêts
        private bool Verif_TextBoxContactCentresInterets()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactCentresInterets, this._TextBlockContactCentresInterets);
        }

        private void _TextBoxContactCentresInterets_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactCentresInterets();
        }
        #endregion

        #region Cadeaux
        private bool Verif_TextBoxContactCadeaux()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactCadeaux, this._TextBlockContactCadeaux);
        }

        private void _TextBoxContactCadeaux_LostFocus(object sender, RoutedEventArgs e)
        {
            this._TextBoxContactCadeaux.Text = this._TextBoxContactCadeaux.Text.Trim();
            if (this._TextBoxContactCadeaux.Text.Length == 0)
            {
                this._CheckBoxCadeau.IsChecked = false;
            }
            else
            {
                this._CheckBoxCadeau.IsChecked = true;
            }
            this.Verif_TextBoxContactCadeaux();
        }
        #endregion

        #region Amis
        private bool Verif_TextBoxContactAmis()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactAmis, this._TextBlockContactAmis);
        }

        private void _TextBoxContactAmis_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactAmis();
        }
        #endregion

        #endregion

        #region temp adresse useless
        private bool verif_tabLocalisation()
        {
            bool test = true;

            if (!Verif_TextBoxContactAdressePro())
            {
                test = false;
            }
            if (!Verif_TextBoxContactAdressePerso())
            {
                test = false;
            }

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tab_localisation, test);

            return test;
        }
        #region Adresse Pro
        private bool Verif_TextBoxContactAdressePro()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactAdressePro, this._TextBlockContactAdressePro);
        }

        private void _TextBoxContactAdressePro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactAdressePro();
        }

        private bool Verif_TextBoxContactAdresseCompPro()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactAdresseCompPro, this._TextBlockContactAdresseCompPro);
        }

        private void _TextBoxContactAdresseCompPro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactAdresseCompPro();
        }

        #region Champs Ville
        private bool Verif_ComboBoxContactVillePro()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxContactVillePro.SelectedItem == null)
            {
                verif = true;
                this._TextBlockContactVillePro.Foreground = Brushes.Green;
                this._ComboBoxContactVillePro.Background = vert;
            }
            else
            {
                this._TextBoxContactCodePostalPro.Text = ((Ville)this._ComboBoxContactVillePro.SelectedItem).Code_Postal;
                this._ComboBoxContactPaysPro.SelectedItem = ((Ville)this._ComboBoxContactVillePro.SelectedItem).Pays1;
                verif = true;
                this._TextBlockContactVillePro.Foreground = Brushes.Green;
                this._ComboBoxContactVillePro.Background = vert;
            }

            return verif;
        }

        private void _ComboBoxContactVillePro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxContactVillePro();
        }

        #endregion

        #region Champs Code postal
        private bool Verif_TextBoxContactCodePostalPro()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);


            if (this._TextBoxContactCodePostalPro.Text.Trim().Length < 5 || this._TextBoxContactCodePostalPro.Text.Trim().Length > 5)
            {
                verif = true;
                this._TextBlockContactCodePostalPro.Foreground = Brushes.Green;
                this._TextBoxContactCodePostalPro.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockContactCodePostalPro.Foreground = Brushes.Green;
                this._TextBoxContactCodePostalPro.Background = vert;
            }

            ObservableCollection<Ville> tmp = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville);
            if (this._TextBoxContactCodePostalPro.Text.Trim().Length != 0)
            {
                tmp = new ObservableCollection<Ville>(tmp.Where(vil => vil.Code_Postal == this._TextBoxContactCodePostalPro.Text.Trim()));
            }
            if (this._ComboBoxContactPaysPro.SelectedItem != null)
            {
                tmp = new ObservableCollection<Ville>(tmp.Where(vil => vil.Pays1 == (Pays)this._ComboBoxContactPaysPro.SelectedItem));
            }
            this._ComboBoxContactVillePro.ItemsSource = tmp;

            return verif;
        }

        private void _TextBoxContactCodePostalPro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactCodePostalPro();
        }

        #endregion

        #region Champs Pays
        private bool Verif_ComboBoxContactPaysPro()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxContactPaysPro.SelectedItem == null)
            {
                verif = true;
                this._TextBlockContactPaysPro.Foreground = Brushes.Green;
                this._ComboBoxContactPaysPro.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockContactPaysPro.Foreground = Brushes.Green;
                this._ComboBoxContactPaysPro.Background = vert;
            }

            ObservableCollection<Ville> tmp = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville);
            if (this._TextBoxContactCodePostalPro.Text.Trim().Length != 0)
            {
                tmp = new ObservableCollection<Ville>(tmp.Where(vil => vil.Code_Postal == this._TextBoxContactCodePostalPro.Text.Trim()));
            }
            if (this._ComboBoxContactPaysPro.SelectedItem != null)
            {
                tmp = new ObservableCollection<Ville>(tmp.Where(vil => vil.Pays1 == (Pays)this._ComboBoxContactPaysPro.SelectedItem));
            }
            this._ComboBoxContactVillePro.ItemsSource = tmp;

            return verif;
        }

        private void _ComboBoxCoordonneesPays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxContactPaysPro();
        }

        #endregion

        #endregion

        #region adresse perso

        private bool Verif_TextBoxContactAdressePerso()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactAdressePerso, this._TextBlockContactAdressePerso);
        }

        private void _TextBoxContactAdressePerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactAdressePerso();
        }

        private bool Verif_TextBoxContactAdresseCompPerso()
        {
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._TextBoxContactAdresseCompPerso, this._TextBlockContactAdresseCompPerso);
        }

        private void _TextBoxContactAdresseCompPerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactAdresseCompPerso();
        }

        #region Champs Ville
        private bool Verif_ComboBoxContactVillePerso()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxContactVillePerso.SelectedItem == null)
            {
                verif = true;
                this._TextBlockContactVillePerso.Foreground = Brushes.Green;
                this._ComboBoxContactVillePerso.Background = vert;
            }
            else
            {
                this._TextBoxContactCodePostalPerso.Text = ((Ville)this._ComboBoxContactVillePerso.SelectedItem).Code_Postal;
                this._ComboBoxContactPaysPerso.SelectedItem = ((Ville)this._ComboBoxContactVillePerso.SelectedItem).Pays1;
                verif = true;
                this._TextBlockContactVillePerso.Foreground = Brushes.Green;
                this._ComboBoxContactVillePerso.Background = vert;
            }

            return verif;
        }

        private void _ComboBoxContactVillePerso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxContactVillePerso();
        }

        #endregion

        #region Champs Code postal
        private bool Verif_TextBoxContactCodePostalPerso()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);


            if (this._TextBoxContactCodePostalPerso.Text.Trim().Length < 5 || this._TextBoxContactCodePostalPerso.Text.Trim().Length > 5)
            {
                verif = true;
                this._TextBlockContactCodePostalPerso.Foreground = Brushes.Green;
                this._TextBoxContactCodePostalPerso.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockContactCodePostalPerso.Foreground = Brushes.Green;
                this._TextBoxContactCodePostalPerso.Background = vert;
            }

            ObservableCollection<Ville> tmp = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville);
            if (this._TextBoxContactCodePostalPerso.Text.Trim().Length != 0)
            {
                tmp = new ObservableCollection<Ville>(tmp.Where(vil => vil.Code_Postal == this._TextBoxContactCodePostalPerso.Text.Trim()));
            }
            if (this._ComboBoxContactPaysPerso.SelectedItem != null)
            {
                tmp = new ObservableCollection<Ville>(tmp.Where(vil => vil.Pays1 == (Pays)this._ComboBoxContactPaysPerso.SelectedItem));
            }
            this._ComboBoxContactVillePerso.ItemsSource = tmp;

            return verif;
        }

        private void _TextBoxContactCodePostalPerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxContactCodePostalPerso();
        }

        #endregion

        #region Champs Pays
        private bool Verif_ComboBoxContactPaysPerso()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxContactPaysPerso.SelectedItem == null)
            {
                verif = true;
                this._TextBlockContactPaysPerso.Foreground = Brushes.Green;
                this._ComboBoxContactPaysPerso.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockContactPaysPerso.Foreground = Brushes.Green;
                this._ComboBoxContactPaysPerso.Background = vert;
            }

            ObservableCollection<Ville> tmp = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville);
            if (this._TextBoxContactCodePostalPerso.Text.Trim().Length != 0)
            {
                tmp = new ObservableCollection<Ville>(tmp.Where(vil => vil.Code_Postal == this._TextBoxContactCodePostalPerso.Text.Trim()));
            }
            if (this._ComboBoxContactPaysPerso.SelectedItem != null)
            {
                tmp = new ObservableCollection<Ville>(tmp.Where(vil => vil.Pays1 == (Pays)this._ComboBoxContactPaysPerso.SelectedItem));
            }
            this._ComboBoxContactVillePerso.ItemsSource = tmp;

            return verif;
        }

        private void _ComboBoxCoordonneesPaysPerso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxContactPaysPerso();
        }

        #endregion

        #endregion
        #endregion

        #endregion

        #region Fonctions

        #region initiales
        private void constructionInitiales()
        {
            String initiales = "";
            int i = 0;
            if (this._TextBoxContactPrenom.Text.Length > 0)
            {
                foreach (char c in this._TextBoxContactPrenom.Text)
                {
                    if (i == 0)
                    {
                        initiales += c;
                    }
                    i++;
                    if (c == '-')
                    {
                        i = 0;
                    }
                }
            }

            if (this._TextBoxContactNom.Text.Length > 0)
            {
                i = 0;
                foreach (char c in this._TextBoxContactNom.Text)
                {
                    if (i == 0)
                    {
                        initiales += c;
                    }
                    i++;
                }
            }

            this._TextBoxContactInitiales.Text = initiales.ToUpper();
        }

        #endregion

        #region Lecture Seule

        //Passe tous les composant en lecture seule
        public void lectureSeule()
        {
            //Coordonnées
            _ComboBoxContactCivilité.IsEnabled = false;
            _TextBoxContactNom.IsReadOnly = true;
            _TextBoxContactNomJeuneFille.IsReadOnly = true;
            _TextBoxContactPrenom.IsReadOnly = true;
            _TextBoxContactInitiales.IsReadOnly = true;
            NewCivilite.IsEnabled = false;
            _TextBoxContactCommentaires.IsReadOnly = true;

            //Correspondance
            _TextBoxContactTelFixePerso.IsReadOnly = true;
            _TextBoxContactTelPortPerso.IsReadOnly = true;
            _TextBoxContactFaxPerso.IsReadOnly = true;
            _TextBoxContactEmailPerso.IsReadOnly = true;
            _TextBoxContactTelFixePro.IsReadOnly = true;
            _TextBoxContactTelPortPro.IsReadOnly = true;
            _TextBoxContactFaxPro.IsReadOnly = true;
            _TextBoxContactEmailPro.IsReadOnly = true;
            //sendMail.IsEnabled = false;
            //sendMailPerso.IsEnabled = false;

            //Entreprise
            _ComboBoxContactEntreprise.IsEnabled = false;
            _ComboBoxContactEntrepriseFonction.IsEnabled = false;
            _ComboBoxContactEntrepriseService.IsEnabled = false;
            NewEntreprise.IsEnabled = false;
            NewFonction.IsEnabled = false;
            NewService.IsEnabled = false;
            NullFonction.IsEnabled = false;
            NullService.IsEnabled = false;

            //Divers
            _TextBoxContactCentresInterets.IsReadOnly = true;
            _TextBoxContactCadeaux.IsReadOnly = true;
            _CheckBoxCadeau.IsEnabled = false;
            _TextBoxContactAmis.IsReadOnly = true;

            //Adresse
            _TextBoxContactAdresse.IsReadOnly = true;
            _TextBoxContactAdresseComp.IsReadOnly = true;
            _TextBoxContactVille.IsReadOnly = true;
            _TextBoxContactCodePostal.IsReadOnly = true;
            _TextBoxContactPays.IsReadOnly = true;

            _TextBoxContactAdressePro.IsReadOnly = true;
            _TextBoxContactAdresseCompPro.IsReadOnly = true;
            _ComboBoxContactVillePro.IsReadOnly = true;
            _TextBoxContactCodePostalPro.IsReadOnly = true;
            _ComboBoxContactPaysPro.IsReadOnly = true;

            _TextBoxContactAdressePerso.IsReadOnly = true;
            _TextBoxContactAdresseCompPerso.IsReadOnly = true;
            _ComboBoxContactVillePerso.IsReadOnly = true;
            _TextBoxContactCodePostalPerso.IsReadOnly = true;
            _ComboBoxContactPaysPerso.IsReadOnly = true;
        }

        #endregion

        #region vide s'il n'y a pas de cadeaux

        private void _CheckBoxCadeau_Click(object sender, RoutedEventArgs e)
        {
            if (this._CheckBoxCadeau.IsChecked == false)
            {
                this._TextBoxContactCadeaux.Text = "";
            }
        }

        #endregion

        #region Fonctions Adresses auto

        private void _ComboBoxContactVillePerso_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((ObservableCollection<Ville>)this._ComboBoxContactVillePerso.ItemsSource).Count == 0)
            {
                MessageBox.Show("Attention, aucune ville ne correspond à votre numéro de code postal et/ou votre pays.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void _ComboBoxContactVillePro_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((ObservableCollection<Ville>)this._ComboBoxContactVillePro.ItemsSource).Count == 0)
            {
                MessageBox.Show("Attention, aucune ville ne correspond à votre numéro de code postal et/ou votre pays.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion

        #endregion

        #region fenetre chargé

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #endregion

        #region boutons sendMail
        private void sendMail_Click(object sender, RoutedEventArgs e)
        {
            MailViaOutlook sendMail = new MailViaOutlook();
            sendMail.send(this._TextBoxContactEmailPro.Text);
        }

        private void sendMailPerso_Click(object sender, RoutedEventArgs e)
        {
            MailViaOutlook sendMail = new MailViaOutlook();
            sendMail.send(this._TextBoxContactEmailPerso.Text);
        }
        #endregion

        #region boutons nouveau / voir

        #region entreprise
        private void NewEntreprise_Click(object sender, RoutedEventArgs e)
        {
            ListeEntreprisesControl listeEntrepriseControl = new ListeEntreprisesControl();
            Entreprise entreprise = listeEntrepriseControl.Add();
            if (entreprise != null)
            {
                this.EntrepriseList.Add(entreprise);
                this.EntrepriseList = new ObservableCollection<Entreprise>(this.EntrepriseList.OrderBy(ent => ent.Libelle));
                this._ComboBoxContactEntreprise.SelectedItem = entreprise;
            }
            else
            {
                this._ComboBoxContactEntreprise.SelectedItem = null;
            }
        }

        private void LookEntreprise_Click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxContactEntreprise.SelectedItem != null)
            {
                //Création de la fenêtre
                EntrepriseWindow entrepriseWindow = new EntrepriseWindow();

                //Initialisation du Datacontext en entreprise et association à la entreprise sélectionnée
                entrepriseWindow.DataContext = new Entreprise();
                entrepriseWindow.DataContext = (Entreprise)this._ComboBoxContactEntreprise.SelectedItem;

                //Je positionne la lecture seule sur la fenêtre
                entrepriseWindow.creation = false;
                entrepriseWindow.SoloLecture = true;
                entrepriseWindow.lectureSeule();

                //J'affiche la fenêtre
                entrepriseWindow.ShowDialog();
            }
        }
        #endregion

        #region Civilité
        private void NewCivilite_Click(object sender, RoutedEventArgs e)
        {
            ParametreCiviliteControl parametreCiviliteControl = new ParametreCiviliteControl();
            Civilite civilite = parametreCiviliteControl.Add();
            if (civilite != null)
            {
                this.CivilityList.Add(civilite);
                this.CivilityList = new ObservableCollection<Civilite>(this.CivilityList.OrderBy(civ => civ.Libelle_Long));
                this._ComboBoxContactCivilité.SelectedItem = civilite;
            }
            else
            {
                this._ComboBoxContactCivilité.SelectedItem = null;
            }
        }

        private void LookCivilite_Click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxContactCivilité.SelectedItem != null)
            {
                ParametreCiviliteControl parametreCiviliteControl = new ParametreCiviliteControl();
                parametreCiviliteControl.Look((Civilite)this._ComboBoxContactCivilité.SelectedItem);
            }
        }
        #endregion

        #region Agence
        private void LookAgence_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewAgence_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Fonction
        private void NewFonction_Click(object sender, RoutedEventArgs e)
        {
            ParametreContactFonctionControl parametreContactFonctionControl = new ParametreContactFonctionControl();
            Contact_Fonction contact_fonction = parametreContactFonctionControl.Add();
            if (contact_fonction != null)
            {
                this.CFonctionList.Add(contact_fonction);
                this.CFonctionList = new ObservableCollection<Contact_Fonction>(this.CFonctionList.OrderBy(cf => cf.Libelle));
                this._ComboBoxContactEntrepriseFonction.SelectedItem = contact_fonction;
            }
            else
            {
                this._ComboBoxContactCivilité.SelectedItem = null;
            }
        }

        private void LookFonction_Click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxContactEntrepriseFonction.SelectedItem != null)
            {
                ParametreContactFonctionControl parametreContactFonctionControl = new ParametreContactFonctionControl();
                parametreContactFonctionControl.Look((Contact_Fonction)this._ComboBoxContactEntrepriseFonction.SelectedItem);
            }
        }
        #endregion

        #region Service
        private void NewService_Click(object sender, RoutedEventArgs e)
        {
            ParametreContactServiceControl parametreContactServiceControl = new ParametreContactServiceControl();
            Contact_Service contact_service = parametreContactServiceControl.Add();
            if (contact_service != null)
            {
                this.CServiceList.Add(contact_service);
                this.CServiceList = new ObservableCollection<Contact_Service>(this.CServiceList.OrderBy(cs => cs.Libelle));
                this._ComboBoxContactEntrepriseService.SelectedItem = contact_service;
            }
            else
            {
                this._ComboBoxContactEntrepriseService.SelectedItem = null;
            }
        }

        private void LookService_Click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxContactEntrepriseService.SelectedItem != null)
            {
                ParametreContactServiceControl parametreContactServiceControl = new ParametreContactServiceControl();
                ParametresMain parametresMain = new ParametresMain(((App)App.Current)._theMainWindow);
                parametreContactServiceControl._DataGridMain.SelectedItem = (Contact_Service)this._ComboBoxContactEntrepriseService.SelectedItem;
                parametresMain.LookContactService(parametreContactServiceControl);
            }
        }
        #endregion

        #endregion

        #region null comboBox

        private void NullFonction_Click_1(object sender, RoutedEventArgs e)
        {
            this._ComboBoxContactEntrepriseFonction.SelectedItem = null;
        }

        private void NullService_Click_1(object sender, RoutedEventArgs e)
        {
            this._ComboBoxContactEntrepriseService.SelectedItem = null;
        }

        #endregion

    }
}
