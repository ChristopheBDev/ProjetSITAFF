﻿using System;
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
    /// Logique d'interaction pour ParametreTypeDevisControl.xaml
    /// </summary>
    public partial class ParametreTypeDevisControl : UserControl
    {
        #region proprieté de dependance

        public ObservableCollection<Devis_Type> listTypeDevis
        {
            get { return (ObservableCollection<Devis_Type>)GetValue(listTypeDevisProperty); }
            set { SetValue(listTypeDevisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listTypeDevis.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listTypeDevisProperty =
            DependencyProperty.Register("listTypeDevis", typeof(ObservableCollection<Devis_Type>), typeof(ParametreTypeDevisControl), new UIPropertyMetadata(null));

        #endregion

        #region constructeur

        public ParametreTypeDevisControl()
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
        private void initialisationDataDatagridMain(ObservableCollection<Devis_Type> listToPut)
        {
            if (listToPut == null)
            {
                this.listTypeDevis = new ObservableCollection<Devis_Type>(((App)App.Current).mySitaffEntities.Devis_Type.OrderBy(lib => lib.Libelle));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.listTypeDevis = new ObservableCollection<Devis_Type>(listToPut);
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

                #region CRUD (Create Read Update Delete)


                /// <summary>
                /// Ajoute une nouvelle condition de réglement fonction à la liste à l'aide d'une nouvelle fenêtre
                /// </summary>
                public Devis_Type Add()
                {
                    //Affichage du message "ajout en cours"
                    ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Ajout d'un type de devis en cours ...";

                    //Initialisation de la fenêtre
                    TypeDevisWindow typedevisWindow = new TypeDevisWindow();

                    //Création de l'objet temporaire
                    Devis_Type tmp = new Devis_Type();

                    //Mise de l'objet temporaire dans le datacontext
                    typedevisWindow.DataContext = tmp;


                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = typedevisWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        //Si j'appuie sur le bouton Ok, je renvoi l'objet banque se trouvant dans le datacontext de la fenêtre
                        return (Devis_Type)typedevisWindow.DataContext;
                    }
                    else
                    {
                        try
                        {
                            //On détache la devis
                            ((App)App.Current).mySitaffEntities.Detach((Devis_Type)typedevisWindow.DataContext);
                        }
                        catch (Exception)
                        {
                        }

                        //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                        ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Ajout d'untype de devis annulé : " + this.listTypeDevis.Count() + " / " + this.max;

                        return null;
                    }
                }

                /// <summary>
                /// Ouvre la condition de réglement séléctionnée à l'aide d'une nouvelle fenêtre
                /// </summary>
                public Devis_Type Open()
                {
                    if (this._DataGridMain.SelectedItem != null)
                    {
                        if (this._DataGridMain.SelectedItems.Count == 1)
                        {
                            //Affichage du message "modification en cours"
                            ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
                            ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Modification d'un type de devis en cours ...";

                            //Création de la fenêtre
                            TypeDevisWindow typedevisWindow = new TypeDevisWindow();

                            //Initialisation du Datacontext en Devis_Fournisseur et association à la Devis_Fournisseur sélectionnée
                            typedevisWindow.DataContext = new Devis_Type();
                            typedevisWindow.DataContext = (Devis_Type)this._DataGridMain.SelectedItem;

                            //booléen nullable vrai ou faux ou null
                            bool? dialogResult = typedevisWindow.ShowDialog();

                            if (dialogResult.HasValue && dialogResult.Value == true)
                            {
                                //Si j'appuie sur le bouton Ok, je renvoi l'objet DAO se trouvant dans le datacontext de la fenêtre
                                return (Devis_Type)typedevisWindow.DataContext;
                            }
                            else
                            {
                                //Je récupère les anciennes données de la base sur les modifications effectuées
                                ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Devis_Type)(this._DataGridMain.SelectedItem));
                                //La devis étant un objet "critique" au niveau des associations, je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                                ((App)App.Current).refreshEDMXSansVidage();
                                this.filtrage();

                                //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                                ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
                                this.recalculMax();
                                ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Modification d'un type de devis annulé : " + this.listTypeDevis.Count() + " / " + this.max;

                                return null;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vous ne devez sélectionner qu'un seul type de devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            return null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vous devez sélectionner un type de devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return null;
                    }
                }

                /// <summary>
                /// Supprime de la condition de réglement séléctionné avec une confirmation
                /// </summary>
                public Devis_Type Remove()
                {
                    if (this._DataGridMain.SelectedItem != null)
                    {
                        if (this._DataGridMain.SelectedItems.Count == 1)
                        {
                            //Affichage du message "suppression en cours"
                            ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
                            ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Suppression d'un type de devis en cours ...";

                            if (MessageBox.Show("Voulez-vous rééllement supprimer le type de devis séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                //Supprimer l'élément 
                                return (Devis_Type)this._DataGridMain.SelectedItem;
                            }
                            else
                            {
                                //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                                ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
                                this.recalculMax();
                                ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Suppression d'un type de devis annulé : " + this.listTypeDevis.Count() + " / " + this.max;

                                return null;
                            }

                        }
                        else
                        {
                            MessageBox.Show("Vous ne devez sélectionner qu'un seul type de devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            return null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vous devez sélectionner un type de devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return null;
                    }
                }

                /// <summary>
                /// Ouvre de la condition de réglement séléctionné à l'aide d'une nouvelle fenêtre
                /// </summary>
                public Devis_Type Look()
                {
                    if (this._DataGridMain.SelectedItem != null)
                    {
                        if (this._DataGridMain.SelectedItems.Count == 1)
                        {
                            //Affichage du message "affichage en cours"
                            ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = true;
                            ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Affichage d'un type de devis en cours ...";

                            //Création de la fenêtre
                            TypeDevisWindow typedevisWindow = new TypeDevisWindow();

                            //Initialisation du Datacontext en Devis_Fournisseur et association à l'ativite sélectionnée
                            typedevisWindow.DataContext = new Devis_Type();
                            typedevisWindow.DataContext = (Devis_Type)this._DataGridMain.SelectedItem;

                            //Je positionne la lecture seule sur la fenêtre
                            typedevisWindow.lectureSeule();

                            //J'affiche la fenêtre
                            bool? dialogResult = typedevisWindow.ShowDialog();

                            //Affichage du message "affichage en cours"
                            ((App)App.Current)._theMainWindow.parametreMain.progressBarMainWindow.IsIndeterminate = false;
                            ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Affichage d'un type de devis terminé : " + this.listTypeDevis.Count() + " / " + this.max;

                            //Renvoi null
                            return null;
                        }
                        else
                        {
                            MessageBox.Show("Vous ne devez sélectionner qu'un type de devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            return null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vous devez sélectionner un type de devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

                    ObservableCollection<Devis_Type> listToPut = new ObservableCollection<Devis_Type>(((App)App.Current).mySitaffEntities.Devis_Type.OrderBy(lib => lib.Libelle));

                    if (this._filterContainLibelle.Text != "")
                    {
                        listToPut = new ObservableCollection<Devis_Type>(listToPut.Where(lib => lib.Libelle.Trim().ToLower().Contains(this._filterContainLibelle.Text.Trim().ToLower())));
                    }

                    ((App)App.Current)._theMainWindow.parametreMain.stopThread();

                    //Insertion des données dans le datagrid
                    this.initialisationDataDatagridMain(listToPut);
                    if (this.listTypeDevis.Count() == 0)
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
                        if (_filterContainLibelle.Text != "" || this.max != this.listTypeDevis.Count())
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
                    this.max = ((App)App.Current).mySitaffEntities.Devis_Type.Count();
                }

                /// <summary>
                /// Met à jour l'état en bas pour l'utilisateur
                /// </summary>
                /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
                /// <param name="dao">un objet Devis_Fournisseur soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
                public void MiseAJourEtat(string typeEtat, Devis_Type lib)
                {
                    //Je racalcul le nombre max d'élements
                    this.recalculMax();
                    //En fonction de l'libion, j'affiche le message
                    if (typeEtat == "Filtrage")
                    {
                        ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "filtrage des types de devis terminé : " + this.listTypeDevis.Count() + " / " + this.max;
                    }
                    else if (typeEtat == "Ajout")
                    {
                        //J'ajoute la devis_fournisseur dans le linsting
                        this.listTypeDevis.Add(lib);
                        //Je racalcul le nombre max d'élements après l'ajout
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Ajout d'un type de devis dénommé '" + lib.Libelle + "' effectué avec succès. Nombre d'élements : " + this.listTypeDevis.Count() + " / " + this.max;
                    }
                    else if (typeEtat == "Modification")
                    {
                        //Je raffraichis mon datagrid
                        this._DataGridMain.Items.Refresh();
                        ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Modification du type de devis dénommé : '" + lib.Libelle + "' effectuée avec succès. Nombre d'élements : " + this.listTypeDevis.Count() + " / " + this.max;
                    }
                    else if (typeEtat == "Suppression")
                    {
                        //Je supprime de mon listing l'élément supprimé
                        this.listTypeDevis.Remove(lib);
                        //Je racalcul le nombre max d'élements après la suppression
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Suppression du type de devis dénommé : '" + lib.Libelle + "' effectuée avec succès. Nombre d'élements : " + this.listTypeDevis.Count() + " / " + this.max;
                    }
                    else if (typeEtat == "Look")
                    {

                    }
                    else
                    {
                        ((App)App.Current)._theMainWindow.parametreMain.textBlockMainWindow.Text = "Chargement des types de devis terminé : " + this.listTypeDevis.Count() + " / " + this.max;
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
                    this.listTypeDevis = new ObservableCollection<Devis_Type>(this.listTypeDevis.OrderBy(lib => lib.Libelle));
                }

                #endregion

                #region Deviss

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

