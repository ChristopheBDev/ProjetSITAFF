using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public partial class Entreprise_RIB
    {

        public string fullIBAN
        {
            get
            {
                string retour = "";

                if (IBAN_GROUP_1 != "" || IBAN_GROUP_2 != "" || IBAN_GROUP_3 != "" || IBAN_GROUP_4 != "" || IBAN_GROUP_5 != "" || IBAN_GROUP_6 != "" || IBAN_GROUP_7 != "")
                {
                    retour = IBAN_GROUP_1 + " " + IBAN_GROUP_2 + " " + IBAN_GROUP_3 + " " + IBAN_GROUP_4 + " " + IBAN_GROUP_5 + " " + IBAN_GROUP_6 + " " + IBAN_GROUP_7;
                }

                return retour;
            }
        }

        public string fullRIB
        {
            get
            {
                string retour = "";

                if (RIB_Etablissement != "" || RIB_Guichet != "" || RIB_Compte != "" || RIB_Cle != "")
                {
                    retour = RIB_Etablissement + " " + RIB_Guichet + " " + RIB_Compte + " " + RIB_Cle;
                }

                return retour;
            }
        }
    }
}
