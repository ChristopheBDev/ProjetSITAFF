using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace SitaffRibbon
{
    class EtatDevis : EntityObject
    {
        private String _chaine;

        public String chaine
        {
            get { return _chaine; }
            set { _chaine = value; }
        }

        public EtatDevis(String _string)
        {
            this.chaine = _string;
        }
        
    }
}
