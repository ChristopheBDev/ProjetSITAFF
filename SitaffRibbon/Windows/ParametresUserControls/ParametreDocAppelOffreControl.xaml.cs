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
    /// Logique d'interaction pour ParametreDocAppelOffreControl.xaml
    /// </summary>
    public partial class ParametreDocAppelOffreControl : UserControl
    {
        public ParametreDocAppelOffreControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((ParametresMain)((Grid)((Border)this.Parent).Parent).Parent).progressBarMainWindow.IsIndeterminate = false;
            ((ParametresMain)((Grid)((Border)this.Parent).Parent).Parent).textBlockMainWindow.Text = "Chargement des documents d'appels d'offres réussi.";
            ((ParametresMain)((Grid)((Border)this.Parent).Parent).Parent).stopThread();
        }


        #region propiétés de dépendance

        #region mesDocsAppelsOffres
        public ObservableCollection<Document_Appel_Offre> mesDocsAppelsOffres
        {
            get { return (ObservableCollection<Document_Appel_Offre>)GetValue(mesDocsAppelsOffresProperty); }
            set { SetValue(mesDocsAppelsOffresProperty, value); }
        }

        // Using a DependencyProperty as the backing store for mesDocsAppelsOffres.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty mesDocsAppelsOffresProperty =
            DependencyProperty.Register("mesDocsAppelsOffres", typeof(ObservableCollection<Document_Appel_Offre>), typeof(ParametreDocAppelOffreControl), new UIPropertyMetadata(null));
        

        #endregion

        #endregion


        #region CRUD (Create Read Update Delete)


        /// <summary>
        /// Ajoute un nouveau document d'appel d'ofre à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Document_Appel_Offre Add()
        {
            DocAppelOffreWindow docappeloffrewindow = new DocAppelOffreWindow();
            docappeloffrewindow.DataContext = new Document_Appel_Offre();
            docappeloffrewindow.mesAppelsOffres = new ObservableCollection<Appel_Offre>(((App)App.Current).mySitaffEntities.Appel_Offre.OrderBy(appeloffre => appeloffre.Reference));
            
            //booléen nullable vrai ou faux ou null

            bool? dialogResult = docappeloffrewindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                return (Document_Appel_Offre)docappeloffrewindow.DataContext;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Ouvre du document d'appel d'offre séléctionné à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Document_Appel_Offre Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    DocAppelOffreWindow docappeloffrewindow = new DocAppelOffreWindow();
                    docappeloffrewindow.DataContext = new Document_Appel_Offre();
                    docappeloffrewindow.DataContext = (Document_Appel_Offre)this._DataGridMain.SelectedItem;
                    docappeloffrewindow.mesAppelsOffres = new ObservableCollection<Appel_Offre>(((App)App.Current).mySitaffEntities.Appel_Offre.OrderBy(appeloffre => appeloffre.Reference));
            
                    bool? dialogResult = docappeloffrewindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Document_Appel_Offre)docappeloffrewindow.DataContext;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul document d'appel offres.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Supprime du document d'appel d'offre séléctionné avec une confirmation
        /// </summary>
        public Document_Appel_Offre Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    if (MessageBox.Show("Voulez-vous rééllement supprimer du document d'appel d'offre séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //Supprimer l'élément 
                        return (Document_Appel_Offre)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul document type salarié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Ouvre du document d'appel d'offre séléctionné à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Document_Appel_Offre Look()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    DocAppelOffreWindow docappeloffrewindow = new DocAppelOffreWindow();
                    docappeloffrewindow.DataContext = new Document_Appel_Offre();
                    docappeloffrewindow.DataContext = (Document_Appel_Offre)this._DataGridMain.SelectedItem;
                    docappeloffrewindow.mesAppelsOffres = new ObservableCollection<Appel_Offre>(((App)App.Current).mySitaffEntities.Appel_Offre.OrderBy(appeloffre => appeloffre.Reference));
            
                    docappeloffrewindow.lectureSeule();

                    bool? dialogResult = docappeloffrewindow.ShowDialog();


                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Document_Appel_Offre)docappeloffrewindow.DataContext;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul document d'appel d'offre salarié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
