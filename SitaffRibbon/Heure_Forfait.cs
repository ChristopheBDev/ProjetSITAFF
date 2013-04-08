using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Heure_Forfait
    {
        
        public double totalHeures
        {
            get
            {
                double retour = 0;

                retour += this.Heures_Lundi_Jour;
                retour += this.Heures_Mardi_Jour;
                retour += this.Heures_Mercredi_Jour;
                retour += this.Heures_Jeudi_Jour;
                retour += this.Heures_Vendredi_Jour;
                retour += this.Heures_Samedi_Jour;
                retour += this.Heures_Dimanche_Jour;
                retour += this.Heures_Lundi_Nuit;
                retour += this.Heures_Mardi_Nuit;
                retour += this.Heures_Mercredi_Nuit;
                retour += this.Heures_Jeudi_Nuit;
                retour += this.Heures_Vendredi_Nuit;
                retour += this.Heures_Samedi_Nuit;
                retour += this.Heures_Dimanche_Nuit;

                return retour;
            }
        }
    }

}
