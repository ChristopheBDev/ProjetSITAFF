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
    /// Logique d'interaction pour ParametreArticleFactureControl.xaml
    /// </summary>
    public partial class ParametreArticleFactureControl : UserControl
    {
        #region Variables

        long max = 0;

        //Les MenuItems Afficher / Masquer

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

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

        #region Constructeur

        public ParametreArticleFactureControl()
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
            this.initialisationAutoCompleteBox();
            this._filterZone.Height = 21;
        }

        private void initialisationAutoCompleteBox()
        {
            List<string> listPlanComptableTva = new List<string>();
            List<string> listPlanComptableImputation = new List<string>();
            List<string> listPlanComptableImputation1 = new List<string>();
            foreach (Article_Facture item in ((App)App.Current).mySitaffEntities.Article_Facture)
            {
                //Pour remplir les cpi
                if (item.Plan_Comptable_Imputation1 != null)
                {
                    if (!listPlanComptableImputation.Contains(item.Plan_Comptable_Imputation1.Libelle))
                    {
                        listPlanComptableImputation.Add(item.Plan_Comptable_Imputation1.Libelle);
                    }
                }
                if (item.Plan_Comptable_Imputation2 != null)
                {
                    if (!listPlanComptableImputation1.Contains(item.Plan_Comptable_Imputation2.Libelle))
                    {
                        listPlanComptableImputation1.Add(item.Plan_Comptable_Imputation2.Libelle);
                    }
                }

                if (item.Plan_Comptable_Tva1 != null)
                {
                    if (!listPlanComptableTva.Contains(item.Plan_Comptable_Tva1.Libelle))
                    {
                        listPlanComptableTva.Add(item.Plan_Comptable_Tva1.Libelle);
                    }
                }
            }

            _filterContainPlanComptableImputation1.ItemsSource = listPlanComptableImputation;
            _filterContainPlanComptableImputation2.ItemsSource = listPlanComptableImputation1;
            _filterContainPlanComptableTva.ItemsSource = listPlanComptableTva;
        }

        #endregion

        #region initialisation donnés datagridMain
        private void initialisationDataDatagridMain(ObservableCollection<Article_Facture> listToPut)
        {
            if (listToPut == null)
            {
                this.listArticle = new ObservableCollection<Article_Facture>(((App)App.Current).mySitaffEntities.Article_Facture.OrderBy(lib => lib.Libelle));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listArticle = new ObservableCollection<Article_Facture>(listToPut);
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



        public ObservableCollection<Article_Facture> listArticle
        {
            get { return (ObservableCollection<Article_Facture>)GetValue(listArticleProperty); }
            set { SetValue(listArticleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listArticle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listArticleProperty =
            DependencyProperty.Register("listArticle", typeof(ObservableCollection<Article_Facture>), typeof(ParametreArticleFactureControl), new UIPropertyMetadata(null));




        public ObservableCollection<Plan_Comptable_Imputation> listPlanComptableImputation1
        {
            get { return (ObservableCollection<Plan_Comptable_Imputation>)GetValue(listPlanComptableImputation1Property); }
            set { SetValue(listPlanComptableImputation1Property, value); }
        }

        // Using a DependencyProperty as the backing store for listPlanComptableImputation1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listPlanComptableImputation1Property =
            DependencyProperty.Register("listPlanComptableImputation1", typeof(ObservableCollection<Plan_Comptable_Imputation>), typeof(ParametreArticleFactureControl), new UIPropertyMetadata(null));

        


        public ObservableCollection<Plan_Comptable_Imputation> listPlanComptableImputation
        {
            get { return (ObservableCollection<Plan_Comptable_Imputation>)GetValue(listPlanComptableImputationProperty); }
            set { SetValue(listPlanComptableImputationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listPlanComptableImputation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listPlanComptableImputationProperty =
            DependencyProperty.Register("listPlanComptableImputation", typeof(ObservableCollection<Plan_Comptable_Imputation>), typeof(ParametreArticleFactureControl), new UIPropertyMetadata(null));




        public ObservableCollection<Plan_Comptable_Tva> listPlanComptableTva
        {
            get { return (ObservableCollection<Plan_Comptable_Tva>)GetValue(listPlanComptableTvaProperty); }
            set { SetValue(listPlanComptableTvaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listPlanComptableTva.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listPlanComptableTvaProperty =
            DependencyProperty.Register("listPlanComptableTva", typeof(ObservableCollection<Plan_Comptable_Tva>), typeof(ParametreArticleFactureControl), new UIPropertyMetadata(null));

        

        #endregion

        #region CRUD (Create Read Update Delete)

        /// <summary>
        /// Ajoute une nouvelle ville à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Article_Facture Add()
        {
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Ajout d'un article en cours ...";

            //Initialisation de la fenêtre
            ArticleFactureWindow articleFactureWindow = new ArticleFactureWindow();

            //Création de l'objet temporaire
            Article_Facture tmp = new Article_Facture();

            //Mise de l'objet temporaire dans le datacontext
            articleFactureWindow.DataContext = tmp;


            //booléen nullable vrai ou faux ou null
            bool? dialogResult = articleFactureWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Si j'appuie sur le bouton Ok, je renvoi l'objet ville se trouvant dans le datacontext de la fenêtre
                return (Article_Facture)articleFactureWindow.DataContext;
            }
            else
            {
                try
                {
                    //On détache la commande
                    ((App)App.Current).mySitaffEntities.Detach((Article_Facture)articleFactureWindow.DataContext);
                }
                catch (Exception)
                {
                }

                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
                ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Ajout d'un article annulé : " + this.listArticle.Count() + " / " + this.max;

                return null;
            }
        }


        /// <summary>
        /// Ouvre la ville séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Article_Facture Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Modification d'un article en cours ...";

                    //Création de la fenêtre
                    ArticleFactureWindow articleFactureWindow = new ArticleFactureWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à la Commande_Fournisseur sélectionnée
                    articleFactureWindow.DataContext = new Article_Facture();
                    articleFactureWindow.DataContext = (Article_Facture)this._DataGridMain.SelectedItem;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = articleFactureWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                        return (Article_Facture)articleFactureWindow.DataContext;
                    }
                    else
                    {
                        //Je récupère les anciennes données de la base sur les modifications effectuées
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Article_Facture)(this._DataGridMain.SelectedItem));
                        //La commande étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                        ((App)App.Current).refreshEDMXSansVidage();
                        this.filtrage();

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Modification d'un article annulé : " + this.listArticle.Count() + " / " + this.max;

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul article.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un article.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime la ville séléctionnéé avec une confirmation
        /// </summary>
        public Article_Facture Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Suppression d'un article en cours ...";

                    if (MessageBox.Show("Voulez-vous rééllement supprimer l'article séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //Supprimer l'élément 
                        return (Article_Facture)this._DataGridMain.SelectedItem;
                    }
                    else
                    {
                        //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                        ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Suppression d'un article annulé : " + this.listArticle.Count() + " / " + this.max;

                        return null;
                    }

                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul article.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un article.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ouvre la ville séléctionnée à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Article_Facture Look()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Affichage d'un article en cours ...";

                    //Création de la fenêtre
                    ArticleFactureWindow articleFactureWindow = new ArticleFactureWindow();

                    //Initialisation du Datacontext en Commande_Fournisseur et association à l'ativite sélectionnée
                    articleFactureWindow.DataContext = new Article_Facture();
                    articleFactureWindow.DataContext = (Article_Facture)this._DataGridMain.SelectedItem;

                    //Je positionne la lecture seule sur la fenêtre
                    articleFactureWindow.lectureSeule();

                    //J'affiche la fenêtre
                    bool? dialogResult = articleFactureWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
                    ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Affichage d'un article terminé : " + this.listArticle.Count() + " / " + this.max;

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul article.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un article.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            _filterContainLibelle.Text = "";
            _filterContainCode.Text = "";
            _filterContainCondition.Text = "";
            _filterContainPlanComptableImputation1.Text = "";
            _filterContainPlanComptableImputation2.Text = "";
            _filterContainPlanComptableTva.Text = "";
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

            ObservableCollection<Article_Facture> listToPut = new ObservableCollection<Article_Facture>(((App)App.Current).mySitaffEntities.Article_Facture.OrderBy(lib => lib.Libelle));

            if (this._filterContainLibelle.Text != "")
            {
                listToPut = new ObservableCollection<Article_Facture>(listToPut.Where(lib => lib.Libelle.Trim().ToLower().Contains(this._filterContainLibelle.Text.Trim().ToLower())));
            }
            if (this._filterContainCode.Text != "")
            {
                listToPut = new ObservableCollection<Article_Facture>(listToPut.Where(lib => lib.Code != null));
                listToPut = new ObservableCollection<Article_Facture>(listToPut.Where(lib => lib.Code.Trim().ToLower().Contains(this._filterContainCode.Text.Trim().ToLower())));
            }
            if (this._filterContainCondition.Text != "")
            {
                listToPut = new ObservableCollection<Article_Facture>(listToPut.Where(lib => lib.Condition != null));
                listToPut = new ObservableCollection<Article_Facture>(listToPut.Where(lib => lib.Condition.Trim().ToLower().Contains(this._filterContainCondition.Text.Trim().ToLower())));
            }
            if (this._filterContainPlanComptableImputation1.Text != "")
            {
                listToPut = new ObservableCollection<Article_Facture>(listToPut.Where(lib => lib.Plan_Comptable_Imputation1 != null));
                listToPut = new ObservableCollection<Article_Facture>(listToPut.Where(lib => lib.Plan_Comptable_Imputation1.Libelle.Trim().ToLower().Contains(this._filterContainPlanComptableImputation1.Text.Trim().ToLower())));
            }
            if (this._filterContainPlanComptableImputation2.Text != "")
            {
                listToPut = new ObservableCollection<Article_Facture>(listToPut.Where(lib => lib.Plan_Comptable_Imputation2 != null));
                listToPut = new ObservableCollection<Article_Facture>(listToPut.Where(lib => lib.Plan_Comptable_Imputation2.Libelle.Trim().ToLower().Contains(this._filterContainPlanComptableImputation2.Text.Trim().ToLower())));
            }
            if (this._filterContainPlanComptableTva.Text != "")
            {
                listToPut = new ObservableCollection<Article_Facture>(listToPut.Where(lib => lib.Plan_Comptable_Tva1 != null));
                listToPut = new ObservableCollection<Article_Facture>(listToPut.Where(lib => lib.Plan_Comptable_Tva1.Libelle.Trim().ToLower().Contains(this._filterContainPlanComptableTva.Text.Trim().ToLower())));
            }
            ((App)App.Current)._theMainWindow.parametreMain.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataDatagridMain(listToPut);
            if (this.listArticle.Count() == 0)
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
                if (_filterContainLibelle.Text != "" || _filterContainCode.Text != "" || _filterContainCondition.Text != "" || _filterContainPlanComptableImputation2.Text != "" || _filterContainPlanComptableTva.Text != "" || _filterContainPlanComptableImputation1.Text != "" || this.max != this.listArticle.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._ButtonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainLibelle.Focus();
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
        #endregion

        #endregion

        #region fonctions

        /// <summary>
        /// Recalcul le nombre d'élements maximum
        /// </summary>
        private void recalculMax()
        {
            this.max = ((App)App.Current).mySitaffEntities.Article_Facture.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Commande_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Article_Facture lib)
        {
            //Je racalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'libion, j'affiche le message
            if (typeEtat == "Filtrage")
            {
                ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "filtrage des villes terminée : " + this.listArticle.Count() + " / " + this.max;
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute la commande_fournisseur dans le linsting
                this.listArticle.Add(lib);
                //Je racalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Ajout d'un article dénommée '" + lib.Libelle + "' effectué avec succès. Nombre d'élements : " + this.listArticle.Count() + " / " + this.max;
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
                ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Modification de la ville dénommée : '" + lib.Libelle + "' effectuée avec succès. Nombre d'élements : " + this.listArticle.Count() + " / " + this.max;
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.listArticle.Remove(lib);
                //Je racalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Suppression de la ville dénommée : '" + lib.Libelle + "' effectuée avec succès. Nombre d'élements : " + this.listArticle.Count() + " / " + this.max;
            }
            else if (typeEtat == "Look")
            {

            }
            else
            {
                ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Chargement des villes terminé : " + this.listArticle.Count() + " / " + this.max;
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
            this.listArticle = new ObservableCollection<Article_Facture>(this.listArticle.OrderBy(lib => lib.Libelle));
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
