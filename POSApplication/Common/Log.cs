using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Windows.Forms;

namespace POSApplication
{
    /// <summary>
    /// 日志管理类
    /// </summary>
    public class Log
    {
        //是否记录SQL操作日志
        private static bool IsSQLlog = false;

        /// <summary>
        /// 销售日志默认路径
        /// </summary>
        private static string SaleLogPath
        {
            get
            {
                //判断默认Log文件夹是否存在，不存在就创建
                DirectoryInfo logDirectory = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\Log\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();
                return System.Windows.Forms.Application.StartupPath + "\\Log\\" + string.Format("销售_{0}.log", DateTime.Now.ToString("yyy-MM-dd"));
            }
        }

        /// <summary>
        /// 默认保存日志的路径
        /// </summary>
        public static string LogFilePath
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\Log\\" + string.Format("log_{0}.log", DateTime.Now.ToString("yyy-MM-dd"));
            }
        }

        /// <summary>
        /// 默认SQL日志文件路径
        /// </summary>
        private static string SqlLogFilePath
        {
            get
            {
                //判断默认Log文件夹是否存在，不存在就创建
                DirectoryInfo logDirectory = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\Log\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();
                return System.Windows.Forms.Application.StartupPath + "\\Log\\" + string.Format("sql_{0}.log", DateTime.Now.ToString("yyy-MM-dd"));
            }
        }

        /// <summary>
        /// 默认错误日志路径
        /// </summary>
        private static string ErrorLogFilePath
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\Log\\" + string.Format("error_{0}.log", DateTime.Now.ToString("yyy-MM-dd"));
            }
        }

        private static string NormalLogFilePath
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\Log\\" + string.Format("normal_{0}.log", DateTime.Now.ToString("yyy-MM-dd"));
            }
        }

        #region Write/Read
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="LogFileName">文件</param>
        /// <param name="Strings">消息</param>
        public static void Write(string LogFileName, string Strings)
        {
            Write(LogFileName, Strings, Encoding.Default);
        }

        /// <summary>
        /// 写日志gb2312 UTF-8
        /// </summary>
        /// <param name="fileName">文件</param>
        /// <param name="str">消息</param>
        /// <param name="encoding">编码gb2312 UTF-8</param>
        public static bool Write(string fileName, string str, Encoding encoding)
        {
            try
            {


                if (!File.Exists(fileName))
                {
                    FileStream f = File.Create(fileName);
                    f.Close();
                    f.Dispose();
                }
                StreamWriter f2 = new StreamWriter(fileName, true, encoding);
                f2.WriteLine(str);
                f2.Close();
                f2.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
#if DEBUG
                throw ex;
#endif
            }
        }
        #endregion

        /// <summary>
        /// 写SQL日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="param">参数</param>
        /// <param name="source">来源</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="spendTimes">执行时间</param>
        public static void WriteSQLLog(string message, string param, string source, DateTime beginDate, DateTime endDate, int spendTimes)
        {
            if (IsSQLlog)
            {
                StringBuilder strLog = new StringBuilder();
                strLog.Append("/*********************************** " + DateTime.Now.ToString() + " ***********************************/" + Environment.NewLine);
                strLog.Append("{Source:" + source + "}");
                strLog.Append("{Message:" + message + "}");
                strLog.Append("{Params:" + param + "}");
                strLog.Append("{BeginDate:" + beginDate.ToString("yyyy-MM-dd HH:mm:ss:fff") + "}");
                strLog.Append("{EndDate:" + endDate.ToString("yyyy-MM-dd HH:mm:ss:fff") + "}");
                strLog.Append("{SpendTimes:" + spendTimes + "}");
                Write(SqlLogFilePath, strLog.ToString());
            }
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="operate">操作者</param>
        /// <param name="source">来源</param>
        /// <param name="filePath">路径(可为空)</param>
        public static void WriteNormalLog(string message, string operate, string source, string filePath)
        {
            StringBuilder strLog = new StringBuilder();
            strLog.Append("/*********************************** [@" + DateTime.Now.ToString() + "] ***********************************/" + Environment.NewLine);
            strLog.Append("{Message:" + message + "}{Operator:" + operate + "}{Source:" + source + "}");
            Write(filePath, strLog.ToString());
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="operate">操作者</param>
        /// <param name="source">来源</param>
        public static void WriteNormalLog(string message, string operate, string source)
        {
            WriteNormalLog(message, operate, source, NormalLogFilePath);
        }

        /// <summary>
        /// 自定义日志名称
        /// </summary>
        /// <param name="message"></param>
        /// <param name="operate"></param>
        /// <param name="source"></param>
        /// <param name="logName"></param>
        public static void WriteLog(string message, string operate, string source, string logName)
        {
            string logFilePath = System.Windows.Forms.Application.StartupPath + "\\Log\\" + string.Format("{0}_{1}.log", logName, DateTime.Now.ToString("yyy-MM-dd"));
            //string logFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Log/" + string.Format("{0}_{1}.log",logName, DateTime.Now.ToString("yyy-MM-dd")));
            WriteNormalLog(message, operate, source, logFilePath);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="operate">操作者</param>
        /// <param name="source">来源</param>
        public static void WriteErrorLog(string message, string source, string operation)
        {
            StringBuilder strLog = new StringBuilder();
            strLog.Append("/*********************************** [@" + DateTime.Now.ToString() + "] ***********************************/" + Environment.NewLine);
            strLog.Append("{Message:" + message + "}{Operation:" + operation + "}{Source:" + source + "}");
            Write(ErrorLogFilePath, strLog.ToString());
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="msg">错误消息</param>
        public static void WriteErrorLog(string message)
        {
            WriteErrorLog(message, "", "");
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="msg">错误消息</param>
        public static void WriteErrorLog(string filePath, string message)
        {
            StringBuilder strLog = new StringBuilder();
            strLog.Append("/*********************************** [@" + DateTime.Now.ToString() + "] ***********************************/" + Environment.NewLine);
            strLog.Append("{Message:" + message + "}{Operator:}{Source:}");
            Write(filePath, strLog.ToString());
        }

        /// <summary>
        /// 写删除记录日志
        /// </summary>
        /// <param name="staff">员工</param>
        /// <param name="sup">收银主管</param>
        /// <param name="index">序号</param>
        /// <param name="filePath"></param>
        public static void WriteDeleteLog(string staff, string sup, int index)
        {
            string log = "员工：" + staff + " 于" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒")
                            + "获得收银主管：" + sup + "的许可，删除小票：" + GlobalParams.xph.ToString().PadLeft(4, '0') + "的第" + index + "条记录。";
            Write(SaleLogPath, log);
        }

        /// <summary>
        /// 写整单删除记录日志
        /// </summary>
        /// <param name="staff">员工</param>
        /// <param name="sup">收银主管</param>
        /// <param name="index">序号</param>
        /// <param name="filePath"></param>
        public static void WriteDeleteLog(string staff, string sup)
        {
            string log = "员工：" + staff + " 于" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒")
                            + "获得收银主管：" + sup + "的许可，删除整单小票：" + GlobalParams.xph.ToString().PadLeft(4, '0') + "。";
            Write(SaleLogPath, log);
        }

        /// <summary>
        /// 单品打折日志
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="sup"></param>
        /// <param name="index"></param>
        /// <param name="discount"></param>
        public static void WriteDiscountLog(string staff, string sup, int index, string discount)
        {
            string log = "员工：" + staff + " 于" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒")
                            + "获得收银主管：" + sup + "的许可，对小票：" + GlobalParams.xph.ToString().PadLeft(4, '0') + "的第" + index + "条记录进行打折：" + discount + "。";
            Write(SaleLogPath, log);
        }

        /// <summary>
        /// 整体打折日志
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="sup"></param>
        /// <param name="discount"></param>
        public static void WriteDiscountOverAllLog(string staff, string sup, string discount)
        {
            string log = "员工：" + staff + " 于" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒")
                                + "获得收银主管：" + sup + "的许可，对小票：" + GlobalParams.xph.ToString().PadLeft(4, '0') + "整体打折：" + discount + "。";
            Write(SaleLogPath, log);
        }
    }
}
