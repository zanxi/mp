using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helper
{
    public class Driver
    {
        public String name;
        public String description;
        public int lenData;
        public int lenInit;
        public string header;
        public string type;
        public Dictionary<String, Signal> sigs;
        public Dictionary<String, Init> inits;
        public String code;
        public Driver()
        {
            sigs = new Dictionary<string, Signal>();
            inits = new Dictionary<string, Init>();
        }
        public String MakeDrvString(Device dev)
        {
            List<String> res = new List<string>();
            for (int i = 0; i < lenInit; i++)
            {
                res.Add("0");
            }
            foreach (Init ini in inits.Values)
            {
                res[ini.address] = ini.value;
            }

            foreach (InitAss ia in dev.inits)
            {
                Init a = inits[ia.nameSig];
                res[a.address] = ia.value;
            }
            string rez = "#include \"" + header + "\"\n";
            rez+= "static char buf_" + dev.name + "[" + lenData + "];\t//" + dev.description + "\n";
            rez += "static "+type+" ini_" + dev.name + "={";
            foreach (String s in res)
            {
                rez += s + ",";
            }
            rez += "};\n";
            rez += "#pragma pack(push,1)\n";
            rez += "static table_drv table_" + dev.name + "={0,0,&ini_" + dev.name + ",buf_" + dev.name + ",0,0};\n";
            rez += "#pragma pop\n";
            rez += "#pragma pack(push,1)\n";
            rez += "static DriverRegister def_buf_"+dev.name+"[]={\n";
            foreach(Assign asig in dev.signals)
            {
                rez += "\t{&" + asig.nameVar + "," + sigs[asig.nameSig].format + "," + sigs[asig.nameSig].address + "},\n";
            }
            rez += "\t{NULL,0,0},\n};\n";
            rez += "#pragma pop";
            return rez;
        }
    }

    public class Signal
    {
        public String name;
        public byte format;
        public int address;
        public bool need;
    }
    public class Init
    {
        public String name;
        public int address;
        public String value;
    }

}
