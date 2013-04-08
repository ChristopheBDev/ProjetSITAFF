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
using SitaffRibbon.UserControls;
using SitaffRibbon.Classes;


namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour DevisChoixContactWindow.xaml
    /// </summary>
    public partial class DevisChoixContactWindow : Window
    {

        public Devis devis;
        public Entreprise entreprise;

        public DevisChoixContactWindow(Entreprise _entreprise)
        {
            InitializeComponent();

            //Mise en place des droits sur les boutons et tabs
            Securite securite = new Securite();
            if (!securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeContactsControl", "Add"))
            {
                this.NewContact.Visibility = Visibility.Collapsed;
            }
            if (!securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeContactsControl", "Look"))
            {
                this.LookContact.Visibility = Visibility.Collapsed;
            }

            this.entreprise = _entreprise;
        }

        #region proprièté de dépendance

        public ObservableCollection<Contact> Contacts
        {
            get { return (ObservableCollection<Contact>)GetValue(ContactsProperty); }
            set { SetValue(ContactsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contacts.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactsProperty =
            DependencyProperty.Register("Contacts", typeof(ObservableCollection<Contact>), typeof(DevisChoixContactWindow), new UIPropertyMetadata(null));

        

        #endregion

        private void _ButtonDevisChoixContactValider_Click(object sender, RoutedEventArgs e)
        {
            if (this.verificationChamps())
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un contact", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void _ButtonDevisChoixContactAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
 
        #region Verification

        public bool verificationChamps()
        {
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._ComboBoxDevisChoixContact, this._textBlockChoix);
        }

        #endregion

        private void _ComboBoxDevisChoixContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			if (verificationChamps())
            {
                this.DataContext = (Contact)this._ComboBoxDevisChoixContact.SelectedItem;
            }
        }

        #region Fonction
        #endregion


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #region contacts

        private void NewContact_Click(object sender, RoutedEventArgs e)
        {
            //ListeContactsControl listeContactsControl = new ListeContactsControl();
            //Personne personne = ((App)App.Current)._theMainWindow.AddContacts(listeContactsControl);
            //if (personne != null)
            //{
            //    if (personne.Contact != null)
            //    {
            //        if (devis.Client2 != null)
            //        {
            //            this.Contacts = new ObservableCollection<Contact>(((App)App.Current).mySitaffEntities.Contact.Where(con => con.Personne.Entreprise1.Libelle == devis.Client2.Entreprise.Libelle).OrderBy(con => con.Personne.Nom));
            //        }
            //        else
            //        {
            //            this.Contacts = new ObservableCollection<Contact>(((App)App.Current).mySitaffEntities.Contact.OrderBy(con => con.Personne.Nom));
            //        }
            //        this._ComboBoxDevisChoixContact.SelectedItem = personne.Contact;
            //    }
            //}
            //else
            //{
            //    this._ComboBoxDevisChoixContact.SelectedItem = null;
            //}
            ContactWindow contactWindow = new ContactWindow();
            Personne tmp = new Personne();
            if (this.entreprise != null)
            {
                tmp.Entreprise1 = this.entreprise;
                contactWindow._ComboBoxContactEntreprise.IsEnabled = false;
            }
            tmp.Contact = new Contact();
            contactWindow.DataContext = tmp;

            //booléen nullable vrai ou faux ou null
            bool? dialogResult = contactWindow.ShowDialog();
            Personne personne = (Personne)contactWindow.DataContext;

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                this.Contacts.Clear();
                this.Contacts = new ObservableCollection<Contact>(((App)App.Current).mySitaffEntities.Contact.Where(con => con.Personne.Entreprise1.Libelle == this.entreprise.Libelle).OrderBy(con => con.Personne.Nom));
                this.Contacts.Add(((Personne)contactWindow.DataContext).Contact);
                this._ComboBoxDevisChoixContact.SelectedItem = personne.Contact;                
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

        private void LookContact_Click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxDevisChoixContact.SelectedItem != null)
            {
                ListeContactsControl listeContactsControl = new ListeContactsControl();
                ((App)App.Current)._theMainWindow.LookContacts(listeContactsControl, ((Contact)this._ComboBoxDevisChoixContact.SelectedItem).Personne);
            }
        }

        #endregion
    }
}
