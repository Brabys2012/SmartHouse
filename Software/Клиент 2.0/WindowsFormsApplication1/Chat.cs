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
    public partial class Chat : Form
    {
        AsynchronousClient _serv = new AsynchronousClient(); 
        bool _sendType;
        public Chat(AsynchronousClient Server, bool SendType, OpenWindow _OpW)
        {
            InitializeComponent();
            _serv = Server;
            _sendType = SendType;
            _serv.IsNeedUpdateThreeEvent += new IsNeedUpdateThreeDelegate(Client_IsNeedUpdateThreeEvent);
            _serv.IsNeedShowChatMessageEvent += new IsNeedShowChatMessage(_serv_IsNeedShowChatMessageEvent);
            _serv.Send("GetOnLineClients", _serv._srv.encryptIt);
        }

        void _serv_IsNeedShowChatMessageEvent(string message)
        {
            tbChat.Text = message + "\r\n";
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
                this.treeUsers.BeginUpdate();
                for (int i = 0; i < dev.Length; i++)
                {
                    int index = treeUsers.Nodes.IndexOfKey(dev[i]);
                    if (index > -1)
                    {
                        treeUsers.Nodes[index].Remove();

                    }
                    treeUsers.Nodes.Add(dev[i], dev[i]); 
                }
                this.treeUsers.EndUpdate();
            }
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            this.treeUsers.BeginUpdate();
            treeUsers.Nodes.Clear();
            this.treeUsers.EndUpdate();
            _serv.Send("GetOnLineClients", _serv._srv.encryptIt);
        }

        private void butSend_Click(object sender, EventArgs e)
        {
            _serv.Send("Chat/" + tbMessage.Text, _serv._srv.encryptIt);
            tbChat.Text += "Я - " + tbMessage.Text + "\r\n";
            tbMessage.Text = "";
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (_sendType)
            {
                if (e.KeyValue == 13 && e.Control)
                {
                    _serv.Send("Chat/" + tbMessage.Text, _serv._srv.encryptIt);
                    tbChat.Text += "Я - " + tbMessage.Text + "\r\n";
                    tbMessage.Text = "";
                }
            }
            else
            {
                if (e.KeyValue == 13)
                {
                    _serv.Send("Chat/" + tbMessage.Text, _serv._srv.encryptIt);
                    tbChat.Text += "Я - " + tbMessage.Text + "\r\n";
                    tbMessage.Text = "";
                }
            }
        }

        private void Chat_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
