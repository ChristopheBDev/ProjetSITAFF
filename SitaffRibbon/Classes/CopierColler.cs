using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;

namespace SitaffRibbon.Classes
{
    class CopierColler
    {
        public void PasteData()
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                String data = Clipboard.GetText();
                MessageBox.Show(data);
            }
        }

        public ObservableCollection<Contenu_Commande_Fournisseur> PasteDataCommandeWindow()
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                ObservableCollection<Contenu_Commande_Fournisseur> toReturn = new ObservableCollection<Contenu_Commande_Fournisseur>();
                String data = Clipboard.GetText();
                ObservableCollection<String> listLignes = new ObservableCollection<string>(data.Split('\n'));
                int nbLignes = 1;
                int ligneToIgnore = listLignes.Count();
                foreach (String ligne in listLignes)
                {
                    if (nbLignes != ligneToIgnore)
                    {
                        if (this.verifNombreColonnes(ligne, 4))
                        {
                            ObservableCollection<String> listColonnes = new ObservableCollection<string>(ligne.Split('\t'));
                            int i = 1;
                            Contenu_Commande_Fournisseur toAdd = new Contenu_Commande_Fournisseur();
                            toAdd.Prix_Remise = 0;
                            toAdd.Taux_Remise = 0;
                            foreach (String colonne in listColonnes)
                            {
                                if (i == 1)
                                {
                                    toAdd.Reference = colonne;
                                }
                                if (i == 2)
                                {
                                    toAdd.Designation = colonne;
                                }
                                if (i == 3)
                                {
                                    double val;
                                    if (double.TryParse(colonne.Replace(".", ","), out val))
                                    {
                                        toAdd.Quantite = double.Parse(colonne.Replace(".", ","));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Le collage s'arrete car à  la ligne n°" + nbLignes + ", vous essayez de rentrer une quantité qui n'est pas un chiffre", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return null;
                                    }
                                }
                                if (i == 4)
                                {
                                    double val;
                                    if (double.TryParse(colonne.Replace(".", ","), out val))
                                    {
                                        toAdd.Prix_Unitaire = double.Parse(colonne.Replace(".", ","));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Le collage s'arrete car à  la ligne n°" + nbLignes + ", vous essayez de rentrer un prix unitaire qui n'est pas un chiffre", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return null;
                                    }
                                }
                                toReturn.Add(toAdd);
                                i = i + 1;
                            }
                            nbLignes = nbLignes + 1;
                        }
                        else
                        {
                            MessageBox.Show("Le collage s'arrete car à la ligne n°" + nbLignes + ", le nombre de colonne n'est pas bon, il doit y en avoir 4 (Référence, désignation, Quantité, Prix unitaire).", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                            return null;
                        }
                    }
                }
                return toReturn;
            }
            else
            {
                return null;
            }
        }

        public ObservableCollection<Contenu_Sortie_Atelier> PasteDataSortieAtelierWindow()
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                ObservableCollection<Contenu_Sortie_Atelier> toReturn = new ObservableCollection<Contenu_Sortie_Atelier>();
                String data = Clipboard.GetText();
                ObservableCollection<String> listLignes = new ObservableCollection<string>(data.Split('\n'));
                int nbLignes = 1;
                int ligneToIgnore = listLignes.Count();
                foreach (String ligne in listLignes)
                {
                    if (nbLignes != ligneToIgnore)
                    {
                        if (this.verifNombreColonnes(ligne, 5))
                        {
                            ObservableCollection<String> listColonnes = new ObservableCollection<string>(ligne.Split('\t'));
                            int i = 1;
                            Contenu_Sortie_Atelier toAdd = new Contenu_Sortie_Atelier();
                            foreach (String colonne in listColonnes)
                            {
                                if (i == 1)
                                {
                                    toAdd.Reference = colonne;
                                }
                                if (i == 2)
                                {
                                    toAdd.Designation = colonne;
                                }
                                if (i == 3)
                                {
                                    double val;
                                    if (double.TryParse(colonne.Replace(".", ","), out val))
                                    {
                                        toAdd.Quantite = double.Parse(colonne.Replace(".", ","));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Le collage s'arrete car à  la ligne n°" + nbLignes + ", vous essayez de rentrer une quantité qui n'est pas un chiffre", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return null;
                                    }
                                }
                                if (i == 4)
                                {
                                    double val;
                                    if (double.TryParse(colonne.Replace(".", ","), out val))
                                    {
                                        toAdd.Prix = double.Parse(colonne.Replace(".", ","));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Le collage s'arrete car à  la ligne n°" + nbLignes + ", vous essayez de rentrer un prix unitaire qui n'est pas un chiffre", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return null;
                                    }
                                }
                                if (i == 5)
                                {
                                    double val;
                                    if (double.TryParse(colonne.Replace(".", ","), out val))
                                    {
                                        toAdd.Prix_Remise = double.Parse(colonne.Replace(".", ","));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Le collage s'arrete car à  la ligne n°" + nbLignes + ", vous essayez de rentrer un prix remisé qui n'est pas un chiffre", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return null;
                                    }
                                }
                                toReturn.Add(toAdd);
                                i = i + 1;
                            }
                            nbLignes = nbLignes + 1;
                        }
                        else
                        {
                            MessageBox.Show("Le collage s'arrete car à la ligne n°" + nbLignes + ", le nombre de colonne n'est pas bon, il doit y en avoir 5 (Référence, désignation, Quantité, Prix unitaire, Prix remisé).", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                            return null;
                        }
                    }
                }
                return toReturn;
            }
            else
            {
                return null;
            }
        }

        public ObservableCollection<Contenu_Retour_Chantier> PasteDataRetourChantierWindow()
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                ObservableCollection<Contenu_Retour_Chantier> toReturn = new ObservableCollection<Contenu_Retour_Chantier>();
                String data = Clipboard.GetText();
                ObservableCollection<String> listLignes = new ObservableCollection<string>(data.Split('\n'));
                int nbLignes = 1;
                int ligneToIgnore = listLignes.Count();
                foreach (String ligne in listLignes)
                {
                    if (nbLignes != ligneToIgnore)
                    {
                        if (this.verifNombreColonnes(ligne, 4))
                        {
                            ObservableCollection<String> listColonnes = new ObservableCollection<string>(ligne.Split('\t'));
                            int i = 1;
                            Contenu_Retour_Chantier toAdd = new Contenu_Retour_Chantier();
                            foreach (String colonne in listColonnes)
                            {
                                if (i == 1)
                                {
                                    toAdd.Reference = colonne;
                                }
                                if (i == 2)
                                {
                                    toAdd.Designation = colonne;
                                }
                                if (i == 3)
                                {
                                    double val;
                                    if (double.TryParse(colonne.Replace(".", ","), out val))
                                    {
                                        toAdd.Quantite = double.Parse(colonne.Replace(".", ","));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Le collage s'arrete car à  la ligne n°" + nbLignes + ", vous essayez de rentrer une quantité qui n'est pas un chiffre", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return null;
                                    }
                                }
                                if (i == 4)
                                {
                                    double val;
                                    if (double.TryParse(colonne.Replace(".", ","), out val))
                                    {
                                        toAdd.Prix = double.Parse(colonne.Replace(".", ","));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Le collage s'arrete car à  la ligne n°" + nbLignes + ", vous essayez de rentrer un prix unitaire qui n'est pas un chiffre", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return null;
                                    }
                                }
                                toReturn.Add(toAdd);
                                i = i + 1;
                            }
                            nbLignes = nbLignes + 1;
                        }
                        else
                        {
                            MessageBox.Show("Le collage s'arrete car à la ligne n°" + nbLignes + ", le nombre de colonne n'est pas bon, il doit y en avoir 4 (Référence, désignation, Quantité, Prix unitaire).", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                            return null;
                        }
                    }
                }
                return toReturn;
            }
            else
            {
                return null;
            }
        }

        public ObservableCollection<Facture_Fournisseur_Contenu> PasteDataFactureFournisseurWindow()
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                ObservableCollection<Facture_Fournisseur_Contenu> toReturn = new ObservableCollection<Facture_Fournisseur_Contenu>();
                String data = Clipboard.GetText();
                ObservableCollection<String> listLignes = new ObservableCollection<string>(data.Split('\n'));
                int nbLignes = 1;
                int ligneToIgnore = listLignes.Count();
                foreach (String ligne in listLignes)
                {
                    if (nbLignes != ligneToIgnore)
                    {
                        if (this.verifNombreColonnes(ligne, 4))
                        {
                            ObservableCollection<String> listColonnes = new ObservableCollection<string>(ligne.Split('\t'));
                            int i = 1;
                            Facture_Fournisseur_Contenu toAdd = new Facture_Fournisseur_Contenu();
                            toAdd.Qte_Commandee = 0;
                            toAdd.Qte_Livree = 0;
                            toAdd.Prix_Unitaire_Commande_HT = 0;
                            toAdd.Prix_Unitaire_Remise_HT = 0;
                            foreach (String colonne in listColonnes)
                            {
                                if (i == 1)
                                {
                                    toAdd.Reference_Fournisseur = colonne;
                                }
                                if (i == 2)
                                {
                                    toAdd.Designation = colonne;
                                }
                                if (i == 3)
                                {
                                    double val;
                                    if (double.TryParse(colonne.Replace(".", ","), out val))
                                    {
                                        toAdd.Qte_Facturee = double.Parse(colonne.Replace(".", ","));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Le collage s'arrete car à  la ligne n°" + nbLignes + ", vous essayez de rentrer une quantité qui n'est pas un chiffre", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return null;
                                    }
                                }
                                if (i == 4)
                                {
                                    double val;
                                    if (double.TryParse(colonne.Replace(".", ","), out val))
                                    {
                                        toAdd.Prix_Unitaire_Facture_HT = double.Parse(colonne.Replace(".", ","));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Le collage s'arrete car à  la ligne n°" + nbLignes + ", vous essayez de rentrer un prix unitaire qui n'est pas un chiffre", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return null;
                                    }
                                }
                                toReturn.Add(toAdd);
                                i = i + 1;
                            }
                            nbLignes = nbLignes + 1;
                        }
                        else
                        {
                            MessageBox.Show("Le collage s'arrete car à la ligne n°" + nbLignes + ", le nombre de colonne n'est pas bon, il doit y en avoir 4 (Référence, désignation, Quantité, Prix unitaire).", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                            return null;
                        }
                    }
                }
                return toReturn;
            }
            else
            {
                return null;
            }
        }

        public ObservableCollection<Bon_Livraison_Contenu_Commande_Supplementaire> PasteDataBonLivraisonWindow()
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                ObservableCollection<Bon_Livraison_Contenu_Commande_Supplementaire> toReturn = new ObservableCollection<Bon_Livraison_Contenu_Commande_Supplementaire>();
                String data = Clipboard.GetText();
                ObservableCollection<String> listLignes = new ObservableCollection<string>(data.Split('\n'));
                int nbLignes = 1;
                int ligneToIgnore = listLignes.Count();
                foreach (String ligne in listLignes)
                {
                    if (nbLignes != ligneToIgnore)
                    {
                        if (this.verifNombreColonnes(ligne, 4))
                        {
                            ObservableCollection<String> listColonnes = new ObservableCollection<string>(ligne.Split('\t'));
                            int i = 1;
                            Bon_Livraison_Contenu_Commande_Supplementaire toAdd = new Bon_Livraison_Contenu_Commande_Supplementaire();
                            foreach (String colonne in listColonnes)
                            {
                                if (i == 1)
                                {
                                    toAdd.Reference = colonne;
                                }
                                if (i == 2)
                                {
                                    toAdd.Designation = colonne;
                                }
                                if (i == 3)
                                {
                                    double val;
                                    if (double.TryParse(colonne.Replace(".", ","), out val))
                                    {
                                        toAdd.Quantite_Livree = double.Parse(colonne.Replace(".", ","));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Le collage s'arrete car à la ligne n°" + nbLignes + ", vous essayez de rentrer une quantité qui n'est pas un chiffre", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return null;
                                    }
                                }
                                if (i == 4)
                                {
                                    double val;
                                    if (double.TryParse(colonne.Replace(".", ","), out val))
                                    {
                                        toAdd.Prix_Remise = double.Parse(colonne.Replace(".", ","));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Le collage s'arrete car à  la ligne n°" + nbLignes + ", vous essayez de rentrer un prix unitaire qui n'est pas un chiffre", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return null;
                                    }
                                }
                                toReturn.Add(toAdd);
                                i = i + 1;
                            }
                            nbLignes = nbLignes + 1;
                        }
                        else
                        {
                            MessageBox.Show("Le collage s'arrete car à la ligne n°" + nbLignes + ", le nombre de colonne n'est pas bon, il doit y en avoir 4 (Référence, désignation, Quantité, Prix unitaire).", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                            return null;
                        }
                    }
                }
                return toReturn;
            }
            else
            {
                return null;
            }
        }

        public ObservableCollection<Frais_Km> PasteDataFraisWindow()
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                ObservableCollection<Frais_Km> toReturn = new ObservableCollection<Frais_Km>();
                String data = Clipboard.GetText();
                ObservableCollection<String> listLignes = new ObservableCollection<string>(data.Split('\n'));
                int nbLignes = 1;
                int ligneToIgnore = listLignes.Count();
                foreach (String ligne in listLignes)
                {
                    if (nbLignes != ligneToIgnore)
                    {
                        if (this.verifNombreColonnes(ligne, 6))
                        {
                            ObservableCollection<String> listColonnes = new ObservableCollection<string>(ligne.Split('\t'));
                            int i = 1;
                            Frais_Km toAdd = new Frais_Km();

                            foreach (String colonne in listColonnes)
                            {
                                if (i == 1)
                                {
                                    //on ne colle pas l'affaire
                                }
                                if (i == 2)
                                {
                                    bool val;
                                    bool.TryParse(colonne, out val);
                                    toAdd.ImputerAffaire = val;
                                }
                                if (i == 3)
                                {
                                    toAdd.Libelle = colonne;
                                }
                                if (i == 4)
                                {
                                    //one ne colle pas la puissance fiscale
                                }
                                if (i == 5)
                                {
                                    double val;
                                    if (double.TryParse(colonne, out val))
                                    {
                                        toAdd.Km = double.Parse(colonne.Replace(".", ","));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Le collage s'arrete car à  la ligne n°" + nbLignes + ", vous essayez de rentrer un Kilomètre erroner", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return null;
                                    }
                                }

                                if (i == 6)
                                {
                                    double val;
                                    if (double.TryParse(colonne, out val))
                                    {
                                        toAdd.Base = double.Parse(colonne.Replace(".", ","));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Le collage s'arrete car à  la ligne n°" + nbLignes + ", vous essayez de rentrer une Base de chevaux fiscaux erroner", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return null;
                                    }
                                }
                                toReturn.Add(toAdd);
                                i = i + 1;
                            }
                            nbLignes = nbLignes + 1;
                        }
                        else
                        {
                            MessageBox.Show("Le collage s'arrete car à la ligne n°" + nbLignes + ", le nombre de colonne n'est pas bon, il doit y en avoir 5 (Affaire,Imputer sur affaire,Libellé,Km,Base).", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                            return null;
                        }
                    }
                }
                return toReturn;
            }
            else
            {
                return null;
            }
        }

        public ObservableCollection<Ligne_Fiche_Frais> PasteDataLigneFraisWindow(bool admin, bool etranger)
        {

            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                int lignesuivantAffichage = 8;

                if (admin)
                {
                    if (etranger)
                    {
                        lignesuivantAffichage = 14;
                    }
                    else
                    {
                        lignesuivantAffichage = 12;
                    }
                }
                else
                {
                    if (etranger)
                    {
                        lignesuivantAffichage = 10;
                    }
                }

                ObservableCollection<Ligne_Fiche_Frais> toReturn = new ObservableCollection<Ligne_Fiche_Frais>();
                String data = Clipboard.GetText();
                ObservableCollection<String> listLignes = new ObservableCollection<string>(data.Split('\n'));
                int nbLignes = 1;
                int ligneToIgnore = listLignes.Count();
                foreach (String ligne in listLignes)
                {
                    if (nbLignes != ligneToIgnore)
                    {

                        if (this.verifNombreColonnes(ligne, lignesuivantAffichage))
                        {
                            ObservableCollection<String> listColonnes = new ObservableCollection<string>(ligne.Split('\t'));
                            int i = 1;
                            Ligne_Fiche_Frais toAdd = new Ligne_Fiche_Frais();

                            foreach (String colonne in listColonnes)
                            {


                                switch (lignesuivantAffichage)
                                {
                                    case 8://pas administrateur, etranger non coché; caché : TTC Euro, Taux de change, Taux de change réel, Commentaire, Plan Comptable Imputation, Plan Comptable Tva
                                        if (i == 1)
                                        {
                                            //on ne colle pas Client
                                        }
                                        if (i == 2)
                                        {
                                            //on le colle pas Affaire
                                        }
                                        if (i == 3)
                                        {
                                            bool val;
                                            if (bool.TryParse(colonne, out val))
                                            {
                                                toAdd.Imputer_Affaire = val;
                                            }
                                        }
                                        if (i == 4)
                                        {
                                            if (!String.IsNullOrEmpty(colonne))
                                            {
                                                toAdd.Libelle = colonne;
                                            }
                                        }
                                        if (i == 5)
                                        {
                                            //on ne colle pas Type de Frais
                                        }
                                        if (i == 6)
                                        {
                                            //on colle le TTC sur Ticket
                                            bool val;
                                            double res;
                                            val = Double.TryParse(colonne, out res);
                                            if (val == true)
                                            {
                                                toAdd.TTC_Sur_Ticket = res;
                                            }
                                        }
                                        if (i == 7)
                                        {
                                            //on ne colle pas la checkbox Etranger
                                        }
                                        if (i == 8)
                                        {
                                            //on ne colle pas Tva récupérable
                                        }
                                        break;

                                    case 10://pas administrateur, etranger coché; caché : Taux de change réel, Plan Comptable Imputation, Plan Comptable Tva, Commentaire
                                        if (i == 1)
                                        {
                                            //on ne colle pas Client
                                        }
                                        if (i == 2)
                                        {
                                            //on ne colle pas le N° d'affaire
                                        }
                                        if (i == 3)
                                        {
                                            bool res;
                                            if (bool.TryParse(colonne, out res) == true)
                                            {
                                                toAdd.Imputer_Affaire = res;
                                            }
                                        }
                                        if (i == 4)
                                        {
                                            if (!String.IsNullOrEmpty(colonne))
                                            {
                                                toAdd.Libelle = colonne;
                                            }
                                        }
                                        if (i == 5)
                                        {
                                            //on ne colle pas Type de Frais
                                        }
                                        if (i == 6)
                                        {
                                            //on colle le TTC sur Ticket
                                            bool val;
                                            double res;
                                            val = Double.TryParse(colonne, out res);
                                            if (val == true)
                                            {
                                                toAdd.TTC_Sur_Ticket = res;
                                            }
                                        }

                                        if (i == 7)
                                        {
                                            //on colle la checkbox Etranger
                                            bool res;
                                            if (bool.TryParse(colonne, out res) == true)
                                            {
                                                toAdd.Etranger = res;
                                            }
                                        }
                                        if (i == 8)
                                        {
                                            //on ne colle pas la TTC en Euro
                                        }
                                        if (i == 9)
                                        {
                                            //on colle le Taux de change
                                            bool val;
                                            double res;
                                            val = Double.TryParse(colonne, out res);
                                            if (val == true)
                                            {
                                                toAdd.Taux_Change = res;
                                            }
                                        }
                                        if (i == 10)
                                        {
                                            //on ne colle pas Tva récupérable
                                        }
                                        break;



                                    case 12://administrateur, etranger non coché; caché : TTC en Euro, Taux de change réel, Taux de change
                                        if (i == 1)
                                        {
                                            //on ne colle pas Client
                                        }
                                        if (i == 2)
                                        {
                                            //on ne colle pas le N° d'affaire
                                        }
                                        if (i == 3)
                                        {
                                            bool val;
                                            if (bool.TryParse(colonne, out val))
                                            {
                                                toAdd.Imputer_Affaire = val;
                                            }
                                        }
                                        if (i == 4)
                                        {
                                            if (!String.IsNullOrEmpty(colonne))
                                            {
                                                toAdd.Libelle = colonne;
                                            }
                                        }
                                        if (i == 5)
                                        {
                                            //on ne colle pas Type de Frais
                                        }
                                        if (i == 6)
                                        {
                                            //on colle le TTC sur Ticket
                                            bool val;
                                            double res;
                                            val = Double.TryParse(colonne, out res);
                                            if (val == true)
                                            {
                                                toAdd.TTC_Sur_Ticket = res;
                                            }
                                        }
                                        if (i == 7)
                                        {
                                            //on colle la checkbox Etranger
                                            bool res;
                                            if (bool.TryParse(colonne, out res) == true)
                                            {
                                                toAdd.Etranger = res;
                                            }

                                        }
                                        if (i == 8)
                                        {
                                            //on colle le Taux de change reel
                                            bool val;
                                            double res;
                                            val = Double.TryParse(colonne, out res);
                                            if (val == true)
                                            {
                                                toAdd.Taux_Change_Reel = res;
                                            }
                                        }
                                        if (i == 9)
                                        {
                                            //on ne colle pas la TVA Recuperable
                                        }
                                        if (i == 10)
                                        {
                                            //on ne colle pas le Plan Comptable Tva
                                        }
                                        if (i == 11)
                                        {
                                            //on ne colle pas le Plan Comptable Imputation
                                        }
                                        if (i == 12)
                                        {
                                            if (colonne != null && !String.IsNullOrEmpty(colonne))
                                            {
                                                toAdd.Commentaire = colonne;
                                            }
                                        }
                                        break;

                                    case 14:
                                        if (i == 1)
                                        {
                                            //on ne colle pas Client

                                        }
                                        if (i == 2)
                                        {
                                            //on le colle pas Affaire
                                        }
                                        if (i == 3)
                                        {
                                            bool val;
                                            if (bool.TryParse(colonne, out val))
                                            {
                                                toAdd.Imputer_Affaire = val;
                                            }
                                        }
                                        if (i == 4)
                                        {
                                            if (!String.IsNullOrEmpty(colonne))
                                            {
                                                toAdd.Libelle = colonne;
                                            }
                                        }
                                        if (i == 5)
                                        {
                                            //on ne colle pas Type de Frais
                                        }

                                        if (i == 6)
                                        {
                                            //on colle le TTC sur Ticket
                                            bool val;
                                            double res;
                                            val = Double.TryParse(colonne, out res);
                                            if (val == true)
                                            {
                                                toAdd.TTC_Sur_Ticket = res;
                                            }
                                        }
                                        if (i == 7)
                                        {
                                            //on colle la checkbox Etranger
                                            bool res;
                                            if (bool.TryParse(colonne, out res) == true)
                                            {
                                                toAdd.Etranger = res;
                                            }

                                        }
                                        if (i == 8)
                                        {
                                            //on ne colle pas le TTC en Euro

                                        }
                                        if (i == 9)
                                        {
                                            //on colle le Taux de change
                                            bool val;
                                            double res;
                                            val = Double.TryParse(colonne, out res);
                                            if (val == true)
                                            {
                                                toAdd.Taux_Change = res;
                                            }
                                        }
                                        if (i == 10)
                                        {
                                            //on colle le Taux de change reel
                                            bool val;
                                            double res;
                                            val = Double.TryParse(colonne, out res);
                                            if (val == true)
                                            {
                                                toAdd.Taux_Change_Reel = res;
                                            }
                                        }
                                        if (i == 11)
                                        {
                                            //on ne colle pas la TVA Recuperable
                                        }
                                        if (i == 12)
                                        {
                                            //on ne colle pas le Plan Comptable Tva
                                        }
                                        if (i == 13)
                                        {
                                            //on ne colle pas le Plan Comptable Imputation
                                        }
                                        if (i == 14)
                                        {
                                            if (colonne != null && !String.IsNullOrEmpty(colonne))
                                            {
                                                toAdd.Commentaire = colonne;
                                            }
                                        }
                                        break;

                                    default: toAdd = new Ligne_Fiche_Frais();
                                        break;
                                }

                                toReturn.Add(toAdd);
                                i = i + 1;
                            }
                            nbLignes = nbLignes + 1;
                        }
                        else
                        {
                            MessageBox.Show("Le collage s'arrete car à la ligne n°" + nbLignes + ", le nombre de colonne n'est pas bon.", "Coller : erreur !", MessageBoxButton.OK, MessageBoxImage.Error);
                            return null;
                        }
                    }
                }
                return toReturn;
            }
            else
            {
                return null;
            }
        }

        private bool verifNombreColonnes(String toTest, int nombre)
        {
            int result = 0;
            foreach (char c in toTest)
            {
                if (c == '\t')
                {
                    result = result + 1;
                }
            }
            if ((result + 1) == nombre)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
