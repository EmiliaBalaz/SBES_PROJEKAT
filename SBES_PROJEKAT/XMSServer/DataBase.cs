using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMSServer
{
    public class DataBase
    {

        internal static Dictionary<string, Osoba> osobe = new Dictionary<string, Osoba>();

        static DataBase()
        {
            Osoba o1 = new Osoba(Tip.STUDENT, "Emilija", "1");
            Osoba o2 = new Osoba(Tip.PROFESOR, "Imre", "2");
            Osoba o3 = new Osoba(Tip.STUDENT, "Cvijetin", "3");

            osobe.Add("1", o1);
            osobe.Add("2", o2);
            osobe.Add("3", o3);
        }
    }
}
