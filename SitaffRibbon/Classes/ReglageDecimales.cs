using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;

namespace SitaffRibbon.Classes
{
    class ReglageDecimales
    {

        public void Reglage_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            bool test = true;
            //On remplace tout "." éventuel qui viendrait malheureusement se glisser dans le champ par une ","
            if (((TextBox)sender).Text.Contains("."))
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.Replace(".", ",");
                test = false;
            }

            // On replace le curseur en fin de saisie, sinon il reste devant la virgule fraichement remplaçée
            if (!test)
            {
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
        }

        public void Reglage_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool test = true;
            //On remplace tout "." éventuel qui viendrait malheureusement se glisser dans le champ par une ","
            if (((TextBox)sender).Text.Contains("."))
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.Replace(".", ",");
                test = false;
            }

            // On replace le curseur en fin de saisie, sinon il reste devant la virgule fraichement remplaçée
            if (!test)
            {
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
        }


        //Code à copier pour les datagrids
        private void _DataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Tab)
                {
                    ReglageDecimales reg = new ReglageDecimales();
                    switch ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString())
                    {
                        case "Qté facturée":
                            reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                            break;
                        default:
                            break;
                    }
                }
                //if ((((DataGridTextColumn)((DataGridCell)((TextBox)e.OriginalSource).Parent).Column)).Header.ToString() == "Réf fournisseur")
                //{

                //    //MessageBox.Show("A faire");
                //    reg.Reglage_TextBox_KeyUp((TextBox)e.OriginalSource, e);
                //    MessageBox.Show("Fait");                    
                //}
            }
            catch (Exception)
            {
            }
        }
    }
}
