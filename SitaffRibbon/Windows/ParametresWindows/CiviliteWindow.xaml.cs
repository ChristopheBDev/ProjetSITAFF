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
    /// Logique d'interaction pour CiviliteWindow.xaml
    /// </summary>
    public partial class CiviliteWindow : Window
    {

        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public CiviliteWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxLibelleLong.Focus();
        }

        #endregion

        #region Verfication champs
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxLibelleLong())
            {
                verif = false;
            }
            if (!Verif_TextBoxLibelleCourt())
            {
                verif = false;
            }


            return verif;
        }
        #region txtbxLibelleLong

        private bool Verif_TextBoxLibelleLong()
        {
            bool verif = true;
            char[] masque = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int j = 0;
            char i;

            if ((this._TextBoxLibelleLong.Text.Trim().Length > 0) && (this._TextBoxLibelleLong.Text.Trim().Length <= 50))
            {
                while ((j < masque.Length) && (verif))
                {
                    i = masque[j];
                    if (this._TextBoxLibelleLong.Text.Contains(i))
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

			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxLibelleLong, this._textBlockCivilite, verif);
            return verif;
        }

        private void _TextBoxLibelleLong_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelleLong();
        }

        #endregion

        #region txtbxLibelleCourt

        private bool Verif_TextBoxLibelleCourt()
        {
            bool verif = true;
            char[] masque = new char[] {'1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int j = 0;
            char i;

            if ((this._TextBoxLibelleCourt.Text.Trim().Length > 0) && (this._TextBoxLibelleCourt.Text.Trim().Length <= 20))
            {
                while ((j < masque.Length) && (verif))
                {
                    i = masque[j];
                    if (this._TextBoxLibelleCourt.Text.Contains(i))
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
			((App)App.Current).verifications.MettreTextBoxEnCouleur(this._TextBoxLibelleCourt,this._textBlockDimin,verif);
            return verif;
        }

        private void _TextBoxLibelleCourt_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelleCourt();
        }

        #endregion       

        #endregion

        #region bouton ok et annuler

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                if (((App)App.Current).mySitaffEntities.Civilite.Where(act => act.Identifiant != ((Civilite)this.DataContext).Identifiant).
                    Where(lib => lib.Libelle_Court.Trim().ToLower() == this._TextBoxLibelleCourt.Text.Trim().ToLower()).
                        Where(lib => lib.Libelle_Long.Trim().ToLower() == this._TextBoxLibelleLong.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Une civilité est déjà présente avec ce diminutif et ce libellé", "Doublon de civilité", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            this._TextBoxLibelleCourt.IsReadOnly = false;
            this._TextBoxLibelleLong.IsReadOnly = false;
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
