using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helper
{
    public class ModbusDevice
    {
        public bool type=false;
        public String name;
        public String description;
        public String xml;
        public int port = 0;
        public String ip1="localhost";
        public String ip2= "localhost";
        public int step=0;
        public List<Register> registers;
        public ModbusDevice()
        {
            registers = new List<Register>();
        }
        public bool isMaster()
        {
            return type;
        }
        public void makeSlave()
        {
            type = false;
        }
        public void makeMaster()
        {
            type = true;
        }
        public String VerifyNames(Dictionary<String, Variable> variables )
        {
            String result="";
            foreach (Register reg in registers)
            {
                if (!variables.ContainsKey(reg.name))
                {
                    result += "!";
                    Util.message("В устройстве " + name + " есть " + reg.name + " но его нет в переменных ");
                    continue;
                }
                if (variables[reg.name].format != reg.format)
                {
                    result += "!";
                    Util.message("В устройстве " + name + " есть " + reg.name + "его тип "+ reg.format
                            +" а у переменной "+ variables[reg.name].format);
                    continue;
                }
            }
            return result;
        }
    }
}
