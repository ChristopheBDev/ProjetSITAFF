using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Net;
using System.ComponentModel;

namespace SitaffRibbon
{
    /// <summary>
    /// Logique d'interaction pour DownloadFileURL.xaml
    /// </summary>
    public partial class DownloadFileURL : Window
    {
        Timer myTimer;
        double _positionAnimation = 0;
        bool sensDroite = true;

        WebClient webClient = new WebClient();

        public string urlToDownload = "";
        public string nomFichier = "";
        public string PositionFichier = "";

        #region Constructeur

        public DownloadFileURL()
        {
            InitializeComponent();

            this.startThread();

            ((App)App.Current)._mainCursor = this.Cursor;

            ((App)App.Current).personnalisation.InitLogScreen(this);
            //Je récupère les couleurs car on ne peut les faire en automatique pour TOUS les usercontrols
            this.colorToDontMove1.Color = new SolidColorBrush(((App)App.Current).personnalisation.ColorFixe).Color;
            this.colorToDontMove1.Color = ((App)App.Current).personnalisation.ColorFixe;
            this.colorToDontMove2.Color = ((App)App.Current).personnalisation.ColorFixe;
            this.colorToMove.Color = ((App)App.Current).personnalisation.ColorMove;
        }

        #endregion

        #region Gestion mouvement couleur autour fenêtre

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

        #endregion

        public void ModificationTexte(string _text)
        {
            this._TextBlockMessage.Text = "Téléchargement en cours de : " + _text + " ...";
        }

        public void TelechargementFichier()
        {
            try
            {
                //webClient.Credentials = new NetworkCredential("administrateur", "53admin35", "GROUPESIT");
                webClient.Credentials = CredentialCache.DefaultCredentials;
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)");
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                if (this.PositionFichier == "")
                {
                    this.PositionFichier = Environment.GetEnvironmentVariable("TEMP");
                    webClient.DownloadFileAsync(new Uri(this.urlToDownload), PositionFichier + @"\" + this.nomFichier + ".pdf");
                }
                else
                {
                    System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                    saveFileDialog.DefaultExt = ".pdf";
                    saveFileDialog.Filter = "fichier pdf |.pdf";

                    saveFileDialog.ShowDialog();
                    if (saveFileDialog.FileName != null || saveFileDialog.FileName != "")
                    {
                        this.PositionFichier = saveFileDialog.FileName;
                        webClient.DownloadFileAsync(new Uri(this.urlToDownload), PositionFichier);
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    this.DialogResult = false;
                }
                catch (Exception) { }
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.progressBarLoading.Value = e.ProgressPercentage;
            this._textProgession.Text = e.ProgressPercentage + " %";
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

    }
}
