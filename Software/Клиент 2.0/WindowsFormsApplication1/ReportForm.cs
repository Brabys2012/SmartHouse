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
    public partial class ReportForm : Form
    {
        public ReportForm(string data)
        {
            InitializeComponent();
            string[] tmpString;
            string[] tmp = data.Split('*');
            for (int i = 0; i < tmp.Length; i++)
            {
                tmpString = tmp[i].Split(',');
                cht.Series[0].Points.AddXY(tmpString[0], tmpString[1]);
            }
        }
    }
}
