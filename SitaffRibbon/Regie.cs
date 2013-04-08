using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Regie
    {
        /// <summary>
        /// Renvoi l'état de la régie
        /// </summary>
        public string etatRegie
        {
            get
            {
                if (Termine)
                {
                    return "Terminé";
                }
                else
                {
                    return "En cours";
                }
            }
        }

        public string EtatRegieCommande
        {
            get
            {
                string retour = "Erreur";

                if (this.Versions1 == null)
                {
                    retour = "En attente de valorisation";
                }
                else
                {
                    if (this.Versions1.Commande1 == null)
                    {
                        retour = "En attente de chiffrage";
                    }
                    else
                    {
                        if (this.Versions1.Commande1.Numero_Commande == null || this.Versions1.Commande1.Numero_Commande == "")
                        {
                            retour = "En attente BC client";
                        }
                        else
                        {
                            if (this.Versions1.Commande1.Facture.Count() == 0)
                            {
                                retour = "A facturer";
                            }
                            else
                            {
                                retour = "Facturé";
                            }
                        }
                    }
                }

                return retour;
            }
        }

        public double HeureAssociees
        {
            get
            {
                double retour = 0;

                foreach (Travail_Sur_Regie item in this.Travail_Sur_Regie)
                {
                        retour += item.Nombre_Heures;                    
                }

                foreach (Heure_Atelier item in this.Heure_Atelier)
                {
                    retour += item.Heures_Lundi + item.Heures_Mardi + item.Heures_Mercredi + item.Heures_Jeudi + item.Heures_Vendredi + item.Heures_Samedi;
                }

                return retour;
            }
        }
    }
}
