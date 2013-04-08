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
using System.ComponentModel;
using System.Collections.ObjectModel;
using SitaffRibbon.Classes;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour FraisWindow.xaml
    /// </summary>
    public partial class FraisWindow : Window
    {

        #region Attribut
        private List<String> listNomUtilisateurAdministrateurModule;
        private int placeSalarieDansList;
        private DateTime datemoiscourant;
        #endregion

        #region Propriété de dépendances
        public ObservableCollection<Affaire> listAffaire
        {
            get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
            set { SetValue(listAffaireProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(FraisWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Salarie> listSalarie
        {
            get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
            set { SetValue(listSalarieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(FraisWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Entreprise> listEntreprise
        {
            get { return (ObservableCollection<Entreprise>)GetValue(listEntrepriseProperty); }
            set { SetValue(listEntrepriseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listClient.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseProperty =
            DependencyProperty.Register("listEntreprise", typeof(ObservableCollection<Entreprise>), typeof(FraisWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Type_Frais> listTypeFrais
        {
            get { return (ObservableCollection<Type_Frais>)GetValue(listTypeFraisProperty); }
            set { SetValue(listTypeFraisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listTypeFrais.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listTypeFraisProperty =
            DependencyProperty.Register("listTypeFrais", typeof(ObservableCollection<Type_Frais>), typeof(FraisWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Avance> listAvanceDemande
        {
            get { return (ObservableCollection<Avance>)GetValue(listAvanceDemandeProperty); }
            set { SetValue(listAvanceDemandeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAvanceDemande.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAvanceDemandeProperty =
            DependencyProperty.Register("listAvanceDemande", typeof(ObservableCollection<Avance>), typeof(FraisWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Avance> listAvanceValider
        {
            get { return (ObservableCollection<Avance>)GetValue(listAvanceValiderProperty); }
            set { SetValue(listAvanceValiderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listAvanceValider.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listAvanceValiderProperty =
            DependencyProperty.Register("listAvanceValider", typeof(ObservableCollection<Avance>), typeof(FraisWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Frais_Km> listFraisKmARembourse
        {
            get { return (ObservableCollection<Frais_Km>)GetValue(listFraisKmARembourseProperty); }
            set { SetValue(listFraisKmARembourseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listKmARembourse.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listFraisKmARembourseProperty =
            DependencyProperty.Register("listFraisKmARembourse", typeof(ObservableCollection<Frais_Km>), typeof(FraisWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Fiche_Frais> listARecupererFicheFrais
        {
            get { return (ObservableCollection<Fiche_Frais>)GetValue(listARecupererFicheFraisProperty); }
            set { SetValue(listARecupererFicheFraisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listARecupererFicheFrais.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listARecupererFicheFraisProperty =
            DependencyProperty.Register("listARecupererFicheFrais", typeof(ObservableCollection<Fiche_Frais>), typeof(FraisWindow), new UIPropertyMetadata(null));

        /*
        public ObservableCollection<Type_PuissanceFiscale> listTypeCoefChevauxFiscaux
        {
            get { return (ObservableCollection<Type_PuissanceFiscale>)GetValue(listTypeCoefChevauxFiscauxProperty); }
            set { SetValue(listTypeCoefChevauxFiscauxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listTypeCoefChevauxFiscaux.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listTypeCoefChevauxFiscauxProperty =
            DependencyProperty.Register("listTypeCoefChevauxFiscaux", typeof(ObservableCollection<Type_PuissanceFiscale>), typeof(FraisWindow), new UIPropertyMetadata(null));
        */


        #endregion

        #region Initialisation
        //initialisation de la liste des administrateurs
        private void initialisationListAdministrateur()
        {
            listNomUtilisateurAdministrateurModule = new List<string>();
            listNomUtilisateurAdministrateurModule.Add("imurai");
            listNomUtilisateurAdministrateurModule.Add("mmassicot");
            listNomUtilisateurAdministrateurModule.Add("achaudet");
            listNomUtilisateurAdministrateurModule.Add("aquinton");
            listNomUtilisateurAdministrateurModule.Add("sguennec");
        }

        private void initialisationPropDependance()
        {
            this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Identifiant));
            this.listSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.OrderBy(s => s.Personne.Nom));
            this.listEntreprise = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.Where(en => en.Is_Client == true));
            this.listTypeFrais = new ObservableCollection<Type_Frais>(((App)App.Current).mySitaffEntities.Type_Frais.OrderBy(tf => tf.Libelle));
            this.listAvanceValider = new ObservableCollection<Avance>(((App)App.Current).mySitaffEntities.Avance.Where(a => a.Frais1 != null));
            this.listAvanceDemande = new ObservableCollection<Avance>(((App)App.Current).mySitaffEntities.Avance.Where(a => a.Frais1 == null));
            this.listFraisKmARembourse = new ObservableCollection<Frais_Km>();
            this.listARecupererFicheFrais = new ObservableCollection<Fiche_Frais>();
            //Quand le tableau des Types de chevaux fiscaux sera fonctionnel
            //this.listTypeCoefChevauxFiscaux = new ObservableCollection<PuissanceFiscale>(((App)App.Current).mySitaffEntities.PuissanceFiscale.OrderBy(pf => pf.Libelle));
        }

        private void initialisationSecurite()
        {

        }

        #endregion

        #region Constructeur
        public FraisWindow()
        {
            InitializeComponent();

            //initialisation du mois courant
            bool val;
            val = DateTime.TryParse("01/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString(), out datemoiscourant);

            //Initialisation des propriétés de dépendances
            this.initialisationPropDependance();

            //Initialisation de la sécurité
            this.initialisationSecurite();

            //initialise la liste des administrateurs 
            this.initialisationListAdministrateur();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);
        }
        #endregion

        #region Fenêtre chargée
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChoixAffichage();//Affiche ou masque certains champs/colonnes... suivant l'utilisateur
            PlaceUtilisateur();//trouve la place de l'utilisateur dans la listSalarie et place la comboBoxNomSalarie sur le salarié

            if (this.DataContext == null)
            {
                NouveauFrais();//Créé un nouveau frais rattaché au salarié connecté et le met dans le DataContext
            }

            RechargeAvances();//met les avances

            double totalTTC = 0;
            double totalHT = 0;
            double totalTVA = 0;
            double totalARembourser = 0;
            this.listARecupererFicheFrais.Clear();
            foreach (Fiche_Frais item in ((Frais)this.DataContext).Fiche_Frais.OfType<Fiche_Frais>())
            {
                Fiche_Frais tempff = new Fiche_Frais();
                tempff.Total_TVA = 0;
                tempff.Numero = item.Numero;
                tempff.Date_Fiche = item.Date_Fiche;
                if (item.Total_HT != null)
                {
                    totalHT += item.Total_HT.Value;
                }
                if (item.Total_TTC != null)
                {
                    totalTTC += item.Total_TTC.Value;
                }
                if (item.Total_TVA != null)
                {
                    totalTVA += item.Total_TVA.Value;
                }

                foreach (Ligne_Fiche_Frais lff in item.Ligne_Fiche_Frais.OfType<Ligne_Fiche_Frais>())
                {
                    totalARembourser += lff.TVA_Recuperable;
                    tempff.Total_TVA += lff.TVA_Recuperable;
                }
                this.listARecupererFicheFrais.Add(tempff);
            }

            ((Frais)this.DataContext).Total_HT = totalHT;
            ((Frais)this.DataContext).Total_TTC = totalTTC;
            ((Frais)this.DataContext).Total_TVA = totalTVA;
            ((Frais)this.DataContext).Total_A_Rembourser = totalARembourser;



            double Arembourser = 0;
            try
            {
                listFraisKmARembourse.Clear();
                foreach (Frais_Km item in ((Frais)this.DataContext).Frais_Km.OfType<Frais_Km>())
                {
                    //calcul
                    if (item.Km != null && item.Base != null)
                    {
                        int cv = 0; int.TryParse(item.Base.ToString(), out cv);
                        //Arembourser += baremeKM(item.Km, cv);
                        Arembourser += item.Km * item.Base;
                        Frais_Km fk = new Frais_Km();
                        //fk.Km = baremeKM(item.Km, cv);
                        fk.Km = item.Km * item.Base;
                        fk.Libelle = item.Libelle;
                        listFraisKmARembourse.Add(fk);//ajoute le frais Km a la liste des détails des frais Km
                    }
                }
            }
            catch (Exception) { }
            ((Frais)this.DataContext).Total_A_Rembourser += Arembourser;

            double totalAvanceVerser = 0;
            foreach (Avance item in ((Frais)this.DataContext).Avance.OfType<Avance>())
            {
                totalAvanceVerser += item.Somme.Value;
            }
            ((Frais)this.DataContext).Total_Avance = totalAvanceVerser;

            //met la personnalisation des lignes alterné sur le datagrid des Frais Généraux ( pas sur le RowDetails)
            this._dataGridFraisGeneraux.AlternatingRowBackground = ((App)App.Current).personnalisation.BackGroundUserControlDataGridAlternateColor;

        }
        #endregion

        #region Boutons
        //Enregistre et quitte la fenêtre 
        private void _buttonOk_Click(object sender, RoutedEventArgs e)
        {
            RefreshAllTotaux();
            if (VerificationFrais())
            {
                if (VerificationKm())
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Les données à ajouter ne sont pas conformes.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        //Quitte la fenêtre SANS enregistré
        private void _buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshAllTotaux();
                this.DialogResult = false;
                this.Close();
            }
            catch (Exception)
            {
            }
        }


        //Ouvre la fenêtre 'FicheFraisWindow' pour ajouter une fiche de frais
        private void _buttonAjouterFraisGeneraux_Click(object sender, RoutedEventArgs e)
        {
            FicheFraisWindow fraisAjoutWindow = new FicheFraisWindow();
            Fiche_Frais tempff = new Fiche_Frais();

            tempff.Date_Fiche = DateTime.Now.Date;
            if (this._dataGridFraisGeneraux.Items.Count >= 1 && this._dataGridFraisGeneraux.DataContext != null)
            {
                if (this._dataGridFraisGeneraux.Items.GetItemAt(0) != null && ((Fiche_Frais)this._dataGridFraisGeneraux.Items.GetItemAt(this._dataGridFraisGeneraux.Items.Count - 1)).Numero != null)
                {
                    tempff.Numero = ((Fiche_Frais)this._dataGridFraisGeneraux.Items.GetItemAt(this._dataGridFraisGeneraux.Items.Count - 1)).Numero + 1;
                }
                else
                {
                    tempff.Numero = 1;
                }
            }
            else
            {
                tempff.Numero = 1;
            }

            tempff.Ligne_Fiche_Frais.Add(new Ligne_Fiche_Frais());
            tempff.Total_HT = 0;
            tempff.Total_TTC = 0;
            tempff.Total_TVA = 0;
            if (datemoiscourant > ((Frais)this.DataContext).Date_Fin)
            {
                tempff.Date_Fiche = ((Frais)this.DataContext).Date_Debut;
            }
            else
            {
                tempff.Date_Fiche = DateTime.Today.Date;
            }

            tempff.Frais1 = ((Frais)this.DataContext);
            fraisAjoutWindow.DataContext = tempff;

            bool? dialogResult = fraisAjoutWindow.ShowDialog();
            Fiche_Frais ff = (Fiche_Frais)fraisAjoutWindow.DataContext;

            if (dialogResult.HasValue == true && dialogResult.Value == false)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(ff);
                }
                catch (Exception)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Fiche_Frais.DeleteObject(ff);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            RefreshAllTotaux();
        }

        //Ouvre la fenêtre 'FicheFraisWindow' pour modifier une fiche de frais
        private void _buttonModifierFraisGeneraux_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridFraisGeneraux.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner une note de frais à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridFraisGeneraux.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'une note de frais à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridFraisGeneraux.SelectedItem != null)
            {
                try
                {
                    //lance une FicheFraisWindow avec les données du FraisWindow
                    FicheFraisWindow fraisModifierWindow = new FicheFraisWindow();

                    if (((Fiche_Frais)this._dataGridFraisGeneraux.SelectedItem) != null)
                    {
                        int index = (this._dataGridFraisGeneraux).SelectedIndex;
                        Fiche_Frais ff = new Fiche_Frais();

                        ff.Date_Fiche = ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Date_Fiche;
                        ff.Numero = ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Numero;
                        ff.Total_HT = ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Total_HT;
                        ff.Total_TTC = ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Total_TTC;
                        ff.Total_TVA = ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Total_TVA;

                        foreach (Ligne_Fiche_Frais lff in ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Ligne_Fiche_Frais.OfType<Ligne_Fiche_Frais>())
                        {
                            Ligne_Fiche_Frais templff = new Ligne_Fiche_Frais();
                            templff.Affaire1 = lff.Affaire1;
                            templff.Commentaire = lff.Commentaire;
                            templff.Entreprise1 = lff.Entreprise1;
                            templff.Etranger = lff.Etranger;
                            templff.Imputer_Affaire = lff.Imputer_Affaire;
                            templff.Libelle = lff.Libelle;
                            templff.Plan_Comptable_Imputation1 = lff.Plan_Comptable_Imputation1;
                            templff.Plan_Comptable_Tva1 = lff.Plan_Comptable_Tva1;
                            templff.Taux_Change = lff.Taux_Change;
                            templff.Taux_Change_Reel = lff.Taux_Change_Reel;
                            templff.TTC_En_Euro = lff.TTC_En_Euro;
                            templff.TTC_Sur_Ticket = lff.TTC_Sur_Ticket;
                            templff.TVA_Recuperable = lff.TVA_Recuperable;
                            templff.Type_Frais1 = lff.Type_Frais1;
                            ff.Ligne_Fiche_Frais.Add(templff);
                        }
                        ff.Frais1 = ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Frais1;

                        fraisModifierWindow.DataContext = ff;
                        bool? dialogResult = fraisModifierWindow.ShowDialog();


                        if (dialogResult.Value)
                        {

                            ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Date_Fiche = ff.Date_Fiche;
                            ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Numero = ff.Numero;
                            ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Total_HT = ff.Total_HT;
                            ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Total_TTC = ff.Total_TTC;
                            ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Total_TVA = ff.Total_TVA;
                            ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Frais1 = ff.Frais1;
                            ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Ligne_Fiche_Frais.Clear();
                            while (ff.Ligne_Fiche_Frais.OfType<Ligne_Fiche_Frais>().Count() != 0)
                            {
                                ((Fiche_Frais)this._dataGridFraisGeneraux.Items[index]).Ligne_Fiche_Frais.Add(ff.Ligne_Fiche_Frais.ElementAt(0));
                            }
                        }
                        try
                        {
                            int compt = 0;
                            while (compt < ff.Ligne_Fiche_Frais.OfType<Ligne_Fiche_Frais>().Count())
                            {
                                try
                                {
                                    ((App)App.Current).mySitaffEntities.Detach(ff.Ligne_Fiche_Frais.ElementAt(compt));
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        ((App)App.Current).mySitaffEntities.Ligne_Fiche_Frais.DeleteObject(ff.Ligne_Fiche_Frais.ElementAt(compt));
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                                compt++;
                            }
                            ((App)App.Current).mySitaffEntities.Detach(ff);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                ((App)App.Current).mySitaffEntities.Fiche_Frais.DeleteObject(ff);
                            }
                            catch (Exception)
                            {

                            }
                        }
                        this._dataGridFraisGeneraux.Items.Refresh();
                    }
                }
                catch (Exception) { }
            }
            RefreshAllTotaux();
        }

        //Supprime UNE fiche de frais
        private void _buttonSupprimerFicheFraisGeneraux_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridFraisGeneraux.SelectedItem != null && this._dataGridFraisGeneraux.SelectedItems.Count == 1)
            {
                int compt = 0;
                while (((Fiche_Frais)this._dataGridFraisGeneraux.SelectedItem).Ligne_Fiche_Frais.Count > 0)
                {
                    Ligne_Fiche_Frais item = ((Fiche_Frais)this._dataGridFraisGeneraux.SelectedItem).Ligne_Fiche_Frais.FirstOrDefault();
                    try
                    {
                        item.Fiche_Frais1 = null;
                        ((Frais)this.DataContext).Fiche_Frais.ElementAt(compt).Ligne_Fiche_Frais.Remove(item);
                        ((App)App.Current).mySitaffEntities.Ligne_Fiche_Frais.DeleteObject(item);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            ((Frais)this.DataContext).Fiche_Frais.ElementAt(compt).Ligne_Fiche_Frais.Remove(item);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                this._dataGridFraisGeneraux.Items.Remove(item);

                            }
                            catch (Exception) { }
                        }
                    }
                    compt++;
                }

                Fiche_Frais item2 = ((Fiche_Frais)this._dataGridFraisGeneraux.SelectedItem);

                compt = 0;
                while (compt < this.listARecupererFicheFrais.Count)
                {
                    if (this.listARecupererFicheFrais[compt].Numero == item2.Numero)
                    {
                        this.listARecupererFicheFrais.RemoveAt(compt);
                    }
                    compt++;
                }

                try
                {
                    item2.Frais1 = null;
                    ((Frais)this.DataContext).Fiche_Frais.Remove(item2);
                    ((App)App.Current).mySitaffEntities.Fiche_Frais.DeleteObject(item2);
                }
                catch (Exception)
                {
                    try
                    {
                        ((Frais)this.DataContext).Fiche_Frais.Remove(item2);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            this._dataGridFraisGeneraux.Items.Remove(item2);

                        }
                        catch (Exception) { }
                    }
                }
            }
            RefreshAllTotaux();
        }


        //Pour coller une ou plusieurs ligne(s) de Frais Km
        private void _buttonCollerFraisKm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CopierColler ClassPaste = new CopierColler();
                ObservableCollection<Frais_Km> listToAdd = ClassPaste.PasteDataFraisWindow();
                if (listToAdd != null)
                {
                    foreach (Frais_Km fk in listToAdd)
                    {
                        ((App)App.Current).mySitaffEntities.AddToFrais_Km(fk);
                        ((Frais)this.DataContext).Frais_Km.Add(fk);
                    }
                    this._dataGridFraisKm.Items.Refresh(); // erreur si edition de l'affaire et coller en même temps
                    RefreshAllTotaux();
                }
            }
            catch (Exception) { }
        }

        //Pour supprimer une ou plusieurs ligne(s) de Frais Km
        private void _buttonSupprimerFraisKm_Click(object sender, RoutedEventArgs e)
        {
            DeleteLigneFraisKm();
            RefreshAllTotaux();
        }


        //Passe l'avance en Validé ( Gauche -> Droite )
        private void _buttonPasserAvancesEnValide_Click(object sender, RoutedEventArgs e)
        {
            int nb;
            //si une Avance est selectionnée
            if (this._dataGridAvancesDemande.SelectedItem != null && this._dataGridAvancesDemande.SelectedItems.Count == 1)
            {
                Avance temp = new Avance();

                temp = ((Avance)this._dataGridAvancesDemande.SelectedItem);
                temp.Frais1 = ((Frais)this.DataContext);//met l'avance en validé

                this.listAvanceValider.Add(temp);
                this.listAvanceDemande.Remove((Avance)this._dataGridAvancesDemande.SelectedItem);
            }
            if (this._dataGridAvancesDemande.SelectedItem != null && this._dataGridAvancesDemande.SelectedItems.Count > 1)//si plusieurs Avance sont selectionnées
            {   //pour chaque Avance selectionnées
                int compteur = 0; nb = this._dataGridAvancesDemande.SelectedItems.Count;
                while (compteur < nb)
                {
                    Avance temp = new Avance(); //Avance temporaire 
                    temp = ((Avance)this._dataGridAvancesDemande.SelectedItems[0]);
                    temp.Frais1 = ((Frais)this.DataContext);

                    this.listAvanceValider.Add(temp);
                    this.listAvanceDemande.Remove((Avance)this._dataGridAvancesDemande.SelectedItems[0]);

                    compteur++;
                }
            }
            this._dataGridAvancesValide.Items.Refresh();
            this._dataGridAvancesDemande.Items.Refresh();
            RefreshAllTotaux();
        }

        //Met l'avance en Demandé  ( Droite -> Gauche )
        private void _buttonPasserAvancesEnDemande_Click(object sender, RoutedEventArgs e)
        {
            int nb;
            //si une Avance est selectionnée
            if (this._dataGridAvancesValide.SelectedItem != null && this._dataGridAvancesValide.SelectedItems.Count == 1)
            {
                Avance temp = new Avance();
                temp = ((Avance)this._dataGridAvancesValide.SelectedItem);
                temp.Frais1 = null;

                this.listAvanceDemande.Add(temp);
                this.listAvanceValider.Remove((Avance)this._dataGridAvancesValide.SelectedItem);


            }
            if (this._dataGridAvancesValide.SelectedItem != null && this._dataGridAvancesValide.SelectedItems.Count > 1)//si plusieurs Avance sont selectionnées
            {   //pour chaque Avance selectionnées

                int compteur = 0; nb = this._dataGridAvancesValide.SelectedItems.Count;
                while (compteur < nb)
                {
                    Avance temp = new Avance(); //Avance temporaire 
                    temp = ((Avance)this._dataGridAvancesValide.SelectedItems[0]);
                    temp.Frais1 = null;

                    this.listAvanceDemande.Add(temp);
                    this.listAvanceValider.Remove((Avance)this._dataGridAvancesValide.SelectedItems[0]);

                    compteur++;
                }
            }
            this._dataGridAvancesDemande.Items.Refresh();
            this._dataGridAvancesValide.Items.Refresh();

            RefreshAllTotaux();
        }


        //Ouvre la fenêtre 'AvanceWindow' pour ajouter une avance
        private void _buttonAjoutAvance_Click(object sender, RoutedEventArgs e)
        {
            DateTime d = new DateTime();
            Salarie salarieCourant = new Salarie();
            AvanceWindow avanceAjoutWindow = new AvanceWindow();
            avanceAjoutWindow.DataContext = new Avance();

            salarieCourant = this.listSalarie.ElementAt(placeSalarieDansList);


            if (!this.listNomUtilisateurAdministrateurModule.Contains(((App)App.Current)._connectedUser.Nom_Utilisateur))
            {
                avanceAjoutWindow._comboBoxSalarie.IsEnabled = false;//bloquer la combox de choix du Nom ( pour ne pas qu'un salarié fasse une demande d'avance sur quelqu'un d'autre que lui)
            }


            ((Avance)avanceAjoutWindow.DataContext).Salarie1 = salarieCourant;
            DateTime.TryParse(DateTime.Today.ToShortDateString(), out d);
            ((Avance)avanceAjoutWindow.DataContext).Date_Avance = d;
            ((Avance)avanceAjoutWindow.DataContext).Somme = 0;

            bool? dialogResult = avanceAjoutWindow.ShowDialog();
            Avance avemaria = (Avance)avanceAjoutWindow.DataContext;


            if (dialogResult.HasValue == true && dialogResult.Value == false)
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(avemaria);
                }
                catch (Exception)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Avance.DeleteObject(avemaria);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            else
            {
                if (avemaria.Date_Avance.Value.Month == datemoiscourant.Month)
                {
                    this.listAvanceDemande.Add(avemaria);
                }
            }

            RefreshAllTotaux();
        }

        //Ouvre la fenêtre 'ContactWindow' pour ajouter un nouveau contact
        private void _buttonAjouterContact_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow contactWindow = new ContactWindow();
            Personne tmp = new Personne();

            tmp.Contact = new Contact();
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

        //Recalcul tout le frais
        private void _buttonCalculer_Click(object sender, RoutedEventArgs e)
        {
            RefreshAllTotaux();
            IsChecked_ImputerFraisKm();
            VerificationKm();
            this._dataGridAvancesDemande.Items.Refresh();
            this._dataGridAvancesValide.Items.Refresh();
            this._dataGridFraisGeneraux.Items.Refresh();
            this._dataGridFraisKm.Items.Refresh();
            this._datagridResumeFicheFrais.Items.Refresh();
            this._datagridResumeFraisKm.Items.Refresh();
        }

        #endregion

        #region Verification
        //verifie qu'il y a une fiche de frais OU un frais Km OU une avance
        private bool VerificationFrais()
        {
            bool verif = false;
            bool verifFicheFrais = false;
            bool verifFraisKM = false;
            bool verifAvance = false;
            if (this._dataGridFraisGeneraux.HasItems)//s'il y a une fiche de frais
            {
                verifFicheFrais = true;
            }

            if (this._dataGridFraisKm.HasItems)// s'il y a un Frais_Km
            {
                if (this._dataGridFraisKm.Items[0].ToString() != "{NewItemPlaceholder}")// si la 1ere ligne ne corespond pas un un nouvel item vide
                {
                    foreach (Frais_Km fk in this._dataGridFraisKm.Items.OfType<Frais_Km>())
                    {
                        if (fk != null && (!String.IsNullOrEmpty(fk.Base.ToString())) && (!String.IsNullOrEmpty(fk.Km.ToString())))//si les champs pour calculer ne sont pas vide ou null
                        {
                            verifFraisKM = true;
                        }
                    }
                }
            }


            if (this._dataGridAvancesDemande.HasItems || this._dataGridAvancesValide.HasItems)
            {
                verifAvance = true;
            }


            if (VerificationDateDebut() || VerificationNomSalarie())
            {
                verif = true;
            }

            //s'il y a une frais OU une fiche frais 
            if (verifFicheFrais || verifFraisKM || verifAvance)
            {
                verif = true;
            }
            else
            {
                verif = false;
            }
            
            return verif;
        }

        private bool VerificationNomSalarie()
        {
            return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(_comboBoxNomSalarie, _textBlockNomSalarie); 
        }

        private bool VerificationDateDebut()
        {
           return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateDeDebut, _textBlockDateDeDebut);
        }

        #region Verification Frais_Km
        //Met en rouge les champs non valide, en vert les valide
        private bool VerificationKm()
        {
            bool verif = false;
            bool verifImputer = false;
            bool verifKm = false;
            bool verifPuissancefiscale = true; // Verification a faire (parametre puissance fiscale)
            String messageErreur = String.Empty;
            if (this._dataGridFraisKm.HasItems)
            {
                foreach (Frais_Km fk in this._dataGridFraisKm.Items.OfType<Frais_Km>())
                {
                    try
                    {

                        if (fk.ImputerAffaire)// si imputer est coché
                        {
                            if (fk.Affaire1 == null) // si l'affaire n'est pas rempli
                            {
                                messageErreur += "Affaire n'est pas renseigné ";
                            }
                            else
                            {
                                verifImputer = true;
                            }
                        }
                        else
                        {
                            verifImputer = true;
                        }

                        if (fk.Km <= 0)//test si le Km n'est pas valide
                        {
                            messageErreur += "Km est inférieur ou égal a 0 ";
                        }
                        else
                        {
                            verifKm = true;
                        }




                        DataGridRow row = (DataGridRow)this._dataGridFraisKm.ItemContainerGenerator.ContainerFromItem(fk);
                        if (!verifImputer || !verifKm || !verifPuissancefiscale)
                        {
                            if (row != null)
                            {
                                row.ToolTip = messageErreur;
                                row.Background = ((App)App.Current).verifications.Couleur_Champ_Non_Ok;
                            }

                        }
                        else
                        {
                            if (row != null)
                            {
                                row.ToolTip = String.Empty;
                                row.Background = ((App)App.Current).verifications.Couleur_Champ_Ok;
                            }
                            verif = true;
                        }


                    }
                    catch (Exception) { }
                }
                if (this._dataGridFraisKm.Items.Count == 1)//sinon si la 1ere ligne est vide
                {
                    if (this._dataGridFraisKm.Items[0].ToString() == "{NewItemPlaceholder}")//si la 1er ligne correspond a une ligne null
                    {
                        verif = true;
                    }
                    else if (((Frais_Km)this._dataGridFraisKm.Items[0]).Km == 0 && ((Frais_Km)this._dataGridFraisKm.Items[0]).Base == 0 && (((Frais_Km)this._dataGridFraisKm.Items[0]).Affaire1 == null || String.IsNullOrEmpty(((Frais_Km)this._dataGridFraisKm.Items[0]).Affaire1.Numero)) && String.IsNullOrEmpty(((Frais_Km)this._dataGridFraisKm.Items[0]).Libelle))
                    {
                        verif = true;
                    }
                }
            }
            else
            {
                verif = true;
            }


            //met l'onglet en couleur Ok ou Non_Ok
            ((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemFraisKm, verif);


            return verif;
        }

        #endregion
        #endregion

        #region Evénements
        #region coller CTRL+V
        private void _FraisKmColler_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            CopierColler ClassPaste = new CopierColler();
            ObservableCollection<Frais_Km> listToAdd = ClassPaste.PasteDataFraisWindow();//PasteDataFraisWindow dans la classe Copier/coller
            if (listToAdd != null)
            {
                foreach (Frais_Km fk in listToAdd)
                {
                    ((App)App.Current).mySitaffEntities.AddToFrais_Km(fk);
                    ((Frais)this.DataContext).Frais_Km.Add(fk);
                }
                this._dataGridFraisGeneraux.Items.Refresh();//rafraichi le datagrid des frais generaux

                RefreshAllTotaux();
            }
        }

        private void _FraisKmColler_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion

        //Déselectionne la Fiche_Frais selectionner et cache les Ligne_Fiche_Frais ( sur _dataGridFraisGeneraux prop VisibleWhenSelected )
        private void _dataGridFraisGeneraux_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (((Fiche_Frais)this._dataGridFraisGeneraux.SelectedItem) != null)
                {
                    this._dataGridFraisGeneraux.SelectedIndex = -1;
                }
            }
            catch (Exception)
            {
            }
        }

        //Recalcul les Totaux lors de changement sur le datagrid Frais_Km
        private void _dataGridFraisKm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshAllTotaux();
            IsChecked_ImputerFraisKm();
            VerificationKm();
        }

        //quand parametre fini, va mettre dans la case "Base" le coefficient
        private void ComboBoxTypeChevauxFiscaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((Frais_Km)this._dataGridFraisKm.SelectedItem).Base = ChoixCoefficientKm();
        }

        //recharge le "Lot", les "Avances" et la place du salarie 
        private void _comboBoxNomSalarie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.RechargePlaceSalarie();
            this.RechargeLotSalarie();
            this.RechargeAvances();
            this.RefreshAllTotaux();
            this.VerificationNomSalarie();
        }

        //met a jour la date de fin
        private void _datePickerDateDeDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.MetAJourFinMois();
            this.RechargeLotSalarie();
            this.VerificationDateDebut();
        }
        #endregion

        #region Fonctions
        //Cache certains champs/colonnes suivant l'utilisateur ; defini dans 'initialisationListAdministrateur()'
        private void ChoixAffichage()
        {
            if (((App)App.Current)._connectedUser != null)
            {
                if (!String.IsNullOrEmpty(((App)App.Current)._connectedUser.Nom_Utilisateur) && ((App)App.Current)._connectedUser.Nom_Utilisateur != null)
                {
                    if (this.listNomUtilisateurAdministrateurModule.Contains(((App)App.Current)._connectedUser.Nom_Utilisateur))
                    {
                        AffichageAdministration();
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
        }

        //choix Utilisateur
        private void PlaceUtilisateur()
        {
            if (this.DataContext == null && this.listSalarie.Contains(((App)App.Current)._connectedUser.Salarie_Interne1.Salarie))
            {
                placeSalarieDansList = this.listSalarie.IndexOf(((App)App.Current)._connectedUser.Salarie_Interne1.Salarie);
                this._comboBoxNomSalarie.SelectedIndex = placeSalarieDansList;
            }
        }

        //supprime une ou plusieurs Frais_Km
        private void DeleteLigneFraisKm()
        {

            if (this._dataGridFraisKm.HasItems && this._dataGridFraisKm.Items.Count > 1)
            {
                try
                {
                    ((Frais_Km)this._dataGridFraisKm.SelectedItem).ToString();

                    if (this._dataGridFraisKm.SelectedItem != null && this._dataGridFraisKm.SelectedItems.Count == 1)
                    {
                        try
                        {
                            ((Frais_Km)this._dataGridFraisKm.SelectedItem).Libelle = null;
                            ((Frais_Km)this._dataGridFraisKm.SelectedItem).Affaire1 = null;
                            ((Frais_Km)this._dataGridFraisKm.SelectedItem).Km = 0.0;
                            ((Frais_Km)this._dataGridFraisKm.SelectedItem).ImputerAffaire = false;
                            ((Frais_Km)this._dataGridFraisKm.SelectedItem).Base = 0;
                            ((Frais_Km)this._dataGridFraisKm.SelectedItem).Frais1 = null;
                        }
                        catch (Exception) { }

                        Frais_Km item = (Frais_Km)this._dataGridFraisKm.SelectedItem;

                        try
                        {
                            item.Affaire1 = null;
                            ((Frais_Km)this.DataContext).Frais1.Frais_Km.Remove(item);
                            ((App)App.Current).mySitaffEntities.Frais_Km.DeleteObject(item);

                        }
                        catch (Exception)
                        {
                            try
                            {
                                ((Frais_Km)this.DataContext).Frais1.Frais_Km.Remove(item);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    this._dataGridFraisKm.Items.Remove(item);

                                }
                                catch (Exception) { }
                            }
                        }
                    }
                    else
                    {
                        if (this._dataGridFraisKm.SelectedItems.Count > 1)
                        {
                            ObservableCollection<Frais_Km> toRemove = new ObservableCollection<Frais_Km>();
                            foreach (Frais_Km item in this._dataGridFraisKm.SelectedItems.OfType<Frais_Km>())
                            {
                                toRemove.Add(item);
                            }
                            foreach (Frais_Km item in toRemove)
                            {
                                try
                                {
                                    item.Affaire1 = null;
                                    item.Base = 0;
                                    item.Frais1 = null;
                                    item.ImputerAffaire = false;
                                    item.Km = 0;
                                    item.Libelle = null;
                                }
                                catch (Exception) { }
                                try
                                {
                                    ((Frais_Km)this.DataContext).Frais1.Frais_Km.Remove(item);
                                    ((App)App.Current).mySitaffEntities.Frais_Km.DeleteObject(item);
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        ((Frais_Km)this.DataContext).Frais1.Frais_Km.Remove(item);
                                    }
                                    catch (Exception)
                                    {
                                        try
                                        {
                                            this._dataGridFraisKm.Items.Remove(item);
                                        }
                                        catch (Exception) { }
                                    }
                                }
                            }
                        }
                    }
                    this._dataGridFraisKm.Items.Refresh();
                }
                catch (Exception) { }
            }
            RefreshAllTotaux();
        }

        //Met un nouveau Frais dans le DataContext
        public void NouveauFrais()
        {
            bool val;
            Frais newfrais = new Frais();

            /*remplissage du nouveau frais*/
            try
            {
                int moisfin = DateTime.DaysInMonth(int.Parse(DateTime.Today.Year.ToString()), int.Parse(DateTime.Today.Month.ToString()));
                //remplissage date
                DateTime d;
                string datefin = moisfin.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
                val = DateTime.TryParse(datefin, out d);
                newfrais.Date_Debut = datemoiscourant.Date;
                newfrais.Date_Fin = d.Date;

                //remplissage Lot
                string initial = listSalarie.ElementAt(placeSalarieDansList).Personne.Initiales.ToString();
                newfrais.Lot = initial + "-" + DateTime.Today.Month.ToString() + DateTime.Today.Year.ToString();


                newfrais.Salarie1 = this.listSalarie.ElementAt(placeSalarieDansList);
            }
            catch (Exception)
            {
            }



            this.DataContext = newfrais;
            this._dataGridAvancesDemande.Items.Refresh();
            this._dataGridAvancesValide.Items.Refresh();
            this._dataGridFraisGeneraux.Items.Refresh();
            this._dataGridFraisKm.Items.Refresh();
            this._datagridResumeFicheFrais.Items.Refresh();
            this._datagridResumeFraisKm.Items.Refresh();
            RefreshAllTotaux();
        }

        //Recharge les listes listAvanceValider et listAvanceDemande selon le salarié (_comboBoxNomSalarie) et la date du mois courant (datemoiscourant)
        private void RechargeAvances()
        {
            this.listAvanceValider = new ObservableCollection<Avance>(((App)App.Current).mySitaffEntities.Avance.Where(a => a.Frais1 != null));
            ObservableCollection<Avance> listAvanceDemandetemp = new ObservableCollection<Avance>();
            ObservableCollection<Avance> listAvanceValidetemp = new ObservableCollection<Avance>();
            foreach (Avance a in this.listAvanceValider.OfType<Avance>())
            {
                if (a.Salarie1 != null && a.Salarie1 == this.listSalarie.ElementAt(placeSalarieDansList))
                {
                    listAvanceValidetemp.Add(a);
                }
            }
            this.listAvanceValider.Clear();
            this.listAvanceValider = listAvanceValidetemp;

            this.listAvanceDemande = new ObservableCollection<Avance>(((App)App.Current).mySitaffEntities.Avance.Where(a => a.Frais1 == null));
            foreach (Avance a in this.listAvanceDemande.OfType<Avance>())
            {
                if (a.Salarie1 != null && a.Salarie1 == this.listSalarie.ElementAt(placeSalarieDansList))
                {
                    listAvanceDemandetemp.Add(a);
                }
            }
            this.listAvanceDemande.Clear();
            this.listAvanceDemande = listAvanceDemandetemp;
        }

        //Met a jour la date du fin du mois
        private void MetAJourFinMois()
        {
            bool val;
            int nbjourmoisfin = DateTime.DaysInMonth(int.Parse(DateTime.Today.Year.ToString()), int.Parse(this._datePickerDateDeDebut.SelectedDate.Value.Month.ToString()));
            //date de fin du mois
            DateTime d;
            string datefin = nbjourmoisfin.ToString() + "/" + this._datePickerDateDeDebut.SelectedDate.Value.Month.ToString() + "/" + this._datePickerDateDeDebut.SelectedDate.Value.Year.ToString();
            val = DateTime.TryParse(datefin, out d);
            ((Frais)this.DataContext).Date_Fin = d.Date;
        }

        //Recharge le Lot avec les Initiales du salarié
        private void RechargeLotSalarie()
        {
            //remplissage Lot
            string initial = listSalarie.ElementAt(placeSalarieDansList).Personne.Initiales.ToString();
            if (this._datePickerDateDeDebut.SelectedDate != null && this._datePickerDateFin.SelectedDate != null)
            {
                ((Frais)this.DataContext).Lot = initial + "-" + this._datePickerDateDeDebut.SelectedDate.Value.Month + this._datePickerDateFin.SelectedDate.Value.Year;
            }
        }

        //Recharge la variable 'placeSalarieDansList'
        private void RechargePlaceSalarie()
        {
            if (this.listSalarie.Contains((Salarie)this._comboBoxNomSalarie.SelectedItem))
            {
                placeSalarieDansList = this.listSalarie.IndexOf((Salarie)this._comboBoxNomSalarie.SelectedItem);
                this._comboBoxNomSalarie.SelectedIndex = placeSalarieDansList;
                ((Frais)this.DataContext).Salarie1 = this.listSalarie.ElementAt(placeSalarieDansList);
            }
        }

        //retourne la liste des administrateurs du module
        public List<String> GetListAdministrateur()
        {
            this.initialisationListAdministrateur();
            return this.listNomUtilisateurAdministrateurModule;
        }

        //Met l'affaire a null si Imputer n'est pas coché
        private void IsChecked_ImputerFraisKm()
        {
            if (this._dataGridFraisKm.HasItems)
            {
                foreach (Frais_Km fk in ((Frais)this.DataContext).Frais_Km.OfType<Frais_Km>())
                {
                    if (fk.ImputerAffaire == false)
                    {
                        fk.Affaire1 = null;
                    }
                }
            }
        }


        #region Calcul
        //calcul tout
        private void RefreshAllTotaux()
        {
            CalculFicheFrais();
            CalculFraisKm();
            CalculAvance();
        }

        //calcul totaux Fiche_Frais
        private void CalculFicheFrais()
        {
            if (this._dataGridFraisGeneraux.HasItems)
            {
                double totalTTC = 0;
                double totalHT = 0;
                double totalTVA = 0;
                double totalARembourser = 0;
                this.listARecupererFicheFrais.Clear();
                foreach (Fiche_Frais item in this._dataGridFraisGeneraux.Items.OfType<Fiche_Frais>())
                {
                    Fiche_Frais tempff = new Fiche_Frais();
                    tempff.Total_TVA = 0;
                    tempff.Numero = item.Numero;
                    tempff.Date_Fiche = item.Date_Fiche;
                    if (item.Total_HT != null)
                    {
                        totalHT += item.Total_HT.Value;
                    }
                    if (item.Total_TTC != null)
                    {
                        totalTTC += item.Total_TTC.Value;
                    }
                    if (item.Total_TVA != null)
                    {
                        totalTVA += item.Total_TVA.Value;
                    }

                    foreach (Ligne_Fiche_Frais lff in item.Ligne_Fiche_Frais.OfType<Ligne_Fiche_Frais>())
                    {
                        totalARembourser += lff.TVA_Recuperable;
                        tempff.Total_TVA += lff.TVA_Recuperable;
                    }
                    this.listARecupererFicheFrais.Add(tempff);
                }

                ((Frais)this.DataContext).Total_HT = totalHT;
                ((Frais)this.DataContext).Total_TTC = totalTTC;
                ((Frais)this.DataContext).Total_TVA = totalTVA;
                ((Frais)this.DataContext).Total_A_Rembourser = totalARembourser;
            }
        }

        //calcul totaux Frais_Km
        private void CalculFraisKm()
        {
            if (this._dataGridFraisKm.HasItems)
            {
                double Arembourser = 0;
                try
                {
                    listFraisKmARembourse.Clear();
                    foreach (Frais_Km item in this._dataGridFraisKm.Items.OfType<Frais_Km>())
                    {
                        //calcul
                        if (item.Km != null && item.Base != null)
                        {
                            int cv = 0; int.TryParse(item.Base.ToString(), out cv);
                            //Arembourser += baremeKM(item.Km, cv);
                            Arembourser += item.Km * item.Base;
                            Frais_Km fk = new Frais_Km();
                            //fk.Km = baremeKM(item.Km, cv);
                            fk.Km = item.Km * item.Base;
                            fk.Libelle = item.Libelle;
                            listFraisKmARembourse.Add(fk);//ajoute le frais Km a la liste des détails des frais Km
                        }
                    }
                }
                catch (Exception) { }
                ((Frais)this.DataContext).Total_A_Rembourser += Arembourser;
            }
        }

        //calcul totaux Avance
        private void CalculAvance()
        {
            if (this._dataGridAvancesValide.HasItems)
            {
                double totalAvanceVerser = 0;
                foreach (Avance item in this._dataGridAvancesValide.Items)
                {
                    totalAvanceVerser += item.Somme.Value;
                }
                ((Frais)this.DataContext).Total_Avance = totalAvanceVerser;
            }
        }

        //on cherche le coefficient correspondant a la comboBox _comboBoxTypeChevauxFiscaux
        private double ChoixCoefficientKm()
        {
            double coef = 0;
            /* On cherche le coefficient qui correspond au libelle selectionner dans la comboBox _comboBoxTypeChevauxFiscaux
              
            */
            return coef;
        }

        #endregion

        #region Afficher
        //Affiche le boutton DroiteGauche de Avances
        private void AfficherBtnPasserAvancesEnDemande()
        {
            this._buttonPasserAvancesEnDemande.Visibility = System.Windows.Visibility.Visible;
        }

        //Affiche le boutton DroiteGauche de Avances
        private void AfficherBtnPasserAvancesEnValide()
        {
            this._buttonPasserAvancesEnValide.Visibility = System.Windows.Visibility.Visible;
        }
        #endregion

        #region Cacher
        //Cache le boutton DroiteGauche de Avances
        private void CacherBtnPasserAvancesEnDemande()
        {
            this._buttonPasserAvancesEnDemande.Visibility = System.Windows.Visibility.Collapsed;
        }

        //Cache le boutton DroiteGauche de Avances
        private void CacherBtnPasserAvancesEnValide()
        {
            this._buttonPasserAvancesEnValide.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion

        #endregion

        #region Lecture seule
        public void lectureSeule()
        {
            //textBox
            this._textBoxHT.IsReadOnly = true;
            this._textBoxLot.IsReadOnly = true;
            this._textBoxTotalTTC.IsReadOnly = true;
            this._textBoxTotalTVArecup.IsReadOnly = true;
            this._textBoxTotalAvance.IsReadOnly = true;
            this._textBoxTotalARembourser.IsReadOnly = true;

            this._datePickerDateDeDebut.IsEnabled = false;
            this._datePickerDateFin.IsEnabled = false;

            //comboBox
            this._comboBoxNomSalarie.IsEnabled = false;
            this._comboBoxNomSalarie.IsReadOnly = true;


            //datagrid
            this._dataGridAvancesValide.IsReadOnly = true;
            this._dataGridAvancesDemande.IsReadOnly = true;
            this._dataGridFraisGeneraux.IsReadOnly = true;
            this._dataGridFraisKm.IsReadOnly = true;
            this._datagridResumeFicheFrais.IsReadOnly = true;
            this._datagridResumeFraisKm.IsReadOnly = true;

            this._dataGridAvancesValideColumnDate.IsReadOnly = true;
            this._dataGridAvancesValideColumnSomme.IsReadOnly = true;
            this._dataGridAvancesDemandeColumnDate.IsReadOnly = true;
            this._dataGridAvancesDemandeColumnSomme.IsReadOnly = true;

            this._dataGridFraisGenerauxColumnDate.IsReadOnly = true;
            this._dataGridFraisGenerauxColumnNumero.IsReadOnly = true;
            this._dataGridFraisGenerauxColumnTotalHT.IsReadOnly = true;
            this._dataGridFraisGenerauxColumnTotalTVA.IsReadOnly = true;
            this._dataGridFraisGenerauxColumnTotalTTC.IsReadOnly = true;

            this._dataGridFraisKmColumnAffaire.IsReadOnly = true;
            this._dataGridFraisKmColumnBase.IsReadOnly = true;
            this._dataGridFraisKmColumnImputerSurAffaire.IsReadOnly = true;
            this._dataGridFraisKmColumnKm.IsReadOnly = true;
            this._dataGridFraisKmColumnLibelle.IsReadOnly = true;

            //bouton
            this._buttonAjouterFraisGeneraux.IsEnabled = false;
            this._buttonAnnuler.IsEnabled = true;
            this._buttonCollerFraisKm.IsEnabled = false;
            this._buttonSupprimerFraisKm.IsEnabled = false;
            this._buttonPasserAvancesEnDemande.IsEnabled = false;
            this._buttonPasserAvancesEnValide.IsEnabled = false;
            this._buttonModifierFraisGeneraux.IsEnabled = false;
            this._buttonOk.IsEnabled = true;
            this._buttonAjoutAvance.IsEnabled = false;
            this._buttonSupprimerFicheFraisGeneraux.IsEnabled = false;
        }

        private void deletelectureSeule()
        {

            //textBox
            this._textBoxHT.IsReadOnly = false;
            this._textBoxLot.IsReadOnly = false;
            this._textBoxTotalTTC.IsReadOnly = false;
            this._textBoxTotalTVArecup.IsReadOnly = false;
            this._textBoxTotalAvance.IsReadOnly = true;
            this._textBoxTotalARembourser.IsReadOnly = true;

            //datepicker
            this._datePickerDateDeDebut.IsEnabled = true;
            this._datePickerDateFin.IsEnabled = true;

            //comboBox
            this._comboBoxNomSalarie.IsEnabled = true;

            //datagrid
            this._dataGridAvancesValide.IsReadOnly = false;
            this._dataGridAvancesDemande.IsReadOnly = false;
            this._dataGridFraisGeneraux.IsReadOnly = false;
            this._dataGridFraisKm.IsReadOnly = false;
            this._datagridResumeFicheFrais.IsReadOnly = false;
            this._datagridResumeFraisKm.IsReadOnly = false;

            this._dataGridAvancesValideColumnDate.IsReadOnly = false;
            this._dataGridAvancesValideColumnSomme.IsReadOnly = false;
            this._dataGridAvancesDemandeColumnDate.IsReadOnly = false;
            this._dataGridAvancesDemandeColumnSomme.IsReadOnly = false;

            this._dataGridFraisGenerauxColumnDate.IsReadOnly = false;
            this._dataGridFraisGenerauxColumnNumero.IsReadOnly = false;
            this._dataGridFraisGenerauxColumnTotalHT.IsReadOnly = false;
            this._dataGridFraisGenerauxColumnTotalTVA.IsReadOnly = false;
            this._dataGridFraisGenerauxColumnTotalTTC.IsReadOnly = false;

            //this._dataGridFraisKmColumnAffaire.IsReadOnly = false;
            this._dataGridFraisKmColumnBase.IsReadOnly = false;
            this._dataGridFraisKmColumnImputerSurAffaire.IsReadOnly = false;
            this._dataGridFraisKmColumnKm.IsReadOnly = false;
            this._dataGridFraisKmColumnLibelle.IsReadOnly = false;

            //bouton
            this._buttonAjouterFraisGeneraux.IsEnabled = true;
            this._buttonAnnuler.IsEnabled = true;
            this._buttonCollerFraisKm.IsEnabled = true;
            this._buttonSupprimerFraisKm.IsEnabled = true;
            this._buttonPasserAvancesEnDemande.IsEnabled = true;
            this._buttonPasserAvancesEnValide.IsEnabled = true;
            this._buttonModifierFraisGeneraux.IsEnabled = true;
            this._buttonOk.IsEnabled = true;
            this._buttonAjoutAvance.IsEnabled = true;
            this._buttonSupprimerFicheFraisGeneraux.IsEnabled = true;
        }
        #endregion

        #region Affichage Salarie
        private void AffichageSalarie()
        {
            //Désactive la comboBox pour changer le nom du salarié
            this._comboBoxNomSalarie.IsEnabled = false;

            //cache les boutons pour valider les Avances
            CacherBtnPasserAvancesEnDemande();
            CacherBtnPasserAvancesEnValide();

        }
        #endregion

        #region Affichage Administration
        private void AffichageAdministration()
        {
            //Affiche les boutons pour valider les Avances
            AfficherBtnPasserAvancesEnDemande();
            AfficherBtnPasserAvancesEnValide();

            this.Title = "Notes de frais - Affichage Administrateur";
        }
        #endregion

    }
}
