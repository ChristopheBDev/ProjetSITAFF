using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour SelectionDevisWindow.xaml
    /// </summary>
    public partial class SelectionDevisWindow : Window
    {

        public Devis devis;

        public ObservableCollection<Devis> listDevis
        {
            get { return (ObservableCollection<Devis>)GetValue(listDevisProperty); }
            set { SetValue(listDevisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listDevis.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listDevisProperty =
            DependencyProperty.Register("listDevis", typeof(ObservableCollection<Devis>), typeof(SelectionDevisWindow), new UIPropertyMetadata(null));

        

        public SelectionDevisWindow()
        {
            InitializeComponent();
            this.listDevis = new ObservableCollection<Devis>(((App)App.Current).mySitaffEntities.Devis.OrderBy(dev => dev.Numero));
        }

        private void _ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBox.SelectedItem != null)
            {
                this.devis = (Devis)this._ComboBox.SelectedItem;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un devis.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void _ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
