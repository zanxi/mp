using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace helper
{
    public class Project
    {
        public String name;
        public String description;
        public String path;
        public String defdrv;
        public Dictionary<String, Subsystem> subs;
        public Dictionary<String, Driver> drvs;
        public Project()
        {
            subs = new Dictionary<string, Subsystem>();
            drvs = new Dictionary<string, Driver>();
        }
        public void LoadProject(String nameMain)
        {
            XmlDocument mainxml = new XmlDocument();
            mainxml.Load(nameMain);
            LoadingUtils.LoadProject(mainxml,this);


        }
        public string VerifyNames()
        {
            string result = "";
            foreach(Subsystem sb in subs.Values)
            {
                Util.message("Подсистема " + sb.name);
                result += sb.VerifyNames();
            }
            return result;
        }
        public void MakeCode()
        {
            foreach (Subsystem sbp in subs.Values)
            {
                String sbPath = path + sbp.path + "/";
                Util.message("Подсистема " + sbp.name );
                sbp.MakeCode(sbPath);
            }
            return;
        }
    }
}
