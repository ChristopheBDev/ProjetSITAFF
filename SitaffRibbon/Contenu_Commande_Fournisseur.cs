using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Contenu_Commande_Fournisseur
    {
        /// <summary>
        /// Renvoi le nombre restant à livrer
        /// </summary>
        public double QuantiteRestante
        {
            get
            {
                if (this.Commande_Fournisseur1 != null)
                {
                    if (this.Commande_Fournisseur1.Affaire1 != null)
                    {
                        if (this.Commande_Fournisseur1.Affaire1.Numero.ToLower().Contains("tarif") || this.Commande_Fournisseur1.Affaire1.Numero.ToLower().Contains("test"))
                        {
                            return 0;
                        }
                    }
                }
                double retour = this.Quantite;
                Commande_Fournisseur temp = this.Commande_Fournisseur1;
                foreach (Bon_Livraison bl in temp.Bon_Livraison)
                {
                    foreach (Bon_Livraison_Contenu_Commande blcc in bl.Bon_Livraison_Contenu_Commande)
                    {
                        if (blcc.Contenu_Commande_Fournisseur == this)
                        {
                            retour = retour - blcc.Quantite;
                        }
                    }
                }
                foreach (Facture_Fournisseur_Contenu item in this.Facture_Fournisseur_Contenu)
                {
                    retour = retour - item.Qte_Facturee;
                }
                return retour;
            }
        }

        public double PrixMinimum
        {
            get
            {
                double retour = 0;
                bool test = true;

                try
                {
                    foreach (GetShopCommandeWithEntrepriseNameAndReference_Result item in ((App)App.Current).mySitaffEntities.GetShopCommandeWithEntrepriseNameAndReference(this.Commande_Fournisseur1.Fournisseur1.Identifiant, this.Reference))
                    {
                        if (item.Reference.ToLower().Trim() == this.Reference.ToLower().Trim())
                        {
                            if (test)
                            {
                                try
                                {
                                    retour = double.Parse(item.Min_Prix_Unitaire.ToString());
                                    test = false;
                                }
                                catch (Exception) { }
                            }
                            else
                            {
                                if (item.Min_Prix_Unitaire < retour)
                                {
                                    try
                                    {
                                        retour = double.Parse(item.Min_Prix_Unitaire.ToString());
                                    }
                                    catch (Exception) { }
                                }
                            }
                        }
                    }
                }
                catch (Exception) { }

                return retour;
            }
        }

        public double PTREM
        {
            get
            {
                double val;
                if (double.TryParse(this.Prix_Remise.ToString(), out val) && double.TryParse(this.Quantite.ToString(), out val))
                {
                    double retour = this.Prix_Remise * this.Quantite;
                    return retour;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string isText
        {
            get
            {
                string retour = "";

                if (this.Description != null)
                {
                    if (this.Description != "")
                    {
                        retour = "T";
                    }
                }

                return retour;
            }
        }
    }
}
