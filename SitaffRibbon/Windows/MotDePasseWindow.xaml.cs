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
using System.ComponentModel;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour MotDePasseWindow.xaml
    /// </summary>
    public partial class MotDePasseWindow : Window
    {
        public MotDePasseWindow()
        {
            InitializeComponent();
        }

        #region boutons

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                this.DialogResult = true;
                ((App)App.Current)._connectedUser.Mot_De_Passe = this._textBoxNouveauMotDePasse.Text;
                this.Close();
            }
        }

        private void _ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        #region Verifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!this.Verif_textBoxAncienMotDePasse())
            {
                verif = false;
            }
            if (!this.Verif_textBoxNouveauMotDePasse())
            {
                verif = false;
            }
            if (!this.Verif_textBoxEncoreMotDePasse())
            {
                verif = false;
            }

            return verif;
        }

        #region Ancien mot de passe

        private bool Verif_textBoxAncienMotDePasse()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxAncienMotDePasse, this._textBlockAncienMotDePasse, ((App)App.Current)._connectedUser.Mot_De_Passe);
        }

        private void _textBoxAncienMotDePasse_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxAncienMotDePasse();
        }

        #endregion

        #region Nouveau mot de passe

        private bool Verif_textBoxNouveauMotDePasse()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxNouveauMotDePasse, this._textBlockNouveauMotDePasse);
        }

        private void _textBoxNouveauMotDePasse_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxNouveauMotDePasse();
        }

        #endregion

        #region Encore Mot De Passe

        private bool Verif_textBoxEncoreMotDePasse()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxEncoreMotDePasse, this._textBlockEncoreMotDePasse, this._textBoxNouveauMotDePasse);
        }

        private void _textBoxEncoreMotDePasse_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_textBoxEncoreMotDePasse();
        }

        #endregion

        #endregion
		
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }


    }
}
