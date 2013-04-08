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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour CompetenceWindow.xaml
    /// </summary>
    public partial class CompetenceWindow : Window
    {
        public CompetenceWindow()
        {
            InitializeComponent();
        }

        #region Propriétés de dépendances



        public ObservableCollection<Competence> listCompetences
        {
            get { return (ObservableCollection<Competence>)GetValue(listCompetencesProperty); }
            set { SetValue(listCompetencesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listCompetences.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listCompetencesProperty =
            DependencyProperty.Register("listCompetences", typeof(ObservableCollection<Competence>), typeof(CompetenceWindow), new UIPropertyMetadata(null));

        

        #endregion

        #region Boutons ok et cancel

        /// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.Verif_ComboBoxSalarieFormation())
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

        #region Verifications

        private bool Verif_ComboBoxSalarieFormation()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxSalarieFormation, this._TextBlockCompetence);
        }

        private void _ComboBoxSalarieFormation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Verif_ComboBoxSalarieFormation())
            {
                ((Interimaire_Competence)this.DataContext).Competence1 = (Competence)this._ComboBoxSalarieFormation.SelectedItem;
            }
        }

        #endregion


    }
}
