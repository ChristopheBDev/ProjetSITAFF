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
    /// Logique d'interaction pour TypeCommandeWindow.xaml
    /// </summary>
    public partial class TypeCommandeWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public TypeCommandeWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxLibelle.Focus();
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
                if (((App)App.Current).mySitaffEntities.Type_Commande.Where(act => act.Identifiant != ((Type_Commande)this.DataContext).Identifiant).
                    Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxLibelle.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Un type de commande est déjà présent avec ce libellé", "Doublon de type de commande", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

            if (!Verif_TextBoxLibelle())
            {
                verif = false;
            }
            if (!this.Verif_TextBoxDescription())
            {
                verif = false;
            }


            return verif;
        }

        private bool Verif_TextBoxLibelle()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(_TextBoxLibelle, _TextBlockLibelle);
        }


        private void _TextBoxLibelle_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelle();
        }

        private bool Verif_TextBoxDescription()
        {
				return ((App)App.Current).verifications.TextBoxNonObligatoire(_TextBoxDescription, _TextBlockDescription);
        }

        private void _TextBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxDescription();
        }

        #endregion

        #region lecture seule

        public void lectureSeule()
        {
            _TextBoxLibelle.IsReadOnly = false;
            _TextBoxDescription.IsReadOnly = false;
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
