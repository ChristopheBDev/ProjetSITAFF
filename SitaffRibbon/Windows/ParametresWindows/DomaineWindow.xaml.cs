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
    /// Logique d'interaction pour DomaineWindow.xaml
    /// </summary>
    public partial class DomaineWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region contructeur

        public DomaineWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxDomaine.Focus();
        }

        #endregion

        #region Verfication champs
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxCode())
            {
                verif = false;
            }

            if (!Verif_TextBoxDomaine())
            {
                verif = false;
            }


            return verif;
        }
        #region _TextBoxCode

        private bool Verif_TextBoxCode()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxCode, this.label1, 10);
        }

        private void _TextBoxCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCode();
        }

        #endregion

        #region _TextBoxDomaine

        private bool Verif_TextBoxDomaine()
        {
            bool verif = true;
            char[] masque = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int j = 0;
            char i;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if ((this._TextBoxDomaine.Text.Trim().Length > 0) && (this._TextBoxDomaine.Text.Trim().Length <= 255))
            {
                while ((j < masque.Length) && (verif))
                {
                    i = masque[j];
                    if (this._TextBoxDomaine.Text.Contains(i))
                    {
                        verif = false;
                    }
                    else
                    {
                        verif = true;
                    }
                    j++;
                }
            }
            else
            {
                verif = false;
            }
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxDomaine, this.label2, verif);
            return verif;
        }

        private void _TextBoxDomaine_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxDomaine();
        }

        #endregion
        
        #endregion

        #region bouton ok et annuler

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                if (((App)App.Current).mySitaffEntities.Domaine.Where(act => act.Identifiant != ((Domaine)this.DataContext).Identifiant).
                    Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxDomaine.Text.Trim().ToLower()).
                        Where(cod => cod.Code.Trim().ToLower() == this._TextBoxCode.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Un domaine est déjà présent avec ce libellé et ce code", "Doublon de domaine", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void _ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        #region lecture seule

        public void lectureSeule()
        {
            this._TextBoxDomaine.IsReadOnly = false;
            this._TextBoxCode.IsReadOnly = false;
        }

        #endregion

        #region fenetre chargé

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #endregion
    }
}
