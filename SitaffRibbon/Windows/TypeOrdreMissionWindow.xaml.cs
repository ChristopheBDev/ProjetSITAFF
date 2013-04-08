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

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour TypeOrdreMissionWindow.xaml
    /// </summary>
    public partial class TypeOrdreMissionWindow : Window
    {
        
		#region Attributs

		public bool interimaire;
		public bool equipe;

		#endregion

		#region Constructeur

		public TypeOrdreMissionWindow()
		{
			InitializeComponent();

			//Intialisation de la personnalisation utilisateur
			((App)App.Current).personnalisation.initWindows(this);
		}

		#endregion

		#region Fenêtre Chargée

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this._checkBoxInterimaire.IsChecked = true;
		}

		#endregion

		#region Boutons

		private void _buttonAnnuler_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}

		private void _buttonOK_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			this.Close();
		}

		#endregion

		#region Evenements

		private void _checkBoxEquipe_Checked(object sender, RoutedEventArgs e)
		{
			this._checkBoxInterimaire.IsChecked = false;
			this.equipe = true;
			this.interimaire = false;
		}

		private void _checkBoxEquipe_Unchecked(object sender, RoutedEventArgs e)
		{
			if (this._checkBoxInterimaire.IsChecked == false)
			{
				this._checkBoxEquipe.IsChecked = true;
			}
		}

		private void _checkBoxInterimaire_Checked(object sender, RoutedEventArgs e)
		{
			this._checkBoxEquipe.IsChecked = false;
			this.interimaire = true;
			this.equipe = false;
		}

		private void _checkBoxInterimaire_Unchecked(object sender, RoutedEventArgs e)
		{
			if (this._checkBoxEquipe.IsChecked == false)
			{
				this._checkBoxInterimaire.IsChecked = true;
			}
		}

		#endregion
	}
}
