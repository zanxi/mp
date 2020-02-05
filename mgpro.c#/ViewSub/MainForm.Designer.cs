namespace ViewSub
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SubsView = new System.Windows.Forms.TreeView();
            this.MessageBox = new System.Windows.Forms.RichTextBox();
            this.seekText = new System.Windows.Forms.TextBox();
            this.dataView = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VerifyNames = new System.Windows.Forms.ToolStripMenuItem();
            this.makeCode = new System.Windows.Forms.ToolStripMenuItem();
            this.Reload = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exiter = new System.Windows.Forms.ToolStripMenuItem();
            this.message = new System.Windows.Forms.Timer(this.components);
            this.subsystem = new System.Windows.Forms.Label();
            this.UpdateTable = new System.Windows.Forms.Timer(this.components);
            this.buttonConnect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // SubsView
            // 
            this.SubsView.Location = new System.Drawing.Point(12, 46);
            this.SubsView.Name = "SubsView";
            this.SubsView.Size = new System.Drawing.Size(240, 466);
            this.SubsView.TabIndex = 0;
            this.SubsView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.SubsSelect);
            // 
            // MessageBox
            // 
            this.MessageBox.Location = new System.Drawing.Point(12, 518);
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.Size = new System.Drawing.Size(1542, 231);
            this.MessageBox.TabIndex = 1;
            this.MessageBox.Text = "";
            // 
            // seekText
            // 
            this.seekText.Location = new System.Drawing.Point(1104, 12);
            this.seekText.Name = "seekText";
            this.seekText.Size = new System.Drawing.Size(450, 20);
            this.seekText.TabIndex = 2;
            this.seekText.TextChanged += new System.EventHandler(this.seekText_TextChanged);
            // 
            // dataView
            // 
            this.dataView.AllowUserToAddRows = false;
            this.dataView.AllowUserToDeleteRows = false;
            this.dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.description,
            this.value});
            this.dataView.Location = new System.Drawing.Point(258, 48);
            this.dataView.Name = "dataView";
            this.dataView.ReadOnly = true;
            this.dataView.Size = new System.Drawing.Size(1296, 466);
            this.dataView.TabIndex = 4;
            this.dataView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataView_CellContentClick);
            // 
            // name
            // 
            this.name.Frozen = true;
            this.name.HeaderText = "Имя";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // description
            // 
            this.description.Frozen = true;
            this.description.HeaderText = "Описание";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Width = 950;
            // 
            // value
            // 
            this.value.Frozen = true;
            this.value.HeaderText = "Значение";
            this.value.Name = "value";
            this.value.ReadOnly = true;
            this.value.Width = 200;
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.параметрыToolStripMenuItem,
            this.exiter});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1566, 24);
            this.menuMain.TabIndex = 5;
            this.menuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VerifyNames,
            this.makeCode,
            this.Reload});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // VerifyNames
            // 
            this.VerifyNames.Name = "VerifyNames";
            this.VerifyNames.Size = new System.Drawing.Size(199, 22);
            this.VerifyNames.Text = "Проверить имена";
            this.VerifyNames.Click += new System.EventHandler(this.VerifyNames_Click);
            // 
            // makeCode
            // 
            this.makeCode.Name = "makeCode";
            this.makeCode.Size = new System.Drawing.Size(199, 22);
            this.makeCode.Text = "Построить программу";
            this.makeCode.Click += new System.EventHandler(this.makeCode_Click);
            // 
            // Reload
            // 
            this.Reload.Name = "Reload";
            this.Reload.Size = new System.Drawing.Size(199, 22);
            this.Reload.Text = "Перезагрузить";
            this.Reload.Click += new System.EventHandler(this.Reload_Click);
            // 
            // параметрыToolStripMenuItem
            // 
            this.параметрыToolStripMenuItem.Name = "параметрыToolStripMenuItem";
            this.параметрыToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.параметрыToolStripMenuItem.Text = "Параметры";
            // 
            // exiter
            // 
            this.exiter.Name = "exiter";
            this.exiter.Size = new System.Drawing.Size(53, 20);
            this.exiter.Text = "Выход";
            this.exiter.Click += new System.EventHandler(this.exiter_Click);
            // 
            // message
            // 
            this.message.Enabled = true;
            this.message.Interval = 1000;
            this.message.Tick += new System.EventHandler(this.message_Tick);
            // 
            // subsystem
            // 
            this.subsystem.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.subsystem.Location = new System.Drawing.Point(335, 8);
            this.subsystem.Name = "subsystem";
            this.subsystem.Size = new System.Drawing.Size(500, 24);
            this.subsystem.TabIndex = 6;
            this.subsystem.Text = "Нет выбора ";
            this.subsystem.UseMnemonic = false;
            // 
            // UpdateTable
            // 
            this.UpdateTable.Enabled = true;
            this.UpdateTable.Interval = 1000;
            this.UpdateTable.Tick += new System.EventHandler(this.UpdateTable_Tick);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(859, 12);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(86, 23);
            this.buttonConnect.TabIndex = 8;
            this.buttonConnect.Text = "Подключить";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1566, 761);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.subsystem);
            this.Controls.Add(this.dataView);
            this.Controls.Add(this.seekText);
            this.Controls.Add(this.MessageBox);
            this.Controls.Add(this.SubsView);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView SubsView;
        private System.Windows.Forms.RichTextBox MessageBox;
        private System.Windows.Forms.TextBox seekText;
        private System.Windows.Forms.DataGridView dataView;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VerifyNames;
        private System.Windows.Forms.ToolStripMenuItem makeCode;
        private System.Windows.Forms.ToolStripMenuItem параметрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exiter;
        private System.Windows.Forms.Timer message;
        private System.Windows.Forms.Label subsystem;
        private System.Windows.Forms.Timer UpdateTable;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ToolStripMenuItem Reload;
    }
}

