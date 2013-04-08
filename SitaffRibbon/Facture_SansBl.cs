using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Facture_SansBL
    {

        /// <summary>
        /// Renvoi la prix de la TVA
        /// </summary>
        public double getTva
        {
            get
            {
                try
                {
                    return this.Montant_TTC - this.Montant_HT;
                }
                catch (Exception) 
                {
                    return 0;
                }
            }
        }

    }
}
