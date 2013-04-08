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

namespace SitaffRibbon.Windows.ParametresWindows
{
    /// <summary>
    /// Logique d'interaction pour ExerciceWindow.xaml
    /// </summary>
    public partial class ExerciceWindow : Window
    {
        #region Attributs

        public bool soloLecture = false;

        #endregion

        #region constructeur

        public ExerciceWindow()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this._DatePickerExerciceDebut.Focus();
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
                bool verif = true;
                foreach (Exercice item in ((App)App.Current).mySitaffEntities.Exercice)
                {

                    if (((Exercice)this.DataContext).Identifiant == item.Identifiant)
                    {
                        if (((Exercice)this.DataContext).Date_Debut != item.Date_Debut)
                        {
                            verif = ValidOk();
                        }
                        if (((Exercice)this.DataContext).Date_Fin != item.Date_Fin && verif == true)
                        {
                            verif = ValidOk();
                        }
                    }
                    else
                    {
                        verif = ValidOk();
                    }
                }
                if (verif == true)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Un exercice est déjà présent pendant ou durant cette periode", "Doublon d'exercice", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
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

        #region Verifications

        private bool VerificationChamps()
        {
            bool verif = true;

            if (!Verif_DatePickerExerciceDebut())
            {
                verif = false;
            }
            if (!Verif_DatePickerExerciceFin())
            {
                verif = false;
            }
            
            return verif;
        }

        #region Champs Date de debut d'exercice
        private bool Verif_DatePickerExerciceDebut()
        {
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._DatePickerExerciceDebut, this._TextBlockExerciceDebut);
        }

        private void _DatePickerExerciceDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerExerciceDebut();
        }

        #endregion

        #region Champs Date de fin d'exercice
        private bool Verif_DatePickerExerciceFin()
        {
			return ((App)App.Current).verifications.DatePickerSelectionObligatoire(this._DatePickerExerciceFin, this._TextBlockExerciceFin, this._DatePickerExerciceDebut);
        }

        private void _DatePickerExerciceFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Verif_DatePickerExerciceFin();
        }

        #endregion

        #region Verif Date Bouton Ok

        private bool ValidOk()
        {
            bool verif = false;

            ObservableCollection<Exercice> listValid = new ObservableCollection<Exercice>(((App)App.Current).mySitaffEntities.Exercice.Where( ex => ex.Date_Debut == this._DatePickerExerciceDebut.SelectedDate && ex.Date_Fin == this._DatePickerExerciceFin.SelectedDate && ex.Identifiant != ((Exercice)this.DataContext).Identifiant));

            if (listValid.Count == 0)
            {
                verif = true;
            }

            foreach (Exercice item in listValid)
            {
                if (int.Parse(this._DatePickerExerciceDebut.Text.Substring(0 , 2)) < int.Parse(item.Date_Debut.ToString().Substring(0 , 2)) && int.Parse(this._DatePickerExerciceFin.SelectedDate.ToString().Substring(0 , 2)) < int.Parse(item.Date_Fin.ToString().Substring(0 , 2)))
                {
                    verif = true;
                }

                if (int.Parse(this._DatePickerExerciceDebut.Text.Substring(0, 2)) > int.Parse(item.Date_Fin.ToString().Substring(0, 2)) && int.Parse(this._DatePickerExerciceFin.Text.Substring(0, 2)) > int.Parse(item.Date_Fin.ToString().Substring(0, 2)))
                {
                    verif = true;
                }
                if (int.Parse(this._DatePickerExerciceDebut.Text.Substring(0, 2)) == int.Parse(item.Date_Debut.ToString().Substring(0, 2)) && int.Parse(this._DatePickerExerciceFin.Text.Substring(0, 2)) == int.Parse(item.Date_Debut.ToString().Substring(0, 2)))
                {
                    if (int.Parse(this._DatePickerExerciceFin.Text.Substring(3, 2)) < int.Parse(item.Date_Debut.ToString().Substring(3, 2)))
                    {

                        verif = true;
                    }
                }
                if (int.Parse(this._DatePickerExerciceDebut.Text.Substring(0, 2)) == int.Parse(item.Date_Fin.ToString().Substring(0, 2)) && int.Parse(this._DatePickerExerciceFin.Text.Substring(0, 2)) == int.Parse(item.Date_Fin.ToString().Substring(0, 2)))
                {
                    if (int.Parse(item.Date_Fin.ToString().Substring(3, 2)) < int.Parse(this._DatePickerExerciceDebut.Text.Substring(3, 2)))
                    {
                        verif = true;
                    }
                }
                if (int.Parse(this._DatePickerExerciceDebut.Text.Substring(0, 2)) == int.Parse(item.Date_Fin.ToString().Substring(0, 2)) && int.Parse(this._DatePickerExerciceFin.Text.Substring(0, 2)) > int.Parse(item.Date_Fin.ToString().Substring(0, 2)))
                {
                    if (int.Parse(item.Date_Fin.ToString().Substring(3, 2)) < int.Parse(this._DatePickerExerciceDebut.Text.Substring(3, 2)))
                    {
                        verif = true;
                    }
                }
                if (int.Parse(this._DatePickerExerciceDebut.Text.Substring(0, 2)) < int.Parse(item.Date_Debut.ToString().Substring(0, 2)) && int.Parse(this._DatePickerExerciceFin.Text.Substring(0, 2)) == int.Parse(item.Date_Debut.ToString().Substring(0, 2)))
                {
                    if (int.Parse(item.Date_Debut.ToString().Substring(3, 2)) > int.Parse(this._DatePickerExerciceFin.Text.Substring(3, 2)))
                    {
                        verif = true;
                    }
                }
            }

            return verif;
        }

        #endregion

        #endregion

        #region Lecture seule
        //Passe tous les composant en lecture seule
        public void lectureSeule()
        {
            this._DatePickerExerciceDebut.IsEnabled = false;
            this._DatePickerExerciceFin.IsEnabled = false;
        }
        #endregion

        #region fenetre chargé

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._theMainWindow.Cursor = ((App)App.Current)._mainCursor;
        }

        #endregion
    }
}
