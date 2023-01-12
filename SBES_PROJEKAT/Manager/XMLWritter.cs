using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Manager
{
    public class XMLWritter
    {
        public static void CreateXmlFile()
        {
            string accountName = Formater.ParseName(WindowsIdentity.GetCurrent().Name.ToLower());
            Console.WriteLine(accountName);
            string path = $"C:\\Users\\Emily\\Desktop\\SBES_novi\\SBES_PROJEKAT\\Manager\\emily.xml";
            using (XmlWriter writer = XmlWriter.Create(path))
            {
                writer.WriteStartElement("List");
                writer.WriteEndElement();
                writer.Flush();
            }

            List<FileSystemRights> pravaPristupa = new List<FileSystemRights>() { FileSystemRights.Read, FileSystemRights.CreateFiles, FileSystemRights.Modify };
            List<FileSystemRights> nemaPristupa = new List<FileSystemRights>() { FileSystemRights.Delete };
            AccessControlList.AddAllow(accountName, pravaPristupa, $"emily.xml");
            AccessControlList.AddDeny(accountName, nemaPristupa, $"emily.xml");
        }

        public static void DeleteXmlFile()
        {

                string path = $"C:\\Users\\Emily\\Desktop\\SBES_novi\\SBES_PROJEKAT\\Manager\\emily.xml";
                File.Delete(path);
        }
    }
}
