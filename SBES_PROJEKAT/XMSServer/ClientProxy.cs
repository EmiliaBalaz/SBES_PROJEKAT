using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace XMSServer
{
    public class ClientProxy : ChannelFactory<ISecurityService>, ISecurityService, IDisposable
    {
        ISecurityService factory;

        public ClientProxy(NetTcpBinding binding, string address) : base(binding, address)
        {

            factory = this.CreateChannel();
        }

        public void TestMetoda(string message, byte[] signature)
        {
            try
            {
                factory.TestMetoda(message, signature);
                Console.WriteLine("Poslao sam podatak.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}.");
            }
        }
    }
}
