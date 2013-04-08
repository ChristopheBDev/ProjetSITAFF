using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Facture_Fournisseur_Contenu
    {
        /// <summary>
        /// D'où vient le contenu
        /// </summary>
        public string provenance
        {
            get
            {
                if (Bon_Livraison_Contenu_Commande1 != null)
                {
                    return "Contenu de bon de livraison n°" + Bon_Livraison_Contenu_Commande1.Bon_Livraison1.Numero;
                }
                else if (Bon_Livraison_Contenu_Commande_Supplementaire1 != null)
                {
                    return "Contenu supplémentaire de bon de livraison n°" + Bon_Livraison_Contenu_Commande_Supplementaire1.Bon_Livraison1.Numero;
                }
                else if (Contenu_Commande_Fournisseur1 != null)
                {
                    return "Contenu de commande fournisseur n°" + Contenu_Commande_Fournisseur1.Commande_Fournisseur1.Numero;
                }
                else
                {
                    return "Manuel";
                }
            }
        }

    }
}
