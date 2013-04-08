﻿using System;
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
    /// Logique d'interaction pour ConditionReglementWindow.xaml
    /// </summary>
    public partial class ConditionReglementWindow : Window
    {

        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public ConditionReglementWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxConditionReglemnt.Focus();
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
                if (((App)App.Current).mySitaffEntities.Condition_Reglement.Where(act => act.Identifiant != ((Condition_Reglement)this.DataContext).Identifiant).Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxConditionReglemnt.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Une condition de règlement est déjà présente avec ce libellé", "Doublon de condition de règlement", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

            if (!Verif_TextBoxConditionReglemnt())
            {
                verif = false;
            }


            return verif;
        }
        #region _TextBoxConditionReglemnt
        private bool Verif_TextBoxConditionReglemnt()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxConditionReglemnt, this._TextBlock, 255);
        }

        private void _TextBoxConditionReglemnt_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxConditionReglemnt();
        }
        #endregion

        #endregion

        #region lecture seule
        //Passe tous les composant en lecture seule
        public void lectureSeule()
        {
            this._TextBoxConditionReglemnt.IsReadOnly = false;
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
