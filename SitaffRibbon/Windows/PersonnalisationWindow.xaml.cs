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
using System.ComponentModel;
using Microsoft.Win32;
using SitaffRibbon.Classes;

namespace SitaffRibbon.Windows
{
    /// <summary>
    /// Logique d'interaction pour PersonnalisationWindow.xaml
    /// </summary>
    public partial class PersonnalisationWindow : Window
    {
        #region constructeur

        public PersonnalisationWindow()
        {
            InitializeComponent();

            ((App)App.Current).personnalisation.initWindows(this);

            this.ComboBox_FontFamilyBackGroundLogScreen.ItemsSource = Fonts.SystemFontFamilies.OrderBy(fon => fon.ToString());
            this.ComboBox_FontFamilyWindow.ItemsSource = Fonts.SystemFontFamilies.OrderBy(fon => fon.ToString());
            this.ComboBox_FontFamilyUserControl.ItemsSource = Fonts.SystemFontFamilies.OrderBy(fon => fon.ToString());

            this.initialisation();
        }

        #endregion

        #region initialisateurs

        public void initialisation()
        {
            this.initColorsTest();
            this.initTextTest();
            this.initDoubleTest();
            this.initStyleVide();
        }

        public void initColorsTest()
        {
            //Log Screen
            this._colorToTestBackGroundLogScreen.Background = ((App)App.Current).personnalisation.BackGroundLogScreenColor;
            this._colorToTestMoveLogScreen.Background = new SolidColorBrush(((App)App.Current).personnalisation.ColorMove);
            this._colorToTestFixeLogScreen.Background = new SolidColorBrush(((App)App.Current).personnalisation.ColorFixe);

            //Windows
            this._colorToTestWindow.Background = ((App)App.Current).personnalisation.BackGroundWindowColor;

            //UserControl
            this._colorToTestUserControlFilterZone.Background = ((App)App.Current).personnalisation.BackGroundUserControlFilterColor;
            this._colorToTestUserControlDataGrid.Background = ((App)App.Current).personnalisation.BackGroundUserControlDataGridColor;
            this._colorToTestUserControlDataGridAlternative.Background = ((App)App.Current).personnalisation.BackGroundUserControlDataGridAlternateColor;
        }

        public void initTextTest()
        {
            //Log Screen
            this.FontFamilyToTestBackGroundLogScreen.FontFamily = ((App)App.Current).personnalisation.FontFamilyBackGroundLogScreen;
            this.ComboBox_FontFamilyBackGroundLogScreen.SelectedItem = ((App)App.Current).personnalisation.FontFamilyBackGroundLogScreen;

            //Windows
            this.FontFamilyToTestWindow.FontFamily = ((App)App.Current).personnalisation.FontFamilyWindow;
            this.ComboBox_FontFamilyWindow.SelectedItem = ((App)App.Current).personnalisation.FontFamilyWindow;

            //UserControl
            this.FontFamilyToTestUserControl.FontFamily = ((App)App.Current).personnalisation.FontFamilyUserControl;
            this.ComboBox_FontFamilyUserControl.SelectedItem = ((App)App.Current).personnalisation.FontFamilyUserControl;
        }

        public void initDoubleTest()
        {
            //Log Screen
            this._doubleUpDownFontSizeLogScreen.Text = ((App)App.Current).personnalisation.FontSizeLogScreen.ToString();
            this.FontSizeToTestLogScreen.FontSize = ((App)App.Current).personnalisation.FontSizeLogScreen;

            //Window
            this._doubleUpDownFontSizeWindow.Text = ((App)App.Current).personnalisation.FontSizeWindow.ToString();
            this.FontSizeToTestWindow.FontSize = ((App)App.Current).personnalisation.FontSizeWindow;

            //UserControl
            this._doubleUpDownFontSizeUserControl.Text = ((App)App.Current).personnalisation.FontSizeUserControl.ToString();
            this.FontSizeToTestUserControl.FontSize = ((App)App.Current).personnalisation.FontSizeUserControl;
        }

        public void initStyleVide()
        {
            if (((App)App.Current).personnalisation.styleVide != null)
            {
                if (((App)App.Current).personnalisation.styleVide == "1")
                {
                    this.ComboBox_styleVide.SelectedItem = "Classique";
                }
                else if (((App)App.Current).personnalisation.styleVide == "2")
                {
                    this.ComboBox_styleVide.SelectedItem = "Ronds animés";
                }
                else if (((App)App.Current).personnalisation.styleVide == "3")
                {
                    this.ComboBox_styleVide.SelectedItem = "Cube tournant";
                }
                else if (((App)App.Current).personnalisation.styleVide == "4")
                {
                    this.ComboBox_styleVide.SelectedItem = "Gouttes de pluie";
                }                    
                else
                {
                    this.ComboBox_styleVide.SelectedItem = "Classique";
                }
            }
            else
            {
                this.ComboBox_styleVide.SelectedItem = "Classique";
            }
        }

        #endregion

        #region tab Log Screen

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Instancier une boite de dilogue de Winform
            System.Windows.Forms.ColorDialog dialogBox = new System.Windows.Forms.ColorDialog();

            // Affichage de la boite de dialogue
            if (dialogBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
                //Converti la couleur en Hexa
                ((App)App.Current).personnalisation.BackGroundLogScreenColor = (Brush)converter.ConvertFrom(string.Concat("#", (dialogBox.Color.ToArgb() & 0x00FFFFFF).ToString("X6")));

                ((App)App.Current).personnalisation.saveBackGroundLogScreenColor(string.Concat("#", (dialogBox.Color.ToArgb() & 0x00FFFFFF).ToString("X6")));
            }
            this._colorToTestBackGroundLogScreen.Background = ((App)App.Current).personnalisation.BackGroundLogScreenColor;
        }

        private void Modifier_ColorMove_Click_1(object sender, RoutedEventArgs e)
        {
            // Instancier une boite de dilogue de Winform
            System.Windows.Forms.ColorDialog dialogBox = new System.Windows.Forms.ColorDialog();

            // Affichage de la boite de dialogue
            if (dialogBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
                //Converti la couleur en Hexa
                ((App)App.Current).personnalisation.ColorMove = Color.FromArgb(dialogBox.Color.A, dialogBox.Color.R, dialogBox.Color.G, dialogBox.Color.B);

                ((App)App.Current).personnalisation.saveColorMove(string.Concat(dialogBox.Color.A, "/", dialogBox.Color.R, "/", dialogBox.Color.G, "/", dialogBox.Color.B));
            }
            this._colorToTestMoveLogScreen.Background = new SolidColorBrush(((App)App.Current).personnalisation.ColorMove);
        }

        private void Modifier_ColorFixe_Click_1(object sender, RoutedEventArgs e)
        {
            // Instancier une boite de dilogue de Winform
            System.Windows.Forms.ColorDialog dialogBox = new System.Windows.Forms.ColorDialog();

            // Affichage de la boite de dialogue
            if (dialogBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
                //Converti la couleur en Hexa
                ((App)App.Current).personnalisation.ColorFixe = Color.FromArgb(dialogBox.Color.A, dialogBox.Color.R, dialogBox.Color.G, dialogBox.Color.B);

                ((App)App.Current).personnalisation.saveColorFixe(string.Concat(dialogBox.Color.A, "/", dialogBox.Color.R, "/", dialogBox.Color.G, "/", dialogBox.Color.B));
            }
            this._colorToTestFixeLogScreen.Background = new SolidColorBrush(((App)App.Current).personnalisation.ColorFixe);
        }

        private void ComboBox_FontFamilyBackGroundLogScreen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBox_FontFamilyBackGroundLogScreen.SelectedItem != null)
            {
                ((App)App.Current).personnalisation.FontFamilyBackGroundLogScreen = ((FontFamily)this.ComboBox_FontFamilyBackGroundLogScreen.SelectedItem);
                this.FontFamilyToTestBackGroundLogScreen.FontFamily = ((FontFamily)this.ComboBox_FontFamilyBackGroundLogScreen.SelectedItem);
                ((App)App.Current).personnalisation.saveFontFamilyBackGroundLogScreen();
            }
        }

        private void DoubleUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            double test;

            if (double.TryParse(this._doubleUpDownFontSizeLogScreen.Text, out test))
            {
                if (double.Parse(this._doubleUpDownFontSizeLogScreen.Text) != 0)
                {
                    ((App)App.Current).personnalisation.FontSizeLogScreen = double.Parse(this._doubleUpDownFontSizeLogScreen.Text);
                    this.FontSizeToTestLogScreen.FontSize = double.Parse(this._doubleUpDownFontSizeLogScreen.Text);
                    ((App)App.Current).personnalisation.saveFontSizeLogScreen();
                }
            }
        }

        #endregion

        #region tab UserControl

        private void _buttonModifierColorUserControlFilterZone_Click(object sender, RoutedEventArgs e)
        {
            // Instancier une boite de dilogue de Winform
            System.Windows.Forms.ColorDialog dialogBox = new System.Windows.Forms.ColorDialog();

            // Affichage de la boite de dialogue
            if (dialogBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
                //Converti la couleur en Hexa
                ((App)App.Current).personnalisation.BackGroundUserControlFilterColor = (Brush)converter.ConvertFrom(string.Concat("#", (dialogBox.Color.ToArgb() & 0x00FFFFFF).ToString("X6")));

                ((App)App.Current).personnalisation.saveBackGroundUserControlFilterColor(string.Concat("#", (dialogBox.Color.ToArgb() & 0x00FFFFFF).ToString("X6")));
            }
            this._colorToTestUserControlFilterZone.Background = ((App)App.Current).personnalisation.BackGroundUserControlFilterColor;
        }

        private void _buttonModifierColorUserControlDataGrid_Click(object sender, RoutedEventArgs e)
        {
            // Instancier une boite de dilogue de Winform
            System.Windows.Forms.ColorDialog dialogBox = new System.Windows.Forms.ColorDialog();

            // Affichage de la boite de dialogue
            if (dialogBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
                //Converti la couleur en Hexa
                ((App)App.Current).personnalisation.BackGroundUserControlDataGridColor = (Brush)converter.ConvertFrom(string.Concat("#", (dialogBox.Color.ToArgb() & 0x00FFFFFF).ToString("X6")));

                ((App)App.Current).personnalisation.saveBackGroundUserControlDataGridColor(string.Concat("#", (dialogBox.Color.ToArgb() & 0x00FFFFFF).ToString("X6")));
            }
            this._colorToTestUserControlDataGrid.Background = ((App)App.Current).personnalisation.BackGroundUserControlDataGridColor;
        }

        private void _buttonModifierColorUserControlDataGridAlternative_Click(object sender, RoutedEventArgs e)
        {
            // Instancier une boite de dilogue de Winform
            System.Windows.Forms.ColorDialog dialogBox = new System.Windows.Forms.ColorDialog();

            // Affichage de la boite de dialogue
            if (dialogBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
                //Converti la couleur en Hexa
                ((App)App.Current).personnalisation.BackGroundUserControlDataGridAlternateColor = (Brush)converter.ConvertFrom(string.Concat("#", (dialogBox.Color.ToArgb() & 0x00FFFFFF).ToString("X6")));

                ((App)App.Current).personnalisation.saveBackGroundUserControlDataGridAlternateColor(string.Concat("#", (dialogBox.Color.ToArgb() & 0x00FFFFFF).ToString("X6")));
            }
            this._colorToTestUserControlDataGridAlternative.Background = ((App)App.Current).personnalisation.BackGroundUserControlDataGridAlternateColor;
        }

        private void ComboBox_FontFamilyUserControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBox_FontFamilyUserControl.SelectedItem != null)
            {
                ((App)App.Current).personnalisation.FontFamilyUserControl = ((FontFamily)this.ComboBox_FontFamilyUserControl.SelectedItem);
                this.FontFamilyToTestUserControl.FontFamily = ((FontFamily)this.ComboBox_FontFamilyUserControl.SelectedItem);
                ((App)App.Current).personnalisation.saveFontFamilyUserControl();
            }
        }

        private void _doubleUpDownFontSizeUserControl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            double test;

            if (double.TryParse(this._doubleUpDownFontSizeUserControl.Text, out test))
            {
                if (double.Parse(this._doubleUpDownFontSizeUserControl.Text) != 0)
                {
                    ((App)App.Current).personnalisation.FontSizeUserControl = double.Parse(this._doubleUpDownFontSizeUserControl.Text);
                    this.FontSizeToTestUserControl.FontSize = double.Parse(this._doubleUpDownFontSizeUserControl.Text);
                    ((App)App.Current).personnalisation.saveFontSizeUserControl();
                }
            }
        }

        private void ComboBox_styleVide_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBox_styleVide.SelectedItem != null)
            {
                //Classique : 1
                //Ronds animés : 2
                //Cube tournant : 3
                //Gouttes de pluie : 4
                try
                {
                    if (((string)this.ComboBox_styleVide.SelectedItem) == "Classique")
                    {
                        ((App)App.Current).personnalisation.styleVide = "1";
                        ((App)App.Current).personnalisation.savestyleVide();
                    }
                    if (((string)this.ComboBox_styleVide.SelectedItem) == "Ronds animés")
                    {
                        ((App)App.Current).personnalisation.styleVide = "2";
                        ((App)App.Current).personnalisation.savestyleVide();
                    }
                    if (((string)this.ComboBox_styleVide.SelectedItem) == "Cube tournant")
                    {
                        ((App)App.Current).personnalisation.styleVide = "3";
                        ((App)App.Current).personnalisation.savestyleVide();
                    }
                    if (((string)this.ComboBox_styleVide.SelectedItem) == "Gouttes de pluie")
                    {
                        ((App)App.Current).personnalisation.styleVide = "4";
                        ((App)App.Current).personnalisation.savestyleVide();
                    }                    
                }
                catch (Exception)
                {
                    ((App)App.Current).personnalisation.styleVide = "1";
                    ((App)App.Current).personnalisation.savestyleVide();
                }

            }
        }

        #endregion

        #region tab Window

        private void _doubleUpDownFontSizeWindow_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            double test;

            if (double.TryParse(this._doubleUpDownFontSizeWindow.Text, out test))
            {
                if (double.Parse(this._doubleUpDownFontSizeWindow.Text) != 0)
                {
                    ((App)App.Current).personnalisation.FontSizeWindow = double.Parse(this._doubleUpDownFontSizeWindow.Text);
                    this.FontSizeToTestWindow.FontSize = double.Parse(this._doubleUpDownFontSizeWindow.Text);
                    ((App)App.Current).personnalisation.saveFontSizeWindow();
                }
            }
        }

        private void ComboBox_FontFamilyWindow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBox_FontFamilyWindow.SelectedItem != null)
            {
                ((App)App.Current).personnalisation.FontFamilyWindow = ((FontFamily)this.ComboBox_FontFamilyWindow.SelectedItem);
                this.FontFamilyToTestWindow.FontFamily = ((FontFamily)this.ComboBox_FontFamilyWindow.SelectedItem);
                ((App)App.Current).personnalisation.saveFontFamilyWindow();
            }
        }

        private void _buttonModifierColorWindow_Click(object sender, RoutedEventArgs e)
        {
            // Instancier une boite de dilogue de Winform
            System.Windows.Forms.ColorDialog dialogBox = new System.Windows.Forms.ColorDialog();

            // Affichage de la boite de dialogue
            if (dialogBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
                //Converti la couleur en Hexa
                ((App)App.Current).personnalisation.BackGroundWindowColor = (Brush)converter.ConvertFrom(string.Concat("#", (dialogBox.Color.ToArgb() & 0x00FFFFFF).ToString("X6")));

                ((App)App.Current).personnalisation.saveBackGroundWindowColor(string.Concat("#", (dialogBox.Color.ToArgb() & 0x00FFFFFF).ToString("X6")));
            }
            this._colorToTestWindow.Background = ((App)App.Current).personnalisation.BackGroundWindowColor;
        }

        #endregion

        #region Bouton général

        private void _ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        private void _Reset_Click(object sender, RoutedEventArgs e)
        {
            ((App)App.Current).personnalisation.ResetPersonnalisation();
            ((App)App.Current).personnalisation = new Personnalisation();
            ((App)App.Current).personnalisation.initWindows(this);
            this.initialisation();
        }

    }
}
