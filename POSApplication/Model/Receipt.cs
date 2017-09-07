using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSApplication.Model
{
    [Serializable]
    public class Receipt
    {
        /// <summary>
        /// 小票号
        /// </summary>
        public string xph { get; set; }

        /// <summary>
        /// 机号
        /// </summary>
        public string jh { get; set; }

        /// <summary>
        /// 收款员
        /// </summary>
        public string syybh { get; set; }

        /// <summary>
        /// 商品列表
        /// </summary>
        public List<OrderItem> order { get; set; }

        /// <summary>
        /// 总计商品数
        /// </summary>
        public decimal goodsCount { get; set; }

        /// <summary>
        /// 总计原价
        /// </summary>
        public decimal moneyTotal { get; set; }

        /// <summary>
        /// 优惠
        /// </summary>
        public decimal yh { get; set; }

        /// <summary>
        /// 抹零
        /// </summary>
        public decimal zjj { get; set; }

        /// <summary>
        /// 总计
        /// </summary>
        public decimal actualTotal { get; set; }

        /// <summary>
        /// 现金支付
        /// </summary>
        public decimal cash { get; set; }

        /// <summary>
        /// 刷卡支付
        /// </summary>
        public decimal cardPay { get; set; }

        /// <summary>
        /// 找零
        /// </summary>
        public decimal change { get; set; }

        /// <summary>
        /// 会员卡号
        /// </summary>
        public string mcard { get; set; }

        /// <summary>
        /// 会员名
        /// </summary>
        public string mName { get; set; }

        /// <summary>
        /// 交易前余额
        /// </summary>
        public decimal iniBalance { get; set; }

        /// <summary>
        /// 交易后余额
        /// </summary>
        public decimal endBalance { get; set; }

        /// <summary>
        /// 打印时间
        /// </summary>
        public DateTime printTime { get; set; }
    }
}
