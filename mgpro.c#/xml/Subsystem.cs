using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helper
{
    public class Subsystem
    {
        public String name;
        public String description;
        public String main;
        public String second;
        public String path;
        public String shema;
        public String file;
        public int step;
        public int sizeBuffer;
        public int max_id;
        public String status;
        public String varxml;
        public String namesavefile;
        public List<Device> devices;
        public List<ModbusDevice> modbuses;
        public List<Save> saves;
        public Dictionary<String,Variable> variables;
        public Dictionary<int, String> ids;
        public String rezult;
        public Subsystem()
        {
            devices = new List<Device>();
            modbuses = new List<ModbusDevice>();
            saves = new List<Save>();
            variables = new Dictionary<String,Variable>();
            ids = new Dictionary<int, string>();
            max_id = 0;
        }

        public string  VerifyNames()
        {
            String result="";
            foreach(Save sv in saves)
            {
                if (!variables.ContainsKey(sv.name))
                {
                    result += "Переменная для сохранения " + sv.name + " отсутствует\n";
                }
            }
            foreach(ModbusDevice mb in modbuses)
            {
                result += mb.VerifyNames(variables);
            }
            foreach(Device dv in devices)
            {
                result += dv.VerifyNames(variables);
            }
            return result;
            

        }

        public string MakeCode(string path)
        {
            String result = "";
            using (StreamWriter sw = File.CreateText(this.rezult+"/"+name+".h"))
            {
                sw.WriteLine("#ifndef "+name.ToUpper()+"_H");
                sw.WriteLine("#define " + name.ToUpper() + "_H");
                sw.WriteLine("// Подсистема "+name+":"+description);

                sw.WriteLine("static int StepCycle="+step+";\t // Время цикла в ms");
                sw.WriteLine("#define SIZE_BUFFER " + sizeBuffer.ToString() + "");

                sw.WriteLine("static char BUFFER["+sizeBuffer.ToString()+"];");
                foreach(Variable var in variables.Values)
                {
                    sw.WriteLine("#define " + var.name + "\t BUFFER["+var.address+"]\t//"+var.description);
                }

                sw.WriteLine("#pragma pack(push,1)");
                sw.WriteLine("static VarCtrl allVariables[]={      // Описание всех переменных");
                foreach (Variable var in variables.Values)
                {
                    sw.WriteLine("\t{ " + var.id + "\t," + var.format + "\t, &" + var.name + "},\t//" + var.description);
                }
                sw.WriteLine("\t{-1,0,NULL},");
                sw.WriteLine("};");
                sw.WriteLine("static char NameSaveFile[]=\"" + namesavefile + "\\0\";   // Имя файла для сохранения констант");
                sw.WriteLine("#pragma pop");
                sw.WriteLine("static short saveVariables[]={      // Id переменных для сохранения");
                foreach (Save sv in saves)
                {
                    Variable var = variables[sv.name];
                    sw.Write(var.id + ",");
                }
                sw.WriteLine("-1,};");
                string ModStr = "static ModbusDevice modbuses[]={\n";
                foreach(ModbusDevice mb in modbuses)
                {
                    String coil = "#pragma pack(push,1)\nstatic ModbusRegister coil_" + mb.name + "[]={  // \n";
                    String di = "#pragma pack(push,1)\nstatic ModbusRegister di_" + mb.name + "[]={  // \n";
                    String ir = "#pragma pack(push,1)\nstatic ModbusRegister ir_" + mb.name + "[]={  // \n";
                    String hr = "#pragma pack(push,1)\nstatic ModbusRegister hr_" + mb.name + "[]={  // \n";
                    foreach(Register reg in mb.registers)
                    {
                        String str="\t{&"+ reg.name+","+reg.format+","+reg.address+"},\t//"+reg.description+"\n";
                        switch (reg.type)
                        {
                            case 0:
                                coil += str;
                                break;
                            case 1:
                                di += str;
                                break;
                            case 2:
                                ir += str;
                                break;
                            case 3:
                                hr += str;
                                break;
                        }
                    }
                    coil += "\t{NULL,0,0},\n};";
                    di += "\t{NULL,0,0},\n};";
                    ir += "\t{NULL,0,0},\n};";
                    hr += "\t{NULL,0,0},\n};";
                    sw.WriteLine(coil);
                    sw.WriteLine("#pragma pop");
                    sw.WriteLine(di);
                    sw.WriteLine("#pragma pop");
                    sw.WriteLine(ir);
                    sw.WriteLine("#pragma pop");
                    sw.WriteLine(hr);
                    sw.WriteLine("#pragma pop");
                    if (mb.isMaster())
                    {
                        sw.WriteLine("static char "+mb.name+"_ip1[]={\""+mb.ip1+"\\0\"};");
                        sw.WriteLine("static char " + mb.name + "_ip2[]={\"" + mb.ip2 + "\\0\"};");
                    }
                    //sw.WriteLine("#pragma pack(push,1)");
                    ModStr += "\t{" + (mb.type ? 1 : 0) + ","+mb.port+",&coil_" + mb.name + "[0],&di_" + mb.name + "[0],&ir_" + mb.name + "[0],&hr_" + mb.name+ "[0]";
                    ModStr += ",NULL";
                    if (mb.isMaster())
                    {
                        ModStr += "," + mb.name + "_ip1";
                        ModStr += "," + mb.name + "_ip2";
                        ModStr += "," + mb.step;
                    } else
                    {
                        ModStr += ",NULL,NULL,0";

                    }
                    ModStr += "},\t //"+mb.description+"\n";

                    //sw.WriteLine("#pragma pop");
                }
                sw.WriteLine("#pragma pack(push,1)");
                sw.WriteLine(ModStr);
                sw.WriteLine("\t{0,-1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,0},};");
                sw.WriteLine("#pragma pop");

                foreach(Device dev in devices)
                {
                    Driver drv = LoadingUtils.defdrv[dev.driver];
                    sw.WriteLine(drv.MakeDrvString(dev));
                }
                sw.WriteLine("#pragma pack(push,1)");
                sw.WriteLine("static Driver drivers[]={");

                foreach (Device dev in devices)
                {
                    sw.WriteLine(dev.MakeDeviceString());
                }
                sw.WriteLine("\t{0,0,0,0,NULL,NULL},\n};");
                sw.WriteLine("#pragma pop");

                result += "File " + path + name + ".h" + " done...\n";

                sw.WriteLine("#endif");

            }
            return result;
        }
    }
}
