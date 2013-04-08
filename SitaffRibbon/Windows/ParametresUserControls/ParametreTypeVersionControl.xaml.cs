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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SitaffRibbon.Windows.ParametresWindows;
/* 
 * Using pour utilisation des IObservableCollection (afin d'éviter de mettre
 * System.Collections.ObjectModel.IObservableCollection en entier)
 */
using System.Collections.ObjectModel;
using System.Threading;

namespace SitaffRibbon.Windows.ParametresUserControls
{
    /// <summary>
    /// Logique d'interaction pour ParametreCiviliteControl.xaml
    /// </summary>
    public partial class ParametreTypeVersionControl : UserControl
    {


        #region propriété de dépendance
        

        #endregion

        public ParametreTypeVersionControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((ParametresMain)((Grid)((Border)this.Parent).Parent).Parent).progressBarMainWindow.IsIndeterminate = false;
            ((ParametresMain)((Grid)((Border)this.Parent).Parent).Parent).textBlockMainWindow.Text = "Chargement des civilités réussi.";
            ((ParametresMain)((Grid)((Border)this.Parent).Parent).Parent).stopThread();
        }

        #region CRUD (Create Read Update Delete)

        /// <summary>
        /// Ajoute une nouvelle civilité à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Civilite Add()
        {
            CiviliteWindow civilitewindow = new CiviliteWindow();
            civilitewindow.DataContext = new Civilite();

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = civilitewindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                return (Civilite)civilitewindow.DataContext;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Ouvre la civilité séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Civilite Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    CiviliteWindow civilitewindow = new CiviliteWindow();
                    civilitewindow.DataContext = new Civilite();
                    civilitewindow.DataContext = (Civilite)this._DataGridMain.SelectedItem;
                    bool? dialogResult = civilitewindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Civilite)civilitewindow.DataContext;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule civilité.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous  devez sélectionner une seule civilité.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;

            }
        }

        /// <summary>
        /// Supprime la civilité séléctionnéé avec une confirmation
        /// </summary>
        public Civilite Remove()
        {

            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    if (MessageBox.Show("Voulez-vous rééllement supprimer la civilité séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //Supprimer l'élément 
                        return (Civilite)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule civilité.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous  devez sélectionner une seule civilité.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre la civilité séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public void Look()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    CiviliteWindow civilitewindow = new CiviliteWindow();
                    civilitewindow.DataContext = new Civilite();
                    civilitewindow.DataContext = (Civilite)this._DataGridMain.SelectedItem;

                    civilitewindow.lectureSeule();

                    bool? dialogResult = civilitewindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule version type.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Vous  devez sélectionner une seule version type.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion


    }
}
