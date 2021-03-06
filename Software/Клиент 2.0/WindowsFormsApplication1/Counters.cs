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
    public partial class Counters : Form
    {
        AsynchronousClient _serv = new AsynchronousClient();
 
        public Counters(AsynchronousClient Server)
        {
            InitializeComponent();
            _serv = Server;
            _serv.IsNeedUpdateThreeEvent += new IsNeedUpdateThreeDelegate(Client_IsNeedUpdateThreeEvent);
            _serv.Send("GetUpdate/Счётчики", _serv._srv.encryptIt);
        }

        /// <summary>
        /// Обработка события обновления дерева.
        /// </summary>
        /// <param name="DevData"></param>
        void Client_IsNeedUpdateThreeEvent(string DevData)
        {
            int index = 0;
            if (this.InvokeRequired)
            {
                this.Invoke(new IsNeedUpdateThreeDelegate(Client_IsNeedUpdateThreeEvent), DevData);
            }
            else
            {
                if (DevData != "")
                {
                    string[] dev = DevData.Split('*');
                    this.treeCounters.BeginUpdate();
                    this.treeCounters.Nodes.Clear();
                    treeCounters.Nodes.Add(dev[1], dev[1]);
                    index = treeCounters.Nodes.IndexOfKey(dev[1]);
                    treeCounters.Nodes[index].Tag = dev[2];
                    this.treeCounters.EndUpdate();
                }
            }
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            this.treeCounters.BeginUpdate();
            treeCounters.Nodes.Clear();
            this.treeCounters.EndUpdate();
            _serv.Send("GetUpdate/Счётчики", _serv._srv.encryptIt);
        }

        private void treeCounters_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lValue.Text = treeCounters.SelectedNode.Tag.ToString();
            lName.Text = treeCounters.SelectedNode.Text;

        }

        /// <summary>
        /// Обработка нажатия на кнопку "Построить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butPlot_Click(object sender, EventArgs e)
        {
            //TODO Доделать построение графиков.

            //ReportForm _ReportDataForm = new ReportForm();
            //if (_ReportDataForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{

            //    Client.Send("GetCounterRec/" + _ReportDataForm.BegDate + "/" +
            //        _ReportDataForm.EndDate + "/" + this.trvDevice.SelectedNode.Text, Client._srv.encryptIt);
            //}
        }

        private void Counters_FormClosed(object sender, FormClosedEventArgs e)
        {
            _serv.IsNeedUpdateThreeEvent += new IsNeedUpdateThreeDelegate(Client_IsNeedUpdateThreeEvent);
        }
    }
}
