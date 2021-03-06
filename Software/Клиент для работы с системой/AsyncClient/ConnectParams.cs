﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsyncClient
{
    public partial class ConnectParams : Form
    {
        public ConnectParams(string Ip, string Port, bool EncryptIt)
        {
            InitializeComponent();
            this.tbIP.Text = Ip;
            this.tbPort.Text = Port;
            this.chbUseEncrypting.Checked = EncryptIt;
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
