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
using SitaffRibbon.Windows;
/* 
 * Using pour utilisation des IObservableCollection (afin d'éviter de mettre
 * System.Collections.ObjectModel.IObservableCollection en entier)
 */
using System.Collections.ObjectModel;
using System.Threading;


namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeAppelOffresControl.xaml
    /// </summary>
    public partial class ListeAppelOffresControl : UserControl
    {
        public ListeAppelOffresControl()
        {
            InitializeComponent();
            this._filterZone.Height = 21;
            this.AppelOffres = new ObservableCollection<Appel_Offre>(((App)App.Current).mySitaffEntities.Appel_Offre.OrderBy(apo => apo.Identifiant));
            this.creationMenuClicDroit();
        }


        #region Propriétés de dépendances


        public ObservableCollection<Appel_Offre> AppelOffres
        {
            get { return (ObservableCollection<Appel_Offre>)GetValue(AppelOffresProperty); }
            set { SetValue(AppelOffresProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AppelOffres.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AppelOffresProperty =
            DependencyProperty.Register("AppelOffres", typeof(ObservableCollection<Appel_Offre>), typeof(ListeAppelOffresControl), new UIPropertyMetadata(null));

        
        #endregion


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((MainWindow)((Grid)((Border)this.Parent).Parent).Parent).progressBarMainWindow.IsIndeterminate = false;
            ((MainWindow)((Grid)((Border)this.Parent).Parent).Parent).textBlockMainWindow.Text = "Chargement des appel d'offres réussi.";
            ((MainWindow)((Grid)((Border)this.Parent).Parent).Parent).stopThread();
        }


        #region CRUD (Create Read Update Delete)

        public Appel_Offre Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    AppelOffreWindow appelOffreWindow = new AppelOffreWindow();
                    appelOffreWindow.DataContext = new Appel_Offre();
                    appelOffreWindow.entrepriseList = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(ent => ent.Nom));
                    appelOffreWindow.DataContext = this._DataGridMain.SelectedItem;
                    bool? dialogResult = appelOffreWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Appel_Offre)appelOffreWindow.DataContext;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule appel d'offre.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public Appel_Offre Look()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    AppelOffreWindow appelOffreWindow = new AppelOffreWindow();
                    appelOffreWindow.DataContext = new Appel_Offre();
                    appelOffreWindow.entrepriseList = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(ent => ent.Nom));
                    appelOffreWindow.DataContext = this._DataGridMain.SelectedItem;

                    appelOffreWindow.lectureSeule();

                    bool? dialogResult = appelOffreWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Appel_Offre)appelOffreWindow.DataContext;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule appel d'offre.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region clic droit

        private void creationMenuClicDroit()
        {
            ContextMenu contextMenu = new ContextMenu();
            this._DataGridMain.ContextMenu = contextMenu;

            MenuItem itemAfficher = new MenuItem();
            itemAfficher.Header = "Afficher";
            itemAfficher.Click += new RoutedEventHandler(delegate { this.menuLook(); });

            MenuItem itemAfficher2 = new MenuItem();
            itemAfficher2.Header = "Ajouter";
            itemAfficher2.Click += new RoutedEventHandler(delegate { this.menuAdd(); });

            MenuItem itemAfficher3 = new MenuItem();
            itemAfficher3.Header = "Modifier";
            itemAfficher3.Click += new RoutedEventHandler(delegate { this.menuUpdate(); });

            MenuItem itemAfficher4 = new MenuItem();
            itemAfficher4.Header = "Supprimer";
            itemAfficher4.Click += new RoutedEventHandler(delegate { this.menuDelete(); });


            contextMenu.Items.Add(itemAfficher);
            contextMenu.Items.Add(itemAfficher2);
            contextMenu.Items.Add(itemAfficher3);
            contextMenu.Items.Add(itemAfficher4);
        }

        private void menuLook()
        {
            ((App)App.Current)._theMainWindow._CommandLook.Command.Execute(((App)App.Current)._theMainWindow);
        }

        private void menuAdd()
        {
            ((App)App.Current)._theMainWindow._CommandAdd.Command.Execute(((App)App.Current)._theMainWindow);
        }

        private void menuUpdate()
        {
            ((App)App.Current)._theMainWindow._CommandUpdate.Command.Execute(((App)App.Current)._theMainWindow);
        }

        private void menuDelete()
        {
            ((App)App.Current)._theMainWindow._CommandDelete.Command.Execute(((App)App.Current)._theMainWindow);
        }

        private void menuAddTime()
        {
            ((App)App.Current)._theMainWindow._CommandAddTime.Command.Execute(((App)App.Current)._theMainWindow);
        }

        #endregion

        private void _DataGridMain_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((App)App.Current)._theMainWindow._CommandLook.Command.Execute(((App)App.Current)._theMainWindow);
        }
    }
}
