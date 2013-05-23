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

        string[] allParam;
        string[] loginAndPass;
        

        public MainForm()
        {
            InitializeComponent();
            //Инициализация базовых узлов дерева.
            trvDevice.BeginUpdate();
            trvDevice.Nodes.Add("Простое устройство", "Простое устройство");
            trvDevice.Nodes["Простое устройство"].Tag = "Простое устройство";
            trvDevice.Nodes.Add("Датчик", "Датчик");
            trvDevice.Nodes["Датчик"].Tag = "Датчик";
            trvDevice.Nodes.Add("Димеры", "Димеры");
            trvDevice.Nodes["Димеры"].Tag = "Димеры";
            trvDevice.Nodes.Add("Счётчики", "Счётчики");
            trvDevice.Nodes["Счётчики"].Tag = "Счётчики";
            trvDevice.EndUpdate();
            grpSensor.Enabled = false;
            grpDimmers.Enabled = false;
            grpDevice.Enabled = false;
            this.SystemConf.Visible = false;

            //Подписываемя на события логической части лиента.
            Client.IsNeedUpdateThreeEvent += new IsNeedUpdateThreeDelegate(Client_IsNeedUpdateThreeEvent);
            Client.IsNeedShowLoginFormEvent += new IsNeedShowDelegate(Client_IsNeedShowLoginFormEvent);
            Client.IsNeedShowDataEvent += new IsNeedShowDataDelegate(Client_IsNeedShowDataEvent);
            Client.IsNeedToPlotEvent += new IsNeedToPlotDelegate(Client_IsNeedToPlotEvent);
            Client.IsNeedChangeStatusEvent += new IsNeedChangeStatus(Client_IsNeedChangeStatusEvent);
            Client.IsNeedToChangeConfStatusEvent += new IsNeedToChangeConfStatus(Client_IsNeedToChangeConfStatusEvent);
            Client.IsNeedShowOperationResultEvent += new IsNeedShowOperationResult(Client_IsNeedShowOperationResultEvent);

            //Автоподключение
            string FileData;
            char[] trimChar = new char[2] {'\r','\n'};
            using (var sr = new StreamReader("Config.dat", Encoding.GetEncoding(1251)))
            {
                FileData = sr.ReadToEnd();
            }
            allParam = FileData.Split(';');
            loginAndPass = allParam[0].Split(',');
            Client.StartClient(loginAndPass[0], Convert.ToInt32(loginAndPass[1]));
            Client._srv.encryptIt = Convert.ToBoolean(allParam[1].TrimStart(trimChar));
        }

        void Client_IsNeedShowOperationResultEvent(string result)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new IsNeedShowOperationResult(Client_IsNeedShowOperationResultEvent), result);
            }
            else
            {
                MessageBox.Show(result, "Сообщение", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        /// <summary>
        /// Обработка события изменения видимости панели конфигиурирования
        /// </summary>
        /// <param name="role">роль клиента(его права)</param>
        void Client_IsNeedToChangeConfStatusEvent(string role)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new IsNeedToChangeConfStatus(Client_IsNeedToChangeConfStatusEvent), role);
            }
            else
            {
                if (role == "admin")
                    this.SystemConf.Visible = true;
                else
                    this.SystemConf.Visible = false;
            }
        }

        /// <summary>
        /// Обработка события необходимости измениния статуса
        /// </summary>
        void Client_IsNeedChangeStatusEvent()
        {
           if (this.InvokeRequired)
                {
                    this.Invoke(new IsNeedChangeStatus(Client_IsNeedChangeStatusEvent));
                }
           else
           {
               if (Client._srv.status)
               {
                   Client._srv.status = false;
                   this.stLabel.Text = "Отключен";
               }
               else
               {
                   Client._srv.status = true;
                   this.stLabel.Text = "Подключен";
               }
           }
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
                int index = trvDevice.Nodes[dev[1]].Nodes.IndexOfKey(dev[0]) ;
                if ( index > -1)
                {
                    trvDevice.Nodes[dev[0]].Nodes[index].Remove();

                }
                trvDevice.Nodes[dev[1]].Nodes.Add(dev[0], dev[0]);
                index = trvDevice.Nodes[dev[1]].Nodes.IndexOfKey(dev[0]);
                trvDevice.Nodes[dev[1]].Nodes[index].Tag = dev[2];
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
        /// Обработка события необходимости отобразить форму заполнения логина и пароля.
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
                    Client.Send(_frm.tbLogin.Text + "." + _frm.tbPassword.Text,Client._srv.encryptIt);
                }
            }
        
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
                Client.Send("EXIT",Client._srv.encryptIt);
                Client.CloseConnection();
            } 
        }

        /// <summary>
        /// Работа с конфигурационным файлом. Сохранение параметров подключения.
        /// </summary>
        ConnectParams _ParamForm;
        private void ConfCoonectParms_Click(object sender, EventArgs e)
        {
            string[] IpAndPort = { "", "" };
            string FileData;
            char[] trimChar = new char[2] { '\r', '\n' };
            using (var sr = new StreamReader("Config.dat", Encoding.GetEncoding(1251)))
            {
                FileData = sr.ReadToEnd();
            }
            allParam = FileData.Split(';');
            IpAndPort = allParam[0].Split(',');
            _ParamForm = new ConnectParams(IpAndPort[0], IpAndPort[1], Convert.ToBoolean(allParam[1].TrimStart(trimChar)));
            if (_ParamForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Client.SaveConnData(_ParamForm.tbIP.Text + "," + _ParamForm.tbPort.Text,
                    @"\d\d?\d?\.\d\d?\d?\.\d\d?\d?\.\d\d?\d?\,{1}\d*");
                ///TODO придумать паттерн
                Client.SaveConnData(Convert.ToString(_ParamForm.chbUseEncrypting.Checked), "");
            }
        }

        /// <summary>
        /// Обработка кнопки "Подключится" контексного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connect_Click(object sender, EventArgs e)
        {
            //проверка текущего подключения
            if (!Client._srv.status)
            {
                string[] IpAndPort = { "", "" };
                //если в текущий момент отключены
                string FileData;
                char[] trimChar = new char[2] { '\r', '\n' };
                using (var sr = new StreamReader("Config.dat", Encoding.GetEncoding(1251)))
                {
                    FileData = sr.ReadToEnd();
                }
                allParam = FileData.Split(';');
                IpAndPort = allParam[0].Split(',');
                Client.StartClient(loginAndPass[0], Convert.ToInt32(loginAndPass[1]));
                Client._srv.encryptIt = Convert.ToBoolean(allParam[1].TrimStart(trimChar));
               
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
                Client.Send("EXIT", Client._srv.encryptIt);
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
                if (trvDevice.SelectedNode.Tag.ToString() == "Простое устройство")
                {
                    grpDevice.Enabled = true;
                    grpDimmers.Enabled = false;
                    grpSensor.Enabled = false;
                    grpCounters.Enabled = false;
                    this.lDevName_.Text = trvDevice.SelectedNode.Text;
                    this.butAction.Enabled = false;
                    
                }
                else if (trvDevice.SelectedNode.Tag.ToString() == "Датчик")
                {
                    grpSensor.Enabled = true;
                    grpDimmers.Enabled = false;
                    grpDevice.Enabled = false;
                    grpCounters.Enabled = false;
                }
                else if (trvDevice.SelectedNode.Tag.ToString() == "Димеры")
                {
                    grpDevice.Enabled = false;
                    grpDimmers.Enabled = true;
                    grpSensor.Enabled = false;
                    grpCounters.Enabled = false;
                    this.tbDimmersPower.Enabled = false;
                    this.butDimmersSet.Enabled = false;
                }
                else if (trvDevice.SelectedNode.Tag.ToString() == "Счётчики")
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
                if (trvDevice.SelectedNode.Parent.Tag.ToString() == "Простое устройство")
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
                else if (trvDevice.SelectedNode.Parent.Tag.ToString() == "Датчик")
                {
                    grpSensor.Enabled = true;
                    grpDimmers.Enabled = false;
                    grpDevice.Enabled = false;
                    this.lSensorValue.Text = trvDevice.SelectedNode.Tag.ToString();
                }
                else if (trvDevice.SelectedNode.Parent.Tag.ToString() == "Димеры")
                {
                    grpDevice.Enabled = false;
                    grpDimmers.Enabled = true;
                    grpSensor.Enabled = false;
                    this.tbDimmersPower.Text = trvDevice.SelectedNode.Tag.ToString();
                }
                else if (trvDevice.SelectedNode.Parent.Tag.ToString() == "Счётчики")
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
                    _ReportDataForm.EndDate + "/" + this.trvDevice.SelectedNode.Text, Client._srv.encryptIt);
            }
        }

        private void butAction_Click(object sender, EventArgs e)
        {
            string param = "";
            if (butAction.Text == "Выключить")
                param = "0";
            else param = "1";
            Client.Send("SetParam/" + trvDevice.SelectedNode.Text + "/" + param, Client._srv.encryptIt);
        }

        private void butGetUpdate_Click(object sender, EventArgs e)
        {
            Client.Send("GetUpdate", Client._srv.encryptIt);
        }

        private void butDimmersSet_Click(object sender, EventArgs e)
        {
            Client.Send("SetParam/" + trvDevice.SelectedNode.Text + "/" + tbDimmersPower.Text, Client._srv.encryptIt);
        }

        private void SystemConf_Click(object sender, EventArgs e)
        {
            AdminPanel AdmPanel = new AdminPanel(Client, Client._srv.encryptIt);
            AdmPanel.ShowDialog();
        }

        ChangePasswordForm _ChangePassword;
        private void ChangePassword_Click(object sender, EventArgs e)
        {
            _ChangePassword = new ChangePasswordForm();
            if (_ChangePassword.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Client.Send("UpdatePassword/LOGIN/" + "/" + _ChangePassword.tbOldPassword + 
                    "/" + _ChangePassword.tbNewPassword + "/" + _ChangePassword.tbConfirmPass, 
                    Client._srv.encryptIt);
            }
        }
    }
}
