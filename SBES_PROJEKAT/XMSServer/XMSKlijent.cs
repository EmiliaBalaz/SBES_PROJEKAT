using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace XMSServer
{
    public class XMSKlijent
    {

        static public void PosaljiPoruku(string poruka)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/SecurityService";

            //Windows Autentifikacija
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            string signCertCN = Formater.ParseName(WindowsIdentity.GetCurrent().Name.ToLower()) + "_sign";

            using (ClientProxy proxy = new ClientProxy(binding, address))
            {
                X509Certificate2 certificateSign = CertManager.GetCertificateFromStorage(StoreName.My,
                    StoreLocation.LocalMachine, signCertCN);

                byte[] signature = DigitalSignature.Create(poruka, HashAlgorithm.SHA1, certificateSign);

                proxy.TestMetoda(poruka, signature);
            }


        }
    }
}
