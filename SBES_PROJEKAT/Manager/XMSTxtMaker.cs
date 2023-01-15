using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class XMSTxtMaker
    {
        public static void CreateTxt(string serverName)
        {
            string pathTxt = $"D:\\Fakultet\\CETVRTA GODINA\\PROJEKAT\\Manager\\{serverName}.txt";
            if (!File.Exists(pathTxt))
            {
                StreamWriter sw = File.CreateText(pathTxt);
                sw.Close();
            }
        }

        public static void WriteToTxt(string serverName, string message)
        {
            string pathTxt = $"D:\\Fakultet\\CETVRTA GODINA\\PROJEKAT\\Manager\\{serverName}.txt";

            TextWriter tw = new StreamWriter(pathTxt, true);
            tw.WriteLine(message);
            tw.Close();
        }
    }
}
