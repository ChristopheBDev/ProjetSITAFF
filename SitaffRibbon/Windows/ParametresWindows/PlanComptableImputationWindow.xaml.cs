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
    /// Logique d'interaction pour PlanComptableImputationWindow.xaml
    /// </summary>
    public partial class PlanComptableImputationWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region proprieté de dependance

        public ObservableCollection<Entreprise_Mere> listEntreprise_Mere
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntreprise_MereProperty); }
            set { SetValue(listEntreprise_MereProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntreprise_Mere.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntreprise_MereProperty =
            DependencyProperty.Register("listEntreprise_Mere", typeof(ObservableCollection<Entreprise_Mere>), typeof(PlanComptableImputationWindow), new PropertyMetadata(null));

        #endregion

        #region constructeur

        public PlanComptableImputationWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._TextBoxNumero.Focus();
        }

        #region initialisation

        private void initialisationPropDependance()
        {

        }

        #endregion

        #endregion

        #region Verifications
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_TextBoxNumero())
            {
                verif = false;
            }
            if (!Verif_TextBoxLibelle())
            {
                verif = false;
            }

            return verif;
        }


        #region numero

        private bool Verif_TextBoxNumero()
        {
            return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._TextBoxNumero, this._TextBlockNumero);
        }

        private void _TextBoxNumero_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxNumero();
        }

        #endregion

        #region libelle

        private bool Verif_TextBoxLibelle()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxLibelle, _TextBlockLibelle);
        }

        private void _TextBoxLibelle_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxLibelle();
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
                if (((App)App.Current).mySitaffEntities.Plan_Comptable_Imputation.Where(act => act.Identifiant != ((Plan_Comptable_Imputation)this.DataContext).Identifiant).Where(lib => lib.Libelle.Trim().ToLower() == this._TextBoxLibelle.Text.Trim().ToLower()).Count() == 0)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Un plan comptable imputation est déjà présent avec ce libellé", "Doublon de plan comptable imputation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

        private void _buttonAjouterEntMere_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._listBoxEntrepriseMere.SelectedItem != null)
            {
                ObservableCollection<Entreprise_Mere> toAdd = new ObservableCollection<Entreprise_Mere>();
                foreach (Entreprise_Mere item in this._listBoxEntrepriseMere.SelectedItems.OfType<Entreprise_Mere>())
                {
                    toAdd.Add(item);
                }
                foreach (Entreprise_Mere item in toAdd)
                {
                    ((Plan_Comptable_Imputation)this.DataContext).Entreprise_Mere.Add(item);
                    this.listEntreprise_Mere.Remove(item);
                }
            }
        }

        private void _buttonSupprimerEntMere_Click_1(object sender, RoutedEventArgs e)
        {
            if (this._listBoxEntrepriseMereAssociees.SelectedItem != null)
            {
                ObservableCollection<Entreprise_Mere> toRemove = new ObservableCollection<Entreprise_Mere>();
                foreach (Entreprise_Mere item in this._listBoxEntrepriseMereAssociees.SelectedItems.OfType<Entreprise_Mere>())
                {
                    toRemove.Add(item);
                }
                foreach (Entreprise_Mere item in toRemove)
                {
                    ((Plan_Comptable_Imputation)this.DataContext).Entreprise_Mere.Remove(item);
                    this.listEntreprise_Mere.Add(item);
                }
            }
        }

        #endregion

        #region lecture seule

        public void lectureSeule()
        {
            _TextBoxLibelle.IsReadOnly = false;
            _TextBoxNumero.IsReadOnly = false;
            _buttonAjouterEntMere.IsEnabled = false;
            _buttonSupprimerEntMere.IsEnabled = false;
        }

        #endregion

        #region fenetre chargé

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;

            this.listEntreprise_Mere = new ObservableCollection<Entreprise_Mere>();

            foreach (Entreprise_Mere item in ((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(em => em.Nom))
            {
                if (!((Plan_Comptable_Imputation)this.DataContext).Entreprise_Mere.Contains(item))
                {
                    this.listEntreprise_Mere.Add(item);
                }
            }
        }

        #endregion
    }
}
