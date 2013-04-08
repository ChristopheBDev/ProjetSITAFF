using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace SitaffRibbon
{
    public partial class Affaire
    {
	
        /// <summary>
        /// Renvoi le complet
        /// </summary>
        public string CompletHeures
        {
            get
            {
                string retour = ""; 

                retour += this.Numero + " / ";
                retour += this.Nombre_Heure_Forfait + "H. Forfait / ";
                retour += this.Nombre_Heure_Regie + "H. Régie / ";
                retour += this.Nombre_Heure_Atelier + "H. Atelier";

                return retour;
            }
        }

        /// <summary>
        /// Renvoi le nombre d'heures de forfait
        /// </summary>
        public string Nombre_Heure_Forfait
        {
            get
            {
                double total = 0;

                foreach (Releve_Heure_Forfait rhf in this.Releve_Heure_Forfait)
                {
                    total += rhf.Total_Forfait;
                }

                return total.ToString();
            }
        }

        /// <summary>
        /// Renvoi le nombre d'heures de Regie
        /// </summary>
        public string Nombre_Heure_Regie
        {
            get
            {
                double total = 0;

                foreach (Releve_Heure_Forfait rhf in this.Releve_Heure_Forfait)
                {
                    total += rhf.Total_Regie;
                }

                return total.ToString();
            }
        }

        /// <summary>
        /// Renvoi le nombre d'heures de Regie
        /// </summary>
        public string Nombre_Heure_Atelier
        {
            get
            {
                double total = 0;

                foreach (Heure_Atelier rhf in this.Heure_Atelier)
                {
                    total += rhf.Total;
                }

                return total.ToString();
            }
        }
    }
}
