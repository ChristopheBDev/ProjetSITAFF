using System;
using System.Collections.Generic;
using System.Deployment.Application;
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

namespace SitaffRibbon
{
    /// <summary>
    /// Logique d'interaction pour Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        Timer myTimer;
        double _positionAnimation = 0;
        bool sensDroite = true;

        public Splash()
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

            this._textBlockVersion.Text += "";

            if (ApplicationDeployment.IsNetworkDeployed) //Car ne fonctionne pas en mode debug
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                this._textBlockVersion.Text += ad.CurrentVersion.ToString();
            }
            else
            {
                this._textBlockVersion.Text += "mode débug, pas de version ...";
            }

        }

        private void startThread()
        {
            //AutoResetEvent autoEvent = new AutoResetEvent(false);
            //TimerCallback tcb = this.moveAnimation;
            //myTimer = new Timer(tcb, autoEvent, 1, 200);
            //System.Threading.Timer timer;
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

        public void modifTextBlockEnCours(string enCours)
        {
            this._TextBlockEnCours.Text = enCours;
        }

    }
}
