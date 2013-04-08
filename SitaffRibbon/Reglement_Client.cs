using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Reglement_Client
    {

        /// <summary>
        /// Pourcentage du réglement
        /// </summary>
        public double pourcentageReglement
        {
            get
            {
                double retour = 0;

                if (this.Montant != null)
                {
                    if (this.Facture1 != null)
                    {
                        if (this.Facture1.Montant_TTC != null)
                        {
                            retour = double.Parse(this.Montant.ToString()) / double.Parse(this.Facture1.Montant_TTC.ToString());
                        }
                    }
                }

                return retour;
            }
        }

    }
}
