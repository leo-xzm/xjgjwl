using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.Model;
using POSApplication.Common;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Transactions;
using System.Configuration;
using System.Data;

namespace POSApplication.DAL
{
    public class SalesDAL
    {
        /// <summary>
        /// 向单次销售明细表中增加商品销售数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int AddSalesTime(Sales item)
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO sales_time (tzm,spbh,txm,jyj,jysl,ssje,xsrq,xssj,syybh,xph,jsfsbh,jh,yj,zjj,mcard,yhmx,jfmx,szbz,cxbh,xpxh,cxid,Baka,Bakb) 
                    VALUES (@tzm,@spbh,@txm,@jyj,@jysl,@ssje,@xsrq,@xssj,@syybh,@xph,@jsfsbh,@jh,@yj,@zjj,@mcard,@yhmx,@jfmx,@szbz,@cxbh,@xpxh,@cxid,@Baka,@Bakb)";

                MySqlParameter[] parameter = 
                {
                    DataAccess.AddParamInput("@tzm", item.tzm, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@spbh", item.spbh, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@txm", item.txm, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@jyj", item.jyj, MySqlDbType.Decimal),
                    DataAccess.AddParamInput("@jysl", item.jysl, MySqlDbType.Decimal),
                    DataAccess.AddParamInput("@ssje", item.ssje, MySqlDbType.Decimal),
                    DataAccess.AddParamInput("@xsrq", item.xsrq, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@xssj", item.xssj, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@syybh", item.syybh, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@xph", item.xph, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@jsfsbh", item.jsfsbh, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@jh", item.jh, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@yj", item.yj, MySqlDbType.Decimal),
                    DataAccess.AddParamInput("@zjj", item.zjj, MySqlDbType.Decimal),
                    DataAccess.AddParamInput("@mcard", item.mcard, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@yhmx", item.yhmx, MySqlDbType.Decimal),
                    DataAccess.AddParamInput("@jfmx", item.jfmx, MySqlDbType.Decimal),
                    DataAccess.AddParamInput("@szbz", item.szbz, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@xpxh", item.xpxh, MySqlDbType.Decimal),
                    DataAccess.AddParamInput("@cxbh", item.cxbh, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@cxid", item.cxid, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@Baka", item.Baka, MySqlDbType.VarChar),
                    DataAccess.AddParamInput("@Bakb", item.Bakb, MySqlDbType.Decimal)
                };

                result = DataAccess.ExecuteNonQuery("POSMySQL", sql, parameter);
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


        /// <summary>
        /// 清空单次销售明细表
        /// </summary>
        /// <returns></returns>
        public int DeleteSalesTime()
        {
            int result = 0;

            try
            {
                string sql = "delete from sales_time";

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

        /// <summary>
        /// 清空单日销售明细表
        /// </summary>
        /// <returns></returns>
        public int DeleteSalesDay()
        {
            int result = 0;

            try
            {
                string sql = "delete from sales_day";

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

        /// <summary>
        /// 删除单次销售明细表中的999校验
        /// </summary>
        /// <returns></returns>
        public int DeleteSalesTime999()
        {
            int result = 0;

            try
            {
                string sql = "delete from sales_time where tzm='999'";

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

        /// <summary>
        /// 新增单日商品销售明细数据
        /// </summary>
        /// <returns></returns>
        public bool AddSalesDay()
        {
            bool result = true;

            try
            {
                string sql = "insert into sales_day select * from sales_time";
            
                result = DataAccess.ExecTrans("POSMySQL", sql);
            }
            catch (Exception ex)
            {
                result = false;
                //Log.WriteErrorLog(ex.Message, ex.Source, "新增单日商品销售明细数据");
#if DEBUG
                throw ex;
#endif
            }

            return result;
                
            //using (TransactionScope ts = new TransactionScope())//使整个代码块成为事务性代码
            //{
            //    #region 在这里编写需要具备Transaction的代码

            //    bool result = true;
            //    string conString = ConfigurationManager.ConnectionStrings["POSMySQL"].ConnectionString;
            //    MySqlConnection myConnection = new MySqlConnection(conString);
            //    myConnection.Open();
            //    MySqlCommand myCommand = new MySqlCommand();
            //    myCommand.Connection = myConnection;

            //    try
            //    {
            //        myCommand.CommandText = "insert into sales_day select * from sales_time";

            //        DateTime time = DateTime.Now; 
            //        myCommand.ExecuteNonQuery();
            //        DateTime time2 = DateTime.Now;
            //        Log.WriteSQLLog(myCommand.CommandText, "", "POSApplication.DAL.AddSalesDay()", time, time2, (time2 - time).Milliseconds);
            
            //    }
            //    catch (Exception ex)
            //    {
            //        result = false;
            //        Log.WriteErrorLog(ex.Message, ex.Source, "新增单日商品销售明细数据");
            //    }
            //    finally
            //    {
            //        myConnection.Close();
            //    }

            //    #endregion

            //    ts.Complete();
            //    return result;
            //}
        }


        /// <summary>
        /// 新增商品销售明细数据
        /// </summary>
        /// <returns></returns>
        public bool AddSalesData()
        {
            bool result = true;
            List<string> cmdList = new List<string>();

            try
            {
                string sql = "insert into sales_day select * from sales_time";
                cmdList.Add(sql);
                sql = "insert into sales_xsxb select * from sales_time";
                cmdList.Add(sql);

                result = DataAccess.ExecTrans("POSMySQL",cmdList);
            }
            catch (Exception ex)
            {
                result = false;
                //Log.WriteErrorLog(ex.Message, ex.Source, "新增单日商品销售明细数据");
#if DEBUG
                throw ex;
#endif
            }

            return result;
        }

        /// <summary>
        /// 查询 sales_backups 表数据量
        /// </summary>
        /// <returns></returns>
        public int SelectSalesBackups()
        {
            int result = -1;

            try
            {
                string sql = "select * from sales_backups";
                DataTable dt = DataAccess.GetDataTable("POSMySQL", sql);

                if (dt != null)
                    result = dt.Rows.Count;
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#endif
            }

            return result;
        }

        /// <summary>
        /// 查询 sales_unsure 表数据量
        /// </summary>
        /// <returns></returns>
        public int SelectSalesUnsure()
        {
            int result = -1;

            try
            {
                string sql = "select * from sales_unsure";
                DataTable dt = DataAccess.GetDataTable("POSMySQL", sql);

                if (dt != null)
                    result = dt.Rows.Count;
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#endif
            }

            return result;
        }

        /// <summary>
        /// 清空 sales_unsure 表
        /// </summary>
        /// <returns></returns>
        public int DeleteSalesUnsure()
        {
            int result = 0;

            try
            {
                string sql = "delete from sales_unsure";

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

        /// <summary>
        /// 追加数据 从 sales_time 到 sales_backups 表
        /// </summary>
        /// <returns></returns>
        public int TimeToBackups()
        {
            int result = 0;

            try
            {
                string sql = "insert into sales_backups select * from sales_time";

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

        /// <summary>
        /// 追加数据 从 sales_time 到 sales_unsure 表
        /// </summary>
        /// <returns></returns>
        public int TimeToUnsure()
        {
            int result = 0;

            try
            {
                string sql = "insert into sales_unsure select * from sales_time";

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

        /// <summary>
        /// 追加数据 从 sales_unsure 到 sales_backups 表
        /// </summary>
        /// <returns></returns>
        public int UnsureToBackups()
        {
            int result = 0;

            try
            {
                string sql = "insert into sales_backups select * from sales_unsure";

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

        /// <summary>
        /// 追加数据 从 sales_backups 到 sales_unsure 表
        /// </summary>
        /// <returns></returns>
        public int BackupsToUnsure()
        {
            int result = 0;

            try
            {
                string sql = "insert into sales_unsure select * from sales_backups";

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

        /// <summary>
        /// 清空 backups，复制数据 从 time 到 backups 表
        /// </summary>
        /// <returns></returns>
        public int TimeCoverBackups()
        {
            int result = 0;

            try
            {
                string sql = "delete from sales_backups; insert into sales_backups select * from sales_time;";

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

        /// <summary>
        /// 获取当日销售明细中的支付数据
        /// </summary>
        /// <returns></returns>
        public List<Sales> GetSalesDay()
        {
            try
            {
                List<Sales> list = new List<Sales>();

                string sql = "select * from sales_day where tzm='1' or tzm='253' or tzm='2'";

                DataTable dt = DataAccess.GetDataTable("POSMySQL", sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    Sales sales = new Sales();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sales = new ORM<Sales>().SwichByDR(dt.Rows[i]);
                        list.Add(sales);
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



        public Goods GetGoodsByCode(string code)
        {
            try
            {
                string sql = "select * from sales_xsxb where sell_code=@code";

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
                string sql = "select * from sales_xsxb where c_code=@cid and pre2=1 order by sell_code";

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
    }
}
