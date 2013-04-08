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
using SitaffRibbon.UserControls;
using SitaffRibbon.Windows.ParametresUserControls;
using SitaffRibbon.Windows.ParametresWindows;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour RelanceFactureWindow.xaml
    /// </summary>
    public partial class RelanceFactureWindow : Window
    {
        #region Attributs

		public bool soloLecture = false;

		#endregion

		#region Propriétés de dépendances

		public ObservableCollection<Facture> listFacture
		{
			get { return (ObservableCollection<Facture>)GetValue(listFactureProperty); }
			set { SetValue(listFactureProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listFacture.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listFactureProperty =
			DependencyProperty.Register("listFacture", typeof(ObservableCollection<Facture>), typeof(RelanceFactureWindow), new PropertyMetadata(null));

		public ObservableCollection<Salarie> listSalarie
		{
			get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
			set { SetValue(listSalarieProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listSalarieProperty =
            DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(RelanceFactureWindow), new PropertyMetadata(null));

		public ObservableCollection<Affaire> listAffaire
		{
			get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
			set { SetValue(listAffaireProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listAffaireProperty =
            DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(RelanceFactureWindow), new PropertyMetadata(null));

		public ObservableCollection<Contact> listContact
		{
			get { return (ObservableCollection<Contact>)GetValue(listContactProperty); }
			set { SetValue(listContactProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listContact.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listContactProperty =
            DependencyProperty.Register("listContact", typeof(ObservableCollection<Contact>), typeof(RelanceFactureWindow), new PropertyMetadata(null));

		public ObservableCollection<Client> listClient
		{
			get { return (ObservableCollection<Client>)GetValue(listClientProperty); }
			set { SetValue(listClientProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listClient.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listClientProperty =
			DependencyProperty.Register("listClient", typeof(ObservableCollection<Client>), typeof(RelanceFactureWindow), new PropertyMetadata(null));

		#endregion

		#region Lecture seule

		private void lectureSeule()
		{
			this._comboBoxAffaire.IsEnabled = false;
			this._comboBoxClient.IsEnabled = false;
			this._comboBoxContact.IsEnabled = false;
			this._comboBoxFacture.IsEnabled = false;
			this._comboBoxSalarie.IsEnabled = false;

			this._textBoxCommentaire.IsEnabled = false;
			this._textBoxCommentaireEnvoi.IsEnabled = false;
			this._textBoxCorps.IsEnabled = false;
			this._textBoxDateFacture.IsEnabled = false;
			this._textBoxDestinataire.IsEnabled = false;
			this._textBoxMontantFacture.IsEnabled = false;
			this._textBoxMontantRelance.IsEnabled = false;

			this._datePickerDateProchaineRelance.IsEnabled = false;
			this._datePickerDateRelance.IsEnabled = false;
		}

		#endregion

		#region Constructeur

		public RelanceFactureWindow()
		{
			InitializeComponent();

			InitialisationPropDep();
		}

		#region Initialisation

		private void InitialisationPropDep()
		{
			this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
			this.listFacture = new ObservableCollection<Facture>(((App)App.Current).mySitaffEntities.Facture.OrderBy(fac => fac.Numero));
			this.listSalarie = new ObservableCollection<Salarie>(((App)App.Current).mySitaffEntities.Salarie.Where(sal => sal.Personne != null).OrderBy(sal => sal.Personne.Nom).ThenBy(sal => sal.Personne.Prenom));
			this.listContact = new ObservableCollection<Contact>(((App)App.Current).mySitaffEntities.Contact.Where(con => con.Personne != null).OrderBy(con => con.Personne.Nom).ThenBy(con => con.Personne.Prenom));
			this.listClient = new ObservableCollection<Client>(((App)App.Current).mySitaffEntities.Client.OrderBy(cli => cli.Entreprise.Libelle));
		}

		#endregion
		
		#endregion

		#region Fenêtre chargée

		private void Window_Loaded_1(object sender, RoutedEventArgs e)
		{
			((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;

			DateAuto();
		}

		#endregion

		#region Boutons

		/// <summary>
		/// Fonction lancée après clic sur Ok
		/// </summary>
		/// <param name="sender">Objet qui a provoqué le lancement de la fonction</param>
		/// <param name="e"></param>
		private void _ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			if (this.VerificationChamps())
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

		#region Mail
		
		private void _buttonComposerAuto_Click_1(object sender, RoutedEventArgs e)
		{
			ComposerAuto();
		}

		private void _buttonEnvoyerMail_Click_1(object sender, RoutedEventArgs e)
		{
			EnvoyerMail();
		}

		#endregion

		#endregion

		#region Vérification

		private bool VerificationChamps()
		{
			bool verif = true;

			if (!Verif_MontantRelance())
			{
				verif = false;
			}
			if (!Verif_DateRelance())
			{
				verif = false;
			}
			if (!Verif_DateProchaineRelance())
			{
				verif = false;
			}
			if (!Verif_Salarie())
			{
				verif = false;
			}
			if (!Verif_Contact())
			{
				verif = false;
			}
			if (!Verif_Destinataire())
			{
				verif = false;
			}
			if (!Verif_Object())
			{
				verif = false;
			}
			if (!Verif_Corps())
			{
				verif = false;
			}
			if (!Verif_Commentaire())
			{
				verif = false;
			}
			if (!Verif_CommentaireEnvoi())
			{
				verif = false;
			}
			if (!Verif_Affaire())
			{
				verif = false;
			}
			if (!Verif_Client())
			{
				verif = false;
			}
			if (!Verif_Facture())
			{
				verif = false;
			}

			return verif;
		}

		#region Champs Facture

		private bool Verif_Facture()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxFacture, this._textBlockFacture);
		}

		private void _comboBoxFacture_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_Facture();
		}

		#endregion

		#region Champs Affaire

		private bool Verif_Affaire()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxAffaire, this._textBlockAffaire);
		}

		private void _comboBoxAffaire_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_Affaire();
		}

		#endregion

		#region Champs Client

		private bool Verif_Client()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxClient, this._textBlockClient);
		}

		private void _comboBoxClient_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_Client();
		}

		#endregion

		#region Champs MontantRelance

		private bool Verif_MontantRelance()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxMontantRelance, this._textBlockMontantRelance);
		}

		private void _textBoxMontantRelance_LostFocus_1(object sender, RoutedEventArgs e)
		{
			Verif_MontantRelance();
		}

		#endregion

		#region Champs DateRelance

		private bool Verif_DateRelance()
		{
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateRelance, this._textBlockDateRelance, ((Relance_Facture)this.DataContext).Facture1.Date_Facture.Value);
		}

		private void _datePickerDateRelance_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_DateRelance();
		}

		#endregion

		#region Champs DateProchaineRelance

		private bool Verif_DateProchaineRelance()
		{
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateProchaineRelance, this._textBlockDateProchaineRelance, this._datePickerDateRelance);
		}

		private void _datePickerDateProchaineRelance_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_DateProchaineRelance();
		}

		#endregion

		#region Champs Salarie

		private bool Verif_Salarie()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxSalarie, this._textBlockSalarie);
		}

		private void _comboBoxSalarie_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_Salarie();
		}

		#endregion

		#region Champs Contact

		private bool Verif_Contact()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxContact, this._textBlockContact);
		}

		private void _comboBoxContact_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			Verif_Contact();
		}

		#endregion

		#region Champs Destinataire

		private bool Verif_Destinataire()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxDestinataire, this._textBlockDestinataire);
		}

		private void _textBoxDestinataire_LostFocus_1(object sender, RoutedEventArgs e)
		{
			Verif_Destinataire();
		}

		#endregion

		#region Champs Object

		private bool Verif_Object()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxObjet, this._textBlockObjet);
		}

		private void _textBoxObjet_LostFocus_1(object sender, RoutedEventArgs e)
		{
			Verif_Object();
		}

		#endregion

		#region Champs Corps

		private bool Verif_Corps()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxCorps, this._textBlockCorps);
		}

		private void _textBoxCorps_LostFocus_1(object sender, RoutedEventArgs e)
		{
			Verif_Corps();
		}

		#endregion

		#region Champs Commentaire

		private bool Verif_Commentaire()
		{
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxCommentaire, this._textBlockCommentaire);
		}

		private void _textBoxCommentaire_LostFocus_1(object sender, RoutedEventArgs e)
		{
			Verif_Commentaire();
		}

		#endregion

		#region Champs CommentaireEnvoi

		private bool Verif_CommentaireEnvoi()
		{
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxCommentaireEnvoi, this._textBlockCommentaireEnvoi);
		}

		private void _textBoxCommentaireEnvoi_LostFocus_1(object sender, RoutedEventArgs e)
		{
			Verif_CommentaireEnvoi();
		}

		#endregion

		#endregion

		#region Fonction

		private void ComposerAuto()
		{

		}

		private void EnvoyerMail()
		{

		}

		private void DateAuto()
		{
			if (((Relance_Facture)this.DataContext).Date_Relance == null)
			{
				((Relance_Facture)this.DataContext).Date_Relance = DateTime.Today;
			}
		}

		#endregion

		#region Evenements

		#region KeyUp

		private void _textBoxMontant_KeyUp_1(object sender, KeyEventArgs e)
		{
			ReglageDecimales reg = new ReglageDecimales();
			reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
		}

		#endregion

		#endregion                        

    }
}
