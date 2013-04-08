using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.ObjectModel;

namespace SitaffRibbon
{
    public partial class Releve_Heure_Atelier
    {

        /// <summary>
        /// Renvoi le total de la semaine en heures
        /// </summary>
        public double Total_Semaine
        {
            get
            {
                return Total_Lundi + Total_Mardi + Total_Mercredi + Total_Jeudi + Total_Vendredi + Total_Samedi;
            }
        }

        public ObservableCollection<RecapHeureAtelier> listAffairesRegies
        {
            get
            {
                ObservableCollection<RecapHeureAtelier> toReturn = new ObservableCollection<RecapHeureAtelier>();

                foreach (Heure_Atelier item in this.Heure_Atelier)
                {
                    bool test = false;
                    RecapHeureAtelier tmp = null;

                    foreach (RecapHeureAtelier rha in toReturn)
                    {
                        if (item.Regie1 == rha.regie && item.Affaire1 == rha.affaire)
                        {
                            test = true;
                            tmp = rha;
                        }
                    }

                    double theTotal = item.Heures_Lundi + item.Heures_Mardi + item.Heures_Mercredi + item.Heures_Jeudi + item.Heures_Vendredi + item.Heures_Samedi;

                    if (test)
                    {
                        tmp.totalHeures += theTotal;
                    }
                    else
                    {
                        RecapHeureAtelier temp = new RecapHeureAtelier(item.Affaire1, item.Regie1, theTotal);
                        toReturn.Add(temp);
                    }

                }

                return toReturn;
            }
        }

        public ObservableCollection<RecapTravauxAtelier> listTravaux
        {
            get
            {
                ObservableCollection<RecapTravauxAtelier> toReturn = new ObservableCollection<RecapTravauxAtelier>();

                foreach (Heure_Atelier_Autre item in this.Heure_Atelier_Autre)
                {
                    bool test = false;
                    RecapTravauxAtelier tmp = null;

                    foreach (RecapTravauxAtelier rha in toReturn)
                    {
                        if (item.Tache_Atelier1 == rha.tache_atelier)
                        {
                            test = true;
                            tmp = rha;
                        }
                    }

                    double theTotal = item.Heures_Lundi + item.Heures_Mardi + item.Heures_Mercredi + item.Heures_Jeudi + item.Heures_Vendredi + item.Heures_Samedi;

                    if (test)
                    {
                        tmp.totalHeures += theTotal;
                    }
                    else
                    {
                        RecapTravauxAtelier temp = new RecapTravauxAtelier(item.Tache_Atelier1, theTotal);
                        toReturn.Add(temp);
                    }

                }

                return toReturn;
            }
        }

    }
}
