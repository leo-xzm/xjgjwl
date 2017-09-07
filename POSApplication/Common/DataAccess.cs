using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Transactions;
using System.Configuration;

namespace POSApplication
{
    /// <summary>
    /// 数据库操作类 
    /// </summary>
    public class DataAccess {

        /// <summary>
        /// 解决mysql带in条件只取到一条记录
        /// </summary>
        /// <param name="arryList"></param>
        /// <param name="list"></param>
        /// <param name="isIntOrStr">是int型还是string</param>
        /// <returns></returns>
        public static string GetMysqlInStr(string arryList, ref List<OdbcParameter> list,bool isIntOrStr=true)
        {
            string where = string.Empty;
            string[] arry = arryList.Split(',');
            for (int i = 0; i < arry.Length; i++)
            {
                if (arry[i].Trim() != string.Empty)
                {
                    where = where + "?,";
                    list.Add(new OdbcParameter("?", isIntOrStr ? arry[i] : arry[i].Replace("'", "")));
                }
            }
            where = where.TrimEnd(','); 
             
            return where;
        }

        /// <summary>
        /// 解决mysql带in条件只取到一条记录
        /// </summary>
        /// <param name="arryList"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetMysqlInStr(DataTable dt, string fieldName, ref List<OdbcParameter> list)
        {
            string where = string.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    where = where + "?,";
                    list.Add(new OdbcParameter("?", dt.Rows[i][fieldName].ToString()));
                }
                where = where.TrimEnd(',');
            } 
            return where;
        }
        #region 获取参 SQL/MYSQL 参数列表
        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        private static string GetSqlParams(params object[] param) {
            string retValue = "";
            foreach (object o in param) {
                retValue += o.ToString() + ",";
            }
            return retValue.TrimEnd(',');
        }

        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        private static string GetSqlParams(params DbParameter[] param) {
            string retValue = "";
            foreach (DbParameter sp in param) {
                retValue += sp.Value + ",";
            }
            return retValue.TrimEnd(',');
        }
        #endregion

        #region DataTable(新)

        #region 执行SQL语句 执行存储过程 执行事务 返回DataTable

        /// <summary>
        /// 执行带参数的SQL语句(MSSQL,MYSQL) 返回DataTable
        /// </summary>
        /// <param name="dbName">数据库名字</param>
        /// <param name="cmdText">SQL</param>
        /// <param name="paramsValue">参数列表</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string dbName, string cmdText, params DbParameter[] paramsValue) {
            DataTable dt = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DbCommand dbCmd = db.GetSqlStringCommand(cmdText);
                dbCmd.Parameters.AddRange(paramsValue);
                DateTime time = DateTime.Now;
                dt = db.ExecuteDataSet(dbCmd).Tables[0];
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(dbCmd.CommandText, GetSqlParams(paramsValue), "POSApplication.GetDataTable(cmdtext,params DbParameter[])", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataTable(cmdtext,params DbParameter[])", cmdText);
                dt = null;
#if DEBUG
                throw ex;
#endif
            }
            return dt;
        }

        /// <summary>
        /// 执行带参数的SQL语句(MSSQL,MYSQL) 返回DataTable
        /// </summary>
        /// <param name="dbName">数据库名字</param>
        /// <param name="cmdText">SQL</param>
        /// <param name="paramsValue">参数列表</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string dbName, string cmdText)
        {
            DataTable dt = null;
            try
            {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DbCommand dbCmd = db.GetSqlStringCommand(cmdText); 
                DateTime time = DateTime.Now;
                dt = db.ExecuteDataSet(dbCmd).Tables[0];
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(dbCmd.CommandText, string.Empty, "POSApplication.GetDataTable(cmdtext,params DbParameter[])", time, time2, (time2 - time).Milliseconds);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataTable(cmdtext,params DbParameter[])", cmdText);
                dt = null;
#if DEBUG
                throw ex;
#endif
            }
            return dt;
        }

        /// <summary>
        /// 执行带参数的SQL语句(MSSQL,MYSQL) 返回DataTable
        /// </summary>
        /// <param name="cmdText">SQL</param>
        /// <param name="paramsValue">参数列表</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string cmdText, params DbParameter[] paramsValue) {
            return GetDataTable("", cmdText, paramsValue);
        }

        /// <summary>
        /// 执行SQL语句(MSSQL,MYSQL) 返回DataTable
        /// </summary>
        /// <param name="cmdText">SQL</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string cmdText) {
            return GetDataTable(CommandType.Text, cmdText);
        }

        /// <summary>
        /// 执行带参数的SQL或存储过程，(SQL语句支持MSSQL,MYSQL)，返回datatable
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="cmdText">SQL</param>
        /// <param name="paramsValue">参数列表</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string dbName, CommandType comdType, string cmdText, params DbParameter[] paramsValue)
        {
            DataTable dt = null;
            DataSet ds = null;
            DbCommand dbcmd=null;
            switch(comdType)
            {
                case CommandType.Text: dt = GetDataTable(dbName, cmdText, paramsValue); break;
                case CommandType.StoredProcedure:
                    try
                    {
                        Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                        dbcmd = db.GetStoredProcCommand(cmdText);
                        dbcmd.Parameters.AddRange(paramsValue);
                        dbcmd.CommandTimeout = 500;
                        DateTime time = DateTime.Now;
                        ds = db.ExecuteDataSet(dbcmd);
                        if (ds != null && ds.Tables.Count > 0)
                            dt = ds.Tables[0];
                        DateTime time2 = DateTime.Now;
                        Log.WriteSQLLog(cmdText, GetSqlParams(paramsValue), "POSApplication.GetDataTable(procName,CommandType,params DbParameter[])", time, time2, (time2 - time).Milliseconds);
                    }
                    catch(Exception ex) 
                    {
                        Log.WriteErrorLog(ex.Message, "POSApplication.GetDataTable(procName,CommandType,params DbParameter[])", cmdText);
                        dbcmd = null;
                        dt = null;
                    }
                    break;
            }
            return dt;
        }

        /// <summary>
        /// 执行带参数的SQL或存储过程，(SQL语句支持MSSQL,MYSQL)，返回datatable
        /// </summary>
        /// <param name="cmdText">SQL</param>
        /// <param name="paramsValue">参数列表</param>
        /// <returns></returns>
        public static DataTable GetDataTable(CommandType comdType, string cmdText, params DbParameter[] paramsValue) {
            return GetDataTable("", comdType, cmdText, paramsValue);
        }

        /// <summary>
        /// 根据 CommandType 执行带事务的SQL 返回DataTable
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="transaction">事务</param>
        /// <param name="cmdText">SQL</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string dbName, System.Data.Common.DbTransaction transaction, string cmdText, params SqlParameter[] paramsValue) {
            DataTable dt = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DbCommand dbCmd = db.GetSqlStringCommand(cmdText);
                dbCmd.Parameters.AddRange(paramsValue);
                DateTime time = DateTime.Now;
                dt = db.ExecuteDataSet(dbCmd, transaction).Tables[0];
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(cmdText, GetSqlParams(paramsValue), "POSApplication.GetDataTable(transaction,cmdText,params SqlParameter[])", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataTable(transaction,cmdText,params SqlParameter[])", cmdText);
                dt = null;
#if DEBUG
                throw ex;
#endif
            }
            return dt;
        }

        /// <summary>
        /// 根据 CommandType 执行带事务的SQL 返回DataTable
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="cmdText">SQL</param>
        /// <returns></returns>
        public static DataTable GetDataTable(System.Data.Common.DbTransaction transaction, string cmdText, params SqlParameter[] paramsValue) {
            return GetDataTable("", transaction, cmdText, paramsValue);
        }

        #endregion

        #region 执行commad 返回DataTable

        /// <summary>
        /// 执行 command 返回datatable
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="command">命令</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string dbName, System.Data.Common.DbCommand command) {
            DataTable dt = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DateTime time = DateTime.Now;
                dt = db.ExecuteDataSet(command).Tables[0];
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(command.CommandText, GetSqlParams(command.Parameters), "POSApplication.GetDataTable(DbCommand)", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataTable(DbCommand)", "");
                dt = null;
#if DEBUG
                throw ex;
#endif
            }
            return dt;
        }

        /// <summary>
        /// 执行 command 返回datatable
        /// </summary>
        /// <param name="command">命令</param>
        /// <returns></returns>
        public static DataTable GetDataTable(System.Data.Common.DbCommand command) {
            return GetDataTable("", command);
        }

        /// <summary>
        /// 执行 command 返回datatable
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="transaction">事务</param>
        /// <param name="command">命令</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string dbName, System.Data.Common.DbTransaction transaction, System.Data.Common.DbCommand command) {
            DataTable dt = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DateTime time = DateTime.Now;
                dt = db.ExecuteDataSet(command, transaction).Tables[0];
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(command.CommandText, GetSqlParams(command.Parameters), "POSApplication.GetDataTable(transaction,command)", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataTable(transaction,command)", "");
                dt = null;
#if DEBUG
                throw ex;
#endif
            }
            return dt;
        }

        /// <summary>
        /// 执行 command 返回datatable
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="command">命令</param>
        /// <returns></returns>
        public static DataTable GetDataTable(System.Data.Common.DbTransaction transaction, System.Data.Common.DbCommand command) {
            return GetDataTable("", transaction, command);
        }

        #endregion

        #region 根据 DataSet 获取 DataTable

        /// <summary>
        /// 根据DataSet的索引获取DataTable
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="tbIndex">索引</param>
        /// <returns></returns>
        public static DataTable GetDataTable(DataSet ds, int tbIndex) {
            try {
                return ds.Tables[tbIndex];
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.DataTable", "根据DataSet的索引获取DataTable失败");
                return null;
            }
        }

        #endregion

        #endregion

        #region DataSet(新)

        #region 执行存储过程 返回DataSet

        /// <summary>
        /// 根据存储过程的名称和对应的参数获取 DataSet
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameterValues">参数(参数1，参数2，参数3....)</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string dbName, string procName, params object[] parameterValues) {
            DataSet ds = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DateTime time = DateTime.Now;
                ds = db.ExecuteDataSet(procName, parameterValues);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(procName, GetSqlParams(parameterValues), "POSApplication.GetDataSet(proc,params object[])", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataSet(proc,params object[])", procName);
                ds = null;
#if DEBUG
                throw ex;
#endif
            }
            return ds;
        }

        /// <summary>
        /// 根据存储过程的名称和对应的参数获取 DataSet
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameterValues">参数(参数1，参数2，参数3....)</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string procName, params object[] parameterValues) {
            return GetDataSet("", procName, parameterValues);
        }

        /// <summary>
        /// 根据存储过程的名称和对应参数执行一个带事务获取 DataSet
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="transaction">事务</param>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string dbName, System.Data.Common.DbTransaction transaction, string procName, params object[] parameterValues) {
            DataSet ds = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DateTime time = DateTime.Now;
                ds = db.ExecuteDataSet(transaction, procName, parameterValues);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(procName, GetSqlParams(parameterValues), "POSApplication.GetDataSet(transaction,procName,params object[])", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataSet(transaction,procName,params object[])", procName);
                ds = null;
#if DEBUG
                throw ex;
#endif
            }
            return ds;
        }

        /// <summary>
        /// 根据存储过程的名称和对应参数执行一个带事务获取 DataSet
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns></returns>
        public static DataSet GetDataSet(System.Data.Common.DbTransaction transaction, string procName, params object[] parameterValues) {
            return GetDataSet("", transaction, procName, parameterValues);
        }

        /// <summary>
        /// 执行带参数的存储过程，反回DataSet,DbCommand
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="procName">存储过程名称</param>
        /// <param name="outDbComd">返回DbCommand</param>
        /// <param name="paramsValue">参数</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string dbName, string procName, out DbCommand outDbComd, params SqlParameter[] paramsValue) {
            DataSet ds = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                outDbComd = db.GetStoredProcCommand(procName);
                outDbComd.Parameters.AddRange(paramsValue);
                DateTime time = DateTime.Now;
                ds = db.ExecuteDataSet(outDbComd);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(procName, GetSqlParams(paramsValue), "POSApplication.GetDataSet(procName,outDbComd,params SqlParameter[])", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataSet(procName,outDbComd,params SqlParameter[])", procName);
                outDbComd = null;
                ds = null;
#if DEBUG
                throw ex;
#endif
            }
            return ds;
        }

        /// <summary>
        /// 执行带参数的存储过程，反回DataSet,DbCommand
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="procName">存储过程名称</param>
        /// <param name="outDbComd">返回DbCommand</param>
        /// <param name="paramsValue">参数</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string procName, out DbCommand outDbComd, params SqlParameter[] paramsValue) {
            return GetDataSet("", procName, out outDbComd, paramsValue);
        }

        #endregion

        #region 执行SQL语句 返回DataSet

        /// <summary>
        /// 执行SQL 返回DataSet
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="cmdText">执行文本</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string dbName, string cmdText, params DbParameter[] paramsValues) {
            DataSet ds = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DateTime time = DateTime.Now;
                DbCommand dbComd = db.GetSqlStringCommand(cmdText);
                dbComd.Parameters.AddRange(paramsValues);
                ds = db.ExecuteDataSet(dbComd);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(cmdText, GetSqlParams(paramsValues), "POSApplication.GetDataSet(cmdText,params DbParameter[])", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataSet(cmdText,params DbParameter[])", cmdText);
                ds = null;
#if DEBUG
                throw ex;
#endif
            }
            return ds;
        }

        /// <summary>
        /// 执行SQL 返回DataSet
        /// </summary>
        /// <param name="cmdText">执行文本</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string cmdText, params DbParameter[] paramsValues) {
            return GetDataSet("", cmdText, paramsValues);
        }

        /// <summary>
        /// 根据 CommandType 执行SQL，返回dataset
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="cmdText">要执行的文本</param>
        /// <param name="cmdType">命令</param>
        /// <param name="paramsValues">参数</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string dbName, string cmdText, CommandType cmdType, params DbParameter[] paramsValues)
        {
            DataSet ds = null;
            DbCommand dbcmd = null;
            switch(cmdType)
            {
                case CommandType.Text: ds = GetDataSet(dbName, cmdText, paramsValues); break;
                case CommandType.StoredProcedure:
                    try
                    {
                        Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                        dbcmd = db.GetStoredProcCommand(cmdText);
                        dbcmd.Parameters.AddRange(paramsValues);
                        DateTime time = DateTime.Now;
                        ds = db.ExecuteDataSet(dbcmd);
                        DateTime time2 = DateTime.Now;
                        Log.WriteSQLLog(cmdText, GetSqlParams(paramsValues), "POSApplication.GetDataSet(cmdText,CommandType,params DbParameter[])", time, time2, (time2 - time).Milliseconds);
                    }
                    catch(Exception ex)
                    {
                        Log.WriteErrorLog(ex.Message, "POSApplication.GetDataSet(cmdText,CommandType,params DbParameter[])", cmdText);
                        dbcmd = null;
                        ds = null;
                    }
                    break;
            }
            return ds;
        }

        /// <summary>
        /// 根据 CommandType 执行SQL，返回dataset
        /// </summary>
        /// <param name="cmdText">要执行的文本</param>
        /// <param name="cmdType">命令</param>
        /// <param name="paramsValues">参数</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string cmdText, CommandType cmdType, params DbParameter[] paramsValues)
        {
            return GetDataSet("", cmdText, cmdType, paramsValues);
        }

        /// <summary>
        /// 根据 CommandType 执行带事务的SQL 返回DataSet
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="transaction">事务</param>
        /// <param name="cmdText">SQL</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string dbName, System.Data.Common.DbTransaction transaction, string cmdText, params SqlParameter[] paramsValues) {
            DataSet ds = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DbCommand dbComd = db.GetSqlStringCommand(cmdText);
                dbComd.Parameters.AddRange(paramsValues);
                DateTime time = DateTime.Now;
                ds = db.ExecuteDataSet(dbComd, transaction);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(cmdText, GetSqlParams(paramsValues), "POSApplication.GetDataSet(transaction,cmdText,params SqlParameter[])", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataSet(transaction,cmdText,params SqlParameter[])", cmdText);
                ds = null;
#if DEBUG
                throw ex;
#endif
            }
            return ds;
        }

        /// <summary>
        /// 根据 CommandType 执行带事务的SQL 返回DataSet
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="cmdText">SQL</param>
        /// <returns></returns>
        public static DataSet GetDataSet(System.Data.Common.DbTransaction transaction, string cmdText, params SqlParameter[] paramsValues) {
            return GetDataSet("", transaction, cmdText, paramsValues);
        }

        #endregion

        #region 执行commad 返回DataSet

        /// <summary>
        /// 执行 command 返回DataSet
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="command">命令</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string dbName, System.Data.Common.DbCommand command) {
            DataSet ds = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DateTime time = DateTime.Now;
                ds = db.ExecuteDataSet(command);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(command.CommandText, GetSqlParams(command.Parameters), "POSApplication.GetDataSet(command)", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataSet(command)", "");
                ds = null;
#if DEBUG
                throw ex;
#endif
            }
            return ds;
        }

        /// <summary>
        /// 执行 command 返回DataSet
        /// </summary>
        /// <param name="command">命令</param>
        /// <returns></returns>
        public static DataSet GetDataSet(System.Data.Common.DbCommand command) {
            return GetDataSet("", command);
        }

        /// <summary>
        /// 执行 command 返回DataSet
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="transaction">事务</param>
        /// <param name="command">命令</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string dbName, System.Data.Common.DbTransaction transaction, System.Data.Common.DbCommand command) {
            DataSet ds = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DateTime time = DateTime.Now;
                ds = db.ExecuteDataSet(command, transaction);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(command.CommandText, GetSqlParams(command.Parameters), "POSApplication.GetDataSet(transaction,command)", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataSet(transaction,command)", "");
                ds = null;
#if DEBUG
                throw ex;
#endif
            }
            return ds;
        }

        /// <summary>
        /// 执行 command 返回DataSet
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="command">命令</param>
        /// <returns></returns>
        public static DataSet GetDataSet(System.Data.Common.DbTransaction transaction, System.Data.Common.DbCommand command) {
            return GetDataSet("", transaction, command);
        }

        #endregion

        #endregion

        #region ExecuteNonQuery(新)

        /// <summary>
        /// 执行 Delete,Update,Insert 语句，返回int
        /// </summary>
        /// <param name="dbName">数据名称</param>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string dbName, string strSql, params DbParameter[] paramsValues)
        {
            int retValue = -1;
            try
            {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DbCommand dbCmd = db.GetSqlStringCommand(strSql);
                dbCmd.Parameters.AddRange(paramsValues);
                DateTime time = DateTime.Now;
                retValue = db.ExecuteNonQuery(dbCmd);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(strSql, GetSqlParams(paramsValues), "POSApplication.ExecuteNonQuery(commandText,params DbParameter[])", time, time2, (time2 - time).Milliseconds);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, "POSApplication.ExecuteNonQuery(commandText,params DbParameter[])", strSql);
                retValue = -1;
#if DEBUG
                throw ex;
#endif
            }
            return retValue;
        }

        /// <summary>
        /// 执行 Delete,Update,Insert 语句，返回int
        /// </summary>
        /// <param name="dbName">数据名称</param>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string dbName, string strSql)
        {
            int retValue = -1;
            try
            {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DbCommand dbCmd = db.GetSqlStringCommand(strSql);
                DateTime time = DateTime.Now;
                retValue = db.ExecuteNonQuery(dbCmd);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(strSql, string.Empty, "POSApplication.ExecuteNonQuery(commandText,params DbParameter[])", time, time2, (time2 - time).Milliseconds);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, "POSApplication.ExecuteNonQuery(commandText,params DbParameter[])", strSql);
                retValue = -1;
#if DEBUG
                throw ex;
#endif
            }
            return retValue;
        }
        /// <summary>
        /// 执行 Delete,Update,Insert 语句，返回int
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string strSql, params DbParameter[] paramsValues) {
            return ExecuteNonQuery("", strSql, paramsValues);
        }

        /// <summary>
        /// 执行 Delete,Update,Insert 语句，返回int
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string dbName, string procName, params object[] parameterValues) {
            int retValue = -1;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DateTime time = DateTime.Now;
                retValue = db.ExecuteNonQuery(procName, parameterValues);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(procName, GetSqlParams(parameterValues), "POSApplication.ExecuteNonQuery(procName,params object[])", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.ExecuteNonQuery(procName,params object[])", procName);
                retValue = -1;
#if DEBUG
                throw ex;
#endif
            }
            return retValue;
        }

        /// <summary>
        /// 执行 Delete,Update,Insert 语句，返回int
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string procName, params object[] parameterValues) {
            return ExecuteNonQuery("", procName, parameterValues);
        }

        /// <summary>
        /// 执行DbCommand，返回int
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="dbCmd">执行命令</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string dbName, DbCommand dbCmd) {
            int retValue = -1;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DateTime time = DateTime.Now;
                retValue = db.ExecuteNonQuery(dbCmd);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(dbCmd.CommandText, GetSqlParams(dbCmd.Parameters), "POSApplication.ExecuteNonQuery(DbCommand)", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.ExecuteNonQuery(DbCommand)", "DbCommand");
                retValue = -1;
#if DEBUG
                throw ex;
#endif
            }
            return retValue;
        }

        /// <summary>
        /// 执行DbCommand，返回int
        /// </summary>
        /// <param name="dbCmd">执行命令</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(DbCommand dbCmd) {
            return ExecuteNonQuery("", dbCmd);
        }

        /// <summary>
        /// 根据 CommandType 执行SQL，返回INT
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="cmdText">SQL</param>
        /// <param name="cmdType">命令</param>
        /// <param name="paramsValues">参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string dbName, string cmdText, CommandType cmdType, params DbParameter[] paramsValues)
        {
            int retValue = -1;
            switch(cmdType)
            {
                case CommandType.Text: retValue = ExecuteNonQuery(dbName, cmdText, paramsValues); break;
                case CommandType.StoredProcedure:
                    try
                    {
                        Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                        DbCommand dbcmd = db.GetStoredProcCommand(cmdText);
                        dbcmd.Parameters.AddRange(paramsValues);
                        DateTime time = DateTime.Now;
                        retValue = db.ExecuteNonQuery(dbcmd);
                        DateTime time2 = DateTime.Now;
                        Log.WriteSQLLog(cmdText, GetSqlParams(paramsValues), "POSApplication.ExecuteNonQuery(commandText,CommandType,params DbParameter[])", time, time2, (time2 - time).Milliseconds);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteErrorLog(ex.Message, "POSApplication.ExecuteNonQuery(commandText,CommandType,params DbParameter[])", cmdText);
                        retValue = -1;
#if DEBUG
                throw ex;
#endif
                    }
                    break;
            }
            return retValue;
        }

        /// <summary>
        /// 根据 CommandType 执行SQL，返回INT
        /// </summary>
        /// <param name="cmdText">SQL</param>
        /// <param name="cmdType">命令</param>
        /// <param name="paramsValues">参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdText, CommandType cmdType, params DbParameter[] paramsValues)
        {
            return ExecuteNonQuery("", cmdText, cmdType, paramsValues);
        }

        #endregion

        #region GetFieldValue(新)

        /// <summary>
        /// 执行SQL，获取某一列的值
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public static object GetFieldValue(string dbName, string strSql, params DbParameter[] paramsValues) {
            object obj = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DbCommand dbCmd = db.GetSqlStringCommand(strSql);
                dbCmd.Parameters.AddRange(paramsValues);
                DateTime time = DateTime.Now;
                obj = db.ExecuteScalar(dbCmd);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(strSql, GetSqlParams(paramsValues), "POSApplication.GetFieldValue(commandText,params DbParameter[])", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetFieldValue(commandText,params DbParameter[])", strSql);
                obj = null;
#if DEBUG
                throw ex;
#endif
            }
            return obj;
        }

        /// <summary>
        /// 执行SQL，获取某一列的值
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public static object GetFieldValue(string strSql, params DbParameter[] paramsValues) {
            return GetFieldValue("", strSql, paramsValues);
        }

        /// <summary>
        /// 执行proc，获取某一列的值
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns></returns>
        public static object GetFieldValue(string dbName, string procName, params object[] parameterValues) {
            object obj = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DateTime time = DateTime.Now;
                obj = db.ExecuteScalar(procName, parameterValues);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(procName, GetSqlParams(parameterValues), "POSApplication.GetFieldValue(procName,params object[])", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetFieldValue(procName,params object[])", procName);
                obj = null;
#if DEBUG
                throw ex;
#endif
            }
            return obj;
        }

        /// <summary>
        /// 执行proc，获取某一列的值
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns></returns>
        public static object GetFieldValue(string procName, params object[] parameterValues) {
            return GetFieldValue("", procName, parameterValues);
        }

        /// <summary>
        /// 执行DbCommand，获取某一列的值
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="dbCmd">执行命令</param>
        /// <returns></returns>
        public static object GetFieldValue(string dbName, DbCommand dbCmd) {
            object obj = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DateTime time = DateTime.Now;
                obj = db.ExecuteScalar(dbCmd);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(dbCmd.CommandText, GetSqlParams(dbCmd.Parameters), "POSApplication.GetFieldValue(DbCommand)", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetFieldValue(DbCommand)", "DbCommand");
                obj = null;
#if DEBUG
                throw ex;
#endif
            }
            return obj;
        }

        /// <summary>
        /// 执行DbCommand，获取某一列的值
        /// </summary>
        /// <param name="dbCmd">执行命令</param>
        /// <returns></returns>
        public static object GetFieldValue(DbCommand dbCmd) {
            return GetFieldValue("", dbCmd);
        }

        /// <summary>
        /// 根据 CommandType 执行SQL，返回object
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="cmdText">SQL</param>
        /// <param name="cmdType">命令</param>
        /// <param name="paramsValues">参数</param>
        /// <returns></returns>
        public static object GetFieldValue(string dbName, string cmdText, CommandType cmdType, params DbParameter[] paramsValues)
        {
            object retValue = null;
            switch(cmdType)
            {
                case CommandType.Text: retValue = GetFieldValue(dbName, cmdText, paramsValues); break;
                case CommandType.StoredProcedure:
                    try
                    {
                        Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                        DbCommand dbcmd = db.GetStoredProcCommand(cmdText);
                        dbcmd.Parameters.AddRange(paramsValues);
                        DateTime time = DateTime.Now;
                        retValue = db.ExecuteScalar(dbcmd);
                        DateTime time2 = DateTime.Now;
                        Log.WriteSQLLog(cmdText, GetSqlParams(paramsValues), "POSApplication.GetFieldValue(commandText,CommandType,params DbParameter[])", time, time2, (time2 - time).Milliseconds);
                    }
                    catch(Exception ex)
                    {
                        Log.WriteErrorLog(ex.Message, "POSApplication.GetFieldValue(commandText,CommandType,params DbParameter[])", cmdText);
                        retValue = null;
#if DEBUG
                throw ex;
#endif
                    }
                    break;
            }
            return retValue;
        }

        /// <summary>
        /// 根据 CommandType 执行SQL，返回object
        /// </summary>
        /// <param name="cmdText">SQL</param>
        /// <param name="cmdType">命令</param>
        /// <param name="paramsValues">参数</param>
        /// <returns></returns>
        public static object GetFieldValue(string cmdText, CommandType cmdType, params DbParameter[] paramsValues)
        {
            return GetFieldValue("", cmdText, cmdType, paramsValues);
        }

        #endregion

        #region GetDataReader(新)

        /// <summary>
        /// 执行SQL，返回IDataReader
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public static IDataReader GetDataReader(string dbName, string strSql, params DbParameter[] paramsValues) {
            IDataReader idr = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DbCommand dbCmd = db.GetSqlStringCommand(strSql);
                dbCmd.Parameters.AddRange(paramsValues);
                DateTime time = DateTime.Now;
                idr = db.ExecuteReader(dbCmd);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(strSql, GetSqlParams(paramsValues), "POSApplication.GetDataReader(commandText,params DbParameter[])", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataReader(commandText,params DbParameter[])", strSql);
                idr = null;
#if DEBUG
                throw ex;
#endif
            }
            return idr;
        }

        /// <summary>
        /// 执行SQL，返回IDataReader
        /// </summary>
        /// <param name="cmdType">执行命令</param>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public static IDataReader GetDataReader(string strSql, params DbParameter[] paramsValues) {
            return GetDataReader("", strSql, paramsValues);
        }

        /// <summary>
        /// 执行PROC，返回IDataReader
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns></returns>
        public static IDataReader GetDataReader(string dbName, string procName, params object[] parameterValues) {
            IDataReader idr = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DateTime time = DateTime.Now;
                idr = db.ExecuteReader(procName, parameterValues);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(procName, GetSqlParams(parameterValues), "POSApplication.GetDataReader(procName,params object[])", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataReader(procName,params object[])", procName);
                idr = null;
#if DEBUG
                throw ex;
#endif
            }
            return idr;
        }

        /// <summary>
        /// 执行PROC，返回IDataReader
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns></returns>
        public static IDataReader GetDataReader(string procName, params object[] parameterValues) {
            return GetDataReader("", procName, parameterValues);
        }

        /// <summary>
        /// 执行DbCommand，返回IDataReader
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="dbCmd">执行命令</param>
        /// <returns></returns>
        public static IDataReader GetDataReader(string dbName, DbCommand dbCmd) {
            IDataReader idr = null;
            try {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DateTime time = DateTime.Now;
                idr = db.ExecuteReader(dbCmd);
                DateTime time2 = DateTime.Now;
                Log.WriteSQLLog(dbCmd.CommandText, GetSqlParams(dbCmd.Parameters), "POSApplication.GetDataReader(DbCommand)", time, time2, (time2 - time).Milliseconds);
            } catch (Exception ex) {
                Log.WriteErrorLog(ex.Message, "POSApplication.GetDataReader(DbCommand)", "DbCommand");
                idr = null;
#if DEBUG
                throw ex;
#endif
            }
            return idr;
        }

        /// <summary>
        /// 执行DbCommand，返回IDataReader
        /// </summary>
        /// <param name="dbCmd">执行命令</param>
        /// <returns></returns>
        public static IDataReader GetDataReader(DbCommand dbCmd) {
            return GetDataReader("", dbCmd);
        }

        /// <summary>
        /// 根据 CommandType 执行SQL，返回IDataReader
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="cmdText">SQL</param>
        /// <param name="cmdType">命令</param>
        /// <param name="paramsValues">参数</param>
        /// <returns></returns>
        public static IDataReader GetDataReader(string dbName, string cmdText, CommandType cmdType, params DbParameter[] paramsValues)
        {
            IDataReader retValue = null;
            switch(cmdType)
            {
                case CommandType.Text: retValue = GetDataReader(dbName, cmdText, paramsValues); break;
                case CommandType.StoredProcedure:
                    try
                    {
                        Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                        DbCommand dbcmd = db.GetStoredProcCommand(cmdText);
                        dbcmd.Parameters.AddRange(paramsValues);
                        DateTime time = DateTime.Now;
                        retValue = db.ExecuteReader(dbcmd);
                        DateTime time2 = DateTime.Now;
                        Log.WriteSQLLog(cmdText, GetSqlParams(paramsValues), "POSApplication.GetDataReader(commandText,CommandType,params DbParameter[])", time, time2, (time2 - time).Milliseconds);
                    }
                    catch(Exception ex)
                    {
                        Log.WriteErrorLog(ex.Message, "POSApplication.GetDataReader(commandText,CommandType,params DbParameter[])", cmdText);
                        retValue = null;
#if DEBUG
                throw ex;
#endif
                    }
                    break;
            }
            return retValue;
        }

        /// <summary>
        /// 根据 CommandType 执行SQL，返回IDataReader
        /// </summary>
        /// <param name="cmdText">SQL</param>
        /// <param name="cmdType">命令</param>
        /// <param name="paramsValues">参数</param>
        /// <returns></returns>
        public static IDataReader GetDataReader(string cmdText, CommandType cmdType, params DbParameter[] paramsValues)
        {
            return GetDataReader("", cmdText, cmdType, paramsValues);
        }

        #endregion

        #region CreateDataBase

        /// <summary>
        /// 创建数据源
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <returns></returns>
        private static Database CreateDataBase(string dbName = "")
        {
            Database db = null;
            try
            {
                if ("".Equals(dbName))
                    db = DatabaseFactory.CreateDatabase();
                else
                    db = DatabaseFactory.CreateDatabase(dbName);
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#endif
            }
            return db;
        }

        #endregion

        #region 添加参数
        /// <summary>
        /// MySql添加参数(只传入)
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="value">参数值</param> 
        /// <param name="sqlDbType">参数类型</param>
        /// <returns></returns>
        public static MySqlParameter AddParamInput(string ParamName, object value,MySqlDbType mySqlDbType)
        {
            MySqlParameter arg = new MySqlParameter();
            arg.ParameterName = ParamName;
            arg.MySqlDbType = mySqlDbType;
            arg.Value = value;
            arg.Direction = ParameterDirection.Input;
            return arg;
        }

        /// <summary>
        /// ODBC添加参数(只传入)
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <returns></returns>
        public static OdbcParameter AddParamInput(string ParamName, object value, OdbcType odbcType) {
            OdbcParameter arg = new OdbcParameter();
            arg.ParameterName = ParamName;
            arg.OdbcType = odbcType;
            arg.Value = value;
            arg.Direction = ParameterDirection.Input;
            return arg;
        }

        /// <summary>
        /// 添加参数(只传入)
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="value">参数值</param> 
        /// <returns></returns>
        public static SqlParameter AddParamInput(string ParamName, object value)
        {
            SqlParameter arg = new SqlParameter();
            arg.ParameterName = ParamName;
            arg.Value = value;
            arg.Direction = ParameterDirection.Input;
            return arg;
        }

        /// <summary>
        /// 添加参数(只传入)
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <returns></returns>
        public static SqlParameter AddParamInput(string ParamName, object value, SqlDbType sqlDbType)
        {
            SqlParameter arg = new SqlParameter();

            arg.ParameterName = ParamName;
            arg.SqlDbType = sqlDbType;
            arg.Value = value;
            arg.Direction = ParameterDirection.Input;
            return arg;
        }
        /// <summary>
        /// 添加参数(只传入)
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <param name="size">size</param>
        /// <returns></returns>
        public static SqlParameter AddParamInput(string ParamName, object value, SqlDbType sqlDbType, int size)
        {
            SqlParameter arg = new SqlParameter();

            arg.ParameterName = ParamName;
            arg.SqlDbType = sqlDbType;
            arg.Value = value;
            arg.Direction = ParameterDirection.Input;
            arg.Size = size;
            return arg;
        }

        /// <summary>
        /// 添加参数(只传出)
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <returns></returns>
        public static SqlParameter AddParamOuput(string ParamName, SqlDbType sqlDbType)
        {
            SqlParameter arg = new SqlParameter();

            arg.ParameterName = ParamName;
            arg.SqlDbType = sqlDbType;
            arg.Direction = ParameterDirection.Output;

            return arg;
        }

        /// <summary>
        /// 添加参数(只传出)
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <returns></returns>
        public static SqlParameter AddParamOuput(string ParamName, SqlDbType sqlDbType, int size)
        {
            SqlParameter arg = new SqlParameter();

            arg.ParameterName = ParamName;
            arg.SqlDbType = sqlDbType;
            arg.Direction = ParameterDirection.Output;
            arg.Size = size;
            return arg;
        }

        /// <summary>
        /// 添加参数 
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="ParameterDirection">是什么类型的参数</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <param name="size">size</param>
        /// <returns></returns>
        public static SqlParameter AddParam(string ParamName, object value, ParameterDirection Direction, SqlDbType sqlDbType, int size)
        {
            SqlParameter arg = new SqlParameter(); 
            arg.ParameterName = ParamName;
            arg.SqlDbType = sqlDbType;
            arg.Value = value;
            arg.Direction = Direction;
            arg.Size = size;
            return arg;
        }

        #endregion

        /// <summary>
        /// 执行SQL 返回DataSet
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="cmdText">执行文本</param>
        /// <returns></returns>
        public static bool ExecTrans(string dbName, List<string> cmdTextList)
        {
            bool result = true;
            int count = 0;
            try
            {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();

                using (IDbConnection ldbConn = db.CreateConnection())
                {
                    ldbConn.Open();
                    DbCommand dbComd = null;
                    IDbTransaction trans = ldbConn.BeginTransaction();

                    foreach(string cmdText in cmdTextList)
                    {
                        DateTime time = DateTime.Now;
                        try
                        {
                            dbComd = db.GetSqlStringCommand(cmdText);
                            count += db.ExecuteNonQuery(dbComd);
                        }
                        catch(Exception ex)
                        {
                            DateTime time2 = DateTime.Now;
                            Log.WriteSQLLog(cmdText, "", "POSApplication.ExecTrans(cmdText,params DbParameter[])", time, time2, (time2 - time).Milliseconds);
#if DEBUG
                            throw ex;
#endif
                        }
                    }

                    if (count < cmdTextList.Count)
                    {
                        result = false;
                        trans.Rollback();
                    }
                    else
                        trans.Commit();
                }

                
            }
            catch (Exception ex)
            {
                result = false;
                Log.WriteErrorLog(ex.Message, "POSApplication.ExecTrans(string dbName, List<string> cmdTextList)", "");
#if DEBUG
                throw ex;
#endif
            }
            return result;
        }

        public static bool ExecTrans(string dbName, string cmdText)
        {
            int result;

            using (TransactionScope ts = new TransactionScope())//使整个代码块成为事务性代码
            {
                #region 在这里编写需要具备Transaction的代码
                try
                {
                Database db = dbName != "" ? DatabaseFactory.CreateDatabase(dbName) : DatabaseFactory.CreateDatabase();
                DbCommand dbCmd = db.GetSqlStringCommand(cmdText);
                DateTime time = DateTime.Now;
                result = db.ExecuteNonQuery(dbCmd);
                DateTime time2 = DateTime.Now;

                Log.WriteSQLLog(cmdText, "", "POSApplication.ExecTrans(string dbName, string cmdText)", time, time2, (time2 - time).Milliseconds);
                }
                catch (Exception ex)
                {
                    result = -1;
                    Log.WriteErrorLog(ex.Message, "POSApplication.ExecTrans(string dbName, string cmdText)", "");
                }

                #endregion

                ts.Complete();

                if (result < 0)
                    return false;
                else
                    return true;
            }
        }
    }
}
