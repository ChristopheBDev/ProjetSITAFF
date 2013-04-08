using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SitaffRibbon.Classes
{
    public class Personnalisation
    {
        #region Variables

        #region LogScreen

        public Brush BackGroundLogScreenColor = Brushes.Aqua;
        public Color ColorMove = Colors.White;
        public Color ColorFixe = Colors.Black;
        public FontFamily FontFamilyBackGroundLogScreen = Fonts.SystemFontFamilies.FirstOrDefault(fon => fon.ToString().Contains("Antiqua"));
        public double FontSizeLogScreen = 12;

        #endregion

        #region Window

        public Brush BackGroundWindowColor = Brushes.WhiteSmoke;
        public FontFamily FontFamilyWindow = Fonts.SystemFontFamilies.FirstOrDefault(fon => fon.ToString().Contains("Antiqua"));
        public double FontSizeWindow = 12;

        #endregion

        #region UserControl

        public Brush BackGroundUserControlFilterColor = (Brush)(TypeDescriptor.GetConverter(typeof(Brush))).ConvertFrom("#FFDFE9F5");
        public Brush BackGroundUserControlDataGridColor = Brushes.White;
        public Brush BackGroundUserControlDataGridAlternateColor = (Brush)(TypeDescriptor.GetConverter(typeof(Brush))).ConvertFrom("#FFE9EDF4");
        public FontFamily FontFamilyUserControl = Fonts.SystemFontFamilies.FirstOrDefault(fon => fon.ToString().Contains("Antiqua"));
        public double FontSizeUserControl = 12;
        public string styleVide = "1";

        #endregion

        #endregion

        public Personnalisation()
        {
            this.initBackGroundLogScreenColor();
            this.initFontFamilyBackGroundLogScreen();
            this.initFontSizeLogScreen();
            this.initColorMove();
            this.initColorFixe();

            this.initBackGroundWindowColor();
            this.initFontFamilyWindow();
            this.initFontSizeWindow();

            this.initBackGroundUserControlDataGridAlternateColor();
            this.initBackGroundUserControlDataGridColor();
            this.initBackGroundUserControlFilterColor();
            this.initFontFamilyUserControl();
            this.initFontSizeUserControl();
            this.initstyleVide();
        }

        #region LogScreen

        public void InitLogScreen(Window window)
        {
            window.Background = this.BackGroundLogScreenColor;
            window.FontFamily = this.FontFamilyBackGroundLogScreen;
            window.FontSize = this.FontSizeLogScreen;
            //Les couleurs de l'animation sont mise à l'intérieur de la fenêtre
        }

        #region FontFamilyBackGroundLogScreen

        public void initFontFamilyBackGroundLogScreen()
        {
            string textFont = "Antiqua";

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
            }
            else
            {
                textFont = (String)regVersion.GetValue("FontFamilyBackGroundLogScreen", textFont);
            }

            try
            {
                this.FontFamilyBackGroundLogScreen = Fonts.SystemFontFamilies.FirstOrDefault(fon => fon.ToString().Contains(textFont));
            }
            catch (Exception)
            {
                try
                {
                    this.FontFamilyBackGroundLogScreen = Fonts.SystemFontFamilies.FirstOrDefault(fon => fon.ToString().Contains("Antiqua"));
                }
                catch (Exception)
                {
                    this.FontFamilyBackGroundLogScreen = Fonts.SystemFontFamilies.FirstOrDefault(fon => fon.ToString().Contains("Comic"));
                }
            }
            regVersion.Close();

            this.saveFontFamilyBackGroundLogScreen();
        }

        public void saveFontFamilyBackGroundLogScreen()
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            regVersion.SetValue("FontFamilyBackGroundLogScreen", this.FontFamilyBackGroundLogScreen.ToString());
            regVersion.Close();
        }

        #endregion

        #region BackGroundLogScreenColor

        public void initBackGroundLogScreenColor()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexa = "";

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
            }
            else
            {
                colorHexa = (String)regVersion.GetValue("BackGroundLogScreenColor", colorHexa);
            }

            try
            {
                this.BackGroundLogScreenColor = (Brush)converter.ConvertFrom(colorHexa);
            }
            catch (Exception)
            {
                this.BackGroundLogScreenColor = Brushes.Aqua;
            }
            regVersion.Close();

            this.saveBackGroundLogScreenColor(colorHexa);
        }

        public void saveBackGroundLogScreenColor(string colorHexa)
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            regVersion.SetValue("BackGroundLogScreenColor", colorHexa);
            regVersion.Close();
        }

        #endregion

        #region ColorMove

        public void initColorMove()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexa = "";

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
            }
            else
            {
                colorHexa = (String)regVersion.GetValue("ColorMove", colorHexa);
            }

            try
            {
                //this.ColorMove = (Color)converter.ConvertFrom(colorHexa);
                string[] couleurs = colorHexa.Split('/');
                this.ColorMove = Color.FromArgb(Byte.Parse(couleurs[0]), Byte.Parse(couleurs[1]), Byte.Parse(couleurs[2]), Byte.Parse(couleurs[3]));
            }
            catch (Exception)
            {
                this.ColorMove = Colors.White;
            }
            regVersion.Close();

            this.saveColorMove(colorHexa);
        }

        public void saveColorMove(string colorHexa)
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            regVersion.SetValue("ColorMove", colorHexa);
            regVersion.Close();
        }

        #endregion

        #region ColorFixe

        public void initColorFixe()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexa = "";

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
            }
            else
            {
                colorHexa = (String)regVersion.GetValue("ColorFixe", colorHexa);
            }

            try
            {
                //this.ColorFixe = (Color)converter.ConvertFrom(colorHexa);
                string[] couleurs = colorHexa.Split('/');
                this.ColorFixe = Color.FromArgb(Byte.Parse(couleurs[0]), Byte.Parse(couleurs[1]), Byte.Parse(couleurs[2]), Byte.Parse(couleurs[3]));
            }
            catch (Exception)
            {
                this.ColorFixe = Colors.Black;
            }
            regVersion.Close();

            this.saveColorFixe(colorHexa);
        }

        public void saveColorFixe(string colorHexa)
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            regVersion.SetValue("ColorFixe", colorHexa);
            regVersion.Close();
        }

        #endregion

        #region FontSizeLogScreen

        public void initFontSizeLogScreen()
        {
            string taille = "12";

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
            }
            else
            {
                taille = (String)regVersion.GetValue("FontSizeLogScreen", taille);
            }

            try
            {
                this.FontSizeLogScreen = double.Parse(taille);
            }
            catch (Exception)
            {
                this.FontSizeLogScreen = 12;
            }
            regVersion.Close();

            this.saveFontSizeLogScreen();
        }

        public void saveFontSizeLogScreen()
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            regVersion.SetValue("FontSizeLogScreen", this.FontSizeLogScreen.ToString());
            regVersion.Close();
        }

        #endregion

        #endregion

        #region UserControl

        public void initUserControl(UserControl userControl)
        {
            userControl.FontFamily = this.FontFamilyUserControl;
            userControl.FontSize = this.FontSizeUserControl;
            //Les couleurs seront mise dans le initialize du usercontrol
        }

        #region FontFamilyWindow

        public void initFontFamilyUserControl()
        {
            string textFont = "Antiqua";

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
            }
            else
            {
                textFont = (String)regVersion.GetValue("FontFamilyUserControl", textFont);
            }

            try
            {
                this.FontFamilyUserControl = Fonts.SystemFontFamilies.FirstOrDefault(fon => fon.ToString().Contains(textFont));
            }
            catch (Exception)
            {
                try
                {
                    this.FontFamilyUserControl = Fonts.SystemFontFamilies.FirstOrDefault(fon => fon.ToString().Contains("Antiqua"));
                }
                catch (Exception)
                {
                    this.FontFamilyUserControl = Fonts.SystemFontFamilies.FirstOrDefault(fon => fon.ToString().Contains("Comic"));
                }
            }
            regVersion.Close();

            this.saveFontFamilyUserControl();
        }

        public void saveFontFamilyUserControl()
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            regVersion.SetValue("FontFamilyUserControl", this.FontFamilyUserControl.ToString());
            regVersion.Close();
        }

        #endregion

        #region BackGroundUserControlFilterColor

        public void initBackGroundUserControlFilterColor()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexa = "";

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
            }
            else
            {
                colorHexa = (String)regVersion.GetValue("BackGroundUserControlFilterColor", colorHexa);
            }

            try
            {
                this.BackGroundUserControlFilterColor = (Brush)converter.ConvertFrom(colorHexa);
            }
            catch (Exception)
            {
                this.BackGroundUserControlFilterColor = (Brush)(TypeDescriptor.GetConverter(typeof(Brush))).ConvertFrom("#FFDFE9F5");
            }
            regVersion.Close();

            this.saveBackGroundUserControlFilterColor(colorHexa);
        }

        public void saveBackGroundUserControlFilterColor(string colorHexa)
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            regVersion.SetValue("BackGroundUserControlFilterColor", colorHexa);
            regVersion.Close();
        }

        #endregion

        #region BackGroundUserControlDataGridColor

        public void initBackGroundUserControlDataGridColor()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexa = "";

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
            }
            else
            {
                colorHexa = (String)regVersion.GetValue("BackGroundUserControlDataGridColor", colorHexa);
            }

            try
            {
                this.BackGroundUserControlDataGridColor = (Brush)converter.ConvertFrom(colorHexa);
            }
            catch (Exception)
            {
                this.BackGroundUserControlDataGridColor = Brushes.White;
            }
            regVersion.Close();

            this.saveBackGroundUserControlDataGridColor(colorHexa);
        }

        public void saveBackGroundUserControlDataGridColor(string colorHexa)
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            regVersion.SetValue("BackGroundUserControlDataGridColor", colorHexa);
            regVersion.Close();
        }

        #endregion

        #region BackGroundUserControlDataGridAlternateColor

        public void initBackGroundUserControlDataGridAlternateColor()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexa = "";

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
            }
            else
            {
                colorHexa = (String)regVersion.GetValue("BackGroundUserControlDataGridAlternateColor", colorHexa);
            }

            try
            {
                this.BackGroundUserControlDataGridAlternateColor = (Brush)converter.ConvertFrom(colorHexa);
            }
            catch (Exception)
            {
                this.BackGroundUserControlDataGridAlternateColor = (Brush)(TypeDescriptor.GetConverter(typeof(Brush))).ConvertFrom("#FFE9EDF4");
            }
            regVersion.Close();

            this.saveBackGroundUserControlDataGridAlternateColor(colorHexa);
        }

        public void saveBackGroundUserControlDataGridAlternateColor(string colorHexa)
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            regVersion.SetValue("BackGroundUserControlDataGridAlternateColor", colorHexa);
            regVersion.Close();
        }

        #endregion

        #region FontSizeUserControl

        public void initFontSizeUserControl()
        {
            string taille = "12";

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
            }
            else
            {
                taille = (String)regVersion.GetValue("FontSizeUserControl", taille);
            }

            try
            {
                this.FontSizeUserControl = double.Parse(taille);
            }
            catch (Exception)
            {
                this.FontSizeUserControl = 12;
            }
            regVersion.Close();

            this.saveFontSizeUserControl();
        }

        public void saveFontSizeUserControl()
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            regVersion.SetValue("FontSizeUserControl", this.FontSizeUserControl.ToString());
            regVersion.Close();
        }

        #endregion

        #region styleVide

        public void initstyleVide()
        {
            string style = "1";

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
            }
            else
            {
                style = (String)regVersion.GetValue("styleVide", style);
            }

            try
            {
                this.styleVide = style;
            }
            catch (Exception)
            {
                this.styleVide = "1";
            }
            regVersion.Close();

            this.saveFontSizeUserControl();
        }

        public void savestyleVide()
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            regVersion.SetValue("styleVide", this.styleVide);
            regVersion.Close();
        }

        #endregion

        #endregion

        #region Windows

        public void initWindows(Window window)
        {
            window.Background = this.BackGroundWindowColor;
            window.FontFamily = this.FontFamilyWindow;
            window.FontSize = this.FontSizeWindow;
        }

        #region FontFamilyWindow

        public void initFontFamilyWindow()
        {
            string textFont = "Antiqua";

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
            }
            else
            {
                textFont = (String)regVersion.GetValue("FontFamilyWindow", textFont);
            }

            try
            {
                this.FontFamilyWindow = Fonts.SystemFontFamilies.FirstOrDefault(fon => fon.ToString().Contains(textFont));
            }
            catch (Exception)
            {
                try
                {
                    this.FontFamilyWindow = Fonts.SystemFontFamilies.FirstOrDefault(fon => fon.ToString().Contains("Antiqua"));
                }
                catch (Exception)
                {
                    this.FontFamilyWindow = Fonts.SystemFontFamilies.FirstOrDefault(fon => fon.ToString().Contains("Comic"));
                }
            }
            regVersion.Close();

            this.saveFontFamilyWindow();
        }

        public void saveFontFamilyWindow()
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            regVersion.SetValue("FontFamilyWindow", this.FontFamilyWindow.ToString());
            regVersion.Close();
        }

        #endregion

        #region BackGroundWindowColor

        public void initBackGroundWindowColor()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            string colorHexa = "";

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
            }
            else
            {
                colorHexa = (String)regVersion.GetValue("BackGroundWindowColor", colorHexa);
            }

            try
            {
                this.BackGroundWindowColor = (Brush)converter.ConvertFrom(colorHexa);
            }
            catch (Exception)
            {
                this.BackGroundWindowColor = Brushes.WhiteSmoke;
            }
            regVersion.Close();

            this.saveBackGroundWindowColor(colorHexa);
        }

        public void saveBackGroundWindowColor(string colorHexa)
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            regVersion.SetValue("BackGroundWindowColor", colorHexa);
            regVersion.Close();
        }

        #endregion

        #region FontSizeWindow

        public void initFontSizeWindow()
        {
            string taille = "12";

            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            if (regVersion == null)
            {
                regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0");
            }
            else
            {
                taille = (String)regVersion.GetValue("FontSizeWindow", taille);
            }

            try
            {
                this.FontSizeWindow = double.Parse(taille);
            }
            catch (Exception)
            {
                this.FontSizeWindow = 12;
            }
            regVersion.Close();

            this.saveFontSizeWindow();
        }

        public void saveFontSizeWindow()
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            regVersion.SetValue("FontSizeWindow", this.FontSizeWindow.ToString());
            regVersion.Close();
        }

        #endregion

        #endregion

        #region Remettre par défaut

        public void ResetPersonnalisation()
        {
            RegistryKey regVersion;
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sitaff2011\\1.0", true);
            //LogScreen
            try
            {
                regVersion.DeleteValue("FontFamilyBackGroundLogScreen");
            }
            catch (Exception) { }
            try
            {
                regVersion.DeleteValue("BackGroundLogScreenColor");
            }
            catch (Exception) { }
            try
            {
                regVersion.DeleteValue("FontSizeLogScreen");
            }
            catch (Exception) { }
            try
            {
                regVersion.DeleteValue("ColorMove");
            }
            catch (Exception) { }
            try
            {
                regVersion.DeleteValue("ColorFixe");
            }
            catch (Exception) { }

            //Windows
            try
            {
                regVersion.DeleteValue("FontFamilyWindow");
            }
            catch (Exception) { }
            try
            {
                regVersion.DeleteValue("BackGroundWindowColor");
            }
            catch (Exception) { }
            try
            {
                regVersion.DeleteValue("FontSizeWindow");
            }
            catch (Exception) { }

            //UserControls
            try
            {
                regVersion.DeleteValue("FontFamilyUserControl");
            }
            catch (Exception) { }
            try
            {
                regVersion.DeleteValue("BackGroundUserControlFilterColor");
            }
            catch (Exception) { }
            try
            {
                regVersion.DeleteValue("BackGroundUserControlDataGridColor");
            }
            catch (Exception) { }
            try
            {
                regVersion.DeleteValue("BackGroundUserControlDataGridAlternateColor");
            }
            catch (Exception) { }
            try
            {
                regVersion.DeleteValue("FontSizeUserControl");
            }
            catch (Exception) { }
            try
            {
                regVersion.DeleteValue("styleVide");
            }
            catch (Exception) { }
            regVersion.Close();
        }

        #endregion
    }
}
