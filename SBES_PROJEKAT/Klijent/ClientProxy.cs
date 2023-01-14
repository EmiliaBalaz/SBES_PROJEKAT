using Manager;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Klijent
{
    public class ClientProxy : ChannelFactory<IServices>, IServices, IDisposable
    {

        IServices factory;
        public ClientProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            //cltCertCN je podesen na korisnicko ime klijenta 
            string cltCertCN = Formater.ParseName(WindowsIdentity.GetCurrent().Name.ToLower());
            Console.WriteLine(cltCertCN);
            //string cltCertCN = "wcfservice";

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();
        }
        public void AddPerson(string key, Osoba osoba, string nazivKlijenta)
        {
            try
            {
                factory.AddPerson(key, osoba,nazivKlijenta);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

            }
        }

        public void CreateFile(string nazivKlijenta)
        {
            try
            {
                factory.CreateFile(nazivKlijenta);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

            }
        }

        public void DeleteFile(string nazivKlijenta)
        {
            try
            {
                factory.DeleteFile(nazivKlijenta);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void DeletePerson(string key, string nazivKlijenta)
        {
            try
            {
                factory.DeletePerson(key,nazivKlijenta);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }

        public void Read(string key, string nazivKlijenta)
        {
            try
            {
                factory.Read(key,nazivKlijenta); ;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
