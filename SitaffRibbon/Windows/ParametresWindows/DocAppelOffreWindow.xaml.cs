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

namespace SitaffRibbon.Windows.ParametresWindows
{
    /// <summary>
    /// Logique d'interaction pour DocAppelOffreWindow.xaml
    /// </summary>
    public partial class DocAppelOffreWindow : Window
    {
        public DocAppelOffreWindow()
        {
            InitializeComponent();
        }

        #region proprété de dépendance

        #region mesAppelsOffres
        public ObservableCollection<Appel_Offre> mesAppelsOffres
        {
            get { return (ObservableCollection<Appel_Offre>)GetValue(mesAppelsOffresProperty); }
            set { SetValue(mesAppelsOffresProperty, value); }
        }

        // Using a DependencyProperty as the backing store for mesAppelsOffres.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty mesAppelsOffresProperty =
            DependencyProperty.Register("mesAppelsOffres", typeof(ObservableCollection<Appel_Offre>), typeof(DocAppelOffreWindow), new UIPropertyMetadata(null));
        #endregion

        #endregion

        #region Boutons

        #region bouton ok
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
        #endregion

        #region bouton annuler
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

        #endregion

        #region Verifications

        #region _TextBoxLibelle

        private bool Verif_TextBoxLibelle()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxLibelle.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBoxLibelle.Foreground = Brushes.Red;
                this._TextBoxLibelle.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBoxLibelle.Foreground = Brushes.Green;
                this._TextBoxLibelle.Background = vert;
            }

            return verif;
        }

        private void _TextBoxLibelle_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelle();
        }
        #endregion

        #region _TextBoxCommentaire

        private bool Verif_TextBoxCommentaire()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxCommentaire.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBoxCommentaire.Foreground = Brushes.Red;
                this._TextBoxCommentaire.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBoxCommentaire.Foreground = Brushes.Green;
                this._TextBoxCommentaire.Background = vert;
            }

            return verif;
        }

        private void _TextBoxCommentaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCommentaire();
        }
        #endregion

        #region _TextBoxChemin

        private bool Verif_TextBoxChemin()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxChemin.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBoxChemin.Foreground = Brushes.Red;
                this._TextBoxChemin.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBoxChemin.Foreground = Brushes.Green;
                this._TextBoxChemin.Background = vert;
            }

            return verif;
        }

        private void _TextBoxChemin_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxChemin();
        }
        #endregion
        


        #endregion

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

        //Passe tous les composant en lecture seule
        public void lectureSeule()
        {
            this._TextBoxChemin.IsEnabled = false;
            this._TextBoxCommentaire.IsEnabled = false;
            this._TextBoxLibelle.IsEnabled = false;
            this._ComboBoxAppelOffre.IsEnabled = false;
        }
    }
}
