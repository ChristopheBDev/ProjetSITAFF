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

namespace SitaffRibbon.Windows.ParametresWindows
{
    /// <summary>
    /// Logique d'interaction pour DistanceVilleWindow.xaml
    /// </summary>
    public partial class DistanceVilleWindow : Window
	{

		#region Attributs

		public bool soloLecture = false;

		#endregion

		#region Propriétés de dépendances

		public ObservableCollection<Ville> listVille
		{
			get { return (ObservableCollection<Ville>)GetValue(listVille1Property); }
			set { SetValue(listVille1Property, value); }
		}
		// Using a DependencyProperty as the backing store for listVille1.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty listVille1Property =
			DependencyProperty.Register("listVille1", typeof(ObservableCollection<Ville>), typeof(DistanceVilleWindow), new UIPropertyMetadata(null));

		#endregion

		#region Constructeur

		public DistanceVilleWindow()
		{
			InitializeComponent();

			InitialisationPropDep();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._textBoxKm.Focus();

		}

		#region Initialisation

		private void InitialisationPropDep()
		{
			this.listVille = new ObservableCollection<Ville>(((App)App.Current).mySitaffEntities.Ville.OrderBy(vil => vil.Libelle));
		}

		#endregion

		#endregion

		#region Lecture seule

		public void lectureSeule()
		{
			this._comboBoxVille1.IsReadOnly = false;
            this._comboBoxVille2.IsReadOnly = false;
            this._textBoxKm.IsReadOnly = false;
            this._textBoxTemps.IsReadOnly = false;
		}

		#endregion

		#region Fenêtre Chargée

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (this.soloLecture)
			{
				this.lectureSeule();
			}

			//Remise du curseur normal
			((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;

		}

		#endregion

		#region Boutons

		#region Bouton Ok/Annuler

		private void _buttonOk_Click(object sender, RoutedEventArgs e)
		{
			if (this.VerificationChamps())
			{
				this.DialogResult = true;
				this.Close();
			}
		}

		private void _buttonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}

		#endregion

		#endregion

		#region Vérifications

		private bool VerificationChamps()
		{
			bool verif = true;

			if (!Verif_ComboBoxVille1())
			{
				verif = false;
			}
			if (!Verif_ComboBoxVille2())
			{
				verif = false;
			}
			if (!Verif_TextBoxKm())
			{
				verif = false;
			}
			if (!Verif_TextBoxTemps())
			{
				verif = false;
			}

			return verif;
		}

		#region Champ Ville1

		private bool Verif_ComboBoxVille1()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxVille1, this._textBlockVille1);
		}

		private void _comboBoxVille1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Verif_ComboBoxVille1();
		}

		#endregion

		#region Champ Ville2

		private bool Verif_ComboBoxVille2()
		{
			return ((App)App.Current).verifications.ComboBoxSelectionObligatoire(this._comboBoxVille2, this._textBlockVille2);
		}

		private void _comboBoxVille2_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Verif_ComboBoxVille2();
		}

		#endregion

		#region Champ Km

		private bool Verif_TextBoxKm()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxKm, this._textBlockKm);
		}

		private void _textBoxKm_TextChanged(object sender, TextChangedEventArgs e)
		{
			Verif_TextBoxKm();
		}

		#endregion

		#region Champ Temps

		private bool Verif_TextBoxTemps()
		{
			return ((App)App.Current).verifications.TextBoxDoubleObligatoire(this._textBoxTemps, this._textBlockTemps);
		}

		private void _textBoxTemps_TextChanged(object sender, TextChangedEventArgs e)
		{
			Verif_TextBoxTemps();
		}

		#endregion

        private void _textBoxTemps_KeyUp(object sender, KeyEventArgs e)
        {
            ReglageDecimales reg = new ReglageDecimales();
            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
        }

		#endregion

	}
}
