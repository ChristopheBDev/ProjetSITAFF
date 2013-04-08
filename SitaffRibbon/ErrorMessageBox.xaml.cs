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

namespace SitaffRibbon
{
    /// <summary>
    /// Logique d'interaction pour ErrorMessageBox.xaml
    /// </summary>
    public partial class ErrorMessageBox : Window
    {

        #region Attributs

        // le texte qui sera affiché
        private string _Text;

        // les détails qui seront affichés
        private string _Details;

        // le titre
        private string _Title;

        //Détail affiché ?
        private bool hide = true;

        #endregion

        public ErrorMessageBox(string text, string details, string title)
        {
            InitializeComponent();

            this._Text = text;
            this._Details = details;
            this._Title = title;

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);
        }

        #region Fenêtre chargée

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this._textBlockError.Text = this._Text;
            this._textBlockException.Text = this._Details;
            this.Title = this._Title;
        }

        #endregion

        #region Fonctions

        /// <summary>
        /// Affiche une ErrorMessageBox avec le contenu du champ "details" dans une fenêtre pouvant être ouverte/fermée.
        /// </summary>
        /// <param name="text">Texte principal du message d'erreur.</param>
        /// <param name="details">Contenu du champ "détails" du message d'erreur.</param>
        /// <param name="title">Titre de la fenêtre du message d'erreur.</param>
        public static void Show(string text, string details, string title)
        {
            // on vérifie les paramètres
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }
            if (string.IsNullOrEmpty(details))
            {
                throw new ArgumentNullException("details");
            }
            if (string.IsNullOrEmpty(title))
            {
                title = "Erreur !";
            }

            // on crée une ErrorMessageBox et on l'affiche
            ErrorMessageBox errorMessageBox = new ErrorMessageBox(text, details, title);
            errorMessageBox.ShowDialog();
        }

        #endregion

        #region Boutons

        private void _buttonDetail_Click(object sender, RoutedEventArgs e)
        {
            if (this.hide)
            {
                this._textBlockException.Height = double.NaN;                
                this.hide = false;
            }
            else
            {
                this._textBlockException.Height = 0;
                this.hide = true;
            }
            this.SizeToContent = SizeToContent.Manual;
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private void _buttonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
