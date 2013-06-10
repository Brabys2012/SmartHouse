namespace AsyncClient
{
    partial class ChangeKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeKey));
            this.tbNewKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCurrentKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbNewKey
            // 
            this.tbNewKey.Location = new System.Drawing.Point(14, 82);
            this.tbNewKey.Name = "tbNewKey";
            this.tbNewKey.Size = new System.Drawing.Size(234, 20);
            this.tbNewKey.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Новый ключ:";
            // 
            // butSave
            // 
            this.butSave.BackgroundImage = global::AsyncClient.Properties.Resources.Very_Basic_Ok_icon;
            this.butSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butSave.Location = new System.Drawing.Point(166, 108);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(82, 65);
            this.butSave.TabIndex = 2;
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Текущий ключ:";
            // 
            // tbCurrentKey
            // 
            this.tbCurrentKey.Location = new System.Drawing.Point(12, 35);
            this.tbCurrentKey.Name = "tbCurrentKey";
            this.tbCurrentKey.Size = new System.Drawing.Size(234, 20);
            this.tbCurrentKey.TabIndex = 3;
            // 
            // ChangeKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(260, 185);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbCurrentKey);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNewKey);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(276, 188);
            this.Name = "ChangeKey";
            this.Text = "Изменить ключ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tbNewKey;
        public System.Windows.Forms.TextBox tbCurrentKey;
    }
}