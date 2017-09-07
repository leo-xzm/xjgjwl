using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSApplication.Model
{
    /// <summary>
    /// 购物销售单品
    /// </summary>
    public class OrderItemForSales:OrderItem
    {
        /// <summary>
        /// 小票号
        /// </summary>
        public int xph { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        //public int xpxh { get; set; }

        /// <summary>
        /// 购物销售单品
        /// </summary>
        /// <param name="item">购物单单品</param>
        /// <param name="xph">小票号</param>
        /// <param name="isShow">是否显示在购物单中（应对销售负记录）</param>
        public OrderItemForSales(OrderItem item, int xph, int xpxh)
        {
            this.spbh = item.spbh;
            this.txm = item.txm;
            this.Name = item.Name;
            this.JYJ = item.JYJ;
            this.JYSL = item.JYSL;
            this.Discount = item.Discount;
            this.XJ = item.XJ;

            this.xph = xph;
            this.xpxh = xpxh;
        }
    }
}
