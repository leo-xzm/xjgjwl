using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POSApplication.Model;
using POSApplication.Common;
using System.Threading;
using POSApplication.BLL;
using System.IO;
using POSApplication.UserController;

namespace POSApplication.UI
{
    public partial class frmMain : Form
    {
        private Staff staff;
        private string logPath;
        private frmLogIn login;

        /// <summary>
        /// 显示标记
        /// </summary>
        int btnShowFlag;

        /// <summary>
        /// 自动隐藏
        /// </summary>
        Thread btnHideThread;

        //上传数据时的错误提示
        string message;

        /// <summary>
        /// 系统名称
        /// </summary>
        public static string SysTitle = "便捷通4.0";

        public frmMain(Staff staff, string logPath, frmLogIn login)
        {
            InitializeComponent();
            this.staff = staff;
            this.logPath = logPath;
            this.login = login;

            message = "";
            //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            //this.WindowState = FormWindowState.Maximized;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.lbWelcome.Text = SysTitle;

            timer1.Start();
            //退款权限设定，员工没权限退款



            lbJH.Text = "机号：" + ConfigHelper.GetAppConfig("JH");
            lbStaff.Text = "员工：" + staff.Staff_name;

            //确定当前的小票号
            string xphStr = ConfigHelper.GetAppConfig("XPH");
            if (string.IsNullOrWhiteSpace(xphStr))
                GlobalParams.xph = 1;
            else
            {
                //日期加小票号，#分隔：20150701#1
                string[] xphArray = xphStr.Split('#');
                string day = DateTime.Now.ToString("yyyyMMdd");

                if (day == xphArray[0])
                    GlobalParams.xph = int.Parse(xphArray[1]);
                else
                {
                    GlobalParams.xph = 1;

                    //清除单日销售明细数据
                    if (new SalesBLL().DeleteSalesDay() < 0)
                        Log.WriteErrorLog("清空单日销售明细表出错");
                }
            }
        }

        private void frmMain_VisibleChanged(object sender, EventArgs e)
        {
            btnShowFlag = 0;

            btnQuit.Hide();
            btnReboot.Hide();
            btnShutdown.Hide();

            lbJH.Text = "机号：" + ConfigHelper.GetAppConfig("JH");
        }

        //打开销售收银界面
        private void btnSell_Click(object sender, EventArgs e)
        {
            string comBHS = ConfigHelper.GetAppConfig("BHS_Com");
            string comKXP = ConfigHelper.GetAppConfig("KXP_Com");
            if (comBHS == "" || comKXP == "")
            {
                MessageBox.Show("请在系统管理界面选择扫描枪或客显串口！");
                return;
            }

            frmSell sellFrm = new frmSell(staff, this);
            sellFrm.Show();

            this.Hide();
        }

        //打开解款打印界面
        private void btnCash_Click(object sender, EventArgs e)
        {
            frmCash cashFrm = new frmCash(staff, this);
            cashFrm.Show();

            this.Hide();
        }

        //打开系统管理界面
        private void btnConfig_Click(object sender, EventArgs e)
        {
            frmConfig configFrm = new frmConfig(staff, this);
            configFrm.Show();

            this.Hide();

        }

        //打开单日数据上传界面
        private void btnUpload_Click(object sender, EventArgs e)
        {
            frmData dataFrm = new frmData(staff, this);
            dataFrm.Show();

            this.Hide();
        }

        //打开挂单恢复界面
        private void btnJob_Click(object sender, EventArgs e)
        {
            frmRecoverOrder roFrm = new frmRecoverOrder(staff, this);
            roFrm.Show();

            this.Hide();
        }

        //打开小票补打界面
        private void btnReceipt_Click(object sender, EventArgs e)
        {
            frmReceipt reFrm = new frmReceipt(staff, this);
            reFrm.Show();

            this.Hide();
        }

        public void UploadData()
        {
            if (!DataUpload.UploadSalesDay(ref message))
                MessageBox.Show("单日销售明细上传失败：" + message);

            if (!DataUpload.ForSure(ref message))
                MessageBox.Show(message);
        }

        //退出程序
        private void btnQuit_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("确定要退出程序返回Windows桌面吗？", "", MessageBoxButtons.OKCancel);
            if (r == DialogResult.OK)
            {
                UploadData();

                //写日志
                string log = "管理员：" + staff.Staff_name + " 于" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒") + "退出系统。";
                Log.Write(logPath, log);

                Application.Exit();
            }
        }

        //重启
        private void btnReboot_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("确定要重启系统吗？", "", MessageBoxButtons.OKCancel);
            if (r == DialogResult.OK)
            {
                UploadData();

                //写日志
                string log = "收银员：" + staff.Staff_name + " 于" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒") + "重启系统。";
                Log.Write(logPath, log);

                Thread.Sleep(1000);

                SysOperate.DoExitWin(1);
            }
        }

        //关机
        private void btnShutdown_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("确定要关机吗？", "", MessageBoxButtons.OKCancel);
            if (r == DialogResult.OK)
            {
                UploadData();

                //写日志
                string log = "收银员：" + staff.Staff_name + " 于" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒") + "关机。";
                Log.Write(logPath, log);

                Thread.Sleep(1000);

                SysOperate.DoExitWin(0);
            }
        }

        //注销，记录日志，回收主界面，显示登录窗口
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("确定要注销登录吗？", "", MessageBoxButtons.OKCancel);
            if (r == DialogResult.OK)
            {
                UploadData();

                //写日志
                string log = "员工：" + staff.Staff_name + " 于" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒") + "注销登录。";
                Log.Write(logPath, log);

                login.Show();
                //回收
                this.Dispose();
            }
        }

        //关闭键，切换退出、重启、关机键显示和隐藏
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (btnReboot.Visible == false)
            {
                btnReboot.Show();
                btnShutdown.Show();
            }
            else
            {
                btnReboot.Hide();
                btnShutdown.Hide();
            }

            //一般收银员不能退出软件
            if (staff.Rank_no != "10")
            {
                if (btnQuit.Visible == false)
                    btnQuit.Show();
                else
                    btnQuit.Hide();
            }

            btnHideThread = new Thread(new ParameterizedThreadStart(buttonHide));
            if (btnReboot.Visible)
            {
                //主动点击显示，开启新的子线程，标记自增
                btnShowFlag++;
                //点击显示后，启动自动隐藏子线程，传入标记
                btnHideThread.Start(btnShowFlag);

                //延时三秒隐藏
                //Action a = new Action(
                //    () =>
                //    {
                //        Thread.Sleep(3000);
                //        //一般收银员不能退出软件
                //        if (staff.Rank_no != "10")

                //            if (btnQuit.Visible == true)
                //                BeginInvoke(new btnHide(btnQuitHide));


                //        if (btnReboot.Visible == true)
                //            BeginInvoke(new btnHide(btnRebootHide));

                //        if (btnShutdown.Visible == true)
                //            BeginInvoke(new btnHide(btnShutdownHide));
                //    }
                //);

                //a.BeginInvoke(null, null);
            }
            else
                //终止并不是立即生效的，执行完才终止
                //btnHideThread.Abort();
                //主动点击隐藏的话，flag 增加 1，让之前自动隐藏的子线程不再执行自动隐藏操作
                btnShowFlag++;
        }

        /// <summary>
        /// 自动隐藏方法
        /// </summary>
        /// <param name="flag">线程标记</param>
        public void buttonHide(object flag)
        {

            int hideflag = (int)flag;

            Thread.Sleep(2000);

            //查看标记是否匹配最新的，不匹配说明已经主动点击隐藏，不再自动隐藏
            if (hideflag == btnShowFlag)
            {
                if (staff.Rank_no != "10")
                    if (btnQuit.Visible == true)
                        BeginInvoke(new btnHide(btnQuitHide));

                if (btnReboot.Visible == true)
                {
                    BeginInvoke(new btnHide(btnRebootHide));
                    BeginInvoke(new btnHide(btnShutdownHide));
                }
            }
        }

        public delegate void btnHide();
        public void btnQuitHide()
        {
            btnQuit.Hide();
        }
        public void btnRebootHide()
        {
            btnReboot.Hide();
        }
        public void btnShutdownHide()
        {
            btnShutdown.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("yyyy年MM月dd日HH点mm分ss秒");

            //每十分钟自动跟新一次数据，防止交易时没有网络
            try
            {
                int time = Convert.ToInt32(DateTime.Now.ToString("HHmmss")) % 3000;
                if (time == 0)
                {
                    DataUpload.UploadSalesDay(ref message);
                }
            }
            catch
            {

            }
        }


        //以下为增加退款功能
        private void btnRefund_Click(object sender, EventArgs e)
        {

            frmRefundPow frm = new frmRefundPow(staff, this);


            frm.Show();
            //this.Hide();
            //frmRefund frm = new frmRefund(staff,this);
            //frm.Show();
            //this.Hide();
        }




    }
}
