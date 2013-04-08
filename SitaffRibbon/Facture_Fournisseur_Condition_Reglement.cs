using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Facture_Fournisseur_Condition_Reglement
    {

        /// <summary>
        /// Renvoi le complet
        /// </summary>
        public double? MontantEcheance
        {
            get
            {
                double? result = null;

                if (this.Pourcentage != null)
                {
                    try
                    {
                        result = (double.Parse(this.Pourcentage.ToString()) / 100) * this.Facture_Fournisseur1.Montant_TTC;
                    }
                    catch (Exception) { }
                }

                return result;
            }
        }

    }
}
