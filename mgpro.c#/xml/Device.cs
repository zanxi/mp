using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helper
{
    public class Device
    {
        public String name;
        public String description;
        public string driver;
        public String slot;
        public String xml;
        public List<Assign> signals;
        public List<InitAss> inits;
        public Device()
        {
            signals = new List<Assign>();
            inits = new List<InitAss>();
        }
        public String VerifyNames(Dictionary<String, Variable> variables)
        {
            String result = "";
            foreach (Assign sig in signals)
            {
                if (!variables.ContainsKey(sig.nameVar))
                {
                    result += "!"; 
                        Util.message("В устройстве " + name + " есть " + sig.nameVar + " но его нет в переменных ");
                    continue;
                 }
                Variable vv = variables[sig.nameVar]; 
                Driver drv = LoadingUtils.defdrv[driver];
                if (!drv.sigs.ContainsKey(sig.nameSig))
                {
                    result += "!";
                    Util.message("В устройстве " + name + " есть " + sig.nameSig + " но его нет в описании устройства");
                    continue;
                }
                Signal sg = drv.sigs[sig.nameSig];
                if (vv.format != sg.format)
                {
                    result += "!";
                    Util.message("В устройстве " + name + " есть " + sig.nameVar + " но его формат не совпадает с переменными");
                    continue;
                }
            }
            return result;
        }
        public String MakeDeviceString()
        {
            Driver drv = LoadingUtils.defdrv[driver];
            return "\t{" + drv.code + "," + slot + "," + drv.lenInit + "," + drv.lenData + ",def_buf_" + name + ",&table_" + name + "}, //" + description ;
        }

    }

    public class Assign
    {
        public String nameVar;
        public String nameSig;
        public byte format;
    }
    public class InitAss
    {
        public String nameSig;
        public String value;
    }
}
