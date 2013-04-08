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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SitaffRibbon.Classes;
using System.Diagnostics;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour FicheFraisWindow.xaml
    /// </summary>
    public partial class FicheFraisWindow : Window
    {

        #region Attribut
        private bool affichageAdministrateur; //true si l'affichage doit se mettre en administrateur; false si salarié
        private bool Etranger;
        #endregion

        #region Propriété de dépendances

        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(FicheFraisWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Contact> listContactClient
        {
            get { return (ObservableCollection<Contact>)GetValue(listContactClientProperty); }
            set { SetValue(listContactClientProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listContactClient.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listContactClientProperty =
            DependencyProperty.Register("listContactClient", typeof(ObservableCollection<Contact>), typeof(FicheFraisWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Entreprise> listEntreprise
        {
            get { return (ObservableCollection<Entreprise>)GetValue(listEntrepriseProperty); }
            set { SetValue(listEntrepriseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseProperty =
            DependencyProperty.Register("listEntreprise", typeof(ObservableCollection<Entreprise>), typeof(FicheFraisWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(FicheFraisWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Type_Frais> listTypeFrais
        {
            get { return (ObservableCollection<Type_Frais>)GetValue(listTypeFraisProperty); }
            set { SetValue(listTypeFraisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntreprise.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listTypeFraisProperty =
            DependencyProperty.Register("listTypeFrais", typeof(ObservableCollection<Type_Frais>), typeof(FicheFraisWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Plan_Comptable_Imputation> listPlanComptableImputation
        {
            get { return (ObservableCollection<Plan_Comptable_Imputation>)GetValue(listPlanComptableImputationProperty); }
            set { SetValue(listPlanComptableImputationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listPlanComptableImputation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listPlanComptableImputationProperty =
            DependencyProperty.Register("listPlanComptableImputation", typeof(ObservableCollection<Plan_Comptable_Imputation>), typeof(FicheFraisWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Plan_Comptable_Tva> listPlanComptableTva
        {
            get { return (ObservableCollection<Plan_Comptable_Tva>)GetValue(listPlanComptableTvaProperty); }
            set { SetValue(listPlanComptableTvaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntreprise.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listPlanComptableTvaProperty =
            DependencyProperty.Register("listPlanComptableTva", typeof(ObservableCollection<Plan_Comptable_Tva>), typeof(FicheFraisWindow), new UIPropertyMetadata(null));



        #endregion

        #region Initialisation
        private void initialisationPropDependance()
        {
            this.listContactClient = new ObservableCollection<Contact>();
            this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Identifiant));
            this.listSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.OrderBy(sal => sal.Personne.Nom));
            this.listEntreprise = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.Where(e => e.Is_Client == true).OrderBy(e => e.Libelle));
            this.listTypeFrais = new ObservableCollection<Type_Frais>(((App)App.Current).mySitaffEntities.Type_Frais.OrderBy(tf => tf.Libelle));
            this.listPlanComptableImputation = new ObservableCollection<Plan_Comptable_Imputation>(((App)App.Current).mySitaffEntities.Plan_Comptable_Imputation.OrderBy(pci => pci.Numero));
            this.listPlanComptableTva = new ObservableCollection<Plan_Comptable_Tva>(((App)App.Current).mySitaffEntities.Plan_Comptable_Tva.OrderBy(pct => pct.Numero));
        }

        private void initialisationSecurite()
        {

        }
        #endregion

        #region Constructeur
        public FicheFraisWindow()
        {
            InitializeComponent();

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Initialisation de la sécurité
            this.initialisationSecurite();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);
        }
        #endregion

        #region Fenêtre chargée
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //affiche ou cache des colonnes suivant les noms dans 'initialisationListAdministrateur()'
            affichageAdministrateur = ChoixAffichage();

            //Defini le mois courant a afficher ( n'affichera que le mois courant)
            int annecourante = 0;
            int moiscourant = 0;

            DateTime dstart = new DateTime();
            DateTime dend = new DateTime();
            if (this.DataContext != null) // si le datacontext n'est pas null on prend la date de la fiche
            {
                try
                {
                    dstart = ((Fiche_Frais)this.DataContext).Frais1.Date_Debut.Value;
                    dend = ((Fiche_Frais)this.DataContext).Frais1.Date_Fin.Value;
                }
                catch (Exception) { }
            }
            else
            {
                int.TryParse(DateTime.Today.Year.ToString(), out annecourante);
                int.TryParse(DateTime.Today.Month.ToString(), out moiscourant);
                DateTime.TryParse(DateTime.DaysInMonth(annecourante, moiscourant) + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString(), out dend);
                DateTime.TryParse("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year, out dstart);
            }


            this._datePickerDate.BlackoutDates.Clear();

            //initialisation des date des fiches de frais déjà existante pour ne pas pouvoir créé 2 fiche de frais avec la même date
            //Erreur non trouver : n'affiche pas les dates avant celle d'aujourd'hui
            /*foreach (Fiche_Frais item in ((Fiche_Frais)this.DataContext).Frais1.Fiche_Frais.OfType<Fiche_Frais>())
            {
                if (item.Date_Fiche != ((Fiche_Frais)this.DataContext).Date_Fiche)
                {
                    this._datePickerDate.BlackoutDates.Add(new CalendarDateRange(item.Date_Fiche.Value, item.Date_Fiche.Value));

                }
                else if (item.Date_Fiche == DateTime.Today.Date && item.Date_Fiche == ((Fiche_Frais)this.DataContext).Date_Fiche)
                {
                    if (((Fiche_Frais)this.DataContext).Date_Fiche.Value.AddDays(1).Month.CompareTo(moiscourant) < 0)
                    {
                        ((Fiche_Frais)this.DataContext).Date_Fiche = dend;
                        this._datePickerDate.BlackoutDates.Add(new CalendarDateRange(item.Date_Fiche.Value, item.Date_Fiche.Value));
                    }
                }
            }*/

            this._datePickerDate.DisplayDateStart = dstart;//defini la date de début du mois courant
            this._datePickerDate.DisplayDateEnd = dend;//defini la date de fin du mois courant

            this._dataGridLignesNotesDeFrais.AlternatingRowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridAlternateColor;

            this._gridContact.Visibility = System.Windows.Visibility.Collapsed;//cache la grille de contact 

            ColumnIsChecked_Datagrid();//cache les colonnes selon les checkbox du datagrid (Etranger)
            this._dataGridLignesNotesDeFrais.Items.Refresh();
        }
        #endregion

        #region Boutons
        //Enregistre les Lignes de Frais dans la Fiche_Frais correspondante
        private void _buttonOk_Click(object sender, RoutedEventArgs e)
        {
            CalculFicheFrais();
            if (verificationLigneFicheFrais())
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        //Sort de la fenêtre sans sauvegarder
        private void _buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        //Colle une ou plusieurs Lignes
        private void _buttonCollerLigneFrais_Click(object sender, RoutedEventArgs e)
        {

            CopierColler ClassPaste = new CopierColler();
            ObservableCollection<Ligne_Fiche_Frais> listToAdd = ClassPaste.PasteDataLigneFraisWindow(affichageAdministrateur, Etranger);
            if (listToAdd != null)
            {
                foreach (Ligne_Fiche_Frais lff in listToAdd)
                {
                    ((App)App.Current).mySitaffEntities.AddToLigne_Fiche_Frais(lff);
                    ((Fiche_Frais)this.DataContext).Ligne_Fiche_Frais.Add(lff);
                }
                this._dataGridLignesNotesDeFrais.Items.Refresh();
                CalculFicheFrais();
            }

        }

        //Supprime une ou plusieurs lignes de frais
        private void _buttonSupprimerLigneFrais_Click(object sender, RoutedEventArgs e)
        {
            DeleteLigneFrais();
            CalculFicheFrais();
        }

        //Ouvre une fenêtre pour ajouter un nouveau contact
        private void _buttonAjouterNouveauContactGridContact_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow contactWindow = new ContactWindow();
            Contact tmp = new Contact();

            contactWindow.DataContext = tmp;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = contactWindow.ShowDialog();
            Contact contact = (Contact)contactWindow.DataContext;

            if (dialogResult.HasValue == true && dialogResult.Value == false)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(contact);
                }
                catch (Exception)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Contact.DeleteObject(contact);
                    }
                    catch (Exception)
                    {

                    }
                }
            }

        }

        //Ouvre une page internet 
        private void _buttonAfficherSiteConverteurMonaie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process p = new Process();
                string chemin = "http://www.xe.com/ucc/convert/?Amount=0&From=UAH&To=EUR";
                if (((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Etranger && this._dataGridLignesNotesDeFrais.SelectedItems.Count == 1 && this._dataGridLignesNotesDeFrais.SelectedItem != null)
                {
                    chemin = "http://www.xe.com/ucc/convert/?Amount=" + ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).TTC_Sur_Ticket.ToString().Replace(",", ".") + "&From=UAH&To=EUR";
                }
                p.StartInfo.FileName = chemin;
                p.Start();
            }
            catch (Exception) { }
        }

        //Rempli les lignes de frais avec les informations du 'Type_Frais' choisi
        private void _buttonRemplirEtCalcul_Click(object sender, RoutedEventArgs e)
        {
            RemplirDataGridAvecDonneDuTypeFrais();
            ColumnIsChecked_Datagrid();
            RefreshListeContactClient();
        }

        #endregion

        #region Verification

        /* Test si les lignes de fiche frais sont valide
         * si vrai : met en vert (((App)App.Current).verifications.Couleur_Champ_Ok) les lignes
         * sinon : met en rouge (((App)App.Current).verifications.Couleur_Champ_Non_Ok) les lignes et met en ToolTip ce qui manque
        */
        private bool verificationLigneFicheFrais()
        {
            bool verif = false;
            bool verifTauxChange = false;
            bool verifTypeFrais = false;
            bool verifTTCticket = false;
            bool verifEntreprise = false;
            bool verifClientObligatoire = true;
            bool verifAffaire = false;
            string messageErreur = String.Empty;
            if (this._dataGridLignesNotesDeFrais.HasItems)
            {
                foreach (Ligne_Fiche_Frais lff in this._dataGridLignesNotesDeFrais.Items.OfType<Ligne_Fiche_Frais>())//pour chaque ligne fiche frais
                {
                    verifTauxChange = false;
                    verifTypeFrais = false;
                    verifTTCticket = false;
                    verifEntreprise = false;
                    verifClientObligatoire = true;
                    messageErreur = String.Empty;
                    if (lff != null)//la ligne est valide
                    {
                        if (affichageAdministrateur) // si on est dans l'affichage administrateur  
                        {
                            if (lff.Etranger)
                            {
                                if (lff.Taux_Change_Reel != null && lff.Taux_Change_Reel != 0)//si le taux de change reel est valide
                                {
                                    verifTauxChange = true;
                                }
                                else
                                {
                                    messageErreur += "Taux de change réel égal a 0; ";
                                }
                            }
                            else
                            {
                                lff.Taux_Change = 1;
                                lff.Taux_Change_Reel = 1;
                                lff.TTC_En_Euro = lff.TTC_Sur_Ticket;
                                verifTauxChange = true;
                            }
                        }
                        else
                        {
                            if (lff.Etranger)
                            {
                                if (lff.Taux_Change != null && lff.Taux_Change != 0)//si le taux de change est valide
                                {
                                    verifTauxChange = true;
                                }
                                else
                                {
                                    messageErreur += "Taux de change égal a 0; ";
                                }
                            }
                            else
                            {
                                lff.Taux_Change = 1;
                                lff.Taux_Change_Reel = 1;
                                lff.TTC_En_Euro = lff.TTC_Sur_Ticket;
                                verifTauxChange = true;
                            }
                        }

                        if (!lff.Imputer_Affaire)
                        {
                            if (lff.Affaire1 == null)
                            {
                                verifAffaire = true;
                            }
                        }
                        else
                        {
                            if (lff.Affaire1 == null)
                            {
                                messageErreur += "Affaire non renseigner; ";
                            } 
                        }

                        if (lff.TTC_Sur_Ticket != null && lff.TTC_Sur_Ticket > 0)
                        {
                            verifTTCticket = true;
                        }
                        else
                        {
                            messageErreur += "TTC sur ticket inférieur ou égal a 0; ";
                        }

                        if (lff.Type_Frais1 != null && !String.IsNullOrEmpty(lff.Type_Frais1.Libelle))
                        {
                            verifTypeFrais = true;

                            if (lff.Type_Frais1.Client_Obligatoire)
                            {
                                if (lff.Entreprise1 == null)
                                {
                                    messageErreur += "Entreprise non renseigné; ";
                                    verifClientObligatoire = false;
                                }
                            }

                            if (lff.Type_Frais1.Entreprise_Mere1 == null)
                            {
                                verifEntreprise = true;
                            }
                            else
                            {
                                if (lff.Entreprise1 == lff.Type_Frais1.Entreprise_Mere1.Entreprise1)
                                {
                                    verifEntreprise = true;
                                }
                                else
                                {
                                    messageErreur += "Entreprise non correspondante a l'entreprise du type de frais; ";
                                }
                            }

                        }
                        else
                        {
                            messageErreur += "Type de frais n'est pas renseigné; ";
                        }

                    }
                    DataGridRow row = (DataGridRow)this._dataGridLignesNotesDeFrais.ItemContainerGenerator.ContainerFromItem(lff);

                    if (!verifTauxChange || !verifTTCticket || !verifTypeFrais || !verifEntreprise || !verifClientObligatoire || !verifAffaire)// si au moins une des verification est fausse
                    {
                        if (row != null)
                        {
                            row.ToolTip = String.Empty;
                            row.ToolTip = messageErreur;
                            row.Background = ((App)App.Current).verifications.Couleur_Champ_Non_Ok;
                        }
                    }
                    else//sinon, on met la ligne en couleur OK
                    {
                        if (row != null)
                        {
                            row.ToolTip = String.Empty;
                            row.Background = ((App)App.Current).verifications.Couleur_Champ_Ok;
                            verif = true;
                        }
                    }

                }
            }
            if (this._dataGridLignesNotesDeFrais.Items[0].ToString() == "{NewItemPlaceholder}")
            {
                verif = false;
            }

            if (!VerificationDateFicheFrais())
            {
                verif = false;
            }

            return verif;
        }

        private bool VerificationDateFicheFrais()
        {
            return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDate, this._textBlockDateNotesDeFrais);
        }

        #endregion

        #region Evenements
        //Pour recharger les colonnes + la liste des Contact du client si besoin + recalcul + verification des lignes
        private void _dataGridLignesNotesDeFrais_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ColumnIsChecked_Datagrid();
            CalculFicheFrais();
            RefreshListeContactClient();
            verificationLigneFicheFrais();
        }

        //Pour ajouter a la liste d'inviter
        private void _comboBoxGridContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AjoutInviteContactDansLibelle();
        }


        //Si la comboBox des entreprises change rafraichi la liste des contacts du client 
        private void _comboBoxDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshListeContactClient();
        }


        //rafraichi la liste des contacts du client 
        private void _comboBoxDataGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            this._gridContact.Visibility = System.Windows.Visibility.Visible;
            RefreshListeContactClient();
        }

        //Lors d'une saisie dans le datagrid remplace les Virgule par des Points, sinon erreur dans calcul
        private void _dataGridLigneNotesDeFrais_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                //On remplace toute "," éventuel qui viendrait malheureusement se glisser dans le champ par un "."
                if (((TextBox)e.OriginalSource).Text.Contains(","))
                {
                    ((TextBox)e.OriginalSource).Text = ((TextBox)e.OriginalSource).Text.Replace(",", ".");
                    // On replace le curseur en fin de saisie, sinon il reste devant la virgule fraichement remplaçée
                    ((TextBox)e.OriginalSource).SelectionStart = ((TextBox)e.OriginalSource).Text.Length;
                }
            }
            catch (Exception) { }
            CalculFicheFrais();
        }

        //Verifie qu'il y a une date
        private void _datePickerDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            VerificationDateFicheFrais();
        }

        //Colle une ligne de Frais
        #region coller CTRL+V
        private void _LigneFicheFraisColler_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            //erreur quand CTRL+C et comboBox selectionner
            try
            {
                CopierColler ClassPaste = new CopierColler();
                ObservableCollection<Ligne_Fiche_Frais> listToAdd = ClassPaste.PasteDataLigneFraisWindow(affichageAdministrateur, Etranger);
                if (listToAdd != null)
                {
                    foreach (Ligne_Fiche_Frais lff in listToAdd)
                    {
                        ((App)App.Current).mySitaffEntities.AddToLigne_Fiche_Frais(lff);
                        ((Fiche_Frais)this.DataContext).Ligne_Fiche_Frais.Add(lff);
                    }
                    this._dataGridLignesNotesDeFrais.Items.Refresh();

                }
            }
            catch (Exception) { }

        }

        private void _LigneFicheFraisColler_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion
        #endregion

        #region Fonctions
        //renvoi True si ca met en Affichage Administrateur, voir initialisationListAdministrateur() pour les noms des administrateurs
        private bool ChoixAffichage()
        {
            bool estAdministrateur = false;
            FraisWindow ftemp = new FraisWindow();
            if (((App)App.Current)._connectedUser != null)
            {
                if (((App)App.Current)._connectedUser.Nom_Utilisateur != null && !String.IsNullOrEmpty(((App)App.Current)._connectedUser.Nom_Utilisateur))
                {
                    if (ftemp.GetListAdministrateur().Contains(((App)App.Current)._connectedUser.Nom_Utilisateur))
                    {
                        AffichageAdministration();
                        estAdministrateur = true;
                    }
                    else
                    {
                        AffichageSalarie();
                    }
                }
                else
                {
                    MessageBox.Show("Votre nom d'utilisateur n'a pas été trouver en base de donnée. Veuillez contacter un administrateur", "Compte", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.DialogResult = false;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Erreur compte. Veuillez contacter un administrateur", "Compte", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
                this.Close();
            }

            try
            {
                //Detachement de tous les éléments de ftemp 
                ((App)App.Current).mySitaffEntities.Detach((Frais)ftemp.DataContext);
            }
            catch (Exception)
            {

            }

            return estAdministrateur;
        }

        //Affiche des colonnes si Etranger est coché et affiche la grid des contacts du client et rempli des variables pour le copier/coller
        private void ColumnIsChecked_Datagrid()
        {
            if (this._dataGridLignesNotesDeFrais.SelectedItem != null)
            {
                try
                {   //met la grille en caché si 'Client' est vide
                    if (((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Entreprise1.Libelle != null && ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Entreprise1 != null && ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem) != null)
                    {
                        if (!String.IsNullOrEmpty(((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Entreprise1.Libelle))
                        {
                            this._gridContact.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            this._gridContact.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }
                }
                catch (Exception)
                {
                    this._gridContact.Visibility = System.Windows.Visibility.Collapsed;
                }
            }

            Etranger = false;
            //si Imputer sur affaire non coché, l'affaire est null
            //Si etranger coché, afficher taux de change + taux de change reel + ttc en euro + Bouton Site internet convertiseur
            foreach (Ligne_Fiche_Frais ite in this._dataGridLignesNotesDeFrais.Items.OfType<Ligne_Fiche_Frais>())
            {
                if (ite.Etranger == true)
                {
                    Etranger = true;
                }
            }

            if (!Etranger)
            {
                this._dataGridLigneNotesDeFraisColumnTauxDeChange.Visibility = System.Windows.Visibility.Collapsed;
                this._buttonAfficherSiteConverteurMonaie.Visibility = System.Windows.Visibility.Collapsed;
                this._dataGridLigneNotesDeFraisColumnTTCEuro.Visibility = System.Windows.Visibility.Collapsed;
                if (affichageAdministrateur)
                {
                    this._dataGridLigneNotesDeFraisColumnTauxDeChangeReel.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    this._dataGridLigneNotesDeFraisColumnTauxDeChangeReel.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            else
            {
                this._dataGridLigneNotesDeFraisColumnTauxDeChange.Visibility = System.Windows.Visibility.Visible;
                this._buttonAfficherSiteConverteurMonaie.Visibility = System.Windows.Visibility.Visible;
                this._dataGridLigneNotesDeFraisColumnTTCEuro.Visibility = System.Windows.Visibility.Visible;
                if (affichageAdministrateur)
                {
                    this._dataGridLigneNotesDeFraisColumnTauxDeChangeReel.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    this._dataGridLigneNotesDeFraisColumnTauxDeChangeReel.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }

        //Rafraichi la liste des contacts du client selectionné
        private void RefreshListeContactClient()
        {
            int compteur = 0;
            try
            {
                this.listContactClient.Clear();
                this._comboBoxGridContact.Items.Refresh();

                if (this._dataGridLignesNotesDeFrais.SelectedItem != null && ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Entreprise1 != null)
                {
                    if (((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Libelle == null)
                    {
                        ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Libelle = String.Empty;
                    }
                    while (compteur != -1 && compteur < this.listEntreprise.Count)
                    {
                        //si l'element de la liste Contact a pour Entreprise celle selectionner
                        if (this.listEntreprise[compteur] != null && listEntreprise[compteur].Personne != null && this.listEntreprise[compteur].Libelle == ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Entreprise1.Libelle)
                        {
                            foreach (Personne personneDeLEntreprise in this.listEntreprise[compteur].Personne)
                            {
                                if (personneDeLEntreprise.Contact != null)
                                {
                                    this.listContactClient.Add(personneDeLEntreprise.Contact);//ajoute dans la liste le contact
                                }
                            }
                            compteur = -2;
                        }

                        compteur++;
                    }
                }
            }
            catch (Exception) { }

            this._comboBoxGridContact.Items.Refresh();
        }

        //Supprime une ou plusieurs ligne(s)
        private void DeleteLigneFrais()
        {
            try
            {
                if (((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem) != null)
                {
                    if (this._dataGridLignesNotesDeFrais.SelectedItem != null && this._dataGridLignesNotesDeFrais.SelectedItems.Count == 1)
                    {
                        try
                        {
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Entreprise1 = null;
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Libelle = null;
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Affaire1 = null;
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Commentaire = null;
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Etranger = false;
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Imputer_Affaire = false;
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Plan_Comptable_Imputation1 = null;
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Plan_Comptable_Tva1 = null;
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Taux_Change = 1;
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Taux_Change_Reel = 1;
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).TTC_En_Euro = 0;
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).TTC_Sur_Ticket = 0;
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).TVA_Recuperable = 0;
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Type_Frais1 = null;
                            this.listContactClient.Clear();
                        }
                        catch (Exception) { }

                        Ligne_Fiche_Frais item = (Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem;

                        try
                        {
                            item.Fiche_Frais1 = null;

                            ((Ligne_Fiche_Frais)this.DataContext).Fiche_Frais1.Ligne_Fiche_Frais.Remove(item);
                            ((App)App.Current).mySitaffEntities.Ligne_Fiche_Frais.DeleteObject(item);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                ((Ligne_Fiche_Frais)this.DataContext).Fiche_Frais1.Ligne_Fiche_Frais.Remove(item);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    this._dataGridLignesNotesDeFrais.Items.Remove(item);

                                }
                                catch (Exception) { }
                            }
                        }


                    }
                    else
                    {
                        if (this._dataGridLignesNotesDeFrais.SelectedItems.Count > 1)
                        {
                            ObservableCollection<Ligne_Fiche_Frais> toRemove = new ObservableCollection<Ligne_Fiche_Frais>();
                            foreach (Ligne_Fiche_Frais item in this._dataGridLignesNotesDeFrais.SelectedItems.OfType<Ligne_Fiche_Frais>())
                            {
                                toRemove.Add(item);
                            }
                            foreach (Ligne_Fiche_Frais item in toRemove)
                            {
                                try
                                {
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Entreprise1 = null;
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Libelle = null;
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Affaire1 = null;
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Commentaire = null;
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Etranger = false;
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Fiche_Frais1 = null;
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Imputer_Affaire = false;
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Plan_Comptable_Imputation1 = null;
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Plan_Comptable_Tva1 = null;
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Taux_Change = 1;
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Taux_Change_Reel = 1;
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).TTC_En_Euro = 0;
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).TTC_Sur_Ticket = 0;
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).TVA_Recuperable = 0;
                                    ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Type_Frais1 = null;
                                    this.listContactClient.Clear();
                                }
                                catch (Exception) { }
                                try
                                {
                                    item.Fiche_Frais1 = null;
                                    ((Ligne_Fiche_Frais)this.DataContext).Fiche_Frais1.Ligne_Fiche_Frais.Remove(item);
                                    ((App)App.Current).mySitaffEntities.Ligne_Fiche_Frais.DeleteObject(item);
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        ((Ligne_Fiche_Frais)this.DataContext).Fiche_Frais1.Ligne_Fiche_Frais.Remove(item);

                                    }
                                    catch (Exception)
                                    {
                                        try
                                        {
                                            this._dataGridLignesNotesDeFrais.Items.Remove(item);
                                        }
                                        catch (Exception) { }
                                    }
                                }
                            }
                        }
                    }
                    this._dataGridLignesNotesDeFrais.Items.Refresh();
                }
            }
            catch (Exception) { }
        }

        //Affichage pour l'administration
        private void AffichageAdministration()
        {
            this._dataGridLigneNotesDeFraisColumnCommentaire.Visibility = System.Windows.Visibility.Visible;
            this._dataGridLigneNotesDeFraisColumnCompteCharge.Visibility = System.Windows.Visibility.Visible;
            this._dataGridLigneNotesDeFraisColumnCompteTVA.Visibility = System.Windows.Visibility.Visible;
            this._buttonAfficherSiteConverteurMonaie.Visibility = System.Windows.Visibility.Collapsed;

            this.Title = "Fiche de frais - affichage administrateur";
        }

        //Affichage pour un salarié
        private void AffichageSalarie()
        {
            this._dataGridLigneNotesDeFraisColumnCommentaire.Visibility = System.Windows.Visibility.Collapsed;
            this._dataGridLigneNotesDeFraisColumnCompteCharge.Visibility = System.Windows.Visibility.Collapsed;
            this._dataGridLigneNotesDeFraisColumnCompteTVA.Visibility = System.Windows.Visibility.Collapsed;
            this._buttonAfficherSiteConverteurMonaie.Visibility = System.Windows.Visibility.Collapsed;
        }

        //calcul la TVA récupérable
        private void CalculTVARecuperable()
        {
            int cmptLigne = 0;
            double LigneTVA = 0;
            double totalTVARembourser = 0;
            if (this._dataGridLignesNotesDeFrais.HasItems && this._dataGridLignesNotesDeFrais.Items != null)
            {
                try
                {
                    foreach (Ligne_Fiche_Frais lff in this._dataGridLignesNotesDeFrais.Items.OfType<Ligne_Fiche_Frais>())
                    {
                        if (lff.Type_Frais1 != null && lff.Type_Frais1.Pourcentage != null)
                        {
                            if (lff.Etranger)
                            {
                                LigneTVA = (lff.TTC_En_Euro / (1 + (lff.Type_Frais1.Plan_Comptable_Tva1.Tva1.Taux / 100))) * (lff.Type_Frais1.Plan_Comptable_Tva1.Tva1.Taux / 100) * (lff.Type_Frais1.Pourcentage / 100);
                            }
                            else
                            {
                                LigneTVA = (lff.TTC_Sur_Ticket / (1 + (lff.Type_Frais1.Plan_Comptable_Tva1.Tva1.Taux / 100))) * (lff.Type_Frais1.Plan_Comptable_Tva1.Tva1.Taux / 100) * (lff.Type_Frais1.Pourcentage / 100);
                            }

                            ((Fiche_Frais)this.DataContext).Ligne_Fiche_Frais.ElementAt(cmptLigne).TVA_Recuperable = LigneTVA;
                            totalTVARembourser += LigneTVA;
                            cmptLigne++;
                        }
                    }
                    ((Fiche_Frais)this.DataContext).Frais1.Total_A_Rembourser = totalTVARembourser;
                }
                catch (Exception) { }
            }
        }

        //calcul les totaux de la Fiche_Frais
        private void CalculFicheFrais()
        {
            double totalHT = 0;
            double totalTTC = 0;
            double totalTVA = 0;
            int cmpt = 0;
            if (this._dataGridLignesNotesDeFrais.HasItems && this._dataGridLignesNotesDeFrais.Items != null)
            {
                try
                {
                    foreach (Ligne_Fiche_Frais lff in this._dataGridLignesNotesDeFrais.Items.OfType<Ligne_Fiche_Frais>())
                    {
                        if (lff.Etranger)
                        {
                            if (affichageAdministrateur)
                            {
                                ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.Items[cmpt]).TTC_En_Euro = lff.TTC_Sur_Ticket * lff.Taux_Change_Reel;
                                totalTTC += lff.TTC_En_Euro;
                                if (lff.Type_Frais1 != null && lff.Type_Frais1.Pourcentage != null && lff.Type_Frais1.Plan_Comptable_Tva1 != null && lff.Type_Frais1.Plan_Comptable_Tva1.Tva1 != null && lff.Type_Frais1.Plan_Comptable_Tva1.Tva1.Taux != null)
                                {
                                    totalHT += lff.TTC_En_Euro / (1 + lff.Type_Frais1.Plan_Comptable_Tva1.Tva1.Taux / 100);
                                    totalTVA += lff.TTC_En_Euro - (lff.TTC_En_Euro / (1 + lff.Type_Frais1.Plan_Comptable_Tva1.Tva1.Taux / 100));
                                }
                            }
                            else
                            {
                                ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.Items[cmpt]).TTC_En_Euro = lff.TTC_Sur_Ticket * lff.Taux_Change;
                                totalTTC += lff.TTC_En_Euro;
                                if (lff.Type_Frais1 != null && lff.Type_Frais1.Pourcentage != null && lff.Type_Frais1.Plan_Comptable_Tva1 != null && lff.Type_Frais1.Plan_Comptable_Tva1.Tva1 != null && lff.Type_Frais1.Plan_Comptable_Tva1.Tva1.Taux != null)
                                {
                                    totalHT += lff.TTC_En_Euro / (1 + lff.Type_Frais1.Plan_Comptable_Tva1.Tva1.Taux / 100);
                                    totalTVA += lff.TTC_En_Euro - (lff.TTC_En_Euro / (1 + lff.Type_Frais1.Plan_Comptable_Tva1.Tva1.Taux / 100));
                                }
                            }
                        }
                        else
                        {
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.Items[cmpt]).TTC_En_Euro = lff.TTC_Sur_Ticket * lff.Taux_Change;
                            totalTTC += lff.TTC_Sur_Ticket;
                            if (lff.Type_Frais1 != null && lff.Type_Frais1.Pourcentage != null && lff.Type_Frais1.Plan_Comptable_Tva1 != null && lff.Type_Frais1.Plan_Comptable_Tva1.Tva1 != null && lff.Type_Frais1.Plan_Comptable_Tva1.Tva1.Taux != null)
                            {
                                totalHT += lff.TTC_En_Euro / (1 + lff.Type_Frais1.Plan_Comptable_Tva1.Tva1.Taux / 100);
                                totalTVA += lff.TTC_En_Euro - (lff.TTC_En_Euro / (1 + lff.Type_Frais1.Plan_Comptable_Tva1.Tva1.Taux / 100));
                            }
                        }
                        cmpt++;
                    }
                    ((Fiche_Frais)this.DataContext).Total_HT = totalHT;
                    ((Fiche_Frais)this.DataContext).Total_TTC = totalTTC;
                    ((Fiche_Frais)this.DataContext).Total_TVA = totalTVA;
                    CalculTVARecuperable();
                }
                catch (Exception) { }
            }
        }

        //Rempli le datagrid avec les Plan Comptable et Entreprise du Type_Frais choisi
        private void RemplirDataGridAvecDonneDuTypeFrais()
        {
            if (this._dataGridLignesNotesDeFrais.HasItems)
            {
                foreach (Ligne_Fiche_Frais ite in this._dataGridLignesNotesDeFrais.Items.OfType<Ligne_Fiche_Frais>())
                {
                    if (ite.Taux_Change == 0)
                    {
                        ite.Taux_Change = 1;
                    }
                    if (ite.Taux_Change_Reel == 0)
                    {
                        ite.Taux_Change_Reel = 1;
                    }

                    // si 'Imputer sur affaire' est décoché : affaire = null
                    if (ite.Imputer_Affaire == false)
                    {
                        ite.Affaire1 = null;
                    }
                    else
                    {
                        if (ite.Type_Frais1 != null)
                        {
                            if (ite.Plan_Comptable_Imputation1 == null && ite.Type_Frais1.Plan_Comptable_Imputation1 != null)
                            {
                                ite.Plan_Comptable_Imputation1 = ite.Type_Frais1.Plan_Comptable_Imputation1;
                            }
 
                        }
                    }

                    // met les plan comptable et rempli client suivant le type
                    if (ite.Type_Frais1 != null && (ite.Plan_Comptable_Imputation1 == null || ite.Plan_Comptable_Tva1 == null))
                    {
                        if (ite.Type_Frais1.Entreprise_Mere1 != null)
                        {
                            ite.Entreprise1 = ite.Type_Frais1.Entreprise_Mere1.Entreprise1;
                        }

                        if (ite.Etranger == false)
                        {
                            ite.Plan_Comptable_Tva1 = ite.Type_Frais1.Plan_Comptable_Tva1;
                        }
                        else
                        {
                            ite.Plan_Comptable_Imputation1 = ite.Type_Frais1.Plan_Comptable_Imputation;
                            ite.Plan_Comptable_Tva1 = ite.Type_Frais1.Plan_Comptable_Tva;
                        }
                    }
                }

                this._dataGridLignesNotesDeFrais.Items.Refresh();
                CalculFicheFrais();
                verificationLigneFicheFrais();
                this._dataGridLignesNotesDeFrais.Items.Refresh();
            }
        }

        //Ajoute le nom selectionné de _comboBoxGridContact dans le libelle de la ligne selectionné
        private void AjoutInviteContactDansLibelle()
        {
            if (_comboBoxGridContact.SelectedItem != null)
            {
                if (this._dataGridLignesNotesDeFrais.SelectedItem != null)
                {
                    try
                    {
                        String nom = ((Contact)_comboBoxGridContact.SelectedItem).Personne.fullname;
                        String messageLibelle = ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Libelle;
                        if (!((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Libelle.Contains("Invité(s)") && ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Libelle != null)
                        {
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Libelle = messageLibelle + " Invité(s) : ";
                        }

                        if (((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.Items.GetItemAt(0)) != null && this._dataGridLignesNotesDeFrais.HasItems && ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Libelle.Contains("Invité(s)"))
                        {
                            String NomInvite = ((Contact)this._comboBoxGridContact.SelectedItem).Personne.fullname + ";";
                            ((Ligne_Fiche_Frais)this._dataGridLignesNotesDeFrais.SelectedItem).Libelle += NomInvite;
                        }
                    }
                    catch (Exception) { }
                }
            }
        }
        #endregion



    }
}
