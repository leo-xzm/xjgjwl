using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.Common;

namespace POSApplication.Model
{
    /// <summary>
    /// 销售明细表：sales_xsxb
    /// </summary>
    [Serializable]
    [TableAttributes("Sales")]
    public class Sales
    {
        #region properties
                   
        /// <summary>
        /// 特征码
        /// </summary>
        [ColumnAttributes("tzm")]
        public string tzm { get; set; }
                   
        /// <summary>
        /// 商品编码
        /// </summary>
        [ColumnAttributes("spbh")]
        public string spbh { get; set; }
                   
        /// <summary>
        /// 条形码
        /// </summary>
        [ColumnAttributes("txm")]
        public string txm { get; set; }
                   
        /// <summary>
        /// 交易价
        /// </summary>
        [ColumnAttributes("jyj")]
        public decimal jyj { get; set; }
                   
        /// <summary>
        /// 交易数量
        /// </summary>
        [ColumnAttributes("jysl")]
        public decimal jysl { get; set; }
                   
        /// <summary>
        /// 实收金额
        /// </summary>
        [ColumnAttributes("ssje")]
        public decimal ssje { get; set; }
                   
        /// <summary>
        /// 销售日期
        /// </summary>
        [ColumnAttributes("xsrq")]
        public string xsrq { get; set; }
                   
        /// <summary>
        /// 销售时间
        /// </summary>
        [ColumnAttributes("xssj")]
        public string xssj { get; set; }
                   
        /// <summary>
        /// 收银员编号
        /// </summary>
        [ColumnAttributes("syybh")]
        public string syybh { get; set; }
                   
        /// <summary>
        /// 小票号
        /// </summary>
        [ColumnAttributes("xph")]
        public string xph { get; set; }
                   
        /// <summary>
        /// 结算方式号
        /// </summary>
        [ColumnAttributes("jsfsbh")]
        public string jsfsbh { get; set; }
                   
        /// <summary>
        /// pos机号
        /// </summary>
        [ColumnAttributes("jh")]
        public string jh { get; set; }
                   
        /// <summary>
        /// 原价
        /// </summary>
        [ColumnAttributes("yj")]
        public decimal yj { get; set; }
                   
        /// <summary>
        /// 找零
        /// </summary>
        [ColumnAttributes("zjj")]
        public decimal zjj { get; set; }
                   
        /// <summary>
        /// 会员卡号
        /// </summary>
        [ColumnAttributes("mcard")]
        public string mcard { get; set; }
                   
        /// <summary>
        /// 优惠分摊明细
        /// </summary>
        [ColumnAttributes("yhmx")]
        public decimal yhmx { get; set; }
                   
        /// <summary>
        /// 单品积分明细
        /// </summary>
        [ColumnAttributes("jfmx")]
        public decimal jfmx { get; set; }
                   
        /// <summary>
        /// 18位码标志
        /// </summary>
        [ColumnAttributes("szbz")]
        public string szbz { get; set; }
                   
        /// <summary>
        /// 促销类型编号
        /// </summary>
        [ColumnAttributes("cxbh")]
        public string cxbh { get; set; }
                   
        /// <summary>
        /// 小票序号
        /// </summary>
        [ColumnAttributes("xpxh")]
        public decimal xpxh { get; set; }
                   
        /// <summary>
        /// 促销活动编号
        /// </summary>
        [ColumnAttributes("cxid")]
        public string cxid { get; set; }
                   
        /// <summary>
        /// 预留字段
        /// </summary>
        [ColumnAttributes("Baka")]
        public string Baka { get; set; }
                   
        /// <summary>
        /// 预留字段
        /// </summary>
        [ColumnAttributes("Bakb")]
        public decimal Bakb { get; set; }
        
        #endregion
    }
}
