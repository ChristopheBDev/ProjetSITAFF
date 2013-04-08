using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    /// <summary>
    /// Classe "partial" Personne qui permet l'ajout de fonctions aux fonctions initialement formées par l'ADO.
    /// </summary>
    public partial class Personne
    {
        /// <summary>
        /// Renvoi le Prénom suivi du Nom (séparés par un espace) en une seule chaine de caractères.
        /// </summary>
        public string fullname
        {
            get
            {
                return (Nom + " " + Prenom);
            }
        }

        /// <summary>
        /// Renvoi le Prénom suivi du Nom (séparés par un espace) en une seule chaine de caractères suivi de la fonction du contact.
        /// </summary>
        public string ContactfullnameSuiviFonction
        {
            get
            {
                string retour = fullname;

                if (this.Contact != null)
                {
                    if (this.Contact.Contact_Fonction1 != null)
                    {
                        retour += " || " + this.Contact.Contact_Fonction1.Libelle;
                    }
                }

                return retour;
            }
        }

        /// <summary>
        /// Renvoi si la personne est salarié ou contact
        /// </summary>
        public string typePersonne
        {
            get
            {
                if (Contact != null && Salarie != null)
                {
                    return ("Contact et Salarié. Cela n'est normalement pas possible ...");
                }
                else
                {
                    if (Contact != null)
                    {
                        return ("Contact");
                    }
                    else if (Salarie != null)
                    {
                        return ("Salarié");
                    }
                    else
                    {
                        return ("Rien. Cela n'est normalement pas possible ...");
                    }
                }
            }
        }

        /// <summary>
        /// Renvoi le type de salarié
        /// </summary>
        public string typeSalarie
        {
            get
            {
                if (Salarie != null)
                {
                    if (Salarie.Salarie_Interne != null)
                    {
                        return ("Salarié interne");
                    }
                    else if (Salarie.Tiers != null)
                    {
                        return ("Tiers");
                    }
                    else if (Salarie.Interimaire != null)
                    {
                        return ("Interimaire");
                    }
                    else
                    {
                        return ("Rien. Cela n'est normalement pas possible");
                    }
                }
                else
                {
                    return ("Pas salarié. Cela n'est normalement pas possible");
                }
            }
        }

        /// <summary>
        /// Renvoi le numéro de téléphone portable avec des espaces
        /// </summary>
        public string NumTelPortProAvecEspaces
        {
            get
            {
                if (Tel_Port_Pro != null)
                {
                    return Tel_Port_Pro;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// Renvoi le numéro de téléphone avec des espaces
        /// </summary>
        public string NumTelFixeProAvecEspaces
        {
            get
            {
                if (Tel_Fixe_Pro != null)
                {
                    return Tel_Fixe_Pro;
                }
                else
                {
                    if (this.Contact != null)
                    {
                        if (this.Entreprise1 != null)
                        {
                            if (this.Entreprise1.Telephone != null)
                            {
                                if (this.Entreprise1.Telephone.Length == 10)
                                {
                                    return this.Entreprise1.Telephone;
                                }
                                else
                                {
                                    return Tel_Fixe_Pro;
                                }
                            }
                            else
                            {
                                return Tel_Fixe_Pro;
                            }
                        }
                        else
                        {
                            return Tel_Fixe_Pro;
                        }
                    }
                    else
                    {
                        return Tel_Fixe_Pro;
                    }
                }
            }
        }

        /// <summary>
        /// Renvoi le numéro de téléphone avec des espaces
        /// </summary>
        public string NumFaxProAvecEspaces
        {
            get
            {
                if (Fax_Pro != null)
                {
                    return Fax_Pro;
                }
                else
                {
                    return "";
                }
            }
        }

    }
}
