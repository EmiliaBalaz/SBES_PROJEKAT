using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;


namespace Manager
{
    //: System.Collections.CollectionBase
    public class AccessControlList 
    {
        //kljuc je account, vrednost lista prava pristupa

        public static Dictionary<string, List<FileSystemRights>> acllist = new Dictionary<string, List<FileSystemRights>>();

        public static void AddAllow(string accountname, List<FileSystemRights> listaPravaPristupa, string nazivXml)
        {
            if (acllist.ContainsKey(accountname))
            {
                acllist[accountname] = listaPravaPristupa;
            }
            else
            {
                acllist.Add(accountname, listaPravaPristupa);
            }
            foreach (var el in listaPravaPristupa)
            {
                FileSystemSecurity.AddFileSecurity($"D:\\Fakultet\\CETVRTA GODINA\\PROJEKAT\\Manager\\{nazivXml}", accountname, el, AccessControlType.Allow);
            }
        }

        public static void AddDeny(string accountname, List<FileSystemRights> listaPravaPristupa, string nazivXml)
        {
            if (acllist.ContainsKey(accountname))
            {
                acllist[accountname] = listaPravaPristupa;
            }
            else
            {
                acllist.Add(accountname, listaPravaPristupa);
            }
            foreach (var el in listaPravaPristupa)
            {
                FileSystemSecurity.AddFileSecurity($"D:\\Fakultet\\CETVRTA GODINA\\PROJEKAT\\Manager\\{nazivXml}", accountname, el, AccessControlType.Deny);
            }
        }

        public void Remove(string accountname)
        {
            acllist[accountname] = null;
        }
    }
}
