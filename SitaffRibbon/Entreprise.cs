using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Entreprise
    {
        /// <summary>
        /// Renvoi si l'entreprise est client / fournisseur / client - fournisseur
        /// </summary>
        public string TypeEntrepriseClientFournisseur
        {
            get
            {
                if (this.Is_Client == true || this.Client != null || this.Is_Fournisseur == true || this.Fournisseur != null)
                {
                    if (this.Is_Client == true && this.Client != null && this.Is_Fournisseur == true && this.Fournisseur != null)
                    {
                        return "Client - Fournisseur";
                    }
                    else
                    {
                        if (this.Is_Client == true && this.Client != null)
                        {
                            return "Client";
                        }
                        else
                        {
                            if (this.Is_Fournisseur == true && this.Fournisseur != null)
                            {
                                return "Fournisseur";
                            }
                        }
                    }
                }
                else
                {
                    return "Entreprise ni cliente ni fournisseur";
                }
                return "";
            }
        }

        /// <summary>
        /// Renvoi le nom de l'entreprise suivi de sa ville
        /// </summary>
        public string NomEntrepriseAvecVille
        {
            get
            {
                //string retour = "";

                //retour = this.Libelle;

                //if (this.Adresse1 != null)
                //{
                //    if (this.Adresse1.Ville1 != null)
                //    {
                //        if (this.Adresse1.Ville1.Libelle != null)
                //        {
                //            retour += " || " + this.Adresse1.Ville1.Libelle;
                //        }
                //    }
                //}
                //return retour;
                try
                {
                    return this.Libelle + " || " + this.Adresse1.Ville1.Libelle;
                }
                catch (Exception)
                {
                    return this.Libelle;
                }
            }
        }

        public double nbComandes
        {
            get
            {
                double retour = 0;

                if (this.Fournisseur != null)
                {
                    retour = this.Fournisseur.Commande_Fournisseur.Count();
                }

                return retour;
            }
        }
    }
}
