namespace ControlsNOBD
{
    partial class AddDev
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbCurrentValue = new System.Windows.Forms.TextBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.lType = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.cbAdress = new System.Windows.Forms.ComboBox();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.lName = new System.Windows.Forms.Label();
            this.lAdress = new System.Windows.Forms.Label();
            this.lPort = new System.Windows.Forms.Label();
            this.b_Ok = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.bDel = new System.Windows.Forms.Button();
            this.cbState = new System.Windows.Forms.CheckBox();
            this.lCurVal = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbState);
            this.groupBox1.Controls.Add(this.tbCurrentValue);
            this.groupBox1.Controls.Add(this.lCurVal);
            this.groupBox1.Controls.Add(this.cbType);
            this.groupBox1.Controls.Add(this.lType);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.cbAdress);
            this.groupBox1.Controls.Add(this.cbPort);
            this.groupBox1.Controls.Add(this.lName);
            this.groupBox1.Controls.Add(this.lAdress);
            this.groupBox1.Controls.Add(this.lPort);
            this.groupBox1.Location = new System.Drawing.Point(18, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(569, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация об устройстве (датчике) (* - обязательные поля)";
            // 
            // tbCurrentValue
            // 
            this.tbCurrentValue.Location = new System.Drawing.Point(236, 85);
            this.tbCurrentValue.Name = "tbCurrentValue";
            this.tbCurrentValue.Size = new System.Drawing.Size(191, 20);
            this.tbCurrentValue.TabIndex = 9;
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(310, 52);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(191, 21);
            this.cbType.TabIndex = 7;
            this.cbType.SelectionChangeCommitted += new System.EventHandler(this.cbType_SelectionChangeCommitted);
            // 
            // lType
            // 
            this.lType.AutoSize = true;
            this.lType.Location = new System.Drawing.Point(241, 55);
            this.lType.Name = "lType";
            this.lType.Size = new System.Drawing.Size(63, 13);
            this.lType.TabIndex = 6;
            this.lType.Text = "Тип уст-ва:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(310, 26);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(191, 20);
            this.tbName.TabIndex = 5;
            // 
            // cbAdress
            // 
            this.cbAdress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAdress.FormattingEnabled = true;
            this.cbAdress.Location = new System.Drawing.Point(117, 52);
            this.cbAdress.Name = "cbAdress";
            this.cbAdress.Size = new System.Drawing.Size(113, 21);
            this.cbAdress.TabIndex = 4;
            // 
            // cbPort
            // 
            this.cbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Location = new System.Drawing.Point(117, 26);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(113, 21);
            this.cbPort.TabIndex = 3;
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(272, 29);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(32, 13);
            this.lName.TabIndex = 2;
            this.lName.Text = "Имя:";
            // 
            // lAdress
            // 
            this.lAdress.AutoSize = true;
            this.lAdress.Location = new System.Drawing.Point(34, 29);
            this.lAdress.Name = "lAdress";
            this.lAdress.Size = new System.Drawing.Size(77, 13);
            this.lAdress.TabIndex = 1;
            this.lAdress.Text = "*Адрес порта:";
            // 
            // lPort
            // 
            this.lPort.AutoSize = true;
            this.lPort.Location = new System.Drawing.Point(6, 55);
            this.lPort.Name = "lPort";
            this.lPort.Size = new System.Drawing.Size(105, 13);
            this.lPort.TabIndex = 0;
            this.lPort.Text = "*Адрес устройства:";
            // 
            // b_Ok
            // 
            this.b_Ok.Location = new System.Drawing.Point(18, 161);
            this.b_Ok.Name = "b_Ok";
            this.b_Ok.Size = new System.Drawing.Size(124, 23);
            this.b_Ok.TabIndex = 1;
            this.b_Ok.Text = "ОК";
            this.b_Ok.UseVisualStyleBackColor = true;
            this.b_Ok.Click += new System.EventHandler(this.b_Ok_Click);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(463, 161);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(124, 23);
            this.bClose.TabIndex = 2;
            this.bClose.Text = "Отмена";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // bDel
            // 
            this.bDel.Location = new System.Drawing.Point(241, 161);
            this.bDel.Name = "bDel";
            this.bDel.Size = new System.Drawing.Size(117, 23);
            this.bDel.TabIndex = 3;
            this.bDel.Text = "Отключить";
            this.bDel.UseVisualStyleBackColor = true;
            this.bDel.Click += new System.EventHandler(this.bDel_Click);
            // 
            // cbState
            // 
            this.cbState.AutoSize = true;
            this.cbState.Location = new System.Drawing.Point(445, 87);
            this.cbState.Name = "cbState";
            this.cbState.Size = new System.Drawing.Size(92, 17);
            this.cbState.TabIndex = 10;
            this.cbState.Text = "Активирован";
            this.cbState.UseVisualStyleBackColor = true;
            this.cbState.CheckedChanged += new System.EventHandler(this.cbState_CheckedChanged);
            // 
            // lCurVal
            // 
            this.lCurVal.AutoSize = true;
            this.lCurVal.Location = new System.Drawing.Point(125, 88);
            this.lCurVal.Name = "lCurVal";
            this.lCurVal.Size = new System.Drawing.Size(105, 13);
            this.lCurVal.TabIndex = 8;
            this.lCurVal.Text = "Текущее значение:";
            // 
            // AddDev
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 221);
            this.Controls.Add(this.bDel);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.b_Ok);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AddDev";
            this.Text = "Подключение";
            this.Load += new System.EventHandler(this.AddDev_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label lType;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.ComboBox cbAdress;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.Label lAdress;
        private System.Windows.Forms.Label lPort;
        private System.Windows.Forms.Button b_Ok;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Button bDel;
        private System.Windows.Forms.TextBox tbCurrentValue;
        private System.Windows.Forms.CheckBox cbState;
        private System.Windows.Forms.Label lCurVal;
    }
}