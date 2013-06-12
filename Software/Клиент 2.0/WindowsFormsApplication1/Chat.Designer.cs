namespace AsyncClient
{
    partial class Chat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chat));
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.tbChat = new System.Windows.Forms.TextBox();
            this.treeUsers = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.butRefresh = new System.Windows.Forms.Button();
            this.butSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbMessage
            // 
            this.tbMessage.BackColor = System.Drawing.Color.White;
            this.tbMessage.ForeColor = System.Drawing.Color.Black;
            this.tbMessage.Location = new System.Drawing.Point(12, 277);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(426, 59);
            this.tbMessage.TabIndex = 0;
            this.tbMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMessage_KeyDown);
            // 
            // tbChat
            // 
            this.tbChat.BackColor = System.Drawing.Color.White;
            this.tbChat.ForeColor = System.Drawing.Color.Black;
            this.tbChat.Location = new System.Drawing.Point(12, 12);
            this.tbChat.Multiline = true;
            this.tbChat.Name = "tbChat";
            this.tbChat.Size = new System.Drawing.Size(270, 242);
            this.tbChat.TabIndex = 1;
            // 
            // treeUsers
            // 
            this.treeUsers.BackColor = System.Drawing.Color.White;
            this.treeUsers.ForeColor = System.Drawing.Color.Black;
            this.treeUsers.Location = new System.Drawing.Point(313, 35);
            this.treeUsers.Name = "treeUsers";
            this.treeUsers.ShowLines = false;
            this.treeUsers.Size = new System.Drawing.Size(121, 219);
            this.treeUsers.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(309, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Сейчас онлайн";
            // 
            // butRefresh
            // 
            this.butRefresh.BackColor = System.Drawing.Color.Transparent;
            this.butRefresh.BackgroundImage = global::AsyncClient.Properties.Resources.Very_Basic_Sinchronize_icon;
            this.butRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butRefresh.Location = new System.Drawing.Point(444, 35);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(79, 59);
            this.butRefresh.TabIndex = 6;
            this.butRefresh.UseVisualStyleBackColor = false;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // butSend
            // 
            this.butSend.BackColor = System.Drawing.Color.Transparent;
            this.butSend.BackgroundImage = global::AsyncClient.Properties.Resources.Very_Basic_Ok_icon;
            this.butSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butSend.Location = new System.Drawing.Point(444, 277);
            this.butSend.Name = "butSend";
            this.butSend.Size = new System.Drawing.Size(75, 59);
            this.butSend.TabIndex = 2;
            this.butSend.UseVisualStyleBackColor = false;
            this.butSend.Click += new System.EventHandler(this.butSend_Click);
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(526, 348);
            this.Controls.Add(this.butRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeUsers);
            this.Controls.Add(this.butSend);
            this.Controls.Add(this.tbChat);
            this.Controls.Add(this.tbMessage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(542, 386);
            this.MinimumSize = new System.Drawing.Size(542, 386);
            this.Name = "Chat";
            this.Text = "Чат";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.TextBox tbChat;
        private System.Windows.Forms.Button butSend;
        private System.Windows.Forms.TreeView treeUsers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butRefresh;
    }
}