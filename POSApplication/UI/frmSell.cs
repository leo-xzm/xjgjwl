﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POSApplication.Model;
using POSApplication.UserController;
using POSApplication.BLL;
using POSApplication.Common;
using System.IO.Ports;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;

namespace POSApplication.UI
{
    public partial class frmSell : Form
    {
        #region 属性
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

        /// <summary>
        /// 读卡器
        /// </summary>
        private MwrfRead mr;

        /// <summary>
        /// 商品重量
        /// </summary>
        private string weight;
        private decimal weighttodecimal = 0;


        //购物单
        private List<OrderItem> order;

        //datagridview的序号和order的可能不一致（无效的记录不显示在dgv里）
        //商品小窗选择的商品序号
        private int goodsIndex = -1;
        //购物单中的排序号
        private int orderIndex = 0;

        //商品小窗体
        private GoodsFrame goodsFrame;

        //自定义输入框（密码和整单打折）
        private InputBox inputBox;

        //输入密码后要进行的操作
        private OrderHandleType pwdFor;

        /// <summary>
        /// 扫描平台串口
        /// </summary>
        private SerialPort serialPortCOM1;

        /// <summary>
        /// 扫描枪串口
        /// </summary>
        private SerialPort serialPortCOM7;

        private string barcode;

        /// <summary>
        /// 是否将条码输入到文本框
        /// </summary>
        private bool isWrite;

        /// <summary>
        /// 客显串口
        /// </summary>
        public SerialPort DisplayPort { get; set; }

        /// <summary>
        ///读取会员卡的进程
        /// </summary>
        public Thread readCard;
        /// <summary>
        /// 读卡设备标识
        /// </summary>
        public int icdev;

        /// <summary>
        /// 总计
        /// </summary>
        public decimal MoneyTotal { get; set; }

        /// <summary>
        /// 优惠（促销）
        /// </summary>
        public decimal YH { get; set; }

        /// <summary>
        /// 优惠（打折）
        /// </summary>
        public decimal DZ { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public decimal GoodsCount { get; set; }

        /// <summary>
        /// 会员卡
        /// </summary>
        public string Membercard;
        public string tempCard;

        /// <summary>
        /// 会员名
        /// </summary>
        public string Mname;

        /// <summary>
        /// 卡余额
        /// </summary>
        public decimal Balance;

        /// <summary>
        /// 卡余额
        /// </summary>
        public Boolean isCash;

        /// <summary>
        /// 会员卡标识
        /// </summary>
        public string cardflag { get; set; }

        private ICustomerDisplayer custDisplayer = ObjFactory.CreateCustomerDisplayer();
        #endregion

        public frmSell(Staff staff, frmMain main)
        {
            InitializeComponent();
            this.staff = staff;
            this.main = main;
            order = new List<OrderItem>();
            Membercard = "";
            tempCard = "";
            cbbScanType.Enabled = false;
            //绑定小键盘事件，默认绑定条码输入框
            miniKeyboard.Press += MiniKeyboardHandler_CodeInput;
        }

        private void frmSell_Load(object sender, EventArgs e)
        {
            //设置扫描枪
            IniScanPort();
            BHS_Conn();
            cbbScanType.Items.Add("扫描平台");



            //label2.Visible = false;
            //cbbScanType.Visible = false;

            //todo临时取消扫描枪

            // cbbScanType.Items.Add("扫描枪");

            //显示扫描方式
            switch (ConfigHelper.GetAppConfig("BHS_Com"))
            {
                case "COM1":
                    cbbScanType.SelectedItem = "扫描平台";
                    break;
                case "COM7":
                    cbbScanType.SelectedItem = "扫描枪";
                    break;
                default:
                    cbbScanType.SelectedItem = "";
                    break;
            }

            //设置客显串口
            DisplayPort = custDisplayer.InitSettings;
            custDisplayer.CLS(DisplayPort);

            //开启读卡器监听线程
            mr = new MwrfRead();
            try
            {
                readCard = new Thread(new ThreadStart(RfListen));
                readCard.IsBackground = true;
                readCard.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //加载底部信息栏
            BottomBar bottom = new BottomBar(staff);
            Controls.Add(bottom);
            bottom.Location = new Point(0, 575);
            bottom.Show();

            //禁用功能按钮
            btnDel.Enabled = false;
            btnAlter.Enabled = false;
            btnDiscount.Enabled = false;
            btnDiscountOverall.Enabled = false;
            btnCancel.Enabled = false;
            btnSuspend.Enabled = false;

            //读取临时保存的购物单
            try
            {
                LoadOrder();
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, ex.Source, "销售界面初始化读取临时保存的购物单");
            }
        }

        #region 小键盘绑定
        //存放小键盘所按下的键值
        private string keyInput;

        //小键盘绑定条码输入框的事件
        private void MiniKeyboardHandler_CodeInput(object sender, MiniKeyboardArgs e)
        {
            keyInput = e.KeyCode;
            Keyboard_CodeInput(txtCode);
        }

        //小键盘绑定会员卡输入框的事件
        private void MiniKeyboardHandler_CardInput(object sender, MiniKeyboardArgs e)
        {
            keyInput = e.KeyCode;
            Keyboard_CodeInput(txtCardID);
        }

        //小键盘绑定商品小窗体的事件
        public void MiniKeyboardHandler_GoodsFrameInput(object sender, MiniKeyboardArgs e)
        {
            keyInput = e.KeyCode;
            Keyboard_GoodsFrameInput(goodsFrame.type);
        }

        //小键盘绑定自定义输入框的事件
        public void MiniKeyboardHandler_Input(object sender, MiniKeyboardArgs e)
        {
            keyInput = e.KeyCode;
            Keyboard_Input(inputBox.type);
        }

        /// <summary>
        /// 界面输入框按键绑定：输入条码时，确定键刷新购物单列表；输入会员卡时，确定键聚焦条码输入框，都不能输入小数点
        /// </summary>
        /// <param name="focusing">当前焦点输入框</param>
        private void Keyboard_CodeInput(TextBox focusing)
        {

            //如果 txtCode 没有焦点，输入的字符会被选中
            if (focusing == txtCode)
                focusing.Focus();

            //退格
            if (keyInput == miniKeyboard.Backspace)
            {
                InputValidate.InputBackspace(focusing);
            }
            //按确定，刷新商品列表
            else if (keyInput == miniKeyboard.KeyEnter)
            {
                //输入条码时，确定键刷新购物单列表
                if (focusing == txtCode)
                {
                    goodsIndex = -1;
                    dgvGoods_Add(0, 0);
                }
                //输入会员卡时，确定键聚焦条码输入框
                else
                {
                    //隐藏会员卡输入框，给标签赋值
                    lbCard.Text = "会员卡：" + focusing.Text;
                    Membercard = focusing.Text;
                    tempCard = focusing.Text;

                    txtCode.Enabled = true;
                    txtCode.Focus();
                    txtCode.SelectionStart = txtCode.TextLength;

                    focusing.Hide();
                    return;
                }
            }
            //按取消，条码输入框清空输入框，会员卡输入框重置内容并隐藏
            else if (keyInput == miniKeyboard.Cancel)
            {
                //聚焦条码时，取消键清除列表中的选择
                if (focusing == txtCode)
                {
                    focusing.Text = "";
                    ClearSelection();
                }
                else if (focusing == txtCardID)
                {
                    focusing.Hide();
                    return;
                }
            }
            //其他键直接输入，会员卡输入框不能输入 . 和 X
            else
            {
                if (focusing == txtCode || (focusing == txtCardID && keyInput != miniKeyboard.Dot && keyInput != miniKeyboard.X))
                {
                    if (focusing.SelectedText != "")
                        focusing.SelectedText = keyInput;
                    else
                        focusing.SelectedText += keyInput;
                }
            }

            focusing.Focus();
        }

        /// <summary>
        /// 商品小窗输入框按键绑定
        /// </summary>
        /// <param name="type"></param>
        private void Keyboard_GoodsFrameInput(OrderHandleType type)
        {
            TextBox focusing = new TextBox();
            OrderItem item = new OrderItem();

            //删除
            if (type == OrderHandleType.delete)
            {
                if (keyInput == miniKeyboard.Cancel)
                {
                    goodsFrame.Hide();
                }
                else if (keyInput == miniKeyboard.KeyEnter)
                {
                    //-1说明没选
                    if (goodsIndex >= 0)
                    {
                        order[orderIndex].isValid = false;

                        //深复制
                        item = ObjectCopier.Clone<OrderItem>(order[orderIndex]);

                        //加负记录，数量设为负，小票序号增加
                        order.Add(item);
                        item.xpxh = order.Count;

                        decimal copyXJ = item.XJ;

                        //item.JYJ *= -1;
                        item.JYSL *= -1;

                        //增加 称重商品ssje要直接取负，否则会按数量*单价计算，有误差
                        item.XJ = copyXJ * -1;

                        //删除商品列表的选择
                        goodsIndex = -1;

                        dgvGoods_DataBind(order);
                        ClearSelection();
                        goodsFrame.Hide();

                        //写日志
                        Log.WriteDeleteLog(staff.Staff_name, sup.Staff_name, orderIndex + 1);
                    }
                }
            }
            //修改数量和打折
            else
            {
                //确定焦点框
                if (type == OrderHandleType.alter)
                    focusing = (TextBox)goodsFrame.Controls.Find("txtCount", false).FirstOrDefault();
                else
                    focusing = (TextBox)goodsFrame.Controls.Find("txtDiscount", false).FirstOrDefault();

                //退格
                if (keyInput == miniKeyboard.Backspace)
                {
                    InputValidate.InputBackspace(focusing);
                }
                //确定
                else if (keyInput == miniKeyboard.KeyEnter)
                {
                    //-1说明没选
                    if (goodsIndex >= 0)
                    {
                        item = order[orderIndex];

                        //选定商品修改数量的逻辑
                        if (type == OrderHandleType.alter)
                        {
                            if (Convert.ToDecimal(focusing.Text == "" ? "0" : focusing.Text) <= 0)
                                MessageBox.Show("必须大于0");
                            else
                            {
                                //更新购物单数量（小计在属性set中修改）
                                item.JYSL = Convert.ToDecimal(focusing.Text);

                                dgvGoods_DataBind(order);
                                goodsFrame.Hide();
                            }
                        }
                        //选定商品打折的逻辑
                        else if (type == OrderHandleType.discount)
                        {
                            //更新购物单单品打折（小计在属性set中修改）
                            item.Discount = focusing.Text;

                            dgvGoods_DataBind(order);
                            goodsFrame.Hide();

                            //写日志
                            Log.WriteDiscountLog(staff.Staff_name, sup.Staff_name, orderIndex + 1, item.Discount);
                        }
                    }
                }
                //按取消，隐藏商品小窗
                else if (keyInput == miniKeyboard.Cancel)
                {
                    goodsFrame.Hide();
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
                #region 如果是输入密码，根据 pwdFor 存储的 type 确定要打开的商品小窗类型
                {
                    string pwd = focusing.Text;
                    //根据密码获取主管
                    sup = new StaffBLL().GetStaffByPwd(pwd);
                    if (sup != null)
                    {
                        OrderItem item = new OrderItem();
                        switch (pwdFor)
                        {
                            case OrderHandleType.alter:
                                inputBox.Hide();
                                item = order[orderIndex];
                                GoodsFrameShow(item, OrderHandleType.alter);
                                break;
                            case OrderHandleType.delete:
                                inputBox.Hide();
                                item = order[orderIndex];
                                GoodsFrameShow(item, OrderHandleType.delete);
                                break;
                            case OrderHandleType.discount:
                                inputBox.Hide();
                                item = order[orderIndex];
                                GoodsFrameShow(item, OrderHandleType.discount);
                                break;
                            case OrderHandleType.discountOverall:
                                inputBox.Hide();
                                InputBoxShow(OrderHandleType.discountOverall);
                                break;
                            case OrderHandleType.deleteAll://整单取消
                                {
                                    inputBox.Hide();

                                    DialogResult dr = MessageBox.Show("确定要整单取消吗？", "整单取消确认", MessageBoxButtons.YesNo);
                                    if (dr == DialogResult.Yes)
                                    {
                                        if (DeleteOrder())
                                            MessageBox.Show("整单取消成功！");
                                        else
                                            MessageBox.Show("整单取消失败！");
                                    }
                                    break;
                                }
                        }
                    }
                    else
                    {
                        MessageBox.Show("密码不正确");
                    }
                }
                #endregion

                else if (inputBox.type == OrderHandleType.discountOverall)
                #region 整单打折
                {
                    if (order.Count > 0)
                    {
                        foreach (OrderItem item in order.Where(o => o.isValid == true).ToList())
                        {
                            //更新购物单单品打折（小计在属性set中修改）
                            item.Discount = focusing.Text;
                        }
                        dgvGoods_DataBind(order);
                        ClearSelection();
                        inputBox.Hide();

                        //写日志
                        Log.WriteDiscountOverAllLog(staff.Staff_name, sup.Staff_name, focusing.Text);
                    }
                    else
                        MessageBox.Show("购物单中没有商品，无法进行整单打折！");
                }
                #endregion
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
        /// 整单取消
        /// </summary>
        /// <returns></returns>
        private bool DeleteOrder()
        {
            bool result = true;

            //有效商品不存在时，整单取消失败
            if (order.Where(o => o.isValid == true).ToList().Count < 1)
                result = false;
            else
            {
                int count = order.Count;
                int deleteCount = 0;

                for (int i = 0; i < count; i++)
                {
                    order[i].yhmx = 0;
                    order[i].cxid = "";
                    order[i].cxbh = "";
                    order[i].Mcard = "";
                    OrderItem item = order[i];
                    if (item.isValid)
                    {
                        OrderItem itemNegative = new OrderItem();

                        //深复制
                        itemNegative = ObjectCopier.Clone<OrderItem>(item);
                        //防止称重商品根据单价和重量重新计算小计
                        decimal copyXj = itemNegative.XJ;
                        //加负记录，数量设为负，小票序号增加
                        order.Add(itemNegative);
                        deleteCount++;
                        itemNegative.xpxh = count + deleteCount;
                        //item.JYJ *= -1;
                        itemNegative.JYSL *= -1;
                        itemNegative.XJ = copyXj * -1;
                    }
                }

                //保存到本地数据库
                List<Sales> sList = new frmPay(staff, this).GetSalesList(order);

                //更新
                new DataOperator().UpdateSalesData(sList);

                //前后台交互
                DataUpload.Upload(string.Format("小票号：{0}", GlobalParams.xph));

                //写日志
                Log.WriteDeleteLog(staff.Staff_name, sup.Staff_name);

                //小票号递增，记录格式
                GlobalParams.xph++;
                ConfigHelper.UpdateAppConfig("XPH", DateTime.Now.ToString("yyyyMMdd") + "#" + GlobalParams.xph);

                //清空缓存
                File.Delete(GlobalParams.orderPath);

                frmSell_VisibleChanged(null, null);
            }
            return result;
        }
        #endregion

        #region 功能按钮
        //返回主界面按钮
        private void btnReturn_Click(object sender, EventArgs e)
        {
            BHS_Disconn();

            Display_Disconn();

            CardReader_Disconn();

            main.Show();
            this.Dispose();
        }

        //修改商品数量按钮，打开商品小窗-修改数量模式
        private void btnAlter_Click(object sender, EventArgs e)
        {
            if (goodsIndex < 0)
            {
                MessageBox.Show("请选择要修改的商品");
                return;
            }

            OrderItem item = new OrderItem();
            item = order[orderIndex];

            GoodsFrameShow(item, OrderHandleType.alter);

            ////输入密码后的操作为修改商品数量
            //pwdFor = OrderHandleType.alter;
            ////打开自定义输入窗-密码模式
            //InputBoxShow(OrderHandleType.password);
        }

        // 删除商品按钮，打开自定义输入窗-密码模式
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (goodsIndex < 0)
            {
                MessageBox.Show("请选择要删除的商品");
                return;
            }

            //输入密码后的操作为商品删除
            pwdFor = OrderHandleType.delete;
            //打开自定义输入窗-密码模式
            InputBoxShow(OrderHandleType.password);
        }

        //打折按钮，打开自定义输入窗-密码模式
        private void btnDiscount_Click(object sender, EventArgs e)
        {
            if (goodsIndex < 0)
            {
                MessageBox.Show("请选择要打折的商品");
                return;
            }

            //输入密码后的操作为选定商品打折
            pwdFor = OrderHandleType.discount;
            //打开自定义输入窗-密码模式
            InputBoxShow(OrderHandleType.password);
        }

        //整单打折按钮，打开自定义输入窗-密码模式
        private void btnDiscountOverall_Click(object sender, EventArgs e)
        {
            //输入密码后的操作为选定商品打折
            pwdFor = OrderHandleType.discountOverall;
            //打开自定义输入窗-整单打折模式
            InputBoxShow(OrderHandleType.password);
        }

        // 购物单整单取消，加负数量
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //输入密码后的操作为商品删除
            pwdFor = OrderHandleType.deleteAll;

            //打开自定义输入窗-密码模式
            InputBoxShow(OrderHandleType.password);
        }

        // 挂单按钮
        private void btnSuspend_Click(object sender, EventArgs e)
        {
            if (File.Exists(GlobalParams.orderPath))
            {
                DialogResult dr = MessageBox.Show("要挂起当前购物单吗？", "挂单确认", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                    File.Move(GlobalParams.orderPath, GlobalParams.susPath);
            }

            frmSell_VisibleChanged(null, null);
        }

        //结账按钮
        private void btnPay_Click(object sender, EventArgs e)
        {
            //如果购物车都删了，还是会有串行化的文件，留到下一张小票的销售数据里
            //购物车不为空（存在有效商品）时，才能结账
            if (order.Count(o => o.isValid == true) > 0)
            {
                BHS_Disconn();
                //处理促销的情况
                YH = new PromotionBLL().PromotionProcessor(ref order);
                //this.Invoke(new OrderSerial(SerializeOrder), order);
                SerializeOrder(order);
                bool isYgCard = string.IsNullOrWhiteSpace(txtCardID.Text);

                #region /*把打折的已减优惠传过去显示出来2017-7-27 注释说明：前后逻辑关联，暂定不能修改，否则和老后台对不上*/
                /*
                decimal totalPrice = 0, dzTotalPrice = 0;
                foreach (OrderItem oi in order)
                {
                    totalPrice += (oi.JYSL * oi.JYJ);
                    if (string.IsNullOrEmpty(oi.Discount))
                    {
                        oi.Discount = "1";
                    }
                    dzTotalPrice += ((oi.JYSL * oi.JYJ) * decimal.Parse(oi.Discount));
                }
                DZ = Math.Round(totalPrice - dzTotalPrice, 1);//打折优惠
                */
                #endregion

                frmPay pay = new frmPay(staff, this);
                pay.Show();

                this.Hide();
            }
        }
        #endregion

        #region 购物单绑定数据
        /// <summary>
        /// 读取条码框条码，允许6位货号或大于13位条形码，购物单绑定商品数据
        /// </summary>
        /// <param name="number">手动指定数量</param>
        /// <param name="price">手动指定价格</param>
        private void dgvGoods_Add(int number, decimal price)
        {
            string code = txtCode.Text;
            // MessageBox.Show(code);
            string oldcode = code;
            // MessageBox.Show(code);
            if (code == string.Empty)
            {

                MessageBox.Show("请输入条码或货号");
                txtCode.Focus();
                return;
            }

            Goods goods = new Goods();

            //生鲜码是24开头的18位条形码
            if (code.Length == 18 && code.Substring(0, 2) == "24")
            {
                weight = code.Substring(12, 5);
                code = code.Substring(0, 12) + code.Substring(code.Length - 1, 1);

                if (weight.Substring(2, 3) == "000")
                {
                    weighttodecimal = Convert.ToDecimal(weight.Substring(0, 2)) + 1 / 1000;
                }
                else
                {
                    weighttodecimal = Convert.ToDecimal(weight.Substring(0, 2)) + Convert.ToDecimal(weight.Substring(2, 3)) / 1000;
                }
            }

            //称重码是 24开头的13位条形码
            if (code.Length == 13 && code.Substring(0, 2) == "24")
            {
                string goodsCode = code.Substring(2, 5);
                List<char> list = ("2000000" + goodsCode).ToList();
                List<int> ean13 = new List<int>();
                foreach (char a in list)
                {
                    ean13.Add(Convert.ToInt32(a.ToString()));

                }

                //获取校验位
                int checkCode = Convert.ToInt32(code.Substring(12, 1));
                //根据EAN-13规则计算内码
                int result = 10 - (((ean13[0] + ean13[2] + ean13[4] + ean13[6] + ean13[8] + ean13[10])
                    + (ean13[1] + ean13[3] + ean13[5] + ean13[7] + ean13[9] + ean13[11]) * 3) % 10);
                if (result == 10)
                {
                    result = 0;
                }
                //if (result != checkCode)
                //{
                //    MessageBox.Show("称重码校验失败");
                //    txtCode.Focus();
                //    return;
                //}
                //else
                //{
                // goodsCode = goodsCode + checkCode;
                goodsCode = goodsCode + result;
                goods = new GoodsBLL().GetGoodsByCID(goodsCode);
                //称重码自带价格
                if (code.Substring(10, 2) == "00")
                {
                    price = (Convert.ToDecimal(code.Substring(7, 3)) + 1 / 100) / (weighttodecimal * 2);
                    price = Math.Round(price, 2);
                }
                else
                {

                    price = (Convert.ToDecimal(code.Substring(7, 3)) + Convert.ToDecimal(code.Substring(10, 2)) / 100) / (weighttodecimal * 2);
                    price = Math.Round(price, 2);
                }
                ////测试
                //    MessageBox.Show(goodsCode);
                //    MessageBox.Show(weighttodecimal.ToString());
                //    MessageBox.Show(price.ToString());
                if (goods == null)
                {
                    MessageBox.Show("没有找到商品：" + oldcode);
                    txtCode.Clear();
                    weighttodecimal = 0;
                    return;
                }


                //}
            }
            else
            {
                //只有6位就是货号
                if (code.Length == 6)
                    goods = new GoodsBLL().GetGoodsByCID(code);
                else
                    goods = new GoodsBLL().GetGoodsByCode(code);

                if (goods == null)
                {
                    MessageBox.Show("没有找到商品：" + code);
                    txtCode.Clear();

                    return;
                }
            }

            //根据商品模型初始化销售数据模型
            OrderItem oi = new OrderItem(goods);
            //if (weighttodecimal > 0) {
            //    oi.JYJ = weighttodecimal;
            //}
            //会员卡，有会员卡，JYJ将使用 m_sell_price；没有就使用 sell_price
            oi.Mcard = txtCardID.Text;
            Membercard = txtCardID.Text;
            oi.Mname = Mname;
            oi.Balance = Balance;

            //是否手动指定数量和价格
            if (number > 0)
                oi.JYSL = number;
            if (price > 0)
                oi.JYJ = price;
            if (weighttodecimal > 0)
            {
                oi.JYSL = Math.Round(weighttodecimal * 2, 2);
                weighttodecimal = 0;
            }

            //18位称重条形商品总价直接截取获得5位总价
            if (oldcode.Length == 18)
            {
                oi.XJ = (Convert.ToDecimal(oldcode.Substring(7, 5))) / 100;
                //测试
                //MessageBox.Show(oi.XJ.ToString());
            }
            //获取时段促销的打折
            oi.Discount = new DiscountBLL().GetDiscountByCID(oi.spbh);

            OrderItem item = order.Where(o => o.txm == oi.txm && o.isValid == true).FirstOrDefault();

            //称重商品允许单价不一样
            if (oldcode.Length != 18)
            {
                if (item != null && item.JYJ != oi.JYJ)
                {
                    MessageBox.Show("已扫商品不能再指定其它价格！");
                    return;
                }
            }

            //获取到会员名表示是内部员工卡，此时购买商品总金额不得超过卡内余额
            //刷员工卡现金支付的时候不做余额判断


            if (!isCash)
            {

                if (Mname != "" && oi.XJ + MoneyTotal > Balance)
                {
                    // MessageBox.Show(Mname+  Balance);
                    MessageBox.Show(string.Format("总金额将达到：{0} 超出卡内余额！", oi.XJ + MoneyTotal),
                        "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            ////测试
            ////测试
            //MessageBox.Show(oi.XJ.ToString());

            //购物单里有条码重复商品时，直接增加其数量和金额；否则就加入购物单
            RepeatGoodsFilter(oi, order);

            //判断是不是刷会员卡现金支付,有商品时不允许再操作


            //记录小票序号
            oi.xpxh = order.Count;

            //添加商品后停止读卡器线程
            //this.Invoke(new Action(CardReader_Disconn));
            // MessageBox.Show("加载数据");
            dgvGoods_DataBind(order);
            ClearSelection();

            //顾客显示牌显示单价
            CustomerDisplayData data = new CustomerDisplayData();
            data.Price = oi.JYJ.ToString();
            data.StatusLight = 1;
            custDisplayer.Display(DisplayPort, data);
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="order"></param>
        private void dgvGoods_DataBind(List<OrderItem> order)
        {
            //list 直接绑定给 datagridview不会刷新数据，要使用 BindingList
            //只显示标记显示的记录
            dgvGoods.DataSource = new BindingList<OrderItem>(order.Where(m => m.isValid == true).ToList());
            //int a = dgvGoods.Rows.Count;
            //dgvGoods.Rows[a-1].
            dgvGoods.Columns["xpxh"].Visible = false;
            dgvGoods.Columns["txm"].Visible = false;
            dgvGoods.Columns["number"].Visible = false;
            dgvGoods.Columns["Mcard"].Visible = false;
            dgvGoods.Columns["Mname"].Visible = false;
            dgvGoods.Columns["Balance"].Visible = false;
            dgvGoods.Columns["YJ"].Visible = false;
            dgvGoods.Columns["sell_price"].Visible = false;
            dgvGoods.Columns["m_sell_price"].Visible = false;
            dgvGoods.Columns["jf_rate"].Visible = false;
            dgvGoods.Columns["isValid"].Visible = false;
            dgvGoods.Columns["yhmx"].Visible = false;
            dgvGoods.Columns["cxbh"].Visible = false;
            dgvGoods.Columns["cxid"].Visible = false;



            dgvGoods.RowsDefaultCellStyle.Font = new Font("宋体", 12, FontStyle.Regular);

            #region 列绑定写设计界面里，没有数据也能看到
            //dgvGoods.Columns["spbh"].HeaderText = "货号";
            //dgvGoods.Columns["spbh"].Width = 70;
            //dgvGoods.Columns["spbh"].ReadOnly = true;
            //dgvGoods.Columns["Name"].HeaderText = "商品名称";
            //dgvGoods.Columns["Name"].Width = 160;
            //dgvGoods.Columns["Name"].ReadOnly = true;
            //dgvGoods.Columns["dj"].HeaderText = "单价";
            //dgvGoods.Columns["dj"].Width = 80;
            //dgvGoods.Columns["dj"].ReadOnly = true;
            //dgvGoods.Columns["jysl"].HeaderText = "数量";
            //dgvGoods.Columns["jysl"].Width = 65;
            //dgvGoods.Columns["jysl"].ReadOnly = true;
            //dgvGoods.Columns["jyj"].HeaderText = "小计";
            //dgvGoods.Columns["jyj"].Width = 80;
            //dgvGoods.Columns["jyj"].ReadOnly = true;
            //dgvGoods.Columns["discount"].HeaderText = "打折";
            //dgvGoods.Columns["discount"].Width = 65;
            //dgvGoods.Columns["discount"].ReadOnly = true;
            //dgvGoods.Columns["discount"].DefaultCellStyle.Format = "N2";//打折显示两位小数
            //dgvGoods.RowHeadersVisible = false;
            #endregion

            //先去掉默认的选择
            dgvGoods.ClearSelection();

            //如果之前有选择的行，保持选择；否则就显示最后一行

            //保持列表显示到最后一行
            //if (order.Count > 0)
            //{
            //    if (goodsIndex >= 0)
            //    {
            //        dgvGoods.Rows[goodsIndex].Selected = true;

            //        dgvGoods.CurrentCell = dgvGoods.Rows[goodsIndex].Cells[1];
            //    }
            //    else if (goodsIndex != -1)//可能会删到空
            //        //保证每行高度一样，不然定位不准确；滚动条的位置应该是根据行数和高度算出来的
            //        dgvGoods.FirstDisplayedScrollingRowIndex = dgvGoods.Rows.Count - 1;
            //}

            if (dgvGoods.Rows.Count > 0)
            {
                //dgvGoods.Rows[dgvGoods.Rows.Count - 1].Selected = true;
                dgvGoods.CurrentCell = dgvGoods.Rows[dgvGoods.Rows.Count - 1].Cells[0];
            }

            CountTotalData(order);

            //序列化
            if (order.Count > 0)
                //this.Invoke(new OrderSerial(SerializeOrder), order);
                SerializeOrder(order);
        }

        /// <summary>
        /// 读取临时保存的购物单
        /// </summary>
        /// <returns>true：有临时的 order 文件，读取成功；false：没有临时的 order 文件，或者文件里没有数据</returns>
        public bool LoadOrder()
        {
            bool result = true;

            if (File.Exists(GlobalParams.orderPath))
            {
                order = new List<OrderItem>();

                BinaryFormatter formatter = new BinaryFormatter();
                //反序列化
                using (Stream input = File.OpenRead(GlobalParams.orderPath))
                {
                    if (input.Length > 0)
                        order = (List<OrderItem>)formatter.Deserialize(input);
                }

                //成功读取到临时购物单
                if (order.Count > 0)
                {
                    txtCardID.Text = order[0].Mcard;
                    Membercard = order[0].Mcard;
                    tempCard = order[0].Mcard;
                    Mname = order[0].Mname;
                    Balance = order[0].Balance;

                    //lbCard.Text = "会员卡：" + txtCardID.Text;
                    if (Membercard != "")
                    {
                        if (Mname != "")
                            lbCard.Text = "持卡人：" + Mname + "  余额：" + Balance;
                        else
                            lbCard.Text = "非内部员工卡";

                        lbCard.Show();
                    }
                    else
                        lbCard.Hide();

                    dgvGoods_DataBind(order);
                    //MessageBox.Show("数据绑定成功开始清空");
                    ClearSelection();
                }
                else
                    result = false;
            }
            else
                result = false;

            return result;
        }

        /// <summary>
        /// 购物单刷新，取消选择商品，重新设定功能键状态
        /// </summary>
        private void ClearSelection()
        {
            txtCode.Text = "";
            dgvGoods.ClearSelection();

            goodsIndex = -1;
            btnAlter.Enabled = false;
            btnDel.Enabled = false;
            btnDiscount.Enabled = false;
            if (order.Count > 0)
            {
                btnDiscountOverall.Enabled = true;
                btnCancel.Enabled = true;
                btnSuspend.Enabled = true;
            }
            else
            {
                btnDiscountOverall.Enabled = false;
                btnCancel.Enabled = false;
                btnSuspend.Enabled = false;
            }
        }

        /// <summary>
        /// 重复的商品只累加数量和价格
        /// </summary>
        /// <param name="oi"></param>
        /// <param name="order"></param>
        private void RepeatGoodsFilter(OrderItem oi, List<OrderItem> order)
        {
            //重复标识
            bool isRepeat = false;
            int dgvIndex = 0;//dgv显示的都是有效商品，记录有效商品在dgv的序号

            //去重，加数量
            foreach (OrderItem item in order.Where(o => o.isValid == true).ToList())
            {
                //dgvIndex++;
                //goodsIndex = dgvIndex;

                //条码重复的，加数量，跳出循环
                //涉及到称重商品数量变化的时候都不能数量乘以单价算总计
                if (item.txm == oi.txm)
                {
                    item.XJ += oi.XJ;
                    decimal copyXJ = item.XJ;
                    item.JYSL += oi.JYSL;
                    item.XJ = copyXJ;
                    isRepeat = true;
                    goodsIndex = dgvIndex;
                    dgvIndex++;
                    break;
                }

                else
                    isRepeat = false;
            }
            ////测试
            //MessageBox.Show(oi.XJ.ToString());
            if (!isRepeat)
                order.Add(oi);
        }

        /// <summary>
        /// 点击购物单列表单元格，选择商品，记录小票号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvGoods_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                goodsIndex = e.RowIndex;
                //所选商品的购物单排序号，xpxh 从1开始的，要减去1
                orderIndex = Convert.ToInt32(dgvGoods.Rows[e.RowIndex].Cells["xpxh"].Value) - 1;
                dgvGoods.Rows[e.RowIndex].Selected = true;

                //启用功能键
                btnAlter.Enabled = true;
                btnDel.Enabled = true;
                btnDiscount.Enabled = true;

                //把选择的商品的条码显示到 txtCode
                //txtCode.Text = dgvGoods.Rows[e.RowIndex].Cells["txm"].Value.ToString();
            }
        }

        /// <summary>
        /// 更新购物单总金额和总数量，并更新客显
        /// </summary>
        /// <param name="order"></param>
        private void CountTotalData(List<OrderItem> order)
        {
            MoneyTotal = 0;
            GoodsCount = 0;

            if (order.Count > 0)
                foreach (OrderItem item in order.Where(o => o.isValid == true).ToList())
                {
                    MoneyTotal += item.XJ;
                    //GoodsCount += item.JYSL;
                    if (item.JYSL * 100 % 100 == 0)
                    {
                        GoodsCount += item.JYSL;
                    }
                    else
                    {
                        GoodsCount += 1;
                    }
                }

            lbMoneyTotal.Text = MoneyTotal.ToString();
            lbGoodsCount.Text = GoodsCount.ToString();

            //金额大于10w和100w时，缩小字体
            if (lbMoneyTotal.Text.Length > 8)
                lbMoneyTotal.Font = new Font("宋体", 25);
            if (lbMoneyTotal.Text.Length > 9)
                lbMoneyTotal.Font = new Font("宋体", 20);

            //发送到客显
            try
            {
                /*
                //string str = string.Format(" Money: {0}" + Environment.NewLine + " Count: {1}" + Environment.NewLine
                //               + "   Pay: " + Environment.NewLine + "Change: ", MoneyTotal.ToString(), GoodsCount.ToString());
                string str = string.Format(" Money: {0}" + Environment.NewLine
                            + "   Pay: " + Environment.NewLine
                            + "Change: ", MoneyTotal.ToString());
                //string str = string.Format(" Money: {0}" + Environment.NewLine
                //           + " Count: {1}", MoneyTotal.ToString(), GoodsCount.ToString());

                custDisplayer.CLS(DisplayPort);
                Log.WriteErrorLog("清屏完毕开始发送数据  端口"+DisplayPort.PortName);
                Log.WriteErrorLog("清屏完毕开始发送数据  端口" + DisplayPort);
                custDisplayer.SendToDisplay(DisplayPort, str);*/

                CustomerDisplayData data = new CustomerDisplayData();
                data.StatusLight = 2;
                data.Total = MoneyTotal.ToString();
                custDisplayer.Display(DisplayPort, data);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, ex.Source, "更新购物单总金额和总数量，发送到客显");
            }
        }

        #region 单点鼠标移动，模拟上滑和下滑

        Point _mousePoint;
        private void dgvGoods_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mousePoint.X = e.X;
                _mousePoint.Y = e.Y;
            }
        }

        private void dgvGoods_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Y - _mousePoint.Y > 20 && dgvGoods.FirstDisplayedScrollingRowIndex > 0)
                    dgvGoods.FirstDisplayedScrollingRowIndex--;

                if (e.Y - _mousePoint.Y < -20 && dgvGoods.FirstDisplayedScrollingRowIndex < dgvGoods.RowCount
                    && dgvGoods.FirstDisplayedScrollingRowIndex > -1)
                    dgvGoods.FirstDisplayedScrollingRowIndex++;

                if (e.X - _mousePoint.X > 80 && dgvGoods.FirstDisplayedScrollingColumnIndex > 0)
                    dgvGoods.FirstDisplayedScrollingColumnIndex--;
                //显示前六列
                if (e.X - _mousePoint.X < -80 && dgvGoods.FirstDisplayedScrollingColumnIndex <= 6)
                    dgvGoods.FirstDisplayedScrollingColumnIndex++;
            }
        }
        #endregion

        #endregion

        #region 会员卡输入框
        //触摸会员卡标签，显示会员卡输入框
        private void lbCard_Click(object sender, EventArgs e)
        {
            //会员卡应在扫码前输入
            //扫码后，不允许再输入会员卡
            if (order.Count == 0)
            {
                txtCardID.Text = tempCard;
                //txtCardID.Text = Membercard;

                txtCardID.Show();
                txtCardID.Focus();
            }
        }

        //会员卡输入框显示/隐藏时，禁用/启用其他控件
        private void txtCardID_VisibleChanged(object sender, EventArgs e)
        {
            //会员卡输入框显示，并绑定小键盘
            if (txtCardID.Visible)
            {
                txtCardIDBindingMinikeyboard();

                BHS_Disconn();
            }
            //会员卡输入框隐藏，启用所有控件，重新绑定小键盘
            else
            {
                foreach (Control control in Controls)
                    control.Enabled = true;
                miniKeyboard.Press -= MiniKeyboardHandler_CardInput;
                miniKeyboard.Press += MiniKeyboardHandler_CodeInput;

                BHS_Conn();
            }
        }

        //卡号输入框绑定小键盘：除了输入框和小键盘，禁用其余控件
        private void txtCardIDBindingMinikeyboard()
        {
            foreach (Control control in Controls)
                if (control != txtCardID && control != miniKeyboard)
                    control.Enabled = false;

            miniKeyboard.Press -= MiniKeyboardHandler_CodeInput;
            miniKeyboard.Press += MiniKeyboardHandler_CardInput;
        }
        #endregion

        #region 商品小窗体
        /// <summary>
        /// 显示商品小窗体
        /// </summary>
        /// <param name="item"></param>
        /// <param name="type"></param>
        private void GoodsFrameShow(OrderItem item, OrderHandleType type)
        {
            goodsFrame = new GoodsFrame(item, type);

            Controls.Add(goodsFrame);
            goodsFrame.BringToFront();

            Point location = new Point(8, 140);
            goodsFrame.Location = location;

            GoodsFrameBindingMinikeyboard();
            goodsFrame.VisibleChanged += new System.EventHandler(this.goodsFrame_VisibleChanged);

            TextBox foucsing = new TextBox();

            //根据模式，选择焦点输入框
            if (type == OrderHandleType.alter)
            {
                foucsing = (TextBox)goodsFrame.Controls.Find("txtCount", false).FirstOrDefault();
                foucsing.Focus();
                foucsing.SelectionStart = foucsing.TextLength;
            }
            else if (type == OrderHandleType.discount)
            {
                foucsing = (TextBox)goodsFrame.Controls.Find("txtDiscount", false).FirstOrDefault();
                foucsing.Focus();
                foucsing.SelectionStart = foucsing.TextLength;
            }

            BHS_Disconn();
        }

        /// <summary>
        /// 商品小窗体在显示/隐藏时，禁用/启用其他控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goodsFrame_VisibleChanged(object sender, EventArgs e)
        {
            //商品小窗显示，并绑定小键盘
            if (goodsFrame.Visible)
            {
                GoodsFrameBindingMinikeyboard();
                BHS_Disconn();
            }
            //商品小窗隐藏，启用所有控件，重新绑定小键盘
            else
            {
                foreach (Control control in Controls)
                    control.Enabled = true;
                miniKeyboard.Press -= MiniKeyboardHandler_GoodsFrameInput;
                miniKeyboard.Press += MiniKeyboardHandler_CodeInput;

                BHS_Conn();
                goodsFrame.Dispose();
            }
        }

        /// <summary>
        ///  商品小窗绑定小键盘：除了商品小窗和小键盘，禁用其余控件
        /// </summary>
        private void GoodsFrameBindingMinikeyboard()
        {
            foreach (Control control in Controls)
                if (control != goodsFrame && control != miniKeyboard)
                    control.Enabled = false;
            //重新绑定小键盘
            miniKeyboard.Press -= MiniKeyboardHandler_CodeInput;
            miniKeyboard.Press += MiniKeyboardHandler_GoodsFrameInput;
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

            Controls.Add(inputBox);
            inputBox.BringToFront();

            Point location = new Point(120, 140);
            inputBox.Location = location;

            InputBoxBindingMinikeyboard();
            inputBox.VisibleChanged += new System.EventHandler(this.inputBox_VisibleChanged);

            BHS_Disconn();
        }

        // 自定义输入窗在显示/隐藏时，禁用/启用其他控件
        private void inputBox_VisibleChanged(object sender, EventArgs e)
        {
            //自定义输入窗显示，并绑定小键盘
            if (inputBox.Visible)
            {
                InputBoxBindingMinikeyboard();
                BHS_Disconn();
            }
            //自定义输入窗隐藏，启用所有控件，重新绑定小键盘
            else
            {
                foreach (Control control in Controls)
                    control.Enabled = true;
                miniKeyboard.Press -= MiniKeyboardHandler_Input;
                miniKeyboard.Press += MiniKeyboardHandler_CodeInput;

                BHS_Conn();
                inputBox.Dispose();
            }
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
            miniKeyboard.Press -= MiniKeyboardHandler_CodeInput;
            miniKeyboard.Press += MiniKeyboardHandler_Input;
        }

        #endregion

        #region 扫描枪
        /// <summary>
        /// 初始化扫描端口
        /// </summary>
        private void IniScanPort()
        {
            serialPortCOM1 = new SerialPort("COM1");
            serialPortCOM1.BaudRate = 9600;
            serialPortCOM1.Parity = Parity.None;
            serialPortCOM1.DataBits = 8;
            serialPortCOM1.StopBits = StopBits.One;
            // MessageBox.Show("初始化成功");
            serialPortCOM1.DataReceived += new SerialDataReceivedEventHandler(ComDataReceived);

            serialPortCOM7 = new SerialPort("COM7");
            serialPortCOM7.BaudRate = 9600;
            serialPortCOM7.Parity = Parity.None;
            serialPortCOM7.DataBits = 8;
            serialPortCOM7.StopBits = StopBits.One;
            //MessageBox.Show("初始化成功");
            serialPortCOM7.DataReceived += new SerialDataReceivedEventHandler(ComDataReceived);

        }

        private delegate void BHS_Delegate();
        private void ComDataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            //MessageBox.Show(isWrite.ToString());

            if (isWrite)
                this.BeginInvoke(new BHS_Delegate(GetCodeFromBHS));
        }

        /// <summary>
        /// 异步执行方法，读取扫码条码
        /// </summary>
        public void GetCodeFromBHS()
        {
            int number = 0;
            decimal price = 0;

            if (txtCode.Text.Length > 0)
            {
                //手动输入数量 第一位是 X
                if (txtCode.Text.Substring(0, 1) == miniKeyboard.X)
                    number = Convert.ToInt32(txtCode.Text.Replace(miniKeyboard.X, ""));
                //手动输入价格 第一位是 .
                else if (txtCode.Text.Length > 0 && txtCode.Text.Substring(0, 1) == miniKeyboard.Dot)
                    price = Convert.ToDecimal(txtCode.Text.Substring(1));
            }

            barcode += serialPortCOM1.ReadExisting();
            barcode += serialPortCOM7.ReadExisting();
            // MessageBox.Show(barcode);
            //如果 barcode 是空的，或者如果不是空的，但是最后一位不是 0D (char)13，就返回
            //扫描平台每次读码，不是一次性向缓冲区存入数据，而是分两次到四次，所以要进行拼接操作

            // if (barcode == "" || barcode.Substring(barcode.Length - 1) != ((char)13).ToString())
            if (barcode.Length > 0)
            {

                if (barcode == "" || (barcode.Substring(barcode.Length - 1) != ((char)13).ToString() && barcode.Substring(barcode.Length - 1) != ((char)10).ToString()))
                {
                    return;
                }

                //MessageBox.Show("barcode");
                //if (barcode == "")
                //{
                //    MessageBox.Show(barcode.Substring(barcode.Length - 1));
                //    MessageBox.Show("空或不符合格式");
                //    return;
                //}
                else
                {
                    //扫码到条码输入框，去除回车符和换行符
                    for (int i = 0; i < 4; i++)
                    {
                        barcode = barcode.TrimEnd((char)13);
                        barcode = barcode.TrimEnd((char)10);
                    }
                    txtCode.Text = barcode;
                    // MessageBox.Show("执行add");
                    //加入购物单
                    dgvGoods_Add(number, price);
                    // MessageBox.Show("add执行完");
                    //清除条码输入框
                    txtCode.Clear();
                    barcode = "";
                    return;
                }
            }
        }

        /// <summary>
        /// 断开扫描枪
        /// </summary>
        public void BHS_Disconn()
        {
            isWrite = false;
            if (serialPortCOM1.IsOpen)
                serialPortCOM1.Close();
            if (serialPortCOM7.IsOpen)
                serialPortCOM7.Close();
        }

        /// <summary>
        /// 打开串口，连接扫描枪
        /// </summary>
        public void BHS_Conn()
        {



            isWrite = true;
            bool flag = true;

            //#if DEBUG
            //            flag = false;
            //#endif

            if (flag)
            {
                if (serialPortCOM1 != null)
                {
                    if (!serialPortCOM1.IsOpen)
                        serialPortCOM1.Open();
                    serialPortCOM1.DiscardInBuffer();
                }
                if (serialPortCOM7 != null)
                {
                    if (!serialPortCOM7.IsOpen)
                        serialPortCOM7.Open();
                    serialPortCOM7.DiscardInBuffer();
                }
            }
        }

        //修改扫描方式，扫描枪 COM7，扫描平台 COM1
        private void cbbScanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //serialPort.Dispose();

            //string typeName = cbbScanType.Items[cbbScanType.SelectedIndex].ToString();

            //switch (typeName)
            //{
            //    case "扫描平台":
            //        serialPort.PortName = "COM1";
            //        ConfigHelper.UpdateAppConfig("BHS_Com", "COM1");
            //        break;
            //    case "扫描枪":
            //        serialPort.PortName = "COM7";
            //        ConfigHelper.UpdateAppConfig("BHS_Com", "COM7");
            //        break;
            //}

            //IniScanPort();
            //BHS_Conn();
        }
        #endregion

        #region 客显
        /// <summary>
        /// 清屏，关闭客显串口
        /// </summary>
        private void Display_Disconn()
        {
            if (DisplayPort.IsOpen)
            {
                custDisplayer.CLS(DisplayPort);
                DisplayPort.Close();
            }
        }
        #endregion

        #region 读卡器
        /// <summary>
        /// 读卡器读卡方法
        /// </summary>
        public void RfListen()
        {
            try
            {
                bool flag = true;

                //#if DEBUG
                //                flag = false;
                //#endif

                //true 开启读卡
                while (flag)
                {
                    if (order.Count == 0)
                    {
                        //密码和扇区
                        cardflag = mr.ReadCardID("ffffffffffff", "0");
                        //记录当前的读卡设备
                        icdev = mr.icdev;

                        if (cardflag != "" && cardflag != tempCard)
                        {
                            mr.RFbeep();
                            tempCard = cardflag;

                            this.Invoke(new ShowID(ShowCardID), tempCard);
                        }
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
                Log.WriteErrorLog(ex.Message, ex.Source, "");
#if DEBUG
                throw ex;
#endif
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, ex.Source, "");
#if DEBUG     
                throw ex;
#endif
            }
        }

        private delegate void ShowID(string card);
        private void ShowCardID(string card)
        {
            //lbCard.Text = "会员卡：" + card;
            txtCardID.Text = card;

            //会员卡标签被隐藏
            //txtCardID.Show();
            //txtCardID.Focus();
            //MessageBox.Show("读取到新会员卡！");

            string result = CardTran.cardQuery(card);//提交SVN和生产环境使用（生成Release版本）
            //string result = "OK:王五|100";//pos上测试时使用（生成Release版本）
            //#if DEBUG
            //  result = "OK:泰瑞尔|1000";


            //访问会员卡接口
            if (result.Substring(0, 2) == "OK")
            {
                //OK:abc|123
                Mname = result.Substring(result.IndexOf(":") + 1, result.IndexOf("|") - result.IndexOf(":") - 1);
                Balance = Convert.ToDecimal(result.Substring(result.IndexOf("|") + 1));
                //#endif

                //MessageBox.Show("持卡人：" + Mname + Environment.NewLine + "余额：" + Balance);
                frmPayWay frm = new frmPayWay(this);
                frm.ShowDialog();

                lbCard.Text = "持卡人：" + Mname + "  余额：" + Balance;
                lbCard.Show();
            }
            else
            {
                //接口返回错误，清空窗体会员卡数据
                MessageBox.Show(result);

                lbCard.Text = "不是有效的会员卡";
                lbCard.Show();

                tempCard = "";
                txtCardID.Text = "";
                Mname = "";
                Balance = 0;
            }
        }

        /// <summary>
        /// 释放读卡器设备
        /// </summary>
        private void CardReader_Disconn()
        {
            if (icdev > 0)
            {
                MwrfCommon.rf_exit(icdev);

                icdev = 0;
            }

            //关闭线程
            if (readCard.IsAlive)
                readCard.Abort();
        }
        #endregion

        /// <summary>
        /// 从支付界面返回销售界面时，加载临时的购物单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSell_VisibleChanged(object sender, EventArgs e)
        {

            if (this.Visible)
            {

                BHS_Conn();
                txtCode.Clear();

                bool ret = true;
                //读取临时保存的购物单
                try
                {
                    ret = LoadOrder();
                }
                catch (Exception ex)
                {
                    ret = false;
                    Log.WriteErrorLog(ex.Message, ex.Source, "销售界面初始化读取临时保存的购物单");
                }

                //Load order 文件失败，说明支付完毕，就新建 order，清空会员卡  同时恢复iscash默认值
                if (!ret)
                {
                    try
                    {
                        isCash = false;
                        order = new List<OrderItem>();
                        dgvGoods_DataBind(order);
                        ClearSelection();

                        txtCardID.Clear();
                        lbCard.Text = "";
                        Membercard = "";
                        tempCard = "";
                        Mname = "";
                        Balance = 0;
                    }
                    catch (Exception ex)
                    {
                        Log.WriteErrorLog(ex.Message, ex.Source, "销售界面新建Order");
                    }
                }

            }



        }

        private delegate void OrderSerial(List<OrderItem> order);
        /// <summary>
        /// 序列化购物单
        /// </summary>
        /// <param name="order"></param>
        public void SerializeOrder(List<OrderItem> order)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream output = File.Create(GlobalParams.orderPath))
                {
                    formatter.Serialize(output, order);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, ex.Source, "序列化购物单");
#if DEBUG
                throw ex;
#endif
            }
        }

    }
}
