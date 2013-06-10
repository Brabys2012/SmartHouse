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
    public partial class PasswordCh : Form
    {
        public PasswordCh()
        {
            InitializeComponent();
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            if (tbNewPassword.Text != tbConfirmPassword.Text)
            {
                MessageBox.Show("Пароль и подтверждение пароля не совпадают",
                    "Предкпреждение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

    }
}
