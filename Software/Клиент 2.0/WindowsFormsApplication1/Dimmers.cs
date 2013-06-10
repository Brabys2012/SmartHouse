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
    public partial class Dimmers : Form
    {
        AsynchronousClient _serv = new AsynchronousClient();

        public Dimmers(AsynchronousClient Server)
        {
            InitializeComponent();
            _serv = Server;
            _serv.IsNeedUpdateThreeEvent += new IsNeedUpdateThreeDelegate(Client_IsNeedUpdateThreeEvent);
            _serv.Send("Update/Диммеры", _serv._srv.encryptIt);

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
                string[] dev = DevData.Split('*');
                this.treeDimmers.BeginUpdate();
                int index = treeDimmers.Nodes.IndexOfKey(dev[1]);
                if (index > -1)
                {
                    treeDimmers.Nodes[index].Remove();

                }
                treeDimmers.Nodes.Add(dev[1], dev[1]);
                index = treeDimmers.Nodes.IndexOfKey(dev[1]);
                treeDimmers.Nodes[index].Tag = dev[2];
                this.treeDimmers.EndUpdate();
            }
        }

        private void trackValue_Scroll(object sender, EventArgs e)
        {
            tbValue.Text = trackValue.Value.ToString();
        }

        private void treeDimmers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lValue.Text =  treeDimmers.SelectedNode.Tag.ToString();
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            this.treeDimmers.BeginUpdate();
            treeDimmers.Nodes.Clear();
            this.treeDimmers.EndUpdate();
            _serv.Send("Update/Диммеры", _serv._srv.encryptIt);
        }

        private void butSet_Click(object sender, EventArgs e)
        {
            if (treeDimmers.SelectedNode != null)
            {
                _serv.Send("SetParam/" + treeDimmers.SelectedNode.Text
                    + "/" + trackValue.Value, _serv._srv.encryptIt);
            }
            else 
            {
                MessageBox.Show("Выберите диммер из списка",
                    "Не выбран диммер", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); 
            }
        }
    }
}
