namespace ControlsNOBD
{
    partial class db_settings
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
            this.gDBSet = new System.Windows.Forms.GroupBox();
            this.t_pass = new System.Windows.Forms.TextBox();
            this.t_schema = new System.Windows.Forms.TextBox();
            this.t_user = new System.Windows.Forms.TextBox();
            this.t_server = new System.Windows.Forms.TextBox();
            this.lPass = new System.Windows.Forms.Label();
            this.lschema = new System.Windows.Forms.Label();
            this.lUser = new System.Windows.Forms.Label();
            this.lServer = new System.Windows.Forms.Label();
            this.gbSecSet = new System.Windows.Forms.GroupBox();
            this.cb_win_auth = new System.Windows.Forms.RadioButton();
            this.cb_psec = new System.Windows.Forms.RadioButton();
            this.b_apply = new System.Windows.Forms.Button();
            this.b_abolition = new System.Windows.Forms.Button();
            this.gDBSet.SuspendLayout();
            this.gbSecSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // gDBSet
            // 
            this.gDBSet.Controls.Add(this.t_pass);
            this.gDBSet.Controls.Add(this.t_schema);
            this.gDBSet.Controls.Add(this.t_user);
            this.gDBSet.Controls.Add(this.t_server);
            this.gDBSet.Controls.Add(this.lPass);
            this.gDBSet.Controls.Add(this.lschema);
            this.gDBSet.Controls.Add(this.lUser);
            this.gDBSet.Controls.Add(this.lServer);
            this.gDBSet.Location = new System.Drawing.Point(13, 13);
            this.gDBSet.Name = "gDBSet";
            this.gDBSet.Size = new System.Drawing.Size(590, 109);
            this.gDBSet.TabIndex = 0;
            this.gDBSet.TabStop = false;
            this.gDBSet.Text = "Параметры подключения (* - обязательные поля):";
            // 
            // t_pass
            // 
            this.t_pass.Location = new System.Drawing.Point(397, 65);
            this.t_pass.Name = "t_pass";
            this.t_pass.Size = new System.Drawing.Size(158, 20);
            this.t_pass.TabIndex = 7;
            // 
            // t_schema
            // 
            this.t_schema.Location = new System.Drawing.Point(397, 31);
            this.t_schema.Name = "t_schema";
            this.t_schema.Size = new System.Drawing.Size(158, 20);
            this.t_schema.TabIndex = 6;
            // 
            // t_user
            // 
            this.t_user.Location = new System.Drawing.Point(103, 65);
            this.t_user.Name = "t_user";
            this.t_user.Size = new System.Drawing.Size(158, 20);
            this.t_user.TabIndex = 5;
            // 
            // t_server
            // 
            this.t_server.Location = new System.Drawing.Point(103, 31);
            this.t_server.Name = "t_server";
            this.t_server.Size = new System.Drawing.Size(158, 20);
            this.t_server.TabIndex = 4;
            // 
            // lPass
            // 
            this.lPass.AutoSize = true;
            this.lPass.Location = new System.Drawing.Point(336, 68);
            this.lPass.Name = "lPass";
            this.lPass.Size = new System.Drawing.Size(55, 13);
            this.lPass.TabIndex = 3;
            this.lPass.Text = "* Пароль:";
            // 
            // lschema
            // 
            this.lschema.AutoSize = true;
            this.lschema.Location = new System.Drawing.Point(283, 34);
            this.lschema.Name = "lschema";
            this.lschema.Size = new System.Drawing.Size(108, 13);
            this.lschema.TabIndex = 2;
            this.lschema.Text = "* Имя базы данных:";
            // 
            // lUser
            // 
            this.lUser.AutoSize = true;
            this.lUser.Location = new System.Drawing.Point(7, 68);
            this.lUser.Name = "lUser";
            this.lUser.Size = new System.Drawing.Size(90, 13);
            this.lUser.TabIndex = 1;
            this.lUser.Text = "* Пользователи:";
            // 
            // lServer
            // 
            this.lServer.AutoSize = true;
            this.lServer.Location = new System.Drawing.Point(46, 34);
            this.lServer.Name = "lServer";
            this.lServer.Size = new System.Drawing.Size(51, 13);
            this.lServer.TabIndex = 0;
            this.lServer.Text = "*Сервер:";
            // 
            // gbSecSet
            // 
            this.gbSecSet.Controls.Add(this.cb_win_auth);
            this.gbSecSet.Controls.Add(this.cb_psec);
            this.gbSecSet.Location = new System.Drawing.Point(17, 134);
            this.gbSecSet.Name = "gbSecSet";
            this.gbSecSet.Size = new System.Drawing.Size(586, 72);
            this.gbSecSet.TabIndex = 1;
            this.gbSecSet.TabStop = false;
            this.gbSecSet.Text = "Безопасность:";
            // 
            // cb_win_auth
            // 
            this.cb_win_auth.AutoSize = true;
            this.cb_win_auth.Location = new System.Drawing.Point(7, 43);
            this.cb_win_auth.Name = "cb_win_auth";
            this.cb_win_auth.Size = new System.Drawing.Size(190, 17);
            this.cb_win_auth.TabIndex = 1;
            this.cb_win_auth.TabStop = true;
            this.cb_win_auth.Text = "Проверка подлинности Windows";
            this.cb_win_auth.UseVisualStyleBackColor = true;
            this.cb_win_auth.CheckedChanged += new System.EventHandler(this.cb_win_auth_CheckedChanged);
            // 
            // cb_psec
            // 
            this.cb_psec.AutoSize = true;
            this.cb_psec.Location = new System.Drawing.Point(7, 20);
            this.cb_psec.Name = "cb_psec";
            this.cb_psec.Size = new System.Drawing.Size(345, 17);
            this.cb_psec.TabIndex = 0;
            this.cb_psec.TabStop = true;
            this.cb_psec.Text = "Persist Security (Использовать данный параметр подключения);";
            this.cb_psec.UseVisualStyleBackColor = true;
            this.cb_psec.CheckedChanged += new System.EventHandler(this.cb_psec_CheckedChanged);
            // 
            // b_apply
            // 
            this.b_apply.Location = new System.Drawing.Point(24, 227);
            this.b_apply.Name = "b_apply";
            this.b_apply.Size = new System.Drawing.Size(96, 23);
            this.b_apply.TabIndex = 2;
            this.b_apply.Text = "Применить";
            this.b_apply.UseVisualStyleBackColor = true;
            this.b_apply.Click += new System.EventHandler(this.b_apply_Click);
            // 
            // b_abolition
            // 
            this.b_abolition.Location = new System.Drawing.Point(472, 227);
            this.b_abolition.Name = "b_abolition";
            this.b_abolition.Size = new System.Drawing.Size(96, 23);
            this.b_abolition.TabIndex = 3;
            this.b_abolition.Text = "Отменить";
            this.b_abolition.UseVisualStyleBackColor = true;
            this.b_abolition.Click += new System.EventHandler(this.b_abolition_Click);
            // 
            // db_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 262);
            this.Controls.Add(this.b_abolition);
            this.Controls.Add(this.b_apply);
            this.Controls.Add(this.gbSecSet);
            this.Controls.Add(this.gDBSet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "db_settings";
            this.Text = "Настройки соединения";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.db_settings_FormClosed);
            this.Shown += new System.EventHandler(this.db_settings_Shown);
            this.gDBSet.ResumeLayout(false);
            this.gDBSet.PerformLayout();
            this.gbSecSet.ResumeLayout(false);
            this.gbSecSet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gDBSet;
        private System.Windows.Forms.TextBox t_pass;
        private System.Windows.Forms.TextBox t_schema;
        private System.Windows.Forms.TextBox t_user;
        private System.Windows.Forms.TextBox t_server;
        private System.Windows.Forms.Label lPass;
        private System.Windows.Forms.Label lschema;
        private System.Windows.Forms.Label lUser;
        private System.Windows.Forms.Label lServer;
        private System.Windows.Forms.GroupBox gbSecSet;
        private System.Windows.Forms.RadioButton cb_win_auth;
        private System.Windows.Forms.RadioButton cb_psec;
        private System.Windows.Forms.Button b_apply;
        private System.Windows.Forms.Button b_abolition;
    }
}