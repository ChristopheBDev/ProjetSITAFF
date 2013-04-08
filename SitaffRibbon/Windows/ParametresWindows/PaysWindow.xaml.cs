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
    /// Logique d'interaction pour PaysWindow.xaml
    /// </summary>
    public partial class PaysWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public PaysWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxNom.Focus();
        }

        #endregion

        #region bouton ok et annuler

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                if (((App)App.Current).mySitaffEntities.Pays.Where(act => act.Identifiant != ((Pays)this.DataContext).Identifiant).
                    Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxNom.Text.Trim().ToLower()).
                        Where(cod => cod.Code.Trim().ToLower() == this._TextBoxCode.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Un pays est déjà présent avec ce libellé et ce code", "Doublon de pays", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void _ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        #region verfification
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxNom())
            {
                verif = false;
            }
            if (!Verif_TextBoxCode())
            {
                verif = false;
            }
            if (!Verif_TextBoxMonnaie())
            {
                verif = false;
            }
            if (!Verif_TextBoxMonnaieDivision())
            {
                verif = false;
            }
            if (!Verif_TextBoxAttribution())
            {
                verif = false;
            }
            if (!Verif_TextBoxCodeTelephonique())
            {
                verif = false;
            }
            if (!Verif_TextBoxLibelleNational())
            {
                verif = false;
            }


            return verif;
        }
        #region _TextBoxNom

        private bool Verif_TextBoxNom()
        {
            bool verif = true;
            char[] masque = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int j = 0;
            char i;

            if ((this._TextBoxNom.Text.Trim().Length > 0) && (this._TextBoxNom.Text.Trim().Length <= 255))
            {
                while ((j < masque.Length) && (verif))
                {
                    i = masque[j];
                    if (this._TextBoxNom.Text.Contains(i))
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

			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxNom, this.textBlock3, verif);
            return verif;
        }

        private void _TextBoxNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxNom();
        }
        #endregion

        #region _TextBoxCode

        private bool Verif_TextBoxCode()
        {
            bool verif = true;
            char[] masque = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int j = 0;
            char i;

            if ((this._TextBoxCode.Text.Trim().Length > 0) && (this._TextBoxCode.Text.Trim().Length <= 5))
            {
                while ((j < masque.Length) && (verif))
                {
                    i = masque[j];
                    if (this._TextBoxCode.Text.Contains(i))
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

			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxCode, this.textBlock1, verif);
            return verif;
        }

        private void _TextBoxCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCode();
        }

        #endregion

        #region _TextBoxMonnaie

        private bool Verif_TextBoxMonnaie()
        {
            bool verif = true;
            char[] masque = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int j = 0;
            char i;

            if ((this._TextBoxMonnaie.Text.Trim().Length > 0) && (this._TextBoxMonnaie.Text.Trim().Length <= 25))
            {
                while ((j < masque.Length) && (verif))
                {
                    i = masque[j];
                    if (this._TextBoxMonnaie.Text.Contains(i))
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

			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxMonnaie, this.textBlock2, verif);

            return verif;
        }

        private void _TextBoxMonnaie_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxMonnaie();
        }
        #endregion

        #region _TextBoxMonnaieDivision

        private bool Verif_TextBoxMonnaieDivision()
        {
            bool verif = true;
            char[] masque = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int j = 0;
            char i;

            if ((this._TextBoxMonnaieDivision.Text.Trim().Length > 0) && (this._TextBoxMonnaieDivision.Text.Trim().Length <= 25))
            {
                while ((j < masque.Length) && (verif))
                {
                    i = masque[j];
                    if (this._TextBoxMonnaieDivision.Text.Contains(i))
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

			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxMonnaieDivision, this.textBlock4, verif);

            return verif;
        }

        private void _TextBoxMonnaieDivision_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxMonnaieDivision();
        }
        #endregion

        #region _TextBoxAttribution

        private bool Verif_TextBoxAttribution()
        {
            bool verif = true;
            char[] masque = new char[] { '&', '"', '\'', '(', '_', ')', '=', '~', '#', '{', '[', '|', '`', '\\', '^', '@', ']', '}', '*', '+', ',', ';', ':', '!', '?', '.', '/', '§', '¨', '%', '£', 'µ', '$', '¤', '<', '>', 'a', 'z', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'q', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'w', 'x', 'c', 'v', 'b', 'n' };
            int j = 0;
            char i;

            if ((this._TextBoxAttribution.Text.Trim().Length > 0)&&(this._TextBoxAttribution.Text.Trim().Length <= 50))
            {

                //foreach (char i in masque)
                while ((j < masque.Length) && (verif))
                {
                    i = masque[j];
                    if (this._TextBoxAttribution.Text.Contains(i))
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
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxAttribution, this.textBlock5, verif);

            return verif;
        }

        private void _TextBoxAttribution_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxAttribution();
        }
        #endregion

        #region _TextBoxCodeTelephonique

        private bool Verif_TextBoxCodeTelephonique()
        {
            bool verif = true;
            char[] masque = new char[] { '&', '"', '\'', '(', '_', ')', '=', '~', '#', '{', '[', '|', '`', '\\', '^', '@', ']', '}', '*', ',', ';', ':', '!', '?', '.', '/', '§', '¨', '%', '£', 'µ', '$', '¤', '<', '>', 'a', 'z', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'q', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'w', 'x', 'c', 'v', 'b', 'n' };
            int j = 0;
            char i;

            if (this._TextBoxCodeTelephonique.Text.Trim().Length == 3)
            {
                if (this._TextBoxCodeTelephonique.Text.Trim().Substring(0, 1) == "+")
                {

                    //foreach (char i in masque)
                    while ((j < masque.Length) && (verif))
                    {
                        i = masque[j];
                        if (this._TextBoxCodeTelephonique.Text.Contains(i))
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
            }
            else
            {
                verif = false;
            }
            
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxCodeTelephonique, this.textBlock6, verif);

            return verif;
        }

        private void _TextBoxCodeTelephonique_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCodeTelephonique();
        }
        #endregion

        #region _TextBoxLibelleNational

        private bool Verif_TextBoxLibelleNational()
        {
            bool verif = true;
            char[] masque = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int j = 0;
            char i;

            if ((this._TextBoxLibelleNational.Text.Trim().Length > 0) && (this._TextBoxLibelleNational.Text.Trim().Length <= 100))
            {
                while ((j < masque.Length) && (verif))
                {
                    i = masque[j];
                    if (this._TextBoxLibelleNational.Text.Contains(i))
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

			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxLibelleNational, this.textBlock7, verif);

            return verif;
        }

        private void _TextBoxLibelleNational_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelleNational();
        }

        #endregion

        #endregion

        #region lecture seule

        public void lectureSeule()
        {
            this._TextBoxNom.IsReadOnly = false;
            this._TextBoxMonnaieDivision.IsReadOnly = false;
            this._TextBoxMonnaie.IsReadOnly = false;
            this._TextBoxLibelleNational.IsReadOnly = false;
            this._TextBoxCodeTelephonique.IsReadOnly = false;
            this._TextBoxCode.IsReadOnly = false;
            this._TextBoxAttribution.IsReadOnly = false;

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
