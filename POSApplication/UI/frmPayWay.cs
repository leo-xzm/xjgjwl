using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POSApplication.UI
{
    public partial class frmPayWay : Form
    {
        private frmSell sell;
        public frmPayWay(frmSell frm)
        {
            InitializeComponent();
            sell = frm;
            lbMName.Text = "持卡人：" + sell.Mname;
            lbBalance.Text = "余  额：" + sell.Balance;
        }

        private void btnCard_Click(object sender, EventArgs e)
        {
            sell.isCash = false;
            this.Close();

        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            sell.isCash = true;
            this.Close();
        }
    }
}
