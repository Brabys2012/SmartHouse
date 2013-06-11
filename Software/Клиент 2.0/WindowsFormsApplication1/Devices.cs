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
    public partial class Devices : Form
    {
        AsynchronousClient _serv = new AsynchronousClient();

        public Devices(AsynchronousClient Server)
        {
            InitializeComponent();
            _serv = Server;
            _serv.IsNeedUpdateThreeEvent += new IsNeedUpdateThreeDelegate(Client_IsNeedUpdateThreeEvent);
            _serv.Send("GetUpdate/Простое устройство", _serv._srv.encryptIt);
        }

        /// <summary>
        /// Обработка события обновления дерева.
        /// </summary>
        /// <param name="DevData"></param>
        void Client_IsNeedUpdateThreeEvent(string DevData)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new IsNeedUpdateThreeDelegate(Client_IsNeedUpdateThreeEvent), DevData);
            }
            else
            {
                if (DevData != "")
                {
                    string[] dev = DevData.Split('*');
                    this.treeDevices.BeginUpdate();
                    int index = treeDevices.Nodes.IndexOfKey(dev[1]);
                    if (index > -1)
                    {
                        treeDevices.Nodes[index].Remove();

                    }
                    treeDevices.Nodes.Add(dev[1], dev[1]);
                    index = treeDevices.Nodes.IndexOfKey(dev[1]);
                    treeDevices.Nodes[index].Tag = dev[2];
                    this.treeDevices.EndUpdate();
                }
            }
        }

        private void butAction_Click(object sender, EventArgs e)
        {
            if (treeDevices.SelectedNode != null)
            {
                _serv.Send("SetParam/" + treeDevices.SelectedNode.Text
                    + "/" + (1 - Convert.ToInt32(treeDevices.SelectedNode.Tag)).ToString(), _serv._srv.encryptIt);
                this.treeDevices.BeginUpdate();
                treeDevices.Nodes.Clear();
                this.treeDevices.EndUpdate();
                _serv.Send("GetUpdate/Устройства", _serv._srv.encryptIt);
            }
            else
            {
                MessageBox.Show("Выберите устройство из списка",
                    "Не выбрано устройство", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            this.treeDevices.BeginUpdate();
            treeDevices.Nodes.Clear();
            this.treeDevices.EndUpdate();
            _serv.Send("Update/Устройства", _serv._srv.encryptIt);

        }

        private void treeDevices_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lName.Text = treeDevices.SelectedNode.Text;
            if (Convert.ToInt32(treeDevices.SelectedNode.Tag) == 1)
                butAction.BackgroundImage = AsyncClient.Properties.Resources.ВЫКЛ;
            else
                butAction.BackgroundImage = AsyncClient.Properties.Resources.ВКЛ;
        }
    }
}
