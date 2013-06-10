namespace AsyncClient
{
    partial class Sensors
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sensors));
            this.treeSensors = new System.Windows.Forms.TreeView();
            this.lName_ = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.lCurrentValue = new System.Windows.Forms.Label();
            this.lValue = new System.Windows.Forms.Label();
            this.butRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeSensors
            // 
            this.treeSensors.BackColor = System.Drawing.Color.White;
            this.treeSensors.Location = new System.Drawing.Point(28, 27);
            this.treeSensors.Name = "treeSensors";
            this.treeSensors.Size = new System.Drawing.Size(121, 197);
            this.treeSensors.TabIndex = 0;
            this.treeSensors.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeSensors_AfterSelect);
            // 
            // lName_
            // 
            this.lName_.AutoSize = true;
            this.lName_.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lName_.ForeColor = System.Drawing.Color.White;
            this.lName_.Location = new System.Drawing.Point(182, 34);
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
            this.lName.Location = new System.Drawing.Point(318, 34);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(81, 21);
            this.lName.TabIndex = 2;
            this.lName.Text = "значение";
            // 
            // lCurrentValue
            // 
            this.lCurrentValue.AutoSize = true;
            this.lCurrentValue.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lCurrentValue.ForeColor = System.Drawing.Color.White;
            this.lCurrentValue.Location = new System.Drawing.Point(182, 68);
            this.lCurrentValue.Name = "lCurrentValue";
            this.lCurrentValue.Size = new System.Drawing.Size(157, 21);
            this.lCurrentValue.TabIndex = 3;
            this.lCurrentValue.Text = "Текущее значение:";
            // 
            // lValue
            // 
            this.lValue.AutoSize = true;
            this.lValue.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lValue.ForeColor = System.Drawing.Color.White;
            this.lValue.Location = new System.Drawing.Point(345, 68);
            this.lValue.Name = "lValue";
            this.lValue.Size = new System.Drawing.Size(81, 21);
            this.lValue.TabIndex = 4;
            this.lValue.Text = "значение";
            // 
            // butRefresh
            // 
            this.butRefresh.BackColor = System.Drawing.Color.Transparent;
            this.butRefresh.BackgroundImage = global::AsyncClient.Properties.Resources.Very_Basic_Sinchronize_icon;
            this.butRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butRefresh.Location = new System.Drawing.Point(169, 161);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(84, 63);
            this.butRefresh.TabIndex = 5;
            this.butRefresh.UseVisualStyleBackColor = false;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // Sensors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(578, 262);
            this.Controls.Add(this.butRefresh);
            this.Controls.Add(this.lValue);
            this.Controls.Add(this.lCurrentValue);
            this.Controls.Add(this.lName);
            this.Controls.Add(this.lName_);
            this.Controls.Add(this.treeSensors);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(594, 300);
            this.MinimumSize = new System.Drawing.Size(594, 300);
            this.Name = "Sensors";
            this.Text = "Датчики";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeSensors;
        private System.Windows.Forms.Label lName_;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.Label lCurrentValue;
        private System.Windows.Forms.Label lValue;
        private System.Windows.Forms.Button butRefresh;
    }
}