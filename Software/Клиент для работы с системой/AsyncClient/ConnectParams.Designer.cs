namespace AsyncClient
{
    partial class ConnectParams
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
            this.tbIP = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.lLogin = new System.Windows.Forms.Label();
            this.lPassword = new System.Windows.Forms.Label();
            this.butOk = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.chbUseEncrypting = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(75, 25);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(181, 20);
            this.tbIP.TabIndex = 0;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(75, 59);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(181, 20);
            this.tbPort.TabIndex = 1;
            // 
            // lLogin
            // 
            this.lLogin.AutoSize = true;
            this.lLogin.Location = new System.Drawing.Point(13, 28);
            this.lLogin.Name = "lLogin";
            this.lLogin.Size = new System.Drawing.Size(59, 13);
            this.lLogin.TabIndex = 2;
            this.lLogin.Text = "IP - адрес:";
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(13, 62);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(35, 13);
            this.lPassword.TabIndex = 3;
            this.lPassword.Text = "Порт:";
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(12, 116);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 4;
            this.butOk.Text = "Сохранить";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(181, 116);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 5;
            this.butCancel.Text = "Отменить";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // chbUseEncrypting
            // 
            this.chbUseEncrypting.AutoSize = true;
            this.chbUseEncrypting.Location = new System.Drawing.Point(18, 92);
            this.chbUseEncrypting.Name = "chbUseEncrypting";
            this.chbUseEncrypting.Size = new System.Drawing.Size(166, 17);
            this.chbUseEncrypting.TabIndex = 6;
            this.chbUseEncrypting.Text = "Использовать шифрование";
            this.chbUseEncrypting.UseVisualStyleBackColor = true;
            // 
            // ConnectParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 147);
            this.Controls.Add(this.chbUseEncrypting);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.lLogin);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbIP);
            this.Name = "ConnectParams";
            this.Text = "Параметры подключения";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lLogin;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butCancel;
        public System.Windows.Forms.TextBox tbIP;
        public System.Windows.Forms.TextBox tbPort;
        public System.Windows.Forms.CheckBox chbUseEncrypting;
    }
}