using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public class RecapTravauxAtelier
    {
        private Tache_Atelier _tache_atelier;

        public Tache_Atelier tache_atelier
        {
            get { return _tache_atelier; }
            set { _tache_atelier = value; }
        }

        private double _totalHeures;

        public double totalHeures
        {
            get { return _totalHeures; }
            set { _totalHeures = value; }
        }

        public RecapTravauxAtelier(Tache_Atelier tacheatelier, double heures)
        {
            this._tache_atelier = tacheatelier;
            this._totalHeures = heures;
        }
        
    }
}
