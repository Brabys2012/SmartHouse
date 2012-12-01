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
    public partial class mainForm : Form
    {
        private string _current_state;
        public mainForm()
        {
            InitializeComponent();
            lDatchik.Visible = false;
            bGenMess.Visible = false;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.WW.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void b_connect_to_db_Click(object sender, EventArgs e)
        {
            //Блокируем кнопку меню на момент выполнения операции
            b_connect_to_db.Enabled = false;
            //результат попытки подключения/отключения БД
            string result;
            //Если в данный момент подключение к базе данных отсутствует
            if (Program.data_module.get_connect_status() == "DISCONNECTED")
            {
                this.connect_status.Text = "Подключение к базе данных...";
                this.Update();
                result = Program.data_module.connect_to_db();
                if (!(result == "OK"))
                {
                    //Не удалось подключиться к базе данных
                    MessageBox.Show(result);
                    this.connect_status.Text = "Отключен от базы данных";
                    return;
                }
                else
                {
                    //Успешное подключение к базе данных
                    this.connect_status.Text = "Подключен к базе данных";
                    b_connect_to_db.Text = "Отключиться";
                }
            }
            else
                //Если в данный момент подключены к базе, то...
                if (Program.data_module.get_connect_status() == "CONNECTED")
                {
                    this.connect_status.Text = "Отключение от базы данных...";
                    this.Update();
                    result = Program.data_module.disconnect_db();
                    if (!(result == "OK"))
                    {
                        //Не удалось отлкючиться от БД
                        MessageBox.Show(result);
                        this.connect_status.Text = "Подключен к базе данных";
                        return;
                    }
                    else
                    {
                        //Успешное отключение от БД
                        this.connect_status.Text = "Отключен от базы данных";
                        b_connect_to_db.Text = "Подключиться";
                    }

                }

            b_connect_to_db.Enabled = true;
            this.Update();

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            db_settings db_set = new db_settings();
            db_set.ShowDialog();
            db_set.Dispose();
        }
        private void load_data_table(string state)
        {
            gw.Hide();
            bs.DataSource = Program.data_module.get_data_table(state).Tables["Devices"];
            gw.DataSource = bs;
            gw.Update();
            gw.Show();
            this._current_state = state;
        }

        private void b_SimpleDatchik_Click(object sender, EventArgs e)
        {
            this.load_data_table("Простой датчик");
            lDatchik.Visible = true;
            bGenMess.Visible = true;
        }

        private void bUstr_Click(object sender, EventArgs e)
        {
            this.load_data_table("Простое устройство");
            lDatchik.Visible = false;
            bGenMess.Visible = false;
        }

        private void tExDatcik_Click(object sender, EventArgs e)
        {
            this.load_data_table("Датчик с несколькими состояниями");
            lDatchik.Visible = false;
            bGenMess.Visible = false;
        }

        private void bDimmer_Click(object sender, EventArgs e)
        {
            this.load_data_table("Диммер");
            lDatchik.Visible = false;
            bGenMess.Visible = false;
        }

        private void b_commit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.gw.EndEdit();
                Program.data_module.apply_changes(this._current_state);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            AddDev MyForm = new AddDev();
            MyForm.ShowDialog();
        }

        private void bGenMess_Click(object sender, EventArgs e)
        {
            string Port = gw.Rows[gw.CurrentRow.Index].Cells[1].Value.ToString();
            string Adress =gw.Rows[gw.CurrentRow.Index].Cells[2].Value.ToString();
            string Name = gw.Rows[gw.CurrentRow.Index].Cells[0].Value.ToString();
            byte[] comand = new byte[3];
            comand[0] = Convert.ToByte(Port);
            comand[1] = Convert.ToByte(Adress);
            comand[2] = 100;
            Program.MCE.SendInform(comand);
            Program.data_module.UpdDevices(Name, Port, Adress, "Простой датчик", "1");
        }

        private void bMod_Click(object sender, EventArgs e)
        {
            AddDev MyForm = new AddDev(gw.Rows[gw.CurrentRow.Index].Cells[0].Value.ToString(), gw.Rows[gw.CurrentRow.Index].Cells[1].Value.ToString(), gw.Rows[gw.CurrentRow.Index].Cells[2].Value.ToString(),this._current_state, gw.Rows[gw.CurrentRow.Index].Cells[3].Value.ToString());
            MyForm.ShowDialog();
        }

        private void mainForm_Activated(object sender, EventArgs e)
        {
            load_data_table(_current_state);
        }

    }
}
