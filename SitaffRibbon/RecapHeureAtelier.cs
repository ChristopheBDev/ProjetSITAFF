using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public class RecapHeureAtelier
    {

        private Affaire _affaire;

        public Affaire affaire
        {
            get { return _affaire; }
            set { _affaire = value; }
        }

        private Regie _regie;

        public Regie regie
        {
            get { return _regie; }
            set { _regie = value; }
        }

        private double _totalHeures;

        public double totalHeures
        {
            get { return _totalHeures; }
            set { _totalHeures = value; }
        }

        public RecapHeureAtelier(Affaire affaireP, Regie regieP, double heuresP)
        {
            this._affaire = affaireP;
            this._regie = regieP;
            this.totalHeures = heuresP;
        }
    }
}
