using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSApplication.Model
{
    /// <summary>
    /// 针对购物车订单的操作枚举
    /// </summary>
    public enum OrderHandleType
    {
        /// <summary>
        /// 修改
        /// </summary>
        alter,
        /// <summary>
        /// 删除
        /// </summary>
        delete,
        /// <summary>
        /// 整单删除
        /// </summary>
        deleteAll,
        /// <summary>
        /// 打折
        /// </summary>
        discount,
        /// <summary>
        /// 整单打折
        /// </summary>
        discountOverall,
        /// <summary>
        /// 密码模式
        /// </summary>
        password,
        /// <summary>
        /// 解款打印
        /// </summary>
        cash
    }
}
