using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
    public class ItemShop : EntityObject
    {

        private double? _Max_Prix_Unitaire;

        public double? Max_Prix_Unitaire
        {
            get { return _Max_Prix_Unitaire; }
            set { _Max_Prix_Unitaire = value; }
        }


        private double? _Moyenne_Prix_Unitaire;

        public double? Moyenne_Prix_Unitaire
        {
            get { return _Moyenne_Prix_Unitaire; }
            set { _Moyenne_Prix_Unitaire = value; }
        }


        private double? _Min_Prix_Unitaire;

        public double? Min_Prix_Unitaire
        {
            get { return _Min_Prix_Unitaire; }
            set { _Min_Prix_Unitaire = value; }
        }


        private double? _Max_Prix_Remise;

        public double? Max_Prix_Remise
        {
            get { return _Max_Prix_Remise; }
            set { _Max_Prix_Remise = value; }
        }


        private double? _Moyenne_Prix_Remise;

        public double? Moyenne_Prix_Remise
        {
            get { return _Moyenne_Prix_Remise; }
            set { _Moyenne_Prix_Remise = value; }
        }


        private double? _Min_Prix_Remise;

        public double? Min_Prix_Remise
        {
            get { return _Min_Prix_Remise; }
            set { _Min_Prix_Remise = value; }
        }


        private int? _Nb_Fois_Commande;

        public int? Nb_Fois_Commande
        {
            get { return _Nb_Fois_Commande; }
            set { _Nb_Fois_Commande = value; }
        }


        private string _Designation;

        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }

        private string _Fournisseur;

        public string Fournisseur
        {
            get { return _Fournisseur; }
            set { _Fournisseur = value; }
        }

        private string _Reference;

        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        

        private long _idFournisseur;

        public long idFournisseur
        {
            get { return _idFournisseur; }
            set { _idFournisseur = value; }
        }

        public ItemShop(string oReference, string oDesignation, string oFournisseur, long oidFournisseur, int? oNb_Fois_Commande, double? oMin_Prix_Remise, double? oMoyenne_Prix_Remise, double? oMax_Prix_Remise, double? oMin_Prix_Unitaire, double? oMoyenne_Prix_Unitaire, double? oMax_Prix_Unitaire)
        {
            this.Max_Prix_Unitaire = oMax_Prix_Unitaire;
            this.Moyenne_Prix_Unitaire = oMoyenne_Prix_Unitaire;
            this.Min_Prix_Unitaire = oMin_Prix_Unitaire;
            this.Max_Prix_Remise = oMax_Prix_Remise;
            this.Moyenne_Prix_Remise = oMoyenne_Prix_Remise;
            this.Min_Prix_Remise = oMin_Prix_Remise;
            this.Nb_Fois_Commande = oNb_Fois_Commande;
            this.Designation = oDesignation;
            this.Fournisseur = oFournisseur;
            this.idFournisseur = oidFournisseur;
            this.Reference = oReference;
        }


    }
}
