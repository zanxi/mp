using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace helper
{
    public class LoadingUtils
    {
        public static int maxAddres;
        public static Dictionary<String, Driver> defdrv;
        public static String DefultDrv;
        public static void LoadProject(XmlDocument xml, Project pr)
        {
            XmlNode x = xml.SelectSingleNode("general");
            if (x == null) return;
            defdrv = new Dictionary<string, Driver>();
            pr.name = x.Attributes["name"].Value;
            pr.description = x.Attributes["description"].Value;
            pr.path = x.Attributes["path"].Value;
            pr.defdrv = x.Attributes["defdrv"].Value;
            DefultDrv = pr.defdrv;
            foreach (XmlNode n in xml.SelectNodes("general/subs")) // выбор подподсекции device подподсекции Device основной секции Apax конфигурационного файла OPULeft.xml или OPURight.xml для считывания значений
            {
                Subsystem sb = new Subsystem();
                sb.name = n.Attributes["name"].Value;
                sb.file = n.Attributes["file"].Value;
                sb.description = n.Attributes["description"].Value;
                sb.main = n.Attributes["main"].Value;
                sb.second = n.Attributes["second"].Value;
                sb.shema = n.Attributes["shema"].Value;
                sb.step = int.Parse(n.Attributes["step"].Value);
                sb.rezult = n.Attributes["result"].Value;
                sb.path= n.Attributes["path"].Value;
                String sbFile = sb.path +"/"+ sb.file + ".xml";
                XmlDocument subxml = new XmlDocument();
                subxml.Load(sbFile);
                sb = LoadSubsystem(subxml, sb);
                pr.subs.Add(sb.name, sb);
            }
            pr.drvs = defdrv;
        }
        public static Subsystem LoadSubsystem(XmlDocument xml,Subsystem sub)
        {
            XmlNode x = xml.SelectSingleNode("subsystem");
            if (x == null) return null;
            
            sub.variables = LoadingUtils.LoadVariables(xml,sub);
            sub.sizeBuffer = LoadingUtils.maxAddres;
            sub.saves = LoadingUtils.LoadSaves(xml, sub);
            sub.devices = LoadingUtils.LoadDevices(xml, sub);
            sub.modbuses = LoadingUtils.LoadModbus(xml, sub);
            return sub;
        }
        public static List<Save> LoadSaves(XmlDocument xml,Subsystem sub)
        {
            List<Save> svs = new List<Save>();
            XmlNode r = xml.SelectSingleNode("subsystem/saves");
            if (r == null) return null;
                String file = sub.path + "/" + r.Attributes["xml"].Value+".xml";
                XmlDocument savexml = new XmlDocument();
                savexml.Load(file);
            XmlNode n = savexml.SelectSingleNode("saves");
                if (n == null) return null;
                sub.namesavefile = n.Attributes["name"].Value;
                foreach (XmlNode x in savexml.SelectNodes("saves/save")) // выбор подподсекции device подподсекции Device основной секции Apax конфигурационного файла OPULeft.xml или OPURight.xml для считывания значений
                {
                    Save sv = new Save();
                    sv.name = x.InnerText;
                    svs.Add(sv);
                }
            return svs;
        }
        public static List<Device> LoadDevices(XmlDocument xml,Subsystem sub)
        {
            List<Device> devs = new List<Device>();
            foreach (XmlNode x in xml.SelectNodes("subsystem/devices")) // выбор подподсекции device подподсекции Device основной секции Apax конфигурационного файла OPULeft.xml или OPURight.xml для считывания значений
            {
                String file = sub.path +"/"+ x.Attributes["xml"].Value+".xml";
                XmlDocument devxml = new XmlDocument();
                devxml.Load(file);
                XmlNode r = devxml.SelectSingleNode("devices");
                if (r == null) return null;
                String assfile = sub.path + "/" + r.Attributes["xml"].Value + ".xml";
                XmlDocument assxml = new XmlDocument();
                assxml.Load(assfile);
                foreach (XmlNode n in devxml.SelectNodes("devices/device"))
                {
                    Device md = new Device();
                    md.description = n.Attributes["description"].Value;
                    md.name = n.Attributes["name"].Value;
                    md.driver = n.Attributes["driver"].Value;
                    md.slot = n.Attributes["slot"].Value;
                    if (!defdrv.ContainsKey(md.driver))
                    {
                        Driver drv = LoadDriver(md.driver);
                        defdrv.Add(drv.name, drv);    
                    }
                    foreach (XmlNode ass in assxml.SelectNodes("assign/"+md.name))
                    {
                        foreach(XmlNode def in assxml.SelectNodes("assign/" + md.name + "/def"))
                        {
                            Assign asig = new Assign();
                            asig.nameSig = def.InnerText;
                            asig.nameVar = def.Attributes["name"].Value;
                            md.signals.Add(asig);

                        }
                        foreach (XmlNode init in assxml.SelectNodes("assign/" + md.name + "/init"))
                        {
                            InitAss ainit = new InitAss();
                            ainit.nameSig = init.Attributes["name"].Value;
                            ainit.value = init.Attributes["value"].Value;
                            md.inits.Add(ainit);

                        }
                    }
                    devs.Add(md);

                }
            }

            return devs;
        }
        public static Driver LoadDriver(String file)
        {
            Driver drv = new Driver();
            file = DefultDrv + "/" + file+".xml";
            XmlDocument drvxml = new XmlDocument();
            drvxml.Load(file);
            XmlNode n = drvxml.SelectSingleNode("driver");
            drv.name = n.Attributes["name"].Value;
            drv.description= n.Attributes["description"].Value;
            drv.code = n.Attributes["code"].Value;
            drv.lenData= int.Parse(n.Attributes["lenData"].Value);
            drv.lenInit = int.Parse(n.Attributes["lenInit"].Value);
            drv.header= n.Attributes["header"].Value;
            foreach (XmlNode sig in drvxml.SelectNodes("driver/signals/signal"))
            {
                Signal sg = new Signal();
                sg.name = sig.Attributes["name"].Value;
                sg.format= (byte)int.Parse(sig.Attributes["format"].Value);
                sg.need = sig.Attributes["need"].Value.Equals("y") ? true : false;
                sg.address= int.Parse(sig.Attributes["address"].Value);
                drv.sigs.Add(sg.name, sg);
            }
            XmlNode ni = drvxml.SelectSingleNode("driver/init");
            drv.type=ni.Attributes["type"].Value;
            foreach (XmlNode init in drvxml.SelectNodes("driver/init/signal"))
            {
                Init isg = new Init();
                isg.name = init.Attributes["name"].Value;
                isg.value = init.Attributes["value"].Value;
                isg.address = int.Parse(init.Attributes["address"].Value);
                drv.inits.Add(isg.name, isg);
            }
            return drv;
        }

        public static List<ModbusDevice> LoadModbus(XmlDocument xml,Subsystem sub)
        {
            List<ModbusDevice> devs = new List<ModbusDevice>();
            foreach (XmlNode n in xml.SelectNodes("subsystem/modbus")) // выбор подподсекции device подподсекции Device основной секции Apax конфигурационного файла OPULeft.xml или OPURight.xml для считывания значений
            {
                string type;
                ModbusDevice md=new ModbusDevice();
                type = n.Attributes["type"].Value;
                if (type.Equals("slave"))   md.makeSlave();
                else                        md.makeMaster();
                md.description = n.Attributes["description"].Value;
                md.name = n.Attributes["name"].Value;
                md.xml= n.Attributes["xml"].Value;
                if (md.isMaster())
                {
                    md.ip1= n.Attributes["ip1"].Value;
                    md.ip2 = n.Attributes["ip2"].Value;
                    md.step = int.Parse(n.Attributes["step"].Value);
                    md.port = int.Parse(n.Attributes["port"].Value);
                }
                else
                {
                    md.port = int.Parse(n.Attributes["port"].Value);
                }
                md.registers=LoadRegistersModBus(sub.path + "/" + md.xml + ".xml");
                devs.Add(md);
            }
            return devs;
        }
    public static Dictionary<String,Variable> LoadVariables(XmlDocument xml,Subsystem sub)
        {
            Dictionary<String, Variable> vars = new Dictionary<String, Variable>();
            short id = 1;
            int address = 0;
            foreach (XmlNode x in xml.SelectNodes("subsystem/variable")) // выбор подподсекции device подподсекции Device основной секции Apax конфигурационного файла OPULeft.xml или OPURight.xml для считывания значений
            {
                String file = sub.path + "/" + x.Attributes["xml"].Value+".xml";
                XmlDocument varxml = new XmlDocument();
                varxml.Load(file);
                foreach (XmlNode n in varxml.SelectNodes("vars/var"))
                {
                    Variable var = new Variable();
                    var.name = n.Attributes["name"].Value;
                    if (vars.ContainsKey(var.name)) continue;
                    var.description = n.Attributes["description"].Value;
                    var.format = byte.Parse(n.Attributes["format"].Value);
                    sub.max_id = id;
                    sub.ids.Add(id, var.name);
                    var.id = id++;

                    var.address = address;
                    address += var.lenData() + 1;         // Добавим байт достоверности
                    vars.Add(var.name, var);
                }
            }
            maxAddres = address;
            return vars;
        }
        public static List<Register> LoadRegistersModBus(string namefile) //  загрузка регистров Modbus
        {
            XmlDocument regxml = new XmlDocument();
            regxml.Load(namefile);
            List<Register> regs = new List<Register>();
            foreach (XmlNode n in regxml.SelectNodes("table/records/record")) // Считывание регистров из указанной подсекции конфигурационного файла
            {
                string name = "", description = "", address = "0", ssize = "1", type = "0", format = "2", unitId = "1";
                foreach (XmlNode m in n.ChildNodes)
                {
                    string attr = m.Attributes["name"].Value, attr_txt = m.InnerText;

                    switch (attr) //  В зависимости от типа аттрибута, присваивается значение переменной
                    {
                        case "name":
                            name = attr_txt;
                            break;
                        case "description":
                            description = attr_txt;
                            break;
                        case "address":
                            address = attr_txt;
                            break;
                        case "size":
                            ssize = attr_txt;
                            break;
                        case "type":
                            type = attr_txt;
                            break;
                        case "format":
                            format = attr_txt;
                            break;
                        case "unitId":
                            unitId = attr_txt;
                            break;
                    }
                }
                Register reg = new Register();
                reg.size = ushort.Parse(ssize);
                reg.address = ushort.Parse(address);
                reg.type = int.Parse(type);
                reg.format = int.Parse(format);
                if (reg.type < 2) reg.format = 1;
                reg.name = name;
                reg.description = description;
                regs.Add(reg);
            }
            return regs;
        }

    }
}
