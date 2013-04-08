using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Facture
    {
		public double restantDu
		{
			get 
            {
                double totalRegle = 0;
                foreach (Reglement_Client_Facture item in this.Reglement_Client_Facture)
                {
                    if (item.Montant != null)
                    {
                        if (item.Reglement_Client1 != null)
                        {
                            totalRegle += (double)item.Montant;
                        }
                    }
                }

                return ((double)Net_A_Payer - totalRegle);
            }
		}

        public double pourcentageRegle
        {
            get
            {
                try
                {
                    double totalRegle = 0;
                    foreach (Reglement_Client_Facture item in this.Reglement_Client_Facture)
                    {
                        if (item.Montant != null)
                        {
                            if (item.Reglement_Client1 != null)
                            {
                                totalRegle += (double)item.Montant;
                            }
                        }
                    }
                    return (totalRegle / (double)this.Net_A_Payer) * 100;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }
}
