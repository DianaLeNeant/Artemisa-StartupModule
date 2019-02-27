using System;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using OpenSSL.X509Certificate2Provider;

namespace Artemisa
{
    public class Startup
    {
        public readonly string ModuleName = "Artemisa - Startup";
        public readonly string UploadsDirectory = "Uploads";
        public readonly string SSLDirectory = "SSL";
        public readonly string ModulesDirectory = "Modules";
        public readonly string MediaDirectory = "Media";
        public readonly string TemplatesDirectory = "Templates";
        public readonly string MailTemplatesDirectory = "Templates/Mail";
        public readonly string DocumentTemplatesDirectory = "Templates/Documents";
        public readonly string FormsDirectory = "Forms";
        public readonly string ConfigurationsDirectory = "Config";

        private string onDir;
        public dynamic Parent;

        public void Start() {
            onDir = Parent.localPath;

            Parent.log("Directories check start.", this);

            createIfNotExists("Uploads");
            createIfNotExists("SSL");
            createIfNotExists("Modules");
            createIfNotExists("Media");
            createIfNotExists("Templates");
            createIfNotExists("Templates/Mail");
            createIfNotExists("Templates/Documents");
            createIfNotExists("Forms");
            createIfNotExists("Config");

            string certPath = onDir + "/" + SSLDirectory;
            if (!File.Exists(certPath  + "/certificate.ssl")) {
                Parent.log("Certificate file not found at '" + certPath + "'.", this);
            } else {
                string certificateText = File.ReadAllText(certPath + "/certificate.ssl");
                string privateKeyText = File.ReadAllText(certPath + "/privatekey.ssl");

                ICertificateProvider provider = new CertificateFromFileProvider(certificateText, privateKeyText);

                Parent.serverCertificate = provider.Certificate;
            }

            Parent.log("Directories check finished.", this);
        }
        
        private void createIfNotExists(string dir) {
            if (!Directory.Exists(onDir + "/" + dir)) Directory.CreateDirectory(onDir + "/" + dir);
        }
    }
}
