using Manager;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using HashAlgorithm = Manager.HashAlgorithm;

namespace MASServer
{
    public class SecurityService : ISecurityService
    {
        public void TestMetoda(string message, byte[] signature)
        {


            string clienName = Formater.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name.ToLower());

            string clientNameSign = clienName + "_sign";
            X509Certificate2 certificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople,
                StoreLocation.LocalMachine, clientNameSign);

            /// Verify signature using SHA1 hash algorithm
            if (DigitalSignature.Verify(message, HashAlgorithm.SHA1, signature, certificate))
            {
                Console.WriteLine("Sign is valid");
                Console.WriteLine(message);
                //WriteToTXT.WriteToTxt(message);
            }
            else
            {
                Console.WriteLine("Sign is invalid");
            }
        }  
    }
}
