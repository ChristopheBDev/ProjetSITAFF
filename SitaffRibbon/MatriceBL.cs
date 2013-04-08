using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace SitaffRibbon
{
    public class MatriceBL : EntityObject
    {
        private Affaire _affaire;

        public Affaire affaire
        {
            get { return _affaire; }
            set { _affaire = value; }
        }

        private Bon_Livraison _bon_livraison;

        public Bon_Livraison bon_livraison
        {
            get { return _bon_livraison; }
            set { _bon_livraison = value; }
        }

        private Commande_Fournisseur _commande_fournisseur;

        public Commande_Fournisseur commande_fournisseur
        {
            get { return _commande_fournisseur; }
            set { _commande_fournisseur = value; }
        }
       
    }
}
