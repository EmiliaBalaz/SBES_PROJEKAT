using Manager;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace XMSServer
{
    public class Services : IServices
    {
        public void AddPerson(string key, Osoba osoba,string nazivKlijenta)
        {
            try
            {
                XMLWritter.WriteToXml(osoba, nazivKlijenta);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}.", e.Message);
            }
        }

        public void CreateFile(string nazivKlijenta)
        {
            try
            { 
                XMLWritter.CreateXmlFile(nazivKlijenta);
                Console.WriteLine($"Klijent koji je kreirao fajl je: {nazivKlijenta}!");
                Audit.AuthorizationSuccess(nazivKlijenta,
                OperationContext.Current.IncomingMessageHeaders.Action);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}.", e.Message);
                Audit.AuthorizationFailed(nazivKlijenta,
                    OperationContext.Current.IncomingMessageHeaders.Action, "CreateFile method need CreateFiles permission.", "");

            }
        }

        public void DeleteFile(string nazivKlijenta)
        {
            try
            {
                XMLWritter.DeleteXmlFile(nazivKlijenta);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}.", e.Message);
            }
        }

        public void DeletePerson(string key, string nazivKlijenta)
        {
            try
            {
                XMLWritter.DeleteFromXml(key, nazivKlijenta);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}.", e.Message);
            }
        }

        public void Read(string key, string nazivKlijenta)
        {
            try
            {
                XMLWritter.ReadFromXml(key, nazivKlijenta);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}.", e.Message);
            }
        }
    }
}
