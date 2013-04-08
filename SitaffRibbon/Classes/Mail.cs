using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Windows.Forms;

namespace SitaffRibbon.Classes
{
    class Mail : IDisposable
    {

        private String SMTP = "mails.collaboration-sfr.com";
        private int portSMTP = 587;
        private String SMTPUser = "sitaff@groupesit.com";
        private String MDPSMTPUser = "53admin35";
        private String adresseExpediteur = "sitaff@groupesit.com";
        private String nomExpediteur = "Sitaff";

        private MailMessage msg;
        private SmtpClient client;

        public void EnvoiMessage(string destinataire, string cc, string message, string objet)
        {
            try
            {
                //Objet Mail
                msg = new MailMessage();

                // Expéditeur
                msg.From = new MailAddress(this.adresseExpediteur, this.nomExpediteur);

                // Destinataire(s)
                foreach (String dest in destinataire.Split(';'))
                {
                    msg.To.Add(new MailAddress(dest, dest));
                }

                // Destinataire(s) en copie
                if (cc != null && cc != "")
                {
                    foreach (String destcc in cc.Split(';'))
                    {
                        msg.CC.Add(new MailAddress(destcc));
                    }
                }

                //Objet du mail
                msg.Subject = objet;

                //Corps du mail
                msg.Body = message;

                // Configuration SMTP
                client = new SmtpClient(SMTP, portSMTP);
                client.EnableSsl = false;
                client.Credentials = new NetworkCredential(SMTPUser, MDPSMTPUser);

                // Envoi du mail
                client.Send(msg);

                //Tue le mail
                msg.Dispose();
                //Ferme la connexion au SMTP
                client.Dispose();
            }
            catch (Exception ex)
            {
                //Tue le mail
                if (msg != null)
                {
                    msg.Dispose();
                }
                //Ferme la connexion au SMTP
                if (client != null)
                {
                    client.Dispose();
                }
                throw new Exception(ex.Message);
            }
        }

        public void EnvoiMessageAvecPJ(string destinataire, string cc, string message, string objet, string pj, string adresseAMettre)
        {
            try
            {
                //Objet Mail
                msg = new MailMessage();

                // Expéditeur
                msg.From = new MailAddress(this.adresseExpediteur, this.nomExpediteur);

                // Destinataire(s)
                foreach (String dest in destinataire.Split(';'))
                {
                    msg.To.Add(new MailAddress(dest, dest));
                }

                // Destinataire(s) en copie
                if (cc != null && cc != "")
                {
                    foreach (String destcc in cc.Split(';'))
                    {
                        msg.CC.Add(new MailAddress(destcc));
                    }
                }

                //Objet du mail
                msg.Subject = objet;

                //Corps du mail
                //Contenu du mail
                SautinSoft.RtfToHtml r = new SautinSoft.RtfToHtml();
                r.ImageStyle.IncludeImageInHtml = true;
                System.Collections.ArrayList arListWithImages = new System.Collections.ArrayList();
                string html = r.ConvertString(message, arListWithImages).Replace("Trial version can convert up to 30000 symbols.", "").Replace("Get the full featured version!", "");

                //Signature
                string signature = "";
                if (((App)App.Current)._connectedUser.Signature != null)
                {
                    signature = ((App)App.Current)._connectedUser.Signature;
                }

                //Logo
                string image = "<br /><br /><img src=cid:companylogo>";
                string cheminimage = "logo.gif";

                //Adresse
                string adresse = "<br />Tél : +33 (0)2 43 49 17 55 – Fax : +33 (0)2 43 49 02 29<br />P.A des Morandières - Bd de Galilée<br />53810 CHANGE - LAVAL";
                if (((App)App.Current)._connectedUser.Salarie_Interne1 != null)
                {
                    if (((App)App.Current)._connectedUser.Salarie_Interne1.Entreprise_Mere1 != null)
                    {
                        if (((App)App.Current)._connectedUser.Salarie_Interne1.Entreprise_Mere1.AdresseEMail != null)
                        {
                            adresse = ((App)App.Current)._connectedUser.Salarie_Interne1.Entreprise_Mere1.AdresseEMail;
                            cheminimage = ((App)App.Current)._connectedUser.Salarie_Interne1.Entreprise_Mere1.Logo;
                        }
                    }
                }

                //Mentions légales
                string MentLeg = "<br />Ce message (y compris les pièces jointes) est rédigé a l'intention exclusive de ses destinataires et peut contenir des informations confidentielles. Si vous recevez ce message par erreur, merci de le détruire et d'en avertir immédiatement l'expéditeur. Si vous n'êtes pas un destinataire, toute utilisation est non-autorisée et peut être illégale. Tout message électronique est susceptible d'altération et son intégrité ne peut être assurée. La société SIT (et ses filiales) décline(nt) toute responsabilité au titre de ce message, dans l'hypothèse ou il aurait été modifié. Merci.<br />This message (including any attachments) is intended solely for the use of the addressee(s) and may contain confidential informations.<br />If you receive this message in error, please delete it and immediately notify the sender.If the reader of this message is not the intended recipient, you are hereby notified that any unauthorized use, copying or dissemination is prohibited. E-mails are susceptible to alteration and their integrity cannot be guaranteed. Neither SIT group nor any of its subsidiaries or affiliates shall be liable for the message if altered, changed or falsified. Thank you.<br />";

                String contenuMail = html + signature + image + adresse + MentLeg;

                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(contenuMail, null, "text/html");
                //Lien de l'image CID dans la vue
                LinkedResource logo = new LinkedResource("\\\\stockagenas\\sitaff\\images\\" + cheminimage);
                logo.ContentId = "companylogo";
                htmlView.LinkedResources.Add(logo);

                //Ajout de la vue dans le mail
                msg.AlternateViews.Add(htmlView);

                //ajout de la pj
                Attachment piecejointe = new Attachment(pj);
                msg.Attachments.Add(piecejointe);

                //Ajout de l'adresse mail de la personne qui envoi si elle existe
                if (((App)App.Current)._connectedUser.Salarie_Interne1 != null)
                {
                    if (((App)App.Current)._connectedUser.Salarie_Interne1.Salarie != null)
                    {
                        if (((App)App.Current)._connectedUser.Salarie_Interne1.Salarie.Personne != null)
                        {
                            if (((App)App.Current)._connectedUser.Salarie_Interne1.Salarie.Personne.EMail_Pro != null && ((App)App.Current)._connectedUser.Salarie_Interne1.Salarie.Personne.EMail_Pro != "")
                            {
                                try
                                {
                                    msg.From = new MailAddress(((App)App.Current)._connectedUser.Salarie_Interne1.Salarie.Personne.EMail_Pro);
                                    msg.CC.Add(new MailAddress(((App)App.Current)._connectedUser.Salarie_Interne1.Salarie.Personne.EMail_Pro));
                                }
                                catch (Exception) { }
                            }
                            else
                            {
                                if (((App)App.Current)._connectedUser.Salarie_Interne1.Salarie.Personne.EMail != null && ((App)App.Current)._connectedUser.Salarie_Interne1.Salarie.Personne.EMail != "")
                                {
                                    try
                                    {
                                        msg.From = new MailAddress(((App)App.Current)._connectedUser.Salarie_Interne1.Salarie.Personne.EMail);
                                        msg.CC.Add(new MailAddress(((App)App.Current)._connectedUser.Salarie_Interne1.Salarie.Personne.EMail));
                                    }
                                    catch (Exception) { }
                                }
                            }
                        }
                    }
                }

                // Configuration SMTP
                client = new SmtpClient(SMTP, portSMTP);
                client.EnableSsl = false;
                client.Credentials = new NetworkCredential(SMTPUser, MDPSMTPUser);

                // Envoi du mail
                client.Send(msg);

                //Tue le mail
                msg.Dispose();
                //Ferme la connexion au SMTP
                client.Dispose();
            }
            catch (Exception ex)
            {
                //Tue le mail
                if (msg != null)
                {
                    msg.Dispose();
                }
                //Ferme la connexion au SMTP
                if (client != null)
                {
                    client.Dispose();
                }
                throw new Exception(ex.Message);
            }
        }

        #region IDisposable Membres

        public void Dispose()
        {
            //Tue le mail
            if (msg != null)
            {
                msg.Dispose();
            }
            //Ferme la connexion au SMTP
            if (client != null)
            {
                client.Dispose();
            }
        }

        #endregion

    }
}
