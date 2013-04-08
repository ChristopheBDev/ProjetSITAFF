using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Facture_Fournisseur
    {

        public double NetAPayer
        {
            get
            {
                double result = 0;

                result = this.Montant_TTC - this.Accompte;

                return result;
            }
        }

        public string YATIlUnAvoir
        {
            get
            {
                string result = "non";

                if (this.Avoir_Facture_Fournisseur.Count() > 0)
                {
                    foreach (Avoir_Facture_Fournisseur item in this.Avoir_Facture_Fournisseur)
                    {
                        if (item.Montant > 0)
                        {
                            result = "oui";
                        }
                    }
                }

                return result;
            }
        }

    }
}
