using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XMSServer
{
    public class Konekcija
    {
        public void ProveraKonekcije()
        {
            while (true)
            {
                try
                {
                    string path = "D:\\Fakultet\\CETVRTA GODINA\\PROJEKAT\\XMSServer\\bin\\Debug\\temp.txt";
                    if (File.Exists(path))
                    {
                        XMSKlijent.PosaljiPoruku(" ");
                        string[] lines = File.ReadAllLines(path);
                        foreach (string line in lines)
                        {
                            XMSKlijent.PosaljiPoruku(line);
                        }

                        File.Delete(path);
                    }
                }
                catch (Exception e) { }
                Thread.Sleep(1000);
            }
        }
    }
}
