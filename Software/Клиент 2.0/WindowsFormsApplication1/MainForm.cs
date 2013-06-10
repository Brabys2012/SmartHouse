using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsyncClient
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Экземпляр для работы с сервером.
        /// </summary>
        AsynchronousClient Client = new AsynchronousClient();

        Params parameters = new Params();

        public MainForm()
        {
            InitializeComponent();

            lStatusValue.Text = "Отключён";
            butAdmin.Enabled = false;
            butChat.Enabled = false;
            butCounters.Enabled = false;
            butDevices.Enabled = false;
            butDimmers.Enabled = false;
            butSensors.Enabled = false;

            lInTemp.Text = "--°C";
            lOutTemp.Text = "--°C";

            parameters = XMLLoader.getSetting();
            Client.StartClient(parameters.IP, parameters.Port, parameters.IsNeedUseKeepAlive);
            Client._srv.encryptIt = parameters.IsNeedUseEncrypt;

            //Подписываемя на события логической части лиента.
            Client.IsNeedShowLoginFormEvent += new IsNeedShowDelegate(Client_IsNeedShowLoginFormEvent);
            Client.IsNeedToPlotEvent += new IsNeedToPlotDelegate(Client_IsNeedToPlotEvent);
            Client.IsNeedChangeStatusEvent += new IsNeedChangeStatus(Client_IsNeedChangeStatusEvent);
            Client.IsNeedToChangeConfStatusEvent += new IsNeedToChangeConfStatus(Client_IsNeedToChangeConfStatusEvent);
            Client.IsNeedShowOperationResultEvent += new IsNeedShowOperationResult(Client_IsNeedShowOperationResultEvent);
        }

        /// <summary>
        /// Обработка события необходимотси отобразить результат операции
        /// </summary>
        /// <param name="result"></param>
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
                    butAdmin.Visible = true;
                else
                    butAdmin.Visible = false;
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
                    lStatusValue.Text = "Отключен";
                    butConDisc.Text = "Подключиться";
                    butAdmin.Enabled = false;
                    butChat.Enabled = false;
                    butCounters.Enabled = false;
                    butDevices.Enabled = false;
                    butDimmers.Enabled = false;
                    butSensors.Enabled = false;
                }
                else
                {
                    Client._srv.status = true;
                    lStatusValue.Text = "Подключен";
                    butConDisc.Text = "Отключится";
                    butAdmin.Enabled = true;
                    butChat.Enabled = true;
                    butCounters.Enabled = true;
                    butDevices.Enabled = true;
                    butDimmers.Enabled = true;
                    butSensors.Enabled = true;
                }
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
                this.Invoke(new IsNeedToPlotDelegate(Client_IsNeedToPlotEvent), reportData);
            }
            else
            {
                rptForm = new ReportForm(reportData);
                rptForm.ShowDialog();
            }
        }

        /// <summary>
        /// Обработка события необходимости отобразить форму заполнения логина и пароля.
        /// </summary>
        LogOn _LOn;
        void Client_IsNeedShowLoginFormEvent()
        {
            if (this.InvokeRequired)
                this.Invoke(new IsNeedShowDelegate(Client_IsNeedShowLoginFormEvent));
            else
            {
                _LOn = new LogOn();
                if (_LOn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Client.Send(_LOn.tbLogin.Text + "." + _LOn.tbPassword.Text, Client._srv.encryptIt);
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
            if (lStatusValue.Text == "Подключён")
            {
                Client.Send("EXIT", Client._srv.encryptIt);
                Client.CloseConnection();
            }
        }

        /// <summary>
        /// Обработка кнопки "Подключится/Отключится" контексного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butConDisc_Click(object sender, EventArgs e)
        {
            //проверка текущего подключения
            if (butConDisc.Text == "Подключиться")
            {
                
                Client.StartClient(parameters.IP, parameters.Port, parameters.IsNeedUseKeepAlive);
                Client._srv.encryptIt = parameters.IsNeedUseEncrypt;

            }
            else
            {
                Client.CloseConnection();
            }
        }

        private void butSettings_Click(object sender, EventArgs e)
        {
            Settings Set = new Settings(Client);
            Set.ShowDialog();
        }

        private void butDevices_Click(object sender, EventArgs e)
        {
            Devices Dev = new Devices(Client);
            Dev.Show();
        }

        private void butCounters_Click(object sender, EventArgs e)
        {
            Counters Count = new Counters(Client);
            Count.Show();
        }

        private void butChat_Click(object sender, EventArgs e)
        {
            Chat Ch = new Chat(Client, Client._srv.SendType);
            Ch.Show();
        }

        private void butDimmers_Click(object sender, EventArgs e)
        {
            Dimmers Dimm = new Dimmers(Client);
            Dimm.Show();
        }

        private void butSensors_Click(object sender, EventArgs e)
        {
            Sensors Sens = new Sensors(Client);
            Sens.Show();
        }

        private void butAdmin_Click(object sender, EventArgs e)
        {
            AdminPanel AdmPanel = new AdminPanel(Client, parameters.IsNeedUseEncrypt);
            AdmPanel.ShowDialog();
        }

    }
}
