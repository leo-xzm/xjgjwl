using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.DAL;

namespace POSApplication.BLL
{
    public class DiscountBLL
    {
        /// <summary>
        /// 根据货号获取时段优惠
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public string GetDiscountByCID(string cid)
        {
            return new DiscountDAL().GetDiscountByCID(cid, DateTime.Now.ToString("HH:mm:ss"));
        }

        /// <summary>
        /// 清空时段折扣表
        /// </summary>
        /// <returns></returns>
        public int DeleteDiscount()
        {
            return new DiscountDAL().DeleteDiscount();
        }
    }
}
