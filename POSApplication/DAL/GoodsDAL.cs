using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using POSApplication.Model;
using POSApplication.Common;

namespace POSApplication.DAL
{
    public class GoodsDAL
    {
        /// <summary>
        /// 根据条码获取商品
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Goods GetGoodsByCode(string code)
        {
            try
            {
                string sql = "select * from goods_spmx where sell_code=@code";

                MySqlParameter[] parameter = 
                {
                    DataAccess.AddParamInput("@code", code, MySqlDbType.VarChar)
                };
                DataTable dt = DataAccess.GetDataTable("POSMySQL", sql, parameter);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return new ORM<Goods>().SwichByDR(dt.Rows[0]);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#endif
            }

            return null;
        }

        /// <summary>
        /// 根据货号获取商品，默认选择规格数量为1，且条形码最小的
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public Goods GetGoodsByCID(string cid)
        {
            try
            {
                string sql = "select * from goods_spmx where c_code=@cid and pre2=1 order by sell_code";

                MySqlParameter[] parameter = 
                {
                    DataAccess.AddParamInput("@cid", cid, MySqlDbType.VarChar)
                };
                DataTable dt = DataAccess.GetDataTable("POSMySQL", sql, parameter);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return new ORM<Goods>().SwichByDR(dt.Rows[0]);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#endif
            }

            return null;
        }

        /// <summary>
        /// 清空商品表
        /// </summary>
        /// <returns></returns>
        public int DeleteGoods()
        {
            int result = 0;

            try
            {
                string sql = "delete from goods_spmx";

                result = DataAccess.ExecuteNonQuery("POSMySQL", sql);
            }
            catch (Exception ex)
            {
                result = -1;
#if DEBUG
                throw ex;
#endif
            }

            return result;
        }
    }
}
