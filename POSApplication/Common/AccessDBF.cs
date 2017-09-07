using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data;
using System.IO;
using System.Data.OleDb;


namespace POSApplication.Common
{
    public class AccessDBF
    {
        public AccessDBF() { }

        /// <summary>
        /// 读取DBF文件，转化成DataTable
        /// </summary>
        /// <param name="fileName">读取的DBFDBASE3文件名加路径</param>
        /// <returns>返回的数据集DT</returns>
        public DataTable RdeadDBFV3(string fileName)
        {
            DataTable dt = new DataTable();
            //连接字符串，引用foxpro驱动
            OdbcConnection conn = new OdbcConnection();
            string connStr = @"Driver={Microsoft Visual FoxPro Driver};SourceType=DBF;SourceDB=" + fileName + ";Exclusive=No;NULL=NO;Collate=Machine;BACKGROUNDFETCH=NO;DELETED=YES";

            try
            {
                conn.ConnectionString = connStr;
                conn.Open();

                string sql = @"select * from " + fileName;
                OdbcDataAdapter da = new OdbcDataAdapter(sql, conn);

                da.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
                Log.WriteErrorLog(ex.Message, ex.Source, "读取DBF文件，转化成DataTable");
#if DEBUG
                throw ex;
#endif
            }

            if (conn.State == ConnectionState.Open)
                conn.Close();

            conn.Dispose();
            return dt;
        }

        /// <summary>
        /// 将 DataTable 追加到数据库指定的表中
        /// </summary>
        /// <param name="dt">存入的DataTable</param>
        /// <param name="tablename">插入的数据库相应的表名</param>
        /// <returns></returns>
        public bool WriteDataToDB(DataTable dt, string tablename)
        {
            bool result = true;
            int ret = 0;
            string colNames = "";

            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    Log.WriteErrorLog("DataTable 中没有数据", string.Format("WriteDataToDB(DataTable dt, string tablename):{0}", tablename), "");
                    return result;
                }

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    colNames += dt.Columns[i].ColumnName + ",";
                }

                colNames = colNames.TrimEnd(',');

                string cmd = "";
                string colValues;
                string cmdmode = string.Format("insert into {0}({1}) values({{0}});", tablename, colNames);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    colValues = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j].GetType() == typeof(DBNull))
                        {
                            colValues += "NULL,";
                            continue;
                        }
                        if (dt.Columns[j].DataType == typeof(string))
                            colValues += string.Format("'{0}',", dt.Rows[i][j]);
                        else if (dt.Columns[j].DataType == typeof(int) || dt.Columns[j].DataType == typeof(float) || dt.Columns[j].DataType == typeof(double) || dt.Columns[j].DataType == typeof(Decimal))
                        {
                            colValues += string.Format("{0},", dt.Rows[i][j]);
                        }
                        else if (dt.Columns[j].DataType == typeof(DateTime))
                        {
                            colValues += string.Format("cast('{0}' as datetime),", dt.Rows[i][j]);
                        }
                        else if (dt.Columns[j].DataType == typeof(bool))
                        {
                            colValues += string.Format("{0},", dt.Rows[i][j].ToString());
                        }
                        else
                            colValues += string.Format("'{0}',", dt.Rows[i][j]);
                    }
                    cmd = string.Format(cmdmode, colValues.TrimEnd(','));

                    ret = DataAccess.ExecuteNonQuery("POSMySQL", cmd);
                }
                if (ret == -1)
                {
                    result = false;
                    Log.WriteErrorLog("向表中插入数据出错", string.Format("WriteDataToDB(DataTable dt, string tablename):{0}", tablename), "");
                }
            }
            catch(Exception ex)
            {
                result = false;
                Log.WriteErrorLog(ex.Message, ex.Source,"");
#if DEBUG
                throw ex;
#endif
            }
            
            return result;
        }


        /// <summary>
        /// 更新 DBF 数据文件
        /// </summary>
        /// <param name="dt">需要插入的数据</param>
        /// <param name="fileName">原始 DBF文件+路径</param>
        /// <returns>-1为未找到原始DBF文件；-2为插入异常；1为插入成功</returns>
        public int UpdateDBF(DataTable dt, string fileName)
        {
            int rtn = 0;
            OdbcConnection ole_conn = new OdbcConnection();
            string connStr = @"Driver={Microsoft Visual FoxPro Driver};SourceType=DBF;SourceDB=" + fileName + ";Exclusive=No;NULL=NO;Collate=Machine;BACKGROUNDFETCH=NO;DELETED=NO";

            try
            {
                ole_conn.ConnectionString = connStr;
               // Log.WriteErrorLog("执行打开");
                ole_conn.Open();
                //Log.WriteErrorLog("打开");
                if (File.Exists(fileName))
                {
                    //已经存在则删除原有数据 
                    OdbcCommand cmd1 = new OdbcCommand("DELETE FROM " + fileName, ole_conn);
                    rtn = cmd1.ExecuteNonQuery();
                    //Log.WriteErrorLog("开始导入数据");
                    //导入数据
                    string tname = fileName;
                    string colNames = "";
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        colNames += dt.Columns[i].ColumnName + ",";
                    }
                    colNames = colNames.TrimEnd(',');
                    string cmd = "";
                    string colValues;
                    string cmdmode = string.Format("insert into {0} ({1}) values({{0}});", tname, colNames);
                   // Log.WriteErrorLog("执行循环");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        colValues = "";
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].GetType() == typeof(DBNull))
                            {
                                colValues += "NULL,";
                                continue;
                            }
                            if (dt.Columns[j].DataType == typeof(string))
                                colValues += string.Format("'{0}',", dt.Rows[i][j]);
                            else if (dt.Columns[j].DataType == typeof(int) || dt.Columns[j].DataType == typeof(float) || dt.Columns[j].DataType == typeof(double) || dt.Columns[j].DataType == typeof(Decimal))
                            {
                                colValues += string.Format("{0},", dt.Rows[i][j]);
                            }
                            else if (dt.Columns[j].DataType == typeof(DateTime))
                            {
                                colValues += string.Format("cast('{0}' as datetime),", dt.Rows[i][j]);
                            }
                            else if (dt.Columns[j].DataType == typeof(bool))
                            {
                                colValues += string.Format("{0},", dt.Rows[i][j].ToString());
                            }
                            else
                                colValues += string.Format("{0},", dt.Rows[i][j]);
                        }
                        cmd = string.Format(cmdmode, colValues.TrimEnd(','));

                        OdbcCommand cmd2 = new OdbcCommand(cmd, ole_conn);
                        rtn = cmd2.ExecuteNonQuery();
                       // Log.WriteErrorLog("执行结束");
                    }
                }
                else
                {
                    Log.WriteErrorLog("DBF 文件不存在", string.Format("UpdateDBF(DataTable dt, string fileName), dt:{0}, fileName:{1}", dt, fileName), "");
                    rtn = -1;
                }
            }
            catch (System.Exception ex)
            {
                //Log.WriteErrorLog("异常");
                rtn = -2;
                Log.WriteErrorLog(ex.Message, string.Format("UpdateDBF(DataTable dt, string fileName), dt:{0}, fileName:{1}", dt, fileName), "");
#if DEBUG
                throw ex;
#endif
            }

            if (ole_conn.State == ConnectionState.Open)
                ole_conn.Close();

            ole_conn.Dispose();

            return rtn;
        }
    }
}
