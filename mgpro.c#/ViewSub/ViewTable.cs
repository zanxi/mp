using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using helper;

namespace ViewSub
{
    public class ViewTable
    {
        private DataGridView dataTable;
        private List<Variable> listView = null;
        private Subsystem  sub;
        private Dumper dump;
        public ViewTable(DataGridView dataTable)
        {
            this.dataTable = dataTable;
        }

        internal void Redraw(Subsystem sub, Dumper dump, string seek)
        {
            this.sub = sub;
            this.dump = dump;
            listView = sub.variables.Values.ToList();
            if (seek.Length > 0)
            {
                List<Variable> listTemp = new List<Variable>();
                foreach (Variable var in listView)
                {
                    if (var.name.ToLower().Contains(seek) || var.description.ToLower().Contains(seek)) listTemp.Add(var);
                }
                listView = new List<Variable>();
                foreach (Variable var in listTemp)
                {
                    listView.Add(var);
                }
            }
            dataTable.Rows.Clear();
            dataTable.ColumnCount = 3;
            if (listView.Count > 0) dataTable.Rows.Add(listView.Count);
            int i = 0;
            //            listView.Sort();
            listView.Sort(delegate (Variable v1, Variable v2)
            { return v1.name.CompareTo(v2.name); });
            foreach (Variable var in listView)
            {
                dataTable.Rows[i].Cells["name"].Value = var.name;
                dataTable.Rows[i].Cells["description"].Value = var.description;
                i++;
            }
            Update();
        }
        internal void Update()
        {
            for (int i = 0; i < dataTable.RowCount; i++)
            {
                string name = (string)dataTable.Rows[i].Cells["name"].Value;
                if (name == null) continue;
                string value = dump.getValue(sub.variables[name].id);
                dataTable.Rows[i].Cells["value"].Value = value;
            }
        }
    }

}
