using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace SitaffRibbon
{
    class EtatDeConge : EntityObject
    {
        private String _etat;

        public String etat
        {
            get { return _etat; }
            set { _etat = value; }
        }

        public EtatDeConge(String state)
        {
            this._etat = state;
        }

    }
}
