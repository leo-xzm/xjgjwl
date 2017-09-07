using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSApplication.Common
{
    /// <summary>
    /// 顾客显示牌数据模型类
    /// </summary>
    public class CustomerDisplayData
    {
        /// <summary>
        /// 单价
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 总计
        /// </summary>
        public string Total { get; set; }
        /// <summary>
        /// 收款
        /// </summary>
        public string Pay { get; set; }
        /// <summary>
        /// 找零
        /// </summary>
        public string Change { get; set; }
        /// <summary>
        /// 状态灯（1-单价；2-总计；3-收款；4-找零）
        /// </summary>
        public int StatusLight { get; set; }
    }
}
