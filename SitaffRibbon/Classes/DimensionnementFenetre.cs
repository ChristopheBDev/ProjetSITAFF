using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SitaffRibbon.Classes
{
    public class DimensionnementFenetre
    {
        #region Attributs

        public int longueur;
        public int hauteur;

        #endregion

        public DimensionnementFenetre()
        {
            this.longueur = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            this.hauteur = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        }

        public void InitTailles(Window window)
        {
            window.MaxWidth = this.longueur;
            window.MaxHeight = this.hauteur;
        }
    }
}
