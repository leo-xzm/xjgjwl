using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace POSApplication.Model
{
    /// <summary>
    /// 购物车订单中的单项商品
    /// </summary>
    [Serializable]
    public class OrderItem
    {
        public OrderItem()
        { }

        public OrderItem(Goods goods)
        {
            spbh = goods.c_code;
            txm = goods.sell_code;
            Name = goods.short_name;
            //规格数量
            number = goods.Pre2 == 0 ? 1 : goods.Pre2;
            Mcard = "";
            YJ = goods.company_price;
            sell_price = goods.sell_price;
            m_sell_price = goods.m_sell_price;
            jf_rate = goods.jf_rate;
            JYSL = 1;
            Discount = "";

            //默认非促销
            cxbh = "00";
            cxid = "0";

            isValid = true;
        }
        /// <summary>
        /// 货号
        /// </summary>
        public string spbh { get; set; }        
        
        /// <summary>
        /// 品名
        /// </summary>
        public string Name { get; set; }

        private decimal jyj;
        /// <summary>
        /// 单价（交易价）
        /// </summary>
        public decimal JYJ
        {
            get
            {
                return jyj;
            }
            set
            {
                jyj = value;
                XJ = JYSL * jyj * (Discount == "" ? 1 : Convert.ToDecimal(Discount));
            }
        }

        private decimal jysl;
        /// <summary>
        /// 数量
        /// </summary>
        public decimal JYSL
        {
            get
            {
                return jysl;
            }
            set
            {
                jysl = value;
                XJ = jysl * JYJ * (Discount == "" ? 1 : Convert.ToDecimal(Discount));
            }
        }

        private decimal xj;
        /// <summary>
        /// 小计，保留两位小数（折扣后）
        /// </summary>
        public decimal XJ
        {
            get
            {
                return xj;
            }
            set
            {
                xj = Math.Round(value, 2);
            }
        }

        private string discount;
        /// <summary>
        /// 折扣
        /// </summary>
        public string Discount
        {
            get
            {
                return discount;
            }
            set
            {
                discount = value;
                //XJ = JYSL * JYJ * (discount == "" ? 1 : Convert.ToDecimal(discount));
                // 按单价乘以数量会影响称重商品的小计， 改为直接小计乘以折扣
                XJ = XJ * (discount == "" ? 1 : Convert.ToDecimal(discount));
            }
        }

        /// <summary>
        /// 小票序号
        /// </summary>
        public int xpxh { get; set; }

        /// <summary>
        /// 条形码
        /// </summary>
        public string txm { get; set; }

        /// <summary>
        /// 规格数量
        /// </summary>
        public decimal number { get; set; }

        private string mcard;
        /// <summary>
        /// 会员卡
        /// </summary>
        public string Mcard {
            get
            { 
                return mcard; 
            }
            set 
            {
                mcard = value;
                if (JYJ == 0)
                {
                    //没有会员号时，单价使用门店售价
                    //有会员号时，单价使用会员售价
                    if (mcard == "")
                        JYJ = sell_price;
                    else
                        JYJ = m_sell_price;
                }
            }
        }

        /// <summary>
        /// 持卡人
        /// </summary>
        public string Mname { get; set; }

        /// <summary>
        /// 卡余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 原价（公司售价）
        /// </summary>
        public decimal YJ { get; set; }

        /// <summary>
        /// 门店售价
        /// </summary>
        public decimal sell_price { get; set; }

        /// <summary>
        /// 会员售价
        /// </summary>
        public decimal m_sell_price { get; set; }

        /// <summary>
        /// 积分比率
        /// </summary>
        public decimal jf_rate { get; set; }

        /// <summary>
        /// 是否有效（抵消的正负记录不再显示和出现在逻辑中）
        /// </summary>
        public bool isValid { get; set; }

        /// <summary>
        /// 优惠分摊明细
        /// </summary>
        public decimal yhmx { get; set; }

        /// <summary>
        /// 促销类型编号
        /// </summary>
        public string cxbh { get; set; }

        /// <summary>
        /// 促销活动编号
        /// </summary>
        public string cxid { get; set; }
    }
}
