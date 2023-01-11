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
using XMSServer;

namespace Klijent
{
    class Program
    {
        static void Main(string[] args)
        {
            string srvCertCN = "wcfservice"; //wcfservice
            NetTcpBinding binding = new NetTcpBinding();

            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            //iz CertManagera uzimamo sertifikat
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9997/Services"),
                                      new X509CertificateEndpointIdentity(srvCert));

            string nazivKlijenta = Formater.ParseName(WindowsIdentity.GetCurrent().Name.ToLower());
            Console.WriteLine($"Ja sam {nazivKlijenta}.");



            using (ClientProxy proxy = new ClientProxy(binding, address))
            {
                while (true)
                {
                    Console.WriteLine("IZABERITE JEDNU OD OPCIJA: ");

                    Console.WriteLine("1.DODAVANJE OSOBE U XML.");
                    Console.WriteLine("2.BRISANJE OSOBE IZ XML-a.");
                    Console.WriteLine("3.ISCITAVANJE OSOBE IZ XML-a.");
                    Console.WriteLine("4.BRISANJE XML FAJLA.");
                    Console.WriteLine("5.KREIRANJE XML FAJLA.");

                    string opcija = Console.ReadLine();

                    switch (opcija)
                    {
                        case "1":
                            Console.WriteLine("Unesite sledeca polja: ");
                            Console.WriteLine("Id: ");
                            string id = Console.ReadLine();
                            Console.WriteLine("Naziv: ");
                            string naziv = Console.ReadLine();

                            proxy.AddPerson(id, new Osoba(Tip.STUDENT, naziv, id));
                            break;
                        case "2":
                            Console.WriteLine("Unesite id osobe: ");
                            string idOs = Console.ReadLine();
                            proxy.DeletePerson(idOs);
                            break;
                        case "3":
                            Console.WriteLine("unesite id osobe: ");
                            string idCitanje = Console.ReadLine();
                            proxy.Read(idCitanje);
                            break;
                        case "4":
                            proxy.DeleteFile();
                            break;
                        case "5":
                            proxy.CreateFile();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
