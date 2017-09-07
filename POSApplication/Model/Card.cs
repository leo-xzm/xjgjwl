using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.Common;

namespace POSApplication.Model
{
    /// <summary>
    /// 结算方式：paymentmethod_jsfs
    /// </summary>
    [Serializable]
    [TableAttributes("Card")]
    public class Card
    {
        #region properties
        /// <summary>
        /// 序号
        /// </summary>
        [ColumnAttributes("id")]
        public string id { get; set; }

        /// <summary>
        /// 结算名称
        /// </summary>
        [ColumnAttributes("card")]
        public string card { get; set; }

        /// <summary>
        /// 结算方式编号
        /// </summary>
        [ColumnAttributes("jsfsbh")]
        public string jsfsbh { get; set; }

        /// <summary>
        /// 启用标记
        /// </summary>
        [ColumnAttributes("state")]
        public string State { get; set; }

        /// <summary>
        /// 启用日期
        /// </summary>
        [ColumnAttributes("ksrq")]
        public string ksrq { get; set; }
        #endregion
    }
}
