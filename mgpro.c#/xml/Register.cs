using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helper
{
    public class Register
    {
        public int type;
        public int format;
        public int size;
        public int address;
        public String name;
        public String description;
        public ushort GetLen()
        {
            if (type < 2) return (ushort)(address + size);
            int len = 1;
            if (format >= 4 && format <= 9) len = 2;
            else if (format >= 11 && format <= 15) len = 4;
            return (ushort)(address + (len * size));
        }
        public object ValueToString(object value)
        {
            if (type == 0 || type == 1) return value;
            else
            {
                if (format > 0 && format <= 7) return ((int)value).ToString();
                if (format > 7 && format < 11) return F2S((float)value);
                if (format > 10 && format < 14) return ((long)value).ToString();
                if (format > 13 && format < 16) return ((float)value).ToString("F"); ;
                if (format > 17 && format < 20) return (string)value;
            }
            return "!!!";

        }
        internal object F2S(float value)
        {
            string rez;
            int pos = 1;
            do
            {
                rez = value.ToString("F" + pos.ToString());
                pos++;
            } while (!"0".Equals(rez.Substring(rez.Length - 1)));
            return rez;
        }
     }
}
