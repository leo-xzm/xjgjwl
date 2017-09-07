using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using POSApplication.BLL;
using System.Windows.Forms;

namespace POSApplication.Common
{
    public class DataUpload
    {
        /// <summary>
        /// 前后台交互，上传销售明细数据
        /// </summary>
        /// <param name="flag">是否是 forSure 的交互</param>
        /// <returns></returns>
        public static int Upload(string source)
        {
            //1 不能访问
            //2 有 control.tab
            //3 有 error.tab
            //4 后台未回收数据
            //5 正常状态
            //A:1,2; B:3,4; C:5
            int state = 0;

            //-1 A 查询 unsure 表数据出错
            //-2 B 导出数据到本地 DBF 文件出错
            //-3 C 查询 unsure 表数据出错
            //-4 C 导出数据到本地 DBF 文件出错
            //-5 C backups 表没有数据, 停止导出
            //1 A 新数据追加到 backups 表
            //2 A 没有新数据可追加
            //3 A 将 backups 数据存入 unsure 表
            //4 A 将新数据存入 unsure 表
            //5 B unsure 表数据追加到 backups 表，然后清空 unsure 表
            //6 B 本地没有历史数据无法重新上传，出现数据丢失，后台请核对
            //7 B 新数据追加到 backups 表
            //8 B 没有新数据
            //9 C 清空 unsure 表，新数据追加到 backups 表
            //10 C 没有新数据可追加
            //11 C 新数据覆盖 backups 表
            //12 C 没有新数据可覆盖
            int ret = 0;

            #region 开始交互
            //服务端文件夹是否可以访问
            DirectoryInfo dataDirectory = new DirectoryInfo(GlobalParams.ServerPath);

            //不能访问-不确定状态
            if (!dataDirectory.Exists)
            {
                state = 1;
                Log.WriteNormalLog(string.Format("无法连接服务端文件夹：{0}，停止与后台交互", 
                    GlobalParams.ServerPath), "Upload()", source);

                //不确定状态下的操作
                ret = DataOperator.UnsureOperation(source);
            }
            //可以访问
            else
            {
                FileInfo[] files = dataDirectory.GetFiles();
                List<string> filesName = new List<string>();

                foreach (FileInfo f in files)
                    filesName.Add(f.Name);

                //有 control.tab - 不确定状态
                if (filesName.Contains("control.tab"))
                {
                    state = 2;
                    Log.WriteNormalLog(string.Format("后台正在操作服务端文件夹：{0}，停止与后台交互", 
                        GlobalParams.ServerPath), "Upload()", source);

                    //不确定状态下的操作
                    ret = DataOperator.UnsureOperation(source);
                }
                //有 xsxb.dbf 或者 error.tab - 重新上传状态
                else if (filesName.Contains("xsxb.dbf") || filesName.Contains("error.tab"))
                {
                    if (filesName.Contains("error.tab"))
                    {
                        state = 3;
                        Log.WriteNormalLog("出现 error.tab，准备重新上传数据", "Upload()", source);
                    }
                    else
                    {
                        state = 4;
                        Log.WriteNormalLog("后台未回收数据，准备重新上传数据", "Upload()", source);
                    }

                    PutPosTab("Upload()", source);

                    //重新上传状态下的操作
                    ret = DataOperator.ReUploadOperation(source);

                    if (ret > 0)
                    {
                        try
                        {
                            //上传文件
                            File.Copy(GlobalParams.xsxbPath, GlobalParams.xsxbServerPath, true);
                            Log.WriteNormalLog("上传销售明细数据完毕", "Upload()", source);
                        }
                        catch (Exception ex)
                        {
                            Log.WriteErrorLog(ex.Message, ex.Source, "上传本地 DBF 文件到服务端");
                        }

                        if (filesName.Contains("error.tab"))
                        {
                            File.Delete(GlobalParams.ServerPath + "error.tab");
                            Log.WriteNormalLog("删除 error.tab", "Upload()", source);
                        }
                    }

                    DeletePosTab("Upload()", source);
                }
                //没有 control.tab 、xsxb.dbf、error.tab -正常状态
                else
                {
                    state = 5;
                    Log.WriteNormalLog("准备上传新数据", "Upload()", source);

                    PutPosTab("Upload()", source);

                    //正常状态下的操作
                    ret = DataOperator.EmptyOperation(source);

                    if (ret > 0)
                    {
                        try
                        {
                            //上传文件
                            File.Copy(GlobalParams.xsxbPath, GlobalParams.xsxbServerPath, true);
                        }
                        catch (Exception ex)
                        {
                            Log.WriteErrorLog(ex.Message, ex.Source, "上传本地 DBF 文件到服务端");
                        }

                        Log.WriteNormalLog("上传新数据完毕", "Upload()", source);
                    }

                    DeletePosTab("Upload()", source);
                }
            }
            #endregion 结束交互

            return ret;
        }

        /// <summary>
        /// 向服务端放 pos.tab
        /// </summary>
        /// <param name="operate">调用者</param>
        public static void PutPosTab(string operate, string source)
        {
            try
            {
                File.Copy(GlobalParams.PosTabLocalPath, GlobalParams.PosTabServerPath, true);
                Log.WriteNormalLog(string.Format("前台开始操作服务端文件夹：{0}", GlobalParams.ServerPath), operate, source);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, ex.Source, "向服务端放 pos.tab");
            }
        }

        /// <summary>
        /// 删除服务端 pos.tab
        /// </summary>
        /// <param name="operate"></param>
        public static void DeletePosTab(string operate, string source)
        {
            try
            {
                File.Delete(GlobalParams.PosTabServerPath);
                Log.WriteNormalLog(string.Format("前台结束操作服务端文件夹：{0}", GlobalParams.ServerPath), operate, source);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, ex.Source, "删除服务端 pos.tab");
            }
        }

        /// <summary>
        /// 导出单日销售明细数据，并上传服务器
        /// </summary>
        public static bool UploadSalesDay(ref string message, string date = "")
        {
            bool ret = true;

            int result = 0;
            string filePath = "";

            if (date != "")
            {
                int year;
                int month;
                int day;

                year = Convert.ToInt32(date.Substring(0, 4));
                month = Convert.ToInt32(date.Substring(4, 2));
                day = Convert.ToInt32(date.Substring(6, 2));

                GlobalParams.DayDesignate = new DateTime(year, month, day);

                filePath = GlobalParams.xbDesignateLocalPath;

                //复制原始文件到单次销售明细 dbf 文件本地保存路径（覆盖）
                //dbf文件无法真正删除老数据，每次上传前先使用比较小的原始文件
                File.Copy(GlobalParams.xsxbIniPath, filePath, true);

                //导出 指定日期 单日销售明细数据到本地 DBF 文件
                result = new DataOperator().SalesDayDesignatetoDBF(date);
            }
            else
            {
                filePath = GlobalParams.xbLocalPath;

                //复制原始文件到单次销售明细 dbf 文件本地保存路径（覆盖）
                //dbf文件无法真正删除老数据，每次上传前先使用比较小的原始文件
                File.Copy(GlobalParams.xsxbIniPath, filePath, true);

                //导出单日销售明细数据到本地 DBF 文件
                result = new DataOperator().SalesDaytoDBF();
            }

            if (result > 0)
            {
                if (File.Exists(filePath))
                {
                    try
                    {
                        if (date != "")
                            File.Copy(filePath, GlobalParams.xbDesignateServerPath, true);
                        else
                            File.Copy(filePath, GlobalParams.xbServerPath, true);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteErrorLog(ex.Message, ex.Source, "上传单日销售明细数据");
                        message = ex.Message;
                        ret = false;
                    }
                }
                else
                {
                    ret = false;
                    message = "本地文件不存在";
                    Log.WriteErrorLog("上传单日销售明细数据，本地文件不存在");
                }
            }
            else
            {
                ret = false;
                
                //删除之前复制的文件
                File.Delete(filePath);

                switch (result)
                {
                    case 0:
                        message = "暂无数据";
                        break;
                    case -1:
                        message = "DBF 文件不存在或路径出错";
                        break;
                    case -2:
                        message = "ODBC 连接 DBF 文件出错";
                        break;
                }
            }

            return ret;
        }

        /// <summary>
        /// 关机/重启/注销/数据上传时，再次确认服务端文件夹状态，并交互
        /// </summary>
        /// <returns></returns>
        public static bool ForSure(ref string message)
        {
            bool result = true;

            Dictionary<int, string> UploadResult = new Dictionary<int, string>() 
            { 
                {-1, "A 查询 unsure 表数据出错"},
                {-2, "B 导出数据到本地 DBF 文件出错"},
                {-3, "C 查询 unsure 表数据出错"},
                {-4, "C 导出数据到本地 DBF 文件出错"},
                {-5, "backups 表没有数据, 停止导出"}
            };

            //先清空单词销售明细数据，保证没有新数据
            if (new SalesBLL().DeleteSalesTime() >= 0)
            {
                //开始交互
                int ret = Upload("再次确认服务端文件夹状态");

                if (ret < 0 && ret != -5)
                {
                    message = "再次确认服务端文件夹状态并交互：" + UploadResult[ret];
                    result = false;
                }
            }
            else
            {
                message = "清空单次销售明细数据出错，请重试";
                result = false;
            }

            return result;
        }

    }
}
