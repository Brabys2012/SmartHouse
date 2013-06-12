namespace AsyncClient
{
    partial class AdminPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminPanel));
            this.panel3 = new System.Windows.Forms.Panel();
            this.gpAddDevice = new System.Windows.Forms.GroupBox();
            this.butAddDevice = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.tbNameLinkedDevice = new System.Windows.Forms.TextBox();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chbLinkWith = new System.Windows.Forms.CheckBox();
            this.cbDeviceType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbDeviceName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbDeviceNumber = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbPortNumber = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gpDelUser = new System.Windows.Forms.GroupBox();
            this.tbLoginToDel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.butDelUser = new System.Windows.Forms.Button();
            this.gbDelDevice = new System.Windows.Forms.GroupBox();
            this.tbNameToDel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.butDelDevice = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbAddUser = new System.Windows.Forms.GroupBox();
            this.cbRole = new System.Windows.Forms.ComboBox();
            this.buеCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPassConfirm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.butAdd = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            this.gpAddDevice.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gpDelUser.SuspendLayout();
            this.gbDelDevice.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbAddUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gpAddDevice);
            this.panel3.Location = new System.Drawing.Point(12, 359);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(516, 348);
            this.panel3.TabIndex = 14;
            this.panel3.TabStop = true;
            // 
            // gpAddDevice
            // 
            this.gpAddDevice.Controls.Add(this.butAddDevice);
            this.gpAddDevice.Controls.Add(this.label12);
            this.gpAddDevice.Controls.Add(this.tbNameLinkedDevice);
            this.gpAddDevice.Controls.Add(this.tbMessage);
            this.gpAddDevice.Controls.Add(this.label11);
            this.gpAddDevice.Controls.Add(this.chbLinkWith);
            this.gpAddDevice.Controls.Add(this.cbDeviceType);
            this.gpAddDevice.Controls.Add(this.label10);
            this.gpAddDevice.Controls.Add(this.tbDeviceName);
            this.gpAddDevice.Controls.Add(this.label9);
            this.gpAddDevice.Controls.Add(this.cbDeviceNumber);
            this.gpAddDevice.Controls.Add(this.label8);
            this.gpAddDevice.Controls.Add(this.label7);
            this.gpAddDevice.Controls.Add(this.cbPortNumber);
            this.gpAddDevice.Location = new System.Drawing.Point(19, 23);
            this.gpAddDevice.Name = "gpAddDevice";
            this.gpAddDevice.Size = new System.Drawing.Size(484, 315);
            this.gpAddDevice.TabIndex = 12;
            this.gpAddDevice.TabStop = false;
            this.gpAddDevice.Text = "Добавить устройство";
            // 
            // butAddDevice
            // 
            this.butAddDevice.BackColor = System.Drawing.Color.Transparent;
            this.butAddDevice.BackgroundImage = global::AsyncClient.Properties.Resources.Very_Basic_Ok_icon;
            this.butAddDevice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butAddDevice.Location = new System.Drawing.Point(403, 246);
            this.butAddDevice.Name = "butAddDevice";
            this.butAddDevice.Size = new System.Drawing.Size(75, 63);
            this.butAddDevice.TabIndex = 8;
            this.butAddDevice.UseVisualStyleBackColor = false;
            this.butAddDevice.Click += new System.EventHandler(this.butAddDevice_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 109);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Наименование:";
            // 
            // tbNameLinkedDevice
            // 
            this.tbNameLinkedDevice.Location = new System.Drawing.Point(100, 105);
            this.tbNameLinkedDevice.Name = "tbNameLinkedDevice";
            this.tbNameLinkedDevice.Size = new System.Drawing.Size(177, 20);
            this.tbNameLinkedDevice.TabIndex = 6;
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(9, 169);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(469, 71);
            this.tbMessage.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 140);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(393, 26);
            this.label11.TabIndex = 20;
            this.label11.Text = "Сообщение для отправки пользователю при активации данного устройства\r\n(необязател" +
                "ьное поле):";
            // 
            // chbLinkWith
            // 
            this.chbLinkWith.AutoSize = true;
            this.chbLinkWith.Location = new System.Drawing.Point(11, 82);
            this.chbLinkWith.Name = "chbLinkWith";
            this.chbLinkWith.Size = new System.Drawing.Size(200, 17);
            this.chbLinkWith.TabIndex = 5;
            this.chbLinkWith.Text = "Привязано к другому устройству?";
            this.chbLinkWith.UseVisualStyleBackColor = true;
            this.chbLinkWith.Click += new System.EventHandler(this.chbLinkWith_CheckedChanged);
            // 
            // cbDeviceType
            // 
            this.cbDeviceType.FormattingEnabled = true;
            this.cbDeviceType.Location = new System.Drawing.Point(403, 22);
            this.cbDeviceType.Name = "cbDeviceType";
            this.cbDeviceType.Size = new System.Drawing.Size(75, 21);
            this.cbDeviceType.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(308, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Тип устройства:";
            // 
            // tbDeviceName
            // 
            this.tbDeviceName.Location = new System.Drawing.Point(101, 54);
            this.tbDeviceName.Name = "tbDeviceName";
            this.tbDeviceName.Size = new System.Drawing.Size(176, 20);
            this.tbDeviceName.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Наименование:";
            // 
            // cbDeviceNumber
            // 
            this.cbDeviceNumber.FormattingEnabled = true;
            this.cbDeviceNumber.Location = new System.Drawing.Point(243, 22);
            this.cbDeviceNumber.Name = "cbDeviceNumber";
            this.cbDeviceNumber.Size = new System.Drawing.Size(43, 21);
            this.cbDeviceNumber.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(133, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Номер устройства:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Номер порта:";
            // 
            // cbPortNumber
            // 
            this.cbPortNumber.DropDownWidth = 39;
            this.cbPortNumber.ItemHeight = 13;
            this.cbPortNumber.Location = new System.Drawing.Point(88, 22);
            this.cbPortNumber.MaxDropDownItems = 13;
            this.cbPortNumber.Name = "cbPortNumber";
            this.cbPortNumber.Size = new System.Drawing.Size(39, 21);
            this.cbPortNumber.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gpDelUser);
            this.panel2.Controls.Add(this.gbDelDevice);
            this.panel2.Location = new System.Drawing.Point(12, 200);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(516, 153);
            this.panel2.TabIndex = 13;
            this.panel2.TabStop = true;
            // 
            // gpDelUser
            // 
            this.gpDelUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.gpDelUser.Controls.Add(this.tbLoginToDel);
            this.gpDelUser.Controls.Add(this.label5);
            this.gpDelUser.Controls.Add(this.butDelUser);
            this.gpDelUser.Location = new System.Drawing.Point(17, 13);
            this.gpDelUser.Name = "gpDelUser";
            this.gpDelUser.Size = new System.Drawing.Size(200, 133);
            this.gpDelUser.TabIndex = 1;
            this.gpDelUser.TabStop = false;
            this.gpDelUser.Text = "Удалить пользователя";
            // 
            // tbLoginToDel
            // 
            this.tbLoginToDel.Location = new System.Drawing.Point(65, 32);
            this.tbLoginToDel.Name = "tbLoginToDel";
            this.tbLoginToDel.Size = new System.Drawing.Size(129, 20);
            this.tbLoginToDel.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Логин:";
            // 
            // butDelUser
            // 
            this.butDelUser.BackColor = System.Drawing.Color.Transparent;
            this.butDelUser.BackgroundImage = global::AsyncClient.Properties.Resources.Very_Basic_Ok_icon;
            this.butDelUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butDelUser.Location = new System.Drawing.Point(119, 64);
            this.butDelUser.Name = "butDelUser";
            this.butDelUser.Size = new System.Drawing.Size(75, 63);
            this.butDelUser.TabIndex = 2;
            this.butDelUser.UseVisualStyleBackColor = false;
            this.butDelUser.Click += new System.EventHandler(this.butDelUser_Click);
            // 
            // gbDelDevice
            // 
            this.gbDelDevice.Controls.Add(this.tbNameToDel);
            this.gbDelDevice.Controls.Add(this.label6);
            this.gbDelDevice.Controls.Add(this.butDelDevice);
            this.gbDelDevice.Location = new System.Drawing.Point(238, 13);
            this.gbDelDevice.Name = "gbDelDevice";
            this.gbDelDevice.Size = new System.Drawing.Size(263, 133);
            this.gbDelDevice.TabIndex = 2;
            this.gbDelDevice.TabStop = false;
            this.gbDelDevice.Text = "Удалить устройство";
            // 
            // tbNameToDel
            // 
            this.tbNameToDel.Location = new System.Drawing.Point(117, 29);
            this.tbNameToDel.Name = "tbNameToDel";
            this.tbNameToDel.Size = new System.Drawing.Size(129, 20);
            this.tbNameToDel.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Наименование:";
            // 
            // butDelDevice
            // 
            this.butDelDevice.BackColor = System.Drawing.Color.Transparent;
            this.butDelDevice.BackgroundImage = global::AsyncClient.Properties.Resources.Very_Basic_Ok_icon;
            this.butDelDevice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butDelDevice.Location = new System.Drawing.Point(182, 64);
            this.butDelDevice.Name = "butDelDevice";
            this.butDelDevice.Size = new System.Drawing.Size(75, 63);
            this.butDelDevice.TabIndex = 4;
            this.butDelDevice.UseVisualStyleBackColor = false;
            this.butDelDevice.Click += new System.EventHandler(this.butDelDevice_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbAddUser);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 182);
            this.panel1.TabIndex = 12;
            this.panel1.TabStop = true;
            // 
            // gbAddUser
            // 
            this.gbAddUser.Controls.Add(this.cbRole);
            this.gbAddUser.Controls.Add(this.buеCancel);
            this.gbAddUser.Controls.Add(this.label4);
            this.gbAddUser.Controls.Add(this.tbPassConfirm);
            this.gbAddUser.Controls.Add(this.label3);
            this.gbAddUser.Controls.Add(this.butAdd);
            this.gbAddUser.Controls.Add(this.tbPassword);
            this.gbAddUser.Controls.Add(this.label2);
            this.gbAddUser.Controls.Add(this.label1);
            this.gbAddUser.Controls.Add(this.tbLogin);
            this.gbAddUser.Location = new System.Drawing.Point(17, 18);
            this.gbAddUser.Name = "gbAddUser";
            this.gbAddUser.Size = new System.Drawing.Size(484, 158);
            this.gbAddUser.TabIndex = 0;
            this.gbAddUser.TabStop = false;
            this.gbAddUser.Text = "Добавить пользователя";
            // 
            // cbRole
            // 
            this.cbRole.FormattingEnabled = true;
            this.cbRole.Location = new System.Drawing.Point(338, 30);
            this.cbRole.Name = "cbRole";
            this.cbRole.Size = new System.Drawing.Size(129, 21);
            this.cbRole.TabIndex = 2;
            // 
            // buеCancel
            // 
            this.buеCancel.BackColor = System.Drawing.Color.Transparent;
            this.buеCancel.BackgroundImage = global::AsyncClient.Properties.Resources.Very_Basic_Cancel_icon;
            this.buеCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buеCancel.Location = new System.Drawing.Point(322, 89);
            this.buеCancel.Name = "buеCancel";
            this.buеCancel.Size = new System.Drawing.Size(75, 63);
            this.buеCancel.TabIndex = 5;
            this.buеCancel.UseVisualStyleBackColor = false;
            this.buеCancel.Click += new System.EventHandler(this.buеCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(202, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Роль:";
            // 
            // tbPassConfirm
            // 
            this.tbPassConfirm.Location = new System.Drawing.Point(338, 55);
            this.tbPassConfirm.Name = "tbPassConfirm";
            this.tbPassConfirm.Size = new System.Drawing.Size(129, 20);
            this.tbPassConfirm.TabIndex = 4;
            this.tbPassConfirm.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Подтверждение пароля:";
            // 
            // butAdd
            // 
            this.butAdd.BackColor = System.Drawing.Color.Transparent;
            this.butAdd.BackgroundImage = global::AsyncClient.Properties.Resources.Very_Basic_Ok_icon;
            this.butAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butAdd.Location = new System.Drawing.Point(403, 89);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(75, 63);
            this.butAdd.TabIndex = 6;
            this.butAdd.UseVisualStyleBackColor = false;
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(66, 55);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(129, 20);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Пароль:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Логин:";
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(66, 28);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(129, 20);
            this.tbLogin.TabIndex = 1;
            // 
            // AdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(536, 709);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(552, 747);
            this.MinimumSize = new System.Drawing.Size(552, 726);
            this.Name = "AdminPanel";
            this.Text = "Панель администратора";
            this.panel3.ResumeLayout(false);
            this.gpAddDevice.ResumeLayout(false);
            this.gpAddDevice.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.gpDelUser.ResumeLayout(false);
            this.gpDelUser.PerformLayout();
            this.gbDelDevice.ResumeLayout(false);
            this.gbDelDevice.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbAddUser.ResumeLayout(false);
            this.gbAddUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox gpAddDevice;
        private System.Windows.Forms.Button butAddDevice;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbNameLinkedDevice;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chbLinkWith;
        private System.Windows.Forms.ComboBox cbDeviceType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbDeviceName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbDeviceNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbPortNumber;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gpDelUser;
        private System.Windows.Forms.TextBox tbLoginToDel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button butDelUser;
        private System.Windows.Forms.GroupBox gbDelDevice;
        private System.Windows.Forms.TextBox tbNameToDel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button butDelDevice;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbAddUser;
        private System.Windows.Forms.ComboBox cbRole;
        private System.Windows.Forms.Button buеCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPassConfirm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butAdd;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLogin;
    }
}