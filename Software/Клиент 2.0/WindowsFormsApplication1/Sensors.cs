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
    public partial class Sensors : Form
    {
        AsynchronousClient _serv = new AsynchronousClient();

        public Sensors(AsynchronousClient Server)
        {
            InitializeComponent();
            _serv = Server;
            _serv.IsNeedUpdateThreeEvent += new IsNeedUpdateThreeDelegate(Client_IsNeedUpdateThreeEvent);
            _serv.Send(@"Update/Датчики", Server._srv.encryptIt);
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
                this.treeSensors.BeginUpdate();
                int index = treeSensors.Nodes.IndexOfKey(dev[1]);
                if (index > -1)
                {
                    treeSensors.Nodes[index].Remove();

                }
                treeSensors.Nodes.Add(dev[1], dev[1]);
                index = treeSensors.Nodes.IndexOfKey(dev[1]);
                treeSensors.Nodes[index].Tag = dev[2];
                this.treeSensors.EndUpdate();
            }
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            this.treeSensors.BeginUpdate();
            treeSensors.Nodes.Clear();
            this.treeSensors.EndUpdate();
            _serv.Send(@"Update/Даттчики", _serv._srv.encryptIt);
        }

        private void treeSensors_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lName.Text = treeSensors.SelectedNode.Text;
            lValue.Text = treeSensors.SelectedNode.Tag.ToString();
        }
    }
}
