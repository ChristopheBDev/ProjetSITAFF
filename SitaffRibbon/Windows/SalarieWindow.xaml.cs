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
/* 
 * Using pour utilisation des IObservableCollection (afin d'éviter de mettre
 * System.Collections.ObjectModel.IObservableCollection en entier)
 */
using System.Collections.ObjectModel;
//Using pour utiliser le type TypeConverter pour la conversion de couleur
using System.ComponentModel;
using SitaffRibbon.Classes;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour SalarieWindow.xaml
    /// </summary>
    public partial class SalarieWindow : Window
    {
        public bool creation = false;

        public SalarieWindow()
        {
            InitializeComponent();            
        }

        #region Propriétés de dépendances



        public ObservableCollection<Outillage> listOutillage
        {
            get { return (ObservableCollection<Outillage>)GetValue(listOutillageProperty); }
            set { SetValue(listOutillageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listOutillage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listOutillageProperty =
            DependencyProperty.Register("listOutillage", typeof(ObservableCollection<Outillage>), typeof(SalarieWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Visite_Medicale> listVisiteMedicale
        {
            get { return (ObservableCollection<Visite_Medicale>)GetValue(listVisiteMedicaleProperty); }
            set { SetValue(listVisiteMedicaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListContrats.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listVisiteMedicaleProperty =
            DependencyProperty.Register("listVisiteMedicale", typeof(ObservableCollection<Visite_Medicale>), typeof(SalarieWindow), new UIPropertyMetadata(null));


        public ObservableCollection<Contrat> ListContrats
        {
            get { return (ObservableCollection<Contrat>)GetValue(ListContratsProperty); }
            set { SetValue(ListContratsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListContrats.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListContratsProperty =
            DependencyProperty.Register("ListContrats", typeof(ObservableCollection<Contrat>), typeof(SalarieWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Agence_Interimaire> ListAgences
        {
            get { return (ObservableCollection<Agence_Interimaire>)GetValue(ListAgencesProperty); }
            set { SetValue(ListAgencesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListAgences.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListAgencesProperty =
            DependencyProperty.Register("ListAgences", typeof(ObservableCollection<Agence_Interimaire>), typeof(SalarieWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Pays> PaysListPro
        {
            get { return (ObservableCollection<Pays>)GetValue(PaysListProProperty); }
            set { SetValue(PaysListProProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PaysListPro.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaysListProProperty =
            DependencyProperty.Register("PaysListPro", typeof(ObservableCollection<Pays>), typeof(SalarieWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Pays> PaysListPerso
        {
            get { return (ObservableCollection<Pays>)GetValue(PaysListPersoProperty); }
            set { SetValue(PaysListPersoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PaysListPerso.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaysListPersoProperty =
            DependencyProperty.Register("PaysListPerso", typeof(ObservableCollection<Pays>), typeof(SalarieWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Ville> VilleListPro
        {
            get { return (ObservableCollection<Ville>)GetValue(VilleListProProperty); }
            set { SetValue(VilleListProProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VilleListPro.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VilleListProProperty =
            DependencyProperty.Register("VilleListPro", typeof(ObservableCollection<Ville>), typeof(SalarieWindow), new UIPropertyMetadata(null));

        public ObservableCollection<Ville> VilleListPerso
        {
            get { return (ObservableCollection<Ville>)GetValue(VilleListPersoProperty); }
            set { SetValue(VilleListPersoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VilleListPerso.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VilleListPersoProperty =
            DependencyProperty.Register("VilleListPerso", typeof(ObservableCollection<Ville>), typeof(SalarieWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Entreprise_Mere> listEntreprise_Mere
        {
            get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntreprise_MereProperty); }
            set { SetValue(listEntreprise_MereProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntreprise_Mere.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntreprise_MereProperty =
            DependencyProperty.Register("listEntreprise_Mere", typeof(ObservableCollection<Entreprise_Mere>), typeof(SalarieWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Civilite> ListCivilite
        {
            get { return (ObservableCollection<Civilite>)GetValue(ListCiviliteProperty); }
            set { SetValue(ListCiviliteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListCivilite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListCiviliteProperty =
            DependencyProperty.Register("ListCivilite", typeof(ObservableCollection<Civilite>), typeof(SalarieWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Groupe> ListGroupe
        {
            get { return (ObservableCollection<Groupe>)GetValue(ListGroupeProperty); }
            set { SetValue(ListGroupeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListGroupe.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListGroupeProperty =
            DependencyProperty.Register("ListGroupe", typeof(ObservableCollection<Groupe>), typeof(SalarieWindow), new UIPropertyMetadata(null));



        public ObservableCollection<Entreprise> ListEntreprise
        {
            get { return (ObservableCollection<Entreprise>)GetValue(ListEntrepriseProperty); }
            set { SetValue(ListEntrepriseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListEntreprise.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListEntrepriseProperty =
            DependencyProperty.Register("ListEntreprise", typeof(ObservableCollection<Entreprise>), typeof(SalarieWindow), new UIPropertyMetadata(null));




        #endregion

        #region Boutons

        #region boutons ok / annuler
        /// <summary>
        /// Fonction lancée après clic sur Ok
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerificationChamps())
            {
                if (this.checkBox_SalarieInterimaire.IsChecked == false)
                {
                    ((Personne)this.DataContext).Salarie.Interimaire = null;
                }
                if (this.checkBox_SalarieInterne.IsChecked == false)
                {
                    ((Personne)this.DataContext).Salarie.Salarie_Interne = null;
                    ((Personne)this.DataContext).Salarie.Matricule_Devis = null;
                }
                if (this.checkBox_SalarieTiers.IsChecked == false)
                {
                    ((Personne)this.DataContext).Salarie.Tiers = null;
                }
                if (((Personne)this.DataContext).Adresse2.Rue == "" || ((Personne)this.DataContext).Adresse2.Rue == null)
                {
                    ((Personne)this.DataContext).Adresse2 = null;
                }
                this.DialogResult = true;
                this.Close();
            }
        }

        /// <summary>
        /// Fonction lancée après clic sur Annuler
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
        #endregion

        #region boutons diplomes
        private void _ButtonDiplomesNouveau_Click(object sender, RoutedEventArgs e)
        {
            DiplomeWindow diplomeWindow = new DiplomeWindow();
            ObservableCollection<Diplome> test = new ObservableCollection<Diplome>(((App)App.Current).mySitaffEntities.Diplome.OrderBy(dip => dip.Libelle));
            bool toDelete = false;
            foreach (Diplome di in ((Personne)this.DataContext).Salarie.Diplome)
            {
                toDelete = false;
                if (test.Contains(di))
                {
                    toDelete = true;
                }
                if (toDelete)
                {
                    test.Remove(di);
                }
            }
            diplomeWindow.listDiplome = test;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = diplomeWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                ((Personne)this.DataContext).Salarie.Diplome.Add((Diplome)diplomeWindow.DataContext);
            }
        }

        private void _ButtonDiplomesSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridDiplomes.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un diplôme à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridDiplomes.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les diplômes à supprimer un par un.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ((Personne)this.DataContext).Salarie.Diplome.Remove((Diplome)this._dataGridDiplomes.SelectedItem);
                }
            }
        }
        #endregion

        #region boutons emplois

        private void _ButtonEmploiNouveau_Click(object sender, RoutedEventArgs e)
        {
            QualificationWindow qualificationWindow = new QualificationWindow();
            ObservableCollection<Qualification> test = new ObservableCollection<Qualification>(((App)App.Current).mySitaffEntities.Qualification.OrderBy(qual => qual.Libelle));
            bool toDelete = false;
            foreach (Qualification qu in ((Personne)this.DataContext).Salarie.Qualification)
            {
                toDelete = false;
                if (test.Contains(qu))
                {
                    toDelete = true;
                }
                if (toDelete)
                {
                    test.Remove(qu);
                }
            }
            qualificationWindow.listQualification = test;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = qualificationWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                ((Personne)this.DataContext).Salarie.Qualification.Add((Qualification)qualificationWindow.DataContext);
            }
        }

        private void _ButtonEmploiSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridEmploiOccupe.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un emploi à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridEmploiOccupe.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les emplois à supprimer un par un.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ((Personne)this.DataContext).Salarie.Qualification.Remove((Qualification)this._dataGridEmploiOccupe.SelectedItem);
                }
            }
        }

        #endregion

        #region boutons permis

        private void _ButtonPermisNouveau_Click(object sender, RoutedEventArgs e)
        {
            PermisWindow permisWindow = new PermisWindow();
            Salarie_Permis tmp = new Salarie_Permis();
            tmp.Salarie1 = ((Personne)this.DataContext).Salarie;
            permisWindow.DataContext = tmp;
            ObservableCollection<Permis> test = new ObservableCollection<Permis>(((App)App.Current).mySitaffEntities.Permis.OrderBy(per => per.Libelle));
            bool toDelete = false;
            foreach (Salarie_Permis sp in ((Personne)this.DataContext).Salarie.Salarie_Permis)
            {
                toDelete = false;
                if (test.Contains(sp.Permis1))
                {
                    toDelete = true;
                }
                if (toDelete)
                {
                    test.Remove(sp.Permis1);
                }
            }
            permisWindow.listPermis = test;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = permisWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                this._dataGridPermis.Items.Refresh();
            }
            else
            {
                ((Personne)this.DataContext).Salarie.Salarie_Permis.Remove(tmp);
            }

        }

        private void _ButtonPermisSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridPermis.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un permis à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridPermis.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les permis à supprimer un par un.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ((Personne)this.DataContext).Salarie.Salarie_Permis.Remove((Salarie_Permis)this._dataGridPermis.SelectedItem);
                }
            }
        }

        #endregion

        #region boutons formations

        private void _ButtonFormationNouveau_Click(object sender, RoutedEventArgs e)
        {
            FormationWindow formationWindow = new FormationWindow();
            Salarie_Formation tmp = new Salarie_Formation();
            tmp.Salarie1 = ((Personne)this.DataContext).Salarie;
            formationWindow.DataContext = tmp;
            ObservableCollection<Formation> test = new ObservableCollection<Formation>(((App)App.Current).mySitaffEntities.Formation.OrderBy(form => form.Libelle));
            bool toDelete = false;
            foreach (Salarie_Formation sp in ((Personne)this.DataContext).Salarie.Salarie_Formation)
            {
                toDelete = false;
                if (test.Contains(sp.Formation1))
                {
                    toDelete = true;
                }
                if (toDelete)
                {
                    test.Remove(sp.Formation1);
                }
            }
            formationWindow.listFormations = test;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = formationWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                this._dataGridFormations.Items.Refresh();
            }
            else
            {
                ((Personne)this.DataContext).Salarie.Salarie_Formation.Remove(tmp);
            }

        }

        private void _ButtonFormationSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridFormations.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une formation à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridFormations.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les formations à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ((Personne)this.DataContext).Salarie.Salarie_Formation.Remove((Salarie_Formation)this._dataGridFormations.SelectedItem);
                }
            }
        }

        #endregion

        #region boutons interimaire competences

        private void _ButtonInterimaireCompetenceNouveau_Click(object sender, RoutedEventArgs e)
        {
            CompetenceWindow competenceWindow = new CompetenceWindow();
            Interimaire_Competence tmp = new Interimaire_Competence();
            tmp.Interimaire1 = ((Personne)this.DataContext).Salarie.Interimaire;
            competenceWindow.DataContext = tmp;
            ObservableCollection<Competence> test = new ObservableCollection<Competence>(((App)App.Current).mySitaffEntities.Competence.OrderBy(form => form.Libelle));
            bool toDelete = false;
            foreach (Interimaire_Competence sp in ((Personne)this.DataContext).Salarie.Interimaire.Interimaire_Competence)
            {
                toDelete = false;
                if (test.Contains(sp.Competence1))
                {
                    toDelete = true;
                }
                if (toDelete)
                {
                    test.Remove(sp.Competence1);
                }
            }
            competenceWindow.listCompetences = test;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = competenceWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                //Interimaire_Competence tmpToAdd = new Interimaire_Competence();
                //tmpToAdd.Competence1 = ((Interimaire_Competence)competenceWindow.DataContext).Competence1;
                //((Personne)this.DataContext).Salarie.Interimaire.Interimaire_Competence.Add(tmpToAdd);
                this._dataGridCompetencesInterimaire.ItemsSource = ((Personne)this.DataContext).Salarie.Interimaire.Interimaire_Competence;
                this._dataGridCompetencesInterimaire.Items.Refresh();
                //MessageBox.Show(((Personne)this.DataContext).Salarie.Interimaire.Interimaire_Competence.ToString(), "test");
            }
            else
            {
                ((Personne)this.DataContext).Salarie.Interimaire.Interimaire_Competence.Remove(tmp);
            }
        }

        private void _ButtonInterimaireCompetenceSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridCompetencesInterimaire.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une compétence à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridCompetencesInterimaire.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les compétences à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ((Personne)this.DataContext).Salarie.Interimaire.Interimaire_Competence.Remove((Interimaire_Competence)this._dataGridCompetencesInterimaire.SelectedItem);
                }
            }
        }

        #endregion

        #region boutons tiers competences

        private void _ButtonTiersCompetenceNouveau_Click(object sender, RoutedEventArgs e)
        {
            CompetenceWindow competenceWindow = new CompetenceWindow();
            Interimaire_Competence tmp = new Interimaire_Competence();
            Tiers_Competence tmp2 = new Tiers_Competence();
            tmp2.Tiers1 = ((Personne)this.DataContext).Salarie.Tiers;
            //tmp.Interimaire1 = ((Personne)this.DataContext).Salarie.Interimaire;
            competenceWindow.DataContext = tmp;
            ObservableCollection<Competence> test = new ObservableCollection<Competence>(((App)App.Current).mySitaffEntities.Competence.OrderBy(form => form.Libelle));
            bool toDelete = false;
            foreach (Tiers_Competence sp in ((Personne)this.DataContext).Salarie.Tiers.Tiers_Competence)
            {
                toDelete = false;
                if (test.Contains(sp.Competence1))
                {
                    toDelete = true;
                }
                if (toDelete)
                {
                    test.Remove(sp.Competence1);
                }
            }
            competenceWindow.listCompetences = test;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = competenceWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                tmp2.Competence1 = tmp.Competence1;
                this._dataGridCompetencesTiers.ItemsSource = ((Personne)this.DataContext).Salarie.Tiers.Tiers_Competence;
                this._dataGridCompetencesTiers.Items.Refresh();
            }
            else
            {
                ((Personne)this.DataContext).Salarie.Interimaire.Interimaire_Competence.Remove(tmp);
            }
        }

        private void _ButtonTiersCompetenceSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridCompetencesTiers.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une compétence à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridCompetencesTiers.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les compétences à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ((Personne)this.DataContext).Salarie.Tiers.Tiers_Competence.Remove((Tiers_Competence)this._dataGridCompetencesTiers.SelectedItem);
                }
            }
        }

        #endregion

        #region boutons habilitations

        private void _ButtonHabilitationNouveau_Click(object sender, RoutedEventArgs e)
        {
            HabilitationWindow habilitationWindow = new HabilitationWindow();
            Salarie_Habilitation tmp = new Salarie_Habilitation();
            tmp.Salarie1 = ((Personne)this.DataContext).Salarie;
            habilitationWindow.DataContext = tmp;
            ObservableCollection<Habilitation> test = new ObservableCollection<Habilitation>(((App)App.Current).mySitaffEntities.Habilitation.OrderBy(hab => hab.Libelle));
            bool toDelete = false;
            foreach (Salarie_Habilitation sp in ((Personne)this.DataContext).Salarie.Salarie_Habilitation)
            {
                toDelete = false;
                if (test.Contains(sp.Habilitation1))
                {
                    toDelete = true;
                }
                if (toDelete)
                {
                    test.Remove(sp.Habilitation1);
                }
            }
            habilitationWindow.listHabilitations = test;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = habilitationWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                this._dataGridHabilitations.Items.Refresh();
            }
            else
            {
                ((Personne)this.DataContext).Salarie.Salarie_Habilitation.Remove(tmp);
            }

        }

        private void _ButtonHabilitationSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridHabilitations.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une formation à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridHabilitations.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les habilitations à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ((Personne)this.DataContext).Salarie.Salarie_Habilitation.Remove((Salarie_Habilitation)this._dataGridHabilitations.SelectedItem);
                }
            }
        }

        #endregion

        #region boutons Mobilite Interimaire

        private void _ButtonInterimaireMobiliteNouveau_Click(object sender, RoutedEventArgs e)
        {
            DepartementWindow departementWindow = new DepartementWindow();
            ObservableCollection<Departement> test = new ObservableCollection<Departement>(((App)App.Current).mySitaffEntities.Departement.OrderBy(qual => qual.Libelle));
            bool toDelete = false;
            foreach (Departement qu in ((Personne)this.DataContext).Salarie.Interimaire.Departement)
            {
                toDelete = false;
                if (test.Contains(qu))
                {
                    toDelete = true;
                }
                if (toDelete)
                {
                    test.Remove(qu);
                }
            }
            departementWindow.listDepartements = test;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = departementWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                ((Personne)this.DataContext).Salarie.Interimaire.Departement.Add((Departement)departementWindow.DataContext);
                this._dataGridMobiliteInterimaire.ItemsSource = ((Personne)this.DataContext).Salarie.Interimaire.Departement;
            }
        }

        private void _ButtonInterimaireMobiliteSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridMobiliteInterimaire.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un département de mobilité à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridMobiliteInterimaire.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les départements de mobilité à supprimer un par un.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ((Personne)this.DataContext).Salarie.Interimaire.Departement.Remove((Departement)this._dataGridMobiliteInterimaire.SelectedItem);
                }
            }
        }

        #endregion

        #region boutons Mobilite Tiers

        private void _ButtonTiersMobiliteNouveau_Click(object sender, RoutedEventArgs e)
        {
            DepartementWindow departementWindow = new DepartementWindow();
            ObservableCollection<Departement> test = new ObservableCollection<Departement>(((App)App.Current).mySitaffEntities.Departement.OrderBy(qual => qual.Libelle));
            bool toDelete = false;
            foreach (Departement qu in ((Personne)this.DataContext).Salarie.Tiers.Departement)
            {
                toDelete = false;
                if (test.Contains(qu))
                {
                    toDelete = true;
                }
                if (toDelete)
                {
                    test.Remove(qu);
                }
            }
            departementWindow.listDepartements = test;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = departementWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                ((Personne)this.DataContext).Salarie.Tiers.Departement.Add((Departement)departementWindow.DataContext);
                this._dataGridMobiliteTiers.ItemsSource = ((Personne)this.DataContext).Salarie.Tiers.Departement;
            }
        }

        private void _ButtonTiersMobiliteSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridMobiliteTiers.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un département de mobilité à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridMobiliteTiers.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les départements de mobilité à supprimer un par un.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ((Personne)this.DataContext).Salarie.Tiers.Departement.Remove((Departement)this._dataGridMobiliteTiers.SelectedItem);
                }
            }
        }

        #endregion

        #region boutons visite medicale

        private void _ButtonInterneVisiteMedicaleNouveau_Click(object sender, RoutedEventArgs e)
        {
            VisiteMedicaleWindow visiteMedicaleWindow = new VisiteMedicaleWindow();
            Visite_Medicale tmp = new Visite_Medicale();
            tmp.Adresse1 = new Adresse();
            tmp.Salarie_Interne1 = ((Personne)this.DataContext).Salarie.Salarie_Interne;
            visiteMedicaleWindow.DataContext = tmp;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = visiteMedicaleWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                this._dataGridVisiteMedicaleInterne.Items.Refresh();
            }
            else
            {
                ((Personne)this.DataContext).Salarie.Salarie_Interne.Visite_Medicale.Remove(tmp);
            }
        }

        private void _ButtonInterneVisiteMedicaleModifier_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridVisiteMedicaleInterne.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vous devez sélectionner une visite médicale à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridVisiteMedicaleInterne.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vous ne devez sélectionner qu'une visite médicale à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (this._dataGridVisiteMedicaleInterne.SelectedItem != null)
            {
                VisiteMedicaleWindow visiteMedicaleWindow = new VisiteMedicaleWindow();
                visiteMedicaleWindow.DataContext = (Visite_Medicale)this._dataGridVisiteMedicaleInterne.SelectedItem;


                bool? dialogResult = visiteMedicaleWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    this._dataGridVisiteMedicaleInterne.Items.Refresh();
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, (Visite_Medicale)visiteMedicaleWindow.DataContext);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
        }

        private void _ButtonInterneVisiteMedicaleSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridVisiteMedicaleInterne.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une visite médicale à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridVisiteMedicaleInterne.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les visites médicales à supprimer une par une.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Visite_Medicale.DeleteObject((Visite_Medicale)this._dataGridVisiteMedicaleInterne.SelectedItem);
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        ((Personne)this.DataContext).Salarie.Salarie_Interne.Visite_Medicale.Remove((Visite_Medicale)this._dataGridVisiteMedicaleInterne.SelectedItem);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }


        #endregion

        #region bouton outillage

        private void _ButtonVersDroite_click(object sender, RoutedEventArgs e)
        {
            if (this._listBoxOutillage.SelectedItem != null && this._listBoxOutillage.SelectedItems.Count == 1)
            {
                Salarie_Outillage temp = new Salarie_Outillage();
                temp.Outillage1 = (Outillage)this._listBoxOutillage.SelectedItem;
                ((Personne)this.DataContext).Salarie.Salarie_Outillage.Add(temp);
                this.listOutillage.Remove((Outillage)this._listBoxOutillage.SelectedItem);
            }
        }

        private void _ButtonVersGauche_click(object sender, RoutedEventArgs e)
        {
            if (this._DataGridSalarie_Outillage.SelectedItem != null && this._DataGridSalarie_Outillage.SelectedItems.Count == 1)
            {
                this.listOutillage.Add((Outillage)((Salarie_Outillage)this._DataGridSalarie_Outillage.SelectedItem).Outillage1);
                ((Personne)this.DataContext).Salarie.Salarie_Outillage.Remove((Salarie_Outillage)this._DataGridSalarie_Outillage.SelectedItem);
            }
        }

        #endregion

        #region boutons repondeur

        private void _ButtonRepondeurNouveau_Click(object sender, RoutedEventArgs e)
        {
            RepondeurCongeWindow repondeurCongeWindow = new RepondeurCongeWindow();
            repondeurCongeWindow.DataContext = new Salarie_Repondeur();
            ObservableCollection<Salarie> listDeSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.Where(sa => sa.Salarie_Interne != null).OrderBy(sal => sal.Personne.Nom).ThenBy(sala => sala.Personne.Prenom));
            bool toDelete = false;
            foreach (Salarie_Repondeur sa in ((Personne)this.DataContext).Salarie.Salarie_Repondeur)
            {
                toDelete = false;
                if (listDeSalarie.Contains(sa.Salarie2))
                {
                    toDelete = true;
                }
                if (toDelete)
                {
                    listDeSalarie.Remove(sa.Salarie2);
                }
            }
            repondeurCongeWindow.listSalaries = listDeSalarie;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = repondeurCongeWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                ((Personne)this.DataContext).Salarie.Salarie_Repondeur.Add((Salarie_Repondeur)repondeurCongeWindow.DataContext);
            }
        }

        private void _ButtonRepondeurSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this._dataGridRepondeurConge.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un répondeur à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (this._dataGridRepondeurConge.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Sélectionnez les répondeurs à supprimer un par un.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ((Personne)this.DataContext).Salarie.Salarie_Repondeur.Remove((Salarie_Repondeur)this._dataGridRepondeurConge.SelectedItem);
                }
            }
        }

        #endregion

        #endregion

        #region Verifications

        /// <summary>
        /// Verifie si tous les champs sont bien renseignés.
        /// </summary>
        /// <returns>booléen vrai si tous les champs sont bien renseignés, sinon retourne faux</returns>
        private bool VerificationChamps()
        {
            bool verif = true;

            if (!verif_tab_info_general())
            {
                verif = false;
            }
            if (!verif_tab_VetementsTravail(true))
            {
                verif = false;
            }
            if (!verif_tab_comptabilite())
            {
                verif = false;
            }
            if (!verif_tab_Competences())
            {
                verif = false;
            }
            if (!verif_tab_info_professionnelles())
            {
                verif = false;
            }

            return verif;
        }

        #region Tab Info General

        private bool verif_tab_info_general()
        {
            bool test = true;

            if (!verif_GroupBoxCoordonnees())
            {
                test = false;
            }
            if (!verif_GroupBoxLocalisation())
            {
                test = false;
            }
            if (!verif_GroupBoxCorrespondance())
            {
                test = false;
            }
            if (!verif_GroupBoxInfoComplementaires())
            {
                test = false;
            }
            if (!verif_GroupBoxEmploiOccupe())
            {
                test = false;
            }
            if (!verif_GroupBoxEntreprise())
            {
                test = false;
            }


            if (test == true)
            {
                this._tabInformationsGenerales.Background = Brushes.Green;
            }
            else
            {
                this._tabInformationsGenerales.Background = Brushes.Red;
            }

            return test;
        }

        #region GroupBox Coordonnées

        private bool verif_GroupBoxCoordonnees()
        {
            bool test = true;

            if (!verif_tabCoordonneesIdentite())
            {
                test = false;
            }
            if (!verif_tabCoordonneesIdentiteSociale())
            {
                test = false;
            }

            if (test == true)
            {
                this._groupBoxCoordonnees.Foreground = Brushes.Green;
            }
            else
            {
                this._groupBoxCoordonnees.Foreground = Brushes.Red;
            }

            return test;
        }

        #region Tab Identité

        private bool verif_tabCoordonneesIdentite()
        {
            bool test = true;

            if (!Verif_TextBoxSalarieNom())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieNomJeuneFille())
            {
                test = false;
            }
            if (!Verif_TextBoxSalariePrenom())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieInitiales())
            {
                test = false;
            }
            if (!Verif_ComboBoxCivilite())
            {
                test = false;
            }

            if (test == true)
            {
                this._tabCoordonneesIdentite.Background = Brushes.Green;
            }
            else
            {
                this._tabCoordonneesIdentite.Background = Brushes.Red;
            }

            return test;
        }

        #region ComboBox Civilite

        private bool Verif_ComboBoxCivilite()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieCivilite.SelectedItem == null)
            {
                verif = false;
                this._TextBlockSalarieCivilité.Foreground = Brushes.Red;
                this._ComboBoxSalarieCivilite.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieCivilité.Foreground = Brushes.Green;
                this._ComboBoxSalarieCivilite.Background = vert;
            }

            return verif;
        }

        private void _ComboBoxSalarieCivilite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxCivilite();
        }

        #endregion

        #region Champs Nom
        private bool Verif_TextBoxSalarieNom()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieNom.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBlockSalarieNom.Foreground = Brushes.Red;
                this._TextBoxSalarieNom.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieNom.Foreground = Brushes.Green;
                this._TextBoxSalarieNom.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalarieNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieNom();
            this.constructionInitiales();
        }

        private void _TextBoxSalarieNom_LostFocus(object sender, RoutedEventArgs e)
        {
            this._TextBoxSalarieNom.Text = this._TextBoxSalarieNom.Text.ToUpper();
        }

        #endregion

        #region Champs Nom de jeune fille
        private bool Verif_TextBoxSalarieNomJeuneFille()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            //if (this._TextBoxSalarieNomJeuneFille.Text.Trim().Length <= 0)
            //{
            //    verif = false;
            //    this._TextBlockSalarieNomJeuneFille.Foreground = Brushes.Red;
            //    this._TextBoxSalarieNomJeuneFille.Background = rouge;
            //}
            //else
            //{
            verif = true;
            this._TextBlockSalarieNomJeuneFille.Foreground = Brushes.Green;
            this._TextBoxSalarieNomJeuneFille.Background = vert;
            //}

            return verif;
        }

        private void _TextBoxSalarieNomJeuneFille_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieNomJeuneFille();
        }

        private void _TextBoxSalarieNomJeuneFille_LostFocus(object sender, RoutedEventArgs e)
        {
            this._TextBoxSalarieNomJeuneFille.Text = this._TextBoxSalarieNomJeuneFille.Text.ToUpper();
        }

        #endregion

        #region Champs Prenom
        private bool Verif_TextBoxSalariePrenom()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalariePrenom.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBlockSalariePrenom.Foreground = Brushes.Red;
                this._TextBoxSalariePrenom.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockSalariePrenom.Foreground = Brushes.Green;
                this._TextBoxSalariePrenom.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalariePrenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalariePrenom();
            this.constructionInitiales();
        }

        #endregion

        #region Champs Initiales
        private bool Verif_TextBoxSalarieInitiales()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieInitiales.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBlockSalarieInitiales.Foreground = Brushes.Red;
                this._TextBoxSalarieInitiales.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieInitiales.Foreground = Brushes.Green;
                this._TextBoxSalarieInitiales.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalarieInitiales_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieInitiales();
        }

        #endregion

        #region Champs RIB


        #region Verification General RIB
        public bool verificationRIBcomplet()
        {
            bool test = false;

            if (_TextBoxEtablissement.Text.Trim().Length != 0 &&
                _TextBoxGuichet.Text.Trim().Length != 0 &&
                _TextBoxCompte.Text.Trim().Length != 0 &&
                _TextBoxClé.Text.Trim().Length != 0)
            {
                test = true;
            }

            return test;
        }
        public bool verificationRIBnull()
        {
            bool test = false;

            if (_TextBoxEtablissement.Text.Trim().Length == 0 &&
                _TextBoxGuichet.Text.Trim().Length == 0 &&
                _TextBoxCompte.Text.Trim().Length == 0 &&
                _TextBoxClé.Text.Trim().Length == 0)
            {
                test = true;
            }

            return test;
        }

        public bool verificationRIB()
        {
            bool test = true;

            if (!Verif_TextBoxEtablissement())
            {
                test = false;
            }
            if (!Verif_TextBoxGuichet())
            {
                test = false;
            }
            if (!Verif_TextBoxCompte())
            {
                test = false;
            }
            if (!Verif_TextBoxClé())
            {
                test = false;
            }
            if (!verificationRIBcomplet())
            {
                test = false;
            }
            if (verificationRIBnull())
            {
                test = true;
            }

            return test;

        }
        #endregion

        #region Champs Etablissement
        private bool Verif_TextBoxEtablissement()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxEtablissement.Text.Trim().Length < 0)
            {
                verif = false;
                this._TextBoxEtablissement.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBoxEtablissement.Background = vert;
            }

            if (verificationRIBcomplet() || verificationRIBnull())
            {
                this._TextBlockInfoRIB.Foreground = Brushes.Green;
            }
            else
            {
                this._TextBlockInfoRIB.Foreground = Brushes.Red;
            }


            return verif;
        }

        private void _TextBoxEtablissement_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxEtablissement();
        }
        #endregion

        #region Champs Guichet
        private bool Verif_TextBoxGuichet()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxGuichet.Text.Trim().Length < 0)
            {
                verif = false;
                this._TextBoxGuichet.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBoxGuichet.Background = vert;
            }

            if (verificationRIBcomplet() || verificationRIBnull())
            {
                this._TextBlockInfoRIB.Foreground = Brushes.Green;
            }
            else
            {
                this._TextBlockInfoRIB.Foreground = Brushes.Red;
            }

            return verif;
        }

        private void _TextBoxGuichet_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxGuichet();
        }
        #endregion

        #region Champs Compte
        private bool Verif_TextBoxCompte()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxCompte.Text.Trim().Length < 0)
            {
                verif = false;
                this._TextBoxCompte.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBoxCompte.Background = vert;
            }

            if (verificationRIBcomplet() || verificationRIBnull())
            {
                this._TextBlockInfoRIB.Foreground = Brushes.Green;
            }
            else
            {
                this._TextBlockInfoRIB.Foreground = Brushes.Red;
            }

            return verif;
        }

        private void _TextBoxCompte_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCompte();
        }
        #endregion

        #region Champs Clé
        private bool Verif_TextBoxClé()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#80FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxClé.Text.Trim().Length < 0)
            {
                verif = false;
                this._TextBoxClé.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBoxClé.Background = vert;
            }

            if (verificationRIBcomplet() || verificationRIBnull())
            {
                this._TextBlockInfoRIB.Foreground = Brushes.Green;
            }
            else
            {
                this._TextBlockInfoRIB.Foreground = Brushes.Red;
            }

            return verif;
        }

        private void _TextBoxClé_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxClé();
        }
        #endregion

        #endregion

        #endregion

        #region Identité sociale

        private bool verif_tabCoordonneesIdentiteSociale()
        {
            bool test = true;

            if (!Verif_DatePickerSalarieDateNaissance())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieLieuNaissance())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieNumeroSecuriteSociale())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieNumeroPasseport())
            {
                test = false;
            }
            if (!Verif_DatePickerSalarieDateValiditePasseport())
            {
                test = false;
            }

            if (test == true)
            {
                this._tabCoordonneesIdentiteSociale.Background = Brushes.Green;
            }
            else
            {
                this._tabCoordonneesIdentiteSociale.Background = Brushes.Red;
            }

            return test;
        }

        #region Champs Date de naissance
        private bool Verif_DatePickerSalarieDateNaissance()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._DatePickerSalarieDateNaissance.SelectedDate == null)
            {
                //verif = false;
                //this._TextBlockSalarieDateNaissance.Foreground = Brushes.Red;
                //this._DatePickerSalarieDateNaissance.Background = rouge;
                verif = true;
                this._TextBlockSalarieDateNaissance.Foreground = Brushes.Green;
                this._DatePickerSalarieDateNaissance.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieDateNaissance.Foreground = Brushes.Green;
                this._DatePickerSalarieDateNaissance.Background = vert;
            }

            return verif;
        }

        private void _DatePickerSalarieDateNaissance_SelectedDateChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_DatePickerSalarieDateNaissance();
        }

        #endregion

        #region Champs Lieu de naissance
        private bool Verif_TextBoxSalarieLieuNaissance()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieLieuNaissance.Text.Trim().Length <= 0)
            {
                verif = true;
                this._TextBlockSalarieLieuNaissance.Foreground = Brushes.Green;
                this._TextBoxSalarieLieuNaissance.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieLieuNaissance.Foreground = Brushes.Green;
                this._TextBoxSalarieLieuNaissance.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalarieLieuNaissance_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieLieuNaissance();
        }

        #endregion

        #region Champs Numero de securite sociale
        private bool Verif_TextBoxSalarieNumeroSecuriteSociale()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieNumeroSecuriteSociale.Text.Trim().Length != 0)
            {
                double test;
                if (double.TryParse(this._TextBoxSalarieNumeroSecuriteSociale.Text, out test))
                {
                    verif = true;
                    this._TextBlockSalarieNumeroSecuriteSociale.Foreground = Brushes.Green;
                    this._TextBoxSalarieNumeroSecuriteSociale.Background = vert;
                }
                else
                {
                    verif = false;
                    this._TextBlockSalarieNumeroSecuriteSociale.Foreground = Brushes.Red;
                    this._TextBoxSalarieNumeroSecuriteSociale.Background = rouge;
                }
            }
            else
            {
                verif = true;
                this._TextBlockSalarieNumeroSecuriteSociale.Foreground = Brushes.Green;
                this._TextBoxSalarieNumeroSecuriteSociale.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalarieNumeroSecuriteSociale_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieNumeroSecuriteSociale();
        }

        #endregion

        #region Champs Numero de passeport
        private bool Verif_TextBoxSalarieNumeroPasseport()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieNumeroPasseport.Text.Trim().Length <= 0)
            {
                //verif = false;
                //this._TextBlockSalarieNumeroPasseport.Foreground = Brushes.Red;
                //this._TextBoxSalarieNumeroPasseport.Background = rouge;
                verif = true;
                this._TextBlockSalarieNumeroPasseport.Foreground = Brushes.Green;
                this._TextBoxSalarieNumeroPasseport.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieNumeroPasseport.Foreground = Brushes.Green;
                this._TextBoxSalarieNumeroPasseport.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalarieNumeroPasseport_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieNumeroPasseport();
        }

        #endregion

        #region Champs Date validité passeport
        private bool Verif_DatePickerSalarieDateValiditePasseport()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._DatePickerSalarieDateValiditePasseport.SelectedDate == null)
            {
                if (this._TextBoxSalarieNumeroPasseport.Text.Trim().Length <= 0)
                {
                    verif = true;
                    this._TextBlockSalarieDateValiditePasseport.Foreground = Brushes.Green;
                    this._DatePickerSalarieDateValiditePasseport.Background = vert;
                }
                else
                {
                    verif = false;
                    this._TextBlockSalarieDateValiditePasseport.Foreground = Brushes.Red;
                    this._DatePickerSalarieDateValiditePasseport.Background = rouge;
                }
            }
            else
            {
                verif = true;
                this._TextBlockSalarieDateValiditePasseport.Foreground = Brushes.Green;
                this._DatePickerSalarieDateValiditePasseport.Background = vert;
            }

            return verif;
        }

        private void _DatePickerSalarieDateValiditePasseport_SelectedDateChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_DatePickerSalarieDateValiditePasseport();
        }

        #endregion

        #endregion

        #endregion

        #region GroupBox Localisation

        private bool verif_GroupBoxLocalisation()
        {
            bool test = true;

            if (!verif_tab_Personnelle())
            {
                test = false;
            }
            if (!verif_tab_Professionnelle())
            {
                test = false;
            }

            if (test == true)
            {
                this._groupBoxLocalisation.Foreground = Brushes.Green;
            }
            else
            {
                this._groupBoxLocalisation.Foreground = Brushes.Red;
            }

            return test;
        }

        #region Tab Personnelle

        private bool verif_tab_Personnelle()
        {
            bool test = true;

            if (!Verif_TextBoxSalarieAdressePerso())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieAdresseComplementairePerso())
            {
                test = false;
            }
            if (!Verif_ComboBoxSalarieVillePerso())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieCodePostalPerso())
            {
                test = false;
            }
            if (!Verif_ComboBoxSalariePaysPerso())
            {
                test = false;
            }

            if (test == true)
            {
                this._tabLocalisationPersonnelle.Background = Brushes.Green;
            }
            else
            {
                this._tabLocalisationPersonnelle.Background = Brushes.Red;
            }

            return test;
        }

        #region Champs Adresse
        private bool Verif_TextBoxSalarieAdressePerso()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieAdressePerso.Text.Trim().Length <= 0)
            {
                verif = false;
                this._TextBlockSalarieAdressePerso.Foreground = Brushes.Red;
                this._TextBoxSalarieAdressePerso.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieAdressePerso.Foreground = Brushes.Green;
                this._TextBoxSalarieAdressePerso.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalarieAdressePerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieAdressePerso();
        }

        #endregion

        #region Champs Adresse complementaire
        private bool Verif_TextBoxSalarieAdresseComplementairePerso()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieAdresseComplementairePerso.Text.Trim().Length <= 0)
            {
                verif = true;
                this._TextBlockSalarieAdresseComplementairePerso.Foreground = Brushes.Green;
                this._TextBoxSalarieAdresseComplementairePerso.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieAdresseComplementairePerso.Foreground = Brushes.Green;
                this._TextBoxSalarieAdresseComplementairePerso.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalarieAdresseComplementairePerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieAdresseComplementairePerso();
        }

        #endregion

        #region Champs Ville
        private bool Verif_ComboBoxSalarieVillePerso()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieVillePerso.SelectedItem == null)
            {
                verif = false;
                this._TextBlockSalarieVillePerso.Foreground = Brushes.Red;
                this._ComboBoxSalarieVillePerso.Background = rouge;
            }
            else
            {
                this._TextBoxSalarieCodePostalPerso.Text = ((Ville)this._ComboBoxSalarieVillePerso.SelectedItem).Code_Postal;
                this._ComboBoxSalariePaysPerso.SelectedItem = ((Ville)this._ComboBoxSalarieVillePerso.SelectedItem).Pays1;
                verif = true;
                this._TextBlockSalarieVillePerso.Foreground = Brushes.Green;
                this._ComboBoxSalarieVillePerso.Background = vert;
            }

            return verif;
        }

        private void _ComboBoxCoordonneesVille_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxSalarieVillePerso();
        }

        #endregion

        #region Champs Code postal
        private bool Verif_TextBoxSalarieCodePostalPerso()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);


            if (this._TextBoxSalarieCodePostalPerso.Text.Trim().Length < 5 || this._TextBoxSalarieCodePostalPerso.Text.Trim().Length > 5)
            {
                verif = false;
                this._TextBlockSalarieCodePostalPerso.Foreground = Brushes.Red;
                this._TextBoxSalarieCodePostalPerso.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieCodePostalPerso.Foreground = Brushes.Green;
                this._TextBoxSalarieCodePostalPerso.Background = vert;
            }

            this.VilleListPerso = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville);
            if (this._TextBoxSalarieCodePostalPerso.Text.Trim().Length != 0)
            {
                this.VilleListPerso = new ObservableCollection<Ville>(this.VilleListPerso.Where(vil => vil.Code_Postal == this._TextBoxSalarieCodePostalPerso.Text.Trim()));
            }
            if (this._ComboBoxSalariePaysPerso.SelectedItem != null)
            {
                this.VilleListPerso = new ObservableCollection<Ville>(this.VilleListPerso.Where(vil => vil.Pays1 == (Pays)this._ComboBoxSalariePaysPerso.SelectedItem));
            }

            return verif;
        }

        private void _TextBoxSalarieCodePostalPerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieCodePostalPerso();
        }

        #endregion

        #region Champs Pays
        private bool Verif_ComboBoxSalariePaysPerso()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalariePaysPerso.SelectedItem == null)
            {
                verif = false;
                this._TextBlockSalariePaysPerso.Foreground = Brushes.Red;
                this._ComboBoxSalariePaysPerso.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockSalariePaysPerso.Foreground = Brushes.Green;
                this._ComboBoxSalariePaysPerso.Background = vert;
            }

            this.VilleListPerso = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville);
            if (this._TextBoxSalarieCodePostalPerso.Text.Trim().Length != 0)
            {
                this.VilleListPerso = new ObservableCollection<Ville>(this.VilleListPerso.Where(vil => vil.Code_Postal == this._TextBoxSalarieCodePostalPerso.Text.Trim()));
            }
            if (this._ComboBoxSalariePaysPerso.SelectedItem != null)
            {
                this.VilleListPerso = new ObservableCollection<Ville>(this.VilleListPerso.Where(vil => vil.Pays1 == (Pays)this._ComboBoxSalariePaysPerso.SelectedItem));
            }

            return verif;
        }

        private void _ComboBoxCoordonneesPays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxSalariePaysPerso();
        }

        #endregion
        #endregion

        #region Professionnelle

        private bool verif_tab_Professionnelle()
        {
            bool test = true;

            if (!Verif_TextBoxSalarieAdressePro())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieAdresseComplementairePro())
            {
                test = false;
            }
            if (!Verif_ComboBoxSalarieVillePro())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieCodePostalPro())
            {
                test = false;
            }
            if (!Verif_ComboBoxSalariePaysPro())
            {
                test = false;
            }

            if (test == true)
            {
                this._tabLocalisationProfessionnelle.Background = Brushes.Green;
            }
            else
            {
                this._tabLocalisationProfessionnelle.Background = Brushes.Red;
            }

            return test;
        }

        #region Champs Adresse
        private bool Verif_TextBoxSalarieAdressePro()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieAdressePro.Text.Trim().Length <= 0)
            {

                if (this._ComboBoxSalarieVillePro.SelectedItem == null)
                {
                    verif = true;
                    this._TextBlockSalarieAdressePro.Foreground = Brushes.Green;
                    this._TextBoxSalarieAdressePro.Background = vert;
                }
                else
                {
                    verif = false;
                    this._TextBlockSalarieAdressePro.Foreground = Brushes.Red;
                    this._TextBoxSalarieAdressePro.Background = rouge;
                }
            }
            else
            {
                verif = true;
                this._TextBlockSalarieAdressePro.Foreground = Brushes.Green;
                this._TextBoxSalarieAdressePro.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalarieAdressePro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieAdressePro();
        }

        #endregion

        #region Champs Adresse complementaire
        private bool Verif_TextBoxSalarieAdresseComplementairePro()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieAdresseComplementairePro.Text.Trim().Length <= 0)
            {
                verif = true;
                this._TextBlockSalarieAdresseComplementairePro.Foreground = Brushes.Green;
                this._TextBoxSalarieAdresseComplementairePro.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieAdresseComplementairePro.Foreground = Brushes.Green;
                this._TextBoxSalarieAdresseComplementairePro.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalarieAdresseComplementairePro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieAdresseComplementairePro();
        }

        #endregion

        #region Champs Ville
        private bool Verif_ComboBoxSalarieVillePro()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieVillePro.SelectedItem == null)
            {
                //verif = false;
                //this._TextBlockSalarieVillePro.Foreground = Brushes.Red;
                //this._ComboBoxSalarieVillePro.Background = rouge;
                verif = true;
                this._TextBlockSalarieVillePro.Foreground = Brushes.Green;
                this._ComboBoxSalarieVillePro.Background = vert;
            }
            else
            {
                this._TextBoxSalarieCodePostalPro.Text = ((Ville)this._ComboBoxSalarieVillePro.SelectedItem).Code_Postal;
                this._ComboBoxSalariePaysPro.SelectedItem = ((Ville)this._ComboBoxSalarieVillePro.SelectedItem).Pays1;
                verif = true;
                this._TextBlockSalarieVillePro.Foreground = Brushes.Green;
                this._ComboBoxSalarieVillePro.Background = vert;
            }

            return verif;
        }


        private void _ComboBoxSalarieVillePro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxSalarieVillePro();
        }

        #endregion

        #region Champs Code postal
        private bool Verif_TextBoxSalarieCodePostalPro()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);


            if (this._TextBoxSalarieCodePostalPro.Text.Trim().Length < 5 || this._TextBoxSalarieCodePostalPro.Text.Trim().Length > 5)
            {
                //verif = false;
                //this._TextBlockSalarieCodePostalPro.Foreground = Brushes.Red;
                //this._TextBoxSalarieCodePostalPro.Background = rouge;
                verif = true;
                this._TextBlockSalarieCodePostalPro.Foreground = Brushes.Green;
                this._TextBoxSalarieCodePostalPro.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieCodePostalPro.Foreground = Brushes.Green;
                this._TextBoxSalarieCodePostalPro.Background = vert;
            }

            this.VilleListPro = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville);
            if (this._TextBoxSalarieCodePostalPro.Text.Trim().Length != 0)
            {
                this.VilleListPro = new ObservableCollection<Ville>(this.VilleListPro.Where(vil => vil.Code_Postal == this._TextBoxSalarieCodePostalPro.Text.Trim()));
            }
            if (this._ComboBoxSalariePaysPro.SelectedItem != null)
            {
                this.VilleListPro = new ObservableCollection<Ville>(this.VilleListPro.Where(vil => vil.Pays1 == (Pays)this._ComboBoxSalariePaysPro.SelectedItem));
            }

            return verif;
        }

        private void _TextBoxSalarieCodePostalPro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieCodePostalPro();
        }

        #endregion

        #region Champs Pays
        private bool Verif_ComboBoxSalariePaysPro()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalariePaysPro.SelectedItem == null)
            {
                //verif = false;
                //this._TextBlockSalariePaysPro.Foreground = Brushes.Red;
                //this._ComboBoxSalariePaysPro.Background = rouge;
                verif = true;
                this._TextBlockSalariePaysPro.Foreground = Brushes.Green;
                this._ComboBoxSalariePaysPro.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockSalariePaysPro.Foreground = Brushes.Green;
                this._ComboBoxSalariePaysPro.Background = vert;
            }

            this.VilleListPro = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville);
            if (this._TextBoxSalarieCodePostalPro.Text.Trim().Length != 0)
            {
                this.VilleListPro = new ObservableCollection<Ville>(this.VilleListPro.Where(vil => vil.Code_Postal == this._TextBoxSalarieCodePostalPro.Text.Trim()));
            }
            if (this._ComboBoxSalariePaysPro.SelectedItem != null)
            {
                this.VilleListPro = new ObservableCollection<Ville>(this.VilleListPro.Where(vil => vil.Pays1 == (Pays)this._ComboBoxSalariePaysPro.SelectedItem));
            }

            return verif;
        }

        private void _ComboBoxSalariePaysPro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxSalariePaysPro();
        }

        #endregion
        #endregion

        #endregion

        #region GroupBox Correspondance

        private bool verif_GroupBoxCorrespondance()
        {
            bool test = true;

            if (!verif_tab_CorrespondancePersonnelle())
            {
                test = false;
            }
            if (!verif_tab_CorrespondanceProfessionnelle())
            {
                test = false;
            }

            if (test == true)
            {
                this._groupBoxCorrespondance.Foreground = Brushes.Green;
            }
            else
            {
                this._groupBoxCorrespondance.Foreground = Brushes.Red;
            }

            return test;
        }

        #region tab Personnelle

        private bool verif_tab_CorrespondancePersonnelle()
        {
            bool test = true;

            if (!Verif_TextBoxSalarieTelephoneportablePerso())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieTelephonefixePerso())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieEmailPerso())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieFaxPerso())
            {
                test = false;
            }

            if (test == true)
            {
                this._tabCorrespondancePersonnelle.Background = Brushes.Green;
            }
            else
            {
                this._tabCorrespondancePersonnelle.Background = Brushes.Red;
            }

            return test;
        }

        #region Champs Telephone portable
        private bool Verif_TextBoxSalarieTelephoneportablePerso()
        {
            bool verif = true;
            double test = 0;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieTelephoneportablePerso.Text.Trim().Length != 0)
            {
                if (this._TextBoxSalarieTelephoneportablePerso.Text.Trim().Length < 10 || this._TextBoxSalarieTelephoneportablePerso.Text.Trim().Length > 10)
                {
                    verif = false;
                    this._TextBlockSalarieTelephoneportablePerso.Foreground = Brushes.Red;
                    this._TextBoxSalarieTelephoneportablePerso.Background = rouge;
                }
                else
                {
                    if (double.TryParse(this._TextBoxSalarieTelephoneportablePerso.Text.Trim(), out test) == false)
                    {
                        verif = false;
                        this._TextBlockSalarieTelephoneportablePerso.Foreground = Brushes.Red;
                        this._TextBoxSalarieTelephoneportablePerso.Background = rouge;
                    }
                    else
                    {
                        verif = true;
                        this._TextBoxSalarieTelephoneportablePerso.Text = this._TextBoxSalarieTelephoneportablePerso.Text.Trim();
                        this._TextBlockSalarieTelephoneportablePerso.Foreground = Brushes.Green;
                        this._TextBoxSalarieTelephoneportablePerso.Background = vert;
                    }
                }
            }
            else
            {
                verif = true;
                this._TextBoxSalarieTelephoneportablePerso.Text = this._TextBoxSalarieTelephoneportablePerso.Text.Trim();
                this._TextBlockSalarieTelephoneportablePerso.Foreground = Brushes.Green;
                this._TextBoxSalarieTelephoneportablePerso.Background = vert;
            }
            return verif;
        }

        private void _TextBoxSalarieTelephoneportablePerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieTelephoneportablePerso();
        }

        #endregion

        #region Champs Telephone Fixe
        private bool Verif_TextBoxSalarieTelephonefixePerso()
        {
            bool verif = true;
            double test = 0;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieTelephonefixePerso.Text.Trim().Length != 0)
            {
                if (this._TextBoxSalarieTelephonefixePerso.Text.Trim().Length < 10 || this._TextBoxSalarieTelephonefixePerso.Text.Trim().Length > 10)
                {
                    verif = false;
                    this._TextBlockSalarieTelephonefixePerso.Foreground = Brushes.Red;
                    this._TextBoxSalarieTelephonefixePerso.Background = rouge;
                }
                else
                {
                    if (double.TryParse(this._TextBoxSalarieTelephonefixePerso.Text.Trim(), out test) == false)
                    {
                        verif = false;
                        this._TextBlockSalarieTelephonefixePerso.Foreground = Brushes.Red;
                        this._TextBoxSalarieTelephonefixePerso.Background = rouge;
                    }
                    else
                    {
                        verif = true;
                        this._TextBoxSalarieTelephonefixePerso.Text = this._TextBoxSalarieTelephonefixePerso.Text.Trim();
                        this._TextBlockSalarieTelephonefixePerso.Foreground = Brushes.Green;
                        this._TextBoxSalarieTelephonefixePerso.Background = vert;
                    }
                }
            }
            else
            {
                verif = true;
                this._TextBoxSalarieTelephonefixePerso.Text = this._TextBoxSalarieTelephonefixePerso.Text.Trim();
                this._TextBlockSalarieTelephonefixePerso.Foreground = Brushes.Green;
                this._TextBoxSalarieTelephonefixePerso.Background = vert;
            }
            return verif;
        }

        private void _TextBoxSalarieTelephonefixePerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieTelephonefixePerso();
        }

        #endregion

        #region Champs Email
        private bool Verif_TextBoxSalarieEmailPerso()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieEmailPerso.Text.Trim().Length <= 0)
            {
                //verif = false;
                //this._TextBlockSalarieEmailPerso.Foreground = Brushes.Red;
                //this._TextBoxSalarieEmailPerso.Background = rouge;
                verif = true;
                this._TextBlockSalarieEmailPerso.Foreground = Brushes.Green;
                this._TextBoxSalarieEmailPerso.Background = vert;
            }
            else
            {
                if (this._TextBoxSalarieEmailPerso.Text.Contains('@'))
                {
                    verif = true;
                    this._TextBlockSalarieEmailPerso.Foreground = Brushes.Green;
                    this._TextBoxSalarieEmailPerso.Background = vert;
                }
                else
                {
                    verif = false;
                    this._TextBlockSalarieEmailPerso.Foreground = Brushes.Red;
                    this._TextBoxSalarieEmailPerso.Background = rouge;
                }
            }

            return verif;
        }

        private void _TextBoxSalarieEmailPerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieEmailPerso();
        }

        #endregion

        #region Champs Fax
        private bool Verif_TextBoxSalarieFaxPerso()
        {
            bool verif = true;
            double test = 0;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieFaxPerso.Text.Trim().Length != 0)
            {
                if (this._TextBoxSalarieFaxPerso.Text.Trim().Length < 10 || this._TextBoxSalarieFaxPerso.Text.Trim().Length > 10)
                {
                    verif = false;
                    this._TextBlockSalarieFaxPerso.Foreground = Brushes.Red;
                    this._TextBoxSalarieFaxPerso.Background = rouge;
                }
                else
                {
                    if (double.TryParse(this._TextBoxSalarieFaxPerso.Text.Trim(), out test) == false)
                    {
                        verif = false;
                        this._TextBlockSalarieFaxPerso.Foreground = Brushes.Red;
                        this._TextBoxSalarieFaxPerso.Background = rouge;
                    }
                    else
                    {
                        verif = true;
                        this._TextBoxSalarieFaxPerso.Text = this._TextBoxSalarieFaxPerso.Text.Trim();
                        this._TextBlockSalarieFaxPerso.Foreground = Brushes.Green;
                        this._TextBoxSalarieFaxPerso.Background = vert;
                    }
                }
            }
            else
            {
                verif = true;
                this._TextBoxSalarieFaxPerso.Text = this._TextBoxSalarieFaxPerso.Text.Trim();
                this._TextBlockSalarieFaxPerso.Foreground = Brushes.Green;
                this._TextBoxSalarieFaxPerso.Background = vert;
            }
            return verif;
        }

        private void _TextBoxSalarieTelephonefaxPerso_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieFaxPerso();
        }

        #endregion

        #endregion

        #region Professionnelle

        private bool verif_tab_CorrespondanceProfessionnelle()
        {
            bool test = true;

            if (!Verif_TextBoxSalarieTelephoneportablePro())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieTelephonefixePro())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieEmailPro())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieFaxPro())
            {
                test = false;
            }

            if (test == true)
            {
                this._tabCorrespondanceProfessionnelle.Background = Brushes.Green;
            }
            else
            {
                this._tabCorrespondanceProfessionnelle.Background = Brushes.Red;
            }

            return test;
        }

        #region Champs Telephone portable
        private bool Verif_TextBoxSalarieTelephoneportablePro()
        {
            bool verif = true;
            double test = 0;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieTelephoneportablePro.Text.Trim().Length != 0)
            {
                if (this._TextBoxSalarieTelephoneportablePro.Text.Trim().Length < 10 || this._TextBoxSalarieTelephoneportablePro.Text.Trim().Length > 10)
                {
                    verif = false;
                    this._TextBlockSalarieTelephoneportablePro.Foreground = Brushes.Red;
                    this._TextBoxSalarieTelephoneportablePro.Background = rouge;
                }
                else
                {
                    if (double.TryParse(this._TextBoxSalarieTelephoneportablePro.Text.Trim(), out test) == false)
                    {
                        verif = false;
                        this._TextBlockSalarieTelephoneportablePro.Foreground = Brushes.Red;
                        this._TextBoxSalarieTelephoneportablePro.Background = rouge;
                    }
                    else
                    {
                        verif = true;
                        this._TextBoxSalarieTelephoneportablePro.Text = this._TextBoxSalarieTelephoneportablePro.Text.Trim();
                        this._TextBlockSalarieTelephoneportablePro.Foreground = Brushes.Green;
                        this._TextBoxSalarieTelephoneportablePro.Background = vert;
                    }
                }
            }
            else
            {
                verif = true;
                this._TextBoxSalarieTelephoneportablePro.Text = this._TextBoxSalarieTelephoneportablePro.Text.Trim();
                this._TextBlockSalarieTelephoneportablePro.Foreground = Brushes.Green;
                this._TextBoxSalarieTelephoneportablePro.Background = vert;
            }
            return verif;
        }

        private void _TextBoxSalarieTelephoneportablePro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieTelephoneportablePro();
        }

        #endregion

        #region Champs Telephone Fixe
        private bool Verif_TextBoxSalarieTelephonefixePro()
        {
            bool verif = true;
            double test = 0;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieTelephonefixePro.Text.Trim().Length != 0)
            {
                if (this._TextBoxSalarieTelephonefixePro.Text.Trim().Length < 10 || this._TextBoxSalarieTelephonefixePro.Text.Trim().Length > 10)
                {
                    verif = false;
                    this._TextBlockSalarieTelephonefixePro.Foreground = Brushes.Red;
                    this._TextBoxSalarieTelephonefixePro.Background = rouge;
                }
                else
                {
                    if (double.TryParse(this._TextBoxSalarieTelephonefixePro.Text.Trim(), out test) == false)
                    {
                        verif = false;
                        this._TextBlockSalarieTelephonefixePro.Foreground = Brushes.Red;
                        this._TextBoxSalarieTelephonefixePro.Background = rouge;
                    }
                    else
                    {
                        verif = true;
                        this._TextBoxSalarieTelephonefixePro.Text = this._TextBoxSalarieTelephonefixePro.Text.Trim();
                        this._TextBlockSalarieTelephonefixePro.Foreground = Brushes.Green;
                        this._TextBoxSalarieTelephonefixePro.Background = vert;
                    }
                }
            }
            else
            {
                verif = true;
                this._TextBoxSalarieTelephonefixePro.Text = this._TextBoxSalarieTelephonefixePro.Text.Trim();
                this._TextBlockSalarieTelephonefixePro.Foreground = Brushes.Green;
                this._TextBoxSalarieTelephonefixePro.Background = vert;
            }
            return verif;
        }

        private void _TextBoxSalarieTelephonefixePro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieTelephonefixePro();
        }

        #endregion

        #region Champs Email
        private bool Verif_TextBoxSalarieEmailPro()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieEmailPro.Text.Trim().Length <= 0)
            {
                //verif = false;
                //this._TextBlockSalarieEmailPro.Foreground = Brushes.Red;
                //this._TextBoxSalarieEmailPro.Background = rouge;
                verif = true;
                this._TextBlockSalarieEmailPro.Foreground = Brushes.Green;
                this._TextBoxSalarieEmailPro.Background = vert;
            }
            else
            {
                if (this._TextBoxSalarieEmailPro.Text.Contains('@'))
                {
                    verif = true;
                    this._TextBlockSalarieEmailPro.Foreground = Brushes.Green;
                    this._TextBoxSalarieEmailPro.Background = vert;
                }
                else
                {
                    verif = false;
                    this._TextBlockSalarieEmailPro.Foreground = Brushes.Red;
                    this._TextBoxSalarieEmailPro.Background = rouge;
                }
            }

            return verif;
        }

        private void _TextBoxSalarieEmailPro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieEmailPro();
        }

        #endregion

        #region Champs Fax
        private bool Verif_TextBoxSalarieFaxPro()
        {
            bool verif = true;
            double test = 0;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieFaxPro.Text.Trim().Length != 0)
            {
                if (this._TextBoxSalarieFaxPro.Text.Trim().Length < 10 || this._TextBoxSalarieFaxPro.Text.Trim().Length > 10)
                {
                    verif = false;
                    this._TextBlockSalarieFaxPro.Foreground = Brushes.Red;
                    this._TextBoxSalarieFaxPro.Background = rouge;
                }
                else
                {
                    if (double.TryParse(this._TextBoxSalarieFaxPro.Text.Trim(), out test) == false)
                    {
                        verif = false;
                        this._TextBlockSalarieFaxPro.Foreground = Brushes.Red;
                        this._TextBoxSalarieFaxPro.Background = rouge;
                    }
                    else
                    {
                        verif = true;
                        this._TextBoxSalarieFaxPro.Text = this._TextBoxSalarieFaxPro.Text.Trim();
                        this._TextBlockSalarieFaxPro.Foreground = Brushes.Green;
                        this._TextBoxSalarieFaxPro.Background = vert;
                    }
                }
            }
            else
            {
                verif = true;
                this._TextBoxSalarieFaxPro.Text = this._TextBoxSalarieFaxPro.Text.Trim();
                this._TextBlockSalarieFaxPro.Foreground = Brushes.Green;
                this._TextBoxSalarieFaxPro.Background = vert;
            }
            return verif;
        }

        private void _TextBoxSalarieTelephonefaxPro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieFaxPro();
        }

        #endregion

        #endregion

        #endregion

        #region GroupBox Info complemantaires

        private bool verif_GroupBoxInfoComplementaires()
        {
            bool test = true;

            if (!Verif_TextBoxSalarieCommentaires())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieCentresInterets())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieDistanceAtelier())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieTempsRouteAtelier())
            {
                test = false;
            }

            if (test == true)
            {
                this._groupBoxCoordonnees.Foreground = Brushes.Green;
            }
            else
            {
                this._groupBoxCoordonnees.Foreground = Brushes.Red;
            }

            return test;
        }

        #region Champs Commentaires
        private bool Verif_TextBoxSalarieCommentaires()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieCommentaires.Text.Trim().Length <= 0)
            {
                verif = true;
                this._TextBlockSalarieCommentaires.Foreground = Brushes.Green;
                this._TextBoxSalarieCommentaires.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieCommentaires.Foreground = Brushes.Green;
                this._TextBoxSalarieCommentaires.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalarieCommentaires_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieCommentaires();
        }

        #endregion

        #region Champs Centres d'interets
        private bool Verif_TextBoxSalarieCentresInterets()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieCentresInterets.Text.Trim().Length <= 0)
            {
                verif = true;
                this._TextBlockSalarieCentresInterets.Foreground = Brushes.Green;
                this._TextBoxSalarieCentresInterets.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieCentresInterets.Foreground = Brushes.Green;
                this._TextBoxSalarieCentresInterets.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalarieCentresInterets_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieCentresInterets();
        }

        #endregion

        #region Champs Distance Atelier
        private bool Verif_TextBoxSalarieDistanceAtelier()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieDistanceAtelier.Text.Trim().Length <= 0)
            {
                verif = true;
                this._TextBlockSalarieDistanceAtelier.Foreground = Brushes.Green;
                this._TextBoxSalarieDistanceAtelier.Background = vert;
            }
            else
            {
                float test;
                if (float.TryParse(this._TextBoxSalarieDistanceAtelier.Text, out test))
                {
                    verif = true;
                    this._TextBlockSalarieDistanceAtelier.Foreground = Brushes.Green;
                    this._TextBoxSalarieDistanceAtelier.Background = vert;
                }
                else
                {
                    verif = false;
                    this._TextBlockSalarieDistanceAtelier.Foreground = Brushes.Red;
                    this._TextBoxSalarieDistanceAtelier.Background = rouge;
                }
            }

            return verif;
        }

        private void _TextBoxSalarieDistanceAtelier_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieDistanceAtelier();
        }

        #endregion

        #region Champs Temps route atelier
        private bool Verif_TextBoxSalarieTempsRouteAtelier()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieTempsRouteAtelier.Text.Trim().Length <= 0)
            {
                verif = true;
                this._TextBlockSalarieTempsRouteAtelier.Foreground = Brushes.Green;
                this._TextBoxSalarieTempsRouteAtelier.Background = vert;
            }
            else
            {
                float test;
                if (float.TryParse(this._TextBoxSalarieTempsRouteAtelier.Text, out test))
                {
                    verif = true;
                    this._TextBlockSalarieTempsRouteAtelier.Foreground = Brushes.Green;
                    this._TextBoxSalarieTempsRouteAtelier.Background = vert;
                }
                else
                {
                    verif = false;
                    this._TextBlockSalarieTempsRouteAtelier.Foreground = Brushes.Red;
                    this._TextBoxSalarieTempsRouteAtelier.Background = rouge;
                }
            }

            return verif;
        }

        private void _TextBoxSalarieTempsRouteAtelier_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieTempsRouteAtelier();
        }

        #endregion

        #endregion

        #region GroupBox Entreprise

        private bool verif_GroupBoxEntreprise()
        {
            bool test = true;

            if (!Verif_ComboBoxContrat())
            {
                test = false;
            }
            if (!Verif_ComboBoxEntreprise())
            {
                test = false;
            }

            if (test == true)
            {
                this._groupBoxEntreprise.Foreground = Brushes.Green;
            }
            else
            {
                this._groupBoxEntreprise.Foreground = Brushes.Red;
            }

            return test;
        }

        #region ComboBox Groupe

        private bool Verif_ComboBoxContrat()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieContrat.SelectedItem == null)
            {
                verif = false;
                this._TextBlockSalarieContrat.Foreground = Brushes.Red;
                this._ComboBoxSalarieContrat.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieContrat.Foreground = Brushes.Green;
                this._ComboBoxSalarieContrat.Background = vert;
            }

            return verif;
        }

        private void _ComboBoxSalarieContrat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxContrat();
        }

        #endregion

        #region Entreprise

        private bool Verif_ComboBoxEntreprise()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieEntreprise.SelectedItem == null)
            {
                verif = false;
                this._TextBlockSalarieEntreprise.Foreground = Brushes.Red;
                this._ComboBoxSalarieEntreprise.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieEntreprise.Foreground = Brushes.Green;
                this._ComboBoxSalarieEntreprise.Background = vert;
            }

            return verif;
        }

        private void _ComboBoxSalarieEntreprise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxEntreprise();
        }

        #endregion

        #endregion

        #region GroupBox Emploi Occupé

        private bool verif_GroupBoxEmploiOccupe()
        {
            bool test = true;

            if (!Verif_DataGridEmploiOccupe())
            {
                test = false;
            }

            if (test == true)
            {
                this._groupBoxEmploiOccupe.Foreground = Brushes.Green;
            }
            else
            {
                this._groupBoxEmploiOccupe.Foreground = Brushes.Red;
            }

            return test;
        }

        #region DataGrid Emploi Occupé

        private bool Verif_DataGridEmploiOccupe()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._dataGridEmploiOccupe.Items.Count <= 0)
            {
                verif = false;
                this._dataGridEmploiOccupe.Background = rouge;
            }
            else
            {
                verif = true;
                this._dataGridEmploiOccupe.Background = vert;
            }

            return verif;
        }

        #endregion

        #endregion

        #endregion

        #region Compétences

        private bool verif_tab_Competences()
        {
            bool test = true;

            if (!verif_GroupBoxFormations())
            {
                test = false;
            }
            if (!verif_GroupBoxHabilitations())
            {
                test = false;
            }
            if (!verif_GroupBoxPermis())
            {
                test = false;
            }
            if (!verif_GroupBoxDiplomes())
            {
                test = false;
            }


            if (test == true)
            {
                this._tabCompetences.Background = Brushes.Green;
            }
            else
            {
                this._tabCompetences.Background = Brushes.Red;
            }

            return test;
        }

        #region GroupBox Formations

        private bool verif_GroupBoxFormations()
        {
            bool test = true;

            if (!Verif_DataGridFormations())
            {
                test = false;
            }

            if (test == true)
            {
                this._groupBoxFormations.Foreground = Brushes.Green;
            }
            else
            {
                this._groupBoxFormations.Foreground = Brushes.Red;
            }

            return test;
        }

        #region DataGrid Formations

        private bool Verif_DataGridFormations()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._dataGridFormations.Items.Count <= 0)
            {
                //verif = false;
                //this._dataGridFormations.Background = rouge;
                verif = true;
                this._dataGridFormations.Background = vert;
            }
            else
            {
                verif = true;
                this._dataGridFormations.Background = vert;
            }

            return verif;
        }

        #endregion

        #endregion

        #region GroupBox Habilitations

        private bool verif_GroupBoxHabilitations()
        {
            bool test = true;

            if (!Verif_DataGridHabilitations())
            {
                test = false;
            }

            if (test == true)
            {
                this._groupBoxHabilitations.Foreground = Brushes.Green;
            }
            else
            {
                this._groupBoxHabilitations.Foreground = Brushes.Red;
            }

            return test;
        }

        #region DataGrid Habilitations

        private bool Verif_DataGridHabilitations()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._dataGridHabilitations.Items.Count <= 0)
            {
                //verif = false;
                //this._dataGridHabilitations.Background = rouge;
                verif = true;
                this._dataGridHabilitations.Background = vert;
            }
            else
            {
                verif = true;
                this._dataGridHabilitations.Background = vert;
            }

            return verif;
        }

        #endregion

        #endregion

        #region GroupBox Permis

        private bool verif_GroupBoxPermis()
        {
            bool test = true;

            if (!Verif_DataGridPermis())
            {
                test = false;
            }

            if (test == true)
            {
                this._groupBoxPermis.Foreground = Brushes.Green;
            }
            else
            {
                this._groupBoxPermis.Foreground = Brushes.Red;
            }

            return test;
        }

        #region DataGrid Permis

        private bool Verif_DataGridPermis()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._dataGridPermis.Items.Count <= 0)
            {
                //verif = false;
                //this._dataGridPermis.Background = rouge;
                verif = true;
                this._dataGridPermis.Background = vert;
            }
            else
            {
                verif = true;
                this._dataGridPermis.Background = vert;
            }

            return verif;
        }

        #endregion

        #endregion

        #region GroupBox Diplomes

        private bool verif_GroupBoxDiplomes()
        {
            bool test = true;

            if (!Verif_DataGridDiplomes())
            {
                test = false;
            }

            if (test == true)
            {
                this._groupBoxDiplomes.Foreground = Brushes.Green;
            }
            else
            {
                this._groupBoxDiplomes.Foreground = Brushes.Red;
            }

            return test;
        }

        #region DataGrid Diplomes

        private bool Verif_DataGridDiplomes()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._dataGridDiplomes.Items.Count <= 0)
            {
                //verif = false;
                //this._dataGridDiplomes.Background = rouge;
                verif = true;
                this._dataGridDiplomes.Background = vert;
            }
            else
            {
                verif = true;
                this._dataGridDiplomes.Background = vert;
            }

            return verif;
        }

        #endregion

        #endregion

        #endregion

        #region Tab Vetements de travail

        private bool verif_tab_VetementsTravail(bool toPutColorOnTab)
        {
            bool test = true;

            if (!Verif_DatePickerSalarieVeste())
            {
                test = false;
            }
            if (!Verif_DatePickerSalariePantalon())
            {
                test = false;
            }
            if (!Verif_DatePickerSalarieBlouse())
            {
                test = false;
            }
            if (!Verif_DatePickerSalarieCombinaison())
            {
                test = false;
            }
            if (!Verif_DatePickerSalarieTeeShirts())
            {
                test = false;
            }
            if (!Verif_DatePickerSalariePolaire())
            {
                test = false;
            }
            if (!Verif_DatePickerSalarieBlouson())
            {
                test = false;
            }
            if (!Verif_DatePickerSalarieChaussure())
            {
                test = false;
            }
            if (!Verif_DatePickerSalarieBotte())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieVesteQuantite())
            {
                test = false;
            }
            if (!Verif_TextBoxSalariePantalonQuantite())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieBlouseQuantite())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieCombinaisonQuantite())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieTeeShirtsQuantite())
            {
                test = false;
            }
            if (!Verif_TextBoxSalariePolaireQuantite())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieBlousonQuantite())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieChaussureQuantite())
            {
                test = false;
            }
            if (!Verif_TextBoxSalarieBotteQuantite())
            {
                test = false;
            }
            if (!Verif_ComboBoxSalarieVesteTaille())
            {
                test = false;
            }
            if (!Verif_ComboBoxSalariePantalonTaille())
            {
                test = false;
            }
            if (!Verif_ComboBoxSalarieBlouseTaille())
            {
                test = false;
            }
            if (!Verif_ComboBoxSalarieCombinaisonTaille())
            {
                test = false;
            }
            if (!Verif_ComboBoxSalarieTeeShirtsTaille())
            {
                test = false;
            }
            if (!Verif_ComboBoxSalariePolaireTaille())
            {
                test = false;
            }
            if (!Verif_ComboBoxSalarieBlousonTaille())
            {
                test = false;
            }
            if (!Verif_ComboBoxSalarieChaussureTaille())
            {
                test = false;
            }
            if (!Verif_ComboBoxSalarieBotteTaille())
            {
                test = false;
            }


            if (toPutColorOnTab == true)
            {
                if (test == true)
                {
                    this._tabVetementsTravail.Background = Brushes.Green;
                }
                else
                {
                    this._tabVetementsTravail.Background = Brushes.Red;
                }
            }

            return test;
        }

        #region Veste

        #region champ veste date attribution
        private bool Verif_DatePickerSalarieVeste()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieVesteTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieVesteQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieVesteQuantite.Text.Trim() == "0") && this._DatePickerSalarieVeste.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieVeste.Foreground = Brushes.Green;
                this._DatePickerSalarieVeste.Background = vert;
            }
            else
            {
                if (this._DatePickerSalarieVeste.SelectedDate == null)
                {
                    verif = false;
                    this._TextBlockSalarieVeste.Foreground = Brushes.Red;
                    this._DatePickerSalarieVeste.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalarieVeste.Foreground = Brushes.Green;
                    this._DatePickerSalarieVeste.Background = vert;
                }
            }

            return verif;
        }

        private void _DatePickerSalarieVeste_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Veste Quantité
        private bool Verif_TextBoxSalarieVesteQuantite()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieVesteTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieVesteQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieVesteQuantite.Text.Trim() == "0") && this._DatePickerSalarieVeste.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieVeste.Foreground = Brushes.Green;
                this._TextBoxSalarieVesteQuantite.Background = vert;
            }
            else
            {
                int test;
                if (this._TextBoxSalarieVesteQuantite.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalarieVeste.Foreground = Brushes.Red;
                    this._TextBoxSalarieVesteQuantite.Background = rouge;
                }
                else
                {
                    if (int.TryParse(this._TextBoxSalarieVesteQuantite.Text.Trim(), out test))
                    {
                        verif = true;
                        this._TextBlockSalarieVeste.Foreground = Brushes.Green;
                        this._TextBoxSalarieVesteQuantite.Background = vert;
                    }
                    else
                    {
                        verif = false;
                        this._TextBlockSalarieVeste.Foreground = Brushes.Red;
                        this._TextBoxSalarieVesteQuantite.Background = rouge;
                    }
                }
            }

            return verif;
        }

        private void _TextBoxSalarieVesteQuantite_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Veste Taille

        private bool Verif_ComboBoxSalarieVesteTaille()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieVesteTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieVesteQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieVesteQuantite.Text.Trim() == "0") && this._DatePickerSalarieVeste.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieVeste.Foreground = Brushes.Green;
                this._ComboBoxSalarieVesteTaille.Background = vert;
            }
            else
            {
                if (this._ComboBoxSalarieVesteTaille.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalarieVeste.Foreground = Brushes.Red;
                    this._ComboBoxSalarieVesteTaille.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalarieVeste.Foreground = Brushes.Green;
                    this._ComboBoxSalarieVesteTaille.Background = vert;
                }
            }

            return verif;
        }

        private void _ComboBoxSalarieVesteTaille_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #endregion

        #region Pantalon

        #region champ Pantalon date attribution
        private bool Verif_DatePickerSalariePantalon()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalariePantalonTaille.Text.Trim().Length == 0 && (this._TextBoxSalariePantalonQuantite.Text.Trim().Length == 0 || this._TextBoxSalariePantalonQuantite.Text.Trim() == "0") && this._DatePickerSalariePantalon.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalariePantalon.Foreground = Brushes.Green;
                this._DatePickerSalariePantalon.Background = vert;
            }
            else
            {
                if (this._DatePickerSalariePantalon.SelectedDate == null)
                {
                    verif = false;
                    this._TextBlockSalariePantalon.Foreground = Brushes.Red;
                    this._DatePickerSalariePantalon.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalariePantalon.Foreground = Brushes.Green;
                    this._DatePickerSalariePantalon.Background = vert;
                }
            }

            return verif;
        }

        private void _DatePickerSalariePantalon_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerSalariePantalon();
        }

        #endregion

        #region Champs Pantalon Quantité
        private bool Verif_TextBoxSalariePantalonQuantite()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalariePantalonTaille.Text.Trim().Length == 0 && (this._TextBoxSalariePantalonQuantite.Text.Trim().Length == 0 || this._TextBoxSalariePantalonQuantite.Text.Trim() == "0") && this._DatePickerSalariePantalon.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalariePantalon.Foreground = Brushes.Green;
                this._TextBoxSalariePantalonQuantite.Background = vert;
            }
            else
            {
                int test;
                if (this._TextBoxSalariePantalonQuantite.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalariePantalon.Foreground = Brushes.Red;
                    this._TextBoxSalariePantalonQuantite.Background = rouge;
                }
                else
                {
                    if (int.TryParse(this._TextBoxSalariePantalonQuantite.Text.Trim(), out test))
                    {
                        verif = true;
                        this._TextBlockSalariePantalon.Foreground = Brushes.Green;
                        this._TextBoxSalariePantalonQuantite.Background = vert;
                    }
                    else
                    {
                        verif = false;
                        this._TextBlockSalariePantalon.Foreground = Brushes.Red;
                        this._TextBoxSalariePantalonQuantite.Background = rouge;
                    }
                }
            }

            return verif;
        }

        private void _TextBoxSalariePantalonQuantite_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Pantalon Taille

        private bool Verif_ComboBoxSalariePantalonTaille()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalariePantalonTaille.Text.Trim().Length == 0 && (this._TextBoxSalariePantalonQuantite.Text.Trim().Length == 0 || this._TextBoxSalariePantalonQuantite.Text.Trim() == "0") && this._DatePickerSalariePantalon.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalariePantalon.Foreground = Brushes.Green;
                this._ComboBoxSalariePantalonTaille.Background = vert;
            }
            else
            {
                if (this._ComboBoxSalariePantalonTaille.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalariePantalon.Foreground = Brushes.Red;
                    this._ComboBoxSalariePantalonTaille.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalariePantalon.Foreground = Brushes.Green;
                    this._ComboBoxSalariePantalonTaille.Background = vert;
                }
            }

            return verif;
        }

        private void _ComboBoxSalariePantalonTaille_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #endregion

        #region Blouse

        #region champ Blouse date attribution
        private bool Verif_DatePickerSalarieBlouse()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieBlouseTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieBlouseQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieBlouseQuantite.Text.Trim() == "0") && this._DatePickerSalarieBlouse.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieBlouse.Foreground = Brushes.Green;
                this._DatePickerSalarieBlouse.Background = vert;
            }
            else
            {
                if (this._DatePickerSalarieBlouse.SelectedDate == null)
                {
                    verif = false;
                    this._TextBlockSalarieBlouse.Foreground = Brushes.Red;
                    this._DatePickerSalarieBlouse.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalarieBlouse.Foreground = Brushes.Green;
                    this._DatePickerSalarieBlouse.Background = vert;
                }
            }

            return verif;
        }

        private void _DatePickerSalarieBlouse_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Blouse Quantité
        private bool Verif_TextBoxSalarieBlouseQuantite()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieBlouseTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieBlouseQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieBlouseQuantite.Text.Trim() == "0") && this._DatePickerSalarieBlouse.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieBlouse.Foreground = Brushes.Green;
                this._TextBoxSalarieBlouseQuantite.Background = vert;
            }
            else
            {
                int test;
                if (this._TextBoxSalarieBlouseQuantite.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalarieBlouse.Foreground = Brushes.Red;
                    this._TextBoxSalarieBlouseQuantite.Background = rouge;
                }
                else
                {
                    if (int.TryParse(this._TextBoxSalarieBlouseQuantite.Text.Trim(), out test))
                    {
                        verif = true;
                        this._TextBlockSalarieBlouse.Foreground = Brushes.Green;
                        this._TextBoxSalarieBlouseQuantite.Background = vert;
                    }
                    else
                    {
                        verif = false;
                        this._TextBlockSalarieBlouse.Foreground = Brushes.Red;
                        this._TextBoxSalarieBlouseQuantite.Background = rouge;
                    }
                }
            }

            return verif;
        }

        private void _TextBoxSalarieBlouseQuantite_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Blouse Taille

        private bool Verif_ComboBoxSalarieBlouseTaille()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieBlouseTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieBlouseQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieBlouseQuantite.Text.Trim() == "0") && this._DatePickerSalarieBlouse.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieBlouse.Foreground = Brushes.Green;
                this._ComboBoxSalarieBlouseTaille.Background = vert;
            }
            else
            {
                if (this._ComboBoxSalarieBlouseTaille.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalarieBlouse.Foreground = Brushes.Red;
                    this._ComboBoxSalarieBlouseTaille.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalarieBlouse.Foreground = Brushes.Green;
                    this._ComboBoxSalarieBlouseTaille.Background = vert;
                }
            }

            return verif;
        }

        private void _ComboBoxSalarieBlouseTaille_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #endregion

        #region Combinaison

        #region champ Combinaison date attribution
        private bool Verif_DatePickerSalarieCombinaison()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieCombinaisonTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieCombinaisonQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieCombinaisonQuantite.Text.Trim() == "0") && this._DatePickerSalarieCombinaison.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieCombinaison.Foreground = Brushes.Green;
                this._DatePickerSalarieCombinaison.Background = vert;
            }
            else
            {
                if (this._DatePickerSalarieCombinaison.SelectedDate == null)
                {
                    verif = false;
                    this._TextBlockSalarieCombinaison.Foreground = Brushes.Red;
                    this._DatePickerSalarieCombinaison.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalarieCombinaison.Foreground = Brushes.Green;
                    this._DatePickerSalarieCombinaison.Background = vert;
                }
            }

            return verif;
        }

        private void _DatePickerSalarieCombinaison_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Combinaison Quantité
        private bool Verif_TextBoxSalarieCombinaisonQuantite()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieCombinaisonTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieCombinaisonQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieCombinaisonQuantite.Text.Trim() == "0") && this._DatePickerSalarieCombinaison.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieCombinaison.Foreground = Brushes.Green;
                this._TextBoxSalarieCombinaisonQuantite.Background = vert;
            }
            else
            {
                int test;
                if (this._TextBoxSalarieCombinaisonQuantite.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalarieCombinaison.Foreground = Brushes.Red;
                    this._TextBoxSalarieCombinaisonQuantite.Background = rouge;
                }
                else
                {
                    if (int.TryParse(this._TextBoxSalarieCombinaisonQuantite.Text.Trim(), out test))
                    {
                        verif = true;
                        this._TextBlockSalarieCombinaison.Foreground = Brushes.Green;
                        this._TextBoxSalarieCombinaisonQuantite.Background = vert;
                    }
                    else
                    {
                        verif = false;
                        this._TextBlockSalarieCombinaison.Foreground = Brushes.Red;
                        this._TextBoxSalarieCombinaisonQuantite.Background = rouge;
                    }
                }
            }

            return verif;
        }

        private void _TextBoxSalarieCombinaisonQuantite_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Combinaison Taille

        private bool Verif_ComboBoxSalarieCombinaisonTaille()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieCombinaisonTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieCombinaisonQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieCombinaisonQuantite.Text.Trim() == "0") && this._DatePickerSalarieCombinaison.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieCombinaison.Foreground = Brushes.Green;
                this._ComboBoxSalarieCombinaisonTaille.Background = vert;
            }
            else
            {
                if (this._ComboBoxSalarieCombinaisonTaille.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalarieCombinaison.Foreground = Brushes.Red;
                    this._ComboBoxSalarieCombinaisonTaille.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalarieCombinaison.Foreground = Brushes.Green;
                    this._ComboBoxSalarieCombinaisonTaille.Background = vert;
                }
            }

            return verif;
        }

        private void _ComboBoxSalarieCombinaisonTaille_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #endregion

        #region TeeShirts

        #region champ TeeShirts date attribution
        private bool Verif_DatePickerSalarieTeeShirts()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieTeeShirtsTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieTeeShirtsQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieTeeShirtsQuantite.Text.Trim() == "0") && this._DatePickerSalarieTeeShirts.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieTeeShirts.Foreground = Brushes.Green;
                this._DatePickerSalarieTeeShirts.Background = vert;
            }
            else
            {
                if (this._DatePickerSalarieTeeShirts.SelectedDate == null)
                {
                    verif = false;
                    this._TextBlockSalarieTeeShirts.Foreground = Brushes.Red;
                    this._DatePickerSalarieTeeShirts.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalarieTeeShirts.Foreground = Brushes.Green;
                    this._DatePickerSalarieTeeShirts.Background = vert;
                }
            }

            return verif;
        }

        private void _DatePickerSalarieTeeShirts_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs TeeShirts Quantité
        private bool Verif_TextBoxSalarieTeeShirtsQuantite()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieTeeShirtsTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieTeeShirtsQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieTeeShirtsQuantite.Text.Trim() == "0") && this._DatePickerSalarieTeeShirts.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieTeeShirts.Foreground = Brushes.Green;
                this._TextBoxSalarieTeeShirtsQuantite.Background = vert;
            }
            else
            {
                int test;
                if (this._TextBoxSalarieTeeShirtsQuantite.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalarieTeeShirts.Foreground = Brushes.Red;
                    this._TextBoxSalarieTeeShirtsQuantite.Background = rouge;
                }
                else
                {
                    if (int.TryParse(this._TextBoxSalarieTeeShirtsQuantite.Text.Trim(), out test))
                    {
                        verif = true;
                        this._TextBlockSalarieTeeShirts.Foreground = Brushes.Green;
                        this._TextBoxSalarieTeeShirtsQuantite.Background = vert;
                    }
                    else
                    {
                        verif = false;
                        this._TextBlockSalarieTeeShirts.Foreground = Brushes.Red;
                        this._TextBoxSalarieTeeShirtsQuantite.Background = rouge;
                    }
                }
            }

            return verif;
        }

        private void _TextBoxSalarieTeeShirtsQuantite_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs TeeShirts Taille

        private bool Verif_ComboBoxSalarieTeeShirtsTaille()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieTeeShirtsTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieTeeShirtsQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieTeeShirtsQuantite.Text.Trim() == "0") && this._DatePickerSalarieTeeShirts.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieTeeShirts.Foreground = Brushes.Green;
                this._ComboBoxSalarieTeeShirtsTaille.Background = vert;
            }
            else
            {
                if (this._ComboBoxSalarieTeeShirtsTaille.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalarieTeeShirts.Foreground = Brushes.Red;
                    this._ComboBoxSalarieTeeShirtsTaille.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalarieTeeShirts.Foreground = Brushes.Green;
                    this._ComboBoxSalarieTeeShirtsTaille.Background = vert;
                }
            }

            return verif;
        }

        private void _ComboBoxSalarieTeeShirtsTaille_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #endregion

        #region Polaire

        #region champ Polaire date attribution
        private bool Verif_DatePickerSalariePolaire()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalariePolaireTaille.Text.Trim().Length == 0 && (this._TextBoxSalariePolaireQuantite.Text.Trim().Length == 0 || this._TextBoxSalariePolaireQuantite.Text.Trim() == "0") && this._DatePickerSalariePolaire.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalariePolaire.Foreground = Brushes.Green;
                this._DatePickerSalariePolaire.Background = vert;
            }
            else
            {
                if (this._DatePickerSalariePolaire.SelectedDate == null)
                {
                    verif = false;
                    this._TextBlockSalariePolaire.Foreground = Brushes.Red;
                    this._DatePickerSalariePolaire.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalariePolaire.Foreground = Brushes.Green;
                    this._DatePickerSalariePolaire.Background = vert;
                }
            }

            return verif;
        }

        private void _DatePickerSalariePolaire_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Polaire Quantité
        private bool Verif_TextBoxSalariePolaireQuantite()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalariePolaireTaille.Text.Trim().Length == 0 && (this._TextBoxSalariePolaireQuantite.Text.Trim().Length == 0 || this._TextBoxSalariePolaireQuantite.Text.Trim() == "0") && this._DatePickerSalariePolaire.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalariePolaire.Foreground = Brushes.Green;
                this._TextBoxSalariePolaireQuantite.Background = vert;
            }
            else
            {
                int test;
                if (this._TextBoxSalariePolaireQuantite.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalariePolaire.Foreground = Brushes.Red;
                    this._TextBoxSalariePolaireQuantite.Background = rouge;
                }
                else
                {
                    if (int.TryParse(this._TextBoxSalariePolaireQuantite.Text.Trim(), out test))
                    {
                        verif = true;
                        this._TextBlockSalariePolaire.Foreground = Brushes.Green;
                        this._TextBoxSalariePolaireQuantite.Background = vert;
                    }
                    else
                    {
                        verif = false;
                        this._TextBlockSalariePolaire.Foreground = Brushes.Red;
                        this._TextBoxSalariePolaireQuantite.Background = rouge;
                    }
                }
            }

            return verif;
        }

        private void _TextBoxSalariePolaireQuantite_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Polaire Taille

        private bool Verif_ComboBoxSalariePolaireTaille()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalariePolaireTaille.Text.Trim().Length == 0 && (this._TextBoxSalariePolaireQuantite.Text.Trim().Length == 0 || this._TextBoxSalariePolaireQuantite.Text.Trim() == "0") && this._DatePickerSalariePolaire.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalariePolaire.Foreground = Brushes.Green;
                this._ComboBoxSalariePolaireTaille.Background = vert;
            }
            else
            {
                if (this._ComboBoxSalariePolaireTaille.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalariePolaire.Foreground = Brushes.Red;
                    this._ComboBoxSalariePolaireTaille.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalariePolaire.Foreground = Brushes.Green;
                    this._ComboBoxSalariePolaireTaille.Background = vert;
                }
            }

            return verif;
        }

        private void _ComboBoxSalariePolaireTaille_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #endregion

        #region Blouson

        #region champ Blouson date attribution
        private bool Verif_DatePickerSalarieBlouson()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieBlousonTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieBlousonQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieBlousonQuantite.Text.Trim() == "0") && this._DatePickerSalarieBlouson.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieBlouson.Foreground = Brushes.Green;
                this._DatePickerSalarieBlouson.Background = vert;
            }
            else
            {
                if (this._DatePickerSalarieBlouson.SelectedDate == null)
                {
                    verif = false;
                    this._TextBlockSalarieBlouson.Foreground = Brushes.Red;
                    this._DatePickerSalarieBlouson.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalarieBlouson.Foreground = Brushes.Green;
                    this._DatePickerSalarieBlouson.Background = vert;
                }
            }

            return verif;
        }

        private void _DatePickerSalarieBlouson_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Blouson Quantité
        private bool Verif_TextBoxSalarieBlousonQuantite()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieBlousonTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieBlousonQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieBlousonQuantite.Text.Trim() == "0") && this._DatePickerSalarieBlouson.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieBlouson.Foreground = Brushes.Green;
                this._TextBoxSalarieBlousonQuantite.Background = vert;
            }
            else
            {
                int test;
                if (this._TextBoxSalarieBlousonQuantite.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalarieBlouson.Foreground = Brushes.Red;
                    this._TextBoxSalarieBlousonQuantite.Background = rouge;
                }
                else
                {
                    if (int.TryParse(this._TextBoxSalarieBlousonQuantite.Text.Trim(), out test))
                    {
                        verif = true;
                        this._TextBlockSalarieBlouson.Foreground = Brushes.Green;
                        this._TextBoxSalarieBlousonQuantite.Background = vert;
                    }
                    else
                    {
                        verif = false;
                        this._TextBlockSalarieBlouson.Foreground = Brushes.Red;
                        this._TextBoxSalarieBlousonQuantite.Background = rouge;
                    }
                }
            }

            return verif;
        }

        private void _TextBoxSalarieBlousonQuantite_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Blouson Taille

        private bool Verif_ComboBoxSalarieBlousonTaille()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieBlousonTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieBlousonQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieBlousonQuantite.Text.Trim() == "0") && this._DatePickerSalarieBlouson.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieBlouson.Foreground = Brushes.Green;
                this._ComboBoxSalarieBlousonTaille.Background = vert;
            }
            else
            {
                if (this._ComboBoxSalarieBlousonTaille.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalarieBlouson.Foreground = Brushes.Red;
                    this._ComboBoxSalarieBlousonTaille.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalarieBlouson.Foreground = Brushes.Green;
                    this._ComboBoxSalarieBlousonTaille.Background = vert;
                }
            }

            return verif;
        }

        private void _ComboBoxSalarieBlousonTaille_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #endregion

        #region Chaussure

        #region champ Chaussure date attribution
        private bool Verif_DatePickerSalarieChaussure()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieChaussureTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieChaussureQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieChaussureQuantite.Text.Trim() == "0") && this._DatePickerSalarieChaussure.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieChaussure.Foreground = Brushes.Green;
                this._DatePickerSalarieChaussure.Background = vert;
            }
            else
            {
                if (this._DatePickerSalarieChaussure.SelectedDate == null)
                {
                    verif = false;
                    this._TextBlockSalarieChaussure.Foreground = Brushes.Red;
                    this._DatePickerSalarieChaussure.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalarieChaussure.Foreground = Brushes.Green;
                    this._DatePickerSalarieChaussure.Background = vert;
                }
            }

            return verif;
        }

        private void _DatePickerSalarieChaussure_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Chaussure Quantité
        private bool Verif_TextBoxSalarieChaussureQuantite()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieChaussureTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieChaussureQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieChaussureQuantite.Text.Trim() == "0") && this._DatePickerSalarieChaussure.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieChaussure.Foreground = Brushes.Green;
                this._TextBoxSalarieChaussureQuantite.Background = vert;
            }
            else
            {
                int test;
                if (this._TextBoxSalarieChaussureQuantite.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalarieChaussure.Foreground = Brushes.Red;
                    this._TextBoxSalarieChaussureQuantite.Background = rouge;
                }
                else
                {
                    if (int.TryParse(this._TextBoxSalarieChaussureQuantite.Text.Trim(), out test))
                    {
                        verif = true;
                        this._TextBlockSalarieChaussure.Foreground = Brushes.Green;
                        this._TextBoxSalarieChaussureQuantite.Background = vert;
                    }
                    else
                    {
                        verif = false;
                        this._TextBlockSalarieChaussure.Foreground = Brushes.Red;
                        this._TextBoxSalarieChaussureQuantite.Background = rouge;
                    }
                }
            }

            return verif;
        }

        private void _TextBoxSalarieChaussureQuantite_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Chaussure Taille

        private bool Verif_ComboBoxSalarieChaussureTaille()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieChaussureTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieChaussureQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieChaussureQuantite.Text.Trim() == "0") && this._DatePickerSalarieChaussure.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieChaussure.Foreground = Brushes.Green;
                this._ComboBoxSalarieChaussureTaille.Background = vert;
            }
            else
            {
                if (this._ComboBoxSalarieChaussureTaille.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalarieChaussure.Foreground = Brushes.Red;
                    this._ComboBoxSalarieChaussureTaille.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalarieChaussure.Foreground = Brushes.Green;
                    this._ComboBoxSalarieChaussureTaille.Background = vert;
                }
            }

            return verif;
        }

        private void _ComboBoxSalarieChaussureTaille_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #endregion

        #region Botte

        #region champ Botte date attribution
        private bool Verif_DatePickerSalarieBotte()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieBotteTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieBotteQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieBotteQuantite.Text.Trim() == "0") && this._DatePickerSalarieBotte.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieBotte.Foreground = Brushes.Green;
                this._DatePickerSalarieBotte.Background = vert;
            }
            else
            {
                if (this._DatePickerSalarieBotte.SelectedDate == null)
                {
                    verif = false;
                    this._TextBlockSalarieBotte.Foreground = Brushes.Red;
                    this._DatePickerSalarieBotte.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalarieBotte.Foreground = Brushes.Green;
                    this._DatePickerSalarieBotte.Background = vert;
                }
            }

            return verif;
        }

        private void _DatePickerSalarieBotte_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Botte Quantité
        private bool Verif_TextBoxSalarieBotteQuantite()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieBotteTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieBotteQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieBotteQuantite.Text.Trim() == "0") && this._DatePickerSalarieBotte.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieBotte.Foreground = Brushes.Green;
                this._TextBoxSalarieBotteQuantite.Background = vert;
            }
            else
            {
                int test;
                if (this._TextBoxSalarieBotteQuantite.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalarieBotte.Foreground = Brushes.Red;
                    this._TextBoxSalarieBotteQuantite.Background = rouge;
                }
                else
                {
                    if (int.TryParse(this._TextBoxSalarieBotteQuantite.Text.Trim(), out test))
                    {
                        verif = true;
                        this._TextBlockSalarieBotte.Foreground = Brushes.Green;
                        this._TextBoxSalarieBotteQuantite.Background = vert;
                    }
                    else
                    {
                        verif = false;
                        this._TextBlockSalarieBotte.Foreground = Brushes.Red;
                        this._TextBoxSalarieBotteQuantite.Background = rouge;
                    }
                }
            }

            return verif;
        }

        private void _TextBoxSalarieBotteQuantite_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #region Champs Botte Taille

        private bool Verif_ComboBoxSalarieBotteTaille()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieBotteTaille.Text.Trim().Length == 0 && (this._TextBoxSalarieBotteQuantite.Text.Trim().Length == 0 || this._TextBoxSalarieBotteQuantite.Text.Trim() == "0") && this._DatePickerSalarieBotte.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieBotte.Foreground = Brushes.Green;
                this._ComboBoxSalarieBotteTaille.Background = vert;
            }
            else
            {
                if (this._ComboBoxSalarieBotteTaille.Text.Trim().Length <= 0)
                {
                    verif = false;
                    this._TextBlockSalarieBotte.Foreground = Brushes.Red;
                    this._ComboBoxSalarieBotteTaille.Background = rouge;
                }
                else
                {
                    verif = true;
                    this._TextBlockSalarieBotte.Foreground = Brushes.Green;
                    this._ComboBoxSalarieBotteTaille.Background = vert;
                }
            }

            return verif;
        }

        private void _ComboBoxSalarieBotteTaille_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.verif_tab_VetementsTravail(false);
        }

        #endregion

        #endregion

        #endregion

        #region Tab Info professionnelles

        private bool verif_tab_info_professionnelles()
        {
            bool test = true;

            if (!verif_tab_SalarieInterne())
            {
                test = false;
            }
            if (!Verif_textBoxMobiliteInterimaireCommentaire())
            {
                test = false;
            }
            if (!verif_tab_SalarieInterimaire())
            {
                test = false;
            }
            if (!verif_tab_SalarieTiers())
            {
                test = false;
            }
            if (!Verif_TextBoxSeuilCommande())
            {
                test = false;
            }


            if (test == true)
            {
                this._tabInformationsProfessionnelles.Background = Brushes.Green;
            }
            else
            {
                this._tabInformationsProfessionnelles.Background = Brushes.Red;
            }

            return test;
        }

        #region Seuil commande

        private bool Verif_TextBoxSeuilCommande()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if ((this._textBoxSeuilCommande.Text.Trim().Length > 0))
            {
                double test;

                if (double.TryParse(this._textBoxSeuilCommande.Text, out test))
                {
                    verif = true;
                    this._textBlockSeuilCommande.Foreground = Brushes.Green;
                    this._textBoxSeuilCommande.Background = vert;
                }
                else
                {
                    verif = false;
                    this._textBlockSeuilCommande.Foreground = Brushes.Red;
                    this._textBoxSeuilCommande.Background = rouge;
                }
            }
            else
            {
                verif = false;
                this._textBlockSeuilCommande.Foreground = Brushes.Red;
                this._textBoxSeuilCommande.Background = rouge;
            }

            return verif;
        }

        private void _TextBoxSeuilCommande_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSeuilCommande();
        }

        private void _textBoxSeuilCommande_KeyUp_1(object sender, KeyEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
        }

        #endregion

        #region Tab Salarie interne

        private bool verif_tab_SalarieInterne()
        {
            bool test = true;

            if (this.checkBox_SalarieInterne.IsChecked == true)
            {
                if (!Verif_DatePickerSalarieInterneDateEntree())
                {
                    test = false;
                }
                if (!Verif_ComboBoxSalarieInterneEntrepriseMere())
                {
                    test = false;
                }
                if (!Verif_DatePickerSalarieInterneDateSortie())
                {
                    test = false;
                }
                if (!Verif_TextBoxSalarieInterneMatriculeSociete())
                {
                    test = false;
                }
                if (!Verif_TextBoxSalarieNumeroInterne())
                {
                    test = false;
                }
            }



            if (test == true)
            {
                this._tabSalarieInterne.Background = Brushes.Green;
            }
            else
            {
                this._tabSalarieInterne.Background = Brushes.Red;
            }

            return test;
        }

        #region Champs Date entree
        private bool Verif_DatePickerSalarieInterneDateEntree()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._DatePickerSalarieInterneDateEntree.SelectedDate == null)
            {
                verif = false;
                this._TextBlockSalarieInterneDateEntree.Foreground = Brushes.Red;
                this._DatePickerSalarieInterneDateEntree.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieInterneDateEntree.Foreground = Brushes.Green;
                this._DatePickerSalarieInterneDateEntree.Background = vert;
            }

            return verif;
        }

        private void _DatePickerSalarieInterneDateEntree_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerSalarieInterneDateEntree();
        }
        #endregion

        #region ComboBox Entreprise Mere
        private bool Verif_ComboBoxSalarieInterneEntrepriseMere()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxSalarieInterneEntrepriseMere.SelectedItem == null)
            {
                verif = false;
                this._TextBlockSalarieInterneEntrepriseMere.Foreground = Brushes.Red;
                this._ComboBoxSalarieInterneEntrepriseMere.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieInterneEntrepriseMere.Foreground = Brushes.Green;
                this._ComboBoxSalarieInterneEntrepriseMere.Background = vert;
            }

            return verif;
        }

        private void _ComboBoxSalarieInterneEntrepriseMere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxSalarieInterneEntrepriseMere();
        }
        #endregion

        #region Champs Date sortie
        private bool Verif_DatePickerSalarieInterneDateSortie()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._DatePickerSalarieInterneDateSortie.SelectedDate == null)
            {
                verif = true;
                this._TextBlockSalarieInterneDateSortie.Foreground = Brushes.Green;
                this._DatePickerSalarieInterneDateSortie.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockSalarieInterneDateSortie.Foreground = Brushes.Green;
                this._DatePickerSalarieInterneDateSortie.Background = vert;
            }

            return verif;
        }

        private void _DatePickerSalarieInterneDateSortie_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerSalarieInterneDateSortie();
        }
        #endregion

        #region Champs Matricule societe
        private bool Verif_TextBoxSalarieInterneMatriculeSociete()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieInterneMatriculeSociete.Text.Trim().Length > 0)
            {
                int test;

                if (int.TryParse(this._TextBoxSalarieInterneMatriculeSociete.Text, out test))
                {
                    if (Verif_UniciteNumeroMatricule())
                    {
                        verif = true;
                        this._TextBlockSalarieInterneMatriculeSociete.Foreground = Brushes.Green;
                        this._TextBoxSalarieInterneMatriculeSociete.Background = vert;
                    }
                    else
                    {
                        verif = false;
                        this._TextBlockSalarieInterneMatriculeSociete.Foreground = Brushes.Red;
                        this._TextBoxSalarieInterneMatriculeSociete.Background = rouge;
                        MessageBox.Show("Numéro de matricule déjà existant", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    verif = false;
                    this._TextBlockSalarieInterneMatriculeSociete.Foreground = Brushes.Red;
                    this._TextBoxSalarieInterneMatriculeSociete.Background = rouge;
                }
            }
            else
            {
                verif = true;
                this._TextBlockSalarieInterneMatriculeSociete.Foreground = Brushes.Green;
                this._TextBoxSalarieInterneMatriculeSociete.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalarieInterneMatriculeSociete_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_TextBoxSalarieInterneMatriculeSociete();
        }

        private bool Verif_UniciteNumeroMatricule()
        {
            bool test = true;

            foreach (Salarie_Interne si in ((App)App.Current).mySitaffEntities.Salarie_Interne)
            {
                if (si.Identifiant != ((Personne)this.DataContext).Salarie.Salarie_Interne.Identifiant)
                {
                    if (int.Parse(this._TextBoxSalarieInterneMatriculeSociete.Text) == si.Matricule_Interne)
                    {
                        test = false;
                    }
                }
            }

            return test;
        }

        #endregion

        #region Numero Interne Salarie

        private bool Verif_TextBoxSalarieNumeroInterne()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._TextBoxSalarieNumeroInterne.Text.Trim().Length > 0)
            {
                int test;

                if (int.TryParse(this._TextBoxSalarieNumeroInterne.Text, out test))
                {
                    if (Verif_UniciteNumeroMatriculeInterneSalarie())
                    {
                        verif = true;
                        this._TextBlockSalarieNumeroInterne.Foreground = Brushes.Green;
                        this._TextBoxSalarieNumeroInterne.Background = vert;
                    }
                    else
                    {
                        verif = false;
                        this._TextBlockSalarieNumeroInterne.Foreground = Brushes.Red;
                        this._TextBoxSalarieNumeroInterne.Background = rouge;
                        MessageBox.Show("Numéro de matricule déjà existant", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    verif = false;
                    this._TextBlockSalarieNumeroInterne.Foreground = Brushes.Red;
                    this._TextBoxSalarieNumeroInterne.Background = rouge;
                }
            }
            else
            {
                verif = true;
                this._TextBlockSalarieNumeroInterne.Foreground = Brushes.Green;
                this._TextBoxSalarieNumeroInterne.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalarieNumeroInterne_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Verif_TextBoxSalarieNumeroInterne();
        }

        private bool Verif_UniciteNumeroMatriculeInterneSalarie()
        {
            bool test = true;

            foreach (Salarie si in ((App)App.Current).mySitaffEntities.Salarie)
            {
                if (si.Identifiant != ((Personne)this.DataContext).Salarie.Identifiant)
                {
                    if (int.Parse(this._TextBoxSalarieNumeroInterne.Text) == si.Matricule_Devis)
                    {
                        test = false;
                    }
                }
            }

            return test;
        }

        #endregion

        #endregion

        #region Salarie interimaire

        private bool verif_tab_SalarieInterimaire()
        {
            bool test = true;

            if (this.checkBox_SalarieInterimaire.IsChecked == true)
            {
                if (!Verif_dataGridCompetencesInterimaire())
                {
                    test = false;
                }
                if (!Verif_ComboBoxAgence())
                {
                    test = false;
                }
                if (!Verif_textBoxMobiliteInterimaireCommentaire())
                {
                    test = false;
                }
                if (!Verif_dataGridMobiliteInterimaire())
                {
                    test = false;
                }
                if (!Verif_TextBoxExperienceCommentairesInterimaire())
                {
                    test = false;
                }
                if (!Verif_ComboBoxNotationInterimaire())
                {
                    test = false;
                }
                if (!Verif_TextBoxNotationCommentairesInterimaire())
                {
                    test = false;
                }
                if (!Verif_TextBoxCoefficientDelegationInterimaire())
                {
                    test = false;
                }
                if (!Verif_TextBoxCoefficientGestionInterimaire())
                {
                    test = false;
                }
            }



            if (test == true)
            {
                this._tabSalarieInterimaire.Background = Brushes.Green;
            }
            else
            {
                this._tabSalarieInterimaire.Background = Brushes.Red;
            }

            return test;
        }

        #region DataGrid Compétences

        private bool Verif_dataGridCompetencesInterimaire()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._dataGridCompetencesInterimaire.Items.Count > 0)
            {
                verif = true;
                this._dataGridCompetencesInterimaire.Background = vert;
            }
            else
            {
                verif = true;
                this._dataGridCompetencesInterimaire.Background = vert;
            }

            return verif;
        }

        #endregion

        #region DataGrid Mobilité
        private bool Verif_dataGridMobiliteInterimaire()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._dataGridMobiliteInterimaire.Items.Count > 0)
            {
                verif = true;
                this._dataGridMobiliteInterimaire.Background = vert;
            }
            else
            {
                verif = true;
                this._dataGridMobiliteInterimaire.Background = vert;
            }

            return verif;
        }
        #endregion

        #region champ Mobilité Commentaire

        private bool Verif_textBoxMobiliteInterimaireCommentaire()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if ((this._TextBoxExperienceCommentairesInterimaire.Text.Trim().Length > 0))
            {
                verif = true;
                this._textBlockInterimaireMobiliteCommentaire.Foreground = Brushes.Green;
                this._TextBoxInterimaireMobiliteCommentaire.Background = vert;
            }
            else
            {
                verif = true;
                this._textBlockInterimaireMobiliteCommentaire.Foreground = Brushes.Green;
                this._TextBoxInterimaireMobiliteCommentaire.Background = vert;
            }

            return verif;
        }

        private void _TextBoxInterimaireMobiliteCommentaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_textBoxMobiliteInterimaireCommentaire();
        }

        #endregion

        #region Champs Commentaire experience
        private bool Verif_TextBoxExperienceCommentairesInterimaire()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if ((this._TextBoxExperienceCommentairesInterimaire.Text.Trim().Length > 0))
            {
                verif = true;
                this._TextBlockExperienceCommentairesInterimaire.Foreground = Brushes.Green;
                this._TextBoxExperienceCommentairesInterimaire.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockExperienceCommentairesInterimaire.Foreground = Brushes.Green;
                this._TextBoxExperienceCommentairesInterimaire.Background = vert;
            }

            return verif;
        }

        private void Verif_TextBoxExperienceCommentairesInterimaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxExperienceCommentairesInterimaire();
        }
        #endregion

        #region Champs Notation
        private bool Verif_ComboBoxNotationInterimaire()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxNotationInterimaire.SelectedItem == null)
            {
                verif = true;
                this._TextBlockNotationInterimaire.Foreground = Brushes.Green;
                this._ComboBoxNotationInterimaire.BorderBrush = vert;
            }
            else
            {
                verif = true;
                this._TextBlockNotationInterimaire.Foreground = Brushes.Green;
                this._ComboBoxNotationInterimaire.BorderBrush = vert;
            }

            return verif;
        }

        private void _ComboBoxNotationInterimaire_SelectionChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_ComboBoxNotationInterimaire();
        }


        #endregion

        #region Champs Notation commentaire
        private bool Verif_TextBoxNotationCommentairesInterimaire()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if ((this._TextBoxNotationCommentairesInterimaire.Text.Trim().Length > 0))
            {
                verif = true;
                this._TextBlockNotationCommentairesInterimaire.Foreground = Brushes.Green;
                this._TextBoxNotationCommentairesInterimaire.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockNotationCommentairesInterimaire.Foreground = Brushes.Green;
                this._TextBoxNotationCommentairesInterimaire.Background = vert;
            }

            return verif;
        }

        private void Verif_TextBoxNotationCommentairesInterimaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxNotationCommentairesInterimaire();
        }


        #endregion

        #region Champs Coef Delegation
        private bool Verif_TextBoxCoefficientDelegationInterimaire()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if ((this._TextBoxCoefficientDelegationInterimaire.Text.Trim().Length > 0))
            {
                verif = true;
                this._TextBlockCoefficientDelegationInterimaire.Foreground = Brushes.Green;
                this._TextBoxCoefficientDelegationInterimaire.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockCoefficientDelegationInterimaire.Foreground = Brushes.Green;
                this._TextBoxCoefficientDelegationInterimaire.Background = vert;
            }

            return verif;
        }

        private void Verif_TextBoxCoefficientDelegationInterimaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCoefficientDelegationInterimaire();
        }
        #endregion

        #region Champs Coef Gestion
        private bool Verif_TextBoxCoefficientGestionInterimaire()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if ((this._TextBoxCoefficientGestionInterimaire.Text.Trim().Length > 0))
            {
                verif = true;
                this._TextBlockCoefficientGestionInterimaire.Foreground = Brushes.Green;
                this._TextBoxCoefficientGestionInterimaire.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBlockCoefficientGestionInterimaire.Foreground = Brushes.Green;
                this._TextBoxCoefficientGestionInterimaire.Background = vert;
            }

            if (verif)
            {
                this._TextBoxCoefficientGestionInterimaire.Foreground = Brushes.Green;
                this._TextBoxCoefficientGestionInterimaire.Background = vert;
            }
            else
            {
                this._TextBoxCoefficientGestionInterimaire.Foreground = Brushes.Red;
                this._TextBoxCoefficientGestionInterimaire.Background = rouge;
            }

            return verif;
        }

        private void Verif_TextBoxCoefficientGestionInterimaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxCoefficientGestionInterimaire();
        }

        #endregion

        #region ComboBox Agence

        private bool Verif_ComboBoxAgence()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxAgenceInterim.SelectedItem == null)
            {
                verif = false;
                this._TextBlockAgenceInterim.Foreground = Brushes.Red;
                this._ComboBoxAgenceInterim.Background = rouge;
            }
            else
            {
                verif = true;
                this._TextBlockAgenceInterim.Foreground = Brushes.Green;
                this._ComboBoxAgenceInterim.Background = vert;
            }

            return verif;
        }

        private void _ComboBoxAgenceInterim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxAgence();
        }

        #endregion

        #endregion

        #region Salarie Tiers

        private bool verif_tab_SalarieTiers()
        {
            bool test = true;

            if (this.checkBox_SalarieTiers.IsChecked == true)
            {
                if (!Verif_dataGridCompetencesTiers())
                {
                    test = false;
                }
                if (!Verif_textBoxMobiliteTiersCommentaire())
                {
                    test = false;
                }
                if (!Verif_dataGridMobiliteTiers())
                {
                    test = false;
                }
                if (!Verif_TextBoxExperienceCommentairesTiers())
                {
                    test = false;
                }
                if (!Verif_ComboBoxNotationTiers())
                {
                    test = false;
                }
                if (!Verif_TextBoxNotationCommentairesTiers())
                {
                    test = false;
                }
            }



            if (test == true)
            {
                this._tabSalarieTiers.Background = Brushes.Green;
            }
            else
            {
                this._tabSalarieTiers.Background = Brushes.Red;
            }

            return test;
        }

        #region DataGrid Compétences
        private bool Verif_dataGridCompetencesTiers()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._dataGridCompetencesTiers.Items.Count > 0)
            {
                verif = true;
                this._dataGridCompetencesTiers.Background = vert;
            }
            else
            {
                verif = true;
                this._dataGridCompetencesTiers.Background = vert;
            }

            return verif;
        }
        #endregion

        #region champ Mobilité Commentaire

        private bool Verif_textBoxMobiliteTiersCommentaire()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if ((this._TextBoxExperienceCommentairesTiers.Text.Trim().Length > 0))
            {
                verif = true;
                this._textBlockTiersMobiliteCommentaire.Foreground = Brushes.Green;
                this._TextBoxTiersMobiliteCommentaire.Background = vert;
            }
            else
            {
                verif = true;
                this._textBlockTiersMobiliteCommentaire.Foreground = Brushes.Green;
                this._TextBoxTiersMobiliteCommentaire.Background = vert;
            }

            return verif;
        }

        private void _TextBoxTiersMobiliteCommentaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_textBoxMobiliteTiersCommentaire();
        }

        #endregion

        #region DataGrid Mobilité
        private bool Verif_dataGridMobiliteTiers()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._dataGridMobiliteTiers.Items.Count > 0)
            {
                verif = true;
                this._dataGridMobiliteTiers.Background = vert;
            }
            else
            {
                verif = true;
                this._dataGridMobiliteTiers.Background = vert;
            }

            return verif;
        }
        #endregion

        #region Champs Commentaire experience
        private bool Verif_TextBoxExperienceCommentairesTiers()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if ((this._TextBoxExperienceCommentairesTiers.Text.Trim().Length > 0))
            {
                verif = true;
                this._TextBoxExperienceCommentairesTiers.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBoxExperienceCommentairesTiers.Background = vert;
            }

            return verif;
        }

        private void Verif_TextBoxExperienceCommentairesTiers_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxExperienceCommentairesTiers();
        }

        #endregion

        #region Champs Notation
        private bool Verif_ComboBoxNotationTiers()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._ComboBoxNotationTiers.SelectedItem == null)
            {
                verif = true;
                this._ComboBoxNotationTiers.BorderBrush = vert;
            }
            else
            {
                verif = true;
                this._ComboBoxNotationTiers.BorderBrush = vert;
            }

            return verif;
        }

        private void _ComboBoxNotationTiers_SelectionChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_ComboBoxNotationTiers();
        }

        #endregion

        #region Champs Notation commentaire
        private bool Verif_TextBoxNotationCommentairesTiers()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if ((this._TextBoxNotationCommentairesTiers.Text.Trim().Length > 0))
            {
                verif = true;
                this._TextBoxNotationCommentairesTiers.Background = vert;
            }
            else
            {
                verif = true;
                this._TextBoxNotationCommentairesTiers.Background = vert;
            }

            return verif;
        }

        private void Verif_TextBoxNotationCommentairesTiers_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxNotationCommentairesTiers();
        }
        #endregion

        #endregion

        #endregion

        #region Tab Comptabilité

        private bool verif_tab_comptabilite()
        {
            bool test = true;

            if (!Verif_TextBoxSalarieTauxHoraire())
            {
                test = false;
            }


            if (test == true)
            {
                this._tabComptabilite.Background = Brushes.Green;
            }
            else
            {
                this._tabComptabilite.Background = Brushes.Red;
            }

            return test;
        }

        #region Champs Taux Horaire
        private bool Verif_TextBoxSalarieTauxHoraire()
        {
            bool verif = true;

            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if ((this._TextBoxSalarieTauxHoraire.Text.Trim().Length > 0))
            {
                double test;

                if (double.TryParse(this._TextBoxSalarieTauxHoraire.Text, out test))
                {
                    verif = true;
                    this._TextBlockSalarieTauxHoraire.Foreground = Brushes.Green;
                    this._TextBoxSalarieTauxHoraire.Background = vert;
                }
                else
                {
                    verif = false;
                    this._TextBlockSalarieTauxHoraire.Foreground = Brushes.Red;
                    this._TextBoxSalarieTauxHoraire.Background = rouge;
                }
            }
            else
            {
                verif = true;
                this._TextBlockSalarieTauxHoraire.Foreground = Brushes.Green;
                this._TextBoxSalarieTauxHoraire.Background = vert;
            }

            return verif;
        }

        private void _TextBoxSalarieTauxHoraire_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Verif_TextBoxSalarieTauxHoraire();
        }
        #endregion

        #endregion

        #endregion

        #region Fonctions

        #region initiales
        private void constructionInitiales()
        {
            String initiales = "";
            int i = 0;
            if (this._TextBoxSalariePrenom.Text.Length > 0)
            {
                foreach (char c in this._TextBoxSalariePrenom.Text)
                {
                    if (i == 0)
                    {
                        initiales += c;
                    }
                    i++;
                    if (c == '-')
                    {
                        i = 0;
                    }
                }
            }

            if (this._TextBoxSalarieNom.Text.Length > 0)
            {
                i = 0;
                foreach (char c in this._TextBoxSalarieNom.Text)
                {
                    if (i == 0)
                    {
                        initiales += c;
                    }
                    i++;
                }
            }

            this._TextBoxSalarieInitiales.Text = initiales.ToUpper();
        }

        #endregion

        #region Lecture Seule

        public void LectureSeule()
        {
            // Info generales
            _ComboBoxSalarieCivilite.IsEnabled = false;
            _TextBoxSalarieNom.IsEnabled = false;
            _TextBoxSalarieNomJeuneFille.IsEnabled = false;
            _TextBoxSalariePrenom.IsEnabled = false;
            _TextBoxSalarieInitiales.IsEnabled = false;
            _DatePickerSalarieDateNaissance.IsEnabled = false;
            _TextBoxSalarieNumeroSecuriteSociale.IsEnabled = false;
            _TextBoxSalarieNumeroPasseport.IsEnabled = false;
            _DatePickerSalarieDateValiditePasseport.IsEnabled = false;
            _TextBoxSalarieTelephoneportablePerso.IsEnabled = false;
            _TextBoxSalarieTelephonefixePerso.IsEnabled = false;
            _TextBoxSalarieEmailPerso.IsEnabled = false;
            _TextBoxSalarieFaxPerso.IsEnabled = false;
            _TextBoxSalarieTelephoneportablePro.IsEnabled = false;
            _TextBoxSalarieTelephonefixePro.IsEnabled = false;
            _TextBoxSalarieEmailPro.IsEnabled = false;
            _TextBoxSalarieFaxPro.IsEnabled = false;
            _TextBoxSalarieAdressePerso.IsEnabled = false;
            _TextBoxSalarieAdresseComplementairePerso.IsEnabled = false;
            _ComboBoxSalarieVillePerso.IsEnabled = false;
            _TextBoxSalarieCodePostalPerso.IsEnabled = false;
            _ComboBoxSalariePaysPerso.IsEnabled = false;
            _TextBoxSalarieAdressePro.IsEnabled = false;
            _TextBoxSalarieAdresseComplementairePro.IsEnabled = false;
            _ComboBoxSalarieVillePro.IsEnabled = false;
            _TextBoxSalarieCodePostalPro.IsEnabled = false;
            _ComboBoxSalariePaysPro.IsEnabled = false;
            _ButtonEmploiNouveau.IsEnabled = false;
            _ButtonEmploiSupprimer.IsEnabled = false;
            _ComboBoxSalarieContrat.IsEnabled = false;
            _ComboBoxSalarieEntreprise.IsEnabled = false;
            _TextBoxSalarieCommentaires.IsEnabled = false;
            _TextBoxSalarieCentresInterets.IsEnabled = false;
            _TextBoxSalarieDistanceAtelier.IsEnabled = false;
            _TextBoxSalarieTempsRouteAtelier.IsEnabled = false;

            // Competences

            _ButtonFormationsNouveau.IsEnabled = false;
            _ButtonFormationsSupprimer.IsEnabled = false;
            _ButtonHabilitationsNouveau.IsEnabled = false;
            _ButtonHabilitationsSupprimer.IsEnabled = false;
            _ButtonPermisNouveau.IsEnabled = false;
            _ButtonPermisSupprimer.IsEnabled = false;
            _ButtonDiplomesNouveau.IsEnabled = false;
            _ButtonDiplomesSupprimer.IsEnabled = false;

            //Repondeur congé

            _ButtonNouveauRepondeur.IsEnabled = false;
            _ButtonSupprimerRepondeur.IsEnabled = false;

            // Vetement de travail

            _DatePickerSalarieVeste.IsEnabled = false;
            _DatePickerSalariePantalon.IsEnabled = false;
            _DatePickerSalarieBlouse.IsEnabled = false;
            _DatePickerSalarieCombinaison.IsEnabled = false;
            _DatePickerSalarieTeeShirts.IsEnabled = false;
            _DatePickerSalariePolaire.IsEnabled = false;
            _DatePickerSalarieBlouson.IsEnabled = false;
            _DatePickerSalarieChaussure.IsEnabled = false;
            _DatePickerSalarieBotte.IsEnabled = false;
            _ComboBoxSalarieVesteTaille.IsEnabled = false;
            _ComboBoxSalariePantalonTaille.IsEnabled = false;
            _ComboBoxSalarieBlouseTaille.IsEnabled = false;
            _ComboBoxSalarieCombinaisonTaille.IsEnabled = false;
            _ComboBoxSalarieTeeShirtsTaille.IsEnabled = false;
            _ComboBoxSalariePolaireTaille.IsEnabled = false;
            _ComboBoxSalarieBlousonTaille.IsEnabled = false;
            _ComboBoxSalarieChaussureTaille.IsEnabled = false;
            _ComboBoxSalarieBotteTaille.IsEnabled = false;
            _TextBoxSalarieVesteQuantite.IsEnabled = false;
            _TextBoxSalariePantalonQuantite.IsEnabled = false;
            _TextBoxSalarieBlouseQuantite.IsEnabled = false;
            _TextBoxSalarieCombinaisonQuantite.IsEnabled = false;
            _TextBoxSalarieTeeShirtsQuantite.IsEnabled = false;
            _TextBoxSalariePolaireQuantite.IsEnabled = false;
            _TextBoxSalarieBlousonQuantite.IsEnabled = false;
            _TextBoxSalarieChaussureQuantite.IsEnabled = false;
            _TextBoxSalarieBotteQuantite.IsEnabled = false;

            // Info professionnelles

            checkBox_SalarieInterne.IsEnabled = false;
            _DatePickerSalarieInterneDateEntree.IsEnabled = false;
            _ComboBoxSalarieInterneEntrepriseMere.IsEnabled = false;
            _DatePickerSalarieInterneDateSortie.IsEnabled = false;
            _TextBoxSalarieInterneMatriculeSociete.IsEnabled = false;
            _TextBoxSalarieNumeroInterne.IsReadOnly = true;

            checkBox_SalarieInterimaire.IsEnabled = false;
            _ButtonCompetencesInterimaireNouveau.IsEnabled = false;
            _ButtonCompetencesInterimaireSupprimer.IsEnabled = false;
            _ButtonMobiliteInterimaireNouveau.IsEnabled = false;
            _ButtonMobiliteInterimaireSupprimer.IsEnabled = false;
            _TextBoxPolyvalenceCommentairesInterimaire.IsEnabled = false;
            _TextBoxExperienceCommentairesInterimaire.IsEnabled = false;
            _ComboBoxNotationInterimaire.IsEnabled = false;
            _TextBoxNotationCommentairesInterimaire.IsEnabled = false;
            _TextBoxCoefficientDelegationInterimaire.IsEnabled = false;
            _TextBoxCoefficientGestionInterimaire.IsEnabled = false;
            _TextBoxInterimaireMobiliteCommentaire.IsEnabled = false;
            _ComboBoxAgenceInterim.IsEnabled = false;

            checkBox_SalarieTiers.IsEnabled = false;
            _ButtonCompetencesTiersNouveau.IsEnabled = false;
            _ButtonCompetencesTiersSupprimer.IsEnabled = false;
            _ButtonMobiliteTiersNouveau.IsEnabled = false;
            _ButtonMobiliteTiersSupprimer.IsEnabled = false;
            _TextBoxPolyvalenceCommentairesTiers.IsEnabled = false;
            _TextBoxExperienceCommentairesTiers.IsEnabled = false;
            _ComboBoxNotationTiers.IsEnabled = false;
            _TextBoxNotationCommentairesTiers.IsEnabled = false;
            _TextBoxTiersMobiliteCommentaire.IsEnabled = false;

            this.checkBoxIsChantier.IsEnabled = false;
            this.checkBoxIsChargeAffaire.IsEnabled = false;

            // Comptabilité

            _TextBoxSalarieTauxHoraire.IsEnabled = false;

            // Outillage

            _ButtonVersDroite.IsEnabled = false;
            _ButtonVersGauche.IsEnabled = false;
            _TextBoxIdentificateurOutillage.IsEnabled = false;
        }

        #endregion

        #region Fonctions sur salarie

        private void checkBox_SalarieTiers_Click(object sender, RoutedEventArgs e)
        {
            if (this.checkBox_SalarieTiers.IsChecked == true)
            {
                if (this.checkBox_SalarieInterne.IsChecked == true)
                {
                    MessageBox.Show("Le salarié était enregistré en 'salarié interne'. Il passe maintenant en Tiers. Ses informations de 'salarié interne' sont maintenant supprimées. Si vous souhaitez les récupérer, annulez votre modification si vous êtes en modification", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.bloquerSalarieInterne();
                    this.checkBox_SalarieInterne.IsChecked = false;
                }
                if (this.checkBox_SalarieInterimaire.IsChecked == true)
                {
                    MessageBox.Show("Le salarié était enregistré en 'interimaire'. Il passe maintenant en Tiers. Ses informations de 'interimaire' sont maintenant supprimées. Si vous souhaitez les récupérer, annulez votre modification si vous êtes en modification", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.bloquerSalarieInterimaire();
                    this.checkBox_SalarieInterimaire.IsChecked = false;
                }
                this.debloquerSalarieTiers();
            }
            else
            {
                MessageBox.Show("Le salarié était enregistré en 'tiers'. Il n'a maintenant plus de statut. Ses informations de 'tiers' sont maintenant supprimées. Si vous souhaitez les récupérer, annulez votre modification si vous êtes en modification", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                this.bloquerSalarieTiers();
            }
        }

        private void checkBox_SalarieInterimaire_Click(object sender, RoutedEventArgs e)
        {
            if (this.checkBox_SalarieInterimaire.IsChecked == true)
            {
                if (this.checkBox_SalarieInterne.IsChecked == true)
                {
                    MessageBox.Show("Le salarié était enregistré en 'salarié interne'. Il passe maintenant en 'Intérimaire'. Ses informations de 'salarié interne' sont maintenant supprimées. Si vous souhaitez les récupérer, annulez votre modification si vous êtes en modification", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.bloquerSalarieInterne();
                    this.checkBox_SalarieInterne.IsChecked = false;
                }
                if (this.checkBox_SalarieTiers.IsChecked == true)
                {
                    MessageBox.Show("Le salarié était enregistré en 'tiers'. Il passe maintenant en 'Intérimaire'. Ses informations de 'tiers' sont maintenant supprimées. Si vous souhaitez les récupérer, annulez votre modification si vous êtes en modification", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.bloquerSalarieTiers();
                    this.checkBox_SalarieTiers.IsChecked = false;
                }
                this.debloquerSalarieInterimaire();
            }
            else
            {
                MessageBox.Show("Le salarié était enregistré en 'intérimaire'. Il n'a maintenant plus de statut. Ses informations de 'intérimaire' sont maintenant supprimées. Si vous souhaitez les récupérer, annulez votre modification si vous êtes en modification", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                this.bloquerSalarieInterimaire();
            }
        }

        private void checkBox_SalarieInterne_Click(object sender, RoutedEventArgs e)
        {
            if (this.checkBox_SalarieInterne.IsChecked == true)
            {
                if (this.checkBox_SalarieInterimaire.IsChecked == true)
                {
                    MessageBox.Show("Le salarié était enregistré en 'interimaire'. Il passe maintenant en 'Salarié Interne'. Ses informations de 'interimaire' sont maintenant supprimées. Si vous souhaitez les récupérer, annulez votre modification si vous êtes en modification", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.bloquerSalarieInterimaire();
                    this.checkBox_SalarieInterimaire.IsChecked = false;
                }
                if (this.checkBox_SalarieTiers.IsChecked == true)
                {
                    MessageBox.Show("Le salarié était enregistré en 'tiers'. Il passe maintenant en 'Salarié Interne'. Ses informations de 'tiers' sont maintenant supprimées. Si vous souhaitez les récupérer, annulez votre modification si vous êtes en modification", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.bloquerSalarieTiers();
                    this.checkBox_SalarieTiers.IsChecked = false;
                }
                this.debloquerSalarieInterne();
            }
            else
            {
                MessageBox.Show("Le salarié était enregistré en 'salarié interne'. Il n'a maintenant plus de statut. Ses informations de 'salarié interne' sont maintenant supprimées. Si vous souhaitez les récupérer, annulez votre modification si vous êtes en modification", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Information);
                this.bloquerSalarieInterne();
            }
        }

        public void bloquerSalarieInterne()
        {
            _DatePickerSalarieInterneDateEntree.IsEnabled = false;
            _ComboBoxSalarieInterneEntrepriseMere.IsEnabled = false;
            _DatePickerSalarieInterneDateSortie.IsEnabled = false;
            _TextBoxSalarieInterneMatriculeSociete.IsEnabled = false;
            _TextBoxSalarieNumeroInterne.IsEnabled = false;
            _ButtonInterneVisiteMedicaleNouveau.IsEnabled = false;
            _ButtonInterneVisiteMedicaleModifier.IsEnabled = false;
            _ButtonInterneVisiteMedicaleSupprimer.IsEnabled = false;
        }

        public void bloquerSalarieInterimaire()
        {
            _ButtonCompetencesInterimaireNouveau.IsEnabled = false;
            _ButtonCompetencesInterimaireSupprimer.IsEnabled = false;
            _ButtonMobiliteInterimaireNouveau.IsEnabled = false;
            _ButtonMobiliteInterimaireSupprimer.IsEnabled = false;
            _TextBoxPolyvalenceCommentairesInterimaire.IsEnabled = false;
            _TextBoxExperienceCommentairesInterimaire.IsEnabled = false;
            _ComboBoxNotationInterimaire.IsEnabled = false;
            _TextBoxNotationCommentairesInterimaire.IsEnabled = false;
            _TextBoxCoefficientDelegationInterimaire.IsEnabled = false;
            _TextBoxCoefficientGestionInterimaire.IsEnabled = false;
            _TextBoxInterimaireMobiliteCommentaire.IsEnabled = false;
            _ComboBoxAgenceInterim.IsEnabled = false;
        }

        public void bloquerSalarieTiers()
        {
            _ButtonCompetencesTiersNouveau.IsEnabled = false;
            _ButtonCompetencesTiersSupprimer.IsEnabled = false;
            _ButtonMobiliteTiersNouveau.IsEnabled = false;
            _ButtonMobiliteTiersSupprimer.IsEnabled = false;
            _TextBoxPolyvalenceCommentairesTiers.IsEnabled = false;
            _TextBoxExperienceCommentairesTiers.IsEnabled = false;
            _ComboBoxNotationTiers.IsEnabled = false;
            _TextBoxNotationCommentairesTiers.IsEnabled = false;
            _TextBoxTiersMobiliteCommentaire.IsEnabled = false;
        }

        public void debloquerSalarieInterne()
        {
            if (this._DatePickerSalarieInterneDateEntree.Text == "01/01/0001")
            {
                _DatePickerSalarieInterneDateEntree.SelectedDate = null;
            }
            _DatePickerSalarieInterneDateEntree.IsEnabled = true;
            _ComboBoxSalarieInterneEntrepriseMere.IsEnabled = true;
            _DatePickerSalarieInterneDateSortie.IsEnabled = true;
            _TextBoxSalarieInterneMatriculeSociete.IsEnabled = true;
            _TextBoxSalarieNumeroInterne.IsEnabled = true;
            _ButtonInterneVisiteMedicaleNouveau.IsEnabled = true;
            _ButtonInterneVisiteMedicaleModifier.IsEnabled = true;
            _ButtonInterneVisiteMedicaleSupprimer.IsEnabled = true;
        }

        public void debloquerSalarieInterimaire()
        {
            _ButtonCompetencesInterimaireNouveau.IsEnabled = true;
            _ButtonCompetencesInterimaireSupprimer.IsEnabled = true;
            _ButtonMobiliteInterimaireNouveau.IsEnabled = true;
            _ButtonMobiliteInterimaireSupprimer.IsEnabled = true;
            _TextBoxPolyvalenceCommentairesInterimaire.IsEnabled = true;
            _TextBoxExperienceCommentairesInterimaire.IsEnabled = true;
            _ComboBoxNotationInterimaire.IsEnabled = true;
            _TextBoxNotationCommentairesInterimaire.IsEnabled = true;
            _TextBoxCoefficientDelegationInterimaire.IsEnabled = true;
            _TextBoxCoefficientGestionInterimaire.IsEnabled = true;
            _TextBoxInterimaireMobiliteCommentaire.IsEnabled = true;
            _ComboBoxAgenceInterim.IsEnabled = true;
        }

        public void debloquerSalarieTiers()
        {
            _ButtonCompetencesTiersNouveau.IsEnabled = true;
            _ButtonCompetencesTiersSupprimer.IsEnabled = true;
            _ButtonMobiliteTiersNouveau.IsEnabled = true;
            _ButtonMobiliteTiersSupprimer.IsEnabled = true;
            _TextBoxPolyvalenceCommentairesTiers.IsEnabled = true;
            _TextBoxExperienceCommentairesTiers.IsEnabled = true;
            _ComboBoxNotationTiers.IsEnabled = true;
            _TextBoxNotationCommentairesTiers.IsEnabled = true;
            _TextBoxTiersMobiliteCommentaire.IsEnabled = true;
        }

        #endregion

        #region Fonctions Adresses auto

        private void _ComboBoxCoordonneesVille_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.VilleListPerso.Count == 0)
            {
                MessageBox.Show("Attention, aucune ville ne correspond à votre numéro de code postal et/ou votre pays.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void _ComboBoxSalarieVillePro_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.VilleListPro.Count == 0)
            {
                MessageBox.Show("Attention, aucune ville ne correspond à votre numéro de code postal et/ou votre pays.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;

            if (((App)App.Current)._connectedUser.Niveau_Securite1.SalarieRepondeurConge == true)
            {
                this._tabRepondeursConge.Visibility = Visibility.Visible;
            }
            else
            {
                this._tabRepondeursConge.Visibility = Visibility.Collapsed;
            }

            if (((App)App.Current)._connectedUser.Niveau_Securite1.SalarieRepondeurConge == true)
            {
                this._tabComptabilite.Visibility = Visibility.Visible;
            }
            else
            {
                this._tabComptabilite.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

    }
}
