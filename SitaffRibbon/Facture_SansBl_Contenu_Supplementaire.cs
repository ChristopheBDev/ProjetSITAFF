using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Facture_SansBl_Contenu_Supplementaire
    {

        /// <summary>
        /// Renvoi la prix total HT d'une ligne
        /// </summary>
        public double getPTHT
        {
            get
            {
                //if (this.Quantite != null && this.Montant_HT_Facture != null)
                //{
                //    return this.Quantite * this.Montant_HT_Facture;
                //}
                //else
                //{
                //    return 0;
                //}
                try
                {
                    return this.Quantite * this.Montant_HT_Facture;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

    }
}
