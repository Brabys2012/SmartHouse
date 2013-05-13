using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Text;

namespace AsyncClient
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Экземпляр для работы с сервером.
        /// </summary>
        AsynchronousClient Client = new AsynchronousClient();

        /// <summary>
        /// Указывает на необходимость шифрования данных
        /// </summary>
        bool encryptIt = false;

        string[] allParam;
        string[] loginAndPass;
        

        public MainForm()
        {
            InitializeComponent();
            //Инициализация базовых узлов дерева.
            trvDevice.BeginUpdate();
            trvDevice.Nodes.Add("SimpleDev", "SimpleDev");
            trvDevice.Nodes["SimpleDev"].Tag = "SimpleDev";
            trvDevice.Nodes.Add("Simple sensor", "Simple sensor");
            trvDevice.Nodes["Simple sensor"].Tag = "Simple sensor";
            trvDevice.Nodes.Add("Dimmers", "Dimmers");
            trvDevice.Nodes["Dimmers"].Tag = "Dimmers";
            trvDevice.Nodes.Add("Sensors with multi-state", "Sensors with multi-state");
            trvDevice.Nodes["Sensors with multi-state"].Tag = "Sensors with multi-state";
            trvDevice.EndUpdate();
            grpSensor.Enabled = false;
            grpDimmers.Enabled = false;
            grpDevice.Enabled = false;
            this.SystemConf.Visible = false;

            //Подписываемя на события логической части лиента.
            Client.IsNeedUpdateThreeEvent += new IsNeedUpdateThreeDelegate(Client_IsNeedUpdateThreeEvent);
            Client.StatusIsActiveEvent += new StatusIsActiveDelegate(Client_StatusIsActive);
            Client.IsNeedShowLoginFormEvent += new IsNeedShowDelegate(Client_IsNeedShowLoginFormEvent);
            Client.IsNeedShowDataEvent += new IsNeedShowDataDelegate(Client_IsNeedShowDataEvent);
            Client.IsNeedToPlotEvent += new IsNeedToPlotDelegate(Client_IsNeedToPlotEvent);

            //Автоподключение
            string FileData;
            char[] trimChar = new char[2] {'\r','\n'};
            using (var sr = new StreamReader("Config.dat", Encoding.GetEncoding(1251)))
            {
                FileData = sr.ReadToEnd();
            }
            allParam = FileData.Split(';');
            loginAndPass = allParam[0].Split(',');
            encryptIt = Convert.ToBoolean(allParam[1].TrimStart(trimChar));
            Client.StartClient(loginAndPass[0], Convert.ToInt32(loginAndPass[1]));
        }

        /// <summary>
        /// Обработка события обновления дерева.
        /// </summary>
        /// <param name="DevData"></param>
        void Client_IsNeedUpdateThreeEvent(string DevData)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new IsNeedUpdateThreeDelegate(Client_IsNeedUpdateThreeEvent), DevData);
            }
            else
            {
                string[] dev = DevData.Split('*');
                this.trvDevice.BeginUpdate();
                int index = trvDevice.Nodes[dev[0]].Nodes.IndexOfKey(dev[1]) ;
                if ( index > -1)
                {
                    trvDevice.Nodes[dev[0]].Nodes[index].Remove();

                }
                trvDevice.Nodes[dev[0]].Nodes.Add(dev[1], dev[1]);
                index = trvDevice.Nodes[dev[0]].Nodes.IndexOfKey(dev[1]);
                trvDevice.Nodes[dev[0]].Nodes[index].Tag = dev[2];
                this.trvDevice.EndUpdate();
            }
        }

        /// <summary>
        /// Обработка события отображения отчёта. Построения графика.
        /// </summary>
        ReportForm rptForm;
        void Client_IsNeedToPlotEvent(string reportData)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new IsNeedToPlotDelegate(Client_IsNeedToPlotEvent),reportData);
            }
            else
            {
                rptForm = new ReportForm(reportData);
                rptForm.ShowDialog();
            } 
        }

        /// <summary>
        /// Обработка события обновления состояния статуса подключения и вкладки "Конфигурирование".
        /// </summary>
        /// <param name="status"></param>
        void Client_StatusIsActive(bool status)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new StatusIsActiveDelegate(Client_StatusIsActive),status);
                }
                else
                {
                    if (status)
                        this.stLabel.Text = "Подключён";
                    else this.stLabel.Text = "Отключен";
                    if (Client._srv.role == "admin")
                        this.SystemConf.Visible = true;
                    else this.SystemConf.Visible = false;
                }
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        /// <summary>
        /// Обработка события необходимости отобразить информацию
        /// </summary>
        /// <param name="data"></param>
        void Client_IsNeedShowDataEvent(string data)
        {
            if (this.InvokeRequired)
                this.Invoke(new IsNeedShowDataDelegate(Client_IsNeedShowDataEvent), data);
            else
            {
                tbMess.Text += "\r\n" + data;
            }
        }

        /// <summary>
        /// Обработка события необъодимости отобразить форму заполнения логина и пароля.
        /// </summary>
        AuthForm _frm;
        void Client_IsNeedShowLoginFormEvent()
        {
            if (this.InvokeRequired)
                this.Invoke(new IsNeedShowDelegate(Client_IsNeedShowLoginFormEvent));
            else
            {
                _frm = new AuthForm();
                if (_frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Client.Send(_frm.tbLogin.Text + "." + _frm.tbPassword.Text,encryptIt);
                }
            }
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// При закрытии формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.stLabel.Text == "Подключён")
            {
                Client.Send("EXIT",encryptIt);
                Client.CloseConnection();
            } 
        }

        /// <summary>
        /// Работа с конфигурационным файлом. Сохранение параметров подключения(ip - адрес, порт).
        /// </summary>
        ConnectParams _ParamForm;
        private void ConfCoonectParms_Click(object sender, EventArgs e)
        {
            _ParamForm = new ConnectParams();
            if (_ParamForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Client.SaveConnData(_ParamForm.tbIP.Text + "," + _ParamForm.tbPort.Text, @"\d\d?\d?\.\d\d?\d?\.\d\d?\d?\.\d\d?\d?\,{1}\d*"); 
            }
        }

        /// <summary>
        /// Обработка кнопки "Подключится" контексного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connect_Click(object sender, EventArgs e)
        {
            char[] charToTrim = new char[1] { ';' };
            //проверка текущего подключения
            if (!Client._srv.status)
            { //если в екущий момент отключены
                string FileData;
                string[] tempArray;
                using (var sr = new StreamReader("Config.dat", Encoding.GetEncoding(1251)))
                {
                    FileData = sr.ReadLine();
                }
                tempArray = FileData.Split(',');
                Client.StartClient(tempArray[0], Convert.ToInt32(tempArray[1].TrimEnd(charToTrim)));
               
            }
            else 
            { //если уже подключены
                MessageBox.Show("Соединение уже установлено", "Состояние подключения",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Обработка кнопки "Отключить" контексного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Disconnect_Click(object sender, EventArgs e)
        {
            if (this.stLabel.Text == "Подключён")
            {
                Client.Send("EXIT", encryptIt);
                Client.CloseConnection();
                Client._srv.status = false;
                
            } 
        }

        /// <summary>
        /// Обработка события выборя узля дерева.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvDevice_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trvDevice.SelectedNode.Level == 0)
            {
                if (trvDevice.SelectedNode.Tag.ToString() == "SimpleDev")
                {
                    grpDevice.Enabled = true;
                    grpDimmers.Enabled = false;
                    grpSensor.Enabled = false;
                    grpCounters.Enabled = false;
                    this.lDevName_.Text = trvDevice.SelectedNode.Text;
                    this.butAction.Enabled = false;
                    
                }
                else if (trvDevice.SelectedNode.Tag.ToString() == "Simple sensor")
                {
                    grpSensor.Enabled = true;
                    grpDimmers.Enabled = false;
                    grpDevice.Enabled = false;
                    grpCounters.Enabled = false;
                }
                else if (trvDevice.SelectedNode.Tag.ToString() == "Dimmers")
                {
                    grpDevice.Enabled = false;
                    grpDimmers.Enabled = true;
                    grpSensor.Enabled = false;
                    grpCounters.Enabled = false;
                    this.tbDimmersPower.Enabled = false;
                    this.butDimmersSet.Enabled = false;
                }
                else if (trvDevice.SelectedNode.Tag.ToString() == "Sensors with multi-state")
                {
                    grpDevice.Enabled = false;
                    grpDimmers.Enabled = false;
                    grpSensor.Enabled = false;
                    grpCounters.Enabled = true;
                    this.butReport.Enabled = false;
                }
            }
            else
            {
                if (trvDevice.SelectedNode.Parent.Tag.ToString() == "SimpleDev")
                {
                    grpSensor.Enabled = false;
                    grpDimmers.Enabled = false;
                    grpDevice.Enabled = true;
                    this.lDevName_.Text = trvDevice.SelectedNode.Text;
                    if (trvDevice.SelectedNode.Tag.ToString() == "0")
                        butAction.Text = "Включить";
                    else butAction.Text = "Выключить";
                    this.butAction.Enabled = true;
                }
                else if (trvDevice.SelectedNode.Parent.Tag.ToString() == "Simple sensor")
                {
                    grpSensor.Enabled = true;
                    grpDimmers.Enabled = false;
                    grpDevice.Enabled = false;
                    this.lSensorValue.Text = trvDevice.SelectedNode.Tag.ToString();
                }
                else if (trvDevice.SelectedNode.Parent.Tag.ToString() == "Dimmers")
                {
                    grpDevice.Enabled = false;
                    grpDimmers.Enabled = true;
                    grpSensor.Enabled = false;
                    this.tbDimmersPower.Text = trvDevice.SelectedNode.Tag.ToString();
                }
                else if (trvDevice.SelectedNode.Parent.Tag.ToString() == "Sensors with multi-state")
                {
                    grpDevice.Enabled = false;
                    grpDimmers.Enabled = false;
                    grpSensor.Enabled = false;
                    grpCounters.Enabled = true;
                    this.lCounterValue.Text = trvDevice.SelectedNode.Tag.ToString();
                }
            }
        }
        
        /// <summary>
        /// Обработка нажатия на кнопку "Построить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butReport_Click(object sender, EventArgs e)
        {
            DataForReport _ReportDataForm = new DataForReport();
            if (_ReportDataForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                Client.Send("GetCounterRec/" + _ReportDataForm.BegDate +"/" +
                    _ReportDataForm.EndDate + "/" + this.trvDevice.SelectedNode.Text + "?", encryptIt);
            }
        }

        private void butAction_Click(object sender, EventArgs e)
        {
            string param = "";
            if (butAction.Text == "Выключить")
            {
                param = "0";
            }
            else param = "1";
            Client.Send("SetParam/" + trvDevice.SelectedNode.Text + "/" + param + "?", encryptIt);
        }

        private void butGetUpdate_Click(object sender, EventArgs e)
        {
            Client.Send("GetUpdate?", encryptIt);
        }

        private void butDimmersSet_Click(object sender, EventArgs e)
        {
            Client.Send("SetParam/" + trvDevice.SelectedNode.Text + "/" + tbDimmersPower.Text + "?", encryptIt);
        }

        private void SystemConf_Click(object sender, EventArgs e)
        {
            AdminPanel AdmPanel = new AdminPanel(Client, encryptIt);
            AdmPanel.ShowDialog();
        }
    }
}
