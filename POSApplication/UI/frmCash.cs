using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POSApplication.Model;
using POSApplication.UserController;
using POSApplication.Common;
using POSApplication.BLL;

namespace POSApplication.UI
{
    public partial class frmCash : Form
    {
        /// <summary>
        /// 收款员
        /// </summary>
        private Staff staff;

        /// <summary>
        /// 主管
        /// </summary>
        private Staff sup;

        /// <summary>
        /// 主界面
        /// </summary>
        private frmMain main;

        //自定义输入框（票面的数量）
        private InputBox inputBox;

        //现金票数字典
        private Dictionary<string, int> CashNum;
        private string SelectedCash;
        //现金
        private decimal Cash;
        //支票
        private decimal Check;

        public frmCash(Staff staff, frmMain main)
        {
            InitializeComponent();
            this.staff = staff;
            this.main = main;

            //初始化现金票数字典
            CashNum = new Dictionary<string, int> { 
                { "100", 0 }, { "50", 0 }, { "20", 0 }, 
                { "10", 0 }, { "5", 0 }, { "2", 0 },
                { "1", 0 }, { "0.5", 0 }, { "0.2", 0 },
                { "0.1", 0 }, { "0.05", 0 }, { "0.02", 0 }, { "0.01", 0 }
            };
            //初始化支票
            Check = 0;

            //小键盘绑定支票输入框
            miniKeyboard.Press += MiniKeyboardHandler_CheckInput;
        }

        private void frmCash_Load(object sender, EventArgs e)
        {

            //加载底部信息栏
            BottomBar bottom = new BottomBar(staff);
            Controls.Add(bottom);
            bottom.Location = new Point(0, 575);
            bottom.Show();

            //绑定各种票面按钮的点击事件
            foreach (Control btn in gbCash.Controls)
            {
                if (btn.GetType() == typeof(System.Windows.Forms.Button))
                {
                    Button button = (Button)btn;
                    button.Click += new System.EventHandler(this.btn_Click);
                }
            }
        }

        #region 小键盘绑定
        
        //存放小键盘所按下的键值
        private string keyInput;

        //小键盘绑定自定义输入框的事件
        public void MiniKeyboardHandler_Input(object sender, MiniKeyboardArgs e)
        {
            keyInput = e.KeyCode;
            Keyboard_Input(inputBox.type);
        }

        //小键盘绑定支票输入框的事件
        private void MiniKeyboardHandler_CheckInput(object sender, MiniKeyboardArgs e)
        {
            keyInput = e.KeyCode;
            Keyboard_CheckInput(txtCheck);
        }

        /// <summary>
        /// 自定义输入窗口输入框按键绑定，根据 type 绑定确定按钮要打开的窗体
        /// </summary>
        /// <param name="type"></param>
        private void Keyboard_Input(OrderHandleType type)
        {
            TextBox focusing = new TextBox();
            focusing = (TextBox)inputBox.Controls.Find("txtInput", false).FirstOrDefault();
            
            //退格
            if (keyInput == miniKeyboard.Backspace)
            {
                InputValidate.InputBackspace(focusing);
            }
            //按确定
            else if (keyInput == miniKeyboard.KeyEnter)
            {
                if (inputBox.type == OrderHandleType.password)
                {
                    string pwd = focusing.Text;
                    //根据密码获取主管
                    sup = new StaffBLL().GetStaffByPwd(pwd);
                    if (sup != null)
                    {
                        inputBox.Hide();

                        Print prt = new Print();
                        prt.staff = this.staff;

                        prt.PrintSalesDay();

                        Log.WriteNormalLog("打印本机当日销售汇总", sup.Staff_name, "");
                    }
                    else
                    {
                        MessageBox.Show("密码不正确！");
                        focusing.Text = "";
                    }
                }
                else if (inputBox.type == OrderHandleType.cash)
                {
                    if (focusing.Text != "")
                    {
                        CashNum[SelectedCash] = Convert.ToInt32(focusing.Text);

                        //对应的数量标签
                        Label lb = new Label();
                        lb = (Label)gbCash.Controls.Find("lb" + SelectedCash.Replace(".", ""), true).FirstOrDefault();
                        lb.Text = "× " + Convert.ToInt32(focusing.Text);

                        Calculate(true);
                    }
                    else
                    {
                        MessageBox.Show("请输入具体数量！");
                    }

                    inputBox.Hide();
                }
            }
            //按取消，隐藏商品小窗
            else if (keyInput == miniKeyboard.Cancel)
            {
                inputBox.Hide();
            }
            //其他键直接输入
            else
            {
                //不能输入 . 和 X
                if (!(keyInput == miniKeyboard.Dot || keyInput == miniKeyboard.X))
                {
                    if (focusing.SelectedText != "")
                        focusing.SelectedText = keyInput;
                    else
                        focusing.SelectedText += keyInput;
                }
            }

            //按键完毕，保持焦点
            focusing.Focus();
        }

        /// <summary>
        ///输入支票
        /// </summary>
        /// <param name="focusing">当前焦点输入框</param>
        private void Keyboard_CheckInput(TextBox focusing)
        {
            if ((!focusing.Focused) && focusing.Text == "0.00")
                focusing.Text = "";

            //退格
            if (keyInput == miniKeyboard.Backspace)
            {
                InputValidate.InputBackspace(focusing);
            }
            //按确定
            else if (keyInput == miniKeyboard.KeyEnter)
            {
            }
            //按取消，清空输入框
            else if (keyInput == miniKeyboard.Cancel)
            {
                focusing.Text = "";
            }
            //其他键直接输入，不能输入 X
            else if (keyInput != miniKeyboard.X)
            {
                if (focusing.SelectedText != "")
                    focusing.SelectedText = keyInput;
                else
                    focusing.SelectedText += keyInput;
            }
            
            focusing.SelectionStart = focusing.SelectionStart;
            if (focusing.SelectionStart == 0)
                focusing.SelectionStart = focusing.TextLength;

            //按键完毕，保持焦点
            focusing.Focus();
        }

        #endregion

        #region 自定义输入窗
        /// <summary>
        /// 显示自定义输入窗
        /// </summary>
        /// <param name="type"></param>
        private void InputBoxShow(OrderHandleType type)
        {
            inputBox = new InputBox(type);

            if (type == OrderHandleType.cash)
            {
                //输入框显示已有的数量
                TextBox input = (TextBox)inputBox.Controls.Find("txtInput", false).FirstOrDefault();
                input.Text = CashNum[SelectedCash].ToString();
            }

            Controls.Add(inputBox);
            inputBox.BringToFront();

            Point location = new Point(120, 140);
            inputBox.Location = location;
            
            InputBoxBindingMinikeyboard();
            inputBox.VisibleChanged += new System.EventHandler(this.inputBox_VisibleChanged);
        }

        // 自定义输入窗在显示/隐藏时，禁用/启用其他控件
        private void inputBox_VisibleChanged(object sender, EventArgs e)
        {
            //自定义输入窗显示，并绑定小键盘
            if (inputBox.Visible)
            {
                InputBoxBindingMinikeyboard();
            }
            //自定义输入窗隐藏，启用所有控件，重新绑定小键盘
            else
            {
                foreach (Control control in Controls)
                    control.Enabled = true;
                miniKeyboard.Press -= MiniKeyboardHandler_Input;
                miniKeyboard.Press += MiniKeyboardHandler_CheckInput;
            }

            inputBox.Dispose();
        }

        /// <summary>
        ///  自定义输入窗绑定小键盘：除了输入窗和小键盘，禁用其余控件
        /// </summary>
        private void InputBoxBindingMinikeyboard()
        {
            foreach (Control control in Controls)
                if (control != inputBox && control != miniKeyboard)
                    control.Enabled = false;
            //重新绑定小键盘
            miniKeyboard.Press -= MiniKeyboardHandler_CheckInput;
            miniKeyboard.Press += MiniKeyboardHandler_Input;
        }

        #endregion

        #region 功能按键
        //返回主界面
        private void btnReturn_Click(object sender, EventArgs e)
        {
            main.Show();
            this.Dispose();
        }

        //打印解款单
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print prt = new Print();
            prt.staff= this.staff;
            prt.Cash = this.Cash;
            prt.CashNum = this.CashNum;
            prt.Check = this.Check;

            prt.PrintCash();
        }

        #endregion

        /// <summary>
        /// 更新现金、支票和总计
        /// </summary>
        /// <param name="CalCash">true 重新计算现金；false 不计算现金</param>
        private void Calculate(bool CalCash)
        {
            //重新计算现金
            if (CalCash)
            {
                Cash = 0;

                foreach (KeyValuePair<string, int> c in CashNum)
                    Cash += Convert.ToDecimal(c.Key) * Convert.ToDecimal(c.Value);
            }

            lbCash.Text = Cash.ToString();
            lbCheck.Text = Check.ToString();
            lbZJ.Text = (Cash + Check).ToString();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            //记录要输入张数的票面
            SelectedCash = btn.Text.Replace("元", "");

            InputBoxShow(OrderHandleType.cash);
        }

        /// <summary>
        /// 支票输入规范
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCheck_TextChanged(object sender, EventArgs e)
        {
            InputValidate.CashInputValidate(txtCheck);

            //给支票赋值
            if(txtCheck.Text!="")
                Check = Convert.ToDecimal(txtCheck.Text);

            Calculate(false);
        }

        private void btnPrintSalesDay_Click(object sender, EventArgs e)
        {
            //InputBoxShow(OrderHandleType.password);
            Print prt = new Print();
            prt.staff = this.staff;

            prt.PrintSalesDay();
        }
    }
}
