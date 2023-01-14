using ServiceContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Manager
{
    public class XMLWritter
    {
        public static void CreateXmlFile(string nazivKlijenta)
        {
            string accountName = Formater.ParseName(WindowsIdentity.GetCurrent().Name.ToLower());
            Console.WriteLine(accountName);
            string path = $"C:\\Users\\Emily\\Desktop\\SBES_novi\\SBES_PROJEKAT\\Manager\\{nazivKlijenta}.xml";
            using (XmlWriter writer = XmlWriter.Create(path))
            {
                writer.WriteStartElement("List");
                writer.WriteEndElement();
                writer.Flush();
            }

            List<FileSystemRights> pravaPristupa = new List<FileSystemRights>() { FileSystemRights.Read, FileSystemRights.CreateFiles, FileSystemRights.Modify };
            List<FileSystemRights> nemaPristupa = new List<FileSystemRights>() { FileSystemRights.Delete };
            AccessControlList.AddAllow(accountName, pravaPristupa, $"{nazivKlijenta}.xml");
            AccessControlList.AddDeny(accountName, nemaPristupa, $"{nazivKlijenta}.xml");
        }

        public static void DeleteXmlFile(string nazivKlijenta)
        {

                string path = $"C:\\Users\\Emily\\Desktop\\SBES_novi\\SBES_PROJEKAT\\Manager\\{nazivKlijenta}.xml";
                File.Delete(path);
        }


        public static void WriteToXml(Osoba osoba, string nazivKlijenta)
        {
            try
            {
                string path = $"D:\\Fakultet\\CETVRTA GODINA\\PROJEKAT\\Manager\\{nazivKlijenta}.xml";
                var xml = XDocument.Load(path);
                var root = xml.Root;
                root.Add(new XElement("Osoba", new XElement("Id", osoba.Id),
                                  new XElement("Naziv", osoba.Naziv),
                                  new XElement("Tip", osoba.TipOsobe.ToString())));
                xml.Save(path);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);

            }
        }
        public static void DeleteFromXml(string id, string nazivKlijenta)
        {
            try
            {
                string path = $"D:\\Fakultet\\CETVRTA GODINA\\PROJEKAT\\Manager\\{nazivKlijenta}.xml";
                var xml = XDocument.Load(path);
                var root = xml.Root;
                foreach (var element in root.Elements())
                {
                    if (element.Element("Id").Value.Trim() == id)
                    {
                        element.Remove();
                        break;
                    }
                }
                xml.Save(path);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);

            }
        }

        public static void ReadFromXml(string id,string nazivKlijenta)
        {
            try
            {
                string path = $"D:\\Fakultet\\CETVRTA GODINA\\PROJEKAT\\Manager\\{nazivKlijenta}.xml";

                var xml = XDocument.Load(path);

                var root = xml.Root;
                foreach (var element in root.Elements())
                {
                    if (element.Element("Id").Value.Trim() == id)
                    {
                        Console.WriteLine(element.Element("Id").Value);
                        Console.WriteLine(element.Element("Naziv").Value);
                        Console.WriteLine(element.Element("Tip").Value);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);

            }
        }






    }
}
