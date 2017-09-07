using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using POSApplication.BLL;
using POSApplication.Model;

namespace POSApplication.Common
{
    public class DataOperator
    {
        /// <summary>
        /// 更新销售相关表
        /// </summary>
        /// <param name="salesList"></param>
        /// <returns></returns>
        public bool UpdateSalesData(List<Sales> salesList)
        {
            #region 更新销售相关表

            //先清空单次销售明细数据
            if (new SalesBLL().DeleteSalesTime() < 0)
            {
                MessageBox.Show("数据清空出错，请重试");
                Log.WriteErrorLog("单次销售明细数据清空失败");
                return false;
            }

            //保存数据到单次销售明细表中，true 成功，false 失败
            bool result = new SalesBLL().AddSalesTime(salesList);

            //单次销售明细表插入出现错误，清空数据
            if (!result)
            {
                MessageBox.Show("单次销售数据保存出错，请重试");
                Log.WriteErrorLog("单次销售明细数据保存出错");

                if (new SalesBLL().DeleteSalesTime() < 0)
                {
                    MessageBox.Show("数据清空出错");
                    Log.WriteErrorLog("单次销售明细数据清空失败");
                }
                return false;
            }

            //保存数据到单日和总的销售明细表中，使用事务，失败就回滚
            if (!new SalesBLL().AddSalesData())
            {
                MessageBox.Show("销售数据保存出错，请重试");
                Log.WriteErrorLog("销售明细数据保存出错");

                if (new SalesBLL().DeleteSalesTime() < 0)
                {
                    MessageBox.Show("数据清空出错，请重试");
                    Log.WriteErrorLog("单次销售明细数据清空失败");
                }
                return false;
            }

            #endregion

            return true;
        }

        /// <summary>
        /// 读取指定 DBF 文件的数据到指定的表
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dbName"></param>
        public void DBFtoDB(string fileName, string dbName)
        {
            AccessDBF db = new AccessDBF();
            DataTable dt = new DataTable();

            //从 dbf 文件读取数据
            dt = db.RdeadDBFV3(fileName);
            //追加到指定的表
            bool ret = db.WriteDataToDB(dt, dbName);

            if (!ret)
                Log.WriteErrorLog("读取 DBF 文件数据失败", string.Format("DBFtoDB(string fileName, string dbName), fileName:{0}, dbName:{1}", fileName, dbName), "");
        }

        /// <summary>
        /// 导出单次销售明细数据到DBF，>0 为插入成功
        /// </summary>
        public int SalesBackupstoDBF()
        {
            int ret = 0;
            AccessDBF db = new AccessDBF();
            DataTable dt = new DataTable();
            try
            {
                string sql = "select * from sales_backups";
                dt = DataAccess.GetDataTable("POSMySQL", sql);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        #region 加 999 校验行

                        int jysl = dt.Rows.Count;
                        decimal ssje = 0;
                        for (int i = 0; i < jysl; i++)
                            ssje += Convert.ToDecimal(dt.Rows[i]["ssje"]);
                        DataRow dr = dt.NewRow();

                        dr["tzm"] = "999";
                        dr["spbh"] = "";
                        dr["txm"] = "";
                        dr["jyj"] = 0;
                        dr["jysl"] = jysl;
                        dr["ssje"] = ssje;
                        dr["xsrq"] = "";
                        dr["xssj"] = "";
                        dr["syybh"] = "";
                        dr["xph"] = "";
                        dr["jsfsbh"] = "";
                        dr["jh"] = "";
                        dr["yj"] = 0;
                        dr["zjj"] = 0;
                        dr["mcard"] = "";
                        dr["yhmx"] = 0;
                        dr["jfmx"] = 0;
                        dr["szbz"] = "";
                        dr["cxbh"] = "";
                        dr["xpxh"] = 0;
                        dr["cxid"] = "";
                        dr["Baka"] = "";
                        dr["Bakb"] = 0;
                        dt.Rows.Add(dr);

                        #endregion

                        //-1为未找到原始DBF文件；-2为插入异常；1为插入成功
                        ret = db.UpdateDBF(dt, GlobalParams.xsxbPath);
                    }
                    else
                    {
                        ret = -3;
                        Log.WriteNormalLog("backups 表没有数据, 停止导出", "SalesBackupstoDBF()", "");
                    }
                }
                else
                {
                    ret = -4;
                    Log.WriteErrorLog("查询 backups 表返回 null", "", "SalesBackupstoDBF()");
                }
            }
            catch (Exception ex)
            {
                ret = -5;
                Log.WriteErrorLog(ex.Message, ex.Source, "导出单次销售明细数据到DBF");
#if DEBUG
                throw ex;
#endif
            }

            return ret;
        }

        /// <summary>
        /// 导出单日销售明细数据到DBF
        /// </summary>
        public int SalesDaytoDBF()
        {
            int ret = 0;

            AccessDBF db = new AccessDBF();
            DataTable dt = new DataTable();
            try
            {
                string sql = "select * from sales_day";
                dt = DataAccess.GetDataTable("POSMySQL", sql);

                if (dt.Rows.Count == 0)
                    ret = 0;
                else
                    ret = db.UpdateDBF(dt, GlobalParams.xbLocalPath);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, ex.Source, "导出单日销售明细数据到DBF");
#if DEBUG
                throw ex;
#endif
            }

            return ret;
        }

        /// <summary>
        /// 指定日期，导出单日销售明细数据到DBF
        /// </summary>
        public int SalesDayDesignatetoDBF(string date)
        {
            int ret = 0;

            AccessDBF db = new AccessDBF();
            DataTable dt = new DataTable();
            try
            {
                string sql = "select * from sales_xsxb where xsrq = '" + date + "'";
                dt = DataAccess.GetDataTable("POSMySQL", sql);

                if (dt.Rows.Count == 0)
                    ret = 0;
                else
                    ret = db.UpdateDBF(dt, GlobalParams.xbDesignateLocalPath);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, ex.Source, "指定日期，导出单日销售明细数据到DBF");
#if DEBUG
                throw ex;
#endif
            }

            return ret;
        }

        #region 更新主档

        /// <summary>
        /// 读取主档txt文件，更新主档表数据
        /// </summary>
        /// <param name="fileName">txt文件完整路径</param>
        /// <param name="tableName">要更新的表</param>
        /// <returns></returns>
        public static int LoadData(string fileName, string tableName)
        {
            int ret = 0;
            string sql = "load data local infile \"" + fileName.Replace("\\", "/") + "\" into table " + tableName;
            try
            {
                ret = DataAccess.ExecuteNonQuery("POSMySQL", sql);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, ex.Source, string.Format("读取主档txt文件：{0}，更新主档表数据：{1}", fileName, tableName));
#if DEBUG
                throw ex;
#endif
            }
            return ret;
        }

        /// <summary>
        /// 读取变化主档txt文件，更新主档表数据
        /// </summary>
        /// <param name="fileName">txt文件完整路径</param>
        /// <param name="tableName">要更新的表</param>
        /// <returns></returns>
        public static int LoadData_Replace(string fileName, string tableName)
        {
            int ret = 0;
            string sql = "load data local infile \"" + fileName.Replace("\\", "/") + "\" replace into table " + tableName;
            try
            {
                ret = DataAccess.ExecuteNonQuery("POSMySQL", sql);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, ex.Source, string.Format("读取变化主档txt文件：{0}，更新主档表数据：{1}", fileName, tableName));
#if DEBUG
                throw ex;
#endif
            }
            return ret;
        }

        /// <summary>
        /// 更新商品变化主档
        /// </summary>
        public static void UpdateSpit()
        {
            if (File.Exists(GlobalParams.SpitServerPath))
            {
                try
                {
                    //从服务器复制 spit.txt 到本地
                    File.Copy(GlobalParams.SpitServerPath, GlobalParams.SpitLocalPath, true);
                }
                catch (Exception ex)
                {
                    Log.WriteErrorLog(ex.Message, ex.Source, "从服务器复制 spit.txt 到本地");
                }

                if (File.Exists(GlobalParams.SpitLocalPath))
                {
                    int ret = LoadData_Replace(GlobalParams.SpitLocalPath, "goods_spmx");

                    File.Delete(GlobalParams.SpitLocalPath);

                    if (ret < 0)
                        Log.WriteErrorLog("更新商品变化主档失败");
                    else
                    {
                        Log.WriteNormalLog(string.Format("小票号：{0} 结账时更新商品变化主档", GlobalParams.xph), "", "");
                        try
                        {
                            //删除服务器上的 spit.txt
                            File.Delete(GlobalParams.SpitServerPath);
                        }
                        catch (Exception ex)
                        {
                            Log.WriteErrorLog(ex.Message, ex.Source, "删除服务器上的 spit.txt");
                        }
                    }
                }
            }
        }

        //商品表 goods_spmx spmx.txt 需要先更新 spmx 再更新 spit
        //员工库表 staff_ygk ygk.txt
        //结算方式表 paymentmethod_jsfs jsfs.txt
        //时段折扣表 discount_yhxs yhxs.txt
        //促销表 没有主键 promotion_pt pt.txt
        //团购单 job.txt

        /// <summary>
        /// 更新主档
        /// </summary>
        /// <param name="serverPath">服务器路径</param>
        /// <param name="localPath">本地路径</param>
        /// <param name="tableName">表名</param>
        /// <param name="isReplace">是否差异更新</param>
        public static void UpdateData(string serverPath, string localPath, string tableName, bool isReplace)
        {
            if (File.Exists(serverPath))
            {
                try
                {
                    //从服务器复制到本地
                    File.Copy(serverPath, localPath, true);
                }
                catch (Exception ex)
                {
                    Log.WriteErrorLog(ex.Message, ex.Source, string.Format("从服务器复制到本地：{0}", serverPath));
                }

                if (File.Exists(localPath))
                {
                    int ret;

                    if (isReplace)
                        ret = LoadData_Replace(localPath, tableName);
                    else
                        ret = LoadData(localPath, tableName);

                    File.Delete(localPath);

                    if (ret < 0)
                        Log.WriteErrorLog(string.Format("更新主档失败{0}", tableName));
                    else
                    {
                        Log.WriteNormalLog(string.Format("更新主档{0}", tableName), "", "");
                        try
                        {
                            File.Delete(serverPath);
                        }
                        catch (Exception ex)
                        {
                            Log.WriteErrorLog(ex.Message, ex.Source, string.Format("删除服务器上的 txt：{0}", serverPath));
                        }
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 正常状态（文件夹没有 
        /// , control.tab, error.tab），更新本地 xsxb.dbf
        /// </summary>
        /// <returns></returns>
        public static int EmptyOperation(string source)
        {
            int ret = 0;
            int r = 0;

            int unsureDataNumber = new SalesBLL().SelectSalesUnsure();

            if (unsureDataNumber < 0)
            {
                ret = -3;
                Log.WriteErrorLog("查询 unsure 表数据出错", source, "EmptyOperation()");
            }
            else
            {
                //unsure 表有数据，说明 backups 里是延迟上传的数据
                if (unsureDataNumber > 0)
                {
                    //文件夹 empty 说明 unsure 表保存的数据已经成功回收，要清空
                    if (new SalesBLL().DeleteSalesUnsure() > 0)
                        Log.WriteNormalLog("清空 unsure 表", "EmptyOperation()", source);
                    else
                        Log.WriteErrorLog("清空 unsure 表", source, "EmptyOperation()");
                    

                    //将新数据追加到 backups 里
                    r = new SalesBLL().TimeToBackups();
                    if (r > 0)
                    {
                        ret = 9;
                        Log.WriteNormalLog("新数据追加到 backups 表", "EmptyOperation()", source);
                    }
                    else if (r < 0)
                        Log.WriteErrorLog("新数据追加到 backups 表", source, "EmptyOperation()");
                    else
                        ret = 10;//没有新数据可追加
                }
                //unsure 表没有数据，说明 backups 里是已上传的数据，已被成功回收
                else
                {
                    //覆盖新数据到 backups 表
                    r = new SalesBLL().TimeCoverBackups();
                    if (r > 0)
                    {
                        ret = 11;
                        Log.WriteNormalLog("新数据覆盖 backups 表", "EmptyOperation()", source);
                    }
                    else if (r < 0)
                        Log.WriteErrorLog("新数据覆盖 backups 表", source, "EmptyOperation()");
                    else
                        ret = 12;//没有新数据可覆盖
                }

                //复制原始文件到单次销售明细 dbf 文件本地保存路径（覆盖）
                //dbf文件无法真正删除老数据，每次上传前先使用比较小的原始文件
                File.Copy(GlobalParams.xsxbIniPath, GlobalParams.xsxbPath, true);

                try
                {
                    //导出单次销售明细数据到本地 DBF 文件
                    r = new DataOperator().SalesBackupstoDBF();
                    if (r > 0)
                        Log.WriteNormalLog("导出数据到本地 DBF 文件", "EmptyOperation()", source);
                    else if (r == -3)
                    {
                        ret = -5;
                        Log.WriteNormalLog("backups 表没有数据, 停止导出", "EmptyOperation()", source);
                    }
                }
                catch (Exception ex)
                {
                    ret = -4;
                    Log.WriteErrorLog(ex.Message, ex.Source, "EmptyOperation()");
                }
            }

            return ret;
        }

        /// <summary>
        /// 重新上传状态（文件夹有 xsxb.dbf 或者 error.tab）,更新本地 xsxb.dbf
        /// </summary>
        /// <returns></returns>
        public static int ReUploadOperation(string source)
        {
            int ret = 0;
            bool error = false;

            if (new SalesBLL().SelectSalesBackups() == 0 && new SalesBLL().SelectSalesUnsure() == 0)
            {
                error = true;
                Log.WriteNormalLog("本地没有历史数据无法重新上传，出现数据丢失，后台请核对", "ReUploadOperation()", source);
                MessageBox.Show("可能出现数据丢失，请前往后台回收数据，然后核对");
            }

            //将新数据追加到 backups 里
            int r = new SalesBLL().TimeToBackups();

            if (r > 0)
            {
                ret = 7;
                Log.WriteNormalLog("新数据追加到 backups 表", "ReUploadOperation()", source);
            }
            else if (r < 0)
                Log.WriteErrorLog("新数据追加到 backups 表", source, "ReUploadOperation()");
            else
                ret = 8;//没有新数据

            if (error)
                ret = 6;

            //将 unsure 表数据追加到 backups 里
            if (new SalesBLL().UnsureToBackups() > 0)
            {
                //如果 unsure 表里有数据，追加后就清空
                new SalesBLL().DeleteSalesUnsure();

                ret = 5;
                Log.WriteNormalLog("unsure 表数据追加到 backups 表，然后清空 unsure 表", "ReUploadOperation()", source);
            }



            //复制原始文件到单次销售明细 dbf 文件本地保存路径（覆盖）
            //dbf文件无法真正删除老数据，每次上传前先使用比较小的原始文件
            File.Copy(GlobalParams.xsxbIniPath, GlobalParams.xsxbPath, true);

            try
            {
                //导出单次销售明细数据到本地 DBF 文件
                r = new DataOperator().SalesBackupstoDBF();

                if (r > 0)
                    Log.WriteNormalLog("导出数据到本地 DBF 文件", "ReUploadOperation()", source);
            }
            catch (Exception ex)
            {
                ret = -2;
                Log.WriteErrorLog(ex.Message, ex.Source, "ReUploadOperation()");
            }

            return ret;
        }

        /// <summary>
        /// 不确定状态（文件夹有 control.tab，或者无法访问）
        /// </summary>
        /// <returns></returns>
        public static int UnsureOperation(string source)
        {
            int ret = 0;
            int r;

            int unsureDataNumber = new SalesBLL().SelectSalesUnsure();

            if (unsureDataNumber < 0)
            {
                ret = -1;
                Log.WriteErrorLog("查询 unsure 表数据出错", source, "UnsureOperation()");
            }
            else
            {
                //unsure 表有数据，说明 backups 里是延迟上传的数据
                if (unsureDataNumber > 0)
                {
                    //将新数据追加到 backups 里
                    r = new SalesBLL().TimeToBackups();

                    if (r > 0)
                    {
                        ret = 1;
                        Log.WriteNormalLog("新数据追加到 backups 表", "UnsureOperation()", source);
                    }
                    else if (r < 0)
                        Log.WriteErrorLog("新数据追加到 backups 表", source, "UnsureOperation()");
                    else
                        ret = 2;
                }
                //unsure 表没有数据，backups 如果有数据，成了不确定数据，需要移动到 unsure 里，然后新数据覆盖 backups（延迟上传）
                //unsure 表没有数据，backups 如果也没有数据，属于初始状态，
                else
                {
                    //将 backups 数据存入 unsure 表
                    int iniState = new SalesBLL().BackupsToUnsure();

                    if (iniState > 0)
                    {
                        ret = 3;
                        Log.WriteNormalLog("将 backups 数据存入 unsure 表", "UnsureOperation()", source);
                    }
                    //backups 里如果没数据，初始状态，还未上传过 dbf 文件
                    else if (iniState == 0)
                    {
                        Log.WriteNormalLog("初始状态，backups 没有数据", "UnsureOperation()", source);

                        //新数据存入 unsure 里，作为初始状态的标记
                        if (new SalesBLL().TimeToUnsure() > 0)
                        {
                            ret = 4;
                            Log.WriteNormalLog("将新数据存入 unsure 表", "UnsureOperation()", source);
                        }
                    }
                    else
                        Log.WriteErrorLog("将 backups 数据存入 unsure 表", source, "UnsureOperation()");

                    //覆盖新数据到 backups 里
                    r = new SalesBLL().TimeCoverBackups();
                    if (r > 0)
                        Log.WriteNormalLog("新数据覆盖 backups 表", "UnsureOperation()", source);
                    else if (r < 0)
                        Log.WriteErrorLog("新数据覆盖 backups 表", source, "UnsureOperation()");
                }
            }

            return ret;
        }

        /// <summary>
        /// 比较两个文件是否完全相等
        /// </summary>
        /// <param name="file1">第一个文件</param>
        /// <param name="file2">第二个文件</param>
        /// <returns></returns>
        public static bool CompareFile(string file1, string file2)
        {
            try
            {
                ////计算第一个文件的哈希值
                //var hash = System.Security.Cryptography.HashAlgorithm.Create();
                //var stream_1 = new System.IO.FileStream(file1, System.IO.FileMode.Open);
                //byte[] hashByte_1 = hash.ComputeHash(stream_1);
                //stream_1.Close();

                ////计算第二个文件的哈希值
                //var stream_2 = new System.IO.FileStream(file2, System.IO.FileMode.Open);
                //byte[] hashByte_2 = hash.ComputeHash(stream_2);
                //stream_2.Close();

                ////比较两个哈希值
                //if (BitConverter.ToString(hashByte_1) == BitConverter.ToString(hashByte_2))
                //    return true;
                //else
                //    return false;

                FileInfo fi1 = new FileInfo(file1);
                FileInfo fi2 = new FileInfo(file2);
                long size1 = fi1.Length;
                long size2 = fi2.Length;
                if (size1 == size2)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, ex.Source, string.Format("比较两个文件是否一致：{0},{1}", file1, file2));
                return false;
            }
        }
    }
}
