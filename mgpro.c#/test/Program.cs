using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using helper;
namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            String nameFile = "d:/mgpro/du/ctrl.xml";
            String resultFile = "d:/mgpro/du/varctrl.xml";
            List<Register> regs = LoadingUtils.LoadRegistersModBus(nameFile);
            using (StreamWriter sw = File.CreateText(resultFile))
            {
                sw.WriteLine("<vars>");
                foreach (Register reg in regs)
                {
                    int format;
                    format=reg.type < 2?1:reg.format;
                    sw.WriteLine(   "<var name=\"" + reg.name + 
                                    "\" description=\"" + reg.description.Replace("\"", " ") + 
                                    "\" format=\"" + format.ToString() + 
                                    "\"></var>");
                }
                sw.WriteLine("</vars>");

            }


        }
    }
}
