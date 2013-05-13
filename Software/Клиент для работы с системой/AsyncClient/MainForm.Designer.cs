namespace AsyncClient
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.trvDevice = new System.Windows.Forms.TreeView();
            this.gbActions = new System.Windows.Forms.GroupBox();
            this.grpCounters = new System.Windows.Forms.GroupBox();
            this.butReport = new System.Windows.Forms.Button();
            this.lCounterValue = new System.Windows.Forms.Label();
            this.lCurentCountValue = new System.Windows.Forms.Label();
            this.grpDevice = new System.Windows.Forms.GroupBox();
            this.butAction = new System.Windows.Forms.Button();
            this.lDevName = new System.Windows.Forms.Label();
            this.lDevName_ = new System.Windows.Forms.Label();
            this.grpDimmers = new System.Windows.Forms.GroupBox();
            this.butDimmersSet = new System.Windows.Forms.Button();
            this.tbDimmersPower = new System.Windows.Forms.TextBox();
            this.lDimmersPowers = new System.Windows.Forms.Label();
            this.grpSensor = new System.Windows.Forms.GroupBox();
            this.lSensor = new System.Windows.Forms.Label();
            this.lSensorValue = new System.Windows.Forms.Label();
            this.connStat = new System.Windows.Forms.StatusStrip();
            this.stLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbMess = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfCoonectParms = new System.Windows.Forms.ToolStripMenuItem();
            this.Connect = new System.Windows.Forms.ToolStripMenuItem();
            this.Disconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.SystemConf = new System.Windows.Forms.ToolStripMenuItem();
            this.butGetUpdate = new System.Windows.Forms.Button();
            this.gbActions.SuspendLayout();
            this.grpCounters.SuspendLayout();
            this.grpDevice.SuspendLayout();
            this.grpDimmers.SuspendLayout();
            this.grpSensor.SuspendLayout();
            this.connStat.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvDevice
            // 
            this.trvDevice.Location = new System.Drawing.Point(253, 49);
            this.trvDevice.Name = "trvDevice";
            this.trvDevice.Size = new System.Drawing.Size(159, 161);
            this.trvDevice.TabIndex = 6;
            this.trvDevice.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvDevice_AfterSelect);
            // 
            // gbActions
            // 
            this.gbActions.Controls.Add(this.grpCounters);
            this.gbActions.Controls.Add(this.grpDevice);
            this.gbActions.Controls.Add(this.grpDimmers);
            this.gbActions.Controls.Add(this.grpSensor);
            this.gbActions.Location = new System.Drawing.Point(429, 49);
            this.gbActions.Name = "gbActions";
            this.gbActions.Size = new System.Drawing.Size(379, 199);
            this.gbActions.TabIndex = 7;
            this.gbActions.TabStop = false;
            this.gbActions.Text = "Действия";
            // 
            // grpCounters
            // 
            this.grpCounters.Controls.Add(this.butReport);
            this.grpCounters.Controls.Add(this.lCounterValue);
            this.grpCounters.Controls.Add(this.lCurentCountValue);
            this.grpCounters.Location = new System.Drawing.Point(197, 104);
            this.grpCounters.Name = "grpCounters";
            this.grpCounters.Size = new System.Drawing.Size(174, 74);
            this.grpCounters.TabIndex = 11;
            this.grpCounters.TabStop = false;
            this.grpCounters.Text = "Счётчики";
            // 
            // butReport
            // 
            this.butReport.Location = new System.Drawing.Point(7, 45);
            this.butReport.Name = "butReport";
            this.butReport.Size = new System.Drawing.Size(75, 23);
            this.butReport.TabIndex = 2;
            this.butReport.Text = "Отчёт";
            this.butReport.UseVisualStyleBackColor = true;
            this.butReport.Click += new System.EventHandler(this.butReport_Click);
            // 
            // lCounterValue
            // 
            this.lCounterValue.AutoSize = true;
            this.lCounterValue.Location = new System.Drawing.Point(119, 20);
            this.lCounterValue.Name = "lCounterValue";
            this.lCounterValue.Size = new System.Drawing.Size(33, 13);
            this.lCounterValue.TabIndex = 1;
            this.lCounterValue.Text = "value";
            // 
            // lCurentCountValue
            // 
            this.lCurentCountValue.AutoSize = true;
            this.lCurentCountValue.Location = new System.Drawing.Point(8, 20);
            this.lCurentCountValue.Name = "lCurentCountValue";
            this.lCurentCountValue.Size = new System.Drawing.Size(111, 13);
            this.lCurentCountValue.TabIndex = 0;
            this.lCurentCountValue.Text = "Текущее значение - ";
            // 
            // grpDevice
            // 
            this.grpDevice.Controls.Add(this.butAction);
            this.grpDevice.Controls.Add(this.lDevName);
            this.grpDevice.Controls.Add(this.lDevName_);
            this.grpDevice.Location = new System.Drawing.Point(13, 19);
            this.grpDevice.Name = "grpDevice";
            this.grpDevice.Size = new System.Drawing.Size(178, 77);
            this.grpDevice.TabIndex = 10;
            this.grpDevice.TabStop = false;
            this.grpDevice.Text = "Устрйство";
            // 
            // butAction
            // 
            this.butAction.Location = new System.Drawing.Point(9, 48);
            this.butAction.Name = "butAction";
            this.butAction.Size = new System.Drawing.Size(75, 23);
            this.butAction.TabIndex = 0;
            this.butAction.Text = "Включить";
            this.butAction.UseVisualStyleBackColor = true;
            this.butAction.Click += new System.EventHandler(this.butAction_Click);
            // 
            // lDevName
            // 
            this.lDevName.AutoSize = true;
            this.lDevName.Location = new System.Drawing.Point(6, 21);
            this.lDevName.Name = "lDevName";
            this.lDevName.Size = new System.Drawing.Size(98, 13);
            this.lDevName.TabIndex = 6;
            this.lDevName.Text = "Имя устройства - ";
            // 
            // lDevName_
            // 
            this.lDevName_.AutoSize = true;
            this.lDevName_.Location = new System.Drawing.Point(108, 21);
            this.lDevName_.Name = "lDevName_";
            this.lDevName_.Size = new System.Drawing.Size(33, 13);
            this.lDevName_.TabIndex = 7;
            this.lDevName_.Text = "value";
            // 
            // grpDimmers
            // 
            this.grpDimmers.Controls.Add(this.butDimmersSet);
            this.grpDimmers.Controls.Add(this.tbDimmersPower);
            this.grpDimmers.Controls.Add(this.lDimmersPowers);
            this.grpDimmers.Location = new System.Drawing.Point(197, 19);
            this.grpDimmers.Name = "grpDimmers";
            this.grpDimmers.Size = new System.Drawing.Size(174, 77);
            this.grpDimmers.TabIndex = 9;
            this.grpDimmers.TabStop = false;
            this.grpDimmers.Text = "Димер";
            // 
            // butDimmersSet
            // 
            this.butDimmersSet.Location = new System.Drawing.Point(8, 48);
            this.butDimmersSet.Name = "butDimmersSet";
            this.butDimmersSet.Size = new System.Drawing.Size(75, 23);
            this.butDimmersSet.TabIndex = 3;
            this.butDimmersSet.Text = "Задать";
            this.butDimmersSet.UseVisualStyleBackColor = true;
            this.butDimmersSet.Click += new System.EventHandler(this.butDimmersSet_Click);
            // 
            // tbDimmersPower
            // 
            this.tbDimmersPower.Location = new System.Drawing.Point(110, 21);
            this.tbDimmersPower.Name = "tbDimmersPower";
            this.tbDimmersPower.Size = new System.Drawing.Size(48, 20);
            this.tbDimmersPower.TabIndex = 1;
            // 
            // lDimmersPowers
            // 
            this.lDimmersPowers.AutoSize = true;
            this.lDimmersPowers.Location = new System.Drawing.Point(5, 24);
            this.lDimmersPowers.Name = "lDimmersPowers";
            this.lDimmersPowers.Size = new System.Drawing.Size(99, 13);
            this.lDimmersPowers.TabIndex = 2;
            this.lDimmersPowers.Text = "Значение димера:";
            // 
            // grpSensor
            // 
            this.grpSensor.Controls.Add(this.lSensor);
            this.grpSensor.Controls.Add(this.lSensorValue);
            this.grpSensor.Location = new System.Drawing.Point(13, 104);
            this.grpSensor.Name = "grpSensor";
            this.grpSensor.Size = new System.Drawing.Size(174, 74);
            this.grpSensor.TabIndex = 8;
            this.grpSensor.TabStop = false;
            this.grpSensor.Text = "Датчик";
            // 
            // lSensor
            // 
            this.lSensor.AutoSize = true;
            this.lSensor.Location = new System.Drawing.Point(6, 35);
            this.lSensor.Name = "lSensor";
            this.lSensor.Size = new System.Drawing.Size(107, 13);
            this.lSensor.TabIndex = 4;
            this.lSensor.Text = "Значение датчика - ";
            // 
            // lSensorValue
            // 
            this.lSensorValue.AutoSize = true;
            this.lSensorValue.Location = new System.Drawing.Point(119, 34);
            this.lSensorValue.Name = "lSensorValue";
            this.lSensorValue.Size = new System.Drawing.Size(33, 13);
            this.lSensorValue.TabIndex = 5;
            this.lSensorValue.Text = "value";
            // 
            // connStat
            // 
            this.connStat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stLabel});
            this.connStat.Location = new System.Drawing.Point(0, 259);
            this.connStat.Name = "connStat";
            this.connStat.Size = new System.Drawing.Size(820, 22);
            this.connStat.TabIndex = 8;
            this.connStat.Text = "Отключен";
            // 
            // stLabel
            // 
            this.stLabel.Name = "stLabel";
            this.stLabel.Size = new System.Drawing.Size(64, 17);
            this.stLabel.Text = "Отключён";
            // 
            // tbMess
            // 
            this.tbMess.Location = new System.Drawing.Point(13, 49);
            this.tbMess.Multiline = true;
            this.tbMess.Name = "tbMess";
            this.tbMess.Size = new System.Drawing.Size(222, 199);
            this.tbMess.TabIndex = 9;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Settings,
            this.SystemConf});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(820, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Settings
            // 
            this.Settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfCoonectParms,
            this.Connect,
            this.Disconnect});
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(79, 20);
            this.Settings.Text = "Настройки";
            // 
            // ConfCoonectParms
            // 
            this.ConfCoonectParms.Name = "ConfCoonectParms";
            this.ConfCoonectParms.Size = new System.Drawing.Size(298, 22);
            this.ConfCoonectParms.Text = "Редактировать параметры подключения";
            this.ConfCoonectParms.Click += new System.EventHandler(this.ConfCoonectParms_Click);
            // 
            // Connect
            // 
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(298, 22);
            this.Connect.Text = "Подключится";
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Disconnect
            // 
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.Size = new System.Drawing.Size(298, 22);
            this.Disconnect.Text = "Отключится";
            this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // SystemConf
            // 
            this.SystemConf.Name = "SystemConf";
            this.SystemConf.Size = new System.Drawing.Size(134, 20);
            this.SystemConf.Text = "Конфигиурирование";
            this.SystemConf.Click += new System.EventHandler(this.SystemConf_Click);
            // 
            // butGetUpdate
            // 
            this.butGetUpdate.Location = new System.Drawing.Point(253, 225);
            this.butGetUpdate.Name = "butGetUpdate";
            this.butGetUpdate.Size = new System.Drawing.Size(75, 23);
            this.butGetUpdate.TabIndex = 8;
            this.butGetUpdate.Text = "Обновить";
            this.butGetUpdate.UseVisualStyleBackColor = true;
            this.butGetUpdate.Click += new System.EventHandler(this.butGetUpdate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 281);
            this.Controls.Add(this.butGetUpdate);
            this.Controls.Add(this.tbMess);
            this.Controls.Add(this.connStat);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.gbActions);
            this.Controls.Add(this.trvDevice);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Главное окно";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbActions.ResumeLayout(false);
            this.grpCounters.ResumeLayout(false);
            this.grpCounters.PerformLayout();
            this.grpDevice.ResumeLayout(false);
            this.grpDevice.PerformLayout();
            this.grpDimmers.ResumeLayout(false);
            this.grpDimmers.PerformLayout();
            this.grpSensor.ResumeLayout(false);
            this.grpSensor.PerformLayout();
            this.connStat.ResumeLayout(false);
            this.connStat.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvDevice;
        private System.Windows.Forms.GroupBox gbActions;
        private System.Windows.Forms.StatusStrip connStat;
        private System.Windows.Forms.ToolStripStatusLabel stLabel;
        private System.Windows.Forms.TextBox tbMess;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Settings;
        private System.Windows.Forms.ToolStripMenuItem ConfCoonectParms;
        private System.Windows.Forms.ToolStripMenuItem Connect;
        private System.Windows.Forms.ToolStripMenuItem Disconnect;
        private System.Windows.Forms.Label lSensor;
        private System.Windows.Forms.Button butDimmersSet;
        private System.Windows.Forms.Label lDimmersPowers;
        private System.Windows.Forms.Button butAction;
        private System.Windows.Forms.Label lDevName;
        private System.Windows.Forms.GroupBox grpDevice;
        private System.Windows.Forms.GroupBox grpDimmers;
        private System.Windows.Forms.GroupBox grpSensor;
        public System.Windows.Forms.Label lSensorValue;
        public System.Windows.Forms.TextBox tbDimmersPower;
        public System.Windows.Forms.Label lDevName_;
        private System.Windows.Forms.GroupBox grpCounters;
        private System.Windows.Forms.Button butReport;
        private System.Windows.Forms.Label lCounterValue;
        private System.Windows.Forms.Label lCurentCountValue;
        private System.Windows.Forms.Button butGetUpdate;
        private System.Windows.Forms.ToolStripMenuItem SystemConf;
    }
}

