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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Configuration;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;
using System.Reflection;
using System.Windows.Threading;
using System.IO;
using SitaffRibbon.Classes;

namespace SitaffRibbon.UserControls
{
    /// <summary>
    /// Logique d'interaction pour CubeTournantControl.xaml
    /// </summary>
    public partial class CubeTournantControl : UserControl
    {
        public CubeTournantControl()
        {
            InitializeComponent();

            this.Go();
        }

        private void Go()
        {
            _trackball = new Trackball();
            _trackball.Attach(this);
            _trackball.Slaves.Add(myViewport3D);
            _trackball.Enabled = true;

            Storyboard s;

            s = (Storyboard)this.FindResource("RotateStoryboard");
            this.BeginStoryboard(s);
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            //// setup trackball for moving the model around
            //_trackball = new Trackball();
            //_trackball.Attach(this);
            //_trackball.Slaves.Add(myViewport3D);
            //_trackball.Enabled = true;

            //Storyboard s;

            //s = (Storyboard)this.FindResource("RotateStoryboard");
            //this.BeginStoryboard(s);

        }

        #region Globals

        Trackball _trackball;

        #endregion
    }
}
