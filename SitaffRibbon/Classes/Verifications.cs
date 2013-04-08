using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SitaffRibbon.Classes
{
    public class Verifications
    {
        #region Variables

        public Brush Couleur_Champ_Ok;
        public Brush Couleur_Champ_Non_Ok;

        public Brush Couleur_Textblock_Ok;
        public Brush Couleur_Textblock_Non_Ok;

        #endregion

        #region Constructeur

        public Verifications()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
            //Initialisation des couleurs avec transparence
            //Vert avec transparence            
            string colorHexavert = "#2400FF00";
            this.Couleur_Champ_Ok = (Brush)converter.ConvertFrom(colorHexavert);
            //Rouge avec transparence
            string colorHexarouge = "#24FF0000";
            this.Couleur_Champ_Non_Ok = (Brush)converter.ConvertFrom(colorHexarouge);

            this.Couleur_Textblock_Ok = Brushes.Green;
            this.Couleur_Textblock_Non_Ok = Brushes.Red;
        }

        #endregion

        #region TextBox

        #region Texte

        #region String

        string MessageErreur_TextBoxObligatoire = "Du texte doit obligatoirement être renseigné";

        string MessageErreur_TextBoxNonObligatoire = "";

        string MessageErreur_TextBoxObligatoireLimite = "Du texte doit obligatoirement être renseigné attention nombre de caractères limités à ";

        string MessageErreur_TextBoxObligatoireMail = "Du texte doit obligatoirement être renseigné et être un email";

        #endregion

        #region Obligatoire

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool TextBoxObligatoire(TextBox _textBox, TextBlock _textBlock)
        {
            if (_textBox.Text.Trim().Length == 0)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxObligatoire;
                return false;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
                return true;
            }
        }

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu limité et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <param name="limite">Limite de caractères</param>
        /// <returns></returns>
        public bool TextBoxObligatoire(TextBox _textBox, TextBlock _textBlock, int limite)
        {
            if (_textBox.Text.Trim().Length == 0 || _textBox.Text.Trim().Length > limite)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxObligatoireLimite + limite + " caractères";
                return false;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
                return true;
            }
        }

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu égal à un nombre et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <param name="limite">Limite de caractères</param>
        /// <returns></returns>
        public bool TextBoxObligatoire(TextBox _textBox, int limite)
        {
            if (_textBox.Text.Trim().Length == 0 || _textBox.Text.Trim().Length == limite)
            {
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxObligatoireLimite + limite + " caractères";
                return false;
            }
            else
            {
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
                return true;
            }
        }

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu égal à s et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <param name="s">Chaine à comparer</param>
        /// <returns></returns>
        public bool TextBoxObligatoire(TextBox _textBox, TextBlock _textBlock, string s)
        {
            if (_textBox.Text.Trim().Length == 0 && _textBox.Text.Trim().ToLower() == s.Trim().ToLower())
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxObligatoireLimite;
                return false;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
                return true;
            }
        }

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu égal à s et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <param name="_textBox2">TextBox à comparer</param>
        /// <returns></returns>
        public bool TextBoxObligatoire(TextBox _textBox, TextBlock _textBlock, TextBox _textBox2)
        {
            if (_textBox.Text.Trim().Length == 0 && _textBox.Text.Trim().ToLower() == _textBox2.Text.Trim().ToLower())
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxObligatoireLimite;
                return false;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
                return true;
            }
        }

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu de type mail et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool TextBoxObligatoireMail(TextBox _textBox, TextBlock _textBlock)
        {
            if (_textBox.Text.Trim().Length > 0)
            {
                if (_textBox.Text.Trim().Contains('@') && _textBox.Text.Trim().Contains('.'))
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Ok;
                    _textBox.Background = this.Couleur_Champ_Ok;
                    _textBox.ToolTip = null;
                    return true;
                }
            }

            _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
            _textBox.Background = this.Couleur_Champ_Non_Ok;
            _textBox.ToolTip = this.MessageErreur_TextBoxObligatoireMail;
            return false;
        }

        #endregion

        #region NonObligatoire

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre non obligatoire son contenu et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool TextBoxNonObligatoire(TextBox _textBox, TextBlock _textBlock)
        {
            if (_textBox.Text.Trim().Length == 0)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
                return true;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <param name="limite">Limite de caractères</param>
        /// <returns></returns>
        public bool TextBoxNonObligatoire(TextBox _textBox, TextBlock _textBlock, int limite)
        {
            if (_textBox.Text.Trim().Length == 0 || _textBox.Text.Trim().Length <= limite)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
                return true;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxNonObligatoire;
                return false;
            }
        }

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre non obligatoire son contenu de type mail et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool TextBoxNonObligatoireMail(TextBox _textBox, TextBlock _textBlock)
        {
            if (_textBox.Text.Trim().Length > 0)
            {
                if (_textBox.Text.Trim().Contains('@') && _textBox.Text.Trim().Contains('.'))
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Ok;
                    _textBox.Background = this.Couleur_Champ_Ok;
                    _textBox.ToolTip = null;
                    return true;
                }
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxObligatoireMail;
                return false;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
                return true;
            }

        }

        #endregion

        #endregion

        #region Chiffres

        #region Double

        string MessageErreur_TextBoxDoubleObligatoire_CasVide = "Un chiffre doit obligatoirement être renseigné";
        string MessageErreur_TextBoxDoubleObligatoire_CasTexte = "Le contenu de ce champ doit forcément être du type 'chiffre'";
        string MessageErreur_TextBoxDoubleNonObligatoire_CasTexte = "Le contenu de ce champ doit forcément être du type 'chiffre' ou vide";

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu en double et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool TextBoxDoubleObligatoire(TextBox _textBox, TextBlock _textBlock)
        {
            double val;

            if (_textBox.Text.Length == 0)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxDoubleObligatoire_CasVide;
                return false;
            }
            else
            {
                if (double.TryParse(_textBox.Text, out val))
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Ok;
                    _textBox.Background = this.Couleur_Champ_Ok;
                    _textBox.ToolTip = null;
                    return true;
                }
                else
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                    _textBox.Background = this.Couleur_Champ_Non_Ok;
                    _textBox.ToolTip = this.MessageErreur_TextBoxDoubleObligatoire_CasTexte;
                    return false;
                }
            }
        }

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu en double et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <param name="_toCompare">TextBox à comparer</param>
        /// <returns></returns>
        public bool TextBoxDoubleObligatoire(TextBox _textBox, TextBlock _textBlock, TextBox _toCompare)
        {
            double val;
            double val2;

            if (_textBox.Text.Trim().Length == 0 || _toCompare.Text.Trim().Length == 0)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxDoubleObligatoire_CasVide;
                return false;
            }
            else
            {
                if (double.TryParse(_textBox.Text, out val))
                {
                    if (double.TryParse(_toCompare.Text, out val2) && val == val2)
                    {
                        _textBlock.Foreground = this.Couleur_Textblock_Ok;
                        _textBox.Background = this.Couleur_Champ_Ok;
                        _textBox.ToolTip = null;
                        return true;
                    }
                    else
                    {
                        _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                        _textBox.Background = this.Couleur_Champ_Non_Ok;
                        _textBox.ToolTip = this.MessageErreur_TextBoxDoubleObligatoire_CasTexte;
                        return false;
                    }
                }
                else
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                    _textBox.Background = this.Couleur_Champ_Non_Ok;
                    _textBox.ToolTip = this.MessageErreur_TextBoxDoubleObligatoire_CasTexte;
                    return false;
                }
            }
        }

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu en double et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <param name="length">length à comparer</param>
        /// <returns></returns>
        public bool TextBoxDoubleObligatoire(TextBox _textBox, TextBlock _textBlock, double length)
        {
            double val;

            if (_textBox.Text.Trim().Length == 0)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxDoubleObligatoire_CasVide;
                return false;
            }
            else
            {
                if (double.TryParse(_textBox.Text, out val))
                {
                    if (val <= length)
                    {
                        _textBlock.Foreground = this.Couleur_Textblock_Ok;
                        _textBox.Background = this.Couleur_Champ_Ok;
                        _textBox.ToolTip = null;
                        return true;
                    }
                    else
                    {
                        _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                        _textBox.Background = this.Couleur_Champ_Non_Ok;
                        _textBox.ToolTip = this.MessageErreur_TextBoxDoubleObligatoire_CasTexte;
                        return false;
                    }
                }
                else
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                    _textBox.Background = this.Couleur_Champ_Non_Ok;
                    _textBox.ToolTip = this.MessageErreur_TextBoxDoubleObligatoire_CasTexte;
                    return false;
                }
            }
        }

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu en double et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="length">length à comparer</param>
        /// <returns></returns>
        public bool TextBoxDoubleObligatoire(TextBox _textBox, double length)
        {
            double val;

            if (_textBox.Text.Trim().Length == 0)
            {
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxDoubleObligatoire_CasVide;
                return false;
            }
            else
            {
                if (double.TryParse(_textBox.Text, out val))
                {
                    if (val <= length)
                    {
                        _textBox.Background = this.Couleur_Champ_Ok;
                        _textBox.ToolTip = null;
                        return true;
                    }
                    else
                    {
                        _textBox.Background = this.Couleur_Champ_Non_Ok;
                        _textBox.ToolTip = this.MessageErreur_TextBoxDoubleObligatoire_CasTexte;
                        return false;
                    }
                }
                else
                {
                    _textBox.Background = this.Couleur_Champ_Non_Ok;
                    _textBox.ToolTip = this.MessageErreur_TextBoxDoubleObligatoire_CasTexte;
                    return false;
                }
            }
        }

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre non obligatoire son contenu en double mais interdit le texte et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool TextBoxDoubleNonObligatoire(TextBox _textBox, TextBlock _textBlock)
        {
            double val;

            if (_textBox.Text.Length == 0)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
                return true;
            }
            else
            {
                if (double.TryParse(_textBox.Text, out val))
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Ok;
                    _textBox.Background = this.Couleur_Champ_Ok;
                    _textBox.ToolTip = null;
                    return true;
                }
                else
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                    _textBox.Background = this.Couleur_Champ_Non_Ok;
                    _textBox.ToolTip = this.MessageErreur_TextBoxDoubleNonObligatoire_CasTexte;
                    return false;
                }
            }
        }

        #endregion

        #region Int

        string MessageErreur_TextBoxIntObligatoire_CasVide = "Un entier doit obligatoirement être renseigné";
        string MessageErreur_TextBoxIntObligatoire_CasTexte = "Le contenu de ce champ doit forcément être du type 'entier'";
        string MessageErreur_TextBoxIntObligatoire_CasDouble = "Le contenu de ce champ doit forcément être du type 'entier'";
        string MessageErreur_TextBoxIntObligatoire_CasTexteTelephone = "Le contenu de ce champ doit forcément être du type 'téléphone'";

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu en int et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool TextBoxIntObligatoire(TextBox _textBox, TextBlock _textBlock)
        {
            int val;
            double val2;

            if (_textBox.Text.Length == 0)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxIntObligatoire_CasVide;
                return false;
            }
            else
            {
                if (int.TryParse(_textBox.Text, out val))
                {
                    if (double.TryParse(_textBox.Text, out val2))
                    {
                        _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                        _textBox.Background = this.Couleur_Champ_Non_Ok;
                        _textBox.ToolTip = this.MessageErreur_TextBoxIntObligatoire_CasDouble;
                        return false;
                    }
                    else
                    {
                        _textBlock.Foreground = this.Couleur_Textblock_Ok;
                        _textBox.Background = this.Couleur_Champ_Ok;
                        _textBox.ToolTip = null;
                        return true;
                    }
                }
                else
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                    _textBox.Background = this.Couleur_Champ_Non_Ok;
                    _textBox.ToolTip = this.MessageErreur_TextBoxIntObligatoire_CasTexte;
                    return false;
                }
            }
        }

        string MessageErreur_TextBoxIntNonObligatoire_CasTexte = "Le contenu de ce champ doit forcément être du type 'entier' ou vide";
        string MessageErreur_TextBoxIntNonObligatoire_CasDouble = "Le contenu de ce champ doit forcément être du type 'entier' ou vide";

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre non obligatoire son contenu en int mais interdit le texte et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool TextBoxIntNonObligatoire(TextBox _textBox, TextBlock _textBlock)
        {
            int val;
            double val2;

            if (_textBox.Text.Length == 0)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
                return true;
            }
            else
            {
                if (int.TryParse(_textBox.Text, out val))
                {
                    if (double.TryParse(_textBox.Text, out val2))
                    {
                        _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                        _textBox.Background = this.Couleur_Champ_Non_Ok;
                        _textBox.ToolTip = this.MessageErreur_TextBoxIntNonObligatoire_CasDouble;
                        return false;
                    }
                    else
                    {
                        _textBlock.Foreground = this.Couleur_Textblock_Ok;
                        _textBox.Background = this.Couleur_Champ_Ok;
                        _textBox.ToolTip = null;
                        return true;
                    }
                }
                else
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                    _textBox.Background = this.Couleur_Champ_Non_Ok;
                    _textBox.ToolTip = this.MessageErreur_TextBoxIntNonObligatoire_CasTexte;
                    return false;
                }
            }
        }

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu en double type téléphone et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool TextBoxIntObligatoireTelephone(TextBox _textBox, TextBlock _textBlock)
        {
            int val;

            if (_textBox.Text.Length == 0)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxDoubleObligatoire_CasVide;
                return false;
            }
            else
            {
                if (int.TryParse(_textBox.Text, out val) && val >= 10 && val <= 11)
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Ok;
                    _textBox.Background = this.Couleur_Champ_Ok;
                    _textBox.ToolTip = null;
                    return true;
                }
                else
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                    _textBox.Background = this.Couleur_Champ_Non_Ok;
                    _textBox.ToolTip = this.MessageErreur_TextBoxIntObligatoire_CasTexteTelephone;
                    return false;
                }
            }
        }

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre non obligatoire son contenu en double type téléphone et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool TextBoxIntNonObligatoireTelephone(TextBox _textBox, TextBlock _textBlock)
        {
            int val;

            if (_textBox.Text.Length == 0)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
                return true;
            }
            else
            {
                if (int.TryParse(_textBox.Text, out val) && val >= 10 && val <= 11)
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Ok;
                    _textBox.Background = this.Couleur_Champ_Ok;
                    _textBox.ToolTip = null;
                    return true;
                }
                else
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                    _textBox.Background = this.Couleur_Champ_Non_Ok;
                    _textBox.ToolTip = this.MessageErreur_TextBoxIntObligatoire_CasTexteTelephone;
                    return false;
                }
            }
        }

        #endregion

        #endregion

        #region Heures

        string MessageErreur_TextBoxHeureObligatoire = "Vous devez obligatoirement renseigner une heure au format xx:xx";

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu en heure et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool TextBoxHeureObligatoire(TextBox _textBox, TextBlock _textBlock)
        {
            string temp = CheckIsHeure(_textBox.Text, ':');

            if (temp != null)
            {
                _textBox.Text = temp;
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
                return true;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxHeureObligatoire;
                return false;
            }
        }

        string MessageErreur_TextBoxHeureNonObligatoire = "Vous devez obligatoirement renseigner une heure au format xx:xx ou laisser ce champ à vide";

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu en heure ou en vide et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_textBox">TextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool TextBoxHeureNonObligatoire(TextBox _textBox, TextBlock _textBlock)
        {
            string temp = CheckIsHeure(_textBox.Text, ':');

            if (temp != null)
            {
                _textBox.Text = temp;
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
                return true;
            }
            else
            {
                if (_textBox.Text.Length == 0)
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Ok;
                    _textBox.Background = this.Couleur_Champ_Ok;
                    _textBox.ToolTip = null;
                    return true;
                }
                else
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                    _textBox.Background = this.Couleur_Champ_Non_Ok;
                    _textBox.ToolTip = this.MessageErreur_TextBoxHeureNonObligatoire;
                    return false;
                }
            }
        }

        #region Fonction Heures

        private string CheckIsHeure(string s, char c)
        {
            if (s != "" && s.Trim().Length > 0)
            {
                if (s.Length == 4)
                {
                    int i = 0;
                    string newChaine = "";
                    foreach (char item in s)
                    {
                        if (i == 2)
                        {
                            newChaine = newChaine + c;
                        }
                        newChaine = newChaine + item;
                        i = i + 1;
                    }
                    s = newChaine;
                }
                string[] temp;
                temp = s.Split(c);

                if (temp != null)
                {
                    int heure;
                    int min;
                    if (temp.Count<string>() > 1)
                    {
                        if (int.TryParse(temp[0], out heure) && int.TryParse(temp[1], out min))
                        {
                            if (heure >= 0 && heure <= 23 && min >= 0 && min < 60)
                            {
                                if (heure < 10 && min < 10)
                                {
                                    return "0" + heure + ":" + "0" + min;
                                }
                                if (heure < 10)
                                {
                                    return "0" + heure + ":" + min;
                                }
                                if (min < 10)
                                {
                                    return heure + ":" + "0" + min;
                                }
                                return heure + ":" + min;
                            }
                            else if (heure >= 0 && heure <= 23)
                            {
                                if (heure < 10)
                                {
                                    return "0" + heure + ":00";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (int.TryParse(temp[0], out heure))
                        {
                            if (heure >= 0 && heure <= 23)
                            {
                                if (heure < 10)
                                {
                                    return "0" + heure + ":00";
                                }
                                return heure + ":00";
                            }
                        }
                    }
                }
            }
            return null;
        }

        #endregion


        #endregion

        #region Général

        public void MettreTextBoxEnCouleur(TextBox _textBox, TextBlock _textBlock, bool test)
        {
            if (test)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _textBox.Background = this.Couleur_Champ_Ok;
                _textBox.ToolTip = null;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _textBox.Background = this.Couleur_Champ_Non_Ok;
                _textBox.ToolTip = this.MessageErreur_TextBoxObligatoire;
            }
        }

        #endregion

        #endregion

        #region RichTextBox

        #region Obligatoire

        string MessageErreur_RichTextBoxObligatoire = "Du contenu doit forcément être renseigné";

        /// <summary>
        /// Fait une vérification sur une textBox afin de rendre obligatoire son contenu et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_richTextBox">RichTextBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool RichTextBoxObligatoire(RichTextBox _richTextBox, TextBlock _textBlock)
        {
            //message = this._textBoxCorps.Text;
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                                        _richTextBox.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                                        _richTextBox.Document.ContentEnd
                                    );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.

            string test = textRange.Text;
            if (test.Trim().Length == 0)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _richTextBox.Background = this.Couleur_Champ_Non_Ok;
                _richTextBox.ToolTip = this.MessageErreur_RichTextBoxObligatoire;
                return false;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _richTextBox.Background = this.Couleur_Champ_Ok;
                _richTextBox.ToolTip = null;
                return true;
            }
        }

        #endregion

        #endregion

        #region ComboBox

        #region Selection

        string MessageErreur_ComboBoxSelectionObligatoire = "Vous devez forcément sélectionner un élément";

        /// <summary>
        /// Fait une vérification sur une comboBox afin de rendre obligatoire sa sélection et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_comboBox">ComboBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool ComboBoxSelectionObligatoire(ComboBox _comboBox, TextBlock _textBlock)
        {
            if (_comboBox.SelectedItem == null)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _comboBox.Background = this.Couleur_Champ_Non_Ok;
                _comboBox.ToolTip = this.MessageErreur_ComboBoxSelectionObligatoire;
                return false;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _comboBox.Background = this.Couleur_Champ_Ok;
                _comboBox.ToolTip = null;
                return true;
            }
        }

        //string MessageErreur_ComboBoxSelectionNonObligatoire = "";

        /// <summary>
        /// Fait une vérification sur une comboBox afin de rendre non obligatoire sa sélection et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_comboBox">ComboBox à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool ComboBoxSelectionNonObligatoire(ComboBox _comboBox, TextBlock _textBlock)
        {
            if (_comboBox.SelectedItem == null)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _comboBox.Background = this.Couleur_Champ_Ok;
                _comboBox.ToolTip = null;
                return true;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _comboBox.Background = this.Couleur_Champ_Ok;
                _comboBox.ToolTip = null;
                return true;
            }
        }

        public void MettreComboxEnCouleur(ComboBox _comboBox, TextBlock _textBlock, bool test)
        {
            if (test)
            {
                _comboBox.Background = this.Couleur_Champ_Ok;
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _comboBox.ToolTip = null;
            }
            else
            {
                _comboBox.Background = this.Couleur_Champ_Non_Ok;
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _comboBox.ToolTip = this.MessageErreur_ComboBoxSelectionObligatoire;
            }
        }

        #endregion

        #endregion

        #region DatePicker

        #region Selection

        string MessageErreur_DatePickerSelectionObligatoire = "Vous devez forcément sélectionner une date";
        //string MessageErreur_DatePickerSelectionNonObligatoire = "";

        /// <summary>
        /// Fait une vérification sur un datepicker afin de rendre obligatoire sa sélection de date et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_datePicker">DatePicker à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool DatePickerSelectionObligatoire(DatePicker _datePicker, TextBlock _textBlock)
        {
            if (_datePicker.SelectedDate == null)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _datePicker.Background = this.Couleur_Champ_Non_Ok;
                _datePicker.ToolTip = this.MessageErreur_DatePickerSelectionObligatoire;
                return false;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _datePicker.Background = this.Couleur_Champ_Ok;
                _datePicker.ToolTip = null;
                return true;
            }
        }

        /// <summary>
        /// Fait une vérification sur un datepicker afin de rendre obligatoire sa sélection de date et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_datePicker">DatePicker à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <param name="_dateMini">Date minimale à Comparer</param>
        /// <returns></returns>
        public bool DatePickerSelectionObligatoire(DatePicker _datePicker, TextBlock _textBlock, DatePicker _dateMini)
        {
            if (_datePicker.SelectedDate != null)
            {
                if (_dateMini.SelectedDate != null && _datePicker.SelectedDate >= _dateMini.SelectedDate)
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Ok;
                    _datePicker.Background = this.Couleur_Champ_Ok;
                    _datePicker.ToolTip = null;
                    return true;
                }
                else
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                    _datePicker.Background = this.Couleur_Champ_Non_Ok;
                    if (_dateMini.SelectedDate != null)
                    {
                        _datePicker.ToolTip = "Cette date doit être supérieure  à : " + _dateMini.SelectedDate;
                    }
                    else
                    {
                        _datePicker.ToolTip = this.MessageErreur_DatePickerSelectionObligatoire;
                    }
                    return false;
                }
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _datePicker.Background = this.Couleur_Champ_Non_Ok;
                _datePicker.ToolTip = this.MessageErreur_DatePickerSelectionObligatoire;
                return false;
            }
        }

        /// <summary>
        /// Fait une vérification sur un datepicker afin de rendre obligatoire sa sélection de date et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_datePicker">DatePicker à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <param name="_dateMini">Date minimale à Comparer</param>
        /// <returns></returns>
        public bool DatePickerSelectionObligatoire(DatePicker _datePicker, TextBlock _textBlock, DateTime _dateMini)
        {
            if (_datePicker.SelectedDate != null)
            {
                if (_dateMini != null && _datePicker.SelectedDate >= _dateMini)
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Ok;
                    _datePicker.Background = this.Couleur_Champ_Ok;
                    _datePicker.ToolTip = null;
                    return true;
                }
                else
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                    _datePicker.Background = this.Couleur_Champ_Non_Ok;
                    if (_dateMini != null)
                    {
                        _datePicker.ToolTip = "Cette date doit être supérieure  à : " + _dateMini;
                    }
                    else
                    {
                        _datePicker.ToolTip = this.MessageErreur_DatePickerSelectionObligatoire;
                    }
                    return false;
                }
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _datePicker.Background = this.Couleur_Champ_Non_Ok;
                _datePicker.ToolTip = this.MessageErreur_DatePickerSelectionObligatoire;
                return false;
            }
        }

        /// <summary>
        /// Fait une vérification sur un datepicker afin de rendre non obligatoire sa sélection de date et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_datePicker">DatePicker à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <returns></returns>
        public bool DatePickerSelectionNonObligatoire(DatePicker _datePicker, TextBlock _textBlock)
        {
            if (_datePicker.SelectedDate == null)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _datePicker.Background = this.Couleur_Champ_Ok;
                _datePicker.ToolTip = null;
                return true;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _datePicker.Background = this.Couleur_Champ_Ok;
                _datePicker.ToolTip = null;
                return true;
            }
        }

        /// <summary>
        /// Fait une vérification sur un datepicker afin de rendre non obligatoire sa sélection de date et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_datePicker">DatePicker à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <param name="_dateMini">Date minimale à comparer</param>
        /// <returns></returns>
        public bool DatePickerSelectionNonObligatoire(DatePicker _datePicker, TextBlock _textBlock, DatePicker _dateMini)
        {
            if (_datePicker.SelectedDate != null)
            {
                if (_dateMini.SelectedDate == null)
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Ok;
                    _datePicker.Background = this.Couleur_Champ_Ok;
                    _datePicker.ToolTip = null;
                    return true;
                }
                else if (_dateMini.SelectedDate != null && _datePicker.SelectedDate >= _dateMini.SelectedDate)
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Ok;
                    _datePicker.Background = this.Couleur_Champ_Ok;
                    _datePicker.ToolTip = null;
                    return true;
                }
                else
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                    _datePicker.Background = this.Couleur_Champ_Non_Ok;
                    _datePicker.ToolTip = "Cette date doit être supérieure  à : " + _dateMini.SelectedDate;
                    return false;
                }
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _datePicker.Background = this.Couleur_Champ_Ok;
                _datePicker.ToolTip = null;
                return true;
            }
        }

        /// <summary>
        /// Fait une vérification sur un datepicker afin de rendre non obligatoire sa sélection de date et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_datePicker">DatePicker à vérifier</param>
        /// <param name="_textBlock">TextBlock associée</param>
        /// <param name="_dateMini">Date minimale à comparer</param>
        /// <returns></returns>
        public bool DatePickerSelectionNonObligatoire(DatePicker _datePicker, TextBlock _textBlock, DateTime _dateMini)
        {
            if (_datePicker.SelectedDate != null)
            {
                if (_dateMini == null)
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Ok;
                    _datePicker.Background = this.Couleur_Champ_Ok;
                    _datePicker.ToolTip = null;
                    return true;
                }
                else if (_dateMini != null && _datePicker.SelectedDate >= _dateMini)
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Ok;
                    _datePicker.Background = this.Couleur_Champ_Ok;
                    _datePicker.ToolTip = null;
                    return true;
                }
                else
                {
                    _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                    _datePicker.Background = this.Couleur_Champ_Non_Ok;
                    _datePicker.ToolTip = "Cette date doit être supérieure  à : " + _dateMini;
                    return false;
                }
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _datePicker.Background = this.Couleur_Champ_Ok;
                _datePicker.ToolTip = null;
                return true;
            }
        }


        #endregion

        #endregion

        #region CheckBox

        public void MettreCheckBoxEnCouleur(CheckBox _checkBox, bool test)
        {
            if (test)
            {
                _checkBox.Background = this.Couleur_Champ_Ok;
            }
            else
            {
                _checkBox.Background = this.Couleur_Champ_Non_Ok;
            }
        }


        public void MettreCheckBoxEnCouleur(CheckBox _checkBox, TextBlock _textBlock, bool test)
        {
            if (test)
            {
                _checkBox.Background = this.Couleur_Champ_Ok;
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
            }
            else
            {
                _checkBox.Background = this.Couleur_Champ_Non_Ok;
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
            }
        }

        #endregion

        #region DataGrid

        string Message_ErreurDataGridObligatoire = "Vous devez forcément ajouter quelque chose dans ";

        /// <summary>
        /// Fait une vérification sur un datagrid afin de rendre obligatoire un ajout d'élement et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_dataGrid">DataGrid à vérifier</param>
        /// <param name="_textBlock">TextBlock correspondant</param>
        /// <returns></returns>
        public bool DataGridSelectionObligatoire(DataGrid _dataGrid, TextBlock _textBlock)
        {
            if (_dataGrid.Items.Count > 0)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _dataGrid.ToolTip = null;
                return true;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Non_Ok;
                _dataGrid.ToolTip = this.Message_ErreurDataGridObligatoire + _textBlock.Text;
                return false;
            }
        }

        /// <summary>
        /// Fait une vérification sur un datagrid afin de rendre non obligatoire un ajout d'élement et met en couleurs la combinaison de contrôles
        /// </summary>
        /// <param name="_dataGrid">DataGrid à vérifier</param>
        /// <param name="_textBlock">TextBlock correspondant</param>
        /// <returns></returns>
        public bool DataGridSelectionNonObligatoire(DataGrid _dataGrid, TextBlock _textBlock)
        {
            if (_dataGrid.Items.Count > 0)
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _dataGrid.ToolTip = null;
                return true;
            }
            else
            {
                _textBlock.Foreground = this.Couleur_Textblock_Ok;
                _dataGrid.ToolTip = null;
                return true;
            }
        }


        public void MettreDataGridEnCouleur(DataGrid _dataGrid, bool test)
        {
            if (test)
            {
                _dataGrid.Background = this.Couleur_Champ_Ok;
            }
            else
            {
                _dataGrid.Background = this.Couleur_Champ_Non_Ok;
            }
        }
        #endregion

        #region ListBox


        #endregion

        #region Boutons / TabItem

        string MessageErreur_MettreBoutonEnCouleur = "Des champs à l'intérieur de l'encadré afficher/masquer ne sont pas correctement renseignés";

        /// <summary>
        /// Met le bouton passé en paramètre en couleur
        /// </summary>
        /// <param name="_button">Bouton à mettre en couleur</param>
        /// <param name="_verif">Si vrai bouton en couleur 'Bon', si faux bouton en 'Pas bon'</param>
        public void MettreBoutonEnCouleur(Button _button, bool _verif)
        {
            if (_verif == true)
            {
                _button.Background = this.Couleur_Champ_Ok;
                _button.ToolTip = null;
            }
            else
            {
                _button.Background = this.Couleur_Champ_Non_Ok;
                _button.ToolTip = this.MessageErreur_MettreBoutonEnCouleur;
            }
        }

        string MessageErreur_MettreTabItemEnCouleur = "Des champs à l'intérieur de l'onglet ne sont pas correctement renseignés";

        /// <summary>
        /// Met le tabitem passé en paramètre en couleur
        /// </summary>
        /// <param name="_tabItem">TabItem à mettre en couleur</param>
        /// <param name="_verif">Si vrai bouton en couleur 'Bon', si faux bouton en 'Pas bon'</param>
        public void MettreTabItemEnCouleur(TabItem _tabItem, bool _verif)
        {
            if (_verif == true)
            {
                _tabItem.Background = this.Couleur_Champ_Ok;
                _tabItem.ToolTip = null;
            }
            else
            {
                _tabItem.Background = this.Couleur_Champ_Non_Ok;
                _tabItem.ToolTip = this.MessageErreur_MettreTabItemEnCouleur;
            }
        }

        #endregion
    }
}
