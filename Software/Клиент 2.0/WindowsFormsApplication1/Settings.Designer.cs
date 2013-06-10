namespace AsyncClient
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.chbUseKeepAlive = new System.Windows.Forms.CheckBox();
            this.chbUseEncrypt = new System.Windows.Forms.CheckBox();
            this.tbIP = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.butChKey = new System.Windows.Forms.Button();
            this.butChPassword = new System.Windows.Forms.Button();
            this.butSave = new System.Windows.Forms.Button();
            this.chbCTRL_ENTER = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chbUseKeepAlive
            // 
            this.chbUseKeepAlive.AutoSize = true;
            this.chbUseKeepAlive.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chbUseKeepAlive.ForeColor = System.Drawing.Color.White;
            this.chbUseKeepAlive.Location = new System.Drawing.Point(12, 43);
            this.chbUseKeepAlive.Name = "chbUseKeepAlive";
            this.chbUseKeepAlive.Size = new System.Drawing.Size(223, 25);
            this.chbUseKeepAlive.TabIndex = 0;
            this.chbUseKeepAlive.Text = "Использовать KeepAlive";
            this.chbUseKeepAlive.UseVisualStyleBackColor = true;
            // 
            // chbUseEncrypt
            // 
            this.chbUseEncrypt.AutoSize = true;
            this.chbUseEncrypt.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chbUseEncrypt.ForeColor = System.Drawing.Color.White;
            this.chbUseEncrypt.Location = new System.Drawing.Point(12, 12);
            this.chbUseEncrypt.Name = "chbUseEncrypt";
            this.chbUseEncrypt.Size = new System.Drawing.Size(247, 25);
            this.chbUseEncrypt.TabIndex = 1;
            this.chbUseEncrypt.Text = "Использовать шифрование";
            this.chbUseEncrypt.UseVisualStyleBackColor = true;
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(12, 120);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(148, 20);
            this.tbIP.TabIndex = 2;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(12, 146);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(148, 20);
            this.tbPort.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(166, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP сервера";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(166, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "порт сервера";
            // 
            // butChKey
            // 
            this.butChKey.BackColor = System.Drawing.Color.CornflowerBlue;
            this.butChKey.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butChKey.ForeColor = System.Drawing.Color.White;
            this.butChKey.Location = new System.Drawing.Point(17, 186);
            this.butChKey.Name = "butChKey";
            this.butChKey.Size = new System.Drawing.Size(169, 53);
            this.butChKey.TabIndex = 6;
            this.butChKey.Text = "Изменить ключ\r\nшифрования";
            this.butChKey.UseVisualStyleBackColor = false;
            this.butChKey.Click += new System.EventHandler(this.butChKey_Click);
            // 
            // butChPassword
            // 
            this.butChPassword.BackColor = System.Drawing.Color.CornflowerBlue;
            this.butChPassword.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butChPassword.ForeColor = System.Drawing.Color.White;
            this.butChPassword.Location = new System.Drawing.Point(196, 186);
            this.butChPassword.Name = "butChPassword";
            this.butChPassword.Size = new System.Drawing.Size(169, 53);
            this.butChPassword.TabIndex = 7;
            this.butChPassword.Text = "Изменить пароль";
            this.butChPassword.UseVisualStyleBackColor = false;
            this.butChPassword.Click += new System.EventHandler(this.butChPassword_Click);
            // 
            // butSave
            // 
            this.butSave.BackgroundImage = global::AsyncClient.Properties.Resources.Very_Basic_Ok_icon;
            this.butSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butSave.Location = new System.Drawing.Point(290, 104);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(75, 62);
            this.butSave.TabIndex = 8;
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // chbCTRL_ENTER
            // 
            this.chbCTRL_ENTER.AutoSize = true;
            this.chbCTRL_ENTER.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chbCTRL_ENTER.ForeColor = System.Drawing.Color.White;
            this.chbCTRL_ENTER.Location = new System.Drawing.Point(12, 74);
            this.chbCTRL_ENTER.Name = "chbCTRL_ENTER";
            this.chbCTRL_ENTER.Size = new System.Drawing.Size(377, 25);
            this.chbCTRL_ENTER.TabIndex = 9;
            this.chbCTRL_ENTER.Text = "Отправлять сообщения по CTRL + ENTER";
            this.chbCTRL_ENTER.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(388, 260);
            this.Controls.Add(this.chbCTRL_ENTER);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.butChPassword);
            this.Controls.Add(this.butChKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbIP);
            this.Controls.Add(this.chbUseEncrypt);
            this.Controls.Add(this.chbUseKeepAlive);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(393, 254);
            this.Name = "Settings";
            this.Text = "Настройки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbUseKeepAlive;
        private System.Windows.Forms.CheckBox chbUseEncrypt;
        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butChKey;
        private System.Windows.Forms.Button butChPassword;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.CheckBox chbCTRL_ENTER;
    }
}