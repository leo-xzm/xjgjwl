using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using POSApplication.Model;
using POSApplication.Common;

namespace POSApplication.UI
{
    public partial class frmData : Form
    {
        /// <summary>
        /// 收款员
        /// </summary>
        private Staff staff;

        /// <summary>
        /// 主界面
        /// </summary>
        private frmMain main;

        public string message;

        public frmData(Staff staff, frmMain main)
        {
            InitializeComponent();

            this.staff = staff;
            this.main = main;
        }

        private void frmData_Load(object sender, EventArgs e)
        {
            timer1.Start();

            lbJH.Text = "机号：" + ConfigHelper.GetAppConfig("JH");
            lbStaff.Text = "员工：" + staff.Staff_name;

            dtpSalesDay.Value = DateTime.Now;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            main.Show();
            this.Dispose();
        }

        //上传指定日期数据
        private void btnUpload_Click(object sender, EventArgs e)
        {
            string date = dtpSalesDay.Value.ToString("yyyyMMdd");

            if (DataUpload.UploadSalesDay(ref message, date))
                MessageBox.Show(date + " 单日销售明细上传成功！");
            else
                MessageBox.Show(date + " 单日销售明细上传失败：" + message);

            if (!DataUpload.ForSure(ref message))
                MessageBox.Show(message);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("yyyy年MM月dd日HH点mm分ss秒");
        }
    }
}
