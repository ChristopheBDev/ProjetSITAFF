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

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour AppelOffreWindow.xaml
    /// </summary>
    public partial class AppelOffreWindow : Window
    {
        public AppelOffreWindow()
        {
            InitializeComponent();
        }

        #region Propriétés de dépendances



        public ObservableCollection<Entreprise_Mere> entrepriseList
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(entrepriseListProperty); }
            set { SetValue(entrepriseListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for entrepriseList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty entrepriseListProperty =
            DependencyProperty.Register("entrepriseList", typeof(ObservableCollection<Entreprise_Mere>), typeof(AppelOffreWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Appel_Offre> etatList
        {
            get { return (ObservableCollection<Appel_Offre>)GetValue(etatListProperty); }
            set { SetValue(etatListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for etatList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty etatListProperty =
            DependencyProperty.Register("etatList", typeof(ObservableCollection<Appel_Offre>), typeof(AppelOffreWindow), new UIPropertyMetadata(null));

         
        



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




        #endregion

        #region Boutons

        /// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
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

        #region Verifications

        private bool Verif_TextBoxAppelOffreNatureTravaux()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxAppelOffreNatureTravaux.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBlockAppelOffreNatureTravaux.Foreground = Brushes.Red;
                this._TextBoxAppelOffreNatureTravaux.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockAppelOffreNatureTravaux.Foreground = Brushes.Green;
                this._TextBoxAppelOffreNatureTravaux.Background = vert;
            }

            return verif;
        }

        private void _TextBoxAppelOffreNatureTravaux_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxAppelOffreNatureTravaux();
        }

        private bool Verif_TextBoxAppelOffreLieuTravaux()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxAppelOffreLieuTravaux.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBlockAppelOffreLieuTravaux.Foreground = Brushes.Red;
                this._TextBoxAppelOffreLieuTravaux.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockAppelOffreLieuTravaux.Foreground = Brushes.Green;
                this._TextBoxAppelOffreLieuTravaux.Background = vert;
            }

            return verif;
        }

        private void _TextBoxAppelOffreLieuTravaux_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxAppelOffreLieuTravaux();
        }

        private bool Verif_TextBoxAppelOffreCommentaires()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxAppelOffreCommentaires.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBlockAppelOffreCommentaires.Foreground = Brushes.Red;
                this._TextBoxAppelOffreCommentaires.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockAppelOffreCommentaires.Foreground = Brushes.Green;
                this._TextBoxAppelOffreCommentaires.Background = vert;
            }

            return verif;
        }

        private void _TextBoxAppelOffreCommentaires_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxAppelOffreCommentaires();
        }

        private bool Verif_TextBoxAppelOffreChargéAffaires()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxAppelOffreChargéAffaires.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBlockAppelOffreChargéAffaire.Foreground = Brushes.Red;
                this._TextBoxAppelOffreChargéAffaires.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockAppelOffreChargéAffaire.Foreground = Brushes.Green;
                this._TextBoxAppelOffreChargéAffaires.Background = vert;
            }

            return verif;
        }

        private void _TextBoxAppelOffreChargéAffaires_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxAppelOffreChargéAffaires();
        }

        private bool Verif_TextBoxAppelOffreRefus()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxAppelOffreRefus.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBlockAppelOffreRefus.Foreground = Brushes.Red;
                this._TextBoxAppelOffreRefus.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockAppelOffreRefus.Foreground = Brushes.Green;
                this._TextBoxAppelOffreRefus.Background = vert;
            }

            return verif;
        }

        private void _TextBoxAppelOffreRefus_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxAppelOffreRefus();
        }

        private bool Verif_TextBoxDetailsEstimationsTotalVenteMateriel()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxDetailsEstimationsTotalVenteMateriel.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBlockDetailsEstimationsTotalVenteMateriel.Foreground = Brushes.Red;
                this._TextBoxDetailsEstimationsTotalVenteMateriel.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockDetailsEstimationsTotalVenteMateriel.Foreground = Brushes.Green;
                this._TextBoxDetailsEstimationsTotalVenteMateriel.Background = vert;
            }

            return verif;
        }

        private void _TextBoxDetailsEstimationsTotalVenteMateriel_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxDetailsEstimationsTotalVenteMateriel();
        }

        private bool Verif_TextBoxDetailsEstimationsNbHeures()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxDetailsEstimationsNbHeures.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBlockDetailsEstimationsNbHeures.Foreground = Brushes.Red;
                this._TextBoxDetailsEstimationsNbHeures.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockDetailsEstimationsNbHeures.Foreground = Brushes.Green;
                this._TextBoxDetailsEstimationsNbHeures.Background = vert;
            }

            return verif;
        }

        private void _TextBoxDetailsEstimationsNbHeures_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxDetailsEstimationsNbHeures();
        }

        private bool Verif_TextBoxAppelOffreTempsChiffrage()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxAppelOffreTempsChiffrage.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBlockAppelOffreTempsChiffrage.Foreground = Brushes.Red;
                this._TextBoxAppelOffreTempsChiffrage.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockAppelOffreTempsChiffrage.Foreground = Brushes.Green;
                this._TextBoxAppelOffreTempsChiffrage.Background = vert;
            }

            return verif;
        }

        private void _TextBoxAppelOffreTempsChiffrage_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxAppelOffreTempsChiffrage();
        }


        private bool Verif_TextBoxClientPrincipalRéférenceAO()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxClientPrincipalRéférenceAO.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBlockClientPrincipalRéférenceAO.Foreground = Brushes.Red;
                this._TextBoxClientPrincipalRéférenceAO.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockClientPrincipalRéférenceAO.Foreground = Brushes.Green;
                this._TextBoxClientPrincipalRéférenceAO.Background = vert;
            }

            return verif;
        }

        private void _TextBoxClientPrincipalRéférenceAO_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxClientPrincipalRéférenceAO();
        }

        #endregion

        /// <summary>
        /// Verifie si tous les champs sont bien renseignés.
        /// </summary>
        /// <returns>booléen vrai si tous les champs sont bien renseignés, sinon retourne faux</returns>
        private bool VerificationChamps()
        {
            //Booléen à retourner à vrai qui deviendra faux si un champ n'est pas bon dans les vérification
            bool verif = true;
            //Tableaux de caractères qui serviront de masques
            char[] masque = new char[] { '&', '"', '\'', '(', '_', ')', '=', '~', '#', '{', '[', '|', '`', '\\', '^', '@', ']', '}', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '*', '+', ',', ';', ':', '!', '?', '.', '/', '§', '¨', '%', '£', 'µ', '$', '¤', '<', '>' };
            char[] masqueAddr = new char[] { '&', '"', '\'', '(', ')', '=', '~', '#', '{', '[', '|', '`', '\\', '^', '@', ']', '}', '*', '+', ';', '!', '?', '/', '§', '%', '£', 'µ', '$', '¤', '<', '>' };


            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            //////////////////foreach (TextBox txb in this.test.Children)
            //////////////////{
            //////////////////    txb.Background = vert;
            //////////////////}
            ////On met tous les champs en vert et textblock en noirs pour dire que ce qu'ils contiennent est bon
            ////On les passera en rouge s'il sont pas bon par la suite des tests
            //this._TextBoxAddresse.Background = vert;
            //this._TextBoxCP.Background = vert;
            //this._TextBoxFirstname.Background = vert;
            //this._TextBoxLastname.Background = vert;
            //this._TextBoxTelFixe.Background = vert;
            //this._TextBoxTelMobile.Background = vert;
            //this._TextBoxVille.Background = vert;
            //this._DatePickerBirthday.Background = vert;

            //this._TextBlockAddresse.Foreground = Brushes.Black;
            //this._TextBlockBirthday.Foreground = Brushes.Black;
            //this._TextBlockCP.Foreground = Brushes.Black;
            //this._TextBlockFirstname.Foreground = Brushes.Black;
            //this._TextBlockLastname.Foreground = Brushes.Black;
            //this._TextBlockTelFixe.Foreground = Brushes.Black;
            //this._TextBlockTelMobile.Foreground = Brushes.Black;
            //this._TextBlockVille.Foreground = Brushes.Black;


            ////Vérification du Lastname (Nom de famille)
            //this._TextBoxLastname.Text = this._TextBoxLastname.Text.Trim();
            //if (this._TextBoxLastname.Text.Length > 25 || this._TextBoxLastname.Text.Length <= 0)
            //{
            //    verif = false;
            //    this._TextBlockLastname.Foreground = Brushes.Red;
            //    this._TextBoxLastname.Background = rouge;
            //}
            //else
            //{
            //    //Test des caractères impossible dans un nom
            //    foreach (char i in masque)
            //    {
            //        if (this._TextBoxLastname.Text.Contains(i))
            //        {
            //            verif = false;
            //            this._TextBlockLastname.Foreground = Brushes.Red;
            //            this._TextBoxLastname.Background = rouge;
            //        }
            //    }
            //}


            ////Vérification du Firstname (Prénom)
            //this._TextBoxFirstname.Text = this._TextBoxFirstname.Text.Trim();
            //if (this._TextBoxFirstname.Text.Length > 25 || this._TextBoxFirstname.Text.Length <= 0)
            //{
            //    verif = false;
            //    this._TextBlockFirstname.Foreground = Brushes.Red;
            //    this._TextBoxFirstname.Background = rouge;
            //}
            //else
            //{
            //    //Test des caractères impossible dans un nom
            //    foreach (char i in masque)
            //    {
            //        if (this._TextBoxFirstname.Text.Contains(i))
            //        {
            //            verif = false;
            //            this._TextBlockFirstname.Foreground = Brushes.Red;
            //            this._TextBoxFirstname.Background = rouge;
            //        }
            //    }
            //}


            ////Vérification de l'addresse
            //this._TextBoxAddresse.Text = this._TextBoxAddresse.Text.Trim();
            //if (this._TextBoxAddresse.Text.Length > 200 || this._TextBoxAddresse.Text.Length <= 0)
            //{
            //    verif = false;
            //    this._TextBlockAddresse.Foreground = Brushes.Red;
            //    this._TextBoxAddresse.Background = rouge;
            //}
            //else
            //{
            //    //Test des caractères impossible dans un nom
            //    foreach (char i in masqueAddr)
            //    {
            //        if (this._TextBoxAddresse.Text.Contains(i))
            //        {
            //            verif = false;
            //            this._TextBlockAddresse.Foreground = Brushes.Red;
            //            this._TextBoxAddresse.Background = rouge;
            //        }
            //    }
            //}

            ////Vérification du code postal
            //this._TextBoxCP.Text = this._TextBoxCP.Text.Trim();
            //if (this._TextBoxCP.Text.Length != 5)
            //{
            //    verif = false;
            //    this._TextBlockCP.Foreground = Brushes.Red;
            //    this._TextBoxCP.Background = rouge;
            //}
            //else
            //{
            //    //Test du code postal (est-il compris entre 00001 et 99999)
            //    int codepostal;
            //    bool conversion = int.TryParse(this._TextBoxCP.Text, out codepostal);

            //    if ((conversion) && (codepostal > 1 || codepostal > 99999))
            //    {

            //    }
            //    else
            //    {
            //        verif = false;
            //        this._TextBlockCP.Foreground = Brushes.Red;
            //        this._TextBoxCP.Background = rouge;
            //    }
            //}

            ////Vérification de la ville
            //this._TextBoxVille.Text = this._TextBoxVille.Text.Trim();
            //if (this._TextBoxVille.Text.Length > 100 || this._TextBoxVille.Text.Length <= 0)
            //{
            //    verif = false;
            //    this._TextBlockVille.Foreground = Brushes.Red;
            //    this._TextBoxVille.Background = rouge;
            //}
            //else
            //{
            //    //Test des caractères impossible dans un nom
            //    foreach (char i in masque)
            //    {
            //        if (this._TextBoxVille.Text.Contains(i))
            //        {
            //            verif = false;
            //            this._TextBlockVille.Foreground = Brushes.Red;
            //            this._TextBoxVille.Background = rouge;
            //        }
            //    }
            //}

            ////Vérification du téléphone fixe (optionnel)
            //this._TextBoxTelFixe.Text = this._TextBoxTelFixe.Text.Trim();
            //if (this._TextBoxTelFixe.Text.Length != 0)
            //{
            //    if (this._TextBoxTelFixe.Text.Length != 10)
            //    {
            //        verif = false;
            //        this._TextBlockTelFixe.Foreground = Brushes.Red;
            //        this._TextBoxTelFixe.Background = rouge;
            //    }
            //    else
            //    {
            //        //Test du numéro de téléphone (est-il compris entre 0100000000 et 0999999999)
            //        int telFixe;
            //        bool conversion = int.TryParse(this._TextBoxTelFixe.Text, out telFixe);

            //        if ((conversion) && (telFixe >= 100000000 || telFixe > 999999999))
            //        {

            //        }
            //        else
            //        {
            //            verif = false;
            //            this._TextBlockTelFixe.Foreground = Brushes.Red;
            //            this._TextBoxTelFixe.Background = rouge;
            //        }
            //    }
            //}

            ////Vérification du téléphone mobile (optionnel)
            //this._TextBoxTelMobile.Text = this._TextBoxTelMobile.Text.Trim();
            //if (this._TextBoxTelMobile.Text.Length != 0)
            //{
            //    if (this._TextBoxTelMobile.Text.Length != 10)
            //    {
            //        verif = false;
            //        this._TextBlockTelMobile.Foreground = Brushes.Red;
            //        this._TextBoxTelMobile.Background = rouge;
            //    }
            //    else
            //    {
            //        //Test du numéro de téléphone (est-il compris entre 0100000000 et 0999999999)
            //        int telMobile;
            //        bool conversion = int.TryParse(this._TextBoxTelMobile.Text, out telMobile);

            //        if ((conversion) && (telMobile >= 100000000 || telMobile > 999999999))
            //        {

            //        }
            //        else
            //        {
            //            verif = false;
            //            this._TextBlockTelMobile.Foreground = Brushes.Red;
            //            this._TextBoxTelMobile.Background = rouge;
            //        }
            //    }
            //}

            ////Vérification de la date (optionnel)
            //this._DatePickerBirthday.Text = this._DatePickerBirthday.Text.Trim();
            //if (this._DatePickerBirthday.Text.Length != 0)
            //{
            //    DateTime DateAnniversaire;
            //    bool conversion = DateTime.TryParse(this._DatePickerBirthday.Text, out DateAnniversaire);

            //    if (conversion)
            //    {
            //        //Si la date est bien au bon format, je vérifie que la date de naissance ne soit pas au moins supérieure à aujourd'hui
            //        if (DateAnniversaire.Date >= DateTime.Today.Date)
            //        {
            //            verif = false;
            //            this._TextBlockBirthday.Foreground = Brushes.Red;
            //            this._DatePickerBirthday.Background = rouge;
            //        }
            //    }
            //    else
            //    {
            //        verif = false;
            //        this._TextBlockBirthday.Foreground = Brushes.Red;
            //        this._DatePickerBirthday.Background = rouge;
            //    }
            //}

            return verif;
        }

        public void lectureSeule()
        {
            //griser la partie infos générale
            _ComboBoxAppelOffreEntreprise.IsEnabled = false;
            _ComboBoxAppelOffreReçu.IsEnabled = false;
            _ComboBoxAppelOffreCommentGroupeSIT.IsEnabled = false;
            _datePickerAppelOffreDateGroupe.IsEnabled = false;
            _ComboBoxAppelOffreAffecté.IsEnabled = false;
            _TextBoxAppelOffreNatureTravaux.IsEnabled = false;
            _TextBoxAppelOffreLieuTravaux.IsEnabled = false;
            _TextBoxAppelOffreCommentaires.IsEnabled = false;
            _ComboBoxAppelOffreEtat.IsEnabled = false;
            _TextBoxAppelOffreChargéAffaires.IsEnabled = false;
            _TextBoxAppelOffreRefus.IsEnabled = false;
            _datePickerAppelOffreDateRefus.IsEnabled = false;
            _ComboBoxAppelOffreCommentSuivi.IsEnabled = false;

            //griser la partie client

            _ComboBoxClientPrincipalNom.IsEnabled = false;
            _TextBoxClientPrincipalSecteurActivité.IsEnabled = false;
            _TextBoxClientPrincipalRéférenceAO.IsEnabled = false;
            _ComboBoxAdrFacturationNom.IsEnabled = false;
            _TextBoxAdrLivraisonNom.IsEnabled = false;

            //griser la partie détail

            _TextBoxDetailsEstimationsTotalVenteMateriel.IsEnabled = false;
            _TextBoxDetailsEstimationsTotalVenteMaterielPourcent.IsEnabled = false;
            _TextBoxDetailsEstimationsNbHeures.IsEnabled = false;
            _TextBoxDetailsEstimationsTotalVenteHeures.IsEnabled = false;
            _TextBoxDetailsEstimationsTotalAppelOffre.IsEnabled = false;
            _TextBoxDetailsEstimationsTotalAppelOffrePourcent.IsEnabled = false;

            //griser la partie agenda

            _TextBoxAppelOffreTempsChiffrage.IsEnabled = false;

        }

        private void _TextBoxAppelOffreDateRefus_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxDetailsEstimationsTotalAppelOffre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxDetailsEstimationsTotalAppelOffrePourcent_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxDetailsEstimationsTotalVenteHeures_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxDetailsEstimationsTotalVenteHeuresPourcent_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxDetailsEstimationsTotalVenteMaterielPourcent_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        private void _ButtonDocumentSupprimer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _ButtonDocumentNouveau_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _ButtonDocumentAjouter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _ButtonNoteSupprimer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _ButtonNoteAjouter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _ButtonNoteAfficher_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _ButtonContactSupprimer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _ButtonContactAfficher_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _ButtonContactAjouter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _dataGridAppelOffreConditionReglement_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void _ButtonCaracteristiqueSupprimer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _ButtonCaracteristiqueAjouter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _TextBoxClientPrincipalGroupe_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxClientPrincipalAdresse_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxClientPrincipalVille_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxClientPrincipalCP_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxClientPrincipalTel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxClientPrincipalFax_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxAdresseFacturation_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxAdresseFacturationVille_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxAdresseFacturationCP_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxAdresseFacturationTél_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxAdresseFacturationFax_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxAdresseLivraisonAdresse_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxAdresseLivraisonVille_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxAdresseLivraisonCP_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxAdresseLivraisonTel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _TextBoxAdresseLivraisonFax_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _buttonGaucheDroite_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _buttonDroiteGauche_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
