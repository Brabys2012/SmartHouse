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
    public partial class DataForReport : Form
    {
        //Построить график начиная с:
        public string BegDate;
        //и по:
        public string EndDate;

        public DataForReport()
        {
            InitializeComponent();
            //Значения по умолчанию.
            this.rdbOneDay.Checked = true;
            this.dateBegin.Enabled = true;
            this.dateEnd.Enabled = false;
            this.lEnd.Enabled = false;
            this.lBeg.Text = "за:";
            this.lEnd.Text = "";

        }

        private void rdbOneDay_Click(object sender, EventArgs e)
        {
            if (this.rdbOneDay.Checked)
            {
                this.dateBegin.Enabled = true;
                this.dateEnd.Enabled = false;
                this.lEnd.Enabled = false;
                this.lBeg.Text = "за:";
                this.lEnd.Text = "";
            }
            else
            {
                this.dateBegin.Enabled = true;
                this.dateEnd.Enabled = true;
                this.lEnd.Enabled = true;
                this.lBeg.Text = "c:";
                this.lEnd.Text = "по:";
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void butBuild_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            if (this.rdbOneDay.Checked)
            {
                BegDate = this.dateBegin.Value.ToString("dd.MM.yyyy");
                EndDate = "";
            }
            else
            {
                BegDate = this.dateBegin.Value.ToString("dd.MM.yyyy");
                EndDate = this.dateEnd.Value.ToString("dd.MM.yyyy");
            }

            this.Close();
        }
    }
}
