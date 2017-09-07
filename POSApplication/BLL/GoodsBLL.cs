using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.Model;
using POSApplication.DAL;

namespace POSApplication.BLL
{
    /// <summary>
    /// 根据条码获取商品
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public class GoodsBLL
    {
        /// <summary>
        /// 根据条码获取商品
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Goods GetGoodsByCode(string code)
        {
            return new GoodsDAL().GetGoodsByCode(code);
        }


        /// <summary>
        /// 根据货号获取商品，默认选择规格数量为1，且条形码最小的
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public Goods GetGoodsByCID(string cid)
        {
            return new GoodsDAL().GetGoodsByCID(cid);
        }

        /// <summary>
        /// 清空商品表
        /// </summary>
        /// <returns></returns>
        public int DeleteGoods()
        {
            return new GoodsDAL().DeleteGoods();
        }
    }
}
