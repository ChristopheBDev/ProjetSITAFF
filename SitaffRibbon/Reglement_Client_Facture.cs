using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Reglement_Client_Facture
    {
        public double PourcentageRegle
        {
            get
            {
                try
                {
                    return ((double)this.Montant / (double)this.Facture1.Net_A_Payer)*100;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public double vent
        {
            get
            {
                return 0;
            }
            set
            {

            }
        }
    }
}
