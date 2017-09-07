using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using POSApplication.Model;
using POSApplication.Common;
using System.Data.Odbc;

namespace POSApplication.DAL
{
    public class CardDAL
    {
        /// <summary>
        /// 获取支付卡种类
        /// </summary>
        /// <returns></returns>
        public List<Card> GetCards()
        {
            try
            {
                List<Card> list = new List<Card>();

                string sql = "select * from paymentmethod_jsfs where state like '%y%'";

                DataTable dt = DataAccess.GetDataTable("POSMySQL", sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    Card card = new Card();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        card = new ORM<Card>().SwichByDR(dt.Rows[i]);
                        list.Add(card);
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
        /// 清空结算方式表
        /// </summary>
        /// <returns></returns>
        public int DeleteCard()
        {
            int result = 0;

            try
            {
                string sql = "delete from paymentmethod_jsfs";

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
