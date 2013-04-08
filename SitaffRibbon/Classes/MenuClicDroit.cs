using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SitaffRibbon.Classes
{
    public class MenuClicDroit
    {
        public ContextMenu creationBaseMenuClicDroit()
        {
            ContextMenu contextMenu = new ContextMenu();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorToPut = "#A3D0D8E8";
            Brush colorMenu = (Brush)converter.ConvertFrom(colorToPut);
            contextMenu.Background = colorMenu;
            return contextMenu;
        }

        public ContextMenu creationMenuClicDroitMain(UserControl usercontrol)
        {
            ContextMenu contextMenu = this.creationBaseMenuClicDroit();

            if (((App)App.Current).securite.VerificationDroitActionsCRUD(usercontrol.ToString(), "Look"))
            {
                MenuItem itemAfficher = new MenuItem();
                itemAfficher.Header = "Afficher";
                itemAfficher.Click += new RoutedEventHandler(delegate { ((App)App.Current)._theMainWindow._CommandLook.Command.Execute(((App)App.Current)._theMainWindow); });

                contextMenu.Items.Add(itemAfficher);
            }
            if (((App)App.Current).securite.VerificationDroitActionsCRUD(usercontrol.ToString(), "Add"))
            {
                MenuItem itemAfficher = new MenuItem();
                itemAfficher.Header = "Ajouter";
                itemAfficher.Click += new RoutedEventHandler(delegate { ((App)App.Current)._theMainWindow._CommandAdd.Command.Execute(((App)App.Current)._theMainWindow); });

                contextMenu.Items.Add(itemAfficher);
            }
            if (((App)App.Current).securite.VerificationDroitActionsCRUD(usercontrol.ToString(), "Update"))
            {
                MenuItem itemAfficher = new MenuItem();
                itemAfficher.Header = "Modifier";
                itemAfficher.Click += new RoutedEventHandler(delegate { ((App)App.Current)._theMainWindow._CommandUpdate.Command.Execute(((App)App.Current)._theMainWindow); });

                contextMenu.Items.Add(itemAfficher);
            }
            if (((App)App.Current).securite.VerificationDroitActionsCRUD(usercontrol.ToString(), "Remove"))
            {
                MenuItem itemAfficher = new MenuItem();
                itemAfficher.Header = "Supprimer";
                itemAfficher.Click += new RoutedEventHandler(delegate { ((App)App.Current)._theMainWindow._CommandDelete.Command.Execute(((App)App.Current)._theMainWindow); });

                contextMenu.Items.Add(itemAfficher);
            }

            return contextMenu;
        }

        public ContextMenu creationMenuClicDroitParameters(UserControl usercontrol)
        {
            ContextMenu contextMenu = this.creationBaseMenuClicDroit();

            if (((App)App.Current).securite.VerificationDroitActionsCRUDParameters(usercontrol.ToString(), "Look"))
            {
                MenuItem itemAfficher = new MenuItem();
                itemAfficher.Header = "Afficher";
                itemAfficher.Click += new RoutedEventHandler(delegate { ((App)App.Current)._theMainWindow.parametreMain._CommandLook.Command.Execute(((App)App.Current)._theMainWindow.parametreMain); });

                contextMenu.Items.Add(itemAfficher);
            }
            if (((App)App.Current).securite.VerificationDroitActionsCRUDParameters(usercontrol.ToString(), "Add"))
            {
                MenuItem itemAfficher = new MenuItem();
                itemAfficher.Header = "Ajouter";
                itemAfficher.Click += new RoutedEventHandler(delegate { ((App)App.Current)._theMainWindow.parametreMain._CommandAdd.Command.Execute(((App)App.Current)._theMainWindow.parametreMain); });

                contextMenu.Items.Add(itemAfficher);
            }
            if (((App)App.Current).securite.VerificationDroitActionsCRUDParameters(usercontrol.ToString(), "Update"))
            {
                MenuItem itemAfficher = new MenuItem();
                itemAfficher.Header = "Modifier";
                itemAfficher.Click += new RoutedEventHandler(delegate { ((App)App.Current)._theMainWindow.parametreMain._CommandUpdate.Command.Execute(((App)App.Current)._theMainWindow.parametreMain); });

                contextMenu.Items.Add(itemAfficher);
            }
            if (((App)App.Current).securite.VerificationDroitActionsCRUDParameters(usercontrol.ToString(), "Remove"))
            {
                MenuItem itemAfficher = new MenuItem();
                itemAfficher.Header = "Supprimer";
                itemAfficher.Click += new RoutedEventHandler(delegate { ((App)App.Current)._theMainWindow.parametreMain._CommandDelete.Command.Execute(((App)App.Current)._theMainWindow.parametreMain); });

                contextMenu.Items.Add(itemAfficher);
            }

            return contextMenu;
        }

        public MenuItem creationAfficherMasquer(ObservableCollection<DataGridColumn> collectionColumns)
        {
            MenuItem contextMenuTmp = new MenuItem();
            contextMenuTmp.Header = "Afficher / Masquer";

            foreach (DataGridColumn item in collectionColumns)
            {
                MenuItem menuItem = new MenuItem();
                if (item.Visibility == Visibility.Visible)
                {
                    menuItem.IsChecked = false;
                }
                else
                {
                    menuItem.IsChecked = true;
                }
                menuItem.Header = item.Header;
                menuItem.Click += new RoutedEventHandler(delegate { ((App)App.Current)._afficherMasquer.AffMas_Colonne(menuItem, item); });
                ((App)App.Current)._afficherMasquer.AffMas_Colonne(menuItem, item);

                contextMenuTmp.Items.Add(menuItem);
            }

            return contextMenuTmp;
        }
    }
}
