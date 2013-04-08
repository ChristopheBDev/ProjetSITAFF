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
using Microsoft.Win32;
using System.Deployment.Application;
using System.Threading;
using SitaffRibbon.Classes;
using System.Windows.Threading;

namespace SitaffRibbon
{
    /// <summary>
    /// Logique d'interaction pour LogOnWindow.xaml
    /// </summary>
    public partial class LogOnWindow : Window, IDisposable
    {
        Timer myTimer;
        double _positionAnimation = 0;
        bool sensDroite = true;

        public LogOnWindow()
        {
            InitializeComponent();

            this.startThread();

            ((App)App.Current)._mainCursor = this.Cursor;
            ((App)App.Current).personnalisation.InitLogScreen(this);
            //Je récupère les couleurs car on ne peut les faire en automatique pour TOUS les usercontrols
            //this.colorToDontMove1.Color = new SolidColorBrush(((App)App.Current).personnalisation.ColorFixe).Color;
            this.colorToDontMove1.Color = ((App)App.Current).personnalisation.ColorFixe;
            this.colorToDontMove2.Color = ((App)App.Current).personnalisation.ColorFixe;
            this.colorToMove.Color = ((App)App.Current).personnalisation.ColorMove;

            this._textblockBienvenue.FontSize = this.FontSize + 3;

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
                regVersion.SetValue("UserName", "");
                this._textBoxUserName.Focus();
            }
            else
            {
                this._textBoxUserName.Text = (String)regVersion.GetValue("UserName", "");
                this._textBoxPassword.Focus();
            }
            regVersion.Close();

            this._textblockVersion.Text += "";

            if (ApplicationDeployment.IsNetworkDeployed) //Car ne fonctionne pas en mode debug
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                this._textblockVersion.Text += ad.CurrentVersion.ToString();
            }
            else
            {
                this._textblockVersion.Text += "mode débug, pas de version ...";
            }

        }

        private void startThread()
        {
            myTimer = new System.Threading.Timer(new TimerCallback(this.styleThread), null, 0, 20);
        }

        private void styleThread(Object stateInfo)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => this.moveAnimation()));
        }

        public void moveAnimation()
        {
            if (sensDroite)
            {
                this._positionAnimation = this._positionAnimation + 0.01;
                this.colorToMove.Offset = this._positionAnimation;
            }
            else
            {
                this._positionAnimation = this._positionAnimation - 0.01;
                this.colorToMove.Offset = this._positionAnimation;
            }
            if (this._positionAnimation >= 0.99)
            {
                this.sensDroite = false;
            }
            if (this._positionAnimation <= 0.01)
            {
                this.sensDroite = true;
            }
        }

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (this.VerificationChamps())
            {
                if (this.verifUserExist())
                {
                    RegistryKey regVersion;
                    regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
                    regVersion.SetValue("UserName", this._textBoxUserName.Text);
                    regVersion.Close();

                    ((App)App.Current)._theMainWindow = new MainWindow();
                    ((App)App.Current)._theMainWindow.Title = "Sitaff - Connecté sous " + ((App)App.Current)._connectedUser.Salarie_Interne1.Salarie.Personne.Nom + " " + ((App)App.Current)._connectedUser.Salarie_Interne1.Salarie.Personne.Prenom;
                    ((App)App.Current)._actions = new Actions();                    
                    ((App)App.Current)._actions.Date_Connexion_Debut = DateTime.Now;
                    this.Cursor = ((App)App.Current)._mainCursor;                    
                    ((App)App.Current)._theMainWindow.Show();
                    this.myTimer.Dispose();
                    this.Close();
                }
            }
            this.Cursor = ((App)App.Current)._mainCursor;
        }

        private void _ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public bool verifUserExist()
        {
            bool verif = true;

            ObservableCollection<Utilisateur> _listOfUsers = new ObservableCollection<Utilisateur>(((App)App.Current).mySitaffEntities.Utilisateur.Where(us => us.Nom_Utilisateur != null && us.Nom_Utilisateur == this._textBoxUserName.Text.Trim() && us.Mot_De_Passe != null && us.Mot_De_Passe == this._textBoxPassword.Password));

            if (_listOfUsers.Count == 1)
            {
                ((App)App.Current)._connectedUser = _listOfUsers.First();
            }
            else if (_listOfUsers.Count == 0)
            {
                verif = false;
                MessageBox.Show("Mauvais nom d'utilisateur ou mot de passe.", "Erreur de connexion", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (_listOfUsers.Count > 1)
            {
                verif = false;
                MessageBox.Show("Erreur dans le base de données. Plusieurs utilisateurs ont le même nom et mot de passe. Contactez votre administrateur.", "Erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return verif;
        }

        public bool VerificationChamps()
        {
            bool verif = true;

            if (!this.Verif_UserName())
            {
                verif = false;
            }
            if (!this.Verif_Password())
            {
                verif = false;
            }
            return verif;
        }

        private bool Verif_UserName()
        {
            bool verif = true;

            if (this._textBoxUserName.Text.Trim().Length <= 0)
            {
                verif = false;
                this._textBoxUserName.Background = Brushes.Red;
                this._textBlockUserName.Foreground = Brushes.Red;
            }
            else
            {
                this._textBoxUserName.Background = Brushes.Green;
                this._textBlockUserName.Foreground = Brushes.Green;
            }
            return verif;
        }

        private bool Verif_Password()
        {
            bool verif = true;

            if (this._textBoxPassword.Password.Trim().Length <= 0)
            {
                verif = false;
                this._textBoxPassword.Background = Brushes.Red;
                this._textBlockPassword.Foreground = Brushes.Red;
            }
            else
            {
                this._textBoxPassword.Background = Brushes.Green;
                this._textBlockPassword.Foreground = Brushes.Green;
            }
            return verif;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Instancier une boite de dilogue de Winform
            System.Windows.Forms.ColorDialog dialogBox = new System.Windows.Forms.ColorDialog();

            // Affichage de la boite de dialogue
            if (dialogBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
                //Converti la couleur en Hexa
                ((App)App.Current).personnalisation.BackGroundLogScreenColor = (Brush)converter.ConvertFrom(string.Concat("#", (dialogBox.Color.ToArgb() & 0x00FFFFFF).ToString("X6")));

                RegistryKey regVersion;
                regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
                regVersion.SetValue("BackGroundLogScreenColor", string.Concat("#", (dialogBox.Color.ToArgb() & 0x00FFFFFF).ToString("X6")));
                regVersion.Close();

                ((App)App.Current).personnalisation.InitLogScreen(this);
            }
            else
            {
                
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((App)App.Current)._splash.Close();
        }

        public void Dispose()
        {
            try
            {
                this.myTimer.Dispose();
            }
            catch (Exception) { }
        }
    }
}
