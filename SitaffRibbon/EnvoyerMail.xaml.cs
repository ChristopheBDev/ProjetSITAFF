using SitaffRibbon.Classes;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace SitaffRibbon
{
    /// <summary>
    /// Logique d'interaction pour EnvoyerMail.xaml
    /// </summary>
    public partial class EnvoyerMail : Window
    {
        public string pj;
        public string message = "";
        public string objet = "";
        public string A = "";
        public string messagepj = "";
        public string cc = null;
        public string adresseAMettre = null;

        public EnvoyerMail()
        {
            InitializeComponent();

            //Intialisation de la personnalisation utilisateur
            ((App)App.Current).personnalisation.initWindows(this);

            //Position dans le premier champ de la fenêtre
            this.mainRTB.Focus();
        }

        #region Verifs

        public bool verifGenerale()
        {
            bool verif = true;

            if (!verifTextBoxCorps())
            {
                verif = false;
            }
            if (!verifTextBoxA())
            {
                verif = false;
            }
            if (!verifTextBoxObjet())
            {
                verif = false;
            }

            return verif;
        }

        #region Corps

        public bool verifTextBoxCorps()
        {
            return ((App)App.Current).verifications.RichTextBoxObligatoire(this.mainRTB, this._textBlockCorps);
        }

        #endregion

        #region Objet

        public bool verifTextBoxObjet()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxObjet, this._textBlockObjet);
        }

        private void _textBoxObjet_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            this.verifTextBoxObjet();
        }

        #endregion

        #region A

        public bool verifTextBoxA()
        {
            return ((App)App.Current).verifications.TextBoxObligatoire(this._textBoxA, this._textBlockA);
        }

        private void _textBoxA_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            this.verifTextBoxA();
        }

        #endregion

        private void _buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        private void _buttonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.verifGenerale())
            {
                //message = this._textBoxCorps.Text;
                TextRange textRange = new TextRange(
                                                        // TextPointer to the start of content in the RichTextBox.
                                            mainRTB.Document.ContentStart,
                                                        // TextPointer to the end of content in the RichTextBox.
                                            mainRTB.Document.ContentEnd
                                        );
                // The Text property on a TextRange object returns a string
                // representing the plain text content of the TextRange.

                message = SaveXamlPackage();
                objet = this._textBoxObjet.Text;
                A = this._textBoxA.Text;
                Mail mail = new Mail();
                try
                {
                    mail.EnvoiMessageAvecPJ(A, cc, message, objet, this.pj, this.adresseAMettre);
                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Echec de l'envoi du mail");
                }
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            //this._textBoxA.Text = this.A;
            //this._textBoxPJ.Text = this.messagepj;
            //this._textBoxObjet.Text = this.objet;
        }



        void SaveRTBContent(Object sender, RoutedEventArgs args)
        {

            // Send an arbitrary URL and file name string specifying
            // the location to save the XAML in.
            //SaveXamlPackage("C:\\test.rtf");
        }

        // Handle "Load RichTextBox Content" button click.
        void LoadRTBContent(Object sender, RoutedEventArgs args)
        {
            // Send URL string specifying what file to retrieve XAML
            // from to load into the RichTextBox.
            //LoadXamlPackage("C:\\test.rtf");
        }

        // Handle "Print RichTextBox Content" button click.
        void PrintRTBContent(Object sender, RoutedEventArgs args)
        {
            PrintCommand();
        }

        // Save XAML in RichTextBox to a file specified by _fileName
        string SaveXamlPackage()
        {
            string lien_Fichier = Environment.GetEnvironmentVariable("TEMP") + @"\" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".rtf";
            TextRange range;
            FileStream fStream;
            range = new TextRange(mainRTB.Document.ContentStart, mainRTB.Document.ContentEnd);
            fStream = new FileStream(lien_Fichier, FileMode.Create);
            range.Save(fStream, DataFormats.Rtf);
            fStream.Close();
            return LoadXamlPackage(lien_Fichier);
        }

        // Load XAML into RichTextBox from a file specified by _fileName
        string LoadXamlPackage(string _fileName)
        {
            TextRange range;
            FileStream fStream;
            if (File.Exists(_fileName))
            {
                range = new TextRange(mainRTB.Document.ContentStart, mainRTB.Document.ContentEnd);
                fStream = new FileStream(_fileName, FileMode.OpenOrCreate);
                range.Load(fStream, DataFormats.Rtf);
                fStream.Close();
                int counter = 0;
                string line;
                string aRetourner = "";
                System.IO.StreamReader file = new System.IO.StreamReader(_fileName);
                while ((line = file.ReadLine()) != null)
                {
                    aRetourner = aRetourner + line;
                    counter++;
                }

                file.Close();
                return aRetourner;
            }
            return null;

            //SautinSoft.RtfToHtml r = new SautinSoft.RtfToHtml();
            //string AppPath = System.Environment.CurrentDirectory;

            ////specify some options
            //r.OutputFormat = SautinSoft.RtfToHtml.eOutputFormat.HTML_5;
            //r.Encoding = SautinSoft.RtfToHtml.eEncoding.UTF_8;

            ////specify image options
            //r.ImageStyle.ImageFolder = AppPath;			//this folder must exist
            //r.ImageStyle.ImageSubFolder = "test.files";	//this folder will be created by the component
            //r.ImageStyle.ImageFileName = "img";			//template name for images
            //r.ImageStyle.IncludeImageInHtml = false;	//false - save images on HDD, true - save images inside HTML


            //string rtfFile = System.IO.Path.GetFullPath(@"..\..\..\..\..\..\test.rtf");
            //string htmlFile = System.IO.Path.Combine(AppPath, "test.html"); //the result will be located in the same folder as binary
            //string htmlString = null;

            //try
            //{
            //    htmlString = r.ConvertFileToString(_fileName);
            //    return htmlString;
            //}
            //catch (Exception)
            //{
            //    return "";
            //}

        }

        // Print RichTextBox content
        private void PrintCommand()
        {
            PrintDialog pd = new PrintDialog();
            if ((pd.ShowDialog() == true))
            {
                //use either one of the below      
                pd.PrintVisual(mainRTB as Visual, "printing as visual");
                pd.PrintDocument((((IDocumentPaginatorSource)mainRTB.Document).DocumentPaginator), "printing as paginator");
            }
        }
    }
}
