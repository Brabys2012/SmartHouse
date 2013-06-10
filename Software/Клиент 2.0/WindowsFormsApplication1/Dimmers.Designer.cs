namespace AsyncClient
{
    partial class Dimmers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dimmers));
            this.treeDimmers = new System.Windows.Forms.TreeView();
            this.trackValue = new System.Windows.Forms.TrackBar();
            this.lCurrentValue = new System.Windows.Forms.Label();
            this.lValue = new System.Windows.Forms.Label();
            this.butRefresh = new System.Windows.Forms.Button();
            this.butSet = new System.Windows.Forms.Button();
            this.tbValue = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackValue)).BeginInit();
            this.SuspendLayout();
            // 
            // treeDimmers
            // 
            this.treeDimmers.BackColor = System.Drawing.Color.White;
            this.treeDimmers.Location = new System.Drawing.Point(12, 12);
            this.treeDimmers.Name = "treeDimmers";
            this.treeDimmers.Size = new System.Drawing.Size(150, 190);
            this.treeDimmers.TabIndex = 0;
            this.treeDimmers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDimmers_AfterSelect);
            // 
            // trackValue
            // 
            this.trackValue.BackColor = System.Drawing.Color.White;
            this.trackValue.LargeChange = 10;
            this.trackValue.Location = new System.Drawing.Point(171, 12);
            this.trackValue.Maximum = 100;
            this.trackValue.Name = "trackValue";
            this.trackValue.Size = new System.Drawing.Size(398, 45);
            this.trackValue.TabIndex = 1;
            this.trackValue.Scroll += new System.EventHandler(this.trackValue_Scroll);
            // 
            // lCurrentValue
            // 
            this.lCurrentValue.AutoSize = true;
            this.lCurrentValue.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lCurrentValue.ForeColor = System.Drawing.Color.White;
            this.lCurrentValue.Location = new System.Drawing.Point(12, 216);
            this.lCurrentValue.Name = "lCurrentValue";
            this.lCurrentValue.Size = new System.Drawing.Size(157, 21);
            this.lCurrentValue.TabIndex = 2;
            this.lCurrentValue.Text = "Текущее значение:";
            // 
            // lValue
            // 
            this.lValue.AutoSize = true;
            this.lValue.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lValue.ForeColor = System.Drawing.Color.White;
            this.lValue.Location = new System.Drawing.Point(175, 216);
            this.lValue.Name = "lValue";
            this.lValue.Size = new System.Drawing.Size(81, 21);
            this.lValue.TabIndex = 3;
            this.lValue.Text = "значение";
            // 
            // butRefresh
            // 
            this.butRefresh.BackColor = System.Drawing.Color.Transparent;
            this.butRefresh.BackgroundImage = global::AsyncClient.Properties.Resources.Very_Basic_Sinchronize_icon;
            this.butRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butRefresh.Location = new System.Drawing.Point(168, 137);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(88, 65);
            this.butRefresh.TabIndex = 4;
            this.butRefresh.UseVisualStyleBackColor = false;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // butSet
            // 
            this.butSet.BackColor = System.Drawing.Color.Transparent;
            this.butSet.BackgroundImage = global::AsyncClient.Properties.Resources.Very_Basic_Ok_icon;
            this.butSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butSet.Location = new System.Drawing.Point(168, 66);
            this.butSet.Name = "butSet";
            this.butSet.Size = new System.Drawing.Size(88, 65);
            this.butSet.TabIndex = 5;
            this.butSet.UseVisualStyleBackColor = false;
            this.butSet.Click += new System.EventHandler(this.butSet_Click);
            // 
            // tbValue
            // 
            this.tbValue.BackColor = System.Drawing.Color.White;
            this.tbValue.Font = new System.Drawing.Font("Impact", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbValue.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.tbValue.Location = new System.Drawing.Point(291, 66);
            this.tbValue.Multiline = true;
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(241, 65);
            this.tbValue.TabIndex = 6;
            this.tbValue.Text = "0";
            this.tbValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Dimmers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(581, 242);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.butSet);
            this.Controls.Add(this.butRefresh);
            this.Controls.Add(this.lValue);
            this.Controls.Add(this.lCurrentValue);
            this.Controls.Add(this.trackValue);
            this.Controls.Add(this.treeDimmers);
            this.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(597, 280);
            this.MinimumSize = new System.Drawing.Size(597, 280);
            this.Name = "Dimmers";
            this.Text = "Dimmers";
            ((System.ComponentModel.ISupportInitialize)(this.trackValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeDimmers;
        private System.Windows.Forms.TrackBar trackValue;
        private System.Windows.Forms.Label lCurrentValue;
        private System.Windows.Forms.Label lValue;
        private System.Windows.Forms.Button butRefresh;
        private System.Windows.Forms.Button butSet;
        private System.Windows.Forms.TextBox tbValue;
    }
}