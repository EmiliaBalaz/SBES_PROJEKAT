using Manager;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XMSServer
{
    public class XMLWritter
    {
        static List<DateTime> times = new List<DateTime>();
        static int period = 30;
        static int brojPokusaja = 5;
        public static object mylock = new object();
        public static void CreateXmlFile(string nazivKlijenta)
        {
            string accountName = Formater.ParseName(WindowsIdentity.GetCurrent().Name.ToLower());
            Console.WriteLine(accountName);
            string path = $"D:\\Fakultet\\CETVRTA GODINA\\PROJEKAT\\Manager\\{nazivKlijenta}.xml";
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

        public static void DeleteXmlFile(string imeKlijenta)
        {
            
            try
            {
                string path = $"D:\\Fakultet\\CETVRTA GODINA\\PROJEKAT\\Manager\\{imeKlijenta}.xml";
                File.Delete(path);
                try
                {
                    Audit.AuthorizationSuccess(imeKlijenta,
                                    OperationContext.Current.IncomingMessageHeaders.Action);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
            catch (Exception e)
            {
                lock (mylock)
                {
                    DateTime time = DateTime.Now;

                    times.Add(time);
                    foreach (DateTime time1 in times.ToList())
                    {
                        if ((times[times.Count - 1] - time1).TotalSeconds > period)
                        {
                            times.Remove(time1);
                        }
                    }
                    if (times.Count >= brojPokusaja + 1)
                    {
                        //to do
                        Audit.AuthorizationFailed(imeKlijenta,
                        OperationContext.Current.IncomingMessageHeaders.Action, "DeleteFile method need Delete permission.", "CRITICAL LEVEL: ");
                    }
                    else if (times.Count == brojPokusaja)
                    {
                        //medium risk
                        Audit.AuthorizationFailed(imeKlijenta,
                        OperationContext.Current.IncomingMessageHeaders.Action, "DeleteFile method need Delete permission.", "MEDIUM LEVEL: ");
                    }
                    else
                    {
                        Audit.AuthorizationFailed(imeKlijenta,
                        OperationContext.Current.IncomingMessageHeaders.Action, "DeleteFile method need Delete permission.", "LOW LEVEL: ");
                    }
                }
            }
        }


        public static void WriteToXml(Osoba osoba, string imeKlijenta)
        {
            try
            {
                string path = $"D:\\Fakultet\\CETVRTA GODINA\\PROJEKAT\\Manager\\{imeKlijenta}.xml";
                var xml = XDocument.Load(path);
                var root = xml.Root;
                root.Add(new XElement(osoba.TipOsobe.ToString(), new XElement("Id", osoba.Id),
                                  new XElement("Naziv", osoba.Naziv)));
                xml.Save(path);
                try
                {
                    Audit.AuthorizationSuccess(imeKlijenta,
                                    OperationContext.Current.IncomingMessageHeaders.Action);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                DateTime time = DateTime.Now;

                times.Add(time);
                foreach (DateTime time1 in times.ToList())
                {
                    if ((times[times.Count - 1] - time1).TotalSeconds > period)
                    {
                        times.Remove(time1);
                    }
                }
                if (times.Count >= brojPokusaja + 1)
                {
                    //to do
                    Audit.AuthorizationFailed(imeKlijenta,
                    OperationContext.Current.IncomingMessageHeaders.Action, "WriteToXml method need Modify permission.", "CRITICAL LEVEL: ");
                }
                else if (times.Count == brojPokusaja)
                {
                    //medium risk
                    Audit.AuthorizationFailed(imeKlijenta,
                    OperationContext.Current.IncomingMessageHeaders.Action, "WriteToXml method need Modify permission.", "MEDIUM LEVEL: ");
                }
                else
                {
                    Audit.AuthorizationFailed(imeKlijenta,
                    OperationContext.Current.IncomingMessageHeaders.Action, "WriteToXml method need Modify permission.", "LOW LEVEL: ");
                }
            }
        }
        public static void DeleteFromXml(string id, string imeKlijenta)
        {
            try
            {
                string path = $"D:\\Fakultet\\CETVRTA GODINA\\PROJEKAT\\Manager\\{imeKlijenta}.xml";
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
                try
                {
                    Audit.AuthorizationSuccess(imeKlijenta,
                                    OperationContext.Current.IncomingMessageHeaders.Action);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                DateTime time = DateTime.Now;

                times.Add(time);
                foreach (DateTime time1 in times.ToList())
                {
                    if ((times[times.Count - 1] - time1).TotalSeconds > period)
                    {
                        times.Remove(time1);
                    }
                }
                if (times.Count >= brojPokusaja + 1)
                {
                    //to do
                    Audit.AuthorizationFailed(imeKlijenta,
                    OperationContext.Current.IncomingMessageHeaders.Action, "Delete method need Modify permission.", "CRITICAL LEVEL: ");
                }
                else if (times.Count == brojPokusaja)
                {
                    //medium risk
                    Audit.AuthorizationFailed(imeKlijenta,
                    OperationContext.Current.IncomingMessageHeaders.Action, "Delete method need Modify permission.", "MEDIUM LEVEL: ");
                }
                else
                {
                    Audit.AuthorizationFailed(imeKlijenta,
                    OperationContext.Current.IncomingMessageHeaders.Action, "Delete method need Modify permission.", "LOW LEVEL: ");
                }
            }
        }

        public static void ReadFromXml(string id, string imeKlijenta)
        {
            try
            {
                string path = $"D:\\Fakultet\\CETVRTA GODINA\\PROJEKAT\\Manager\\{imeKlijenta}.xml";

                var xml = XDocument.Load(path);

                var root = xml.Root;
                foreach (var element in root.Elements())
                {
                    if (element.Element("Id").Value.Trim() == id)
                    {
                        Console.WriteLine(element.Element("Id").Value);
                        Console.WriteLine(element.Element("Naziv").Value);
                        break;
                    }
                }
                Audit.AuthorizationSuccess(imeKlijenta,
                OperationContext.Current.IncomingMessageHeaders.Action);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Audit.AuthorizationFailed(imeKlijenta,
                   OperationContext.Current.IncomingMessageHeaders.Action, "ReadFromXml method need Read permission.", " ");
            }
        }
    }   
}
