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
    /// Logique d'interaction pour SalleWindow.xaml
    /// </summary>
    public partial class SalleWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region Constructeur

        public SalleWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxLibelle.Focus();
        }

        #region Initialisation

        private void initialisationPropDependance()
        {
            this.listEntrepriseMere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(ent => ent.Nom));
        }

        #endregion

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
                if (((App)App.Current).mySitaffEntities.Salle.Where(act => act.Identifiant != ((Salle)this.DataContext).Identifiant).
                    Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxLibelle.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Une salle est déjà présente avec ce libellé", "Doublon de salle", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

        #region Proprietés de dependances



        public ObservableCollection<Entreprise_Mere> listEntrepriseMere
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntrepriseMereProperty); }
            set { SetValue(listEntrepriseMereProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntrepriseMere.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseMereProperty =
            DependencyProperty.Register("listEntrepriseMere", typeof(ObservableCollection<Entreprise_Mere>), typeof(SalleWindow), new UIPropertyMetadata(null));

        

        #endregion

        #region Verifications

        /// <summary>
        /// Verifie si tous les champs sont bien renseignés.
        /// </summary>
        /// <returns>booléen vrai si tous les champs sont bien renseignés, sinon retourne faux</returns>
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!verif_tab_salle())
            {
                verif = false;
            }


            return verif;
        }
        #region Tab salle
        private bool verif_tab_salle()
        {
            bool test = true;

            if (!Verif_TextBoxLibelle())
            {
                test = false;
            }
            if (!Verif_TextBoxNbPlace())
            {
                test = false;
            }
            if (!Verif_ComboBoxEntrepriseMere())
            {
                test = false;
            }

            return test;
        }
        #endregion

        #region Libelle
        private bool Verif_TextBoxLibelle()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxLibelle, this._TextBlockLibelle);
        }


        private void _TextBoxLibelle_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelle();
        }
        #endregion

        #region Nombre de place

        private bool Verif_TextBoxNbPlace()
        {
            return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._TextBoxNbPlace, this._TextBlockNbPlace);
        }

        private void _TextBoxNbPlace_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxNbPlace();
        }

        #endregion

        #region Entreprise

        private bool Verif_ComboBoxEntrepriseMere()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxEntrepriseMere, this._TextBlockEntrepriseMere);
        }

        private void _ComboBoxEntrepriseMere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxEntrepriseMere();
        }

        #endregion

        #endregion

        #region lecture seule

        public void lectureSeule()
        {
            _TextBoxLibelle.IsReadOnly = false;
            _ComboBoxEntrepriseMere.IsEnabled = false;
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
