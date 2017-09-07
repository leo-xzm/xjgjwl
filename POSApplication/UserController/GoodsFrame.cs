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
    public partial class GoodsFrame : UserControl
    {
        //商品小窗体对应的购物单中选择的商品
        public OrderItem goods;

        //商品小窗体对应的操作
        public OrderHandleType type;

        public GoodsFrame(OrderItem goods, OrderHandleType type)
        {
            InitializeComponent();
            this.goods = goods;

            lbCode.Text = goods.spbh;
            lbName.Text = goods.Name;
            lbTxm.Text = goods.txm;
            //数量
            lbCount.Text = goods.JYSL.ToString();
            txtCount.Text = goods.JYSL.ToString();
            //打折
            lbDiscount.Text = goods.Discount;
            txtDiscount.Text = goods.Discount;

            lbSellPrice.Text = goods.JYJ.ToString();
            lbSubtotal.Text = goods.XJ.ToString();

            this.type = type;
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// 根据type显示相应的控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoodsFrame_Load(object sender, EventArgs e)
        {
            switch (type)
            {
                case OrderHandleType.delete:
                    lbTitle.Text = "删除";
                    txtCount.Hide();
                    txtDiscount.Hide();
                    break;
                case OrderHandleType.alter:
                    lbTitle.Text = "修改商品数量";
                    lbCount.Hide();
                    txtDiscount.Hide();
                    break;
                case OrderHandleType.discount:
                    lbTitle.Text = "打折";
                    lbDiscount.Hide();
                    txtCount.Hide();
                    break;
            }
        }

        /// <summary>
        /// 打折修改，重新计算小计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            InputValidate.DiscountInputValidate(txtDiscount);
            lbSubtotal.Text = Math.Round(((txtDiscount.Text == "" ? 1 : Convert.ToDecimal(txtDiscount.Text)) 
                * goods.JYJ * goods.JYSL),2).ToString();
        }

        /// <summary>
        /// 数量修改，重新计算小计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCount_TextChanged(object sender, EventArgs e)
        {
            lbSubtotal.Text = Math.Round(((goods.Discount == "" ? 1 : Convert.ToDecimal(goods.Discount)) 
                * goods.JYJ * ((txtCount.Text == "" ? 0 : Convert.ToDecimal(txtCount.Text)))),2).ToString();
        }
    }
}
