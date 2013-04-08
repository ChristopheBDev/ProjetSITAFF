using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace SitaffRibbon
{
    class RecapFacture : EntityObject
    {

        #region Attributs

        private Plan_Comptable_Imputation _planComptableImputation;

        public Plan_Comptable_Imputation planComptableImputation
        {
            get { return _planComptableImputation; }
            set { _planComptableImputation = value; }
        }

        private double _total;

        public double total
        {
            get { return _total; }
            set { _total = value; }
        }

        #endregion

        public RecapFacture(Plan_Comptable_Imputation plan, double total)
        {
            this._planComptableImputation = plan;
            this._total = total;
        }

    }
}
