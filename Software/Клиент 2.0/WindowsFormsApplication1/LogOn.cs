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
    public partial class LogOn : Form
    {
        public LogOn()
        {
            InitializeComponent();
        }

        private void butLogIn_Click(object sender, EventArgs e)
        {
            if (tbLogin.Text == "" && tbPassword.Text == "")
            {
                MessageBox.Show("Не заполнены обязательные поля", 
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
