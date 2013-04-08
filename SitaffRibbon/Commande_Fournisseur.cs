using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Commande_Fournisseur
    {
        /// <summary>
        /// Montant de la commande
        /// </summary>
        public double? MontantCommande
        {
            get
            {
                double? result = this.Total_Commande;
                if (Total_Ramene_A != null)
                {
                    result = Total_Ramene_A;
                }

                return result;
            }
        }

        /// <summary>
        /// Concaténation FactureFournisseur
        /// </summary>
        public string ConcatFactureFournisseur
        {
            get
            {
                string result = "";

                result = this.Numero + " || " + this.Date_Livraison_Prevu.Value.Day + "/" + this.Date_Livraison_Prevu.Value.Month + "/" + this.Date_Livraison_Prevu.Value.Year;
                if (this.Type_Commande1 != null)
                {
                    result = result + " || " + this.Type_Commande1.Libelle;
                }
                if (this.Commentaire_General != null && this.Commentaire_General != "")
                {
                    result = result + " || " + this.Commentaire_General;
                }

                return result;
            }
        }

        /// <summary>
        /// IsFranco
        /// </summary>
        public string IsFranco
        {
            get
            {
                string result = "";

                if (Franco == true)
                {
                    result = "Oui";
                }
                else
                {
                    result = "Non";
                }

                return result;
            }
        }

        /// <summary>
        /// Type de commande
        /// </summary>
        public string TypeCommande
        {
            get
            {
                string result = "Ni stock, ni divers, ni affaire. Erreur !";

                if (Stock == true)
                {
                    result = "Stock : " + Entreprise_Mere1.Nom;
                }
                else
                {
                    if (Divers == true)
                    {
                        result = "Divers : " + Entreprise_Mere1.Nom;
                    }
                    else
                    {
                        if (Affaire1 != null)
                        {
                            result = "Affaire : " + Affaire1.Numero;
                        }
                    }
                }

                return result;
            }
        }

        public bool ToutPasseEnBL
        {
            get
            {
                bool retour = true;

                foreach (Contenu_Commande_Fournisseur item in this.Contenu_Commande_Fournisseur)
                {
                    double toTest = item.Quantite;

                    foreach (Bon_Livraison bl in this.Bon_Livraison)
                    {
                        foreach (Bon_Livraison_Contenu_Commande blcc in bl.Bon_Livraison_Contenu_Commande)
                        {
                            if (blcc.Contenu_Commande_Fournisseur == item)
                            {
                                toTest = toTest - blcc.Quantite;
                            }
                        }
                    }

                    if (toTest > 0)
                    {
                        return false;
                    }
                }

                return retour;
            }
        }

        public bool ToutPasseEnFacture
        {
            get
            {
                bool retour = true;

                foreach (Contenu_Commande_Fournisseur item in this.Contenu_Commande_Fournisseur)
                {
                    double toTest = item.Quantite;

                    foreach (Bon_Livraison bl in this.Bon_Livraison)
                    {
                        foreach (Bon_Livraison_Contenu_Commande blcc in bl.Bon_Livraison_Contenu_Commande)
                        {
                            if (blcc.Contenu_Commande_Fournisseur == item)
                            {
                                foreach (Facture_Fournisseur_Contenu ffc in blcc.Facture_Fournisseur_Contenu)
                                {
                                    toTest = toTest - ffc.Qte_Facturee;
                                }
                            }
                        }
                    }

                    foreach (Facture_Fournisseur_Contenu ffc in item.Facture_Fournisseur_Contenu)
                    {
                        toTest = toTest - ffc.Qte_Facturee;
                    }

                    if (toTest > 0)
                    {
                        return false;
                    }
                }

                return retour;
            }
        }
    }
}
