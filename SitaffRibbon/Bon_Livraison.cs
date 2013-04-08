using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SitaffRibbon
{
    public partial class Bon_Livraison
    {
        /// <summary>
        /// Renvoi les elements restant à livrer
        /// </summary>
        public ObservableCollection<Contenu_Commande_Fournisseur> ListeContenuDisponible
        {
            get
            {
                ObservableCollection<Contenu_Commande_Fournisseur> toReturn = new ObservableCollection<Contenu_Commande_Fournisseur>();
                try
                {
                    foreach (Contenu_Commande_Fournisseur ccf in this.Commande_Fournisseur1.Contenu_Commande_Fournisseur)
                    {
                        if (ccf.QuantiteRestante > 0)
                        {
                            toReturn.Add(ccf);
                        }
                    }
                }
                catch (Exception)
                {                    
                }                
                return toReturn;
            }
        }

        public string onWhat
        {
            get
            {
                if (this.StockAtelier == true)
                {
                    return "Stock";
                }
                else if (this.Divers == true)
                {
                    return "Divers";
                }
                else if (this.Affaire1 != null)
                {
                    return "Sur affaire";
                }
                else
                {
                    return "Rien, erreur !";
                }
            }
        }

        public string Reception
        {
            get
            {
                if (this.Recu == true)
                {
                    return "Reçu";
                }
                else
                {
                    return "non reçu";
                }
            }
        }
    }
}
