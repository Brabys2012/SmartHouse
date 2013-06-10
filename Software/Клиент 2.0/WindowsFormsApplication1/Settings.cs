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
    public partial class Settings : Form
    {
        AsynchronousClient _serv = new AsynchronousClient();

        Params _param = new Params();

        public Settings(AsynchronousClient Server)
        {
            InitializeComponent();
            _serv = Server;
            _param = XMLLoader.getSetting();
            tbIP.Text = _param.IP;
            tbPort.Text = _param.Port.ToString();
            chbUseEncrypt.Checked = _param.IsNeedUseEncrypt;
            chbUseKeepAlive.Checked = _param.IsNeedUseKeepAlive;
            chbCTRL_ENTER.Checked = _param.SendType;
        }

        private void butChKey_Click(object sender, EventArgs e)
        {
            ChangeKey ChKey = new ChangeKey();
            if (ChKey.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _serv.Send("UpdateKey/" + ChKey.tbCurrentKey.Text
                    + "*" + ChKey.tbNewKey.Text, _serv._srv.encryptIt);
            }
        }

        private void butChPassword_Click(object sender, EventArgs e)
        {
            PasswordCh PasCh = new PasswordCh();
            if (PasCh.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _serv.Send("UpdatePassword/LOGIN/" + PasCh.tbNewPassword.Text
                    + "/" + PasCh.tbCurrentPassword.Text, _serv._srv.encryptIt);
            }
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            _serv._srv.encryptIt = chbUseEncrypt.Checked;
            _serv._srv.SendType = chbCTRL_ENTER.Checked;
            _param.IP = tbIP.Text;
            _param.Port = Convert.ToInt32(tbPort.Text);
            _param.IsNeedUseEncrypt = chbUseEncrypt.Checked;
            _param.IsNeedUseKeepAlive = chbUseKeepAlive.Checked;
            _param.IsNeedUseKeepAlive = chbUseKeepAlive.Checked;
            _param.SendType = chbCTRL_ENTER.Checked;
            XMLLoader.saveSettings(_param);
            this.Close();
        }
    }
}
