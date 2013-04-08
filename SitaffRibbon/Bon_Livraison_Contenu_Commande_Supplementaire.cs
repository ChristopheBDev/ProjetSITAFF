using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Bon_Livraison_Contenu_Commande_Supplementaire
    {
        public double PTREM
        {
            get
            {
                double val;
                if (double.TryParse(this.Prix_Remise.ToString(), out val) && double.TryParse(this.Quantite_Livree.ToString(), out val))
                {
                    double retour = this.Prix_Remise * this.Quantite_Livree;
                    return retour;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
