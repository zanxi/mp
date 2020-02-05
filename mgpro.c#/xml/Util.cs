using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helper
{
    public class Util
        {
            public static List<string> messageLine;
            public static bool debug = false;
            public static string name;
            public static string description;
            public static string path;


            static public void errorMessage(string message1, string message2)
            {
                DateTime tm = DateTime.Now;

                lock (messageLine)
                {
                    messageLine.Add(item: tm.GetDateTimeFormats()[12] + " Ошибка " + message1 + " --> " + message2 + "\n");
                }
            }
            static public void message(string message)
            {
                DateTime tm = DateTime.Now;
                lock (messageLine)
                {
                    messageLine.Add(tm.GetDateTimeFormats()[12] + " " + message + "\n");
                }
            }

    }
}
