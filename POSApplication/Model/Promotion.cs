using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.Common;

namespace POSApplication.Model
{
    /// <summary>
    /// promotion_pt 促销表
    /// </summary>
    [Serializable]
    [TableAttributes("Promotion")]
    public class Promotion
    {
        #region properties

        /// <summary>
        /// 活动编号
        /// </summary>
        [ColumnAttributes("c_pt_id")]
        public string c_pt_id { get; set; }

        /// <summary>
        /// 活动开始日期
        /// </summary>
        [ColumnAttributes("d_pt_begin")]
        public string d_pt_begin { get; set; }

        /// <summary>
        /// 活动结束日期
        /// </summary>
        [ColumnAttributes("d_pt_end")]
        public string d_pt_end { get; set; }

        /// <summary>
        /// 模式编号
        /// </summary>
        [ColumnAttributes("c_pt_type")]
        public string c_pt_type { get; set; }

        /// <summary>
        /// 条件值
        /// </summary>
        [ColumnAttributes("n_cond_amt")]
        public decimal n_cond_amt { get; set; }

        /// <summary>
        /// 优惠值
        /// </summary>
        [ColumnAttributes("n_minus_disc")]
        public decimal n_minus_disc { get; set; }

        /// <summary>
        /// 对象
        /// </summary>
        [ColumnAttributes("c_pt_cust")]
        public string c_pt_cust { get; set; }

        /// <summary>
        /// 优惠次数
        /// </summary>
        [ColumnAttributes("n_pt_count")]
        public decimal n_pt_count { get; set; }

        /// <summary>
        /// A群组减价分摊金额
        /// </summary>
        [ColumnAttributes("n_disc_share_a")]
        public decimal n_disc_share_a { get; set; }

        /// <summary>
        /// B群组减价分摊金额
        /// </summary>
        [ColumnAttributes("n_disc_share_b")]
        public decimal n_disc_share_b { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [ColumnAttributes("c_pt_timebegin")]
        public string c_pt_timebegin { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [ColumnAttributes("c_pt_timeend")]
        public string c_pt_timeend { get; set; }

        /// <summary>
        /// 周次
        /// </summary>
        [ColumnAttributes("c_circle")]
        public string c_circle { get; set; }

        /// <summary>
        /// 群组A_B
        /// </summary>
        [ColumnAttributes("c_plu_ab")]
        public string c_plu_ab { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [ColumnAttributes("c_sku_id")]
        public string c_sku_id { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        [ColumnAttributes("n_pt_qty")]
        public decimal n_pt_qty { get; set; }
        #endregion
    }
}
