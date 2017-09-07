using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using POSApplication.Model;
using MySql.Data.MySqlClient;
using POSApplication.Common;

namespace POSApplication.DAL
{
    public class PromotionDAL
    {
        /// <summary>
        /// 根据 order 中商品的货号，获取相关的促销编号
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public List<string> GetPtIDList(string codes)
        {
            try
            {
                List<string> ptIDList = new List<string>();
                //string sql = string.Format("select DISTINCT c_pt_id from promotion_pt_temp where c_sku_id in ({0})", codes);

                string sql = string.Format("select DISTINCT c_pt_id from promotion_pt where c_sku_id in ({0})", codes);
                DataTable dt = DataAccess.GetDataTable("POSMySQL", sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ptIDList.Add(dt.Rows[i][0].ToString());
                    }

                    return ptIDList;
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
        /// 根据促销编号获取促销实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Promotion> GetPromotion(string id)
        {
            try
            {
                List<Promotion> list = new List<Promotion>();
                //string sql = "select * from promotion_pt_temp where c_pt_id = @id";

                string sql = "select * from promotion_pt where c_pt_id = @id";

                MySqlParameter[] parameter = 
                {
                    DataAccess.AddParamInput("@id", id, MySqlDbType.VarChar)
                };
                DataTable dt = DataAccess.GetDataTable("POSMySQL", sql, parameter);

                if (dt != null && dt.Rows.Count > 0)
                {
                    Promotion pt = new Promotion();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pt = new ORM<Promotion>().SwichByDR(dt.Rows[i]);
                        list.Add(pt);
                    }
                    return list;
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
        /// 清空促销
        /// </summary>
        /// <returns></returns>
        public int DeletePromotion()
        {
            int result = 0;

            try
            {
                string sql = "delete from promotion_pt";

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
