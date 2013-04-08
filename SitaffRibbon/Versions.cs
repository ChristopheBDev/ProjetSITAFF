using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace SitaffRibbon
{
    public partial class Versions
    {

        public double TotalFacture
        {
            get
            {
                double toReturn = 0;
                try
                {
                    if (this.Commande1 != null)
                    {
                        foreach (Facture item in this.Commande1.Facture)
                        {
                            toReturn = toReturn + double.Parse(item.Montant.ToString());
                        }
                    }
                }
                catch (Exception)
                {
                    return toReturn;
                }
                return toReturn;
            }
        }

        public double TotalRestantAFacture
        {
            get
            {
                try
                {
                    return double.Parse(this.Montant_Remise.ToString()) - this.TotalFacture;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Renvoi la couleur de si c'est en affaire ou non
        /// </summary>
        public SolidColorBrush couleurVersionEnAffaire
        {
            get
            {
                if (this.Affaire1 == null)
                {
                    return Brushes.Red;
                }
                else
                {
                    return Brushes.Green;
                }
            }
        }

        /// <summary>
        /// Renvoi la couleur de si c'est en affaire ou non
        /// </summary>
        public bool EstCeEnAffaire
        {
            get
            {
                if (this.Affaire1 == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


        /// <summary>
        /// Renvoi l'incrémentation de la version
        /// </summary>
        public String numeroComplet
        {
            get
            {
                String message = "";

                message = this.Devis1.Numero + "-" + this.Numero;

                return message;
            }
        }

        /// <summary>
        /// Renvoi avec le commentaire
        /// </summary>
        public String numeroAvecDesignation
        {
            get
            {
                String message = "";

                message = this.Numero + " : " + this.Commentaire;

                return message;
            }
        }

        /// <summary>
        /// Renvoi si en affaire ou sinon son etat
        /// </summary>
        public string EtatDuDevisVersion
        {
            get
            {
                bool test = false;
                if (this.Affaire1 != null)
                {
                    test = true;
                }
                if (test == true)
                {
                    return "En Affaire";
                }
                else
                {
                    if (this.Devis_Etat1 != null)
                    {
                        return this.Devis_Etat1.Libelle;
                    }
                    else
                    {
                        return "Etat inconnu, veuillez le renseigner";
                    }
                }
            }
        }

        /// <summary>
        /// Doit-on vérifier la comboBox
        /// </summary>
        public bool VerifEtatDevisVersion
        {
            get
            {
                bool test = false;
                if (this.Affaire1 != null)
                {
                    test = true;
                }
                if (test == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Doit-on verrouiller etat devis ?
        /// </summary>
        public bool VerouillerEtatDevisVersion
        {
            get
            {
                bool test = false;
                if (this.Affaire1 != null)
                {
                    test = true;
                }
                if (test == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public string fullTestRegie
        {
            get
            {
                string retour = "";

                if (this.Devis1 != null)
                {
                    retour += this.Devis1.Libelle;
                }

                if (this.Numero != null)
                {
                    retour += " || " + this.Numero;
                }

                if (this.Commande1 != null)
                {
                    if (this.Commande1.Numero_Commande != "" && this.Commande1.Numero_Commande != null)
                    {
                        retour += " || " + this.Commande1.Numero_Commande;
                    }
                }

                return retour;
            }
        }

    }
}
