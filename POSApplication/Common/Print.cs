﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using POSApplication.Model;
using POSApplication.UI;
using POSApplication.BLL;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace POSApplication.Common
{
    /// <summary>
    /// 打印相关
    /// </summary>
    public class Print
    {
        //小票
        internal Receipt rec;

        /// <summary>
        /// 收款员
        /// </summary>
        public Staff staff { get; set; }

        /// <summary>
        /// 机号
        /// </summary>
        public string JH { get; set; }

        /// <summary>
        /// 现金票数字典
        /// </summary>
        public Dictionary<string, int> CashNum { get; set; }
        //现金
        public decimal Cash { get; set; }
        //支票
        public decimal Check { get; set; }

        public Print()
        {
            JH = ConfigHelper.GetAppConfig("JH");
        }

        private int getYc(double cm)
        {
            return (int)(cm / 25.4) * 100;
        }

        //是不是退款标志
        public bool isrefund { get; set; }
        #region 打印小票
        /// <summary>
        /// 打印小票
        /// </summary>
        public void PrintReceipt(bool isRefund)
        {
            isrefund = isRefund;
            //打印预览 
            PrintPreviewDialog ppd = new PrintPreviewDialog();

            PrintDocument pd = new PrintDocument();

            //设置边距
            Margins margin = new Margins(20, 20, 20, 20);

            pd.DefaultPageSettings.Margins = margin;

            //纸张设置默认

            PaperSize pageSize = new PaperSize("First custom size", getYc(58), 600);

            pd.DefaultPageSettings.PaperSize = pageSize;

            //打印事件设置            

            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

            //ppd.Document = pd;
            //ppd.ShowDialog();

            try
            {
                PrintController printController = new StandardPrintController();
                pd.PrintController = printController;
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
            }
        }

        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Printinit(e);
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="e"></param>
        private void Printinit(System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Graphics g = frmpay.CreateGraphics();
            Brush brush = new SolidBrush(Color.Black);

            int pcount = rec.order.Count;

            //string title = "光明·西郊品牌店";
            //if (isrefund) {
            //    title = "光明·西郊品牌店" + Environment.NewLine + "退款单";
            //}
            //修改小票头
            string title = "云品中心";
            if (isrefund)
            {
                title = "云品中心" + Environment.NewLine + "退款单";
            }


            //Font printFont = new Font("新宋体", 11, FontStyle.Bold);
            Font printFont = new Font("新宋体", 10, FontStyle.Bold);
            SizeF siF = e.Graphics.MeasureString(title, printFont);
            int titleWidth = Convert.ToInt32(siF.Width);

            //float leftMargin = e.MarginBounds.Left;
            //float topMargin = e.MarginBounds.Top;
            //float withMargin = e.MarginBounds.Width;
            float left = e.PageSettings.Margins.Left;
            //float top = e.PageSettings.Margins.Top;
            //float yPos = topMargin + (1 * printFont.GetHeight(e.Graphics));
            float width = e.PageSettings.PaperSize.Width - left - e.PageSettings.Margins.Right;
            //float height = e.PageSettings.PaperSize.Height - top - e.PageSettings.Margins.Bottom;
            //float yPostitle = Convert.ToInt32(siF.Width) - titleWidth + 98 + pcount * 6 * 15 / 4;
            //float yPostitle2 = Convert.ToInt32(siF.Width) - titleWidth + 143 + pcount * 6 * 15 / 4;
            //float yPosbody = Convert.ToInt32(siF2.Width) - titleWidth1 + 116 + pcount * 6 * 15 / 4 + 80;

            float yPostitle = 98 + pcount * 6 * 15 / 4;
            float yPostitle2 = 168 + pcount * 6 * 15 / 4;
            float yPosbody = 222 + pcount * 6 * 15 / 4;
            if (isrefund)
            {
                //yPostitle = 98 + pcount * 6 * 15 / 4;
                //yPostitle2 = 168 + pcount * 6 * 15 / 4;
                yPosbody = 120 + pcount * 6 * 15 / 4;
            }

            //打印小票标题
            e.Graphics.DrawString(title, printFont, brush, (width - titleWidth)/2+15, 3);

            //打印票号及商品列表
            Font goodsFont = new Font("Arial", 7, FontStyle.Bold);
            e.Graphics.DrawString(GetPrintStr(), goodsFont, brush, 8, 10);

            //打印优惠抹零和总计，字体比支付大
            Font printFont1 = new Font("新宋体", 13, FontStyle.Bold);
            e.Graphics.DrawString(GetPrintStr1(), printFont1, brush, 8, yPostitle);

            //打印支付汇总信息
            e.Graphics.DrawString(GetPrintStr2(), printFont, brush, 8, yPostitle2);

            //打印票尾
            Font footFont = new Font("Arial", 7, FontStyle.Bold);
            e.Graphics.DrawString(GetPrintStr3(), footFont, brush, 8, yPosbody);
        }

        //模拟打印到txt
        public string print(bool isFrefund)
        {
            isrefund = isFrefund;
            string receipt = "";
            if (isrefund)
            {
                receipt = "退款单" + Environment.NewLine;
            }
            if (isrefund)
            {
                receipt += GetPrintStr() + Environment.NewLine
                    + GetPrintStr3() + Environment.NewLine;
            }
            else
            {
                receipt += GetPrintStr() + Environment.NewLine
                    + GetPrintStr1() + Environment.NewLine
                    + GetPrintStr2() + Environment.NewLine
                    + GetPrintStr3() + Environment.NewLine;
            }
            string fileName = System.Windows.Forms.Application.StartupPath + "\\Log\\" + string.Format("小票_{0}.log", DateTime.Now.ToString("yyy-MM-dd"));
            Log.Write(fileName, receipt);

            return receipt;
        }

        /// <summary>
        /// 模块1 商品列表
        /// </summary>
        /// <returns></returns>
        public string GetPrintStr()
        {
            StringBuilder sb = new StringBuilder();

            decimal total = 0.00M;
            if (isrefund)
            {
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
            }
            sb.Append(Environment.NewLine);


            sb.Append("----------------------------------------------------------" + Environment.NewLine);

            sb.Append("票号:" + rec.xph + "   " + "机号:" + rec.jh + "   " + Environment.NewLine);

            sb.Append("----------------------------------------------------------" + Environment.NewLine);

            sb.Append("品名/货号" + "\t" + "数量" + "   " + "单价" + "   " + "小计" + Environment.NewLine);

            //收银明细

            for (int i = 0; i < rec.order.Count; i++)
            {
                sb.Append(rec.order[i].Name + Environment.NewLine + rec.order[i].spbh + "\t\t" + rec.order[i].JYSL + "    " + rec.order[i].JYJ + "    " + rec.order[i].XJ);
                total += rec.order[i].XJ;
                if (i != (rec.order.Count))
                    sb.Append(Environment.NewLine);
            }
            sb.Append("----------------------------------------------------------" + Environment.NewLine);
            if (isrefund)
            {
                sb.Append(Environment.NewLine);
                //sb.Append("总计件数: " + "-"+rec.goodsCount + "\t" + " 总计原价: " +"-"+rec.moneyTotal);
            }
            else
            {
                sb.Append("总计件数: " + rec.goodsCount + "\t" + " 总计原价: " + rec.moneyTotal);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 模块2
        /// </summary>
        /// <returns></returns>
        public string GetPrintStr1()
        {

            StringBuilder sb = new StringBuilder();

            if (isrefund)
            {
                sb.Append(Environment.NewLine);
                sb.Append("退款件数： " + rec.goodsCount + Environment.NewLine);
                sb.Append("退款金额： " + "-" + rec.actualTotal + Environment.NewLine);
                //sb.Append("优惠：      " +"-"+ rec.yh + Environment.NewLine);
                //sb.Append("抹零：      " + "-" + rec.zjj + Environment.NewLine);
                //sb.Append("总计：      " + "-" + rec.actualTotal + Environment.NewLine);
            }
            else
            {
                sb.Append("优惠：      " + rec.yh + Environment.NewLine);
                sb.Append("抹零：      " + rec.zjj + Environment.NewLine);
                sb.Append("总计：      " + rec.actualTotal + Environment.NewLine);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 模块3
        /// </summary>
        /// <returns></returns>
        public string GetPrintStr2()
        {
            StringBuilder sb = new StringBuilder();
            if (isrefund)
            {

                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                //sb.Append("刷卡支付：" + "\t" +"-"+ rec.cardPay + Environment.NewLine);
                //sb.Append("现金支付：" + "\t" + "-" + rec.cash + Environment.NewLine);
                //sb.Append("找零：" + "\t\t" + "-" + rec.change + Environment.NewLine);
            }
            else
            {
                sb.Append("刷卡支付：" + "\t" + rec.cardPay + Environment.NewLine);
                sb.Append("现金支付：" + "\t" + rec.cash + Environment.NewLine);
                sb.Append("找零：" + "\t\t" + rec.change + Environment.NewLine);
                sb.Append("收款员:" + rec.syybh + Environment.NewLine);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 模块4 页脚
        /// </summary>
        /// <returns></returns>
        public string GetPrintStr3()
        {
            StringBuilder sb = new StringBuilder();
            if (isrefund)
            {
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
            }
            sb.Append("----------------------------------------------------------" + Environment.NewLine);
            if (!isrefund)
            {
                if (!string.IsNullOrWhiteSpace(rec.mcard))
                {
                    if (!string.IsNullOrWhiteSpace(rec.mName))
                    {
                        sb.Append("持卡人：" + rec.mName + Environment.NewLine);
                        sb.Append("交易前余额：" + rec.iniBalance + Environment.NewLine);
                        sb.Append("交易后余额：" + rec.endBalance + Environment.NewLine);
                    }
                    else
                        sb.Append("会员卡" + Environment.NewLine);
                    //sb.Append("会员卡号：" + Environment.NewLine);
                    //sb.Append(rec.mcard + Environment.NewLine );
                }
            }
            sb.Append(Environment.NewLine + "     请当面点清所购商品和找零，    " + Environment.NewLine);
            sb.Append("            并妥善保管此单据，   " + Environment.NewLine);
            sb.Append("         谢谢惠顾,欢迎下次光临。   " + Environment.NewLine + Environment.NewLine);
            sb.Append("时间：" + rec.printTime.ToString() + Environment.NewLine);
            return sb.ToString();
        }

        #endregion

        #region 打印解款单
        /// <summary>
        /// 打印解款单
        /// </summary>
        public void PrintCash()
        {
            //打印预览 
            PrintPreviewDialog ppd = new PrintPreviewDialog();

            PrintDocument pd = new PrintDocument();

            //设置边距
            Margins margin = new Margins(20, 20, 20, 20);

            pd.DefaultPageSettings.Margins = margin;

            //纸张设置默认
            PaperSize pageSize = new PaperSize("First custom size", getYc(58), 600);
            pd.DefaultPageSettings.PaperSize = pageSize;

            //打印事件设置
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintCash);

            //打印预览
            //ppd.Document = pd;
            //ppd.ShowDialog();

            try
            {
                PrintController printController = new StandardPrintController();
                pd.PrintController = printController;
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
            }
        }

        private void pd_PrintCash(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            PrintCashinit(e);
        }

        /// <summary>
        /// 
        /// 解款单初始化参数
        /// </summary>
        /// <param name="e"></param>
        private void PrintCashinit(System.Drawing.Printing.PrintPageEventArgs e)
        {
            Brush brush = new SolidBrush(Color.Black);

            string title = "收银员解款单 单位（元）";
            Font titleFont = new Font("新宋体", 11, FontStyle.Bold);

            //打印解款单标题
            e.Graphics.DrawString(title, titleFont, brush, 8, 3);

            //打印票面及张数
            Font cashFont = new Font("Arial", 9, FontStyle.Bold);
            e.Graphics.DrawString(GetPrintCash(), cashFont, brush, 8, 10);

            //打印现金、支票和总计，字体比支付大
            Font zjFont = new Font("新宋体", 13, FontStyle.Bold);
            e.Graphics.DrawString(GetCashZJStr(), zjFont, brush, 8, 183);

            //打印票尾
            Font footFont = new Font("Arial", 7, FontStyle.Bold);
            e.Graphics.DrawString(GetCashFootStr(), footFont, brush, 8, 253);
        }

        /// <summary>
        /// 取得票面和张数
        /// </summary>
        /// <returns></returns>
        public string GetPrintCash()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append("----------------------------------------------------------" + Environment.NewLine);
            sb.Append("票面" + "\t" + "张数" + "\t" + "票面" + "\t" + "张数" + Environment.NewLine);
            sb.Append("100.00" + "\t" + GetNum(CashNum["100"]) + "\t" + "50.00" + "\t" + GetNum(CashNum["50"]) + Environment.NewLine);
            sb.Append("20.00" + "\t" + GetNum(CashNum["20"]) + "\t" + "10.00" + "\t" + GetNum(CashNum["10"]) + Environment.NewLine);
            sb.Append("5.00" + "\t" + GetNum(CashNum["5"]) + "\t" + "2.00" + "\t" + GetNum(CashNum["2"]) + Environment.NewLine);
            sb.Append("1.00" + "\t" + GetNum(CashNum["1"]) + "\t" + "0.50" + "\t" + GetNum(CashNum["0.5"]) + Environment.NewLine);
            sb.Append("0.20" + "\t" + GetNum(CashNum["0.2"]) + "\t" + "0.10" + "\t" + GetNum(CashNum["0.1"]) + Environment.NewLine);
            sb.Append("0.05" + "\t" + GetNum(CashNum["0.05"]) + "\t" + "0.02" + "\t" + GetNum(CashNum["0.02"]) + Environment.NewLine);
            sb.Append("0.01" + "\t" + GetNum(CashNum["0.01"]) + Environment.NewLine);
            sb.Append("----------------------------------------------------------" + Environment.NewLine);
            return sb.ToString();
        }

        public string GetNum(int num)
        {
            if (num == 0)
                return "";
            else
                return num.ToString();
        }

        /// <summary>
        /// 解款单金额汇总
        /// </summary>
        /// <returns></returns>
        public string GetCashZJStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("现金：  " + Cash + Environment.NewLine);
            sb.Append("支票：  " + Check + Environment.NewLine);
            sb.Append("总计：  " + (Cash + Check).ToString() + Environment.NewLine);
            return sb.ToString();
        }

        /// <summary>
        /// 解款单页脚
        /// </summary>
        /// <returns></returns>
        public string GetCashFootStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("----------------------------------------------------------" + Environment.NewLine);
            sb.Append("机号:" + JH + "   " + "收款员:" + staff.Staff_no + " " + staff.Staff_name + Environment.NewLine);
            sb.Append("时间：" + DateTime.Now.ToString() + Environment.NewLine);
            return sb.ToString();
        }

        #endregion

        #region 打印当日销售汇总

        /// <summary>
        /// 打印当日销售汇总
        /// </summary>
        public void PrintSalesDay()
        {
            //打印预览 
            PrintPreviewDialog ppd = new PrintPreviewDialog();

            PrintDocument pd = new PrintDocument();

            //设置边距
            Margins margin = new Margins(20, 20, 20, 20);

            pd.DefaultPageSettings.Margins = margin;

            //纸张设置默认
            PaperSize pageSize = new PaperSize("First custom size", getYc(58), 600);
            pd.DefaultPageSettings.PaperSize = pageSize;

            //打印事件设置
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintSalesDay);

            //打印预览
            //ppd.Document = pd;
            //ppd.ShowDialog();

            try
            {
                PrintController printController = new StandardPrintController();
                pd.PrintController = printController;
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
            }
        }

        private void pd_PrintSalesDay(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            PrintSalesDayinit(e);
        }

        /// <summary>
        /// 汇总单初始化参数
        /// </summary>
        /// <param name="e"></param>
        private void PrintSalesDayinit(System.Drawing.Printing.PrintPageEventArgs e)
        {
            Brush brush = new SolidBrush(Color.Black);

            string title = "当日销售金额汇总 （元）";
            Font titleFont = new Font("新宋体", 11, FontStyle.Bold);

            //打印标题
            e.Graphics.DrawString(title, titleFont, brush, 8, 3);

            //打印票头
            Font footFont = new Font("Arial", 9, FontStyle.Bold);
            e.Graphics.DrawString(GetSalesDayFootStr(), footFont, brush, 8, 20);

            //打印各种支付方式和总计，字体比支付大
            Font zjFont = new Font("新宋体", 13, FontStyle.Bold);
            e.Graphics.DrawString(GetSalesDayStr(), zjFont, brush, 8, 100);

        }

        /// <summary>
        /// 汇总单数据
        /// </summary>
        /// <returns></returns>
        public string GetSalesDayStr()
        {
            decimal salesDayCash = 0;
            decimal salesDayCard = 0;
            decimal salesDayOther = 0;
            decimal salesDayTotal = 0;
            decimal moling = 0;

            List<Sales> salesDay = new SalesBLL().GetSalesDay();
            if (salesDay != null)
            {
                //tzm 为 1 的数据没有抹零到角
                var money =
                   from s in salesDay
                   group s by new { s.tzm, s.xph, s.jsfsbh } into g
                   select new
                   {
                       tzm = g.Key.tzm,
                       xph = g.Key.xph,
                       jsfsbh = g.Key.jsfsbh,
                       ssje = Math.Truncate(g.Sum(o => o.ssje) * 10) / 10
                   };

                //原来的方法
                salesDayCash = money.Where(o => o.jsfsbh == "0").Sum(s => s.ssje);
                salesDayCard = money.Where(o => o.jsfsbh == "5").Sum(s => s.ssje);
                salesDayOther = money.Where(o => (o.jsfsbh != "0" && o.jsfsbh != "5")).Sum(s => s.ssje);
                salesDayTotal = money.Sum(o => o.ssje);

                #region  /* 2017-8-15 暂保留此段注释(另一种统计方式：从当日的销售小票中算当日销售金额汇总) */
                /* 2017-8-15 暂保留此段注释
                //从当日的销售小票中算当日销售金额汇总
                //MessageBox.Show(DateTime.Now.ToString("yyyyMMdd"));
                List<Receipt> rList = new List<Receipt>();
                try
                {

                    BinaryFormatter formatter = new BinaryFormatter();
                    using (Stream input = File.OpenRead(GlobalParams.DataPath + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\"
                        + DateTime.Now.ToString("yyyyMMdd") + ".rec"))
                    {
                        rList = (List<Receipt>)formatter.Deserialize(input);
                    }
                }
                catch
                {
                    MessageBox.Show("未找到当日销售数据");
                }
                if (rList != null)
                {
                    foreach (Receipt rec in rList)
                    {
                        if (!string.IsNullOrEmpty(rec.mName))
                        {
                            salesDayCard += rec.cardPay;
                        }
                        else
                        {
                            salesDayOther += rec.cardPay;

                        }
                        salesDayCash += rec.cash;
                        moling += rec.change;
                    }
                    salesDayCash = salesDayCash - moling;
                    salesDayTotal = salesDayCash + salesDayCard + salesDayOther;
                    // MessageBox.Show("现金"+salesDayCash+"刷卡"+salesDayCard+"其他"+salesDayOther+"总计"+salesDayTotal);
                }*/
                #endregion
            }
            else
                Log.WriteErrorLog("获取当日销售明细中的支付数据", "", "GetSalesDayStr()");

            StringBuilder sb = new StringBuilder();
            sb.Append("现金：  " + salesDayCash + Environment.NewLine);
            sb.Append("刷卡：  " + salesDayCard + Environment.NewLine);
            sb.Append("其它：  " + salesDayOther + Environment.NewLine);
            sb.Append("总计：  " + salesDayTotal + Environment.NewLine);
            return sb.ToString();
        }

        /// <summary>
        /// 汇总单页脚
        /// </summary>
        /// <returns></returns>
        public string GetSalesDayFootStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("----------------------------------------------------------" + Environment.NewLine);
            sb.Append("机号:" + JH + Environment.NewLine);
            sb.Append("收款员:" + staff.Staff_no + " " + staff.Staff_name + Environment.NewLine);
            sb.Append("时间：" + DateTime.Now.ToString() + Environment.NewLine);
            sb.Append("----------------------------------------------------------" + Environment.NewLine);
            return sb.ToString();
        }
        #endregion
    }
}
