using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SitaffRibbon.Classes
{
    class MailViaOutlook
    {
        public void send(string adresseTo)
        {
            string filename = "mailto:" + adresseTo;
            Process myProcess = new Process();
            myProcess.StartInfo.FileName = filename;
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.RedirectStandardOutput = false;
            myProcess.Start();
        }

        public void sendWithPJ(string adresseTo, string pj)
        {
            
                string filename = "mailto:" + adresseTo + @"?attachment=""" + pj + @"""";
            Process myProcess = new Process();
            myProcess.StartInfo.FileName = filename;
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.RedirectStandardOutput = false;
            myProcess.Start();
        }
    }
}
