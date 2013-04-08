using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Bon_Livraison_Contenu_Commande
    {
        /// <summary>
        /// Renvoi la quantité totale "reçue"
        /// </summary>
        public double QuantiteRestante
        {
            get
            {
                double retour = this.Contenu_Commande_Fournisseur.Quantite;
                foreach (Bon_Livraison bl in this.Bon_Livraison1.Commande_Fournisseur1.Bon_Livraison)
                {
                    foreach (Bon_Livraison_Contenu_Commande blcc in bl.Bon_Livraison_Contenu_Commande)
                    {
                        if (this.Contenu_Commande_Fournisseur == blcc.Contenu_Commande_Fournisseur)
                        {
                            retour = retour - blcc.Quantite;
                        }
                    }
                }
                foreach (Facture_Fournisseur_Contenu item in this.Contenu_Commande_Fournisseur.Facture_Fournisseur_Contenu)
                {
                    retour = retour - item.Qte_Facturee;
                }
                return retour;
            }
        }
    }
}
