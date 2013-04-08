using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace SitaffRibbon
{
    public partial class Conge
    {
        /// <summary>
        /// Renvoi l'état du congé
        /// </summary>
        public string EtatConge
        {
            get
            {
                string result = "";
                if (Accepte == null)
                {
                    result = "En cours";
                }
                else
                {
                    if (Accepte == true)
                    {
                        result = "Validé";
                    }
                    else
                    {
                        if (Accepte == false)
                        {
                            result = "Refusé";
                        }
                    }
                }

                return result;
            }
        }

        public System.Windows.Media.Brush CouleurConge
        {
            get
            {
                System.Windows.Media.Brush result = System.Windows.Media.Brushes.Black;
                if (Accepte == null)
                {
                    result = System.Windows.Media.Brushes.Black;
                }
                else
                {
                    if (Accepte == true)
                    {
                        result = System.Windows.Media.Brushes.Blue;
                    }
                    else
                    {
                        if (Accepte == false)
                        {
                            result = System.Windows.Media.Brushes.Red;
                        }
                    }
                }

                return result;
            }
        }
    }
}
