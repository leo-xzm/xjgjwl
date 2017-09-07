using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POSApplication.Model;
using POSApplication.BLL;
using System.IO;
using System.Threading;
using POSApplication.Common;

namespace POSApplication.UI
{
    public partial class frmLogIn : Form
    {
        //员工
        private Staff staff = new Staff();

        //登录日志路径
        private string logPath
        {
            get
            {
                //判断默认Log文件夹是否存在，不存在就创建
                DirectoryInfo logDirectory = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\Log\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();
                return System.Windows.Forms.Application.StartupPath + "\\Log\\" + string.Format("登录_{0}.log", DateTime.Now.ToString("yyy-MM-dd"));
            }
        }

        //存放小键盘所按下的键值
        private string keyInput;

        //小键盘的位置
        private Point IDLocation = new Point(243, 244);
        private Point PwdLocation = new Point(243, 325);

        private void MiniKeyboardHandler(object sender, MiniKeyboardArgs e)
        {
            keyInput = e.KeyCode;

            //根据小键盘当前位置，绑定小键盘要输出的输入框
            //输入编号
            if (miniKeyboard.Location == IDLocation)
                KeyboardInput(txtStaffID, txtStaffPwd, PwdLocation);
            //输入密码
            else if (miniKeyboard.Location == PwdLocation)
                KeyboardInput(txtStaffPwd, txtStaffID, IDLocation);
        }

        public frmLogIn()
        {
            InitializeComponent();
            //绑定小键盘事件
            miniKeyboard.Press += MiniKeyboardHandler;

            //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            //this.WindowState = FormWindowState.Maximized;
        }

        private void frmLogIn_Load(object sender, EventArgs e)
        {
            this.lbWelcome.Text = frmMain.SysTitle;
            //测试号
            //txtStaffID.Text = "0006";
            //txtStaffPwd.Text = "6008";
        }

        //登录，成功写日志，打开主界面
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            miniKeyboard.Visible = false;

            if (!InputValidate())
                return;

            //新建等待提示标签
            Label lb = new Label();
            lb.Location = new Point(260, 480);
            lb.Font = new Font("宋体", 25F);
            lb.ForeColor = Color.White;
            lb.BackColor = Color.FromName("SteelBlue");
            lb.Size = new Size(304, 34);
            lb.Text = "加载中，请等待...";
            this.Controls.Add(lb);
            lb.Refresh();

            btnClear.Visible = false;
            btnLogIn.Visible = false;

            //读取数据放到子线程里
            Thread thr = new Thread(GetData);
            thr.Start();
            thr.Join();
            ////测试数据
            //Staff staff = new Staff() { 
            //Staff_no="0001",
            //Staff_name="测试",
            //password="0001",
            //Rank_no="10"
            //};
            if (staff != null)
            {
                if (txtStaffPwd.Text != staff.password)
                {
                    lb.Dispose();
                    btnClear.Visible = true;
                    btnLogIn.Visible = true;
                    MessageBox.Show("对不起，密码不正确，请重新输入");
                    TxtPwdFocus();
                    return;
                }

                //写日志
                string log = "员工：" + staff.Staff_name + " 于" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒") + "登录系统。";
                Log.Write(logPath, log);

                //登录成功打开主界面
                frmMain frmMain = new frmMain(staff, logPath, this);
                frmMain.Show();
                this.Hide();
                btnClear.Visible = true;
                btnLogIn.Visible = true;

                lb.Dispose();
                this.miniKeyboard.Hide();
                txtStaffID.Text = "";
                txtStaffPwd.Text = "";
            }
            else
            {
                lb.Dispose();
                btnClear.Visible = true;
                btnLogIn.Visible = true;
                MessageBox.Show("对不起，工号不正确，请重新输入");
                TxtIDFocus();
            }

            if (thr.IsAlive)
                thr.Abort();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        private void GetData()
        {
            Thread.Sleep(1000);

            #region 更新主档信息
            //商品主档和商品变化主档同时存在时，先读取商品主档，再读取变化主档
            if (File.Exists(GlobalParams.spmxServerPath))
            {
                int ret;
                ret = new GoodsBLL().DeleteGoods();

                if (ret >= 0)
                    DataOperator.UpdateData(GlobalParams.spmxServerPath, GlobalParams.spmxLocalPath, "goods_spmx", false);
                else
                {
                    MessageBox.Show("商品表更新异常，请重新登录");
                    Log.WriteErrorLog("清空商品表出现异常，中断更新");
                }
            }
            DataOperator.UpdateSpit();

            if (File.Exists(GlobalParams.ygkServerPath))
            {
                int ret;
                ret = new StaffBLL().DeleteStaff();

                if (ret >= 0)
                    DataOperator.UpdateData(GlobalParams.ygkServerPath, GlobalParams.ygkLocalPath, "staff_ygk", false);
                else
                {
                    MessageBox.Show("员工表更新异常，请重新登录");
                    Log.WriteErrorLog("清空员工表出现异常，中断更新");
                }
            }

            if (File.Exists(GlobalParams.jsfsServerPath))
            {
                int ret;
                ret = new CardBLL().DeleteCard();

                if (ret >= 0)
                    DataOperator.UpdateData(GlobalParams.jsfsServerPath, GlobalParams.jsfsLocalPath, "paymentmethod_jsfs", false);
                else
                {
                    MessageBox.Show("结算方式表更新异常，请重新登录");
                    Log.WriteErrorLog("清空结算方式表出现异常，中断更新");
                }
            }

            if (File.Exists(GlobalParams.yhxsServerPath))
            {
                int ret;
                ret = new DiscountBLL().DeleteDiscount();

                if (ret >= 0)
                    DataOperator.UpdateData(GlobalParams.yhxsServerPath, GlobalParams.yhxsLocalPath, "discount_yhxs", false);
                else
                {
                    MessageBox.Show("时段折扣表更新异常，请重新登录");
                    Log.WriteErrorLog("清空时段折扣表出现异常，中断更新");
                }
            }

            //促销没有主键，采用先清空，再导入的策略
            if (File.Exists(GlobalParams.ptServerPath))
            {
                int ret;
                ret = new PromotionBLL().DeletePromotion();

                if (ret >= 0)
                    DataOperator.UpdateData(GlobalParams.ptServerPath, GlobalParams.ptLocalPath, "promotion_pt", false);
                else
                {
                    MessageBox.Show("促销表更新异常，请重新登录");
                    Log.WriteErrorLog("清空促销表出现异常，中断更新");
                }
            }
            #endregion

            Staff loginStaff = new StaffBLL().GetStaffByID(txtStaffID.Text);

            //记录登录的员工
            staff = loginStaff;
        }

        //验证输入是否为空
        private bool InputValidate()
        {
            if (txtStaffID.Text == "")
            {
                MessageBox.Show("请输入工号");
                TxtIDFocus();
                return false;
            }
            if (txtStaffPwd.Text == "")
            {
                MessageBox.Show("请输入密码");
                TxtPwdFocus();
                return false;
            }
            return true;
        }

        //清空输入框的值
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtStaffID.Text = "";
            txtStaffPwd.Text = "";
        }

        //在编号下方显示小键盘
        private void txtStaffID_Click(object sender, EventArgs e)
        {
            KeyboardShow(IDLocation);
        }

        //在密码框下方显示小键盘
        private void txtStaffPwd_Click(object sender, EventArgs e)
        {
            KeyboardShow(PwdLocation);
        }

        //设置小键盘的位置并显示到顶层
        private void KeyboardShow(Point location)
        {
            miniKeyboard.Location = location;
            miniKeyboard.BringToFront();
            miniKeyboard.Visible = true;
        }

        /// <summary>
        /// 为小键盘指定当前输入框和按确定转移焦点后的输入框，实现按键输入、删除和确定键的功能
        /// </summary>
        /// <param name="focusing">当前焦点输入框</param>
        /// <param name="toFoucus">要转移焦点的输入框</param>
        /// <param name="toLocation">转移后小键盘的位置</param>
        private void KeyboardInput(TextBox focusing, TextBox toFoucus, Point toLocation)
        {
            int startDel = 0;

            //退格
            if (keyInput == miniKeyboard.Backspace)
            {
                if (focusing.SelectedText != "")
                    focusing.SelectedText = "";
                else if (focusing.SelectionStart > 0)
                {
                    startDel = focusing.SelectionStart;
                    focusing.Text = focusing.Text.Substring(0, focusing.SelectionStart - 1) +
                        focusing.Text.Substring(focusing.SelectionStart, focusing.Text.Length - focusing.SelectionStart);
                    focusing.SelectionStart = startDel - 1;
                }
            }
            //按确定，焦点转移
            else if (keyInput == miniKeyboard.KeyEnter)
            {
                //如果另一个输入框内容为空，则焦点之，并把小键盘移动到它下面
                if (toFoucus.Text == "")
                {
                    toFoucus.Focus();
                    miniKeyboard.Location = toLocation;
                    return;
                }
                //另一个有内容，则直接登录（当前焦点框为空的话，会弹提示，不用再提前判断）
                else
                {
                    miniKeyboard.Visible = false;
                    this.Refresh();
                    btnLogIn_Click(new Object(), new EventArgs());
                    return;
                }
            }
            //按取消，清空焦点输入框的内容
            else if (keyInput == miniKeyboard.Cancel)
            {
                focusing.Text = "";
            }
            //其他键直接输入
            else
            {
                if (focusing.SelectedText != "")
                    focusing.SelectedText = keyInput;
                else
                    focusing.SelectedText += keyInput;
            }

            //按键完毕，保持焦点
            focusing.Focus();
        }

        private void TxtIDFocus()
        {
            txtStaffID.Focus();
            txtStaffID.SelectionStart = txtStaffID.TextLength;
            KeyboardShow(IDLocation);
        }

        private void TxtPwdFocus()
        {
            //重置密码框
            txtStaffPwd.Text = "";
            txtStaffPwd.Focus();
            KeyboardShow(PwdLocation);
        }

        //点击空白处，小键盘隐藏
        private void frmLogIn_Click(object sender, EventArgs e)
        {
            miniKeyboard.Visible = false;
        }

        //退出程序
        private void btnQuit_Click(object sender, EventArgs e)
        {
            miniKeyboard.Visible = false;

            if (!InputValidate())
                return;

            //新建等待提示标签
            Label lb = new Label();
            lb.Location = new Point(260, 480);
            lb.Font = new Font("宋体", 25F);
            lb.ForeColor = Color.White;
            lb.BackColor = Color.FromName("SteelBlue");
            lb.Size = new Size(304, 34);
            lb.Text = "正在退出...";
            this.Controls.Add(lb);
            lb.Refresh();
            btnClear.Visible = false;
            btnLogIn.Visible = false;
            //读取数据放到子线程里
            Thread thr = new Thread(GetData);
            thr.Start();
            thr.Join();

            if (staff != null)
            {
                if (txtStaffPwd.Text != staff.password)
                {
                    lb.Dispose();
                    btnClear.Visible = true;
                    btnLogIn.Visible = true;
                    MessageBox.Show("对不起，密码不正确，请重新输入");
                    TxtPwdFocus();
                    return;
                }
                //管理员才有权限退出
                else if (staff.Rank_no != "04")
                {
                    lb.Dispose();
                    btnClear.Visible = true;
                    btnLogIn.Visible = true;
                    MessageBox.Show("对不起，权限不足");
                    return;
                }

                //写日志
                string log = "管理员：" + staff.Staff_name + " 于" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒") + "退出系统。";
                Log.Write(logPath, log);

                Application.Exit();
            }
            else
            {
                lb.Dispose();
                btnClear.Visible = true;
                btnLogIn.Visible = true;
                MessageBox.Show("对不起，工号不正确，请重新输入");
                TxtIDFocus();
            }

            if (thr.IsAlive)
                thr.Abort();
        }

    }
}
