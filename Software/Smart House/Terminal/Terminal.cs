using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace RUS_Project.TerminalConfigurator
{

    public partial class frmModemCfg : Form
    {

        /// <summary>Порт, с которым работаем в текущий момент.</summary>
        SerialPort COM_Port;
        /// <summary>Признак необходимости добавить в конец команды символ новой строки.</summary>
        bool AddCRLF;
        /// <summary>Признак необходимости перехватывать клавиатурные нажатия и отправлять сразу же в COM-порт.</summary>
        bool ReadKey;


        #region Form

        /// <summary>
        /// Инициализация компонентов формы.
        /// </summary>
        public frmModemCfg(string[] args)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод, выполняемый при открытии формы.
        /// </summary>
        private void frmModemCfg_Load(object sender, EventArgs e)
        {
            // Отображаем версию программы в заголовке
            this.Text = Application.ProductName + " (v. " + Application.ProductVersion + ")";
            // Загржаем в выпадающий список доступные порты
            if (SerialPort.GetPortNames().Length == 0)
            {
                WriteString("В системе не установлено ни одного COM-порта", MessageType.Error, true);
                WriteString("Произведите установку COM-портов и перезапустите программу", MessageType.Error, true);
                return;
            }
            else
            {
                cmbPortName.Items.AddRange((object[])SerialPort.GetPortNames());
            }
            // Загружаем в списки другие параметры передачи данных.
            cmbPortBaud.Items.AddRange(new object[] { "300", "600", "1200", "2400", "4800", "9600", "14400", "19200", "28800", "38400", "57600", "115200" });
            cmbPortData.Items.AddRange(new object[] { "7", "8" });
            cmbPortParity.Items.AddRange((object[])Enum.GetNames(typeof(Parity)));
            cmbPortStopBits.Items.AddRange((object[])Enum.GetNames(typeof(StopBits)));
            cmbPortHandshake.Items.AddRange((object[])Enum.GetNames(typeof(Handshake)));

            // Инициируем COM-порт и необходимые флаги
            COM_Port = new SerialPort();
            AddCRLF = true;
            ReadKey = false;
            // Инициируем элементы управления
            foreach (Control element in grpParameters.Controls)
                element.Enabled = true;
            foreach (Control element in grpCommand.Controls)
                element.Enabled = false;
            btnCRLF.ForeColor = Color.Green;
            btnKey.ForeColor = Color.Red;
            btnWW.ForeColor = Color.Red;
            btnWW.Enabled = true;
            btnCLR.Enabled = true;
            txtCommandBox.Enabled = true;
            btnRTS.ForeColor = Color.Red;
            btnDTR.ForeColor = Color.Red;
            this.AcceptButton = btnPortOpen;
        }

        /// <summary>
        /// Метод, выполняемый при закрытии формы во время завершения работы программы.
        /// </summary>
        private void frmModemCfg_Closing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Завершить работу с программой?", "Выход ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                // Закрываем порт
                try
                {
                    COM_Port.Close();
                    COM_Port.Dispose();
                    COM_Port = null;
                }
                catch
                {
                    COM_Port.Dispose();
                    COM_Port = null;
                }

                // Ожидаем несколько секунд для закрытия порта
                System.Threading.Thread.Sleep(2000);
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Отвечает за поддержку "Горячих клавиш".
        /// </summary>
        private void frmModemCfg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.O:
                        btnPortOpen_Click(sender, e);
                        break;
                    case Keys.L:
                        btnCRLF_Click(sender, e);
                        break;
                    case Keys.K:
                        btnKey_Click(sender, e);
                        break;
                    case Keys.W:
                        btnWW_Click(sender, e);
                        break;
                    case Keys.R:
                        btnCLR_Click(sender, e);
                        break;
                }
            }
        }

        #endregion

        /// <summary>
        /// Определяет тип выводимого сообщения.
        /// </summary>
        enum MessageType { Success, Error, Normal };

        /// <summary>
        /// Универсальный метод для вывода заданной строки в указанное место.
        /// </summary>
        /// <param name="data">Строка данных для вывода.</param>
        /// <param name="type">Тип выводимого сообщения.</param>
        /// <param name="ns">Добавить символ новой строки.</param>
        delegate void AddText(string data, MessageType type, bool ns);

        /// <summary>
        /// Выводит в поле вывода заданную строку.
        /// </summary>
        /// <param name="data">Строка данных для вывода.</param>
        /// <param name="type">Тип выводимого сообщения.</param>
        /// <param name="ns">Добавить символ новой строки.</param>
        private void WriteString(string data, MessageType type, bool ns)
        {
            if (txtCommandBox.InvokeRequired)
            {
                AddText d = new AddText(WriteString);
                txtCommandBox.BeginInvoke(d, new object[] { data, type, ns });
            }
            else
            {
                switch (type)
                {
                    case MessageType.Success:
                        txtCommandBox.SelectionColor = Color.Green;
                        if (ns)
                            txtCommandBox.AppendText(data + "\n");
                        else
                            txtCommandBox.AppendText(data);
                        break;
                    case MessageType.Error:
                        txtCommandBox.SelectionColor = Color.Red;
                        if (ns)
                            txtCommandBox.AppendText(data + "\n");
                        else
                            txtCommandBox.AppendText(data);
                        break;
                    case MessageType.Normal:
                        txtCommandBox.SelectionColor = Color.Black;
                        txtCommandBox.AppendText(data);
                        break;
                }
                txtCommandBox.SelectionColor = Color.Black;
            }
        }

        /// <summary>
        /// Метод, выполняемый при нажатии на кнопку Открыть
        /// </summary>
        private void btnPortOpen_Click(object sender, EventArgs e)
        {
            // В зависимости от того открыт порт или нет, мы его либо закрываем, либо открываем
            if (COM_Port.IsOpen)
            {
                try
                {
                    // Отписываемся от событий, закрываем порт и выводим сообщение о закрытии последовательного порта
                    COM_Port.DataReceived -= COM_Port_DataReceived;
                    COM_Port.ErrorReceived -= COM_Port_ErrorReceived;
                    COM_Port.Close();
                    WriteString("Последовательный порт " + COM_Port.PortName + " закрыт", MessageType.Success, true);
                }
                catch (Exception exc)
                {
                    WriteString("Не удалось закрыть последовательный порт " + COM_Port.PortName + ", по причине: " + exc.Message, MessageType.Error, true);
                    WriteString("Открытый порт обнулен в NULL", MessageType.Error, true);
                    WriteString("Проверьте настройки подключения и повторите попытку.", MessageType.Error, true);
                    COM_Port.Dispose();
                    COM_Port = null;
                }
                // Активируем или деактивируем элементы управления
                foreach (Control element in grpParameters.Controls)
                    element.Enabled = true;
                btnPortOpen.Text = "Открыть";
                foreach (Control element in grpCommand.Controls)
                    element.Enabled = false;
                btnCLR.Enabled = true;
                btnWW.Enabled = true;
                txtCommandBox.Enabled = true;
                this.AcceptButton = btnPortOpen;
                cmbPortName.Focus();
            }
            else
            {
                try
                {
                    // Открываем последовательный порт с заданными параметрами и выводим сообщение
                    COM_Port = null;
                    COM_Port = new SerialPort();
                    COM_Port.PortName = cmbPortName.SelectedItem.ToString();
                    COM_Port.BaudRate = Convert.ToInt32(cmbPortBaud.SelectedItem.ToString());
                    COM_Port.DataBits = Convert.ToInt32(cmbPortData.SelectedItem.ToString());
                    COM_Port.Parity = (Parity)cmbPortParity.SelectedIndex;
                    COM_Port.StopBits = (StopBits)cmbPortStopBits.SelectedIndex;
                    COM_Port.Handshake = (Handshake)cmbPortHandshake.SelectedIndex;
                    COM_Port.ReadBufferSize = Convert.ToInt32(txtPortBuffer.Text);
                    COM_Port.ReadTimeout = Convert.ToInt32(txtPortTimeout.Text);
                    COM_Port.WriteBufferSize = COM_Port.ReadBufferSize = Convert.ToInt32(txtPortBuffer.Text);
                    COM_Port.WriteTimeout = COM_Port.ReadTimeout = Convert.ToInt32(txtPortTimeout.Text);
                    COM_Port.Encoding = Encoding.ASCII;
                    COM_Port.DataReceived += new SerialDataReceivedEventHandler(COM_Port_DataReceived);
                    COM_Port.ErrorReceived += new SerialErrorReceivedEventHandler(COM_Port_ErrorReceived);
                    COM_Port.Open();
                    WriteString("Последовательный порт " + COM_Port.PortName + " открыт", MessageType.Success, true);
                }
                catch (Exception err)
                {
                    WriteString("Не удалось открыть последовательный порт " + COM_Port.PortName + ", по причине: " + err.Message, MessageType.Error, true);
                    WriteString("Проверьте настройки подключения и повторите попытку.", MessageType.Error, true);
                    return;
                }
                // Активируем или деактивируем элементы управления
                foreach (Control element in grpCommand.Controls)
                    element.Enabled = true;
                foreach (Control PortControl in grpParameters.Controls)
                    PortControl.Enabled = false;
                btnPortOpen.Enabled = true;
                btnPortOpen.Text = "Закрыть";
                this.AcceptButton = btnSendCommand;
                txtCommand.Focus();
            }
        }

        /// <summary>
        /// Метод, выполняемый при появлении данных во входном буффере данных последовательного порта.
        /// </summary>
        /// <param name="sender">Ссылка на экземпляр класса, который сгенерировал сообытие.</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        void COM_Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            //
            // TODO: Дописать необходимый код при получении данных
            //

            WriteString(COM_Port.ReadExisting(), MessageType.Success, true);
        }

        /// <summary>
        /// Метод, выполняемый при появлении ошибки работы последовательного порта.
        /// </summary>
        /// <param name="sender">Ссылка на экземпляр класса, который сгенерировал сообытие.</param>
        /// <param name="e">Данные о произошедшем событии.</param>
        void COM_Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

            //
            // TODO: Дописать необходимый код при получении ошибки
            // 

            WriteString(COM_Port.ReadExisting(), MessageType.Error, true);
        }

        /// <summary>
        /// Метод, изменяющий состояние линии DTR последовательного порта.
        /// </summary>
        private void btnDTR_Click(object sender, EventArgs e)
        {
            COM_Port.DtrEnable = !COM_Port.DtrEnable;
            if (COM_Port.DtrEnable)
                btnDTR.ForeColor = Color.Green;
            else
                btnDTR.ForeColor = Color.Red;
            txtCommand.Focus();
        }

        /// <summary>
        /// Метод, изменяющий состояние линии RTS последовательного порта.
        /// </summary>
        private void btnRTS_Click(object sender, EventArgs e)
        {
            COM_Port.RtsEnable = !COM_Port.RtsEnable;
            if (COM_Port.RtsEnable)
                btnRTS.ForeColor = Color.Green;
            else
                btnRTS.ForeColor = Color.Red;
            txtCommand.Focus();
        }

        /// <summary>
        /// Метод, выполняемый при раскрытии списка доступных портов.
        /// </summary>
        private void cmbPortName_Click(object sender, EventArgs e)
        {
            // Запоминаем предыдущий индекс
            string tmp = cmbPortName.Text;
            // Очищаем список доступных портов и загружаем их заново
            cmbPortName.Items.Clear();
            cmbPortName.Items.AddRange((object[])SerialPort.GetPortNames());
            // Устанавливаем индекс обратно
            int ind = cmbPortName.Items.IndexOf(tmp);
            if (ind > 0)
                cmbPortName.SelectedIndex = ind;
            else
                cmbPortName.SelectedIndex = 0;
        }

        /// <summary>
        /// Метод, осуществляющий прокручивание вниз результата выполнения команды.
        /// </summary>
        private void txtCommandBox_Scroll(object sender, EventArgs e)
        {
            txtCommandBox.SelectionStart = txtCommandBox.Text.Length;
            txtCommandBox.ScrollToCaret();
        }

        /// <summary>
        /// Метод, выполняемый при нажатии на кнопку CR/LF.
        /// </summary>
        private void btnCRLF_Click(object sender, EventArgs e)
        {
            AddCRLF = !AddCRLF;
            if (AddCRLF)
                btnCRLF.ForeColor = Color.Green;
            else
                btnCRLF.ForeColor = Color.Red;
            // Передаем фокус одному из элементов управления
            if (txtCommand.Enabled)
                txtCommand.Focus();
            else
                btnPortOpen.Focus();
        }

        /// <summary>
        /// Метод, выполняемый при нажатии на кнопку Key.
        /// </summary>
        private void btnKey_Click(object sender, EventArgs e)
        {
            ReadKey = !ReadKey;
            if (ReadKey)
            {
                btnKey.ForeColor = Color.Green;
                btnSendCommand.Enabled = false;
                chkSendAsByte.Checked = false;
                chkSendAsByte.Enabled = false;
                this.AcceptButton = null;
            }
            else
            {
                btnKey.ForeColor = Color.Red;
                btnSendCommand.Enabled = true;
                chkSendAsByte.Enabled = true;
                this.AcceptButton = btnPortOpen;
            }
            txtCommand.Focus();
        }

        /// <summary>
        /// Метод, осуществляющий перенос строк в текстовом поле.
        /// </summary>
        private void btnWW_Click(object sender, EventArgs e)
        {
            txtCommandBox.WordWrap = !txtCommandBox.WordWrap;
            if (txtCommandBox.WordWrap)
                btnWW.ForeColor = Color.Green;
            else
                btnWW.ForeColor = Color.Red;
            // Передаем фокус одному из элементов управления
            if (txtCommand.Enabled)
                txtCommand.Focus();
            else
                btnPortOpen.Focus();
        }

        /// <summary>
        /// Метод, осуществляющий очистку текстового поля.
        /// </summary>
        private void btnCLR_Click(object sender, EventArgs e)
        {
            txtCommandBox.Clear();
            txtCommand.Focus();
        }

        /// <summary>
        /// Метод, выполняемый при нажатии на кнопку Отправить.
        /// </summary>
        private void btnSendCommand_Click(object sender, EventArgs e)
        {

            //
            // TODO: Дописать необходимый код при отправке команды
            //

            string message = txtCommand.Text;
            try
            {
                COM_Port.Write(message);
            }
            catch (Exception exc)
            {
                WriteString(exc.Message, MessageType.Error, true);
            }
            WriteString(message, MessageType.Success, true);
            if (!txtCommand.Items.Contains((object)txtCommand.Text))
                txtCommand.Items.Add(txtCommand.Text);
            txtCommand.Text = "";
            txtCommand.Focus();
        }

        /// <summary>
        /// Производит отправку введенных символов в COM-порт.
        /// </summary>
        private void txtCommand_TextChanged(object sender, EventArgs e)
        {
            if (ReadKey & txtCommand.Text != "")
            {
                try { COM_Port.Write(txtCommand.Text); }
                catch (Exception exc) { WriteString(exc.Message, MessageType.Error, true); }
                WriteString(txtCommand.Text, MessageType.Success, false);
                txtCommand.Text = "";
                txtCommand.Focus();
            }

        }

        /// <summary>
        /// Производит отправку Enter-последовательности в COM-порт.
        /// </summary>
        private void txtCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (ReadKey & e.KeyCode == Keys.Enter)
            {
                byte n = 13;
                byte r = 10;
                try { COM_Port.Write(String.Format("{0}{1}", (char)n, (char)r)); }
                catch (Exception exc) { WriteString(exc.Message, MessageType.Error, true); }
                WriteString("", MessageType.Normal, true);
                txtCommand.Focus();
            }

        }

    }
}
