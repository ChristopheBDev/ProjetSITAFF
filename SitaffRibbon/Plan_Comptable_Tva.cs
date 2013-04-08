using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Plan_Comptable_Tva
    {

        public string getConcatenation
        {
            get
            {
                string retour = "";

                retour = this.Numero + "-" + this.Tva1.Taux + "-" + this.Libelle;

                return retour;
            }
        }

    }
}
