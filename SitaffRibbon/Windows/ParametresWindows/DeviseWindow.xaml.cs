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
    /// Logique d'interaction pour DeviseWindow.xaml
    /// </summary>
    public partial class DeviseWindow : Window
    {

        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region contructeur

        public DeviseWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxLibelle.Focus();
        }

        #endregion

        #region Verfication champs
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxSymbole())
            {
                verif = false;
            }
            if (!Verif_TextBoxLibelle())
            {
                verif = false;
            }



            return verif;
        }
        #region txtbxSymbole

        private bool Verif_TextBoxSymbole()
        {
            bool verif = true;
            char[] masque = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int j = 0;
            char i;

            if ((this._TextBoxSymbole.Text.Trim().Length > 0) && (this._TextBoxSymbole.Text.Trim().Length <= 10))
            {
                while ((j < masque.Length) && (verif))
                {
                    i = masque[j];
                    if (this._TextBoxSymbole.Text.Contains(i))
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

			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxSymbole, this.label2, verif);
            return verif;
        }

        private void _TextBoxSymbole_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSymbole();
        }

        #endregion

        #region txtbxLibelle

        private bool Verif_TextBoxLibelle()
        {
            bool verif = true;
            char[] masque = new char[] { '"', '(', '_', ')', '=', '~', '#', '{', '[', '|', '`', '\\', '^', '@', ']', '}', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '*', '+', ',', ';', ':', '!', '?', '.', '/', '§', '¨', '%', '£', 'µ', '$', '¤', '<', '>' };
            int j = 0;
            char i;

            if ((this._TextBoxLibelle.Text.Trim().Length > 0) && (this._TextBoxLibelle.Text.Trim().Length <= 150))
            {
                while ((j < masque.Length) && (verif))
                {
                    i = masque[j];
                    if (this._TextBoxLibelle.Text.Contains(i))
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
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxLibelle, this.label1, verif);
            return verif;
        }

        private void _TextBoxLibelle_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelle();
        }

        #endregion



        #endregion

        #region bouton ok et annuler

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                if (((App)App.Current).mySitaffEntities.Devise.Where(act => act.Identifiant != ((Devise)this.DataContext).Identifiant).
                    Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxLibelle.Text.Trim().ToLower()).
                        Where(sym => sym.Symbole.Trim().ToLower() == this._TextBoxSymbole.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Une devise est déjà présente avec ce libellé et ce symbole", "Doublon de devise", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            this._TextBoxLibelle.IsReadOnly = false;
            this._TextBoxSymbole.IsReadOnly = false;
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
