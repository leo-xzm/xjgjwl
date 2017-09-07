using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POSApplication.Model;
using POSApplication.Common;

namespace POSApplication.UserController
{
    public partial class BottomBar : UserControl
    {
        private Staff staff;

        public BottomBar(Staff staff)
        {
            InitializeComponent();
            timer1.Start();
            this.staff = staff;

            lbJH.Text = "机号：" + ConfigHelper.GetAppConfig("JH");
            lbStaff.Text = "员工：" + staff.Staff_name;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("yyyy年MM月dd日HH点mm分ss秒");
        }
    }
}
