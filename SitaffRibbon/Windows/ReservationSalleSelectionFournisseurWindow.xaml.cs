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
using System.Windows.Shapes;
/* 
 * Using pour utilisation des IObservableCollection (afin d'éviter de mettre
 * System.Collections.ObjectModel.IObservableCollection en entier)
 */
using System.Collections.ObjectModel;
//Using pour utiliser le type TypeConverter pour la conversion de couleur
using System.ComponentModel;
using SitaffRibbon.UserControls;
using SitaffRibbon.Windows.ParametresWindows;
using SitaffRibbon.Windows.ParametresUserControls;
using SitaffRibbon.Classes;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour ReservationSalleSelectionFournisseurWindow.xaml
    /// </summary>
    public partial class ReservationSalleSelectionFournisseurWindow : Window
    {
        public ReservationSalleSelectionFournisseurWindow()
        {
            InitializeComponent();
            this.listEntreprise = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.Where(ent => ent.Is_Fournisseur == true && ent.Fournisseur != null).OrderBy(ent => ent.Libelle));
        }

        #region Propriété de dépendence

        #region Fournisseur


        public ObservableCollection<Entreprise> listEntreprise
        {
            get { return (ObservableCollection<Entreprise>)GetValue(listEntrepriseProperty); }
            set { SetValue(listEntrepriseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listEntreprise.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listEntrepriseProperty =
            DependencyProperty.Register("listEntreprise", typeof(ObservableCollection<Entreprise>), typeof(ReservationSalleSelectionFournisseurWindow), new UIPropertyMetadata(null));

        #endregion
        
        #region Contact

        public ObservableCollection<Contact> listContact
        {
            get { return (ObservableCollection<Contact>)GetValue(listContactProperty); }
            set { SetValue(listContactProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listContact.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listContactProperty =
            DependencyProperty.Register("listContact", typeof(ObservableCollection<Contact>), typeof(ReservationSalleSelectionFournisseurWindow), new UIPropertyMetadata(null));

        #endregion

        #endregion

        #region Boutons

        #region Bouton Ok et Cancel
        /// <summary>
        /// Fonction lancée après clic sur Annuler
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (Verif_General())
            {
                this.DialogResult = true;
                this.Close();
            } 
        }
        /// <summary>
        /// Fonction lancée après clic sur Annuler
        /// </summary>
        /// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
        /// <param name="e"></param>
        private void _ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        #region new Fournisseur
        private void NewFournisseur_Click(object sender, RoutedEventArgs e)
        {
            EntrepriseWindow entrepriseWindow = new EntrepriseWindow();
            Entreprise tmp = new Entreprise();
            tmp.Adresse1 = new Adresse();
            tmp.Adresse2 = new Adresse();
            tmp.Client = new Client();
            tmp.Is_Client = true;
            tmp.Fournisseur = new Fournisseur();
            entrepriseWindow.DataContext = tmp;
            entrepriseWindow.creation = true;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = entrepriseWindow.ShowDialog();
            Entreprise entreprise = (Entreprise)entrepriseWindow.DataContext;

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                ((App)App.Current).mySitaffEntities.AddToEntreprise(entreprise);
                if (entreprise.Fournisseur != null && entreprise.Is_Fournisseur == true)
                {
                    this.listEntreprise = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.Where(civ => civ.Fournisseur != null && civ.Is_Fournisseur == true).OrderBy(civ => civ.Libelle));
                    this.listEntreprise.Add(entreprise);
                    this.listEntreprise = new ObservableCollection<Entreprise>(this.listEntreprise.OrderBy(tc => tc.Libelle));

                    this._comboBoxFournisseur.SelectedItem = entreprise;
                }
            }
            else
            {
                //Entreprise non validée (on détache tout !)
                // On enlève tous les Commande_Fournisseur associés
                foreach (Commande_Fournisseur item in entreprise.Commande_Fournisseur1)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Commande_Fournisseur1.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Commande_Fournisseur1.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Commande_Fournisseur associés
                foreach (Commande_Fournisseur item in entreprise.Commande_Fournisseur2)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Commande_Fournisseur1.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Commande_Fournisseur1.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Entreprise_Activite associés
                foreach (Entreprise_Activite item in entreprise.Entreprise_Activite)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Entreprise_Activite.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Entreprise_Activite.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Entreprise_Litige associés
                foreach (Entreprise_Litige item in entreprise.Entreprise_Litige)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Entreprise_Litige.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Entreprise_Litige.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Entreprise_Mere associés
                foreach (Entreprise_Mere item in entreprise.Entreprise_Mere)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Entreprise_Mere.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Entreprise_Mere.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Numero_Tva_Intraco associés
                foreach (Numero_Tva_Intraco item in entreprise.Numero_Tva_Intraco)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Numero_Tva_Intraco.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Numero_Tva_Intraco.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les NumeroTvaIntracommunautaire associés
                foreach (NumeroTvaIntracommunautaire item in entreprise.NumeroTvaIntracommunautaire)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.NumeroTvaIntracommunautaire.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.NumeroTvaIntracommunautaire.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                // On enlève tous les Personne associés
                foreach (Personne item in entreprise.Personne)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Detach(item);
                        entreprise.Personne.Remove(item);

                    }
                    catch (Exception)
                    {
                        entreprise.Personne.Remove(item);
                        ((App)App.Current).mySitaffEntities.Detach(item);
                    }
                }
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(entreprise.Fournisseur);
                    entreprise.Fournisseur = null;

                }
                catch (Exception)
                {
                    try
                    {
                        entreprise.Fournisseur = null;
                        ((App)App.Current).mySitaffEntities.Detach(entreprise.Fournisseur);
                    }
                    catch (Exception)
                    {

                    }
                }
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(entreprise.Client);
                    entreprise.Client = null;

                }
                catch (Exception)
                {
                    try
                    {
                        entreprise.Client = null;
                        ((App)App.Current).mySitaffEntities.Detach(entreprise.Client);
                    }
                    catch (Exception)
                    {

                    }
                }
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(entreprise);
                }
                catch (Exception)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Entreprise.DeleteObject(entreprise);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        #endregion

        #region new Contact
        private void NewContact_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow contactWindow = new ContactWindow();
            Personne tmp = new Personne();

            ObservableCollection<Entreprise> toPutOnComboBox = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.OrderBy(ent => ent.Libelle));
            if (!toPutOnComboBox.Contains((Entreprise)this._comboBoxFournisseur.SelectedItem))
            {
                toPutOnComboBox.Add((Entreprise)this._comboBoxFournisseur.SelectedItem);
            }
            contactWindow.EntrepriseList = new ObservableCollection<Entreprise>(toPutOnComboBox.OrderBy(ent => ent.Libelle));

            if (this._comboBoxFournisseur.SelectedItem != null)
            {
                tmp.Entreprise1 = (Entreprise)this._comboBoxFournisseur.SelectedItem;
            }
            tmp.Contact = new Contact();
            contactWindow.DataContext = tmp;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = contactWindow.ShowDialog();
            Personne personne = (Personne)contactWindow.DataContext;

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                ((App)App.Current).mySitaffEntities.AddToPersonne(personne);
                this.listContact.Clear();
                if (this._comboBoxFournisseur.SelectedItem != null)
                {
                    foreach (Personne pers in ((Entreprise)this._comboBoxFournisseur.SelectedItem).Personne.Where(per => per.Contact != null))
                    {
                        this.listContact.Add(pers.Contact);
                    }
                }
                this._comboBoxContact.SelectedItem = personne.Contact;
                if (this._comboBoxFournisseur.SelectedItem != null)
                {
                    Entreprise E = (Entreprise)this._comboBoxFournisseur.SelectedItem;
                    ObservableCollection<Contact> temp = new ObservableCollection<Contact>();
                    foreach (Personne p in E.Personne)
                    {
                        if (p.Contact != null)
                        {
                            temp.Add(p.Contact);
                        }
                    }
                    this._comboBoxContact.ItemsSource = temp;
                }
            }
            else
            {
                try
                {
                    ((App)App.Current).mySitaffEntities.Detach(personne.Contact);
                    ((App)App.Current).mySitaffEntities.Detach(personne);
                }
                catch (Exception)
                {
                    try
                    {
                        ((App)App.Current).mySitaffEntities.Contact.DeleteObject(personne.Contact);
                        ((App)App.Current).mySitaffEntities.Personne.DeleteObject(personne);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        #endregion

        #endregion

        #region Vérifications

        private bool Verif_General()
        {
            bool verif = true;
            if (!this.Verif_ComboBoxFournisseur())
            {
                verif = false;
            }
            if (!this.Verif_ComboBoxContact())
            {
                verif = false;
            }
            return verif;
        }

        #region ComboBox

        #region Fournisseur
        private bool Verif_ComboBoxFournisseur()
        {
            bool verif = true;
            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._comboBoxFournisseur.SelectedItem == null)
            {
                verif = false;
                this._comboBoxFournisseur.Background = rouge;
            }
            else
            {
                verif = true;
                this._comboBoxFournisseur.Background = vert;
            }
            return verif;
        }
        private void _ComboBoxFournisseur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxFournisseur();
            this.listContact = new ObservableCollection<Contact>(((App)App.Current).mySitaffEntities.Contact.Where(sa => sa.Personne.Entreprise1.Identifiant == ((Entreprise)this._comboBoxFournisseur.SelectedItem).Identifiant));
        }
        #endregion

        #region Contact
        private bool Verif_ComboBoxContact()
        {
            bool verif = true;
            //Initialisation des couleurs avec transparence
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexavert = "#8900CE00";
            Brush vert = (Brush)converter.ConvertFrom(colorHexavert);
            string colorHexarouge = "#89FF0000";
            Brush rouge = (Brush)converter.ConvertFrom(colorHexarouge);

            if (this._comboBoxContact.SelectedItem == null)
            {
                verif = false;
                this._comboBoxContact.Background = rouge;
            }
            else
            {
                verif = true;
                this._comboBoxContact.Background = vert;
            }
            return verif;
        }
        private void _ComboBoxContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_ComboBoxContact();
            if (this._comboBoxContact.SelectedItem != null)
            {
                this._comboBoxFournisseur.SelectedItem = ((Contact)this._comboBoxContact.SelectedItem).Personne.Entreprise1;
            }
        }
        #endregion

        #endregion

        #endregion

    }
}
