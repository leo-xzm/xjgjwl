using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.Common;

namespace POSApplication.Model
{
    /// <summary>
    /// 时段折扣表：discount_yhxs
    /// </summary>
    [Serializable]
    [TableAttributes("Discount")]
    public class Discount
    {
        #region properties

        /// <summary>
        /// 货号
        /// </summary>
        [ColumnAttributes("C_code")]
        public string C_code { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [ColumnAttributes("Kssj")]
        public string Kssj { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [ColumnAttributes("Jssj")]
        public string Jssj { get; set; }

        /// <summary>
        /// 优惠率
        /// </summary>
        [ColumnAttributes("Yhl")]
        public decimal Yhl { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        [ColumnAttributes("Short_name")]
        public string Short_name { get; set; }

        /// <summary>
        /// 前台售价
        /// </summary>
        [ColumnAttributes("Sell_price")]
        public decimal Sell_price { get; set; }
        #endregion
    }
}
