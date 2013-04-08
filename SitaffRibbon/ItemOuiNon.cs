using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace SitaffRibbon
{
    class ItemOuiNon : EntityObject
    {
        private String _chaine;

        public String chaine
        {
            get { return _chaine; }
            set { _chaine = value; }
        }

        public ItemOuiNon(String state)
        {
            this._chaine = state;
        }
    }
}
