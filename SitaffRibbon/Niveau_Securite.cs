using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SitaffRibbon
{
    public partial class Niveau_Securite
    {

        public Visibility VisibiliteChangementParametres
        {
            get
            {
                if (this.OpenParametres)
                {
                    return Visibility.Visible;
                    //return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

    }
}
