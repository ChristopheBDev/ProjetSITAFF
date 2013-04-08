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
    /// Logique d'interaction pour ParametreRessourcesControl.xaml
    /// </summary>
    public partial class ParametreRessourcesControl : UserControl
    {
        public ParametreRessourcesControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((ParametresMain)((Grid)((Border)this.Parent).Parent).Parent).progressBarMainWindow.IsIndeterminate = false;
            ((ParametresMain)((Grid)((Border)this.Parent).Parent).Parent).textBlockMainWindow.Text = "Chargement des ressources réussi.";
            ((ParametresMain)((Grid)((Border)this.Parent).Parent).Parent).stopThread();
        }

        #region propiété de dépendance


        public ObservableCollection<Ressources> mesRessources
        {
            get { return (ObservableCollection<Ressources>)GetValue(mesRessourcesProperty); }
            set { SetValue(mesRessourcesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for mesRessources.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty mesRessourcesProperty =
            DependencyProperty.Register("mesRessources", typeof(ObservableCollection<Ressources>), typeof(ParametreRessourcesControl), new UIPropertyMetadata(null));

        
        #endregion


        #region CRUD (Create Read Update Delete)


        /// <summary>
        /// Ajoute un nouvelle ressource à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Ressources Add()
        {
            RessourcesWindow ressourceswindow = new RessourcesWindow();
            ressourceswindow.DataContext = new Ressources();
            //booléen nullable vrai ou faux ou null

            bool? dialogResult = ressourceswindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                return (Ressources)ressourceswindow.DataContext;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Ouvre la ressource séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Ressources Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    RessourcesWindow ressourceswindow = new RessourcesWindow();
                    ressourceswindow.DataContext = new Ressources();
                    ressourceswindow.DataContext = (Ressources)this._DataGridMain.SelectedItem;

                    bool? dialogResult = ressourceswindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Ressources)ressourceswindow.DataContext;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule ressource.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Supprime la ressource séléctionnée avec une confirmation
        /// </summary>
        public Ressources Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    if (MessageBox.Show("Voulez-vous rééllement supprimer la ressource séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //Supprimer l'élément 
                        return (Ressources)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule ressource.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Ouvre la ressource séléctionné à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Ressources Look()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    RessourcesWindow ressourceswindow = new RessourcesWindow();
                    ressourceswindow.DataContext = new Ressources();
                    ressourceswindow.DataContext = (Ressources)this._DataGridMain.SelectedItem;

                    ressourceswindow.lectureSeule();

                    bool? dialogResult = ressourceswindow.ShowDialog();


                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Ressources)ressourceswindow.DataContext;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule ressource.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

    }
}
