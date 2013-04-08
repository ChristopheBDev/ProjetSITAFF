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
	/// Logique d'interaction pour OrdreMissionWindow.xaml
	/// </summary>
	public partial class OrdreMissionWindow : Window
	{
		#region Attributs

		public bool SoloLecture = false;
		public bool creation = false;

		#endregion

		#region Propriétés de dépendances

		#region General

		public ObservableCollection<Entreprise_Mere> listEntrepriseMere
		{
			get { return (ObservableCollection<Entreprise_Mere>)GetValue(listEntrepriseMereProperty); }
			set { SetValue(listEntrepriseMereProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listEntrepriseMere.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listEntrepriseMereProperty =
			DependencyProperty.Register("listEntrepriseMere", typeof(ObservableCollection<Entreprise_Mere>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Affaire> listAffaire
		{
			get { return (ObservableCollection<Affaire>)GetValue(listAffaireProperty); }
			set { SetValue(listAffaireProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listAffaire.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listAffaireProperty =
			DependencyProperty.Register("listAffaire", typeof(ObservableCollection<Affaire>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Salarie> listDonneurOrdre
		{
			get { return (ObservableCollection<Salarie>)GetValue(listDonneurOrdreProperty); }
			set { SetValue(listDonneurOrdreProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listDonneurOrdre.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listDonneurOrdreProperty =
			DependencyProperty.Register("listDonneurOrdre", typeof(ObservableCollection<Salarie>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Entreprise> listLieuMission
		{
			get { return (ObservableCollection<Entreprise>)GetValue(listLieuMissionProperty); }
			set { SetValue(listLieuMissionProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listLieuMission.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listLieuMissionProperty =
			DependencyProperty.Register("listLieuMission", typeof(ObservableCollection<Entreprise>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Salarie> listContactMission_Personnel
		{
			get { return (ObservableCollection<Salarie>)GetValue(listContactMission_PersonnelProperty); }
			set { SetValue(listContactMission_PersonnelProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listContactMission_Personnel.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listContactMission_PersonnelProperty =
			DependencyProperty.Register("listContactMission_Personnel", typeof(ObservableCollection<Salarie>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Contact> listContactMission_Client
		{
			get { return (ObservableCollection<Contact>)GetValue(listContactMission_ClientProperty); }
			set { SetValue(listContactMission_ClientProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listContactMission_Client.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listContactMission_ClientProperty =
			DependencyProperty.Register("listContactMission_Client", typeof(ObservableCollection<Contact>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Motif_Mission> listMotif_Mission
		{
			get { return (ObservableCollection<Motif_Mission>)GetValue(listMotif_MissionProperty); }
			set { SetValue(listMotif_MissionProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listMotif_Mission.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listMotif_MissionProperty =
			DependencyProperty.Register("listMotif_Mission", typeof(ObservableCollection<Motif_Mission>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Salarie> listSalarieAbsent
		{
			get { return (ObservableCollection<Salarie>)GetValue(listSalarieAbsentProperty); }
			set { SetValue(listSalarieAbsentProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listSalarieAbsent.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listSalarieAbsentProperty =
			DependencyProperty.Register("listSalarieAbsent", typeof(ObservableCollection<Salarie>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		#endregion

		#region Intérimaire

		public ObservableCollection<Salarie> listSalarie
		{
			get { return (ObservableCollection<Salarie>)GetValue(listSalarieProperty); }
			set { SetValue(listSalarieProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listSalarie.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listSalarieProperty =
			DependencyProperty.Register("listSalarie", typeof(ObservableCollection<Salarie>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Evenement_Remboursement> listTypeDeplacement
		{
			get { return (ObservableCollection<Evenement_Remboursement>)GetValue(listTypeDeplacementProperty); }
			set { SetValue(listTypeDeplacementProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listTypeDeplacement.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listTypeDeplacementProperty =
			DependencyProperty.Register("listTypeDeplacement", typeof(ObservableCollection<Evenement_Remboursement>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Type_Remboursement> listTypeRemboursement
		{
			get { return (ObservableCollection<Type_Remboursement>)GetValue(listTypeRemboursementProperty); }
			set { SetValue(listTypeRemboursementProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listTypeRemboursement.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listTypeRemboursementProperty =
			DependencyProperty.Register("listTypeRemboursement", typeof(ObservableCollection<Type_Remboursement>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));



		#endregion

		#region Equipe

		public ObservableCollection<Commande_Fournisseur> listCommande
		{
			get { return (ObservableCollection<Commande_Fournisseur>)GetValue(listCommandeProperty); }
			set { SetValue(listCommandeProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listCommande.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listCommandeProperty =
			DependencyProperty.Register("listCommande", typeof(ObservableCollection<Commande_Fournisseur>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Piece_Administrative> listPiece_Administrative
		{
			get { return (ObservableCollection<Piece_Administrative>)GetValue(listPiece_AdministrativeProperty); }
			set { SetValue(listPiece_AdministrativeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for listPiece_Administrative.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listPiece_AdministrativeProperty =
			DependencyProperty.Register("listPiece_Administrative", typeof(ObservableCollection<Piece_Administrative>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Salarie> listSalarieTiers
		{
			get { return (ObservableCollection<Salarie>)GetValue(listSalarieTiersProperty); }
			set { SetValue(listSalarieTiersProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listSalarieTiers.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listSalarieTiersProperty =
			DependencyProperty.Register("listSalarieTiers", typeof(ObservableCollection<Salarie>), typeof(OrdreMissionWindow), new PropertyMetadata(null));

		#endregion

		#region Both

		public ObservableCollection<Entreprise> listFournisseur
		{
			get { return (ObservableCollection<Entreprise>)GetValue(listFournisseurProperty); }
			set { SetValue(listFournisseurProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listEntreprise.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listFournisseurProperty =
			DependencyProperty.Register("listFournisseur", typeof(ObservableCollection<Entreprise>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Contact> listContact
		{
			get { return (ObservableCollection<Contact>)GetValue(listContactProperty); }
			set { SetValue(listContactProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listContact.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listContactProperty =
			DependencyProperty.Register("listContact", typeof(ObservableCollection<Contact>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		public ObservableCollection<Qualification> listQualification
		{
			get { return (ObservableCollection<Qualification>)GetValue(listQualificationProperty); }
			set { SetValue(listQualificationProperty, value); }
		}
		// Using a DependencyProperty as the backing store for listQualification.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listQualificationProperty =
			DependencyProperty.Register("listQualification", typeof(ObservableCollection<Qualification>), typeof(OrdreMissionWindow), new UIPropertyMetadata(null));

		#endregion

		#endregion

		#region Constructeur

		public OrdreMissionWindow()
		{
			InitializeComponent();

			//Initialisation des propriétés de dépendances
			this.initialisationPropDependance();

			//Initialisation de la sécurité
			this.initialisationSecurite();

			//Intialisation de la personnalisation utilisateur
			((App)App.Current).personnalisation.initWindows(this);

		}

		#region initialisation

		private void initialisationPropDependance()
		{
			this.listEntrepriseMere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(ent => ent.Nom));
			this.listAffaire = new ObservableCollection<Affaire>(((App)App.Current).mySitaffEntities.Affaire.OrderBy(aff => aff.Numero));
			this.listQualification = new ObservableCollection<Qualification>();
			this.listTypeDeplacement = new ObservableCollection<Evenement_Remboursement>(((App)App.Current).mySitaffEntities.Evenement_Remboursement.OrderBy(eve => eve.Libelle));
			this.listMotif_Mission = new ObservableCollection<Motif_Mission>(((App)App.Current).mySitaffEntities.Motif_Mission.OrderBy(mot => mot.Libelle));
			this.listContactMission_Personnel = new ObservableCollection<Salarie>();
			foreach (Salarie item in ((App)App.Current).mySitaffEntities.Salarie.Where(sal => sal.Personne != null))
			{
				this.listContactMission_Personnel.Add(item);
			}
			this.listContactMission_Personnel = new ObservableCollection<Salarie>(this.listContactMission_Personnel.OrderBy(sal => sal.Personne.fullname));
			this.listDonneurOrdre = new ObservableCollection<Salarie>(this.listContactMission_Personnel.Where(sal => sal.Salarie_Interne != null).OrderBy(sal => sal.Personne.Nom));
			this.listSalarie = new ObservableCollection<Salarie>(this.listContactMission_Personnel.Where(sal => sal.Interimaire != null && sal.Salarie_Interne == null).OrderBy(Sal => Sal.Personne.Nom));
		}

		private void initialisationSecurite()
		{
			//Mise en place des droits sur les boutons et tabs

			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeEntreprisesControl", "Add"))
			{
				this._buttonEquNouvelleEntreprise.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeEntreprisesControl", "Look"))
			{
				this._buttonEquVoirEntreprise.Visibility = Visibility.Collapsed;
			}

			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeContactsControl", "Add"))
			{
				this._buttonEquNouveauContact.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeContactsControl", "Look"))
			{
				this._buttonEquVoirContact.Visibility = Visibility.Collapsed;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl", "Add"))
			{
				this._buttonIntAddDistanceVille.IsEnabled = false;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUDParameters("SitaffRibbon.Windows.ParametresUserControls.ParametreDistanceVilleControl", "Look"))
			{
				this._buttonIntAddDistanceVille.IsEnabled = false;
			}
			if (!((App)App.Current).securite.VerificationDroitActionsCRUD("SitaffRibbon.UserControls.ListeSalarieControl", "Add"))
			{
				this._buttonAddSalarie.IsEnabled = false;
			}
		}

		#endregion

		#endregion

		#region Lecture Seule

		public void lectureSeule()
		{
			//textBox
			_textBoxCommentaire.IsReadOnly = true;
			_textBoxEquDureeHebdo.IsReadOnly = true;
			_textBoxEquMontant.IsReadOnly = true;
			_textBoxEquTauxHoraire.IsReadOnly = true;
			_textBoxIntDistance.IsReadOnly = true;
			_textBoxIntDureeHebdo.IsReadOnly = true;
			_textBoxIntMontant.IsReadOnly = true;
			_textBoxIntMontantDeplacement.IsReadOnly = true;
			_textBoxIntTauxHoraire.IsReadOnly = true;
			_textBoxIntTemps.IsReadOnly = true;
			_textBoxNumeroContrat.IsReadOnly = true;
			_textBoxLibelleMission.IsReadOnly = true;
			_textBoxHeureRDV.IsReadOnly = true;

			//comboBox
			_comboBoxContactMission_Client.IsEnabled = false;
			_comboBoxContactMission_Personnel.IsEnabled = false;
			_comboBoxDonneurOrdre.IsEnabled = false;
			_comboBoxEntrepriseMereDemandeuse.IsEnabled = false;
			_comboBoxEquContact.IsEnabled = false;
			_comboBoxEquEntreprise.IsEnabled = false;
			_comboBoxIntContact.IsEnabled = false;
			_comboBoxIntEntreprise.IsEnabled = false;
			_comboBoxIntNom.IsEnabled = false;
			_comboBoxIntType.IsEnabled = false;
			_comboBoxLieuMission.IsEnabled = false;
			_comboBoxNumeroAffaire.IsEnabled = false;
			_comboBoxMotifMission.IsEnabled = false;
			_comboBoxSalarieAbsent.IsEnabled = false;

			//dataGrid
			_dataGridQualification.IsReadOnly = true;
			_dataGridEquPiecesAdminDemande.IsReadOnly = true;

			//button
			_buttonEquContactNull.IsEnabled = false;
			_buttonEquAjouterQualif.IsEnabled = false;
			_buttonEquAjouterPiece.IsEnabled = false;
			_buttonEquNouveauContact.IsEnabled = false;
			_buttonEquCommande.IsEnabled = false;
			_buttonEquSupprimerQualif.IsEnabled = false;
			_buttonEquSupprimerPiece.IsEnabled = false;
			_buttonEquVoirContact.IsEnabled = false;
			_buttonEquVoirEntreprise.IsEnabled = false;
			_buttonIntContactNull.IsEnabled = false;
			_buttonIntAjouterQualif.IsEnabled = false;
			_buttonIntNouveauContact.IsEnabled = false;
			_buttonIntNouvelleEntreprise.IsEnabled = false;
			_buttonIntSupprimerQualif.IsEnabled = false;
			_buttonIntVoirContact.IsEnabled = false;
			_buttonIntVoirEntreprise.IsEnabled = false;
			_buttonIntNouvelleEntreprise.IsEnabled = false;
			_buttonIntVoirNom.IsEnabled = false;
			_buttonIntDistanceDefaut.IsEnabled = false;
			_buttonIntMontantDefaut.IsEnabled = false;
			_buttonIntTempsDefaut.IsEnabled = false;
			this._buttonAddSalarie.IsEnabled = false;

			//checkBox
			_checkBoxAtelier.IsEnabled = false;
			_checkBoxAutre.IsEnabled = false;
			_checkBoxChantier.IsEnabled = false;
			_checkBoxDelegation.IsEnabled = false;
			_checkBoxGestion.IsEnabled = false;
			_checkBoxIntBareme.IsEnabled = false;
			_checkBoxClient.IsEnabled = false;
			_checkBoxPersonnel.IsEnabled = false;
			_checkBoxRemplacement.IsEnabled = false;

			//DatePicker
			_datePickerDateDebut.IsEnabled = false;
			_datePickerDateFin.IsEnabled = false;

		}

		#endregion

		#region Fenêtre Chargée

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.ReglagesPropDependances();

			this.AfficherMasquerTabControl();

			if (this.SoloLecture)
			{
				this.lectureSeule();
			}

			//Remise du curseur normal
			((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;

			//Recalcul du montant pour une equipe
			if (((Ordre_Mission)this.DataContext).Equipe_Tiers == true)
			{
				this.calculMontantEquipe();
			}
		}

		#endregion

		#region Boutons

		#region Bouton Ok/Annuler

		private void _buttonOk_Click(object sender, RoutedEventArgs e)
		{
			if (this.VerificationChamps())
			{
				if (((Ordre_Mission)this.DataContext).Mission_Tiers1 != null)
				{
					if (((Ordre_Mission)this.DataContext).Mission_Tiers1.Commande_Fournisseur1 != null)
					{
						try
						{
							if (this._comboBoxEquEntreprise.SelectedItem != null)
							{
								((Ordre_Mission)this.DataContext).Mission_Tiers1.Commande_Fournisseur1.Fournisseur1 = ((Entreprise)this._comboBoxEquEntreprise.SelectedItem).Fournisseur;
							}
						}
						catch (Exception)
						{
						}
						try
						{
							if (this._comboBoxNumeroAffaire.SelectedItem != null)
							{
								((Ordre_Mission)this.DataContext).Mission_Tiers1.Commande_Fournisseur1.Affaire1 = (Affaire)this._comboBoxNumeroAffaire.SelectedItem;
							}
						}
						catch (Exception)
						{
						}
                        try
                        {
                            if (this._datePickerDateDebut.SelectedDate != null)
                            {
                                ((Ordre_Mission)this.DataContext).Mission_Tiers1.Commande_Fournisseur1.Date_Commande = this._datePickerDateDebut.SelectedDate;
                            }
                        }
                        catch (Exception)
                        {
                        }
					}
				}				

				this.DialogResult = true;
				this.Close();
			}
			else
			{
				MessageBox.Show("Les données à ajouter ne sont pas conformes.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		private void _buttonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}

		#endregion

		#region ButtonsContact

		private void _buttonIntNouveauContact_Click(object sender, RoutedEventArgs e)
		{
			if (this._comboBoxIntEntreprise.SelectedItem != null)
			{
				ContactWindow contactWindow = new ContactWindow();
				Personne tmp = new Personne();
			
				tmp.Entreprise1 = ((Entreprise)this._comboBoxIntEntreprise.SelectedItem);
				tmp.Contact = new Contact();
				contactWindow.DataContext = tmp;

				//booléen nullable vrai ou faux ou null
				bool? dialogResult = contactWindow.ShowDialog();
				Personne personne = (Personne)contactWindow.DataContext;

				if (dialogResult.HasValue && dialogResult.Value == true)
				{
					this.listContact.Clear();
					if (this._comboBoxIntEntreprise.SelectedItem != null)
					{
						foreach (Personne pers in ((Entreprise)this._comboBoxIntEntreprise.SelectedItem).Personne)
						{
							if (pers.Contact != null)
							{
								this.listContact.Add(pers.Contact);
							}
						}
					}
					this._comboBoxIntContact.SelectedItem = personne.Contact;
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

		}

		private void _buttonEquNouveauContact_Click(object sender, RoutedEventArgs e)
		{
			if (this._comboBoxEquEntreprise.SelectedItem != null)
			{
				ContactWindow contactWindow = new ContactWindow();
				Personne tmp = new Personne();
				tmp.Entreprise1 = ((Entreprise)this._comboBoxEquEntreprise.SelectedItem);
				tmp.Contact = new Contact();
				contactWindow.DataContext = tmp;

				//booléen nullable vrai ou faux ou null
				bool? dialogResult = contactWindow.ShowDialog();
				Personne personne = (Personne)contactWindow.DataContext;

				if (dialogResult.HasValue && dialogResult.Value == true)
				{
					this.listContact.Clear();
					if (this._comboBoxEquEntreprise.SelectedItem != null)
					{
						foreach (Personne pers in ((Entreprise)this._comboBoxEquEntreprise.SelectedItem).Personne.Where(per => per.Contact != null))
						{
							this.listContact.Add(pers.Contact);
						}
					}
					this._comboBoxEquContact.SelectedItem = personne.Contact;
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

		}

		private void _buttonVoirContact_Click(object sender, RoutedEventArgs e)
		{
			if (this._comboBoxEquContact.SelectedItem != null)
			{
				ListeContactsControl listeContactControl = new ListeContactsControl();
				listeContactControl.Look(((Contact)this._comboBoxEquContact.SelectedItem).Personne);
			}
			else if (this._comboBoxIntContact.SelectedItem != null)
			{
				ListeContactsControl listeContactControl = new ListeContactsControl();
				listeContactControl.Look(((Contact)this._comboBoxIntContact.SelectedItem).Personne);
			}
		}

		private void _buttonEquContactNull_Click(object sender, RoutedEventArgs e)
		{
			this._comboBoxEquContact.SelectedItem = null;
		}

		private void _buttonIntContactNull_Click(object sender, RoutedEventArgs e)
		{
			this._comboBoxIntContact.SelectedItem = null;
		}

		#endregion

		#region ButtonsEntreprise

		private void _buttonIntNouvelleEntreprise_Click(object sender, RoutedEventArgs e)
		{
			EntrepriseWindow entrepriseWindow = new EntrepriseWindow();
			Entreprise tmp = new Entreprise();
			tmp.Adresse1 = new Adresse();
			tmp.Client = new Client();
			tmp.Is_Client = false;
			tmp.Fournisseur = new Fournisseur();
			//tmp.Is_Fournisseur = true;
			entrepriseWindow.DataContext = tmp;
			entrepriseWindow.creation = true;

			//booléen nullable vrai ou faux ou null
			bool? dialogResult = entrepriseWindow.ShowDialog();
			Entreprise entreprise = (Entreprise)entrepriseWindow.DataContext;

			if (dialogResult.HasValue && dialogResult.Value == true)
			{
				if (entreprise.Fournisseur != null)
				{
					this.listFournisseur.Add(entreprise);
					this._comboBoxIntEntreprise.SelectedItem = entreprise;
				}
				else
				{
					MessageBox.Show("L'entreprise que vous avez ajouté n'a pas été définie en tant que 'fournisseur', vous ne pourrez donc pas la sélectionner", "Entreprise non fournisseur", MessageBoxButton.OK, MessageBoxImage.Warning);
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

		private void _buttonEquNouvelleEntreprise_Click(object sender, RoutedEventArgs e)
		{
			EntrepriseWindow entrepriseWindow = new EntrepriseWindow();
			Entreprise tmp = new Entreprise();
			tmp.Adresse1 = new Adresse();
			tmp.Client = new Client();
			tmp.Is_Client = false;
			tmp.Fournisseur = new Fournisseur();
			//tmp.Is_Fournisseur = true;
			entrepriseWindow.DataContext = tmp;
			entrepriseWindow.creation = true;

			//booléen nullable vrai ou faux ou null
			bool? dialogResult = entrepriseWindow.ShowDialog();
			Entreprise entreprise = (Entreprise)entrepriseWindow.DataContext;

			if (dialogResult.HasValue && dialogResult.Value == true)
			{
				if (entreprise.Fournisseur != null)
				{
					this.listFournisseur.Add(entreprise);
					this._comboBoxEquEntreprise.SelectedItem = entreprise;
				}
				else
				{
					MessageBox.Show("L'entreprise que vous avez ajouté n'a pas été définie en tant que 'fournisseur', vous ne pourrez donc pas la sélectionner", "Entreprise non fournisseur", MessageBoxButton.OK, MessageBoxImage.Warning);
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

		private void _buttonVoirEntreprise_Click(object sender, RoutedEventArgs e)
		{
			if (this._comboBoxIntEntreprise.SelectedItem != null)
			{
				ListeEntreprisesControl listeEntrepriseControl = new ListeEntreprisesControl();
				listeEntrepriseControl.Look((Entreprise)this._comboBoxIntEntreprise.SelectedItem);
			}
			else if (this._comboBoxEquEntreprise.SelectedItem != null)
			{
				ListeEntreprisesControl listeEntrepriseControl = new ListeEntreprisesControl();
				listeEntrepriseControl.Look((Entreprise)this._comboBoxEquEntreprise.SelectedItem);
			}
		}

		#endregion

		#region ButtonsInterimaire

		#region ButtonSalarie(Nom)

		private void _buttonIntVoirNom_Click(object sender, RoutedEventArgs e)
		{
			if (this._comboBoxIntNom.SelectedItem != null)
			{
				ListeSalarieControl listeSalarieControl = new ListeSalarieControl();
				listeSalarieControl.Look(((Salarie)this._comboBoxIntNom.SelectedItem).Personne);
			}
		}

		#endregion

		#region ButtonIntQualif

		private void _buttonIntAjouter_Click(object sender, RoutedEventArgs e)
		{
			if (this._listBoxIntQualificationOffre.SelectedItem != null && this._listBoxIntQualificationOffre.SelectedItems.Count == 1)
			{
				Qualification tempQualif = new Qualification();
				tempQualif = (Qualification)this._listBoxIntQualificationOffre.SelectedItem;

				Mission_InterimaireQualification tmp = new Mission_InterimaireQualification();
				tmp.Qualification1 = tempQualif;
				tmp.Mission_Interimaire1 = ((Ordre_Mission)this.DataContext).Mission_Interimaire1;

				this.listQualification.Remove((Qualification)this._listBoxIntQualificationOffre.SelectedItem);

			}
			verification_tabItemIntQualification();
		}

		private void _buttonIntSupprimer_Click(object sender, RoutedEventArgs e)
		{
			if (this._listBoxIntQualificationDemande.SelectedItem != null && this._listBoxIntQualificationDemande.SelectedItems.Count == 1)
			{
				this.listQualification.Add((Qualification)((Mission_InterimaireQualification)this._listBoxIntQualificationDemande.SelectedItem).Qualification1);
				((Mission_InterimaireQualification)this._listBoxIntQualificationDemande.SelectedItem).Mission_Interimaire1 = null;
			}
		}

		#endregion

		#region Déplacement

		private void _buttonIntTempsDefaut_Click(object sender, RoutedEventArgs e)
		{
			RecuperationTempsDefaut_Deplacement();
		}

		private void _buttonIntDistanceDefaut_Click(object sender, RoutedEventArgs e)
		{
			RecuperationDistanceDefaut_Deplacement();
		}

		private void _buttonIntMontantDefaut_Click(object sender, RoutedEventArgs e)
		{
			RecuperationMontantDefaut_Deplacement();
		}

		private void _buttonIntAddDistanceVille_Click(object sender, RoutedEventArgs e)
		{
			ParametreDistanceVilleControl p = new ParametreDistanceVilleControl();
			Distance_Ville d = new Distance_Ville();
			if (this._comboBoxLieuMission.SelectedItem != null)
			{
				d.Ville = (Ville)((Entreprise)this._comboBoxLieuMission.SelectedItem).Adresse1.Ville1;
			}
			if (this._comboBoxIntNom.SelectedItem != null)
			{
				d.Ville3 = (Ville)((Salarie)this._comboBoxIntNom.SelectedItem).Personne.Adresse1.Ville1;
			}
			d = p.Add(d);

			if (d != null)
			{
				RecuperationTempsDefaut_Deplacement();				
				RecuperationDistanceDefaut_Deplacement();
				RecuperationMontantDefaut_Deplacement();
			}
		}

		#endregion

		#endregion

		#region ButtonsEquipe

		#region ButtonCommande

		private void _buttonEquCommande_Click(object sender, RoutedEventArgs e)
		{
			if (this._comboBoxNumeroAffaire.SelectedItem != null)
			{
				//Instancie une nouvelle fenêtre
				CommandeOrdreMissionWindow comWindow = new CommandeOrdreMissionWindow();

				if (((Ordre_Mission)this.DataContext).Mission_Tiers1.Commande_Fournisseur1 == null)
				{
					//Nouvelle commande
					Commande_Fournisseur item = new Commande_Fournisseur();
					item.Mission_Tiers.Add(((Ordre_Mission)this.DataContext).Mission_Tiers1);
					if (this._textBoxLibelleMission.Text.Trim() != "")
					{
						Contenu_Commande_Fournisseur temp = new Contenu_Commande_Fournisseur();
						temp.Designation = this._textBoxLibelleMission.Text;
						temp.Quantite = 1;
						item.Contenu_Commande_Fournisseur.Add(temp);
					}
					if (this._datePickerDateDebut.SelectedDate != null)
					{
						item.Date_Commande = this._datePickerDateDebut.SelectedDate;
						item.Date_Livraison_Prevu = this._datePickerDateDebut.SelectedDate;
					}

					//Instancie le nouveau data context
					comWindow.DataContext = item;
				}
				else
				{
					//Instancie le data context déjà exitant
					comWindow.DataContext = ((Ordre_Mission)this.DataContext).Mission_Tiers1.Commande_Fournisseur1;
				}

				//booléen nullable vrai ou faux ou null
				bool? dialogResult = comWindow.ShowDialog();

				//Vérifie le retour
				if (dialogResult.HasValue && dialogResult.Value == true)
				{
					//Si j'appuie sur le bouton Ok, je renvoi l'objet ordreMission dans le datacontext de la fenêtre
					((Ordre_Mission)this.DataContext).Mission_Tiers1.Commande_Fournisseur1 = (Commande_Fournisseur)comWindow.DataContext;
				}
				else if (comWindow.creation == true)
				{
					((Ordre_Mission)this.DataContext).Mission_Tiers1.Commande_Fournisseur1 = null;
				}

				this.verif_buttonEquCommande();
			}
		}

		#endregion

		#region ButtonEquQualif

		private void _buttonEquAjouterQualif_Click(object sender, RoutedEventArgs e)
		{
			if (this._listBoxEquQualificationOffre.SelectedItem != null && this._listBoxEquQualificationOffre.SelectedItems.Count == 1)
			{
				Mission_TiersQualification tmp = new Mission_TiersQualification();
				tmp.Qualification1 = ((Qualification)this._listBoxEquQualificationOffre.SelectedItem);

				((Ordre_Mission)this.DataContext).Mission_Tiers1.Mission_TiersQualification.Add(tmp);
				this.calculMontantEquipe();

			}
		}

		private void _buttonEquSupprimerQualif_Click(object sender, RoutedEventArgs e)
		{
			if (this._dataGridQualification.SelectedItem != null && this._dataGridQualification.SelectedItems.Count == 1)
			{
				try
				{
					((Mission_TiersQualification)this._dataGridQualification.SelectedItem).Mission_Tiers1 = null;
					this.calculMontantEquipe();
				}
				catch (Exception)
				{
					this.calculMontantEquipe();
				}
			}
		}

		#endregion

		#region ButtonEquPiecesAdmin

		private void _buttonEquAjouterPiece_Click(object sender, RoutedEventArgs e)
		{
			if (this._listBoxEquPiecesAdminOffre.SelectedItems.Count >=1)
			{
				while( this._listBoxEquPiecesAdminOffre.SelectedItems.Count >= 1)
				{
					Mission_TiersPiece_Administrative tmp = new Mission_TiersPiece_Administrative();
					tmp.Piece_Administrative1 = ((Piece_Administrative)this._listBoxEquPiecesAdminOffre.SelectedItem);

					((Ordre_Mission)this.DataContext).Mission_Tiers1.Mission_TiersPiece_Administrative.Add(tmp);

					this.listPiece_Administrative.Remove((Piece_Administrative)this._listBoxEquPiecesAdminOffre.SelectedItem);
				}
			}
		}

		private void _buttonEquSupprimerPiece_Click(object sender, RoutedEventArgs e)
		{
			if (this._dataGridEquPiecesAdminDemande.SelectedItem != null && this._dataGridEquPiecesAdminDemande.SelectedItems.Count == 1)
			{
				try
				{
					this.listPiece_Administrative.Add((Piece_Administrative)((Mission_TiersPiece_Administrative)this._dataGridEquPiecesAdminDemande.SelectedItem).Piece_Administrative1);
					((Mission_TiersPiece_Administrative)this._dataGridEquPiecesAdminDemande.SelectedItem).Mission_Tiers1 = null;
				}
				catch (Exception)
				{

				}
			}
		}

		#endregion

		#region AddSalarie
		
		private void _buttonAddSalarie_Click_1(object sender, RoutedEventArgs e)
		{
			SalarieWindow salarieWindow = new SalarieWindow();
			salarieWindow.creation = true;
			Personne tmp = new Personne();
			tmp.Salarie = new Salarie();
			tmp.Salarie.Salarie_Interne = new Salarie_Interne();
			tmp.Salarie.Tiers = new Tiers();
			tmp.Salarie.Interimaire = new Interimaire();
			tmp.Adresse1 = new Adresse();
			tmp.Adresse2 = new Adresse();
			salarieWindow.ListAgences = new ObservableCollection<Agence_Interimaire>(((App)App.Current).mySitaffEntities.Agence_Interimaire.OrderBy(ai => ai.Fournisseur.Entreprise.Libelle));
			salarieWindow.VilleListPerso = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));
			salarieWindow.PaysListPerso = new ObservableCollection<Pays>(((App)App.Current).mySitaffEntities.Pays.OrderBy(pay => pay.Libelle));
			salarieWindow.VilleListPro = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));
			salarieWindow.PaysListPro = new ObservableCollection<Pays>(((App)App.Current).mySitaffEntities.Pays.OrderBy(pay => pay.Libelle));
			salarieWindow.ListCivilite = new ObservableCollection<Civilite>(((App)App.Current).mySitaffEntities.Civilite.OrderBy(civ => civ.Libelle_Long));
			salarieWindow.ListEntreprise = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.Where(ent => ent.Fournisseur != null).Where(ent => ent.Fournisseur.Sous_Traitant != null || ent.Fournisseur.Agence_Interimaire != null).OrderBy(ent => ent.Libelle));
			salarieWindow.ListGroupe = new ObservableCollection<Groupe>(((App)App.Current).mySitaffEntities.Groupe.OrderBy(grp => grp.Libelle));
			salarieWindow.listEntreprise_Mere = new ObservableCollection<Entreprise_Mere>(((App)App.Current).mySitaffEntities.Entreprise_Mere.OrderBy(em => em.Nom));
			salarieWindow.ListContrats = new ObservableCollection<Contrat>(((App)App.Current).mySitaffEntities.Contrat.OrderBy(ct => ct.Libelle));
			salarieWindow.listOutillage = new ObservableCollection<Outillage>(((App)App.Current).mySitaffEntities.Outillage.OrderBy(ou => ou.Libelle));
			salarieWindow.DataContext = tmp;
			salarieWindow.bloquerSalarieInterne();
			salarieWindow.bloquerSalarieInterimaire();
			salarieWindow.bloquerSalarieTiers();

			bool? dialogResult = salarieWindow.ShowDialog();

			if (dialogResult.HasValue && dialogResult.Value == true)
			{
				this.listSalarieTiers.Add(((Personne)salarieWindow.DataContext).Salarie);
			}
			else
			{
				try
				{
					((App)App.Current).mySitaffEntities.Detach((Personne)salarieWindow.DataContext);
				}
				catch (Exception)
				{
				} 
			}
		}

		#endregion

		#endregion

		#endregion

		#region Vérifications

		private bool VerificationChamps()
		{
			bool verif = true;

			if (!verificationGenerale())
			{
				verif = false;
			}
			if (!verificationTabControls())
			{
				verif = false;
			}

			return verif;
		}

		#region Général

		private bool verificationGenerale()
		{
			bool verif = true;

			if (!verif_comboBoxEntrepriseMere())
			{
				verif = false;
			}
			if (!verif_comboBoxNumeroAffaire())
			{
				verif = false;
			}
			if (!verif_comboBoxDonneurOrdre())
			{
				verif = false;
			}
			if (!verif_comboBoxContactMission())
			{
				verif = false;
			}
			if (!verif_comboBoxLieuMission())
			{
				verif = false;
			}
			if (!verif_textBoxHeureRDV())
			{
				verif = false;
			}
			if (!verif_datePickerDateDebut())
			{
				verif = false;
			}
			if (!verif_datePickerDateFin())
			{
				verif = false;
			}
			if (!verif_comboBoxMotifMission())
			{
				verif = false;
			}
			if (!verif_textBoxNumeroContrat())
			{
				verif = false;
			}
			if (!verif_comboBoxSalarieAbsent())
			{
				verif = false;
			}
			
			return verif;
		}

		#region comboBox

		#region Champ _comboBoxEntrepriseMere

		private bool verif_comboBoxEntrepriseMere()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxEntrepriseMereDemandeuse, this._textBlockEntrepriseMereDemandeuse);
		}

		private void _comboBoxEntrepriseMereDemandeuse_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this._comboBoxEntrepriseMereDemandeuse.SelectedItem != null)
			{
				ObservableCollection<Salarie> listToPutSalarie = new ObservableCollection<Salarie>();
				foreach (Salarie item in (this.listContactMission_Personnel.Where(sal => sal.Salarie_Interne != null)))
				{
					//Pour récupérer que les salariés internes
					if (item.Salarie_Interne.Entreprise_Mere1 != null && item.Salarie_Interne.Entreprise_Mere1 == ((Entreprise_Mere)this._comboBoxEntrepriseMereDemandeuse.SelectedItem))
					{
						listToPutSalarie.Add(item);
					}
				}
				
				this.listAffaire = new ObservableCollection<Affaire>(((Entreprise_Mere)this._comboBoxEntrepriseMereDemandeuse.SelectedItem).Affaire);

				if (this._comboBoxDonneurOrdre.SelectedItem != null)
				{
					Salarie tmp = (Salarie)this._comboBoxDonneurOrdre.SelectedItem;

					this.listDonneurOrdre = new ObservableCollection<Salarie>();

					this.listDonneurOrdre = listToPutSalarie;

					this._comboBoxDonneurOrdre.SelectedItem = tmp;
				}
				else
				{
					this.listDonneurOrdre = new ObservableCollection<Salarie>();

					this.listDonneurOrdre = listToPutSalarie;
				}

				if (this._comboBoxNumeroAffaire.SelectedItem == null)
				{
					this._checkBoxChantier.IsChecked = false;
					this._checkBoxAtelier.IsChecked = false;
					this._checkBoxAutre.IsChecked = false;
					this._comboBoxLieuMission.SelectedItem = null;
				}

				if (((Ordre_Mission)this.DataContext).Interimaire != null && ((Ordre_Mission)this.DataContext).Interimaire == true)
				{
					this.listTypeRemboursement = new ObservableCollection<Type_Remboursement>(((App)App.Current).mySitaffEntities.Type_Remboursement.Where(typ => typ.Entreprise_Mere1.Nom == ((Entreprise_Mere)this._comboBoxEntrepriseMereDemandeuse.SelectedItem).Nom));
				}
			}
			verif_comboBoxEntrepriseMere();
		}

		#endregion

		#region Champ _comboBoxNumeroAffaire

		private bool verif_comboBoxNumeroAffaire()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxNumeroAffaire, this._textBlockNumeroAffaire);
		}

		private void _comboBoxNumeroAffaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			verif_comboBoxNumeroAffaire();

			//Sélection de l'entreprise mère
			if (this._comboBoxEntrepriseMereDemandeuse.SelectedItem == null)
			{
				this._comboBoxEntrepriseMereDemandeuse.SelectedItem = ((Affaire)this._comboBoxNumeroAffaire.SelectedItem).Entreprise_Mere1;
			}

			//Sélection du donneur d'ordre
			if (this._comboBoxDonneurOrdre.SelectedItem == null)
			{
				if (((Affaire)this._comboBoxNumeroAffaire.SelectedItem) != null && ((Affaire)this._comboBoxNumeroAffaire.SelectedItem).Salarie != null)
				{
					this._comboBoxDonneurOrdre.SelectedItem = ((Affaire)this._comboBoxNumeroAffaire.SelectedItem).Salarie;
				}
			}

			//Sélection Contact Mission Personnel
			if (this._comboBoxContactMission_Personnel.SelectedItem == null)
			{
				if (((Affaire)this._comboBoxNumeroAffaire.SelectedItem) != null && ((Affaire)this._comboBoxNumeroAffaire.SelectedItem).Affaire_Chef_Chantier != null)
				{
					if (this._datePickerDateDebut.SelectedDate != null)
					{						
						foreach (Affaire_Chef_Chantier item in ((Affaire)this._comboBoxNumeroAffaire.SelectedItem).Affaire_Chef_Chantier)
						{
							if (item.Date_Debut >= this._datePickerDateDebut.SelectedDate)
							{
								this._comboBoxContactMission_Personnel.SelectedItem = item.Salarie1;
							}
						}
					}
					else if (((Affaire)this._comboBoxNumeroAffaire.SelectedItem).Affaire_Chef_Chantier.Count > 0 && ((Affaire_Chef_Chantier)((Affaire)this._comboBoxNumeroAffaire.SelectedItem).Affaire_Chef_Chantier.First()) != null && ((Affaire_Chef_Chantier)((Affaire)this._comboBoxNumeroAffaire.SelectedItem).Affaire_Chef_Chantier.First()).Salarie1 != null)
					{
						this._comboBoxContactMission_Personnel.SelectedItem = ((Affaire_Chef_Chantier)((Affaire)this._comboBoxNumeroAffaire.SelectedItem).Affaire_Chef_Chantier.First()).Salarie1;
					}
				}
			}
		}

		#endregion

		#region Champ _comboBoxDonneurOrdre

		private bool verif_comboBoxDonneurOrdre()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxDonneurOrdre, this._textBlockDonneurOrdre);
		}

		private void _comboBoxDonneurOrdre_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (verif_comboBoxDonneurOrdre() && this._comboBoxEntrepriseMereDemandeuse.SelectedItem == null)
			{
				this._comboBoxEntrepriseMereDemandeuse.SelectedItem = ((Salarie)this._comboBoxDonneurOrdre.SelectedItem).Salarie_Interne.Entreprise_Mere1;
			}
		}

		#endregion

		#region Champ _comboBoxContactMission

		private bool verif_comboBoxContactMission()
		{
			bool verif = true;
			if (this._checkBoxPersonnel.IsChecked == false && this._checkBoxClient.IsChecked == false)
			{
				verif = false;
				((App)App.Current).verifications.MettreComboxEnCouleur(this._comboBoxContactMission_Client, this._textBlockClient, verif);
				((App)App.Current).verifications.MettreComboxEnCouleur(this._comboBoxContactMission_Personnel, this._textBlockPersonnel, verif);
			}
			else
			{
				if (this._checkBoxClient.IsChecked == false)
				{
					verif = ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxContactMission_Client, this._textBlockClient);
					verif = ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxContactMission_Personnel, this._textBlockPersonnel);
				}

				if (this._checkBoxPersonnel.IsChecked == false)
				{
					verif = ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxContactMission_Personnel, this._textBlockPersonnel);
					verif = ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxContactMission_Client, this._textBlockClient);
				}
			}
			return verif;
		}

		private void _comboBoxContactMission_Personnel_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this._comboBoxContactMission_Personnel.SelectedItem != null && this._checkBoxPersonnel.IsChecked == false)
			{
				this._checkBoxPersonnel.IsChecked = true;
			}
			verif_comboBoxContactMission();
		}

		private void _comboBoxContactMission_Client_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this._comboBoxContactMission_Client.SelectedItem != null && this._checkBoxClient.IsChecked == false)
			{
				this._checkBoxClient.IsChecked = true;
			}
			verif_comboBoxContactMission();
		}

		#endregion

		#region Champ _comboBoxLieuMission

		private bool verif_comboBoxLieuMission()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxLieuMission, this._textBlockLieuMission);
		}

		private void _comboBoxLieuMission_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (verif_comboBoxLieuMission())
			{
				this.listContactMission_Client = new ObservableCollection<Contact>();
				if (((Entreprise)this._comboBoxLieuMission.SelectedItem).Personne != null)
				{
					foreach (Personne item in ((Entreprise)this._comboBoxLieuMission.SelectedItem).Personne.Where(per => per.Contact != null))
					{
						this.listContactMission_Client.Add(item.Contact);
					}
				}
			}

		}

		#endregion

		#region Champ _comboBoxMotifMission

		private bool verif_comboBoxMotifMission()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxMotifMission, this._textBlockMotifMission);
		}

		private void _comboBoxMotifMission_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			verif_comboBoxMotifMission();
		}

		#endregion

		#region Champ _comboBoxSalarieAbsent

		private bool verif_comboBoxSalarieAbsent()
		{
			if (this._checkBoxRemplacement.IsChecked == true)
			{
				return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxSalarieAbsent, this._textBlockRemplacement);
			}
			else
			{
				return ((App)App.Current).verifications.ComboBoxSelectionNonObligatoire(this._comboBoxSalarieAbsent, this._textBlockRemplacement);
			}
		}

		private void _comboBoxSalarieAbsent_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			verif_comboBoxSalarieAbsent();
		}

		#endregion

		#endregion

		#region textBox

		#region Champ _textBoxHeureRDV

		private bool verif_textBoxHeureRDV()
		{
			return ((App)App.Current).verifications.TextBoxHeureObligatoire(this._textBoxHeureRDV,this._textBlockHeureRDV);
		}

		private void _textBoxHeureRDV_LostFocus(object sender, RoutedEventArgs e)
		{
			verif_textBoxHeureRDV();
		}

		#endregion

		#region Champ _textBoxNumeroContrat

		private bool verif_textBoxNumeroContrat()
		{
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxNumeroContrat, this._textBlockNumeroContrat);
		}

		private void _textBoxNumeroContrat_TextChanged(object sender, TextChangedEventArgs e)
		{
			verif_textBoxNumeroContrat();
		}

		#endregion

		#endregion

		#region datePicker

		#region Champ _datePickerDateDebut

		private bool verif_datePickerDateDebut()
		{
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateDebut,this._textBlockDateDebut);
		}

		private void _datePickerDateDebut_TextChanged(object sender, TextChangedEventArgs e)
		{
			verif_datePickerDateDebut();
			calculMontantEquipe();
		}

		#endregion

		#region Champ _datePickerDateFin

		private bool verif_datePickerDateFin()
		{
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._datePickerDateFin,this._textBlockDateFin, this._datePickerDateDebut);
		}

		private void _datePickerDateFin_TextChanged(object sender, TextChangedEventArgs e)
		{
			verif_datePickerDateFin();
			calculMontantEquipe();
		}

		#endregion

		#endregion

		#endregion

		#region TabControls

		private bool verificationTabControls()
		{
			bool verif = true;
			if (((Ordre_Mission)this.DataContext).Equipe_Tiers == true)
			{
				if (!verificationTabEquipe())
				{
					verif = false;
				}
			}
			else if (((Ordre_Mission)this.DataContext).Interimaire == true)
			{
				if (!verificationTabInterimaire())
				{
					verif = false;
				}
			}
			else
			{
				verif = false;
			}

			return verif;
		}

		#region TabIntérimaire

		private bool verificationTabInterimaire()
		{
			bool verif = true;

			if (!verification_tabItemIntGeneral())
			{
				verif = false;
			}
			if (!verification_tabItemIntDeplacement())
			{
				verif = false;
			}
			if (!verification_tabItemIntQualification())
			{
				verif = false;
			}

			return verif;
		}

		#region TabItem

		#region TabItemGeneral

		private bool verification_tabItemIntGeneral()
		{
			bool verif = true;

			if (!verif_comboBoxIntEntreprise())
			{
				verif = false;
			}
			if (!verif_comboBoxIntNom())
			{
				verif = false;
			}
			if (!verif_textBoxIntTauxHoraire())
			{
				verif = false;
			}
			if (!verif_textBoxIntDureeHebdo())
			{
				verif = false;
			}
			if (!verif_textBoxIntMontant())
			{
				verif = false;
			}
			if (!verif_checkboxChecked())
			{
				verif = false;
			}

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemIntGeneral, verif);

			return verif;
		}

		#region Champ _comboBoxIntEntreprise

		private bool verif_comboBoxIntEntreprise()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxIntEntreprise, this._textBlockIntEntreprise);
		}

		private void _comboBoxIntEntreprise_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this._comboBoxIntEntreprise.SelectedItem != null && ((Entreprise)this._comboBoxIntEntreprise.SelectedItem).Personne != null)
			{
				//Récupère les contacts de l'entreprise
				this.listContact = new ObservableCollection<Contact>();
				ObservableCollection<Contact> listToPutContact = new ObservableCollection<Contact>();
				foreach (Personne item in ((Entreprise)this._comboBoxIntEntreprise.SelectedItem).Personne.Where(pers => pers.Contact != null).OrderBy(pers => pers.Nom))
				{
					listToPutContact.Add(item.Contact);
				}
				this.listContact = listToPutContact;
			}
			verif_comboBoxIntEntreprise();
		}

		#endregion

		#region Champ _comboBoxIntNom

		private bool verif_comboBoxIntNom()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxIntNom, this._textBlockIntNom);
		}

		private void _comboBoxIntNom_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (verif_comboBoxIntNom())
			{
				this.listQualification = new ObservableCollection<Qualification>();
				foreach (Qualification item in ((Salarie)this._comboBoxIntNom.SelectedItem).Qualification)
				{
					if (((Ordre_Mission)DataContext).Mission_Interimaire1.Mission_InterimaireQualification.Count == 0)
					{
						this.listQualification.Add(item);
					}
					foreach (Mission_InterimaireQualification item2 in ((Ordre_Mission)DataContext).Mission_Interimaire1.Mission_InterimaireQualification)
					{
						if (!item.Equals(item2))
						{
							this.listQualification.Add(item);
						}
					}
				}				
			}
		}

		#endregion

		#region Champ _textBoxIntTauxHoraire

		private bool verif_textBoxIntTauxHoraire()
		{
			return ((App)App.Current).verifications.TextBoxDoubleNonObligatoire(this._textBoxIntTauxHoraire, this._textBlockIntTauxHoraire);
		}

		private void _textBoxIntTauxHoraire_TextChanged(object sender, TextChangedEventArgs e)
		{
			verif_textBoxIntTauxHoraire();
			calculMontantInterimaire();
		}

		#endregion

		#region Champ _textBoxIntDureeHebdo

		private bool verif_textBoxIntDureeHebdo()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxIntDureeHebdo, this._textBlockIntDureeHebdo);
		}

		private void _textBoxIntDureeHebdo_TextChanged(object sender, TextChangedEventArgs e)
		{
			verif_textBoxIntDureeHebdo();
			calculMontantInterimaire();
		}

		#endregion

		#region Champ _textBoxIntMontant

		private bool verif_textBoxIntMontant()
		{
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxIntMontant,this._textBlockIntMontant);
		}

		private void _textBoxIntMontant_TextChanged(object sender, TextChangedEventArgs e)
		{
			verif_textBoxIntMontant();
		}

		#endregion

		#region CheckBox coefficient

		private bool verif_checkboxChecked()
		{
			bool verif = true;

			if (this._checkBoxDelegation.IsChecked == false && this._checkBoxGestion.IsChecked == false)
			{
				verif = false;
			}

			((App)App.Current).verifications.MettreCheckBoxEnCouleur(this._checkBoxGestion, this._textBlockCoefficient, verif);
			((App)App.Current).verifications.MettreCheckBoxEnCouleur(this._checkBoxDelegation, this._textBlockCoefficient, verif);

			return verif;
		}

		#endregion

		#endregion

		#region TabItemDeplacement

		private bool verification_tabItemIntDeplacement()
		{
			bool verif = true;

			if (!verif_comboBoxIntType())
			{
				verif = false;
			}
			if (!verif_textBoxIntDistance())
			{
				verif = false;
			}
			if (!verif_textBoxIntMontantDeplacement())
			{
				verif = false;
			}
			if (!verif_textBoxIntTemps())
			{
				verif = false;
			}

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemIntDeplacement,verif);
			return verif;
		}

		#region Champ _comboBoxIntType

		private bool verif_comboBoxIntType()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxIntType, this._textBlockIntType);
		}

		private void _comboBoxIntType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			verif_comboBoxIntType();
		}

		#endregion

		#region Champ _textBoxIntTemps

		private bool verif_textBoxIntTemps()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxIntTemps, this._textBlockIntTemps);
		}

		private void _textBoxIntTemps_TextChanged(object sender, TextChangedEventArgs e)
		{
			verif_textBoxIntTemps();
		}

		#endregion

		#region Champ _textBoxIntDistance

		private bool verif_textBoxIntDistance()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxIntDistance, this._textBlockIntDistance);
		}

		private void _textBoxIntDistance_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (verif_textBoxIntDistance())
			{
				if (creation == true)
				{
					double val;
					double.TryParse(this._textBoxIntDistance.Text, out val);
					if (val <= 100)
					{
						this._checkBoxIntBareme.IsChecked = true;
					}
				}				
			}			
		}

		#endregion

		#region Champ _textBoxIntMontantDeplacement

		private bool verif_textBoxIntMontantDeplacement()
		{
			return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxIntMontantDeplacement, this._textBlockIntMontantDeplacement);
		}

		private void _textBoxIntMontantDeplacement_TextChanged(object sender, TextChangedEventArgs e)
		{
			verif_textBoxIntMontantDeplacement();
		}

		#endregion

		#endregion

		#region TabItemQualification

		private bool verification_tabItemIntQualification()
		{
			bool verif = true;

			if (this._listBoxIntQualificationDemande.Items.Count == 0)
			{
				verif = false;
			}
			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemIntQualification, verif);

			return verif;
		}

		#endregion

		#endregion

		#endregion

		#region TabEquipe

		private bool verificationTabEquipe()
		{
			bool verif = true;

			if (!verification_tabItemEquGeneral())
			{
				verif = false;
			}
			if (!verification_tabItemEquPiecesAdmin())
			{
				verif = false;
			}
			if (!verification_tabItemEquQualification())
			{
				verif = false;
			}

			return verif;
		}

		#region TabItemGeneral

		private bool verification_tabItemEquGeneral()
		{
			bool verif = true;

			if (!verif_comboBoxEquEntreprise())
			{
				verif = false;
			}
			if (!verif_buttonEquCommande())
			{
				verif = false;
			}
			if (!verif_textBoxEquDureeHebdo())
			{
				verif = false;
			}
			if (!verif_textBoxEquMontant())
			{
				verif = false;
			}
			if (!verif_textBoxEquTauxHoraire())
			{
				verif = false;
			}
			
			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemEquGeneral, verif);

			return verif;
		}

		#region Champ _comboBoxEquEntreprise

		private bool verif_comboBoxEquEntreprise()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxEquEntreprise,this._textBlockEquEntreprise);
		}

		private void _comboBoxEquEntreprise_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ObservableCollection<Contact> listToPutContact = new ObservableCollection<Contact>();
			foreach (Personne item in ((Entreprise)this._comboBoxEquEntreprise.SelectedItem).Personne.Where(pers => pers.Contact != null).OrderBy(pers => pers.Nom))
			{
				listToPutContact.Add(item.Contact);
			}

			this.listContact = listToPutContact;

			ObservableCollection<Commande_Fournisseur> listToPutcomm = new ObservableCollection<Commande_Fournisseur>();
			foreach (Commande_Fournisseur item in ((Entreprise)this._comboBoxEquEntreprise.SelectedItem).Fournisseur.Commande_Fournisseur.Where(com => com != null))
			{
				listToPutcomm.Add(item);
			}

			this.listCommande = listToPutcomm;

			verif_comboBoxEquEntreprise();
		}

		#endregion

		#region Champ _buttonEquCommande

		private bool verif_buttonEquCommande()
		{
			bool verif = true;

			if (((Ordre_Mission)this.DataContext).Mission_Tiers1.Commande_Fournisseur1 == null)
			{
				verif = false;
			}

			((App)App.Current).verifications.MettreBoutonEnCouleur(this._buttonEquCommande, verif);

			return verif;
		}

		#endregion

		#region Champ _textBoxEquTauxHoraire

		private bool verif_textBoxEquTauxHoraire()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxEquTauxHoraire, this._textBlockEquTauxHoraire);
		}

		private void _textBoxEquTauxHoraire_TextChanged(object sender, TextChangedEventArgs e)
		{
			verif_textBoxEquTauxHoraire();
			calculMontantEquipe();
		}

		#endregion

		#region Champ _textBoxEquDureeHebdo

		private bool verif_textBoxEquDureeHebdo()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxEquDureeHebdo, this._textBlockEquDureeHebdo);
		}

		private void _textBoxEquDureeHebdo_TextChanged(object sender, TextChangedEventArgs e)
		{
			verif_textBoxEquDureeHebdo();
			calculMontantEquipe();
		}

		#endregion

		#region Champ _textBoxEquMontant

		private bool verif_textBoxEquMontant()
		{
			return ((App)App.Current).verifications.TextBoxNonObligatoire(this._textBoxEquMontant,this._textBlockEquMontant);
		}

		private void _textBoxEquMontant_TextChanged(object sender, TextChangedEventArgs e)
		{
			verif_textBoxEquMontant();
		}

		#endregion

		#endregion

		#region TabItemQualification

		private bool verification_tabItemEquQualification()
		{
			bool verif = true;

			if (this._dataGridQualification.ItemsSource.OfType<Mission_TiersQualification>().Count() == 0)
			{
				verif = false;
			}

			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemEquQualification, verif);
			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridQualification, verif);

			return verif;
		}

		#endregion

		#region TabItemPiecesAdmin

		private bool verification_tabItemEquPiecesAdmin()
		{
			bool verif = true;

			if (_dataGridEquPiecesAdminDemande.Items.Count == 0)
			{				
				verif = false;
			}

			((App)App.Current).verifications.MettreDataGridEnCouleur(this._dataGridEquPiecesAdminDemande, verif);
			((App)App.Current).verifications.MettreTabItemEnCouleur(this._tabItemEquPiecesAdmin, verif);

			return verif;
		}

		#endregion

		#endregion

		#endregion

		#endregion

		#region Fonctions

		private void AfficherMasquerTabControl()
		{

			//Afficher ou masquer intérimaire
			if (((Ordre_Mission)this.DataContext).Interimaire != null && ((Ordre_Mission)this.DataContext).Interimaire == true)
			{
				this._tabControlInterimaire.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				this._tabControlInterimaire.Visibility = System.Windows.Visibility.Collapsed;
			}
			if (((Ordre_Mission)this.DataContext).Equipe_Tiers != null && ((Ordre_Mission)this.DataContext).Equipe_Tiers == true)
			{
				this._tabControlEquipe.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				this._tabControlEquipe.Visibility = System.Windows.Visibility.Collapsed;
			}
		}

		private void RecuperationMontantDefaut_Deplacement()
		{
			if (this._comboBoxLieuMission.SelectedItem != null && this._comboBoxIntNom.SelectedItem != null)
			{
				Distance_Ville dist_ville = GetDistance_Ville((Entreprise)this._comboBoxLieuMission.SelectedItem, (Salarie)this._comboBoxIntNom.SelectedItem);
				
				if (dist_ville != null && this._datePickerDateDebut.SelectedDate != null && this._datePickerDateFin.SelectedDate != null)
				{

					ObservableCollection<Type_Remboursement> tmp;

					if (this._checkBoxIntBareme.IsChecked == true)
					{
						tmp = new ObservableCollection<Type_Remboursement>(((App)App.Current).mySitaffEntities.Type_Remboursement.Where(typ => typ.Distance_Debut <= dist_ville.Kilometres && typ.Distance_Fin >= dist_ville.Kilometres && typ.Date_Debut <= _datePickerDateDebut.SelectedDate && typ.Date_Fin >= _datePickerDateFin.SelectedDate && typ.Entreprise_Mere1==null));
					}
					else
					{
						tmp = new ObservableCollection<Type_Remboursement>(((App)App.Current).mySitaffEntities.Type_Remboursement.Where(typ => typ.Distance_Debut <= dist_ville.Kilometres && typ.Distance_Fin >= dist_ville.Kilometres && typ.Date_Debut <= _datePickerDateDebut.SelectedDate && typ.Date_Fin >= _datePickerDateFin.SelectedDate && typ.Entreprise_Mere1 != null));
					}
					if (tmp != null && tmp.Count != 0)
					{
						Type_Remboursement type_Remb = tmp.First();
						((Ordre_Mission)this.DataContext).Mission_Interimaire1.Montant_Deplacement = type_Remb.Montant_Deplacement;
						this._comboBoxIntType.SelectedItem = type_Remb.Evenement_Remboursement;
						this._textBoxNoResult.Visibility = Visibility.Collapsed;
						this._buttonIntAddDistanceVille.Visibility = Visibility.Collapsed;
					}
				}
				else
				{
					this._textBoxNoResult.Visibility = Visibility.Visible;
					this._buttonIntAddDistanceVille.Visibility = Visibility.Visible;
				}
			}
			
		}

		private void RecuperationDistanceDefaut_Deplacement()
		{
			if (this._comboBoxLieuMission.SelectedItem != null && this._comboBoxIntNom.SelectedItem != null)
			{
				Distance_Ville dist_ville = GetDistance_Ville((Entreprise)this._comboBoxLieuMission.SelectedItem, (Salarie)this._comboBoxIntNom.SelectedItem);

				if (dist_ville != null)
				{
					((Ordre_Mission)this.DataContext).Mission_Interimaire1.Distance_Deplacement = dist_ville.Kilometres;
					this._textBoxNoResult.Visibility = Visibility.Collapsed;
					this._buttonIntAddDistanceVille.Visibility = Visibility.Collapsed;
				}
				else
				{
					this._textBoxNoResult.Visibility = Visibility.Visible;
					this._buttonIntAddDistanceVille.Visibility = Visibility.Visible;
				}
			}
		}

		private void RecuperationTempsDefaut_Deplacement()
		{
			if (this._comboBoxLieuMission.SelectedItem != null && this._comboBoxIntNom.SelectedItem != null)
			{
				Distance_Ville dist_ville = GetDistance_Ville((Entreprise)this._comboBoxLieuMission.SelectedItem, (Salarie)this._comboBoxIntNom.SelectedItem);

				if (dist_ville != null)
				{
					((Ordre_Mission)this.DataContext).Mission_Interimaire1.Temps_Deplacement = dist_ville.Temps;
					this._textBoxNoResult.Visibility = Visibility.Collapsed;
					this._buttonIntAddDistanceVille.Visibility = Visibility.Collapsed;
				}
				else
				{
					this._textBoxNoResult.Visibility = Visibility.Visible;
					this._buttonIntAddDistanceVille.Visibility = Visibility.Visible;
				}
			}
		}

		private Distance_Ville GetDistance_Ville(Entreprise e, Salarie s)
		{
			Distance_Ville dist_ville = null;

			if (e.Adresse1 != null && s.Personne != null && s.Personne.Adresse1 != null)
			{
				if (e.Adresse1.Ville1 != null && s.Personne.Adresse1.Ville1 != null)
				{
					ObservableCollection<Distance_Ville> list = new ObservableCollection<Distance_Ville>(((App)App.Current).mySitaffEntities.Distance_Ville);

					foreach (Distance_Ville item in ((App)App.Current).mySitaffEntities.Distance_Ville.Where(vil => vil.Ville.Identifiant == e.Adresse1.Ville1.Identifiant && vil.Ville3.Identifiant == s.Personne.Adresse1.Ville1.Identifiant))
					{
						dist_ville = item;
					}
					if (dist_ville == null)
					{
						foreach (Distance_Ville item in ((App)App.Current).mySitaffEntities.Distance_Ville.Where(vil => vil.Ville.Identifiant == s.Personne.Adresse1.Ville1.Identifiant && vil.Ville3.Identifiant == e.Adresse1.Ville1.Identifiant))
						{
							dist_ville = item;
						}
					}
				}
			}

			return dist_ville;
		}

		private void ReglagesPropDependances()
		{
			if (((Ordre_Mission)this.DataContext).Interimaire != null && ((Ordre_Mission)this.DataContext).Interimaire == true)
			{
				this.listFournisseur = new ObservableCollection<Entreprise>();
				ObservableCollection<Agence_Interimaire> toPut = new ObservableCollection<Agence_Interimaire>(((App)App.Current).mySitaffEntities.Agence_Interimaire.Where(age => age.Fournisseur != null));
				foreach (Agence_Interimaire item in toPut)
				{
					if (item.Fournisseur.Entreprise != null)
					{
						this.listFournisseur.Add(item.Fournisseur.Entreprise);						
					}
				}
				if (this.listFournisseur != null)
				{
					this.listFournisseur.OrderBy(ent => ent.Libelle);
				}
			}
			if (((Ordre_Mission)this.DataContext).Equipe_Tiers != null && ((Ordre_Mission)this.DataContext).Equipe_Tiers == true)
			{

				this.listQualification = new ObservableCollection<Qualification>(((App)App.Current).mySitaffEntities.Qualification.OrderBy(qua => qua.Libelle));
				this.listPiece_Administrative = new ObservableCollection<Piece_Administrative>(((App)App.Current).mySitaffEntities.Piece_Administrative.OrderBy(pie => pie.Libelle));
				foreach (Mission_TiersPiece_Administrative item in ((Ordre_Mission)DataContext).Mission_Tiers1.Mission_TiersPiece_Administrative)
				{
					this.listPiece_Administrative.Remove(item.Piece_Administrative1);
				}


				this.listFournisseur = new ObservableCollection<Entreprise>();
				ObservableCollection<Sous_Traitant> toPutBis = new ObservableCollection<Sous_Traitant>(((App)App.Current).mySitaffEntities.Sous_Traitant.Where(sou => sou.Fournisseur != null));
				foreach (Sous_Traitant item in toPutBis)
				{
					if (item.Fournisseur.Entreprise != null)
					{
						this.listFournisseur.Add(item.Fournisseur.Entreprise);
					}
				}
				if (this.listFournisseur != null)
				{
					this.listFournisseur.OrderBy(ent => ent.Libelle);
				}

				this.listSalarieTiers = new ObservableCollection<Salarie>(); 
				this.listSalarieTiers = this.listContactMission_Personnel;
			}
		}


		#region Calculs Montant

		private void calculMontantInterimaire()
		{
			if (verif_textBoxIntTauxHoraire() && verif_textBoxIntDureeHebdo())
			{
				double dureeHebdo;
				double txHoraire;
				double.TryParse(this._textBoxIntDureeHebdo.Text, out dureeHebdo);
				double.TryParse(this._textBoxIntTauxHoraire.Text, out txHoraire);

				((Ordre_Mission)this.DataContext).Mission_Interimaire1.Montant = dureeHebdo * txHoraire;
			}
			else
			{
				((Ordre_Mission)this.DataContext).Mission_Interimaire1.Montant = 0.0;
			}
		}

		private void calculMontantEquipe()
		{
			if (verif_textBoxEquDureeHebdo() && verif_textBoxEquTauxHoraire() && this._dataGridQualification.Items.Count > 0)
			{
				double dureeHebdo;
				double txHoraire;
				double.TryParse(this._textBoxEquDureeHebdo.Text, out dureeHebdo);
				double.TryParse(this._textBoxEquTauxHoraire.Text, out txHoraire);

				((Ordre_Mission)this.DataContext).Mission_Tiers1.Montant = dureeHebdo * txHoraire * this._dataGridQualification.Items.Count;

				if (this.verif_datePickerDateDebut() && this.verif_datePickerDateFin())
				{
					((Ordre_Mission)this.DataContext).Mission_Tiers1.Montant = ((Ordre_Mission)this.DataContext).Mission_Tiers1.Montant * getDays((DateTime)this._datePickerDateDebut.SelectedDate, (DateTime)this._datePickerDateFin.SelectedDate);
				}				
			}
			else
			{
				((Ordre_Mission)this.DataContext).Mission_Tiers1.Montant = 0.0;
			}
		}

		private double getDays(DateTime dateDebut, DateTime dateFin)
		{
			double nbJoursTotal = 0;

			Collection<DateTime> tempListDay = new Collection<DateTime>();
			DateTime tempDate = dateDebut;
			while (tempDate <= dateFin)
			{
				tempListDay.Add(tempDate);
				tempDate = tempDate.AddDays(1);
			}


			ObservableCollection<JourFerie> listJoursFeries = new ObservableCollection<JourFerie>(((App)App.Current).mySitaffEntities.JourFerie.Where(dat => dat.Date_Fin >= dateDebut && dat.Date_Fin <= dateFin));

			foreach (DateTime item in tempListDay)
			{
				if (item.DayOfWeek.ToString() == "Monday" || item.DayOfWeek.ToString() == "Tuesday" || item.DayOfWeek.ToString() == "Wednesday" || item.DayOfWeek.ToString() == "Thursday" || item.DayOfWeek.ToString() == "Friday")
				{
					nbJoursTotal++;

					foreach (JourFerie item2 in listJoursFeries)
					{
						if (item2.Date_Fin == item)
						{
							nbJoursTotal--;
						}
					}
				}
			}
			return nbJoursTotal;
		}

		#endregion

		#endregion

		#region Evenements

		#region CheckBox

		#region CheckBoxDelegation/Gestion

		private void _checkBoxDelegation_Checked(object sender, RoutedEventArgs e)
		{
			this._checkBoxGestion.IsChecked = false;
			verif_checkboxChecked();
			if (this._comboBoxIntEntreprise.SelectedItem != null && ((Entreprise)this._comboBoxIntEntreprise.SelectedItem).Fournisseur.Agence_Interimaire != null && ((Entreprise)this._comboBoxIntEntreprise.SelectedItem).Fournisseur.Agence_Interimaire.Coef_Delegation != null)
			{
				this._textBlockInfoCoefDelegation.Text = " Valeur : " + ((Entreprise)this._comboBoxIntEntreprise.SelectedItem).Fournisseur.Agence_Interimaire.Coef_Delegation;
				this._textBlockInfoCoefGestion.Text = " Valeur : 0";
			}
			else
			{
				this._textBlockInfoCoefDelegation.Text = " Valeur : 0";
				this._textBlockInfoCoefGestion.Text = " Valeur : 0";
			}
		}

		private void _checkBoxGestion_Checked(object sender, RoutedEventArgs e)
		{
			this._checkBoxDelegation.IsChecked = false;
			verif_checkboxChecked();
			if (this._comboBoxIntEntreprise.SelectedItem != null && ((Entreprise)this._comboBoxIntEntreprise.SelectedItem).Fournisseur.Agence_Interimaire != null && ((Entreprise)this._comboBoxIntEntreprise.SelectedItem).Fournisseur.Agence_Interimaire.Coef_Gestion != null)
			{
				this._textBlockInfoCoefGestion.Text = " Valeur : " + ((Entreprise)this._comboBoxIntEntreprise.SelectedItem).Fournisseur.Agence_Interimaire.Coef_Gestion;
				this._textBlockInfoCoefDelegation.Text = " Valeur : 0";
			}
			else
			{
				this._textBlockInfoCoefGestion.Text = " Valeur : 0";
				this._textBlockInfoCoefDelegation.Text = " Valeur : 0";
			}
		}

		private void _checkBoxDelegation_Unchecked(object sender, RoutedEventArgs e)
		{
			if (this._checkBoxGestion.IsChecked == false)
			{
				this._checkBoxDelegation.IsChecked = true;
			}
		}

		private void _checkBoxGestion_Unchecked(object sender, RoutedEventArgs e)
		{
			if (this._checkBoxDelegation.IsChecked == false)
			{
				this._checkBoxGestion.IsChecked = true;
			}
		}

		#endregion

		#region CheckBoxLieuMission

		private void _checkBoxChantier_Checked(object sender, RoutedEventArgs e)
		{
			this._checkBoxAtelier.IsChecked = false;
			this._checkBoxAutre.IsChecked = false;
			if (this._comboBoxNumeroAffaire.SelectedItem != null)
			{
				this.listLieuMission = new ObservableCollection<Entreprise>();
				if (((Affaire)_comboBoxNumeroAffaire.SelectedItem).Versions != null && ((Affaire)_comboBoxNumeroAffaire.SelectedItem).Versions.Count > 0)
				{
					try
					{
						Versions tmpVersion = ((Affaire)_comboBoxNumeroAffaire.SelectedItem).Versions.First();

						this.listLieuMission.Add(tmpVersion.Devis1.Client1.Entreprise);
						this._comboBoxLieuMission.SelectedItem = tmpVersion.Devis1.Client1.Entreprise;
					}
					catch (Exception)
					{
						//Bug de la fonction First
					}
				}
			}
		}

		private void _checkBoxAtelier_Checked(object sender, RoutedEventArgs e)
		{
			this._checkBoxAutre.IsChecked = false;
			this._checkBoxChantier.IsChecked = false;

			this.listLieuMission = new ObservableCollection<Entreprise>();

			foreach (Entreprise_Mere em in this.listEntrepriseMere)
			{
				bool test = false;
				foreach (Entreprise ent in this.listLieuMission)
				{
					if (ent.Identifiant == em.Entreprise1.Identifiant)
					{
						test = true;
					}
				}
				if (test == false)
				{
					this.listLieuMission.Add(em.Entreprise1);
				}
			}
			this._comboBoxLieuMission.SelectedItem = null;
		}

		private void _checkBoxAutre_Checked(object sender, RoutedEventArgs e)
		{
			this._checkBoxChantier.IsChecked = false;
			this._checkBoxAtelier.IsChecked = false;

			this.listLieuMission = new ObservableCollection<Entreprise>(((App)App.Current).mySitaffEntities.Entreprise.OrderBy(ent => ent.Libelle));

		}

		#endregion

		#region CheckBoxContactMission

		private void _checkBoxClient_Checked(object sender, RoutedEventArgs e)
		{
			this._checkBoxPersonnel.IsChecked = false;
			this._comboBoxContactMission_Personnel.SelectedItem = null;
			this._comboBoxContactMission_Personnel.IsEnabled = false;
			this._comboBoxContactMission_Client.IsEnabled = true;
			verif_comboBoxContactMission();
		}

		private void _checkBoxPersonnel_Checked(object sender, RoutedEventArgs e)
		{
			this._checkBoxClient.IsChecked = false;
			this._comboBoxContactMission_Client.SelectedItem = null;
			this._comboBoxContactMission_Personnel.IsEnabled = true;
			this._comboBoxContactMission_Client.IsEnabled = false;
			verif_comboBoxContactMission();
		}

		private void _checkBoxClient_Unchecked(object sender, RoutedEventArgs e)
		{
			if (this._checkBoxPersonnel.IsChecked == false)
			{
				this._checkBoxClient.IsChecked = true;
			}
		}

		private void _checkBoxPersonnel_Unchecked(object sender, RoutedEventArgs e)
		{
			if (this._checkBoxClient.IsChecked == false)
			{
				this._checkBoxPersonnel.IsChecked = true;
			}
		}

		#endregion

		#region CheckBox Remplacement

		private void _checkBoxRemplacement_Checked(object sender, RoutedEventArgs e)
		{
			this._comboBoxSalarieAbsent.Visibility = Visibility.Visible;
			this.listSalarieAbsent = this.listDonneurOrdre;
			if (this._datePickerDateDebut.SelectedDate != null)
			{
				ObservableCollection<Salarie> listToPutAbs = new ObservableCollection<Salarie>();
				foreach (Salarie item in this.listSalarieAbsent)
				{
					if (item.Conge != null)
					{
						foreach (Conge item2 in item.Conge)
						{
							if (item2.Accepte == true && item2.Date_Debut >= this._datePickerDateDebut.SelectedDate)
							{
								listToPutAbs.Add(item);
							}
							else if (item2.Accepte == true && item2.Date_Debut <= this._datePickerDateDebut.SelectedDate)
							{
								listToPutAbs.Add(item);
							}
						}
					}
				}
				this.listSalarieAbsent = listToPutAbs;
			}
		}

		private void _checkBoxRemplacement_Unchecked(object sender, RoutedEventArgs e)
		{
			this._comboBoxSalarieAbsent.Visibility = Visibility.Collapsed;
			this._comboBoxSalarieAbsent.SelectedItem = null;
		}

		#endregion

		#region Checkbox Accoss

		private void _checkBoxIntBareme_Checked(object sender, RoutedEventArgs e)
		{
			
		}

		#endregion

		#endregion

		#region DatePicker

		private void _datePickerDateDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this._checkBoxRemplacement.IsChecked == true)
			{
				ObservableCollection<Salarie> listToPutAbs = new ObservableCollection<Salarie>();
				foreach (Salarie item in this.listSalarieAbsent)
				{
					if (item.Conge != null)
					{
						foreach (Conge item2 in item.Conge)
						{
							if (item2.Accepte == true && item2.Date_Debut == this._datePickerDateDebut.SelectedDate)
							{
								listToPutAbs.Add(item);
							}
						}
					}
				}
				this.listSalarieAbsent = listToPutAbs;
			}
			this.verif_datePickerDateDebut();
		}

		private void _datePickerDateFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			this.verif_datePickerDateFin();
		}

		#endregion

		#region KeyUp

		private void _textBox_KeyUp(object sender, KeyEventArgs e)
		{
			ReglageDecimales reg = new ReglageDecimales();
			reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
		}

		#endregion

		#endregion

	}
}
