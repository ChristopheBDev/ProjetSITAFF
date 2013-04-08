using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Devis
    {
        /// <summary>
        /// Renvoi si en affaire ou sinon son etat
        /// </summary>
        public string EtatDuDevis
        {
            get
            {
                bool test = false;
                foreach (Versions v in this.Versions)
                {
                    if (v.Affaire1 != null)
                    {
                        test = true;
                    }
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
        public bool VerifEtatDevis
        {
            get
            {
                bool test = false;
                foreach (Versions v in this.Versions)
                {
                    if (v.Affaire1 != null)
                    {
                        test = true;
                    }
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
        public bool VerouillerEtatDevis
        {
            get
            {
                bool test = false;
                foreach (Versions v in this.Versions)
                {
                    if (v.Affaire1 != null)
                    {
                        test = true;
                    }
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

        public double totalVersions
        {
            get
            {
                double result = 0;

                foreach (Versions item in this.Versions)
                {
                    double val;
                    if (double.TryParse(item.Montant.ToString(), out val))
                    {
                        result += double.Parse(item.Montant.ToString());
                    }
                }

                return result;
            }
        }

        public double montantDerniereVersions
        {
            get
            {
                double result = 0;
                double val;

                if (this.Versions.Where(ver => ver.Version_Type1.Code.Contains('V')).Count() > 0)
                {
                    Versions tmp = new Versions();
                    foreach (Versions item in this.Versions.Where(ver => ver.Version_Type1.Code.Contains('V')).OrderBy(ver => ver.Numero))
                    {
                        tmp = item;
                    }
                    if (double.TryParse(tmp.Montant.ToString(), out val))
                    {
                        result = double.Parse(tmp.Montant.ToString());
                    }
                }
                else
                {
                    Versions tmp = new Versions();
                    foreach (Versions item in this.Versions.OrderBy(ver => ver.Numero))
                    {
                        tmp = item;
                    }
                    if (double.TryParse(tmp.Montant.ToString(), out val))
                    {
                        result = double.Parse(tmp.Montant.ToString());
                    }
                }

                return result;
            }
        }
    }
}
