using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Security.Permissions;

namespace SitaffRibbon.Classes
{
    class DossierAffaire
    {

        #region attributs

        private String dossierRepertoireDeBase = @"\\srv-sauve\Sitaff\Base-Affaire";
        public String dossierAffaire = @"\\stockageNAS\Affaires\Dossiers";

        #endregion

        public void MiseEnPlaceDroits(string dossier)
        {            
            //TEST
            DirectorySecurity dirSec = Directory.GetAccessControl(dossier);

            FileSystemAccessRule fsar;
            FileSystemRights rights;

            rights = FileSystemRights.FullControl;

            fsar = new FileSystemAccessRule("aquinton", rights, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);

            dirSec.AddAccessRule(fsar);

            Directory.SetAccessControl(dossier, dirSec);
        }

        public void CopyRulesFolder(DirectoryInfo source, DirectoryInfo destination)
        {
            try
            {
                DirectorySecurity dirSource = Directory.GetAccessControl(source.FullName);
                DirectorySecurity dirDestination = Directory.GetAccessControl(destination.FullName);
                DirectorySecurity SecuritePourDestination = new DirectorySecurity();

                //J'ajoute les droits du dossier source à la sécurité du dossier destination
                AuthorizationRuleCollection autorisations;
                autorisations = dirSource.GetAccessRules(true, true, typeof(NTAccount));
                foreach (FileSystemAccessRule fsar in autorisations.OfType<FileSystemAccessRule>())
                {
                    SecuritePourDestination.AddAccessRule(new FileSystemAccessRule(fsar.IdentityReference.Value, fsar.FileSystemRights, fsar.InheritanceFlags, fsar.PropagationFlags, fsar.AccessControlType));
                }

                //Ajout des droits au dossier de destination
                Directory.SetAccessControl(destination.FullName, SecuritePourDestination);
            }
            catch (Exception)
            {
            }
        }

        public void DeplacerDossierAffaire(String numeroAff)
        {
            try
            {
                DirectoryInfo sourceDir = new DirectoryInfo(dossierRepertoireDeBase);
                DirectoryInfo destinationDir = new DirectoryInfo(dossierAffaire + @"\" + numeroAff);

                CopyDirectory(sourceDir, destinationDir);
            }
            catch (Exception e)
            {
                ErrorMessageBox.Show("Erreur, impossible de créer le dossier de l'affaire sur le NAS, contactez votre administrateur système.", e.Message, "erreur");
            }
        }

        public void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }
            CopyRulesFolder(source, destination);

            // Copy all files.
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                try
                {
                    file.CopyTo(Path.Combine(destination.FullName,
                        file.Name));
                }
                catch (Exception) { }
            }

            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                // Get destination directory.
                string destinationDir = Path.Combine(destination.FullName, dir.Name);

                // Call CopyDirectory() recursively.
                CopyDirectory(dir, new DirectoryInfo(destinationDir));
            }
        }

        public void OuvrirDossier(String numeroAff)
        {
            System.Diagnostics.Process.Start(dossierAffaire + @"\" + numeroAff);
        }

        public void CreationTousDossier()
        {
            foreach (Affaire item in ((App)App.Current).mySitaffEntities.Affaire)
            {
                this.DeplacerDossierAffaire(item.Numero);
            }
        }

    }
}
