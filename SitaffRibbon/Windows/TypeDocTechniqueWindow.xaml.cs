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
    /// Logique d'interaction pour TypeDocTechniqueWindow.xaml
    /// </summary>
    public partial class TypeDocTechniqueWindow : Window
    {
        public TypeDocTechniqueWindow()
        {
            InitializeComponent();
            this.listTypeCommande = new ObservableCollection<Type_Commande>(((App)App.Current).mySitaffEntities.Type_Commande.OrderBy(typc => typc.Libelle));
        }

        #region proprietes de dependance

        public ObservableCollection<Type_Commande> listTypeCommande
        {
            get { return (ObservableCollection<Type_Commande>)GetValue(listTypeCommandeProperty); }
            set { SetValue(listTypeCommandeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntreprise_Mere.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listTypeCommandeProperty =
            DependencyProperty.Register("listTypeCommande", typeof(ObservableCollection<Type_Commande>), typeof(TypeDocTechniqueWindow), new UIPropertyMetadata(null));

        #endregion

        #region Verifications

        private bool Verif_Generale()
        {
            bool verif = true;

            if (!this.Verif_ComboBoxDocTechnique())
            {
                verif = false;
            }
            if (!this.Verif_TextBoxDescription())
            {
                verif = false;
            }

            return verif;
        }

        #region Verif Doc technique

        private bool Verif_ComboBoxDocTechnique()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxDocTechnique, this._TextBlockDocTechnique);
        }

        private void _ComboBoxDocTechnique_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxDocTechnique();
            if (this._ComboBoxDocTechnique.SelectedItem != null)
            {
                if (((Affaire_Type_Commande)this.DataContext).Description != null)
                {
                    if (((Affaire_Type_Commande)this.DataContext).Description.Length == 0)
                    {
                        ((Affaire_Type_Commande)this.DataContext).Description = ((Type_Commande)this._ComboBoxDocTechnique.SelectedItem).Description;
                        this._TextBoxDescription.Text = ((Type_Commande)this._ComboBoxDocTechnique.SelectedItem).Description;
                    }
                    else
                    {
                        this._TextBoxDescription.Text = ((Affaire_Type_Commande)this.DataContext).Description;
                    }
                }
                else
                {
                    ((Affaire_Type_Commande)this.DataContext).Description = ((Type_Commande)this._ComboBoxDocTechnique.SelectedItem).Description;
                    this._TextBoxDescription.Text = ((Type_Commande)this._ComboBoxDocTechnique.SelectedItem).Description;
                }
            }
        }

        #endregion

        #region Verif Description

        private bool Verif_TextBoxDescription()
        {
			return ((App)App.Current).verifications.TextBoxObligatoire(this._TextBoxDescription, this._TextBlockDescription);
        }

        private void _TextBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxDescription();
            ((Affaire_Type_Commande)this.DataContext).Description = this._TextBoxDescription.Text;
        }

        #endregion

        #endregion

        #region lecture seul

        public void LectureSeule()
        {
            _ComboBoxDocTechnique.IsEnabled = false;
            _TextBoxDescription.IsEnabled = false;
        }

        #endregion

        #region Boutons ok et cancel

        /// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.Verif_Generale())
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }
    }
}
