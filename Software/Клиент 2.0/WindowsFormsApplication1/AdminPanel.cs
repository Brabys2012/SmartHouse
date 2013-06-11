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
    public partial class AdminPanel : Form
    {
        AsynchronousClient AdminClient;
        bool NeedToEncypt;

        public AdminPanel(AsynchronousClient Client, bool NeedEncrypt)
        {
            InitializeComponent();

            for (int i = 0; i <= 255; i++)
            {
                cbPortNumber.Items.Add(i);
            }

            for (int i = 0; i <= 255; i++)
            {
                cbDeviceNumber.Items.Add(i);
            }

            cbDeviceType.Items.Add("Датчики");
            cbDeviceType.Items.Add("Простое устройство");
            cbDeviceType.Items.Add("Диммеры");
            cbDeviceType.Items.Add("Счётчики");

            cbRole.Items.Add("user");
            cbRole.Items.Add("admin");

            chbLinkWith.Checked = false;
            tbNameLinkedDevice.Enabled = false;

            AdminClient = Client;
            NeedToEncypt = NeedEncrypt;
        }

        private void butExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buеCancel_Click(object sender, EventArgs e)
        {
            tbLogin.Text = "";
            tbPassConfirm.Text = "";
            tbPassword.Text = "";
            cbRole.SelectedIndex = -1;
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            if ((tbLogin.Text != "") && (tbPassword.Text != "") &&
                (tbPassConfirm.Text != "") && (cbRole.SelectedItem.ToString() != ""))
            {
                if (tbPassword.Text == tbPassConfirm.Text)
                {
                    AdminClient.Send("AddUser/" + tbLogin.Text + "/" +
                        tbPassword.Text + "/" + cbRole.SelectedItem.ToString(), NeedToEncypt);
                    MessageBox.Show("Сообщение о добавлении нового\r\n пользоветеля успешно отправлено.",
                        "Добавление пользователя", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Пароль и подтверждение пароля не совпадают!",
                    "Добавление пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Не заполнены обязательные поля!",
                   "Добавление пользователя", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void butDelUser_Click(object sender, EventArgs e)
        {
            if (tbLoginToDel.Text != "")
            {
                AdminClient.Send("DeleteUser/" + tbLoginToDel.Text, NeedToEncypt);
            }
            else
                MessageBox.Show("Введите логин удаляемого пользователя!");
        }

        private void butDelDevice_Click(object sender, EventArgs e)
        {
            if (tbNameToDel.Text != "")
            {
                AdminClient.Send("DeleteDevice/" + tbNameToDel.Text, NeedToEncypt);
            }
            else
                MessageBox.Show("Введите наименование удаляемого устройства!",
                   "Удаление устройства", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void butAddDevice_Click(object sender, EventArgs e)
        {
            string temp = "";

            if (cbDeviceNumber.SelectedItem.ToString() != "" &&
                cbPortNumber.SelectedItem.ToString() != "" &&
                cbDeviceType.SelectedItem.ToString() != "" &&
                tbDeviceName.Text != "")
            {

                if (chbLinkWith.Checked)
                    temp = tbNameLinkedDevice.Text;
                AdminClient.Send("AddDev/" + cbPortNumber.SelectedItem.ToString() + "/" +
                    cbDeviceNumber.SelectedItem.ToString() +
                    "/" + tbDeviceName.Text +
                    "/" + cbDeviceType.SelectedItem.ToString() +
                    "/" + tbMessage.Text +
                    "/" + temp, NeedToEncypt);

                cbDeviceNumber.SelectedIndex = -1;
                cbDeviceType.SelectedIndex = -1;
                cbPortNumber.SelectedIndex = -1;

                tbDeviceName.Text = "";
                tbMessage.Text = "";
                if (chbLinkWith.Checked)
                    tbNameLinkedDevice.Text = "";

                MessageBox.Show("Сообщение о добавлении\r\n устройства успешно отправлено." +
                    "\r\n Проверьте список устройств.", "Добавление устройства",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Заполнены не все  данные\r\n для добавления устройства",
                    "Добавление устройства", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }


        }

        private void chbLinkWith_CheckedChanged(object sender, EventArgs e)
        {
            if (tbNameLinkedDevice.Enabled)
                tbNameLinkedDevice.Enabled = false;
            else
                tbNameLinkedDevice.Enabled = true;
        }
    }
}
