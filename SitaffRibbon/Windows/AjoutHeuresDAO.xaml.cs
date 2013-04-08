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
    /// Logique d'interaction pour AjoutHeuresDAO.xaml
    /// </summary>
    public partial class AjoutHeuresDAO : Window
    {
        #region Variables

        public double timeToAdd = 0;

        #endregion

        #region Constructeur

        public AjoutHeuresDAO()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);
        }

        #endregion

        #region Boutons

        #region Boutons ok / annuler

        /// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.Verif_TextBoxNombreHeure())
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

        #endregion

        #region Verifications

        private bool Verif_TextBoxNombreHeure()
        {
            return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._TextBoxNombreHeure,this._TextBlockNombreHeure);
        }

        private void _TextBoxNombreHeure_TextChanged(object sender, RoutedEventArgs e)
        {
            this.Verif_TextBoxNombreHeure();
        }

        #endregion

        #region lecture seul

        public void lectureSeule()
        {
            this._TextBoxNombreHeure.IsEnabled = false;
        }

        #endregion
    }
}
