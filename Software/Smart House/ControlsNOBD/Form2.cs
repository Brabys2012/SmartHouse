using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlsNOBD
{
    public partial class AddDev : Form
    {
        string status;
        string FAdress;
        string FPort;


        public AddDev()
        {
            InitializeComponent();
            FillComboxs();
            status = "Add";
            bDel.Visible = false;
        }

        public AddDev(string Name, string Num_Port, string Adress,string TypeDev, string currVal)
        {
            InitializeComponent();
            FillComboxs();
            lAdress.Visible = false;
            lPort.Visible = false;
            cbPort.Visible = false;
            cbAdress.Visible = false;
            status = "Mod";
            tbName.Text = Name;
            cbType.Text = TypeDev;
            tbCurrentValue.Text = currVal;
            FAdress = Adress;
            FPort = Num_Port;
        }

        private void FillComboxs()
        {
            for (int i = 0 ; i<256 ; i++)
            {
                cbAdress.Items.Add(i.ToString());
                cbPort.Items.Add(i.ToString());
            }
            cbType.Items.Add("Простой датчик");
            cbType.Items.Add("Простое устройство");
            cbType.Items.Add("Диммер");
            cbType.Items.Add("Датчик с несколькими состояниями");
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void b_Ok_Click(object sender, EventArgs e)
        {
            string result;
            if (status == "Add")
            {
                result = Program.data_module.AddDevices(tbName.Text, cbPort.Text, cbAdress.Text, cbType.Text, tbCurrentValue.Text);
                if (result == "OK")
                {
                    MessageBox.Show("Устройство подключено успешно");
                    string comand = cbPort.Text.Replace(" ", "") + "." + cbAdress.Text.Replace(" ", "") + "." + 255 + "/";
                    Program.MCE.SendInform(comand);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка " + result);
                }
            }
            else if (status == "Mod") 
            {
                result = Program.data_module.UpdDevices(tbName.Text, FPort, FAdress, cbType.Text,tbCurrentValue.Text);
                if (result == "OK")
                {
                    MessageBox.Show("Устройство переподключено успешно");
                    string comand = FPort.Replace(" ", "") + "." + FAdress.Replace(" ", "") + "." + 128 + "/";
                    Program.MCE.SendInform(comand);
                    comand = FPort.Replace(" ", "") + "." + FAdress.Replace(" ", "") + "." + 255 + "/";
                    Program.MCE.SendInform(comand);
                    this.Close();
                }
            }
        }

        private void AddDev_Load(object sender, EventArgs e)
        {

        }

        private void bDel_Click(object sender, EventArgs e)
        {
            string res = Program.data_module.DeleteDevices(FPort, FAdress);
            if (res == "OK")
            {
                MessageBox.Show("Устройство успешно отключено");
                string comand = FPort.Replace(" ", "") + "." + FAdress.Replace(" ", "") + "." + 128 + "/";
                Program.MCE.SendInform(comand);
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка " + res);
            }
        }
    }
}
