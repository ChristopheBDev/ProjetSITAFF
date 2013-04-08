using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    class RecapFactureTva
    {

        #region Attributs

        private double _total;

        public double total
        {
            get { return _total; }
            set { _total = value; }
        }

        private string _chaine;

        public string chaine
        {
            get { return _chaine; }
            set { _chaine = value; }
        }

        private string _numero;

        public string numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        #endregion

        public RecapFactureTva(double total, string nom, string num)
        {
            this._total = total;
            this.chaine = nom;
            this.numero = num;
        }

    }
}
