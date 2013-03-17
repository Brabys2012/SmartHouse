namespace AsyncClient
{
    partial class DataForReport
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
            this.rdbOneDay = new System.Windows.Forms.RadioButton();
            this.rdbPeriod = new System.Windows.Forms.RadioButton();
            this.grpType = new System.Windows.Forms.GroupBox();
            this.dateBegin = new System.Windows.Forms.DateTimePicker();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lBeg = new System.Windows.Forms.Label();
            this.lEnd = new System.Windows.Forms.Label();
            this.butBuild = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.grpType.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdbOneDay
            // 
            this.rdbOneDay.AutoSize = true;
            this.rdbOneDay.Location = new System.Drawing.Point(19, 42);
            this.rdbOneDay.Name = "rdbOneDay";
            this.rdbOneDay.Size = new System.Drawing.Size(65, 17);
            this.rdbOneDay.TabIndex = 0;
            this.rdbOneDay.TabStop = true;
            this.rdbOneDay.Text = "За день";
            this.rdbOneDay.UseVisualStyleBackColor = true;
            this.rdbOneDay.Click += new System.EventHandler(this.rdbOneDay_Click);
            // 
            // rdbPeriod
            // 
            this.rdbPeriod.AutoSize = true;
            this.rdbPeriod.Location = new System.Drawing.Point(110, 42);
            this.rdbPeriod.Name = "rdbPeriod";
            this.rdbPeriod.Size = new System.Drawing.Size(77, 17);
            this.rdbPeriod.TabIndex = 1;
            this.rdbPeriod.TabStop = true;
            this.rdbPeriod.Text = "За период";
            this.rdbPeriod.UseVisualStyleBackColor = true;
            this.rdbPeriod.Click += new System.EventHandler(this.rdbOneDay_Click);
            // 
            // grpType
            // 
            this.grpType.Controls.Add(this.rdbOneDay);
            this.grpType.Controls.Add(this.rdbPeriod);
            this.grpType.Location = new System.Drawing.Point(12, 12);
            this.grpType.Name = "grpType";
            this.grpType.Size = new System.Drawing.Size(200, 89);
            this.grpType.TabIndex = 2;
            this.grpType.TabStop = false;
            this.grpType.Text = "Вариант построения";
            // 
            // dateBegin
            // 
            this.dateBegin.Location = new System.Drawing.Point(55, 19);
            this.dateBegin.Name = "dateBegin";
            this.dateBegin.Size = new System.Drawing.Size(200, 20);
            this.dateBegin.TabIndex = 3;
            // 
            // dateEnd
            // 
            this.dateEnd.Location = new System.Drawing.Point(56, 54);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(200, 20);
            this.dateEnd.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lEnd);
            this.groupBox2.Controls.Add(this.lBeg);
            this.groupBox2.Controls.Add(this.dateBegin);
            this.groupBox2.Controls.Add(this.dateEnd);
            this.groupBox2.Location = new System.Drawing.Point(228, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(264, 89);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Период";
            // 
            // lBeg
            // 
            this.lBeg.AutoSize = true;
            this.lBeg.Location = new System.Drawing.Point(7, 25);
            this.lBeg.Name = "lBeg";
            this.lBeg.Size = new System.Drawing.Size(33, 13);
            this.lBeg.TabIndex = 5;
            this.lBeg.Text = "value";
            // 
            // lEnd
            // 
            this.lEnd.AutoSize = true;
            this.lEnd.Location = new System.Drawing.Point(7, 60);
            this.lEnd.Name = "lEnd";
            this.lEnd.Size = new System.Drawing.Size(33, 13);
            this.lEnd.TabIndex = 6;
            this.lEnd.Text = "value";
            // 
            // butBuild
            // 
            this.butBuild.Location = new System.Drawing.Point(122, 123);
            this.butBuild.Name = "butBuild";
            this.butBuild.Size = new System.Drawing.Size(75, 23);
            this.butBuild.TabIndex = 6;
            this.butBuild.Text = "Построить";
            this.butBuild.UseVisualStyleBackColor = true;
            this.butBuild.Click += new System.EventHandler(this.butBuild_Click);
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(303, 123);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 7;
            this.butCancel.Text = "Отмена";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // DataForReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 158);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butBuild);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpType);
            this.Name = "DataForReport";
            this.Text = "Параметры отчёта";
            this.grpType.ResumeLayout(false);
            this.grpType.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbOneDay;
        private System.Windows.Forms.RadioButton rdbPeriod;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.DateTimePicker dateBegin;
        private System.Windows.Forms.DateTimePicker dateEnd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lEnd;
        private System.Windows.Forms.Label lBeg;
        private System.Windows.Forms.Button butBuild;
        private System.Windows.Forms.Button butCancel;
    }
}