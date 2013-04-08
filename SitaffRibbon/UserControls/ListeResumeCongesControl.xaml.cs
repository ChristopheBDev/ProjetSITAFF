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
using System.Collections.ObjectModel;
using SitaffRibbon.Windows;
using System.Threading;
using SitaffRibbon.Classes;

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeResumeCongesControl.xaml
    /// </summary>
    public partial class ListeResumeCongesControl : UserControl
    {

        #region Propriétés de dépendances


        public ObservableCollection<Conge> listConge
        {
            get { return (ObservableCollection<Conge>)GetValue(listCongeProperty); }
            set { SetValue(listCongeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listConge.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listCongeProperty =
            DependencyProperty.Register("listConge", typeof(ObservableCollection<Conge>), typeof(ListeResumeCongesControl), new UIPropertyMetadata(null));

        

        #endregion

        #region constructeur

        public ListeResumeCongesControl()
        {
            InitializeComponent();
            this._filterZone.Height = 21;
            this.listConge = new ObservableCollection<Conge>(((App)App.Current).mySitaffEntities.Conge.Where(con => con.Salarie1.Identifiant == ((App)App.Current)._connectedUser.Salarie_Interne1.Salarie.Identifiant).OrderByDescending(cong => cong.Demande_Fait_Le).ThenByDescending(cong => cong.Date_Debut));
            this.creationMenuClicDroit();
        }

        #endregion

        public void MiseTotauxAnnee()
        {

        }

        #region evenement

        #region bouton click

        /// <summary>
        /// Augmente la taille de la police
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ButtonPlusPolice_Click(object sender, RoutedEventArgs e)
        {
            this._DataGridMain.FontSize = this._DataGridMain.FontSize + 0.5;
            this._textBlockTaillePolice.Text = "Taille police (" + this._DataGridMain.FontSize.ToString() + ") :     ";
        }

        /// <summary>
        /// Rappetice la taille de la police
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ButtonMoinsPolice_Click(object sender, RoutedEventArgs e)
        {
            if (this._DataGridMain.FontSize > 0.5)
            {
                this._DataGridMain.FontSize = this._DataGridMain.FontSize - 0.5;
                this._textBlockTaillePolice.Text = "Taille police (" + this._DataGridMain.FontSize.ToString() + ") :     ";
            }
        }

        #endregion

        #region double click

        /// <summary>
        /// Double click sur une ligne du datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _DataGridMain_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((App)App.Current)._theMainWindow._CommandLook.Command.Execute(((App)App.Current)._theMainWindow);
        }

        #endregion

        #endregion

        #region fenetre chargé

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((MainWindow)((Grid)((Border)this.Parent).Parent).Parent).progressBarMainWindow.IsIndeterminate = false;
            ((MainWindow)((Grid)((Border)this.Parent).Parent).Parent).textBlockMainWindow.Text = "Chargement des congés réussi.";
            ((MainWindow)((Grid)((Border)this.Parent).Parent).Parent).stopThread();
        }

        #endregion

        #region fonctions

        /// <summary>
        /// Ouvre le rapport congé séléctionné
        /// </summary>
        public Conge RapportImprimer()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    ReportingWindow reportingWindow = new ReportingWindow();
                    long toShow = ((Conge)this._DataGridMain.SelectedItem).Identifiant;
                    reportingWindow._webBrowser.Source = new Uri("http://srv-sql/ReportServer/Pages/ReportViewer.aspx?%2fCONGES&rs:Command=Render&ReportParameter1=" + toShow);
                    reportingWindow.Title = "Rapport pour impression : congé de - " + ((Conge)this._DataGridMain.SelectedItem).Salarie1.Personne.fullname + "-";

                    bool? dialogResult = reportingWindow.ShowDialog();
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul congé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un congé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        #endregion

        #region clic droit

        private void creationMenuClicDroit()
        {
            ContextMenu contextMenu = new ContextMenu();
            this._DataGridMain.ContextMenu = contextMenu;

            MenuItem itemAfficher6 = new MenuItem();
            itemAfficher6.Header = "Imprimer";
            itemAfficher6.Click += new RoutedEventHandler(delegate { this.ImprimerConge(); });

            contextMenu.Items.Add(itemAfficher6);
        }
        private void ImprimerConge()
        {
            this.RapportImprimer();
        }

        #endregion

        #region Commandes

        #region Plus

        private void _CommandPlus_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._DataGridMain.FontSize = this._DataGridMain.FontSize + 0.5;
            this._textBlockTaillePolice.Text = "Taille police (" + this._DataGridMain.FontSize.ToString() + ") :     ";
        }

        private void _CommandPlus_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region Moins

        private void _CommandMoins_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this._DataGridMain.FontSize > 0.5)
            {
                this._DataGridMain.FontSize = this._DataGridMain.FontSize - 0.5;
            }
            this._textBlockTaillePolice.Text = "Taille police (" + this._DataGridMain.FontSize.ToString() + ") :     ";
        }

        private void _CommandMoins_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion


        #endregion
    }
}
