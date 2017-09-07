using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.Common;
using System.IO;

namespace POSApplication
{
    public class GlobalParams
    {
        //小票号，格式：八位数字，重置规则（一天内累加）
        //登陆 frmMain 后，读取 app.config 里的小票号
        public static int xph = 1;

        public GlobalParams()
        {
            xph = 1;
        }

        /// <summary>
        /// 程序根目录
        /// </summary>
        public static string RootPath
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\";
            }
        }

        /// <summary>
        /// 程序数据目录
        /// </summary>
        public static string DataPath
        {
            get
            {
                //判断默认文件夹是否存在，不存在就创建
                DirectoryInfo logDirectory = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\Data\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();

                return System.Windows.Forms.Application.StartupPath + "\\Data\\";
            }
        }

        /// <summary>
        /// 挂单目录
        /// </summary>
        public static string suspendOrderPath
        {
            get
            {
                //判断默认文件夹是否存在，不存在就创建
                DirectoryInfo logDirectory = new DirectoryInfo(DataPath + "SuspendOrder\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();

                return DataPath + "SuspendOrder\\";
            }
        }

        /// <summary>
        /// 服务端目录
        /// </summary>
        public static string ServerPath
        {
            get
            {
                //return System.Windows.Forms.Application.StartupPath + "\\Server\\";
                if (ConfigHelper.GetAppConfig("JH") == "")
                    return "Z:\\pos01\\";
                else
                    return "Z:\\pos" + ConfigHelper.GetAppConfig("JH").Substring(3, 2) + "\\";
            }
        }

        /// <summary>
        /// 指定日期
        /// </summary>
        public static DateTime DayDesignate { get; set; }

        #region 单次销售明细 xsxb 相关
        /// <summary>
        /// 原始单次销售明细 dbf 文件本地路径
        /// </summary>
        public static string xsxbIniPath
        {
            get
            {
                return RootPath +"xsxb.dbf";
            }
        }

        /// <summary>
        /// 单次销售明细 dbf 文件本地路径
        /// </summary>
        public static string xsxbPath
        {
            get
            {
                return DataPath + "xsxb.dbf";
            }
        }

        /// <summary>
        /// 单次销售明细 dbf 文件本地备份路径
        /// </summary>
        public static string xsxbBackupsPath
        {
            get
            {
                //判断文件夹是否存在，不存在就创建
                DirectoryInfo logDirectory = new DirectoryInfo(DataPath + "Backups\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();

                return DataPath + "Backups\\xsxb.dbf";
            }
        }

        /// <summary>
        /// 单次销售明细 dbf 文件服务端路径
        /// </summary>
        public static string xsxbServerPath
        {
            get
            {
                return ServerPath + "xsxb.dbf";
            }
        }
        #endregion

        #region 单日销售明细 xbyyMMdd 相关
        /// <summary>
        /// 单日销售明细 dbf 文件本地路径
        /// </summary>
        public static string xbLocalPath
        {
            get
            {
                //判断年份文件夹是否存在，不存在就创建
                DirectoryInfo logDirectory = new DirectoryInfo(DataPath + DateTime.Now.Year + "\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();

                //判断月份文件夹是否存在，不存在就创建
                logDirectory = new DirectoryInfo(DataPath + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();

                return DataPath + DateTime.Now.Year+ "\\" + DateTime.Now.Month + "\\" 
                    + "xb" + DateTime.Now.ToString("yyMMdd") + ".dbf";
            }
        }

        /// <summary>
        /// 指定日期的单日销售明细 dbf 文件本地路径
        /// </summary>
        public static string xbDesignateLocalPath
        {
            get
            {
                //判断年份文件夹是否存在，不存在就创建
                DirectoryInfo logDirectory = new DirectoryInfo(DataPath + DayDesignate.Year + "\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();

                //判断月份文件夹是否存在，不存在就创建
                logDirectory = new DirectoryInfo(DataPath + DateTime.Now.Year + "\\" + DayDesignate.Month + "\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();

                return DataPath + DayDesignate.Year + "\\" + DayDesignate.Month + "\\"
                    + "xb" + DayDesignate.ToString("yyMMdd") + ".dbf";
            }
        }

        /// <summary>
        /// 单日销售明细 dbf 文件服务器路径
        /// </summary>
        public static string xbServerPath
        {
            get
            {
                return ServerPath + "bak\\xb" + DateTime.Now.ToString("yyMMdd") + ".dbf";
            }
        }

        /// <summary>
        /// 指定日期，单日销售明细 dbf 文件服务器路径
        /// </summary>
        public static string xbDesignateServerPath
        {
            get
            {
                return ServerPath + "bak\\xb" + DayDesignate.ToString("yyMMdd") + ".dbf";
            }
        }
        #endregion

        #region 小票单文件
        /// <summary>
        /// 当日小票文件本地路径
        /// </summary>
        public static string recLocalPath
        {
            get
            {
                //判断年份文件夹是否存在，不存在就创建
                DirectoryInfo logDirectory = new DirectoryInfo(DataPath + DateTime.Now.Year + "\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();

                //判断月份文件夹是否存在，不存在就创建
                logDirectory = new DirectoryInfo(DataPath + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();

                return DataPath + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\"
                    + DateTime.Now.ToString("yyyyMMdd") + ".rec";
            }
        }

        /// <summary>
        /// 指定日期的小票文件本地路径
        /// </summary>
        public static string recDesignateLocalPath
        {
            get
            {
                return DataPath + DayDesignate.Year + "\\" + DayDesignate.Month + "\\"
                    + DayDesignate.ToString("yyyyMMdd") + ".rec";
            }
        }
        #endregion

        #region 控制标识文件
        /// <summary>
        /// 上传销售明细时，服务端控制标识文件
        /// </summary>
        public static string ControlTabPath
        {
            get
            {
                return ServerPath + "control.tab";
            }
        }

        /// <summary>
        /// 上传销售明细时，POS控制标识文件本地路径
        /// </summary>
        public static string PosTabLocalPath
        {
            get
            {
                return DataPath + "pos.tab";
            }
        }

        /// <summary>
        /// 上传销售明细时，POS控制标识文件服务端路径
        /// </summary>
        public static string PosTabServerPath
        {
            get
            {
                return ServerPath + "pos.tab";
            }
        }
        #endregion

        #region 主档相关
        /// <summary>
        /// 服务端的商品变化主档路径
        /// </summary>
        public static string SpitServerPath
        {
            get
            {
                return ServerPath + "spit.txt";
            }
        }

        /// <summary>
        /// 本地的商品主档路径
        /// </summary>
        public static string SpitLocalPath
        {
            get
            {
                return DataPath + "spit.txt";
            }
        }

        /// <summary>
        /// 服务端的商品主档路径
        /// </summary>
        public static string spmxServerPath
        {
            get
            {
                return ServerPath + "spmx.txt";
            }
        }

        /// <summary>
        /// 本地的商品主档路径
        /// </summary>
        public static string spmxLocalPath
        {
            get
            {
                return DataPath + "spmx.txt";
            }
        }

        /// <summary>
        /// 服务端的员工库主档路径
        /// </summary>
        public static string ygkServerPath
        {
            get
            {
                return ServerPath + "ygk.txt";
            }
        }

        /// <summary>
        /// 本地的商品员工库主档路径
        /// </summary>
        public static string ygkLocalPath
        {
            get
            {
                return DataPath + "ygk.txt";
            }
        }

        /// <summary>
        /// 服务端的结算方式主档路径
        /// </summary>
        public static string jsfsServerPath
        {
            get
            {
                return ServerPath + "jsfs.txt";
            }
        }

        /// <summary>
        /// 本地的结算方式主档路径
        /// </summary>
        public static string jsfsLocalPath
        {
            get
            {
                return DataPath + "jsfs.txt";
            }
        }

        /// <summary>
        /// 服务端的时段折扣主档路径
        /// </summary>
        public static string yhxsServerPath
        {
            get
            {
                return ServerPath + "yhxs.txt";
            }
        }

        /// <summary>
        /// 本地的时段折扣主档路径
        /// </summary>
        public static string yhxsLocalPath
        {
            get
            {
                return DataPath + "yhxs.txt";
            }
        }

        /// <summary>
        /// 服务端的促销主档路径
        /// </summary>
        public static string ptServerPath
        {
            get
            {
                return ServerPath + "pt.txt";
            }
        }

        /// <summary>
        /// 本地的促销主档路径
        /// </summary>
        public static string ptLocalPath
        {
            get
            {
                return DataPath + "pt.txt";
            }
        }

        /// <summary>
        /// 服务端的团购主档路径
        /// </summary>
        public static string jobServerPath
        {
            get
            {
                return ServerPath + "job.txt";
            }
        }

        /// <summary>
        /// 本地的团购主档路径
        /// </summary>
        public static string jobLocalPath
        {
            get
            {
                return DataPath + "job.txt";
            }
        }
        #endregion

        /// <summary>
        /// 购物单保存默认路径
        /// </summary>
        public static string orderPath
        {
            get
            {
                return DataPath + "temp.order";
            }
        }
        public static string refundPath
        {
            get
            {
                return DataPath + "refund.order";
            }
        }

        /// <summary>
        /// 挂单保存默认路径
        /// </summary>
        public static string susPath
        {
            get
            {
                return suspendOrderPath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".sus";
            }
        }
    }
}
