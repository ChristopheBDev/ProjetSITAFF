using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public class ImportPourRetourChantier
    {

        #region Attributs

        private string _Designation;

        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }

        private string _Reference;

        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }

        private string _Fournisseur;

        public string Fournisseur
        {
            get { return _Fournisseur; }
            set { _Fournisseur = value; }
        }

        private double _PrixUnitaireRemise;

        public double PrixUnitaireRemise
        {
            get { return _PrixUnitaireRemise; }
            set { _PrixUnitaireRemise = value; }
        }

        private string _Provenance;

        public string Provenance
        {
            get { return _Provenance; }
            set { _Provenance = value; }
        }
        

        #endregion

        public ImportPourRetourChantier()
        {

        }

    }
}
