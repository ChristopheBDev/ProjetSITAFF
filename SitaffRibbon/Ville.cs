using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Ville
    {

        /// <summary>
        /// Renvoi le nom de la ville suivi de son code postal
        /// </summary>
        public string fullVille
        {
            get
            {
                string retour = "";

                retour += Libelle;
                retour += " || ";
                retour += Code_Postal;

                return retour;
            }
        }

    }
}
