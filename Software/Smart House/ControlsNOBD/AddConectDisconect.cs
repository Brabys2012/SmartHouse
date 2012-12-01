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
            tbCurrentValue.Visible = false;
            lCurVal.Visible = false;
            cbState.Visible = true;
            tbCurrentValue.Text = "0";
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
            tbCurrentValue.Visible = false;
            lCurVal.Visible = false;
            cbState.Visible = true;
            tbCurrentValue.Text = "0";
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
            cbType.Text = "Простое устройство";
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
                    try 
                    {
                       byte[] comand = new byte[3];
                       comand[0] = Convert.ToByte(cbPort.Text);
                       comand[1] = Convert.ToByte(cbAdress.Text);
                       comand[2] = 255;
                       Program.MCE.SendInform(comand);
                       this.Close();
                       MessageBox.Show("Устройство подключено успешно");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка:" + ex);
                    }
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
                    try
                    {
                        MessageBox.Show("Устройство переподключено успешно");
                        byte[] comand =new byte[3];
                        comand[0] = Convert.ToByte(FPort);
                        comand[1] = Convert.ToByte(FAdress);
                        comand[2] = 128;
                        Program.MCE.SendInform(comand);
                        comand[2] = 255;
                        Program.MCE.SendInform(comand);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex);
                    }
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
                try
                {
                    MessageBox.Show("Устройство успешно отключено");
                    byte[] comand = new byte[3];
                    comand[0] = Convert.ToByte(FPort);
                    comand[1] = Convert.ToByte(FAdress);
                    comand[2] = 128;
                    Program.MCE.SendInform(comand);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex);
                }
            }
            else
            {
                MessageBox.Show("Ошибка " + res);
            }
        }

        private void cbState_CheckedChanged(object sender, EventArgs e)
        {
            if (cbState.Checked)
            {
                tbCurrentValue.Text = "1";
            }
            else
            {
                tbCurrentValue.Text = "0";
            }
        }

        private void cbType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((cbType.Text == "Простой датчик") || (cbType.Text == "Простое устройство"))
            {
                tbCurrentValue.Visible = false;
                lCurVal.Visible = false;
                cbState.Visible = true;
            }
            else
            {
                tbCurrentValue.Visible = true;
                lCurVal.Visible = true;
                cbState.Visible = false;
            }
        }
    }
}
