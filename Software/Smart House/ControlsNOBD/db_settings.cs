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
    public partial class db_settings : Form
    {
        public db_settings()
        {
            InitializeComponent();

            this.t_server.Text = Program.user_set._server;
            this.t_schema.Text = Program.user_set._schema;
            this.t_user.Text = Program.user_set._login;
            this.t_pass.Text = Program.user_set._password;
            this.cb_psec.Checked = Program.user_set._security;

            this.cb_win_auth.Checked = Program.user_set._win_auth;
        }

        private void cb_user_con_string_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cb_win_auth.Checked)
            {
                this.t_pass.Enabled = false;
                this.t_user.Enabled = false;
                this.cb_psec.Enabled = false;
            }
            else
            {
                this.t_pass.Enabled = true;
                this.t_user.Enabled = true;
                this.cb_psec.Enabled = true;
            }
        }

        private void b_apply_Click(object sender, EventArgs e)
        {
            if (!(Program.user_set.setDBSettings(this.t_server.Text, this.t_schema.Text, this.t_user.Text, this.t_pass.Text, this.cb_psec.Checked,
                                                this.cb_win_auth.Checked)))
            {
                MessageBox.Show("Параметры указаны неверно!\nУкажите корректные данные для подключения.");
                return;
            }
            this.Close();

        }

        private void b_abolition_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void db_settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.user_set.saveSettingsToFile();
        }

        private void db_settings_Shown(object sender, EventArgs e)
        {
            

        }
    }
}