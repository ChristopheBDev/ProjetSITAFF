using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Commande
    {

        public string NumeroAvecPrixRemise
        {
            get
            {
                string retour = this.Numero_Commande;

                if (this.Versions.Count > 0)
                {
                    try
                    {
                        Versions tmp = this.Versions.First();

                        if (tmp.Montant_Remise != null)
                        {
                            retour = retour + " -- " + String.Format("{0:n}", tmp.Montant_Remise);
                        }
                    }
                    catch (Exception) { }
                }

                return retour;
            }
        }

        public double MontantRemiseCommande
        {
            get
            {
                double retour = 0;

                if (this.Versions.Count > 0)
                {
                    try
                    {
                        Versions tmp = this.Versions.First();

                        if (tmp.Montant_Remise != null)
                        {
                            retour = double.Parse(tmp.Montant_Remise.ToString());
                        }
                    }
                    catch (Exception) { }
                }

                return retour;
            }
        }

        public Affaire getAffaire
        {
            get
            {
                try
                {
                    return this.Versions.First().Affaire1;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public Client getClient
        {
            get
            {
                try
                {
                    return this.Versions.First().Devis1.Client2;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

    }
}
