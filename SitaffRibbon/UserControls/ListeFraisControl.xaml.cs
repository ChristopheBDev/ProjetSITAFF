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
using System.ComponentModel;
using SitaffRibbon.Classes;

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListeFraisControl.xaml
    /// </summary>
    public partial class ListeFraisControl : UserControl
    {

        #region Variable

        long max = 0;
        MenuItem MenuItem_ColonneNomSalarie;
        MenuItem MenuItem_ColonneFraisDateDebut;
        MenuItem MenuItem_ColonneFraisDateFin;
        MenuItem MenuItem_ColonneFraisLot;
        MenuItem MenuItem_ColonneTotalHT;
        MenuItem MenuItem_ColonneTotalTTC;
        MenuItem MenuItem_ColonneTotalTVA;
        MenuItem MenuItem_ColonneTotalAvance;

        MenuItem MenuItem_AfficherTout;
        MenuItem MenuItem_MasquerTout;

        #endregion

        #region Propriétés de dépendances
        public ObservableCollection<Frais> Frais
        {
            get { return (ObservableCollection<Frais>)GetValue(FraisProperty); }
            set { SetValue(FraisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FraisProperty =
            DependencyProperty.Register("Frais", typeof(ObservableCollection<Frais>), typeof(ListeFraisControl), new UIPropertyMetadata(null));


        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(ListeFraisControl), new UIPropertyMetadata(null));

        public ObservableCollection<Entreprise> listEntreprise
        {
            get { return (ObservableCollection<Entreprise>)GetValue(listEntrepriseProperty); }
            set { SetValue(listEntrepriseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listClient.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseProperty =
            DependencyProperty.Register("listEntreprise", typeof(ObservableCollection<Entreprise>), typeof(ListeFraisControl), new UIPropertyMetadata(null));
        #endregion

        #region Constructeur
        public ListeFraisControl()
        {
            InitializeComponent();

            //Initialisation de la zone de filtrage
            this.initialisationFilterZone();

            //Initialisation des datas
            this.initialisationDataGridMain(null);

            //Je passe le usercontrol à la personnalisation de l'utilisateur
            ((App)App.Current).personnalisation.initUserControl(this);
            //Je récupère les couleurs car on ne peut les faire en automatique pour TOUS les usercontrols
            this._filterZone.Background = ((App)App.Current).personnalisation.BackGroundUserControlFilterColor;
            this._DataGridMain.RowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridColor;
            this._DataGridMain.AlternatingRowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridAlternateColor;
            this._textBlockTaillePolice.Text = "Taille police (" + this._DataGridMain.FontSize.ToString() + ") :     ";

            //Masquer la zone de filtre
            this.AfficherMasquer();

            this.creationMenuClicDroit();
        }


        #region Initialisation Zone de filtrage

        private void initialisationFilterZone()
        {
            this.initialisationPropDependance();
            this.initialisationAutoCompleteBox();
        }

        private void initialisationAutoCompleteBox()
        {
            //Variables temporaires
            List<string> listFraisLot = new List<string>();
            List<string> listNomSalarie = new List<string>();

            //Récupération des données
            foreach (Frais item in ((App)App.Current).mySitaffEntities.Frais)
            {
                //Rapport à un frais
                if (item != null)
                {
                    if (item.Lot != null)
                    {
                        listFraisLot.Add(item.Lot);
                    }


                    if (listSalarie != null && item.Salarie1 != null && item.Salarie1.Personne != null && item.Salarie1.Personne.fullname != null)
                    {
                        foreach (Salarie s in listSalarie)
                        {
                            if (s.Personne.fullname == item.Salarie1.Personne.fullname && !listNomSalarie.Contains(s.Personne.fullname))
                            {
                                listNomSalarie.Add(s.Personne.fullname);
                            }
                        }
                    }

                }
            }

            //Assignation des valeurs
            this._filterContainFraisLot.ItemsSource = listFraisLot;
            this._filterContainNomSalarie.ItemsSource = listNomSalarie;

        }

        private void initialisationPropDependance()
        {
            this.listEntreprise = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.OrderBy(e => e.Is_Client));
            this.listSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.OrderBy(s => s.Personne.Nom).ThenBy(s => s.Personne.Prenom));
        }

        #endregion

        #region Initialisation Données DataGridMain

        private void initialisationDataGridMain(ObservableCollection<Frais> listToPut)
        {
            if (listToPut == null)
            {
                this.Frais = new ObservableCollection<Frais>(((App)App.Current).mySitaffEntities.Frais.OrderBy(f => f.Date_Debut));
                this.MiseAJourEtat("", null);
            }
            else
            {
                this.Frais = new ObservableCollection<Frais>(listToPut);
                this.MiseAJourEtat("Filtrage", null);
            }
        }

        #endregion

        #region Clic droit

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

            MenuItem itemAfficher5 = new MenuItem();
            itemAfficher5.Header = "Dupliquer";
            itemAfficher5.Click += new RoutedEventHandler(delegate { this.clicDroitDupliquer(); });

            MenuItem itemAfficher8 = RemplirMenuAfficherMasquerColonnes(new MenuItem());
            itemAfficher8.Header = "Afficher / Masquer";

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
            if (((App)App.Current).securite.VerificationDroitActionsCRUD(this.ToString(), "Add"))
            {
                contextMenu.Items.Add(new Separator());
                contextMenu.Items.Add(itemAfficher5);
            }
            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(itemAfficher8);
        }

        private MenuItem RemplirMenuAfficherMasquerColonnes(MenuItem menuItem)
        {

            this.MenuItem_ColonneNomSalarie = new MenuItem();
            this.MenuItem_ColonneNomSalarie.IsChecked = true;
            this.MenuItem_ColonneNomSalarie.Header = "Nom du Salarie";
            this.MenuItem_ColonneNomSalarie.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneNomSalarie(); });
            this.AffMas_ColonneNomSalarie();
            menuItem.Items.Add(this.MenuItem_ColonneNomSalarie);


            this.MenuItem_ColonneFraisDateDebut = new MenuItem();
            this.MenuItem_ColonneFraisDateDebut.IsChecked = true;
            this.MenuItem_ColonneFraisDateDebut.Header = "Date de début du frais";
            this.MenuItem_ColonneFraisDateDebut.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFraisDateDebut(); });
            this.AffMas_ColonneFraisDateDebut();
            menuItem.Items.Add(this.MenuItem_ColonneFraisDateDebut);


            this.MenuItem_ColonneFraisDateFin = new MenuItem();
            this.MenuItem_ColonneFraisDateFin.IsChecked = true;
            this.MenuItem_ColonneFraisDateFin.Header = "Date de fin du frais";
            this.MenuItem_ColonneFraisDateFin.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFraisDateFin(); });
            this.AffMas_ColonneFraisDateFin();
            menuItem.Items.Add(this.MenuItem_ColonneFraisDateFin);



            this.MenuItem_ColonneFraisLot = new MenuItem();
            this.MenuItem_ColonneFraisLot.IsChecked = true;
            this.MenuItem_ColonneFraisLot.Header = "Lot du frais";
            this.MenuItem_ColonneFraisLot.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneFraisLot(); });
            this.AffMas_ColonneFraisLot();
            menuItem.Items.Add(this.MenuItem_ColonneFraisLot);


            this.MenuItem_ColonneTotalHT = new MenuItem();
            this.MenuItem_ColonneTotalHT.IsChecked = true;
            this.MenuItem_ColonneTotalHT.Header = "Total Hors Taxes du frais";
            this.MenuItem_ColonneTotalHT.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTotalHT(); });
            this.AffMas_ColonneTotalHT();
            menuItem.Items.Add(this.MenuItem_ColonneTotalHT);


            this.MenuItem_ColonneTotalTVA = new MenuItem();
            this.MenuItem_ColonneTotalTVA.IsChecked = true;
            this.MenuItem_ColonneTotalTVA.Header = "Total TVA du frais";
            this.MenuItem_ColonneTotalTVA.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTotalTVA(); });
            this.AffMas_ColonneTotalTVA();
            menuItem.Items.Add(this.MenuItem_ColonneTotalTVA);


            this.MenuItem_ColonneTotalTTC = new MenuItem();
            this.MenuItem_ColonneTotalTTC.IsChecked = true;
            this.MenuItem_ColonneTotalTTC.Header = "Total TTC du frais";
            this.MenuItem_ColonneTotalTTC.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTotalTTC(); });
            this.AffMas_ColonneTotalTTC();
            menuItem.Items.Add(this.MenuItem_ColonneTotalTTC);


            this.MenuItem_ColonneTotalAvance = new MenuItem();
            this.MenuItem_ColonneTotalAvance.IsChecked = true;
            this.MenuItem_ColonneTotalAvance.Header = "Total des avances du frais";
            this.MenuItem_ColonneTotalAvance.Click += new RoutedEventHandler(delegate { this.AffMas_ColonneTotalAvance(); });
            this.AffMas_ColonneTotalAvance();
            menuItem.Items.Add(this.MenuItem_ColonneTotalAvance);




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

        private void clicDroitDupliquer()
        {
            //((App)App.Current)._theMainWindow._CommandDuplicateFrais.Command.Execute(((App)App.Current)._theMainWindow)
        }

        #region Afficher / Masquer

        #region Tout

        private void AffMas_AfficherTout()
        {
            this.MenuItem_ColonneNomSalarie.IsChecked = false;
            this.MenuItem_ColonneFraisDateDebut.IsChecked = false;
            this.MenuItem_ColonneFraisDateFin.IsChecked = false;
            this.MenuItem_ColonneFraisLot.IsChecked = false;
            this.MenuItem_ColonneTotalHT.IsChecked = false;
            this.MenuItem_ColonneTotalTTC.IsChecked = false;
            this.MenuItem_ColonneTotalTVA.IsChecked = false;
            this.MenuItem_ColonneTotalAvance.IsChecked = false;


            this.AffMas_ColonneNomSalarie();
            this.AffMas_ColonneFraisDateDebut();
            this.AffMas_ColonneFraisDateFin();
            this.AffMas_ColonneFraisLot();
            this.AffMas_ColonneTotalHT();
            this.AffMas_ColonneTotalTTC();
            this.AffMas_ColonneTotalTVA();
            this.AffMas_ColonneTotalAvance();
        }

        private void AffMas_MasquerTout()
        {
            this.MenuItem_ColonneNomSalarie.IsChecked = true;
            this.MenuItem_ColonneFraisDateDebut.IsChecked = true;
            this.MenuItem_ColonneFraisDateFin.IsChecked = true;
            this.MenuItem_ColonneFraisLot.IsChecked = true;
            this.MenuItem_ColonneTotalHT.IsChecked = true;
            this.MenuItem_ColonneTotalTTC.IsChecked = true;
            this.MenuItem_ColonneTotalTVA.IsChecked = true;
            this.MenuItem_ColonneTotalAvance.IsChecked = true;


            this.AffMas_ColonneNomSalarie();
            this.AffMas_ColonneFraisDateDebut();
            this.AffMas_ColonneFraisDateFin();
            this.AffMas_ColonneFraisLot();
            this.AffMas_ColonneTotalHT();
            this.AffMas_ColonneTotalTTC();
            this.AffMas_ColonneTotalTVA();
            this.AffMas_ColonneTotalAvance();

        }

        #endregion

        private void AffMas_ColonneNomSalarie()
        {
            if (this.MenuItem_ColonneNomSalarie.IsChecked == true)
            {
                this._ColonneNomSalarie.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneNomSalarie.IsChecked = false;
            }
            else
            {
                this._ColonneNomSalarie.Visibility = Visibility.Visible;
                this.MenuItem_ColonneNomSalarie.IsChecked = true;
            }
        }


        private void AffMas_ColonneFraisDateDebut()
        {
            if (this.MenuItem_ColonneFraisDateDebut.IsChecked == true)
            {
                this._ColonneFraisDateDebut.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFraisDateDebut.IsChecked = false;
            }
            else
            {
                this._ColonneFraisDateDebut.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFraisDateDebut.IsChecked = true;
            }
        }


        private void AffMas_ColonneFraisDateFin()
        {
            if (this.MenuItem_ColonneFraisDateFin.IsChecked == true)
            {
                this._ColonneFraisDateFin.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFraisDateFin.IsChecked = false;
            }
            else
            {
                this._ColonneFraisDateFin.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFraisDateFin.IsChecked = true;
            }
        }


        private void AffMas_ColonneFraisLot()
        {
            if (this.MenuItem_ColonneFraisLot.IsChecked == true)
            {
                this._ColonneFraisLot.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneFraisLot.IsChecked = false;
            }
            else
            {
                this._ColonneFraisLot.Visibility = Visibility.Visible;
                this.MenuItem_ColonneFraisLot.IsChecked = true;
            }
        }


        private void AffMas_ColonneTotalHT()
        {
            if (this.MenuItem_ColonneTotalHT.IsChecked == true)
            {
                this._ColonneTotalHT.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTotalHT.IsChecked = false;
            }
            else
            {
                this._ColonneTotalHT.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTotalHT.IsChecked = true;
            }
        }


        private void AffMas_ColonneTotalTTC()
        {
            if (this.MenuItem_ColonneTotalTTC.IsChecked == true)
            {
                this._ColonneTotalTTC.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTotalTTC.IsChecked = false;
            }
            else
            {
                this._ColonneTotalTTC.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTotalTTC.IsChecked = true;
            }
        }


        private void AffMas_ColonneTotalTVA()
        {
            if (this.MenuItem_ColonneTotalTVA.IsChecked == true)
            {
                this._ColonneTotalTVA.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTotalTVA.IsChecked = false;
            }
            else
            {
                this._ColonneTotalTVA.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTotalTVA.IsChecked = true;
            }
        }


        private void AffMas_ColonneTotalAvance()
        {
            if (this.MenuItem_ColonneTotalAvance.IsChecked == true)
            {
                this._ColonneTotalAvance.Visibility = Visibility.Collapsed;
                this.MenuItem_ColonneTotalAvance.IsChecked = false;
            }
            else
            {
                this._ColonneTotalAvance.Visibility = Visibility.Visible;
                this.MenuItem_ColonneTotalAvance.IsChecked = true;
            }
        }


        #endregion

        #endregion

        #endregion

        #region Fenêtre chargée
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((MainWindow)((Grid)((Border)this.Parent).Parent).Parent).stopThread();
            AffMas_AfficherTout();
        }
        #endregion

        #region CRUD (Create Read Update Delete)
        /// <summary>
        /// Ajoute un nouveau Frais à la liste à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Frais Add()
        {
            bool FraisExist = false;
            //Affichage du message "ajout en cours"
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Ajout d'un Frais en cours ...";

            //Initialisation de la fenêtre
            FraisWindow fraisWindow = new FraisWindow();

            foreach (Frais f in Frais.OfType<Frais>())
            {
                if (f.Date_Debut.Value.Month == DateTime.Today.Month)
                {
                    if (fraisWindow.GetListAdministrateur() != null && f.Salarie1 != null &&  f.Salarie1.Salarie_Interne != null && f.Salarie1.Salarie_Interne.Utilisateur != null && f.Salarie1.Salarie_Interne.Utilisateur.Contains(((App)App.Current)._connectedUser) && !fraisWindow.GetListAdministrateur().Contains(((App)App.Current)._connectedUser.Salarie_Interne1.Utilisateur.ElementAtOrDefault(0).Nom_Utilisateur))
                    {
                        FraisExist = true;
                        this._DataGridMain.SelectedItem = f;
                        break;
                    }
                    else
                    {
                        FraisExist = false;
                    }
                }
            }
            if (FraisExist)
            {
                MessageBox.Show("Il existe déjà un frais pour ce mois.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            if (!FraisExist)
            {
                if (fraisWindow.GetListAdministrateur().Contains(((App)App.Current)._connectedUser.Salarie_Interne1.Utilisateur.ElementAtOrDefault(0).Nom_Utilisateur))
                {
                    fraisWindow._comboBoxNomSalarie.IsEnabled = true;
                }
                fraisWindow._datePickerDateDeDebut.IsEnabled = true;
                //booléen nullable vrai ou faux ou null
                bool? dialogResult = fraisWindow.ShowDialog();


                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    //Si j'appuie sur le bouton Ok, je renvoi l'objet frais dans le datacontext de la fenêtre
                    return ((Frais)fraisWindow.DataContext);
                }
                else
                {
                    try
                    {
                        //Detachement de tous les éléments 
                        ((App)App.Current).mySitaffEntities.Detach((Frais)fraisWindow.DataContext);
                    }
                    catch (Exception)
                    {

                    }
                    //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    this.recalculMax();
                    ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Ajout d'un Frais annulé : " + this.Frais.Count() + " / " + this.max;

                    return null;
                }
            }
            else
            {
                try
                {
                    //Detachement de tous les éléments 
                    ((App)App.Current).mySitaffEntities.Detach((Frais)fraisWindow.DataContext);
                }
                catch (Exception)
                {

                }
                //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                this.recalculMax();
                ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Ajout d'un Frais annulé : " + this.Frais.Count() + " / " + this.max;

                return null;
            }

        }

        /// <summary>
        /// Ouvre le Frais séléctionné à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Frais Open()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "modification en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Modification d'un Frais en cours ...";

                    bool EstAutoriser = false;
                    FraisWindow fraisWindowtemp = new FraisWindow();
                    //test si le frais est rattacher a l'utilisateur courant ou si l'utilisateur courant est un administrateur du module
                    if (((Frais)this._DataGridMain.SelectedItem).Salarie1 != null && ((Frais)this._DataGridMain.SelectedItem).Salarie1.Salarie_Interne != null && ((Frais)this._DataGridMain.SelectedItem).Salarie1.Salarie_Interne.Utilisateur != null &&  ((Frais)this._DataGridMain.SelectedItem).Salarie1.Salarie_Interne.Utilisateur.Contains(((App)App.Current)._connectedUser) || fraisWindowtemp.GetListAdministrateur().Contains(((App)App.Current)._connectedUser.Salarie_Interne1.Utilisateur.ElementAtOrDefault(0).Nom_Utilisateur))
                    {
                        EstAutoriser = true;
                    }


                    if (EstAutoriser)
                    {

                        //Création de la fenêtre
                        FraisWindow fraisWindow = new FraisWindow();
                        fraisWindow._comboBoxNomSalarie.IsEnabled = false;
                        fraisWindow._datePickerDateDeDebut.IsEnabled = true;
                        //Initialisation du Datacontext en Frais et association au Frais sélectionné
                        fraisWindow.DataContext = new Frais();
                        fraisWindow.DataContext = (Frais)this._DataGridMain.SelectedItem;

                        //booléen nullable vrai ou faux ou null
                        bool? dialogResult = fraisWindow.ShowDialog();

                        if (dialogResult.HasValue && dialogResult.Value == true)
                        {
                            //Si j'appuie sur le bouton Ok, je renvoi l'objet Frais dans le datacontext de la fenêtre
                            return (Frais)fraisWindow.DataContext;
                        }
                        else
                        {
                            //Je récupère les anciennes données de la base sur les modifications effectuées
                            ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Frais)(this._DataGridMain.SelectedItem));
                            //je refresh l'edmx et je relance le filtrage s'il y en avait un afin d'avoir les mêmes infos (invisible pour l'user)
                            ((App)App.Current).refreshEDMXSansVidage();
                            this.filtrage();

                            //Si j'appuie sur le bouton annuler, je préviens que j'annule ma modification
                            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                            this.recalculMax();
                            ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Modification d'un Frais annulé : " + this.Frais.Count() + " / " + this.max;

                            return null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ce n'est pas un de vos frais.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un Frais.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un Frais.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Supprime le Frais séléctionné avec une confirmation
        /// </summary>
        public Frais Remove()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "suppression en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Suppression d'un frais en cours ...";

                    bool test = true;

                    if (test)
                    {
                        if (MessageBox.Show("Voulez-vous rééllement supprimer le frais séléctionné ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            int cmpt = 0;
                            int cmptligne = 0;

                            if (((Frais)this._DataGridMain.SelectedItem).Fiche_Frais != null && ((Frais)this._DataGridMain.SelectedItem).Fiche_Frais.Count != 0)
                            {
                                cmpt = 0;
                                while (cmpt < ((Frais)this._DataGridMain.SelectedItem).Fiche_Frais.Count)
                                {
                                    if (((Frais)this._DataGridMain.SelectedItem).Fiche_Frais.ElementAt(cmpt).Ligne_Fiche_Frais != null && ((Frais)this._DataGridMain.SelectedItem).Fiche_Frais.ElementAt(cmpt).Ligne_Fiche_Frais.Count > 0)
                                    {
                                        while (cmptligne < ((Frais)this._DataGridMain.SelectedItem).Fiche_Frais.ElementAt(cmpt).Ligne_Fiche_Frais.Count)
                                        {
                                            try
                                            {
                                                ((App)App.Current).mySitaffEntities.Ligne_Fiche_Frais.DeleteObject(((Frais)this._DataGridMain.SelectedItem).Fiche_Frais.ElementAt(cmpt).Ligne_Fiche_Frais.ElementAt(cmptligne));
                                            }
                                            catch (Exception)
                                            {
                                                try
                                                {
                                                    ((App)App.Current).mySitaffEntities.Ligne_Fiche_Frais.Detach(((Frais)this._DataGridMain.SelectedItem).Fiche_Frais.ElementAt(cmpt).Ligne_Fiche_Frais.ElementAt(cmptligne));

                                                }
                                                catch (Exception)
                                                {
                                                    ((App)App.Current).mySitaffEntities.Detach(((Frais)this._DataGridMain.SelectedItem).Fiche_Frais.ElementAt(cmpt).Ligne_Fiche_Frais.ElementAt(cmptligne));
                                                }
                                            }
                                            cmptligne++;
                                        }
                                    }
                                    try
                                    {
                                        ((App)App.Current).mySitaffEntities.Detach(((Frais)this._DataGridMain.SelectedItem).Fiche_Frais.ElementAt(cmpt));
                                        ((App)App.Current).mySitaffEntities.Fiche_Frais.DeleteObject(((Frais)this._DataGridMain.SelectedItem).Fiche_Frais.ElementAt(cmpt));

                                    }
                                    catch (Exception)
                                    {
                                        try
                                        {
                                            ((App)App.Current).mySitaffEntities.Fiche_Frais.Detach(((Frais)this._DataGridMain.SelectedItem).Fiche_Frais.ElementAt(cmpt));

                                        }
                                        catch (Exception)
                                        {
                                            ((App)App.Current).mySitaffEntities.Detach(((Frais)this._DataGridMain.SelectedItem).Fiche_Frais.ElementAt(cmpt));
                                        }
                                    }
                                    cmpt++;
                                }
                            }




                            if (((Frais)this._DataGridMain.SelectedItem).Frais_Km != null && ((Frais)this._DataGridMain.SelectedItem).Frais_Km.Count != 0)
                            {
                                cmpt = 0;
                                if (((Frais)this._DataGridMain.SelectedItem).Frais_Km.Count != 0)
                                {
                                    while (cmpt < ((Frais)this._DataGridMain.SelectedItem).Frais_Km.Count)
                                    {
                                        try
                                        {
                                            ((App)App.Current).mySitaffEntities.Frais_Km.Detach(((Frais)this._DataGridMain.SelectedItem).Frais_Km.ElementAt(cmpt));
                                            ((App)App.Current).mySitaffEntities.Frais_Km.DeleteObject(((Frais)this._DataGridMain.SelectedItem).Frais_Km.ElementAt(cmpt));
                                        }
                                        catch (Exception)
                                        {
                                            ((App)App.Current).mySitaffEntities.Detach(((Frais)this._DataGridMain.SelectedItem).Frais_Km.ElementAt(cmpt));
                                            ((App)App.Current).mySitaffEntities.DeleteObject(((Frais)this._DataGridMain.SelectedItem).Frais_Km.ElementAt(cmpt));
                                            try
                                            {
                                            }
                                            catch (Exception) { }
                                        }
                                        cmpt++;
                                    }
                                }
                            }




                            if (((Frais)this._DataGridMain.SelectedItem).Avance != null && ((Frais)this._DataGridMain.SelectedItem).Avance.Count != 0)
                            {
                                cmpt = 0;
                                while (cmpt < ((Frais)this._DataGridMain.SelectedItem).Avance.Count)
                                {
                                    if (((Frais)this._DataGridMain.SelectedItem).Avance.ElementAt(cmpt).Salarie1 == null)
                                    {
                                        try
                                        {
                                            ((App)App.Current).mySitaffEntities.Avance.Detach(((Frais)this._DataGridMain.SelectedItem).Avance.ElementAt(cmpt));
                                            ((App)App.Current).mySitaffEntities.Avance.DeleteObject(((Frais)this._DataGridMain.SelectedItem).Avance.ElementAt(cmpt));
                                        }
                                        catch (Exception)
                                        {
                                            ((App)App.Current).mySitaffEntities.Detach(((Frais)this._DataGridMain.SelectedItem).Avance.ElementAt(cmpt));
                                            ((App)App.Current).mySitaffEntities.DeleteObject(((Frais)this._DataGridMain.SelectedItem).Avance.ElementAt(cmpt));
                                        }
                                    }
                                    cmpt++;
                                }
                            }

                            return ((Frais)this._DataGridMain.SelectedItem);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul frais.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un frais.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
            return null;
        }

        /// <summary>
        /// Ouvre le Frais séléctionné en lecture seule à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Frais Look(Frais frais)
        {
            if (this._DataGridMain.SelectedItem != null || frais != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1 || frais != null)
                {
                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Affichage d'un frais en cours ...";

                    //Création de la fenêtre
                    FraisWindow fraisWindow = new FraisWindow();
                    
                    //Initialisation du Datacontext en Frais 
                    fraisWindow.DataContext = new Frais();
                    if (frais != null)
                    {
                        fraisWindow.DataContext = frais;
                    }
                    else
                    {
                        fraisWindow.DataContext = (Frais)this._DataGridMain.SelectedItem ;
                    }

                    //Je positionne la lecture seule sur la fenêtre
                    fraisWindow.lectureSeule();

                    //J'affiche la fenêtre
                    bool? dialogResult = fraisWindow.ShowDialog();

                    //Affichage du message "affichage en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                    ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Affichage d'un frais terminé : " + this.Frais.Count() + " / " + this.max;

                    //Renvoi null
                    return null;
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul frais.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un frais.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }


        #endregion

        #region Action Supplémentaire
        /// <summary>
        /// duplique (a voir quoi dupliqué) à l'aide d'une nouvelle fenêtre
        /// </summary>
        public Frais Duplicate()
        {
            if (this._DataGridMain.SelectedItem != null)
            {
                if (this._DataGridMain.SelectedItems.Count == 1)
                {
                    //Affichage du message "ajout en cours"
                    ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
                    ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Dupliquer un frais en cours ...";

                    //Création de la fenêtre
                    FraisWindow fraisWindow = new FraisWindow();

                    //Duplication du frais sélectionnée
                    Frais tmp = new Frais();
                    tmp = duplicateFrais((Frais)this._DataGridMain.SelectedItem);


                    //Association de l'élement dupliqué au datacontext de la fenêtre
                    fraisWindow.DataContext = tmp;

                    //booléen nullable vrai ou faux ou null
                    bool? dialogResult = fraisWindow.ShowDialog();

                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        return (Frais)fraisWindow.DataContext;
                    }
                    else
                    {
                        try
                        {
                            foreach (Fiche_Frais ff in ((Frais)fraisWindow.DataContext).Fiche_Frais.OfType<Fiche_Frais>())
                            {
                                foreach (Ligne_Fiche_Frais lff in ff.Ligne_Fiche_Frais.OfType<Ligne_Fiche_Frais>())
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(((Frais)fraisWindow.DataContext).Fiche_Frais.FirstOrDefault().Ligne_Fiche_Frais.First());
                                }
                                ((App)App.Current).mySitaffEntities.Detach(((Frais)fraisWindow.DataContext).Fiche_Frais.FirstOrDefault());
                            }

                            ((App)App.Current).mySitaffEntities.Detach((Frais)fraisWindow.DataContext);
                        }
                        catch (Exception)
                        {

                        }
                        //Si j'appuie sur le bouton annuler, je préviens que j'annule mon ajout
                        ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
                        this.recalculMax();
                        ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Duplication d'un frais annulé : " + this.Frais.Count() + " / " + this.max;

                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Vous ne devez sélectionner qu'un seul frais.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un frais.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }
        #endregion

        #region Filtrages

        #region Remise à zéro

        private void _buttonRaz_Click(object sender, RoutedEventArgs e)
        {
            this.remiseAZero();
        }

        private void remiseAZero()
        {
            //Remise à zéro de tous les champs de filtrage
            //Text
            this._filterContainFraisLot.Text = String.Empty;
            this._filterContainNomSalarie.Text = String.Empty;



            //Dates
            this._datePickerFraisDateDebut.SelectedDate = null;
            this._datePickerFraisDateFin.SelectedDate = null;


            //Rechargement des élements
            this.initialisationDataGridMain(null);
        }

        #endregion

        #region Button Filtrer

        private void _buttonFiltrer_Click(object sender, RoutedEventArgs e)
        {
            this.filtrage();
        }

        private void filtrage()
        {
            ((App)App.Current)._theMainWindow._mutex.WaitOne();
            ((App)App.Current)._theMainWindow.startThread();
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = true;
            ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Filtrage en cours ...";

            ObservableCollection<Frais> listToPut = new ObservableCollection<Frais>(((App)App.Current).mySitaffEntities.Frais.OrderBy(f => f.Date_Debut));

            if (this._filterContainFraisLot.Text != "")
            {
                listToPut = new ObservableCollection<Frais>(listToPut.Where(f => f.Lot != null));
                listToPut = new ObservableCollection<Frais>(listToPut.Where(f => f.Lot.Trim().ToLower().Contains(this._filterContainFraisLot.Text.Trim().ToLower())));
            }

            if (this._filterContainNomSalarie.Text != "")
            {
                listToPut = new ObservableCollection<Frais>(listToPut.Where(f => f.Salarie1.Personne.Nom != null));
                listToPut = new ObservableCollection<Frais>(listToPut.Where(f => f.Salarie1.Personne.Nom.Trim().ToLower().Contains(this._filterContainNomSalarie.Text.Trim().ToLower())));
            }


            if (this._datePickerFraisDateDebut.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Frais>(listToPut.Where(f => f.Date_Debut >= this._datePickerFraisDateDebut.SelectedDate).Where(f => f.Date_Debut >= this._datePickerFraisDateDebut.SelectedDate));
            }
            if (this._datePickerFraisDateFin.SelectedDate != null)
            {
                listToPut = new ObservableCollection<Frais>(listToPut.Where(f => f.Date_Fin <= this._datePickerFraisDateFin.SelectedDate).Where(f => f.Date_Fin <= this._datePickerFraisDateFin.SelectedDate));
            }

            ((App)App.Current)._theMainWindow.stopThread();

            //Insertion des données dans le datagrid
            this.initialisationDataGridMain(listToPut);

            //Si aucun résultat, j'affiche un message
            if (this.Frais.Count() == 0)
            {
                MessageBox.Show("Aucun résultat ne correspont à votre recherche.", "Aucun résultat", MessageBoxButton.OK);
            }
        }

        #endregion

        #region bouton masquer / afficher

        private void _buttonMasqueFiltre_Click(object sender, RoutedEventArgs e)
        {
            this.AfficherMasquer();
        }

        public void AfficherMasquer()
        {

            if (_filterZone.Height != 21)
            {
                this._filterZone.Height = 21;
                this._buttonMasqueFiltre.Content = "Afficher les filtres";
                //Lorsque je masque, je remet à zéro si certains champs sont rempli OU si le nombre d'élements max n'est pas égal au nombre d'éléments actuel
                if (this._filterContainFraisLot.Text != "" || this._filterContainNomSalarie.Text != "" || this._datePickerFraisDateDebut.SelectedDate != null || this._datePickerFraisDateFin.SelectedDate != null || this.max != this.Frais.Count())
                {
                    this.remiseAZero();
                }
            }
            else
            {
                this._filterZone.Height = double.NaN;
                this._buttonMasqueFiltre.Content = "Masquer les filtres";
                //Je me positionne sur le premier champ
                this._filterContainFraisLot.Focus();
            }
        }

        #endregion

        #region nullBox

        private void _buttonFraisDateDebutNull_Click(object sender, RoutedEventArgs e)
        {
            this._datePickerFraisDateDebut.SelectedDate = null;
        }

        private void _buttonFraisDateFinNull_Click(object sender, RoutedEventArgs e)
        {
            this._datePickerFraisDateFin.SelectedDate = null;
        }

        #endregion
        #endregion

        #region Evènements

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

        #endregion

        #endregion

        #region Fonctions
        /// <summary>
        /// Recalcul le nombre d'élements maximum
        /// </summary>
        private void recalculMax()
        {
            this.max = ((App)App.Current).mySitaffEntities.Frais.Count();
        }

        /// <summary>
        /// Met à jour l'état en bas pour l'utilisateur
        /// </summary>
        /// <param name="typeEtat">texte : "Filtrage", "Ajout", "Modification", "Suppression", "Look", "" ("" = Chargement)</param>
        /// <param name="dao">un objet Frais soit pour l'ajouter au listing, soit pour afficher qui a été modifié ou supprimé</param>
        public void MiseAJourEtat(string typeEtat, Frais f)
        {
            //Je recalcul le nombre max d'élements
            this.recalculMax();
            //En fonction de l'action, j'affiche le message
            if (typeEtat == "Filtrage")
            {
                ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Filtrage des frais terminé : " + this.Frais.Count() + " / " + this.max;
            }
            else if (typeEtat == "Ajout")
            {
                //J'ajoute le frais dans le listing
                this.Frais.Add(f);
                //Je recalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Ajout d'un frais effectué avec succès. Nombre d'élements : " + this.Frais.Count() + " / " + this.max;
                try
                {
                    this._DataGridMain.SelectedItem = f;
                }
                catch (Exception) { }
            }
            else if (typeEtat == "Modification")
            {
                //Je raffraichis mon datagrid
                this._DataGridMain.Items.Refresh();
                ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Modification du frais effectué avec succès. Nombre d'élements : " + this.Frais.Count() + " / " + this.max;
            }
            else if (typeEtat == "Suppression")
            {
                //Je supprime de mon listing l'élément supprimé
                this.Frais.Remove(f);
                //Je recalcul le nombre max d'élements après la suppression
                this.recalculMax();
                ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Suppression du frais avec succès. Nombre d'élements : " + this.Frais.Count() + " / " + this.max;
            }
            else if (typeEtat == "Look")
            {

            }
            else if (typeEtat == "Duplicate")
            {
                //J'ajoute le frais dans le linsting
                this.Frais.Add(f);
                //Je recalcul le nombre max d'élements après l'ajout
                this.recalculMax();
                ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Dupliquer un frais effectué avec succès. Nombre d'élements : " + this.Frais.Count() + " / " + this.max;
                try
                {
                    this._DataGridMain.SelectedItem = f;
                }
                catch (Exception) { }
            }
            else
            {
                ((App)App.Current)._theMainWindow.textBlockMainWindow.Text = "Chargement des frais terminé : " + this.Frais.Count() + " / " + this.max;
            }
            //Je retri les données dans le sens par défaut
            this.triDatas();
            this._DataGridMain.Items.Refresh();
            //J'arrete la progressbar
            ((App)App.Current)._theMainWindow.progressBarMainWindow.IsIndeterminate = false;
        }

        /// <summary>
        /// Tri les données dans le sens par défaut
        /// </summary>
        private void triDatas()
        {
            this.Frais = new ObservableCollection<Frais>(this.Frais.OrderBy(f => f.Date_Debut));
            try
            {
                for (int i = 0; i < this.Frais.Count; i++)
                {
                    if (this.Frais.ElementAt(i) == null || this.Frais.ElementAt(i).Salarie1 == null || String.IsNullOrEmpty(this.Frais.ElementAt(i).Lot))
                    {
                        ((App)App.Current).mySitaffEntities.DeleteObject(this.Frais.ElementAt(i));
                        ((App)App.Current).mySitaffEntities.SaveChanges();
                        this.MiseAJourEtat("Suppression", this.Frais.ElementAt(i));
                    }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Duplique le frais passé en paramètre
        /// </summary>
        /// <param name="commande1">frais à dupliquer</param>
        private Frais duplicateFrais(Frais itemToCopy)
        {
            Frais tmp = new Frais();

            foreach (Avance item in itemToCopy.Avance)
            {
                tmp.Avance.Add(item);
            }
            tmp.Date_Debut = itemToCopy.Date_Debut;
            tmp.Date_Fin = itemToCopy.Date_Fin;
            tmp.Lot = itemToCopy.Lot;


            foreach (Fiche_Frais itemff in itemToCopy.Fiche_Frais)
            {
                tmp.Fiche_Frais.Add(itemff);

            }


            foreach (Frais_Km itemfk in itemToCopy.Frais_Km)
            {
                tmp.Frais_Km.Add(itemfk);
            }

            tmp.Total_A_Rembourser = itemToCopy.Total_A_Rembourser;
            tmp.Total_Avance = itemToCopy.Total_Avance;
            tmp.Total_HT = itemToCopy.Total_HT;
            tmp.Total_TTC = itemToCopy.Total_TTC;
            tmp.Total_TVA = itemToCopy.Total_TVA;


            return tmp;
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
