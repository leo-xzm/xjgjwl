using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

//com.xj.card
namespace POSApplication.Common
{
    public class CardTran
    {
     
        /// <summary>
        /// 卡交易函数
        /// </summary>
        /// <param name="mdh">门店号 5位，左补0</param>
        /// <param name="jh">机号 5位 左补0</param>
        /// <param name="kh">卡号 32位</param>
        /// <param name="xph">小票号 8位 左补0</param>
        /// <param name="je">金额 两位小数 如果没有小数确保.00</param>
        /// <returns>返回： “OK:流水号”表示成功，冒号后面跟流水号，“ERROR:××××”表示失败，后面是错误信息，直接显示到界面</returns>      
        public static string cardTran(string mdh,string jh, string kh, string xph,string je)     //测试用,ref string reqstr,ref string refresult)
        {
            string rtn = "ERROR";
            byte[] b2 = System.Text.Encoding.GetEncoding("UTF-8").GetBytes("直销交易");
            string tradeType = System.Text.Encoding.GetEncoding("UTF-8").GetString(b2); //交易类型，默认值是直销交易，防止码制错误，直接先转换
            b2 = System.Text.Encoding.GetEncoding("UTF-8").GetBytes("直销中心商品销售");
            string productName = System.Text.Encoding.GetEncoding("UTF-8").GetString(b2);
            string sellerNo = "Z000000004";//直销中心编号
            string url = "http://mail.shxjgj.com/webInfoTransfer/directsell.do";
            string detail = "";
            string result = "";
            Dictionary<string, string> config = new Dictionary<string, string>();
              
            try
            {
                //读取配置文件,获得 url和sellerNo
                config = getConfig();
                if (config["url"] != null && config["url"] != "")
                {
                    url = config["url"];
                    
                }

                if (config["sellerNo"] != null && config["sellerNo"] != "")
                {
                    sellerNo = config["sellerNo"];

                }

                //组织请求内容
                #region
                string requestNo = mdh + jh + xph + BuildRandomStr(5); //直销中心唯一交易流水  5位门店号+5位机号 +8位小票号 + 5位随机数（主要考虑同张小票多次交易）
                detail = "<SellerDirectProduct ProductName='" + productName + "' SellIncome='" + je + "' SellPrice='" + je + "' SellPieces='1' SellWeight='' />";
                string reqString = "";
                reqString = "type=directSell&sellXML=<SellerDirectManageBill requestNo='" + requestNo + "'  SellerNo='" + sellerNo + "'  BuyerCardNo='" + kh + "'  MoneyQty='" + je + "' TradeType='" + tradeType + "' >" +
                                detail +
                                "</SellerDirectManageBill>";
                //reqstr = reqString; //测试用
                #endregion

                //发起调用请求
                result = doPost(url,reqString);
                //refresult = result; //测试用
                //解析结果
                string error = "";
                string msg = "";

                error = GetValue(result, "error='", "'").Trim().ToUpper();
                if (error.Equals("FALSE"))
                {
                    rtn = "OK:" + requestNo;
                }
                else 
                {
                    msg = GetValue(result," msg='","'");
                    rtn = "ERROR:" + msg;
                }


            }
            catch (Exception e)
            {
                rtn = "ERROR:" + e.Message.ToString();
            }
           


            return rtn;
 
        }

        /// <summary>
        /// 会员卡余额查询
        /// </summary>
        /// <param name="cardNo">会员卡号</param>
        /// <returns>返回：“OK:会员名称|余额”表示成功，名称和余额直接用竖线符分隔，“ERROR:××××”表示失败，后面是错误信息，直接显示到界面</returns>
        public static string cardQuery(string cardNo)
        {
            string rtn = "ERROR";
            string result = "";
            string reqStr = "";
            Dictionary<string, string> config = new Dictionary<string, string>();
            string url = "http://mail.shxjgj.com/webInfoTransfer/directsell.do";

            try
            {
                //读取配置文件,获得 url和sellerNo
                config = getConfig();
                if (config["url"] != null && config["url"] != "")
                {
                    url = config["url"];
                }

                if ((cardNo == null) || cardNo.Equals(""))
                {
                    rtn = "ERROR:卡号为空！";
                    return rtn;
                }

                //组织请求内容

                reqStr = "type=getBalance&cardNo=" + cardNo;
               
                //发起调用请求
                result = doPost(url, reqStr);
                
                //发起调用请求
                result = doPost(url, reqStr);
                
                //refresult = result; //测试用
                //解析结果
                string error = "";
                string msg = "";
                string memberName = "";
                string balance = "";

                error = GetValue(result, "error='", "'").Trim().ToUpper();
                if (error.Equals("FALSE"))
                {
                    memberName = GetValue(result, "memberName='", "'");
                    balance = GetValue(result, "cardBalance='", "'");
                    rtn = "OK:" + memberName+"|"+ balance;
                }
                else
                {
                    msg = GetValue(result, " msg='", "'");
                    rtn = "ERROR:" + msg;
                }

            }
            catch (Exception e)
            {
                rtn = "ERROR:" + e.Message.ToString();
            }

            return rtn;
        }
       
       /// <summary>
       /// 取随机数
       /// </summary>
       /// <param name="length">随机数长度</param>
       /// <returns></returns>
        public static string BuildRandomStr(int length)
        {
            Random rand = new Random();

            int num = rand.Next();

            string str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                int n = length - str.Length;
                while (n > 0)
                {
                    str.Insert(0, "0");
                    n--;
                }
            }

            return str;
        }

        /// <summary>获得字符串中开始和结束字符串中间得值
        /// 获得字符串中开始和结束字符串中间得值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="s">开始</param>
        /// <param name="e">结束</param>
        /// <returns></returns> 
        public static string GetValue(string str, string s, string e)
        {
            Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(str).Value;
        }


        /// <summary>
        /// 调用http post方法
        /// </summary>
        /// <param name="url">调用的URL</param>
        /// <param name="postXML">请求参数</param>
        /// <returns>返回结果</returns>
        public static string doPost(string url,string postXML)
        {
            string result = string.Empty;
            //string reqstr = "";

            #region ---- 完成 HTTP POST 请求----
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.Method = "POST";

                req.KeepAlive = true;

                req.Timeout = 300*1000;//延时1分钟

                req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";//utf-8 gbk

                byte[] postData = Encoding.GetEncoding("UTF-8").GetBytes(postXML); //utf-8 gbk

                //String ss = Encoding.GetEncoding("UTF-8").GetString(postData);

               // reqstr = ss;

                Stream reqStream = req.GetRequestStream();

                reqStream.Write(postData, 0, postData.Length);

                reqStream.Close();


                HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();


                Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);

                Stream stream = null;

                StreamReader reader = null;

                stream = rsp.GetResponseStream();

                reader = new StreamReader(stream, encoding);

                result = reader.ReadToEnd();

                if (reader != null) reader.Close();

                if (stream != null) stream.Close();

                if (rsp != null) rsp.Close();
            }
            catch (WebException e)
            {
                //模拟返回
                result = "<EntityDataSet RequestDs=\""+url + "\"  msg=\""+ "本地http请求错误：" + e.Message.ToString() +"\" name=\"result\" error=\"false\"> </EntityDataSet>";
               
            }
            #endregion

            return Regex.Replace(result, @"[\x00-\x08\x0b-\x0c\x0e-\x1f]", "");

        }

        /// <summary>
        /// 读取配置文件;注意文件路径的设定
        /// </summary>
        /// <returns>返回配置</returns>
        public static Dictionary<String, String> getConfig()
        {
            Dictionary<string, string> rtn = new Dictionary<string, string>();
            string path = GlobalParams.RootPath + "card.ini";
           
            string[] tmp = new string[2];
            try
            {
                StreamReader sr = File.OpenText(path);
                string nextLine;
                while ((nextLine = sr.ReadLine()) != null)
                {
                   tmp =  nextLine.Split('=');
                   rtn.Add(tmp[0],tmp[1]); 
                }
                sr.Close();
            }
            catch (Exception e)
            {
                rtn.Add("url","");
                rtn.Add("sellerNo","");
                rtn.Add("error",e.Message.ToString());
            }
            
            return rtn;
        }
       
    }
}
