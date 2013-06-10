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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.butSettings = new System.Windows.Forms.Button();
            this.butDimmers = new System.Windows.Forms.Button();
            this.butConDisc = new System.Windows.Forms.Button();
            this.butDevices = new System.Windows.Forms.Button();
            this.butCounters = new System.Windows.Forms.Button();
            this.butSensors = new System.Windows.Forms.Button();
            this.butChat = new System.Windows.Forms.Button();
            this.lStatus = new System.Windows.Forms.Label();
            this.lStatusValue = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.butAdmin = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lInTemp = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lOutTemp = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // butSettings
            // 
            this.butSettings.BackColor = System.Drawing.Color.Transparent;
            this.butSettings.BackgroundImage = global::AsyncClient.Properties.Resources.Very_Basic_Settings_icon;
            this.butSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butSettings.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butSettings.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.butSettings.Location = new System.Drawing.Point(70, 187);
            this.butSettings.Name = "butSettings";
            this.butSettings.Size = new System.Drawing.Size(122, 115);
            this.butSettings.TabIndex = 0;
            this.toolTip1.SetToolTip(this.butSettings, "Пункт \"Настройки\"\r\nПозволяет задать настройки подключения.");
            this.butSettings.UseVisualStyleBackColor = false;
            this.butSettings.Click += new System.EventHandler(this.butSettings_Click);
            // 
            // butDimmers
            // 
            this.butDimmers.BackColor = System.Drawing.Color.Transparent;
            this.butDimmers.BackgroundImage = global::AsyncClient.Properties.Resources.Memory_Cards_Torrent_icon;
            this.butDimmers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butDimmers.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butDimmers.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.butDimmers.Location = new System.Drawing.Point(420, 75);
            this.butDimmers.Name = "butDimmers";
            this.butDimmers.Size = new System.Drawing.Size(122, 115);
            this.butDimmers.TabIndex = 2;
            this.butDimmers.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.toolTip1.SetToolTip(this.butDimmers, "Пункт \"Димеры\".\r\nПозволяет изменить значение диммерных устройств\r\nподключенных к " +
                    "системе.");
            this.butDimmers.UseVisualStyleBackColor = false;
            this.butDimmers.Click += new System.EventHandler(this.butDimmers_Click);
            // 
            // butConDisc
            // 
            this.butConDisc.BackColor = System.Drawing.Color.Transparent;
            this.butConDisc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butConDisc.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butConDisc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.butConDisc.Location = new System.Drawing.Point(312, 222);
            this.butConDisc.Name = "butConDisc";
            this.butConDisc.Size = new System.Drawing.Size(140, 45);
            this.butConDisc.TabIndex = 3;
            this.butConDisc.Text = "Подключиться";
            this.butConDisc.UseVisualStyleBackColor = false;
            this.butConDisc.Click += new System.EventHandler(this.butConDisc_Click);
            // 
            // butDevices
            // 
            this.butDevices.BackColor = System.Drawing.Color.Transparent;
            this.butDevices.BackgroundImage = global::AsyncClient.Properties.Resources.Sections_of_Website_Solutions_icon;
            this.butDevices.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butDevices.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butDevices.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.butDevices.Location = new System.Drawing.Point(233, 291);
            this.butDevices.Name = "butDevices";
            this.butDevices.Size = new System.Drawing.Size(122, 115);
            this.butDevices.TabIndex = 4;
            this.butDevices.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.toolTip1.SetToolTip(this.butDevices, "Пункт \"Простое устройство\".\r\nПозволяет управлять такими устройствами как розетка," +
                    " лампа и т.д.");
            this.butDevices.UseVisualStyleBackColor = false;
            this.butDevices.Click += new System.EventHandler(this.butDevices_Click);
            // 
            // butCounters
            // 
            this.butCounters.BackColor = System.Drawing.Color.Transparent;
            this.butCounters.BackgroundImage = global::AsyncClient.Properties.Resources.Objects_Scale_icon;
            this.butCounters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butCounters.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butCounters.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.butCounters.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butCounters.Location = new System.Drawing.Point(420, 291);
            this.butCounters.Name = "butCounters";
            this.butCounters.Size = new System.Drawing.Size(122, 115);
            this.butCounters.TabIndex = 5;
            this.butCounters.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.toolTip1.SetToolTip(this.butCounters, "Пункт \"Счтётчики\".\r\nПозволяет просмотреть зачения счёткика\r\nза определённый перио" +
                    "д. ");
            this.butCounters.UseVisualStyleBackColor = false;
            this.butCounters.Click += new System.EventHandler(this.butCounters_Click);
            // 
            // butSensors
            // 
            this.butSensors.BackColor = System.Drawing.Color.Transparent;
            this.butSensors.BackgroundImage = global::AsyncClient.Properties.Resources.Industry_Electronics_icon1;
            this.butSensors.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butSensors.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butSensors.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.butSensors.Location = new System.Drawing.Point(233, 75);
            this.butSensors.Name = "butSensors";
            this.butSensors.Size = new System.Drawing.Size(122, 115);
            this.butSensors.TabIndex = 6;
            this.butSensors.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.toolTip1.SetToolTip(this.butSensors, "Пункт \"Датчики\"\r\nПозволяет просмотреть текущее состояние всех\r\nподключённых к сис" +
                    "теме датчиков.");
            this.butSensors.UseVisualStyleBackColor = false;
            this.butSensors.Click += new System.EventHandler(this.butSensors_Click);
            // 
            // butChat
            // 
            this.butChat.BackColor = System.Drawing.Color.Transparent;
            this.butChat.BackgroundImage = global::AsyncClient.Properties.Resources.Buzz_Message_outline_icon1;
            this.butChat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butChat.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butChat.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.butChat.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butChat.Location = new System.Drawing.Point(587, 187);
            this.butChat.Name = "butChat";
            this.butChat.Size = new System.Drawing.Size(122, 115);
            this.butChat.TabIndex = 7;
            this.butChat.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.toolTip1.SetToolTip(this.butChat, "Пункт \"Чат\".\r\nПозволяет обмениваться сообщениями \r\nс пользователями системы.");
            this.butChat.UseVisualStyleBackColor = false;
            this.butChat.Click += new System.EventHandler(this.butChat_Click);
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.BackColor = System.Drawing.Color.Transparent;
            this.lStatus.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStatus.ForeColor = System.Drawing.Color.Black;
            this.lStatus.Location = new System.Drawing.Point(66, 400);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(69, 21);
            this.lStatus.TabIndex = 8;
            this.lStatus.Text = "Статус:";
            // 
            // lStatusValue
            // 
            this.lStatusValue.AutoSize = true;
            this.lStatusValue.BackColor = System.Drawing.Color.Transparent;
            this.lStatusValue.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStatusValue.ForeColor = System.Drawing.Color.Black;
            this.lStatusValue.Location = new System.Drawing.Point(132, 400);
            this.lStatusValue.Name = "lStatusValue";
            this.lStatusValue.Size = new System.Drawing.Size(81, 21);
            this.lStatusValue.TabIndex = 9;
            this.lStatusValue.Text = "значение";
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // butAdmin
            // 
            this.butAdmin.BackColor = System.Drawing.Color.Transparent;
            this.butAdmin.BackgroundImage = global::AsyncClient.Properties.Resources.Users_Worker_icon;
            this.butAdmin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butAdmin.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butAdmin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.butAdmin.Location = new System.Drawing.Point(67, 47);
            this.butAdmin.Name = "butAdmin";
            this.butAdmin.Size = new System.Drawing.Size(55, 50);
            this.butAdmin.TabIndex = 10;
            this.butAdmin.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.toolTip1.SetToolTip(this.butAdmin, "Пункт \"Конфигурация\".\r\nПозволяет администраторусистемы \r\nдобавлятьудалять устройс" +
                    "тва и пользователей.");
            this.butAdmin.UseVisualStyleBackColor = false;
            this.butAdmin.Click += new System.EventHandler(this.butAdmin_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AsyncClient.Properties.Resources.Weather_Rain_icon;
            this.pictureBox1.Location = new System.Drawing.Point(564, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AsyncClient.Properties.Resources.Weather_Thermometer_icon;
            this.pictureBox2.Location = new System.Drawing.Point(602, 320);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(53, 60);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // lInTemp
            // 
            this.lInTemp.AutoSize = true;
            this.lInTemp.BackColor = System.Drawing.Color.Transparent;
            this.lInTemp.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lInTemp.ForeColor = System.Drawing.Color.Black;
            this.lInTemp.Location = new System.Drawing.Point(607, 381);
            this.lInTemp.Name = "lInTemp";
            this.lInTemp.Size = new System.Drawing.Size(81, 21);
            this.lInTemp.TabIndex = 13;
            this.lInTemp.Text = "значение";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::AsyncClient.Properties.Resources.Weather_Thermometer_icon1;
            this.pictureBox3.Location = new System.Drawing.Point(670, 13);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(53, 60);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 14;
            this.pictureBox3.TabStop = false;
            // 
            // lOutTemp
            // 
            this.lOutTemp.AutoSize = true;
            this.lOutTemp.BackColor = System.Drawing.Color.Transparent;
            this.lOutTemp.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lOutTemp.ForeColor = System.Drawing.Color.Black;
            this.lOutTemp.Location = new System.Drawing.Point(675, 76);
            this.lOutTemp.Name = "lOutTemp";
            this.lOutTemp.Size = new System.Drawing.Size(81, 21);
            this.lOutTemp.TabIndex = 15;
            this.lOutTemp.Text = "значение";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "СУ УмныйДом";
            this.notifyIcon1.Visible = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.BackgroundImage = global::AsyncClient.Properties.Resources.house_hi;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(777, 430);
            this.Controls.Add(this.lOutTemp);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.lInTemp);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.butAdmin);
            this.Controls.Add(this.lStatusValue);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.butChat);
            this.Controls.Add(this.butSensors);
            this.Controls.Add(this.butCounters);
            this.Controls.Add(this.butDevices);
            this.Controls.Add(this.butConDisc);
            this.Controls.Add(this.butDimmers);
            this.Controls.Add(this.butSettings);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(793, 468);
            this.MinimumSize = new System.Drawing.Size(793, 468);
            this.Name = "MainForm";
            this.Text = "Главное окно";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butSettings;
        private System.Windows.Forms.Button butDimmers;
        private System.Windows.Forms.Button butConDisc;
        private System.Windows.Forms.Button butDevices;
        private System.Windows.Forms.Button butCounters;
        private System.Windows.Forms.Button butSensors;
        private System.Windows.Forms.Button butChat;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.Label lStatusValue;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button butAdmin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lInTemp;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lOutTemp;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

