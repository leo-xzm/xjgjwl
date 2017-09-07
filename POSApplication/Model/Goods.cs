using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.Common;

namespace POSApplication.Model
{
    /// <summary>
    /// goods_spmx: 商品明细表
	/// </summary>
    [Serializable]
    [TableAttributes("Goods")]
    public class Goods
    {
        #region properties
        /// <summary>
        /// 货号
        /// </summary>
        [ColumnAttributes("c_code")]
        public string c_code { get; set; }

        /// <summary>
        /// 条形码
        /// </summary>
        [ColumnAttributes("sell_code")]
        public string sell_code { get; set; }

        /// <summary>
        /// 货品简称
        /// </summary>
        [ColumnAttributes("short_name")]
        public string short_name { get; set; }

        /// <summary>
        /// 公司售价
        /// </summary>
        [ColumnAttributes("company_price")]
        public decimal company_price { get; set; }

        /// <summary>
        /// 门店售价
        /// </summary>
        [ColumnAttributes("sell_price")]
        public decimal sell_price { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [ColumnAttributes("date")]
        public string date { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [ColumnAttributes("time")]
        public string time { get; set; }

        /// <summary>
        /// 货品属性
        /// </summary>
        [ColumnAttributes("type")]
        public string type { get; set; }

        /// <summary>
        /// 积分比率
        /// </summary>
        [ColumnAttributes("jf_rate")]
        public decimal jf_rate { get; set; }

        /// <summary>
        /// 会员售价
        /// </summary>
        [ColumnAttributes("m_sell_price")]
        public decimal m_sell_price { get; set; }

        /// <summary>
        /// 预留字段1
        /// </summary>
        [ColumnAttributes("Pre1")]
        public decimal Pre1 { get; set; }

        /// <summary>
        /// 规格数量
        /// </summary>
        [ColumnAttributes("Pre2")]
        public decimal Pre2 { get; set; }

        /// <summary>
        /// 返利比率
        /// </summary>
        [ColumnAttributes("hl_fl")]
        public string hl_fl { get; set; }

        /// <summary>
        /// 自营标记
        /// </summary>
        [ColumnAttributes("Zybz")]
        public string Zybz { get; set; }

        /// <summary>
        /// 参加活动标记
        /// </summary>
        [ColumnAttributes("Act1")]
        public string Act1 { get; set; }
        #endregion   
    }
}
