namespace RUS_Project.TerminalConfigurator
{
    partial class frmModemCfg
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModemCfg));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabTerminal = new System.Windows.Forms.TabPage();
            this.grpParameters = new System.Windows.Forms.GroupBox();
            this.txtPortTimeout = new System.Windows.Forms.TextBox();
            this.lblPortReadTimeout = new System.Windows.Forms.Label();
            this.txtPortBuffer = new System.Windows.Forms.TextBox();
            this.lblPortReadBuffer = new System.Windows.Forms.Label();
            this.btnPortOpen = new System.Windows.Forms.Button();
            this.lblPortHandshake = new System.Windows.Forms.Label();
            this.cmbPortHandshake = new System.Windows.Forms.ComboBox();
            this.lblPortParity = new System.Windows.Forms.Label();
            this.cmbPortParity = new System.Windows.Forms.ComboBox();
            this.lblPortData = new System.Windows.Forms.Label();
            this.cmbPortData = new System.Windows.Forms.ComboBox();
            this.lblPortBaud = new System.Windows.Forms.Label();
            this.lblPortName = new System.Windows.Forms.Label();
            this.cmbPortBaud = new System.Windows.Forms.ComboBox();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.grpCommand = new System.Windows.Forms.GroupBox();
            this.chkSendAsByte = new System.Windows.Forms.CheckBox();
            this.btnKey = new System.Windows.Forms.Button();
            this.btnCRLF = new System.Windows.Forms.Button();
            this.btnRTS = new System.Windows.Forms.Button();
            this.btnDTR = new System.Windows.Forms.Button();
            this.btnWW = new System.Windows.Forms.Button();
            this.btnCLR = new System.Windows.Forms.Button();
            this.txtCommand = new System.Windows.Forms.ComboBox();
            this.btnSendCommand = new System.Windows.Forms.Button();
            this.txtCommandBox = new System.Windows.Forms.RichTextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabTerminal.SuspendLayout();
            this.grpParameters.SuspendLayout();
            this.grpCommand.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabTerminal
            // 
            this.tabTerminal.BackColor = System.Drawing.Color.White;
            this.tabTerminal.Controls.Add(this.grpParameters);
            this.tabTerminal.Controls.Add(this.grpCommand);
            this.tabTerminal.Location = new System.Drawing.Point(4, 22);
            this.tabTerminal.Name = "tabTerminal";
            this.tabTerminal.Padding = new System.Windows.Forms.Padding(3);
            this.tabTerminal.Size = new System.Drawing.Size(697, 540);
            this.tabTerminal.TabIndex = 0;
            this.tabTerminal.Text = "COM-порт - Терминал";
            this.toolTip.SetToolTip(this.tabTerminal, "Терминал для работы с COM-портом (Ctrl + T)");
            // 
            // grpParameters
            // 
            this.grpParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grpParameters.Controls.Add(this.txtPortTimeout);
            this.grpParameters.Controls.Add(this.lblPortReadTimeout);
            this.grpParameters.Controls.Add(this.txtPortBuffer);
            this.grpParameters.Controls.Add(this.lblPortReadBuffer);
            this.grpParameters.Controls.Add(this.btnPortOpen);
            this.grpParameters.Controls.Add(this.lblPortHandshake);
            this.grpParameters.Controls.Add(this.cmbPortHandshake);
            this.grpParameters.Controls.Add(this.lblPortParity);
            this.grpParameters.Controls.Add(this.cmbPortParity);
            this.grpParameters.Controls.Add(this.lblPortData);
            this.grpParameters.Controls.Add(this.cmbPortData);
            this.grpParameters.Controls.Add(this.lblPortBaud);
            this.grpParameters.Controls.Add(this.lblPortName);
            this.grpParameters.Controls.Add(this.cmbPortBaud);
            this.grpParameters.Controls.Add(this.cmbPortName);
            this.grpParameters.Location = new System.Drawing.Point(6, 6);
            this.grpParameters.Name = "grpParameters";
            this.grpParameters.Size = new System.Drawing.Size(154, 530);
            this.grpParameters.TabIndex = 9;
            this.grpParameters.TabStop = false;
            this.grpParameters.Text = "Параметры COM-порта";
            // 
            // txtPortTimeout
            // 
            this.txtPortTimeout.Location = new System.Drawing.Point(6, 288);
            this.txtPortTimeout.Name = "txtPortTimeout";
            this.txtPortTimeout.Size = new System.Drawing.Size(139, 20);
            this.txtPortTimeout.TabIndex = 19;
            this.toolTip.SetToolTip(this.txtPortTimeout, "Время таймаута для операций ввода/вывода");
            // 
            // lblPortReadTimeout
            // 
            this.lblPortReadTimeout.AutoSize = true;
            this.lblPortReadTimeout.Location = new System.Drawing.Point(6, 272);
            this.lblPortReadTimeout.Name = "lblPortReadTimeout";
            this.lblPortReadTimeout.Size = new System.Drawing.Size(50, 13);
            this.lblPortReadTimeout.TabIndex = 18;
            this.lblPortReadTimeout.Text = "Таймаут";
            // 
            // txtPortBuffer
            // 
            this.txtPortBuffer.Location = new System.Drawing.Point(6, 249);
            this.txtPortBuffer.Name = "txtPortBuffer";
            this.txtPortBuffer.Size = new System.Drawing.Size(139, 20);
            this.txtPortBuffer.TabIndex = 15;
            this.toolTip.SetToolTip(this.txtPortBuffer, "Размер буффера для операций ввода/вывода");
            // 
            // lblPortReadBuffer
            // 
            this.lblPortReadBuffer.AutoSize = true;
            this.lblPortReadBuffer.Location = new System.Drawing.Point(6, 233);
            this.lblPortReadBuffer.Name = "lblPortReadBuffer";
            this.lblPortReadBuffer.Size = new System.Drawing.Size(126, 13);
            this.lblPortReadBuffer.TabIndex = 14;
            this.lblPortReadBuffer.Text = "Размер буффера (байт)";
            // 
            // btnPortOpen
            // 
            this.btnPortOpen.Location = new System.Drawing.Point(6, 314);
            this.btnPortOpen.Name = "btnPortOpen";
            this.btnPortOpen.Size = new System.Drawing.Size(139, 23);
            this.btnPortOpen.TabIndex = 22;
            this.btnPortOpen.Text = "Открыть";
            this.toolTip.SetToolTip(this.btnPortOpen, "Открыть/Закрыть подключение к выбранному COM-порту (Ctrl + O)");
            this.btnPortOpen.UseVisualStyleBackColor = true;
            this.btnPortOpen.Click += new System.EventHandler(this.btnPortOpen_Click);
            // 
            // lblPortHandshake
            // 
            this.lblPortHandshake.AutoSize = true;
            this.lblPortHandshake.Location = new System.Drawing.Point(6, 193);
            this.lblPortHandshake.Name = "lblPortHandshake";
            this.lblPortHandshake.Size = new System.Drawing.Size(89, 13);
            this.lblPortHandshake.TabIndex = 10;
            this.lblPortHandshake.Text = "Протокол связи";
            // 
            // cmbPortHandshake
            // 
            this.cmbPortHandshake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortHandshake.FormattingEnabled = true;
            this.cmbPortHandshake.Location = new System.Drawing.Point(6, 209);
            this.cmbPortHandshake.Name = "cmbPortHandshake";
            this.cmbPortHandshake.Size = new System.Drawing.Size(139, 21);
            this.cmbPortHandshake.TabIndex = 11;
            this.toolTip.SetToolTip(this.cmbPortHandshake, "Протокол управления:\r\nNone - нет\r\nXOnXOff - программный\r\nRequestToSend - аппаратн" +
                    "ый\r\nRequestToSendXOnXOff - смешанный");
            // 
            // lblPortParity
            // 
            this.lblPortParity.AutoSize = true;
            this.lblPortParity.Location = new System.Drawing.Point(6, 105);
            this.lblPortParity.Name = "lblPortParity";
            this.lblPortParity.Size = new System.Drawing.Size(105, 13);
            this.lblPortParity.TabIndex = 6;
            this.lblPortParity.Text = "Проверка четности";
            // 
            // cmbPortParity
            // 
            this.cmbPortParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortParity.FormattingEnabled = true;
            this.cmbPortParity.Location = new System.Drawing.Point(6, 121);
            this.cmbPortParity.Name = "cmbPortParity";
            this.cmbPortParity.Size = new System.Drawing.Size(139, 21);
            this.cmbPortParity.TabIndex = 7;
            this.toolTip.SetToolTip(this.cmbPortParity, "Вид проверки четности:\r\nNone - без проверки\r\nOdd - нечет\r\nEven - чет\r\nSpace - нол" +
                    "ь\r\nMark - единица");
            // 
            // lblPortData
            // 
            this.lblPortData.AutoSize = true;
            this.lblPortData.Location = new System.Drawing.Point(6, 145);
            this.lblPortData.Name = "lblPortData";
            this.lblPortData.Size = new System.Drawing.Size(65, 13);
            this.lblPortData.TabIndex = 4;
            this.lblPortData.Text = "Бит данных";
            // 
            // cmbPortData
            // 
            this.cmbPortData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortData.FormattingEnabled = true;
            this.cmbPortData.Location = new System.Drawing.Point(6, 161);
            this.cmbPortData.Name = "cmbPortData";
            this.cmbPortData.Size = new System.Drawing.Size(139, 21);
            this.cmbPortData.TabIndex = 5;
            this.toolTip.SetToolTip(this.cmbPortData, "Число бит данных в передаваемом байте");
            // 
            // lblPortBaud
            // 
            this.lblPortBaud.AutoSize = true;
            this.lblPortBaud.Location = new System.Drawing.Point(6, 65);
            this.lblPortBaud.Name = "lblPortBaud";
            this.lblPortBaud.Size = new System.Drawing.Size(55, 13);
            this.lblPortBaud.TabIndex = 2;
            this.lblPortBaud.Text = "Скорость";
            // 
            // lblPortName
            // 
            this.lblPortName.AutoSize = true;
            this.lblPortName.Location = new System.Drawing.Point(6, 25);
            this.lblPortName.Name = "lblPortName";
            this.lblPortName.Size = new System.Drawing.Size(29, 13);
            this.lblPortName.TabIndex = 0;
            this.lblPortName.Text = "Имя";
            // 
            // cmbPortBaud
            // 
            this.cmbPortBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortBaud.FormattingEnabled = true;
            this.cmbPortBaud.Location = new System.Drawing.Point(6, 81);
            this.cmbPortBaud.Name = "cmbPortBaud";
            this.cmbPortBaud.Size = new System.Drawing.Size(139, 21);
            this.cmbPortBaud.TabIndex = 3;
            this.toolTip.SetToolTip(this.cmbPortBaud, "Скорость передачи данных (бод)");
            // 
            // cmbPortName
            // 
            this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Location = new System.Drawing.Point(6, 41);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(139, 21);
            this.cmbPortName.TabIndex = 1;
            this.toolTip.SetToolTip(this.cmbPortName, "Имя COM-порта для подключения");
            this.cmbPortName.Click += new System.EventHandler(this.cmbPortName_Click);
            // 
            // grpCommand
            // 
            this.grpCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCommand.Controls.Add(this.chkSendAsByte);
            this.grpCommand.Controls.Add(this.btnKey);
            this.grpCommand.Controls.Add(this.btnCRLF);
            this.grpCommand.Controls.Add(this.btnRTS);
            this.grpCommand.Controls.Add(this.btnDTR);
            this.grpCommand.Controls.Add(this.btnWW);
            this.grpCommand.Controls.Add(this.btnCLR);
            this.grpCommand.Controls.Add(this.txtCommand);
            this.grpCommand.Controls.Add(this.btnSendCommand);
            this.grpCommand.Controls.Add(this.txtCommandBox);
            this.grpCommand.Location = new System.Drawing.Point(166, 6);
            this.grpCommand.Name = "grpCommand";
            this.grpCommand.Size = new System.Drawing.Size(525, 530);
            this.grpCommand.TabIndex = 10;
            this.grpCommand.TabStop = false;
            // 
            // chkSendAsByte
            // 
            this.chkSendAsByte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSendAsByte.AutoSize = true;
            this.chkSendAsByte.Location = new System.Drawing.Point(506, 506);
            this.chkSendAsByte.Name = "chkSendAsByte";
            this.chkSendAsByte.Size = new System.Drawing.Size(15, 14);
            this.chkSendAsByte.TabIndex = 32;
            this.chkSendAsByte.TabStop = false;
            this.toolTip.SetToolTip(this.chkSendAsByte, "Отправлять сообщение как байты данных");
            this.chkSendAsByte.UseVisualStyleBackColor = true;
            // 
            // btnKey
            // 
            this.btnKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnKey.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnKey.Location = new System.Drawing.Point(268, 11);
            this.btnKey.Name = "btnKey";
            this.btnKey.Size = new System.Drawing.Size(41, 23);
            this.btnKey.TabIndex = 26;
            this.btnKey.TabStop = false;
            this.btnKey.Text = "KEY";
            this.toolTip.SetToolTip(this.btnKey, "Прямой ввод - отправка символов, вводимых с клавиатуры без буферизации в поле вво" +
                    "да комманд (Ctrl + K)");
            this.btnKey.UseVisualStyleBackColor = true;
            this.btnKey.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // btnCRLF
            // 
            this.btnCRLF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCRLF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCRLF.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCRLF.Location = new System.Drawing.Point(205, 11);
            this.btnCRLF.Name = "btnCRLF";
            this.btnCRLF.Size = new System.Drawing.Size(61, 23);
            this.btnCRLF.TabIndex = 25;
            this.btnCRLF.TabStop = false;
            this.btnCRLF.Text = "CR/LF";
            this.toolTip.SetToolTip(this.btnCRLF, "Добавлять символ новой строки при отправке команды (Ctrl + L)");
            this.btnCRLF.UseVisualStyleBackColor = true;
            this.btnCRLF.Click += new System.EventHandler(this.btnCRLF_Click);
            // 
            // btnRTS
            // 
            this.btnRTS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRTS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRTS.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRTS.Location = new System.Drawing.Point(372, 11);
            this.btnRTS.Name = "btnRTS";
            this.btnRTS.Size = new System.Drawing.Size(41, 23);
            this.btnRTS.TabIndex = 15;
            this.btnRTS.TabStop = false;
            this.btnRTS.Text = "RTS";
            this.toolTip.SetToolTip(this.btnRTS, "Изменить состояние RTS-линии COM-порта");
            this.btnRTS.UseVisualStyleBackColor = true;
            this.btnRTS.Click += new System.EventHandler(this.btnRTS_Click);
            // 
            // btnDTR
            // 
            this.btnDTR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDTR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDTR.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDTR.Location = new System.Drawing.Point(329, 11);
            this.btnDTR.Name = "btnDTR";
            this.btnDTR.Size = new System.Drawing.Size(41, 23);
            this.btnDTR.TabIndex = 14;
            this.btnDTR.TabStop = false;
            this.btnDTR.Text = "DTR";
            this.toolTip.SetToolTip(this.btnDTR, "Изменить состояние DTR-линии COM-порта");
            this.btnDTR.UseVisualStyleBackColor = true;
            this.btnDTR.Click += new System.EventHandler(this.btnDTR_Click);
            // 
            // btnWW
            // 
            this.btnWW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWW.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnWW.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnWW.Location = new System.Drawing.Point(434, 11);
            this.btnWW.Name = "btnWW";
            this.btnWW.Size = new System.Drawing.Size(41, 23);
            this.btnWW.TabIndex = 13;
            this.btnWW.TabStop = false;
            this.btnWW.Text = "WW";
            this.toolTip.SetToolTip(this.btnWW, "Переносить строки в окне результатов (Ctrl + W)");
            this.btnWW.UseVisualStyleBackColor = true;
            this.btnWW.Click += new System.EventHandler(this.btnWW_Click);
            // 
            // btnCLR
            // 
            this.btnCLR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCLR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCLR.Location = new System.Drawing.Point(478, 11);
            this.btnCLR.Name = "btnCLR";
            this.btnCLR.Size = new System.Drawing.Size(41, 23);
            this.btnCLR.TabIndex = 12;
            this.btnCLR.TabStop = false;
            this.btnCLR.Text = "CLR";
            this.toolTip.SetToolTip(this.btnCLR, "Очистить окно результатов  (Ctrl + R)");
            this.btnCLR.UseVisualStyleBackColor = true;
            this.btnCLR.Click += new System.EventHandler(this.btnCLR_Click);
            // 
            // txtCommand
            // 
            this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommand.FormattingEnabled = true;
            this.txtCommand.Location = new System.Drawing.Point(6, 503);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(424, 21);
            this.txtCommand.TabIndex = 10;
            this.txtCommand.TextChanged += new System.EventHandler(this.txtCommand_TextChanged);
            this.txtCommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommand_KeyDown);
            // 
            // btnSendCommand
            // 
            this.btnSendCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendCommand.Location = new System.Drawing.Point(436, 501);
            this.btnSendCommand.Name = "btnSendCommand";
            this.btnSendCommand.Size = new System.Drawing.Size(75, 23);
            this.btnSendCommand.TabIndex = 9;
            this.btnSendCommand.Text = "Отправить";
            this.toolTip.SetToolTip(this.btnSendCommand, "Отправить введенную команду в COM-порт (Enter)");
            this.btnSendCommand.UseVisualStyleBackColor = true;
            this.btnSendCommand.Click += new System.EventHandler(this.btnSendCommand_Click);
            // 
            // txtCommandBox
            // 
            this.txtCommandBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommandBox.Location = new System.Drawing.Point(6, 39);
            this.txtCommandBox.Name = "txtCommandBox";
            this.txtCommandBox.ReadOnly = true;
            this.txtCommandBox.Size = new System.Drawing.Size(513, 457);
            this.txtCommandBox.TabIndex = 8;
            this.txtCommandBox.TabStop = false;
            this.txtCommandBox.Text = "";
            this.txtCommandBox.WordWrap = false;
            this.txtCommandBox.TextChanged += new System.EventHandler(this.txtCommandBox_Scroll);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabTerminal);
            this.tabControl.Location = new System.Drawing.Point(3, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(705, 566);
            this.tabControl.TabIndex = 0;
            this.tabControl.TabStop = false;
            this.toolTip.SetToolTip(this.tabControl, "Редактор скрипта прошивки (Ctrl + E)");
            // 
            // frmModemCfg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 572);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(725, 610);
            this.Name = "frmModemCfg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmModemCfg_Closing);
            this.Load += new System.EventHandler(this.frmModemCfg_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmModemCfg_KeyDown);
            this.tabTerminal.ResumeLayout(false);
            this.grpParameters.ResumeLayout(false);
            this.grpParameters.PerformLayout();
            this.grpCommand.ResumeLayout(false);
            this.grpCommand.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage tabTerminal;
        private System.Windows.Forms.GroupBox grpParameters;
        private System.Windows.Forms.TextBox txtPortTimeout;
        private System.Windows.Forms.Label lblPortReadTimeout;
        private System.Windows.Forms.TextBox txtPortBuffer;
        private System.Windows.Forms.Label lblPortReadBuffer;
        private System.Windows.Forms.Button btnPortOpen;
        private System.Windows.Forms.Label lblPortHandshake;
        private System.Windows.Forms.ComboBox cmbPortHandshake;
        private System.Windows.Forms.Label lblPortParity;
        private System.Windows.Forms.ComboBox cmbPortParity;
        private System.Windows.Forms.Label lblPortData;
        private System.Windows.Forms.ComboBox cmbPortData;
        private System.Windows.Forms.Label lblPortBaud;
        private System.Windows.Forms.Label lblPortName;
        private System.Windows.Forms.ComboBox cmbPortBaud;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.GroupBox grpCommand;
        private System.Windows.Forms.CheckBox chkSendAsByte;
        private System.Windows.Forms.Button btnKey;
        private System.Windows.Forms.Button btnCRLF;
        private System.Windows.Forms.Button btnRTS;
        private System.Windows.Forms.Button btnDTR;
        private System.Windows.Forms.Button btnWW;
        private System.Windows.Forms.Button btnCLR;
        private System.Windows.Forms.ComboBox txtCommand;
        private System.Windows.Forms.Button btnSendCommand;
        private System.Windows.Forms.RichTextBox txtCommandBox;
        private System.Windows.Forms.TabControl tabControl;


    }
}

