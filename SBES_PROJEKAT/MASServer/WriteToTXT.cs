using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASServer
{
    public class WriteToTXT
    {
        static public void WriteToTxt(string data)
        {
            string path = "C:\\Users\\Emily\\Desktop\\Sbes_projekat\\SBES_PROJEKAT\\Manager\\Podaci.txt";
            TextWriter tw = new StreamWriter(path, true);
            tw.WriteLine(data);
            tw.Close();
        }
    }
}
