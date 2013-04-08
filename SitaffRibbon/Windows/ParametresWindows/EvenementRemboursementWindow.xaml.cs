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
    /// Logique d'interaction pour EvenementRemboursementWindow.xaml
    /// </summary>
    public partial class EvenementRemboursementWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public EvenementRemboursementWindow()
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
                if (((App)App.Current).mySitaffEntities.Evenement_Remboursement.Where(act => act.Identifiant != ((Evenement_Remboursement)this.DataContext).Identifiant).
                    Where(lib => lib.Libelle.Trim().ToLower() == this._textBoxLibelle.Text.Trim().ToLower()).
                        Where(cod => cod.Code.Trim().ToLower() == this._textBoxCode.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Un évènement de remboursement est déjà présent avec ce libellé et ce code", "Doublon d'évènement de remboursement", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

			if (!Verif_textBoxLibelle() && !Verif_textBoxCode())
			{
				verif = false;
			}


			return verif;
		}

		private bool Verif_textBoxLibelle()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxLibelle, this._textBlockLibelle);
		}

		private bool Verif_textBoxCode()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxCode, this._textBlockCode);
		}

		private void _textBoxCode_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.Verif_textBoxCode();
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
            _textBoxCode.IsReadOnly = false;
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
