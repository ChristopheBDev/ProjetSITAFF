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
using SitaffRibbon.Classes;

namespace SitaffRibbon.Windows.ParametresUserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeExerciceControl.xaml
    /// </summary>
    public partial class ParametreExerciceControl : UserControl
    {
        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region constructeur

        public ParametreExerciceControl()
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

        #region initialisation zone de filtrage

        private void initialisationFilterZone()
        {
            this._filterZone.Height = 21;
        }

        #endregion

        #region initialisation donnés datagridMain
        private void initialisationDataDatagridMain(ObservableCollection<Exercice> listToPut)
        {
            if (listToPut == null)
            {
                this.listExercice = new ObservableCollection<Exercice>(((App)App.Current).mySitaffEntities.Exercice.OrderBy(exe => exe.Date_Debut));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listExercice = new ObservableCollection<Exercice>(listToPut);
                this.MiseAJourEtat("Filtrage", null);
            }
        }

        #endregion

        #region clic droit

        private void creationMenuClicDroit()
        {
            //Création du menu
            ContextMenu contextMenu = ((App)App.Current)._menuClicDroit.creationMenuClicDroitParameters(this);

            //Zone actions particulières

            //Afficher Masquer
            contextMenu.Items.Add(new Separator());
            MenuItem menuItemAffMas = ((App)App.Current)._menuClicDroit.creationAfficherMasquer(this._DataGridMain.Columns);

            menuItemAffMas.Items.Add(new Separator());

            this.MenuItem_AfficherTout = new MenuItem();
            this.MenuItem_AfficherTout.Header = "Afficher tout";
            this.MenuItem_AfficherTout.Click += new RoutedEventHandler(delegate { this.AffMas_AfficherTout(); });
            menuItemAffMas.Items.Add(this.MenuItem_AfficherTout);

            this.MenuItem_MasquerTout = new MenuItem();
            this.MenuItem_MasquerTout.Header = "Masquer tout";
            this.MenuItem_MasquerTout.Click += new RoutedEventHandler(delegate { this.AffMas_MasquerTout(); });
            menuItemAffMas.Items.Add(this.MenuItem_MasquerTout);

            contextMenu.Items.Add(menuItemAffMas);

            //Association du menu

            this._DataGridMain.ContextMenu = contextMenu;
        }

        private void AffMas_AfficherTout()
        {
            foreach (DataGridColumn item in this._DataGridMain.Columns)
            {
                item.Visibility = Visibility.Visible;
            }
            this.creationMenuClicDroit();
        }

        private void AffMas_MasquerTout()
        {
            foreach (DataGridColumn item in this._DataGridMain.Columns)
            {
                item.Visibility = Visibility.Collapsed;
            }
            this.creationMenuClicDroit();
        }

        #endregion

        #endregion

        #region Proprietés de dependances
        public ObservableCollection<Exercice> listExercice
        {
            get { return (ObservableCollection<Exercice>)GetValue(listExerciceProperty); }
            set { SetValue(listExerciceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listExerciceProperty =
            DependencyProperty.Register("listExercice", typeof(ObservableCollection<Exercice>), typeof(ParametreExerciceControl), new UIPropertyMetadata(null));

        #endregion

        #region fenetre chargé
        /// <summary>
        /// Fin du chargement de la fenêtre (pour fermer la fenêtre de chargement)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
            ((App)App.Current)._theMainWindow.parametreMain.stopThread();
        }

        #endregion

        #region CRUD (Create Read Update Delete)

        /// <summary>
        /// Ajoute une nouvelle Exercice à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Exercice Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Ajout d'un exercice en cours ...";

            //Initialisation de la fenêtre
            ExerciceWindow exerciceWindow = new ExerciceWindow();

            //Création de l'objet temporaire
            Exercice tmp = new Exercice();

            //Mise de l'objet temporaire dans le datacontext
            exerciceWindow.DataContext = tmp;


            //booléen nullable vrai ou faux ou null
            bool? dialogResult = exerciceWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet banque se trouvant dans le datacontext de la fenêtre
                return (Exercice)exerciceWindow.DataContext;
            }
            else
            {
                try
                {
                    //On détache la commande
                    ((App)App.Current).mySitaffEntities.Detach((Exercice)exerciceWindow.DataContext);
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
                ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Ajout d'un exercice annulé : " + this.listExercice.Count() + " / " + this.max;

                return null;
            }
        }


        /// <summary>
        /// Ouvre la Exercice séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Exercice Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Modification d'un exercice en cours ...";

                    //Création de la fenêtre
                    ExerciceWindow exerciceWindow = new ExerciceWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    exerciceWindow.DataContext = new Exercice();
                    exerciceWindow.DataContext = (Exercice)this._DataGridMain.SelectedItem;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = exerciceWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                        return (Exercice)exerciceWindow.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Exercice)(this._DataGridMain.SelectedItem));
                        //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Modification d'un exercice annulé : " + this.listExercice.Count() + " / " + this.max;

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul exercice.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un paramètre de exercice.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime l'exercice séléctionnée avec une confirmation
        /// </summary>
        public Exercice Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Suppression d'un paramètre de exercice en cours ...";

                    if (MessageBox.Show("Voulez-vous rééllement supprimer le exercice séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //Supprimer l'élément 
                        return (Exercice)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Suppression d'un exercice annulé : " + this.listExercice.Count() + " / " + this.max;

                        return null;
                    }

                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul exercice.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un exercice.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre l'exercice séléctionnée en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Exercice Look()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Affichage d'un exercice en cours ...";

                    //Création de la fenêtre
                    ExerciceWindow exerciceWindow = new ExerciceWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à l'ativite sélectionnée
                    exerciceWindow.DataContext = new Exercice();
                    exerciceWindow.DataContext = (Exercice)this._DataGridMain.SelectedItem;

                    //Je positionne la lecture seule sur la fenêtre
                    exerciceWindow.lectureSeule();

                    //J'affiche la fenêtre
                    bool? dialogResult = exerciceWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
                    ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Affichage d'un exercice terminé : " + this.listExercice.Count() + " / " + this.max;

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul exercice.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un exercice.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        #endregion

        #region filtrage

        #region remise a zero

        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            _filterContainDateDebut.SelectedDate = null;
            _filterContainDateFin.SelectedDate = null;
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
            ((App)App.Current)._theMainWindow.parametreMain._mutex.WaitOne();
            ((App)App.Current)._theMainWindow.parametreMain.startThread();
            ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Filtrage en cours ...";

            ObservableCollection<Exercice> listToPut = new ObservableCollection<Exercice>(((App)App.Current).mySitaffEntities.Exercice.OrderBy(lib => lib.Date_Debut));

            if (this._filterContainDateDebut.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Exercice>(listToPut.Where(lib => lib.Date_Debut == this._filterContainDateDebut.SelectedDate));
            }
            if (this._filterContainDateFin.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Exercice>(listToPut.Where(lib => lib.Date_Fin == this._filterContainDateFin.SelectedDate));
            }

            ((App)App.Current)._theMainWindow.parametreMain.stopThread();

            ((App)App.Current)._theMainWindow.parametreMain.stopThread();


            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);
            if (this.listExercice.Count() == 0)
            {
                MessageBox.Show("Aucun résultat ne correspont à votre recherche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (_filterContainDateDebut.SelectedDate != null || _filterContainDateDebut.SelectedDate != null || this.max != this.listExercice.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainDateDebut.Focus();
            }
        }

        #endregion

        #endregion

        #region évenements

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
            ((App)App.Current)._theMainWindow.parametreMain._CommandLook.Command.Execute(((App)App.Current)._theMainWindow.parametreMain);
        }

        #endregion

        #region KeyUp

        /// <summary>
        /// Quand l'utilisateur fais entrée
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
        #endregion

        #endregion

        #region fonctions

        /// <summary>
        /// Recalcul le nombre d'élements maximum
        /// </summary>
        private void recalculMax()
        {
            this.max = ((App)App.Current).mySitaffEntities.Exercice.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Exercice lib)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'libion, j'affiche le message
            if (typeEtat == "Filtrage")
            {
                ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "filtrage des exercices terminé : " + this.listExercice.Count() + " / " + this.max;
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listExercice.Add(lib);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Ajout d'un exercice dénommé '" + lib.Date_Debut + "' effectué avec succès. Nombre d'élements : " + this.listExercice.Count() + " / " + this.max;
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
                ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Modification d'un exercice dénommé : '" + lib.Date_Debut + "' effectuée avec succès. Nombre d'élements : " + this.listExercice.Count() + " / " + this.max;
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listExercice.Remove(lib);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Suppression d'un exercice dénommé : '" + lib.Date_Debut + "' effectuée avec succès. Nombre d'élements : " + this.listExercice.Count() + " / " + this.max;
            }
            else if (typeEtat == "Look")
            {

            }
            else
            {
                ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Chargement des exercices terminé : " + this.listExercice.Count() + " / " + this.max;
            }
            //Je retri les données dans le sens par défaut
            this.triDatas();
            //J'arrete la progressbar
            ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
        }

        /// <summary>
        /// Tri les données dans le sens par défaut
        /// </summary>
        private void triDatas()
        {
            this.listExercice = new ObservableCollection<Exercice>(this.listExercice.OrderBy(exe => exe.Date_Debut));
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