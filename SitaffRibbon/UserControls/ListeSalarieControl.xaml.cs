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
//using System.Windows.Forms;

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeSalarieControl.xaml
    /// </summary>
    public partial class ListeSalarieControl : UserControl
    {

        /// <summary>
        /// Fin du chargement de la fenêtre (pour fermer la fenêtre de chargement)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((MainWindow)((Grid)((Border)this.Parent).Parent).Parent).progressBarMainWindow.IsIndeterminate = false;
            ((MainWindow)((Grid)((Border)this.Parent).Parent).Parent).textBlockMainWindow.Text = "Chargement des salariés réussi.";
            ((MainWindow)((Grid)((Border)this.Parent).Parent).Parent).stopThread();
        }

        #region Propriétés de dépendances

        public ObservableCollection<Entreprise_Mere> listSocieteMere
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(listSocieteMereProperty); }
            set { SetValue(listSocieteMereProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSocieteMereProperty =
            DependencyProperty.Register("listSocieteMere", typeof(ObservableCollection<Entreprise_Mere>), typeof(ListeSalarieControl), new UIPropertyMetadata(null));

        public ObservableCollection<Personne> listSalarie
        {
            get { return (ObservableCollection<Personne>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Personne>), typeof(ListeSalarieControl), new UIPropertyMetadata(null));

        #endregion

        public ListeSalarieControl()
        {            
            InitializeComponent();
            this._filterZone.Height = 21;
            this.listSocieteMere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(nom => nom.Nom));
            this.listSalarie = new ObservableCollection<Personne>(((App)App.Current).mySitaffEntities.Personne.Where(p => p.Salarie != null).OrderBy(p => p.Nom));
            ObservableCollection<TypeDeSalarie> listTypeSalarie = new ObservableCollection<TypeDeSalarie>();
            listTypeSalarie.Add(new TypeDeSalarie("Salarié interne"));
            listTypeSalarie.Add(new TypeDeSalarie("Tiers"));
            listTypeSalarie.Add(new TypeDeSalarie("Interimaire"));
            this._filterContainTypeSalarie.ItemsSource = listTypeSalarie;
            this.creationMenuClicDroit();
        }

        #region CRUD (Create Read Update Delete)
        /// <summary>
        /// Ajoute un nouveau salarié à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Personne Add()
        {
            SalarieWindow salarieWindow = new SalarieWindow();
            salarieWindow.creation = true;
            Personne tmp = new Personne();
            tmp.Salarie = new Salarie();
            tmp.Salarie.Salarie_Interne = new Salarie_Interne();
            tmp.Salarie.Tiers = new Tiers();
            tmp.Salarie.Interimaire = new Interimaire();
            tmp.Adresse1 = new Adresse();
            tmp.Adresse2 = new Adresse();
            salarieWindow.ListAgences = new ObservableCollection<Agence_Interimaire>(((App)App.Current).mySitaffEntities.Agence_Interimaire.OrderBy(ai => ai.Fournisseur.Entreprise.Libelle));
            salarieWindow.VilleListPerso = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));
            salarieWindow.PaysListPerso = new ObservableCollection<Pays>(((App)App.Current).mySitaffEntities.Pays.OrderBy(pay => pay.Libelle));
            salarieWindow.VilleListPro = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));
            salarieWindow.PaysListPro = new ObservableCollection<Pays>(((App)App.Current).mySitaffEntities.Pays.OrderBy(pay => pay.Libelle));
            salarieWindow.ListCivilite = new ObservableCollection<Civilite>(((App)App.Current).mySitaffEntities.Civilite.OrderBy(civ => civ.Libelle_Long));
            salarieWindow.ListEntreprise = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.Where(ent => ent.Fournisseur != null).Where(ent => ent.Fournisseur.Sous_Traitant != null || ent.Fournisseur.Agence_Interimaire != null).OrderBy(ent => ent.Libelle));
            salarieWindow.ListGroupe = new ObservableCollection<Groupe>(((App)App.Current).mySitaffEntities.Groupe.OrderBy(grp => grp.Libelle));
            salarieWindow.listEntreprise_Mere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(em => em.Nom));
            salarieWindow.ListContrats = new ObservableCollection<Contrat>(((App)App.Current).mySitaffEntities.Contrat.OrderBy(ct => ct.Libelle));
            salarieWindow.listOutillage = new ObservableCollection<Outillage>(((App)App.Current).mySitaffEntities.Outillage.OrderBy(ou => ou.Libelle));
            salarieWindow.DataContext = tmp;
            salarieWindow.bloquerSalarieInterne();
            salarieWindow.bloquerSalarieInterimaire();
            salarieWindow.bloquerSalarieTiers();

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = salarieWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                return (Personne)salarieWindow.DataContext;
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach((Personne)salarieWindow.DataContext);
                }
                catch (Exception)
                {
                }                       
                return null;
            }
        }

        /// <summary>
        /// Ouvre le salarié séléctionné à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Personne Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    SalarieWindow salarieWindow = new SalarieWindow();
                    Personne tmp = new Personne();
                    tmp = (Personne)this._DataGridMain.SelectedItem;
                    salarieWindow.ListAgences = new ObservableCollection<Agence_Interimaire>(((App)App.Current).mySitaffEntities.Agence_Interimaire.OrderBy(ai => ai.Fournisseur.Entreprise.Libelle));
                    salarieWindow.VilleListPerso = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));
                    salarieWindow.PaysListPerso = new ObservableCollection<Pays>(((App)App.Current).mySitaffEntities.Pays.OrderBy(pay => pay.Libelle));
                    salarieWindow.VilleListPro = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));
                    salarieWindow.PaysListPro = new ObservableCollection<Pays>(((App)App.Current).mySitaffEntities.Pays.OrderBy(pay => pay.Libelle));
                    salarieWindow.ListCivilite = new ObservableCollection<Civilite>(((App)App.Current).mySitaffEntities.Civilite.OrderBy(civ => civ.Libelle_Long));
                    salarieWindow.ListEntreprise = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.Where(ent => ent.Fournisseur != null).Where(ent => ent.Fournisseur.Sous_Traitant != null || ent.Fournisseur.Agence_Interimaire != null).OrderBy(ent => ent.Libelle));
                    salarieWindow.ListGroupe = new ObservableCollection<Groupe>(((App)App.Current).mySitaffEntities.Groupe.OrderBy(grp => grp.Libelle));
                    salarieWindow.listEntreprise_Mere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(em => em.Nom));
                    salarieWindow.ListContrats = new ObservableCollection<Contrat>(((App)App.Current).mySitaffEntities.Contrat.OrderBy(ct => ct.Libelle));
                    salarieWindow.listOutillage = new ObservableCollection<Outillage>(((App)App.Current).mySitaffEntities.Outillage.OrderBy(ou => ou.Libelle));
                    foreach (Salarie_Outillage so in tmp.Salarie.Salarie_Outillage)
                    {
                        salarieWindow.listOutillage.Remove(so.Outillage1);
                    }
                    if (((Personne)this._DataGridMain.SelectedItem).Salarie.Salarie_Interne == null)
                    {
                        salarieWindow.bloquerSalarieInterne();
                        tmp.Salarie.Salarie_Interne = new Salarie_Interne();
                    }
                    else
                    {
                        salarieWindow.checkBox_SalarieInterne.IsChecked = true;
                    }
                    if (((Personne)this._DataGridMain.SelectedItem).Salarie.Interimaire == null)
                    {
                        salarieWindow.bloquerSalarieInterimaire();
                        tmp.Salarie.Interimaire = new Interimaire();
                    }
                    else
                    {
                        salarieWindow.checkBox_SalarieInterimaire.IsChecked = true;
                    }
                    if (((Personne)this._DataGridMain.SelectedItem).Salarie.Tiers == null)
                    {
                        salarieWindow.bloquerSalarieTiers();
                        tmp.Salarie.Tiers = new Tiers();
                    }
                    else
                    {
                        salarieWindow.checkBox_SalarieTiers.IsChecked = true;
                    }
                    if (tmp.Adresse1 == null)
                    {
                        tmp.Adresse1 = new Adresse();
                    }
                    if (tmp.Adresse2 == null)
                    {
                        tmp.Adresse2 = new Adresse();
                    }
                    salarieWindow.DataContext = tmp;

                    bool? dialogResult = salarieWindow.ShowDialog();
                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Personne)salarieWindow.DataContext;
                    }
                    else
                    {
                        return null;
                    }


                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul salarié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un salarié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime le salarié séléctionné avec une confirmation
        /// </summary>
        public Personne Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    if (MessageBox.Show("Voulez-vous rééllement supprimer le salarié séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //Supprimer l'élément 
                        return (Personne)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seule salarié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un salarié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre le salarié séléctionné en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Personne Look(Personne salarieToLook)
        {
            if (this._DataGridMain.SelectedItem != null || salarieToLook != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1 || salarieToLook != null)
                {
                    SalarieWindow salarieWindow = new SalarieWindow();
                    salarieWindow.DataContext = new Personne();
                    salarieWindow.ListAgences = new ObservableCollection<Agence_Interimaire>(((App)App.Current).mySitaffEntities.Agence_Interimaire.OrderBy(ai => ai.Fournisseur.Entreprise.Libelle));
                    salarieWindow.VilleListPerso = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));
                    salarieWindow.PaysListPerso = new ObservableCollection<Pays>(((App)App.Current).mySitaffEntities.Pays.OrderBy(pay => pay.Libelle));
                    salarieWindow.VilleListPro = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));
                    salarieWindow.PaysListPro = new ObservableCollection<Pays>(((App)App.Current).mySitaffEntities.Pays.OrderBy(pay => pay.Libelle));
                    salarieWindow.ListCivilite = new ObservableCollection<Civilite>(((App)App.Current).mySitaffEntities.Civilite.OrderBy(civ => civ.Libelle_Long));
                    salarieWindow.ListEntreprise = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.OrderBy(ent => ent.Libelle));
                    salarieWindow.ListGroupe = new ObservableCollection<Groupe>(((App)App.Current).mySitaffEntities.Groupe.OrderBy(grp => grp.Libelle));
                    salarieWindow.listEntreprise_Mere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(em => em.Nom));
                    salarieWindow.ListContrats = new ObservableCollection<Contrat>(((App)App.Current).mySitaffEntities.Contrat.OrderBy(ct => ct.Libelle));
                    salarieWindow.listOutillage = new ObservableCollection<Outillage>(((App)App.Current).mySitaffEntities.Outillage.OrderBy(ou => ou.Libelle));
                    
                    if (salarieToLook != null)
                    {
                        foreach (Salarie_Outillage so in salarieToLook.Salarie.Salarie_Outillage)
                        {
                            salarieWindow.listOutillage.Remove(so.Outillage1);
                        }
                        salarieWindow.DataContext = salarieToLook;
                        if (salarieToLook.Salarie.Salarie_Interne != null)
                        {
                            salarieWindow.checkBox_SalarieInterne.IsChecked = true;
                        }
                        if (salarieToLook.Salarie.Interimaire != null)
                        {
                            salarieWindow.checkBox_SalarieInterimaire.IsChecked = true;
                        }
                        if (salarieToLook.Salarie.Tiers != null)
                        {
                            salarieWindow.checkBox_SalarieTiers.IsChecked = true;
                        }
                    }
                    else
                    {
                        foreach (Salarie_Outillage so in ((Personne)this._DataGridMain.SelectedItem).Salarie.Salarie_Outillage)
                        {
                            salarieWindow.listOutillage.Remove(so.Outillage1);
                        }
                        salarieWindow.DataContext = this._DataGridMain.SelectedItem;
                        if (((Personne)this._DataGridMain.SelectedItem).Salarie.Salarie_Interne != null)
                        {
                            salarieWindow.checkBox_SalarieInterne.IsChecked = true;
                        }
                        if (((Personne)this._DataGridMain.SelectedItem).Salarie.Interimaire != null)
                        {
                            salarieWindow.checkBox_SalarieInterimaire.IsChecked = true;
                        }
                        if (((Personne)this._DataGridMain.SelectedItem).Salarie.Tiers != null)
                        {
                            salarieWindow.checkBox_SalarieTiers.IsChecked = true;
                        }
                    }

                    salarieWindow.LectureSeule();

                    bool? dialogResult = salarieWindow.ShowDialog();
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul salarié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un salarié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }
        #endregion

        private void _DataGridMain_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.menuLook();
        }

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

            MenuItem itemAfficher5 = new MenuItem();
            itemAfficher5.Header = "Imprimer";
            itemAfficher5.Click += new RoutedEventHandler(delegate { this.imprimer(); });

            contextMenu.Items.Add(itemAfficher);
            contextMenu.Items.Add(itemAfficher2);
            contextMenu.Items.Add(itemAfficher3);
            contextMenu.Items.Add(itemAfficher4);
            //contextMenu.Items.Add(new LineBreak());
            //contextMenu.Items.Add(itemAfficher5);
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
        private void imprimer()
        {
            try
            {
                //this._DataGridMain.SelectedItem = null;
                //PrintDialog printDialog = new PrintDialog();
                //if (printDialog.ShowDialog() == true)
                //{
                //    printDialog.PrintVisual(this._DataGridMain, "Salariés");
                //}
                PrintDialog Printdlg = new System.Windows.Controls.PrintDialog();
                if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
                {
                    Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                    // sizing of the element.
                    this._DataGridMain.Measure(pageSize);
                    this._DataGridMain.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));
                    Printdlg.PrintVisual(this._DataGridMain, "Salariés");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Impression raté. Veuillez contacter votre administrateur système.      :    " + e.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            //System.Windows.Controls.PrintDialog test2 = new System.Windows.Controls.PrintDialog();
            //test2.PageRangeSelection = PageRangeSelection.AllPages;
            //test2.UserPageRangeEnabled = true;

            //bool? print = test2.ShowDialog();
            //if (print)
            //{
            //    XpsDocument xpsDocument = new XpsDocument
            //}

        }

        #endregion

        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            _filterContainName.Text = "";
            _filterContainFirstName.Text = "";
            _filterContainSocieteMere.SelectedItem = null;
            _filterContainTelProFixe.Text = "";
            _filterContainTelProMobile.Text = "";
            _filterContainFaxPro.Text = "";
            _filterContainMailPro.Text = "";
            _filterContainMatricule.Text = "";
            _filterContainTypeSalarie.SelectedItem = null;
            this.listSalarie = new ObservableCollection<Personne>(((App)App.Current).mySitaffEntities.Personne.Where(p => p.Salarie != null).OrderBy(p => p.Nom));
        }

        private void _buttonFiltrer_Click(object sender, RoutedEventArgs e)
        {
            this.filtrage();
        }

        private void filtrage()
        {
            ((App)App.Current)._theMainWindow._mutex.WaitOne();
            ((App)App.Current)._theMainWindow.startThread();
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Filtrage en cours ...");
            this.listSalarie = new ObservableCollection<Personne>(((App)App.Current).mySitaffEntities.Personne.Where(p => p.Salarie != null).OrderBy(p => p.Nom));
            if (this._filterContainName.Text != "")
            {
                this.listSalarie = new ObservableCollection<Personne>(this.listSalarie.Where(sal => sal.Nom.Trim().ToLower().Contains(this._filterContainName.Text.Trim().ToLower())));
            }
            if (this._filterContainFirstName.Text != "")
            {
                this.listSalarie = new ObservableCollection<Personne>(this.listSalarie.Where(sal => sal.Prenom.Trim().ToLower().Contains(this._filterContainFirstName.Text.Trim().ToLower())));
            }
            if (this._filterContainSocieteMere.SelectedItem != null)
            {
                this.listSalarie = new ObservableCollection<Personne>(this.listSalarie.Where(sal => sal.Salarie != null));
                this.listSalarie = new ObservableCollection<Personne>(this.listSalarie.Where(sal => sal.Salarie.Salarie_Interne != null));
                this.listSalarie = new ObservableCollection<Personne>(this.listSalarie.Where(sal => sal.Salarie.Salarie_Interne.Entreprise_Mere1 != null));
                this.listSalarie = new ObservableCollection<Personne>(this.listSalarie.Where(sal => sal.Salarie.Salarie_Interne.Entreprise_Mere1.Identifiant == ((Entreprise_Mere)this._filterContainSocieteMere.SelectedItem).Identifiant));
            }
            if (this._filterContainTelProFixe.Text != "")
            {
                this.listSalarie = new ObservableCollection<Personne>(this.listSalarie.Where(sal => sal.Tel_Port_Pro.Trim().Replace(".", "").Replace(" ", "").ToLower().Contains(this._filterContainTelProFixe.Text.Replace(".", "").Replace(" ", "").ToLower())));
            }
            if (this._filterContainTelProMobile.Text != "")
            {
                this.listSalarie = new ObservableCollection<Personne>(this.listSalarie.Where(sal => sal.Tel_Fixe_Pro.Trim().Replace(".", "").Replace(" ", "").ToLower().Contains(this._filterContainTelProMobile.Text.Trim().Replace(".", "").Replace(" ", "").ToLower())));
            }
            if (this._filterContainFaxPro.Text != "")
            {
                this.listSalarie = new ObservableCollection<Personne>(this.listSalarie.Where(sal => sal.Fax_Pro.Trim().Replace(".", "").Replace(" ", "").ToLower().Contains(this._filterContainFaxPro.Text.Trim().Replace(".", "").Replace(" ", "").ToLower())));
            }
            if (this._filterContainMailPro.Text != "")
            {
                this.listSalarie = new ObservableCollection<Personne>(this.listSalarie.Where(sal => sal.EMail_Pro.Trim().ToLower().Contains(this._filterContainMailPro.Text.Trim().ToLower())));
            }
            if (this._filterContainMatricule.Text != "")
            {
                int val;
                if (int.TryParse(this._filterContainMatricule.Text, out val))
                {
                    this.listSalarie = new ObservableCollection<Personne>(this.listSalarie.Where(sal => sal.Salarie != null));
                    this.listSalarie = new ObservableCollection<Personne>(this.listSalarie.Where(sal => sal.Salarie.Salarie_Interne != null));
                    this.listSalarie = new ObservableCollection<Personne>(this.listSalarie.Where(sal => sal.Salarie.Salarie_Interne.Matricule_Interne == int.Parse(this._filterContainMatricule.Text)));
                }
            }
            if (this._filterContainTypeSalarie.SelectedItem != null)
            {
                this.listSalarie = new ObservableCollection<Personne>(this.listSalarie.Where(sal => sal.typeSalarie == ((TypeDeSalarie)this._filterContainTypeSalarie.SelectedItem).chaine));
            }
            ((App)App.Current)._theMainWindow.stopThread();
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Filtrage terminé ...");
            Thread.Sleep(20);
            if (this.listSalarie.Count() == 0)
            {
                MessageBox.Show("Aucun résultat ne correspont à votre recherche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region bouton masquer / afficher

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (_filterZone.Height != 21)
            {
                this._filterZone.Height = 21;
                this._ButtonMasqueFiltre.Content = "Afficher les filtres";
                this.remiseAZero();
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
            }
        }

        #endregion
    }
}
