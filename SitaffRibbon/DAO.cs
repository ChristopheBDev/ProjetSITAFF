using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class DAO
    {
        /// <summary>
        /// Renvoi P si sur Devis et C si sur Affaire
        /// </summary>
        public string getType
        {
            get
            {
                if (Devis1 == null && Affaire1 == null)
                {
                    return "ni P ni C - Erreur !";
                }
                else
                {
                    if (Devis1 != null && Affaire1 != null)
                    {
                        return "P et C - Normalement pas possible !";
                    }
                    else
                    {
                        if (Devis1 != null)
                        {
                            bool test = false;
                            foreach (Versions v in Devis1.Versions)
                            {
                                if (v.Affaire1 != null)
                                {
                                    test = true;
                                    this.Affaire1 = v.Affaire1;
                                }
                            }
                            if (test)
                            {
                                this.Devis1 = null;
                                return "C";                                
                            }
                            else
                            {
                                return "P";
                            }
                        }
                        else
                        {
                            return "C";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ToolTip P-C
        /// </summary>
        public string ToolTipPC
        {
            get
            {
                String toTest = this.getType;
                if (toTest == "C")
                {
                    return "Plan sur Affaire - Conception";
                }
                else
                {
                    if (toTest == "P")
                    {
                        return "Plan sur Devis - Projet";
                    }
                    else
                    {
                        if (toTest == "P et C - Normalement pas possible !")
                        {
                            return "Erreur, sur affaire et sur devis, normalement pas possible";
                        }
                        else
                        {
                            return "Erreur, ni sur affaire, ni sur devis";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Renvoi le numéro du dessin complet
        /// </summary>
        public string getNumero
        {
            get
            {
                try
                {
                    string _year;
                    string _month;
                    string _incr;

                    _year = Annee.ToString().Substring(Annee.ToString().Length - 2, Annee.ToString().Length - (Annee.ToString().Length - 2));

                    if (this.Mois.ToString().Length == 1)
                    {
                        _month = "0" + this.Mois;
                    }
                    else
                    {
                        _month = this.Mois.ToString();
                    }

                    if (this.Increment.ToString().Length == 1)
                    {
                        _incr = "00" + this.Increment;
                    }
                    else
                    {
                        if (this.Increment.ToString().Length == 2)
                        {
                            _incr = "0" + this.Increment;
                        }
                        else
                        {
                            _incr = this.Increment.ToString();
                        }
                    }

                    return _year + "-" + _month + "-" + _incr;
                }
                catch (Exception)
                {
                    return "";
                }                
            }
        }
    }
}
