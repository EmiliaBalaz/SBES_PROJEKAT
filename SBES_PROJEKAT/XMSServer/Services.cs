using Manager;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMSServer
{
    public class Services : IServices
    {
        public void AddPerson(string key, Osoba osoba)
        {
            throw new NotImplementedException();
        }

        public void CreateFile()
        {
            try
            { 
                XMLWritter.CreateXmlFile();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}.", e.Message);
            }
        }

        public void DeleteFile()
        {
            try
            {
                XMLWritter.DeleteXmlFile();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}.", e.Message);
            }
        }

        public void DeletePerson(string key)
        {
            throw new NotImplementedException();
        }

        public void Read(string key)
        {
            throw new NotImplementedException();
        }
    }
}
