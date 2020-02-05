using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using helper;
namespace genCtest
{
    class GenCTest
    {
        static Project mp= new Project();
        static String nameMain = "d:/md/pti/pr/main.xml";

        static void Main(string[] args)
        {
            mp.LoadProject(nameMain);
            Console.WriteLine("Work " + mp.name + " :" + mp.description);
            String result = mp.VerifyNames();
            if (result.Length != 0)
            {
                Console.WriteLine("Найдены ошибки при проверке имен\n"+result);
                Console.WriteLine("done..");
                Console.ReadKey();
                return;
            }
            result = mp.MakeCode();
            Console.WriteLine( result);
            Console.WriteLine("done..");
            Console.ReadKey();

        }
    }
}
