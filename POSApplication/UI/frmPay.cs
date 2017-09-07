using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POSApplication.UserController;
using POSApplication.Model;
using POSApplication.BLL;
using POSApplication.Common;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Ports;
using System.Threading;

namespace POSApplication.UI
{
    public partial class frmPay : Form
    {
        // 测试用 无参构造方法
        public frmPay()
        {
            InitializeComponent();
        }
        //构造函数
        public frmPay(Staff staff, frmSell sell)
        {
            InitializeComponent();
            this.staff = staff;
            this.sell = sell;

            isAddorAlter = true;
            payedCardList = new List<PayedCard>();

            //抹零到角
            ZJ = Math.Truncate((sell.MoneyTotal - sell.YH) * 10) / 10;
            Zjj = (sell.MoneyTotal - sell.YH) - ZJ;

            //应收现金赋初值
            Cash = ZJ;
            //没支付前，Change为负
            Change = -1;

            //小键盘绑定实收现金输入框
            miniKeyboard.Press += MiniKeyboardHandler_PayCashInput;
        }

        #region 属性
        private Staff staff;
        private frmSell sell;

        /// <summary>
        /// 抹零到角后的总计
        /// </summary>
        private decimal ZJ { get; set; }

        /// <summary>
        /// 抹零
        /// </summary>
        private decimal Zjj { get; set; }

        /// <summary>
        /// 应收现金（总计 减去 卡付）
        /// </summary>
        private decimal Cash { get; set; }

        /// <summary>
        /// 找零
        /// </summary>
        private decimal Change { get; set; }

        /// <summary>
        /// 支付方式小窗
        /// </summary>
        private CardBox box;

        /// <summary>
        /// 标记支付小窗要进行的操作是新增还是修改, add true; alter false
        /// </summary>
        private bool isAddorAlter;

        /// <summary>
        /// 刷卡列表
        /// </summary>
        private List<PayedCard> payedCardList;

        /// <summary>
        /// 卡列表选择序号
        /// </summary>
        private int cardIndex = -1;

        /// <summary>
        /// 刷卡总金额
        /// </summary>
        private decimal CardPayed;

        /// <summary>
        /// 会员卡支付小窗
        /// </summary>
        private MCardPay mCardPay;

        /// <summary>
        /// 持卡人姓名
        /// </summary>
        private string mName;

        /// <summary>
        /// 交易前余额
        /// </summary>
        private decimal mIniBalance;

        /// <summary>
        /// 交易后余额
        /// </summary>
        private decimal mEndBalance;

        /// <summary>
        /// 小票单
        /// </summary>
        private List<Receipt> rList;

        private ICustomerDisplayer custDisplayer = ObjFactory.CreateCustomerDisplayer();
        #endregion

        #region 功能按钮及事件
        private void frmPay_Load(object sender, EventArgs e)
        {
            //设置客显
            UpdateDisplay();

            //显示金额和数量
            lbYH.Text = (sell.YH + sell.DZ).ToString();
            lbMoneyTotal.Text = ZJ.ToString();
            lbGoodsCount.Text = sell.GoodsCount.ToString();
            lbCash.Text = Cash.ToString();

            //加载底部信息栏
            BottomBar bottom = new BottomBar(staff);
            Controls.Add(bottom);
            bottom.Location = new Point(0, 575);
            bottom.Show();

            //禁用支付方式删除和修改按钮
            btnDel.Enabled = false;
            btnAlter.Enabled = false;

            //有内部会员卡显示会员卡支付按钮
            if (sell.Mname != "" && !sell.isCash)
            {
                btnMCardPay.Visible = true;

                miniKeyboard.Enabled = false;
                dgvCardPay.Enabled = false;
                btnAdd.Enabled = false;
                btnDel.Enabled = false;
                btnAlter.Enabled = false;
            }
            else
                btnMCardPay.Visible = false;

            //得到小票列表
            rList = new List<Receipt>();
            if (File.Exists(GlobalParams.recLocalPath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                //反序列化小票单
                using (Stream input = File.OpenRead(GlobalParams.recLocalPath))
                {
                    if (input.Length > 0)
                        rList = (List<Receipt>)formatter.Deserialize(input);
                }
            }
        }

        //结账按钮
        private void btnPay_Click(object sender, EventArgs e)
        {
            CheckPay();
        }

        //会员卡支付按钮
        private void btnMCardPay_Click(object sender, EventArgs e)
        {
            mCardPay = new MCardPay(sell.Membercard);

            //默认卡付金额，总计减去现金支付和已有卡付支付金额
            mCardPay.MoneyTotal = ZJ - Convert.ToDecimal(txtPayCash.Text == "" ? "0" : txtPayCash.Text) - CardPayed;

            //待付的金额大于0才显示卡付面板
            if (mCardPay.MoneyTotal > 0)
            {
                mCardPayShow();
                isAddorAlter = true;
            }
        }

        //返回销售按钮
        private void btnReturn_Click(object sender, EventArgs e)
        {
            //可能出现结账之后返回销售界面的情况，要保持原来的iscash值
            //sell.isCash = false;
            sell.Show();
            this.Dispose();
        }

        //点击增加卡付，显示支付方式小窗
        private void btnAdd_Click(object sender, EventArgs e)
        {
            box = new CardBox();

            //默认卡付金额，总计减去现金支付和已有卡付支付金额
            box.MoneyTotal = ZJ - Convert.ToDecimal(txtPayCash.Text == "" ? "0" : txtPayCash.Text) - CardPayed;

            //待付的金额大于0才显示卡付面板
            if (box.MoneyTotal > 0)
            {
                cardBoxShow();
                isAddorAlter = true;
            }
        }

        //删除所选卡付
        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("确定要删除吗？", "", MessageBoxButtons.OKCancel);
            if (r == DialogResult.OK)
            {
                payedCardList.Remove(payedCardList[cardIndex]);
                dgvCardPay_DataBind();
                //禁用删除修改
                btnAlter.Enabled = false;
                btnDel.Enabled = false;
            }
        }

        //点击修改所选卡付，显示支付方式小窗
        private void btnAlter_Click(object sender, EventArgs e)
        {
            decimal moneyTotal = ZJ - Convert.ToDecimal(txtPayCash.Text == "" ? "0" : txtPayCash.Text) - CardPayed + payedCardList[cardIndex].je;


            box = new CardBox(payedCardList[cardIndex], moneyTotal);

            //修改状态
            //if (isAddorAlter)

            cardBoxShow();
            box.txtCardCash.Focus();//获得焦点
            box.txtCardCash.SelectAll();//全选
            isAddorAlter = false;
        }

        /// <summary>
        /// 输入实收金额规范，并计算和显示找零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPayCash_TextChanged(object sender, EventArgs e)
        {
            decimal payCash = 0;

            InputValidate.PayInputValidate(txtPayCash);

            if (txtPayCash.TextLength > 0)
                payCash = Convert.ToDecimal(txtPayCash.Text);

            txtPayCash.SelectionStart = txtPayCash.TextLength;

            Change = payCash - Cash;

            if (payCash != 0)
                lbChange.Text = Change.ToString();
            else
                lbChange.Text = "0.0";

            UpdateDisplay();
        }

        #endregion

        #region 支付小窗
        /// <summary>
        /// 设定支付小窗位置，绑定显隐事件
        /// </summary>
        private void cardBoxShow()
        {
            Controls.Add(box);
            box.BringToFront();

            Point location = new Point(12, 57);
            box.Location = location;

            boxBindingMinikeyboard();
            box.VisibleChanged += new System.EventHandler(this.box_VisibleChanged);
        }

        /// <summary>
        /// 支付方式小窗在显示/隐藏时，禁用/启用其他控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void box_VisibleChanged(object sender, EventArgs e)
        {
            //支付方式小窗显示，并绑定小键盘
            if (box.Visible)
                boxBindingMinikeyboard();
            //支付方式小窗隐藏，启用所有控件，重新绑定小键盘
            else
            {
                foreach (Control control in Controls)
                    control.Enabled = true;

                //禁用支付方式删除和修改按钮
                btnDel.Enabled = false;
                btnAlter.Enabled = false;

                miniKeyboard.Press -= MiniKeyboardHandler_CardInput;
                miniKeyboard.Press += MiniKeyboardHandler_PayCashInput;

                box.Dispose();
            }
        }

        /// <summary>
        /// 支付方式小窗绑定小键盘
        /// </summary>
        private void boxBindingMinikeyboard()
        {
            foreach (Control control in Controls)
                if (control != box && control != miniKeyboard)
                    control.Enabled = false;
            miniKeyboard.Press -= MiniKeyboardHandler_PayCashInput;
            miniKeyboard.Press += MiniKeyboardHandler_CardInput;
        }
        #endregion

        #region 会员卡支付小窗
        /// <summary>
        /// 设定会员卡支付小窗位置，绑定显隐事件
        /// </summary>
        private void mCardPayShow()
        {
            miniKeyboard.Enabled = true;
            Controls.Add(mCardPay);
            mCardPay.BringToFront();

            Point location = new Point(12, 57);
            mCardPay.Location = location;

            mCardPayBindingMinikeyboard();
            mCardPay.VisibleChanged += new System.EventHandler(this.mCardPay_VisibleChanged);
        }

        /// <summary>
        /// 会员卡支付方式小窗在显示/隐藏时，禁用/启用其他控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mCardPay_VisibleChanged(object sender, EventArgs e)
        {
            //会员卡支付方式小窗显示，并绑定小键盘
            if (mCardPay.Visible)
                mCardPayBindingMinikeyboard();

            //会员卡支付方式小窗隐藏，启用所有控件，重新绑定小键盘
            else
            {
                foreach (Control control in Controls)
                    control.Enabled = true;

                //禁用支付方式删除和修改按钮
                btnDel.Enabled = false;
                btnAlter.Enabled = false;
                miniKeyboard.Enabled = false;//内部员工卡支付后不能再使用现金

                miniKeyboard.Press -= MiniKeyboardHandler_mCardPayInput;
                miniKeyboard.Press += MiniKeyboardHandler_PayCashInput;

                mCardPay.Dispose();
            }
        }

        /// <summary>
        /// 会员卡支付方式小窗绑定小键盘
        /// </summary>
        private void mCardPayBindingMinikeyboard()
        {
            foreach (Control control in Controls)
                if (control != mCardPay && control != miniKeyboard)
                    control.Enabled = false;
            miniKeyboard.Press -= MiniKeyboardHandler_PayCashInput;
            miniKeyboard.Press += MiniKeyboardHandler_mCardPayInput;
        }

        #endregion

        #region 小键盘绑定
        //存放小键盘所按下的键值
        private string keyInput;

        //小键盘绑定实收现金框的事件
        private void MiniKeyboardHandler_PayCashInput(object sender, MiniKeyboardArgs e)
        {
            keyInput = e.KeyCode;
            Keyboard_Input(txtPayCash);
        }

        //小键盘绑定支付方式的事件
        private void MiniKeyboardHandler_CardInput(object sender, MiniKeyboardArgs e)
        {
            keyInput = e.KeyCode;
            Keyboard_CardInput();
        }

        //小键盘绑定支付方式的事件
        private void MiniKeyboardHandler_mCardPayInput(object sender, MiniKeyboardArgs e)
        {
            keyInput = e.KeyCode;
            Keyboard_mCardPayInput();
        }

        /// <summary>
        /// 实收现金框按键绑定
        /// </summary>
        /// <param name="focusing"></param>
        private void Keyboard_Input(TextBox focusing)
        {
            if ((!focusing.Focused) && focusing.Text == "0.0")
                focusing.Text = "";

            //退格
            if (keyInput == miniKeyboard.Backspace)
            {
                InputValidate.InputBackspace(focusing);
            }
            else if (keyInput == miniKeyboard.KeyEnter)
            {
                //使用专门的结账按钮
                //Check();
            }
            //按取消，清空输入框
            else if (keyInput == miniKeyboard.Cancel)
            {
                focusing.Text = "";
            }
            //其他键直接输入
            //不能输入 X
            else if (keyInput != miniKeyboard.X)
            {
                //应收现金大于0才可以输入实收现金
                if (Cash != 0)
                {
                    if (focusing.SelectedText != "")
                        focusing.SelectedText = keyInput;
                    else
                        focusing.SelectedText += keyInput;
                }
            }

            //focusing.SelectionStart = focusing.SelectionStart;
            //按键完毕，保持焦点
            focusing.Focus();
        }

        /// <summary>
        /// 支付小窗按键绑定
        /// </summary>
        /// <param></param>
        private void Keyboard_CardInput()
        {
            TextBox focusing = (TextBox)box.Controls.Find("txtCardCash", true).FirstOrDefault();

            if (focusing.Text == "0.0" && (!focusing.Focused))
                focusing.Text = "";

            //退格
            if (keyInput == miniKeyboard.Backspace)
            {
                InputValidate.InputBackspace(focusing);
            }
            else if (keyInput == miniKeyboard.KeyEnter)
            {
                Panel panelCard = (Panel)box.Controls.Find("panelCard", false).FirstOrDefault();
                Panel panelCardCash = (Panel)box.Controls.Find("panelCardCash", false).FirstOrDefault();
                if (panelCard.Visible)
                {
                    MessageBox.Show("请选择支付方式");
                    return;
                }
                else if (focusing.Text == "" && panelCardCash.Visible)
                {
                    MessageBox.Show("请输入支付金额");
                    return;
                }

                PayedCard payedCard = new PayedCard(box.cardList[box.index]);
                payedCard.je = Convert.ToDecimal(focusing.Text);

                //根据标记判断是增加还是修改操作
                if (isAddorAlter)
                    payedCardList.Add(payedCard);
                else
                    payedCardList[cardIndex] = payedCard;

                dgvCardPay_DataBind();
                box.Hide();
            }
            //按取消，关闭小窗
            else if (keyInput == miniKeyboard.Cancel)
            {
                Panel panelCard = (Panel)box.Controls.Find("panelCard", false).FirstOrDefault();
                Panel panelCardCash = (Panel)box.Controls.Find("panelCardCash", false).FirstOrDefault();
                //根据面板判断：如果输入支付金额面板显示，就隐藏并显示选择支付方式面板
                //否则说明显示的是选择支付方式面板，取消键直接关闭支付小窗
                if (panelCardCash.Visible)
                {
                    panelCard.Show();
                    box.MoneyTotal = Convert.ToDecimal(box.txtCardCash.Text == "" ? "0" : box.txtCardCash.Text);
                    panelCardCash.Hide();
                }
                else
                {
                    dgvCardPay_DataBind();
                    box.Hide();
                }
            }
            //其他键直接输入
            //不能输入 X
            else if (keyInput != miniKeyboard.X)
            {
                if (focusing.SelectedText != "")
                    focusing.SelectedText = keyInput;
                else
                    focusing.SelectedText += keyInput;
            }

            //按键完毕，保持焦点 
            focusing.Focus();
        }

        /// <summary>
        /// 会员卡支付小窗按键绑定
        /// </summary>
        /// <param></param>
        private void Keyboard_mCardPayInput()
        {
            TextBox focusing = (TextBox)mCardPay.Controls.Find("txtCardCash", true).FirstOrDefault();

            if (focusing.Text == "0.0" && (!focusing.Focused))
                focusing.Text = "";

            //退格
            if (keyInput == miniKeyboard.Backspace)
            {
                InputValidate.InputBackspace(focusing);
            }
            else if (keyInput == miniKeyboard.KeyEnter)
            {
                if (focusing.Text == "")
                {
                    MessageBox.Show("请输入支付金额");
                    return;
                }
                else if (Convert.ToDecimal(focusing.Text) == 0)
                {
                    MessageBox.Show("支付金额必须大于零");
                    return;
                }

                //增加员工卡模型，员工卡结算方式编号 "5"
                PayedCard payedCard = new PayedCard("5");
                if (payedCardList != null)
                {
                    payedCard.je = Convert.ToDecimal(focusing.Text);

                    string mdh = ConfigHelper.GetAppConfig("MDH");
                    string jh = ConfigHelper.GetAppConfig("JH");
                    string xph = GetXPH();

                    //会员卡支付接口
                    string result = CardTran.cardTran(mdh, jh, sell.Membercard, xph, focusing.Text);//提交SVN和生产环境使用（生成Release版本）
                    //string result = "OK";//pos上测试时使用（生成Release版本）
                    //#if DEBUG
                    //            result = "OK";
                    //#endif

                    if (result.Substring(0, 2) == "OK")
                    {
                        //MessageBox.Show("支付成功！");
                        payedCardList.Add(payedCard);

                        if (mName != mCardPay.mName)
                            mIniBalance = mCardPay.Balance;

                        mName = mCardPay.mName;

                        mEndBalance = mCardPay.Balance - payedCard.je;

                        Log.WriteNormalLog(mName + " 卡付 " + payedCard.je + "元", "", "");

                        btnReturn.Enabled = false;
                        btnReturn.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show(result);
                        return;
                    }
                }
                else
                    return;

                ////根据标记判断是增加还是修改操作
                //if (isAddorAlter)
                //    payedCardList.Add(payedCard);
                //else
                //    payedCardList[cardIndex] = payedCard;

                dgvCardPay_DataBind();
                mCardPay.Hide();
            }
            //按取消，关闭小窗
            else if (keyInput == miniKeyboard.Cancel)
            {
                mCardPay.Hide();
            }
            //其他键直接输入
            //不能输入 X
            else if (keyInput != miniKeyboard.X)
            {
                if (focusing.SelectedText != "")
                    focusing.SelectedText = keyInput;
                else
                    focusing.SelectedText += keyInput;
            }

            //按键完毕，保持焦点 
            focusing.Focus();
        }
        #endregion

        #region 刷卡列表
        /// <summary>
        /// 点击单元格，选择行，设置 cardIndex
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCardPay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                cardIndex = e.RowIndex;
                dgvCardPay.Rows[e.RowIndex].Selected = true;

                //启用功能键
                btnAlter.Enabled = true;
                btnDel.Enabled = true;
            }
        }

        /// <summary>
        /// 卡支付列表数据刷新
        /// </summary>
        private void dgvCardPay_DataBind()
        {
            dgvCardPay.DataSource = new BindingList<PayedCard>(payedCardList);

            dgvCardPay.Columns["id"].Visible = false;
            dgvCardPay.Columns["jsfsbh"].Visible = false;
            dgvCardPay.Columns["State"].Visible = false;
            dgvCardPay.Columns["ksrq"].Visible = false;

            dgvCardPay.ClearSelection();
            cardIndex = -1;

            CardPayed = 0;
            //计算刷卡金额
            foreach (PayedCard card in payedCardList)
                CardPayed += card.je;

            lbCardPay.Text = CardPayed.ToString();

            //从新计算应收现金
            Cash = ZJ - CardPayed;
            lbCash.Text = Cash.ToString();

            //从新计算找零
            Change = Convert.ToDecimal(txtPayCash.Text == "" ? "0" : txtPayCash.Text) - Cash;

            lbChange.Text = Change.ToString();

            UpdateDisplay();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 结账前的判断
        /// </summary>
        private void CheckPay()
        {
            /*按确定的逻辑，根据应付现金判断实收现金是否合理*/
            if (CheckPayCash.Check(Cash, Change))
            {
                PayEnd();
            }
            else
            {
                MessageBox.Show("请输入正确的金额。");
            }
        }

        /// <summary>
        /// 更新客显
        /// </summary>
        private void UpdateDisplay()
        {
            try
            {
                //pay = 卡付 + 实收现金
                /*string str = string.Format(" Money: {0}" + Environment.NewLine
                            + "   Pay: {1}" + Environment.NewLine
                            + "Change: {2}", ZJ.ToString(),
                            CardPayed + Convert.ToDecimal(txtPayCash.Text == "" ? "0" : txtPayCash.Text), lbChange.Text);

                custDisplayer.CLS(sell.DisplayPort);
                custDisplayer.SendToDisplay(sell.DisplayPort, str);*/

                CustomerDisplayData data = new CustomerDisplayData();
                data.Total = ZJ.ToString();
                data.Pay = CardPayed + Convert.ToDecimal(txtPayCash.Text == "" ? "0" : txtPayCash.Text).ToString();
                data.Change = lbChange.Text;
                data.StatusLight = 2;
                custDisplayer.Display(sell.DisplayPort, data);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, "UpdateDisplay()", "frmPay.cs");
            }
        }

        /// <summary>
        /// 结账
        /// </summary>
        private void PayEnd()
        {
            //禁用控件，除了返回键
            foreach (Control control in Controls)
                control.Enabled = false;
            btnReturn.Enabled = true;

            List<OrderItem> order = new List<OrderItem>();
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream input = File.OpenRead(GlobalParams.orderPath))
            {
                if (input.Length > 0)
                    order = (List<OrderItem>)formatter.Deserialize(input);
            }

            //得到销售数据模型 salesList
            List<Sales> salesList = GetSalesListModel(order);
            //测试
            //List<Sales> salesList = GetSalesList(order);
            new DataOperator().UpdateSalesData(salesList);

            #region 打印小票

            //小票模板传参
            Receipt rpt = new Receipt()
            {
                xph = GetXPH(),
                jh = ConfigHelper.GetAppConfig("JH").Substring(1, 4),
                syybh = staff.Staff_no,
                order = order.Where(o => o.isValid == true).ToList(),
                goodsCount = sell.GoodsCount,
                moneyTotal = sell.MoneyTotal,
                yh = (sell.YH + sell.DZ),//sell.YH,
                zjj = Zjj,
                actualTotal = ZJ,
                cash = Convert.ToDecimal(txtPayCash.Text == "" ? "0" : txtPayCash.Text),
                cardPay = CardPayed,
                change = Change,
                mcard = sell.Membercard,
                mName = mName,
                iniBalance = mIniBalance,
                endBalance = mEndBalance,
                printTime = DateTime.Now
            };

            //增加小票到小票列表，序列化保存
            rList.Add(rpt);
            SerializeReceiptList(rList);

            //更新客显
            UpdateDisplay();

            bool flag = true;
#if DEBUG
            flag = false;
#endif
            //开钱箱，收现金
            if (rpt.cash > 0 && flag)
            {
                ICustomerDisplayer custDisplayer = ObjFactory.CreateCustomerDisplayer();
                if ("TECT10".Equals(custDisplayer.GetTypeID()))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo("tec_drw.exe");
                    //startInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    Process.Start(startInfo);
                }
            }

            //打印小票
            Print prt = new Print();

            //如果是内部员工卡打印两张一样的小票
            if (sell.Mname != "" || sell.isCash)
            {
                prt.rec = rpt;
                prt.PrintReceipt(false);
                //打印到 txt
                prt.print(false);
            }


            prt.rec = rpt;
            prt.PrintReceipt(false);
            //打印到 txt
            prt.print(false);
            #endregion

            //收银完毕，删除购物单临时文件
            File.Delete(GlobalParams.orderPath);

            //小票号递增，记录格式
            GlobalParams.xph++;
            ConfigHelper.UpdateAppConfig("XPH", DateTime.Now.ToString("yyyyMMdd") + "#" + GlobalParams.xph);

            //前后台交互
            DataUpload.Upload(string.Format("小票号：{0}", GlobalParams.xph - 1));

            #region 导出单次销售明细表数据到 DBF 文件 （废）

            ////服务端是否存在 control.tab
            ////没有，就上传 pos.tab（可能碰到断网），表示前台开始控制
            ////有，意味着 DBF 文件正在被服务端占用，需要延迟上传
            //if (!File.Exists(GlobalParams.ControlTabPath))
            //{
            //    try
            //    {
            //        File.Copy(GlobalParams.PosTabLocalPath, GlobalParams.PosTabServerPath, true);
            //    }
            //    catch (Exception ex)
            //    {
            //        //断网情况会失败
            //        Log.WriteErrorLog(ex.Message, ex.Source, "向服务端放 pos.tab ");
            //    }

            //    //服务端是否存在 DBF 文件：存在意味着服务端还没有读取，需要向其中追加
            //    if (File.Exists(GlobalParams.xsxbServerPath))
            //    {
            //        try
            //        {
            //            //先拷贝到本地
            //            File.Copy(GlobalParams.xsxbServerPath, GlobalParams.xsxbPath, true);

            //            //读取 DBF 文件数据到单次销售明细表（追加）
            //            new DataOperator().DBFtoDB(GlobalParams.xsxbPath, "sales_time");
            //            new SalesBLL().DeleteSalesTime999();
            //        }
            //        catch (Exception ex)
            //        {
            //            Log.WriteErrorLog(ex.Message, ex.Source, "从服务端拷贝 xsxb.dbf 文件 ");
            //        }
            //    }
            //}

            ////备份路径是否存在 DBF 文件：存在意味着上次上传失败或延迟上传
            //if (File.Exists(GlobalParams.xsxbBackupsPath))
            //{
            //    //如果服务端存在DBF文件，复制到本地后，判断和备份文件是否一致
            //    //不一致才追加备份文件
            //    //先拷贝到本地
            //    File.Copy(GlobalParams.xsxbServerPath, GlobalParams.xsxbPath, true);

            //    if (!DataOperator.CompareFile(GlobalParams.xsxbBackupsPath, GlobalParams.xsxbPath))
            //    {
            //        //读取备份 DBF 文件数据到单次销售明细表（追加）
            //        new DataOperator().DBFtoDB(GlobalParams.xsxbBackupsPath, "sales_time");
            //        new SalesBLL().DeleteSalesTime999();

            //        //删除备份 DBF 文件
            //        File.Delete(GlobalParams.xsxbBackupsPath);
            //    }
            //}

            ////复制原始文件到单次销售明细 dbf 文件本地保存路径（覆盖）
            ////dbf文件无法真正删除老数据，每次上传前先使用比较小的原始文件
            //File.Copy(GlobalParams.xsxbIniPath, GlobalParams.xsxbPath, true);

            ////导出单次销售明细数据到本地 DBF 文件
            ////new DataOperator().SalesTimetoDBF();

            ////服务端是否有pos.tab：有意味着是正常上传，没有意味着要延迟到下次上传（可能断网了）
            //if (File.Exists(GlobalParams.PosTabServerPath))
            //{
            //    //上传本地 DBF 文件到服务端
            //    if (File.Exists(GlobalParams.xsxbPath))
            //    {
            //        try
            //        {
            //            File.Copy(GlobalParams.xsxbPath, GlobalParams.xsxbServerPath, true);
            //        }
            //        catch (Exception ex)
            //        {
            //            Log.WriteErrorLog(ex.Message, ex.Source, "上传本地 DBF 文件到服务端");
            //        }
            //    }

            //    //判断本地和服务端的文件是否一致，不一致说明上传失败，保存本地 DBF 文件到备份路径
            //    //有一种可能要排除：刚才上传成功了，但是突然断网了，现在再去对比，由于服务器的文件访问不了，也是不一致
            //    //此时会备份文件，断网了也删不掉服务器上的文件
            //    //到下次读取备份文件时，要先对比下是否和服务器上的文件是否一致，一致的话就不用追加到单次明细里了
            //    if (!(File.Exists(GlobalParams.xsxbServerPath) 
            //        && DataOperator.CompareFile(GlobalParams.xsxbPath, GlobalParams.xsxbServerPath)))
            //    {
            //        File.Copy(GlobalParams.xsxbPath, GlobalParams.xsxbBackupsPath);
            //        Log.WriteNormalLog("","服务端与本地文件对比失败，复制到备份路径","");

            //        //服务端如果存在dbf文件，因为此时是上传失败的，所以它是老文件
            //        //老文件的数据已经添加到本地的备份文件中，所以要删除掉老文件防止重复数据
            //        if (File.Exists(GlobalParams.xsxbServerPath))
            //        {
            //            try
            //            {
            //                File.Delete(GlobalParams.xsxbServerPath);
            //                Log.WriteNormalLog("", "删除服务端老的单次销售明细数据", "");
            //            }
            //            catch (Exception ex)
            //            {
            //                Log.WriteErrorLog(ex.Message, ex.Source, "删除服务端老的单次销售明细数据");
            //            }
            //        }
            //    }

            //    //删除服务端 pos.tab
            //    try
            //    {
            //        File.Delete(GlobalParams.PosTabServerPath);
            //    }
            //    catch (Exception ex)
            //    {
            //        Log.WriteErrorLog(ex.Message, ex.Source, "上传后删除服务端的 pos.tab");
            //    }
            //}
            //else
            //{
            //    File.Copy(GlobalParams.xsxbPath, GlobalParams.xsxbBackupsPath);
            //    Log.WriteNormalLog("","延迟上传单次销售明细数据，复制到备份路径","");
            //}

            ////删掉本地的DBF文件
            //File.Delete(GlobalParams.xsxbPath);

            #endregion

            //更新商品变化主档
            DataOperator.UpdateSpit();

            //找零时，暂停3秒
            if (Change > 0)
                Thread.Sleep(3000);
            sell.isCash = false;
            sell.Show();
            this.Dispose();
        }

        /// <summary>
        /// 得到销售数据模型 salesList
        /// </summary>
        /// <param name="order">购物单</param>
        /// <returns></returns>
        public List<Sales> GetSalesListModel(List<OrderItem> order)
        {
            //商品销售记录
            List<Sales> salesList = GetSalesList(order);

            //合计后抹零到角，舍去的分放在该小票最高交易价的商品的 zjj 字段
            salesList.OrderByDescending(s => s.jyj).Take(1).ToList().ForEach(o => { o.zjj = Zjj; });

            //支付现金
            decimal cash = Convert.ToDecimal(txtPayCash.Text == "" ? "0" : txtPayCash.Text);

            //有现金支付
            if (cash > 0)
            {
                //存在卡支付，就是现金+卡多种支付
                if (payedCardList.Count > 0)
                {
                    //混币(多种结算方式)时，商品销售记录的特征码设为 11
                    foreach (Sales sales in salesList)
                        sales.tzm = "11";

                    //加现金支付记录
                    salesList = AddPayToSalesList(salesList, true);
                    //加卡支付记录
                    salesList = AddPayToSalesList(salesList, false);

                    //混币记录第一行记录抹去的分，待定！--支付方式里不再放 zjj
                    //salesList[salesList.Count - payedCardList.Count].zjj = Zjj;
                }
            }
            //没有现金支付
            else
            {
                //只有一个元素且没有支付现金时，不需要额外增加记录支付方式的元素，只用修改下已有元素的 jsfsbh
                if (payedCardList.Count == 1)
                {
                    foreach (Sales sales in salesList)
                        sales.jsfsbh = payedCardList[0].jsfsbh;
                }
                //额外增加记录各种支付方式金额的元素
                else if (payedCardList.Count > 1)
                {
                    //混币(多种结算方式)时，商品销售记录的特征码设为 11
                    foreach (Sales sales in salesList)
                        sales.tzm = "11";

                    //加卡支付记录
                    salesList = AddPayToSalesList(salesList, false);

                    //混币记录第一行记录抹去的分，待定！--支付方式里不再放 zjj
                    //salesList[salesList.Count - payedCardList.Count].zjj = Zjj;
                }
            }

            return salesList;
        }

        /// <summary>
        /// 填充商品销售记录模型
        /// </summary>
        /// <param name="order">购物车列表</param>
        /// <param name="day">销售日期</param>
        /// <param name="time">销售时间</param>
        /// <returns></returns>
        public List<Sales> GetSalesList(List<OrderItem> order)
        {
            List<Sales> salesList = new List<Sales>();
            string day = DateTime.Now.ToString("yyyyMMdd");
            string time = DateTime.Now.ToString("HH:mm:ss");

            foreach (OrderItem item in order)
            {
                Sales sales = new Sales();

                //默认没有用多种支付方式结算（否则为混币，是11，还要额外增加记录各种支付方式金额的元素）
                sales.tzm = "1";

                sales.spbh = item.spbh;
                sales.txm = item.txm;
                sales.jyj = item.JYJ;
                sales.jysl = item.JYSL;
                sales.ssje = item.XJ;
                sales.xsrq = day;
                sales.xssj = time;
                sales.syybh = staff.Staff_no;

                //小票号

                sales.xph = GetXPH();

                //默认的的支付方式是现金 0
                sales.jsfsbh = "0";

                sales.jh = ConfigHelper.GetAppConfig("JH").Substring(1, 4);
                sales.yj = item.YJ;

                //默认收银抹零是0；
                sales.zjj = 0;

                sales.mcard = item.Mcard;
                sales.yhmx = item.yhmx;

                //单品积分明细，实收金额乘以积分比率（1，0）
                sales.jfmx = Math.Round(item.XJ * item.jf_rate);

                //18位码标志，待定！
                sales.szbz = "";

                sales.cxbh = item.cxbh;
                sales.xpxh = item.xpxh;
                sales.cxid = item.cxid;
                sales.Baka = "0";
                sales.Bakb = 1;

                salesList.Add(sales);
            }

            return salesList;
        }

        /// <summary>
        /// 向商品销售记录模型中添加支付方式记录
        /// </summary>
        /// <param name="salesList">商品销售记录</param>
        /// <param name="isAllCashPayed">是否纯现金支付</param>
        /// <returns></returns>
        public List<Sales> AddPayToSalesList(List<Sales> salesList, bool isAllCashPayed)
        {
            Sales sales = new Sales();

            //纯现金支付
            if (isAllCashPayed)
            {
                sales = GetPayFromSalesList(salesList);

                //现金支付去掉找零
                sales.ssje = Cash;
                sales.jsfsbh = "0";

                salesList.Add(sales);
            }
            else
            {
                foreach (PayedCard payedCard in payedCardList)
                {
                    sales = GetPayFromSalesList(salesList);

                    sales.ssje = payedCard.je;
                    sales.jsfsbh = payedCard.jsfsbh;

                    salesList.Add(sales);
                }
            }

            return salesList;
        }

        /// <summary>
        /// 添加一条支付方式记录
        /// </summary>
        /// <param name="salesList">商品销售记录</param>
        /// <returns></returns>
        public Sales GetPayFromSalesList(List<Sales> salesList)
        {
            Sales sales = new Sales();

            //深复制商品销售记录中的第一个元素
            sales = ObjectCopier.Clone<Sales>(salesList[0]);
            //spbh可以为此票中任意商品编码，使用第一个商品的
            //sales.spbh = salesList[0].spbh;
            //sales.txm = salesList[0].txm;
            //sales.jyj = salesList[0].jyj;
            //sales.jysl = salesList[0].jysl;
            //sales.xsrq = salesList[0].xsrq;
            //sales.xssj = salesList[0].xssj;
            //sales.syybh = salesList[0].syybh;
            //sales.xph = salesList[0].xph;
            //sales.jh = salesList[0].jh;
            //sales.yj = salesList[0].yj;
            //sales.mcard = salesList[0].mcard;
            //sales.yhmx = salesList[0].yhmx;
            //sales.jfmx = salesList[0].jfmx;
            //sales.szbz = salesList[0].szbz;
            //sales.cxbh = salesList[0].cxbh;
            //sales.cxid = salesList[0].cxid;
            //sales.Baka = "0";
            //sales.Bakb = 1;

            //记录各种支付方式金额的是 253
            sales.tzm = "253";

            //默认收银抹零是0；
            sales.zjj = 0;

            //小票内部序号
            sales.xpxh = salesList[salesList.Count - 1].xpxh + 1;

            return sales;
        }

        /// <summary>
        /// 获得规范的小票号格式
        /// </summary>
        /// <returns></returns>
        public string GetXPH()
        {
            string xph = GlobalParams.xph.ToString();
            for (int i = 0; i < (8 - GlobalParams.xph.ToString().Length); i++)
                xph = "0" + xph;

            return xph;
        }

        /// <summary>
        /// 序列化当日小票单
        /// </summary>
        /// <param name="order"></param>
        public void SerializeReceiptList(List<Receipt> rList)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream output = File.Create(GlobalParams.recLocalPath))
                {
                    formatter.Serialize(output, rList);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, ex.Source, "序列化当日小票单");
#if DEBUG
                throw ex;
#endif
            }
        }

        #endregion
    }
}
