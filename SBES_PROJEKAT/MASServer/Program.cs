using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MASServer
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
            binding.SendTimeout = new TimeSpan(0, 10, 0);
            string address = "net.tcp://localhost:9999/SecurityService";

            //Autentifikacija tipa Windows
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            ServiceHost serviceHost = new ServiceHost(typeof(SecurityService));
            serviceHost.AddServiceEndpoint(typeof(ISecurityService), binding, address);

            try
            {
                serviceHost.Open();
                Console.WriteLine("WCFService is started.\nPress <enter> to stop ...");

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] {0}", e.Message);
                Console.WriteLine("[StackTrace] {0}", e.StackTrace);
            }
            finally
            {
                serviceHost.Close();
            };

            Console.ReadLine();
        }
    }
}
