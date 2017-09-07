using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POSApplication.Common;

namespace POSApplication.UserController
{
    public partial class MCardPay : UserControl
    {
        public MCardPay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 待付金额
        /// </summary>
        public decimal MoneyTotal;

        /// <summary>
        /// 会员卡号
        /// </summary>
        public string CardNo;

        /// <summary>
        /// 持卡人姓名
        /// </summary>
        public string mName;

        /// <summary>
        /// 卡余额
        /// </summary>
        public decimal Balance;

        public MCardPay(string cardNo)
        {
            InitializeComponent();

            //卡接口，查询姓名和余额
            this.CardNo = cardNo;

            string result = CardTran.cardQuery(CardNo);//提交SVN和生产环境使用（生成Release版本）
            //string result = "OK:王五|100";//pos上测试时使用（生成Release版本）
//#if DEBUG
 //       result = "OK:泰瑞尔|1000";
//#endif

            if (result.Substring(0, 2) == "OK")
            {
                //OK:abc|123
                mName = result.Substring(result.IndexOf(":") + 1, result.IndexOf("|") - result.IndexOf(":") - 1);
                Balance = Convert.ToDecimal(result.Substring(result.IndexOf("|") + 1));
            }
            else
            {
                MessageBox.Show(result);
                mName = "";
                Balance = 0;
            }

            lbName.Text = mName;
            lbBalance.Text = Balance.ToString();
        }

        private void txtCardCash_TextChanged(object sender, EventArgs e)
        {
            InputValidate.PayInputValidate(txtCardCash);

            if (Balance >= MoneyTotal)
            {
                //如果输入的值大于待付金额，就改成待付金额
                if (Convert.ToDecimal(txtCardCash.Text == "" ? "0" : txtCardCash.Text) > MoneyTotal)
                    txtCardCash.Text = MoneyTotal.ToString();
            }
            else
            {
                //如果输入的值大于待付金额，就改成待付金额
                if (Convert.ToDecimal(txtCardCash.Text == "" ? "0" : txtCardCash.Text) > Balance)
                    txtCardCash.Text = Balance.ToString();
            }

            txtCardCash.SelectionStart = txtCardCash.TextLength;
        }

        private void MCardPay_Load(object sender, EventArgs e)
        {
            txtCardCash.Text = MoneyTotal.ToString();
            txtCardCash.SelectionStart = txtCardCash.TextLength;
        }
    }
}
