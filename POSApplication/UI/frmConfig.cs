using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POSApplication.Model;
using System.IO.Ports;
using POSApplication.Common;
using System.IO;

namespace POSApplication.UI
{
    public partial class frmConfig : Form
    {
        /// <summary>
        /// 收款员
        /// </summary>
        private Staff staff;

        /// <summary>
        /// 主界面
        /// </summary>
        private frmMain main;

        public frmConfig(Staff staff, frmMain main)
        {
            InitializeComponent();

            this.staff = staff;
            this.main = main;
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lbStaff.Text = "员工：" + staff.Staff_name;

            //取所有可用串口

            //todo 临时取消扫描枪
            string[] portNames = SerialPort.GetPortNames();

            ////如果没有串口，加上虚拟的
            //if (portNames.Length < 1)
            portNames = new string[] { "COM1", "COM2", "COM3", "COM4", "COM5", "COM6" };

            for (int i = 0; i < portNames.Length; i++)
            {
                // cbbCom.Items.Add(portNames[i]);
                cbKCom.Items.Add(portNames[i]);
            }

            cbbCom.Items.Add("COM1");

            string com = ConfigHelper.GetAppConfig("BHS_Com");
            string comkx = ConfigHelper.GetAppConfig("KXP_Com");
            string mdh = ConfigHelper.GetAppConfig("MDH");
            string jh = ConfigHelper.GetAppConfig("JH");


            if (com != "")
                cbbCom.SelectedItem = com;
            if (comkx != "")
                cbKCom.SelectedItem = comkx;
            if (mdh != "")
                txtMDH.Text = mdh;
            if (jh != "")
                txtJH.Text = jh;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                string com = cbbCom.SelectedItem.ToString();
                string comkx = cbKCom.SelectedItem.ToString();

                ConfigHelper.UpdateAppConfig("MDH", txtMDH.Text);
                ConfigHelper.UpdateAppConfig("JH", txtJH.Text);
                ConfigHelper.UpdateAppConfig("BHS_Com", com);
                ConfigHelper.UpdateAppConfig("KXP_Com", comkx);

                main.Show();
                this.Dispose();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                main.Show();
                this.Dispose();
            }
        }

        private bool Check()
        {
            bool result = true;

            //设置店号
            if (string.IsNullOrWhiteSpace(txtMDH.Text))
            {
                MessageBox.Show("请填写门店号");
                result = false;
            }
            else if (txtMDH.Text.Length != 5)
            {
                MessageBox.Show("门店号格式不正确，请填写5位，例如 00001");
                result = false;
            }

            //设置POS机号
            if (string.IsNullOrWhiteSpace(txtJH.Text))
            {
                MessageBox.Show("请填写POS机号");
                result = false;
            }
            else if (txtJH.Text.Length != 5)
            {
                MessageBox.Show("POS机号格式不正确，请填写5位，例如 00001");
                result = false;
            }

            //设置扫描枪串口
            if (cbbCom.SelectedItem == null)
            {
                MessageBox.Show("请选择扫描枪串口");
                result = false;
            }

            //设置客显牌串口
            if (cbKCom.SelectedItem == null)
            {
                MessageBox.Show("请选择客显牌串口");
                result = false;
            }

            return result;
        }

        //清除缓存
        private void btnClearTempOrder_Click(object sender, EventArgs e)
        {
            File.Delete(GlobalParams.orderPath);
            MessageBox.Show("缓存已清空！");
        }

        //设置开机启动
        private void btnBoot_Click(object sender, EventArgs e)
        {
            if (SysOperate.runWhenStart(true, "POS", System.Windows.Forms.Application.StartupPath + "\\POSApplication.exe"))
                MessageBox.Show("设置开机启动成功");
            else
                MessageBox.Show("设置开机启动失败");

        }

        private void btnUnBoot_Click(object sender, EventArgs e)
        {
            if (SysOperate.runWhenStart(false, "POS", System.Windows.Forms.Application.StartupPath + "\\POSApplication.exe"))
                MessageBox.Show("取消开机启动成功");
            else
                MessageBox.Show("取消开机启动失败");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("yyyy年MM月dd日HH点mm分ss秒");
        }
    }
}
