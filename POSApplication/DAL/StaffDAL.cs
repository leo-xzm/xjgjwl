using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using POSApplication.Common;
using POSApplication.Model;
using MySql.Data.MySqlClient;
using System.Data.Odbc;

namespace POSApplication.DAL
{
    public class StaffDAL
    {
        /// <summary>
        /// 根据工号获取员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Staff GetStaffByID(string id)
        {
            try
            {
                string sql = "select * from staff_ygk where Staff_no=@id";
                MySqlParameter[] parameter = 
                {
                    DataAccess.AddParamInput("@id", id, MySqlDbType.VarChar)
                };
                DataTable dt = DataAccess.GetDataTable("POSMySQL", sql, parameter);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return new ORM<Staff>().SwichByDR(dt.Rows[0]);
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
        /// 根据密码获取收银主管
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public Staff GetStaffByPwd(string pwd)
        {
            try
            {
                string sql = "select * from staff_ygk where password=@pwd and Rank_no='04'";
                MySqlParameter[] parameter = 
                {
                    DataAccess.AddParamInput("@pwd", pwd, MySqlDbType.VarChar)
                };
                DataTable dt = DataAccess.GetDataTable("POSMySQL", sql, parameter);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return new ORM<Staff>().SwichByDR(dt.Rows[0]);
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
        /// 清空员工表
        /// </summary>
        /// <returns></returns>
        public int DeleteStaff()
        {
             int result = 0;

            try
            {
                string sql = "delete from staff_ygk";

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
