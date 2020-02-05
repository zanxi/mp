using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using helper;

namespace ViewSub
{
    public partial class MainForm : Form
    {
        Project mp = new Project();
        Dictionary<string, Dumper> values=new Dictionary<string, Dumper>();
        Subsystem activeSub = null;
        Dumper activeDump = null;
        public string seeking = "";
        ViewTable viewTable = null;
        string start;
        public MainForm(string startxml)
        {
            //start = startxml;
            startxml = "d:\\mgpro\\du\\ctrl.xml";
            start = "d:\\mgpro\\du\\ctrl.xml";
            InitializeComponent();
            Util.messageLine = new List<string>();
            Util.message("Начинаем загрузку ");
            mp.LoadProject(startxml);
            Util.message("Загрузка завершена");
            SubsView.BeginUpdate();
            SubsView.Nodes.Clear();
            foreach(Subsystem sub in mp.subs.Values)
            {
                SubsView.Nodes.Add(new TreeNode(sub.name));
                Dumper value = new Dumper(sub.main, 1080, sub.max_id);
                values.Add(sub.name, value);
            }
            SubsView.Sort();
            SubsView.EndUpdate();
            subsystem.BackColor = Color.Red;
            viewTable = new ViewTable(dataView);
        }

        private void seekText_TextChanged(object sender, EventArgs e)
        {
            seeking = seekText.Text.ToLower();
            if(activeDump.isConnected()) viewTable.Redraw(activeSub, activeDump, seeking);

        }

        private void message_Tick(object sender, EventArgs e)
        {
            lock (Util.messageLine)
            {
                for (int i = 0; i < Util.messageLine.Count; i++)
                {

                    MessageBox.Text = Util.messageLine[i] + MessageBox.Text;
                }
                Util.messageLine.Clear();
            }
            if (activeSub == null)
            {
                subsystem.Text = "Не выбран";
                subsystem.BackColor = Color.Red;
                buttonConnect.Text = "Подключить";
            }
            if (activeDump!=null)
            {
                subsystem.Text = activeSub.description;
                subsystem.BackColor = activeDump.isConnected() ? Color.Green : Color.Red;
                            buttonConnect.Text = activeDump.isConnected() ? "Отключить" : "Подключить";
            }

        }

        private void SubsSelect(object sender, TreeViewEventArgs e)
        {
            string node = e.Node.Text;
            activeSub=mp.subs[node];
            activeDump = values[node];
            if (activeDump.isConnected())
            {
                viewTable.Redraw(activeSub, activeDump, seeking);
                buttonConnect.Text = "Отключить";

            }else
            {
                buttonConnect.Text = "Подключить";
                viewTable.Redraw(activeSub, activeDump, seeking);
            }
        }

        private void VerifyNames_Click(object sender, EventArgs e)
        {
            Util.message("Проверка имен ...");
            String result = mp.VerifyNames();
            if (result.Length != 0)
            {
                Util.message("Найдены ошибки при проверке имен\n" + result);
            }
            Util.message("Проверка имен завершена");

        }

        private void makeCode_Click(object sender, EventArgs e)
        {
            Util.message("Строим результат ...");
            mp.MakeCode();
            Util.message("Результирующий код построен");

        }

        private void UpdateTable_Tick(object sender, EventArgs e)
        {
            viewTable.Update();
            dataView.Refresh();
        }

        private void exiter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

         private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (activeDump == null) return;
            if (buttonConnect.Text.CompareTo("Подключить")>=0)
            {
                if (!activeDump.isConnected()) activeDump.Connect();
                if (activeDump.isConnected()) 
                {
                    viewTable.Redraw(activeSub, activeDump, seeking);
                }
            }
            else
            {
                if (activeDump.isConnected()) activeDump.Close();
            }
            if (activeDump.isConnected())
                buttonConnect.Text="Отключить";
            else buttonConnect.Text = "Подключить";

        }

        private void Reload_Click(object sender, EventArgs e)
        {
            Util.message("Начинаем загрузку ");
            mp = new Project();
            mp.LoadProject(start);
            Util.message("Загрузка завершена");
            SubsView.BeginUpdate();
            SubsView.Nodes.Clear();
            values = new Dictionary<string, Dumper>();
            foreach (Subsystem sub in mp.subs.Values)
            {
                SubsView.Nodes.Add(new TreeNode(sub.name));
                Dumper value = new Dumper(sub.main, 1080, sub.max_id);
                values.Add(sub.name, value);
            }
            SubsView.Sort();
            SubsView.EndUpdate();
            subsystem.BackColor = Color.Red;
            activeDump = null;
            activeSub = null;
        }

        private void dataView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void PositionElementWinForm()
        {
            this.Location = new Point(50, 50);
            this.Width = (int)(SystemInformation.PrimaryMonitorSize.Width * 0.88);
            this.Height = (int)(SystemInformation.PrimaryMonitorSize.Height * 0.85);
            this.Opacity = 1.0;

            //textBoxSearch.Location = new Point(buttonOpenDBx, buttonOpenDBy - 30);
            SubsView.Location = new Point(12, 46);
            SubsView.Width = 240;
            SubsView.Height = 466;

            dataView.Location = new Point(258, 48);
            dataView.Width = 1296;
            dataView.Height = 466;

            MessageBox.Location = new Point(12, 518);
            MessageBox.Width = 1542;
            MessageBox.Height = 231;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PositionElementWinForm();            

        }
    }

}
