using SitaffRibbon.Classes;
using SitaffRibbon.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeAvanceControl.xaml
    /// </summary>
    public partial class ListeAvanceControl : UserControl
    {

        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer
        MenuItem MenuItem_ColonneSalarie;
        MenuItem MenuItem_ColonneDate;
        MenuItem MenuItem_ColonneSomme;

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Propd

        public ObservableCollection<Avance> listAvances
        {
            get { return (ObservableCollection<Avance>)GetValue(listAvancesProperty); }
            set { SetValue(listAvancesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAvances.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAvancesProperty =
            DependencyProperty.Register("listAvances", typeof(ObservableCollection<Avance>), typeof(ListeAvanceControl), new PropertyMetadata(null));

        #endregion

        #region Constructeur

        public ListeAvanceControl()
        {
            InitializeComponent();

            //Initialisation de la zone de filtrage
            this.initialisationFilterZone();

            //Création du menu du clic droit
            this.creationMenuClicDroit();

            //Je calcul le nombre d'élements du datagrid
            this.recalculMax();

            //J'initialise les data
            this.initialisationDataDatagridMain(null);

            //Je passe le usercontrol à la personnalisation de l'utilisateur
            ((App)App.Current).personnalisation.initUserControl(this);
            //Je récupère les couleurs car on ne peut les faire en automatique pour TOUS les usercontrols
            this._filterZone.Background = ((App)App.Current).personnalisation.BackGroundUserControlFilterColor;
            this._DataGridMain.RowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridColor;
            this._DataGridMain.AlternatingRowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridAlternateColor;
            this._textBlockTaillePolice.Text = "Taille police (" + this._DataGridMain.FontSize.ToString() + ") :     ";
        }

        #region initialisation Zone de filtrage

        private void initialisationFilterZone()
        {
            this.initialisationAutoCompleteBox();
            this._filterZone.Height = 21;
        }

        private void initialisationAutoCompleteBox()
        {
            List<string> listSalarie = new List<string>();
            foreach (Avance item in ((App)App.Current).mySitaffEntities.Avance)
            {
                //Pour remplir les salariés
                if (item.Salarie1 != null)
                {
                    if (item.Salarie1.Personne != null)
                    {
                        if (!listSalarie.Contains(item.Salarie1.Personne.fullname))
                        {
                            listSalarie.Add(item.Salarie1.Personne.fullname);
                        }
                    }
                }
            }

            _filterContainSalarie.ItemsSource = listSalarie;
        }

        #endregion

        #region initialisation Donnés datagridMain

        private void initialisationDataDatagridMain(ObservableCollection<Avance> listToPut)
        {
            if (listToPut == null)
            {
                this.listAvances = new ObservableCollection<Avance>(((App)App.Current).mySitaffEntities.Avance.OrderByDescending(ava => ava.Date_Avance));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listAvances = new ObservableCollection<Avance>(listToPut);
                this.MiseAJourEtat("Filtrage", null);
            }
        }

        #endregion

        #region clic droit

        private void creationMenuClicDroit()
        {
            ContextMenu contextMenu = new ContextMenu();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorToPut = "#A3D0D8E8";
            Brush colorMenu = (Brush)converter.ConvertFrom(colorToPut);
            contextMenu.Background = colorMenu;
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

            MenuItem itemAfficher5 = RemplirMenuAfficherMasquerColonnes(new MenuItem());
            itemAfficher5.Header = "Afficher / Masquer";

            Securite securite = new Securite();
            if (securite.VerificationDroitActionsCRUD(this.ToString(), "Look"))
            {
                contextMenu.Items.Add(itemAfficher);
            }
            if (securite.VerificationDroitActionsCRUD(this.ToString(), "Add"))
            {
                contextMenu.Items.Add(itemAfficher2);
            }
            if (securite.VerificationDroitActionsCRUD(this.ToString(), "Update"))
            {
                contextMenu.Items.Add(itemAfficher3);
            }
            if (securite.VerificationDroitActionsCRUD(this.ToString(), "Remove"))
            {
                contextMenu.Items.Add(itemAfficher4);
            }

            contextMenu.Items.Add(new Separator());

            contextMenu.Items.Add(itemAfficher5);
        }

        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {
            this.MenuItem_ColonneSalarie = new MenuItem();
            this.MenuItem_ColonneSalarie.IsChecked = false;
            this.MenuItem_ColonneSalarie.Header = "Salarié";
            this.MenuItem_ColonneSalarie.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneSalarie(); });
            this.AffMas_ColonneSalarie();
            menuItem.Items.Add(this.MenuItem_ColonneSalarie);

            this.MenuItem_ColonneDate = new MenuItem();
            this.MenuItem_ColonneDate.IsChecked = false;
            this.MenuItem_ColonneDate.Header = "Date";
            this.MenuItem_ColonneDate.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneDate(); });
            this.AffMas_ColonneDate();
            menuItem.Items.Add(this.MenuItem_ColonneDate);

            this.MenuItem_ColonneSomme = new MenuItem();
            this.MenuItem_ColonneSomme.IsChecked = false;
            this.MenuItem_ColonneSomme.Header = "Somme";
            this.MenuItem_ColonneSomme.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneSomme(); });
            this.AffMas_ColonneSomme();
            menuItem.Items.Add(this.MenuItem_ColonneSomme);

            menuItem.Items.Add(new Separator());

            this.MenuItem_AfficherTout = new MenuItem();
            this.MenuItem_AfficherTout.Header = "Afficher tout";
            this.MenuItem_AfficherTout.Click += new RoutedEventHandler(delegate { this.AffMas_AfficherTout(); });
            menuItem.Items.Add(this.MenuItem_AfficherTout);

            this.MenuItem_MasquerTout = new MenuItem();
            this.MenuItem_MasquerTout.Header = "Masquer tout";
            this.MenuItem_MasquerTout.Click += new RoutedEventHandler(delegate { this.AffMas_MasquerTout(); });
            menuItem.Items.Add(this.MenuItem_MasquerTout);

            return menuItem;
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

        #region Afficher / Masquer

        #region Tout

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneSalarie.IsChecked = false;
            this.MenuItem_ColonneDate.IsChecked = false;
            this.MenuItem_ColonneSomme.IsChecked = false;

            this.AffMas_ColonneSalarie();
            this.AffMas_ColonneDate();
            this.AffMas_ColonneSomme();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneSalarie.IsChecked = true;
            this.MenuItem_ColonneDate.IsChecked = true;
            this.MenuItem_ColonneSomme.IsChecked = true;

            this.AffMas_ColonneSalarie();
            this.AffMas_ColonneDate();
            this.AffMas_ColonneSomme();
        }

        #endregion

        private void AffMas_ColonneSalarie()
        {
            if (this.MenuItem_ColonneSalarie.IsChecked == true)
            {
                this._ColonneSalarie.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneSalarie.IsChecked = false;
            }
            else
            {
                this._ColonneSalarie.Visibility = Visibility.Visible;
                this.MenuItem_ColonneSalarie.IsChecked = true;
            }
        }

        private void AffMas_ColonneDate()
        {
            if (this.MenuItem_ColonneDate.IsChecked == true)
            {
                this._ColonneDate.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneDate.IsChecked = false;
            }
            else
            {
                this._ColonneDate.Visibility = Visibility.Visible;
                this.MenuItem_ColonneDate.IsChecked = true;
            }
        }

        private void AffMas_ColonneSomme()
        {
            if (this.MenuItem_ColonneSomme.IsChecked == true)
            {
                this._ColonneSomme.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneSomme.IsChecked = false;
            }
            else
            {
                this._ColonneSomme.Visibility = Visibility.Visible;
                this.MenuItem_ColonneSomme.IsChecked = true;
            }
        }

        #endregion

        #endregion

        #endregion

        #region Fenêtre chargée

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
            ((App)App.Current)._theMainWindow.stopThread();
        }

        #endregion

        #region CRUD (Create Read Update Delete)

        /// <summary>
        /// Ajoute une nouvelle Commande_Fournisseur à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Avance Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une avance en cours ...");

            //Initialisation de la fenêtre
            AvanceWindow avanceWindow = new AvanceWindow();

            //Création de l'objet temporaire
            Avance tmp = new Avance();

            //Mise de l'objet temporaire dans le datacontext
            avanceWindow.DataContext = tmp;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = avanceWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet commande se trouvant dans le datacontext de la fenêtre
                return (Avance)avanceWindow.DataContext;
            }
            else
            {
                try
                {
                    //On détache l'avance
                    ((App)App.Current).mySitaffEntities.Detach((Avance)avanceWindow.DataContext);
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une avance annulé : " + this.listAvances.Count() + " / " + this.max);

                return null;
            }
        }

        /// <summary>
        /// Ouvre la commande fournisseur séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Avance Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une commande fournisseur en cours ...");

                    bool test = true;
                    if (test)
                    {
                        if (((Avance)this._DataGridMain.SelectedItem).Frais1 != null)
                        {
                            test = false;
                            MessageBox.Show("Vous ne pouvez modifier cette avance car elle est liée à une fiche de frais", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification impossible, cause : avance associée à une fiche de frais : " + this.listAvances.Count() + " / " + this.max);
                        }
                    }

                    if (test)
                    {
                        //Création de la fenêtre
                        AvanceWindow avanceWindow = new AvanceWindow();

                        //Initialisation du Datacontext en Avance et association à la Avance sélectionnée
                        avanceWindow.DataContext = new Avance();
                        avanceWindow.DataContext = (Avance)this._DataGridMain.SelectedItem;

                        //booléen nullable vrai ou faux ou null
                        bool? dialogResult = avanceWindow.ShowDialog();

                        if (dialogResult.HasValue && dialogResult.Value == true)
                        {
                            //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                            return (Avance)avanceWindow.DataContext;
                        }
                        else
                        {
                            //Je récupère les anciennes données de la base sur les modifications effectuées
                            ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Avance)(this._DataGridMain.SelectedItem));
                            //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                            ((App)App.Current).refreshEDMXSansVidage();
                            this.filtrage();

                            //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification d'une avance annulée : " + this.listAvances.Count() + " / " + this.max);

                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule avance.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une avance.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime l'avance séléctionnée avec une confirmation
        /// </summary>
        public Avance Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une avance en cours ...");

                    bool test = true;
                    if (test)
                    {
                        if (((Avance)this._DataGridMain.SelectedItem).Frais1 != null)
                        {
                            test = false;
                            MessageBox.Show("Vous ne pouvez supprimer cette avance car elle est liée à une fiche de frais", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression impossible, cause : avance associée à une fiche de frais : " + this.listAvances.Count() + " / " + this.max);
                        }
                    }

                    if (test)
                    {
                        if (MessageBox.Show("Voulez-vous rééllement supprimer l'avance séléctionnée ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            //Supprimer l'élément 
                            return (Avance)this._DataGridMain.SelectedItem;
                        }
                        else
                        {
                            //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression d'une avance annulée : " + this.listAvances.Count() + " / " + this.max);

                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule avance.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une avance.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre l'avance séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Avance Look(Avance avance)
        {
            if (this._DataGridMain.SelectedItem != null || avance != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1 || avance != null)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une commande fournisseur en cours ...");

                    //Création de la fenêtre
                    AvanceWindow avanceWindow = new AvanceWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    avanceWindow.DataContext = new Avance();
                    if (avance != null)
                    {
                        avanceWindow.DataContext = avance;
                    }
                    else
                    {
                        avanceWindow.DataContext = (Avance)this._DataGridMain.SelectedItem;
                    }

                    //Je positionne la lecture seule sur la fenêtre
                    avanceWindow.lectureSeule();

                    //J'affiche la fenêtre
                    bool? dialogResult = avanceWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
((App)App.Current)._theMainWindow.changementTexteStatusBar("Affichage d'une avance terminé : " + this.listAvances.Count() + " / " + this.max);

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'une seule avance.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une avance.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        #endregion

        #region filtrages

        #region Remise à Zéro

        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            //Remise à zéro de tous les champs de filtrage
            //Text
            _filterContainSalarie.Text = "";
            _filterContainDate.Text = "";
            _filterContainSalarie.Text = "";

            //Rechargement des élements
            this.initialisationDataDatagridMain(null);
        }

        #endregion

        #region bouton filtrer

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

            ObservableCollection<Avance> listToPut = new ObservableCollection<Avance>(((App)App.Current).mySitaffEntities.Avance.OrderByDescending(ava => ava.Date_Avance));

            if (this._filterContainSalarie.Text != "")
            {
                listToPut = new ObservableCollection<Avance>(listToPut.Where(com => com.Salarie1 != null));
                listToPut = new ObservableCollection<Avance>(listToPut.Where(com => com.Salarie1.Personne != null));
                listToPut = new ObservableCollection<Avance>(listToPut.Where(com => com.Salarie1.Personne.fullname.Trim().ToLower().Contains(this._filterContainSalarie.Text.Trim().ToLower()) || com.Salarie1.Personne.Initiales.Trim().ToLower().Contains(this._filterContainSalarie.Text.Trim().ToLower())));
            }
            if (this._filterContainSomme.Text != "")
            {
                double val;
                if (double.TryParse(this._filterContainSomme.Text.Trim(), out val))
                {
                    listToPut = new ObservableCollection<Avance>(listToPut.Where(com => com.Somme != null));
                    listToPut = new ObservableCollection<Avance>(listToPut.Where(com => com.Somme.ToString().Contains(double.Parse(this._filterContainSomme.Text.Trim()).ToString())));
                }
            }
            if (this._filterContainDate.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Avance>(listToPut.Where(com => com.Date_Avance != null));
                listToPut = new ObservableCollection<Avance>(listToPut.Where(com => com.Date_Avance.Value.Year == this._filterContainDate.SelectedDate.Value.Year && com.Date_Avance.Value.Month == this._filterContainDate.SelectedDate.Value.Month && com.Date_Avance.Value.Day == this._filterContainDate.SelectedDate.Value.Day));
            }

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);

            //Si aucun résultat, j'affiche un message
            if (this.listAvances.Count() == 0)
            {
                MessageBox.Show("Aucun résultat ne correspont à votre recherche.", "Aucun résultat", MessageBoxButton.OK);
            }
        }

        #endregion

        #region bouton masquer / afficher

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.AfficherMasquer();
        }

        public void AfficherMasquer()
        {
            if (_filterZone.Height != 21)
            {
                this._filterZone.Height = 21;
                this._ButtonMasqueFiltre.Content = "Afficher les filtres";
                //Lorsque je masque, je remet à zéro si certains champs sont rempli OU si le nombre d'élements max n'est pas égal au nombre d'éléments actuel
                if (_filterContainSalarie.Text != "" || _filterContainDate.Text != "" || _filterContainSomme.Text != "" || this.max != this.listAvances.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainSalarie.Focus();
            }
        }

        #endregion

        #endregion

        #region évenements

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

        #region KeyUp

        /// <summary>
        /// Quand l'utilisateur fais entrée dans une AutoCompleteBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _filter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.filtrage();
            }
        }

        private void _filterContainMontant_KeyUp_1(object sender, KeyEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
            if (e.Key == Key.Enter)
            {
                this.filtrage();
            }
        }

        #endregion

        #endregion

        #region Fonctions

        /// <summary>
        /// Recalcul le nombre d'élements maximum
        /// </summary>
        private void recalculMax()
        {
            this.max = ((App)App.Current).mySitaffEntities.Avance.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Avance ava)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
((App)App.Current)._theMainWindow.changementTexteStatusBar("filtrage des avances terminée : " + this.listAvances.Count() + " / " + this.max);
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listAvances.Add(ava);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Ajout d'une avance effectué avec succès. Nombre d'élements : " + this.listAvances.Count() + " / " + this.max);
                try
                {
                    this._DataGridMain.SelectedItem = ava;
                }
                catch (Exception) { }
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Modification de l'avance effectuée avec succès. Nombre d'élements : " + this.listAvances.Count() + " / " + this.max);
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listAvances.Remove(ava);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
((App)App.Current)._theMainWindow.changementTexteStatusBar("Suppression de l'avance effectuée avec succès. Nombre d'élements : " + this.listAvances.Count() + " / " + this.max);
            }
            else if (typeEtat == "Look")
            {

            }
            else
            {
((App)App.Current)._theMainWindow.changementTexteStatusBar("Chargement des avances terminé : " + this.listAvances.Count() + " / " + this.max);
            }
            //Je retri les données dans le sens par défaut
            this.triDatas();
            //J'arrete la progressbar
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
        }

        /// <summary>
        /// Tri les données dans le sens par défaut
        /// </summary>
        private void triDatas()
        {
            this.listAvances = new ObservableCollection<Avance>(this.listAvances.OrderByDescending(ava => ava.Date_Avance));
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

        #region Filtrage

        private void _CommandFiltrage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.AfficherMasquer();
        }

        private void _CommandFiltrage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #endregion

    }
}
