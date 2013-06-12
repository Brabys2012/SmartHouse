namespace AsyncClient
{
    partial class Devices
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Devices));
            this.treeDevices = new System.Windows.Forms.TreeView();
            this.lName_ = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.butAction = new System.Windows.Forms.Button();
            this.butRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeDevices
            // 
            this.treeDevices.BackColor = System.Drawing.Color.White;
            this.treeDevices.Location = new System.Drawing.Point(12, 12);
            this.treeDevices.Name = "treeDevices";
            this.treeDevices.Size = new System.Drawing.Size(121, 180);
            this.treeDevices.TabIndex = 0;
            this.treeDevices.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDevices_AfterSelect);
            // 
            // lName_
            // 
            this.lName_.AutoSize = true;
            this.lName_.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lName_.ForeColor = System.Drawing.Color.White;
            this.lName_.Location = new System.Drawing.Point(155, 12);
            this.lName_.Name = "lName_";
            this.lName_.Size = new System.Drawing.Size(130, 21);
            this.lName_.TabIndex = 1;
            this.lName_.Text = "Наименование:";
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lName.ForeColor = System.Drawing.Color.White;
            this.lName.Location = new System.Drawing.Point(291, 12);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(81, 21);
            this.lName.TabIndex = 2;
            this.lName.Text = "значение";
            // 
            // butAction
            // 
            this.butAction.BackgroundImage = global::AsyncClient.Properties.Resources.ВЫКЛ;
            this.butAction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butAction.Location = new System.Drawing.Point(159, 55);
            this.butAction.Name = "butAction";
            this.butAction.Size = new System.Drawing.Size(85, 68);
            this.butAction.TabIndex = 3;
            this.butAction.UseVisualStyleBackColor = true;
            this.butAction.Click += new System.EventHandler(this.butAction_Click);
            // 
            // butRefresh
            // 
            this.butRefresh.BackColor = System.Drawing.Color.Transparent;
            this.butRefresh.BackgroundImage = global::AsyncClient.Properties.Resources.Very_Basic_Sinchronize_icon;
            this.butRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butRefresh.Location = new System.Drawing.Point(159, 129);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(85, 68);
            this.butRefresh.TabIndex = 4;
            this.butRefresh.UseVisualStyleBackColor = false;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // Devices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(623, 202);
            this.Controls.Add(this.butRefresh);
            this.Controls.Add(this.butAction);
            this.Controls.Add(this.lName);
            this.Controls.Add(this.lName_);
            this.Controls.Add(this.treeDevices);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(639, 240);
            this.MinimumSize = new System.Drawing.Size(639, 240);
            this.Name = "Devices";
            this.Text = "Devices";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Devices_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeDevices;
        private System.Windows.Forms.Label lName_;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.Button butAction;
        private System.Windows.Forms.Button butRefresh;
    }
}