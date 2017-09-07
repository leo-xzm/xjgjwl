using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace POSApplication.DAL
{
    public class DiscountDAL
    {
        /// <summary>
        /// 根据货号获取时段优惠
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public string GetDiscountByCID(string cid, string time)
        {
            try
            {
                string sql = "select * from discount_yhxs where c_code=@cid and @time between  Kssj and Jssj";

                MySqlParameter[] parameter = 
                {
                    DataAccess.AddParamInput("@cid", cid, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@time", time, MySqlDbType.VarChar)
                };
                DataTable dt = DataAccess.GetDataTable("POSMySQL", sql, parameter);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Yhl"].ToString();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#endif
            }

            return "";
        }

        /// <summary>
        /// 清空时段折扣表
        /// </summary>
        /// <returns></returns>
        public int DeleteDiscount()
        {
            int result = 0;

            try
            {
                string sql = "delete from discount_yhxs";

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
