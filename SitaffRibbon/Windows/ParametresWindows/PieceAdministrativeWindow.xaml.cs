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

namespace SitaffRibbon.Windows.ParametresWindows
{
    /// <summary>
    /// Logique d'interaction pour PieceAdministrativeWindow.xaml
    /// </summary>
    public partial class PieceAdministrativeWindow : Window
	{
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public PieceAdministrativeWindow()
        {
			InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._textBoxLibelle.Focus();
		}

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
                if (((App)App.Current).mySitaffEntities.Piece_Administrative.Where(act => act.Identifiant != ((Piece_Administrative)this.DataContext).Identifiant).Where(lib => lib.Libelle.Trim().ToLower() == this._textBoxLibelle.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Une pièce administrative est déjà présente avec ce libellé", "Doublon de pièce administrative", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
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
		private bool VerificationChamps()
		{
			bool verif = true;

			if (!Verif_textBoxLibelle())
			{
				verif = false;
			}


			return verif;
		}
		private bool Verif_textBoxLibelle()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(_textBoxLibelle, _textBlockLibelle);
		}


		private void _textBoxLibelle_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.Verif_textBoxLibelle();
		}

		#endregion

        #region lecture seule

        public void lectureSeule()
		{
            _textBoxLibelle.IsReadOnly = false;
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
